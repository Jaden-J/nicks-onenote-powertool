using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NicksPowerTool.ONReader.PageNodeAugmentation;

namespace NicksPowerTool.ONReader.PageNodes
{
    [NodeName("Image")]
    class ImageNode : PageElement, IHasBinaryData, IHasPageArea
    {
    }
}
