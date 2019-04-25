 using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public Menu currentMenu;
    public Camera mainCamera;
    public int rotateM;
    bool canRotate = false;
    void Start()
    {
        ShowMenu(currentMenu);
    }
    public void ShowMenu(Menu menu)
    {

        if (currentMenu != null)
        {
            currentMenu.IsOpen = false;
            currentMenu = menu;
            currentMenu.IsOpen = true;
        }
    }
    public void ShowSave(GameObject toggle)
    {

        toggle.SetActive(true);
        GameObject.Find("players1").SetActive(false);

    }
    public void sb(GameObject ds)
    {

        ds.SetActive(true);

    }
    public void dg(GameObject ds)
    {

        ds.SetActive(false);

    }

    void Update()
    {
      
            switch (rotateM)
            {
                case 1:
                    mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, Quaternion.Euler(Vector3.forward), 2.0f * Time.deltaTime);
                    break;
                case 234:
                    mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, Quaternion.Euler(new Vector3 (0.0f, 270.0f)), 2.0f * Time.deltaTime);
                    break;
            case 3:
                mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, Quaternion.Euler(new Vector3(0.0f, 180.0f)), 2.0f * Time.deltaTime);
                break;
                default: break;
            }
    }

    public void CameraMovement(int menu)
    {
        rotateM = menu;
        canRotate = true;
    }
}