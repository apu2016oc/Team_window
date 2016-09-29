using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace VRStandardAssets.Utils
{
    // This class is used to fade the entire screen to black (or
    // any chosen colour).  It should be used to smooth out the
    // transition between scenes or restarting of a scene.
    public class VRCameraFade : MonoBehaviour
    {
        public event Action OnFadeComplete;                             // This is called when the fade in or out has finished.


        [SerializeField]
        private Image m_FadeImage, m_ChangeSceneImage;                  // Reference to the image that covers the screen.
        [SerializeField] private AudioMixerSnapshot m_DefaultSnapshot;  // Settings for the audio mixer to use normally.
        [SerializeField] private AudioMixerSnapshot m_FadedSnapshot;    // Settings for the audio mixer to use when faded out.
        [SerializeField]
        private Color m_FadeColor = Color.black,
            m_SceneChanegeColor = Color.white;                          // The colour the image fades out to.
        [SerializeField] private float m_FadeDuration = 3.0f;           // How long it takes to fade in seconds.
        [SerializeField] private bool m_FadeInOnSceneLoad = false;      // Whether a fade in should happen as soon as the scene is loaded.
        [SerializeField] private bool m_FadeInOnStart = false;          // Whether a fade in should happen just but Updates start.
        [SerializeField] private GameObject m_particle;

        private bool m_IsFading;                                        // Whether the screen is currently fading.
        private float m_FadeStartTime;                                  // The time when fading started.
        private Color m_FadeOutColor,m_SceneChangeOutColor;             // This is a transparent version of the fade colour, it will ensure fading looks normal.
        

        public bool IsFading { get { return m_IsFading; } }
        public static bool IsFadeIn = false;

        private void Awake()
        {
            m_FadeOutColor = new Color(m_FadeColor.r, m_FadeColor.g, m_FadeColor.b, 0f);
            m_SceneChangeOutColor = new Color(m_SceneChanegeColor.r, m_SceneChanegeColor.g, m_SceneChanegeColor.b, 1f);
            m_FadeImage.enabled = true;
        }


        private void Start()
        {
            //m_particle = GameObject.Find("FlyerTracersParticles");
            // If applicable set the immediate colour to be faded out and then fade in.
            if (m_FadeInOnStart)
            {
                m_FadeImage.color = m_FadeColor;
                m_ChangeSceneImage.color = m_SceneChanegeColor;
                FadeIn(true);
            }
        }

        private void Update()
        {
           if(ApplicationManager.changeSceneSwich == true)
            {
                m_particle.SetActive(true);
                StartCoroutine(BeginFade(m_SceneChangeOutColor, m_SceneChanegeColor, m_FadeDuration));
            }
        }

        private void OnLevelWasLoaded()
        {
            // If applicable set the immediate colour to be faded out and then fade in.
            if (m_FadeInOnSceneLoad)
            {
                m_FadeImage.color = m_FadeColor;
                m_ChangeSceneImage.color = m_SceneChanegeColor;
                FadeIn(true);
            }
        }
        
        // Since no duration is specified with this overload use the default duration.
        public void FadeOut(bool fadeAudio)
        {
            FadeOut(m_FadeDuration, fadeAudio);
        }


        public void FadeOut(float duration, bool fadeAudio)
        {
            // If not already fading start a coroutine to fade from the fade out colour to the fade colour.
            if (m_IsFading)
                return;
            StartCoroutine(BeginFade(m_FadeOutColor, m_FadeColor, duration));
            
            // Fade out the audio over the same duration.
            if(m_FadedSnapshot && fadeAudio)
                m_FadedSnapshot.TransitionTo (duration);
        }


        // Since no duration is specified with this overload use the default duration.
        public void FadeIn(bool fadeAudio)
        {
            FadeIn(m_FadeDuration, fadeAudio);
        }


        public void FadeIn(float duration, bool fadeAudio)
        {
            // If not already fading start a coroutine to fade from the fade colour to the fade out colour.
            if (m_IsFading)
                return;
            StartCoroutine(BeginFade(m_FadeColor, m_FadeOutColor, duration));
            //Gamestart
            //StartCoroutine(BeginFade(m_SceneChangeOutColor, m_SceneChanegeColor, m_FadeDuration));
            // Fade in the audio over the same duration.
            if (m_DefaultSnapshot && fadeAudio)
                m_DefaultSnapshot.TransitionTo (duration);
        }


        public IEnumerator BeginFadeOut (bool fadeAudio)
        {
            // Fade out the audio over the default duration.
            if(m_FadedSnapshot && fadeAudio)
                m_FadedSnapshot.TransitionTo (m_FadeDuration);

            yield return StartCoroutine(BeginFade(m_FadeOutColor, m_FadeColor, m_FadeDuration));
        }


        public IEnumerator BeginFadeOut(float duration, bool fadeAudio)
        {
            // Fade out the audio over the given duration.
            if(m_FadedSnapshot && fadeAudio)
                m_FadedSnapshot.TransitionTo (duration);

            yield return StartCoroutine(BeginFade(m_FadeOutColor, m_FadeColor, duration));
        }


        public IEnumerator BeginFadeIn (bool fadeAudio)
        {
            // Fade in the audio over the default duration.
            if(m_DefaultSnapshot && fadeAudio)
                m_DefaultSnapshot.TransitionTo (m_FadeDuration);

            yield return StartCoroutine(BeginFade(m_FadeColor, m_FadeOutColor, m_FadeDuration));
        }


        public IEnumerator BeginFadeIn(float duration, bool fadeAudio)
        {
            // Fade in the audio over the given duration.
            if(m_DefaultSnapshot && fadeAudio)
                m_DefaultSnapshot.TransitionTo (duration);

            yield return StartCoroutine(BeginFade(m_FadeColor, m_FadeOutColor, duration));
        }


        private IEnumerator BeginFade(Color startCol, Color endCol, float duration)
        {
            // Fading is now happening.  This ensures it won't be interupted by non-coroutine calls.
            m_IsFading = true;

            // Execute this loop once per frame until the timer exceeds the duration.
            float timer = 0f;
            while (timer <= duration)
            {
                // Set the colour based on the normalised time.
                if (ApplicationManager.changeSceneSwich == false)
                    m_FadeImage.color = Color.Lerp(startCol, endCol, timer / duration);
                //Gamestart
                else
                    m_ChangeSceneImage.color = Color.Lerp(startCol, endCol, 1 - timer / duration);
                
                // Increment the timer by the time between frames and return next frame.
                timer += Time.deltaTime;
                yield return null;
            }

            // Fading is finished so allow other fading calls again.
            m_IsFading = false;
            if (ApplicationManager.changeSceneSwich)
                IsFadeIn = true;
            // If anything is subscribed to OnFadeComplete call it.
            if (OnFadeComplete != null)
                OnFadeComplete();
        }
    }
}