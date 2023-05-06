using System;
using System.IO;
using UnityEngine;

/// <summary>
/// This static class deals with data loading and saving implementations
/// </summary>
public static class Utils
{
    private static ShipDataCollection _shipCollectionData;

    private static readonly string PersistentSchemaPath = Application.streamingAssetsPath + "/schema";
    private static readonly string PersistentDataPath = Application.persistentDataPath + "/bin";

    public static void InitializeGameData()
    {
        if (_shipCollectionData == null)
        {
            LoadAllShipResources();
        }
    }

    /// <summary>
    /// get specific ship's spec data
    /// </summary>
    /// <returns></returns> 
    public static ShipSpec GetShipSpec(string shipId)
    {
        var shipSpec = _shipCollectionData.shipCollection.Find(c => c.sId.Equals(shipId));
        return shipSpec;
    }

    /// <summary>
    /// get current selected ship's spec data
    /// </summary>
    /// <returns></returns>
    public static ShipSpec GetCurrShipSpec()
    {
        InitializeGameData();
        var dataObject = _shipCollectionData.currShip;
        return dataObject;
    }

    /// <summary>
    /// update changes for current selected ship
    /// </summary>
    /// <param name="shipSpec"></param>
    public static void SetCurrShipSpec(ShipSpec shipSpec)
    {
        _shipCollectionData.currShip = shipSpec;

        try
        {
            var index = _shipCollectionData.shipCollection.FindIndex(c => c.sId.Equals(shipSpec.sId));
            _shipCollectionData.shipCollection[index] = _shipCollectionData.currShip;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            throw;
        }
    }

    private static void LoadFirstTime()
    {
        var schema = File.ReadAllText(PersistentSchemaPath);
        PlayerPrefs.SetString("ShipCollectionData", schema);
        File.WriteAllText(PersistentDataPath, schema);
    }

    /// <summary>
    /// load or generate default data for project
    /// </summary>
    private static void LoadAllShipResources()
    {
        var stackAvail = PlayerPrefs.HasKey("ShipCollectionData");
        if (stackAvail == false)
        {
            LoadFirstTime();
        }

        try
        {
            var dataJson = File.ReadAllText(PersistentDataPath);
            var load = JsonUtility.FromJson<ShipDataCollection>(dataJson);

            _shipCollectionData = load;
            PlayerPrefs.SetString("ShipCollectionData", dataJson);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            throw;
        }
    }

    /// <summary>
    /// save all ship selections triggered from checkpoints
    /// </summary>
    internal static void SaveAllShipConfigurations()
    {
        try
        {
            var dataJson = JsonUtility.ToJson(_shipCollectionData);
            File.WriteAllText(PersistentDataPath, dataJson);

            PlayerPrefs.SetString("ShipCollectionData", dataJson);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
