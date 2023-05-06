using System;
using System.Collections.Generic;

/// <summary>
/// classes for ship data serialization and in game usages
/// </summary>

[Serializable]
public class ShipDataCollection
{
    public ShipSpec currShip;
    public List<ShipSpec> shipCollection;
}

[Serializable]
public class ShipSpec
{
    public string sId;
    public string sName;
    public ShipClass sClass;
    public ShipVar sVar;
}

public enum ShipClass
{
    Scout = 0,
    Destroyer = 1
}

public enum ShipVar
{
    Base = 0,
    Tier1 = 1,
    Tier2 = 2
}
