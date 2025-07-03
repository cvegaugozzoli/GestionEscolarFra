using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.qrcode;
using System.Diagnostics;

public partial class ChequerasCopia : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    DataTable dt2 = new DataTable();
    GESTIONESCOLAR.Negocio.Curso ocnCurso = new GESTIONESCOLAR.Negocio.Curso();
    int Id = 0;
    int cur;
    int AnioCur;
    GESTIONESCOLAR.Negocio.ConceptosIntereses ocnConceptosIntereses = new GESTIONESCOLAR.Negocio.ConceptosIntereses();

    GESTIONESCOLAR.Negocio.InscripcionCursado ocnInscripcionCursado = new GESTIONESCOLAR.Negocio.InscripcionCursado();
    GESTIONESCOLAR.Negocio.EspacioCurricular ocnEspacioCurricular = new GESTIONESCOLAR.Negocio.EspacioCurricular();
    GESTIONESCOLAR.Negocio.PlanEstudio ocnPlanEstudio = new GESTIONESCOLAR.Negocio.PlanEstudio();
    GESTIONESCOLAR.Negocio.Campo ocnCampo = new GESTIONESCOLAR.Negocio.Campo();
    GESTIONESCOLAR.Negocio.Alumno ocnAlumno = new GESTIONESCOLAR.Negocio.Alumno();
    GESTIONESCOLAR.Negocio.UsuarioEspacioCurricular ocnUsuarioEspacioCurricular = new GESTIONESCOLAR.Negocio.UsuarioEspacioCurricular();
    GESTIONESCOLAR.Negocio.Conceptos ocnConceptos = new GESTIONESCOLAR.Negocio.Conceptos();
    GESTIONESCOLAR.Negocio.TipoCarrera ocnTipoCarrera = new GESTIONESCOLAR.Negocio.TipoCarrera();
    GESTIONESCOLAR.Negocio.InstitucionNivel ocnInstitucionNivel = new GESTIONESCOLAR.Negocio.InstitucionNivel();
    GESTIONESCOLAR.Negocio.Carrera ocnCarrera = new GESTIONESCOLAR.Negocio.Carrera();
    GESTIONESCOLAR.Negocio.InscripcionConcepto ocnInscripcionConcepto = new GESTIONESCOLAR.Negocio.InscripcionConcepto();
    GESTIONESCOLAR.Negocio.ChequeraImpimir ocnChequeraImpimir = new GESTIONESCOLAR.Negocio.ChequeraImpimir();

    int insId;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                int usuario = Convert.ToInt32(Session["_usuId"].ToString());

                this.Master.TituloDelFormulario = "Chequeras";
                btnImprimir.Visible = false;
                btnGenerar.Visible = false;
                chbDeuda.Visible = false;
                lblInsId.Text = Convert.ToString(Session["_Institucion"]);
                insId = Convert.ToInt32(Session["_Institucion"]);
                NivelID.DataValueField = "Valor"; NivelID.DataTextField = "Texto"; NivelID.DataSource = (new GESTIONESCOLAR.Negocio.InstitucionNivel()).ObtenerListaxIns("[Seleccionar...]", insId); NivelID.DataBind();

                #region PageIndex
                int PageIndex = 0;

                if (this.Session["Chequeras.PageIndex"] == null)
                {
                    Session.Add("Chequeras.PageIndex", 0);
                }
                else
                {
                    PageIndex = Convert.ToInt32(Session["Chequeras.PageIndex"]);
                }
                #endregion



                //GrillaCargar(PageIndex);
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
    protected void NivelID_SelectedIndexChanged(object sender, EventArgs e)
    {
        alerError.Visible = false;
        DataTable dt = new DataTable();
        insId = Convert.ToInt32(Session["_Institucion"]);
        dt = ocnCarrera.ObtenerUnoxNivel(Convert.ToInt32(NivelID.SelectedValue));
        int car = 0;
        int pla = 0;
        if (Convert.ToInt32(dt.Rows[0]["SinPC"].ToString()) == 0)//TIENE CARRERA Y PLAN? 0=SUPERIOR
        {
            carId.Enabled = true;
            carId.DataValueField = "Valor";
            carId.DataTextField = "Texto";
            carId.DataSource = (new GESTIONESCOLAR.Negocio.Carrera()).ObtenerLista("[Seleccionar...]");
            carId.DataBind();
            carId.Enabled = true;

        }
        else// es primario inicial o secundario
        {
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();

            dt3 = ocnCarrera.ObtenerUnoxNivel(Convert.ToInt32(NivelID.SelectedValue));
            if (dt3.Rows.Count > 0)
            {
                car = Convert.ToInt32(dt3.Rows[0]["Id"].ToString());
                dt4 = ocnPlanEstudio.ObtenerUnoxCarrera(car);
                if (dt4.Rows.Count > 0)
                {
                    pla = Convert.ToInt32(dt4.Rows[0]["Id"].ToString());
                }
            }
            curId.DataValueField = "Valor";
            curId.DataTextField = "Texto";
            curId.DataSource = (new GESTIONESCOLAR.Negocio.Curso()).ObtenerListaPorUnPlanEstudio("[Seleccionar...]", pla);
            curId.DataBind();
            carId.Enabled = false;
            plaId.Enabled = false;
            carId.SelectedValue = "0";
            plaId.SelectedValue = "0";

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


    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
        foreach (GridViewRow row in Grilla.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                row.Attributes["onmouseover"] = "this.style.background = '#CCCCFF'; this.style.cursor = 'pointer'";
                row.Attributes["onmouseout"] = "this.style.background='#ffffff'";
            }
        }
        base.Render(writer);
    }

    private void GrillaCargar2(int PageIndex)
    {
        try
        {
            alerError.Visible = false;
            if (Convert.ToInt32(NivelID.SelectedValue) == 0)
            {
                alerError.Visible = true;
                lblError.Text = "Debe ingresar un Nivel..";
                return;
            }


            lblMjeTabla.Text = "";
            Session["Chequeras.PageIndex"] = PageIndex;
            insId = Convert.ToInt32(Session["_Institucion"]);

            Int32 car = 0;
            Int32 pla = 0;

            if (Convert.ToInt32(NivelID.SelectedValue) == 4)
            {
                if (carId.SelectedValue.ToString() != "" & carId.SelectedValue.ToString() != "0")
                {
                    car = Convert.ToInt32(carId.SelectedValue.ToString());
                    if (plaId.SelectedValue.ToString() != "" & plaId.SelectedValue.ToString() != "0")
                    {
                        pla = Convert.ToInt32(plaId.SelectedValue.ToString());
                    }
                    else
                    {
                        alerError.Visible = true;
                        lblError.Text = "Debe ingresar un Plan ";

                        return;
                    }
                }
                else
                {
                    alerError.Visible = true;
                    lblError.Text = "Debe ingresar una Carrera ";
                    return;
                }
            }


            if (curId.SelectedValue.ToString() != "" & curId.SelectedValue.ToString() != "0")
            {
                cur = Convert.ToInt32(curId.SelectedValue.ToString());

            }
            if (AnioCursado.Text == "")
            {
                DateTime fechaActual = DateTime.Today;
                AnioCur = Convert.ToInt32(fechaActual.Year.ToString());
            }
            else
            {
                AnioCur = Convert.ToInt32(AnioCursado.Text);
            }

            dt = new DataTable();

            dt = ocnInscripcionConcepto.ObtenerTodoParaChequera(insId, cur, AnioCur, Convert.ToInt32(ddlDesde.SelectedValue), Convert.ToInt32(ddlHasta.SelectedValue), Convert.ToInt32(ddlConc.SelectedValue),0);//listado por curso, Estado = cursando
            //(Int32 insId, Int32 curId, Int32 Anio, Int32 Cuota1, Int32 Cuota2, Int32 ConId, Int32 CarId)
            if (dt.Rows.Count != 0)
            {
                this.Grilla.DataSource = dt;
                this.Grilla.PageIndex = PageIndex;
                this.Grilla.DataBind();
                //TextTC.Text = dt.Rows[0]["TipoCarrera"].ToString();
                btnGenerar.Visible = true;
                chbDeuda.Visible = true;
                btnSeleccionarTodo.Visible = true;
            }
            else
            {

                Grilla.DataBind();
                alerError.Visible = true;
                lblError.Text = "No hay alumnos para ese curso o concepto";
                btnGenerar.Visible = false;
                chbDeuda.Visible = false;
                btnSeleccionarTodo.Visible = false;
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

    protected void btnSeleccionarTodo_Click(object sender, EventArgs e)
    {
        try
        {

            if (btnSeleccionarTodo.Text == "Desmarcar todo")
            {
                foreach (GridViewRow row in Grilla.Rows)
                {
                    CheckBox check = row.FindControl("chkSeleccion") as CheckBox;

                    if (check.Checked == true)
                    {
                        check.Checked = false;

                    }
                }
                btnSeleccionarTodo.Text = "Seleccionar todo";
            }
            else
            {
                foreach (GridViewRow row in Grilla.Rows)
                {
                    CheckBox check = row.FindControl("chkSeleccion") as CheckBox;

                    if (check.Checked == false)
                    {
                        check.Checked = true;

                    }
                }
                btnSeleccionarTodo.Text = "Desmarcar todo";
            }

            //BtnGrabar.Enabled = true;
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

    protected void Grilla_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Sort" && e.CommandName != "Page" && e.CommandName != "")
            {
                string IC = ((HyperLink)Grilla.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Controls[1]).Text;

                if (AnioCursado.Text == "")
                {
                    DateTime fechaActual = DateTime.Today;
                    AnioCur = Convert.ToInt32(fechaActual.Year.ToString());

                }
                else
                {
                    AnioCur = Convert.ToInt32(AnioCursado.Text);
                }

                if (e.CommandName == "Eliminar")
                {
                    //ocnCurso.Eliminar(Convert.ToInt32(Id));
                    this.GrillaCargar2(this.Grilla.PageIndex);
                }

                if (e.CommandName == "Copiar")
                {
                    //ocnCurso = new GESTIONESCOLAR.Negocio.Curso(Convert.ToInt32(IC));
                    ////ocnCurso.Copiar();
                    //this.GrillaCargar(this.Grilla.PageIndex);
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

    protected void Grilla_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor; this.style.backgroundColor='#CCCCFF';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;");
        }
    }

    protected void Grilla_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (Session["Chequeras.PageIndex"] != null)
            {
                Session["Chequeras.PageIndex"] = e.NewPageIndex;
            }
            else
            {
                Session.Add("Chequeras.PageIndex", e.NewPageIndex);
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


    protected void btnAplicar_Click(object sender, EventArgs e)
    {
        alerError2.Visible = false;
        alerError.Visible = false;
        GrillaCargar2(Grilla.PageIndex);
    }

    protected void carId_SelectedIndexChanged(object sender, EventArgs e)
    {
        carIdCargar();
        alerError.Visible = false;
    }

    private void carIdCargar()
    {
        if (carId.SelectedIndex > 0)
        {
            DataTable dt = new DataTable();
            dt = ocnPlanEstudio.ObtenerListaPorUnaCarrera("[Seleccionar...]", Convert.ToInt32(carId.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                plaId.DataValueField = "Valor";
                plaId.DataTextField = "Texto";
                plaId.DataSource = (new GESTIONESCOLAR.Negocio.PlanEstudio()).ObtenerListaPorUnaCarrera("[Seleccionar...]", Convert.ToInt32(carId.SelectedValue));
                plaId.DataBind();
                plaId.Enabled = true;
            }
        }
    }

    protected void plaId_SelectedIndexChanged(object sender, EventArgs e)
    {
        plaIdCargar();
        alerError.Visible = false;
    }

    private void plaIdCargar()
    {
        if (plaId.SelectedIndex > 0)
        {

            //ClubB.Negocio.Evento ocnEvento = new ClubB.Negocio.Evento();
            DataTable dt = new DataTable();
            dt = ocnCurso.ObtenerListaPorUnPlanEstudio("[Seleccionar...]", Convert.ToInt32(plaId.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                curId.DataValueField = "Valor";
                curId.DataTextField = "Texto";
                curId.DataSource = (new GESTIONESCOLAR.Negocio.Curso()).ObtenerListaPorUnPlanEstudio("[Seleccionar...]", Convert.ToInt32(plaId.SelectedValue));
                curId.DataBind();
            }
        }
    }

    protected void curId_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int usuario = Convert.ToInt32(Session["_usuId"].ToString());
        //dt = ocnUsuarioEspacioCurricular.ObtenerUno(usuario);
        //if (dt.Rows[0]["carTipoCarrera"].ToString() == "3") // Si el Tipo de carrera es igual a 3 === Secundario
        //{

        //    //escId.DataValueField = "Valor"; escId.DataTextField = "Texto"; escId.DataSource = (new GESTIONESCOLAR.Negocio.UsuarioEspacioCurricular()).ObtenerListaxusuIdyCur("[Seleccionar...]", usuario, Convert.ToInt32(curId.SelectedValue)); escId.DataBind();

        //}
    }

    protected void ddlTipoConc_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            alerError.Visible = false;
            insId = Convert.ToInt32(Session["_Institucion"]);

            int TipoConc = Convert.ToInt32(ddlTipoConc.SelectedValue);
            DataTable dt = new DataTable();
            dt = ocnConceptos.ObtenerListaPorUnTipoConcepto("[Seleccionar...]", insId, Convert.ToInt32(ddlTipoConc.SelectedValue), Convert.ToInt32(AnioCursado.Text));
            if (dt.Rows.Count > 0)
            {
                ddlConc.DataValueField = "Valor";
                ddlConc.DataTextField = "Texto";
                ddlConc.DataSource = (new GESTIONESCOLAR.Negocio.Conceptos()).ObtenerListaPorUnTipoConcepto("[Seleccionar...]", insId, Convert.ToInt32(ddlTipoConc.SelectedValue), Convert.ToInt32(AnioCursado.Text));
                ddlConc.DataBind();
            }
            else
            {
                //LblMjeGridConcepto.Text = "No existe Concepto para ese Año Lectivo..";
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

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        try
        {
            insId = Convert.ToInt32(Session["_Institucion"]);
            alerError2.Visible = false;
            Int32 curso = Int32.Parse(curId.SelectedValue);
            Int32 anio = 0;
            int conId = Int32.Parse(ddlConc.SelectedValue.ToString());
            int conId2 = Int32.Parse(ddlConc.SelectedValue.ToString());
            int conId3 = Int32.Parse(ddlConc.SelectedValue.ToString());
            int conId4 = Int32.Parse(ddlConc.SelectedValue.ToString());
            if (AnioCursado.Text.Trim().ToString() != "")
            {
                anio = Convert.ToInt32(AnioCursado.Text.Trim().ToString());
            }

            DataTable dt1 = new DataTable();
            DataTable dtCA = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dtTot = new DataTable();
            //Int32 insid, Int32 conId, Int32 conAnioLectivo
            insId = Convert.ToInt32(lblInsId.Text);
            float Recargo = 0;
            String[] Cuotas;
            Cuotas = new String[3];
            String[] Vtos;
            Vtos = new String[3];
            float[] ImpMes;
            ImpMes = new float[3];

            String[] AplicaBeca;
            AplicaBeca = new String[3];
            String[] AplicaInteresAbierto;
            AplicaInteresAbierto = new String[3];
            int Seleccion = 0;
            int Becados = 0;
            String ApellidoyNombre;
            String Cuerpo, barra, DiasVtoEntreUnoyDos;

            ocnChequeraImpimir.Eliminar();

            Int32 TotRegistros;
            if (chbDeuda.Checked == true)
            {
                foreach (GridViewRow row in Grilla.Rows)
                {
                    CheckBox check = row.FindControl("chkSeleccion") as CheckBox;
                    int aluIdFila = 0;
                    aluIdFila = Convert.ToInt32(Grilla.DataKeys[row.RowIndex].Values["Id"]);

                    if ((check.Checked)) // Si esta seleccionado..
                    {
                        Seleccion = 1;
                        dt1 = ocnInscripcionConcepto.ChequeraImpimirObtenerporVarios(insId, Convert.ToInt32(ddlTipoConc.SelectedValue), Convert.ToInt32(AnioCursado.Text), aluIdFila, Convert.ToInt32(ddlDesde.SelectedValue), Convert.ToInt32(ddlHasta.SelectedValue));
                        if (dt1.Rows.Count > 0)
                        {

                            String NombreArchivo = dt1.Rows[0]["InsCodigo"].ToString().PadLeft(4, '0') + DateTime.Now.ToString("MMyyyy") + ".txt";
                            String Cabecera;
                            Cabecera = dt1.Rows[0]["InsCodigo"].ToString().PadLeft(4, '0') + DateTime.Now.ToString("ddMMyy");
                            ocnChequeraImpimir.Cuerpo = Cabecera;
                            //ocnChequeraImpimir.InsertarCuerpo();

                            TotRegistros = dt1.Rows.Count + 2;

                            Single intMensual = '0';
                            //Single ImpConBecaConInteresMensualCalculado = '0';
                            //Single interesMensualCalculado = '0';
                            Single ImpConBeca = '0';
                            int NroCuota = 0;
                            int NroCuotaSegunFechaEmision = 0;
                            int MultiplicadorCuotas = 0;
                            int CantVtos = 0;

                            Vtos[0] = "";
                            Vtos[1] = "";
                            Vtos[2] = "";

                            foreach (DataRow row2 in dt1.Rows)
                            {

                                //NroCuota = Convert.ToInt32(row2["icoNroCuota"].ToString());
                                //NroCuotaSegunFechaEmision = Convert.ToInt32(txtDesde.Text.ToString().Substring(3, 2)) - 3;
                                //if (NroCuotaSegunFechaEmision <= 0)
                                //{
                                //    NroCuotaSegunFechaEmision = 1;
                                //}
                                ////MultiplicadorCuotas = (NroCuota +1) - NroCuotaSegunFechaEmision;
                                //MultiplicadorCuotas = (NroCuotaSegunFechaEmision + 1) - NroCuota;


                                //ImpConBecaConInteresMensualCalculado = Convert.ToSingle(row["ImpConBecaConInteresMensualCalculado"].ToString());
                                intMensual = Convert.ToSingle(row2["ConInteresMensual"].ToString());
                                //interesMensualCalculado = Convert.ToSingle(row["InteresMensualCalculado"].ToString());
                                ImpConBeca = Convert.ToSingle(row2["ImpConBeca"].ToString());
                                CantVtos = Convert.ToInt32(row2["conCantVtos"].ToString());



                                Cuotas[0] = row2["IcoNroCuota"].ToString();
                                Cuotas[1] = row2["conMesInicio"].ToString();
                                Cuotas[2] = String.Format(row2["IcoNroCuota"].ToString(), "00");
                                Int32 C = 0;


                                dt2 = ocnConceptosIntereses.ObtenerListaxconIdxNroCuota(Convert.ToInt32(row2["conId"].ToString()), (Convert.ToInt32(row2["IcoNroCuota"].ToString())));
                                if (dt2.Rows.Count > 0)
                                {
                                    foreach (DataRow row3 in dt2.Rows)
                                    {
                                        AplicaBeca[C] = row3["coiAplicaBeca"].ToString();
                                        AplicaInteresAbierto[C] = row3["coiAplicaInteresAbierto"].ToString();

                                        Vtos[C] = row3["FechaVto"].ToString();
                                        Recargo += Convert.ToSingle(row3["coiValorInteres"].ToString());
                                        if (row3["coiAplicaBeca"].ToString() == "1")
                                        {
                                            Single ValorInteresCI = 0;
                                            //Single ValorInteresCI1, ValorInteresCI2;
                                            if (Convert.ToDecimal(row2["BecId"].ToString()) > 0)
                                            {
                                                //ValorInteresCI1 = Convert.ToSingle(row3["coiValorInteres"].ToString());
                                                //ValorInteresCI2 = Convert.ToSingle(row2["BecInteres"].ToString());

                                                ValorInteresCI = ((Convert.ToSingle(row3["coiValorInteres"].ToString()) * Convert.ToSingle(row2["BecInteres"].ToString())) / 100);
                                                ValorInteresCI = Convert.ToSingle(Math.Round(Convert.ToDecimal(ValorInteresCI), 2));
                                                //ValorInteresCI = Math.Round((Convert.ToDecimal(row4["ValorInteres"].ToString()) * Convert.ToDecimal(dt9.Rows[0]["BecInteres"].ToString()) / 100), 2);

                                                ImpMes[C] = Convert.ToSingle(Convert.ToSingle(row2["ImpConBeca"].ToString()) + ValorInteresCI);
                                            } else
                                            {
                                                ImpMes[C] = Convert.ToSingle(row2["ImpConBeca"].ToString()) + Convert.ToSingle(row3["coiValorInteres"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            ImpMes[C] = Convert.ToSingle(row2["icoImporte"].ToString()) + Convert.ToSingle(row3["coiValorInteres"].ToString());
                                        }
                                        C += 1;
                                    }
                                }
                                if (C == 2 && Vtos[2] == "")  // Si solo tiene 2 Vtos definidos pongo como 3 Vto al 2 Vto para evaluar más abajo si aplica interés mensual o no
                                {
                                    Vtos[2] = Vtos[1];
                                }
                                if (row2["conTieneVtoAbierto"].ToString() == "1")
                                {
                                    if (AplicaInteresAbierto[C - 1] == "0")
                                    {
                                        if (AplicaBeca[C - 1] == "1")
                                        {
                                            Recargo = Convert.ToSingle(row2["ImpConBeca"].ToString());
                                            ImpMes[C] = Convert.ToSingle(row2["ImpConBeca"].ToString());
                                        }
                                        else
                                        {
                                            Recargo = Convert.ToSingle(row2["icoImporte"].ToString());
                                            ImpMes[C] = Convert.ToSingle(row2["icoImporte"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        Recargo = Convert.ToSingle(row2["icoImporte"].ToString()) + Convert.ToSingle(row2["conRecargoVtoAbierto"].ToString());
                                        ImpMes[C] = Convert.ToSingle(row2["icoImporte"].ToString()) + Convert.ToSingle(row2["conRecargoVtoAbierto"].ToString());
                                    }
                                }
                                else
                                {

                                    // Modificado 08/02/2024
                                    if (C < 3)
                                    {
                                        ImpMes[C] = ImpMes[C - 1];
                                        Vtos[C] = Vtos[C - 1];
                                    }
                                    //Modificado 08/02/2024
                                    // Si tiene interés mensual y la fecha de generación del archivo es mayor que la del último vto, entonces, pongo en el importe
                                    // de esa cuota, en el último vto, el importe de la cuota + el interés mensual
                                    //if (intMensual > 0 && Convert.ToDateTime(txtDesde.Text.ToString()) > Convert.ToDateTime(Vtos[2].ToString()) && CantVtos == 2)
                                    //{
                                    //    //ImpMes[2] = (interesMensualCalculado * MultiplicadorCuotas) + ImpMes[1];
                                    //    //Single pp = (((ImpMes[1] * intMensual) / 100) * MultiplicadorCuotas);
                                    //    ImpMes[2] = (((ImpMes[1] * intMensual) / 100) * MultiplicadorCuotas) + ImpMes[1];
                                    //    Vtos[2] = txtHasta.Text;
                                    //}

                                }
                                //if (intMensual > 0 && Convert.ToDateTime(txtDesde.Text.ToString()) > Convert.ToDateTime(Vtos[2].ToString()))
                                //{
                                //    Vtos[2] = txtHasta.Text;
                                //}


                                //Cuotas(2) = Format("01/" & (recbusco!MesInicio + (recbusco!CodValor - 1)), "mmmm")

                                int MesArchivo = Convert.ToInt32(row2["ConMesInicio"]) + Convert.ToInt32(row2["IcoNroCuota"]) - 1;

                                String mesanio = Convert.ToDateTime(MesArchivo.ToString() + "/" + row2["icuAnoCursado"]).ToString("MMMM yyyy");

                                ApellidoyNombre = row2["aluNombre"].ToString();
                                if (row2["cntId"].ToString() == "1")
                                {
                                    ApellidoyNombre = "Inscripción " + row2["icuAnoCursado"].ToString() + " - " + ApellidoyNombre;
                                }
                                else
                                {
                                    ApellidoyNombre = mesanio + " - " + ApellidoyNombre;
                                }

                                if (ApellidoyNombre.Length < 40)
                                {
                                    ApellidoyNombre = ApellidoyNombre.PadRight(40);
                                }
                                if (ApellidoyNombre.Length > 40)
                                {
                                    ApellidoyNombre = ApellidoyNombre.Substring(0, 40);
                                }

                                //string.Format("{0:000000000000}",  dt1.Rows[0]["aluDoc"].ToString());
                                //myString.PadLeft(myString.Length + 5, '0');
                                Cuerpo = row2["aluDoc"].ToString().PadLeft(12, '0');  // Format("000000000000");
                                Cuerpo = Cuerpo + ApellidoyNombre;
                                Cuerpo = Cuerpo + Convert.ToDateTime(Vtos[0]).ToString("ddMMyy");
                                Cuerpo = Cuerpo + (ImpMes[0] * 100).ToString().PadLeft(10, '0');  //Format(CStr(ImpsMes(1) * 100), "0000000000")
                                Cuerpo = Cuerpo + Convert.ToDateTime(Vtos[1]).ToString("ddMMyy");
                                Cuerpo = Cuerpo + (ImpMes[1] * 100).ToString().PadLeft(10, '0');
                                Cuerpo = Cuerpo + Convert.ToDateTime(Vtos[2]).ToString("ddMMyy");
                                Cuerpo = Cuerpo + (ImpMes[2] * 100).ToString().PadLeft(10, '0');

                                barra = "4" + row2["insCodigo"].ToString().PadLeft(3, '0');   // string.Format("{0:C3}", dt1.Rows[0]["insCodigo"].ToString());
                                barra = barra + row2["insId"].ToString().PadLeft(3, '0');   //string.Format("{0:C3}", dt1.Rows[0]["insId"].ToString());
                                barra = barra + row2["aluId"].ToString().PadLeft(5, '0');   //string.Format("{0:C5}", dt1.Rows[0]["aluId"].ToString());
                                barra = barra + Cuotas[2].ToString().PadLeft(2, '0');     //string.Format("{0:C2}", Cuotas[2]);
                                                                                          //String pp = (Convert.ToInt32(Convert.ToDateTime(Vtos[0]).ToString("yyyyMMdd")) - Convert.ToInt32(Convert.ToDateTime("1/1/1996").ToString("yyyyMMdd"))).ToString().PadLeft(4, '0');



                                if (Convert.ToInt32((Convert.ToDateTime(Vtos[1]) - Convert.ToDateTime("1/1/1996")).TotalDays.ToString()) <= 9999)
                                //if (Convert.ToInt32(Convert.ToDateTime(Vtos[0]).ToString("yyyyMMdd")) - Convert.ToInt32(Convert.ToDateTime("1/1/1996").ToString("yyyyMMdd")) <= 9999)
                                {
                                    String barra1 = (Convert.ToDateTime(Vtos[0]) - Convert.ToDateTime("01/01/1996")).TotalDays.ToString().PadLeft(4, '0');
                                    barra = barra + (Convert.ToDateTime(Vtos[0]) - Convert.ToDateTime("01/01/1996")).TotalDays.ToString().PadLeft(4, '0');
                                    //barra = barra + (Convert.ToInt32(Convert.ToDateTime(Vtos[0]).ToString("yyyyMMdd")) - Convert.ToInt32(Convert.ToDateTime("01/01/1996").ToString("yyyyMMdd"))).ToString().PadLeft(4, '0');
                                }
                                else
                                {
                                    barra = barra + "9999";
                                }
                                barra = barra + (ImpMes[0] * 10).ToString().PadLeft(6, '0');   //string.Format("{0:C6}", (ImpMes[0] * 10).ToString());

                                DiasVtoEntreUnoyDos = "99";
                                if (Convert.ToInt32((Convert.ToDateTime(Vtos[1]) - Convert.ToDateTime(Vtos[0])).TotalDays.ToString()) <= 99)
                                {
                                    DiasVtoEntreUnoyDos = (Convert.ToDateTime(Vtos[1]) - Convert.ToDateTime(Vtos[0])).TotalDays.ToString().PadLeft(2, '0');
                                    //DiasVtoEntreUnoyDos = (Convert.ToInt32(Convert.ToDateTime(Vtos[1]).ToString("yyyyMMdd")) - Convert.ToInt32(Convert.ToDateTime(Vtos[0]).ToString("yyyyMMdd"))).ToString().PadLeft(2, '0');
                                }
                                barra = barra + DiasVtoEntreUnoyDos;

                                barra = barra + ((Convert.ToSingle(ImpMes[1]) - Convert.ToSingle(ImpMes[0])) * 10).ToString().PadLeft(4, '0');

                                String fechaVto2 = Vtos[2];
                                String fechaVto0 = Vtos[0];


                                if (Convert.ToInt32((Convert.ToDateTime(Vtos[2]) - Convert.ToDateTime(Vtos[0])).TotalDays.ToString()) <= 999)
                                {
                                    barra = barra + (Convert.ToDateTime(Vtos[2]) - Convert.ToDateTime(Vtos[0])).TotalDays.ToString().PadLeft(3, '0');   //Format(CInt((CDate(Vtos(3)) - CDate(Vtos(1)))), "000")
                                }
                                else
                                {
                                    barra = barra + "999";
                                }
                                //barra = barra + ((Convert.ToSingle(ImpMes[1]) - Convert.ToSingle(ImpMes[0])) * 10).ToString().PadLeft(4, '0');
                                barra = barra + ((Convert.ToSingle(ImpMes[2]) - Convert.ToSingle(ImpMes[0])) * 10).ToString().PadLeft(4, '0');   // Format(((ImpsMes(3) - CSng(ImpsMes(1))) * 10), "0000")

                                Cuerpo = Cuerpo + barra;

                                ocnChequeraImpimir.barra = barra;
                                ocnChequeraImpimir.codcole = Convert.ToInt32(row2["insId"].ToString());
                                ocnChequeraImpimir.codalumno = Convert.ToInt32(row2["aluId"].ToString());
                                ocnChequeraImpimir.apellidoynombre = row2["aluNombre"].ToString();
                                ocnChequeraImpimir.telef = row2["aluTelefono"].ToString();
                                ocnChequeraImpimir.dni = row2["aluDoc"].ToString();
                                ocnChequeraImpimir.numcuota = row2["icoNroCuota"].ToString();
                                ocnChequeraImpimir.privto = Convert.ToDateTime(Vtos[0]).ToString("dd/MM/yy"); //string.Format("{0:dd/mm/yy}", Vtos[0]);
                                ocnChequeraImpimir.priimporte = Convert.ToDecimal(ImpMes[0]);
                                ocnChequeraImpimir.segvto = Convert.ToDateTime(Vtos[1]).ToString("dd/MM/yy");
                                ocnChequeraImpimir.segimporte = Convert.ToDecimal(ImpMes[1]);
                                ocnChequeraImpimir.tervto = Convert.ToDateTime(Vtos[2]).ToString("dd/MM/yy");
                                ocnChequeraImpimir.impabierto = Convert.ToDecimal(ImpMes[2]);
                                ocnChequeraImpimir.concepto = row2["conNombre"].ToString();
                                ocnChequeraImpimir.curso = row2["curNombre"].ToString();
                                ocnChequeraImpimir.aniolectivo = Convert.ToInt32(row2["conAniolectivo"].ToString());
                                ocnChequeraImpimir.beca = row2["becNombre"].ToString();
                                ocnChequeraImpimir.Cuerpo = Cuerpo;
                                ocnChequeraImpimir.CONCEPTOID = row2["CnceptoId"].ToString();
                                ocnChequeraImpimir.Insertar();


                            }
                            Double TotalPrimerVto, TotalSegVto, TotalTerVto;
                            TotalPrimerVto = 0;
                            TotalSegVto = 0;
                            TotalTerVto = 0;
                            dtTot = ocnChequeraImpimir.ObtenerTotales();

                            if (dtTot.Rows.Count > 0)
                            {
                                TotalPrimerVto = Convert.ToDouble(dtTot.Rows[0]["P"].ToString());
                                TotalSegVto = Convert.ToDouble(dtTot.Rows[0]["S"].ToString());
                                TotalTerVto = Convert.ToDouble(dtTot.Rows[0]["T"].ToString());
                                TotalPrimerVto = TotalPrimerVto * 100;
                                TotalSegVto = TotalSegVto * 100;
                                TotalTerVto = TotalTerVto * 100;
                            }
                            String Pie;
                            Pie = TotRegistros.ToString().PadLeft(12, '0') + TotalPrimerVto.ToString().PadLeft(18, '0') + TotalSegVto.ToString().PadLeft(18, '0') + TotalTerVto.ToString().PadLeft(18, '0');
                            ocnChequeraImpimir.Cuerpo = Pie;
                            //ocnChequeraImpimir.InsertarCuerpo();

                        }
                        else
                        {
                            Becados = 1;
                        }
                    }
                }
            }
            else
            {
                foreach (GridViewRow row in Grilla.Rows)
                {
                    CheckBox check = row.FindControl("chkSeleccion") as CheckBox;
                    int aluIdFila = 0;
                    aluIdFila = Convert.ToInt32(Grilla.DataKeys[row.RowIndex].Values["Id"]);

                    if ((check.Checked)) // Si esta seleccionado..
                    {
                        Seleccion = 1;
                        dt1 = ocnInscripcionConcepto.ChequeraImpimirObtenerporVariosPyNP(insId, Convert.ToInt32(ddlTipoConc.SelectedValue), Convert.ToInt32(AnioCursado.Text), aluIdFila, Convert.ToInt32(ddlDesde.SelectedValue), Convert.ToInt32(ddlHasta.SelectedValue));
                        if (dt1.Rows.Count > 0)
                        {

                            String NombreArchivo = dt1.Rows[0]["InsCodigo"].ToString().PadLeft(4, '0') + DateTime.Now.ToString("MMyyyy") + ".txt";
                            String Cabecera;
                            Cabecera = dt1.Rows[0]["InsCodigo"].ToString().PadLeft(4, '0') + DateTime.Now.ToString("ddMMyy");
                            ocnChequeraImpimir.Cuerpo = Cabecera;
                            //ocnChequeraImpimir.InsertarCuerpo();

                            TotRegistros = dt1.Rows.Count + 2;

                            foreach (DataRow row2 in dt1.Rows)
                            {
                                //Int32 ooo, rrr, fff, lll;
                                //ooo = Convert.ToInt32(row["aluId"].ToString());
                                //rrr = Convert.ToInt32(dt1.Rows[0]["IcoNroCuota"].ToString());
                                //fff = Convert.ToInt32(dt1.Rows[0]["ConAnioLectivo"].ToString());

                                //if (ooo == 426 & fff==2020)
                                //{
                                //    lll = 1;
                                //}

                                Cuotas[0] = row2["IcoNroCuota"].ToString();
                                Cuotas[1] = row2["conMesInicio"].ToString();
                                Cuotas[2] = String.Format(row2["IcoNroCuota"].ToString(), "00");
                                Int32 C = 0;


                                dt2 = ocnConceptosIntereses.ObtenerListaxconIdxNroCuota(Convert.ToInt32(row2["conId"].ToString()), (Convert.ToInt32(row2["IcoNroCuota"].ToString())));
                                if (dt2.Rows.Count > 0)
                                {
                                    foreach (DataRow row3 in dt2.Rows)
                                    {
                                        AplicaBeca[C] = row3["coiAplicaBeca"].ToString();
                                        AplicaInteresAbierto[C] = row3["coiAplicaInteresAbierto"].ToString();

                                        Vtos[C] = row3["FechaVto"].ToString();
                                        Recargo += Convert.ToSingle(row3["coiValorInteres"].ToString());
                                        if (row3["coiAplicaBeca"].ToString() == "1")
                                        {
                                            ImpMes[C] = Convert.ToSingle(row2["ImpConBeca"].ToString()) + Convert.ToSingle(row3["coiValorInteres"].ToString());
                                        }
                                        else
                                        {
                                            ImpMes[C] = Convert.ToSingle(row2["icoImporte"].ToString()) + Convert.ToSingle(row3["coiValorInteres"].ToString());
                                        }
                                        C += 1;
                                    }
                                }
                                if (row2["conTieneVtoAbierto"].ToString() == "1")
                                {
                                    if (AplicaInteresAbierto[C - 1] == "0")
                                    {
                                        if (AplicaBeca[C - 1] == "1")
                                        {
                                            Recargo = Convert.ToSingle(row2["ImpConBeca"].ToString());
                                            ImpMes[C] = Convert.ToSingle(row2["ImpConBeca"].ToString());
                                        }
                                        else
                                        {
                                            Recargo = Convert.ToSingle(row2["icoImporte"].ToString());
                                            ImpMes[C] = Convert.ToSingle(row2["icoImporte"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        Recargo = Convert.ToSingle(row2["icoImporte"].ToString()) + Convert.ToSingle(row2["conRecargoVtoAbierto"].ToString());
                                        ImpMes[C] = Convert.ToSingle(row2["icoImporte"].ToString()) + Convert.ToSingle(row2["conRecargoVtoAbierto"].ToString());
                                    }
                                }
                                else
                                {
                                    //String eee, ccc, tttt;
                                    //eee = row["aluDoc"].ToString();
                                    //ccc = row["IcoNroCuota"].ToString();
                                    //tttt = row["conNombre"].ToString();

                                    //ImpMes[C] = ImpMes[C - 1];
                                }
                                if (Vtos[2] == null)
                                {
                                    //Vtos[2] = txtHasta.Text;
                                }

                                //Cuotas(2) = Format("01/" & (recbusco!MesInicio + (recbusco!CodValor - 1)), "mmmm")

                                int MesArchivo = Convert.ToInt32(row2["ConMesInicio"]) + Convert.ToInt32(row2["IcoNroCuota"]) - 1;

                                String mesanio = Convert.ToDateTime(MesArchivo.ToString() + "/" + row2["icuAnoCursado"]).ToString("MMMM yyyy");

                                ApellidoyNombre = row2["aluNombre"].ToString();
                                if (row2["cntId"].ToString() == "1")
                                {
                                    ApellidoyNombre = "Inscripción " + row2["icuAnoCursado"].ToString() + " - " + ApellidoyNombre;
                                }
                                else
                                {
                                    ApellidoyNombre = mesanio + " - " + ApellidoyNombre;
                                }

                                if (ApellidoyNombre.Length < 40)
                                {
                                    ApellidoyNombre = ApellidoyNombre.PadRight(40);
                                }
                                if (ApellidoyNombre.Length > 40)
                                {
                                    ApellidoyNombre = ApellidoyNombre.Substring(0, 40);
                                }

                                //string.Format("{0:000000000000}",  dt1.Rows[0]["aluDoc"].ToString());
                                //myString.PadLeft(myString.Length + 5, '0');
                                Cuerpo = row2["aluDoc"].ToString().PadLeft(12, '0');  // Format("000000000000");
                                Cuerpo = Cuerpo + ApellidoyNombre;
                                Cuerpo = Cuerpo + Convert.ToDateTime(Vtos[0]).ToString("ddMMyy");
                                Cuerpo = Cuerpo + (ImpMes[0] * 100).ToString().PadLeft(10, '0');  //Format(CStr(ImpsMes(1) * 100), "0000000000")
                                Cuerpo = Cuerpo + Convert.ToDateTime(Vtos[1]).ToString("ddMMyy");
                                Cuerpo = Cuerpo + (ImpMes[1] * 100).ToString().PadLeft(10, '0');
                                Cuerpo = Cuerpo + Convert.ToDateTime(Vtos[2]).ToString("ddMMyy");
                                Cuerpo = Cuerpo + (ImpMes[2] * 100).ToString().PadLeft(10, '0');

                                barra = "4" + row2["insCodigo"].ToString().PadLeft(3, '0');   // string.Format("{0:C3}", dt1.Rows[0]["insCodigo"].ToString());
                                barra = barra + row2["insId"].ToString().PadLeft(3, '0');   //string.Format("{0:C3}", dt1.Rows[0]["insId"].ToString());
                                barra = barra + row2["aluId"].ToString().PadLeft(5, '0');   //string.Format("{0:C5}", dt1.Rows[0]["aluId"].ToString());
                                barra = barra + Cuotas[2].ToString().PadLeft(2, '0');     //string.Format("{0:C2}", Cuotas[2]);
                                                                                          //String pp = (Convert.ToInt32(Convert.ToDateTime(Vtos[0]).ToString("yyyyMMdd")) - Convert.ToInt32(Convert.ToDateTime("1/1/1996").ToString("yyyyMMdd"))).ToString().PadLeft(4, '0');



                                if (Convert.ToInt32((Convert.ToDateTime(Vtos[1]) - Convert.ToDateTime("1/1/1996")).TotalDays.ToString()) <= 9999)
                                //if (Convert.ToInt32(Convert.ToDateTime(Vtos[0]).ToString("yyyyMMdd")) - Convert.ToInt32(Convert.ToDateTime("1/1/1996").ToString("yyyyMMdd")) <= 9999)
                                {
                                    String barra1 = (Convert.ToDateTime(Vtos[0]) - Convert.ToDateTime("01/01/1996")).TotalDays.ToString().PadLeft(4, '0');
                                    barra = barra + (Convert.ToDateTime(Vtos[0]) - Convert.ToDateTime("01/01/1996")).TotalDays.ToString().PadLeft(4, '0');
                                    //barra = barra + (Convert.ToInt32(Convert.ToDateTime(Vtos[0]).ToString("yyyyMMdd")) - Convert.ToInt32(Convert.ToDateTime("01/01/1996").ToString("yyyyMMdd"))).ToString().PadLeft(4, '0');
                                }
                                else
                                {
                                    barra = barra + "9999";
                                }
                                barra = barra + (ImpMes[0] * 10).ToString().PadLeft(6, '0');   //string.Format("{0:C6}", (ImpMes[0] * 10).ToString());

                                DiasVtoEntreUnoyDos = "99";
                                if (Convert.ToInt32((Convert.ToDateTime(Vtos[1]) - Convert.ToDateTime(Vtos[0])).TotalDays.ToString()) <= 99)
                                {
                                    DiasVtoEntreUnoyDos = (Convert.ToDateTime(Vtos[1]) - Convert.ToDateTime(Vtos[0])).TotalDays.ToString().PadLeft(2, '0');
                                    //DiasVtoEntreUnoyDos = (Convert.ToInt32(Convert.ToDateTime(Vtos[1]).ToString("yyyyMMdd")) - Convert.ToInt32(Convert.ToDateTime(Vtos[0]).ToString("yyyyMMdd"))).ToString().PadLeft(2, '0');
                                }
                                barra = barra + DiasVtoEntreUnoyDos;

                                barra = barra + ((Convert.ToSingle(ImpMes[1]) - Convert.ToSingle(ImpMes[0])) * 10).ToString().PadLeft(4, '0');

                                String fechaVto2 = Vtos[2];
                                String fechaVto0 = Vtos[0];

                                //if (row["aluDoc"].ToString() == "44602937")
                                //{
                                //    String mesanioArch = Convert.ToDateTime(MesArchivo.ToString() + "/" + row["icuAnoCursado"]).ToString("MMMM yyyy");

                                //    String fecha2 = Convert.ToDateTime(Vtos[2]).ToString("yyyyMMdd");
                                //    String fecha0 = Convert.ToDateTime(Vtos[0]).ToString("yyyyMMdd");
                                //    Int32 Vto2 = Convert.ToInt32(Convert.ToDateTime(Vtos[2]).ToString("yyyyMMdd"));
                                //    Int32 Vto0 = Convert.ToInt32(Convert.ToDateTime(Vtos[0]).ToString("yyyyMMdd"));


                                //    String rr = (Convert.ToDateTime(Vtos[1]) - Convert.ToDateTime("1/1/1996")).TotalDays.ToString();

                                //    String V1S = (Convert.ToDateTime(Vtos[2]) - Convert.ToDateTime(Vtos[0])).ToString();
                                //    TimeSpan V1TS = Convert.ToDateTime(Vtos[2]) - Convert.ToDateTime(Vtos[0]);

                                //    String V1I = V1TS.TotalDays.ToString();
                                //    String V1IPL = V1TS.TotalDays.ToString().PadLeft(3, '0');

                                //    // Convert.ToInt32(  ).ToString().PadLeft(3, '0');
                                //}


                                if (Convert.ToInt32((Convert.ToDateTime(Vtos[2]) - Convert.ToDateTime(Vtos[0])).TotalDays.ToString()) <= 999)
                                {
                                    // barra = barra + (Convert.ToInt32(Convert.ToDateTime(Vtos[2]).ToString("yyyyMMdd")) - Convert.ToInt32(Convert.ToDateTime(Vtos[0]).ToString("yyyyMMdd"))).ToString().PadLeft(3, '0');   //Format(CInt((CDate(Vtos(3)) - CDate(Vtos(1)))), "000")
                                    barra = barra + (Convert.ToDateTime(Vtos[2]) - Convert.ToDateTime(Vtos[0])).TotalDays.ToString().PadLeft(3, '0');   //Format(CInt((CDate(Vtos(3)) - CDate(Vtos(1)))), "000")
                                }
                                else
                                {
                                    barra = barra + "999";
                                }
                                //barra = barra + ((Convert.ToSingle(ImpMes[1]) - Convert.ToSingle(ImpMes[0])) * 10).ToString().PadLeft(4, '0');
                                barra = barra + ((Convert.ToSingle(ImpMes[2]) - Convert.ToSingle(ImpMes[0])) * 10).ToString().PadLeft(4, '0');   // Format(((ImpsMes(3) - CSng(ImpsMes(1))) * 10), "0000")

                                Cuerpo = Cuerpo + barra;

                                ocnChequeraImpimir.barra = barra;
                                ocnChequeraImpimir.codcole = Convert.ToInt32(row2["insId"].ToString());
                                ocnChequeraImpimir.codalumno = Convert.ToInt32(row2["aluId"].ToString());
                                ocnChequeraImpimir.apellidoynombre = row2["aluNombre"].ToString();
                                ocnChequeraImpimir.telef = row2["aluTelefono"].ToString();
                                ocnChequeraImpimir.dni = row2["aluDoc"].ToString();
                                ocnChequeraImpimir.numcuota = row2["icoNroCuota"].ToString();
                                ocnChequeraImpimir.privto = Convert.ToDateTime(Vtos[0]).ToString("dd/MM/yy"); //string.Format("{0:dd/mm/yy}", Vtos[0]);
                                ocnChequeraImpimir.priimporte = Convert.ToDecimal(ImpMes[0]);
                                ocnChequeraImpimir.segvto = Convert.ToDateTime(Vtos[1]).ToString("dd/MM/yy");
                                ocnChequeraImpimir.segimporte = Convert.ToDecimal(ImpMes[1]);
                                ocnChequeraImpimir.tervto = Convert.ToDateTime(Vtos[2]).ToString("dd/MM/yy");
                                ocnChequeraImpimir.impabierto = Convert.ToDecimal(ImpMes[2]);
                                ocnChequeraImpimir.concepto = row2["conNombre"].ToString();
                                ocnChequeraImpimir.curso = row2["curNombre"].ToString();
                                ocnChequeraImpimir.aniolectivo = Convert.ToInt32(row2["conAniolectivo"].ToString());
                                ocnChequeraImpimir.beca = row2["becNombre"].ToString();
                                ocnChequeraImpimir.Cuerpo = Cuerpo;
                                ocnChequeraImpimir.CONCEPTOID = row2["CnceptoId"].ToString();
                                ocnChequeraImpimir.Insertar();


                            }
                            Double TotalPrimerVto, TotalSegVto, TotalTerVto;
                            TotalPrimerVto = 0;
                            TotalSegVto = 0;
                            TotalTerVto = 0;
                            dtTot = ocnChequeraImpimir.ObtenerTotales();



                            if (dtTot.Rows.Count > 0)
                            {
                                TotalPrimerVto = Convert.ToDouble(dtTot.Rows[0]["P"].ToString());
                                TotalSegVto = Convert.ToDouble(dtTot.Rows[0]["S"].ToString());
                                TotalTerVto = Convert.ToDouble(dtTot.Rows[0]["T"].ToString());
                                TotalPrimerVto = TotalPrimerVto * 100;
                                TotalSegVto = TotalSegVto * 100;
                                TotalTerVto = TotalTerVto * 100;
                            }
                            String Pie;
                            Pie = TotRegistros.ToString().PadLeft(12, '0') + TotalPrimerVto.ToString().PadLeft(18, '0') + TotalSegVto.ToString().PadLeft(18, '0') + TotalTerVto.ToString().PadLeft(18, '0');
                            ocnChequeraImpimir.Cuerpo = Pie;
                            //ocnChequeraImpimir.InsertarCuerpo();
                           
                        }
                        else
                        {
                            Becados = 1;
                        }
                    }

                    //NomRep = "InformeListadoBecados.rpt";
                    //FuncionesUtiles.AbreVentana("Reporte.aspx?insId=" + insId + "&curso=" + curso + "&anio=" + anio + "&Concepto=" + Concepto + "&NomRep=" + NomRep);
                }


            }
            if (Seleccion == 0)
            {
                alerError2.Visible = true;
                lblalerError2.Text = "Seleccione uno o más alumnos..";
                return;
            }
            else
            {
                if (Becados == 1)
                {
                    alerError2.Visible = true;
                    lblalerError2.Text = "Se imprime solo alumnos no becados o cuya beca < a 100";
                    return;
                }
            }

            dtCA = ocnChequeraImpimir.ObtenerTodo();
            if (dtCA.Rows.Count > 0)
            {
                ImprimirChequera();
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


    protected void AnioCursado_TextChanged(object sender, EventArgs e)
    {
        ddlTipoConc.DataValueField = "Valor"; ddlTipoConc.DataTextField = "Texto"; ddlTipoConc.DataSource = (new GESTIONESCOLAR.Negocio.ConceptosTipos()).ObtenerLista("[Seleccionar...]"); ddlTipoConc.DataBind();
        ddlConc.ClearSelection();

    }




    private void ImprimirChequera()
    {
        DataTable dt = new DataTable();
        dt = ocnChequeraImpimir.InformeChequeraImprimir2();
        int i = 1;
        int j = 1;
        String PriVto = "";
        String SegVto = "";
        String TerVto = "";
        String PriImporte = "";
        String SegImporte = "";
        String TerImporte = "";
        // Creamos el tipo de Font que vamos utilizar
        iTextSharp.text.Font _standardFontBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font _standardFontLBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font _standardFontL = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        // Creamos el documento con el tamaño de página tradicional
        Document doc = new Document(PageSize.A4, 15f, 10f, 10f, 10f);

        // Indicamos donde vamos a guardar el documento

        string NomRep, Imagenes, ImageLogo;
        NomRep = "~/PaginasGenerales/Chequeras/chequera.pdf";
        Imagenes = "~/PaginasGenerales/Imagenes/loguito1.jpg";
        ImageLogo = MapPath(Imagenes);
        string Ruta = MapPath(NomRep);  //System.Web.UI.Page.Server.MapPath(NomRep);
        NomRep = Ruta;

        //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\Aplicativos\GestionEscolarFra\chequera.pdf", FileMode.Create));
        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(NomRep, FileMode.Create));

        if (dt.Rows.Count > 0)
        {
            //Document doc = new Document(PageSize.LETTER);

            // Abrimos el archivo
            doc.Open();

            Int32 c = 1;
            foreach (DataRow row in dt.Rows)// por cada fila..
            {
                if (c == 1)
                {
                    doc.SetMargins(15f, 10f, 10f, 0f);
                }
                else
                {
                    doc.SetMargins(15f, 10f, 0f, 0f);
                }

                PdfPTable tblEncabeza = new PdfPTable(6) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 100 };
                tblEncabeza.SetWidths(new float[] { 4f, 23f, 4f, 23f, 4f, 42f });  //2603222
                tblEncabeza.DefaultCell.Border = 0;

                PdfPTable tblLogo = new PdfPTable(1) { HorizontalAlignment = Element.ALIGN_LEFT };
                tblLogo.SetWidths(new float[] { 3f});  
                
                // add a image
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(ImageLogo);
                jpg.Alignment = Element.ALIGN_LEFT;
                PdfPCell imageCell = new PdfPCell(jpg);
                imageCell.BorderWidth = 0;
                tblLogo.AddCell(imageCell);

                tblEncabeza.AddCell(tblLogo);

                //tblEncabeza.AddCell(imageCell);

                PdfPTable tblDatos = new PdfPTable(1) { HorizontalAlignment = Element.ALIGN_LEFT };                
                tblDatos.SetWidths(new float[] { 23f });  //2603222

                tblDatos.DefaultCell.Border = 0;
                PdfPCell clC1 = new PdfPCell(new Phrase("INST. MADRE MERCEDES GUERRA", _standardFontBold));
                //clC1.FixedHeight = 0.1f;
                clC1.BorderWidth = 0;

                PdfPCell clC2 = new PdfPCell(new Phrase("Hnos. Terc. Franciscanas de la caridad", _standardFont));
                //clC2.FixedHeight = 0.1f;                
                clC2.BorderWidth = 0;
                
                PdfPCell clC3 = new PdfPCell(new Phrase("Incorporado a la enseñanza oficial", _standardFont));
                //clC3.FixedHeight = 0.1f;
                clC3.BorderWidth = 0;

                PdfPCell clC4 = new PdfPCell(new Phrase("CUIT: 3063012583-5 IVA EXENTO", _standardFont));
                clC4.BorderWidth = 0;


                tblDatos.AddCell(clC1);
                tblDatos.AddCell(clC2);
                tblDatos.AddCell(clC3);
                tblDatos.AddCell(clC4);

                tblEncabeza.AddCell(tblDatos);

                tblEncabeza.AddCell(tblLogo);

                tblEncabeza.AddCell(tblDatos);

                tblEncabeza.AddCell(tblLogo);

                tblEncabeza.AddCell(tblDatos);


                doc.Add(tblEncabeza);

                ///////////////////

                PdfPTable tblPrueba = new PdfPTable(3) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 100 };
                tblPrueba.SetWidths(new float[] { 27f, 27f, 46f });

                //// Llenamos la tabla con información
                PdfPCell clCol1 = new PdfPCell(new Phrase(Convert.ToString(row["APELLIDOYNOMBRE"].ToString()), _standardFontBold));
                clCol1.BorderWidth = 0;

                PdfPCell clCol2 = new PdfPCell(new Phrase(Convert.ToString(row["APELLIDOYNOMBRE"].ToString()), _standardFontBold));
                clCol2.BorderWidth = 0;

                PdfPCell clCol3 = new PdfPCell(new Phrase(Convert.ToString(row["APELLIDOYNOMBRE"].ToString()), _standardFontBold));
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
                clCol1 = new PdfPCell(new Phrase(Convert.ToString(row["CONCEPTO"].ToString() + " - " + Convert.ToString(row["MesCuota"].ToString())), _standardFont));
                clCol1.BorderWidth = 0;

                clCol2 = new PdfPCell(new Phrase(Convert.ToString(row["CONCEPTO"].ToString()) + " - " + Convert.ToString(row["MesCuota"].ToString()), _standardFont));
                clCol2.BorderWidth = 0;

                clCol3 = new PdfPCell(new Phrase(Convert.ToString(row["CONCEPTO"].ToString()) + " - " + Convert.ToString(row["MesCuota"].ToString()), _standardFont));
                clCol3.BorderWidth = 0;

                tblPrueba.AddCell(clCol1);
                tblPrueba.AddCell(clCol2);
                tblPrueba.AddCell(clCol3);

                //lblCodigoBarras.Text = Convert.ToString(row["BARRA"].ToString());
                //tblPrueba.AddCell(lblCodigoBarras.Text);
                //clCol3.BorderWidth = 0;
                //tblPrueba.AddCell(clCol3);

                if (Convert.ToString(row["BECA"].ToString()) == "0 %")
                {
                    clCol1 = new PdfPCell(new Phrase(" ", _standardFont));
                    clCol1.Border = 0;
                    clCol2 = new PdfPCell(new Phrase(" ", _standardFont));
                    clCol2.Border = 0;
                    clCol3 = new PdfPCell(new Phrase(" ", _standardFont));
                    clCol3.Border = 0;
                }
                else
                {
                    clCol1 = new PdfPCell(new Phrase(Convert.ToString(row["BECA"].ToString()), _standardFont));
                    clCol1.Border = 0;
                    clCol2 = new PdfPCell(new Phrase(Convert.ToString(row["BECA"].ToString()), _standardFont));
                    clCol2.Border = 0;
                    clCol3 = new PdfPCell(new Phrase(Convert.ToString(row["BECA"].ToString()), _standardFont));
                    clCol3.Border = 0;
                }

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

                doc.Add(tblPrueba);


                PriVto = Convert.ToString(row["PRIVTO"].ToString());
                SegVto = Convert.ToString(row["SEGVTO"].ToString());
                TerVto = Convert.ToString(row["TERVTO"].ToString());
                PriImporte = Convert.ToString(row["PRIIMPORTE"].ToString());
                SegImporte = Convert.ToString(row["SEGIMPORTE"].ToString());
                TerImporte = Convert.ToString(row["IMPABIERTO"].ToString());

                PdfPTable tmpTableDet = new PdfPTable(6) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 100 };
                //tmpTableDet.SetWidths(new float[] { 18f, 8f, 18f, 8f, 30f, 18f });
                tmpTableDet.SetWidths(new float[] { 18f, 9f, 18f, 9f, 30f, 16f });

                PdfPCell clColDet1 = new PdfPCell();
                PdfPCell clColDet2 = new PdfPCell();
                PdfPCell clColDet3 = new PdfPCell();
                PdfPCell clColDet4 = new PdfPCell();
                PdfPCell clColDet5 = new PdfPCell();
                PdfPCell clColDet6 = new PdfPCell();
                int m = 1;
                decimal Total = 0;

                DataTable dt1 = new DataTable();
                dt1 = ocnChequeraImpimir.InformeChequeraImprimirDetalle(Convert.ToInt32(row["CONCEPTOID"].ToString()));  //Convert.ToString
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow rowD in dt1.Rows)// por cada fila..
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

                    doc.Add(tmpTableDet);
                }


                PdfPTable tmpTableVtos = new PdfPTable(9) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 100 };
                //tmpTableVtos.SetWidths(new float[] { 8f, 10f, 8f, 8f, 10f, 8f, 10f, 20f, 18f });
                tmpTableVtos.SetWidths(new float[] { 8f, 10f, 9f, 8f, 10f, 9f, 10f, 18f, 18f });

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
                //tmpTableCB.SetWidths(new float[] { 26f, 26f, 12f, 36f });
                tmpTableCB.SetWidths(new float[] { 27f, 27f, 12f, 34f });

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
                    //doc.NewPage();
                    c = 1;
                }
                else
                {
                    c = c + 1;
                }

            }


            doc.Close();
            writer.Close();
            btnImprimir.Visible = true;


        }

    }








    private void ImprimirChequera1()
    {
        DataTable dt = new DataTable();
        dt = ocnChequeraImpimir.InformeChequeraImprimir2();
        int i = 1;
        int j = 1;
        String PriVto = "";
        String SegVto = "";
        String TerVto = "";
        String PriImporte = "";
        String SegImporte = "";
        String TerImporte = "";
        // Creamos el tipo de Font que vamos utilizar
        iTextSharp.text.Font _standardFontBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font _standardFontLBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font _standardFontL = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        // Creamos el documento con el tamaño de página tradicional
        Document doc = new Document(PageSize.A4, 15f, 10f, 10f, 10f);

        // Indicamos donde vamos a guardar el documento

        string NomRep;
        NomRep = "~/PaginasGenerales/Chequeras/chequera.pdf";
        string Ruta = MapPath(NomRep);  //System.Web.UI.Page.Server.MapPath(NomRep);
        NomRep = Ruta;

        //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\Aplicativos\GestionEscolarFra\chequera.pdf", FileMode.Create));
        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(NomRep, FileMode.Create));

        if (dt.Rows.Count > 0)
        {
            //Document doc = new Document(PageSize.LETTER);

            // Abrimos el archivo
            doc.Open();

            Int32 c = 1;
            foreach (DataRow row in dt.Rows)// por cada fila..
            {
                if (c == 1)
                {
                    doc.SetMargins(15f, 10f, 10f, 0f);
                } else
                {
                    doc.SetMargins(15f, 10f, 0f, 0f);
                }
                

                PdfPTable tblPrueba = new PdfPTable(3) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 100 };
                //tblPrueba.SetWidths(new float[] { 26f, 26f, 48f });
                tblPrueba.SetWidths(new float[] { 27f, 27f, 46f });

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
                clCol1 = new PdfPCell(new Phrase(Convert.ToString(row["CONCEPTO"].ToString() + " - " + Convert.ToString(row["MesCuota"].ToString())), _standardFont));
                clCol1.BorderWidth = 0;

                clCol2 = new PdfPCell(new Phrase(Convert.ToString(row["CONCEPTO"].ToString()) + " - " + Convert.ToString(row["MesCuota"].ToString()), _standardFont));
                clCol2.BorderWidth = 0;

                clCol3 = new PdfPCell(new Phrase(Convert.ToString(row["CONCEPTO"].ToString()) + " - " + Convert.ToString(row["MesCuota"].ToString()), _standardFont));
                clCol3.BorderWidth = 0;

                tblPrueba.AddCell(clCol1);
                tblPrueba.AddCell(clCol2);
                tblPrueba.AddCell(clCol3);

                //lblCodigoBarras.Text = Convert.ToString(row["BARRA"].ToString());
                //tblPrueba.AddCell(lblCodigoBarras.Text);
                //clCol3.BorderWidth = 0;
                //tblPrueba.AddCell(clCol3);

                if(Convert.ToString(row["BECA"].ToString()) == "0 %")
                {
                    clCol1 = new PdfPCell(new Phrase(" ", _standardFont));
                    clCol1.Border = 0;
                    clCol2 = new PdfPCell(new Phrase(" ", _standardFont));
                    clCol2.Border = 0;
                    clCol3 = new PdfPCell(new Phrase(" ", _standardFont));
                    clCol3.Border = 0;
                } else
                {
                    clCol1 = new PdfPCell(new Phrase(Convert.ToString(row["BECA"].ToString()), _standardFont));
                    clCol1.Border = 0;
                    clCol2 = new PdfPCell(new Phrase(Convert.ToString(row["BECA"].ToString()), _standardFont));
                    clCol2.Border = 0;
                    clCol3 = new PdfPCell(new Phrase(Convert.ToString(row["BECA"].ToString()), _standardFont));
                    clCol3.Border = 0;
                }

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
                //tmpTableDet.SetWidths(new float[] { 18f, 8f, 18f, 8f, 30f, 18f });
                tmpTableDet.SetWidths(new float[] { 18f, 9f, 18f, 9f, 30f, 16f });

                PdfPCell clColDet1 = new PdfPCell();
                PdfPCell clColDet2 = new PdfPCell();
                PdfPCell clColDet3 = new PdfPCell();
                PdfPCell clColDet4 = new PdfPCell();
                PdfPCell clColDet5 = new PdfPCell();
                PdfPCell clColDet6 = new PdfPCell();
                int m = 1;
                decimal Total = 0;

                DataTable dt1 = new DataTable();
                dt1 = ocnChequeraImpimir.InformeChequeraImprimirDetalle(Convert.ToInt32(row["CONCEPTOID"].ToString()));  //Convert.ToString
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow rowD in dt1.Rows)// por cada fila..
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

                    //clColDet1 = new PdfPCell(new Phrase(" ", _standardFontL));
                    //clColDet1.Border = 0;
                    //clColDet2 = new PdfPCell(new Phrase(" ", _standardFontL));
                    //clColDet2.Border = 0;
                    //clColDet3 = new PdfPCell(new Phrase(" ", _standardFontL));
                    //clColDet3.Border = 0;
                    //clColDet4 = new PdfPCell(new Phrase(" ", _standardFontL));
                    //clColDet4.Border = 0;
                    //clColDet5 = new PdfPCell(new Phrase(" ", _standardFontL));
                    //clColDet5.Border = 0;
                    //clColDet6 = new PdfPCell(new Phrase(" ", _standardFontL));
                    //clColDet6.Border = 0;

                    //tmpTableDet.AddCell(clColDet1);
                    //tmpTableDet.AddCell(clColDet2);
                    //tmpTableDet.AddCell(clColDet3);
                    //tmpTableDet.AddCell(clColDet4);
                    //tmpTableDet.AddCell(clColDet5);
                    //tmpTableDet.AddCell(clColDet6);

                    doc.Add(tmpTableDet);
                }


                PdfPTable tmpTableVtos = new PdfPTable(9) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 100 };
                //tmpTableVtos.SetWidths(new float[] { 8f, 10f, 8f, 8f, 10f, 8f, 10f, 20f, 18f });
                tmpTableVtos.SetWidths(new float[] { 8f, 10f, 9f, 8f, 10f, 9f, 10f, 18f, 18f });

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


                //QRCode qrcode = new QRCode();

                // http://java-white-box.blogspot.com/2014/09/itext-codigos-de-barras-clases-de-itext.html


                //  https://www.youtube.com/watch?v=lGlRe4cSTeg

                // https://www.codeproject.com/Articles/1250071/QR-Code-Encoder-and-Decoder-Csharp-Class-Library-f

                // Por defecto es A4
                Document documento = new Document();

                // Obtenemos una instancia de un objeto PDFWriter
                // PdfWriter.getInstance(documento, new FileOutputStream("EjemploCodigoDeBarras04.pdf"));
                //PdfWriter.GetInstance(documento, new FileStream("EjemploCodigoDeBarras04.pdf", FileAccess));

                //// Abrimos el documento
                //documento.Open();

                //BarcodeQRCode codigoBarrasQR = new BarcodeQRCode("https://madremercedesguerra.com.ar/", 0, 0, null);

                //documento.Add(codigoBarrasQR.GetImage());


                // PdfPTable tmpTable = new PdfPTable(1); // Comentado 20/01/2022

                // PdfPCell tmpCell = new PdfPCell(barCode); // Comentado 20/01/2022

                PdfPTable tmpTableCB = new PdfPTable(4) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 100 };
                //tmpTableCB.SetWidths(new float[] { 26f, 26f, 12f, 36f });
                tmpTableCB.SetWidths(new float[] { 27f, 27f, 12f, 34f });

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
                    //doc.NewPage();
                    c = 1;
                }
                else
                {
                    c = c + 1;
                }

            }


            doc.Close();
            writer.Close();
            btnImprimir.Visible = true;


        }



    }


    private void ImprimirChequeraOld()
    {
        DataTable dt = new DataTable();
        dt = ocnChequeraImpimir.InformeChequeraImprimir2();
        int i = 1;
        int j = 1;
        String PriVto = "";
        String SegVto = "";
        String TerVto = "";
        String PriImporte = "";
        String SegImporte = "";
        String TerImporte = "";
        // Creamos el tipo de Font que vamos utilizar
        iTextSharp.text.Font _standardFontBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font _standardFontLBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font _standardFontL = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        // Creamos el documento con el tamaño de página tradicional
        Document doc = new Document(PageSize.A4, 15f, 10f, 10f, 10f);

        // Indicamos donde vamos a guardar el documento

        string NomRep;
        NomRep = "~/PaginasGenerales/Chequeras/chequera.pdf";
        string Ruta = MapPath(NomRep);  //System.Web.UI.Page.Server.MapPath(NomRep);
        NomRep = Ruta;

        //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\Aplicativos\GestionEscolarFra\chequera.pdf", FileMode.Create));
        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(NomRep, FileMode.Create));

        if (dt.Rows.Count > 0)
        {
            //Document doc = new Document(PageSize.LETTER);

            //doc.SetMargins(1f, 1f, 1f, 1f);


            // Abrimos el archivo
            doc.Open();

            Int32 c = 1;
            foreach (DataRow row in dt.Rows)// por cada fila..
            {
                PdfPTable tblPrueba = new PdfPTable(3) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 100 };
                //tblPrueba.SetWidths(new float[] { 26f, 26f, 48f });
                tblPrueba.SetWidths(new float[] { 27f, 27f, 46f });

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
                //tmpTableDet.SetWidths(new float[] { 18f, 8f, 18f, 8f, 30f, 18f });
                tmpTableDet.SetWidths(new float[] { 18f, 9f, 18f, 9f, 30f, 16f });

                PdfPCell clColDet1 = new PdfPCell();
                PdfPCell clColDet2 = new PdfPCell();
                PdfPCell clColDet3 = new PdfPCell();
                PdfPCell clColDet4 = new PdfPCell();
                PdfPCell clColDet5 = new PdfPCell();
                PdfPCell clColDet6 = new PdfPCell();
                int m = 1;
                decimal Total = 0;

                DataTable dt1 = new DataTable();
                dt1 = ocnChequeraImpimir.InformeChequeraImprimirDetalle(Convert.ToInt32(row["CONCEPTOID"].ToString()));  //Convert.ToString
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow rowD in dt1.Rows)// por cada fila..
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

                    //clColDet1 = new PdfPCell(new Phrase(" ", _standardFontL));
                    //clColDet1.Border = 0;
                    //clColDet2 = new PdfPCell(new Phrase(" ", _standardFontL));
                    //clColDet2.Border = 0;
                    //clColDet3 = new PdfPCell(new Phrase(" ", _standardFontL));
                    //clColDet3.Border = 0;
                    //clColDet4 = new PdfPCell(new Phrase(" ", _standardFontL));
                    //clColDet4.Border = 0;
                    //clColDet5 = new PdfPCell(new Phrase(" ", _standardFontL));
                    //clColDet5.Border = 0;
                    //clColDet6 = new PdfPCell(new Phrase(" ", _standardFontL));
                    //clColDet6.Border = 0;

                    //tmpTableDet.AddCell(clColDet1);
                    //tmpTableDet.AddCell(clColDet2);
                    //tmpTableDet.AddCell(clColDet3);
                    //tmpTableDet.AddCell(clColDet4);
                    //tmpTableDet.AddCell(clColDet5);
                    //tmpTableDet.AddCell(clColDet6);

                    doc.Add(tmpTableDet);
                }


                PdfPTable tmpTableVtos = new PdfPTable(9) { HorizontalAlignment = Element.ALIGN_LEFT, WidthPercentage = 100 };
                //tmpTableVtos.SetWidths(new float[] { 8f, 10f, 8f, 8f, 10f, 8f, 10f, 20f, 18f });
                tmpTableVtos.SetWidths(new float[] { 8f, 10f, 9f, 8f, 10f, 9f, 10f, 18f, 18f });

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
                //tmpTableCB.SetWidths(new float[] { 26f, 26f, 12f, 36f });
                tmpTableCB.SetWidths(new float[] { 27f, 27f, 12f, 34f });

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
                    //doc.NewPage();
                    c = 1;
                }
                else
                {
                    c = c + 1;
                }

            }


            doc.Close();
            writer.Close();
            btnImprimir.Visible = true;


        }



    }


    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        string NomRep;
        NomRep = "~/PaginasGenerales/Chequeras/chequera.pdf";
        string Ruta = MapPath(NomRep);  //System.Web.UI.Page.Server.MapPath(NomRep);
        NomRep = Ruta;

        Response.Clear();

        Response.ContentType = "application/pdf";

        // la variable nombre es el nombre del archivo .pdf

        //Response.AddHeader("Content-disposition", "attachment; filename=" & "nombre");



        // aqui va la ruta del archivo
        //Response.WriteFile(NomRep);

        Response.Redirect("../PaginasGenerales/Chequeras/chequera.pdf", false);

        //Response.Flush();
        //Response.Close();


        //string _open = "window.open('http://madremercedesguerra.com.ar/public_html/PaginasGenerales/Chequeras/chequera.pdf', '_newtab');";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);


        // Creamos el documento con el tamaño de página tradicional
        //Document doc = new Document(PageSize.A4, 1f, 1f, 1f, 1f);

        //string NomRep;
        //NomRep = "~/PaginasGenerales/Chequeras/chequera.pdf";
        //string Ruta = MapPath(NomRep);  //System.Web.UI.Page.Server.MapPath(NomRep);
        //NomRep = Ruta;

        //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\Aplicativos\GestionEscolarFra\chequera.pdf", FileMode.Create));
        //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(NomRep, FileMode.Open));

        //PdfReader.Get GetInstance(doc, new FileStream(NomRep, FileMode.Open));

        //doc.Open();
        //doc.Close();


        //string NomRep = "";
        //NomRep = @"~\PaginasGenerales\Chequeras\";

        //int esperar = 0;

        //Process p = new Process();
        ////p.StartInfo.FileName = @"C:\Aplicativos\GestionEscolarFra\chequera.pdf";
        //p.StartInfo.FileName = NomRep;
        //p.StartInfo.Verb = "Print";

        //p.Start();
        ////libreria Threading
        //System.Threading.Thread.Sleep(5000);
        ////p.Close();
        ////p.CloseMainWindow();
        //while (!p.HasExited)
        //{
        //    System.Threading.Thread.Sleep(3000);
        //    esperar++;

        //    //p.CloseMainWindow();
        //}
        //p.Close();
    }



}
