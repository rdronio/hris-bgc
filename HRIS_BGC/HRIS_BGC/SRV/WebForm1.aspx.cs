using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRV;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Common objCommon = new Common();
        DataTable dtsave = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUploader.HasFile)
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt = ReadCsvFile();
                    Session["FORDATABASE"] = dt; //RETRIEVE TO SAVE IN DATABASE
                    dgPayroll.DataSource = dt;
                    dgPayroll.DataBind();
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;
                }
            }
            else
            {
                Label1.Text = "You have not specified a file.";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            DataTable dtr = Session["FORDATABASE"] as DataTable;
            objCommon.SaveDataTable(dtr);
        }
        public DataTable ReadCsvFile()
        {

            DataTable dtCsv = new DataTable();
            string Fulltext;
            if (FileUploader.HasFile)
            {
                string FileSaveWithPath = Server.MapPath("\\Upload\\Import" + System.DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".csv");
                FileUploader.SaveAs(FileSaveWithPath);
                using (StreamReader sr = new StreamReader(FileSaveWithPath))
                {
                    while (!sr.EndOfStream)
                    {
                        Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                        string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                        for (int i = 0; i < rows.Count() - 1; i++)
                        {
                            string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                            {
                                if (i == 0)
                                {
                                    for (int j = 0; j < rowValues.Count(); j++)
                                    {
                                        dtCsv.Columns.Add(rowValues[j].Replace(" ","").Replace("\r","")); //add headers  
                                    }
                                }
                                else
                                {
                                    DataRow dr = dtCsv.NewRow();
                                    for (int k = 0; k < rowValues.Count(); k++)
                                    {
                                        dr[k] = rowValues[k].ToString();
                                    }
                                    dtCsv.Rows.Add(dr); //add other rows  
                                }
                            }
                        }
                    }
                }
            }
            return dtCsv;
        }
     
    }
}