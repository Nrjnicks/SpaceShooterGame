using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthable {
	float inflictingDamageAmount{get;}//how much damage will be inflicted on collsion
	System.Action<IHealthable> onHit{get; set;}//event callback on collision
}
