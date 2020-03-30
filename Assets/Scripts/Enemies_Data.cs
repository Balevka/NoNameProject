using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character", order = 51)]
public class Characters : ScriptableObject
{
    [SerializeField] private string name;
    public string Name{
    	get {return name;}
    	protected set {}
    }

    [SerializeField] private float health;
    public float Health{
    	get {return health;}
    	protected set {}
    }
    
    [SerializeField] private float damage;
    public float Damage{
    	get {return damage;}
    	protected set {}
    }
    
    [SerializeField] private float damageResistance;

    public float DamageResistance{
    	get {return damageResistance;}
    	protected set {}
    }
    
    [SerializeField] private float speed;
    public float Speed{
    	get {return speed;}
    	protected set {}
    }
    
    [SerializeField] private Animator animator;
    public Animator Animator{
    	get {return animator;}
    	protected set {}
    }
    
}
