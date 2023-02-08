using System;
using System.Security;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
public class Player : AnimationSprite
 {
    private State _currentState;

    private int _xSpeed = 2;
    private float _ySpeed = 0;
    private float _jumpSpeed = -3.5f;

    bool flipped;
    Sound shot;
    Sound jump;
    Sound coinPickUp;
    Sound death;
    SoundChannel channel;

    public int starsCollected = 0;

    public enum State
    {
         IDLE,
         MOVING,
         JUMPING
    }

    public Player(TiledObject obj = null) : base("spritesheet.png", 13, 1, -1, false, true)
    {
        shot = new Sound("shot.mp3", false, false);
        jump = new Sound("jump.mp3", false, false);
        coinPickUp = new Sound("coinpickup.mp3", false, false);
        death = new Sound("deathsound.mp3", false, false);
        SetOrigin(width / 2, height / 2);
        this.SetScaleXY(1.5f, 1.5f);
    }

    void Update()
    {
        Shoot();
        Movement();
        HandleAnimations();
        Jump();
        CheckBoundaries();
    }

    private void checkJumping()
    {
        if(_currentState == State.JUMPING)
        {

        }
    }

    void HandleAnimations()
    {
        if(_currentState == State.MOVING)
        {
                SetCycle(1, 6);
                Animate(0.07f);
        }

        if(_currentState == State.IDLE)
        {
            SetCycle(9, 5);
            Animate(0.05f);
        }
    }

    void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            channel = shot.Play();
            parent.AddChild(new Bullet(this, flipped, 30, -10, 10));
        }
    }

    void CheckInput()
    {
        
    }

    private void Flip()
    {
        flipped = !flipped;
        scaleX *= -1;
    }

    void Movement()
    {
        if (Input.GetKey(Key.A))
        {
            _currentState = State.MOVING;
            MoveUntilCollision(-_xSpeed, 0);
            if (!flipped)
            {
                Flip();
            }
        }
        else if (Input.GetKey(Key.D))
        {
            _currentState = State.MOVING;
            MoveUntilCollision(_xSpeed, 0);
            if (flipped)
            {
                Flip();
            }
        } else 

        {
            _currentState = State.IDLE;
        }
    }

    void Jump()
    {
        _ySpeed += 0.07f;

        if(_ySpeed > 0)
        {
            _currentState = State.JUMPING;
        }

        if(_ySpeed == 0)
        {
            _currentState = State.JUMPING;
        }

        if(MoveUntilCollision(0, _ySpeed) != null)
        {
            if(_ySpeed > 0)
            {
                _currentState = State.IDLE;
            }
            _ySpeed = 0;
        } else
        {
            _currentState = State.JUMPING;
        }

        if (Input.GetKeyDown(Key.SPACE))
        {
            if(_currentState != State.JUMPING)
            {
                
                channel = jump.Play();
                channel.Volume = 0.4f;
                _ySpeed = _jumpSpeed;
                _currentState = State.JUMPING;
            }
            
        }
    }
     void OnCollision(GameObject other)
    {
        if (other is Star)
        {
            channel = coinPickUp.Play();
            Console.WriteLine("Star");
            other.LateDestroy();
            ((MyGame)game).coinsCollected++;
        }

        if(other is Sign book)
        {
            book.WriteText();
        }
        if(other is Bullet b && !(b.Parent is Player p))
        {
            HandleDeath();

        }

        if(other is EatEnemy)
        {
            HandleDeath();
        }

        if(other is PlantEnemy)
        {
            HandleDeath();
        }
    }

    void CheckBoundaries()
    {
        if(y > 720)
        {
            HandleDeath();
        }
    }

    void HandleDeath()
    {
        ((MyGame)game).coinsCollected = 0;
        channel = death.Play();
        ((MyGame)game).LoadLevel("death.tmx");
    }




}

