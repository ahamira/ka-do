using UnityEngine;
using TMPro;

public class CardController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cardText;

    public void SetCardUI(int suit, int number)
    {
        string suitStr = "";
        switch (suit)
        {
            case 0: suitStr = "<color=black>♠</color>"; break;
            case 1: suitStr = "<color=red>♥</color>"; break;
            case 2: suitStr = "<color=red>♦</color>"; break;
            case 3: suitStr = "<color=black>♣</color>"; break;
        }

        string numStr = number.ToString();
        if (number == 1) numStr = "A";
        if (number == 11) numStr = "J";
        if (number == 12) numStr = "Q";
        if (number == 13) numStr = "K";

        cardText.text = suitStr + "\n" + numStr;
    }
}