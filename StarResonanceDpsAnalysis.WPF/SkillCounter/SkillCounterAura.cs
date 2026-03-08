namespace StarResonanceDpsAnalysis.WPF.SkillCounter;

/// <summary>
/// 스킬 카운터 오라 정의 (저장되는 설정)
/// </summary>
public class SkillCounterAura
{
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>표시 이름</summary>
    public string Name { get; set; } = "새 카운터";

    /// <summary>카운트할 스킬 ID</summary>
    public long TriggerSkillId { get; set; }

    /// <summary>충전 완료까지 필요한 사용 횟수</summary>
    public int Threshold { get; set; } = 5;

    /// <summary>카운트를 리셋하는 스킬 ID (0 = 리셋 없음)</summary>
    public long ResetSkillId { get; set; }

    /// <summary>아이콘 표시 텍스트 (최대 2글자)</summary>
    public string IconText { get; set; } = "?";

    /// <summary>아이콘 배경 색상 (HEX)</summary>
    public string IconColor { get; set; } = "#1690F8";

    /// <summary>활성화 여부</summary>
    public bool IsEnabled { get; set; } = true;
}
