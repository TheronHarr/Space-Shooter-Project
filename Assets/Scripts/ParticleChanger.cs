using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleChanger : MonoBehaviour
{
    public ParticleSystem ps;
    public GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var main = ps.main;

        if (gameController.score >= 100)
        {
            main.simulationSpeed = 50.0f;
        }
    }
}
