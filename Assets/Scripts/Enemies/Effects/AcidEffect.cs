using UnityEngine;

namespace Enemies.Effects
{
    public class AcidEffect : MonoBehaviour
    {
        public float _time = 5.0f;
        private void Start()
        {
            Destroy(this.gameObject, _time);
        }

        private void Update()
        {
            transform.Translate(Vector2.right * 3 * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                IDamageable hit = other.GetComponent<IDamageable>();

                if (hit != null)
                {
                    hit.Damage();
                    Destroy(this.gameObject);
                }
            }
        }
    }
}

