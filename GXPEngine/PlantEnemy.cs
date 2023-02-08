using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

public class PlantEnemy : Enemy
{
    bool flipped = true;
    public PlantEnemy(TiledObject obj = null) : base("spritesheet_plant.png", 8, 3)
    {
        this.SetScaleXY(1.5f, 1.5f);
        SetOrigin(20, 20);
        SetCycle(0, 8);
        Animate();
        healthBar = new EasyDraw(10, 5, false);
        healthBar.SetOrigin(width / 2, height / 2);
        AddChild(healthBar);
    }

    void Update()
    {
        Attack();
        UpdateUI();
        HandleDeath();
        
    }

    protected void Attack()
    {
        SetCycle(0, 8);
        Animate(0.03f);
        if(currentFrame == 4)
        {
            Shoot(2);
            currentFrame = 5;
        }
        
    }

}