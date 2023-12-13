using TMPro;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private int score;
    public int Score => score;
    private SheetsParser parser;

    private void Awake()
    {
        _text.text = $"Target : {score}";
        parser = GetComponent<SheetsParser>();
        StartCoroutine(parser.ParseSheets());
    }
}