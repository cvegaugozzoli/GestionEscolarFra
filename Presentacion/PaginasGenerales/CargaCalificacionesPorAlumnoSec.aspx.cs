using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

using System.Configuration;
using System.Data.SqlClient;

public partial class CargaCalificacionesPorAlumnoSec : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DataTable dt2 = new DataTable();
    DataTable dt3 = new DataTable();
    DataTable dt4 = new DataTable();
    DataTable dt5 = new DataTable();
    int AnioCur;
    GESTIONESCOLAR.Negocio.Curso ocnCurso = new GESTIONESCOLAR.Negocio.Curso();
    GESTIONESCOLAR.Negocio.InscripcionCursado ocnInscripcionCursado = new GESTIONESCOLAR.Negocio.InscripcionCursado();
    GESTIONESCOLAR.Negocio.RegistracionNotaPrevia ocnRegistracionNotaPrevia = new GESTIONESCOLAR.Negocio.RegistracionNotaPrevia();

    GESTIONESCOLAR.Negocio.RegistracionNota ocnRegistracionNota = new GESTIONESCOLAR.Negocio.RegistracionNota();

    int insId;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                this.Master.TituloDelFormulario = " Calificaciones";
                insId = Convert.ToInt32(Session["_Institucion"]);
                int IC = 0;
                if (Request.QueryString["Id"] != null)
                {
                    IC = Convert.ToInt32(Request.QueryString["Id"]);
                    ICtext.Text = Convert.ToString(IC);
                    DataTable dt = new DataTable();
                    dt = ocnInscripcionCursado.ObtenerUno(IC);
                    if (dt.Rows.Count > 0)
                    {
                        this.Master.TituloDelFormulario = dt.Rows[0]["Curso"].ToString() + " : " + "Calificaciones de " + dt.Rows[0]["Alumno"].ToString() +
                            " para el año " + dt.Rows[0]["AnoCursado"].ToString();
                        Session.Add("aluId", dt.Rows[0]["aluId"].ToString());
                    }
                }

                String perfil = Session["_perId"].ToString();
                if ((Session["_perId"].ToString() == "10") | (Session["_perId"].ToString() == "9")) // Si es familiar o Secretaria no muestro para modificar periodo
                {
                    LblPeriodo.Visible = false;
                    PeriodoId.Visible = false;
                    TextNotaAsignar.Visible = false;
                    lblNota.Visible = false;
                    ButtonAsignar.Visible = false;
                }

                #region PageIndex
                int PageIndex = 0;
                if (this.Session["CargaCalificacionesPorAlumnoSec.PageIndex"] == null)
                {
                    Session.Add("CargaCalificacionesPorAlumnoSec.PageIndex", 0);
                }
                else
                {
                    PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoSec.PageIndex"]);
                }
                #endregion

                #region Variables de sesion para filtros    

                GrillaCargar(PageIndex);

            }

            #endregion
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

    protected void OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if ((Session["_perId"].ToString() != "10") & (Session["_perId"].ToString() != "9") & (Session["_perId"].ToString() != "4")) // Si es distinto a familiar Secretaria o Prof hs puedo modificar
            {

                GridView1.EditIndex = e.NewEditIndex;
                int PageIndex = 0;
                PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoSec.PageIndex"]);
                GrillaCargar(PageIndex);
                GridView1.Rows[e.NewEditIndex].Attributes.Remove("ondblclick");
                GridView1.Columns[8].Visible = true;
                GridView1.Columns[9].Visible = true;
            }
            else
            {
                LblMensajeErrorGrilla.Text = "No puede modificar notas..";
                return;
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

    protected void OnRowEditing2(object sender, GridViewEditEventArgs e)
    {

        try
        {

            if ((Session["_perId"].ToString() != "10") & (Session["_perId"].ToString() != "9") & (Session["_perId"].ToString() != "4")) // Si es distinto a familiar o Scretaria o prof hs puedo modificar
            {
                GridView2.EditIndex = e.NewEditIndex;
                int PageIndex = 0;
                PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoSec.PageIndex"]);
                GrillaCargar(PageIndex);
                GridView2.Rows[e.NewEditIndex].Attributes.Remove("ondblclick");
                GridView2.Columns[9].Visible = true;
                GridView2.Columns[10].Visible = false;
            }
            else
            {
                LblMensajeErrorGrilla.Text = "No puede modificar notas..";
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
    protected void OnCancel(object sender, EventArgs e)
    {
        try
        {
            GridView1.EditIndex = -1;
            int PageIndex = 0;
            PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoSec.PageIndex"]);
            GrillaCargar(PageIndex);
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



    protected void OnCancel2(object sender, EventArgs e)
    {
        GridView2.EditIndex = -1;
        int PageIndex = 0;
        PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoSec.PageIndex"]);
        GrillaCargar(PageIndex);

    }

    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                row.Attributes["onmouseover"] = "this.style.background = '#CCCCFF'; this.style.cursor = 'pointer'";
                row.Attributes["onmouseout"] = "this.style.background='#ffffff'";
            }
        }

        foreach (GridViewRow row in GridView2.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                row.Attributes["onmouseover"] = "this.style.background = '#CCCCFF'; this.style.cursor = 'pointer'";
                row.Attributes["onmouseout"] = "this.style.background='#ffffff'";
            }
        }
        base.Render(writer);
    }

    protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        String perfil3 = Session["_perId"].ToString();
        if ((Session["_perId"].ToString() != "10") & (Session["_perId"].ToString() != "9")) // Si es distinto a familiar o Secretaria puedo modificar
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Edit$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
                e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor; this.style.backgroundColor='#F7F7DE';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;");
            }
        }
    }


    protected void OnRowDataBound2(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if ((Session["_perId"].ToString() != "10") & (Session["_perId"].ToString() != "9")) // Si es distinto a familiar puedo modificar
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView2, "Edit$" + e.Row.RowIndex);

                e.Row.Attributes["style"] = "cursor:pointer";
                //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView2, "Select$" + e.Row.RowIndex);

            }
        }
    }
    protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            if ((Session["_perId"].ToString() != "10") & (Session["_perId"].ToString() != "9")) // Si es distinto a familiar puedo modificar
            {
                int IdIC = 0;
                int aluId = Convert.ToInt32(Session["aluId"]);
                if (anioCur.Text == "")

                {

                    if (Request.QueryString["Anio"] == "")
                    {
                        DateTime fechaActual = DateTime.Today;
                        AnioCur = Convert.ToInt32(fechaActual.Year.ToString());

                    }
                    else
                    {
                        AnioCur = Convert.ToInt32(Request.QueryString["Anio"]);
                    }
                    IdIC = Convert.ToInt32(Request.QueryString["Id"]);
                }

                else
                {
                    AnioCur = Convert.ToInt32(anioCur.Text);

                    dt3 = ocnInscripcionCursado.ObtenerTodoxInsIdxaluIdxAnioCursado(insId, AnioCur, aluId);

                    IdIC = Convert.ToInt32(dt3.Rows[0]["Id"].ToString());

                }


                GridViewRow row = GridView1.Rows[e.RowIndex];
                int Id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

                TextBox PCuatr = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtPCuatr");
                TextBox SCuatr = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtSCuatr");
                TextBox PAnual = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtPAnual");
                TextBox NotaDic = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtDiciembre");
                TextBox NotaMar = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtMarzo");
                TextBox renCalfDef = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtrenCalfDef");
                String PCuatr2 = PCuatr.Text;
                String SCuatr2 = SCuatr.Text;
                String PAnual2 = PAnual.Text;
                String NotaDic2 = NotaDic.Text;
                String NotaMar2 = NotaMar.Text;
                String renCalfDef2 = renCalfDef.Text;
                Int32 renCalfDefInt = Convert.ToInt32((renCalfDef2 == "" ? "100" : renCalfDef2));
                DateTime RenFechaHoraCreacion = DateTime.Now;
                DateTime RenFechaHoraUltimaModificacion = DateTime.Now;
                Int32 usuIdCreacion = this.Master.usuId;
                Int32 usuIdUltimaModificacion = this.Master.usuId;


                ocnRegistracionNota.ActualizarSecundaria(Id, PCuatr2, SCuatr2, PAnual2, NotaDic2, NotaMar2, renCalfDef2, RenFechaHoraCreacion, RenFechaHoraUltimaModificacion, usuIdCreacion, usuIdUltimaModificacion);
                GridView1.EditIndex = -1;
                int PageIndex = 0;
                PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoSec.PageIndex"]);


                if ((renCalfDefInt < 6) | (renCalfDef2 == "A"))
                {
                    dt5 = ocnRegistracionNotaPrevia.ObtenerSoloPrevias(aluId);



                    if (dt5.Rows.Count == 3)
                    {
                        LblMensajeErrorGrilla.Text = "El alumno tiene tres previas..";
                        renCalfDef.Text = "";
                    }
                    else
                    {
                        if (dt5.Rows.Count < 3)
                        {

                            dt3 = ocnRegistracionNotaPrevia.ControlRepetida(IdIC, AnioCur, Id);
                            if (dt3.Rows.Count == 0)
                            {
                                ocnRegistracionNotaPrevia.Insertar(Id);
                            }
                        }
                    }

                    this.GridView2.DataSource = dt5;
                    this.GridView2.PageIndex = PageIndex;
                    this.GridView2.DataBind();

                }
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

    protected void OnRowUpdating2(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            if ((Session["_perId"].ToString() != "10") & (Session["_perId"].ToString() != "9")) // Si es distinto a familiar puedo modificar
            {
                GridViewRow row = GridView2.Rows[e.RowIndex];
                int Id = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Values[0]);
                DateTime? Fecha2;
                if (Convert.ToString(GridView2.DataKeys[e.RowIndex].Values[0]) != "")
                {
                    TextBox txtCalPrev = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtCalPrev");
                    TextBox txtFecha = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtFecha");

                    String txtCalPrev2 = Convert.ToString((txtCalPrev.Text == "" ? "" : txtCalPrev.Text));

                    if (txtFecha.Text == "")

                    {
                        LblMensajeErrorGrilla.Text = "Debe ingresar una fecha de examen";
                    }
                    else
                    {
                        Fecha2 = Convert.ToDateTime(txtFecha.Text);
                        ocnRegistracionNotaPrevia.Actualizar(Id, txtCalPrev2, Fecha2);
                        GridView2.EditIndex = -1;
                        int PageIndex = 0;
                        PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoSec.PageIndex"]);
                        GrillaCargar(PageIndex);
                    }
                }
            }
            else
            {
                LblMensajeErrorGrilla.Text = "No puede modificar notas..";
                int PageIndex = 0;
                PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoSec.PageIndex"]);
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



    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Sort" && e.CommandName != "Page" && e.CommandName != "")
            {
                //int anio = Convert.ToInt32(((HyperLink)GridView2.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Controls[0]).Text);
                //string Id = ((HyperLink)GridView2.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Controls[1]).Text;


                if (e.CommandName == "New")
                {
                    int RegNota = Convert.ToInt32(GridView2.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["RegNotaId"]);
                    //Inserto previa
                    //int RegNota = Convert.ToInt32(((HyperLink)GridView2.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Controls[0]).Text);
                    string notaS = Convert.ToString(GridView2.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["Calificacion"]);
                    //int notaN = Convert.ToInt32(GridView2.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["Calificacion"]);

                    if (notaS != "")
                    {
                        ocnRegistracionNotaPrevia.Insertar(RegNota);
                        int PageIndex = 0;
                        PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoSec.PageIndex"]);
                        GrillaCargar(PageIndex);

                    }
                    else
                    {
                        LblMensajeErrorGrilla.Text = "El Registro dene tener una calificación para rendirla nuevamente..";
                    }

                }
                if (e.CommandName == "Eliminar")
                {
                    int Id = Convert.ToInt32(GridView2.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["Id"]);
                    ocnRegistracionNotaPrevia.Eliminar(Id);
                    int PageIndex = 0;
                    PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoSec.PageIndex"]);
                    GrillaCargar(PageIndex);
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

    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //this.BindGrid();
    }
    protected void ButtonAsignar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["Anio"] == "")
            {
                DateTime fechaActual = DateTime.Today;
                AnioCur = Convert.ToInt32(fechaActual.Year.ToString());

            }
            else
            {
                AnioCur = Convert.ToInt32(Request.QueryString["Anio"]);
            }


            if (TextNotaAsignar.Text != "")
            {

                if (PeriodoId.SelectedIndex != 6)
                {
                    dt = ocnRegistracionNota.ObtenerTodoporInscripcionCursadoAnio(Convert.ToInt32(ICtext.Text), AnioCur);
                    if (PeriodoId.SelectedIndex == 0)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                if (Convert.ToInt32(row["FDictado"].ToString()) != 5)
                                {
                                    int RenId = Convert.ToInt32(row["Id"].ToString());
                                    ocnRegistracionNota.AsignarNotaSecPC(RenId, TextNotaAsignar.Text);
                                }
                            }
                        }
                    }
                    if (PeriodoId.SelectedIndex == 1)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                if (Convert.ToInt32(row["FDictado"].ToString()) != 5)
                                {
                                    int RenId = Convert.ToInt32(row["Id"].ToString());
                                    ocnRegistracionNota.AsignarNotaSecSC(RenId, TextNotaAsignar.Text);
                                }
                            }
                        }
                    }
                    if (PeriodoId.SelectedIndex == 2)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                if (Convert.ToInt32(row["FDictado"].ToString()) != 5)
                                {
                                    int RenId = Convert.ToInt32(row["Id"].ToString());
                                    ocnRegistracionNota.AsignarNotaPromA(RenId, TextNotaAsignar.Text);
                                }
                            }
                        }
                    }
                    if (PeriodoId.SelectedIndex == 3)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                if (Convert.ToInt32(row["FDictado"].ToString()) != 5)
                                {
                                    int RenId = Convert.ToInt32(row["Id"].ToString());
                                    ocnRegistracionNota.AsignarNotaDic(RenId, TextNotaAsignar.Text);
                                }
                            }
                        }
                    }

                    if (PeriodoId.SelectedIndex == 4)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                if (Convert.ToInt32(row["FDictado"].ToString()) != 5)
                                {
                                    int RenId = Convert.ToInt32(row["Id"].ToString());
                                    ocnRegistracionNota.AsignarNotaMar(RenId, TextNotaAsignar.Text);
                                }
                            }
                        }
                    }
                    if (PeriodoId.SelectedIndex == 5)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                if (Convert.ToInt32(row["FDictado"].ToString()) != 5)
                                {
                                    int RenId = Convert.ToInt32(row["Id"].ToString());
                                    ocnRegistracionNota.AsignarNotaCalDef(RenId, TextNotaAsignar.Text);
                                }
                            }
                        }
                    }
                    GridView1.EditIndex = -1;
                    int PageIndex = 0;
                    PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoSec.PageIndex"]);
                    GrillaCargar(PageIndex);
                }
                else
                {
                    LblMensajeErrorGrilla.Text = "Seleccione un Periodo..";
                    PeriodoId.Focus();
                    return;
                }
            }
            else
            {
                LblMensajeErrorGrilla.Text = "Asigne una nota..";
                TextNotaAsignar.Focus();
                return;
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
            Session["CargaCalificacionesPorAlumnoSec.PageIndex"] = PageIndex;

            #region Variables de sesion para filtros
            //[VariablesDeSesionParaFiltros1]
            #endregion
            int aluId = Convert.ToInt32(Session["aluId"]);
            int IdIC = 0;
            LblMensajeErrorGrilla.Text = "";
            if (anioCur.Text == "")

            {

                if (Request.QueryString["Anio"] == "")
                {
                    DateTime fechaActual = DateTime.Today;
                    AnioCur = Convert.ToInt32(fechaActual.Year.ToString());

                }
                else
                {
                    AnioCur = Convert.ToInt32(Request.QueryString["Anio"]);
                }
                IdIC = Convert.ToInt32(Request.QueryString["Id"]);
            }

            else
            {
                AnioCur = Convert.ToInt32(anioCur.Text);

                dt3 = ocnInscripcionCursado.ObtenerTodoxInsIdxaluIdxAnioCursado(insId, AnioCur, aluId);

                IdIC = Convert.ToInt32(dt3.Rows[0]["Id"].ToString());

            }

            if (Request.QueryString["Id"] != null)
            {

                dt = new DataTable();
                dt = ocnRegistracionNota.ObtenerTodoporInscripcionCursadoAnio(IdIC, AnioCur);
                this.GridView1.DataSource = dt;

                this.GridView1.PageIndex = PageIndex;
                this.GridView1.DataBind();
                if ((Session["_perId"].ToString() == "10") | (Session["_perId"].ToString() == "9") | (Session["_perId"].ToString() == "4")) // Si es igual a familiar oculto edicion
                {
                    GridView1.Columns[8].Visible = false;
                    GridView1.Columns[9].Visible = false;
                }
                dt4 = ocnRegistracionNotaPrevia.ObtenerSoloPrevias(aluId);

                this.GridView2.DataSource = dt4;
                this.GridView2.PageIndex = PageIndex;
                this.GridView2.DataBind();
                GridView2.Columns[9].Visible = true;
                GridView2.Columns[10].Visible = true;
                if ((Session["_perId"].ToString() == "10") | (Session["_perId"].ToString() == "9") | (Session["_perId"].ToString() == "4")) // Si es igual a familiar no modifico
                {
                    GridView2.Columns[8].Visible = false;
                    GridView2.Columns[9].Visible = false;
                    GridView2.Columns[10].Visible = false;
                    GridView2.Columns[11].Visible = false;
                }
                lblCantidadPrevias.Text = "   " + dt4.Rows.Count.ToString();
                BtnMostrar.Visible = true;

                if (dt4.Rows.Count == 0)
                {

                    lblCantidadPrevias.Text = "No Tiene";

                }

                BtnMostrar.Enabled = true;
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

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            int PageIndex = 0;
            PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoSec.PageIndex"]);
            GrillaCargar(PageIndex);
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

    protected void BtnMostrar_Click(object sender, EventArgs e)
    {
        try
        {
            int aluId = Convert.ToInt32(Session["aluId"]);
            dt4 = ocnRegistracionNotaPrevia.ObtenerTodoxaluId(aluId);
            int PageIndex = 0;
            if (dt4.Rows.Count > 0)
            {
                this.GridView2.DataSource = dt4;

                Convert.ToInt32(Session["CargaCalificacionesPorAlumnoSec.PageIndex"]);
                this.GridView2.PageIndex = PageIndex;
                this.GridView2.DataBind();


                GridView2.Columns[10].Visible = false;
                GridView2.Columns[9].Visible = false;

                BtnSoloPrevias.Visible = true;
                BtnMostrar.Enabled = false;

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


    protected void BtnMostrarPrevias_Click(object sender, EventArgs e)
    {
        try
        {
            int aluId = Convert.ToInt32(Session["aluId"]);
            dt4 = ocnRegistracionNotaPrevia.ObtenerSoloPrevias(aluId);
            int PageIndex = 0;
            if (dt4.Rows.Count > 0)
            {
                this.GridView2.DataSource = dt4;

                Convert.ToInt32(Session["CargaCalificacionesPorAlumnoSec.PageIndex"]);
                this.GridView2.PageIndex = PageIndex;
                this.GridView2.DataBind();
                if ((Session["_perId"].ToString() != "10") & (Session["_perId"].ToString() != "9")) // Si es distinto a familiar puedo modificar
                {   //GridView2.Columns[10].Visible = true;
                    GridView2.Columns[9].Visible = true;
                    BtnSoloPrevias.Visible = false;
                    BtnMostrar.Enabled = true;
                }
            }
            PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoSec.PageIndex"]);
            GrillaCargar(PageIndex);
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
}

