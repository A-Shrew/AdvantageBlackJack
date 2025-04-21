using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private SpriteRenderer cardRenderer;
    [SerializeField] private Button hit;
    [SerializeField] private Button stand;
    [SerializeField] private Button reset;
    [SerializeField] private Transform houseDisplay;
    [SerializeField] private Transform playerDisplay;
    [SerializeField] private int deckSize;
    [SerializeField] private float displaySpacing;

    private Deck deck;
    private Card dealtCard;
    private readonly List<Hand> houseHand = new();
    private readonly List<Hand> playerHands = new();
    private int playerCardCount;
    private int houseCardCount;

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
        NewHand();
    }

    public void Bust()
    { 
        Debug.Log("Bust! " + playerHands[0].handValue);
        hit.interactable = false;
        stand.interactable = false;
        Stand();
    }

    public void Blackjack()
    {
        Debug.Log("Blackjack!");
        hit.interactable = false;
        stand.interactable = false;
        Stand();
    }

    public void NewHand()
    {
        //Reset Buttons
        hit.interactable = true;
        stand.interactable = true;

        //Reset Hands
        houseHand.Clear();
        playerHands.Clear();
        playerHands.Add(new Hand());
        houseHand.Add(new Hand());

        //Deal two cards to each player
        for(int i = 0; i < 2; i++)
        {
            dealtCard = deck.DealCard();
            playerHands[0].GetCard(dealtCard);
            CreatePlayerCard(dealtCard.Face);

            dealtCard = deck.DealCard();
            houseHand[0].GetCard(dealtCard);
            CreateHouseCard(dealtCard.Face);
        }
    }

    public void Hit()
    {
        dealtCard = deck.DealCard();
        playerHands[0].GetCard(dealtCard);
        CreatePlayerCard(dealtCard.Face);
        CheckHand();
    }

    public void Stand()
    {
        ComputerPlay();
        CheckHand();
    }

    private void ComputerPlay()
    {
        while(houseHand[0].handValue < 17)
        {
            dealtCard = deck.DealCard();
            houseHand[0].GetCard(dealtCard);
            CreateHouseCard(dealtCard.Face);
        }
        if(houseHand[0].handValue > playerHands[0].handValue && houseHand[0].handValue <= 21)
        {
            Debug.Log("House Wins!");
        }
        else
        {
            Debug.Log("Player Wins!");
        }
        NewHand();
    }
    
    private void CreatePlayerCard(Sprite sprite)
    { 
        Vector3 playerOffset = new (playerDisplay.position.x + displaySpacing * playerCardCount , playerDisplay.position.y, playerDisplay.position.z);
        SpriteRenderer cardDisplay = Instantiate(cardRenderer, playerOffset, Quaternion.identity);
        cardDisplay.sprite = sprite;
        playerCardCount += 1;
    }

    private void CreateHouseCard(Sprite sprite)
    {
        Vector3 houseOffset = new (houseDisplay.position.x + displaySpacing * houseCardCount, houseDisplay.position.y, houseDisplay.position.z);
        SpriteRenderer cardDisplay = Instantiate(cardRenderer, houseOffset, Quaternion.identity);
        cardDisplay.sprite = sprite;
        houseCardCount += 1;
    }

    private void CheckHand()
    {
        if (playerHands[0].handValue > 21)
        {
            Bust();
        }
        else if (playerHands[0].handValue == 21)
        {
            Blackjack();
        }
        else
        {
            //Debug.Log("Player Hand: " + playerHands[0].handValue);
        }
    }
}