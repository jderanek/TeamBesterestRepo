using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class CSVImporter {
    //Variables for rows, columns, data imported, and file name
    int rows;
    int cols;
    
    public Dictionary<string, Dictionary<string,string>> data;
    
    string path = "Assets/Resources/";
    
    public CSVImporter(int r, int c, string p) {
        data = new Dictionary<string, Dictionary<string, string>>();
        this.rows = r;
        this.cols = c;
        this.path += p;
        
        this.ImportData();
    }
    
    private void ImportData() {
        StreamReader reader = new StreamReader(path);
        string[] curLine;
        string[] variableNames = new string[rows-1];
        
        for (int y=0; y<cols; y++) {
            curLine = reader.ReadLine().Split(',');
            for (int x=0; x<rows; x++) {
                Debug.Log(curLine[x]);
                if (y == 0 && x > 0) {
                    variableNames[x-1] = curLine[x];
                } else if (y > 0) {
                    if (x == 0)
                        data.Add(curLine[x], new Dictionary<string, string>());
                    else 
                        data[curLine[0]].Add(variableNames[x-1], curLine[x]);
                }
            }
        }
    }
}