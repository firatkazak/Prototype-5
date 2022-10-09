using UnityEngine;
using UnityEngine.UI;
public class DifficultyButton : MonoBehaviour
{
    private Button button;//Zorluğu ayarlayacağımız buton.
    private GameManager gameManager;//Hiyerarşi'deki Game Manager nesnesini tanımladık.
    public int difficulty;//Zorluk.
    private void Start()
    {
     button = GetComponent<Button>();//button'a Button komponentini atadık.
     button.onClick.AddListener(SetDifficulty);
     //Seçtiğimiz zorluğa göre oyun başlayacak ve konsolda o zorluk yazdırılacak.
     gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
     //Hiyerarşi'deki gameManager'i Game Manager nesnesine atadık.
    }
    private void SetDifficulty()
    {
     Debug.Log(gameObject.name + " was clicked");//"Tıklanan zorluk"(easy,hard,medium) was clicked yazacak.
     gameManager.StartGame(difficulty);
     //Burada GameManager script'indeki StartGame fonksiyonunu çağırdık. Orada yumurtlama oranı zamana bölünüyor.
     //Butonu easy(1),medium(2) ve hard(3) olarak ayarladık. easy saniyede 1 tane hard 3 tane spawnlıyor.
    }
}
