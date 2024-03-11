
using System;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncUtils
{
    public static async void ForgetAndLog(Task task)
    {
        try
        {
            await task;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }
    }
}
