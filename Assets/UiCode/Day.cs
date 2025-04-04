using UnityEngine;

public class Day : MonoBehaviour
{
    public string GetTime { get { return gameTime > 17.0f ? "Night" : "Day"; } }
    float gameTime;
    bool flag;
    private void FixedUpdate()
    {
        gameTime = (gameTime + Time.fixedDeltaTime * 20) / 24.0f;

        if (GetTime == "Night" && flag)
        {
            flag = false;
            // TakeAlly();
        }
        if(GetTime == "Day" )
            flag = true;
    }
    private void TakeAlly()
    {

    }
}
