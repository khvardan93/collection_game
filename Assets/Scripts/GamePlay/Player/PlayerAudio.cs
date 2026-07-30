using UnityEngine;

namespace GamePlay
{
    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioFootsteps;
        [SerializeField] private AudioSource _landingAudio;
        [SerializeField] private AudioSource _audioFoley;
        [Range(0, 1)] [SerializeField] private float _footstepAudioVolume = 0.5f;

        private void Awake()
        {
            _audioFootsteps.volume = _footstepAudioVolume;
        }
        
        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {

                if (_audioFootsteps != null)
                    _audioFootsteps.Play();
                if (_audioFoley != null)
                    _audioFoley.Play();
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (_landingAudio != null)
                    _landingAudio.Play();

            }
        }
    }
}