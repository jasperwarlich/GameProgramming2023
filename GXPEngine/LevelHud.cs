using System.Drawing;
using GXPEngine;

public class LevelHud : Canvas
{
    Player player;
    public int currentLevel;
    Sprite wood;
    EasyDraw easyDraw;
    public int coinsCollected;

    public LevelHud(Player pPlayer) : base(1280,720, false)
    {
        player = pPlayer;
        easyDraw = new EasyDraw("button3.png");
        easyDraw.SetXY(1000, 400);
        
    }

    void Update()
    {
        
        graphics.Clear(Color.Empty);
        Font font = new Font(new FontFamily("Arial"), 20);
        
        graphics.DrawString("LEVEL: " + currentLevel, font, Brushes.White, 30, 30);
        graphics.DrawString("Coins: " + ((MyGame)game).coinsCollected + " / " + ((MyGame)game).coinsNeeded, font, Brushes.White, 30, 70);
        font = new Font(new FontFamily("Arial"), 12);
        graphics.DrawString("Collect all coins and go through the portal!", font, Brushes.White, 30, 100);
        
        if(((MyGame)game).triggerCoinFailure == true)
        {
            graphics.DrawString("You first need to collect all coins!", font, Brushes.White, 1000, 50);
        }
    }
}
