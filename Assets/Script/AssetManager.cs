using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AssetManager : MonoBehaviour
{
    #region Singleton: AssetManager
    public static AssetManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Another instance of AssetManager exists and will be destroyed!");
            Destroy(gameObject);
            return;
        }

        // Check if the current scene is Scene 2 or later
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex >= 2)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If this instance is in Scene 0 or Scene 1, we don't make it persistent
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        // Log or take action when the AssetManager is being destroyed
        Debug.Log($"AssetManager instance in scene: {SceneManager.GetActiveScene().name} is being destroyed.");
    }
    #endregion

    public int coins;
    public int landCount;
    public int rentedLandCount;
    public int collectionCount;
    public int totalVotingPower;

    public TMP_Text coinsText;
    public TMP_Text landText;
    public TMP_Text rentedLandText;
    public TMP_Text collectionText;
    public TMP_Text totalVotingPowerText;

    
    
    void Start()
    {
        coins = 6; // Initial number of coins
        landCount = 1; // Initial number of land
        rentedLandCount = 0; // Initial number of rented land
        collectionCount = 1; // Initial number of collection

        UpdateAssetUI();
    }

    public int GetTotalVotingPower()
    {
        // Calculate total voting power based on assets and coins
        int spentCoinsVP = coins * 1; // Contribution of spent coins to total voting power
        int landVP = landCount * 2000;
        int collectionVP = collectionCount * 100;

        int totalVotingPower = spentCoinsVP + landVP + collectionVP;
        return totalVotingPower;
    }

    void UpdateAssetUI()
    {
        coinsText.text = "Coins: " + coins;
        landText.text = "Land: " + landCount;
        rentedLandText.text = "Rented Land: " + rentedLandCount;
        collectionText.text = "Collection: " + collectionCount;

        int totalVotingPower = GetTotalVotingPower();
        totalVotingPowerText.text = "Total Voting Power: " + totalVotingPower.ToString();
    }

    public void PurchaseLand(int price)
    {
        coins -= price; //Deduct the price when purchasing land
        landCount++;
        UpdateAssetUI();
    }

    public void PurchaseRentedLand(int price)
    {
        coins -= price;
        UpdateAssetUI();
    }

    public void PurchaseCollection(int price)
    {
        coins -= price;
        collectionCount++;
        UpdateAssetUI();
    }

    //Spending coins
    public void SpendCoins(int amount)
    {
        coins -= amount; // Deduct spent coins from the total number of coins
        int spentCoinsVP = amount * 1; // Calculate the contribution of spent coins to total voting power
        totalVotingPower -= spentCoinsVP; // Deduct the contribution to total voting power based on spent coins
        UpdateAssetUI();
    }
}
