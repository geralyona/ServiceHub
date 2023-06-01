using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServiceHub.Modules.Monitor.Localization
{
    [ExportMetadata("Culture", "en-US")]
    [Export(typeof(ResourceDictionary))]
    public partial class EnglishLanguage : ResourceDictionary
    {
    }
}
