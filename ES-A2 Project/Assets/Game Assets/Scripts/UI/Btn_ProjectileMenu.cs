using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item {
    public string name;
    public Sprite background;
    public Sprite hortaliza;
    public Button.ButtonClickedEvent thingToDo;
    public GameObject projectile;
}

[System.Serializable]
public class Img_and_desc {
    public string name;
    public Image hortaliza;
    public Image descripcion;
}

[System.Serializable]
public class Stats_popup {
    public string name;
    public Image hortaliza;
    public Text damage;
    public Text velocity;
    public Text weight;
    public Text damage_radius;
    public Text detonation_time;
    public Text cost;
    public Image description;
    public Text num_elements;
}

[System.Serializable]
public class user_active {
    public Image player1;
    public Image player1active;
    public Image player2;
    public Image player2active;
    public Button buttonText;
}

[System.Serializable]
public class GameControl {
    public Dictionary<ProjectileScript, int> player1_projectiles = new Dictionary<ProjectileScript, int>();
    public Dictionary<ProjectileScript, int> player2_projectiles = new Dictionary<ProjectileScript, int>();
    public int turn; //1 - Player1 2 - Player2
    public Text player1_money;
    public Text player2_money;
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

    //Player 1 container
    [SerializeField] private List<Image> player1_button_background;
    [SerializeField] private List<Image> player1_button_projectile;
    [SerializeField] private List<Text> player1_elements;

    //Player 2 container
    [SerializeField] private List<Image> player2_button_background;
    [SerializeField] private List<Image> player2_button_projectile;
    [SerializeField] private List<Text> player2_elements;

    //Values of all projectiles
    private string[] projectilesName = { "pastanaga", "tomaquet", "ceba", "pebrot", "alberginia" };
    float[] damages = { 1, 1, 3, 4, 3 };
    float[] speeds = { 3, 0, 3, 1, 4 };
    float[] weights = { 2, 5, 2, 2, 2 };
    int[] damages_radius = { 1, 2, 5, 4, 5 };
    int[] detonations_time = { 4, 2, 3, 3, 5 };
    string[] costes = { "20", "25", "30", "35", "40" };

    [SerializeField] private Slider sl_damage;
    [SerializeField] private Slider sl_speed;
    [SerializeField] private Slider sl_weight;
    [SerializeField] private Slider sl_damage_radius;
    [SerializeField] private Slider sl_det_time;

    public Dictionary<string, ProjectileScript> list_projectiles = new Dictionary<string, ProjectileScript>();

    //Game control
    [SerializeField] private GameControl game_control;

    private Buttons buttons;

    private EstadoJuego estadoJuego;

    [SerializeField] private Sprite btn_play;
    [SerializeField] private Sprite btn_next;

    void Awake() {
        estadoJuego = EstadoJuego.estadoJuego;
        game_control.player1_money.text = estadoJuego.player1.Coins.ToString();
        game_control.player2_money.text = estadoJuego.player2.Coins.ToString();
    }

    // Use this for initialization
    void Start() {
        popup.enabled = false;
        init_projectiles();
        init_attributes();
        clearUsersProjectiles();



        game_control.turn = 1;
        this.buttons = this.GetComponent<Buttons>();
    }

    // Update is called once per frame
    void Update() {

        if (game_control.turn == 1) {
            users_active.player1.enabled = false;
            users_active.player1active.enabled = true;
            users_active.player2.enabled = true;
            users_active.player2active.enabled = false;
            users_active.buttonText.image.sprite = btn_next;
        } else {
            users_active.player1.enabled = true;
            users_active.player1active.enabled = false;
            users_active.player2.enabled = false;
            users_active.player2active.enabled = true;
            users_active.buttonText.image.sprite = btn_play;
        }
    }

    public void PlayOnClick() {
        if (game_control.turn == 2) {
            this.estadoJuego.setProjectileMenuValues(this.game_control.player1_projectiles, this.game_control.player2_projectiles);
            this.buttons.goToGameScene();
        }
        game_control.turn = 2;
    }

    public void BackOnClick() {
        this.buttons.goToCharacterSelectMenu();
    }

    public void CancelOnClick() {
        popup.enabled = false;
    }

    public void BuyOnClick() {
        popup.enabled = false;

        if (game_control.turn == 1) {
            int futureCoins = System.Int32.Parse(game_control.player1_money.text)
                - System.Int32.Parse(stats_popup.num_elements.text) * System.Int32.Parse(stats_popup.cost.text);

            if (futureCoins >= 0) {
                if (game_control.player1_projectiles.ContainsKey(list_projectiles[stats_popup.name])) {
                    game_control.player1_projectiles[list_projectiles[stats_popup.name]] += System.Int32.Parse(stats_popup.num_elements.text);
                } else {
                    game_control.player1_projectiles[list_projectiles[stats_popup.name]] = System.Int32.Parse(stats_popup.num_elements.text);
                }
                game_control.player1_money.text = futureCoins.ToString();
                updateBoughtProjectiles();
            }
        } else {

            int futureCoins = System.Int32.Parse(game_control.player2_money.text)
                - System.Int32.Parse(stats_popup.num_elements.text) * System.Int32.Parse(stats_popup.cost.text);

            if (futureCoins >= 0) {
                if (game_control.player2_projectiles.ContainsKey(list_projectiles[stats_popup.name])) {
                    game_control.player2_projectiles[list_projectiles[stats_popup.name]] += System.Int32.Parse(stats_popup.num_elements.text);
                } else {
                    game_control.player2_projectiles[list_projectiles[stats_popup.name]] = System.Int32.Parse(stats_popup.num_elements.text);
                }
                game_control.player2_money.text = futureCoins.ToString();
                updateBoughtProjectiles();
            }
        }
    }

