using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TheSuperTrueRealCV.Utilities;
using Assets.Scripts.Utility;
using CVCommon.Utility;

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
        Scaling = GetComponent<AttackDamageScaling>();
        lifeTimer = GetComponent<Timer>();
        Scaling = GetComponent<AttackDamageScaling>();

        EntitiesHit = new Dictionary<GameObject, ManualTimer>();
    }

    //this is a nice comment
    public Dictionary<GameObject, ManualTimer> EntitiesHit { get; set; }

    private AttackDamageScaling Scaling;

    public bool Bouncing;

    public int BouncesLeft;

    public bool DiesOnCollision;

    public AttackTarget TargetType { get; set; }

    public bool CanHitEntity(GameObject piEntity)
    {
        if (piEntity.tag.ToLower() == "ground")
            return false;

        if (piEntity == Owner && TargetType != AttackTarget.Everything)
            return false;

        if (!EntitiesHit.ContainsKey(piEntity))
            return true;

        return EntitiesHit[piEntity].Done;
    }

    void Update()
    {
        if (lifeTimer.Done)
            Destroy(gameObject);

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

        if(CanHitEntity(coll.gameObject))
        {
            AddEntityToHit(coll.gameObject);
            var targetStats = coll.gameObject.GetComponent<Stats>();
            float dmg = DamageCalcualtor.CalculateDamage(Owner.GetComponent<Stats>().stats, targetStats.stats, Scaling);
            targetStats.stats.DealDamage(dmg);
        }
    }
}
