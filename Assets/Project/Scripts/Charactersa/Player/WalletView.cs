using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()  => _wallet.Changed += UpdateUI;
    private void OnDisable() => _wallet.Changed -= UpdateUI;

    private void UpdateUI(int coins) => _coinsText.text = coins.ToString();
}
