using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public int suit;  
    public int number; 
    public int GetScore() { return (number > 10) ? 10 : number; }
}

public class BlackjackManager : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] private Transform dealerHandArea;
    [SerializeField] private Transform playerHandArea;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private TMPro.TextMeshProUGUI resultText;

    private List<Card> deck = new List<Card>();
    private List<Card> playerHand = new List<Card>();
    private List<Card> dealerHand = new List<Card>();

    void Start()
    {
        StartNewGame();
    }

    public void StartNewGame()
    {
        foreach (Transform child in dealerHandArea) Destroy(child.gameObject);
        foreach (Transform child in playerHandArea) Destroy(child.gameObject);

        deck.Clear();
        for (int s = 0; s < 4; s++)
            for (int n = 1; n <= 13; n++)
                deck.Add(new Card { suit = s, number = n });

        for (int i = deck.Count - 1; i > 0; i--)
        {
            int r = Random.Range(0, i + 1);
            Card temp = deck[i]; deck[i] = deck[r]; deck[r] = temp;
        }

        playerHand.Clear(); dealerHand.Clear();

        DrawCard(playerHand, playerHandArea);
        DrawCard(playerHand, playerHandArea);
        DrawCard(dealerHand, dealerHandArea);
        DrawCard(dealerHand, dealerHandArea);
    }

    public void OnHitButtonClicked()
    {
        DrawCard(playerHand, playerHandArea);
        int p = CalculateScore(playerHand);
        if (p > 21)
        {
            resultText.text = "<color=red>YOU LOSE</color>\n(BURST)" + $" \nPlayer:{p}";
        }
    }

    public void OnStandButtonClicked()
    {
        while (CalculateScore(dealerHand) < 17)
        {
            DrawCard(dealerHand, dealerHandArea);
        }
        JudgeWinner();
    }

    void DrawCard(List<Card> hand, Transform area)
    {
        if (deck.Count == 0) return;
        Card nextCard = deck[0];
        hand.Add(nextCard);
        deck.RemoveAt(0);

        GameObject newCard = Instantiate(cardPrefab, area);
        CardController controller = newCard.GetComponent<CardController>();
        controller.SetCardUI(nextCard.suit, nextCard.number);
    }

    int CalculateScore(List<Card> hand)
    {
        int score = 0; int aceCount = 0;
        foreach (Card c in hand) { score += c.GetScore(); if (c.number == 1) aceCount++; }
        while (aceCount > 0 && score <= 11) { score += 10; aceCount--; }
        return score;
    }

    void JudgeWinner()
    {
        int p = CalculateScore(playerHand);
        int d = CalculateScore(dealerHand);

        if (p > 21) resultText.text = "<color=red>YOU LOSE</color>\n(BURST)" + $" \nPlayer:{p} vs CPU:{d}";
        else if (d > 21) resultText.text = "<color=blue>YOU WIN!</color>\n(CPU BURST)" + $" \nPlayer:{p} vs CPU:{d}";
        else if (p > d) resultText.text = "<color=blue>YOU WIN!</color>" + $" \nPlayer:{p} vs CPU:{d}";
        else if (p < d) resultText.text = "<color=red>YOU LOSE</color>" + $" \nPlayer:{p} vs CPU:{d}";
        else resultText.text = "DRAW" + $" \nPlayer:{p} vs CPU:{d}";
    }
}