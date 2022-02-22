using UnityEngine;

public class AnimateQuestion : MonoBehaviour
{
    
    private float accumulatedTime = 0f;
    private float totalTime = 0f;
    
    void Update()
    {
        
        accumulatedTime += Time.deltaTime;

        if (accumulatedTime >= 0.15f)
        {
            totalTime += 1f;
            accumulatedTime = 0f;
            
            Material mat = GetComponent<MeshRenderer>().material;
            mat.mainTextureOffset = mat.mainTextureOffset + new Vector2(0f, 0.2f);        
        }

    }
}
