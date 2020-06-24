


public class Stage2Wave4 : WavePattern2
{

    public override void SetUp()
    {
        enemy = GameManager.gameData.patternSprite;
        bullet = GameManager.gameData.ellipseBullet.GetItem(DamageType.Pure);
    }
}
