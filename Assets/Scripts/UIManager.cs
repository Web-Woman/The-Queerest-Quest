using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Text _coinText;
    
    public void UpdateCoinDisplay(int coins)
    {
        _coinText.text = "Coins: " + coins;
    }
    public void UpdateLives(int currentLives)
    {
      _livesImg.sprite = _liveSprites[currentLives];
    }

}
