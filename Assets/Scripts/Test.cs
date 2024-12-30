using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] int speed;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Dem");
    }

    // Update is called once per frame
    void Update()
    {

        transform.position +=  new Vector3(2, 0, 0) * speed * Time.deltaTime;
        //transform.Rotate += new Vector3(2, 0, 0) * Time.deltaTime;
        Debug.Log("JAV");
    }
}
