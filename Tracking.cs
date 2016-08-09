using UnityEngine;
using System.Collections;
using System.Collections.Generic; //para los list<>
using Leap;



public class Tracking : MonoBehaviour
{

    Controller ctrLeap;
    Hand ManoUno;
    Hand ManoDos;
    estados Gestos;
    Pintar MandamosPintar;
    Menus MostrarMenus;
    

    int opcion;

    void Start ()
    {
        ctrLeap = new Controller();
        ManoUno = new Hand();
        ManoDos = new Hand();
        Gestos = gameObject.GetComponent < estados > ();
        MandamosPintar = gameObject.GetComponent<Pintar>();
        MostrarMenus = gameObject.GetComponent<Menus>();
        opcion = 1;
    }
    void Update ()
    {
        
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
                    {
                        DeteccionDer(ManoDos);
                        AmbasManos(ManoUno,ManoDos);
                    }
                    if (ManoDos.IsLeft)
                    {
                        DeteccionIzq(ManoDos);
                        AmbasManos(ManoDos,ManoUno);
                    }
                }
                if (ManoUno.IsRight)
                { DeteccionDer(ManoUno); }                
                if (ManoUno.IsLeft)
                { DeteccionIzq(ManoUno); }
            }
            else
            {
                //no hay manitos
            }
        }
        else { Debug.Log("falta el leap"); }   
    }
    void DeteccionDer(Hand manoDer)  
    {      
        if (Gestos.Apuntar(manoDer))
            {
            List<Finger> dedos = manoDer.Fingers;
            Bone hueso;
                hueso = dedos[1].Bone(Bone.BoneType.TYPE_INTERMEDIATE);
                MandamosPintar.pintamos(hueso.NextJoint);                                       
            }   
            else
            {  
            /*no esta la posicion de pintar*/
            opcion = Gestos.DedosArriba(manoDer);
          
        }
        if (manoDer.GrabStrength==1)
        { gameObject.GetComponent<Collider>().enabled = true; }
        else
        { gameObject.GetComponent<Collider>().enabled = false; }
        
    }
    void DeteccionIzq(Hand manoIzq)
    {        
        if(Gestos.ManoAbierta(manoIzq))
        {
            if (Gestos.PalmaAlla(manoIzq))
            {
                MostrarMenus.NoMostrarColores(); //no mostrar los menu de colores
                MostrarMenus.MenuMateriales(manoIzq);
                MandamosPintar.SustitucionMat(opcion); //aca debemos enviar un material (numero de 1 a 5)
               
            }
            else if (Gestos.PalmaAca(manoIzq))
            {
                MostrarMenus.NoMostrarMateriales();
                MostrarMenus.MenuColores(manoIzq);   //menu colores
                MandamosPintar.SustitucionColor(opcion);
               
            }
        }
        else
        {
            MostrarMenus.NoMostrarColores();
            MostrarMenus.NoMostrarMateriales();
        }
    }
    void AmbasManos(Hand manoIzq, Hand ManoDer)
    {
        if(Gestos.ManoCerrada(ManoDer))
        {
            float stre = Mathf.Abs(manoIzq.GrabStrength - 1);
            stre += 0.1f;
            MandamosPintar.tamanoCir(stre);
        }
    }
  
    
}
