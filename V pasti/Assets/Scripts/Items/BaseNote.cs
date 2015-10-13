using UnityEngine;
using System.Collections;

public class BaseNote : BaseItems
{
	private string noteText;

	public string NoteText
	{
		get{ return noteText;}
		set{ noteText = value;}
	}
}
