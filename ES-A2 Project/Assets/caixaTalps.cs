using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caixaTalps : MonoBehaviour {

    [SerializeField] GameObject talp1_g1;
    [SerializeField] GameObject talp1_g2;
    [SerializeField] GameObject talp1_g3;
    [SerializeField] GameObject talp1_b1;
    [SerializeField] GameObject talp1_b2;
    [SerializeField] GameObject talp1_b3;
    [SerializeField] GameObject talp1_dead;
    [SerializeField] GameObject talp1_num;

    [SerializeField] GameObject talp2_g1;
    [SerializeField] GameObject talp2_g2;
    [SerializeField] GameObject talp2_g3;
    [SerializeField] GameObject talp2_b1;
    [SerializeField] GameObject talp2_b2;
    [SerializeField] GameObject talp2_b3;
    [SerializeField] GameObject talp2_dead;
    [SerializeField] GameObject talp2_num;


    [SerializeField] GameObject talp3_g1;
    [SerializeField] GameObject talp3_g2;
    [SerializeField] GameObject talp3_g3;
    [SerializeField] GameObject talp3_b1;
    [SerializeField] GameObject talp3_b2;
    [SerializeField] GameObject talp3_b3;
    [SerializeField] GameObject talp3_dead;
    [SerializeField] GameObject talp3_num;


    [SerializeField] GameObject talp4_g1;
    [SerializeField] GameObject talp4_g2;
    [SerializeField] GameObject talp4_g3;
    [SerializeField] GameObject talp4_b1;
    [SerializeField] GameObject talp4_b2;
    [SerializeField] GameObject talp4_b3;
    [SerializeField] GameObject talp4_dead;
    [SerializeField] GameObject talp4_num;



    [SerializeField] GameObject talp5_g1;
    [SerializeField] GameObject talp5_g2;
    [SerializeField] GameObject talp5_g3;
    [SerializeField] GameObject talp5_b1;
    [SerializeField] GameObject talp5_b2;
    [SerializeField] GameObject talp5_b3;
    [SerializeField] GameObject talp5_dead;
    [SerializeField] GameObject talp5_num;


    [SerializeField] GameObject talp6_g1;
    [SerializeField] GameObject talp6_g2;
    [SerializeField] GameObject talp6_g3;
    [SerializeField] GameObject talp6_b1;
    [SerializeField] GameObject talp6_b2;
    [SerializeField] GameObject talp6_b3;
    [SerializeField] GameObject talp6_dead;
    [SerializeField] GameObject talp6_num;


    [SerializeField] GameObject talp7_g1;
    [SerializeField] GameObject talp7_g2;
    [SerializeField] GameObject talp7_g3;
    [SerializeField] GameObject talp7_b1;
    [SerializeField] GameObject talp7_b2;
    [SerializeField] GameObject talp7_b3;
    [SerializeField] GameObject talp7_dead;
    [SerializeField] GameObject talp7_num;


    [SerializeField] GameObject talp8_g1;
    [SerializeField] GameObject talp8_g2;
    [SerializeField] GameObject talp8_g3;
    [SerializeField] GameObject talp8_b1;
    [SerializeField] GameObject talp8_b2;
    [SerializeField] GameObject talp8_b3;
    [SerializeField] GameObject talp8_dead;
    [SerializeField] GameObject talp8_num;


    [SerializeField] GameObject talp9_g1;
    [SerializeField] GameObject talp9_g2;
    [SerializeField] GameObject talp9_g3;
    [SerializeField] GameObject talp9_b1;
    [SerializeField] GameObject talp9_b2;
    [SerializeField] GameObject talp9_b3;
    [SerializeField] GameObject talp9_dead;
    [SerializeField] GameObject talp9_num;


    [SerializeField] GameObject talp10_g1;
    [SerializeField] GameObject talp10_g2;
    [SerializeField] GameObject talp10_g3;
    [SerializeField] GameObject talp10_b1;
    [SerializeField] GameObject talp10_b2;
    [SerializeField] GameObject talp10_b3;
    [SerializeField] GameObject talp10_dead;
    [SerializeField] GameObject talp10_num;


    [SerializeField] GameObject talp11_g1;
    [SerializeField] GameObject talp11_g2;
    [SerializeField] GameObject talp11_g3;
    [SerializeField] GameObject talp11_b1;
    [SerializeField] GameObject talp11_b2;
    [SerializeField] GameObject talp11_b3;
    [SerializeField] GameObject talp11_dead;
    [SerializeField] GameObject talp11_num;


    [SerializeField] GameObject talp12_g1;
    [SerializeField] GameObject talp12_g2;
    [SerializeField] GameObject talp12_g3;
    [SerializeField] GameObject talp12_b1;
    [SerializeField] GameObject talp12_b2;
    [SerializeField] GameObject talp12_b3;
    [SerializeField] GameObject talp12_dead;
    [SerializeField] GameObject talp12_num;

    [SerializeField] GameObject game;
    List<Character> infoList;

    List<GameObject> talp_g1 = new List<GameObject>();
    List<GameObject> talp_g2 = new List<GameObject>();
    List<GameObject> talp_g3 = new List<GameObject>();
    List<GameObject> talp_b1 = new List<GameObject>();
    List<GameObject> talp_b2 = new List<GameObject>();
    List<GameObject> talp_b3 = new List<GameObject>();
    List<GameObject> talp_dead = new List<GameObject>();
    List<GameObject> talp_num = new List<GameObject>();







    // Use this for initialization
    void Start () {
        talp_g1.Add(talp1_g1);
        talp_g1.Add(talp2_g1);
        talp_g1.Add(talp3_g1);
        talp_g1.Add(talp4_g1);
        talp_g1.Add(talp5_g1);
        talp_g1.Add(talp6_g1);
        talp_g1.Add(talp7_g1);
        talp_g1.Add(talp8_g1);
        talp_g1.Add(talp9_g1);
        talp_g1.Add(talp10_g1);
        talp_g1.Add(talp11_g1);
        talp_g1.Add(talp12_g1);

        talp_g2.Add(talp1_g2);
        talp_g2.Add(talp2_g2);
        talp_g2.Add(talp3_g2);
        talp_g2.Add(talp4_g2);
        talp_g2.Add(talp5_g2);
        talp_g2.Add(talp6_g2);
        talp_g2.Add(talp7_g2);
        talp_g2.Add(talp8_g2);
        talp_g2.Add(talp9_g2);
        talp_g2.Add(talp10_g2);
        talp_g2.Add(talp11_g2);
        talp_g2.Add(talp12_g2);

        talp_g3.Add(talp1_g3);
        talp_g3.Add(talp2_g3);
        talp_g3.Add(talp3_g3);
        talp_g3.Add(talp4_g3);
        talp_g3.Add(talp5_g3);
        talp_g3.Add(talp6_g3);
        talp_g3.Add(talp7_g3);
        talp_g3.Add(talp8_g3);
        talp_g3.Add(talp9_g3);
        talp_g3.Add(talp10_g3);
        talp_g3.Add(talp11_g3);
        talp_g3.Add(talp12_g3);

        talp_b1.Add(talp1_b1);
        talp_b1.Add(talp2_b1);
        talp_b1.Add(talp3_b1);
        talp_b1.Add(talp4_b1);
        talp_b1.Add(talp5_b1);
        talp_b1.Add(talp6_b1);
        talp_b1.Add(talp7_b1);
        talp_b1.Add(talp8_b1);
        talp_b1.Add(talp9_b1);
        talp_b1.Add(talp10_b1);
        talp_b1.Add(talp11_b1);
        talp_b1.Add(talp12_b1);

        talp_b2.Add(talp1_b2);
        talp_b2.Add(talp2_b2);
        talp_b2.Add(talp3_b2);
        talp_b2.Add(talp4_b2);
        talp_b2.Add(talp5_b2);
        talp_b2.Add(talp6_b2);
        talp_b2.Add(talp7_b2);
        talp_b2.Add(talp8_b2);
        talp_b2.Add(talp9_b2);
        talp_b2.Add(talp10_b2);
        talp_b2.Add(talp11_b2);
        talp_b2.Add(talp12_b2);

        talp_b3.Add(talp1_b3);
        talp_b3.Add(talp2_b3);
        talp_b3.Add(talp3_b3);
        talp_b3.Add(talp4_b3);
        talp_b3.Add(talp5_b3);
        talp_b3.Add(talp6_b3);
        talp_b3.Add(talp7_b3);
        talp_b3.Add(talp8_b3);
        talp_b3.Add(talp9_b3);
        talp_b3.Add(talp10_b3);
        talp_b3.Add(talp11_b3);
        talp_b3.Add(talp12_b3);

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
                if (c.getColor() == "BLACK")
                {
                    talp_g1[count].SetActive(true);
                }
                else if(c.getColor() == "BROWN")
                {
                    talp_g2[count].SetActive(true);
                }
                else if (c.getColor() == "GREY")
                {
                    talp_g3[count].SetActive(true);
                } else
                {
                    talp_g1[count].SetActive(true);
                }
                
            } else
            {
                if (c.getColor() == "BLACK")
                {
                    talp_b1[count].SetActive(true);
                }
                else if (c.getColor() == "BROWN")
                {
                    talp_b2[count].SetActive(true);
                }
                else if (c.getColor() == "GREY")
                {
                    talp_b3[count].SetActive(true);
                }
                else
                {
                    talp_b1[count].SetActive(true);
                }
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
        foreach (GameObject g in talp_g1)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in talp_g2)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in talp_g3)
        {
            g.SetActive(false);
        }
        foreach (GameObject b in talp_b1)
        {
            b.SetActive(false);
        }
        foreach (GameObject b in talp_b2)
        {
            b.SetActive(false);
        }
        foreach (GameObject b in talp_b3)
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
