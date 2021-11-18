using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text test;
    [SerializeField] GameObject fade;
    [SerializeField] GameObject winer;
    [SerializeField] GameObject loser;

    private bool finished;

    // Start is called before the first frame update
    void Start()
    {
        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!finished && GameObject.FindGameObjectsWithTag("Player").Length == 0){
            StartCoroutine(FadeOut());
            Debug.Log("tuningoff");
        }
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            winer.SetActive(true);
        }
        else
        {
            test.text = "Enemigos restantes: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
    }


    IEnumerator FadeOut()
    {
        finished = true;
        yield return new WaitForSeconds(1f);
        while (true)
        {
            Color objColor = fade.GetComponent<Image>().color;
            float fadeAmount = objColor.a + (0.6f * Time.deltaTime);

            fade.GetComponent<Image>().color = new Color(objColor.r, objColor.g, objColor.b, fadeAmount);
            Debug.Log(fadeAmount);
            if (fadeAmount > 2)
            {
                fade.GetComponent<Image>().color = new Color(objColor.r, objColor.g, objColor.b, 0f);
                loser.SetActive(true);
            }

            yield return null;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
