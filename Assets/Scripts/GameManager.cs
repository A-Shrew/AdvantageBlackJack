using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private SpriteRenderer cardDisplay;
    [SerializeField] private int deckSize;
    private List<Hand> playerHands = new();
    private Deck deck;
    private Card dealtCard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        deck = new Deck(deckSize);
        playerHands.Add(new Hand());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dealtCard = deck.DealCard();
            playerHands[0].GetCard(dealtCard);
            cardDisplay.sprite = dealtCard.Face;
        }
    }

    public void Bust()
    { 
        Debug.Log("Bust! " + playerHands[0].handValue);
    }

    public void Blackjack()
    {
        Debug.Log("Blackjack!");
    }

    private void Reset()
    {
        deck = new Deck(deckSize);
        playerHands.Clear();
    }
}
