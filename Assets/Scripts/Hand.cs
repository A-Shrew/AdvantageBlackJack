using System.Collections.Generic;
using UnityEngine;

public class Hand
{
    private readonly List<Card> cards;
    public int handValue;

    public Hand()
    {
        handValue = 0;
        cards = new();
    }
    
    public void GetCard(Card card)
    {
        cards.Add(card);
        if(card.Number == 14)
        {
            if (handValue + 11 > 21)
            {
                handValue += 1;
            }
            else
            {
                handValue += 11;
            }
        }
        else if (card.Number > 10)
        {
            handValue += 10;
        }
        else
            handValue += card.Number;

        CheckHand();
    }

    private void CheckHand()
    {
        if (handValue > 21)
        {
          GameManager.Instance.Bust();
        }
        else if (handValue == 21)
        {
          GameManager.Instance.Blackjack();
        }
        else
        {
            Debug.Log("Hand total: " + handValue);
        }
    }
}
