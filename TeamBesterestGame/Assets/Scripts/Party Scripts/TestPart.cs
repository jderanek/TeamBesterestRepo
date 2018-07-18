using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPart : BaseParty {
	public TestPart(GameObject[] heroes) : base(heroes) {
	}
	public TestPart(BaseHero[] heroes) : base(heroes) {
	}
}
