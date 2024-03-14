using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AltUiMonsterSelect : IAltUiRunner
{
    private TaskCompletionSource<Monster> _resolver;
    private List<Monster> _options;

    public void OnMonsterClicked(Monster monster)
    {
        if(_options.Contains(monster))
        {
            _resolver.SetResult(monster);
        }
    }

    public async Task<Monster> GetUserMonsterSelect(List<Monster> monsters)
    {
        _options = monsters;
        _resolver = new TaskCompletionSource<Monster>();
        Monster selectedOption = await _resolver.Task;
        return selectedOption;
    }
}