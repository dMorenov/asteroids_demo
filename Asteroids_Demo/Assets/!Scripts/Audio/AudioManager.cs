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

        private List<AudioSource> _audioSources = new List<AudioSource>();

        private AudioSource _backgroundSource;

        public override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            _backgroundSource = gameObject.AddComponent<AudioSource>();
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
            //_backgroundSource.Stop();

            _backgroundSource.clip = menuClip;
            _backgroundSource.loop = true;
            _backgroundSource.Play();
        }
        public void SetIngameBackground()
        {
            //_backgroundSource.Stop();

            _backgroundSource.clip = ingameClip;
            _backgroundSource.loop = true;
            _backgroundSource.Play();
        }
    }
}