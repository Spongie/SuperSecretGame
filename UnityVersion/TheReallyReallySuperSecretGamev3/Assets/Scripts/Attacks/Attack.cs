using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Utility;
using System.Linq;

namespace Assets.Scripts.Attacks
{
    //[RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Timer))]
    [RequireComponent(typeof(AttackDamageScaling))]
    public class Attack : MonoBehaviour
    {
        public bool IsMeleeAttack = false;
        public bool IsCurseAreaAttack = false;
        public bool CurseSpawnOnCast = true;
        public bool IsRotating = false;
        public bool ApplyGravity = false;
        public float secondsToLive;
        public float secondsHitReset;
        public Timer lifeTimer;
        public GameObject Owner;
        public AttackEffect[] Saker;
        public GameObject CursedArea;
        public Vector2 Speed = Vector2.zero;
        public bool ThrewToRight = false;
        private Rigidbody2D ivRigidBody;
        private List<string> ivGroundTags;

        void OnEnable()
        {
            ivRigidBody = GetComponent<Rigidbody2D>();

            if(ivRigidBody != null)
                ivRigidBody.gravityScale = ApplyGravity ? 1 : 0;

            EntitiesHit = new Dictionary<GameObject, ManualTimer>();
            Scaling = GetComponent<AttackDamageScaling>();
            lifeTimer = GetComponent<Timer>();

            EntitiesHit = new Dictionary<GameObject, ManualTimer>();

            lifeTimer.Restart(secondsToLive);

            ivGroundTags = new List<string>();
            ivGroundTags.Add("ground");
            ivGroundTags.Add("boost");
            ivGroundTags.Add("boostright");
            ivGroundTags.Add("boostleft");
            ivGroundTags.Add("door");

            if (CurseSpawnOnCast && IsCurseAreaAttack)
                SpawnCurseAttack();

            if (Speed != Vector2.zero && ivRigidBody != null)
            {
                var xModifier = ThrewToRight ? 1 : -1;

                var realSpeed = new Vector2(xModifier * Speed.x, Speed.y);
                Speed = realSpeed;
                ivRigidBody.velocity = Speed;
            }
        }

        private void SpawnCurseAttack()
        {
            Instantiate(CursedArea);
            gameObject.SetActive(false);
        }

        //this is a nice comment
        public Dictionary<GameObject, ManualTimer> EntitiesHit { get; set; }

        private AttackDamageScaling Scaling;

        public bool Bouncing;

        public int BouncesLeft;

        public bool DiesOnCollision;

        public AttackTarget TargetType;

        public bool CanHitEntity(GameObject piEntity)
        {
            if (piEntity.GetComponent<Attack>() != null)
                return false;

            if (HitGround(piEntity.tag))
                return false;

            if (piEntity == Owner && TargetType != AttackTarget.Everything)
                return false;

            if (!EntitiesHit.ContainsKey(piEntity))
                return true;

            return EntitiesHit[piEntity].Done;
        }

        private bool HitGround(string piTag)
        {
            return ivGroundTags.Contains(piTag.ToLower());
        }

        void Update()
        {
            if (Speed != Vector2.zero && ivRigidBody != null)
                ivRigidBody.velocity = Speed;

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

        void OnTriggerEnter2D(Collider2D coll)
        {
            HandleCollision(coll.gameObject);
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            HandleCollision(coll.gameObject);
        }

        private void HandleCollision(GameObject otherGameObject)
        {
            Logger.Log(string.Format("Hit gameobject {0} ", otherGameObject.name));

            if (CanHitEntity(otherGameObject))
            {
                if (IsCurseAreaAttack)
                    SpawnCurseAttack();
                else
                {
                    AddEntityToHit(otherGameObject);
                    DamageController.DoAttack(Owner, otherGameObject, Scaling, GetAttackEffects());
                }
            }

            if (DiesOnCollision)
                Destroy(gameObject);
        }

        public void StartMeleeAttack()
        {
            gameObject.SetActive(true);
        }

        public void StopMeleeAttack()
        {
            gameObject.SetActive(false);
        }

        public IEnumerable<AttackEffect> GetAttackEffects()
        {
            return Saker.Where(effect => effect.Name != "None");
        }
    }
}