using LogAnalyzer.Core.EventBus;
using LogAnalyzer.Core.Extentions;
using LogAnalyzer.Models.Data.Entities;
using LogAnalyzer.Models.Events;

namespace LogAnalyzer.Models.Data.Caches;

public class ProblemCache
{
    private readonly List<Problem> _problems = [];

    public void AddProblem(Problem problem)
    {
        if (!_problems.Select(static p => p.Id).Contains(problem.Id))
        {
            _problems.Add(problem);
            EventBus<ProblemCacheUpdatedEvent>.Raise(new ProblemCacheUpdatedEvent());
        }
    }

    public void AddProblems(IEnumerable<Problem> problems)
    {
        if (_problems.Select(static p => p.Id).ContainsNone(problems.Select(static p => p.Id)))
        {
            if(!problems.Any())
                return;

            _problems.AddRange(problems);
            EventBus<ProblemCacheUpdatedEvent>.Raise(new ProblemCacheUpdatedEvent());
        }
    }

    public IEnumerable<Problem> GetAllProblems()
    {
        return _problems;
    }

    public long? GetNextProblemId()
    {
        if (_problems.Count == 0)
            return null;
        return _problems.Max(static problem => problem.Id) + 1;
    }
}