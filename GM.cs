using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GM : MonoBehaviour
{
    public Player player;
    public Enemy enemy;

    public Image screen;

    public GameObject Choice;
    public GameObject Inventory;

    public Slider HPslider;
    public TextMeshProUGUI MonsterHP;
    public Image eqitem;
    

    static GM instance = null;

    public static GM Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    void Awake()
    {
        instance = this;
        playState = State.Play;
        
    }
    void Start()
    {
        Choice.SetActive(false);

    }

    public void Enemytype()
    {
        HPslider.maxValue = enemy.maxHp;
        HPslider.value = enemy.hp;
        MonsterHP.text = enemy.hp + "/" + enemy.maxHp;
    }

    public void Enemybar()
    {
        HPslider.gameObject.SetActive(true);
    }

    public void Enemybarx()
    {
        HPslider.gameObject.SetActive(false);
    }
  
    public void EnemyAtk()
    {
        if (player.isAttack == true)
        {
            int A = Random.Range(0, 99);
            float X = 100 - player.acc * 3 - enemy.luk;
            if (enemy.hp > 0)
            {
                if (A >= 0 && A < X)
                {
                    EisHit = true;
                    PisHit = false;

                    screen.gameObject.SetActive(true);

                    enemy.enim.SetTrigger("Hit");
                    player.anim.SetTrigger("Hitted");
                    player.hp = player.hp - enemy.str;
                    HittedDamage();
                    Debug.Log("공격당함");
                    player.isAttack = false;
                    enemy.isAttack = true;
                    Choice.SetActive(true);
                }

                else
                {

                    EisHit = false;
                    PisHit = false;

                    enemy.enim.SetTrigger("Hit");
                    Debug.Log("공격회피");
                    NoHittedDamage();
                    player.isAttack = false;
                    enemy.isAttack = true;
                    Choice.SetActive(true);
                }
            }
            else
            {
                enemy.enim.SetTrigger("Die");
            }
        }
    }

    public void LevelUP()
    {

        if (player.isUp == false)
        {
            int random = Random.Range(0, 3);
            if (random == 0)
            {
                player.str = player.str + 1;
            }

            else if(random ==1)
            {
                player.spd = player.spd + 1;
            }

            else if (random == 2)
            {
                player.acc = player.acc + 1;
            }

            else if (random == 3)
            {
                player.luk = player.luk + 1;
            }
        }
    }

    public void DealtimeEnemy()
    {
        Invoke("EnemyAtk", 2f);
    }


    public GameObject hudDamageText;
    public GameObject hudHittedText;

    public void TakeDamage()
    {
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = new Vector2(enemy.x, enemy.y);
        hudText.GetComponent<FloatingText>().damage = player.str;
    }

    public void NoTakeDamage()
    {
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = new Vector2(enemy.x, enemy.y); 
        hudText.GetComponent<FloatingText>().damage = 0;
    }
    public void TakeDamage2()
    {
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = new Vector2(enemy.x, enemy.y);
        hudText.GetComponent<FloatingText>().damage = player.str*2;
    }

    public void TakeDamage3()
    {
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = new Vector2(enemy.x, enemy.y);
        hudText.GetComponent<FloatingText>().damage = player.str*3;
    }
    public void HittedDamage()
    {
        GameObject hudText = Instantiate(hudHittedText);
        hudText.transform.position = new Vector2(player.x, player.y);
        hudText.GetComponent<FloatingText>().damage = enemy.str;
    }

    public void NoHittedDamage()
    {
        GameObject hudText = Instantiate(hudHittedText); 
        hudText.transform.position = new Vector2(player.x, player.y); 
        hudText.GetComponent<FloatingText>().damage = 0; 
    }

    public bool PisHit;
    public bool Crit;
    public bool Critical;
    public bool EisHit;

    public void dieenemy()
    {
        if(enemy.hp <= 0)
        {
            enemy.enim.SetTrigger("Die");
           
            enemy.hp = 0;
            player.currentEXP++;
            setAtk();
        }
    }
    public void PlayerAtk1()
    {
        int A = Random.Range(0, 99);
        float X = 100 - enemy.acc * 3 - player.luk;
        if (player.isAttack == false)
        {
            if (A >= 0 && A < X)
            {
                PisHit = true;

                player.isAttack = true;
                player.anim.SetTrigger("Hit");
                player.Attack1();
                TakeDamage();
                enemy.enim.SetTrigger("Hitted");
                enemy.hp = enemy.hp - player.str;
                enemy.isAttack = false;
                Debug.Log("공격성공");
                Choice.SetActive(false);
                dieenemy();
                if (enemy.hp > 0)
                {
                    DealtimeEnemy();
                }
            }
            else
            {
                enemy.enim.SetTrigger("Miss");
                player.anim.SetTrigger("Hit");
                player.isAttack = true;
                enemy.isAttack = false;
                NoTakeDamage();
                Debug.Log("공격실패");
                
                Choice.SetActive(false);
                if (enemy.hp > 0)
                {
                    DealtimeEnemy();
                }
                
            }
        }
    }

    public void PlayerAtk2()
    {
        int A = Random.Range(0, 99);
        float X = 100 - enemy.acc * 6 - player.luk * 2;
        if (player.isAttack == false)
        {
            if (A >= 0 && A < X)
            {
                PisHit = true;
                Critical = false;
                Crit = true;

                enemy.isAttack = false;
                player.anim.SetTrigger("Hit");
                player.Attack2();
                enemy.enim.SetTrigger("Hitted");
                enemy.hp = enemy.hp - player.str * 2;
                TakeDamage2();
                player.isAttack = true;
                Debug.Log("공격성공");
                Choice.SetActive(false);
                dieenemy();
                if (enemy.hp >= 0)
                {
                    DealtimeEnemy();
                }
            }
            else
            {
                enemy.isAttack = false; 
                enemy.enim.SetTrigger("Miss");
                player.anim.SetTrigger("Hit");
                player.isAttack = true;
                NoTakeDamage();
                Debug.Log("공격실패");
                enemy.isAttack = false;
                Choice.SetActive(false);
                if (enemy.hp >= 0)
                {
                    DealtimeEnemy();
                }
            }
        }
    }

    public void PlayerAtk3()
    {
        int A = Random.Range(0, 99);
        float X = 100 - enemy.acc * 10 - player.luk*3;
        if (player.isAttack == false)
        {
            if (A >= 0 && A < X)
            {
                PisHit = true;
                
                Crit = false;
                Critical = true;
                
                enemy.isAttack = false;
                player.anim.SetTrigger("Hit");
                player.Attack3();
                enemy.enim.SetTrigger("Hitted");
                enemy.hp = enemy.hp - player.str * 3;
                TakeDamage3();
                player.isAttack = true;
                Debug.Log("공격성공");
                Choice.SetActive(false);
                dieenemy();
                if (enemy.hp >= 0)
                {
                    DealtimeEnemy();
                }
            }

            else
            {
                enemy.enim.SetTrigger("Miss");
                player.anim.SetTrigger("Hit");
                player.isAttack = true;
                NoTakeDamage();
                Debug.Log("공격실패");
                enemy.isAttack = false;
                Choice.SetActive(false);
                if (enemy.hp >= 0)
                {
                    DealtimeEnemy();
                }
            }
        }
    }

    
    public void firstAtk()
    {
        if (player.spd >= enemy.spd)
        {
            enemy.isAttack = true;
            Choice.SetActive(true);
            Debug.Log("선공");
        }

        else
        {
            player.isAttack = true;
            enemy.isAttack = false;
            DealtimeEnemy();
            Debug.Log("후공");
        }
    }

    public void setAtk()
    {

        
        Choice.SetActive(false);
        player.isAttack = false;
        enemy.isAttack = false;


        Debug.Log("전투종료");
    }

    

    public enum State
    {
        Title,
        Play,
        GUI,
        PauseGUI,
        subGUI,
    }
    public State playState;
    public GameObject pauseGUI;

    public void pauseGUIOpen()
    {
        
            playState = State.PauseGUI;
            Invoke("PopUp", 0.3f);
            Inventory.SetActive(false);
        
    }

    void PopUp()
    {
        pauseGUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PauseGUIClose()
    {
        pauseGUI.SetActive(false);
        playState = State.Play;
        Time.timeScale = 1f;

    }

    public void MoveHungry()
    {
        player.hungry = player.hungry - 1;
        if(player.hungry <= 0)
        {
            player.hungry = 0;
            player.hp = player.hp - 2;
        }
        else
        {
            player.hungry = player.hungry - 1;
        }
    }

    public GameObject floatingitemimage;

    int currentIem;
    public void RandomSofaItem()
    {
        int A = Random.Range(0, 99);
        if (A >= 0 && A <= 29)
        {
            currentIem = Random.Range(15, 19);
        }
        else if (A >= 30 && A <= 39)
        {
            currentIem = Random.Range(0, 8);
        }
        else
        {

        }
        ItemDataReader.Instance.getItemValue[currentIem]++;
        GameObject floatingitem = Instantiate(floatingitemimage);
        floatingitem.GetComponent<SpriteRenderer>().sprite = ItemDataReader.Instance.itemIcon[currentIem];
        floatingitem.transform.position = new Vector2(player.x, player.y);
    }

    public void RandomDrawerItem()
    {
        int A = Random.Range(0, 99);

        ItemDataReader.Instance.getItemValue[currentIem]++;
        GameObject floatingitem = Instantiate(floatingitemimage);
        floatingitem.GetComponent<SpriteRenderer>().sprite = ItemDataReader.Instance.itemIcon[currentIem];
        floatingitem.transform.position = new Vector2(player.x, player.y);

        if (A >= 0 && A <= 29)
        {
            currentIem = Random.Range(15, 19);
        }
        else if (A >= 30 && A <= 49)
        {
            currentIem = Random.Range(0, 8);
        }
        else
        {

        }
    }

    public void RandomBoxItem()
    {
        int A = Random.Range(0, 99);

        if (A >= 0 && A <= 49)
        {
            currentIem = Random.Range(15, 19);
        }
        else if (A >= 50 && A <= 69)
        {
            currentIem = Random.Range(21, 23);
        }
        else if (A >= 70 && A <= 79)
        {
            currentIem = Random.Range(0, 8);
        }
        else
        {

        }
        ItemDataReader.Instance.getItemValue[currentIem]++;
        GameObject floatingitem = Instantiate(floatingitemimage);
        floatingitem.GetComponent<SpriteRenderer>().sprite = ItemDataReader.Instance.itemIcon[currentIem];
        floatingitem.transform.position = new Vector2(player.x, player.y);
    }

    public void RandomRefrigeratorItem()
    {

        int A = Random.Range(0, 99);

        if (A >= 0 && A <= 69)
        {
            currentIem = Random.Range(0, 8);
        }
        else
        {

        }
        ItemDataReader.Instance.getItemValue[currentIem]++;
        GameObject floatingitem = Instantiate(floatingitemimage);
        floatingitem.GetComponent<SpriteRenderer>().sprite = ItemDataReader.Instance.itemIcon[currentIem];
        floatingitem.transform.position = new Vector2(player.x, player.y);
    }

    public void RandomMedicalBoxItem()
    {

        int A = Random.Range(0, 99);

        if (A >= 0 && A <= 69)
        {
            currentIem = Random.Range(12, 13);
        }
        else
        {

        }
        ItemDataReader.Instance.getItemValue[currentIem]++;
        GameObject floatingitem = Instantiate(floatingitemimage);
        floatingitem.GetComponent<SpriteRenderer>().sprite = ItemDataReader.Instance.itemIcon[currentIem];
        floatingitem.transform.position = new Vector2(player.x, player.y);
    }

    public void KeyItem()
    {

        int A = Random.Range(0, 99);

        if (A >= 0 && A <= 99)
        {
            currentIem = (20);
        }
        else
        {

        }
        ItemDataReader.Instance.getItemValue[currentIem]++;
        GameObject floatingitem = Instantiate(floatingitemimage);
        floatingitem.GetComponent<SpriteRenderer>().sprite = ItemDataReader.Instance.itemIcon[currentIem];
        floatingitem.transform.position = new Vector2(player.x, player.y);
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("01_Play");
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void Gameover()
    {
            if (player.hp <= 0)
        {
            
            Debug.Log("끝");
            player.anim.SetBool("Dead", true);
            if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("Dead") && player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Debug.Log("애니메이션 종료");
                SceneManager.LoadScene("02_GameOver");
            }
        }
            ;
    }
}
