using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using SRV;
using System.Data;
using iTextSharp.text.html.simpleparser;

namespace HRIS_Basic
{
    public partial class CreatePDF : System.Web.UI.Page
    {
        Common objcommon = new Common();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string sql = "Select * from db_owner.Payroll";

            objcommon.LoadDataTable(sql, dt);

            Document pdoc = new Document(PageSize.A4, 20f, 20f, 30f, 30f);
            PdfWriter pWriter = PdfWriter.GetInstance(pdoc, new FileStream("C:\\Test\\CreatePDF.pdf", FileMode.Create));
            pdoc.Open();

            

            iTextSharp.text.Font pfont = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.TIMES_ROMAN.ToString(), 14, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font pfont2 = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.TIMES_ROMAN.ToString(), 12, iTextSharp.text.Font.BOLD);
            //1st paragraph
            Paragraph pgraph1 = new Paragraph("PAYSLIP", pfont);
            pdoc.Add(pgraph1);
            Paragraph pline = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            pdoc.Add(pline);

            //second paragraph
            string totalhourpay = dt.Rows[0]["TotalHourPay"].ToString();
            string totaltardiness = dt.Rows[0]["TotalTardinessPay"].ToString();
            string totalovertime = dt.Rows[0]["TotalOvertimePay"].ToString();
            string totalleave = dt.Rows[0]["Leave"].ToString();
            string SSS = dt.Rows[0]["SSS"].ToString();
            string pagibig = dt.Rows[0]["Pagibig"].ToString();
            string philhealth = dt.Rows[0]["Philhealth"].ToString();
            string totaldecutions = dt.Rows[0]["OtherDeductions"].ToString();
            string grosspay = dt.Rows[0]["GrossPay"].ToString();
            string netpay = dt.Rows[0]["NetPay"].ToString();


            Paragraph space = new Paragraph("");
            pdoc.Add(space);

            Paragraph ptotalhoursovertime = new Paragraph("Total Hours                      " + totalhourpay);
            ptotalhoursovertime.Alignment = Element.ALIGN_CENTER;
            pdoc.Add(ptotalhoursovertime);

            Paragraph ptotaltardinessleave = new Paragraph("Tardiness                        " + totaltardiness);
            ptotaltardinessleave.Alignment = Element.ALIGN_CENTER;
            pdoc.Add(ptotaltardinessleave);
            Paragraph pabsent = new Paragraph("Absent                        " + totalovertime);
            pabsent.Alignment = Element.ALIGN_CENTER;
            pdoc.Add(pabsent);
            pdoc.Add(pline);


            Paragraph pgross = new Paragraph("Gross Pay        " + grosspay, pfont2);
            pdoc.Add(pgross);
            Paragraph sss = new Paragraph("SSS/GSIS        " + totaltardiness);
            //Third Paragraph



            //
            pdoc.Close();
            System.Diagnostics.Process.Start("C:\\Test\\CreatePDF.pdf");

            
        }

        protected void btnCreateHTML_Click(object sender, EventArgs e)
        {
            string sql = "Select * from db_owner.Payroll";

            objcommon.LoadDataTable(sql, dt);

            //Data
            string totalhourpay = dt.Rows[0]["TotalHourPay"].ToString();
            string totaltardiness = dt.Rows[0]["TotalTardinessPay"].ToString();
            string totalovertime = dt.Rows[0]["TotalOvertimePay"].ToString();
            string totalleave = dt.Rows[0]["Leave"].ToString();
            string SSS = dt.Rows[0]["SSS"].ToString();
            string pagibig = dt.Rows[0]["Pagibig"].ToString();
            string philhealth = dt.Rows[0]["Philhealth"].ToString();
            string totaldecutions = dt.Rows[0]["OtherDeductions"].ToString();
            string grosspay = dt.Rows[0]["GrossPay"].ToString();
            string netpay = dt.Rows[0]["NetPay"].ToString();


            Document pdoc = new Document(PageSize.A4, 20f, 20f, 30f, 30f);
            PdfWriter pWriter = PdfWriter.GetInstance(pdoc, new FileStream("C:\\Test\\CreatePDF.pdf", FileMode.Create));
            pdoc.Open();

            // Read in the contents of the Receipt.htm file...
            string contents = File.ReadAllText(Server.MapPath("~/CreateHTMLPayslip.htm"));

            // Replace the placeholders with the user-specified text
            contents = contents.Replace("[TotalHours]", totalhourpay);
            contents = contents.Replace("[TotalTardiness]", totaltardiness);
            contents = contents.Replace("[GROSSPAY]", grosspay);

            // Step 4: Parse the HTML string into a collection of elements...
            var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(contents), null);

            // Enumerate the elements, adding each one to the Document...
            foreach (var htmlElement in parsedHtmlElements)
                pdoc.Add(htmlElement as IElement);

            pdoc.Close();
            System.Diagnostics.Process.Start("C:\\Test\\CreatePDF.pdf");
        }
    }
}