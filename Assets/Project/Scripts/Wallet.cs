using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private CoinSpawner _spawner;

    private int _coins = 0;

    private void OnEnable()
    {
        _spawner.CoinSpawned += SubscribeToCoin;
    }

    private void OnDisable()
    {
        _spawner.CoinSpawned -= SubscribeToCoin;
    }

    private void SubscribeToCoin(Coin coin)
    {
        coin.Collected += AddCoins;
    }

    private void AddCoins(int amount)
    {
        _coins += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _coinsText.text = _coins.ToString();
    }
}