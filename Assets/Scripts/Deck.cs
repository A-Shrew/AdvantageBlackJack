using System.Collections.Generic;
using System;
using UnityEngine;
using System.Security.Cryptography;

public class Deck : MonoBehaviour
{
    // How many 52 card decks to create
    [SerializeField] private int deckSize;

    private List<Card> cardList = new();
    private Stack<Card> deckStack = new();
    private int runningCount;


    void Awake()
    {
        runningCount = 0;
        CreateStack();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DealCard();
        }
    }
   
    // Creates the card list
    public void CreateList(int size)
    {
        for (int j = 0; j < size; j++)
        {
            for (int i = 0; i < 52; i++)
            {
                Card.Suites suite = (Card.Suites)(Math.Floor((decimal)i / 13));
                int val = i % 13 + 2;
                cardList.Add(new Card(val, suite));
            }
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

    // Shuffles the card list
    public void ShuffleList<T>(List<T> list)
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
    public void StackDeck()
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
    }

    // Deals a card from the top of the stack
    public void DealCard()
    {
        if (deckStack.Count == 0)
        {
            Debug.Log("Deck Empty!");
            Debug.Log("Running Count: " + runningCount);
        }
        else
        {
            Card dealtCard = deckStack.Pop();
            HiLoCount(dealtCard);
            Debug.Log("Deal: " + dealtCard.Name);
            Debug.Log("Running Count: " + runningCount);
        }
    }

    private void HiLoCount(Card card)
    {
        if(card.number >= 2 && card.number <= 6)
        {
            runningCount++;
        }
        else if (card.number == 10 || card.number == 11 || card.number == 12 || card.number == 13 || card.number == 14)
        {
            runningCount--;
        }
        else
        {
            return;
        }
    }
}