using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Game : MonoBehaviour {
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject AmmoBox;
    [SerializeField] private GameObject HealthBox;
    [SerializeField] private Text countDownText;
    [SerializeField] private int timeBetweenRounds = 5;
    [SerializeField] private int timeTurn = 10;

    private Round round;
    private TimerGame timerRounds;
    private bool isBetweenRounds;
    [SerializeField] private EstadoJuego estadoJuego;

    public GameObject Player1 {
        get {
            return this.player1;
        }

        set {
            this.player1 = value;
        }
    }

    public GameObject Player2 {
        get {
            return this.player2;
        }

        set {
            this.player2 = value;
        }
    }

    private void Awake()
    {
        estadoJuego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
    }
    // Use this for initialization
    void Start () {
        //this.estadoJuego = gameObject.GetComponent<EstadoJuego>();
        this.initPlayers();
        this.startRound();

    }

    void FixedUpdate() {

        //random healthBox or AmmoBox
        float i = Random.Range(0, 2000);
        if (i > 1995)
        {
            Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 10, 0);
            float type = Random.Range(0, 2);//50%prob ammmo-health
            if (type == 1)
            {
                Instantiate(HealthBox, position, Quaternion.identity);
            }
            else
            {
                Instantiate(AmmoBox, position, Quaternion.identity);
            }
        }



        if (!this.round.Running && !this.isBetweenRounds) {
            this.endRound();
            this.betweenRounds();
        }
        //Update the countdown
        this.countDownText.text = "Count: " + round.getTimeLeft();

        //Comprobar fin de juego
        if (this.player1.GetComponent<Player>().hasNoCharacters() || this.player2.GetComponent<Player>().hasNoCharacters())
        {
            SceneManager.LoadScene("winScene");
            estadoJuego.coinsPlayer1 = this.player1.GetComponent<Player>().getCoins();
            estadoJuego.numCharactersPlayer1 = this.player1.GetComponent<Player>().getNumAliveCharacters();

            estadoJuego.coinsPlayer2 = this.player2.GetComponent<Player>().getCoins();
            estadoJuego.numCharactersPlayer2 = this.player2.GetComponent<Player>().getNumAliveCharacters();
        } 
    }

    /**
     * Introduce los proyectiles seleccionados en los inventarios de los jugadores
     */
    private void initPlayers() {
        // estas 6 primeras lineas inicializan los inventarios con unos proyectiles predefinidos.
        //
        // cuando se quiera elegir los proyectiles desde el juego basta con comentar estas 6 primeras lineas
        // y descomentar las dos que estan comentadas.

        this.player1.GetComponent<Player>().addToInventory(createProjectile("pastanaga"), 50);
        this.player1.GetComponent<Player>().addToInventory(createProjectile("tomaquet"), 4);
        this.player1.GetComponent<Player>().addToInventory(createProjectile("pebrot"), 1);

        this.player2.GetComponent<Player>().addToInventory(createProjectile("tomaquet"), 50);
        this.player2.GetComponent<Player>().addToInventory(createProjectile("alberginia"), 3);
        this.player2.GetComponent<Player>().addToInventory(createProjectile("ceba"), 1);

        //this.player1.GetComponent<Player>().addToInventory(this.projectilescriptTOprojectile(this.estadoJuego.player1_projectiles));
        //this.player2.GetComponent<Player>().addToInventory(this.projectilescriptTOprojectile(this.estadoJuego.player2_projectiles));

        this.player1.GetComponent<Player>().initInventoryPanel();
        this.player2.GetComponent<Player>().initInventoryPanel();
    }

    /**
     * Metodo que crea un proyectil dado el nombre
     * Solo utilizado para hacer pruebas, para no tener que elegir proyectiles en el juego
     */
    private ProjectileInfo createProjectile(string name) {
        string[] projectilesName = { "pastanaga", "tomaquet", "ceba", "pebrot", "alberginia" };
        float[] damages = { 1, 1, 3, 4, 3 };
        float[] speeds = { 3, 0, 3, 1, 4 };
        float[] weights = { 2, 5, 2, 2, 2 };
        int[] damages_radius = { 1, 2, 5, 4, 5 };
        int[] detonations_time = { 4, 2, 3, 3, 5 };
        string[] costes = { "20", "25", "30", "35", "40" };

        int idx = 0;
        while (!projectilesName[idx].Equals(name)) idx++;

        ProjectileInfo info = new ProjectileInfo();
        info.projectileName = name;
        info.damage = (int)damages[idx];
        info.weight = weights[idx];
        info.speed = speeds[idx];
        info.detonationTime = detonations_time[idx];
        return info;
    }

    /**
     * Metodo que instancia una Ronda
     */
    private void startRound()
    {
        this.round = this.gameObject.AddComponent<Round>();
        this.round.TimeTurn = this.timeTurn;
        this.isBetweenRounds = false;



    }

    /**
     * Metodo que destruye la Ronda actual
     */
    private void endRound() {
        Destroy(this.round);
    }

    /**
     * Metodo con el timer entre rondas
     */
    private void betweenRounds() {
        this.isBetweenRounds = true;
        this.timerRounds = this.gameObject.AddComponent<TimerGame>();
        this.timerRounds.init(this.timeBetweenRounds);
        this.StartCoroutine(this.initTimerRound());
    }


    private Dictionary<ProjectileInfo,int> projectilescriptTOprojectileinfo(Dictionary<ProjectileScript,int> psDict) {
        Dictionary<ProjectileInfo, int> pDict = new Dictionary<ProjectileInfo,int>();
        foreach (KeyValuePair<ProjectileScript,int> ps in psDict) {
            ProjectileInfo p = new ProjectileInfo();
            p.projectileName = ps.Key.name;
            p.damage = (int)ps.Key.damage;
            p.speed = ps.Key.velocity;
            p.weight = ps.Key.weight;
            //ps.key.damage_radius;
            p.detonationTime = ps.Key.detonation_time;
            pDict.Add(p, ps.Value);
        }
        return pDict;
    }


    /**
     * Timer que se ejecuta entre rondas
     */
    IEnumerator initTimerRound() {
        while (!this.timerRounds.TimeOver) {
            yield return null;
        }
        this.timerRounds.stop();
        Destroy(this.timerRounds);
        this.startRound();
    }
}
