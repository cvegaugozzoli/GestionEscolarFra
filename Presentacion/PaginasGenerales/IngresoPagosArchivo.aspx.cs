using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Data.OleDb;




public partial class IngresoPagosArchivo : System.Web.UI.Page
{
    GESTIONESCOLAR.Negocio.Usuario ocnUsuario = new GESTIONESCOLAR.Negocio.Usuario();

    GESTIONESCOLAR.Negocio.Alumno ocnAlumno = new GESTIONESCOLAR.Negocio.Alumno();
    GESTIONESCOLAR.Negocio.Familiar ocnFamiliar = new GESTIONESCOLAR.Negocio.Familiar();
    GESTIONESCOLAR.Negocio.ConceptosTipos ocnConceptosTipos = new GESTIONESCOLAR.Negocio.ConceptosTipos();
    GESTIONESCOLAR.Negocio.InscripcionConcepto ocnInscripcionConcepto = new GESTIONESCOLAR.Negocio.InscripcionConcepto();
    GESTIONESCOLAR.Negocio.InscripcionCursado ocnInscripcionCursado = new GESTIONESCOLAR.Negocio.InscripcionCursado();
    GESTIONESCOLAR.Negocio.Becas ocnBecas = new GESTIONESCOLAR.Negocio.Becas();
    GESTIONESCOLAR.Negocio.ComprobantesDetalle ocnComprobantesDetalle = new GESTIONESCOLAR.Negocio.ComprobantesDetalle();
    GESTIONESCOLAR.Negocio.ComprobantesCabecera ocnComprobantesCabecera = new GESTIONESCOLAR.Negocio.ComprobantesCabecera();
    GESTIONESCOLAR.Negocio.ComprobantesPtosVta ocnComprobantesPtosVta = new GESTIONESCOLAR.Negocio.ComprobantesPtosVta();
    GESTIONESCOLAR.Negocio.ComprobantesFormasPago ocnComprobantesFormasPago = new GESTIONESCOLAR.Negocio.ComprobantesFormasPago();
    GESTIONESCOLAR.Negocio.Conceptos ocnConceptos = new GESTIONESCOLAR.Negocio.Conceptos();
    GESTIONESCOLAR.Negocio.ConceptosIntereses ocnConceptosIntereses = new GESTIONESCOLAR.Negocio.ConceptosIntereses();
    GESTIONESCOLAR.Negocio.TempImputaPagos ocnTempImputaPagos = new GESTIONESCOLAR.Negocio.TempImputaPagos();
    GESTIONESCOLAR.Negocio.Instituciones ocnInstituciones = new GESTIONESCOLAR.Negocio.Instituciones();
    int insId;
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    DataTable dt2 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //insId = Convert.ToInt32(Session["_Institucion"]);
                this.Master.TituloDelFormulario = " Ingreso de Pagos";
                insId = Convert.ToInt32(Session["_Institucion"]);
                int usuario = Convert.ToInt32(Session["_usuId"].ToString());
                dt = ocnUsuario.ObtenerUno(usuario);
                btnImputar.Visible = false;
                listado.Visible = false;
                int PageIndex = 0;
                if (this.Session["IngresoPagosArchivo.PageIndex"] == null)
                {
                    Session.Add("IngresoPagosArchivo.PageIndex", 0);
                }
                else
                {
                    PageIndex = Convert.ToInt32(Session["IngresoPagosArchivo.PageIndex"]);
                }
            }

        }
        catch (Exception oError)
        {
            lblMensajeError.Text = @"<div class=""alert alert-danger alert-dismissable"">
<button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
<a class=""alert-link"" href=""#"">Error de Sistema</a><br/>
Se ha producido el siguiente error:<br/>
MESSAGE:<br>" + oError.Message + "<br><br>EXCEPTION:<br>" + oError.InnerException + "<br><br>TRACE:<br>" + oError.StackTrace + "<br><br>TARGET:<br>" + oError.TargetSite +
"</div>";
        }
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "printGrid", "printGrid();", true);
        //}
        //catch { }

        try
        {
            AlerInfo.Visible = true;
            int usuIdCreacion = this.Master.usuId;
            Int32 IdNew;
            String Concepto, Nombre, CodAlumno, Imputa, InstNombre, Curso;
            Decimal Importe;
            Int32 NroCuota;
            DateTime FechaPago;

            //Elimina todo el archivo si hubiera anteriormente una impresión
            ocnTempImputaPagos.EliminarTodo();

            foreach (GridViewRow row1 in GridView1.Rows)
            {
                CodAlumno = Convert.ToString(GridView1.DataKeys[row1.RowIndex].Values["aluId"]);
                Nombre = Convert.ToString(GridView1.DataKeys[row1.RowIndex].Values["Nombre"]);
                Concepto = Convert.ToString(GridView1.DataKeys[row1.RowIndex].Values["Concepto"]);
                NroCuota = Convert.ToInt32(GridView1.DataKeys[row1.RowIndex].Values["NroCuota"]);
                Importe = Convert.ToDecimal(GridView1.DataKeys[row1.RowIndex].Values["Importe"]);
                FechaPago = Convert.ToDateTime(GridView1.DataKeys[row1.RowIndex].Values["FechaPago"]);
                Imputa = Convert.ToString(GridView1.DataKeys[row1.RowIndex].Values["Imputa"]);
                Curso = Convert.ToString(GridView1.DataKeys[row1.RowIndex].Values["Curso"]);
                InstNombre = lblColegio.Text;

                //Insertar tabla temporal
                IdNew = ocnTempImputaPagos.Insertar(Nombre, Concepto, Importe, NroCuota, FechaPago, Imputa, InstNombre, Curso);
            }
            String NomRep;
            NomRep = "ListadoPagosArchivo.rpt";
            FuncionesUtiles.AbreVentana("Reporte.aspx?NomRep=" + NomRep);


        }
        catch (Exception oError)
        {
            lblMensajeError.Text = @"<div class=""alert alert-danger alert-dismissable"">
<button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
<a class=""alert-link"" href=""#"">Error de Sistema</a><br/>
Se ha producido el siguiente error:<br/>
MESSAGE:<br>" + oError.Message + "<br><br>EXCEPTION:<br>" + oError.InnerException + "<br><br>TRACE:<br>" + oError.StackTrace + "<br><br>TARGET:<br>" + oError.TargetSite +
    "</div>";
        }



    }
    protected void Listar_Click(object sender, EventArgs e)
    {
        btnImprimir.Visible = false;
        DataTable dt1 = new DataTable();

        //Verificar si el FileUpload con tiene un Archivo
        if (FileUpload1.HasFile)
        {

            //Colocar el nombre del Archivo en una Variable String
            string filename = FileUpload1.FileName;

            //Enviar el Archivo a un Directorio de forma Temporal
            FileUpload1.SaveAs(Server.MapPath("/Uploads/" + filename));

            //Importar el Archivo Excel a un Gridview con el Metodo ExportToGrid
            ExportToGrid(Server.MapPath("/Uploads/" + filename), Path.GetExtension(Server.MapPath("/Uploads/" + filename)), filename);

        }
    }

    void ExportToGrid(String path, String Extension, String filename)
    {
        lblMensajeError.Text = "";
        listado.Visible = false;
        btnImputar.Visible = false;
        OleDbConnection MiConexion = null;
        DataSet DtSet = null;
        OleDbDataAdapter MiComando = null;

        DataTable dtI = new DataTable();
        string numCta = "";

        if (Extension == ".xls")
        {
            //numCta = filename.Substring(0, filename.Length - 4);
            //numCta = filename.Substring(9, 3);
            //Conexion para Formato .xls 2003
            MiConexion = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + path + "';Extended Properties=Excel 8.0;");
        }

        else if (Extension == ".xlsx")
        {
            //numCta = filename.Substring(0, filename.Length - 5);
            //numCta = filename.Substring(9, 3);
            //Conexion para Formato .xlsx 2007 o 2010
            MiConexion = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties=Excel 12.0 Xml;");
        }

        // Busco el id de institución
        //dtI = ocnInstituciones.ObtenerUnoPorCodigo(numCta);
        //if (dtI.Rows.Count > 0)
        //{
        //    insId = Convert.ToInt32(dtI.Rows[0]["InsId"].ToString());
        //    lblColegio.Text = "Archivo de: " + Convert.ToString(dtI.Rows[0]["InsNombre"].ToString());


        //Seleccionar el archivo Excel
        MiConexion.Open();
        DataTable Datable = new DataTable();

        //Seleccionar la Hoja que Esta Activa
        Datable = MiConexion.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        String Nombre_Hoja = Datable.Rows[0]["TABLE_NAME"].ToString();


        MiComando = new System.Data.OleDb.OleDbDataAdapter("select * from [" + Nombre_Hoja + "]", MiConexion);
        DtSet = new System.Data.DataSet();
        //Bindear todo el Contenido del Excel a un Dataset
        MiComando.Fill(DtSet, "[" + Nombre_Hoja + "]");
        dt1 = DtSet.Tables[0];
        MiConexion.Close();
        //Verificar si el Datatable Contiene Valores
        numCta = "";

        if (dt1.Rows.Count > 0)
        {
            string CodBarraI = "";
            CodBarraI = Convert.ToString(dt1.Rows[0]["Cod# Barra"].ToString());  // Convert.ToString(row["Cod# Barra"].ToString());
            numCta = CodBarraI.Substring(1, 3);
            dtI = ocnInstituciones.ObtenerUnoPorCodigo(numCta);
            insId = Convert.ToInt32(dtI.Rows[0]["InsId"].ToString());
            lblColegio.Text = "Archivo de: " + Convert.ToString(dtI.Rows[0]["InsNombre"].ToString());
            lblColegioId.Text = Convert.ToString(dtI.Rows[0]["InsId"].ToString());
            GridView GridView2 = new GridView();
            GridView2.DataSource = dt1;
            GridView2.DataBind();
            Session["ArchivoImputar"] = dt1;
            //GridView1.DataSource = dt1;
            //GridView1.DataBind();
            //lblMensajeError.Text = "Cantidad de Cuentas a Actualizar: <b><font color=red>" + GridView2.Rows.Count.ToString() + "</font></b>";
            //Panel_Modificaciones.Controls.Add(GridView2);
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            DataTable dt7 = new DataTable();
            dt2.Columns.Add("aluId", typeof(int));
            dt2.Columns.Add("icoId", typeof(Int32));
            dt2.Columns.Add("Nombre", typeof(String));
            dt2.Columns.Add("Concepto", typeof(String));
            dt2.Columns.Add("FechaPago", typeof(DateTime));
            dt2.Columns.Add("NroCuota", typeof(Int32));
            dt2.Columns.Add("Importe", typeof(Decimal));
            dt2.Columns.Add("NroComprobante", typeof(String));
            dt2.Columns.Add("Imputa", typeof(String));
            dt2.Columns.Add("Curso", typeof(String));

            foreach (DataRow row in dt1.Rows)//para cada fila de excel           
            {
                string CodBarra = "";
                //insId = Convert.ToInt32(Session["_Institucion"]);
                CodBarra = Convert.ToString(row["Cod# Barra"].ToString());
                String aluId = CodBarra.Substring(7, 5);
                String nroCuota = CodBarra.Substring(12, 2);
                string ImporteReal = CodBarra.Substring(18, 5);
                DateTime FechaPago = Convert.ToDateTime(row["Fecha"].ToString());
                //String Concepto = 
                //if (aluId == "07909")
                //{
                //    String qqq;
                //    qqq = "1";
                //}

                dt4 = ocnInscripcionConcepto.ObtenerUnoxInsxaluxcuotaximporte(insId, Convert.ToInt32(aluId), Convert.ToInt32(nroCuota), Convert.ToInt32(ImporteReal));// obtengo inscripcion Cocepto que no exista en comprobante detalle

                //dt3 = ocnConceptos.ObtenerUno(Convert.ToInt32(dt4.Rows[0]["conId"].ToString()));
                DataRow row1 = dt2.NewRow();
                if (dt4.Rows.Count > 0)
                {
                    dt3 = ocnAlumno.ObtenerUno(Convert.ToInt32(aluId));

                    if (dt3.Rows.Count > 0)
                    {
                        int icoId = Convert.ToInt32(dt4.Rows[0]["Id"].ToString());
                        row1["aluId"] = Convert.ToString(dt3.Rows[0]["Id"].ToString());
                        row1["icoId"] = Convert.ToInt32(icoId);
                        row1["Nombre"] = Convert.ToString(dt3.Rows[0]["Nombre"].ToString());
                        row1["Concepto"] = Convert.ToString(dt4.Rows[0]["Conceptos"].ToString());
                        row1["FechaPago"] = Convert.ToDateTime(row["Fecha"].ToString());
                        row1["NroCuota"] = Int32.Parse(nroCuota);
                        row1["Importe"] = Convert.ToDecimal(row["Importe"].ToString());
                        row1["NroComprobante"] = Convert.ToString(row["Cpbte#N°"].ToString());
                        row1["Imputa"] = "";
                        String pp;
                        pp = Convert.ToString(dt4.Rows[0]["Curso"].ToString());
                        row1["Curso"] = Convert.ToString(dt4.Rows[0]["Curso"].ToString());
                        dt2.Rows.Add(row1);
                    }
                }
                else
                {
                    dt6 = ocnInscripcionConcepto.ObtenerUnoxInsxaluxcuotaximporteDetalle(insId, Convert.ToInt32(aluId), Convert.ToInt32(nroCuota), Convert.ToInt32(ImporteReal));// obtengo inscripcion Cocepto que  exista en comprobante detalle
                    dt3 = ocnAlumno.ObtenerUno(Convert.ToInt32(aluId));
                    if (dt6.Rows.Count > 0)
                    {
                        if (dt3.Rows.Count > 0)
                        {
                            int icoId = Convert.ToInt32(dt6.Rows[0]["Id"].ToString());

                            row1["aluId"] = Convert.ToString(dt3.Rows[0]["Id"].ToString());
                            row1["icoId"] = Convert.ToInt32(icoId);
                            row1["Nombre"] = Convert.ToString(dt3.Rows[0]["Nombre"].ToString());
                            row1["Concepto"] = Convert.ToString(dt6.Rows[0]["Conceptos"].ToString());
                            row1["FechaPago"] = Convert.ToDateTime(row["Fecha"].ToString());
                            row1["NroCuota"] = Int32.Parse(nroCuota);
                            row1["Importe"] = Convert.ToDecimal(row["Importe"].ToString());
                            row1["NroComprobante"] = Convert.ToString(row["Cpbte#N°"].ToString());
                            row1["Imputa"] = "";
                            String pp;
                            pp = Convert.ToString(dt6.Rows[0]["Curso"].ToString());
                            row1["Curso"] = Convert.ToString(dt6.Rows[0]["Curso"].ToString());  
                            dt2.Rows.Add(row1);
                        }
                    }
                    else
                    {
                        //String qqq, ppp;
                        //qqq = Convert.ToString(dt3.Rows[0]["Nombre"].ToString());
                        row1["aluId"] = Convert.ToString(dt3.Rows[0]["Id"].ToString());
                        row1["icoId"] = 0;
                        row1["Nombre"] = Convert.ToString(dt3.Rows[0]["Nombre"].ToString());
                        //row1["Concepto"] = Convert.ToString(dt6.Rows[0]["Conceptos"].ToString());
                        //ppp = Convert.ToString(row["Concepto"].ToString());
                        //row1["Concepto"] = Convert.ToString(row["Concepto"].ToString());
                        row1["Concepto"] = "";
                        row1["FechaPago"] = Convert.ToDateTime(row["Fecha"].ToString());
                        row1["NroCuota"] = Int32.Parse(nroCuota);
                        row1["Importe"] = Convert.ToDecimal(row["Importe"].ToString());
                        row1["NroComprobante"] = Convert.ToString(row["Cpbte#N°"].ToString());
                        row1["Imputa"] = "NE";

                        dt2.Rows.Add(row1);
                    }
                }
            }

            GridView1.DataSource = dt2;
            GridView1.DataBind();
            lblCantidadRegistros.Text = Convert.ToString(dt2.Rows.Count);
            listado.Visible = true;
            btnImputar.Visible = true;
            btnImprimir.Visible = true;
        }


        //Eliminar el Archivo Excel del Directorio Temporal
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        //Vaciar El Dataset y los Datatable
        dt1 = null;
        DtSet = null;
        Datable = null;
        //} else
        //{

        //}
    }

    protected void btnImputar_Click(object sender, EventArgs e)
    {
      try
        {
            insId = Convert.ToInt32(lblColegioId.Text);
            AlerInfo.Visible = true;
            int destino = 1;//Entrenamiento
            int CompTipo = 1;// factura C
            int LugPago = 3;//BANCO MUNICIPAL
            DateTime FechaHoraCreacion = DateTime.Now;
            DateTime FechaHoraUltimaModificacion = DateTime.Now;
            int usuIdCreacion = this.Master.usuId;
            int usuIdUltimaModificacion = this.Master.usuId;
            DataTable dt8 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt = new DataTable();
            DataTable dt4 = new DataTable(); DataTable dt7 = new DataTable();
            int cocIdNuevo;
            int cfoIdNuevo;
            dt.Columns.Add("aluId", typeof(int));
            dt.Columns.Add("icoId", typeof(Int32));
            dt.Columns.Add("Nombre", typeof(String));
            dt.Columns.Add("Concepto", typeof(String));
            dt.Columns.Add("FechaPago", typeof(DateTime));
            dt.Columns.Add("NroCuota", typeof(Int32));
            dt.Columns.Add("Importe", typeof(Decimal));
            dt.Columns.Add("NroComprobante", typeof(Decimal));
            dt.Columns.Add("Curso", typeof(String));
            dt.Columns.Add("Imputa", typeof(String));
            //insId = Convert.ToInt32(Session["_Institucion"]);

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (Convert.ToString(GridView1.DataKeys[row.RowIndex].Values["Imputa"]) == "")
                {
                    String CodAlumno = Convert.ToString(GridView1.DataKeys[row.RowIndex].Values["aluId"]);
                    Decimal sumatoria = 0;// Saco TOTAL de conceptos
                    int Bandera = 0;
                    int Bandera1 = 0;
                    foreach (GridViewRow row2 in GridView1.Rows)
                    {
                        if (Convert.ToString(GridView1.DataKeys[row2.RowIndex].Values["Imputa"]) == "")
                        {
                            if (CodAlumno == Convert.ToString(GridView1.DataKeys[row2.RowIndex].Values["aluId"]))
                            {
                                Int32 aluId = Convert.ToInt32(GridView1.DataKeys[row2.RowIndex].Values["aluId"]);
                                Int32 icoId = Convert.ToInt32(GridView1.DataKeys[row2.RowIndex].Values["icoId"]);
                                String Nombre = Convert.ToString(GridView1.DataKeys[row2.RowIndex].Values["Nombre"]);
                                String Concepto = Convert.ToString(GridView1.DataKeys[row2.RowIndex].Values["Concepto"]);
                                Int32 NroCuota = Convert.ToInt32(GridView1.DataKeys[row2.RowIndex].Values["NroCuota"]);
                                Decimal Importe = Convert.ToDecimal(GridView1.DataKeys[row2.RowIndex].Values["Importe"]);
                                String Curso = Convert.ToString(GridView1.DataKeys[row2.RowIndex].Values["Curso"]);
                                foreach (DataRow row4 in dt.Rows) //Veo si ya lo inserté
                                {
                                    if (icoId == Convert.ToInt32(row4["icoId"].ToString()))
                                    {
                                        Bandera1 = 1;//Ingresado
                                    }
                                }
                                if (Bandera1 == 0)
                                {
                                    dt5 = ocnComprobantesDetalle.ObtenerUnoxicoId(icoId);
                                    DataRow row1 = dt.NewRow();
                                    row1["aluId"] = aluId;
                                    row1["icoId"] = icoId;
                                    row1["Nombre"] = Nombre;
                                    row1["Concepto"] = Concepto;
                                    row1["FechaPago"] = Convert.ToDateTime(GridView1.DataKeys[row2.RowIndex].Values["FechaPago"]);
                                    row1["NroCuota"] = NroCuota;
                                    row1["Importe"] = Importe;
                                    row1["Curso"] = Curso;
                                    if (dt5.Rows.Count == 0)
                                    {
                                        row1["Imputa"] = "I";
                                        Bandera = 1;
                                        sumatoria += (Convert.ToDecimal(GridView1.DataKeys[row2.RowIndex].Values["Importe"]));
                                    }

                                    else
                                    {
                                        if (dt5.Rows.Count > 0)
                                        {
                                            row1["Imputa"] = "E";
                                        }
                                    }
                                    dt.Rows.Add(row1);
                                }
                            }
                        }
                        else
                        {
                            Int32 aluId = Convert.ToInt32(GridView1.DataKeys[row2.RowIndex].Values["aluId"]);
                            Int32 icoId = Convert.ToInt32(GridView1.DataKeys[row2.RowIndex].Values["icoId"]);
                            String Nombre = Convert.ToString(GridView1.DataKeys[row2.RowIndex].Values["Nombre"]);
                            String Concepto = Convert.ToString(GridView1.DataKeys[row2.RowIndex].Values["Concepto"]);
                            Int32 NroCuota = Convert.ToInt32(GridView1.DataKeys[row2.RowIndex].Values["NroCuota"]);
                            Decimal Importe = Convert.ToDecimal(GridView1.DataKeys[row2.RowIndex].Values["Importe"]);
                            String Curso = Convert.ToString(GridView1.DataKeys[row2.RowIndex].Values["Curso"]);
                            DataRow row1 = dt.NewRow();
                            row1["aluId"] = aluId;
                            row1["icoId"] = 0;
                            row1["Nombre"] = Nombre;
                            row1["Concepto"] = Concepto;
                            row1["FechaPago"] = Convert.ToDateTime(GridView1.DataKeys[row2.RowIndex].Values["FechaPago"]);
                            row1["NroCuota"] = NroCuota;
                            row1["Importe"] = Importe;
                            row1["Curso"] = Curso;
                            row1["Imputa"] = "NE";
                        }
                    }

                    if (dt.Rows.Count > 0)
                    {
                        if (Bandera == 1) // significa que al menos una fila hay que imputar
                        {
                            dt8 = ocnComprobantesPtosVta.ObtenerUnoxInstxTipoCompxDest(insId, CompTipo, destino);

                            String compPtoVta = Convert.ToString(dt8.Rows[0]["Id"].ToString());
                            int valor = Convert.ToInt32(dt8.Rows[0]["UltimoNro"].ToString());
                            int NroCompr = valor + 1;
                            String lblUltimoNro = string.Format("{0:00000000}", NroCompr);

                            //Insertar y Actualizar Tablas
                            //Comprobante Cabecera
                            cocIdNuevo = ocnComprobantesCabecera.InsertarTraerId(CompTipo, compPtoVta, lblUltimoNro, Convert.ToDateTime(dt.Rows[0]["FechaPago"].ToString()), sumatoria, LugPago, "", true, usuIdCreacion, usuIdUltimaModificacion, FechaHoraCreacion, FechaHoraUltimaModificacion);
                            String cocIdNuevo1 = Convert.ToString(cocIdNuevo);
                            //Comprobante Pto Vta
                            ocnComprobantesPtosVta.ActualizarUltimoNro(Convert.ToInt32(compPtoVta), NroCompr, usuIdCreacion, usuIdUltimaModificacion, FechaHoraCreacion, FechaHoraUltimaModificacion);

                            //Cambio 17052021
                            int FormPagoIn = 1;
                            //cfoIdNuevo = ocnComprobantesFormasPago.InsertarTraerId(cocIdNuevo, FormPagoIn, sumatoria, true, usuIdCreacion, usuIdUltimaModificacion, FechaHoraCreacion, FechaHoraUltimaModificacion);
                            /////////

                            foreach (DataRow row3 in dt.Rows)
                            {
                                if (CodAlumno == Convert.ToString(row3["aluId"].ToString()))
                                {
                                    int cdeId=ocnComprobantesDetalle.InsertarTraeId(cocIdNuevo, Convert.ToInt32(row3["icoId"].ToString()), Convert.ToDecimal(row3["Importe"].ToString()), true, usuIdCreacion, usuIdUltimaModificacion, FechaHoraCreacion, FechaHoraUltimaModificacion);
                                    cfoIdNuevo = ocnComprobantesFormasPago.InsertarTraerId(cdeId, FormPagoIn, Convert.ToDecimal(row3["Importe"].ToString()), true, usuIdCreacion, usuIdUltimaModificacion, FechaHoraCreacion, FechaHoraUltimaModificacion);
                                }
                            }
                        }
                    }
                }
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch (Exception oError)
        {
            lblMensajeError.Text = @"<div class=""alert alert-danger alert-dismissable"">
        <button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
        <a class=""alert-link"" href=""#"">Error de Sistema</a><br/>
        Se ha producido el siguiente error:<br/>
        MESSAGE:<br>" + oError.Message + "<br><br>EXCEPTION:<br>" + oError.InnerException + "<br><br>TRACE:<br>" + oError.StackTrace + "<br><br>TARGET:<br>" + oError.TargetSite +
    "</div>";
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../PaginasBasicas/Inicio.aspx", true);
        }
        catch (Exception oError)
        {
            lblMensajeError.Text = @"<div class=""alert alert-danger alert-dismissable"">
<button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
<a class=""alert-link"" href=""#"">Error de Sistema</a><br/>
Se ha producido el siguiente error:<br/>
MESSAGE:<br>" + oError.Message + "<br><br>EXCEPTION:<br>" + oError.InnerException + "<br><br>TRACE:<br>" + oError.StackTrace + "<br><br>TARGET:<br>" + oError.TargetSite +
    "</div>";
        }
    }

    //    protected void btnImprimir(object sender, EventArgs e)
    //    {

    //        try
    //        {
    //            insId = Convert.ToInt32(Session["_Institucion"]);
    //            String NomRep;

    //            Int32 anio = 0;


    //            NomRep = "ListadoPorCursoAnio.rpt";

    //            //FuncionesUtiles.AbreVentana("Reporte.aspx?curso=" + curso + "&anio=" + anio + "&insId=" + insId + "&NomRep=" + NomRep);
    //        }
    //        catch (Exception oError)
    //        {
    //            lblMensajeError.Text = @"<div class=""alert alert-danger alert-dismissable"">
    //<button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
    //<a class=""alert-link"" href=""#"">Error de Sistema</a><br/>
    //Se ha producido el siguiente error:<br/>
    //MESSAGE:<br>" + oError.Message + "<br><br>EXCEPTION:<br>" + oError.InnerException + "<br><br>TRACE:<br>" + oError.StackTrace + "<br><br>TARGET:<br>" + oError.TargetSite +
    //"</div>";
    //        }



}
