using System.Collections.Generic;
using Runtime.Data.ValueObject;

namespace Runtime.Keys
{
    public class EnvironmentDataParams
    {
        public int Score;
        public int CityLevel;
        public Dictionary<int, AreaData> BuildDatas;
        public int CompletedArea;
    }
}