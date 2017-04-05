using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* VERSION: S17 WEEK 9 */
// REQUIRES TEXTMESHPRO! Download it from the asset store and make sure you drag a 
// textmeshpro text object into this script's 'tmp' property.

// Suggested use:
// Call startDialog() from another script, use isDialogDone() to check if you should
// show new dialog.
// 
// Suggested improvements:
// Indicator for when dialog is ready to advance or end
// Advance two lines at a time.
// Sounds, portraits.
// Control visibility of dialog box UI elements in the canvas.

public class Textdisplay : MonoBehaviour {

	// Use this for initialization

	void Start () {
	}


	public TMP_Text tmp;
	string currentDialog = "Hello, this is a message for everyone that will type out one character after another. It's long but will only show three lines at a time.";
	int mode = 3;

	// Update is called once per frame

	List<string> lineStrings;
	int currentLineIndex;
	void Update () {

		if (mode == 0) {

			// Set the TMP object to show all of the currentDialog string, so we can
			// calculate line lengths in order to show characters one at a time. 
			tmp.text = currentDialog;
			tmp.maxVisibleLines = 3;
			// Need to call this for GetTextInfo() to work.
			tmp.ForceMeshUpdate();
			mode = 1;

			// Use the lastCharacterIndex of each line to split up the source string into lines, 
			// in order to manage line display
			TMP_LineInfo[] lineInfoList = tmp.GetTextInfo(tmp.text).lineInfo;
			lineStrings = new List<string>();

			foreach (TMP_LineInfo lineInfo in lineInfoList) {
				int last = lineInfo.lastCharacterIndex;
				int first = lineInfo.firstCharacterIndex;

				// Not sure why but lineinfo has garbage at the end where first/last are zero, so stop in this case.
				if (first == 0 && last == 0) break;

				// sometimes line infos go past whats visible idk why
				if (first >= currentDialog.Length) break;

				lineStrings.Add(currentDialog.Substring(first,(last-first)+1));
			}

			// Keeps track of where to start pulling lines out of lineStrings
			currentLineIndex = 0;

			setLines(tmp,lineStrings,currentLineIndex,tmp.maxVisibleLines);

			tmp.maxVisibleCharacters = 0;

		} else if (mode == 1) {
			tmp.maxVisibleCharacters++;

			if (tmp.maxVisibleCharacters >= tmp.GetTextInfo(tmp.text).characterCount) {
				mode = 2;
			}

		} else if (mode == 2 && Input.GetButtonDown("Jump")) {
			// remove the first line.
			currentLineIndex ++;

			// If this is true then the last line of dialogue already finished, so exit (or reset in this case.)
			if (currentLineIndex > lineStrings.Count - tmp.maxVisibleLines) {
				mode = 3;
				tmp.text = "";

			} else {
				setLines(tmp,lineStrings,currentLineIndex,tmp.maxVisibleLines);
				tmp.maxVisibleCharacters = tmp.GetTextInfo(tmp.text).lineInfo[tmp.maxVisibleLines-2].lastCharacterIndex;
				mode = 1;
			}
		} else if (mode == 3) {
			// Do nothing, text is done for now. 
		}


	}

	public bool isDialogDone() {
		if (mode == 3)
			return true;
		return false;
 	}

	public void startDialog(string _dialog) {
		mode = 0;
		currentDialog = _dialog;
	}

	void setLines(TMP_Text _tmp, List<string> lines, int startLine, int maxLines) {
		_tmp.text = "";
		for (int i =0; i < maxLines; i++) {
			if (startLine + i >= lines.Count) break;
			_tmp.text += lines[startLine+i];
		}
	}
}
