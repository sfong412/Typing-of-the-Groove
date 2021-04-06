using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public Conductor conductor;

    public GameObject orbit;

    public GameObject crystal;

    public GameObject sphere;
    public Material sphereMaterial;

    public GameObject floor;
    public Material floorMaterial;

    public float amplitude;
    public float period;
    public float phase;
    
    private float frequency;
    private float angularFrequency;
    private float elapsedTime;

    private float y;
    private float rotationSpeed = -30f;

    // Start is called before the first frame update
    void Start()
    {
        floorMaterial.shader = Shader.Find("Shader Graphs/Floor Glass");

        sphereMaterial.shader = Shader.Find("Shader Graphs/Waves");
        sphereMaterial.SetFloat("Cell_Density", 0f);
        orbit.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        amplitude = 1.5f;
        period = 5f;
        phase = 90f;

        Debug.Log(crystal.transform.childCount + " children");
    }

    // Update is called once per frame
    void Update()
    {
        SmoothSineWave(amplitude, period, phase);
        rotateOrbit();
        rotateCrystal();
    }

    void FixedUpdate()
    {
        y += Time.deltaTime * rotationSpeed;

        if (y > 360.0f)
        {
            y = 0.0f;
        }
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

        sphereMaterial.SetFloat("Cell_Density", y);
    }

    void rotateOrbit()
    {
        //Quaternion target = Quaternion.Euler(0f, 60f, 0f);

       // orbit.transform.rotation = Quaternion.Slerp(orbit.transform.rotation, target, Time.deltaTime * 0.05f);

       orbit.transform.RotateAround(orbit.transform.position, new Vector3(0.2f, 1, 0), 5 * Time.deltaTime);
    }

    void rotateCrystal()
    {
        foreach (Transform child in crystal.transform)
        {
            child.transform.localRotation = Quaternion.Euler(-90f, y, 0f);
        }
    }
}
