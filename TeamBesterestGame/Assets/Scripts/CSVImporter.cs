using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

//Opens a CSV file, and copies its contents into a Dictionary for ease of access
public class CSVImporter {

    //Variables for rows, columns, data imported, and file name
    int rows;
    int cols;
    
    public Dictionary<string, Dictionary<string,string>> data;
    
    string path = "Assets/Resources/";
	string url;

	MonoBehaviour owner;
	WWW download;

	///<summary>
	///Imports data from given path
	///</summary>
	/// <param name="r">Rows, or lines, to read</param>
	/// <param name="c">Columns, to split lines into</param>
	/// <param name="p">Path to import from</param>
    public CSVImporter(int r, int c, string p) {
        data = new Dictionary<string, Dictionary<string, string>>();
        this.rows = r;
        this.cols = c;
        this.path += p;
        
        this.ImportData();
    }

	///<summary>
	///Imports data from given path
	///</summary>
	/// <param name="r">Rows, or lines, to read</param>
	/// <param name="c">Columns, to split lines into</param>
	/// <param name="p">Path to import from</param>
	/// <param name="url">Path to download from</param>
	/// <param name="owner">Script to attempt download</param>
	public CSVImporter(int r, int c, string p, string url) {
		data = new Dictionary<string, Dictionary<string, string>>();
		this.rows = r;
		this.cols = c;
		this.path += p;
		this.url = url;
		this.owner = owner;

		//this.ImportData();
		this.LoadData();
	}
    
    private void ImportData() {
        StreamReader reader = new StreamReader(path);
        string[] curLine;
        string[] variableNames = new string[rows-1];
        
		for (int y=0; y<rows; y++) {
            curLine = reader.ReadLine().Split(',');
			for (int x=0; x<cols; x++) {
                if (y == 0 && x > 0) {
                    variableNames[x-1] = curLine[x];
                } else if (y > 0) {
					if (x == 0)
						data.Add (curLine [x], new Dictionary<string, string> ());
					else 
                        data[curLine[0]].Add(variableNames[x-1], curLine[x]);
                }
            }
        }

		reader.Close ();
    }

	private void LoadData() {
		this.download = new WWW (url);

		while (!this.download.isDone) {
			if (this.download.error != null) {
				Debug.Log (this.download.error);
				return;
			}
		}
		
		StringReader reader = new StringReader (download.text);
		string[] curLine;
		string[] variableNames = new string[rows-1];

		for (int y=0; y<rows; y++) {
			curLine = reader.ReadLine().Split(',');
			for (int x=0; x<cols; x++) {
				if (y == 0 && x > 0) {
					variableNames[x-1] = curLine[x];
				} else if (y > 0) {
					if (x == 0)
						data.Add (curLine [x], new Dictionary<string, string> ());
					else 
						data[curLine[0]].Add(variableNames[x-1], curLine[x]);
				}
			}
		}
	}
}