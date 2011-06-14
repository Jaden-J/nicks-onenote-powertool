using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using NicksPowerTool.ONReader;
using System.Xml.Serialization;

namespace NicksPowerTool.ONReader.PageNodes.PageNodeProperties
{
    [NodeName("Tag")]
    public class TagProperty : PageProperty
    {
        // <one:Tag index="1" completed="true" disabled="false" 
        //  creationDate="2011-06-07T14:43:43.000Z" completionDate="2011-06-07T14:43:43.000Z" />
        public TagProperty() : base()
        {
        }

        public bool Completed
        {
            get
            {
                return Attributes.GetAttributeValueBool("completed");
            }

            set
            {
                Attributes.AddOrChangeAttribute("completed", value.ToString());
            }
        }

        public bool Disabled
        {
            get
            {
                return Attributes.GetAttributeValueBool("disabled");
            }

            set
            {
                Attributes.AddOrChangeAttribute("disabled", value.ToString());
            }
        }

        public DateTime CompletionDate
        {
            get
            {
                return XmlConvert.ToDateTime(Attributes.GetAttributeValue("completionDate")).ToUniversalTime().ToString(;
            }

            set
            {
                XmlConvert.
            }
        }

        public TagProperty(PageNode parentNode)
            : base(parentNode)
        {
            Attributes.AddOrChangeAttribute("index", "0");
            Attributes.AddOrChangeAttribute("completed", "true");
            Attributes.AddOrChangeAttribute("disabled", "false");
        }
    }
}
