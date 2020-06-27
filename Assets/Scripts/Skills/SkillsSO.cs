using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillsData", menuName = "Skills/New List", order = 1)]
public class SkillsSO : ScriptableObject {
	public List<SkillData> skills;
}

[System.Serializable]
public class SkillData {
	public int Id;
	public string Name;
	public string Desc;
	public Sprite Icon;
	public SkillType SkillType;
	public int Cost;
}
