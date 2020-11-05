using MoveFiles.Component.Helper;
using MoveFiles.Component.Model;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MoveFiles.Component
{
    public class ArgumentParser
    {
        public static async ValueTask<Criteria> Parse(string[] arguments)
        {
            Criteria criteria = null;
            IDictionary<string, string> keyValueArgPairs = new Dictionary<string, string>();

            for (int i = 0; i < 8; i = i + 2)
            {
                keyValueArgPairs.Add(arguments[i].TrimStart('-'), arguments[i + 1]);
            }

            criteria = new Criteria
            {
                FilterFor = keyValueArgPairs["toFilterFile"],
                FilterKind = ConvertEnumFilter(keyValueArgPairs["filterKind"]),
                Source = keyValueArgPairs["source"],
                Destination = keyValueArgPairs["destination"]
            };

            return criteria;
        }

        public static async ValueTask GeneralUsage()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("MoveFiles.Main.exe");
            stringBuilder.AppendLine("      -toFilterFile YourCSVFilePath");
            stringBuilder.AppendLine("      -filterKind YourSearchCriteria [equals | contains | !equals | !contains]");
            stringBuilder.AppendLine("      -source YourSourceFolderPath");
            stringBuilder.AppendLine("      -destination YourDestinationFolderPath");

            await Logger.GeneralLog(stringBuilder.ToString());
        }

        private static FilterCriteria ConvertEnumFilter(string filterKind)
        {
            if (filterKind.Equals("equals", StringComparison.OrdinalIgnoreCase))
            {
                return FilterCriteria.Equals;
            }
            else if (filterKind.Equals("contains", StringComparison.OrdinalIgnoreCase))
            {
                return FilterCriteria.Contains;
            }
            else if (filterKind.Equals("!equals", StringComparison.OrdinalIgnoreCase))
            {
                return FilterCriteria.NotEquals;
            }
            else if (filterKind.Equals("!contains", StringComparison.OrdinalIgnoreCase))
            {
                return FilterCriteria.NotContains;
            }

            return FilterCriteria.None;
        }
    }
}
