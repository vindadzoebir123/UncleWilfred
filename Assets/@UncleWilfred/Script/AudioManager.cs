using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UncleWilfred
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        public AudioSource source;

        void Awake()
        {
            Instance = this;
        }

        public void Play(AudioClip clip)
        {
            source.clip = clip;
            source.Play();

            Debug.Log("<color=red>PLAY AUDIO : " + clip.name + "</color>");
        }
    }
}