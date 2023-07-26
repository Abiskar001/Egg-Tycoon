using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    TimeManager timeManager;
    Animator anim;

    public ChickenBehaviour chickenBehaviour;
    
    public float movementSpeed;

    [Header("Age")]
    [Range(6,10)]
    public int lifeExpentancy;
    [SerializeField] private float lifespanInDays;
    [SerializeField] private float ageInDays;
    float livesTillDay;

    [Header("Hunger")]
    [Range(0f,100f)]
    [SerializeField] private float hunger = 50;
    [SerializeField] private float hungerThreshold = 30;
    [SerializeField] private float hungerDecreaseRate = 0.25f ;
    [SerializeField] private float minimunFeedAmount = 10f;
    [SerializeField] private float hungerDecrementBeforePooping;
    [SerializeField] private float hungerTarget;
    [SerializeField] private bool wantsToEat;
    private Food food;
    private float poopDecrementTracker;
    private float lastHungerValue;
    public GameObject poop;
    public bool poopOnGround = false;

    [Header("Thirst")]
    [Range(0f, 100f)]
    [SerializeField] private float thirst = 50;
    [SerializeField] private float thirstDecreaseRate = 0.4f;
    [SerializeField] private float thirstThreshold = 30f;
    [SerializeField] private float minimunWaterAmount = 10f;
    private Water water;
    [SerializeField] private float thirstTarget;
    [SerializeField] private bool wantsToDrink;

    [Header("Stamina")]
    [Range(0f, 100f)]
    [SerializeField] private float stamina = 50;
    [SerializeField] private float staminaDecreaseRate = 2f;
    [SerializeField] private float staminaIncreaseRate = 10f;

    [Header("Egg Laying")]
    [Range(0f,100f)]
    public float health;
    private float maxHealth = 100f;
    [SerializeField] private Gender gender;
    [SerializeField] private bool canLayEggs;
    [Range(0, 10)]
    [SerializeField] private int eggLaidPerDay;
    public GameObject eggs;
    float eggLayingCountdown , currentEggLayingCountdown;

    [Header("Boundaries")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private Vector3 chickenPosition;

    [Header("Miscelleneous")]
    [SerializeField] private Vector3 randomPosition;
    [SerializeField] private float roamedFor_Seconds;

    private void OnValidate()
    {
        if (gender == Gender.Male)
        {
            canLayEggs = false;
            eggLaidPerDay = 0;
        }

        eggLayingCountdown = 21600 / (eggLaidPerDay * 60);
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        food = FindObjectOfType<Food>();
        water = FindObjectOfType<Water>();
        timeManager = FindObjectOfType<TimeManager>();
        lifespanInDays = Random.Range((lifeExpentancy - 1) *365 , (lifeExpentancy + 1) * 365);
        livesTillDay = Mathf.RoundToInt(timeManager.unaffectedDay + (lifespanInDays - ageInDays));
        randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
        stamina = Random.Range(50f, 100f);
    }
    void Start()
    {
        EventManager.dayPassed += increaseAge;

    }

    // Update is called once per frame
    void Update()
    {
        //sets boundaries for chicken
        chickenPosition = transform.position;
        chickenPosition.x = Mathf.Clamp(chickenPosition.x, minX, maxX);
        chickenPosition.y = Mathf.Clamp(chickenPosition.y, minY, maxY);
        transform.position = chickenPosition;

        if (chickenBehaviour != ChickenBehaviour.Feeding)
        {
            lastHungerValue = hunger;
        }

        //checks how long chicken Roamed for
        if (chickenBehaviour == ChickenBehaviour.Roaming)
        {
            roamedFor_Seconds += Time.deltaTime;
        }

        //decreases thirst and hunger bar as per time goes        
        thirst -= thirstDecreaseRate * Time.deltaTime;
        
        hunger -= hungerDecreaseRate * Time.deltaTime;


        //tracks how much hunger decreased and spawns poop as per time!
        if (chickenBehaviour != ChickenBehaviour.Feeding)
        {
            if (poopDecrementTracker < hungerDecrementBeforePooping)
            {
                poopDecrementTracker += (lastHungerValue - hunger);
            }
            else
            {
                var spawnedPoop = Instantiate(poop);
                spawnedPoop.transform.position = transform.position;
                poopDecrementTracker = 0;
            }
        }

        //code to make sure by when they can lay eggs and till when
        if (ageInDays < 126 || lifespanInDays - ageInDays < 365)
        {
            canLayEggs = false;
        }
        else
        {
            canLayEggs = true;
        }

        //tracks when to lay egg
        if (canLayEggs)
        {
            if (currentEggLayingCountdown >= eggLayingCountdown && health > 50 && canLayEggs && gender == Gender.Female && hunger > hungerThreshold && thirst > thirstThreshold && chickenBehaviour == ChickenBehaviour.Roaming && currentEggLayingCountdown > eggLayingCountdown && chickenBehaviour != ChickenBehaviour.Feeding && chickenBehaviour != ChickenBehaviour.Drinking && roamedFor_Seconds > 5f)
            {
                chickenBehaviour = ChickenBehaviour.LayingEggs;
                currentEggLayingCountdown = 0;
            }
            else
            {
                currentEggLayingCountdown += Time.deltaTime;
            }
        }

        //tracks when to go for food
        if (!wantsToEat && Random.value<0.000001f && food.foodAmount > 0 && chickenBehaviour != ChickenBehaviour.Drinking && roamedFor_Seconds > 5f)
        {
            chickenBehaviour = ChickenBehaviour.Feeding;
            hungerTarget = Random.Range(hunger, hunger + 20);
            wantsToEat = true;
        }
        if (!wantsToEat && hunger < hungerThreshold && food.foodAmount > 0 && chickenBehaviour != ChickenBehaviour.Drinking && roamedFor_Seconds > 5f)
        {
            chickenBehaviour = ChickenBehaviour.Feeding;
            hungerTarget = Random.Range(hungerThreshold + minimunFeedAmount, 100f);
            wantsToEat = true;
        }
        if (food.foodAmount <= 0 && wantsToEat && chickenBehaviour != ChickenBehaviour.Drinking && roamedFor_Seconds > 5f)
        {
            chickenBehaviour = ChickenBehaviour.Roaming;
            wantsToEat = false;
            Debug.Log("No Food");
        }

        //tracks when to go for water
        if (!wantsToDrink && Random.value < 0.000001f && water.waterAmount > 0 && chickenBehaviour != ChickenBehaviour.Feeding && roamedFor_Seconds > 5f)
        {
            chickenBehaviour = ChickenBehaviour.Drinking;
            thirstTarget = Random.Range(thirst, thirst + 20);
            wantsToDrink = true;
        }
        if (!wantsToDrink && thirst < thirstThreshold && chickenBehaviour != ChickenBehaviour.Feeding && roamedFor_Seconds > 5f)
        {
            chickenBehaviour = ChickenBehaviour.Drinking;
            thirstTarget = Random.Range(thirstThreshold + minimunWaterAmount, 100f);
            wantsToDrink = true;
        }
        if (water.waterAmount <= 0 && wantsToDrink && chickenBehaviour != ChickenBehaviour.Feeding && roamedFor_Seconds > 5f)
        {
            chickenBehaviour = ChickenBehaviour.Roaming;
            wantsToDrink = false;
            Debug.Log("No Water");
        }

        if (wantsToDrink && wantsToEat)
        {
            if ((thirstThreshold - thirst) < (hungerThreshold - hunger))
            {
                chickenBehaviour = ChickenBehaviour.Feeding;
                wantsToDrink = false;
            }
            else if ((thirstThreshold - thirst) > (hungerThreshold - hunger))
            {
                chickenBehaviour = ChickenBehaviour.Drinking;
                wantsToEat = false;
            }
        }

        //decrease health if not in proper health
        if (hunger < 0)
        {
            hunger = 0;
            health -= Time.deltaTime;
        }
        if (thirst < 0)
        {
            thirst = 0;
            health -= Time.deltaTime;
        }

        if (health < maxHealth && hunger > hungerThreshold && thirst > thirstThreshold && poopOnGround == false)
        {
            health += Time.deltaTime;
        }


        //controls chicken behaviour
        switch (chickenBehaviour)
        {

            case ChickenBehaviour.Roaming:
                anim.SetBool("isLaying", false);
                anim.SetBool("isFeeding", false);
                anim.SetBool("isResting", false);
                flipper(randomPosition);

                if (stamina <= 0)
                {
                    chickenBehaviour = ChickenBehaviour.Resting;
                }
                if (chickenPosition != randomPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, randomPosition, movementSpeed * Time.deltaTime);
                    stamina -= staminaDecreaseRate * Time.deltaTime;
                }
                else
                {
                    randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);                  
                }
                break;

            case ChickenBehaviour.Feeding:
                roamedFor_Seconds = 0;
                flipper(food.transform.position);
                if (Vector3.Distance(transform.position,food.gameObject.transform.position) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, food.gameObject.transform.position, movementSpeed * 2 * Time.deltaTime);

                    if (food.foodAmount <= 0)
                    {
                        chickenBehaviour = ChickenBehaviour.Roaming;                       
                        Debug.Log("No Food");
                        break;
                    }
                }
                else
                {
                    if (hunger < hungerTarget)
                    {
                        anim.SetBool("isFeeding", true);
                        hunger += food.hungerIncreaseRate * Time.deltaTime;
                        food.foodAmount -= food.hungerIncreaseRate * Time.deltaTime;
                    }
                    else
                    {
                        chickenBehaviour = ChickenBehaviour.Roaming;
                        wantsToEat = false;
                    }
                }
                break;

            case ChickenBehaviour.Drinking:
                roamedFor_Seconds = 0;
                flipper(water.transform.position);
                if (Vector3.Distance(transform.position, water.gameObject.transform.position) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, water.gameObject.transform.position, movementSpeed * 2 * Time.deltaTime);

                    if (water.waterAmount <= 0)
                    {
                        chickenBehaviour = ChickenBehaviour.Roaming;
                        Debug.Log("No Water");
                        break;
                    }
                }
                else
                {
                    if (thirst < thirstTarget)
                    {
                        anim.SetBool("isFeeding", true);
                        thirst += water.thirstIncreaseRate * Time.deltaTime;
                        water.waterAmount -= water.thirstIncreaseRate * Time.deltaTime;                     
                    }
                    else
                    {
                        chickenBehaviour = ChickenBehaviour.Roaming;
                        wantsToDrink = false;
                    }
                }
                break;

            case ChickenBehaviour.LayingEggs:
                anim.SetBool("isLaying", true);
                break;

            case ChickenBehaviour.Resting:
                if (stamina >= 100)
                {
                    chickenBehaviour = ChickenBehaviour.Roaming;
                }
                anim.SetBool("isResting", true);
                stamina += staminaIncreaseRate * Time.deltaTime;
                break;
            default:
                break;
        }
        
    }
    void layEgg()
    {
        var spawnedEgg = Instantiate(eggs);
        spawnedEgg.transform.position = transform.position;
    }

    void increaseAge()
    {
        ageInDays += 1;
    }
    private void OnDisable()
    {
        EventManager.dayPassed -= increaseAge;
    }
    //flips the character
    void rightflip()
    {
        var tempScale = transform.localScale;
        tempScale.x = -0.1f;
        transform.localScale = tempScale;
    }

    void leftflip()
    {
        var tempScale = transform.localScale;
        tempScale.x = 0.1f;
        transform.localScale = tempScale;
    }

    void flipper(Vector3 targetLocation)
    {
        if (targetLocation.x > transform.position.x)
        {
            rightflip();
        }
        else
        {
            leftflip();
        }
    }

    void backToRoam()
    {
        chickenBehaviour = ChickenBehaviour.Roaming;
    }

}