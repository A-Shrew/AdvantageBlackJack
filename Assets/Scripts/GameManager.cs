using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private SpriteRenderer cardRenderer;
    [SerializeField] private TextMeshProUGUI playerScore, houseScore;
    [SerializeField] private Button hit, stand, reset;
    [SerializeField] private Transform houseLocation, playerLocation;
    [SerializeField] private int deckSize;
    [SerializeField] private float displaySpacing;

    private readonly List<Hand> houseHand = new();
    private readonly List<Hand> playerHands = new();
    private readonly List<SpriteRenderer> playerDisplays = new();
    private readonly List<SpriteRenderer> houseDisplays = new();
    private Deck deck;
    private Card dealtCard;
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
        playerScore.text = "Player Total: Bust! " + playerHands[0].handValue;
        hit.interactable = false;
        stand.interactable = false;
    }

    public void Blackjack()
    {
        playerScore.text = "Player Total: BlackJack! 21";
        hit.interactable = false;
        stand.interactable = false;
    }

    public void NewHand()
    {
        //Reset Card Displays
        ClearCards();

        //Reset Card Counts
        playerCardCount = 0;
        houseCardCount = 0;

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

            if(i == 1)
            {
                dealtCard = deck.DealCard();
                houseHand[0].GetCard(dealtCard);
                CreateHouseCard(dealtCard.BackFace);
            }
            else
            {
                dealtCard = deck.DealCard();
                houseHand[0].GetCard(dealtCard);
                CreateHouseCard(dealtCard.Face);
            }
        }

        playerScore.text = "Player Total: " + playerHands[0].handValue;
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
        CheckHand();
        ComputerPlay();
    }

    private void ComputerPlay()
    {
        houseDisplays[1].sprite = houseHand[0].cards[1].Face;

        while (houseHand[0].handValue < 17)
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
        houseScore.text = "House Total: " + houseHand[0].handValue;
        hit.interactable = false;
        stand.interactable = false;
    }
    
    private void CreatePlayerCard(Sprite sprite)
    { 
        Vector3 playerOffset = new (playerLocation.position.x + displaySpacing * playerCardCount , playerLocation.position.y, playerLocation.position.z - 0.01f * playerCardCount);
        SpriteRenderer cardDisplay = Instantiate(cardRenderer, playerOffset, Quaternion.identity);
        cardDisplay.sprite = sprite;
        playerDisplays.Add(cardDisplay);
        playerCardCount += 1;
    }

    private void CreateHouseCard(Sprite sprite)
    {
        Vector3 houseOffset = new (houseLocation.position.x + displaySpacing * houseCardCount, houseLocation.position.y, houseLocation.position.z - 0.01f * houseCardCount);
        SpriteRenderer cardDisplay = Instantiate(cardRenderer, houseOffset, Quaternion.identity);
        cardDisplay.sprite = sprite;
        houseDisplays.Add(cardDisplay);
        houseCardCount += 1;
    }

    private void ClearCards()
    {
        foreach (SpriteRenderer cardDisplay in playerDisplays)
        {
            Destroy(cardDisplay.gameObject);
        }
        playerDisplays.Clear();

        foreach (SpriteRenderer cardDisplay in houseDisplays)
        {
            Destroy(cardDisplay.gameObject);
        }
        houseDisplays.Clear();
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
            playerScore.text = "Player Total: " + playerHands[0].handValue;
        }
    }
}