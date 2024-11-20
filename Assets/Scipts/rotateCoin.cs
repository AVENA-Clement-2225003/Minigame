using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class rotateCoin : MonoBehaviour
{
    public Vector3 m_SpeedRotation;
    public Vector3 m_RandomSpeedRotation;

    // Start is called before the first frame update
    void Start()
    {
        float randomValue = Random.Range(0f, 1f);
        m_RandomSpeedRotation = m_SpeedRotation * randomValue;
        transform.Rotate(m_RandomSpeedRotation * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(m_SpeedRotation * Time.deltaTime);
    }
}
