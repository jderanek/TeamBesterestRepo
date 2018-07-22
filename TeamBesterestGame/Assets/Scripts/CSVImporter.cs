using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

//Opens a CSV file, and copies its contents into a Dictionary for ease of access
public class CSVImporter {
    
	//Dictionary of dictionaries where all data is saved
    public Dictionary<string, Dictionary<string,string>> data;
    
	//IMPORTANT: Appends this to the start of the given path, so users need only give the
	//name of the file. Change this to wherever you save your spreadsheets.
	//The path is also where the loaded will save back-up spreadsheets
    string path = "Assets/Resources/";

	//Url to download from
	string url;

	//Actual download
	WWW download;

	///<summary>
	///Imports data from given path
	///</summary>
	/// <param name="p">Path to import from</param>
    public CSVImporter(string p) {
        data = new Dictionary<string, Dictionary<string, string>>();
        this.path += p;
        
        this.ImportData();
    }

	///<summary>
	///Imports data from given path
	///</summary>
	/// <param name="p">Path to import from</param>
	/// <param name="url">Path to download from</param>
	public CSVImporter(string p, string url) {
		this.path += p;
		this.url = url;

		this.LoadData();
	}
    
	//Reads values from a CSV spreadsheet from the given file name
    private void ImportData() {
		//Resets dictionary from previous imports
		data = new Dictionary<string, Dictionary<string, string>>();
        StreamReader reader = new StreamReader(path);
        string[] curLine;
		List<String> variableNames = new List<string> ();
		String[] variables = null;
		int y = 0;
		int x = 0;

		//Reads all lines of the spreadsheet, and imports values into a dictionary
		Debug.Log("Loading data from: " + this.path);
		curLine = reader.ReadLine().Split(',');
		while (curLine != null) {
			foreach (string stat in curLine) {
				//Halts running once the first empty area is found
				if (stat == "")
					break;

				//Turns list of variable names into an array to speed up performance
				if (y > 0 && variables == null)
					variables = variableNames.ToArray ();

				if (y == 0 && x > 0) {
					variableNames.Add(stat);
				} else if (y > 0) {
					if (x == 0)
						data.Add (stat, new Dictionary<string, string> ());
					else {
						data [curLine [0]].Add (variables [x - 1], curLine [x]);
					}
				}
				x++;
			}
			x = 0;
			y++;

			//Breaks loops once end of stream is reached
			if (reader.Peek () == -1)
				break;

			curLine = reader.ReadLine().Split(',');
		}
		reader.Close ();
    }

	//Reads data from CSV spreadsheet with given url online
	private void LoadData() {
		this.download = new WWW (url);

		//Throws a debug message and loads back-up if download fails
		while (!this.download.isDone) {
			if (this.download.error != null) {
				Debug.Log (this.download.error);
				this.ImportData ();
				return;
			}
		}

		//Saves downloaded text to file at path
		File.WriteAllText(path, download.text);

		//Resets dictionary from previous loads
		data = new Dictionary<string, Dictionary<string, string>>();
		
		StringReader reader = new StringReader (download.text);
		string[] curLine;
		List<String> variableNames = new List<string> ();
		String[] variables = null;
		int y = 0;
		int x = 0;

		//Reads all lines of the spreadsheet, and imports values into a dictionary
		//Debug.Log("Loading data from: " + this.path);
		curLine = reader.ReadLine().Split(',');
		while (curLine != null) {
			foreach (string stat in curLine) {
				//Halts running once the first empty area is found
				if (stat == "")
					break;

				//Turns list of variable names into an array to speed up performance
				if (y > 0 && variables == null)
					variables = variableNames.ToArray ();

				if (y == 0 && x > 0) {
					variableNames.Add(stat);
				} else if (y > 0) {
					if (x == 0)
						data.Add (stat, new Dictionary<string, string> ());
					else {
						data [curLine [0]].Add (variables [x - 1], curLine [x]);
					}
				}
				x++;
			}
			x = 0;
			y++;

			//Breaks loops once end of stream is reached
			if (reader.Peek () == -1)
				break;
			
			curLine = reader.ReadLine().Split(',');
		}
		reader.Close ();
	}
}