using UnityEngine;
using System.Collections;
using System.Collections.Generic; //para los list<>
using Leap;

public class MovCollider : MonoBehaviour
{
    Controller ctrLeap;
    Hand ManoUno;
    Hand ManoDos;

    void Start()
    {
        ctrLeap = new Controller();
        ManoUno = new Hand();
        ManoDos = new Hand();
    }
    void Update()
    {
        Vector3 elegida = new Vector3(0, 0, 0) ;
        if (ctrLeap.IsConnected)
        {
            Frame cuadro = ctrLeap.Frame();//Detectamos todo lo actual en el cuadro actual
            if (cuadro.Hands.Count > 0)
            {
                ManoUno = cuadro.Hands[0];
                if (cuadro.Hands.Count > 1)
                {
                    ManoDos = cuadro.Hands[1];
                    if (ManoDos.IsRight)
                    { elegida = new Vector3 (ManoDos.PalmPosition.x/(-1000),ManoDos.PalmPosition.z/(-1000),ManoDos.PalmPosition.y/1000);  }
                }
                if (ManoUno.IsRight)
                { elegida = new Vector3 (ManoUno.PalmPosition.x/(-1000),ManoUno.PalmPosition.z/(-1000),ManoUno.PalmPosition.y/1000); }
            }
        }
        gameObject.transform.position = elegida;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lineas")
        {
            Destroy(other.gameObject);  
        }
        return;
    }
}
