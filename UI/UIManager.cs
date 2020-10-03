using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UI Manager is null!");
            }
            return _instance;
        }
    }
    public Text playerGemCountText;
    public Image selectionImg;
    public Text gemCountText;
    public Image[] healthBars = new Image[4];
    private void Awake()
    {
        _instance = this;
    }
    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = "" + gemCount + "G";
    }
    public void UpdateShopSelection(int yPos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);
    }
    public void UpdateGem(int count)
    {
        gemCountText.text = "" + count;
    }
    public void UpdateLives(int lives)
    {
        for(int i = 0;i < healthBars.Length;i++)
        {
            if(i == lives)
            {
                healthBars[i].enabled = false;
                return;
            }
        }
    }
}
