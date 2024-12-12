using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrSoft.Common.Config;

public class GoogleSheetsApiConfig
{
    public string ApplicationName { get; set; }
    public string SpreadsheetId { get; set; }
    public string SheetName { get; set; }
}
