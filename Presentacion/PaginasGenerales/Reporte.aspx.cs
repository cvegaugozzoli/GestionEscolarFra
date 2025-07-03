using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using System.Data.OleDb;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

public partial class PaginasGenerales_Reporte : System.Web.UI.Page
{
    private ReportDocument cr = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        string NomRep;

        ParameterValues Params = new ParameterValues();       // Creando una Coleccion de Parametros
        ParameterDiscreteValue Par = new ParameterDiscreteValue();   // Parametro Discreto q viene en el proc. alm. y se muestra en el Crystal Report
        Params.Clear();              // Limpiando la Coleccion de Datos
        int InstId = Convert.ToInt32(this.Session["_Institucion"]);
       
        NomRep = "~/PaginasGenerales/Reportes/" + Request.QueryString["NomRep"]; //  System.Web.UI.Page.Request.Item["NomRep"]; 
            

        string Ruta = MapPath(NomRep);  //System.Web.UI.Page.Server.MapPath(NomRep);
        NomRep = Ruta;

        cr.Load(NomRep);
        try
        {
            Int32 icuId;
            icuId = Int32.Parse(Request.QueryString["icuId"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = icuId;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@icuId"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }


        try
        {
            Int32 Id;
            Id = Int32.Parse(Request.QueryString["Id"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = Id;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@Id"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }
        try
        {
            Int32 cocId;
            cocId = Int32.Parse(Request.QueryString["cocId"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = cocId;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@cocId"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }
        try
        {
            Int32 mes;
            mes = Int32.Parse(Request.QueryString["mes"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = mes;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@mes"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }
        try
        {
            String ParamStr1;
            ParamStr1 = Convert.ToString(Request.QueryString["ParamStr1"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = ParamStr1;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@ParamStr1"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //   throw oError;
        }
        try
        {
            Int32 ParamInt1;
            ParamInt1 = Convert.ToInt32(Request.QueryString["ParamInt1"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = ParamInt1;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@ParamInt1"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //   throw oError;
        }
        //try
        //{
        //    Int32 cocId;
        //    cocId = Int32.Parse(Request.QueryString["cocId"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
        //    Par.Value = cocId;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
        //    Params.Add(Par);
        //    cr.DataDefinition.ParameterFields["@cocId"].ApplyCurrentValues(Params);
        //}
        //catch (Exception oError)
        //{
        //    //    throw oError;
        //}
        try
        {
            Int32 cocId2;
            cocId2 = Int32.Parse(Request.QueryString["cocId2"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = cocId2;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@cocId2"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }
        try
        {
            Int32 cocId3;
            cocId3 = Int32.Parse(Request.QueryString["cocId3"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = cocId3;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@cocId3"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }
        try
        {
            Int32 anio;
            anio = Int32.Parse(Request.QueryString["AnioCur"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = anio;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@anio"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //   throw oError;
        }

        try
        {
            Int32 aniocursado;
            aniocursado = Int32.Parse(Request.QueryString["aniocursado"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = aniocursado;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@aniocursado"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //   throw oError;
        }

        try
        {
            Int32 anio;
            anio = Int32.Parse(Request.QueryString["anio"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = anio;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@anio"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //   throw oError;
        }
        try
        {
            Int32 Anio;
            Anio = Int32.Parse(Request.QueryString["Anio"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = Anio;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@Anio"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //   throw oError;
        }
 try
        {
            Int32 RecauxUsu;
            RecauxUsu = Int32.Parse(Request.QueryString["RecauxUsu"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = RecauxUsu;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@RecauxUsu"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //   throw oError;
        }
        try
        {
            Int32 CuotaDesde;
            CuotaDesde = Int32.Parse(Request.QueryString["CuotaDesde"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = CuotaDesde;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@CuotaDesde"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //   throw oError;
        }

        try
        {
            Int32 CuotaHasta;
            CuotaHasta = Int32.Parse(Request.QueryString["CuotaHasta"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = CuotaHasta;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@CuotaHasta"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //   throw oError;
        }

        try
        {
            Int32 Concepto;
            Concepto = Int32.Parse(Request.QueryString["Concepto"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = Concepto;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@Concepto"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //   throw oError;
        }
        try
        {
            Int32 TConcepto;
            TConcepto = Int32.Parse(Request.QueryString["TConcepto"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = TConcepto;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@TConcepto"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //   throw oError;
        }
        try
        {
            Int32 aluId;
            aluId = Int32.Parse(Request.QueryString["aluId"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = aluId;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@aluId"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //   throw oError;
        }
        try
        {
            Int32 carId;
            carId = Int32.Parse(Request.QueryString["carId"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = carId;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@carId"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }

        try
        {
            Int32 plaId;
            plaId = Int32.Parse(Request.QueryString["plaId"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = plaId;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@plaId"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }

        try
        {
            Int32 curId;
            curId = Int32.Parse(Request.QueryString["curId"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = curId;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@curId"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }

        try
        {
            Int32 curso;
            curso = Int32.Parse(Request.QueryString["curso"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = curso;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@curso"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }

        try
        {
            Int32 camId;
            camId = Int32.Parse(Request.QueryString["camId"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = camId;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@camId"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }

        try
        {
            Int32 escId;
            escId = Int32.Parse(Request.QueryString["escId"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = escId;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@escId"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }

        try
        {
            Int32 insId;
            insId = Int32.Parse(Request.QueryString["insId"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = insId;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@insId"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }

        try
        {
            Int32 espCId;
            espCId = Int32.Parse(Request.QueryString["espCId"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = espCId;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@espCId"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }

        try
        {
            Int32 CntId;
            CntId = Int32.Parse(Request.QueryString["CntId"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = CntId;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@CntId"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }

        try
        {
            Int32 ixaNumeroActa;
            ixaNumeroActa = Int32.Parse(Request.QueryString["ixaNumeroActa"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = ixaNumeroActa;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@ixaNumeroActa"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }

        try
        {
            Int32 aplicafiltrofecha;
            aplicafiltrofecha = Int32.Parse(Request.QueryString["aplicafiltrofecha"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = aplicafiltrofecha;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@aplicafiltrofecha"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }

        try
        {
            DateTime desde;
            desde = DateTime.Parse(Request.QueryString["desde"].ToString()); // System.Web.UI.Page.Request.Item["anio"];
            Par.Value = desde;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@desde"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //    throw oError;
        }

        
        try
        {
            DateTime hasta;
          
            hasta = DateTime.Parse(Request.QueryString["hasta"]);
            Par.Value = hasta;
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@hasta"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //throw oError;
        }

     
        try
        {
            DateTime fechaDesde;
            fechaDesde = DateTime.Parse(Request.QueryString["fechaDesde"]);
            Par.Value = fechaDesde;  // Asignando un Valor Discreto a nuestra variable jalando el valor de una caja de texto de tu formulario
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@fechadesde"].ApplyCurrentValues(Params);   // Aplicando los valores de nuestra coleccion a los parametros del crystal report
        }
        catch (Exception oError)
        {
            //throw oError;
        }

        try
        {
            DateTime fechaHasta;
            //Params.Clear();
            fechaHasta = DateTime.Parse(Request.QueryString["fechaHasta"]);
            Par.Value = fechaHasta;
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@fechahasta"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //throw oError;
        }


        try
        {
            DateTime ixaFechaExamen;
            //Params.Clear();
            ixaFechaExamen = DateTime.Parse(Request.QueryString["ixaFechaExamen"]);
            Par.Value = ixaFechaExamen;
            Params.Add(Par);
            cr.DataDefinition.ParameterFields["@ixaFechaExamen"].ApplyCurrentValues(Params);
        }
        catch (Exception oError)
        {
            //throw oError;
        }



        //var crTableLogonInfos = new TableLogOnInfos();
        TableLogOnInfos crTableLogonInfo = new TableLogOnInfos();
        TableLogOnInfo crtablelogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();

        crConnectionInfo.ServerName = "localhost";
        crConnectionInfo.DatabaseName = "GestionEscolarFra";
        crConnectionInfo.UserID = "sa";
        crConnectionInfo.Password = "ms2014";

        //String Servidor = "PC\\SQL2012";
        //crConnectionInfo.ServerName = Servidor;
        //crConnectionInfo.DatabaseName = "GestionEscolarFra";
        //crConnectionInfo.UserID = "sa";
        //crConnectionInfo.Password = "mds2013";

        //String Servidor = "DESKTOP-DR5HH6H";
        //crConnectionInfo.ServerName = Servidor;
        //crConnectionInfo.DatabaseName = "GestionEscolarFra";
        //crConnectionInfo.UserID = "sa";
        //crConnectionInfo.Password = "racing01";

        Tables CrTables;
        CrTables = cr.Database.Tables;
        foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
        {
            crtablelogoninfo = CrTable.LogOnInfo;
            crtablelogoninfo.ConnectionInfo = crConnectionInfo;
            CrTable.ApplyLogOnInfo(crtablelogoninfo);
            CrTable.Location = CrTable.Name;
        }

        crTableLogonInfo.Add(crtablelogoninfo);

        CrystalReportViewer1.ReportSource = cr; // Mostrando el Reporte
        cr.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, NomRep);
        CrystalReportViewer1.ReportSource = cr; // Mostrando el Reporte
        String Exporta;
        Exporta = Page.Request["Exporta"];
        if (Exporta == "1")
        {
            cr.ExportToHttpResponse(ExportFormatType.Excel, Page.Response, true, NomRep);
        }
        else
        {
            cr.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Page.Response, false, NomRep);
        }

    }

    protected void Page_unload(object sender, EventArgs e)
    {
        if (cr != null)
        {
            if (cr.IsLoaded)
            {
                cr.Close();
                cr.Dispose();
            }
        }

    }
}