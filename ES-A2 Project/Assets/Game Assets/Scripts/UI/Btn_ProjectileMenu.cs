using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;


[System.Serializable]
public class PopupElements {
    public string name;
    public Image hortaliza;
    public Text damage;
    public Text velocity;
    public Text weight;
    public Text damage_radius;
    public Text detonation_time;
    public Text cost;
    public int baseCost;
    public Text description;
    public Text num_elements;
    public Button buyButton;
}

[System.Serializable]
public class PlayerIcons {
    public Image player1;
    public Image player1active;
    public Image player2;
    public Image player2active;
    public Button buttonText;
}

[System.Serializable]
public class GameValues {
    public Dictionary<ProjectileScript, int> player1Projectiles = new Dictionary<ProjectileScript, int>();
    public Dictionary<ProjectileScript, int> player2Projectiles = new Dictionary<ProjectileScript, int>();
    public int turn; //1 - Player1 2 - Player2
    public Text player1Money;
    public Text player2Money;
}

public class Btn_ProjectileMenu : MonoBehaviour {

    [SerializeField] private Canvas popup;

    [SerializeField] private PlayerIcons playerIcons;

    //Center container
    [SerializeField] private Transform contentParent;

    [SerializeField] private GameObject projectileButtonsContainer;
    [SerializeField] private PopupElements stats_popup;

    //Player 1 container
    [SerializeField] private GameObject player1ProjectileGroup;

    //Player 2 container
    [SerializeField] private GameObject player2ProjectileGroup;

    [SerializeField] private GameObject projectilePlayer;

    [SerializeField] private Slider sl_damage;
    [SerializeField] private Slider sl_speed;
    [SerializeField] private Slider sl_weight;
    [SerializeField] private Slider sl_damage_radius;
    [SerializeField] private Slider sl_det_time;

    private List<Button> projectilesButtons;
    private Dictionary<string, GameObject> player1ProjectileButtons = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> player2ProjectileButtons = new Dictionary<string, GameObject>();

    public Dictionary<string, ProjectileScript> projectileList = new Dictionary<string, ProjectileScript>();

    //Game control
    [SerializeField] private GameValues gameValues;

    private Buttons buttons;

    private EstadoJuego estadoJuego;

    [SerializeField] private Sprite btn_play;
    [SerializeField] private Sprite btn_next;

    void Awake() {
        estadoJuego = EstadoJuego.estadoJuego;
        gameValues.player1Money.text = estadoJuego.player1.Coins.ToString();
        gameValues.player2Money.text = estadoJuego.player2.Coins.ToString();
    }

