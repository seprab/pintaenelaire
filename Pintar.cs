using UnityEngine;
using System.Collections;
using System.Collections.Generic; //para los list<>
using Leap;

public class Pintar : MonoBehaviour
{
    Vector3 tamanoCirculos;
    GameObject circulos;
    public List<Material> Materiales;
    Material MaterialElegido;
    Material las_elegido;
    int colorsillo;
    public Transform OculusTrans;
    public Sprite spr;
    Vector4 lasta_color=new Vector4(1,1,1,1);
    public void Start()
    {
        circulos = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        tamanoCirculos = new Vector3(0.01f, 0.01f, 0.01f);
        circulos.transform.localScale = tamanoCirculos;
        circulos.GetComponent<Renderer>().material = Materiales[0];
        circulos.GetComponent<Renderer>().material.color = Color.green;

        GameObject.Find("Image").GetComponent<UnityEngine.UI.Image>().color = new Vector4(1, 0, 0, 1);

        GameObject.Find("Image1").GetComponent<UnityEngine.UI.Image>().color = new Vector4(1, 0, 0, 1);

        GameObject.Find("Image1").GetComponent<UnityEngine.UI.Image>().sprite = GameObject.Find("Image3").GetComponent<UnityEngine.UI.Image>().sprite;
        GameObject.Find("Color_01").GetComponent<Renderer>().material.color = new Vector4(1, 0, 0, 1);
        GameObject.Find("Color_02").GetComponent<Renderer>().material.color = new Vector4(1, 1, 0, 1);
        GameObject.Find("Color_03").GetComponent<Renderer>().material.color = new Vector4(1, 1, 1, 1);
        GameObject.Find("Color_04").GetComponent<Renderer>().material.color = new Vector4(0, 0, 1, 1);
        GameObject.Find("Color_05").GetComponent<Renderer>().material.color = new Vector4(0, 1, 1, 1);
          // GameObject.Find("Color_02").GetComponent<Renderer>().material.SetColor("_TintColor", new Vector4(1, 1, 0, 1));
        circulos.tag = "Lineas";
        colorsillo = 0;
    }
    public void pintamos(Vector posPintar)
    {
        
        Vector3 Pos = new Vector3(posPintar.x / (-1000), posPintar.z / (-1000), posPintar.y / (1000));
        //Pos = PosDesfasado(Pos);
        circulos.transform.position = Pos;
        Instantiate(circulos);
    }
    public Vector3 PosDesfasado(Vector3 PosicionOrigen)
    {
        Vector3 PosFinal = new Vector3(0, 0, 0);
        var Qx = Quaternion.AngleAxis(OculusTrans.eulerAngles.x, OculusTrans.forward);
        PosFinal =(PosicionOrigen+((Qx*OculusTrans.forward)+OculusTrans.position));
        return PosFinal;
    }
    public void SustitucionMat(int Mat)
    {
        if (Mat != 0)
        {
            MaterialElegido = Materiales[Mat - 1];
            
            circulos.GetComponent<Renderer>().material = MaterialElegido;
            circulos.GetComponent<Renderer>().material.SetColor("_TintColor",lasta_color);
            switch (Mat)
            {
                case 1:
                    //sustitucion de material en ui
                    GameObject.Find("Image1").GetComponent<UnityEngine.UI.Image>().sprite = GameObject.Find("Image3").GetComponent<UnityEngine.UI.Image>().sprite;
                    break;
            }
          
        }
    }
    public void SustitucionColor (int Colorr)
    {
        
        colorsillo = Colorr;
        GameObject t = GameObject.Find("Image");
        Vector4 c=new Vector4(0,0,0,1);
        switch (Colorr)
        {
            case 1: c = new Vector4(1, 0, 0, 1);   break;

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
        circulos.GetComponent<Renderer>().material = MaterialElegido;
        t.GetComponent<UnityEngine.UI.Image>().color = c;
        circulos.GetComponent<Renderer>().material.SetColor("_TintColor", c);
        lasta_color = c;

        /*  if (Colorr != 0)
          {
              Color32 ColorElegido = new Color32(0, 0, 0, 0);
              if (Colorr == 1) { ColorElegido.r = 39; ColorElegido.b = 59; }
              if (Colorr == 2) { ColorElegido.g = 196; ColorElegido.b = 14; }
              if (Colorr == 3) { ColorElegido.r = 9; ColorElegido.g = 82; ColorElegido.b = 255; }
              if (Colorr == 4) { ColorElegido.g = 1; ColorElegido.b = 255; }
              if (Colorr == 5) { ColorElegido.r = 255; ColorElegido.g = 26; }
              Color colorUnity = ColorElegido; //pasamos desde 32 bits a Color de 0 a 1



              //MaterialElegido.color = colorUnity;
              //MaterialElegido.SetColor("_ColorTint", colorUnity);
              //MaterialElegido.color.r = colorUnity/255;
              //Debug.Log("Color elegido " + Color);
          }*/
    }
    public void tamanoCir(float stre)
    {
        tamanoCirculos.Set(0.01f * stre, 0.01f * stre, 0.01f * stre);
        circulos.transform.localScale = tamanoCirculos;
        GameObject.Find("Image").transform.localScale = tamanoCirculos * 100;
    }
    public int QueColor()
    {        return colorsillo;    }
}
