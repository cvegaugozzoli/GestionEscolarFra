using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using iTextSharp.text.pdf.qrcode;
using System.Diagnostics;

//import com.itextpdf.text.pdf.Barcode;
//import com.itextpdf.text.pdf.BarcodeEAN;


public partial class ChequeraBarraGrilla4 : System.Web.UI.Page
{
    GESTIONESCOLAR.Negocio.ChequeraImpimir ocnChequeraImpimir = new GESTIONESCOLAR.Negocio.ChequeraImpimir();

    protected void Page_Load(object sender, EventArgs e)
    {
        int PageIndex = 0;
        if (this.Session["ChequeraBarra.PageIndex"] == null)
        {
            Session.Add("ChequeraBarra.PageIndex", 0);
        }
        else
        {
            PageIndex = Convert.ToInt32(Session["ChequeraBarra.PageIndex"]);
        }



        DataTable dt = new DataTable();
        dt = ocnChequeraImpimir.InformeChequeraImprimir2();
        int i = 1;
        int j = 1;
        if (dt.Rows.Count > 0)
        {

            String PriVto;
            String SegVto;
            String TerVto;
            String PriImporte;
            String SegImporte;
            String TerImporte;

            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.A4, 1f, 1f, 1f, 1f);
            //Document doc = new Document(PageSize.LETTER);

            //doc.SetMargins(1f, 1f, 1f, 1f);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\Aplicativos\GestionEscolarFra\chequera.pdf", FileMode.Create));

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFontBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            Int32 c = 1;
            foreach (DataRow row in dt.Rows)// por cada fila..
            {
                PdfPTable tblPrueba = new PdfPTable(3) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 100 };
                tblPrueba.SetWidths(new float[] { 26f, 26f, 48f });

                // Configuramos el título de las columnas de la tabla
                PdfPCell clCol1 = new PdfPCell(new Phrase("INST. MADRE MERCEDES GUERRA", _standardFontBold));
                clCol1.BorderWidth = 0;

                PdfPCell clCol2 = new PdfPCell(new Phrase("INST. MADRE MERCEDES GUERRA", _standardFontBold));
                clCol2.BorderWidth = 0;

                PdfPCell clCol3 = new PdfPCell(new Phrase("INST. MADRE MERCEDES GUERRA", _standardFontBold));
                clCol3.BorderWidth = 0;


                // Añadimos las celdas a la tabla
                tblPrueba.AddCell(clCol1);
                tblPrueba.AddCell(clCol2);
                tblPrueba.AddCell(clCol3);

                //// Llenamos la tabla con información
                clCol1 = new PdfPCell(new Phrase("Hnos. Terc. Franciscanas de la caridad", _standardFont));
                clCol1.BorderWidth = 0;

                clCol2 = new PdfPCell(new Phrase("Hnos. Terc. Franciscanas de la caridad", _standardFont));
                clCol2.BorderWidth = 0;

                clCol3 = new PdfPCell(new Phrase("Hnos. Terc. Franciscanas de la caridad", _standardFont));
                clCol3.BorderWidth = 0;

                tblPrueba.AddCell(clCol1);
                tblPrueba.AddCell(clCol2);
                tblPrueba.AddCell(clCol3);


                //// Llenamos la tabla con información
                clCol1 = new PdfPCell(new Phrase("Incorporado a la enseñanza oficial", _standardFont));
                clCol1.BorderWidth = 0;

                clCol2 = new PdfPCell(new Phrase("Incorporado a la enseñanza oficial", _standardFont));
                clCol2.BorderWidth = 0;

                clCol3 = new PdfPCell(new Phrase("Incorporado a la enseñanza oficial", _standardFont));
                clCol3.BorderWidth = 0;

                tblPrueba.AddCell(clCol1);
                tblPrueba.AddCell(clCol2);
                tblPrueba.AddCell(clCol3);

                //// Llenamos la tabla con información
                clCol1 = new PdfPCell(new Phrase("CUIT: 3063012583-5 IVA EXENTO", _standardFont));
                clCol1.BorderWidth = 0;

                clCol2 = new PdfPCell(new Phrase("CUIT: 3063012583-5 IVA EXENTO", _standardFont));
                clCol2.BorderWidth = 0;

                clCol3 = new PdfPCell(new Phrase("CUIT: 3063012583-5 IVA EXENTO", _standardFont));
                clCol3.BorderWidth = 0;

                tblPrueba.AddCell(clCol1);
                tblPrueba.AddCell(clCol2);
                tblPrueba.AddCell(clCol3);


                //// Llenamos la tabla con información
                clCol1 = new PdfPCell(new Phrase(Convert.ToString(row["APELLIDOYNOMBRE"].ToString()), _standardFontBold));
                clCol1.BorderWidth = 0;

                clCol2 = new PdfPCell(new Phrase(Convert.ToString(row["APELLIDOYNOMBRE"].ToString()), _standardFontBold));
                clCol2.BorderWidth = 0;

                clCol3 = new PdfPCell(new Phrase(Convert.ToString(row["APELLIDOYNOMBRE"].ToString()), _standardFontBold));
                clCol3.BorderWidth = 0;

                tblPrueba.AddCell(clCol1);
                tblPrueba.AddCell(clCol2);
                tblPrueba.AddCell(clCol3);

                //// Llenamos la tabla con información
                clCol1 = new PdfPCell(new Phrase(Convert.ToString(row["CURSO"].ToString()), _standardFont));
                clCol1.BorderWidth = 0;

                clCol2 = new PdfPCell(new Phrase(Convert.ToString(row["CURSO"].ToString()), _standardFont));
                clCol2.BorderWidth = 0;

                clCol3 = new PdfPCell(new Phrase(Convert.ToString(row["CURSO"].ToString()), _standardFont));
                clCol3.BorderWidth = 0;

                tblPrueba.AddCell(clCol1);
                tblPrueba.AddCell(clCol2);
                tblPrueba.AddCell(clCol3);


                //// Llenamos la tabla con información
                clCol1 = new PdfPCell(new Phrase(Convert.ToString(row["CONCEPTO"].ToString()), _standardFont));
                clCol1.BorderWidth = 0;

                clCol2 = new PdfPCell(new Phrase(Convert.ToString(row["CONCEPTO"].ToString()), _standardFont));
                clCol2.BorderWidth = 0;

                clCol3 = new PdfPCell(new Phrase(Convert.ToString(row["CONCEPTO"].ToString()), _standardFont));
                clCol3.BorderWidth = 0;

                tblPrueba.AddCell(clCol1);
                tblPrueba.AddCell(clCol2);
                tblPrueba.AddCell(clCol3);

                //lblCodigoBarras.Text = Convert.ToString(row["BARRA"].ToString());
                //tblPrueba.AddCell(lblCodigoBarras.Text);
                //clCol3.BorderWidth = 0;
                //tblPrueba.AddCell(clCol3);

                clCol1 = new PdfPCell(new Phrase(" ", _standardFont));
                clCol1.Border = 0;
                clCol2 = new PdfPCell(new Phrase(" ", _standardFont));
                clCol2.Border = 0;
                clCol3 = new PdfPCell(new Phrase(" ", _standardFont));
                clCol3.Border = 0;
                tblPrueba.AddCell(clCol1);
                tblPrueba.AddCell(clCol2);
                tblPrueba.AddCell(clCol3);

                clCol1 = new PdfPCell(new Phrase("CONFORMACION DEL ARANCEL", _standardFontBold));
                clCol1.Border = 0;
                clCol2 = new PdfPCell(new Phrase("CONFORMACION DEL ARANCEL", _standardFontBold));
                clCol2.Border = 0;
                clCol3 = new PdfPCell(new Phrase("CONFORMACION DEL ARANCEL", _standardFontBold));
                clCol3.Border = 0;
                tblPrueba.AddCell(clCol1);
                tblPrueba.AddCell(clCol2);
                tblPrueba.AddCell(clCol3);

                PriVto = Convert.ToString(row["PRIVTO"].ToString());
                SegVto = Convert.ToString(row["SEGVTO"].ToString());
                TerVto = Convert.ToString(row["TERVTO"].ToString());
                PriImporte = Convert.ToString(row["PRIIMPORTE"].ToString());
                SegImporte = Convert.ToString(row["SEGIMPORTE"].ToString());
                TerImporte = Convert.ToString(row["IMPABIERTO"].ToString());

                doc.Add(tblPrueba);


                PdfPTable tmpTableDet = new PdfPTable(6) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 100 };
                tmpTableDet.SetWidths(new float[] { 18f, 8f, 18f, 8f, 30f, 18f });

