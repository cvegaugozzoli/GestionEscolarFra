using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class ChequeraBarra : System.Web.UI.Page
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

        foreach (DataRow row in dt.Rows)// por cada fila..
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


            this.Grilla.DataSource = dt;
        this.Grilla.PageIndex = PageIndex;
        this.Grilla.DataBind();
        //var x = document.getElementsByClassName("inputdata");
        //for (var i = 0; i < x.length; i++)
        //{
        //    var code = x[i].innerHtml;
        //    x[i].innerHtml = DrawCode39Barcode(code, 8);
        //}

        ////lblCodigoBarras.Text = row["BARRA"].ToString();



    }

    //public string ObtenerCadenaConDigitoVerificador(string Cadena)
    //{
    //    string CadenaConDigitoVerificador = "";

    //    //DataTable dt = new DataTable();

    //    CadenaConDigitoVerificador = "4033121026080894160250000800800790200";


    //    return CadenaConDigitoVerificador;
    //}

}