using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;//Yiyecek(Pizza, Sandwich, Steak ve Skull) listesi.
    public TextMeshProUGUI scoreText;//Skorun gözükeceği text.
    public TextMeshProUGUI gameOverText;//Oyun bittiğinde gözükecek text.
    public Button restartButton;//Restart butonu.
    public GameObject titleScreen;//Oyunun ana ekranı.
    public bool isGameActive;//Oyun aktif mi? Bool değişkeni.
    private int score;//Skor tutacak değişken.
    private float spawnRate = 2.0f;//2 saniyede 1 spawn edecek.
    private IEnumerator SpawnTarget()//Bu bir Coroutine'dir.
    {
     while(isGameActive)//Oyun aktif olduğu sürece
     {
      yield return new WaitForSeconds(spawnRate);// 2 saniyede bir fonksiyon dönecek.
      int index = Random.Range(0, targets.Count);//Random aralıkta bunu index'te tuttuk.
      //4 tane yiyecek var. Bunların Indeks numarası; Skull:0 Steak:1 Sandwich:2 Pizza:3
      Instantiate(targets[index]);//Yiyeceklerden indeks numarasına göre 1 tane üret dedik.
      // index'te rastgele 4 yiyecekten 1'ini tuttu. Onu da Instantiate ile random üretecek.
     }
    }
    public void UpdateScore(int scoreToAdd)//Eklenecek skoru tutması için parametre aldı.
    {
     score += scoreToAdd;//Target script'inde yiyeceklerin değerinin tutulduğu pointValue değişkeni var.
     //Örn Steak kesince 15 puan tutuyor. Onu buradaki scoreToAdd ile mevcut skora atıyoruz.
     //0 puanla başladık. 1 tane steak patlattık. 15 puanı pointValue ile tuttuk. o 15 puanı mevcut skora ekledik.
     //Mevcut skor 0 dı. score += scoreToAdd ile 15 puanı skora ekledik.
     scoreText.text = "Score: " + score;//Bunu bu şekilde skor text'ine yazdırdık.
    }
    public void GameOver()
    {
     gameOverText.gameObject.SetActive(true);//Game Over Text'ini aktif yap.
     isGameActive = false;//Oyun aktif mi? değişkenini Hayır yap.
     restartButton.gameObject.SetActive(true);//Restart butonunu aktif yap.
    }
    public void RestartGame()
    {
     SceneManager.LoadScene(SceneManager.GetActiveScene().name);//Oyun ekranını çağır.
    }
    public void StartGame(int difficulty)
    {
     isGameActive = true;//Oyun aktif mi? evet.
     StartCoroutine(SpawnTarget());//SpawnTarget Coroutine'ini başlat.
     score = 0;//Skor'u 0 ile başlat.
     UpdateScore(0);//UpdateScore fonksiyonunu sıfırladık.
     titleScreen.gameObject.SetActive(false);//Oyun başlangıç ekranını kapat.
     spawnRate /= difficulty;//Spawn rate'i difficulty'ye böl.
     //Spawn aralığı 2. zorluğu 1 yaparsak 2 sn'de 1, 10 yaparsak 0.2 saniyede 1 yiyecek verir.
     //Aralık ne kadar düşerse oyun haliyle o kadar zorlaşır. Buradan da zorluğu ayarlarız.
    }
}
