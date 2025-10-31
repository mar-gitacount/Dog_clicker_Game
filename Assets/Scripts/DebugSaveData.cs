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
}
