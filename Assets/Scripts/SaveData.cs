using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
  public class SaveData
    {
        public string money;
        public string username;
        public string password;
        public List<SheepCount> sheepCounts = new List<SheepCount>(); // Listを使用
        public int storyIndex;

        // セーブ番号
        public int saveIndex;
        public int saveTime;
    }

    [Serializable]
    public class SheepCount
    {
        public string Key;
        public int Value;
    }

