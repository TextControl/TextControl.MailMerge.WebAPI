using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using TXTextControl.Cloud.Helper;

namespace WebAPI_Client
{
    public partial class index : System.Web.UI.Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            // create the helper object to pass the required parameters
            // as a JSON object
            MailMergeRequestObject requestObject = new MailMergeRequestObject();

            // load the data as XML
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("sample_db.xml"));

            // the Web API expects the data as a JSON object
            // 'JsonConvert' converts the XML to the required JSON format
            string jsonText = JsonConvert.SerializeXmlNode(doc);

            // fill the helper object with the data and the template
            requestObject.Data = jsonText;
            requestObject.Template = System.IO.File.ReadAllBytes(
                Server.MapPath("template.tx"));

            // new helper object of type 'Results' that keeps
            // the resulting document as a byte[]
            Results results;
            string sFormat = DropDownList1.SelectedValue;

            // create a new WebClient object
            using (var client = new WebClient())
            {
                // set the headers to send and accept JSON
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                // upload the helper object as a JSON string
                string result = client.UploadString(
                    "http://localhost:13012/api/Merge?format=" + sFormat,
                    "POST",
                    JsonConvert.SerializeObject(requestObject));

                // deserialize and convert the returned JSON object
                // to the helper object 'Results'
                results = (Results)JsonConvert.DeserializeObject(result, typeof(Results));
            }

            // save the resulting document as a file
            // and create a hyperlink that points to the new file
            System.IO.File.WriteAllBytes(
                Server.MapPath("results." + sFormat), results.Document);
            Response.Write("Download generated document <a href=\"results." + 
                sFormat.ToLower() + "\">here</a> (results." +
                sFormat.ToLower() + ").");
        }
    }
}