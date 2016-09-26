using UnityEngine;
using System.Collections;

public class fovChange : MonoBehaviour {
    private Camera reducedCamera;
    [Range(1, 179)]
    public float cullingFov = 60f;//40.0f;
    private float renderingFov = 106;
    private Rect renderingRect;

    void Awake()
    {
        reducedCamera = this.GetComponent<Camera> ();

        renderingFov = reducedCamera.fieldOfView;

    }

    void OnPreCull()
    {
        reducedCamera.fieldOfView = cullingFov;
    }


    void OnPreRender()
    {
        reducedCamera.fieldOfView = 106.188f;
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
