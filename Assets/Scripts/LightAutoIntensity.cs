using UnityEngine;

public class LightAutoIntensity : MonoBehaviour
{

    public Gradient nightDayColor;

    public float maxIntensity = 3f;
    public float minIntensity = 0f;
    public float minPoint = -0.2f;

    public float maxAmbient = 1f;
    public float minAmbient = 0f;
    public float minAmbientPoint = -0.2f;


    public Gradient nightDayFogColor;
    public AnimationCurve fogDensityCurve;
    public float fogScale = 1f;

    public float dayAtmosphereThickness = 0.4f;
    public float nightAtmosphereThickness = 0.87f;

    public Vector3 dayRotateSpeed;
    public Vector3 nightRotateSpeed;

    float skySpeed = 30;
    public int dayState = 0;
    public int transitionFlag = 0;
    Light mainLight;
    Skybox sky;
    Material skyMat;
    DayNight DayNightScript;

    void Awake()
    {
        DayNightScript = GetComponent<DayNight>();
        DayNightScript.stars.SetActive(false);

        transform.eulerAngles = new Vector3(20, 0, 0);
        
        mainLight = GetComponent<Light>();
        skyMat = RenderSettings.skybox;
    }

    void Start()
    {
    }

    void Update()
    {
        if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                SetTransition(0);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                SetTransition(1);
            }
        }

        TransitionDayState();
    }

    public void TransitionToDay()
    {
        TransitionUpdate();

        //toggle stars
        if (transform.eulerAngles.x > 20 && transform.eulerAngles.x < 100)
        {
            DayNightScript.stars.SetActive(false);
        }
        //end transition on condition
        if (transform.eulerAngles.x < 80 && transform.eulerAngles.x > 70)
        {
            transitionFlag = 0;
        }

        dayState = 0;
    }

    public void TransitionToNight()
    {
        TransitionUpdate();

        //toggle stars
        if (transform.eulerAngles.x < 360 && transform.eulerAngles.x > 350)
        {
            DayNightScript.stars.SetActive(true);
        }
        //end transition on condition
        if (transform.eulerAngles.x < 330 && transform.eulerAngles.x > 320)
        {
            transitionFlag = 0;
        }
        dayState = 1;
    }

    void TransitionDayState()
    {
        if (dayState == 0 && transitionFlag == 1)
        {
            TransitionToDay();
        }
        else if (dayState == 1 && transitionFlag == 1)
        {
            TransitionToNight();
        }
    }

    void SetTransition(int val) {
        if (val == 0)
        {
            dayState = 0;
            transitionFlag = 1;
        }
        else if (val == 1)
        {
            dayState = 1;
            transitionFlag = 1;
        }
    }

    void TransitionUpdate() {
        float tRange = 1 - minPoint;
        float dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
        float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

        mainLight.intensity = i;

        tRange = 1 - minAmbientPoint;
        dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
        i = ((maxAmbient - minAmbient) * dot) + minAmbient;
        RenderSettings.ambientIntensity = i;

        mainLight.color = nightDayColor.Evaluate(dot);
        RenderSettings.ambientLight = mainLight.color;

        RenderSettings.fogColor = nightDayFogColor.Evaluate(dot);
        RenderSettings.fogDensity = fogDensityCurve.Evaluate(dot) * fogScale;

        i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
        skyMat.SetFloat("_AtmosphereThickness", i);

        if (dot > 0)
            transform.Rotate(dayRotateSpeed * Time.deltaTime * skySpeed);
        else
            transform.Rotate(nightRotateSpeed * Time.deltaTime * skySpeed);
    }
}
