using GXPEngine;
using GXPEngine.Core;
using System;
using System.Drawing;
using TiledMapParser;

public class Portal : AnimationSprite
{
    string targetLevel;
    public string text;
    EasyDraw sign;
    Sound portal;
    SoundChannel channel;

    public Portal(string _target) : base("spritesheet_portal.png", 8, 3)
    {
        portal = new Sound("portal.mp3", false, false);
        SetOrigin(width / 2, height / 2 + 10);
        SetScaleXY(2.5f, 2f);
        targetLevel = _target;
        SetCycle(0, 8);
        this.collider.isTrigger = true;
        text = "You first need to collect all coins!";
        sign = new EasyDraw(400, 100, false);
        sign.SetOrigin(sign.width / 2, -60);
        sign.SetScaleXY(10, 10);
        AddChild(sign);

    }

    void Update()
    {
        ((MyGame)game).triggerCoinFailure = false;
        sign.ClearTransparent();
        Animate(0.05f);
    }

    void OnCollision(GameObject other)
    {
        if(((MyGame)game).coinsCollected == ((MyGame)game).coinsNeeded)
        {
            if (other is Player)
            {
                channel = portal.Play();
                portal = new Sound("");
                SetCycle(17, 22);
                Animate(0.02f);
                if (currentFrame == 22)
                {
                    ((MyGame)game).coinsCollected = 0;
                    ((MyGame)game).LoadLevel(targetLevel);
                }

            }
        } else
        {
            if(other is Player)
            {
                Font font = new Font(new FontFamily("Arial"), 15);
                ((MyGame)game).triggerCoinFailure = true;
            }
            
        }
        
    }
}