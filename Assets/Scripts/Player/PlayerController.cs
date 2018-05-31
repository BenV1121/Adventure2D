﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

interface IDamageable
{
    void TakeDamage(float damageDealt);
}

public class PlayerController : MonoBehaviour
{
    float speed = 5;
    float velocity;

    Vector2 move;

    public bool isMove = false;
    public bool isSprint = false;

    public bool canMove = true;
    public bool canDash;

    GameObject sword;
    GameObject arrow;

    public float maxHealth = 10;
    public float health;

    public Image hp;
    public Text healthAmountText;
    public Text maxHealthText;

    public Text emiAmount;
    public int  emiNumber;

    Emi emi;

    // Use this for initialization
    void Start()
    {
        sword = GameObject.Find("sword");
        arrow = GameObject.Find("ArrowPivot");

        health = maxHealth;
        hp = GameObject.FindGameObjectWithTag("HP").GetComponent<Image>();
        SetHealthAmount();
        
        emiNumber = 0;
        SetEmiAmount();

        emi = (Emi)AssetDatabase.LoadAssetAtPath("Assets/Prefab/Drops/1Emi.prefab", typeof(Emi));
    }
	
	// Update is called once per frame
	void Update ()
    {
        sword.SetActive(false);

        velocity = speed * Time.deltaTime;

        move.x = Input.GetAxis("Horizontal") * velocity;
        move.y = Input.GetAxis("Vertical") * velocity;

        if(canMove)
        { 
            // Player Sprinting
            if (Input.GetKey(KeyCode.LeftShift))
                isSprint = true;
            else
                isSprint = false;

            if(isSprint == true)
                transform.Translate(new Vector2(move.x * 1.5f, move.y * 1.5f));
            else
                transform.Translate(new Vector2(move.x, move.y));

            // Player Attacking
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Z))
                sword.SetActive(true);
            else
                sword.SetActive(false);

            //Player facing direction
            if (Input.GetAxisRaw("Vertical") > 0 && !isMove)
                arrow.transform.up = Vector3.up;
            else if (Input.GetAxisRaw("Horizontal") < 0 && !isMove)
                arrow.transform.up = Vector3.left;
            else if (Input.GetAxisRaw("Vertical") < 0 && !isMove)
                arrow.transform.up = Vector3.down;
            else if (Input.GetAxisRaw("Horizontal") > 0 && !isMove)
                arrow.transform.up = Vector3.right;

            // If the player is moving in a direction, it should stay in that direction if there are other buttons pressed
            if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
                isMove = true;
            else
                isMove = false;
        }

        hp.fillAmount = health / maxHealth;

        if(Input.GetKeyDown(KeyCode.F1))
        {
            health -= 1;
            SetHealthAmount();

            if (health <= 0)
                Death();
        }
    }

    public void SetHealthAmount()
    {
        if (health >= maxHealth)
            health = maxHealth;

        healthAmountText.text = health.ToString();
        maxHealthText.text = "/ " + maxHealth.ToString();
    }

    public void SetEmiAmount()
    {
        emiAmount.text = "x " + emiNumber.ToString();
    }

    public void TakeDamage(float damageDealt)
    {
        health -= damageDealt;
        SetHealthAmount();
    }

    public void Death()
    {
        Debug.Log("Die");
    }
}
