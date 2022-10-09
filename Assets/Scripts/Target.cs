using UnityEngine;
public class Target : MonoBehaviour
{
    private Rigidbody targetRb;//Yiyecek ve kuru kafanın rb'si.
    private GameManager gameManager;//GameManager scriptine erişmek için.
    public ParticleSystem explosionParticle;//Yiyeceklere tıkladığımızda patlama efekti için.
    private float minSpeed = 15;//Minimum hız.
    private float maxSpeed = 20;//Maksimum hız.
    private float maxTorque = 20;//Maksimum tork.
    private float xRange = 4;//Yiyeceklerin spawnlanacağı x ekseni aralığı.
    private float ySpawnPos = -6;//Y ekseninde spawnlanacağı yer.
    public int pointValue;//Her yiyeceğin puanı.Pizza 10.Sandwich 5. Biftek 15.Kuru kafa -20.
    private void Start()
    {
     targetRb = GetComponent<Rigidbody>();//Yiyeceklerin rb'sini atadık.
     gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
     //gameManager'a GameManager game objesinin GameManager komponentini atadık.
     targetRb.AddForce(RandomForce(), ForceMode.Impulse);
     //rb'ye RandomForce kadar güç uyguladık.
     targetRb.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(), ForceMode.Impulse);
     //rb'ye x, y ve z için Tork uyguladık.
     transform.position = RandomSpawnPos();//Random spawn pozisyonunu mevcut pozisyona atadık.
    }
    private Vector3 RandomForce()
    {
     return Vector3.up * Random.Range(minSpeed, maxSpeed);
     //Minimum 15 maksimum 20 gücünde yiyecekleri yukarı doğru fırlatacak.
    }
    private Vector3 RandomSpawnPos()
    {
     return new Vector3(Random.Range(-xRange,xRange), ySpawnPos);
     //Yiyecekler x'de -4 +4 aralığında, y'de -6'da olacak şekilde oluşacak.
    }
    private float RandomTorque()
    {
     return Random.Range(-maxTorque, maxTorque);
     //-20 +20 aralığında tork uygulanacak fonksiyon.
    }
    private void OnMouseDown()
    {
     if(gameManager.isGameActive)//Eğer GameManager scriptindeki isGameActive bool'u 1 ise;
     {
      Destroy(gameObject);//Fareyle Yiyeceğe tıklayınca Yiyeceği yok edecek.
      Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
      //Yiyeceğe tıklayınca patlayacak. Haliyle aynı açı ve pozisyonda patlama efekti Üretilmesi lazım.
      gameManager.UpdateScore(pointValue);
      //GameManager'daki UpdateScore fonksiyonuna tıklanan yiyeceğin değeri kadar skor ekle.
     }
    }
    private void OnTriggerEnter(Collider other)
    {
     Destroy(gameObject);//Collider diğer nesneye dokunduğunda yok edecek.
     if(!gameObject.CompareTag("Bad"))//Eğer objenin tag'ı Bad değilse
     {
      gameManager.GameOver();//Oyun kaybedilecek.
      //Ekranın altında sensör var. Yiyecekler sensöre düşene kadar patlatamadıysak becerememişizdir.
      //Haliyle oyun kaybedilir.
      //Fakat kuru kafa patlamamamız gereken bir şey. Haliyle o zaten düşmesini istediğimiz bir nesne.
      //Bu yüzden tag'ı Bad olan nesne düşse de oyun devam ediyor. Mantık bu.
     }
    }
}
