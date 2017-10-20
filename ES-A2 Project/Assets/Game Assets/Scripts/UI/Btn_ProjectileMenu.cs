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
    public Text buttonText;
}

[System.Serializable]
public class GameControl
{
    public Dictionary<ProjectileScript, int> player1_projectiles = new Dictionary<ProjectileScript, int>();
    public Dictionary<ProjectileScript, int> player2_projectiles = new Dictionary<ProjectileScript, int>();
    public int turn; //1 - Player1 2 - Player2
    public Text player1_money;
    public Text player2_money;
}

    public class Btn_ProjectileMenu : MonoBehaviour {

    [SerializeField] private Canvas popup;

    [SerializeField] private Image imagen_prueba;

    [SerializeField] private List<Img_and_desc> imagenes;

    [SerializeField] private user_active users_active;

    //Center container
    [SerializeField] private GameObject sampleImage;
    [SerializeField] private Transform contentParent;
    [SerializeField] private List<Item> projectiles;
    [SerializeField] private Stats_popup stats_popup;

    //Player 1 container
    [SerializeField] private List<Image> player1_button_background;
    [SerializeField] private List<Image> player1_button_projectile;
    [SerializeField] private List<Text> player1_elements;

    //Player 2 container
    [SerializeField] private List<Image> player2_button_background;
    [SerializeField] private List<Image> player2_button_projectile;
    [SerializeField] private List<Text> player2_elements;

    //Values of all projectiles
    string[] projectilesName = { "pastanaga", "tomaquet", "ceba", "pebrot", "alberginia" };

    private Dictionary<string, ProjectileScript> list_projectiles = new Dictionary<string, ProjectileScript>();
    private Dictionary<string, Text> dict_cost = new Dictionary<string, Text>();

    //Game control
    [SerializeField] private GameControl game_control;

    // Use this for initialization
    void Start () {
        popup.enabled = false;
        init_projectiles();
        init_attributes();
        clearUsersProjectiles();
        game_control.turn = 1;
        game_control.player1_money.text = "3000";
        game_control.player2_money.text = "3000";
    }
	
	// Update is called once per frame
	void Update () {

		if(game_control.turn == 1)
        {
            users_active.player1.enabled = false;
            users_active.player1active.enabled = true;
            users_active.player2.enabled = true;
            users_active.player2active.enabled = false;
            users_active.buttonText.text = "Next";
        }
        else
        {
            users_active.player1.enabled = true;
            users_active.player1active.enabled = false;
            users_active.player2.enabled = false;
            users_active.player2active.enabled = true;
            users_active.buttonText.text = "Play!";
        }
    }

	public void PlayOnClick()
	{
        if(game_control.turn == 2)
            SceneManager.LoadScene("Test Scene");
        game_control.turn = 2;
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
        
        if(game_control.turn == 1)
        {
            int futureCoins = System.Int32.Parse(game_control.player1_money.text) 
                - System.Int32.Parse(stats_popup.num_elements.text) * System.Int32.Parse(stats_popup.cost.text);

            if(futureCoins >= 0)
            {
                if (game_control.player1_projectiles.ContainsKey(list_projectiles[stats_popup.name]))
                {
                    game_control.player1_projectiles[list_projectiles[stats_popup.name]] += System.Int32.Parse(stats_popup.num_elements.text);
                }
                else
                {
                    game_control.player1_projectiles[list_projectiles[stats_popup.name]] = System.Int32.Parse(stats_popup.num_elements.text);
                }
                game_control.player1_money.text = futureCoins.ToString();
            }
        }
        else
        {

            int futureCoins = System.Int32.Parse(game_control.player2_money.text)
                - System.Int32.Parse(stats_popup.num_elements.text) * System.Int32.Parse(stats_popup.cost.text);

            if (futureCoins >= 0)
            {
                if (game_control.player2_projectiles.ContainsKey(list_projectiles[stats_popup.name]))
                {
                    game_control.player2_projectiles[list_projectiles[stats_popup.name]] += System.Int32.Parse(stats_popup.num_elements.text);
                }
                else
                {
                    game_control.player2_projectiles[list_projectiles[stats_popup.name]] = System.Int32.Parse(stats_popup.num_elements.text);
                }
                game_control.player2_money.text = futureCoins.ToString();
            }
        }
        updateBoughtProjectiles();
    }

    public void updateBoughtProjectiles()
    {
        if (game_control.turn == 1)
        {
            switch (stats_popup.name)
            {
                case "pastanaga":
                    player1_button_background[0].enabled = true;
                    player1_button_projectile[0].enabled = true;
                    player1_elements[0].enabled = true;
                    player1_elements[0].text = game_control.player1_projectiles[list_projectiles[stats_popup.name]].ToString();
                    break;
                case "tomaquet":
                    player1_button_background[1].enabled = true;
                    player1_button_projectile[1].enabled = true;
                    player1_elements[1].enabled = true;
                    player1_elements[1].text = game_control.player1_projectiles[list_projectiles[stats_popup.name]].ToString();
                    break;
                case "ceba":
                    player1_button_background[2].enabled = true;
                    player1_button_projectile[2].enabled = true;
                    player1_elements[2].enabled = true;
                    player1_elements[2].text = game_control.player1_projectiles[list_projectiles[stats_popup.name]].ToString();
                    break;
                case "pebrot":
                    player1_button_background[3].enabled = true;
                    player1_button_projectile[3].enabled = true;
                    player1_elements[3].enabled = true;
                    player1_elements[3].text = game_control.player1_projectiles[list_projectiles[stats_popup.name]].ToString();
                    break;
                case "alberginia":
                    player1_button_background[4].enabled = true;
                    player1_button_projectile[4].enabled = true;
                    player1_elements[4].enabled = true;
                    player1_elements[4].text = game_control.player1_projectiles[list_projectiles[stats_popup.name]].ToString();
                    break;
            }
        }
        else
        {
            switch (stats_popup.name)
            {
                case "pastanaga":
                    player2_button_background[0].enabled = true;
                    player2_button_projectile[0].enabled = true;
                    player2_elements[0].enabled = true;
                    player2_elements[0].text = game_control.player2_projectiles[list_projectiles[stats_popup.name]].ToString();
                    break;
                case "tomaquet":
                    player2_button_background[1].enabled = true;
                    player2_button_projectile[1].enabled = true;
                    player2_elements[1].enabled = true;
                    player2_elements[1].text = game_control.player2_projectiles[list_projectiles[stats_popup.name]].ToString();
                    break;
                case "ceba":
                    player2_button_background[2].enabled = true;
                    player2_button_projectile[2].enabled = true;
                    player2_elements[2].enabled = true;
                    player2_elements[2].text = game_control.player2_projectiles[list_projectiles[stats_popup.name]].ToString();
                    break;
                case "pebrot":
                    player2_button_background[3].enabled = true;
                    player2_button_projectile[3].enabled = true;
                    player2_elements[3].enabled = true;
                    player2_elements[3].text = game_control.player2_projectiles[list_projectiles[stats_popup.name]].ToString();
                    break;
                case "alberginia":
                    player2_button_background[4].enabled = true;
                    player2_button_projectile[4].enabled = true;
                    player2_elements[4].enabled = true;
                    player2_elements[4].text = game_control.player2_projectiles[list_projectiles[stats_popup.name]].ToString();
                    break;
            }
        }
    }
    public void NextOnClick()
    {
        popup.enabled = false;
        game_control.turn = 2;
    }

    public void init_projectiles()
    {
        float[] damages = { 1, 2, 3, 4, 5 };
        float[] speeds = { 1, 2, 3, 4, 5 };
        float[] weights = { 1, 2, 3, 4, 5 };
        int[] damages_radius = { 1, 2, 3, 4, 5 };
        int[] detonations_time = { 1, 2, 3, 4, 5 };

        int i = 0;

        foreach (var projectile in projectiles)
        {
            GameObject newImage = Instantiate(sampleImage) as GameObject;
            ProjectileScript projectileScript = newImage.GetComponent<ProjectileScript>();
            projectileScript.background.sprite = projectile.background;
            projectileScript.projectileImage.sprite = projectile.hortaliza;
            projectileScript.name = projectile.name;
            projectileScript.button.onClick = projectile.thingToDo;           
            projectileScript.damage = damages[i];
            projectileScript.velocity = speeds[i];
            projectileScript.weight = weights[i];
            projectileScript.damage_radius = damages_radius[i];
            projectileScript.detonation_time = detonations_time[i];

            list_projectiles[projectile.name] = projectileScript;

            //newImage.transform.SetParent(contentParent);

            i += 1;
        }
    }

    public void init_attributes()
    {
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
            list_projectiles[projectile].cost = costs[i];
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
        stats_popup.cost.text = list_projectiles[name].cost.text;
        stats_popup.damage = list_projectiles[name].damage;
        stats_popup.weight = list_projectiles[name].weight;
        stats_popup.detonation_time = list_projectiles[name].detonation_time;
        stats_popup.velocity = list_projectiles[name].velocity;
        stats_popup.damage_radius = list_projectiles[name].damage_radius;
        stats_popup.num_elements.text = "1";

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
    
    public void clearUsersProjectiles() {

        foreach(var button in player1_button_background) {
            button.enabled = false;
        }
        foreach (var element in player1_elements)
        {
            element.enabled = false;
        }
        foreach (var button in player1_button_projectile)
        {
            button.enabled = false;
        }
        foreach (var element in player2_elements)
        {
            element.enabled = false;
        }
        foreach (var button in player2_button_projectile)
        {
            button.enabled = false;
        }
        foreach (var element in player2_button_background)
        {
            element.enabled = false;
        }
    }
}