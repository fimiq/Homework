using TMPro;
using UnityEngine;

[RequireComponent(typeof(Wallet))]
public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    private Wallet _wallet;
    private void Start()
    {
        _wallet = GetComponent<Wallet>();
    }

    private void OnEnable()
    {
        _wallet.OnChanged += UpdateUI; 
    }

    private void OnDisable()
    {
        _wallet.OnChanged -= UpdateUI;
    }

    public void UpdateUI(int coins)
    {
        _coinsText.text = coins.ToString();
    }
}
