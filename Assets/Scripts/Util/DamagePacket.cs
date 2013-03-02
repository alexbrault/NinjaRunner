using UnityEngine;
using System.Collections;

public class DamagePacket {
	public int Damage;
	public int Source;
	
	public DamagePacket(int dmg, int source) {
		Damage = dmg;
		Source = source;
	}
}
