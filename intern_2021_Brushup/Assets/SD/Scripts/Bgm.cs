using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class Bgm : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _audioClip;
        // Start is called before the first frame update
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            Play();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Play()
        {
            int r = Random.Range(0, _audioClip.Length);
            _audioSource.clip = _audioClip[r];
            _audioSource.Play();
        }
    }
}
