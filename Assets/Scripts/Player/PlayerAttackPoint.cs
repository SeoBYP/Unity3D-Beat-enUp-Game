using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
namespace Charecters
{
    public class PlayerAttackPoint : MonoBehaviour
    {
        enum PunchHitSound
        {
            PunchHit1,
            PunchHit2,
            PunchHit3,
            PunchHit4,
            PunchHit5,
        }

        public float _radius = 1;
        public LayerMask enemyLayer;
        private PunchHitSound hitSound = PunchHitSound.PunchHit1;
        private void Update()
        {
            DetectCollision();
        }

        void DetectCollision()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _radius, enemyLayer);

            if (hits.Length > 0)
            {
                Vector3 hitFx_Pos = hits[0].transform.position;
                hitFx_Pos.y += 1.3f;

                if (hits[0].transform.forward.x > 0)
                    hitFx_Pos.x += 0.3f;
                else if (hits[0].transform.forward.x < 0)
                    hitFx_Pos.x -= 0.3f;

                for (int i = 0; i < hits.Length; i++)
                {
                    CheckAttack(hits[i], hitFx_Pos);
                }
            }
            gameObject.SetActive(false);
        }

        private void CheckAttack(Collider collider, Vector3 HitPos)
        {
            int CharacterID = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteCharacterID;
            BaseCharecterController _enemy = collider.GetComponent<BaseCharecterController>();
            float damage = Utils.SetAttackAmount(CharacterID);
            if (_enemy.IsKnockDown == false)
            {
                ResourcesManager.Instance.Instantiate("Hit_FX_Prefabs/HitEffect").transform.position = HitPos;
                GameAudioManager.Instance.Play2DSound(GetHitSoundName(hitSound));
                hitSound++;
            }

            if (gameObject.CompareTag(Tags.LEFT_ARM_TAG) || gameObject.CompareTag(Tags.LEFT_LEG_TAG))
            {
                _enemy.ApplyDamage(damage, true);
            }
            else if (gameObject.CompareTag(Tags.KNOCKBACK_TAG))
            {
                _enemy.ApplyKnockBack(damage, true);
            }
            else
            {
                _enemy.ApplyDamage(damage, false);
            }
            
        }

        string GetHitSoundName(PunchHitSound sound)
        {
            if (sound.GetHashCode() >= 5)
                sound = PunchHitSound.PunchHit1;
            string name = System.Enum.GetName(typeof(PunchHitSound), sound);
            return name;
        }


    }
}