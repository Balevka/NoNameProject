using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 52)]
public class Weapons : ScriptableObject
{
	[SerializeField] private string nameWeapon;
    public string NameWeapon{
    	get {return nameWeapon;}
    	protected set {}
    }

    [SerializeField] private float damage;
    public float Damage{
    	get {return damage;}
    	protected set {}
    }

    [SerializeField] private float damageResistanceBonus;
    public float DamageResistanceBonus{
    	get {return damageResistanceBonus;}
    	protected set {}
    }

    [SerializeField] private float damageBonus;
    public float DamageBonus{
    	get {return damageBonus;}
    	protected set {}
    }
    
    [SerializeField] private Sprite weaponSprite;
    public Sprite WeaponSprite{
    	get {return weaponSprite;}
    	protected set {}
    }
}
