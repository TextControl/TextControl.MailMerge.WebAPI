/*-------------------------------------------------------------------------
** module:      TX Text Control Web API Sample   
** file:        MergeController.cs
** description:	This file contains the controller for the Merge Web API
**
** version:		TXTextControl 22.0
** author:      B. Meyer
**-----------------------------------------------------------------------*/
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Http;
using System.Xml;
using TXTextControl;
using TXTextControl.Cloud.Helper;

namespace tx_webapi_sample.Controllers
{
    /*---------------------------------------------------------------------
    ** MergeController class
    **-------------------------------------------------------------------*/
    public class MergeController : ApiController
    {
        /*-----------------------------------------------------------------
        ** Post method
        ** // POST api/merge?format=[PDF|PDFA|DOCX|DOC]
        **---------------------------------------------------------------*/
        public Results Post([FromBody]dynamic mailMergeRequestObject,
            [FromUri]string format)
        {
            // create a new Results object that contains the 
            // resulting document as a byte[]
            Results results = new Results();

            // helper object of type 'MailMergeRequestObject' to extract the
            // JSON data (template and data)
            MailMergeRequestObject mmroJSONResult = 
                (MailMergeRequestObject)JsonConvert.DeserializeObject(
                    Convert.ToString(mailMergeRequestObject), 
                    typeof(MailMergeRequestObject));

            // create a new ServerTextControl
            using (TXTextControl.ServerTextControl tx =
                new TXTextControl.ServerTextControl())
            {
                tx.Create();
                // create a new MailMerge instance
                using (TXTextControl.DocumentServer.MailMerge mm =
                    new TXTextControl.DocumentServer.MailMerge())
                {
                    mm.TextComponent = tx;
                    // load the template from the helper object
                    mm.LoadTemplateFromMemory(mmroJSONResult.Template,
                        TXTextControl.DocumentServer.FileFormat.InternalUnicodeFormat);

                    // create a new DataSet and the given XML data into it
                    DataSet ds = new DataSet();
                    XmlDocument doc =
                        JsonConvert.DeserializeXmlNode(mmroJSONResult.Data);
                    XmlNodeReader reader = new XmlNodeReader(doc);
                    ds.ReadXml(reader, XmlReadMode.Auto);

                    // merge the template with the DataSet
                    mm.Merge(ds.Tables[0]);

                    byte[] data;
                    BinaryStreamType streamType = BinaryStreamType.AdobePDF;

                    // define the export format based on the given Uri parameter
                    switch (format.ToUpper()) {
                        case "PDFA":
                            streamType = BinaryStreamType.AdobePDFA; break;
                        case "DOCX":
                            streamType = BinaryStreamType.WordprocessingML; break;
                        case "DOC":
                            streamType = BinaryStreamType.MSWord; break;
                    }

                    // save the document and fill the 'Results' object
                    mm.SaveDocumentToMemory(out data, streamType, null);
                    results.Document = data;
                }
            }

            // return the 'Results' object
            return results;
        }
    }
}
