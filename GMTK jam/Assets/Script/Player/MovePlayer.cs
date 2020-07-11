﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public GameManager gameManager;
    private void Start()
    {
        LeanTween.scale(gameObject, new Vector3(0.17f, 0.17f), 1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.rotateAround(gameObject, Vector3.forward, -720f, 1f);
        LeanTween.move(gameObject, gameObject.transform.position + new Vector3(3.5f, -1f, 0f), 1f);
        gameManager.BfadeOut(.7f);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += move * Time.deltaTime * moveSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            LeanTween.scale(gameObject, new Vector3(0f, 0f), .3f).setEase(LeanTweenType.easeOutBounce);
            StartCoroutine(wait(gameObject));
        }
        else if (collision.gameObject.tag == "Start")
        {
            gameManager.BfadeIn(.4f);
            LeanTween.scale(gameObject, new Vector3(0f, 0f), .6f).setEase(LeanTweenType.easeOutBounce);
            LeanTween.rotateAround(gameObject, Vector3.forward, -360f, .6f);
            LeanTween.move(gameObject, gameObject.transform.position + new Vector3(2f, 1f, 0f), .6f);
            StartCoroutine(wait(gameObject, true, 0));
        }
        else if (collision.gameObject.tag == "Base")
        {
            gameManager.BfadeIn(.4f);
            LeanTween.scale(gameObject, new Vector3(0f, 0f), .6f).setEase(LeanTweenType.easeOutBounce);
            LeanTween.rotateAround(gameObject, Vector3.forward, 360f, .6f);
            LeanTween.move(gameObject, gameObject.transform.position + new Vector3(-2f, 1f, 0f), .6f);
            StartCoroutine(wait(gameObject, true, 1));
        }
    }
    IEnumerator wait(GameObject g, bool Stay = true, int scene = 2)
    {
        yield return new WaitForSeconds(.6f);
        Destroy(g);
        if (!Stay)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + scene);
        }
    }
}