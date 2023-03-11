using UnityEngine;

public class CoinsContainer : MonoBehaviour
{
    [SerializeField] private Sandwich sandwich;
    [SerializeField] private int coins = 0;

    private void Start()
    {
        coins = PlayerPrefs.GetInt(StringConstants.CoinsPlayerPrefs);
    }

    public int GetCoins()
    {
        return coins;
    }

    public void IncreaseCoinsValue()
    {
        coins++;
        PlayerPrefs.SetInt(StringConstants.CoinsPlayerPrefs, coins);
    }
}