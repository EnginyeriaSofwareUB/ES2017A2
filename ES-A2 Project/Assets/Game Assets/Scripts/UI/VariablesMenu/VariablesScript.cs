using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariablesScript : MonoBehaviour {
    private EstadoJuego estadoJuego;
    [SerializeField] Buttons buttons;
    [SerializeField] private Text textCounterMoney;
    [SerializeField] private Slider sliderMoney;
    [SerializeField] private Text textCounterAnimal;
    [SerializeField] private Slider sliderAnimal;


    void Start() {
        this.estadoJuego = EstadoJuego.estadoJuego;
        this.estadoJuego.resetInfo();
    }


    public void onChangeSliderMoney() {
        Text coins = textCounterMoney;
        coins.text = ((int)sliderMoney.value).ToString();
    }

    public void onChangeSliderAnimal() {
        Text animals = textCounterAnimal;
        animals.text = ((int)sliderAnimal.value).ToString();
    }

    public void onClickContinue() {
        this.estadoJuego.setVariablesMenuValues((int) this.sliderMoney.value, (int) this.sliderAnimal.value);
        this.buttons.goToCharacterSelectMenu();
    }

    public void onClickBack() {
        this.buttons.goToIndexMenu();
    }
}
