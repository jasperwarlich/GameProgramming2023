using GXPEngine;
using GXPEngine.Core;
using GXPEngine.Managers;
using TiledMapParser;

public class EatEnemy : Enemy
{
    bool flipped = true;
    int _startX;
    int _EndX;
    int _xSpeed = 1;
    public EatEnemy(int pStartX, int pEndX, TiledObject obj = null) : base("saw.png", 1, 1)
    {
        _startX = pStartX;
        _EndX = pEndX;
        SetScaleXY(0.25f, 0.25f);
        SetOrigin(0, height + 20);
        SetCycle(5, 1);
        Animate(0.03f);
        healthBar = new EasyDraw(100, 50, false);
        healthBar.SetOrigin(width / 2, height / 2 + 100);
        AddChild(healthBar);
    }

    void Update()
    {
        Movement();
        UpdateUI();
        HandleDeath();
    }

    void Movement()
    {
        x += _xSpeed;

        // Move till startX, then turn around
        if (x <= _startX) {
            _xSpeed = 1;
        }

        // Move till endX, then turn around
        if (x >= _EndX)
        {
            _xSpeed = -1;
        }


        
    }

    void UpdateUI()
    {
        healthBar.Clear(255, 0, 0);
        healthBar.Fill(0, 255, 0);
        healthBar.Rect(0, 0, hp * 20, 240);
    }

}