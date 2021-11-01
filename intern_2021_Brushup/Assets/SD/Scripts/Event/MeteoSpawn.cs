using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class MeteoSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject _meteo;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Spawn()
        {
            GameObject meteo = Instantiate(_meteo, this.transform);
            Vector3 random = new Vector3();
            random.x = Random.Range(-0.1f, 0.1f);
            random.y = 0.0f;
            random.z = Random.Range(-0.1f, 0.1f);
            Vector3 dir = meteo.transform.forward + random;
            meteo.GetComponent<Rigidbody>().AddForce(dir * 5000.0f);
        }
    }
}
