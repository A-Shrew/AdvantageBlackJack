using System.Collections.Generic;
using System;
using UnityEngine;
using System.Security.Cryptography;

public class Deck
{ 
    private int deckSize;
    private readonly List<Card> cardList = new();
    private readonly Stack<Card> deckStack = new();
    private readonly Dictionary<int,int> remainingCards = new();
    private int runningCount;

    public Deck(int size)
    {
        this.deckSize = size;
        CreateStack();
    }
   
    // Creates the card list
    private void CreateList(int size)
    {
        for (int j = 0; j < size; j++)
        {
            for (int i = 0; i < 52; i++)
            {
                Card.Suites suite = (Card.Suites)(Math.Floor((decimal)i / 13));
                int val = i % 13 + 2;
                cardList.Add(new Card(val, suite));
                Debug.Log(val);
            }
            
        }
        for(int k = 2; k < 15; k++)
        {
            remainingCards.Add(k, 4 * size);
        }
    }

    // Prints the card list
    public void PrintList()
    {
        foreach (Card card in this.cardList)
        {
            Debug.Log(card.Name);
        }
    }

    // Prints the total number of each card remaining the deck
    public void PrintRemainingCards()
    {
        String cardTitle = string.Empty;
        for (int k = 2; k < 15; k++)
        {
            switch (k)
            {
                case (14):
                    cardTitle = "Ace";
                    break;
                case (13):
                    cardTitle = "King";
                    break;
                case (12):
                    cardTitle = "Queen";
                    break;
                case (11):
                    cardTitle = "Jack";
                    break;
                default:
                    cardTitle = k.ToString();
                    break;
            }
            Debug.Log(cardTitle +"'s remaining: " + remainingCards[k]);
        }
    }

    // Shuffles the card list
    private void ShuffleList<T>(List<T> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    // Transforms card list into a stack
    private void StackDeck()
    {
        foreach (Card card in this.cardList)
        {
            deckStack.Push(card);
        }
    }

    // Creates a shuffled stack of cards
    public void CreateStack()
    {
        CreateList(deckSize);
        ShuffleList(cardList);
        StackDeck();
        runningCount = 0;
    }

    // Deals a card from the top of the stack
    public Card DealCard()
    {
        if (deckStack.Count == 0)
        {
            Debug.Log("Deck Empty!");
            Debug.Log("Running Count: " + runningCount);
            return null;
        }
        else
        {
            Card dealtCard = deckStack.Pop();
            HiLoCount(dealtCard);
            remainingCards[dealtCard.Number]--;
            Debug.Log("Deal: " + dealtCard.Name);
            Debug.Log("Running Count: " + runningCount);
            PrintRemainingCards();
            return dealtCard;
        }
    }

    // Counts the a card using the High Low system
    private void HiLoCount(Card card)
    {
        if(card.Number >= 2 && card.Number <= 6)
        {
            runningCount++;
        }
        else if (card.Number == 10 || card.Number == 11 || card.Number == 12 || card.Number == 13 || card.Number == 14)
        {
            runningCount--;
        }
        else
        {
            return;
        }
    }
}