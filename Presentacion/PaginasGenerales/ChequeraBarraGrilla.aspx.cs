using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;


public partial class ChequeraBarraGrilla : System.Web.UI.Page
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
        foreach (DataRow row in dt.Rows)// por cada fila..
        {
            if (i == 1)
            {
                lblApellidoNombre.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
                lblCurso.Text = Convert.ToString(row["CURSO"].ToString());
                lblCuota.Text = Convert.ToString(row["NUMCUOTA"].ToString());
                Label4.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
                Label1.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
                Label5.Text = Convert.ToString(row["CURSO"].ToString());
                Label2.Text = Convert.ToString(row["CURSO"].ToString());
                Label6.Text = Convert.ToString(row["NUMCUOTA"].ToString());
                Label3.Text = Convert.ToString(row["NUMCUOTA"].ToString());

                lblCodigoBarras.Text = Convert.ToString(row["BARRA"].ToString());
            }
            if (i == 2)
            {
                lblApellidoNombre2.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
                lblCurso2.Text = Convert.ToString(row["CURSO"].ToString());
                lblCuota2.Text = Convert.ToString(row["NUMCUOTA"].ToString());
                Label4_2.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
                Label1_2.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
                Label5_2.Text = Convert.ToString(row["CURSO"].ToString());
                Label2_2.Text = Convert.ToString(row["CURSO"].ToString());
                Label6_2.Text = Convert.ToString(row["NUMCUOTA"].ToString());
                Label3_2.Text = Convert.ToString(row["NUMCUOTA"].ToString());

                lblCodigoBarras2.Text = Convert.ToString(row["BARRA"].ToString());
            }
            if (i == 3)
            {
                lblApellidoNombre3.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
                lblCurso3.Text = Convert.ToString(row["CURSO"].ToString());
                lblCuota3.Text = Convert.ToString(row["NUMCUOTA"].ToString());
                Label4_3.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
                Label1_3.Text = Convert.ToString(row["APELLIDOYNOMBRE"].ToString());
                Label5_3.Text = Convert.ToString(row["CURSO"].ToString());
                Label2_3.Text = Convert.ToString(row["CURSO"].ToString());
                Label6_3.Text = Convert.ToString(row["NUMCUOTA"].ToString());
                Label3_3.Text = Convert.ToString(row["NUMCUOTA"].ToString());

                lblCodigoBarras3.Text = Convert.ToString(row["BARRA"].ToString());
            }
            if (i == 3)
            {
                i = 1;
            } else
            {
                i += 1;
            }

        }



    }

}