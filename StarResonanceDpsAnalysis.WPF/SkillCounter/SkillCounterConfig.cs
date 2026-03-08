namespace StarResonanceDpsAnalysis.WPF.SkillCounter;

/// <summary>
/// 스킬 카운터 전체 설정 (JSON으로 저장)
/// </summary>
public class SkillCounterConfig
{
    public List<SkillCounterAura> Auras { get; set; } = new();

    /// <summary>오버레이 창 위치</summary>
    public double WindowLeft { get; set; } = 200;
    public double WindowTop { get; set; } = 200;
}
