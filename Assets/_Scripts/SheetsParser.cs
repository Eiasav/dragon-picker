using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Globalization;

public class SheetsParser : MonoBehaviour
{
    private List<float[]> data = new List<float[]>();
    private readonly string sheet = "https://sheets.googleapis.com/v4/spreadsheets/1WDVGYHi-jJTAS3BrRTzB0QS4D2kkjNlzkbwQUWTQOuc/values/A1%3AZ100?key=AIzaSyAFSsM6SqmKV4ke7UFnrBgG6wkhupD14I8";
    public List<float[]> Data => data;

    public IEnumerator ParseSheets()
    {
        UnityWebRequest curResp = UnityWebRequest.Get(sheet);
        yield return curResp.SendWebRequest();
        string rawResp = curResp.downloadHandler.text;
        var rawJSON = JSON.Parse(rawResp);

        foreach ( var item in rawJSON["values"])
        {
            var parse = JSON.Parse(item.ToString());
            var select = parse[0].AsStringList;
            var count = 0;
            var array = new float[3];

            foreach (var sel in select)
            {
                if (float.TryParse(sel, NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out _) & count <= 3)
                {
                    array[count] = float.Parse(sel, NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"));
                    count++;
                }

                if (count == 3) data.Add(array);
            }
        }
    }
}
