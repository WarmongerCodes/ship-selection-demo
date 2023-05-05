using System.Collections;
using UnityEngine;

namespace Audio
{
    public class AudioHandler : MonoBehaviour
    {
        public AudioClip btnPrimarySfx;
        public AudioClip btnSecondarySfx;
        public AudioClip btnErrorSfx;
        public AudioClip bgMusicClip;

        public AudioSource sfxSource;
        public AudioSource musicSource;

        private void Awake()
        {
            var sources = GetComponents<AudioSource>();
            sfxSource = sources[0];
            musicSource = sources[1];
        }

        internal void PlayPrimarySfx() => sfxSource.PlayOneShot(btnPrimarySfx);

        internal void PlaySecondarySfx() => sfxSource.PlayOneShot(btnSecondarySfx);
        
        internal void PlayErrorSfx() => sfxSource.PlayOneShot(btnErrorSfx);

        internal void PlayBgMusic() => StartCoroutine(CrossFadeInMusic(musicSource, bgMusicClip));

        private static IEnumerator CrossFadeInMusic(AudioSource source, AudioClip clip)
        {
            float time = 0f, duration = 2f;

            source.clip = clip;
            source.loop = true;
            source.Play();

            while (time < duration)
            {
                source.volume = Mathf.Lerp(0, .5f, time / duration);
                time += Time.deltaTime;

                yield return null;
            }
        }
    }
}