                iTextSharp.text.Font _standardFontL = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFontLBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                DataTable dt1 = new DataTable();
                dt1 = ocnChequeraImpimir.InformeChequeraImprimirDetalle(Convert.ToInt32(row["CONCEPTOID"].ToString()));  //Convert.ToString
                if (dt1.Rows.Count > 0)
                {
                    int m = 1;
                
                    PdfPCell clColDet1 = new PdfPCell();
                    PdfPCell clColDet2 = new PdfPCell();
                    PdfPCell clColDet3 = new PdfPCell();
                    PdfPCell clColDet4 = new PdfPCell();
                    PdfPCell clColDet5 = new PdfPCell();
                    PdfPCell clColDet6 = new PdfPCell();

                    decimal Total = 0;

                    foreach (DataRow rowD in dt1.Rows)// por cada fila..
                    {
                        String cntid = Convert.ToString(rowD["cntId"].ToString()); // Tipo de Concepto 2 = Inscripción, 3 = Cuota
                        if (m <= 3)
                        {
                            clColDet1 = new PdfPCell(new Phrase(Convert.ToString(rowD["CodNombre"].ToString()), _standardFontL));
                            
                            clColDet1.Border = 0;
                            clColDet2 = new PdfPCell(new Phrase(Convert.ToString(rowD["ConImporte"].ToString()), _standardFontL));
                            clColDet2.Border = 0;
                            clColDet3 = new PdfPCell(new Phrase(Convert.ToString(rowD["CodNombre"].ToString()), _standardFontL));
                            clColDet3.Border = 0;
                            clColDet4 = new PdfPCell(new Phrase(Convert.ToString(rowD["ConImporte"].ToString()), _standardFontL));
                            clColDet4.Border = 0;
                            clColDet5 = new PdfPCell(new Phrase(Convert.ToString(rowD["CodNombre"].ToString()), _standardFontL));
                            clColDet5.Border = 0;
                            clColDet6 = new PdfPCell(new Phrase(Convert.ToString(rowD["ConImporte"].ToString()), _standardFontL));
                            clColDet6.Border = 0;

                            tmpTableDet.AddCell(clColDet1);
                            tmpTableDet.AddCell(clColDet2);
                            tmpTableDet.AddCell(clColDet3);
                            tmpTableDet.AddCell(clColDet4);
                            tmpTableDet.AddCell(clColDet5);
                            tmpTableDet.AddCell(clColDet6);

                            Total = Total + Convert.ToDecimal(rowD["ConImporte"].ToString());

                            if (cntid == "2")  // Inscripción =  2 son 4 filas, Cuota = 3 son 3 filas
                            {
                                clColDet1 = new PdfPCell(new Phrase(Convert.ToString(rowD["CodNombre"].ToString()), _standardFontL));
                                clColDet1.Border = 0;
                                clColDet2 = new PdfPCell(new Phrase(Convert.ToString(rowD["ConImporte"].ToString()), _standardFontL));
                                clColDet2.Border = 0;
                                clColDet3 = new PdfPCell(new Phrase(Convert.ToString(rowD["CodNombre"].ToString()), _standardFontL));
                                clColDet3.Border = 0;
                                clColDet4 = new PdfPCell(new Phrase(Convert.ToString(rowD["ConImporte"].ToString()), _standardFontL));
                                clColDet4.Border = 0;
                                clColDet5 = new PdfPCell(new Phrase(Convert.ToString(rowD["CodNombre"].ToString()), _standardFontL));
                                clColDet5.Border = 0;
                                clColDet6 = new PdfPCell(new Phrase(Convert.ToString(rowD["ConImporte"].ToString()), _standardFontL));
                                clColDet6.Border = 0;

                                tmpTableDet.AddCell(clColDet1);
                                tmpTableDet.AddCell(clColDet2);
                                tmpTableDet.AddCell(clColDet3);
                                tmpTableDet.AddCell(clColDet4);
                                tmpTableDet.AddCell(clColDet5);
                                tmpTableDet.AddCell(clColDet6);
                            }

                        }
                        m = +1;

                    }

                    clColDet1 = new PdfPCell(new Phrase("TOTAL", _standardFontL));
                    clColDet1.Border = 0;
                    clColDet2 = new PdfPCell(new Phrase(Convert.ToString(Total.ToString()), _standardFontL));
                    clColDet2.Border = 0;
                    clColDet3 = new PdfPCell(new Phrase(Convert.ToString("TOTAL".ToString()), _standardFontL));
                    clColDet3.Border = 0;
                    clColDet4 = new PdfPCell(new Phrase(Convert.ToString(Total.ToString()), _standardFontL));
                    clColDet4.Border = 0;
                    clColDet5 = new PdfPCell(new Phrase(Convert.ToString("TOTAL".ToString()), _standardFontL));
                    clColDet5.Border = 0;
                    clColDet6 = new PdfPCell(new Phrase(Convert.ToString(Total.ToString()), _standardFontL));
                    clColDet6.Border = 0;

                    tmpTableDet.AddCell(clColDet1);
                    tmpTableDet.AddCell(clColDet2);
                    tmpTableDet.AddCell(clColDet3);
                    tmpTableDet.AddCell(clColDet4);
                    tmpTableDet.AddCell(clColDet5);
                    tmpTableDet.AddCell(clColDet6);


                    clColDet1 = new PdfPCell(new Phrase(" ", _standardFontL));
                    clColDet1.Border = 0;
                    clColDet2 = new PdfPCell(new Phrase(" ", _standardFontL));
                    clColDet2.Border = 0;
                    clColDet3 = new PdfPCell(new Phrase(" ", _standardFontL));
                    clColDet3.Border = 0;
                    clColDet4 = new PdfPCell(new Phrase(" ", _standardFontL));
                    clColDet4.Border = 0;
                    clColDet5 = new PdfPCell(new Phrase(" ", _standardFontL));
                    clColDet5.Border = 0;
                    clColDet6 = new PdfPCell(new Phrase(" ", _standardFontL));
                    clColDet6.Border = 0;

                    tmpTableDet.AddCell(clColDet1);
                    tmpTableDet.AddCell(clColDet2);
                    tmpTableDet.AddCell(clColDet3);
                    tmpTableDet.AddCell(clColDet4);
                    tmpTableDet.AddCell(clColDet5);
                    tmpTableDet.AddCell(clColDet6);

                    clColDet1 = new PdfPCell(new Phrase(" ", _standardFontL));
                    clColDet1.Border = 0;
                    clColDet2 = new PdfPCell(new Phrase(" ", _standardFontL));
                    clColDet2.Border = 0;
                    clColDet3 = new PdfPCell(new Phrase(" ", _standardFontL));
                    clColDet3.Border = 0;
                    clColDet4 = new PdfPCell(new Phrase(" ", _standardFontL));
                    clColDet4.Border = 0;
                    clColDet5 = new PdfPCell(new Phrase(" ", _standardFontL));
                    clColDet5.Border = 0;
                    clColDet6 = new PdfPCell(new Phrase(" ", _standardFontL));
                    clColDet6.Border = 0;

                    tmpTableDet.AddCell(clColDet1);
                    tmpTableDet.AddCell(clColDet2);
                    tmpTableDet.AddCell(clColDet3);
                    tmpTableDet.AddCell(clColDet4);
                    tmpTableDet.AddCell(clColDet5);
                    tmpTableDet.AddCell(clColDet6);

                    doc.Add(tmpTableDet);
                }


