using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXTextControl.Cloud.Helper
{
    public class MailMergeRequestObject
    {
        public string Data { get; set; }
        public byte[] Template { get; set; }

        public MailMergeRequestObject() { }
    }

    public class Results
    {
        public byte[] Document { get; set; }

        public Results()
        {

        }
    }

}
