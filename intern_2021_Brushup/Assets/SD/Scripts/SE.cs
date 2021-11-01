/*-------------------------------------------------------
 * 
 *      [SE.cs]
 *      効果音を鳴らす
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
    [Header("鳴らす効果音を指定")]
    [SerializeField] private AudioClip[] _audioClips;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(int index)
    {
        _audioSource.PlayOneShot(_audioClips[index]);
    }
}