    public int indexToAddPlayer1() {
        int i = 1;

        foreach (var projectile in game_control.player1_projectiles) {
            ProjectileScript projectileScript = projectile.Key.GetComponent<ProjectileScript>();
            if (projectileScript.projectileImage.sprite == stats_popup.hortaliza.sprite)
                return i;
            i++;
        }
        return game_control.player1_projectiles.Count - 1;
    }

    public int indexToAddPlayer2() {
        int i = 1;

        foreach (var projectile in game_control.player2_projectiles) {
            ProjectileScript projectileScript = projectile.Key.GetComponent<ProjectileScript>();
            if (projectileScript.projectileImage.sprite == stats_popup.hortaliza.sprite)
                return i;
            i++;
        }
        return game_control.player2_projectiles.Count - 1;
    }

    public void updateBoughtProjectiles() {
        if (game_control.turn == 1) {
            int indice = indexToAddPlayer1();

            player1_button_background[indice].enabled = true;
            player1_button_projectile[indice].sprite = stats_popup.hortaliza.sprite;
            player1_button_projectile[indice].enabled = true;
            player1_elements[indice].enabled = true;
            player1_elements[indice].text = game_control.player1_projectiles[list_projectiles[stats_popup.name]].ToString();
        } else {
            int indice = indexToAddPlayer2();

            player2_button_background[indice].enabled = true;
            player2_button_projectile[indice].sprite = stats_popup.hortaliza.sprite;
            player2_button_projectile[indice].enabled = true;
            player2_elements[indice].enabled = true;
            player2_elements[indice].text = game_control.player2_projectiles[list_projectiles[stats_popup.name]].ToString();
        }
    }

    public void NextOnClick() {
        popup.enabled = false;
        game_control.turn = 2;
    }

    public void init_projectiles() {
        int i = 0;

        foreach (var projectile in projectiles) {
            GameObject newImage = Instantiate(sampleImage) as GameObject;
            ProjectileScript projectileScript = newImage.GetComponent<ProjectileScript>();
            projectileScript.background.sprite = projectile.background;
            projectileScript.projectileImage.sprite = projectile.hortaliza;
            projectileScript.name = projectile.name;
            projectileScript.button.onClick = projectile.thingToDo;
            projectileScript.projectile = projectile.projectile;

            list_projectiles[projectile.name] = projectileScript;

            //newImage.transform.SetParent(contentParent);

            i += 1;
        }
    }

    public void init_attributes() {
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

        Text[] costs = { myText1, myText2, myText3, myText4, myText5 };

        int i = 0;

        foreach (var projectile in projectilesName) {
            list_projectiles[projectile].cost = costs[i];
            i += 1;
        }
    }

    public void DoSomething(string name) {
        Dictionary<string, int> dic = new Dictionary<string, int>();
        ProjectileScript projectileScript = list_projectiles[name];
        Projectile projectile = projectileScript.projectile.GetComponent<Projectile>();

        for (int i = 0; i < 5; i++) {
            dic[projectilesName[i]] = i;
        }
        clearVegetables();
        stats_popup.name = name;
        imagenes[dic[name]].hortaliza.enabled = true;
        imagenes[dic[name]].descripcion.enabled = true;
        stats_popup.hortaliza = imagenes[dic[name]].hortaliza;
        stats_popup.cost.text = projectileScript.cost.text;
        stats_popup.damage.text = projectile.Damage.ToString();
        sl_damage.value = projectile.Damage;
        stats_popup.weight.text = projectile.Weight.ToString();
        sl_weight.value = projectile.Weight;
        stats_popup.detonation_time.text = projectile.DetonationTime.ToString();
        sl_det_time.value = projectile.DetonationTime;
        stats_popup.velocity.text = projectile.Speed.ToString();
        sl_speed.value = projectile.Speed;
        stats_popup.damage_radius.text = projectile.DamageRadius.ToString();
        sl_damage_radius.value = projectile.DamageRadius;
        stats_popup.num_elements.text = "1";

        popup.enabled = true;
    }

    public void clearVegetables() {
        for (int i = 0; i < imagenes.Count; i++) {
            imagenes[i].hortaliza.enabled = false;
            imagenes[i].descripcion.enabled = false;
        }
    }

    public void btn_plus() {

        int num = System.Int32.Parse(stats_popup.num_elements.text);
        num += 1;
        if (num < 30) {
            stats_popup.num_elements.text = num.ToString();
        }
    }

    public void btn_minus() {
        int num = System.Int32.Parse(stats_popup.num_elements.text);
        num -= 1;
        if (num >= 1) {
            stats_popup.num_elements.text = num.ToString();
        }
    }

    public void clearUsersProjectiles() {

        int i = 0;
        foreach (var button in player1_button_background) {
            if (i != 0)
                button.enabled = false;
            i++;
        }
        i = 0;
        foreach (var element in player1_elements) {
            if (i != 0)
                element.enabled = false;
            i++;
        }
        i = 0;
        foreach (var button in player1_button_projectile) {
            if (i != 0)
                button.enabled = false;
            i++;
        }
        i = 0;
        foreach (var element in player2_elements) {
            if (i != 0)
                element.enabled = false;
            i++;
        }
        i = 0;
        foreach (var button in player2_button_projectile) {
            if (i != 0)
                button.enabled = false;
            i++;
        }
        i = 0;
        foreach (var element in player2_button_background) {
            if (i != 0)
                element.enabled = false;
            i++;
        }
    }
}