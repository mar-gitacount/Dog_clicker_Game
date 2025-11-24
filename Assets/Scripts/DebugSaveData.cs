using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class DebugSaveData : ISaveData
{
 
    public void SaveMoney(BigInteger money)
    {

    }
    public void SaveDogCnt(int id , int cnt)
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public BigInteger LoadMoney()
    {
        return 10000000000;
    }
    public int LoadDogCnt(int id)
    {
        return 5;
    }
    public void UserName(string name)
    {
        
    }
    public string LoadUserName()
    {
        return "DebugUserName";
    }
    public void password(string password)
    {
        
    }
    public string LoadPassword()
    {
        return "DebugPassword";
    }
    // Task SaveToCloud();

    public void SaveStoryProgress(int storyIndex)
    {
        
        
    }
    public int LoadStoryProgress()
    {
        return 0;
    }
    public void SavenNow(int saveIndex,int saveLotId=0)
    {

    }
    public int LoadNow()
    {
        return 0;
    }
    public void SaveLotId(int saveIndex)
    {

    }
    public int LoadLotId(int saveIndex)
    {
        return 0;
    }
    public void SaveTime(int time,int slot=0)
    {

    }
    public string LoadTime(int slot=0)
    {
        return "0000/00/00 00:00:00";
    }    
    public SaveData JsonLoadFromLocal(int slot=0)
    {
        return new SaveData();
    }
    
    public SaveData JsonSaveToLocal(SaveData data,int slot=0)
    {
        return new SaveData();
    }
}
