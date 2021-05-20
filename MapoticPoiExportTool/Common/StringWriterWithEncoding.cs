using System.IO;
using System.Text;

namespace MapoticPoiExportTool.Common
{
    public class StringWriterWithEncoding : StringWriter
    {
        private Encoding mEncoding;
        public StringWriterWithEncoding(Encoding encoding)
        {
            mEncoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return mEncoding; }
        }
    }
}
