using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class PCController : MonoBehaviour
{
    public enum AmmoStates
    {
        NoBullets,
        StickyandLiquorice,
        PopRock,
        SourSplash
    }

    public AmmoStates currentAmmoState;

    [Header("PC Variables")] 
    Rigidbody rb;

    [Header("Special Ability Variables")]
    [SerializeField] float bulletSpeed;
    Vector3 dir; //The direction we want the bullet to follow


    [Header("Sticky/Liquorice Ammo Variables")]
    [SerializeField] GameObject projectilePrefab;

    GameObject clone; //the bullet being instantiated

    public float curveHight = 25f;
    public float gravity = -18f;

    public List<GameObject> stickyAmmos;

    [Header("Pop Rock Ammo Variables")]
    [SerializeField] GameObject popRockProjectilePrefab;

    Vector3 playerLaunchDir; //The direction we want the player to be launched when the ammo is launched

    public float pushingForce = 5f;
    public float whenToLaunch = 1.5f;
    public float verticalLaunchOffset = 5f;
    public float horizontalLaunchOffset = 2f;

    float launchDirectionZ; //The position of the launchDirection in the Z
    float launchDirectionY; //The position of the launchDirection in the Y
    float positionZ; //The position of the enemy in the Z
    float positionY; //The position of the enemy in the Y

    public List<GameObject> popRockAmmos;

    [Header("Sour Splash Ammo Variables")]
    [SerializeField] GameObject sourSplashProjectilePrefab;

    public List<GameObject> sourSplashAmmos;

    [Header("Level Checkpoint Variables")]
    List<Transform> levelCheckpoints;


    [Header("Score Variables")]
    public int score;
    int sessionNumber;

    //[Header("VFX Variables")]
    //public GameObject checkpointVfx;
    //VisualEffect checkpointVfxGraph;

    //Color cylinderColor;
    //Color ringColor;

    //public Color newCylinderColor;
    //public Color newRingColor;

    private void Awake()
    {
        //Get session number integer value from key "Session Number", if key not found create key with value = 1
        sessionNumber = PlayerPrefs.GetInt("SessionNumber", 1);

        Debug.Log("Current session number is:" + sessionNumber);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentAmmoState = AmmoStates.NoBullets;

        levelCheckpoints = new List<Transform>();

        score = 0; // at the beginning score is 0

        stickyAmmos = new List<GameObject>();
        popRockAmmos = new List<GameObject>();
        sourSplashAmmos = new List<GameObject>();

       
    }

    private void Update()
    {
                
        switch (currentAmmoState)
        {
            case AmmoStates.NoBullets:

                break;

            case AmmoStates.StickyandLiquorice:

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 30f))
                {
                    //Debug.Log(hit.point);

                    if (Input.GetMouseButtonDown(0) && stickyAmmos.Count > 0)
                    {
                        LaunchProjectile(hit.point);

                        //Remove from list of ammos
                        int i = stickyAmmos.Count - 1; //get the last ammo on the list
                        GameObject stickyAmmoToRemove = stickyAmmos[i]; //store it as a gameobject
                        Destroy(stickyAmmoToRemove); //destroy it
                        stickyAmmos.RemoveAt(i); //then remove from the list

                        UIManager.instance.stickyAmmoText.text = stickyAmmos.Count.ToString(); //Update the new remaining number of ammo on the UI text

                        if (currentAmmoState != AmmoStates.NoBullets)
                        {
                            currentAmmoState = AmmoStates.NoBullets; //switch to no bullets after launching to prevent misfire unless chosen
                        }
                    }

                    else if(Input.GetMouseButtonDown(0) && stickyAmmos.Count <= 0 && !EventSystem.current.IsPointerOverGameObject())
                    {
                        if(currentAmmoState != AmmoStates.NoBullets)
                        {
                            currentAmmoState = AmmoStates.NoBullets;
                        }
                    }

                }

                break;

            case AmmoStates.PopRock:

                PopRockShooting();
                                

                break;

                case AmmoStates.SourSplash:

                SourSplashShooting();

                break;
        }
                
    }


    void PopRockShooting()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 30f))
        {                        
            if (Input.GetMouseButtonDown(0) && popRockAmmos.Count > 0)
            {

                GameObject bullet = Instantiate(popRockProjectilePrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity) as GameObject;

                dir = hit.point - transform.position;
                Vector3 newDir = dir.normalized;


                bullet.GetComponent<Rigidbody>().velocity = newDir * bulletSpeed;

               
                StartCoroutine(LaunchPlayer());

                //Remove from list of ammos
                int i = popRockAmmos.Count - 1; //get the last ammo on the list
                GameObject popRockAmmoToRemove = popRockAmmos[i]; //store it as a gameobject
                Destroy(popRockAmmoToRemove); //destroy it
                popRockAmmos.RemoveAt(i); //then remove from the list

                UIManager.instance.popRockAmmoText.text = popRockAmmos.Count.ToString(); //Update the new remaining number of ammo on the UI text

                if (currentAmmoState != AmmoStates.NoBullets)
                {
                    currentAmmoState = AmmoStates.NoBullets; //switch to no bullets after launching to prevent misfire unless chosen
                }
            }

            else if (Input.GetMouseButtonDown(0) && popRockAmmos.Count <= 0 && !EventSystem.current.IsPointerOverGameObject())
            {
                if (currentAmmoState != AmmoStates.NoBullets)
                {
                    currentAmmoState = AmmoStates.NoBullets;
                }
            }
        }

    }

    IEnumerator LaunchPlayer()
    {
        positionZ = transform.position.z;
        positionY = transform.position.y;
        launchDirectionZ = positionZ + horizontalLaunchOffset;
        launchDirectionY = positionY + verticalLaunchOffset;


        //Launch direction is the difference between our destination and our current position
        playerLaunchDir = new Vector3(0f, launchDirectionY - positionY, launchDirectionZ - positionZ);

        yield return new WaitForSeconds(whenToLaunch);

        AudioManager.instance.PlaySound("PopRockAmmo");

        rb.AddForce(playerLaunchDir * pushingForce, ForceMode.Impulse);
    }

    void SourSplashShooting()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 30f))
        {           

            if (Input.GetMouseButtonDown(0) && sourSplashAmmos.Count > 0)
            {

                GameObject bullet = Instantiate(sourSplashProjectilePrefab, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity) as GameObject;

                dir = hit.point - transform.position;
                Vector3 newDir = dir.normalized;


                bullet.GetComponent<Rigidbody>().velocity = newDir * bulletSpeed;

                //Remove from list of ammos
                int i = sourSplashAmmos.Count - 1; //get the last ammo on the list
                GameObject sourSplashAmmoToRemove = sourSplashAmmos[i]; //store it as a gameobject
                Destroy(sourSplashAmmoToRemove); //destroy it
                sourSplashAmmos.RemoveAt(i); //then remove from the list

                UIManager.instance.sourSplashAmmoText.text = sourSplashAmmos.Count.ToString(); //Update the new remaining number of ammo on the UI text

                if (currentAmmoState != AmmoStates.NoBullets)
                {
                    currentAmmoState = AmmoStates.NoBullets; //switch to no bullets after launching to prevent misfire unless chosen
                }
            }

            else if (Input.GetMouseButtonDown(0) && sourSplashAmmos.Count <= 0 && !EventSystem.current.IsPointerOverGameObject())
            {
                if (currentAmmoState != AmmoStates.NoBullets)
                {
                    currentAmmoState = AmmoStates.NoBullets;
                }
            }
        }

    }

    void LaunchProjectile(Vector3 target)
    {

        clone = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity) as GameObject;

        clone.GetComponent<Rigidbody>().AddForce(Vector3.up * gravity);

        clone.GetComponent<Rigidbody>().velocity = CalculateLaunchData(target).initialVelocity;

        
    }

    LaunchData CalculateLaunchData(Vector3 target)
    {
        float displacementY = target.y - transform.position.y;
        Vector3 displacementXZ = new Vector3(target.x - transform.position.x, 0, target.z - transform.position.z);
        float time = Mathf.Sqrt(-2 * curveHight / gravity) + Mathf.Sqrt(2 * (displacementY - curveHight) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * curveHight);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);

        
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }

      

    private void OnTriggerEnter(Collider c)
    {
        if(c.tag == "RegularPickup")
        {
            score += 1; //increase the score count by 1

            AudioManager.instance.PlaySound("Pickup");

            UIManager.instance.scoreText.text = score.ToString(); //update the UI score text
            UIManager.instance.scoreVictoryScreenText.text = score.ToString(); //update the UI score text on the victory screen

            CandyGameManager.instance.currentNumberOfPickups++; //update the number of pickups collected
            CandyGameManager.instance.pickupText.text = CandyGameManager.instance.currentNumberOfPickups.ToString(); //update the number of pickups collected on the UI

            Destroy(c.gameObject); //Destroy the pickup after collision
        }

        if(c.tag == "SpecialPickup")
        {
            score += 5; //increase the score count by 5

            AudioManager.instance.PlaySound("Pickup");

            UIManager.instance.scoreText.text = score.ToString(); //update the UI score text
            UIManager.instance.scoreVictoryScreenText.text = score.ToString(); //update the UI score text on the victory screen

            CandyGameManager.instance.currentNumberOfPickups++; //update the number of pickups collected
            CandyGameManager.instance.pickupText.text = CandyGameManager.instance.currentNumberOfPickups.ToString(); //update the number of pickups collected on the UI

            Destroy(c.gameObject); //Destroy the pickup after collision
        }

        if(c.tag == "RawMaterial")
        {
            AudioManager.instance.PlaySound("RawMaterial");

            CandyGameManager.instance.currentNumberOfRawMaterials++; //update the number of raw materials collected
            CandyGameManager.instance.rawMaterialsText.text = CandyGameManager.instance.currentNumberOfRawMaterials.ToString(); //update the number of raw materials collected on the UI

            Destroy(c.gameObject); //Destroy the raw material after collision
        }

        if (c.tag == "Death")
        {
            AudioManager.instance.PlaySound("CharacterDeath");

            OnDeath();
        }

        if (c.tag == "CheckpointObjective")
        {
            AudioManager.instance.PlaySound("Checkpoints");

            CandyGameManager.instance.currentNumberOfCheckpointsPassed++; //update the number of checkpoints passed
            CandyGameManager.instance.checkpointsText.text = CandyGameManager.instance.currentNumberOfCheckpointsPassed.ToString(); //update the number of checkpoints passed on the UI

            Destroy(c.gameObject); //Destroy the checkpoint after collision
        }

        if (c.tag == "LevelCheckpoint")
        {
            Transform newCheckpoint = c.gameObject.transform; //store the collided checkpoint
            levelCheckpoints.Add(newCheckpoint); //add it to the list                        

            //Change the color of the vfxgraph on collision
            //ChangeCheckpointColor();
        }

        if(c.tag == "FinalLevelCheckpoint")
        {
            //If level 1
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1"))
            {
                if (CandyGameManager.instance.currentNumberOfCheckpointsPassed >= CandyGameManager.instance.totalnumberOfCheckpoints)
                {
                    int scoreNumber = score;

                    SendScoreToManager(scoreNumber, sessionNumber);

                    sessionNumber += 1;

                    //Update the session number to whatever the current session number is, in the save data
                    PlayerPrefs.SetInt("SessionNumber", sessionNumber);

                    UIManager.instance.victoryScreen.SetActive(true);
                    Time.timeScale = 0f;
                }           
            }
            //If level 2
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2"))
            {
                if(CandyGameManager.instance.currentNumberOfRawMaterials >= CandyGameManager.instance.totalnumberOfRawMaterials)
                {      
                    if(CandyGameManager.instance.currentTime <= 60f)
                    {
                        score += 15;
                        UIManager.instance.scoreText.text = score.ToString(); //update the UI score text
                        UIManager.instance.scoreVictoryScreenText.text = score.ToString(); //update the UI score text on the victory screen
                    }
                    else if (CandyGameManager.instance.currentTime >60f && CandyGameManager.instance.currentTime <= 90f)
                    {
                        score += 10;
                        UIManager.instance.scoreText.text = score.ToString(); //update the UI score text
                        UIManager.instance.scoreVictoryScreenText.text = score.ToString(); //update the UI score text on the victory screen
                    }
                    else if (CandyGameManager.instance.currentTime > 90f && CandyGameManager.instance.currentTime <= 120f)
                    {
                        score += 5;
                        UIManager.instance.scoreText.text = score.ToString(); //update the UI score text
                        UIManager.instance.scoreVictoryScreenText.text = score.ToString(); //update the UI score text on the victory screen
                    }

                    int scoreNumber = score;

                    SendScoreToManager(scoreNumber, sessionNumber);

                    sessionNumber += 1;

                    //Update the session number to whatever the current session number is, in the save data
                    PlayerPrefs.SetInt("SessionNumber", sessionNumber);

                    UIManager.instance.victoryScreen.SetActive(true);
                    Time.timeScale = 0f;
                }
            }
            //If level 3
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
            {
                if (CandyGameManager.instance.currentNumberOfPickups >= CandyGameManager.instance.totalNumberOfPickups)
                {
                    if(CandyGameManager.instance.extraPickups > 0f && CandyGameManager.instance.extraPickups <= 5f)
                    {
                        score += 5;
                        UIManager.instance.scoreText.text = score.ToString(); //update the UI score text
                        UIManager.instance.scoreVictoryScreenText.text = score.ToString(); //update the UI score text on the victory screen
                    }
                    else if (CandyGameManager.instance.extraPickups > 5f && CandyGameManager.instance.extraPickups <= 15f)
                    {
                        score += 10;
                        UIManager.instance.scoreText.text = score.ToString(); //update the UI score text
                        UIManager.instance.scoreVictoryScreenText.text = score.ToString(); //update the UI score text on the victory screen
                    }
                    else if (CandyGameManager.instance.extraPickups > 15f)
                    {
                        score += 15;
                        UIManager.instance.scoreText.text = score.ToString(); //update the UI score text
                        UIManager.instance.scoreVictoryScreenText.text = score.ToString(); //update the UI score text on the victory screen
                    }

                    int scoreNumber = score;

                    SendScoreToManager(scoreNumber, sessionNumber);

                    sessionNumber += 1;

                    //Update the session number to whatever the current session number is, in the save data
                    PlayerPrefs.SetInt("SessionNumber", sessionNumber);

                    UIManager.instance.victoryScreen.SetActive(true);
                    Time.timeScale = 0f;
                }
            }
            //If level 4
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level4"))
            {                
                if (CandyGameManager.instance.currentNumberOfEnemies >= CandyGameManager.instance.totalNumberOfEnemies)
                {
                    if (CandyGameManager.instance.extraEnemies > 0f && CandyGameManager.instance.extraEnemies <= 5f)
                    {
                        score += 5;
                        UIManager.instance.scoreText.text = score.ToString(); //update the UI score text
                        UIManager.instance.scoreVictoryScreenText.text = score.ToString(); //update the UI score text on the victory screen
                    }
                    else if (CandyGameManager.instance.extraEnemies > 5f && CandyGameManager.instance.extraEnemies <= 10f)
                    {
                        score += 10;
                        UIManager.instance.scoreText.text = score.ToString(); //update the UI score text
                        UIManager.instance.scoreVictoryScreenText.text = score.ToString(); //update the UI score text on the victory screen
                    }
                    else if (CandyGameManager.instance.extraEnemies > 10f)
                    {
                        score += 15;
                        UIManager.instance.scoreText.text = score.ToString(); //update the UI score text
                        UIManager.instance.scoreVictoryScreenText.text = score.ToString(); //update the UI score text on the victory screen
                    }

                    int scoreNumber = score;

                    SendScoreToManager(scoreNumber, sessionNumber);

                    sessionNumber += 1;

                    //Update the session number to whatever the current session number is, in the save data
                    PlayerPrefs.SetInt("SessionNumber", sessionNumber);

                    UIManager.instance.finalVictoryScreen.SetActive(true);
                    Time.timeScale = 0f;
                }
            }

        }

        if(c.tag == "StickyAmmo")
        {
            AudioManager.instance.PlaySound("AmmoCollection");

            GameObject newStickyAmmo = c.gameObject; //store the collided ammo
            stickyAmmos.Add(newStickyAmmo); //add it to the list

            UIManager.instance.stickyAmmoText.text = stickyAmmos.Count.ToString(); //update the UI ammo text on the screen

            Destroy(c.gameObject); //Destroy the ammo pickup after collision
        }

        if(c.tag == "PopRockAmmo")
        {
            AudioManager.instance.PlaySound("AmmoCollection");

            GameObject newPopRockAmmo = c.gameObject; //store the collided ammo
            popRockAmmos.Add(newPopRockAmmo); //add it to the list

            UIManager.instance.popRockAmmoText.text = popRockAmmos.Count.ToString(); //update the UI ammo text on the screen

            Destroy(c.gameObject); //Destroy the ammo pickup after collision
        }

        if(c.tag == "SourSplashAmmo")
        {
            AudioManager.instance.PlaySound("AmmoCollection");

            GameObject newSourSplashAmmo = c.gameObject; //store the collided ammo
            sourSplashAmmos.Add(newSourSplashAmmo); //add it to the list

            UIManager.instance.sourSplashAmmoText.text = sourSplashAmmos.Count.ToString(); //update the UI ammo text on the screen

            Destroy(c.gameObject); //Destroy the ammo pickup after collision
        }
    }

    public void SendScoreToManager(int score, int sessionNumber)
    {
        ScoreManager.instance.AddNewScore(score, sessionNumber);
    }

    //void ChangeCheckpointColor()
    //{
    //    Debug.Log("I'm changing color");

    //    checkpointVfxGraph = checkpointVfx.GetComponent<VisualEffect>();

    //    cylinderColor = checkpointVfxGraph.GetVector4("Cylinder Color"); //Vector4 is to get the color variable
    //    ringColor = checkpointVfxGraph.GetVector4("RingColor"); //Vector4 is to get the color variable

    //    cylinderColor = newCylinderColor;
    //    ringColor = newRingColor;
    //}

    void OnDeath()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        int i = levelCheckpoints.Count - 1;
        Transform recentCheckpoint = levelCheckpoints[i];        
        gameObject.transform.position = new Vector3(recentCheckpoint.position.x, recentCheckpoint.position.y + 3f, recentCheckpoint.position.z); //on death the player position is moved to the recently passed checkpoint as stored in the list

        //Camera.main.transform.position = new Vector3(recentCheckpoint.position.x, recentCheckpoint.position.y + 6f, recentCheckpoint.position.z - 9f); //Resetting the camera position
        //Camera.main.transform.LookAt(transform.position);
    }

    public void StickyAmmoSelected()
    {
        if(currentAmmoState != AmmoStates.StickyandLiquorice)
        {
            currentAmmoState = AmmoStates.StickyandLiquorice;
        }
    }

    public void PopRockAmmoSelected()
    {
        if(currentAmmoState != AmmoStates.PopRock)
        {
            currentAmmoState = AmmoStates.PopRock;
        }
    }

    public void SourSplashAmmoSelected()
    {
        if(currentAmmoState != AmmoStates.SourSplash)
        {
            currentAmmoState = AmmoStates.SourSplash;
        }
    }

    
}
