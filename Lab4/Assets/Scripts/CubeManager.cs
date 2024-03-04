using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public GameObject cubePrefab;

    float t = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CreationRequested())
        {
            CreateCube();
        }
        t -= Time.deltaTime;
        
        if(t < 0)
        {
            CreateCube();
            t += 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Physics.gravity *= -1.0f;
        }
    }

    private void CreateCube()
    {
        GameObject cube = GameObject.Instantiate(cubePrefab, transform.position, transform.rotation);
        cube.transform.parent = transform;
        Rigidbody b = cube.GetComponent<Rigidbody>();
        b.AddForce(transform.forward * 20, ForceMode.Impulse);
        b.mass = Random.Range(1, 100);
        b.drag = Random.Range(0, 10);
    }

    private bool CreationRequested()
    {
        return Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.C);
    }
}
