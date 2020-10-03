using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentItemSelected;
    public int currentItemCost;
    private Player player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            if (player != null)
            {
                UIManager.Instance.OpenShop(player.diamonds);
            }
            shopPanel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }
    public void SelectItem(int item)
    {
        Debug.Log("Select Item : " + item);
        switch (item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(101);
                currentItemSelected = 0;
                currentItemCost = 200;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(4);
                currentItemSelected = 1;
                currentItemCost = 400;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-91);
                currentItemSelected = 2;
                currentItemCost = 100;
                break;
        }
    }
    public void BuyItem()
    {
        if (player.diamonds >= currentItemCost)
        {
            if (currentItemSelected == 2)
                GameManager.Instance.HasKeyToCastle = true;
            Debug.Log("Award : " + currentItemSelected);
            player.diamonds -= currentItemCost;
            Debug.Log("Remaining gems : " + player.diamonds);
            shopPanel.SetActive(false);
        }
        else
        {
            shopPanel.SetActive(false);
        }
    }
}
