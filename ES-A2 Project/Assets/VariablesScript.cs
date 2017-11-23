using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariablesScript : MonoBehaviour {

    [SerializeField] private Text textCounterMoney;
    [SerializeField] private Slider sliderMoney;
    [SerializeField] private Text textCounterAnimal;
    [SerializeField] private Slider sliderAnimal;

    public void onChangeSliderMoney()
    {
        Text coins = textCounterMoney;
        coins.text = ((int)sliderMoney.value).ToString();
    }

    public void onChangeSliderAnimal()
    {
        Text animals = textCounterAnimal;
        animals.text = ((int)sliderAnimal.value).ToString();
    }
}
