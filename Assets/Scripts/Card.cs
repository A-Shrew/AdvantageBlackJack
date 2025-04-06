using UnityEngine;

public class Card
{
    public Card(int Value, Suites Suite)
    {
        this.Number = Value;
        this.Suite = Suite;
    }

    public enum Suites
    {
        Hearts = 0,
        Diamonds,
        Clubs,
        Spades
    }

    public int Number
    {
        get;
        set;
    }

    public Suites Suite
    {
        get;
        set;
    }

    public string NamedValue
    {
        get
        {
            string name = string.Empty;
            switch (Number)
            {
                case (14):
                    name = "Ace";
                    break;
                case (13):
                    name = "King";
                    break;
                case (12):
                    name = "Queen";
                    break;
                case (11):
                    name = "Jack";
                    break;
                default:
                    name = Number.ToString();
                    break;
            }

            return name;
        }
    }

    public string Name
    {
        get
        {
            return NamedValue + " of  " + Suite.ToString();
        }
    }

    public Sprite Face
    {
        get
        {
            string path = Suite.ToString() + " " + (Number == 14 ? 1 : Number).ToString();
            Sprite texture = Resources.Load<Sprite>(path);
            return texture;
        }
    }
}