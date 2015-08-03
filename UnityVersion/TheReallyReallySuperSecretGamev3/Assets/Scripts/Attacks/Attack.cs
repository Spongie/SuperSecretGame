using UnityEngine;
using System.Collections.Generic;
using TheSuperTrueRealCV.Utilities;
using Assets.Scripts.Utility;
using System.Linq;
using Assets.Scripts.Attacks;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Timer))]
[RequireComponent(typeof(AttackDamageScaling))]
public class Attack : MonoBehaviour 
{
    public bool IsMeleeAttack = false;
    public bool IsRotating = false;
    public bool ApplyGravity = false;
    public float secondsToLive;
    public float secondsHitReset;
    public Timer lifeTimer;
    public GameObject Owner;
    public AttackEffect[] Saker;
    private Rigidbody2D ivRigidBody;

    void OnEnable()
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
        if (piEntity.GetComponent<Attack>() != null)
            return false;

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
        if (lifeTimer.Done && !IsMeleeAttack)
            Destroy(gameObject);

        foreach (var timer in EntitiesHit)
        {
            timer.Value.Update(Time.deltaTime);
        }

        if (IsRotating)
            Rotate();
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, 300 * Time.deltaTime));
    }

    void AddEntityToHit(GameObject piEntity)
    {
        if (EntitiesHit.ContainsKey(piEntity))
            EntitiesHit[piEntity].Restart();
        else
            EntitiesHit.Add(piEntity, new ManualTimer(secondsHitReset));
    }

    void OnCollisionEnter2D(Collision2D coll) 
    {
        if (DiesOnCollision)
            Destroy(gameObject);

        if (CanHitEntity(coll.gameObject))
        {
            AddEntityToHit(coll.gameObject);
            DamageCalculator.DealDamage(Owner, coll.gameObject, Scaling, GetAttackEffects());
        }
    }


    public IEnumerable<AttackEffect> GetAttackEffects()
    {
        return Saker.Where(effect => effect.Name != "None");
    }
}
