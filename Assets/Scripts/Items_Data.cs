using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 52)]
public class Items_Data : ScriptableObject
{
	[SerializeField] private string name;
    public string Name{
    	get {return name;}
    	protected set {}
    }

    [SerializeField] private float healthBonus;
    public float HealthBonus{
    	get {return healthBonus;}
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
