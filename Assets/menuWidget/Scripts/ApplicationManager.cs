using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ApplicationManager : MonoBehaviour {
    public static GameObject MainCam;
	private GameObject SubCam,m_sliderEasy,m_sliderNormal;
	private Camera Mcam,Scam;
	private Animation Myanime;
    private Slider Myslider1,Myslider2;
    public static bool changeSceneSwich = false;

    void Awake(){
		MainCam = GameObject.Find("MainCamera");
		MainCam.SetActive(false);
      

    }

	void Start () {
		SubCam = GameObject.Find("MenuCamera");
        m_sliderEasy = GameObject.Find("Slider1");
        m_sliderNormal = GameObject.Find("Slider2");
		Scam = SubCam.GetComponent<Camera> ();
        Myslider1 = m_sliderEasy.GetComponent<Slider>();
        Myslider2 = m_sliderNormal.GetComponent<Slider>();
        Myanime = SubCam.GetComponent<Animation> ();

    }

	public void easyModePlay(){
        MoveSlider();
        //Debug.Log("click");
       
    }

	public void normalModePlay(){
        MoveSlider();
    }

	void Update(){
        if (VRStandardAssets.Utils.VRCameraFade.IsFadeIn == true) {
			MainCam.SetActive (true);	
			SubCam.SetActive (false);
		}
	}

    private void MoveSlider()
    {
        //Debug.Log(Myslider.value);
        if (Myslider1.value == 1f || Myslider2.value == 1f)
        {
            changeSceneSwich = true;
            //Debug.Log(changeSceneSwich);

            
        }
    }

    public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
