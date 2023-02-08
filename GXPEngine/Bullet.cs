using GXPEngine;
using GXPEngine.Core;

public class Bullet : Sprite
{

    AnimationSprite bulletExplode;

    private float _velocityX, _velocityY;
    int _damage;
    private GameObject _Parent;
    private bool _flipped;
    private int _initX, _initY;
    private int _bulletSpeed;
    public bool didCollide = false;

    public GameObject Parent { get => _Parent; private set => _Parent = value; }

    public Bullet(GameObject pParent, bool pFlipped, int pInitX, int pInitY, int pBulletSpeed) : base("bullet.png", false, true)
    {
        _bulletSpeed = pBulletSpeed;
        _Parent = pParent;
        _flipped = pFlipped;
        _initX = pInitX;
        _initY = pInitY;
        if (_flipped)
        {
            SetXY(_Parent.x - 100 , _Parent.y - 10);
        }
        else if (!_flipped)
        {
            SetXY(_Parent.x + pInitX,_Parent.y + pInitY);
        }
        SetScaleXY(0.1f, 0.3f);
        bulletExplode = new AnimationSprite("bullet_explode.png", 7, 1, -1, false, false);
        bulletExplode.SetOrigin(width / 2, height / 2);
    }

    void Update()
    {
        
        Move();
        if(didCollide)
        {
            
            LateAddChild(bulletExplode);
            bulletExplode.SetCycle(0, 7);
            bulletExplode.Animate(0.7f);
        }
       
    }

    void Move()
    {
        
        if(_flipped)
        {
            x -= _bulletSpeed;
        } else if (!_flipped)
        {
            x += _bulletSpeed;
        }
    }
}