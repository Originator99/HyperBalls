public interface ISkill {
	Obstacle GetTarget();
	void UseSkill(System.Object data);
}

public enum SkillType {
	BUFF,
	UTILITY,
}
