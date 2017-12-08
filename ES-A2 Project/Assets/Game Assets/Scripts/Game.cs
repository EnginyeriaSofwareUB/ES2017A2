using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Game : MonoBehaviour {

    [SerializeField] private Buttons buttons;
    [SerializeField] private AudioSource sound1;
    [SerializeField] private AudioSource sound2;
    [SerializeField] private AudioSource sound3;
    [SerializeField] private AudioSource sound4;
    [SerializeField] private AudioSource music;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject AmmoBox;
    [SerializeField] private GameObject HealthBox;
    [SerializeField] private Text countDownText;
    [SerializeField] private int timeBetweenRounds = 5;
    [SerializeField] private int timeTurn = 10;

    public Round round;
    private TimerGame timerRounds;
    private bool isBetweenRounds;
    private EstadoJuego estadoJuego;

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

    void Awake() {
        estadoJuego = EstadoJuego.estadoJuego;
        this.initVariables();
    }

    // Use this for initialization
    void Start() {
        this.startRound();
        changeVolume();
        music.Play();

    }

    void FixedUpdate() {

        if (!this.round.Running && !this.isBetweenRounds) {
            this.endRound();
            this.betweenRounds();

            //random healthBox or AmmoBox
            float i = Random.Range(1, 10);
            for (int k = 0; k < i; k++)
            {
                Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 10, 0);
                float type = Random.Range(0, 1);//50%prob ammmo-health
                if (k % 2 == 0)
                {
                    Instantiate(HealthBox, position, Quaternion.identity);
                }
                else
                {
                    Instantiate(AmmoBox, position, Quaternion.identity);
                }

            }

        }
        //Update the countdown
        if (!this.isBetweenRounds)
            this.countDownText.text = "Count: " + this.round.getTimeLeft() + "  ";
        else
            this.countDownText.text = "Next round in... " + ((int)this.timerRounds.getTimeLeft() + 1) + "  ";

        //Comprobar fin de juego
        if (this.player1.GetComponent<Player>().hasNoCharacters() || this.player2.GetComponent<Player>().hasNoCharacters()) {
            this.estadoJuego.setRemaningCharacters(this.player1.GetComponent<Player>().getAliveCharacters().Count, this.player2.GetComponent<Player>().getAliveCharacters().Count);
            this.buttons.goToWinScene();
        }
    }

    private void initVariables() {
        Vector2 initPosition = new Vector2(-10, 0);
        this.initVariables(ref initPosition, this.player1, this.estadoJuego.player1);
        this.initVariables(ref initPosition, this.player2, this.estadoJuego.player2);
    }

    private void initVariables(ref Vector2 initPosition, GameObject playerObject, PlayerUI playerUI) {
        Player player = playerObject.GetComponent<Player>();
        List<GameObject> characters =  playerUI.Characters;
        List<ProjectileInfo> projectiles = playerUI.Projectiles;

        player.Characters = new List<Character>();
        foreach (GameObject characterPrefab in characters) {
            GameObject character = Instantiate(characterPrefab, player.transform, true);
            character.transform.position = initPosition;
            initPosition.x += 2;
            player.Characters.Add(character.GetComponent<Character>());
            character.SetActive(true);
        }

        player.Inventory.initInventory(projectiles);
    }

    /**
     * Metodo que instancia una Ronda
     */
    private void startRound() {
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



    private void changeVolume() {
        Debug.Log(estadoJuego.volumenEfectos);
        sound1.volume = estadoJuego.volumenEfectos / 10.0F;
        sound2.volume = estadoJuego.volumenEfectos / 10.0F;
        sound3.volume = estadoJuego.volumenEfectos / 10.0F;
        sound4.volume = estadoJuego.volumenEfectos / 10.0F;
        music.volume = estadoJuego.volumenMusica / 10.0F;

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
