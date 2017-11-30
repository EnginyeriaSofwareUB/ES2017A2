using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {
    [SerializeField] private GameObject inventoryPanel;
    private Dictionary<GameObject, Toggle> projectileDictionary;
    private List<Toggle> inventoryItems = new List<Toggle>();

    // Use this for initialization
    void Awake () {
        this.projectileDictionary = new Dictionary<GameObject, Toggle>();
        this.inventoryItems.AddRange(this.inventoryPanel.GetComponentsInChildren<Toggle>());
        foreach (Toggle item in inventoryItems) {
            MenuItem menuItem = item.GetComponent<MenuItem>();
            if (menuItem.ProjectilePrefab != null) {
                this.projectileDictionary.Add(menuItem.ProjectilePrefab, item);
            }
        }
        this.selectDefaultProjectile();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void initInventory(List<ProjectileInfo> projectilesInfo) {
        foreach(ProjectileInfo projectileInfo in projectilesInfo) {
            Toggle projectileButton = this.projectileDictionary[projectileInfo.projectile];
            MenuItem menuItem = projectileButton.GetComponent<MenuItem>();
            menuItem.Ammo = projectileInfo.ammo;
        }
    }

    public void closeInventory() {
        this.inventoryPanel.SetActive(false);
    }

    public void openInventory() {
        this.inventoryPanel.SetActive(true);
    }

    public void selectDefaultProjectile() {
        this.inventoryItems[0].isOn = true;
    }
}
