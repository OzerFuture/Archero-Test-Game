using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollecting : MonoBehaviour
{
    public TMP_Text coinsCountText;
    private float coinsCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coinsCount++;
        }
    }

    private void Update()
    {
        coinsCountText.text = coinsCount.ToString();
    }
}
