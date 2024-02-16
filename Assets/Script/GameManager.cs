using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject shopUI;
    public Transform head;
    public float spawnDistance = 4;

    [SerializeField] TMP_Text[] allCoinsUIText;

    public int coins;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("AssetManager instance before UpdateAllCoinsUIText: " + AssetManager.instance);
        UpdateAllCoinsUIText();
        HideShopUI();
        
    }

    //public void ToggleShopUI(bool show)
    //{
        //if (shopUI != null)
        //{
            //shopUI.SetActive(show);
            //if (show)
            //{
                //UpdateAllCoinsUIText(); // Only update the text if the UI is active
               
            //}
        //}
        //else
        //{
           // Debug.LogError("shopUI is not assigned in the inspector");
        //}
   // }
    public void HideShopUI()
    {
        shopUI.SetActive(false);
    }
    public void ToggleShopUI()
    {
       
        //bool isActive = shopUI.activeSelf;
        shopUI.SetActive(!shopUI.activeSelf);

        //if (isActive)
        //{
            //shopUI.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            //shopUI.transform.LookAt(new Vector3(head.position.x, shopUI.transform.position.y, head.position.z));
            //shopUI.transform.forward *= -1;
           
        //}
        //Debug.Log($"ShopUI visibility toggled to: {isActive}");
    }

    public void UseCoins(int amount)
    {
        coins -= amount;
    }

    public bool HasEnoughCoins(int amount)
    {
        return (coins >= amount);
    }

    public void UpdateAllCoinsUIText()
    {
        // Calculate total voting power from assets and coins
        int totalVotingPowerFromAssets = AssetManager.instance.GetTotalVotingPower();

        for (int i = 0; i < allCoinsUIText.Length; i++)
        {
            //allCoinsUIText[i].text = "Coins: " + coins.ToString();
       }
    }

}
