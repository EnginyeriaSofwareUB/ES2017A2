using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterMenu : MonoBehaviour {
    private int numCharacters;
    private bool isPlayer1Selecting;
    private EstadoJuego estadoJuego;

    [SerializeField] private Buttons buttons;

    [SerializeField] private Text player1_money;
    [SerializeField] private Text player2_money;
    [SerializeField] private user_active users_active;

    [SerializeField] private List<Button> charactersButtons;
    [SerializeField] private Button playButton;

    [SerializeField] private List<GameObject> player1Slots;
    [SerializeField] private List<GameObject> player2Slots;

    private List<GameObject> player1Characters = new List<GameObject>();
    private List<GameObject> player2Characters = new List<GameObject>();

    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;
    [SerializeField] private GameObject panel3;


    void Awake() {
        estadoJuego = EstadoJuego.estadoJuego;
        player1_money.text = estadoJuego.player1.Coins.ToString();
        player2_money.text = estadoJuego.player2.Coins.ToString();
        numCharacters = estadoJuego.player1.CharactersCount;
    }

    // Use this for initialization
    void Start() {
        drawMenu();
        isPlayer1Selecting = true;
        playButton.enabled = false;
    }

    private void Update() {
        if (isPlayer1Selecting) {
            this.panel3.SetActive(false);
            this.panel2.SetActive(false);
            this.panel1.SetActive(true);
            users_active.player1.enabled = false;
            users_active.player1active.enabled = true;
            users_active.player2.enabled = true;
            users_active.player2active.enabled = false;
        } else {
            this.panel3.SetActive(false);
            this.panel2.SetActive(true);
            this.panel1.SetActive(false);
            users_active.player1.enabled = true;
            users_active.player1active.enabled = false;
            users_active.player2.enabled = false;
            users_active.player2active.enabled = true;
        }
        if (this.player1Characters.Count == numCharacters && this.player2Characters.Count == numCharacters) {
            this.panel2.SetActive(false);
            this.panel1.SetActive(false);
            this.panel3.SetActive(true);
            playButton.enabled = true;
            users_active.player1.enabled = true;
            users_active.player1active.enabled = false;
            users_active.player2.enabled = true;
            users_active.player2active.enabled = false;
        }
        if (!canSelect(1) && !canSelect(2)) {
            foreach (Button characterButton in this.charactersButtons) {
                characterButton.enabled = false;
            }
        }
        if (numCharacters == 0) {
            playButton.enabled = false;
        }
    }

    public void onClickCharacter(GameObject button) {
        CharacterUI characterUI = button.GetComponent<CharacterUI>();
        if (isPlayer1Selecting && this.player1Slots.Count > 0) {
            this.addCharacter(this.player1Characters, this.player1Slots, characterUI);
        } else if (!isPlayer1Selecting && this.player2Slots.Count > 0) {
            this.addCharacter(this.player2Characters, this.player2Slots, characterUI);

        }
        isPlayer1Selecting = !isPlayer1Selecting;
    }

    private void addCharacter(List<GameObject> playerCharacters, List<GameObject> playerSlots, CharacterUI characterUI) {
        GameObject t = playerSlots[0];
        t.GetComponent<RawImage>().texture = characterUI.CharacterIcon.texture;
        playerCharacters.Add(characterUI.CharacterType);
        playerSlots.RemoveAt(0);
    }

    /// <summary>
    /// Funcion encargada de dibujar los circulos del menu (dependiendo de cuantos personajes hay)
    /// </summary>
    /// <returns></returns>
    public void drawMenu() {
        for (int i = numCharacters; i < this.player1Slots.Count; i++) {
            this.player1Slots[i].SetActive(false);
            this.player2Slots[i].SetActive(false);

        }
    }

    public bool canSelect(int player) {
        if (player == 1) {
            return this.player1Characters.Count < numCharacters;
        }
        return this.player2Characters.Count < numCharacters;
    }
    
    public void onClickContinue() {
        this.estadoJuego.setCharactersMenuValues(this.player1Characters, this.player2Characters);
        this.buttons.goToProjectilesSelectMenu();
    }

    public void onClickBack() {
        this.buttons.goToVariablesMenu();
    }
}


