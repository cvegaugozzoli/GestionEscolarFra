using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class CargaCalificacionesPorAlumnoPri : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DataTable dt2 = new DataTable();
    DataTable dt3 = new DataTable();
    DataTable dt4 = new DataTable();
    DataTable dt5 = new DataTable();
    int AnioCur;
    int insId;
    GESTIONESCOLAR.Negocio.Curso ocnCurso = new GESTIONESCOLAR.Negocio.Curso();
    GESTIONESCOLAR.Negocio.InscripcionCursado ocnInscripcionCursado = new GESTIONESCOLAR.Negocio.InscripcionCursado();
    GESTIONESCOLAR.Negocio.RegistracionNotaPrevia ocnRegistracionNotaPrevia = new GESTIONESCOLAR.Negocio.RegistracionNotaPrevia();
    GESTIONESCOLAR.Negocio.RegistracionNota ocnRegistracionNota = new GESTIONESCOLAR.Negocio.RegistracionNota();
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

                if ((Session["_perId"].ToString() == "10")) // Si es familiar no muestro para modificar periodo
                {
                    LblPeriodo.Visible = false;
                    PeriodoId.Visible = false;
                    TextNotaAsignar.Visible = false;
                    lblNota.Visible = false;
                    ButtonAsignar.Visible = false;
                }

                #region PageIndex
                int PageIndex = 0;
                if (this.Session["CargaCalificacionesPorAlumnoPri.PageIndex"] == null)
                {
                    Session.Add("CargaCalificacionesPorAlumnoPri.PageIndex", 0);
                }
                else
                {
                    PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoPri.PageIndex"]);
                }
                #endregion

                #region Variables de sesion para filtros    

                GrillaCargar(PageIndex);

            }        //if (Session["CursoConsulta.Nombre"] != null) { Curso.Text = Session["CursoConsulta.Nombre"].ToString(); } else { Session.Add("CursoConsulta.Nombre", Nombre.Text.Trim()); }
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
        if ((Session["_perId"].ToString() != "10")) // Si es distinto a familiar puedo modificar
        {

            GridView1.EditIndex = e.NewEditIndex;
            int PageIndex = 0;
            PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoPri.PageIndex"]);
            GrillaCargar(PageIndex);
            GridView1.Rows[e.NewEditIndex].Attributes.Remove("ondblclick");
            GridView1.Columns[10].Visible = true;
            GridView1.Columns[11].Visible = true;
        }
        else
        {
            LblMensajeErrorGrilla.Text = "No puede modificar notas..";
            return;
        }
    }

    protected void OnCancel(object sender, EventArgs e)
    {
        GridView1.EditIndex = -1;
        int PageIndex = 0;
        PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoPri.PageIndex"]);
        GrillaCargar(PageIndex);

    }

    protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if ((Session["_perId"].ToString() != "10")) // Si es distinto a familiar puedo modificar
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

    protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            if ((Session["_perId"].ToString() != "10")) // Si es distinto a familiar puedo modificar
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int Id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

                int aluId = Convert.ToInt32(Session["aluId"]);
                TextBox PTrim = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtPTrim");
                TextBox STrim = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtSTrim");
                TextBox TTrim = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtTTrim");
                TextBox PAnual = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtPAnual");
                TextBox NotaDic = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtDiciembre");
                TextBox NotaMar = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtMarzo");
                TextBox renCalfDef = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtrenCalfDef");
                String PTrim2 = PTrim.Text;
                String STrim2 = STrim.Text;
                String TTrim2 = TTrim.Text;
                String PAnual2 = PAnual.Text;
                String NotaDic2 = NotaDic.Text;
                String NotaMar2 = NotaMar.Text;

                DateTime RenFechaHoraCreacion = DateTime.Now;
                DateTime RenFechaHoraUltimaModificacion = DateTime.Now;
                Int32 usuIdCreacion = this.Master.usuId;
                Int32 usuIdUltimaModificacion = this.Master.usuId;


                String renCalfDef2 = renCalfDef.Text;
                Int32 renCalfDefInt = Convert.ToInt32((renCalfDef2 == "" ? "100" : renCalfDef2));

                ocnRegistracionNota.ActualizarPrimaria(Id, PTrim2, STrim2, TTrim2, PAnual2, NotaDic2, NotaMar2, renCalfDef2, RenFechaHoraCreacion, RenFechaHoraUltimaModificacion, usuIdCreacion, usuIdUltimaModificacion);
                GridView1.EditIndex = -1;
                int PageIndex = 0;
                PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnoPri.PageIndex"]);


                //String CalPrev = Convert.ToString((dt.Rows[0]["Calificacion"].ToString()) == "" ? "100" : dt.Rows[0]["Calificacion"].ToString());
                //String Fecha = Convert.ToString(row["Fecha"].ToString());
                GrillaCargar(PageIndex);
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




    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //this.BindGrid();
    }

    protected void ButtonAsignar_Click(object sender, EventArgs e)
    {
        insId = Convert.ToInt32(Session["_Institucion"]);

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


        if (TextNotaAsignar.Text != "")
        {

            if (PeriodoId.SelectedIndex != 7)
            {
                dt = ocnRegistracionNota.ObtenerTodoporInscripcionCursadoAnio(IdIC, AnioCur);

                if (PeriodoId.SelectedIndex == 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (Convert.ToInt32(row["FDictado"].ToString()) != 5)
                            {
                                int RenId = Convert.ToInt32(row["Id"].ToString());
                                ocnRegistracionNota.AsignarNotaPriPT(RenId, TextNotaAsignar.Text);
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
                                ocnRegistracionNota.AsignarNotaPriST(RenId, TextNotaAsignar.Text);
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
                                ocnRegistracionNota.AsignarNotaPriTT(RenId, TextNotaAsignar.Text);
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
                                ocnRegistracionNota.AsignarNotaPromA(RenId, TextNotaAsignar.Text);
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
                                ocnRegistracionNota.AsignarNotaDic(RenId, TextNotaAsignar.Text);
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
                                ocnRegistracionNota.AsignarNotaMar(RenId, TextNotaAsignar.Text);
                            }
                        }
                    }
                }
                if (PeriodoId.SelectedIndex == 6)
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
                PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnopri.PageIndex"]);
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


    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        int PageIndex = 0;
        PageIndex = Convert.ToInt32(Session["CargaCalificacionesPorAlumnopri.PageIndex"]);
        GrillaCargar(PageIndex);
    }
    private void GrillaCargar(int PageIndex)
    {
        try
        {
            Session["CargaCalificacionesPorAlumnoPri.PageIndex"] = PageIndex;

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


            if (Request.QueryString["Id"] != null)
            {

                dt = new DataTable();
                dt = ocnRegistracionNota.ObtenerTodoporInscripcionCursadoAnio(IdIC, AnioCur);
                this.GridView1.DataSource = dt;
                this.GridView1.PageIndex = PageIndex;
                this.GridView1.DataBind();
                if ((Session["_perId"].ToString() == "10")) // Si es distinto a familiar puedo modificar
                {

                    GridView1.Columns[10].Visible = false;
                    GridView1.Columns[11].Visible = false;
                    //int ban = 0;          
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
    protected void btnEliminarCancelar_Click(object sender, EventArgs e)
    {
        ((Button)sender).Parent.Controls[1].Visible = true;
        ((Button)sender).Parent.Controls[3].Visible = false;
        ((Button)sender).Parent.Controls[5].Visible = false;
    }


    protected void btnEliminarAceptar_Click(object sender, EventArgs e)
    {
        try
        {
            int Id = 0;
            Id = Convert.ToInt32(((HyperLink)((GridViewRow)((Button)sender).Parent.Parent).Cells[0].Controls[1]).Text);

            ocnRegistracionNotaPrevia.Eliminar(Id);

            ((Button)sender).Parent.Controls[1].Visible = true;
            ((Button)sender).Parent.Controls[3].Visible = false;
            ((Button)sender).Parent.Controls[5].Visible = false;

            //GrillaCargar(GridView2.PageIndex);
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



    protected void ButtonInscripcion_Click(object sender, EventArgs e)
    {

    }
}