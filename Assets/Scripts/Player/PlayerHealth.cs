using UnityEngine;

public class PlayerHealth : MonoBehaviour
{   
    public void TakeDamage()
    {
        //should add flashing/delays to death/damage

        LevelManager.Instance.ReloadThisScene();
    }
}