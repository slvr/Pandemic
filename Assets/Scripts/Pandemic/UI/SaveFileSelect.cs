using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveFileSelect : MonoBehaviour {

    public Dropdown fileOptions;
    public List<string> fileNames;

    private void Start(){
        setOptions(LoadJson.loadSaveFileNames().pe_saveFileNames);
    }

    public void setOptions(string[] options) {
        fileOptions.ClearOptions();
        for (int i = 0; i < options.Length; i++) {
            fileNames.Add(options[i]);
        }
        fileOptions.AddOptions(fileNames);
    }

    public string getSaveSelection() {
        return fileNames[fileOptions.value];
    }
}
