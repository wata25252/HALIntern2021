using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TK
{
    public class Compass : MonoBehaviour
    {
        [SerializeField]
        public bool _isHitGround;
        // Start is called before the first frame update
        void Start()
        {
            _isHitGround = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                _isHitGround = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                _isHitGround = false;
            }
        }
    }
}
