using UnityEngine.UI;
using UnityEngine;

namespace Jalaboy
{
    public class GameLogic : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public GameObject enemyPrefab;
        public Transform cannon;
        public Transform enemySpawnPosition;
        public Text pointsText;
        public Text lifesText;
        public AudioSource anticipation;
        public AudioSource soundtrack;
        public AudioSource fireSound;
        public Light gameLight;
        public int pointsToWin = 500;
        public float fireRate = 1f;
        private float nextFire = 0f;
        public float enemyRate = 5f;
        private float nextEnemy = 0f;        
        private float hardTimer = 28.8f;
        private float hardTime;
        private bool hardMode = false;
        public double startPoints = 0;
        public int startLifes = 5;
        private static double points;
        private static int lifes;
        private bool gameStart = false;
        private GameObject[] hud;
        private GameObject startUI;
        private GameObject gameOverUI;
        

        // Start is called before the first frame update
        void Start()
        {
            anticipation.Play();
            hud = GameObject.FindGameObjectsWithTag("UI");
            startUI = GameObject.FindGameObjectWithTag("UI START");
            gameOverUI = GameObject.FindGameObjectWithTag("UI GO");
            gameOverUI.SetActive(false);
            points = startPoints;
            lifes = startLifes;
        }

        // Update is called once per frame
        void Update()
        {
            if (gameStart)
            {
                if (hardMode == false && Time.time > hardTime)
                {
                    enemyRate = 0.5f;
                    gameLight.color = Color.red;
                    SpawnEnemy();
                    SpawnEnemy();
                    SpawnEnemy();
                    hardMode = true;
                }
                pointsText.text = points.ToString();
                lifesText.text = lifes.ToString();
                if (Time.time > nextEnemy)
                {
                    nextEnemy = Time.time + enemyRate;
                    SpawnEnemy();
                }
                if (lifes <= 0) { GameOver(); }
                if (points >= pointsToWin) { Completed(); }
            }
        }
        public void StartGame()
        {
            enemyRate = 5f;
            gameLight.color = Color.white;
            startUI.SetActive(false);
            gameOverUI.SetActive(false);
            foreach (GameObject ui in hud)
            {
                ui.SetActive(true);
            }
            points = startPoints;
            lifes = startLifes;
            anticipation.Stop();
            soundtrack.Play();
            hardMode = false;
            hardTime = hardTimer + Time.time;
            gameStart = true;
        }

        public void GameOver()
        {
            startUI.SetActive(true);
            gameOverUI.SetActive(true);
            gameOverUI.GetComponent<Text>().text = "Game Over\nPoints: " + points.ToString();
            startUI.GetComponentInChildren<Text>().text = "Restart";
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            soundtrack.Stop();
            anticipation.Play();
            gameStart = false;
        }

        public void Completed()
        {
            startUI.SetActive(true);
            gameOverUI.SetActive(true);
            gameOverUI.GetComponent<Text>().text = "Mission Complete!\nPoints: " + pointsToWin.ToString();
            startUI.GetComponentInChildren<Text>().text = "New Game";
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            soundtrack.Stop();
            anticipation.Play();
            gameStart = false;
        }

        public void Fire()
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                GameObject projectileObject = Instantiate(projectilePrefab, cannon.position + cannon.forward, cannon.rotation) as GameObject;
                projectileObject.transform.forward = cannon.transform.forward;
                fireSound.Play();
            }
        }

        public void SpawnEnemy()
        {
            GameObject enemyObject = Instantiate(enemyPrefab, enemySpawnPosition.position + enemySpawnPosition.forward, enemySpawnPosition.rotation) as GameObject;
            enemyObject.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), enemySpawnPosition.transform.position.y, enemySpawnPosition.transform.position.z);
            enemyObject.transform.forward = enemySpawnPosition.transform.forward;
        }

        public static void KillConfirmed(double distance)
        {
            points += distance;
        }

        public static void EnemyEscaped()
        {
            lifes--;
        }
    }

}
