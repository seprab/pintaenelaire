using UnityEngine;
using System.Collections;
using System.Collections.Generic; //para los list<>
using Leap;


//script de funciones de gestos
public class estados : MonoBehaviour
{
   public bool ManoCerrada(Hand mano)
    {
        float fuerza = mano.GrabStrength;
        if (fuerza == 1) { return true; }
        else { return false; }
    }
    public bool ManoAbierta(Hand mano)
    {
        List<Finger> dedos = mano.Fingers;
        bool[] Abierto = { true, true, true, true, true };
        bool[] comprobar = new bool[5];
        int cont = 0;
        for (int i = 0; i < 5; i++)
        {
            comprobar[i] = dedos[i].IsExtended;
            if (Abierto[i] == comprobar[i])
            { cont++; }
        }
        if (cont == 5)
        { return true; }
        else
        { return false; }
    }
    public bool Apuntar(Hand mano)
    {
        List<Finger> dedos = mano.Fingers;
        bool[] Apuntamos = { false, true, false, false, false };
        bool[] comprobar = new bool[5];
        int cont = 0;
        for (int i = 0; i < 5; i++)
        {
            comprobar[i] = dedos[i].IsExtended;
            if (Apuntamos[i] == comprobar[i])
            { cont++; }
        }
        if (cont==5)
        {   return true;       }
        else
        {   return false;      }
    }
    public int DedosArriba(Hand mano)
    {
        List<Finger> dedos = mano.Fingers;
        int cont = 0;
        for (int i = 0; i < 5; i++)
        {
            if (dedos[i].IsExtended == true)
            { cont++; }
        }

        return cont; 
    }
    public bool PalmaAca(Hand mano)
    {
        float orientacion = Mathf.Atan2(mano.PalmNormal.y, mano.PalmNormal.x) * (180 / Mathf.PI);
        if (orientacion < -44 && orientacion > -135)
        { return true; }
        else { return false; }
    }
    public bool PalmaAlla(Hand mano)
    {
        float orientacion = Mathf.Atan2(mano.PalmNormal.y, mano.PalmNormal.x) * (180 / Mathf.PI);
        if(orientacion > 44 && orientacion < 135)
        { return true; }
        else { return false; }
    }
    public float fuerzaMano (Hand mano)
    {
        float fuerza = mano.GrabStrength;
        return fuerza;
    }
}
