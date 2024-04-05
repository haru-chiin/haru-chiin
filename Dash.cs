using System.Collections;
using UnityEngine;

namespace Gamekit3D
{
    public class Dash : MonoBehaviour
    {
        PlayerController moveScript;
        Damageable damageable;

        public float dashSpeed;
        public float dashTime;
        public float dashInvulnerabilityTime;
        public float cooldownEndTime;
        private float dashCooldownTime = 3f;

        private void Start()
        {
            moveScript = GetComponent<PlayerController>();
            damageable = GetComponent<Damageable>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= cooldownEndTime)
            {
                StartCoroutine(GoDash());
                cooldownEndTime = Time.time + dashCooldownTime;
            }
        }

        IEnumerator GoDash()
        {
            damageable.isInvulnerable = true;

            float startTime = Time.time;
            Vector3 originalPosition = transform.position;
            Vector3 moveDirection = transform.forward;

            // Invulnerability duration
            float invulnerabilityEndTime = startTime + dashInvulnerabilityTime;

            while (Time.time < startTime + dashTime)
            {
                transform.position += moveDirection * dashSpeed * Time.deltaTime;

                if (Time.time >= invulnerabilityEndTime)
                {
                    damageable.isInvulnerable = false;
                }

                yield return null;
            }
        }
    }
}
