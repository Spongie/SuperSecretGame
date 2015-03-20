using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TheSuperTrueRealCV.Utilities;
using Assets.Scripts.Utility;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Timer))]
[RequireComponent(typeof(AttackDamageScaling))]
public class Attack : MonoBehaviour 
{
    public float secondsToLive;
    public float secondsHitReset;
    public Timer lifeTimer;
    private Rigidbody2D ivRigidBody;
    public bool ApplyGravity = false;
    public GameObject Owner;

    void Start()
    {
        ivRigidBody = GetComponent<Rigidbody2D>();
        ivRigidBody.gravityScale = ApplyGravity ? 1 : 0;

        EntitiesHit = new Dictionary<GameObject, ManualTimer>();
        Scaling = new AttackDamageScaling();
        lifeTimer = GetComponent<Timer>();
        Scaling = GetComponent<AttackDamageScaling>();

        EntitiesHit = new Dictionary<GameObject, ManualTimer>();
    }

    public Dictionary<GameObject, ManualTimer> EntitiesHit { get; set; }

    private AttackDamageScaling Scaling;

    public bool Bouncing;

    public int BouncesLeft;

    public bool DiesOnCollision;

    public AttackTarget TargetType { get; set; }

    public bool CanHitEntity(GameObject piEntity)
    {
        if (piEntity == Owner && TargetType != AttackTarget.Everything)
            return false;

        if (!EntitiesHit.ContainsKey(piEntity))
            return true;

        return EntitiesHit[piEntity].Done;
    }

    void Update()
    {
        foreach (var timer in EntitiesHit)
        {
            timer.Value.Update(Time.deltaTime);
        }

        transform.Rotate(new Vector3(0, 0, 300 * Time.deltaTime));
    }

    void AddEntityToHit(GameObject piEntity)
    {
        if (EntitiesHit.ContainsKey(piEntity))
            EntitiesHit[piEntity].Restart();
        else
            EntitiesHit.Add(piEntity, new ManualTimer(secondsHitReset));
    }

    void OnCollisionStay2D(Collision2D coll) 
    {
        if (DiesOnCollision)
            Destroy(gameObject);

        if (coll.gameObject.tag == "Ground")
            HandleGroundCollision();

        if(CanHitEntity(coll.gameObject))
        {
            AddEntityToHit(coll.gameObject);
            var targetController = coll.gameObject.GetComponent<Character_Controller>();
            float dmg = DamageCalcualtor.CalculateDamage(Owner.GetComponent<Character_Controller>().CurrentStats, targetController.CurrentStats, Scaling);
            coll.gameObject.GetComponent<Character_Controller>().CurrentStats.DealDamage(dmg);
        }
    }

    private void HandleGroundCollision()
    {
        if (Bouncing)
        {
            BouncesLeft--;
            ivRigidBody.velocity = new Vector2(-0.8f * ivRigidBody.velocity.x, -0.8f * ivRigidBody.velocity.y);

            if (BouncesLeft <= 0)
                Destroy(gameObject);
        }
    }
}
