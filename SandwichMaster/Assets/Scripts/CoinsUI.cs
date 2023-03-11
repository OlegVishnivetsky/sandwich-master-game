using System.Collections;
using System.Drawing;
using TMPro;
using UnityEngine;

public class CoinsUI : MonoBehaviour
{
    [Header("MAIN COMPONENTS")]
    [SerializeField] private Sandwich sandwich;
    [SerializeField] private CoinsContainer coinsContainer;
    [SerializeField] private TextMeshProUGUI coinsText;

    [SerializeField] private GameObject coinPrefab;

    [Header("TRANSFORM COMPONENTS")]
    [SerializeField] private Transform coinsParentTransform;

    [SerializeField] private Transform coinSpawnPosition;
    [SerializeField] private Transform coinMoveTargetTransfrom;

    [Header("TWEEN SETTINGS")]
    [SerializeField, Range(0f, 1f)] private float spawnCoinDelay;

    [SerializeField] private float movingDuration;
    [SerializeField] private LeanTweenType ease;

    private void OnEnable()
    {
        sandwich.OnCustomerCoinsCalculated += SpawnCoins;
    }

    private void OnDisable()
    {
        sandwich.OnCustomerCoinsCalculated -= SpawnCoins;
    }

    private void Start()
    {
        LoadAndUpdateAmountOfCoins();
    }

    public void SpawnCoins(int amount)
    {
        StartCoroutine(SpawnCoinsRoutine(amount));
    }

    private IEnumerator SpawnCoinsRoutine(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(spawnCoinDelay);

            GameObject spawnedCoin = Instantiate(coinPrefab, coinSpawnPosition.position,
                Quaternion.identity, coinsParentTransform);

            LeanTween.move(spawnedCoin, coinMoveTargetTransfrom, movingDuration).setEase(ease)
                .setOnComplete(() =>
                {
                    Destroy(spawnedCoin);
                    coinsContainer.IncreaseCoinsValue();
                    LoadAndUpdateAmountOfCoins();
                });
        }
    }

    private void LoadAndUpdateAmountOfCoins()
    {
        coinsText.text = PlayerPrefs.GetInt(StringConstants.CoinsPlayerPrefs).ToString();
    }
}