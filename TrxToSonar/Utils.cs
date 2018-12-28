using System;
using System.Linq.Expressions;
using TrxToSonar.Model.Trx;

namespace TrxToSonar
{
    public static class Utils
    {
        public static long ToSonarDuration(string trxDuration)
        {
            if (TimeSpan.TryParse(trxDuration, out var result))
            {
                return (long)result.TotalMilliseconds;
            }
            else
            {
                return 0;
            }
        }
    }
}