    // Use this for initialization
    void Start() {
        popup.enabled = false;
        init_projectiles();

        gameValues.turn = 1;
        this.activatePlayer1();
        this.buttons = this.GetComponent<Buttons>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void activatePlayer1() {
        playerIcons.player1.enabled = false;
        playerIcons.player1active.enabled = true;
        playerIcons.player2.enabled = true;
        playerIcons.player2active.enabled = false;
        playerIcons.buttonText.image.sprite = btn_next;
    }

    private void activatePlayer2() {
        playerIcons.player1.enabled = true;
        playerIcons.player1active.enabled = false;
        playerIcons.player2.enabled = false;
        playerIcons.player2active.enabled = true;
        playerIcons.buttonText.image.sprite = btn_play;
    }

    public void PlayOnClick() {
        if (gameValues.turn == 2) {
            this.estadoJuego.setProjectileMenuValues(this.gameValues.player1Projectiles, this.gameValues.player2Projectiles);
            this.buttons.goToGameScene();
        }
        this.activatePlayer2();
        gameValues.turn = 2;
    }

    public void BackOnClick() {
        this.buttons.goToCharacterSelectMenu();
    }

    public void CancelOnClick() {
        popup.enabled = false;
    }

    public void BuyOnClick() {
        popup.enabled = false;

        if (gameValues.turn == 1) {
            this.buyProjectiles(gameValues.player1Money, gameValues.player1Projectiles);
        } else {
            this.buyProjectiles(gameValues.player2Money, gameValues.player2Projectiles);
        }
        updateBoughtProjectiles();
    }

    private void buyProjectiles(Text playerMoney, Dictionary<ProjectileScript, int> playerProjectiles) {
        int futureCoins = int.Parse(playerMoney.text) - int.Parse(stats_popup.cost.text);

        if (playerProjectiles.ContainsKey(projectileList[stats_popup.name])) {
            playerProjectiles[projectileList[stats_popup.name]] = int.Parse(stats_popup.num_elements.text);
        } else {
            playerProjectiles[projectileList[stats_popup.name]] = int.Parse(stats_popup.num_elements.text);
        }
        playerMoney.text = futureCoins.ToString();

    }

    public void updateBoughtProjectiles() {
        if (gameValues.turn == 1) {
            this.updatePlayerProjectiles(this.player1ProjectileGroup, this.player1ProjectileButtons);
        } else {
            this.updatePlayerProjectiles(this.player2ProjectileGroup, this.player2ProjectileButtons);
        }
    }

    public void updatePlayerProjectiles(GameObject projectileGroup, Dictionary<String, GameObject> playerButtons) {
        string projectileName = this.stats_popup.name;
        GameObject playerProjectileButton;
        if (!playerButtons.ContainsKey(projectileName)) {
            playerProjectileButton = Instantiate(this.projectilePlayer, projectileGroup.transform);
            playerProjectileButton.transform.GetChild(0).GetComponent<Image>().sprite = this.stats_popup.hortaliza.sprite;

            playerButtons.Add(projectileName, playerProjectileButton);
        } else {
            playerProjectileButton = playerButtons[projectileName];
        }

        if (int.Parse(this.stats_popup.num_elements.text) == 0) {
            playerButtons.Remove(projectileName);
            Destroy(playerProjectileButton);
        }

        playerProjectileButton.GetComponentInChildren<Text>().text = this.stats_popup.num_elements.text;
    }

    public void NextOnClick() {
        popup.enabled = false;
        gameValues.turn = 2;
    }

    public void init_projectiles() {
        foreach (Button projectileButton in this.projectileButtonsContainer.GetComponentsInChildren<Button>()) {
            ProjectileScript projectileScript = projectileButton.GetComponent<ProjectileScript>();
            projectileList.Add(projectileScript.projectileName, projectileScript);
        }
    }

    public void DoSomething(string name) {
        if (this.gameValues.turn == 1) {
            this.initPopupVariables(name, gameValues.player1Projectiles);
        } else {
            this.initPopupVariables(name, gameValues.player2Projectiles);
        }
        this.checkIfCanBuy();
        popup.enabled = true;
    }

    private void initPopupVariables(string name, Dictionary<ProjectileScript, int> playerProjectiles) {
        ProjectileScript projectileScript = projectileList[name];
        Projectile projectile = projectileScript.projectile.GetComponent<Projectile>();
        stats_popup.cost.text = "0";
        stats_popup.name = name;
        stats_popup.hortaliza.GetComponent<Image>().sprite = projectileScript.projectileImage.sprite;
        stats_popup.baseCost = projectileScript.cost;

        this.setSlidersValues(projectile);

        if (playerProjectiles.ContainsKey(projectileScript)) {
            stats_popup.num_elements.text = playerProjectiles[projectileScript].ToString();
        } else {
            stats_popup.num_elements.text = "0";
        }

        stats_popup.description.text = projectileScript.description;

    }

    private void setSlidersValues(Projectile projectile) {
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
    }

    public void btn_plus() {
        int itemCount = int.Parse(stats_popup.num_elements.text) + 1;

        stats_popup.num_elements.text = itemCount.ToString();
        stats_popup.cost.text = (int.Parse(stats_popup.cost.text) + stats_popup.baseCost).ToString();
        this.checkIfCanBuy();
    }

    public void btn_minus() {
        int itemCount = int.Parse(stats_popup.num_elements.text) - 1;
        if (itemCount >= 0) {
            stats_popup.num_elements.text = itemCount.ToString();
            stats_popup.cost.text = (int.Parse(stats_popup.cost.text) - stats_popup.baseCost).ToString();
            this.checkIfCanBuy();
        }
    }

    private void checkIfCanBuy() {
        int cost = int.Parse(stats_popup.cost.text);

        if (this.gameValues.turn == 1) {
            this.checkIfPlayerCanBuy(int.Parse(this.gameValues.player1Money.text), cost);
        } else {
            this.checkIfPlayerCanBuy(int.Parse(this.gameValues.player2Money.text), cost);
        }
    }

    private void checkIfPlayerCanBuy(int money, int cost) {
        if (cost > money) {
            this.stats_popup.buyButton.interactable = false;
        } else {
            this.stats_popup.buyButton.interactable = true;
        }
    }
}