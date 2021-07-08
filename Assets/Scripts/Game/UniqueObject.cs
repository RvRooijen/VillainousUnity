using System;
using Sirenix.OdinInspector;

public class UniqueObject : SerializedScriptableObject
{
    public Guid Guid = Guid.NewGuid();
}