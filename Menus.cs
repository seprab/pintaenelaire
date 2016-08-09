using UnityEngine;
using System.Collections;
using System.Collections.Generic; //para los list<>
using Leap;

public class Menus : MonoBehaviour
{
    public List<GameObject> Colores;
    public List<GameObject> Materiales;
    public List<Material> MaterialesSust;
    int color;
    Pintar ClasePintar;

    public void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Colores[i].GetComponent<Renderer>().enabled = false;
            Colores[i].GetComponent<Collider>().enabled = false;
            Materiales[i].GetComponent<Renderer>().enabled = false;
            Materiales[i].GetComponent<Collider>().enabled = false;
        }
        color = 1;
      
        ClasePintar = gameObject.GetComponent<Pintar>();
    }
    public void MenuColores(Hand Manitos)
    {
        List<Finger> Dedillos = Manitos.Fingers;
        Vector[] Proyecciones = new Vector[5];
        Vector3[] Proyec3 = new Vector3[5];

        Vector[] inicio = new Vector[5];
        Vector3[] inicio2 = new Vector3[5];

        for (int i = 0; i < 5; i++)
        {
            Proyecciones[i] = Dedillos[i].Bone(Bone.BoneType.TYPE_INTERMEDIATE).Basis.yBasis;  //este es el vector normal desde el dedo
            Proyec3[i].Set(Proyecciones[i].x, Proyecciones[i].z, -Proyecciones[i].y);  //lo convertimos a Vector3
            inicio[i] = Dedillos[i].Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center; //obtenemos el punto centro desde el dedo
            inicio2[i].Set(inicio[i].x / (-1000), inicio[i].z / (-1000), inicio[i].y / (1000)); //hacemos calculos fictys para que encaje en unity y lo convertimos a Vector3
        }
        MostrarColores(Proyeccion(inicio2, Proyec3, 0.05f),Proyec3); //Proyeccion a 0.05 desde el centro
    }

    public void MenuMateriales(Hand Manitos)
    {
        List<Finger> Dedillos = Manitos.Fingers;
        Vector[] Proyecciones = new Vector[5];
        Vector3[] Proyec3 = new Vector3[5];

        Vector[] inicio = new Vector[5];
        Vector3[] inicio2 = new Vector3[5];

        for (int i = 0; i < 5; i++)
        {
            Proyecciones[i] = Dedillos[i].Bone(Bone.BoneType.TYPE_PROXIMAL).Basis.yBasis;  //este es el vector normal desde el dedo
            Proyec3[i].Set(Proyecciones[i].x, Proyecciones[i].z, -Proyecciones[i].y);  //lo convertimos a Vector3
            inicio[i] = Dedillos[i].Bone(Bone.BoneType.TYPE_PROXIMAL).Center; //obtenemos el punto centro desde el dedo
            inicio2[i].Set(inicio[i].x / (-1000), inicio[i].z / (-1000), inicio[i].y / (1000)); //hacemos calculos fictys para que encaje en unity y lo convertimos a Vector3
        }
        Proyec3 = Proyeccion(inicio2,Proyec3,-1);
        MostrarMateriales(Proyeccion(inicio2, Proyec3, 0.005f), Proyec3); //Proyeccion a 0.05 desde el centro
    }
    Vector3[] Proyeccion(Vector3[] Pi,Vector3[] Pf, float DistanciaDeseada)
    {
        Vector3[] ProyeccionDeseada = new Vector3[5];
        Vector3[] Director = new Vector3[5];
        for(int i=0;i<5;i++)
        {
            Director[i].Set(Pf[i].x - Pi[i].x, Pf[i].y - Pi[i].y, Pf[i].z - Pi[i].z);
            ProyeccionDeseada[i].x = Pi[i].x + (Director[i].x * DistanciaDeseada);
            ProyeccionDeseada[i].y = Pi[i].y + (Director[i].y * DistanciaDeseada);
            ProyeccionDeseada[i].z = Pi[i].z + (Director[i].z * DistanciaDeseada);
            //ProyeccionDeseada[i] = ClasePintar.PosDesfasado(ProyeccionDeseada[i]);
        }
        return ProyeccionDeseada;
    }
    void MostrarColores(Vector3[] Punto, Vector3[] Normal)
    {
        for(int i=0; i<5; i++)
        {
            Colores[i].GetComponent<Renderer>().enabled = true;
            Colores[i].GetComponent<Collider>().enabled = true;
            Colores[i].transform.position = Punto[i];
            Colores[i].transform.up = Normal[i];
        }
    }
    void MostrarMateriales(Vector3[] Punto, Vector3[] Normal)
    {
        ColorMateriales();
        for (int i = 0; i < 5; i++)
        {
            Materiales[i].GetComponent<Renderer>().enabled = true;
            Materiales[i].GetComponent<Collider>().enabled = true;
            Materiales[i].transform.position = Punto[i];
            Materiales[i].transform.up = Normal[i];
         
        }
    }
    void ColorMateriales()
    {
        color = ClasePintar.QueColor();
        if (color == 0)
        {
            color = 1;
        }
        
        Vector4 c = new Vector4(0, 0, 0, 1);
        switch (color)
        {
            case 1: c = new Vector4(1, 0, 0, 1); break;

            case 2:
                c = new Vector4(1, 1, 0, 1);
                break;

            case 3:
                c = new Vector4(1, 1, 1, 1);
                break;

            case 4:
                c = new Vector4(0, 0, 1, 1);
                break;
            case 5:
                c = new Vector4(0, 1, 1, 1);
                break;
        }
        for (int i = 0; i < 5; i++)
        {
            Materiales[i].GetComponent<Renderer>().material.color=c;
        }
        /*  int sumador = 0;  //lo que hace es variar entre los menus 
          if (color != 0)
          {
              if (color == 1) { sumador = 0; }
              if (color == 2) { sumador = 5; }
              if (color == 3) { sumador = 10; }
              if (color == 4) { sumador = 15; }
              if (color == 5) { sumador = 20; }

              for (int i = 0; i < 5; i++)
              {
                  Materiales[i].GetComponent<Renderer>().material = MaterialesSust[i+sumador];
              }
          }
          else
          {
              Materiales[0].GetComponent<Renderer>().material.color = new Vector4(1, 0, 0, 1);
              Materiales[1].GetComponent<Renderer>().material.color = new Vector4(1, 1, 0, 1);
              Materiales[2].GetComponent<Renderer>().material.color = new Vector4(1, 1, 1, 1);
              Materiales[3].GetComponent<Renderer>().material.color = new Vector4(0, 0, 1, 1);

              Materiales[5].GetComponent<Renderer>().material.color = new Vector4(0, 0, 0, 1);
              /*
              Materiales[0].GetComponent<Renderer>().material = MaterialesSust[0];
              Materiales[1].GetComponent<Renderer>().material = MaterialesSust[5];
              Materiales[2].GetComponent<Renderer>().material = MaterialesSust[10];
              Materiales[3].GetComponent<Renderer>().material = MaterialesSust[15];
              Materiales[4].GetComponent<Renderer>().material = MaterialesSust[20];

          }*/
    }
    public void NoMostrarColores()
    {
        for (int i = 0; i < 5; i++)
        {
            Colores[i].GetComponent<Renderer>().enabled = false;
            Colores[i].GetComponent<Collider>().enabled = false;
        }
    }
    public void NoMostrarMateriales()
    {
        for (int i = 0; i < 5; i++)
        {
            Materiales[i].GetComponent<Renderer>().enabled = false;
            Materiales[i].GetComponent<Collider>().enabled = false;
        }
    }
}
