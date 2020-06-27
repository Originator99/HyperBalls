public interface ISkill {
	void UseSkill(System.Object data);
}

public enum SkillType {
	BUFF,
	BAD_BLOCK_DAMAGE,
	GOOD_BLOCK_DAMAGE
}
