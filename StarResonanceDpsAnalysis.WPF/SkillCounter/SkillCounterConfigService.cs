using System.IO;
using System.Text.Json;

namespace StarResonanceDpsAnalysis.WPF.SkillCounter;

/// <summary>
/// 스킬 카운터 설정 저장/불러오기 서비스
/// </summary>
public class SkillCounterConfigService
{
    private static readonly string ConfigPath =
        Path.Combine(AppContext.BaseDirectory, "skill_counter.json");

    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public SkillCounterConfig Load()
    {
        if (!File.Exists(ConfigPath))
            return new SkillCounterConfig();

        try
        {
            var json = File.ReadAllText(ConfigPath);
            return JsonSerializer.Deserialize<SkillCounterConfig>(json, JsonOptions)
                   ?? new SkillCounterConfig();
        }
        catch
        {
            return new SkillCounterConfig();
        }
    }

    public async Task SaveAsync(SkillCounterConfig config)
    {
        var json = JsonSerializer.Serialize(config, JsonOptions);
        await File.WriteAllTextAsync(ConfigPath, json);
    }
}
