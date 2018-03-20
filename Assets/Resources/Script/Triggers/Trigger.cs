using UnityEngine;
using System.Collections;

public interface Trigger
{
	//start and end of the current move
	void apply (Vector3 start,Vector3 end, Board[] boards);

}

