using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public string name;
    public Sprite background;
    public Sprite hortaliza;
    public Button.ButtonClickedEvent thingToDo;
}

[System.Serializable]
public class Img_and_desc
{
    public string name;
    public Image hortaliza;
    public Image descripcion;
}

[System.Serializable]
public class Stats_popup
{
    public string name;
    public Image hortaliza;
    public float damage;
    public float velocity;
    public float weight;
    public int damage_radius;
    public int detonation_time;
    public Text cost;
    public Image description;
    public Text num_elements;
}

[System.Serializable]
public class user_active
{
    public Image player1;
    public Image player1active;
    public Image player2;
    public Image player2active;
    public Button buttonNext;
    public Button buttonPlay;
}

[System.Serializable]
public class GameControl
{
    public List<Projectile> player1_projectiles;
    public List<Projectile> player2_projectiles;
    public int turn; //1 - Player1 2 - Player2
}

    public class Btn_ProjectileMenu : MonoBehaviour {

    [SerializeField] private Canvas popup;

    [SerializeField] private List<Img_and_desc> imagenes;

    [SerializeField] private user_active users_active;

    //Center container
    [SerializeField] private GameObject sampleImage;
    [SerializeField] private Transform contentParent;
    [SerializeField] private List<Item> projectiles;
    [SerializeField] private Stats_popup stats_popup;

    //Values of all projectiles
    private Dictionary<string, float> dict_damage = new Dictionary<string, float>();
    private Dictionary<string, float> dict_velocity = new Dictionary<string, float>();
    private Dictionary<string, float> dict_weight = new Dictionary<string, float>();
    private Dictionary<string, int> dict_damage_radius = new Dictionary<string, int>();
    private Dictionary<string, int> dict_detonation_time = new Dictionary<string, int>();
    private Dictionary<string, Text> dict_cost = new Dictionary<string, Text>();
    string[] projectilesName = { "pastanaga", "tomaquet", "ceba", "pebrot", "alberginia" };

    //Game control
    [SerializeField] private GameControl game_control;

    // Use this for initialization
    void Start () {
        popup.enabled = false;
        init_projectiles();
        init_attributes();
        game_control.turn = 1;
    }
	
	// Update is called once per frame
	void Update () {

		if(game_control.turn == 1)
        {
            users_active.player1.enabled = false;
            users_active.player1active.enabled = true;
            users_active.player2.enabled = true;
            users_active.player2active.enabled = false;
            users_active.buttonNext.enabled = true;
            users_active.buttonPlay.enabled = false;
        }
        else
        {
            users_active.player1.enabled = true;
            users_active.player1active.enabled = false;
            users_active.player2.enabled = false;
            users_active.player2active.enabled = true;
            users_active.buttonNext.enabled = false;
            users_active.buttonPlay.enabled = true;
        }
	}

	public void PlayOnClick()
	{
        SceneManager.LoadScene("Test Scene");
    }

    public void BackOnClick()
	{
        SceneManager.LoadScene("IndexMenu");
    }

    public void CancelOnClick()
    {
        popup.enabled = false;
    }

    public void BuyOnClick()
    {
        popup.enabled = false;
        //TODO
    }

    public void NextOnClick()
    {
        popup.enabled = false;
        game_control.turn = 2;
    }

    public void init_projectiles()
    {
        foreach (var projectile in projectiles)
        {
            GameObject newImage = Instantiate(sampleImage) as GameObject;
            ProjectileScript projectileScript = newImage.GetComponent<ProjectileScript>();
            projectileScript.background.sprite = projectile.background;
            projectileScript.projectileImage.sprite = projectile.hortaliza;
            projectileScript.name = projectile.name;
            projectileScript.button.onClick = projectile.thingToDo;

            newImage.transform.SetParent(contentParent);
        }
    }

    public void init_attributes()
    {
        float[] damages = { 1, 2, 3, 4, 5 };
        float[] speeds = { 1, 2, 3, 4, 5 };
        float[] weights = { 1, 2, 3, 4, 5 };
        int[] damages_radius = { 1, 2, 3, 4, 5 };
        int[] detonations_time = { 1, 2, 3, 4, 5 };
        string[] costes = { "20", "25", "30", "35", "40" };
        
        GameObject newGO = new GameObject("myTextGO");
        newGO.transform.SetParent(this.transform);
        Text myText1 = newGO.AddComponent<Text>();
        myText1.text = costes[0];
        GameObject newGO2 = new GameObject("myTextGO");
        newGO2.transform.SetParent(this.transform);
        Text myText2 = newGO2.AddComponent<Text>();
        myText2.text = costes[1];
        GameObject newGO3 = new GameObject("myTextGO");
        newGO3.transform.SetParent(this.transform);
        Text myText3 = newGO3.AddComponent<Text>();
        myText3.text = costes[2];
        GameObject newGO4 = new GameObject("myTextGO");
        newGO4.transform.SetParent(this.transform);
        Text myText4 = newGO4.AddComponent<Text>();
        myText4.text = costes[3];
        GameObject newGO5 = new GameObject("myTextGO");
        newGO5.transform.SetParent(this.transform);
        Text myText5 = newGO5.AddComponent<Text>();
        myText5.text = costes[4];
        Text[] costs = { myText1,myText2,myText3,myText4,myText5};

        int i = 0;

        foreach (var projectile in projectilesName) {
            dict_velocity[projectile] = speeds[i];
            dict_weight[projectile] = weights[i];
            dict_detonation_time[projectile] = detonations_time[i];
            dict_damage_radius[projectile] = damages_radius[i];
            dict_cost[projectile] = costs[i];
            dict_damage[projectile] = damages[i];
            i += 1;
        }
    }

    public void DoSomething(string name)
    {
        Dictionary<string, int> dic = new Dictionary<string, int>();

        for (int i = 0; i < 5; i++)
        {
            dic[projectilesName[i]] = i;
        }

        clearVegetables();
        stats_popup.name = name;
        imagenes[dic[name]].hortaliza.enabled = true;
        imagenes[dic[name]].descripcion.enabled = true;
        stats_popup.hortaliza = imagenes[dic[name]].hortaliza;
        stats_popup.cost.text = dict_cost[name].text;
        stats_popup.damage = dict_damage[name];
        stats_popup.weight = dict_weight[name];
        stats_popup.detonation_time = dict_detonation_time[name];
        stats_popup.velocity = dict_velocity[name];
        stats_popup.damage_radius = dict_damage_radius[name];

        popup.enabled = true;
    }

    public void clearVegetables() {
        for (int i = 0; i < 5; i++)
        {
            imagenes[i].hortaliza.enabled = false;
            imagenes[i].descripcion.enabled = false;
        }
    }

    public void btn_plus() {

        int num = System.Int32.Parse(stats_popup.num_elements.text);
        num += 1;
        if (num < 30)
        {
            stats_popup.num_elements.text = num.ToString();
        }
    }

    public void btn_minus()
    {
        int num = System.Int32.Parse(stats_popup.num_elements.text);
        num -= 1;
        if (num >= 1)
        {
            stats_popup.num_elements.text = num.ToString();
        }
    }
}