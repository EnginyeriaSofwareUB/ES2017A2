using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public Sprite background;
    public Sprite hortaliza;
}

public class CreateScrollList : MonoBehaviour
{

    public GameObject sampleImage;
    public Transform contentParent;
    public List<Item> projectiles;

    private void Start()
    {
        init_projectiles();
    }

    public void init_projectiles()
    {

        foreach (var projectile in projectiles)
        {
            GameObject newImage = Instantiate(sampleImage) as GameObject;
            ProjectileScript projectileScript = newImage.GetComponent<ProjectileScript>();
            projectileScript.background.sprite = projectile.background;
            projectileScript.projectileImage.sprite = projectile.hortaliza;

            newImage.transform.SetParent(contentParent);
        }
    }

}
