/*-------------------------------------------------------------------------
** module:      TXTextControl.Cloud.Helper
** file:        MailMergeRequestObject.cs
** description:	This file contains the helper data containers
**
** version:		TX Text Control 22.0
** author:      B. Meyer
**-----------------------------------------------------------------------*/
namespace TXTextControl.Cloud.Helper
{
    // the request object that will be encrypted as JSON
    public class MailMergeRequestObject
    {
        public string Data { get; set; }
        public byte[] Template { get; set; }

        public MailMergeRequestObject() { }
    }

    // the results contains only a byte[] with the document
    public class Results
    {
        public byte[] Document { get; set; }

        public Results() { }
    }
}