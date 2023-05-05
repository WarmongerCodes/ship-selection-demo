using System;
using System.IO;
using UnityEngine;

/// <summary>
/// This static class deals with data loading and saving implementations
/// </summary>
public static class Utils
{
    private static ShipDataCollection _shipCollectionData;
    
    public static void InitializeGameData()
    {
        if (_shipCollectionData == null) LoadAllShipResources();
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
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// load or generate default data for project
    /// </summary>
    private static void LoadAllShipResources()
    {
        try
        {
            var pathInternal = Application.dataPath + "/Resources/Saves/data";
            if (!File.Exists(pathInternal))
            {
                File.CreateText(pathInternal);
            }
            
            var post = PlayerPrefs.HasKey("ShipCollectionData") ? "Saves/data" : "def";
            var path = Application.dataPath + "/Resources/" + post;
            var dataJson = File.ReadAllText(path);
            var load = JsonUtility.FromJson<ShipDataCollection>(dataJson);

            _shipCollectionData = load;
            PlayerPrefs.SetString("ShipCollectionData", dataJson);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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
            var path = $"{Application.dataPath}/Resources/Saves/data";
            var dataJson = JsonUtility.ToJson(_shipCollectionData);
            File.WriteAllText(path, dataJson);

            PlayerPrefs.SetString("ShipCollectionData", dataJson);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
