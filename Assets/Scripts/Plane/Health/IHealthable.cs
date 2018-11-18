using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthable {
	float inflictingDamageAmount{get;}
	System.Action<IHealthable> onHit{get; set;}
}
