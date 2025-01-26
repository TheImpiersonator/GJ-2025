using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using System.Runtime.CompilerServices;

public abstract class Soda : MonoBehaviour
{
    [SerializeField] GameObject sodaPrefab;

    //====| EVENTS |==== //SOME EVENTS MAY HAVE TO BE TURNED INTO DELEGATES IF NEEDED SUBSCRIPTIONS
    public UnityEvent OnShoot;
    public UnityEvent OnDrink;
    public delegate void DurationEndCall();
    public DurationEndCall OnDurationEnd;
    public delegate void ThrowCall();
    public ThrowCall OnThrow;
    public UnityEvent OnExplode;
    public delegate void LevelUpCall(int level);
    public LevelUpCall OnLevelUp;
    public delegate void RankUpCall(FIZZ_RANKS rank);

    //====| VARIABLES |====
    //SODA-STATS Struct
    [System.Serializable]
    struct sodaStats {
        //___Shoot Stats
        public float damage;        //Damage Output to enemy/objects per fire rate
        public float shootRange;    //Range for the damage RayCast
        public float spread;        //Spread Angle of the damage raycasting
        public float fireRate;      //Rate at which damage is outputted
        public float useDuration;      //For time it takes to fire or drink

        //___Throw
        public float throwStrength;     //Strength of the throw force
        public float throwAngle;        //Angle that the soda is tossed
        public float explodeDistance;   //AOE explosion distance
    }

    [Tooltip("Add a new set to include a new Level (stat set) to the Soda")]
    [SerializeField] List<sodaStats> levelSets = new List<sodaStats>();

    private float totalTimer;
    private float intervalCounter;

    //___Shake
    [SerializeField] float Max_ShakeAmount;
    float curr_ShakeAmount;
    public int shakeLevel = 0;          //The current Level of the Soda
    public enum FIZZ_RANKS {Stale, Carbonated, FIZZED}; //RANK OF THE SODA Based on Shame Amount [Should effect the Explosion stats]
    public FIZZ_RANKS fizzRank = FIZZ_RANKS.Stale;      //Current Fizz Ranking

    [System.Serializable]
    struct RankMultipliers {
        public float staleMod;
        public float carbMod;
        public float fizzedMod;
    }



    //====| METHODS |====
    public void Shoot(Vector3 foward) {
        Pawn player = GameManager.Instance.player.pawn;
        //store all ai from game manager
        Pawn[] aiPawns = GameManager.Instance.ais.ToArray();
        //GIVE THE SHOOT METHOD THE FORWARD VECTOR
        foward.Normalize();
        RaycastHit hit;
        foreach (var ai in aiPawns) {
            if (ai != null) {
                //SEE IF A RAY CAST HITS BASED ON THE SHOOT RANGE
                Vector3 castVect = ai.gameObject.transform.position - player.gameObject.transform.position;
                //angle between the facing direction and the vector to the target
                float angleToTarget = Vector3.Angle(castVect, player.gameObject.transform.forward);
                //cast ray to AI
                Physics.Raycast(player.gameObject.transform.position, castVect, out hit, levelSets[shakeLevel].shootRange);
                Debug.DrawRay(player.gameObject.transform.position, castVect * levelSets[shakeLevel].shootRange, Color.green, 3f);
                //damage AI if it hits them
                if (hit.collider != null) {
                    if (hit.collider.gameObject == ai.gameObject && angleToTarget <= levelSets[shakeLevel].spread) {
                        ai.gameObject.GetComponent<HealthComponent>().TakeDamage(levelSets[shakeLevel].damage);
                    }
                }
            }
        }

        ShootEffect();
    }
    public void Throw() { }
    public void AddShake(float shakeAmount){
        curr_ShakeAmount += shakeAmount;
        UpdateRank();   //Attempt to Rank up
        UpdateLevel();  //Attempt to Level up 
    }

    protected virtual void ShootEffect() { 
        //Going just by base stats depending on the level
    }

    protected virtual void ExplodeEffect() { 
        //Stat = Level's value * the Rank Modifier :)
    }
    //Don't Mind this mess of a function :)
    void UpdateRank()
    {
        float percent = get_ShakePercent();

        //Stale Rank - Lower Shake percent
        if (percent < (1 / 3) && fizzRank != FIZZ_RANKS.Stale)
        { fizzRank = FIZZ_RANKS.Stale; }
        //Carbonated Rank - Mid shake Percent
        else if ((1 / 3) < percent && percent < (2 / 3) && fizzRank != FIZZ_RANKS.Carbonated)
        { fizzRank = FIZZ_RANKS.Carbonated; }
        //FIZZ RANK - High Shake Percent
        else if (percent > (2 / 3) && fizzRank != FIZZ_RANKS.FIZZED)
        { fizzRank = FIZZ_RANKS.FIZZED; }

        else {
            Debug.Log("====| COULDN'T DETECT FIZZ RANK |====");
            fizzRank = FIZZ_RANKS.FIZZED;
        }
    }

    void UpdateLevel() {
        shakeLevel = (int)(levelSets.Count * get_ShakePercent());
    }

    public float get_ShakePercent() {
        return Mathf.Clamp01(curr_ShakeAmount/Max_ShakeAmount);
    }
    public void UpdateShakeAmount(float changeValue) {
        curr_ShakeAmount += changeValue;
    }
    public GameObject get_Prefab()
    {
        return sodaPrefab;
    }
}
