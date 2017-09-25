using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject sceneCamera;
    public GameObject pauseMenu;
    public GameObject playerPrefab;
    public GameObject enemies;
    public string[] CaveWorldAreas;
    public GameObject[] AreaSpawns;
    public bool inGame;
    public float exitDelay;

    private GameObject player;
    private bool pauseMenuOpen = false;
    private GameObject pm;
    private bool canExit = false;

	void Start () {
        if (!inGame)
            return;

        SpawnPlayer();
        SetSceneCameraPlayer();
        SetPlayerGameController();
	}

	void Update () {

        if (!canExit)
            exitDelay -= Time.deltaTime;
        if (exitDelay <= 0)
            canExit = true;

        if (Input.GetButtonDown("Pause"))
        {
            if (pauseMenuOpen)
            {
                Destroy(pm);
                togglePauseMenu();
            }
            else if (!pauseMenuOpen)
            {
                pm = Instantiate(pauseMenu);
                pm.GetComponent<PauseController>().SetGameController(GetComponent<GameController>());
            }
        }
    }

    void SpawnPlayer()
    {
        WorldController.World world = WorldController.GetWorld();

        SpawnPlayerCaveWorld();
    }

    void SpawnPlayerCaveWorld()
    {
        GameObject spawn;
        spawn = AreaSpawns[WorldController.GetSpawn()];
        player = Instantiate(playerPrefab, spawn.transform);
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(spawn.GetComponent<SpawnController>().xForce, spawn.GetComponent<SpawnController>().yForce));
    }

    public void togglePauseMenu()
    {
        pauseMenuOpen = !pauseMenuOpen;
        player.GetComponent<PlayerController>().TogglePaused();
        enemies.GetComponent<EnemyGroupController>().Pause();

    }

    public void ChangeArea(int currArea, int newArea, int spawn)
    {
        WorldController.SetPrevArea(currArea);
        WorldController.SetArea(newArea);
        WorldController.SetSpawn(spawn);

        SceneManager.LoadScene(CaveWorldAreas[newArea]);
    }

    private void SetSceneCameraPlayer()
    {
        sceneCamera.GetComponent<CameraController>().SetPlayer(player);
        player.GetComponent<PlayerController>().SetMainCamera(sceneCamera.GetComponent<CameraController>());
    }

    private void SetPlayerGameController()
    {
        player.GetComponent<PlayerController>().SetGameController(this);
    }

    public bool CanPlayerExitScene()
    {
        return canExit;
    }
}
