using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour
{
    public ParticleSystem ps;

    // Use this for initialization
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
