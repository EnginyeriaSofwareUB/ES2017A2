using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariablesScript : MonoBehaviour {

    //[SerializeField] private Text coins;

    public void onChangeMoneySlider()
    {
        Text coins = GameObject.Find("TextCounterMoney").GetComponent<Text>();
        coins.text = ((int)GameObject.Find("SliderMoney").GetComponent<Slider>().value).ToString();
    }
}
