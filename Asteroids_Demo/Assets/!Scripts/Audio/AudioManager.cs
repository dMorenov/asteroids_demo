using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        public int MaxAudioSources = 5;

        [SerializeField] private AudioClip menuClip;
        [SerializeField] private AudioClip ingameClip;
        [SerializeField] private AudioSource backgroundSource;

        private List<AudioSource> _audioSources = new List<AudioSource>();

        public override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            if (backgroundSource == null)
                backgroundSource = gameObject.AddComponent<AudioSource>();

            if (!TryGetComponent<AudioListener>(out var listener))
                gameObject.AddComponent<AudioListener>();
        }

        public void PlayClip(AudioClip clip, bool isLoop = false)
        {
            foreach (var source in _audioSources)
            {
                if (!source.isPlaying)
                {
                    source.clip = clip;
                    source.loop = isLoop;
                    source.Play();
                    return;
                }
            }

            if (_audioSources.Count < MaxAudioSources)
            {
                var source = gameObject.AddComponent<AudioSource>();
                source.clip = clip;
                source.Play();

                _audioSources.Add(source);
            }
        }

        public void StopLoop(AudioClip clip)
        {
            foreach (var source in _audioSources)
            {
                if (source.isPlaying && source.clip == clip && source.loop)
                {
                    source.loop = false;
                    source.Stop();
                    return;
                }
            }
        }

        public void SetMenuBackground()
        {
            backgroundSource.clip = menuClip;
            backgroundSource.loop = true;
            backgroundSource.Play();
        }
        public void SetIngameBackground()
        {
            backgroundSource.clip = ingameClip;
            backgroundSource.loop = true;
            backgroundSource.Play();
        }
    }
}