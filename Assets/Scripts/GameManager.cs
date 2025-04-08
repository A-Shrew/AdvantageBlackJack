using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private SpriteRenderer cardRenderer;
    [SerializeField] private int deckSize;
    private readonly List<Hand> houseHand = new();
    private readonly List<Hand> playerHands = new();
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
        NewHand();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Bust()
    { 
        Debug.Log("Bust! " + playerHands[0].handValue);
    }

    public void Blackjack()
    {
        Debug.Log("Blackjack!");
    }

    private void NewHand()
    {
        houseHand.Clear();
        playerHands.Clear();
        playerHands.Add(new Hand());
        houseHand.Add(new Hand());

        for(int i = 0; i < 2; i++)
        {
            dealtCard = deck.DealCard();
            playerHands[0].GetCard(dealtCard);
            CreateCard(dealtCard.Face);

            dealtCard = deck.DealCard();
            houseHand[0].GetCard(dealtCard);
            CreateCard(dealtCard.Face);
        }
    }

    public void Hit()
    {
        dealtCard = deck.DealCard();
        playerHands[0].GetCard(dealtCard);
        CreateCard(dealtCard.Face);
    }
        
   private void CreateCard(Sprite sprite)
    {
        SpriteRenderer cardDisplay = Instantiate(cardRenderer, new Vector3(0, 0, -1), Quaternion.identity);
        cardDisplay.sprite = sprite;
    }
}
