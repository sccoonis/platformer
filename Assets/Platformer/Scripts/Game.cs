using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Camera raycastCamera;

    public int points = 0;
    public int coinCount = 0;

    public int world = 1;
    public int level = 1;

    public float time = 300f;
    
    public TextMeshProUGUI pointsUI;
    public TextMeshProUGUI coinsUI;
    public TextMeshProUGUI worldUI;
    public TextMeshProUGUI levelUI;
    public TextMeshProUGUI timeUI;

    public GameObject flag;
    public bool touchedflag = false;
    
    private float accumulatedTime = 0f;


    private void Start()
    {
        pointsUI.text = points.ToString("000000");
        coinsUI.text = coinCount.ToString("00");
        worldUI.text = world.ToString();
        levelUI.text = level.ToString();
        timeUI.text = time.ToString("000");
    }


    void Update()
    {
        // UI update ----------------------------------
        pointsUI.text = points.ToString("000000");
        coinsUI.text = coinCount.ToString("00");
        worldUI.text = world.ToString();
        levelUI.text = level.ToString();
        timeUI.text = time.ToString("000");
        // --------------------------------------------
        
        // Timer update -------------------------------
        accumulatedTime += Time.deltaTime;

        if (accumulatedTime >= 0.6f)
        {
            time--;
            accumulatedTime = 0f;
        }
        // --------------------------------------------


        // clicker ------------------------------------
        
        Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            /*
            float l = 10f;
            Debug.DrawLine(hitInfo.point + Vector3.left * l, hitInfo.point + Vector3.right * l, Color.magenta);
            Debug.DrawLine(hitInfo.point + Vector3.up * l, hitInfo.point + Vector3.down * l, Color.magenta);
            */

            if (hitInfo.transform.name == "Brick(Clone)")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    points += 50;
                    Destroy(hitInfo.collider.gameObject);
                    //Debug.Log("clicked a block");
                }
            }
            
            if (hitInfo.transform.name == "Question(Clone)")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    coinCount++;
                    //Debug.Log("got a coin");
                }
            }
        }
        // --------------------------------------------------
        if ((time == 0) && (touchedflag == false))
        {
            Debug.Log("You lose.");
        }
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if (this.name == "Flag" && time > 0)
        {
            touchedflag = true;
        }
    }
}
