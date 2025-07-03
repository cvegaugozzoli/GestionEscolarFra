
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class EstadoCuentaPadres : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    GESTIONESCOLAR.Negocio.ComprobantesCabecera ocnComprobantesCabecera = new GESTIONESCOLAR.Negocio.ComprobantesCabecera();
    GESTIONESCOLAR.Negocio.Alumno ocnAlumno = new GESTIONESCOLAR.Negocio.Alumno();
    GESTIONESCOLAR.Negocio.ComprobantesDetalle ocnComprobantesDetalle = new GESTIONESCOLAR.Negocio.ComprobantesDetalle();
    GESTIONESCOLAR.Negocio.ComprobantesFormasPago ocnComprobantesFormasPago = new GESTIONESCOLAR.Negocio.ComprobantesFormasPago();
    GESTIONESCOLAR.Negocio.InscripcionCursado ocnInscripcionCursado = new GESTIONESCOLAR.Negocio.InscripcionCursado();
    GESTIONESCOLAR.Negocio.InscripcionConcepto ocnInscripcionConcepto = new GESTIONESCOLAR.Negocio.InscripcionConcepto();
    GESTIONESCOLAR.Negocio.ConceptosIntereses ocnConceptosIntereses = new GESTIONESCOLAR.Negocio.ConceptosIntereses();
    GESTIONESCOLAR.Negocio.Conceptos ocnConceptos = new GESTIONESCOLAR.Negocio.Conceptos();
    GESTIONESCOLAR.Negocio.PagosCheques ocnPagosCheques = new GESTIONESCOLAR.Negocio.PagosCheques();
    GESTIONESCOLAR.Negocio.PagosTarjetas ocnPagosTarjetas = new GESTIONESCOLAR.Negocio.PagosTarjetas();
    GESTIONESCOLAR.Negocio.PagosTransferenciaElectronica ocnPagosTransferenciaElectronica = new GESTIONESCOLAR.Negocio.PagosTransferenciaElectronica();
    GESTIONESCOLAR.Negocio.Instituciones ocnInstituciones = new GESTIONESCOLAR.Negocio.Instituciones();
    GESTIONESCOLAR.Negocio.TEMESTADOCUENTA ocnTEMESTADOCUENTA = new GESTIONESCOLAR.Negocio.TEMESTADOCUENTA();

    int insId;

    Int32 icuId2;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                int Anio = DateTime.Now.Year;
                //txtAnioLectivo.Text = Convert.ToString(Anio);


                alerError2.Visible = false;
                lblMjerror2.Text = "";
                btnImprimir2.Visible = false;
                lblCuotas.Visible = false;
                TexCuotas.Visible = false;
                lblInt.Visible = false;
                txtIntereses.Visible = false;
                txtTot.Visible = false;
                lblTot.Visible = false;
                Session["Bandera"] = 0;
                //btnFacturar.Visible = false;
                //txtFchPago.Visible = false;
                //lblFchPago.Visible = false;
                this.Master.TituloDelFormulario = "Estado de Cuenta";
                //insId = Convert.ToInt32(Session["_Institucion"]);
                String DNIUSUARIO = Convert.ToString(Session["_usuNombreIngreso"].ToString());
                Int32 PageIndex = Convert.ToInt32(Session["EstadoCuentaPadres.PageIndex"]);
                //    GrillaCargar(PageIndex);
                dt = ocnAlumno.ObtenerUnoporDoc(DNIUSUARIO);
                if (dt.Rows.Count > 0)
                {

                    aluNombre.Text = Convert.ToString(dt.Rows[0]["Nombre"].ToString());
                    aluNombre.Enabled = false;
                    aludni.Text = Convert.ToString(dt.Rows[0]["Doc"].ToString());
                    aludni.Enabled = false;
                    lblaluId.Text = Convert.ToString(dt.Rows[0]["Id"].ToString());

                    Grilla.DataSource = null;
                    Grilla.DataBind();
                    AlumnoSeleccionado.Visible = true;
                    PageIndex = Convert.ToInt32(Session["EstadoCuenta.PageIndex"]);
                    GrillaCargar(PageIndex);


                }
                else
                {

                }
                //if (this.Session["_Autenticado"] == null) Response.Redirect("Login.aspx", true);
                int ban = 0;
                Session["Bandera"] = ban;
                #region PageIndex
                PageIndex = 0;
                int aluId1 = Convert.ToInt32(Request.QueryString["Id"]);

                alerError2.Visible = false;

                if (this.Session["EstadoCuenta.PageIndex"] == null)
                {
                    Session.Add("EstadoCuenta.PageIndex", 0);
                }

                else
                {
                    PageIndex = Convert.ToInt32(Session["EstadoCuenta.PageIndex"]);
                }
                #endregion
                //lblaluId.Text = "0";
                #region Variables de sesion para filtros
                //if (Session["LibroDisciplina.Nombre"] != null) { LibroDisciplina.Text = Session["LibroDisciplina.Nombre"].ToString(); } else { Session.Add("SexoConsulta.Nombre", Nombre.Text.Trim()); }
                #endregion
                //if (aluId1 != 0)
                //{
                //    lblaluId.Text = Convert.ToString(aluId1);
                //    PageIndex = Convert.ToInt32(Session["EstadoCuenta.PageIndex"]);
                //    GrillaCargar(PageIndex);
                //}
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

    private void GrillaCargar(int PageIndex)
    {
        try
        {
            DateTime FechaPago;
            DataTable dt9 = new DataTable();
            string dateString = Convert.ToString(DateTime.Now.ToShortDateString());
            FechaPago = DateTime.Parse(dateString);
            DateTime UltVto;
            Decimal ValorInteresCI = 0;

            alerError2.Visible = false;
            lblMjerror2.Text = "";
            btnImprimir2.Visible = false;
            lblCuotas.Visible = false;
            TexCuotas.Visible = false;
            lblInt.Visible = false;
            txtIntereses.Visible = false;
            txtTot.Visible = false;
            lblTot.Visible = false;
            DataTable dt = new DataTable();
            dt.Columns.Add("icoId", typeof(int));
            dt.Columns.Add("conId", typeof(Int32));
            dt.Columns.Add("cntId", typeof(Int32));
            dt.Columns.Add("TipoConcepto", typeof(String));
            dt.Columns.Add("Concepto", typeof(String));
            dt.Columns.Add("Importe", typeof(Decimal));
            dt.Columns.Add("ImporteInteres", typeof(Decimal));
            dt.Columns.Add("AnioLectivo", typeof(Decimal));
            dt.Columns.Add("Beca", typeof(String));
            dt.Columns.Add("BecId", typeof(Int32));
            dt.Columns.Add("NroCuota", typeof(Int32));
            dt.Columns.Add("FchInscripcion", typeof(String));
            dt.Columns.Add("FechaVto", typeof(String));
            dt.Columns.Add("ValorInteres", typeof(Decimal));
            dt.Columns.Add("ImpPagado", typeof(Decimal));
            dt.Columns.Add("FechaPago", typeof(String));
            dt.Columns.Add("NroCompbte", typeof(String));
            dt.Columns.Add("Curso", typeof(String));
            dt.Columns.Add("Contado", typeof(String));
            dt.Columns.Add("Tarjeta", typeof(String));
            dt.Columns.Add("Cheque", typeof(String));
            dt.Columns.Add("TranfElec", typeof(String));
            dt.Columns.Add("Colegio", typeof(String));
            dt.Columns.Add("insId", typeof(Int32));
            dt.Columns.Add("BecasInteres", typeof(Decimal));
            DataTable dtConc = new DataTable();

            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt6 = new DataTable();
            DataTable dt7 = new DataTable();

            // InscripcionCursado.ObtenerTodoporaluIdAnioCursado

            int Bandera;
            Session["EstadoCuenta.PageIndex"] = PageIndex;
            DateTime Vto = DateTime.Now;
            int ins_Id = 0;
            String Colegio = "";
            AlumnoSeleccionado.Visible = true;
            if (ckbDeuda.Checked == false)
            {
                if (txtAnioLectivo.Text == "") //traigo historial del alumno
                {
                    dt3 = ocnInscripcionCursado.ObtenerTodoporaluId(Convert.ToInt32(lblaluId.Text));// obtengo todos los cursos del alumno

                    foreach (DataRow row5 in dt3.Rows)// por cada curso..
                    {
                        icuId2 = Convert.ToInt32(row5["Id"].ToString());

                        ins_Id = Convert.ToInt32(row5["insId"].ToString());
                        Colegio = Convert.ToString(row5["Colegio"].ToString());

                        dt4 = ocnInscripcionConcepto.ObtenerUnoxicuId(icuId2); //tabla de conceptos para ese curso
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt4.Rows)
                            {
                                dt2 = ocnComprobantesDetalle.ObtenerUnoxicoId(Convert.ToInt32(row["Id"].ToString())); //Pago concepto?
                                dt5 = ocnConceptosIntereses.ObtenerInteresxconIdxNroCuota(Convert.ToInt32(row["conId"].ToString()), Convert.ToInt32(row["NroCuota"].ToString()));
                                if (dt5.Rows.Count > 0)
                                {
                                    Bandera = 0;// Para poner importe pagado a un solo vto.. 
                                    foreach (DataRow row2 in dt5.Rows)
                                    {
                                        Decimal ImporteBecado = 0;
                                        if (Bandera == 0)
                                        {
                                            if (dt2.Rows.Count > 0)// Es concepto Pagado
                                            {
                                                dt6 = ocnComprobantesCabecera.ObtenerUno(Convert.ToInt32(dt2.Rows[0]["cocId"].ToString()));
                                                dtConc = ocnConceptos.ObtenerUno(Convert.ToInt32(Convert.ToInt32(row["conId"].ToString())));
                                                DataRow row1 = dt.NewRow();
                                                row1["icoId"] = (Convert.ToInt32(row["Id"].ToString()));
                                                row1["conId"] = (Convert.ToInt32(row["conId"].ToString()));
                                                row1["cntId"] = (Convert.ToInt32(row2["cntId"].ToString()));
                                                row1["Concepto"] = Convert.ToString((row["Conceptos"].ToString()));
                                                row1["TipoConcepto"] = Convert.ToString((row2["TipoConcepto"].ToString()));
                                                row1["Concepto"] = Convert.ToString((row["Conceptos"].ToString()));
                                                row1["NroCuota"] = (Convert.ToInt32(row["NroCuota"].ToString()));
                                                ImporteBecado += (Convert.ToDecimal(row["Importe"].ToString()) - (Convert.ToDecimal(row["Importe"].ToString()) * Convert.ToDecimal(row["BecasInteres"].ToString()) / 100));
                                                row1["Importe"] = Convert.ToDecimal(string.Format("{0:0.00}", ImporteBecado));
                                                //row1["ImporteInteres"] = Convert.ToDecimal(dt2.Rows[0]["InteresporDiferencia"].ToString());
                                                //Single pp = Convert.ToSingle(dt2.Rows[0]["InteresporDiferencia"].ToString());
                                                //if (pp > 0)
                                                //{
                                                //    Single qq = pp;
                                                //}
                                                row1["ImporteInteres"] = Convert.ToDecimal(dt2.Rows[0]["cdeImporte"].ToString()) - Convert.ToDecimal(string.Format("{0:0.00}", ImporteBecado));

                                                //row1["ImporteInteres"] = 0;
                                                row1["AnioLectivo"] = Convert.ToInt32(row["AnioLectivo"].ToString());
                                                row1["Beca"] = Convert.ToString((row["Becas"].ToString()));
                                                row1["BecId"] = Convert.ToString((row["BecId"].ToString()));
                                                row1["FchInscripcion"] = Convert.ToString(row["FechaInscripcion"].ToString());
                                                row1["FechaVto"] = Convert.ToString(row2["FechaVto"].ToString());
                                                row1["ValorInteres"] = Convert.ToDecimal(row2["ValorInteres"].ToString());
                                                //row1["ImpPagado"] = Convert.ToDecimal(dt2.Rows[0]["Importe"].ToString());

                                                row1["ImpPagado"] = Convert.ToDecimal(dt2.Rows[0]["cdeImporte"].ToString());

                                                row1["FechaPago"] = Convert.ToString(dt6.Rows[0]["FechaPago"].ToString());
                                                row1["NroCompbte"] = Convert.ToString(dt6.Rows[0]["NroCompbte"].ToString());
                                                row1["Curso"] = Convert.ToString(row5["Curso"].ToString());

                                                row1["insId"] = ins_Id.ToString();
                                                row1["Colegio"] = Colegio;
                                                row1["BecasInteres"] = row["BecasInteres"].ToString();
                                                Bandera = 1;
                                                dt.Rows.Add(row1);
                                            }

                                            else //Concepto no pagado
                                            {
                                                dtConc = ocnConceptos.ObtenerUno(Convert.ToInt32(Convert.ToInt32(row["conId"].ToString())));
                                                DataRow row1 = dt.NewRow();
                                                row1["icoId"] = (Convert.ToInt32(row["Id"].ToString()));
                                                row1["conId"] = (Convert.ToInt32(row["conId"].ToString()));
                                                row1["cntId"] = (Convert.ToInt32(row2["cntId"].ToString()));
                                                row1["TipoConcepto"] = Convert.ToString((row2["TipoConcepto"].ToString()));
                                                row1["Concepto"] = Convert.ToString((row["Conceptos"].ToString()));
                                                row1["NroCuota"] = (Convert.ToInt32(row["NroCuota"].ToString()));
                                                ImporteBecado += (Convert.ToDecimal(row["Importe"].ToString()) - (Convert.ToDecimal(row["Importe"].ToString()) * Convert.ToDecimal(row["BecasInteres"].ToString()) / 100));
                                                row1["Importe"] = Convert.ToDecimal(string.Format("{0:0.00}", ImporteBecado));
                                                row1["ImporteInteres"] = Convert.ToDecimal(row2["ValorInteres"].ToString());
                                                row1["AnioLectivo"] = Convert.ToInt32(row["AnioLectivo"].ToString());
                                                row1["Beca"] = Convert.ToString((row["Becas"].ToString()));
                                                row1["BecId"] = Convert.ToString((row["BecId"].ToString()));
                                                row1["FchInscripcion"] = Convert.ToString(row["FechaInscripcion"].ToString());
                                                row1["FechaVto"] = Convert.ToString(row2["FechaVto"].ToString());
                                                row1["ValorInteres"] = Convert.ToDecimal(row2["ValorInteres"].ToString());
                                                row1["ImpPagado"] = 0;
                                                row1["FechaPago"] = "";
                                                row1["NroCompbte"] = "";
                                                row1["Curso"] = Convert.ToString(row5["Curso"].ToString());
                                                //row1["Colegio"] = Convert.ToString(dt3.Rows[0]["Colegio"].ToString());
                                                //int qqq = 0;
                                                //qqq = (Convert.ToInt32(dt3.Rows[0]["insId"].ToString()));
                                                // row1["insId"] = (Convert.ToInt32(dt3.Rows[0]["insId"].ToString()));

                                                //DateTime Vto;
                                                String AplicaBeca = "";
                                                float ValorInteres = 0;
                                                float Interes = 0;
                                                int Band = 0;
                                                foreach (DataRow rowi5 in dt5.Rows)
                                                {
                                                    if (Band == 0)
                                                    {
                                                        Vto = DateTime.Parse(rowi5["FechaVto"].ToString());
                                                        AplicaBeca = rowi5["coiAplicaBeca"].ToString();
                                                        // Ver el tema Beca
                                                        if (Convert.ToSingle(row["BecasInteres"].ToString()) > 0 && AplicaBeca == "1")
                                                        {
                                                            if (Convert.ToSingle(row["BecasInteres"].ToString()) < 100)
                                                            {
                                                                Interes = (Convert.ToSingle(row["BecasInteres"].ToString()) * Convert.ToSingle(rowi5["coiValorInteres"].ToString())) / 100;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Interes = Convert.ToSingle(rowi5["coiValorInteres"].ToString());
                                                        }

                                                        if (FechaPago <= Vto)
                                                        {
                                                            if (Convert.ToSingle(row["BecasInteres"].ToString()) > 0 && AplicaBeca == "1")
                                                            {
                                                                if (Convert.ToSingle(row["BecasInteres"].ToString()) < 100)
                                                                {
                                                                    ValorInteres = (Convert.ToSingle(row["BecasInteres"].ToString()) * Convert.ToSingle(rowi5["coiValorInteres"].ToString())) / 100;
                                                                    Band = 1;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ValorInteres = Convert.ToSingle(rowi5["coiValorInteres"].ToString());
                                                                Band = 1;
                                                            }
                                                        }
                                                    }
                                                }
                                                if (Interes > ValorInteres)
                                                {
                                                    ValorInteres = Interes;
                                                }


                                                int MesesAtrasados = 0;
                                                if (Convert.ToSingle(dtConc.Rows[0]["ConInteresMensual"].ToString()) > 0)
                                                {
                                                    if (Convert.ToSingle(row["ImporteConBeca"].ToString()) > 0)  // ImporteConBeca
                                                        if (DateTime.Now.Year > Vto.Year)
                                                        {
                                                            MesesAtrasados = GetMonthDifference(Vto.Date, DateTime.Now.Date); //(DateTime.Now.Date - Vto.Date).Days / (365.25 / 12);
                                                        }
                                                        else
                                                        {
                                                            //MesesAtrasados = MesesAtrasados + DateTime.Now.Date.Month - Convert.ToInt32(dtConc.Rows[0]["ConMesInicio"].ToString());
                                                            MesesAtrasados = MesesAtrasados + (DateTime.Now.Date.Month - (Convert.ToInt32(dtConc.Rows[0]["ConMesInicio"].ToString()) - 1)) - Convert.ToInt32(Convert.ToInt32(row["NroCuota"].ToString()));
                                                        }
                                                }
                                                Single InteresMensualaPagar = (((Convert.ToSingle(dtConc.Rows[0]["ConInteresMensual"].ToString()) * MesesAtrasados) / 100) * (Convert.ToSingle(dtConc.Rows[0]["ConImporte"].ToString()) + ValorInteres));

                                                if (InteresMensualaPagar > 0)
                                                {

                                                    if (Convert.ToDateTime(row2["FechaVto"].ToString()) > DateTime.Now)
                                                    {
                                                        ValorInteres = 0;
                                                    }
                                                    else
                                                    {
                                                        ValorInteres = ValorInteres + InteresMensualaPagar;
                                                    }
                                                }



                                                //ValorInteres = ValorInteres + InteresMensualaPagar;


                                                row1["ImporteInteres"] = Convert.ToDecimal(string.Format("{0:0.00}", ValorInteres));

                                                row1["BecasInteres"] = row["BecasInteres"].ToString();

                                                row1["Colegio"] = Colegio;
                                                row1["insId"] = ins_Id.ToString();
                                                dt.Rows.Add(row1);
                                                Bandera = 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else //seleccionó Año Lectivo
                {
                    dt3 = ocnInscripcionCursado.ObtenerTodoxaluIdxAnio(Convert.ToInt32(lblaluId.Text), Convert.ToInt32(txtAnioLectivo.Text));
                    if (dt3.Rows.Count > 0)
                    {
                        icuId2 = Convert.ToInt32(dt3.Rows[0]["Id"].ToString());
                        ins_Id = Convert.ToInt32(dt3.Rows[0]["insId"].ToString());
                        Colegio = Convert.ToString(dt3.Rows[0]["Colegio"].ToString());

                        dt4 = ocnInscripcionConcepto.ObtenerUnoxicuId(icuId2);
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt4.Rows)
                            {
                                dt2 = ocnComprobantesDetalle.ObtenerUnoxicoId(Convert.ToInt32(row["Id"].ToString()));
                                dt5 = ocnConceptosIntereses.ObtenerInteresxconIdxNroCuota(Convert.ToInt32(row["conId"].ToString()), Convert.ToInt32(row["NroCuota"].ToString()));
                                if (dt5.Rows.Count > 0)
                                {
                                    Bandera = 0;// Para poner importe pagado a uno.. 
                                    foreach (DataRow row2 in dt5.Rows)
                                    {

                                        Decimal ImporteBecado = 0;
                                        if (Bandera == 0)
                                        {
                                            if (dt2.Rows.Count > 0)
                                            {
                                                dt6 = ocnComprobantesCabecera.ObtenerUno(Convert.ToInt32(dt2.Rows[0]["cocId"].ToString()));
                                                dtConc = ocnConceptos.ObtenerUno(Convert.ToInt32(Convert.ToInt32(row["conId"].ToString())));
                                                DataRow row1 = dt.NewRow();
                                                row1["icoId"] = (Convert.ToInt32(row["Id"].ToString()));
                                                row1["conId"] = (Convert.ToInt32(row["conId"].ToString()));
                                                row1["cntId"] = (Convert.ToInt32(row2["cntId"].ToString()));
                                                row1["Concepto"] = Convert.ToString((row["Conceptos"].ToString()));
                                                row1["TipoConcepto"] = Convert.ToString((row2["TipoConcepto"].ToString()));
                                                row1["Concepto"] = Convert.ToString((row["Conceptos"].ToString()));
                                                row1["NroCuota"] = (Convert.ToInt32(row["NroCuota"].ToString()));
                                                ImporteBecado += (Convert.ToDecimal(row["Importe"].ToString()) - (Convert.ToDecimal(row["Importe"].ToString()) * Convert.ToDecimal(row["BecasInteres"].ToString()) / 100));
                                                row1["Importe"] = Convert.ToDecimal(string.Format("{0:0.00}", ImporteBecado));
                                                //row1["ImporteInteres"] = Convert.ToDecimal(row2["ValorInteres"].ToString());
                                                //row1["ImporteInteres"] = 0;
                                                row1["ImporteInteres"] = Convert.ToDecimal(dt2.Rows[0]["cdeImporte"].ToString()) - Convert.ToDecimal(string.Format("{0:0.00}", ImporteBecado));

                                                row1["AnioLectivo"] = Convert.ToInt32(row["AnioLectivo"].ToString());
                                                row1["Beca"] = Convert.ToString((row["Becas"].ToString()));
                                                row1["BecId"] = Convert.ToString((row["BecId"].ToString()));
                                                row1["FchInscripcion"] = Convert.ToString(row["FechaInscripcion"].ToString());
                                                row1["FechaVto"] = Convert.ToString(row2["FechaVto"].ToString());
                                                row1["ValorInteres"] = Convert.ToDecimal(row2["ValorInteres"].ToString());
                                                //row1["ImpPagado"] = Convert.ToDecimal(dt2.Rows[0]["Importe"].ToString());
                                                row1["ImpPagado"] = Convert.ToDecimal(dt2.Rows[0]["cdeImporte"].ToString());
                                                row1["FechaPago"] = Convert.ToString(dt6.Rows[0]["FechaPago"].ToString());
                                                row1["NroCompbte"] = Convert.ToString(dt6.Rows[0]["NroCompbte"].ToString());
                                                row1["Curso"] = Convert.ToString(dt3.Rows[0]["Curso"].ToString());
                                                row1["Colegio"] = Colegio; // Convert.ToString(dt3.Rows[0]["Colegio"].ToString());
                                                                           //Int32 pp = 0;
                                                                           // pp = (Convert.ToInt32(dt3.Rows[0]["insId"].ToString()));
                                                row1["insId"] = ins_Id.ToString(); //(Convert.ToInt32(dt3.Rows[0]["insId"].ToString()));
                                                Bandera = 1;
                                                dt.Rows.Add(row1);
                                            }
                                            else
                                            {
                                                DataRow row1 = dt.NewRow();
                                                dtConc = ocnConceptos.ObtenerUno(Convert.ToInt32(Convert.ToInt32(row["conId"].ToString())));
                                                row1["icoId"] = (Convert.ToInt32(row["Id"].ToString()));
                                                row1["conId"] = (Convert.ToInt32(row["conId"].ToString()));
                                                row1["cntId"] = (Convert.ToInt32(row2["cntId"].ToString()));
                                                row1["TipoConcepto"] = Convert.ToString((row2["TipoConcepto"].ToString()));
                                                row1["Concepto"] = Convert.ToString((row["Conceptos"].ToString()));
                                                row1["NroCuota"] = (Convert.ToInt32(row["NroCuota"].ToString()));
                                                ImporteBecado += (Convert.ToDecimal(row["Importe"].ToString()) - (Convert.ToDecimal(row["Importe"].ToString()) * Convert.ToDecimal(row["BecasInteres"].ToString()) / 100));
                                                row1["Importe"] = Convert.ToDecimal(string.Format("{0:0.00}", ImporteBecado));
                                                row1["ImporteInteres"] = Convert.ToDecimal(row2["ValorInteres"].ToString());
                                                row1["AnioLectivo"] = Convert.ToInt32(row["AnioLectivo"].ToString());
                                                row1["Beca"] = Convert.ToString((row["Becas"].ToString()));
                                                row1["BecId"] = Convert.ToString((row["BecId"].ToString()));
                                                row1["FchInscripcion"] = Convert.ToString(row["FechaInscripcion"].ToString());
                                                row1["FechaVto"] = Convert.ToString(row2["FechaVto"].ToString());
                                                row1["ValorInteres"] = Convert.ToDecimal(row2["ValorInteres"].ToString());
                                                row1["ImpPagado"] = 0;
                                                row1["FechaPago"] = "";
                                                row1["NroCompbte"] = "";
                                                row1["Curso"] = Convert.ToString(dt3.Rows[0]["Curso"].ToString());
                                                row1["Colegio"] = Colegio;
                                                //Int32 ppp = 0;
                                                //ppp = (Convert.ToInt32(dt3.Rows[0]["insId"].ToString()));
                                                row1["insId"] = ins_Id.ToString();

                                                //DateTime Vto;
                                                String AplicaBeca = "";
                                                //DateTime Vto = DateTime.Now;
                                                float ValorInteres = 0;
                                                float Interes = 0;
                                                int Band = 0;
                                                foreach (DataRow rowi5 in dt5.Rows)
                                                {
                                                    if (Band == 0)
                                                    {
                                                        Vto = DateTime.Parse(rowi5["FechaVto"].ToString());
                                                        AplicaBeca = rowi5["coiAplicaBeca"].ToString();
                                                        // Ver el tema Beca
                                                        if (Convert.ToSingle(row["BecasInteres"].ToString()) > 0 && AplicaBeca == "1")
                                                        {
                                                            if (Convert.ToSingle(row["BecasInteres"].ToString()) < 100)
                                                            {
                                                                Interes = (Convert.ToSingle(row["BecasInteres"].ToString()) * Convert.ToSingle(rowi5["coiValorInteres"].ToString())) / 100;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Interes = Convert.ToSingle(rowi5["coiValorInteres"].ToString());
                                                        }

                                                        if (FechaPago <= Vto)
                                                        {
                                                            if (Convert.ToSingle(row["BecasInteres"].ToString()) > 0 && AplicaBeca == "1")
                                                            {
                                                                if (Convert.ToSingle(row["BecasInteres"].ToString()) < 100)
                                                                {
                                                                    ValorInteres = (Convert.ToSingle(row["BecasInteres"].ToString()) * Convert.ToSingle(rowi5["coiValorInteres"].ToString())) / 100;
                                                                    Band = 1;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ValorInteres = Convert.ToSingle(rowi5["coiValorInteres"].ToString());
                                                                Band = 1;
                                                            }
                                                        }
                                                    }
                                                }
                                                if (Interes > ValorInteres)
                                                {
                                                    ValorInteres = Interes;
                                                }


                                                int MesesAtrasados = 0;
                                                if (Convert.ToSingle(dtConc.Rows[0]["ConInteresMensual"].ToString()) > 0)
                                                {
                                                    if (Convert.ToSingle(row["ImporteConBeca"].ToString()) > 0)  // ImporteConBeca
                                                        if (DateTime.Now.Year > Vto.Year)
                                                        {
                                                            MesesAtrasados = GetMonthDifference(Vto.Date, DateTime.Now.Date); //(DateTime.Now.Date - Vto.Date).Days / (365.25 / 12);
                                                        }
                                                        else
                                                        {
                                                            //MesesAtrasados = MesesAtrasados + DateTime.Now.Date.Month - Convert.ToInt32(dtConc.Rows[0]["ConMesInicio"].ToString());
                                                            MesesAtrasados = MesesAtrasados + (DateTime.Now.Date.Month - (Convert.ToInt32(dtConc.Rows[0]["ConMesInicio"].ToString()) - 1)) - Convert.ToInt32(Convert.ToInt32(row["NroCuota"].ToString()));
                                                        }
                                                }
                                                Single InteresMensualaPagar = (((Convert.ToSingle(dtConc.Rows[0]["ConInteresMensual"].ToString()) * MesesAtrasados) / 100) * (Convert.ToSingle(dtConc.Rows[0]["ConImporte"].ToString()) + ValorInteres));


                                                if (InteresMensualaPagar > 0)
                                                {

                                                    if (Convert.ToDateTime(row2["FechaVto"].ToString()) > DateTime.Now)
                                                    {
                                                        ValorInteres = 0;
                                                    }
                                                    else
                                                    {
                                                        ValorInteres = ValorInteres + InteresMensualaPagar;
                                                    }
                                                }



                                                //ValorInteres = ValorInteres + InteresMensualaPagar;




                                                row1["ImporteInteres"] = Convert.ToDecimal(string.Format("{0:0.00}", ValorInteres));

                                                row1["BecasInteres"] = row["BecasInteres"].ToString();

                                                dt.Rows.Add(row1);
                                                Bandera = 1;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            GrillaHistorial.DataSource = null;
                            GrillaHistorial.DataBind();
                            lblPagado.Visible = false;
                            lblVencido.Visible = false;
                            txtcELESTE.Visible = false;
                            txtRojo.Visible = false;

                            alerError2.Visible = true;
                            lblError2.Text = "No se encontraron registros en ese año lectivo..";
                            return;
                        }
                    }
                    else
                    {
                        DataTable dt8 = new DataTable();
                        this.GrillaHistorial.DataSource = dt8;
                        this.GrillaHistorial.PageIndex = PageIndex;
                        this.GrillaHistorial.DataBind();
                        lblPagado.Visible = false;
                        lblVencido.Visible = false;
                        txtcELESTE.Visible = false;
                        txtRojo.Visible = false;

                        if (lblaluId.Text == "")
                        {
                            alerError2.Visible = true;
                            lblError2.Text = "Debe buscar y seleccionar un alumno";

                        }
                        else
                        {
                            alerError2.Visible = true;
                            lblError2.Text = "No se encontró registro";
                        }
                        return;
                    }
                }
            }
            else
            {
                if (txtAnioLectivo.Text == "") //traigo historial del alumno
                {
                    dt3 = ocnInscripcionCursado.ObtenerTodoporaluId(Convert.ToInt32(lblaluId.Text));// obtengo todos los cursos del alumno

                    foreach (DataRow row5 in dt3.Rows)// por cada curso..
                    {
                        icuId2 = Convert.ToInt32(row5["Id"].ToString());

                        ins_Id = Convert.ToInt32(row5["insId"].ToString());
                        Colegio = Convert.ToString(row5["Colegio"].ToString());

                        dt4 = ocnInscripcionConcepto.ObtenerUnoxicuIdSinBecas100(icuId2); //tabla de conceptos para ese curso
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt4.Rows)
                            {
                                dt2 = ocnComprobantesDetalle.ObtenerUnoxicoId(Convert.ToInt32(row["Id"].ToString())); //Pago concepto?
                                dt5 = ocnConceptosIntereses.ObtenerInteresxconIdxNroCuota(Convert.ToInt32(row["conId"].ToString()), Convert.ToInt32(row["NroCuota"].ToString()));
                                if (dt5.Rows.Count > 0)
                                {
                                    Bandera = 0;// Para poner importe pagado a un solo vto.. 
                                    foreach (DataRow row2 in dt5.Rows)
                                    {
                                        Decimal ImporteBecado = 0;
                                        if (Bandera == 0)
                                        {    //|| Convert.ToDecimal(row["BecasInteres"].ToString()) != 100 
                                            if (dt2.Rows.Count > 0)// Es concepto Pagado
                                            {
                                                dt6 = ocnComprobantesCabecera.ObtenerUno(Convert.ToInt32(dt2.Rows[0]["cocId"].ToString()));
                                                dtConc = ocnConceptos.ObtenerUno(Convert.ToInt32(Convert.ToInt32(row["conId"].ToString())));
                                                DataRow row1 = dt.NewRow();
                                                row1["icoId"] = (Convert.ToInt32(row["Id"].ToString()));
                                                row1["conId"] = (Convert.ToInt32(row["conId"].ToString()));
                                                row1["cntId"] = (Convert.ToInt32(row2["cntId"].ToString()));
                                                row1["Concepto"] = Convert.ToString((row["Conceptos"].ToString()));
                                                row1["TipoConcepto"] = Convert.ToString((row2["TipoConcepto"].ToString()));
                                                row1["Concepto"] = Convert.ToString((row["Conceptos"].ToString()));
                                                row1["NroCuota"] = (Convert.ToInt32(row["NroCuota"].ToString()));
                                                ImporteBecado += (Convert.ToDecimal(row["Importe"].ToString()) - (Convert.ToDecimal(row["Importe"].ToString()) * Convert.ToDecimal(row["BecasInteres"].ToString()) / 100));
                                                row1["Importe"] = Convert.ToDecimal(string.Format("{0:0.00}", ImporteBecado));
                                                //row1["ImporteInteres"] = Convert.ToDecimal(row2["ValorInteres"].ToString());
                                                row1["ImporteInteres"] = Convert.ToDecimal(dt2.Rows[0]["cdeImporte"].ToString()) - Convert.ToDecimal(string.Format("{0:0.00}", ImporteBecado));

                                                //row1["ImporteInteres"] = 0;
                                                row1["AnioLectivo"] = Convert.ToInt32(row["AnioLectivo"].ToString());
                                                row1["Beca"] = Convert.ToString((row["Becas"].ToString()));
                                                row1["BecId"] = Convert.ToString((row["BecId"].ToString()));
                                                row1["FchInscripcion"] = Convert.ToString(row["FechaInscripcion"].ToString());
                                                row1["FechaVto"] = Convert.ToString(row2["FechaVto"].ToString());
                                                row1["ValorInteres"] = Convert.ToDecimal(row2["ValorInteres"].ToString());
                                                //row1["ImpPagado"] = Convert.ToDecimal(dt2.Rows[0]["Importe"].ToString());

                                                row1["ImpPagado"] = Convert.ToDecimal(dt2.Rows[0]["cdeImporte"].ToString());



                                                row1["FechaPago"] = Convert.ToString(dt6.Rows[0]["FechaPago"].ToString());
                                                row1["NroCompbte"] = Convert.ToString(dt6.Rows[0]["NroCompbte"].ToString());
                                                row1["Curso"] = Convert.ToString(row5["Curso"].ToString());

                                                //row1["insId"] = (Convert.ToInt32(dt2.Rows[0]["insId"].ToString()));
                                                row1["insId"] = ins_Id.ToString();
                                                row1["Colegio"] = Colegio;
                                                row1["BecasInteres"] = row["BecasInteres"].ToString();
                                                Bandera = 1;
                                                //dt.Rows.Add(row1);
                                            }

                                            else //Concepto no pagado
                                            {
                                                DataRow row1 = dt.NewRow();
                                                dtConc = ocnConceptos.ObtenerUno(Convert.ToInt32(Convert.ToInt32(row["conId"].ToString())));
                                                row1["icoId"] = (Convert.ToInt32(row["Id"].ToString()));
                                                row1["conId"] = (Convert.ToInt32(row["conId"].ToString()));
                                                row1["cntId"] = (Convert.ToInt32(row2["cntId"].ToString()));
                                                row1["TipoConcepto"] = Convert.ToString((row2["TipoConcepto"].ToString()));
                                                row1["Concepto"] = Convert.ToString((row["Conceptos"].ToString()));
                                                row1["NroCuota"] = (Convert.ToInt32(row["NroCuota"].ToString()));
                                                ImporteBecado += (Convert.ToDecimal(row["Importe"].ToString()) - (Convert.ToDecimal(row["Importe"].ToString()) * Convert.ToDecimal(row["BecasInteres"].ToString()) / 100));
                                                row1["Importe"] = Convert.ToDecimal(string.Format("{0:0.00}", ImporteBecado));
                                                row1["ImporteInteres"] = Convert.ToDecimal(row2["ValorInteres"].ToString());
                                                row1["AnioLectivo"] = Convert.ToInt32(row["AnioLectivo"].ToString());
                                                row1["Beca"] = Convert.ToString((row["Becas"].ToString()));
                                                row1["BecId"] = Convert.ToString((row["BecId"].ToString()));
                                                row1["FchInscripcion"] = Convert.ToString(row["FechaInscripcion"].ToString());
                                                row1["FechaVto"] = Convert.ToString(row2["FechaVto"].ToString());
                                                row1["ValorInteres"] = Convert.ToDecimal(row2["ValorInteres"].ToString());
                                                row1["ImpPagado"] = 0;
                                                row1["FechaPago"] = "";
                                                row1["NroCompbte"] = "";
                                                row1["Curso"] = Convert.ToString(row5["Curso"].ToString());
                                                //row1["Colegio"] = Convert.ToString(dt3.Rows[0]["Colegio"].ToString());
                                                //int qqq = 0;
                                                //qqq = (Convert.ToInt32(dt3.Rows[0]["insId"].ToString()));
                                                // row1["insId"] = (Convert.ToInt32(dt3.Rows[0]["insId"].ToString()));
                                                row1["BecasInteres"] = row["BecasInteres"].ToString();
                                                //DateTime Vto;
                                                String AplicaBeca = "";
                                                float ValorInteres = 0;
                                                float Interes = 0;
                                                int Band = 0;
                                                foreach (DataRow rowi5 in dt5.Rows)
                                                {
                                                    if (Band == 0)
                                                    {
                                                        Vto = DateTime.Parse(rowi5["FechaVto"].ToString());
                                                        AplicaBeca = rowi5["coiAplicaBeca"].ToString();
                                                        // Ver el tema Beca
                                                        if (Convert.ToSingle(row["BecasInteres"].ToString()) > 0 && AplicaBeca == "1")
                                                        {
                                                            if (Convert.ToSingle(row["BecasInteres"].ToString()) < 100)
                                                            {
                                                                Interes = (Convert.ToSingle(row["BecasInteres"].ToString()) * Convert.ToSingle(rowi5["coiValorInteres"].ToString())) / 100;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Interes = Convert.ToSingle(rowi5["coiValorInteres"].ToString());
                                                        }

                                                        if (FechaPago <= Vto)
                                                        {
                                                            if (Convert.ToSingle(row["BecasInteres"].ToString()) > 0 && AplicaBeca == "1")
                                                            {
                                                                if (Convert.ToSingle(row["BecasInteres"].ToString()) < 100)
                                                                {
                                                                    ValorInteres = (Convert.ToSingle(row["BecasInteres"].ToString()) * Convert.ToSingle(rowi5["coiValorInteres"].ToString())) / 100;
                                                                    Band = 1;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ValorInteres = Convert.ToSingle(rowi5["coiValorInteres"].ToString());
                                                                Band = 1;
                                                            }
                                                        }
                                                    }
                                                }
                                                if (Interes > ValorInteres)
                                                {
                                                    ValorInteres = Interes;
                                                }

                                                int MesesAtrasados = 0;
                                                if (Convert.ToSingle(dtConc.Rows[0]["ConInteresMensual"].ToString()) > 0)
                                                {
                                                    if (Convert.ToSingle(row["ImporteConBeca"].ToString()) > 0)  // ImporteConBeca
                                                        if (DateTime.Now.Year > Vto.Year)
                                                        {
                                                            MesesAtrasados = GetMonthDifference(Vto.Date, DateTime.Now.Date); //(DateTime.Now.Date - Vto.Date).Days / (365.25 / 12);
                                                        }
                                                        else
                                                        {
                                                            //MesesAtrasados = MesesAtrasados + DateTime.Now.Date.Month - Convert.ToInt32(dtConc.Rows[0]["ConMesInicio"].ToString());
                                                            MesesAtrasados = MesesAtrasados + (DateTime.Now.Date.Month - (Convert.ToInt32(dtConc.Rows[0]["ConMesInicio"].ToString()) - 1)) - Convert.ToInt32(Convert.ToInt32(row["NroCuota"].ToString()));
                                                        }
                                                }
                                                Single InteresMensualaPagar = (((Convert.ToSingle(dtConc.Rows[0]["ConInteresMensual"].ToString()) * MesesAtrasados) / 100) * (Convert.ToSingle(dtConc.Rows[0]["ConImporte"].ToString()) + ValorInteres));


                                                if (InteresMensualaPagar > 0)
                                                {

                                                    if (Convert.ToDateTime(row2["FechaVto"].ToString()) > DateTime.Now)
                                                    {
                                                        ValorInteres = 0;
                                                    }
                                                    else
                                                    {
                                                        ValorInteres = ValorInteres + InteresMensualaPagar;
                                                    }
                                                }


                                                //ValorInteres = ValorInteres + InteresMensualaPagar;



                                                row1["ImporteInteres"] = Convert.ToDecimal(string.Format("{0:0.00}", ValorInteres));


                                                row1["Colegio"] = Colegio;
                                                row1["insId"] = ins_Id.ToString();
                                                dt.Rows.Add(row1);
                                                Bandera = 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else //seleccionó Año Lectivo
                {
                    dt3 = ocnInscripcionCursado.ObtenerTodoxaluIdxAnio(Convert.ToInt32(lblaluId.Text), Convert.ToInt32(txtAnioLectivo.Text));
                    if (dt3.Rows.Count > 0)
                    {
                        icuId2 = Convert.ToInt32(dt3.Rows[0]["Id"].ToString());
                        ins_Id = Convert.ToInt32(dt3.Rows[0]["insId"].ToString());
                        Colegio = Convert.ToString(dt3.Rows[0]["Colegio"].ToString());

                        dt4 = ocnInscripcionConcepto.ObtenerUnoxicuIdSinBecas100(icuId2);
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt4.Rows)
                            {
                                dt2 = ocnComprobantesDetalle.ObtenerUnoxicoId(Convert.ToInt32(row["Id"].ToString()));
                                dt5 = ocnConceptosIntereses.ObtenerInteresxconIdxNroCuota(Convert.ToInt32(row["conId"].ToString()), Convert.ToInt32(row["NroCuota"].ToString()));
                                if (dt5.Rows.Count > 0)
                                {
                                    Bandera = 0;// Para poner importe pagado a uno.. 
                                    foreach (DataRow row2 in dt5.Rows)
                                    {

                                        Decimal ImporteBecado = 0;
                                        if (Bandera == 0)
                                        {
                                            if (dt2.Rows.Count > 0)
                                            {
                                                dtConc = ocnConceptos.ObtenerUno(Convert.ToInt32(Convert.ToInt32(row["conId"].ToString())));
                                                dt6 = ocnComprobantesCabecera.ObtenerUno(Convert.ToInt32(dt2.Rows[0]["cocId"].ToString()));
                                                DataRow row1 = dt.NewRow();
                                                row1["icoId"] = (Convert.ToInt32(row["Id"].ToString()));
                                                row1["conId"] = (Convert.ToInt32(row["conId"].ToString()));
                                                row1["cntId"] = (Convert.ToInt32(row2["cntId"].ToString()));
                                                row1["Concepto"] = Convert.ToString((row["Conceptos"].ToString()));
                                                row1["TipoConcepto"] = Convert.ToString((row2["TipoConcepto"].ToString()));
                                                row1["Concepto"] = Convert.ToString((row["Conceptos"].ToString()));
                                                row1["NroCuota"] = (Convert.ToInt32(row["NroCuota"].ToString()));
                                                ImporteBecado += (Convert.ToDecimal(row["Importe"].ToString()) - (Convert.ToDecimal(row["Importe"].ToString()) * Convert.ToDecimal(row["BecasInteres"].ToString()) / 100));
                                                row1["Importe"] = Convert.ToDecimal(string.Format("{0:0.00}", ImporteBecado));
                                                //row1["ImporteInteres"] = Convert.ToDecimal(row2["ValorInteres"].ToString());
                                                //row1["ImporteInteres"] = 0;
                                                row1["ImporteInteres"] = Convert.ToDecimal(dt2.Rows[0]["cdeImporte"].ToString()) - Convert.ToDecimal(string.Format("{0:0.00}", ImporteBecado));

                                                row1["AnioLectivo"] = Convert.ToInt32(row["AnioLectivo"].ToString());
                                                row1["Beca"] = Convert.ToString((row["Becas"].ToString()));
                                                row1["BecId"] = Convert.ToString((row["BecId"].ToString()));
                                                row1["FchInscripcion"] = Convert.ToString(row["FechaInscripcion"].ToString());
                                                row1["FechaVto"] = Convert.ToString(row2["FechaVto"].ToString());
                                                row1["ValorInteres"] = Convert.ToDecimal(row2["ValorInteres"].ToString());
                                                //row1["ImpPagado"] = Convert.ToDecimal(dt2.Rows[0]["Importe"].ToString());
                                                row1["ImpPagado"] = Convert.ToDecimal(dt2.Rows[0]["cdeImporte"].ToString());
                                                row1["FechaPago"] = Convert.ToString(dt6.Rows[0]["FechaPago"].ToString());
                                                row1["NroCompbte"] = Convert.ToString(dt6.Rows[0]["NroCompbte"].ToString());
                                                row1["Curso"] = Convert.ToString(dt3.Rows[0]["Curso"].ToString());
                                                row1["Colegio"] = Colegio; // Convert.ToString(dt3.Rows[0]["Colegio"].ToString());
                                                                           //Int32 pp = 0;
                                                                           // pp = (Convert.ToInt32(dt3.Rows[0]["insId"].ToString()));
                                                row1["insId"] = ins_Id.ToString(); //(Convert.ToInt32(dt3.Rows[0]["insId"].ToString()));
                                                row1["BecasInteres"] = row["BecasInteres"].ToString();
                                                Bandera = 1;
                                                //dt.Rows.Add(row1);
                                            }
                                            else
                                            {
                                                DataRow row1 = dt.NewRow();
                                                dtConc = ocnConceptos.ObtenerUno(Convert.ToInt32(Convert.ToInt32(row["conId"].ToString())));
                                                row1["icoId"] = (Convert.ToInt32(row["Id"].ToString()));
                                                row1["conId"] = (Convert.ToInt32(row["conId"].ToString()));
                                                row1["cntId"] = (Convert.ToInt32(row2["cntId"].ToString()));
                                                row1["TipoConcepto"] = Convert.ToString((row2["TipoConcepto"].ToString()));
                                                row1["Concepto"] = Convert.ToString((row["Conceptos"].ToString()));
                                                row1["NroCuota"] = (Convert.ToInt32(row["NroCuota"].ToString()));
                                                ImporteBecado += (Convert.ToDecimal(row["Importe"].ToString()) - (Convert.ToDecimal(row["Importe"].ToString()) * Convert.ToDecimal(row["BecasInteres"].ToString()) / 100));
                                                row1["Importe"] = Convert.ToDecimal(string.Format("{0:0.00}", ImporteBecado));
                                                row1["ImporteInteres"] = Convert.ToDecimal(row2["ValorInteres"].ToString());
                                                row1["AnioLectivo"] = Convert.ToInt32(row["AnioLectivo"].ToString());
                                                row1["Beca"] = Convert.ToString((row["Becas"].ToString()));
                                                row1["BecId"] = Convert.ToString((row["BecId"].ToString()));
                                                row1["FchInscripcion"] = Convert.ToString(row["FechaInscripcion"].ToString());
                                                row1["FechaVto"] = Convert.ToString(row2["FechaVto"].ToString());
                                                row1["ValorInteres"] = Convert.ToDecimal(row2["ValorInteres"].ToString());
                                                row1["ImpPagado"] = 0;
                                                row1["FechaPago"] = "";
                                                row1["NroCompbte"] = "";
                                                row1["Curso"] = Convert.ToString(dt3.Rows[0]["Curso"].ToString());
                                                row1["Colegio"] = Colegio;
                                                row1["BecasInteres"] = row["BecasInteres"].ToString();
                                                //Int32 ppp = 0;
                                                //ppp = (Convert.ToInt32(dt3.Rows[0]["insId"].ToString()));

                                                //DateTime Vto;
                                                String AplicaBeca = "";
                                                float ValorInteres = 0;
                                                float Interes = 0;
                                                int Band = 0;
                                                foreach (DataRow rowi5 in dt5.Rows)
                                                {
                                                    if (Band == 0)
                                                    {
                                                        Vto = DateTime.Parse(rowi5["FechaVto"].ToString());
                                                        AplicaBeca = rowi5["coiAplicaBeca"].ToString();
                                                        // Ver el tema Beca
                                                        if (Convert.ToSingle(row["BecasInteres"].ToString()) > 0 && AplicaBeca == "1")
                                                        {
                                                            if (Convert.ToSingle(row["BecasInteres"].ToString()) < 100)
                                                            {
                                                                Interes = (Convert.ToSingle(row["BecasInteres"].ToString()) * Convert.ToSingle(rowi5["coiValorInteres"].ToString())) / 100;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Interes = Convert.ToSingle(rowi5["coiValorInteres"].ToString());
                                                        }

                                                        if (FechaPago <= Vto)
                                                        {
                                                            if (Convert.ToSingle(row["BecasInteres"].ToString()) > 0 && AplicaBeca == "1")
                                                            {
                                                                if (Convert.ToSingle(row["BecasInteres"].ToString()) < 100)
                                                                {
                                                                    ValorInteres = (Convert.ToSingle(row["BecasInteres"].ToString()) * Convert.ToSingle(rowi5["coiValorInteres"].ToString())) / 100;
                                                                    Band = 1;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ValorInteres = Convert.ToSingle(rowi5["coiValorInteres"].ToString());
                                                                Band = 1;
                                                            }
                                                        }
                                                    }
                                                }
                                                if (Interes > ValorInteres)
                                                {
                                                    ValorInteres = Interes;
                                                }

                                                int MesesAtrasados = 0;
                                                if (Convert.ToSingle(dtConc.Rows[0]["ConInteresMensual"].ToString()) > 0)
                                                {
                                                    if (Convert.ToSingle(row["ImporteConBeca"].ToString()) > 0)  // ImporteConBeca
                                                        if (DateTime.Now.Year > Vto.Year)
                                                        {
                                                            MesesAtrasados = GetMonthDifference(Vto.Date, DateTime.Now.Date); //(DateTime.Now.Date - Vto.Date).Days / (365.25 / 12);
                                                        }
                                                        else
                                                        {
                                                            //MesesAtrasados = MesesAtrasados + DateTime.Now.Date.Month - Convert.ToInt32(dtConc.Rows[0]["ConMesInicio"].ToString());
                                                            MesesAtrasados = MesesAtrasados + (DateTime.Now.Date.Month - (Convert.ToInt32(dtConc.Rows[0]["ConMesInicio"].ToString()) - 1)) - Convert.ToInt32(Convert.ToInt32(row["NroCuota"].ToString()));
                                                        }
                                                }
                                                Single InteresMensualaPagar = (((Convert.ToSingle(dtConc.Rows[0]["ConInteresMensual"].ToString()) * MesesAtrasados) / 100) * (Convert.ToSingle(dtConc.Rows[0]["ConImporte"].ToString()) + ValorInteres));

                                                if (InteresMensualaPagar > 0)
                                                {

                                                    if (Convert.ToDateTime(row2["FechaVto"].ToString()) > DateTime.Now)
                                                    {
                                                        ValorInteres = 0;
                                                    }
                                                    else
                                                    {
                                                        ValorInteres = ValorInteres + InteresMensualaPagar;
                                                    }
                                                }



                                                //ValorInteres = ValorInteres + InteresMensualaPagar;


                                                row1["ImporteInteres"] = Convert.ToDecimal(string.Format("{0:0.00}", ValorInteres));


                                                row1["insId"] = ins_Id.ToString();
                                                dt.Rows.Add(row1);
                                                Bandera = 1;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            GrillaHistorial.DataSource = null;
                            GrillaHistorial.DataBind();
                            lblPagado.Visible = false;
                            lblVencido.Visible = false;
                            txtcELESTE.Visible = false;
                            txtRojo.Visible = false;

                            alerError2.Visible = true;
                            lblError2.Text = "No se encontraron registros en ese año lectivo..";
                            return;
                        }
                    }
                    else
                    {
                        DataTable dt8 = new DataTable();
                        this.GrillaHistorial.DataSource = dt8;
                        this.GrillaHistorial.PageIndex = PageIndex;
                        this.GrillaHistorial.DataBind();
                        lblPagado.Visible = false;
                        lblVencido.Visible = false;
                        txtcELESTE.Visible = false;
                        txtRojo.Visible = false;

                        if (lblaluId.Text == "")
                        {
                            alerError2.Visible = true;
                            lblError2.Text = "Debe buscar y seleccionar un alumno";

                        }
                        else
                        {
                            alerError2.Visible = true;
                            lblError2.Text = "No se encontró registro";
                        }
                        return;
                    }
                }
            }

            if (dt.Rows.Count != 0)
            {
                Decimal TotAdeuda = 0;
                Decimal txtInt = 0;
                this.GrillaHistorial.DataSource = dt;
                this.GrillaHistorial.PageIndex = PageIndex;
                this.GrillaHistorial.DataBind();
                int CuotaUltPagada = 0;
                int NroCuotaCon2;
                DateTime Hoy = DateTime.Today;
                String FchaInscripcionCon2;
                DataTable dtFilaRoja = new DataTable();

                dtFilaRoja.Columns.Add("icuId", typeof(int));
                dtFilaRoja.Columns.Add("icoId", typeof(int));
                dtFilaRoja.Columns.Add("cntId", typeof(int));
                dtFilaRoja.Columns.Add("conId", typeof(Int32));
                dtFilaRoja.Columns.Add("TipoConcepto", typeof(String));
                dtFilaRoja.Columns.Add("Concepto", typeof(String));
                dtFilaRoja.Columns.Add("Importe", typeof(Decimal));
                dtFilaRoja.Columns.Add("ImporteInteres", typeof(Decimal));
                dtFilaRoja.Columns.Add("AnioLectivo", typeof(Decimal));
                dtFilaRoja.Columns.Add("Beca", typeof(String));
                dtFilaRoja.Columns.Add("BecId", typeof(Int32));
                dtFilaRoja.Columns.Add("NroCuota", typeof(Int32));
                dtFilaRoja.Columns.Add("FchInscripcion", typeof(String));

                //int pp = Convert.ToInt32(GrillaHistorial.Rows.ToString());
                int pp = GrillaHistorial.Rows.Count;

                foreach (GridViewRow row in GrillaHistorial.Rows)
                {
                    if (Convert.ToDecimal(GrillaHistorial.DataKeys[row.RowIndex].Values["ImpPagado"]) != 0 || Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["BecId"]) == 14) //
                    {
                        row.BackColor = System.Drawing.Color.LightBlue;
                        CuotaUltPagada = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["NroCuota"]);
                        //((CheckBox)row.FindControl("chkSeleccion")).Enabled = false;
                    }

                    else
                    {
                        if (Convert.ToDateTime(GrillaHistorial.DataKeys[row.RowIndex].Values["FechaVto"]) < Hoy)
                        {
                            if (Convert.ToSingle(GrillaHistorial.DataKeys[row.RowIndex].Values["BecasInteres"].ToString()) != 100)
                            {
                                row.BackColor = System.Drawing.Color.FromName("#B81822");
                                row.ForeColor = System.Drawing.Color.White;
                                //btnFacturar.Visible = true;
                                //txtFchPago.Visible = true;
                                //lblFchPago.Visible = true;
                                lblPagado.Visible = true;
                                lblVencido.Visible = true;
                                txtcELESTE.Visible = true;
                                txtRojo.Visible = true;
                                btnImprimir2.Visible = true;
                                lblCuotas.Visible = true;
                                TexCuotas.Visible = true;
                                lblInt.Visible = true;
                                txtIntereses.Visible = true;
                                txtTot.Visible = true;
                                lblTot.Visible = true;

                                TotAdeuda = TotAdeuda + Convert.ToDecimal(GrillaHistorial.DataKeys[row.RowIndex].Values["Importe"]);

                                Int32 bcaIdCon;
                                Int32 AnioCursado;
                                AnioCursado = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["AnioLectivo"]);
                                insId = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["insId"]);
                                dt3 = ocnInscripcionCursado.ObtenerTodoxInsIdxaluIdxAnioCursado(insId, AnioCursado, Convert.ToInt32(lblaluId.Text));
                                icuId2 = Convert.ToInt32(dt3.Rows[0]["Id"].ToString());
                                FchaInscripcionCon2 = Convert.ToString(GrillaHistorial.DataKeys[row.RowIndex].Values["FchInscripcion"]);
                                NroCuotaCon2 = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["NroCuota"]);
                                bcaIdCon = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["BecasId"]);
                                DataRow row1 = dtFilaRoja.NewRow();
                                row1["icuId"] = icuId2;
                                row1["icoId"] = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["icoId"]);
                                row1["cntId"] = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["cntId"]);
                                row1["conId"] = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["conId"]);
                                row1["TipoConcepto"] = Convert.ToString(GrillaHistorial.DataKeys[row.RowIndex].Values["TipoConcepto"]);
                                row1["Concepto"] = Convert.ToString(GrillaHistorial.DataKeys[row.RowIndex].Values["Concepto"]);
                                row1["Importe"] = Convert.ToDecimal(GrillaHistorial.DataKeys[row.RowIndex].Values["Importe"]);
                                //row1["ImporteInteres"] = Convert.ToDecimal(GrillaHistorial.DataKeys[row.RowIndex].Values["ValorInteres"]);

                                row1["AnioLectivo"] = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["AnioLectivo"]);
                                row1["Beca"] = Convert.ToString(GrillaHistorial.DataKeys[row.RowIndex].Values["Beca"]);
                                row1["BecId"] = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["BecId"]);
                                row1["NroCuota"] = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["NroCuota"]);

                                row1["FchInscripcion"] = Convert.ToString(GrillaHistorial.DataKeys[row.RowIndex].Values["FchInscripcion"]);
                                dtFilaRoja.Rows.Add(row1);
                            }
                            else
                            {
                                row.BackColor = System.Drawing.Color.LightBlue;
                            }
                        }
                        Session.Add("FilaRoja", dtFilaRoja);
                    }
                }

                dt2 = Session["FilaRoja"] as DataTable;
                if (dt2 != null)
                {
                    if (dt2.Rows.Count > 0)
                    {
                        //DateTime FechaPago;
                        //DataTable dt9 = new DataTable();
                        //string dateString = Convert.ToString(DateTime.Now.ToShortDateString());

                        //FechaPago = DateTime.Parse(dateString);
                        Bandera = 0;
                        decimal InteresCuotaAsignar = 0;
                        decimal InteresMensualAsignar = 0;

                        string AplicaInteres;
                        decimal RecargoAbiertoAsignar = 0;
                        decimal InteresTotal = 0;
                        DateTime fchVtoAsignar = DateTime.Now;
                        //LblMjeGridConcepto.Text = "";
                        String FchaInscripcionCon;
                        Int32 NroCuotaCon;
                        foreach (DataRow row3 in dt2.Rows)
                        {
                            int ValorSeleccionado;
                            //obtengo incripcionConepto 
                            dt9 = ocnInscripcionConcepto.ObtenerUno(Convert.ToInt32(row3["icoId"].ToString()));
                            int conId = Convert.ToInt32(row3["conId"].ToString());
                            dt3 = ocnConceptos.ObtenerUno(Convert.ToInt32(row3["conId"].ToString()));
                            if (dt9.Rows.Count > 0) //Inscripcion Concepto Existe
                            {
                                FchaInscripcionCon = Convert.ToString(dt9.Rows[0]["FechaInscripcion"].ToString());
                                NroCuotaCon = Convert.ToInt32(dt9.Rows[0]["NroCuota"].ToString());
                                String BecaCon = Convert.ToString(dt9.Rows[0]["Becas"].ToString());
                                //Busco si hay Vencimiento
                                dt4 = ocnConceptosIntereses.ObtenerListaxconIdxNroCuota(Convert.ToInt32(Convert.ToInt32(row3["conId"].ToString())), (Convert.ToInt32(row3["NroCuota"].ToString())));

                                ValorInteresCI = 0;
                                //Decimal ValorInteresCI = 0;
                                DateTime FchaVtofila;
                                int BanderaVto = 0;
                                if (dt4.Rows.Count > 0)//Existe en la Tabla ConceptoInteres
                                {
                                    String pFecha = "";
                                    pFecha = dt4.Rows[0]["FechaVto"].ToString();
                                    FchaVtofila = Convert.ToDateTime(dt4.Rows[0]["FechaVto"].ToString());//1° Vto
                                    int Ban = 0;
                                    int i = 0;
                                    while (i < dt4.Rows.Count && Ban == 0)
                                    {
                                        DataRow nextRow = dt4.Rows[i];
                                        DataRow row4 = dt4.Rows[i];
                                        FchaVtofila = Convert.ToDateTime(row4["FechaVto"].ToString());
                                        if (FchaVtofila >= FechaPago)
                                        {
                                            Ban = 1;
                                        }
                                        fchVtoAsignar = Convert.ToDateTime(row4["FechaVto"].ToString());
                                        AplicaInteres = Convert.ToString(row4["AplicaInteres"].ToString());
                                        ValorSeleccionado = Convert.ToInt32(dt3.Rows[0]["ValorSeleccionado"].ToString());
                                        if (ValorSeleccionado == 0) // Monto fijo
                                        {
                                            if (Convert.ToDecimal(row4["ValorInteres"].ToString()) > 0)
                                            {
                                                if (AplicaInteres == "Si")// Se Cobra Interes
                                                {
                                                    if (Convert.ToDecimal(dt9.Rows[0]["BecInteres"].ToString()) > 0)
                                                    {
                                                        ValorInteresCI = Math.Round((Convert.ToDecimal(row4["ValorInteres"].ToString()) * Convert.ToDecimal(dt9.Rows[0]["BecInteres"].ToString()) / 100), 2);
                                                    }
                                                    else
                                                    {
                                                        ValorInteresCI = Convert.ToDecimal(row4["ValorInteres"].ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    ValorInteresCI = 0;
                                                }
                                            }
                                            else
                                            {
                                                ValorInteresCI = 0;
                                            }
                                        }
                                        else // Porcentaje
                                        {
                                            if (Convert.ToDecimal(row4["ValorInteres"].ToString()) > 0)
                                            {
                                                if (AplicaInteres == "Si")// Se Cobra Interes
                                                {
                                                    if (Convert.ToDecimal(dt9.Rows[0]["BecInteres"].ToString()) > 0)
                                                    {
                                                        ValorInteresCI = Math.Round((Convert.ToDecimal(row3["Importe"].ToString()) * Convert.ToDecimal(row4["ValorInteres"].ToString()) / 100
                                                            * Convert.ToDecimal(dt9.Rows[0]["BecInteres"].ToString()) / 100), 2);
                                                    }
                                                    else
                                                    {
                                                        ValorInteresCI = Convert.ToDecimal(row3["Importe"].ToString()) * Convert.ToDecimal(row4["ValorInteres"].ToString()) / 100;
                                                    }
                                                }
                                                else
                                                {
                                                    ValorInteresCI = 0;
                                                }
                                            }
                                            else
                                            {
                                                ValorInteresCI = 0;
                                            }
                                        }

                                        i = i + 1;
                                    }

                                    InteresCuotaAsignar = ValorInteresCI;
                                    RecargoAbiertoAsignar = 0;
                                    InteresMensualAsignar = 0;
                                    InteresTotal = ValorInteresCI;
                                    fchVtoAsignar = Convert.ToDateTime(FchaVtofila);
                                    BanderaVto = 1;

                                    DataTable dt12 = ocnConceptosIntereses.ObtenerUltimoVencimiento(Convert.ToInt32(Convert.ToInt32(row3["conId"].ToString())), (Convert.ToInt32(row3["NroCuota"].ToString())));
                                    if (dt12.Rows.Count > 0)
                                    {
                                        UltVto = Convert.ToDateTime(dt12.Rows[0]["FechaVto"].ToString());

                                        if (FechaPago > UltVto)
                                        {
                                            Decimal InteresMensualIM;
                                            Decimal InteresAplicar = 0;
                                            Decimal ValorInteresRA = 0;
                                            ValorSeleccionado = Convert.ToInt32(dt3.Rows[0]["ValorSeleccionado"].ToString());
                                            if (Convert.ToString(dt3.Rows[0]["TieneVtoAbierto"]) == "Si")
                                            {

                                                if (ValorSeleccionado == 0)// Interés con Monto Fijo
                                                {
                                                    ValorInteresRA = Convert.ToDecimal(dt3.Rows[0]["RecargoVtoAbierto"].ToString());

                                                    if (Convert.ToInt32(dt3.Rows[0]["InteresMensual"]) != 0)
                                                    {
                                                        Decimal diff = Math.Abs((FechaPago.Month - UltVto.Month) + 12 * (FechaPago.Year - UltVto.Year));

                                                        InteresMensualIM = Decimal.Round(((Convert.ToDecimal(row3["Importe"].ToString()) + ValorInteresCI) * Convert.ToDecimal(dt3.Rows[0]["InteresMensual"])) / 100) ;
                                                        InteresAplicar = InteresMensualIM * Convert.ToDecimal(diff);

                                                        InteresCuotaAsignar = ValorInteresCI;
                                                        RecargoAbiertoAsignar = ValorInteresRA;
                                                        InteresMensualAsignar = InteresAplicar;
                                                        InteresTotal = ValorInteresCI + ValorInteresRA + InteresAplicar;
                                                        fchVtoAsignar = Convert.ToDateTime(UltVto);
                                                    }
                                                    else
                                                    {
                                                        InteresCuotaAsignar = ValorInteresCI;
                                                        RecargoAbiertoAsignar = ValorInteresRA;
                                                        InteresMensualAsignar = InteresAplicar;
                                                        InteresTotal = ValorInteresCI + ValorInteresRA + InteresAplicar;
                                                        fchVtoAsignar = Convert.ToDateTime(UltVto);
                                                    }
                                                }
                                                else// Porcentaje
                                                {
                                                    ValorInteresRA = (Convert.ToDecimal(dt3.Rows[0]["Importe"].ToString()) * Convert.ToDecimal(dt3.Rows[0]["RecargoVtoAbierto"].ToString())) / 100;

                                                    if (Convert.ToInt32(dt3.Rows[0]["InteresMensual"]) != 0)
                                                    {
                                                        Decimal diff = Math.Abs((FechaPago.Month - UltVto.Month) + 12 * (FechaPago.Year - UltVto.Year));
                                                        InteresMensualIM = Decimal.Round(((Convert.ToDecimal(row3["Importe"].ToString()) + ValorInteresCI) * Convert.ToDecimal(dt3.Rows[0]["InteresMensual"])) / 100) ;
                                                        InteresAplicar = InteresMensualIM * Convert.ToDecimal(diff);

                                                        InteresCuotaAsignar = ValorInteresCI;
                                                        RecargoAbiertoAsignar = ValorInteresRA;
                                                        InteresMensualAsignar = InteresAplicar;
                                                        InteresTotal = ValorInteresCI + ValorInteresRA + InteresAplicar;
                                                        fchVtoAsignar = Convert.ToDateTime(UltVto);
                                                    }
                                                    else
                                                    {
                                                        InteresCuotaAsignar = ValorInteresCI;
                                                        RecargoAbiertoAsignar = ValorInteresRA;
                                                        InteresMensualAsignar = InteresAplicar;
                                                        InteresTotal = ValorInteresCI + ValorInteresRA + InteresAplicar;
                                                        fchVtoAsignar = Convert.ToDateTime(UltVto);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (Convert.ToInt32(dt3.Rows[0]["InteresMensual"]) != 0)
                                                {
                                                    if (ValorSeleccionado == 0)// Interés con Monto Fijo
                                                    {
                                                        Decimal diff = Math.Abs((FechaPago.Month - UltVto.Month) + 12 * (FechaPago.Year - UltVto.Year));
                                                        InteresMensualIM = Decimal.Round(((Convert.ToDecimal(row3["Importe"].ToString()) + ValorInteresCI) * Convert.ToDecimal(dt3.Rows[0]["InteresMensual"])) / 100);
                                                        InteresAplicar = InteresMensualIM * Convert.ToDecimal(diff);
                                                        InteresCuotaAsignar = ValorInteresCI;
                                                        RecargoAbiertoAsignar = ValorInteresRA;
                                                        InteresMensualAsignar = InteresAplicar;
                                                        InteresTotal = ValorInteresCI + ValorInteresRA + InteresAplicar;
                                                        fchVtoAsignar = Convert.ToDateTime(UltVto);
                                                    }
                                                    else
                                                    {
                                                        Decimal diff = Math.Abs((FechaPago.Month - UltVto.Month) + 12 * (FechaPago.Year - UltVto.Year));
                                                        InteresMensualIM = Decimal.Round(((Convert.ToDecimal(row3["Importe"].ToString()) + ValorInteresCI) * Convert.ToDecimal(dt3.Rows[0]["InteresMensual"])) / 100);
                                                        InteresAplicar = InteresMensualIM * Convert.ToDecimal(diff);
                                                        InteresCuotaAsignar = ValorInteresCI;
                                                        RecargoAbiertoAsignar = ValorInteresRA;
                                                        InteresMensualAsignar = InteresAplicar;
                                                        InteresTotal = ValorInteresCI + ValorInteresRA + InteresAplicar;
                                                        fchVtoAsignar = Convert.ToDateTime(UltVto);
                                                    }
                                                }
                                                else
                                                {
                                                    InteresCuotaAsignar = ValorInteresCI;
                                                    RecargoAbiertoAsignar = ValorInteresRA;
                                                    InteresMensualAsignar = 0;
                                                    InteresTotal = ValorInteresCI + ValorInteresRA;
                                                    fchVtoAsignar = Convert.ToDateTime(UltVto);
                                                }
                                            }
                                        }
                                    }
                                }
                            }



                            txtInt = txtInt + InteresTotal;


                        }
                        Decimal TotGral = TotAdeuda + txtInt;
                        TexCuotas.Text = Convert.ToString(TotAdeuda);
                        txtIntereses.Text = Convert.ToString(txtInt);
                        txtTot.Text = Convert.ToString(TotGral);
                    }
                    else
                    {
                        TexCuotas.Text = "0";
                        txtIntereses.Text = "0";
                        txtTot.Text = "0";
                    }
                }
            }
            else
            {
                alerError2.Visible = true;
                lblError2.Text = "No registra deuda para ese año";
                GrillaHistorial.DataSource = null;
                GrillaHistorial.DataBind();
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


    public static int GetMonthDifference(DateTime startDate, DateTime endDate)
    {
        int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
        return Math.Abs(monthsApart);
    }


    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        //AlumnoSeleccionado.Visible = false;
        //alerErroBE.Visible = false;
        //int PageIndex = 0;
        //PageIndex = Convert.ToInt32(Session["EstadoCuenta.PageIndex"]);
        //if (TextBuscar.Text != "")
        //{
        //    if (Convert.ToInt32(Session["Bandera"]) == 0)
        //    {
        //        dt = ocnAlumno.ObtenerTodoBuscarxNombre(TextBuscar.Text.Trim());
        //        if (dt.Rows.Count > 0)
        //        {

        //            this.Grilla.DataSource = dt;
        //            this.Grilla.PageIndex = PageIndex;
        //            this.Grilla.DataBind();
        //            this.GrillaHistorial.DataSource = null;
        //            this.GrillaHistorial.PageIndex = PageIndex;
        //            this.GrillaHistorial.DataBind();

        //        }
        //        else
        //        {
        //            alerErroBE.Visible = true;
        //            lblalerErroBE.Text = "No se encuentra Alumno con ese Filtro";
        //            this.GrillaHistorial.DataSource = null;
        //            this.GrillaHistorial.PageIndex = PageIndex;
        //            this.GrillaHistorial.DataBind();

        //        }
        //    }
        //    else
        //    {
        //        dt = ocnAlumno.ObtenerUnoporDoc(TextBuscar.Text.Trim());
        //        if (dt.Rows.Count > 0)
        //        {
        //            CanReg.Visible = true;
        //            lblCantidadRegistros2.Text = "Cantidad de registros: " + dt.Rows.Count.ToString();
        //            this.Grilla.DataSource = dt;
        //            this.Grilla.PageIndex = PageIndex;
        //            this.Grilla.DataBind();
        //            this.GrillaHistorial.DataSource = null;
        //            this.GrillaHistorial.PageIndex = PageIndex;
        //            this.GrillaHistorial.DataBind();
        //        }
        //        else
        //        {
        //            alerErroBE.Visible = true;
        //            lblalerErroBE.Text = "No se encuentra Alumno con ese Filtro";
        //            this.GrillaHistorial.DataSource = null;
        //            this.GrillaHistorial.PageIndex = PageIndex;
        //            this.GrillaHistorial.DataBind();
        //        }
        //    }

        //    //if (dt.Rows.Count > 0)
        //    //{
        //    //    AlumnoSeleccionado.Visible = true;
        //    //    aluNombre.Text = Convert.ToString(dt.Rows[0]["Nombre"]);
        //    //    aluNombre.Enabled = false;
        //    //    aludni.Text = Convert.ToString(dt.Rows[0]["Doc"]);
        //    //    aludni.Enabled = false;

        //    //    PageIndex = Convert.ToInt32(Session["EstadoCuenta.PageIndex"]);
        //    //    String Id = Convert.ToString(dt.Rows[0]["Id"]);
        //    //    lblaluId.Text = Id;
        //    //    GrillaCargar(PageIndex);
        //    //}
        //    //else
        //    //{
        //    //    alerErroBE.Visible = true;
        //    //    lblalerErroBE.Text = "No se encuentra Alumno con ese Filtro";
        //    //}
        //}
        //else
        //{
        //    alerErroBE.Visible = true;
        //    lblalerErroBE.Text = "Debe ingresar DNI del Alumno";
        //}
    }



    protected void btnCancelarAlumno_Click(object sender, EventArgs e)
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


    protected void GrillaHistorial_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            alerError2.Visible = false;

            if (e.CommandName != "Sort" && e.CommandName != "Page" && e.CommandName != "")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                int val = (int)this.GrillaHistorial.DataKeys[rowIndex]["icoId"];
                //string Id = ((HyperLink)GrillaHistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Controls[1]).Text;
                if (e.CommandName == "Pagar") // Esta condición es la clave. Si no hay CommandName="Pagar", este bloque NO SE EJECUTA.
                {
                    alerError2.Visible = false;
                    //insId = Convert.ToInt32(Session["_Institucion"]); // Asegúrate de que insId esté declarado en la clase
                    lblMensajeError.Text = "";

                    DataTable dt = new DataTable();
                    // ... (definición de columnas de dt)
                    dt.Columns.Add("icuId", typeof(int));
                    dt.Columns.Add("icoId", typeof(int));
                    dt.Columns.Add("cntId", typeof(int));
                    dt.Columns.Add("conId", typeof(Int32));
                    dt.Columns.Add("TipoConcepto", typeof(String));
                    dt.Columns.Add("Concepto", typeof(String));
                    dt.Columns.Add("Importe", typeof(Decimal));
                    dt.Columns.Add("AnioLectivo", typeof(Decimal));
                    dt.Columns.Add("Beca", typeof(String));
                    dt.Columns.Add("BecId", typeof(Int32));
                    dt.Columns.Add("NroCuota", typeof(Int32));
                    dt.Columns.Add("FchInscripcion", typeof(String));

                    String FchaInscripcionCon;
                    Int32 NroCuotaCon;
                    Int32 bcaIdCon;
                    Int32 AnioCursado;

                    AnioCursado = Convert.ToInt32(this.GrillaHistorial.DataKeys[rowIndex]["AnioLectivo"]);
                    //insId = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["InsId"]); // Asegúrate de que insId esté declarado y tenga un valor

                    // Aquí hay una dependencia de 'ocnInscripcionCursado' y 'lblaluId.Text'
                    // Asegúrate de que 'ocnInscripcionCursado' esté instanciado y 'lblaluId' tenga texto válido
                    // y que 'icuId2' esté declarado a nivel de clase.
                    // Si esta parte falla por variables no declaradas o nulas, impactará la redirección.
                    // DataTable dt3 = ocnInscripcionCursado.ObtenerTodoxInsIdxaluIdxAnioCursado(insId, AnioCursado, Convert.ToInt32(lblaluId.Text));
                    // icuId2 = Convert.ToInt32(dt3.Rows[0]["Id"].ToString()); 

                    DataRow row1 = dt.NewRow();
                    // row1["icuId"] = icuId2; // Esta línea depende de la línea anterior
                    row1["icoId"] = Convert.ToInt32(this.GrillaHistorial.DataKeys[rowIndex]["icoId"]);
                    row1["cntId"] = Convert.ToInt32(this.GrillaHistorial.DataKeys[rowIndex]["cntId"]);
                    row1["conId"] = Convert.ToInt32(this.GrillaHistorial.DataKeys[rowIndex]["conId"]);
                    row1["TipoConcepto"] = Convert.ToString(this.GrillaHistorial.DataKeys[rowIndex]["TipoConcepto"]);
                    row1["Concepto"] = Convert.ToString(this.GrillaHistorial.DataKeys[rowIndex]["Concepto"]);
                    row1["Importe"] = Convert.ToDecimal(this.GrillaHistorial.DataKeys[rowIndex]["Importe"]);
                    row1["AnioLectivo"] = Convert.ToInt32(this.GrillaHistorial.DataKeys[rowIndex]["AnioLectivo"]);
                    row1["Beca"] = Convert.ToString(this.GrillaHistorial.DataKeys[rowIndex]["Beca"]);
                    row1["BecId"] = Convert.ToInt32(this.GrillaHistorial.DataKeys[rowIndex]["BecId"]);
                    row1["NroCuota"] = Convert.ToInt32(this.GrillaHistorial.DataKeys[rowIndex]["NroCuota"]);
                    row1["FchInscripcion"] = Convert.ToString(this.GrillaHistorial.DataKeys[rowIndex]["FchInscripcion"]);
                    dt.Rows.Add(row1);

                    Session.Add("TablaPagar", dt);
                }
                // Response.Redirect("FacturarCupones.aspx?Id=" + icuId2, false); // Esta redirección ocurrirá SIEMPRE que e.CommandName no sea "Sort", "Page", o ""
                // Esto significa que si haces clic en cualquier otra cosa en la fila que genera un CommandName (y no es un Sort/Page/etc.),
                // Intentará redirigir, lo que podría no ser lo que quieres si solo buscas el modal.
                // Considera sacar esta redirección fuera del if (e.CommandName == "Pagar")
                // o ajustar la condición principal si solo quieres el modal y no hay otras acciones de comando.
            }
            else
            {
                // Este bloque se ejecuta si e.CommandName es "Sort", "Page", o vacío.
                // alerError2.Visible = true;
                // lblError2.Text = "Debe seleccionar un items a facturar";
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


    protected void GrillaHistorial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Obtener el objeto de datos que está enlazado a esta fila
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            // Asignar los atributos data-* a la fila HTML
            // Asegúrate que los nombres de los campos (ej. "Concepto", "Importe")
            // coincidan con las columnas en tu DataTable o propiedades de tu objeto.
            e.Row.Attributes["data-concepto"] = rowView["Concepto"].ToString();
            e.Row.Attributes["data-importe"] = string.Format("{0:C}", rowView["Importe"]); // Formatear a moneda
            e.Row.Attributes["data-importeinteres"] = string.Format("{0:C}", rowView["ImporteInteres"]);
            e.Row.Attributes["data-beca"] = rowView["Beca"].ToString();
            e.Row.Attributes["data-nrocuota"] = rowView["NroCuota"].ToString();
            e.Row.Attributes["data-fechavto"] = string.Format("{0:d}", rowView["FechaVto"]); // Formatear a fecha corta
           
            e.Row.Attributes["data-imppagado"] = string.Format("{0:C}", rowView["ImpPagado"]);
            e.Row.Attributes["data-fechapago"] = rowView["FechaPago"] != DBNull.Value ? string.Format("{0:d}", rowView["FechaPago"]) : "N/A"; // Manejar si es DBNull
            e.Row.Attributes["data-colegio"] = rowView["Colegio"].ToString();
            e.Row.Attributes["data-curso"] = rowView["Curso"].ToString();
            e.Row.Attributes["data-conid"] = rowView["conId"].ToString();
          

            // Calcular ImporteTotal (si no existe como columna directa en tu origen de datos)
            decimal importe = Convert.ToDecimal(rowView["Importe"]);
            decimal importeInteres = rowView["ImporteInteres"] != DBNull.Value ? Convert.ToDecimal(rowView["ImporteInteres"]) : 0;
           
            decimal importeTotalCalculado = importe + importeInteres ;
            e.Row.Attributes["data-importetotal"] = string.Format("{0:C}", importeTotalCalculado);


            // Lógica para el Label de Estado y el color de la fila
            Label lblEstado = (Label)e.Row.FindControl("lblEstado");
            if (lblEstado != null)
            {
                DateTime fechaVto = Convert.ToDateTime(rowView["FechaVto"]);
                Decimal impPagado = Convert.ToDecimal(rowView["ImpPagado"]);

                if (impPagado > 0)
                {
                    lblEstado.Text = "Pagado";
                    e.Row.BackColor = System.Drawing.Color.LightBlue;
                }
                else if (fechaVto < DateTime.Today)
                {
                    lblEstado.Text = "Vencido";
                    e.Row.BackColor = System.Drawing.Color.Red;
                    lblEstado.ForeColor = System.Drawing.Color.White; // Asegura que el texto sea legible
                }
                else
                {
                    lblEstado.Text = "Pendiente";
                }
            }
        }
    }

    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
        foreach (GridViewRow row in Grilla.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                row.Attributes["onmouseover"] = "this.style.background = '#0BB8A1'; this.style.cursor = 'pointer'";
                row.Attributes["onmouseout"] = "this.style.background='#ffffff'";
                row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(Grilla, "Select$" + row.RowIndex, true);
            }
        }

        base.Render(writer);
    }

    protected void Grilla_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor; this.style.backgroundColor='#F7F7DE';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;");
        }
    }

    //    protected void btnFacturarClick(object sender, EventArgs e)
    //    {
    //        try
    //        {
    //            alerError2.Visible = false;
    //            if (txtFchPago.Text == "")
    //            {
    //                int CuotaUltPagada = 0;
    //                foreach (GridViewRow row in GrillaHistorial.Rows)
    //                {
    //                    if (Convert.ToDecimal(GrillaHistorial.DataKeys[row.RowIndex].Values["ImpPagado"]) != 0)
    //                    {
    //                        row.BackColor = System.Drawing.Color.LightBlue;
    //                        CuotaUltPagada = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["NroCuota"]);
    //                        ((CheckBox)row.FindControl("chkSeleccion")).Enabled = false;
    //                    }

    //                    else
    //                    {
    //                        if (Convert.ToDateTime(GrillaHistorial.DataKeys[row.RowIndex].Values["FechaVto"]) < DateTime.Today)
    //                        {
    //                            row.BackColor = System.Drawing.Color.FromName("#B81822");
    //                            row.ForeColor = System.Drawing.Color.White;
    //                            btnFacturar.Visible = true;
    //                            txtFchPago.Visible = true;
    //                            lblFchPago.Visible = true;
    //                            //CheckBox ck = (CheckBox)row.Cells[1].FindControl("chkSeleccion");
    //                            //(CheckBox)row.Cells[1].FindControl("chkSeleccion").Visible = false;

    //                        }
    //                    }
    //                }
    //                alerError2.Visible = true;
    //                lblError2.Text = "Debe ingresar una fecha de pago..";
    //                return;

    //            }

    //            //insId = Convert.ToInt32(Session["_Institucion"]);
    //            lblMensajeError.Text = "";
    //            int BanChk = 0;
    //            foreach (GridViewRow row in GrillaHistorial.Rows)
    //            {
    //                CheckBox check = row.FindControl("chkSeleccion") as CheckBox;
    //                if ((check.Checked)) // Si esta seleccionado..
    //                {
    //                    BanChk = 1;
    //                }
    //            }
    //            if (BanChk == 1)
    //            {
    //                DataTable dt = new DataTable();
    //                DataTable dt9 = new DataTable();
    //                DataTable dt4 = new DataTable();
    //                DataTable dt3 = new DataTable();
    //                DataTable dt1 = new DataTable();
    //                dt.Columns.Add("icuId", typeof(int));
    //                dt.Columns.Add("icoId", typeof(int));
    //                dt.Columns.Add("cntId", typeof(int));
    //                dt.Columns.Add("conId", typeof(Int32));
    //                dt.Columns.Add("TipoConcepto", typeof(String));
    //                dt.Columns.Add("Concepto", typeof(String));
    //                dt.Columns.Add("Importe", typeof(Decimal));
    //                dt.Columns.Add("AnioLectivo", typeof(Decimal));
    //                dt.Columns.Add("Beca", typeof(String));
    //                dt.Columns.Add("BecId", typeof(Int32));
    //                dt.Columns.Add("NroCuota", typeof(Int32));
    //                dt.Columns.Add("FchInscripcion", typeof(String));

    //                String FchaInscripcionCon;
    //                Int32 NroCuotaCon;
    //                Int32 bcaIdCon;
    //                Int32 AnioCursado;

    //                foreach (GridViewRow row in GrillaHistorial.Rows)
    //                {
    //                    CheckBox check = row.FindControl("chkSeleccion") as CheckBox;
    //                    //Int32 EstIC = Convert.ToInt32(GrillaAlumnos.DataKeys[row.RowIndex].Values["Estado"]);

    //                    if ((check.Checked)) // Si esta seleccionado..
    //                    {
    //                        AnioCursado = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["AnioLectivo"]);
    //                        insId = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["insId"]);
    //                        dt3 = ocnInscripcionCursado.ObtenerTodoxInsIdxaluIdxAnioCursado(insId, AnioCursado, Convert.ToInt32(lblaluId.Text));
    //                        icuId2 = Convert.ToInt32(dt3.Rows[0]["Id"].ToString());
    //                        FchaInscripcionCon = Convert.ToString(GrillaHistorial.DataKeys[row.RowIndex].Values["FchInscripcion"]);
    //                        NroCuotaCon = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["NroCuota"]);
    //                        bcaIdCon = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["BecasId"]);
    //                        DataRow row1 = dt.NewRow();
    //                        row1["icuId"] = icuId2;
    //                        row1["icoId"] = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["icoId"]);
    //                        row1["cntId"] = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["cntId"]);
    //                        row1["conId"] = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["conId"]);
    //                        row1["TipoConcepto"] = Convert.ToString(GrillaHistorial.DataKeys[row.RowIndex].Values["TipoConcepto"]);
    //                        row1["Concepto"] = Convert.ToString(GrillaHistorial.DataKeys[row.RowIndex].Values["Concepto"]);
    //                        row1["Importe"] = Convert.ToDecimal(GrillaHistorial.DataKeys[row.RowIndex].Values["Importe"]);
    //                        row1["AnioLectivo"] = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["AnioLectivo"]);
    //                        row1["Beca"] = Convert.ToString(GrillaHistorial.DataKeys[row.RowIndex].Values["Beca"]);
    //                        row1["BecId"] = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["BecId"]);
    //                        row1["NroCuota"] = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["NroCuota"]);

    //                        row1["FchInscripcion"] = Convert.ToString(GrillaHistorial.DataKeys[row.RowIndex].Values["FchInscripcion"]);
    //                        dt.Rows.Add(row1);
    //                    }
    //                    Session.Add("TablaPagar", dt);

    //                }
    //                DateTime FchPago = Convert.ToDateTime(txtFchPago.Text);
    //                Response.Redirect("FacturarCupones.aspx?Id=" + icuId2 + "&FchPago=" + FchPago, false);
    //            }
    //            else
    //            {
    //                int CuotaUltPagada = 0;
    //                foreach (GridViewRow row in GrillaHistorial.Rows)
    //                {
    //                    if (Convert.ToDecimal(GrillaHistorial.DataKeys[row.RowIndex].Values["ImpPagado"]) != 0)
    //                    {
    //                        row.BackColor = System.Drawing.Color.LightBlue;
    //                        CuotaUltPagada = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["NroCuota"]);
    //                        ((CheckBox)row.FindControl("chkSeleccion")).Enabled = false;
    //                    }

    //                    else
    //                    {
    //                        if (Convert.ToDateTime(GrillaHistorial.DataKeys[row.RowIndex].Values["FechaVto"]) < DateTime.Today)
    //                        {
    //                            row.BackColor = System.Drawing.Color.FromName("#B81822");
    //                            row.ForeColor = System.Drawing.Color.White;
    //                            btnFacturar.Visible = true;
    //                            txtFchPago.Visible = true;
    //                            lblFchPago.Visible = true;
    //                            //CheckBox ck = (CheckBox)row.Cells[1].FindControl("chkSeleccion");
    //                            //(CheckBox)row.Cells[1].FindControl("chkSeleccion").Visible = false;

    //                        }
    //                    }
    //                }
    //                alerError2.Visible = true;
    //                lblError2.Text = "Debe seleccionar un items a facturar";
    //            }
    //        }
    //        catch (Exception oError)
    //        {
    //            lblMensajeError.Text = @"<div class=""alert alert-danger alert-dismissable"">
    //<button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
    //<a class=""alert-link"" href=""#"">Error de Sistema</a><br/>
    //Se ha producido el siguiente error:<br/>
    //MESSAGE:<br>" + oError.Message + "<br><br>EXCEPTION:<br>" + oError.InnerException + "<br><br>TRACE:<br>" + oError.StackTrace + "<br><br>TARGET:<br>" + oError.TargetSite +
    //    "</div>";
    //        }
    //    }

    protected void Grilla_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (Session["EstadoCuenta.PageIndex"] != null)
            {
                Session["EstadoCuenta.PageIndex"] = e.NewPageIndex;
            }
            else
            {
                Session.Add("EstadoCuenta.PageIndex", e.NewPageIndex);
            }

            this.GrillaCargar2(e.NewPageIndex);
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
    private void GrillaCargar2(int PageIndex)
    {
        try
        {

            //            AlumnoSeleccionado.Visible = false;
            //            alerErroBE.Visible = false;
            //            PageIndex = Convert.ToInt32(Session["EstadoCuenta.PageIndex"]);
            //            if (TextBuscar.Text != "")
            //            {
            //                if (Convert.ToInt32(Session["Bandera"]) == 0)
            //                {
            //                    dt = ocnAlumno.ObtenerTodoBuscarxNombre(TextBuscar.Text.Trim());
            //                    if (dt.Rows.Count > 0)
            //                    {

            //                        this.Grilla.DataSource = dt;
            //                        this.Grilla.PageIndex = PageIndex;
            //                        this.Grilla.DataBind();
            //                        this.GrillaHistorial.DataSource = null;
            //                        this.GrillaHistorial.PageIndex = PageIndex;
            //                        this.GrillaHistorial.DataBind();

            //                    }
            //                    else
            //                    {
            //                        alerErroBE.Visible = true;
            //                        lblalerErroBE.Text = "No se encuentra Alumno con ese Filtro";
            //                        this.GrillaHistorial.DataSource = null;
            //                        this.GrillaHistorial.PageIndex = PageIndex;
            //                        this.GrillaHistorial.DataBind();

            //                    }
            //                }
            //                else
            //                {
            //                    dt = ocnAlumno.ObtenerUnoporDoc(TextBuscar.Text.Trim());
            //                    if (dt.Rows.Count > 0)
            //                    {
            //                        CanReg.Visible = true;
            //                        lblCantidadRegistros2.Text = "Cantidad de registros: " + dt.Rows.Count.ToString();
            //                        this.Grilla.DataSource = dt;
            //                        this.Grilla.PageIndex = PageIndex;
            //                        this.Grilla.DataBind();
            //                        this.GrillaHistorial.DataSource = null;
            //                        this.GrillaHistorial.PageIndex = PageIndex;
            //                        this.GrillaHistorial.DataBind();
            //                    }
            //                    else
            //                    {
            //                        alerErroBE.Visible = true;
            //                        lblalerErroBE.Text = "No se encuentra Alumno con ese Filtro";
            //                        this.GrillaHistorial.DataSource = null;
            //                        this.GrillaHistorial.PageIndex = PageIndex;
            //                        this.GrillaHistorial.DataBind();
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                alerErroBE.Visible = true;
            //                lblalerErroBE.Text = "Debe ingresar DNI del Alumno";
            //            }
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

    protected void btnImprimirClick(object sender, EventArgs e)
    {
        ocnTEMESTADOCUENTA.EliminarTodo();
        int Id = 0;
        int InstId = Convert.ToInt32(this.Session["_Institucion"]);
        DateTime Hoy = DateTime.Today;
        foreach (GridViewRow row in GrillaHistorial.Rows)
        {
            if (Convert.ToDateTime(GrillaHistorial.DataKeys[row.RowIndex].Values["FechaVto"]) < Hoy && Convert.ToSingle(GrillaHistorial.DataKeys[row.RowIndex].Values["ImpPagado"]) == 0 && Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["BecId"]) != 14)
            {
                ocnTEMESTADOCUENTA = new GESTIONESCOLAR.Negocio.TEMESTADOCUENTA(Convert.ToInt32(Id));
                ocnTEMESTADOCUENTA.tecConcepto = Convert.ToString(GrillaHistorial.DataKeys[row.RowIndex].Values["Concepto"]);
                ocnTEMESTADOCUENTA.tecNumCuota = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["NroCuota"]);
                ocnTEMESTADOCUENTA.tecImporte = Convert.ToDecimal(GrillaHistorial.DataKeys[row.RowIndex].Values["Importe"]);
                ocnTEMESTADOCUENTA.tecColegio = Convert.ToString(GrillaHistorial.DataKeys[row.RowIndex].Values["Colegio"]);
                ocnTEMESTADOCUENTA.tecCurso = Convert.ToString(GrillaHistorial.DataKeys[row.RowIndex].Values["Curso"]);
                ocnTEMESTADOCUENTA.tecImpPagado = Convert.ToDecimal(GrillaHistorial.DataKeys[row.RowIndex].Values["ImpPagado"]);
                ocnTEMESTADOCUENTA.tecFchPago = Convert.ToString(GrillaHistorial.DataKeys[row.RowIndex].Values["FechaPago"]);
                ocnTEMESTADOCUENTA.tecImpInteres = Convert.ToDecimal(GrillaHistorial.DataKeys[row.RowIndex].Values["ImporteInteres"]);
                ocnTEMESTADOCUENTA.tecBeca = Convert.ToString(GrillaHistorial.DataKeys[row.RowIndex].Values["Beca"]);

                ocnTEMESTADOCUENTA.tecFechaVto = Hoy;
                ocnTEMESTADOCUENTA.tecLugarPago = "";
                ocnTEMESTADOCUENTA.tecFormaPago = "";
                ocnTEMESTADOCUENTA.tecTotalCuotasImpagas = 0;
                ocnTEMESTADOCUENTA.tecTotalInteres = 0;
                ocnTEMESTADOCUENTA.tecTotalDeudaalaFecha = 0;

                ocnTEMESTADOCUENTA.Insertar();
            }
        }

        foreach (GridViewRow row in GrillaHistorial.Rows)
        {
            if (Convert.ToDecimal(GrillaHistorial.DataKeys[row.RowIndex].Values["ImpPagado"]) != 0)
            {
                row.BackColor = System.Drawing.Color.LightBlue;
                //CuotaUltPagada = Convert.ToInt32(GrillaHistorial.DataKeys[row.RowIndex].Values["NroCuota"]);
                //((CheckBox)row.FindControl("chkSeleccion")).Enabled = false;
            }

            else
            {
                if (Convert.ToDateTime(GrillaHistorial.DataKeys[row.RowIndex].Values["FechaVto"]) < DateTime.Today)
                {
                    row.BackColor = System.Drawing.Color.FromName("#B81822");
                    row.ForeColor = System.Drawing.Color.White;
                    //btnFacturar.Visible = true;
                    //txtFchPago.Visible = true;
                    //lblFchPago.Visible = true;
                    btnImprimir2.Visible = true;
                    lblCuotas.Visible = true;
                    TexCuotas.Visible = true;
                    lblInt.Visible = true;
                    txtIntereses.Visible = true;
                    txtTot.Visible = true;
                    lblTot.Visible = true;
                    //CheckBox ck = (CheckBox)row.Cells[1].FindControl("chkSeleccion");
                    //(CheckBox)row.Cells[1].FindControl("chkSeleccion").Visible = false;

                }
            }
        }
        String NomRep;
        String ParamStr1 = aluNombre.Text;
        if (InstId == 121)
        {
            NomRep = "EstadoDeCuentaNew.rpt";
        }
        else
        {
            NomRep = "EstadoCuenta.rpt";

        }

        FuncionesUtiles.AbreVentana("Reporte.aspx?ParamStr1=" + ParamStr1 + "&NomRep=" + NomRep);
    }

    protected void RbtBuscar_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int ban;
        //int PageIndex = Convert.ToInt32(Session["EstadoCuenta.PageIndex"]);
        //if (RbtBuscar.SelectedIndex == 1) //la busqueda será por dni
        //{
        //    ban = 1;
        //    //AlumnoSeleccionado.Visible = false;
        //    GrillaHistorial.DataSource = null;
        //    GrillaHistorial.DataBind();
        //    lblPagado.Visible = false;
        //    lblVencido.Visible = false;
        //    txtcELESTE.Visible = false;
        //    txtRojo.Visible = false;
        //btnImprimir2.Visible = false;
        //    lblCuotas.Visible = false;
        //    TexCuotas.Visible = false;
        //    lblInt.Visible = false;
        //    txtIntereses.Visible = false;
        //    AlumnoSeleccionado.Visible = false;
        //    txtTot.Visible = false;
        //    lblTot.Visible = false;
        //}
        //else
        //{
        //    ban = 0;// la busqueda será por nombre
        //    GrillaHistorial.DataSource = null;
        //    GrillaHistorial.DataBind();
        //    lblPagado.Visible = false;
        //    lblVencido.Visible = false;
        //    txtcELESTE.Visible = false;
        //    txtRojo.Visible = false;
        //    btnImprimir2.Visible = false;
        //    lblCuotas.Visible = false;
        //    TexCuotas.Visible = false;
        //    lblInt.Visible = false;
        //    txtIntereses.Visible = false;
        //    AlumnoSeleccionado.Visible = false;
        //    txtTot.Visible = false;
        //    lblTot.Visible = false;
        //}
        //Session["Bandera"] = ban;
        //aludni.Text = "";
        //aluNombre.Text = "";
        //TextBuscar.Text = "";
    }

    protected void Grilla_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Sort" && e.CommandName != "Page" && e.CommandName != "")
            {
                String Id = ((HyperLink)Grilla.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Controls[1]).Text;

                if (e.CommandName == "Select")
                {
                    aluNombre.Text = ((HyperLink)Grilla.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Controls[1]).Text;
                    aluNombre.Enabled = false;
                    aludni.Text = ((HyperLink)Grilla.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Controls[1]).Text;
                    aludni.Enabled = false;
                    aluId.Text = Id;
                    CanReg.Visible = false;
                    Grilla.DataSource = null;
                    Grilla.DataBind();
                    AlumnoSeleccionado.Visible = true;
                    int PageIndex = Convert.ToInt32(Session["EstadoCuenta.PageIndex"]);

                    lblaluId.Text = Id;
                    GrillaCargar(PageIndex);
                    //Grilla.Text = ((HyperLink)Grilla.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Controls[1]).Text;

                }
            }
            if (e.CommandName != "Page")
            {

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



    protected void ckbDeuda_CheckedChanged(object sender, EventArgs e)
    {
        if (AlumnoSeleccionado.Visible == true)
        {
            int PageIndex = Convert.ToInt32(Session["EstadoCuenta.PageIndex"]);
            GrillaCargar(PageIndex);
        }
        else
        {

        }
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        int PageIndex = Convert.ToInt32(Session["EstadoCuenta.PageIndex"]);
        GrillaCargar(PageIndex);
    }
}


