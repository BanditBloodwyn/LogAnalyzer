using LogAnalyzer.Core.Repositories;

namespace LogAnalyzer.Models.Data.Entities;

public class Problem : ISavable
{
    public long Id { get; set; }

    public string Title { get; set; }

    public string Text { get; set; }

    public List<long> SolutionId { get; set; } = [];
}
