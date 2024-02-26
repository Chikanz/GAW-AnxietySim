using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMan : MonoBehaviour
{
    public GameObject circle;
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private TextMeshProUGUI headerText;
    public FirstPersonController FPC;

    public static GameMan instance;

    private bool gameRunning = true;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameRunning && Input.GetMouseButton(0))
        {
            //Reload scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public void Setdeath(string reason)
    {
        if (!gameRunning) return;
        
        circle.SetActive(false);
        gameRunning = false;
        deathText.text = reason;
        FPC.playerCanMove = false;
        FPC.enableHeadBob = false;
        FPC.mouseSensitivity *= 0f;
        var cam = FPC.GetComponentInChildren<Camera>();
        cam.GetComponent<VisionCone>().enabled = false;
        cam.GetComponent<SitInteractor>().enabled = false;
        cam.GetComponent<LookTriggerCamera>().enabled = false;
        headerText.gameObject.SetActive(true);
        FPC.enableZoom = true;
        FPC.isZoomed = true;
        Invoke(nameof(fallOver), 2f);
        ProgressMan.instance.ForceClearGoal();
        GetComponent<AudioSource>().Play();
    }

    private void fallOver()
    {
        FPC.enabled = false;
        FPC.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
