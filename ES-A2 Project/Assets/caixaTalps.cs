using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caixaTalps : MonoBehaviour {

    [SerializeField] GameObject talp1_g;
    [SerializeField] GameObject talp1_b;
    [SerializeField] GameObject talp1_dead;
    [SerializeField] GameObject talp1_num;

    [SerializeField] GameObject talp2_g;
    [SerializeField] GameObject talp2_b;
    [SerializeField] GameObject talp2_dead;
    [SerializeField] GameObject talp2_num;


    [SerializeField] GameObject talp3_g;
    [SerializeField] GameObject talp3_b;
    [SerializeField] GameObject talp3_dead;
    [SerializeField] GameObject talp3_num;


    [SerializeField] GameObject talp4_g;
    [SerializeField] GameObject talp4_b;
    [SerializeField] GameObject talp4_dead;
    [SerializeField] GameObject talp4_num;



    [SerializeField] GameObject talp5_g;
    [SerializeField] GameObject talp5_b;
    [SerializeField] GameObject talp5_dead;
    [SerializeField] GameObject talp5_num;


    [SerializeField] GameObject talp6_g;
    [SerializeField] GameObject talp6_b;
    [SerializeField] GameObject talp6_dead;
    [SerializeField] GameObject talp6_num;


    [SerializeField] GameObject talp7_g;
    [SerializeField] GameObject talp7_b;
    [SerializeField] GameObject talp7_dead;
    [SerializeField] GameObject talp7_num;


    [SerializeField] GameObject talp8_g;
    [SerializeField] GameObject talp8_b;
    [SerializeField] GameObject talp8_dead;
    [SerializeField] GameObject talp8_num;


    [SerializeField] GameObject talp9_g;
    [SerializeField] GameObject talp9_b;
    [SerializeField] GameObject talp9_dead;
    [SerializeField] GameObject talp9_num;


    [SerializeField] GameObject talp10_g;
    [SerializeField] GameObject talp10_b;
    [SerializeField] GameObject talp10_dead;
    [SerializeField] GameObject talp10_num;


    [SerializeField] GameObject talp11_g;
    [SerializeField] GameObject talp11_b;
    [SerializeField] GameObject talp11_dead;
    [SerializeField] GameObject talp11_num;


    [SerializeField] GameObject talp12_g;
    [SerializeField] GameObject talp12_b;
    [SerializeField] GameObject talp12_dead;
    [SerializeField] GameObject talp12_num;

    [SerializeField] GameObject game;
    List<Character> infoList;

    List<GameObject> talp_g = new List<GameObject>();
    List<GameObject> talp_b = new List<GameObject>();
    List<GameObject> talp_dead = new List<GameObject>();
    List<GameObject> talp_num = new List<GameObject>();







    // Use this for initialization
    void Start () {
        talp_g.Add(talp1_g);
        talp_g.Add(talp2_g);
        talp_g.Add(talp3_g);
        talp_g.Add(talp4_g);
        talp_g.Add(talp5_g);
        talp_g.Add(talp6_g);
        talp_g.Add(talp7_g);
        talp_g.Add(talp8_g);
        talp_g.Add(talp9_g);
        talp_g.Add(talp10_g);
        talp_g.Add(talp11_g);
        talp_g.Add(talp12_g);

        talp_b.Add(talp1_b);
        talp_b.Add(talp2_b);
        talp_b.Add(talp3_b);
        talp_b.Add(talp4_b);
        talp_b.Add(talp5_b);
        talp_b.Add(talp6_b);
        talp_b.Add(talp7_b);
        talp_b.Add(talp8_b);
        talp_b.Add(talp9_b);
        talp_b.Add(talp10_b);
        talp_b.Add(talp11_b);
        talp_b.Add(talp12_b);

        talp_dead.Add(talp1_dead);
        talp_dead.Add(talp2_dead);
        talp_dead.Add(talp3_dead);
        talp_dead.Add(talp4_dead);
        talp_dead.Add(talp5_dead);
        talp_dead.Add(talp6_dead);
        talp_dead.Add(talp7_dead);
        talp_dead.Add(talp8_dead);
        talp_dead.Add(talp9_dead);
        talp_dead.Add(talp10_dead);
        talp_dead.Add(talp11_dead);
        talp_dead.Add(talp12_dead);

        talp_num.Add(talp1_num);
        talp_num.Add(talp2_num);
        talp_num.Add(talp3_num);
        talp_num.Add(talp4_num);
        talp_num.Add(talp5_num);
        talp_num.Add(talp6_num);
        talp_num.Add(talp7_num);
        talp_num.Add(talp8_num);
        talp_num.Add(talp9_num);
        talp_num.Add(talp10_num);
        talp_num.Add(talp11_num);
        talp_num.Add(talp12_num);
        deactivateAll();

    }
	
	// Update is called once per frame
	void Update () {
        infoList = game.gameObject.GetComponent<Game>().round.getCharactersToPrint();
        deactivateAll();
        int count = 0;
        foreach (Character c in infoList)
        {
            talp_num[count].SetActive(true);
            
            if (c.getPlayer().name == "Player 1")
            {
                talp_g[count].SetActive(true);
            } else
            {
                talp_b[count].SetActive(true);
            }
            if (!c.isAlive())
            {
                talp_dead[count].SetActive(true);
            }

            count += 1;
        }

    }

    void deactivateAll()
    {
        foreach (GameObject g in talp_g)
        {
            g.SetActive(false);
        }
        foreach (GameObject b in talp_b)
        {
            b.SetActive(false);
        }
        foreach (GameObject d in talp_dead)
        {
            d.SetActive(false);
        }
        foreach (GameObject n in talp_num)
        {
            n.SetActive(false);
        }
    }
}
