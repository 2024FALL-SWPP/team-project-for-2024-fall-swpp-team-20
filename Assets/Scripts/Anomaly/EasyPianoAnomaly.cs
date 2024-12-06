using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyPianoAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        ObjectStorage storage = map.GetComponent<ObjectStorage>();
        Piano piano = storage.pianoObject.GetComponent<Piano>();
        piano.inAnomaly = true;
    }
}
