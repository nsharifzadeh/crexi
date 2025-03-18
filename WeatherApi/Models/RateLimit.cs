using System.Collections.Concurrent;
namespace WeatherApi.Models
{
  public class RateLimit
  {
    private static readonly ConcurrentDictionary<string, List<DateTime>> _requestLogs = new();
    public int RequestLimit { get; set; }
    public TimeSpan TimeWindow { get; set; }
    public string Key { get; set; }

    public RateLimit(int requestLimit, TimeSpan timeWindow, string key)
    {
      Key = key;
      RequestLimit = requestLimit;
      TimeWindow = timeWindow;
    }
    public bool IsRequestValid()
    {
      if (_requestLogs.ContainsKey(Key))
      {
        _requestLogs[Key].Add(DateTime.Now);
        _requestLogs[Key].RemoveAll(r => r < DateTime.Now.AddMinutes(-TimeWindow.Minutes));
        if (_requestLogs[Key].Count > RequestLimit)
        {
          return false;
        }
      }
      else
        _requestLogs.AddOrUpdate(Key, new List<DateTime> { DateTime.Now }, (k, v) => { v.Add(DateTime.Now); return v; });
      
      // We have not reached the maximum number of requests
      return true;

    }
  }
}
