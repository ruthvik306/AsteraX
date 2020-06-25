#define DEBUG_PlayerShip_RespawnNotifications

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    // This is a somewhat protected private singleton for PlayerShip
    static private PlayerShip   _S;
    static public PlayerShip    S
    {
        get
        {
            return _S;
        }
        private set
        {
            if (_S != null)
            {
                Debug.LogWarning("Second attempt to set PlayerShip singleton _S.");
            }
            _S = value;
        }
    }

    [Header("Set in Inspector")]
    public float        shipSpeed = 10f;
    public GameObject   bulletPrefab;

    Rigidbody           rigid;

   public int jumps =  3;

    public TMP_Text Jumptext;
    void Awake()
    {
        S = this;   
        //Setting up the jumps text value
        Jumptext.text = "JUMPS : " + jumps.ToString("0");

        // NOTE: We don't need to check whether or not rigid is null because of [RequireComponent()] above
        rigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // Using Horizontal and Vertical axes to set velocity
        float aX = CrossPlatformInputManager.GetAxis("Horizontal");
        float aY = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 vel = new Vector3(aX, aY);
        if (vel.magnitude > 1)
        {
            // Avoid speed multiplying by 1.414 when moving at a diagonal
            vel.Normalize();
        }

        rigid.velocity = vel * shipSpeed;

        // Mouse input for firing
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            Fire();
        }

        if (jumps < 0) { SceneManager.LoadScene("Game_Over"); }
    }


    void Fire()
    {
        // Get direction to the mouse
        Vector3 mPos = Input.mousePosition;
        mPos.z = -Camera.main.transform.position.z;
        Vector3 mPos3D = Camera.main.ScreenToWorldPoint(mPos);

        // Instantiate the Bullet and set its direction
        GameObject go = Instantiate<GameObject>(bulletPrefab);
        go.transform.position = transform.position;
        go.transform.LookAt(mPos3D);
    }

    static public float MAX_SPEED
    {
        get
        {
            return S.shipSpeed;
        }
    }
    
	static public Vector3 POSITION
    {
        get
        {
            return S.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if( other.gameObject.tag == "Asteroid")
       {
            jumps--;
            Jumptext.text = "JUMPS : " + jumps.ToString("0");
            //Debug.Log(jumps);
            gameObject.SetActive(false);
            Invoke("respawn", 3);
       }
    }

    //iStarts the respawn process
    public void respawn()
    {
        gameObject.transform.position = SafeDistCalculate();
        gameObject.SetActive(true);
    }

    //Calculates a safe distance
    Vector3 SafeDistCalculate()
    {
        Vector3 newSpawnPos = Vector3.zero;
        //calculates the distance between every asteroid and returns the best possible safe distance
        foreach (Asteroid asteroid in FindObjectsOfType<Asteroid>())
        {
            do
            {
                newSpawnPos = RandomSpawnPosition();
            } while (Vector3.Distance(newSpawnPos, asteroid.transform.position) <= 1.8f);
        }
        return newSpawnPos;
    }

    //Returns a randown position inside the screenbounds
    private Vector3 RandomSpawnPosition()
    {
        int xScreenBound = 15;
        int yScreenBound = 8;
        float x = UnityEngine.Random.Range(-xScreenBound, xScreenBound);
        float y = UnityEngine.Random.Range(-yScreenBound, yScreenBound);
        return new Vector3(x, y, 0);
    }
  
   
}
