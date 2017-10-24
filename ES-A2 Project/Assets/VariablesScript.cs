using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariablesScript : MonoBehaviour {

    public void onChangeMoneySlider()
    {
        Text coins = GameObject.Find("TextCounterMoney").GetComponent<Text>();
        coins.text = ((int)GameObject.Find("SliderMoney").GetComponent<Slider>().value).ToString();
    }
}
