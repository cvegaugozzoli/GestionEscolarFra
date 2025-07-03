using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class InformesCurso : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    DataTable dt2 = new DataTable();
    GESTIONESCOLAR.Negocio.Curso ocnCurso = new GESTIONESCOLAR.Negocio.Curso();
    int Id = 0;
    int cur;
    int AnioCur;
    GESTIONESCOLAR.Negocio.InscripcionCursado ocnInscripcionCursado = new GESTIONESCOLAR.Negocio.InscripcionCursado();
    GESTIONESCOLAR.Negocio.EspacioCurricular ocnEspacioCurricular = new GESTIONESCOLAR.Negocio.EspacioCurricular();
    GESTIONESCOLAR.Negocio.PlanEstudio ocnPlanEstudio = new GESTIONESCOLAR.Negocio.PlanEstudio();
    GESTIONESCOLAR.Negocio.Campo ocnCampo = new GESTIONESCOLAR.Negocio.Campo();
    GESTIONESCOLAR.Negocio.Alumno ocnAlumno = new GESTIONESCOLAR.Negocio.Alumno();
    Int32 insId;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                insId = Convert.ToInt32(Session["_Institucion"]);
                Id = Convert.ToInt32(Request.QueryString["Id"]);
                this.Master.TituloDelFormulario = " Cursado - Consulta";
                carId.DataValueField = "Valor"; carId.DataTextField = "Texto"; carId.DataSource = (new GESTIONESCOLAR.Negocio.Carrera()).ObtenerLista("[Seleccionar...]"); carId.DataBind();
                #region PageIndex
                int PageIndex = 0;
                if (this.Session["CursoConsulta.PageIndex"] == null)
                {
                    Session.Add("CursoConsulta.PageIndex", 0);
                }
                else
                {
                    PageIndex = Convert.ToInt32(Session["CursoConsulta.PageIndex"]);
                }
                #endregion

                #region Variables de sesion para filtros
                //if (Session["CursoConsulta.Nombre"] != null) { Curso.Text = Session["CursoConsulta.Nombre"].ToString(); } else { Session.Add("CursoConsulta.Nombre", Nombre.Text.Trim()); }
                #endregion

                GrillaCargar(PageIndex);
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

    protected void BtnImprimir_Click(object sender, EventArgs e)
    {
        try
        {
            lblMensajeError3.Text = "";
            if (TipoInformeId.SelectedValue == "2") //Si tipo Informe = Informe
            {

                if (PeriodoId.SelectedValue == "4")// Si periodo es Todos
                {
                    int ban = 0;
                    foreach (GridViewRow row in Grilla.Rows)
                    {
                        int tipoCarrera = Convert.ToInt32(Grilla.DataKeys[row.RowIndex].Values["TipoCarrera"]);
                        //bandera para ver si al menos uno esta check
                        if (tipoCarrera == 2) // es primaria?
                        {
                            CheckBox check = row.FindControl("chkSeleccion") as CheckBox;
                            //Int32 EstIC = Convert.ToInt32(GrillaAlumnos.DataKeys[row.RowIndex].Values["Estado"]);
                            if ((check.Checked)) // & (EstIC == 1)
                            {
                                int curid = Convert.ToInt32(curId.SelectedValue.ToString());
                                int AnioCur = Convert.ToInt32(AnioCursado.Text);
                                int aluId = Convert.ToInt32(Grilla.DataKeys[row.RowIndex].Values["aluId"]);
                                ban = 1;
                                String NomRep = "LibretaPrimaria.rpt";

                                FuncionesUtiles.AbreVentana("Reporte.aspx?curso=" + curid + "&anio=" + AnioCur + "&aluId=" + aluId + "&NomRep=" + NomRep);
                            }
                        }

                        else
                        {

                            if (tipoCarrera == 3)//es Secundaria?
                            {
                                CheckBox check = row.FindControl("chkSeleccion") as CheckBox;
                                //Int32 EstIC = Convert.ToInt32(GrillaAlumnos.DataKeys[row.RowIndex].Values["Estado"]);
                                if ((check.Checked)) // & (EstIC == 1)
                                {
                                    int curid = Convert.ToInt32(curId.SelectedValue.ToString());
                                    int AnioCur = Convert.ToInt32(AnioCursado.Text);
                                    int aluId = Convert.ToInt32(Grilla.DataKeys[row.RowIndex].Values["aluId"]);
                                    ban = 1;
                                    String NomRep = "LibretaSecundaria.rpt";

                                    FuncionesUtiles.AbreVentana("Reporte.aspx?aluId=" + aluId + "&curso=" + curid + "&anio=" + AnioCur + "&NomRep=" + NomRep);
                                }
                            }
                        }

                    }
                    if (ban == 0) // Selecciono un alumno en la tabla?
                    {
                        lblMensajeError3.Text = "Debe seleccionar al menos un Alumno..";
                    }

                }

                if (PeriodoId.SelectedValue == "1")// Si periodo es 1 Informe
                {

                    foreach (GridViewRow row in Grilla.Rows)
                    {
                        int tipoCarrera = Convert.ToInt32(Grilla.DataKeys[row.RowIndex].Values["TipoCarrera"]);
                        int ban = 0; //bandera para ver si al menos uno esta check
                        if (tipoCarrera == 2) // es primaria?
                        {
                            CheckBox check = row.FindControl("chkSeleccion") as CheckBox;
                            //Int32 EstIC = Convert.ToInt32(GrillaAlumnos.DataKeys[row.RowIndex].Values["Estado"]);
                            if ((check.Checked)) // & (EstIC == 1)
                            {
                                int curid = Convert.ToInt32(curId.SelectedValue.ToString());
                                int AnioCur = Convert.ToInt32(AnioCursado.Text);
                                int aluId = Convert.ToInt32(Grilla.DataKeys[row.RowIndex].Values["aluId"]);
                                ban = 1;
                                String NomRep = "InformePrimariaPT.rpt";

                                FuncionesUtiles.AbreVentana("Reporte.aspx?curId=" + curid + "&AnioCur=" + AnioCur + "&aluId=" + aluId + "&NomRep=" + NomRep);
                            }
                        }

                        else
                        {

                            if (tipoCarrera == 3)//es Secundaria?
                            {
                                CheckBox check = row.FindControl("chkSeleccion") as CheckBox;
                                //Int32 EstIC = Convert.ToInt32(GrillaAlumnos.DataKeys[row.RowIndex].Values["Estado"]);
                                if ((check.Checked)) // & (EstIC == 1)
                                {
                                    int curid = Convert.ToInt32(curId.SelectedValue.ToString());
                                    int AnioCur = Convert.ToInt32(AnioCursado.Text);
                                    int aluId = Convert.ToInt32(Grilla.DataKeys[row.RowIndex].Values["aluId"]);
                                    ban = 1;
                                    String NomRep = "InformeSecundaria1T.rpt";

                                    FuncionesUtiles.AbreVentana("Reporte.aspx?curId=" + curid + "&AnioCur=" + AnioCur + "&aluId=" + aluId + "&NomRep=" + NomRep);
                                }
                            }
                        }
                        if (ban == 0) // Selecciono un alumno en la tabla?
                        {
                            lblMensajeError3.Text = "Debe seleccionar al menos un Alumno..";
                        }
                    }
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

    protected void btnExportarAExcel_Click(object sender, EventArgs e)
    {
        dt = new DataTable();
        dt = ocnCurso.ObtenerListadoxCurso(Id, Convert.ToString(AnioCur));
        string ArchivoNombre = "CursoListadoAlumnos" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + ".xls";
        FuncionesUtiles.ExportarAExcel(dt, ArchivoNombre, this);
    }

    private void GrillaCargar(int PageIndex)
    {
        try
        {
            Session["CursoListadoAlumnos.PageIndex"] = PageIndex;

            #region Variables de sesion para filtros

            #endregion

            Int32 car = 0;
            if (carId.SelectedValue.ToString() != "" & carId.SelectedValue.ToString() != "0")
            {
                car = Convert.ToInt32(carId.SelectedValue.ToString());
            }
            Int32 pla = 0;
            if (plaId.SelectedValue.ToString() != "" & plaId.SelectedValue.ToString() != "0")
            {
                pla = Convert.ToInt32(plaId.SelectedValue.ToString());
            }

            if (carId.SelectedValue.ToString() != "" & carId.SelectedValue.ToString() != "0")
            {
                cur = Convert.ToInt32(curId.SelectedValue.ToString());
            }
            else
            {
                Session.Add("CursoListadoAlumnos.curId", cur);
            }


            if (AnioCursado.Text == "")
            {

                int Anio = Convert.ToInt32(Request.QueryString["Anio"]);
                DateTime fechaActual = DateTime.Today;
                AnioCur = Anio;

            }
            else
            {
                AnioCur = Convert.ToInt32(AnioCursado.Text);
            }

            dt = new DataTable();
            dt = ocnInscripcionCursado.ObtenerporCarporPlaporCurAnio(insId, car, pla, cur, AnioCur);

            this.Grilla.DataSource = dt;
            this.Grilla.PageIndex = PageIndex;
            this.Grilla.DataBind();

            if (dt.Rows.Count > 0)
            {
                lblCantidadRegistros.Text = "Cantidad de registros: " + dt.Rows.Count.ToString();
                lblCurso.Text = dt.Rows[0]["Curso"].ToString();
                lblTC.Text = dt.Rows[0]["TipoCarrera"].ToString();
                PeriodoId.Visible = true;
                lblPeriodo.Visible = true;
                lblTipoInforme.Visible = true;
                TipoInformeId.Visible = true;

                lblCantReg.Visible = true;
                lblCantidadRegistros.Visible = true;
                //btnSeleccionar.Visible = true;
                //MovimientoId.Visible = true;
                //lblmovimiento.Visible = true;
                BtnImprimir.Visible = true;
                btnSeleccionarTodo.Visible = true;

            }
            else
            {
                lblCantidadRegistros.Text = "Cantidad de registros: 0";
                //btnSeleccionar.Visible = false;
                PeriodoId.Visible = false;
                lblPeriodo.Visible = false;
                lblCantReg.Visible = false;
                lblCantidadRegistros.Visible = false;
                lblTipoInforme.Visible = false;
                TipoInformeId.Visible = false;
                BtnImprimir.Visible = false;
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

    protected void TipoInformeId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //GridViewRow row = Grilla.SelectedRow;
            //            var ddl = (DropDownList)sender;

            //string itemSelected = ddl.SelectedValue;

            if (Convert.ToInt32(TipoInformeId.SelectedValue) == 1)
            {
                PeriodoId.Enabled = false;
                BtnImprimir.Enabled = true;
            }
            else
            {
                String TC = lblTC.Text;
                if (Convert.ToInt32(TipoInformeId.SelectedValue) == 2)
                {
                    PeriodoId.Enabled = true;
                    BtnImprimir.Enabled = true;

                    if (TC == "3")
                    {
                        PeriodoId.Items.FindByValue("3").Enabled = false;
                        //.Items.FindByValue("3° Informe").Attributes.Add("Disabled", "Disabled");
                    }
                    if (TC == "2")
                    {
                        PeriodoId.Items.FindByValue("3").Enabled = true;
                    }
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

    protected void Grilla_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //if (e.CommandName != "Sort" && e.CommandName != "Page" && e.CommandName != "")
            //{
            //    string IC = ((HyperLink)Grilla.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Controls[1]).Text;
            //    if (AnioCursado.Text == "")
            //    {
            //        DateTime fechaActual = DateTime.Today;
            //        AnioCur = Convert.ToInt32(fechaActual.Year.ToString());

            //    }
            //    else
            //    {
            //        AnioCur = Convert.ToInt32(AnioCursado.Text);
            //    }

            //    if (e.CommandName == "Eliminar")
            //    {
            //        //ocnCurso.Eliminar(Convert.ToInt32(Id));
            //        this.GrillaCargar(this.Grilla.PageIndex);
            //    }

            //    if (e.CommandName == "Copiar")
            //    {
            //        ocnCurso = new GESTIONESCOLAR.Negocio.Curso(Convert.ToInt32(IC));
            //        //ocnCurso.Copiar();
            //        this.GrillaCargar(this.Grilla.PageIndex);
            //    }

            //    if (e.CommandName == "Ver")
            //    {
            //        String TC = ((HyperLink)Grilla.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Controls[1]).Text;
            //        if (TC == "4")
            //        {
            //            Response.Redirect("RegistracionCalificacionesRegistracion.aspx?Id=" + IC + "&Ver=1", false);
            //        }
            //        else
            //        {
            //            if (TC == "3")
            //            {
            //                Response.Redirect("CargaCalificacionesPorAlumnoSec.aspx?Id=" + IC + "&Anio=" + AnioCur + "&Ver=1", false);
            //            }
            //            else
            //            {
            //                if (TC == "2")
            //                {
            //                    Response.Redirect("CargaCalificacionesPorAlumnoPri.aspx?Id=" + IC + "&Anio=" + AnioCur + "&Ver=1", false);
            //                }
            //            }
            //        }
            //    }
            //}
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
            if (Session["CursoConsulta.PageIndex"] != null)
            {
                Session["CursoConsulta.PageIndex"] = e.NewPageIndex;
            }
            else
            {
                Session.Add("CursoConsulta.PageIndex", e.NewPageIndex);
            }

            this.GrillaCargar(e.NewPageIndex);
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

    protected void lbuEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            ((LinkButton)sender).Visible = false;
            ((LinkButton)sender).Parent.Controls[3].Visible = true;
            ((LinkButton)sender).Parent.Controls[5].Visible = true;
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

    protected void btnEliminarAceptar_Click(object sender, EventArgs e)
    {
        try
        {
            int Id = 0;
            Id = Convert.ToInt32(((HyperLink)((GridViewRow)((Button)sender).Parent.Parent).Cells[0].Controls[1]).Text);

            ocnCurso.Eliminar(Id);

            ((Button)sender).Parent.Controls[1].Visible = true;
            ((Button)sender).Parent.Controls[3].Visible = false;
            ((Button)sender).Parent.Controls[5].Visible = false;

            GrillaCargar(Grilla.PageIndex);
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

    protected void btnEliminarCancelar_Click(object sender, EventArgs e)
    {
        ((Button)sender).Parent.Controls[1].Visible = true;
        ((Button)sender).Parent.Controls[3].Visible = false;
        ((Button)sender).Parent.Controls[5].Visible = false;
    }

    protected void Nombre_TextChanged(object sender, EventArgs e)
    {
        GrillaCargar(Grilla.PageIndex);
    }

    protected void btnAplicar_Click(object sender, EventArgs e)
    {
        UpdatePanel1.Visible = false;
        btnAplicar.Visible = false;
        btnNuevo.Visible = true;
        lblMensajeError3.Text = "";
        GrillaCargar(Grilla.PageIndex);

    }
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        UpdatePanel1.Visible = true;
        btnAplicar.Visible = true;
        plaId.SelectedValue = "0";
        carId.SelectedValue = "0";
        curId.SelectedValue = "0";
        AnioCursado.Text = "";
        btnNuevo.Visible = false;
        lblCurso.Text = "";
        lblMensajeError3.Text = "";
        TipoInformeId.SelectedValue = "0";
        PeriodoId.SelectedValue = "0";
        PeriodoId.Enabled = false;
        BtnImprimir.Enabled = false;
        GrillaCargar(Grilla.PageIndex);
    }

    protected void carId_SelectedIndexChanged(object sender, EventArgs e)
    {
        carIdCargar();
    }

    private void carIdCargar()
    {
        if (carId.SelectedIndex > 0)
        {

            //ClubB.Negocio.Evento ocnEvento = new ClubB.Negocio.Evento();
            DataTable dt = new DataTable();
            dt = ocnPlanEstudio.ObtenerListaPorUnaCarrera("[Seleccionar...]", Convert.ToInt32(carId.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                plaId.DataValueField = "Valor";
                plaId.DataTextField = "Texto";
                plaId.DataSource = (new GESTIONESCOLAR.Negocio.PlanEstudio()).ObtenerListaPorUnaCarrera("[Seleccionar...]", Convert.ToInt32(carId.SelectedValue));
                plaId.DataBind();
            }
        }

    }


    protected void plaId_SelectedIndexChanged(object sender, EventArgs e)
    {
        plaIdCargar();
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




    protected void PeriodoId_SelectedIndexChanged(object sender, EventArgs e)
    {
        BtnImprimir.Enabled = true;
    }
}