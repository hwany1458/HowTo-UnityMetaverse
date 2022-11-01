using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //public Text score;
    // unity version�� �ö󰨿� ����, UI text�� ���� ���ٴ� TextMeshPro�� ����ϴ°� ���� ��
    public TextMeshProUGUI resourceText;

    //private int resource;
    [HideInInspector]
    public static int resource;

    // Start is called before the first frame update
    void Start()
    {
        // public���� �����ؼ� (����) GetComponent<>()�� �� �ʿ� ����
        //resourceText = GetComponent<TextMeshProUGUI>();
        resource = 0;
    }

    // Update is called once per frame
    void Update()
    {
        resourceText.text = "Score : " + resource.ToString();
    }

    public int GetResource()
    {
        return resource;
    }

    public static void AddResource(int addCount)
    {
        resource += addCount;
    }

    public static void AddResource()
    {
        resource++;
    }
}
