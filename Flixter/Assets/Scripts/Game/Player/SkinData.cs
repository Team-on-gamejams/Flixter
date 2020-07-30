using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinData : MonoBehaviour {
	public string ShipLevel;
	public SpriteRenderer bodySprite;

	public CoolShieldEffect shield;

	public GameObject simpleBulletPrefab;
	public GameObject[] bulletStartPos;

	public float shootSpeed = 0.2f;
	public int bulletDmg = 10;
	public int maxHealth = 10;
}
