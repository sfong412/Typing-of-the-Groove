using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public Transform leftHand;
    // = GameObject.Find("Dancer").GetBoneTransform(LeftHand);

    //public Transform 
    public Transform rightHand;

    public ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        particle.Play();
    }

    // Update is called once per frame
    void Update()
    {
        moveParticleSystem(leftHand);
    }

    void moveParticleSystem(Transform hand)
    {        
        //ParticleSystem particle = GameObject.Find("Particle System").GetComponent<ParticleSystem>();

        particle.transform.position = hand.transform.position;
        particle.transform.rotation = new Quaternion(hand.transform.rotation.x, hand.transform.rotation.y + .58f, hand.transform.rotation.z, 1);
        //particle.transform.rotation = hand.transform.rotation;
        //Debug.Log(particle.transform.rotation.y);
    }
}
