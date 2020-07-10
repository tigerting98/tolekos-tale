

public class Coin : Collectible
{
    public int goldAmount;
    protected override void Collect()
    {

        PlayerStats.AddGold(goldAmount);
    }
}
