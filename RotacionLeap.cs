using UnityEngine;
using System.Collections;
using Leap;

public class RotacionLeap : MonoBehaviour
{
    public Transform rotacion;
	// Use this for initialization
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.rotation = rotacion.rotation;
	}
}
