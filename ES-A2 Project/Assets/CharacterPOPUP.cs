using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CharacterPOPUP : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private GameObject image;
    float timeLeft = 4.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            image.SetActive(false);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        //Debug.Log("Mouse enter");
        image.SetActive(true);
        timeLeft = 3.0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse exit");
        image.SetActive(false);
    }
}
