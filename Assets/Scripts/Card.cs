public class Card
{
    public Card(int Value, Suites Suite)
    {
        this.number = Value;
        this.suite = Suite;
    }
    public enum Suites
    {
        Hearts = 0,
        Diamonds,
        Clubs,
        Spades
    }

    public int number
    {
        get;
        set;
    }

    public Suites suite
    {
        get;
        set;
    }

    //Used to get full name, also useful 
    //if you want to just get the named value
    public string NamedValue
    {
        get
        {
            string name = string.Empty;
            switch (number)
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
                    name = number.ToString();
                    break;
            }

            return name;
        }
    }

    public string Name
    {
        get
        {
            return NamedValue + " of  " + suite.ToString();
        }
    }
}