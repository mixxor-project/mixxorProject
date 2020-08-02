using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader
{
	//string 앞에 @가 있으면, 해당 문자열 안의 Escape문을 무시하고 그 문자열 그대로 표기함.
	static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
	static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
	static char[] TRIM_CHARS = { '\"' };
	
	public static List<Dictionary<string, object>> Read(string file)
	{
		var list = new List<Dictionary<string, object>>();
		TextAsset data = Resources.Load (file) as TextAsset;
		
		var lines = Regex.Split (data.text, LINE_SPLIT_RE);
		
		if(lines.Length <= 1) return list;
		
		var header = Regex.Split(lines[0], SPLIT_RE);
		for(var i=1; i < lines.Length; i++) {
			
			var values = Regex.Split(lines[i], SPLIT_RE);
			if(values.Length == 0 ||values[0] == "") continue;
			
			var entry = new Dictionary<string, object>();
			for(var j=0; j < header.Length && j < values.Length; j++ ) {
				string value = values[j];

				// 받아온 값을 수정하는 부분
				value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
				value = value.Replace("<br>", "\n"); // 추가된 부분. 개행문자를 \n대신 <br>로 사용한다.
				value = value.Replace("`", ",");

				object finalvalue = value;
				int n;
				float f;
				if(int.TryParse(value, out n)) {
					finalvalue = n;
				} else if (float.TryParse(value, out f)) {
					finalvalue = f;
				}
				entry[header[j]] = finalvalue;
			}
			list.Add (entry);
		}
		return list;
	}
}
