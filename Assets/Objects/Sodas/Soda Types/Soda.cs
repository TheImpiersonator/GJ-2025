using UnityEngine.Events;
using System.Collections;
using UnityEngine;

public abstract class Soda : MonoBehaviour
{
    //====| EVENTS |====
    public UnityEvent OnShoot;
    public UnityEvent OnDrink;
    public delegate void DurationEndCall();
    public DurationEndCall OnDurationEnd;
    public delegate void ThrowCall();
    public ThrowCall OnThrow;
    public UnityEvent OnExplode;

    //====| VARIABLES |====
    //___Shoot
    public float damage;        //Damage Output to enemy/objects per fire rate
    public float shootRange;    //Range for the damage RayCast
    public float spread;        //Spread Angle of the damage raycasting
    public float fireRate;      //Rate at which damage is outputted
    public float useDuration;      //For time it takes to fire or drink

    //___Throw
    public float throwStrength;     //Strength of the throw force
    public float throwAngle;        //Angle that the soda is tossed
    public float explodeDistance;   //AOE explosion distance

    //___Shake
    //[Still wanting to talk about how the shaking works]
    public float currShakePercent;  //
    protected int MaxShakeLevel;    //Max amount of Levels ups the soda has
    public int shakeLevel;          //The current

    public void Shoot() { }
    public void Throw() { }
    public void Shake() { }

    protected virtual void ShootEffect() { 
        
    }

    protected virtual void ExplodeEffect() { 
        
    }
}
