using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;


public partial class ChequeraBarraGrilla2 : System.Web.UI.Page
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

        //var name = "Juan";
        //// asumiendo que 'el' es un elemento de HTML DOM
        //el.innerHTML = name; // sin peligro
        //Response.Output.Write("Hola Mundo New");

        

        //Response.Write("Hola Mundo"); 


        DataTable dt = new DataTable();
        dt = ocnChequeraImpimir.InformeChequeraImprimir2();
        int i = 1;
        int j = 1;
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

            if (((i / 3) * 3) == 3)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "ImprimirPagina();", true);

                //lblApellidoNombre.Text = "";
                //lblCurso.Text = "";
                //lblCuota.Text = "";
                //Label4.Text = "";
                //Label1.Text = "";
                //Label5.Text = "";
                //Label2.Text = "";
                //Label6.Text = "";
                //Label3.Text = "";
                //lblCodigoBarras.Text = "";

                //lblApellidoNombre2.Text = "";
                //lblCurso2.Text = "";
                //lblCuota2.Text = "";
                //Label4_2.Text = "";
                //Label1_2.Text = "";
                //Label5_2.Text = "";
                //Label2_2.Text = "";
                //Label6_2.Text = "";
                //Label3_2.Text = "";
                //lblCodigoBarras2.Text = "";

                //lblApellidoNombre3.Text = "";
                //lblCurso3.Text = "";
                //lblCuota3.Text = "";
                //Label4_3.Text = "";
                //Label1_3.Text = "";
                //Label5_3.Text = "";
                //Label2_3.Text = "";
                //Label6_3.Text = "";
                //Label3_3.Text = "";
                //lblCodigoBarras3.Text = "";

            }

            if (i == 3)
            {
                i = 1;
            } else
            {
                i += 1;                
            }
            j += 1;

        }
        j -= 1;
        if (((j / 3) * 3) < j)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "ImprimirPagina();", true);
        }


    }

}