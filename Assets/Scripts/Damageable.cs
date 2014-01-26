using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour
{
	[SerializeField] float initialHealth;
	[SerializeField] ObjectIdentifier objectIdentifier;
	[SerializeField] bool groundUnit = false;
	GameObject spawner;
	Vector3 correctedPos;
	public float InitialHealth { get { return initialHealth; }}
	public float Health { get; private set; }
	public bool Dead { get { return Health <= 0; }}
	
	void Start()
	{
		Health = InitialHealth;

		if (!groundUnit)
		{
			correctedPos = new Vector3(0f, -transform.root.position.y, 0f);
			transform.localPosition = correctedPos;
		}
	}

	public void AddHealth(float amount)
	{
		if (Dead)
			return;
		
		Health += amount;
		if (Health > InitialHealth)
			Health = InitialHealth;
	}
	
	public void ApplyDamage(float damage)
	{
		if (Dead)
			return;
		
		Health -= damage;
		if (Health <= 0f)
		{
			Health = 0f;
			Die();
		}
	}
	
	void Die()
	{
		switch(objectIdentifier.ObjectType)
		{
		case "Player Aircraft":
			spawner = GameObject.Find("Player Spawner");
			PlayerSpawner playerSpawner = (PlayerSpawner)spawner.GetComponent(typeof(PlayerSpawner));
			playerSpawner.PlayerDeath();
			Destroy(transform.root.gameObject);
			return;
		case "Enemy Aircraft Easy":
			spawner = GameObject.Find("Enemy Aircraft Easy Spawner");
			break;
		case "Enemy Aircraft Medium":
			spawner = GameObject.Find("Enemy Aircraft Medium Spawner");
			break;
		case "Enemy Aircraft Hard":
			spawner = GameObject.Find("Enemy Aircraft Hard Spawner");
			break;
		case "Enemy Turret":
			spawner = GameObject.Find("Enemy Turret Spawner");
			break;
		case "Enemy Missile Battery":
			spawner = GameObject.Find("Enemy Missile Battery Spawner");
			break;
		case "Ally Aircraft":
			spawner = GameObject.Find("Ally Aircraft Spawner");
			break;
		default:
			Debug.LogError("No Case Switch Defined: " + transform.root.name);
			break;
		}

		TargetSpawner spawnerEnemyID = (TargetSpawner)spawner.GetComponent(typeof(TargetSpawner));
		spawnerEnemyID.RemoveFromList(transform.root.name);
		Destroy(transform.root.gameObject);
	}
}