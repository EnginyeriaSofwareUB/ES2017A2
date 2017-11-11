using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{

    private string wepFired;
    private Button[] btns;
    private int selectedBtn;

    // Use this for initialization
    void Awake()
    {
        this.selectedBtn = 0;
        this.wepFired = null;
        this.btns = new Button[5];
    }

    void Start()
    {

    }

    // Update is called once per frame
    /**
     * Comprueba si se ha pulsado una tecla, si dicha tecla es un numero del 1 al 5 
     * y si la celda del inventario a la que corresponde tiene un arma.
     * Si es asi entonces la selecciona.
     * 
     * Unicamente hace un highlight de la celda al pulsar uno de los numeros correspondientes,
     * para saber el arma que dispararas si disparas.
     */
    void Update()
    {
        if (!this.btns[this.selectedBtn].interactable)
            this.selectedBtn = 0;
        this.btns[this.selectedBtn].Select();  //Sin esto desaparece el highlight del boton al hacer click para cargar el disparo
        if (Input.anyKey)
            for (int i = 0; i < 5 && this.btns[i] != null && this.btns[i].interactable; i++)
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    this.btns[i].Select();
                    this.selectedBtn = i;
                    break;
                }
    }


    /**
     * Metodo para inicializar los botones correspondientes al inventario.
     * 
     * Primero un bucle los linka con la lista de botones 'btns' para tener la referencia.
     * Luego comprueba los proyectiles disponibles en el inventario y actualiza el panel de botones,
     * asi siempre sale la informacion actualizada por pantalla desde el principio
     * Finalmente 'anula' los demas botones para no poder seleccionarlos, 
     * puesto que no tiene sentido aceptar el numero 4 si hay 3 armas en el inventario.
     */
    public void initBtns(Dictionary<ProjectileInfo, int> dic)
    {
        string[] btnsNames = new string[] { "Wep1", "Wep2", "Wep3", "Wep4", "Wep5" };
        int i = 0;
        foreach (string s in btnsNames)
        {
            this.btns[i] = GameObject.Find(s).GetComponent<Button>();
            this.btns[i].GetComponentInChildren<Text>().text = " ";
            i++;
        }
        i = 0;
        foreach (KeyValuePair<ProjectileInfo, int> ps in dic)
        {
            if (ps.Value > 0)
            {
                this.btns[i].GetComponentInChildren<Text>().text = ps.Key.projectileName.Substring(0, 3) + " " + ps.Value;
                this.btns[i].interactable = true;
                i++;
            }
        }
        for (int j = i; j < 5; j++)
            this.btns[j].interactable = false;
        this.selectedBtn = 0;
        this.btns[this.selectedBtn].Select();
    }


    /**
     * Metodo que actualiza los botones como hace el metodo de inicializacion.
     * 
     * De momento solo hace esto. Este metodo se llama cada vez que dispara un proyectil.
     */
    public void updateBtns(Dictionary<ProjectileInfo, int> dic)
    {
        this.initBtns(dic);
    }


    /**
     * Este atributo es el que se 'comunica' con el objeto de Inventory en Player, 
     * para poder eliminar correctamente el proyectil disparado.
     */
    public string WepFired
    {
        get
        {
            string aux = this.btns[this.selectedBtn].GetComponentInChildren<Text>().text.Substring(0, 3);
            this.wepFired = this.btns[0].GetComponentInChildren<Text>().text.Substring(0, 3);
            return aux;
        }
    }


    /**
     * Metodo que inicialmente usaba en lugar de los numeros, 
     * pero al clicar sobre el boton se dispara el proyectil,
     * sin comentar que me parece mas practico y rapido pulsar numeros correspondientes a proyectiles 
     * ya que solo tenemos 5, en lugar de tener que buscarlo con el raton.
     */
    public void btnWepOnClick(Button b)
    {
        Debug.Log("Button pressed: " + b.GetComponentInChildren<Text>().text);
        this.wepFired = b.name;
    }
}
