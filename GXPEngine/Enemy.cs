using GXPEngine;
using GXPEngine.Core;
using System;

public class Enemy : AnimationSprite
{
    public GameObject target;
    bool flipped;
    public EasyDraw healthBar;
    public int hp = 10;
    Sound shot;
    SoundChannel channel;

    protected enum State
    {
        IDLE,
        ATTACK
    }

    protected State _state;

    public Enemy(string pAnimationPath, int pCol, int pRow) : base(pAnimationPath, pCol, pRow)
    {
        shot = new Sound("enemyshot.mp3", false, false);
    }

    protected void UpdateUI()
    {
        healthBar.Clear(255, 0, 0);
        healthBar.Fill(0, 255, 0);
        healthBar.Rect(0, 0, hp * 2, 24);
    }

    protected void Shoot(int pBulletSpeed)
    {
        channel = shot.Play();
        channel.Volume = 0.5f;
        parent.AddChild(new Bullet(this, true, 30,30, pBulletSpeed));
    }

    protected void OnCollision(GameObject other)
    {
        if(other is Bullet bullet && bullet.Parent is Player p)
        {
            bullet.didCollide = true;
            hp -= 2;
            bullet.LateDestroy();
            Console.WriteLine("Hit");
        }
    }

    protected void HandleDeath()
    {
        if(hp <= 0)
        {
            Destroy();
        }
    }
}
