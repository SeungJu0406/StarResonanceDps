using System.Collections.ObjectModel;
using StarResonanceDpsAnalysis.WPF.Models;

namespace StarResonanceDpsAnalysis.WPF.Services;

public interface ISkillTrackerService
{
    /// <summary>등록된 추적 스킬 목록</summary>
    ObservableCollection<SkillTrackerItem> TrackedSkills { get; }

    /// <summary>다음 스킬 대기 중 여부</summary>
    bool IsWaiting { get; }

    /// <summary>이름 추적 시작 - 다음 스킬 사용 시 자동 등록</summary>
    void StartWaiting();

    /// <summary>대기 취소</summary>
    void CancelWaiting();

    /// <summary>특정 스킬 삭제</summary>
    void RemoveSkill(long skillId);

    /// <summary>전체 초기화</summary>
    void Clear();
}
