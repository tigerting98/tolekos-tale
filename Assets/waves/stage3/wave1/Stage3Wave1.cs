public class Stage3Wave1 : WavePattern3 {
    
    
    
    
    public override void SetUp()
    {

        enemy = GameManager.gameData.ghosts.GetItem(DamageType.Earth);
        bulletnormal = GameManager.gameData.ellipseBullet.GetItem(DamageType.Earth);
        bulletexplode = GameManager.gameData.leafBullet3;
    }
}
