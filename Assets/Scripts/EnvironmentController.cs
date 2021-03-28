using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public Conductor conductor;

    public GameObject sphere;
    public Material sphereMaterial;

    public float amplitude;
    public float period;
    public float phase;
    
    private float frequency;
    private float angularFrequency;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        sphereMaterial.shader = Shader.Find("Shader Graphs/Waves");
        sphereMaterial.SetFloat("Cell_Density", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        SmoothSineWave(amplitude, period, phase);
        //warpDensity();
    }

    void activateSphereWaves()
    {

        //float density = 12f;

        //sphereMaterial.SetFloat("Cell_Density", warpDensity());

        //Debug.Log(sphereMaterial.GetFloat("Cell_Density"));
    }

    float warpDensity()
    {
        float min = 0f;
        float max = 10f;
        float t = 1;

        float warp = Mathf.Lerp(min, max, conductor.loopPositionInAnalog);

      //  Debug.Log(warp);
        return warp;
    }

    public void onFinishLoop()
    {
        warpSphere();
    }

    IEnumerator warpSphere()
    {
        sphereMaterial.SetFloat("Cell_Density", 12f);

        yield return new WaitForSeconds(1);

        float density = sphereMaterial.GetFloat("Cell_Density");

        while (density > 0f)
        {
            sphereMaterial.SetFloat("Cell_Density", density - 0.5f);
            yield return null;
        }
        //break;
    }

    void SmoothSineWave(float amplitude, float period, float phase)
    {
        // y(t) = A * sin(ωt + θ) [Basic Sine Wave Equation]
        // [A = amplitude | ω = AngularFrequency ((2*PI)f) | f = 1/T | T = [period (s)] | θ = phase | t = elapsedTime]
        // Public/Serialized Variables: amplitude, period, phase
        // Private/Non-serialized Variables: frequency, angularFrequency, elapsedTime
        // Local Variables: omegaProduct, y

        // If the value of period has altered last known frequency...
        if (1 / (period) != frequency)
        {
            // Recalculate frequency & omega.
            frequency = 1 / (period);
            angularFrequency = (2 * Mathf.PI) *frequency;
        }
        // Update elapsed time.
        elapsedTime += Time.deltaTime;
        // Calculate new omega-time product.
        float omegaProduct = (angularFrequency * elapsedTime);
        // Plug in all calculated variables into the complete Sine wave equation.
        float y = (amplitude * Mathf.Sin(omegaProduct + phase));
        // 
        //transform.localPosition = new Vector3(0, y, 0);

        sphereMaterial.SetFloat("Cell_Density", y);
       // Debug.Log(setRange(0, 0.32f, y));
    }

    float setRange(float min, float max, float f)
    {
        return Mathf.Lerp(min, max, Mathf.InverseLerp (-1, 1, f));
    }
/*
    void rotateSkybox()
    {
        if (skyboxMaterial.HasProperty("_Rotation") == true)
        {
            int rotate = skyboxMaterial.GetInt("_Rotation");

            if (rotate < 360)
            {
                //skyboxMaterial.SetInt("_Rotation", (rotate + 1));
              //  StartCoroutine(waitForSeconds());
              //rotateSkyboxCoroutine(rotate);
            }

            if (rotate == 360)
            {
               // skyboxMaterial.SetInt("_Rotation", 0);
            }
           // Debug.Log(rotate);
        }
        else
        {
         //   Debug.Log("Nope");
        }
    }

    IEnumerator rotateSkyboxCoroutine(int rotate)
    {
        //yield return new WaitForSeconds(1f);

        while (rotate < 360)
        {
            skyboxMaterial.SetInt("_Rotation", (rotate + 1));
            yield return null;
        }
    }
    */
}
