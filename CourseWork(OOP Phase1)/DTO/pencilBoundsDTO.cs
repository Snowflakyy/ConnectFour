using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CourseWork_OOP_Phase1_.DTO
{
    public class pencilBoundsDTO
    {
        [XmlElement("PencilColour")]
        public Color pencilColour { get; set; }
        [XmlElement("Line Thickness")]
        public float LineThickness { get; set; }
    }
}
