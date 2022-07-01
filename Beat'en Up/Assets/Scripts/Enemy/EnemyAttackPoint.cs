using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
namespace Charecters
{

    public class EnemyAttackPoint : MonoBehaviour
    {
        public float _radius = 1;
        public LayerMask playerlayer;

        private PlayerController _player;
        private float damage;
        private void Update()
        {
            DetectCollision();
        }

        public void SetDamage(int charID)
        {
            damage = CharacterStatManager.Instance.GetFloat(charID, CharacterIndex.ATTACK);
        }

        void DetectCollision()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _radius, playerlayer);

            if (hits.Length > 0)
            {
                _player = hits[0].GetComponent<PlayerController>();
                Vector3 hitFx_Pos = hits[0].transform.position;
                hitFx_Pos.y += 1.3f;

                if (hits[0].transform.forward.x > 0)
                    hitFx_Pos.x += 0.3f;
                else if (hits[0].transform.forward.x < 0)
                    hitFx_Pos.x -= 0.3f;

                Managers.ResourcesManager.Instance.Instantiate("Hit_FX_Prefabs/HitEffect").transform.position = hitFx_Pos;
                _player.ApplyDamage(damage);
            }
            gameObject.SetActive(false);
        }
    }
}