                PdfPTable tmpTableVtos = new PdfPTable(9) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 100 };
                tmpTableVtos.SetWidths(new float[] { 8f, 10f, 8f, 8f, 10f, 8f, 10f, 20f, 18f });

                //iTextSharp.text.Font _standardFontL = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                PdfPCell clColV1 = new PdfPCell();
                PdfPCell clColV2 = new PdfPCell();
                PdfPCell clColV3 = new PdfPCell();
                PdfPCell clColV4 = new PdfPCell();
                PdfPCell clColV5 = new PdfPCell();
                PdfPCell clColV6 = new PdfPCell();
                PdfPCell clColV7 = new PdfPCell();
                PdfPCell clColV8 = new PdfPCell();
                PdfPCell clColV9 = new PdfPCell();

                clColV1 = new PdfPCell(new Phrase("VTOS.:", _standardFontLBold));
                clColV1.Border = 0;
                clColV2 = new PdfPCell(new Phrase(PriVto, _standardFontLBold));
                clColV2.Border = 0;
                clColV3 = new PdfPCell(new Phrase(PriImporte, _standardFontLBold));
                clColV3.Border = 0;
                clColV4 = new PdfPCell(new Phrase("VTOS.:", _standardFontLBold));
                clColV4.Border = 0;
                clColV5 = new PdfPCell(new Phrase(PriVto, _standardFontLBold));
                clColV5.Border = 0;
                clColV6 = new PdfPCell(new Phrase(PriImporte, _standardFontLBold));
                clColV6.Border = 0;
                clColV7 = new PdfPCell(new Phrase(Convert.ToString("VTOS.:".ToString()), _standardFontLBold));
                clColV7.Border = 0;
                clColV8 = new PdfPCell(new Phrase(PriVto, _standardFontLBold));
                clColV8.Border = 0;
                clColV9 = new PdfPCell(new Phrase(PriImporte, _standardFontLBold));
                clColV9.Border = 0;

                tmpTableVtos.AddCell(clColV1);
                tmpTableVtos.AddCell(clColV2);
                tmpTableVtos.AddCell(clColV3);
                tmpTableVtos.AddCell(clColV4);
                tmpTableVtos.AddCell(clColV5);
                tmpTableVtos.AddCell(clColV6);
                tmpTableVtos.AddCell(clColV7);
                tmpTableVtos.AddCell(clColV8);
                tmpTableVtos.AddCell(clColV9);

                clColV1 = new PdfPCell(new Phrase("", _standardFontLBold));
                clColV1.Border = 0;
                clColV2 = new PdfPCell(new Phrase(SegVto, _standardFontLBold));
                clColV2.Border = 0;
                clColV3 = new PdfPCell(new Phrase(SegImporte, _standardFontLBold));
                clColV3.Border = 0;
                clColV4 = new PdfPCell(new Phrase("", _standardFontLBold));
                clColV4.Border = 0;
                clColV5 = new PdfPCell(new Phrase(SegVto, _standardFontLBold));
                clColV5.Border = 0;
                clColV6 = new PdfPCell(new Phrase(SegImporte, _standardFontLBold));
                clColV6.Border = 0;
                clColV7 = new PdfPCell(new Phrase(Convert.ToString("".ToString()), _standardFontLBold));
                clColV7.Border = 0;
                clColV8 = new PdfPCell(new Phrase(SegVto, _standardFontLBold));
                clColV8.Border = 0;
                clColV9 = new PdfPCell(new Phrase(SegImporte, _standardFontLBold));
                clColV9.Border = 0;

                tmpTableVtos.AddCell(clColV1);
                tmpTableVtos.AddCell(clColV2);
                tmpTableVtos.AddCell(clColV3);
                tmpTableVtos.AddCell(clColV4);
                tmpTableVtos.AddCell(clColV5);
                tmpTableVtos.AddCell(clColV6);
                tmpTableVtos.AddCell(clColV7);
                tmpTableVtos.AddCell(clColV8);
                tmpTableVtos.AddCell(clColV9);

                clColV1 = new PdfPCell(new Phrase("", _standardFontLBold));
                clColV1.Border = 0;
                clColV2 = new PdfPCell(new Phrase(TerVto, _standardFontLBold));
                clColV2.Border = 0;
                clColV3 = new PdfPCell(new Phrase(TerImporte, _standardFontLBold));
                clColV3.Border = 0;
                clColV4 = new PdfPCell(new Phrase("", _standardFontLBold));
                clColV4.Border = 0;
                clColV5 = new PdfPCell(new Phrase(TerVto, _standardFontLBold));
                clColV5.Border = 0;
                clColV6 = new PdfPCell(new Phrase(TerImporte, _standardFontLBold));
                clColV6.Border = 0;
                clColV7 = new PdfPCell(new Phrase(Convert.ToString("".ToString()), _standardFontLBold));
                clColV7.Border = 0;
                clColV8 = new PdfPCell(new Phrase(TerVto, _standardFontLBold));
                clColV8.Border = 0;
                clColV9 = new PdfPCell(new Phrase(TerImporte, _standardFontLBold));
                clColV9.Border = 0;

                tmpTableVtos.AddCell(clColV1);
                tmpTableVtos.AddCell(clColV2);
                tmpTableVtos.AddCell(clColV3);
                tmpTableVtos.AddCell(clColV4);
                tmpTableVtos.AddCell(clColV5);
                tmpTableVtos.AddCell(clColV6);
                tmpTableVtos.AddCell(clColV7);
                tmpTableVtos.AddCell(clColV8);
                tmpTableVtos.AddCell(clColV9);


                clColV1 = new PdfPCell(new Phrase(" ", _standardFont));
                clColV1.Border = 0;
                clColV2 = new PdfPCell(new Phrase(" ", _standardFontL));
                clColV2.Border = 0;
                clColV3 = new PdfPCell(new Phrase(" ", _standardFontL));
                clColV3.Border = 0;
                clColV4 = new PdfPCell(new Phrase(" ", _standardFontL));
                clColV4.Border = 0;
                clColV5 = new PdfPCell(new Phrase(" ", _standardFontL));
                clColV5.Border = 0;
                clColV6 = new PdfPCell(new Phrase(" ", _standardFontL));
                clColV6.Border = 0;
                clColV7 = new PdfPCell(new Phrase(" ", _standardFontL));
                clColV7.Border = 0;
                clColV8 = new PdfPCell(new Phrase(" ", _standardFontL));
                clColV8.Border = 0;
                clColV9 = new PdfPCell(new Phrase(" ", _standardFontL));
                clColV9.Border = 0;

                tmpTableVtos.AddCell(clColV1);
                tmpTableVtos.AddCell(clColV2);
                tmpTableVtos.AddCell(clColV3);
                tmpTableVtos.AddCell(clColV4);
                tmpTableVtos.AddCell(clColV5);
                tmpTableVtos.AddCell(clColV6);
                tmpTableVtos.AddCell(clColV7);
                tmpTableVtos.AddCell(clColV8);
                tmpTableVtos.AddCell(clColV9);

                doc.Add(tmpTableVtos);


                Barcode128 code128 = new Barcode128();
                code128.CodeType = Barcode.CODE128_RAW;
                code128.ChecksumText = true;
                code128.GenerateChecksum = true;
                code128.Code = Barcode128.GetRawText(Convert.ToString(row["BARRA"].ToString()), false, Barcode128.Barcode128CodeSet.AUTO);
                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White));
                iTextSharp.text.Image barCode = iTextSharp.text.Image.GetInstance(bm, System.Drawing.Imaging.ImageFormat.Png);

                // PdfPTable tmpTable = new PdfPTable(1); // Comentado 20/01/2022

                // PdfPCell tmpCell = new PdfPCell(barCode); // Comentado 20/01/2022

                PdfPTable tmpTableCB = new PdfPTable(4) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 100 };
                tmpTableCB.SetWidths(new float[] { 26f, 26f, 12f, 36f });

                PdfPCell tmpCellCB1 = new PdfPCell(new Phrase("", _standardFont));
                PdfPCell tmpCellCB2 = new PdfPCell(new Phrase("", _standardFont));
                PdfPCell tmpCellCB3 = new PdfPCell(new Phrase("", _standardFont));
                PdfPCell tmpCellCB4 = new PdfPCell(new Phrase(Convert.ToString(row["BARRA"].ToString()), _standardFont));
                tmpCellCB4.HorizontalAlignment = Element.ALIGN_CENTER;  
                tmpCellCB4.VerticalAlignment = Element.ALIGN_BOTTOM;  
                tmpCellCB1.BorderWidth = 0;
                tmpCellCB2.BorderWidth = 0;
                tmpCellCB3.BorderWidth = 0;
                tmpCellCB4.BorderWidth = 0;
                tmpTableCB.AddCell(tmpCellCB1);
                tmpTableCB.AddCell(tmpCellCB2);
                tmpTableCB.AddCell(tmpCellCB3);
                tmpTableCB.AddCell(tmpCellCB4);

                tmpCellCB1 = new PdfPCell(new Phrase("Talon Tutor", _standardFont)); 
                tmpCellCB2 = new PdfPCell(new Phrase("Talon Banco", _standardFont));
                tmpCellCB3 = new PdfPCell(new Phrase("Talon Colegio", _standardFont));                
                tmpCellCB4 = new PdfPCell(barCode);

                //tmpTable.AddCell(new Paragraph(row.Cells[12].Value.ToString().ToUpper(), calibri));
                barCode.ScaleAbsolute(150, 30); //100x40  //Original = 60, 20

                tmpCellCB1.BorderWidth = 0;
                tmpCellCB2.BorderWidth = 0;
                tmpCellCB3.BorderWidth = 0;

                tmpCellCB4.FixedHeight = 45;//60  Original = 30  // Comentado 20/01/2022
                tmpCellCB4.HorizontalAlignment = Element.ALIGN_CENTER;  // Comentado 20/01/2022
                tmpCellCB4.VerticalAlignment = Element.ALIGN_TOP;  // Comentado 20/01/2022
                tmpCellCB4.BorderWidth = 0;  // Comentado 20/01/2022

                //clCol3.FixedHeight = 60;
                //clCol3.HorizontalAlignment = Element.ALIGN_CENTER;
                //clCol3.VerticalAlignment = Element.ALIGN_TOP;
                //clCol3.BorderWidth = 0;

                //tmpTable.AddCell(tmpCell);
                // tmpTable.DefaultCell.BorderWidth = 0;
                // tmpTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                // tmpTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //tmpTable.AddCell(new Paragraph(row.Cells[4].Value.ToString().ToUpper(), calibri6));
                //tmpTable.AddCell(new Paragraph(row.Cells[12].Value.ToString().ToUpper(), calibri));
                // pdfCell.AddElement(tmpTable);
                // tmpCell.AddElement(tmpTable);
                //tmpTable.AddCell(tmpCell);

                // tblPrueba.AddCell(tmpCell);  // Comentado 20/01/2022
                tmpTableCB.AddCell(tmpCellCB1);
                tmpTableCB.AddCell(tmpCellCB2);
                tmpTableCB.AddCell(tmpCellCB3);
                tmpTableCB.AddCell(tmpCellCB4);


                tmpCellCB1 = new PdfPCell(new Phrase(" ", _standardFont));
                tmpCellCB1.Border = 0;
                tmpCellCB2 = new PdfPCell(new Phrase(" ", _standardFont));
                tmpCellCB2.Border = 0;
                tmpCellCB3 = new PdfPCell(new Phrase(" ", _standardFont));
                tmpCellCB3.Border = 0;
                tmpCellCB4 = new PdfPCell(new Phrase(" ", _standardFont));
                tmpCellCB4.Border = 0;

                tmpTableCB.AddCell(tmpCellCB1);
                tmpTableCB.AddCell(tmpCellCB2);
                tmpTableCB.AddCell(tmpCellCB3);
                tmpTableCB.AddCell(tmpCellCB4);

                doc.Add(tmpTableCB);

                if (c == 3)
                {
                    doc.NewPage();
                    c = 1;
                } else
                {
                    c = c + 1;
                }
                
            }

            // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
            //doc.Add(tblPrueba);

            doc.Close();
            writer.Close();



        }



        //int esperar = 0;

        //Process p = new Process();
        //p.StartInfo.FileName = @"C:\Aplicativos\GestionEscolarFra\chequera.pdf";
        //p.StartInfo.Verb = "Print";

        //p.Start();
        ////libreria Threading
        //System.Threading.Thread.Sleep(3000);
        ////p.Close();
        ////p.CloseMainWindow();
        //while (!p.HasExited)
        //{
        //    System.Threading.Thread.Sleep(1000);
        //    esperar++;
        //    p.Close();
        //    //p.CloseMainWindow();
        //}







        //DataTable dt = new DataTable();
        //dt = ocnChequeraImpimir.InformeChequeraImprimir2();
        //int i = 1;
        //int j = 1;
        //foreach (DataRow row in dt.Rows)// por cada fila..
        //{
        //    if (i == 1)
        //    {
        //        lblApellidoNombre.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
        //        lblCurso.Text = Convert.ToString(row["CURSO"].ToString());
        //        lblCuota.Text = Convert.ToString(row["NUMCUOTA"].ToString());
        //        Label4.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
        //        Label1.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
        //        Label5.Text = Convert.ToString(row["CURSO"].ToString());
        //        Label2.Text = Convert.ToString(row["CURSO"].ToString());
        //        Label6.Text = Convert.ToString(row["NUMCUOTA"].ToString());
        //        Label3.Text = Convert.ToString(row["NUMCUOTA"].ToString());
        //        lblCodigoBarras.Text = Convert.ToString(row["BARRA"].ToString());
        //    }
        //    if (i == 2)
        //    {
        //        lblApellidoNombre2.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
        //        lblCurso2.Text = Convert.ToString(row["CURSO"].ToString());
        //        lblCuota2.Text = Convert.ToString(row["NUMCUOTA"].ToString());
        //        Label4_2.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
        //        Label1_2.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
        //        Label5_2.Text = Convert.ToString(row["CURSO"].ToString());
        //        Label2_2.Text = Convert.ToString(row["CURSO"].ToString());
        //        Label6_2.Text = Convert.ToString(row["NUMCUOTA"].ToString());
        //        Label3_2.Text = Convert.ToString(row["NUMCUOTA"].ToString());

        //        lblCodigoBarras2.Text = Convert.ToString(row["BARRA"].ToString());
        //    }
        //    if (i == 3)
        //    {
        //        lblApellidoNombre3.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
        //        lblCurso3.Text = Convert.ToString(row["CURSO"].ToString());
        //        lblCuota3.Text = Convert.ToString(row["NUMCUOTA"].ToString());
        //        Label4_3.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
        //        Label1_3.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
        //        Label5_3.Text = Convert.ToString(row["CURSO"].ToString());
        //        Label2_3.Text = Convert.ToString(row["CURSO"].ToString());
        //        Label6_3.Text = Convert.ToString(row["NUMCUOTA"].ToString());
        //        Label3_3.Text = Convert.ToString(row["NUMCUOTA"].ToString());

        //        lblCodigoBarras3.Text = Convert.ToString(row["BARRA"].ToString());
        //    }

        //    if (((i / 3) * 3) == 3)
        //    {

        //       // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "", "ImprimirPagina();", true);

        //    }

        //    if (i == 3)
        //    {
        //        i = 1;
        //    } else
        //    {
        //        i += 1;                
        //    }
        //    j += 1;

        //}
        //j -= 1;
        //if (((j / 3) * 3) < j)
        //{
        //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "", "ImprimirPagina();", true);
        //}


    }

}