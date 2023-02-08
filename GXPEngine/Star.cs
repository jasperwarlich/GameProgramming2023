using System;
using GXPEngine;
using GXPEngine.Core;

public class Star : AnimationSprite {

    public Star() : base("coin.png", 6, 1)
    {
        SetScaleXY(1.5f, 1.5f);
        this.collider.isTrigger = true;
        SetCycle(0, 6);

    }

    void Update()
    {
        Animate(0.1f);
        
    }

   

}