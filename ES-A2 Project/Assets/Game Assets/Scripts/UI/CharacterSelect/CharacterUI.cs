using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI :MonoBehaviour {
    [SerializeField] private Sprite characterIcon;
    [SerializeField] private GameObject characterType;

    public Sprite CharacterIcon {
        get {
            return characterIcon;
        }

        set {
            characterIcon = value;
        }
    }

    public GameObject CharacterType {
        get {
            return characterType;
        }

        set {
            characterType = value;
        }
    }
}
