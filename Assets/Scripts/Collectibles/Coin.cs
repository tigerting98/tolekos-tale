

public class Coin : Collectible
{
    public int goldAmount;
    protected override void Start()
    {
        base.Start();
        if (goldAmount >= 1000)
        {
            transform.localScale *= 3f;
        }
        else if (goldAmount >= 500)
        {
            transform.localScale *= 2.25f;
        }
        else if (goldAmount >= 100)
        {
            transform.localScale *= 1.75f;
        }
        else if (goldAmount >= 50)
        {
            transform.localScale *= 1.25f;
        }
        else {
            transform.localScale *= 1f;
        }
    }
    protected override void Collect()
    {

        PlayerStats.AddGold(goldAmount);
    }
}
