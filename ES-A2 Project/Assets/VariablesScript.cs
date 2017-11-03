using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariablesScript : MonoBehaviour {

    [SerializeField] private Text textCounterMoney;
    [SerializeField] private Slider sliderMoney;

    public void onChangeSlider()
    {
        Text coins = textCounterMoney;
        coins.text = ((int)sliderMoney.value).ToString();
    }
}
