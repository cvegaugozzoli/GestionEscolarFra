using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class UsuarioEspacioCurricularRegistracion : System.Web.UI.Page
{
    GESTIONESCOLAR.Negocio.UsuarioEspacioCurricular ocnUsuarioEspacioCurricular = new GESTIONESCOLAR.Negocio.UsuarioEspacioCurricular();
    GESTIONESCOLAR.Negocio.Docentes ocnDocentes = new GESTIONESCOLAR.Negocio.Docentes();
    GESTIONESCOLAR.Negocio.InscripcionCursado ocnInscripcionCursado = new GESTIONESCOLAR.Negocio.InscripcionCursado();
    GESTIONESCOLAR.Negocio.EspacioCurricular ocnEspacioCurricular = new GESTIONESCOLAR.Negocio.EspacioCurricular();
    GESTIONESCOLAR.Negocio.PlanEstudio ocnPlanEstudio = new GESTIONESCOLAR.Negocio.PlanEstudio();
    GESTIONESCOLAR.Negocio.Curso ocnCurso = new GESTIONESCOLAR.Negocio.Curso();
    GESTIONESCOLAR.Negocio.Campo ocnCampo = new GESTIONESCOLAR.Negocio.Campo();
    GESTIONESCOLAR.Negocio.Usuario ocnUsuario = new GESTIONESCOLAR.Negocio.Usuario();
    int insId;
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            insId = Convert.ToInt32(Session["_Institucion"]);
            if (!Page.IsPostBack)
            {
                this.Master.TituloDelFormulario = " Usuario Espacio Curricular - Registracion";

                //if (this.Session["_Autenticado"] == null) Response.Redirect("~/PaginasBasicas/Login.aspx", true);
                Session["Bandera"] = 0;
                if (Request.QueryString["Ver"] != null)
                {
                    //btnAceptar.Visible = false;
                    btnAceptar1.Visible = false;
                }

                int Id = 0;
                if (Request.QueryString["Id"] != null)
                {
                    Id = Convert.ToInt32(Request.QueryString["Id"]);

                    /*INCIALIZADORES*/
                    //usuId.DataValueField = "Valor"; usuId.DataTextField = "Texto"; usuId.DataSource = (new GESTIONESCOLAR.Negocio.Usuario()).ObtenerLista("[Seleccionar...]"); usuId.DataBind();
                    carId.DataValueField = "Valor"; carId.DataTextField = "Texto"; carId.DataSource = (new GESTIONESCOLAR.Negocio.Carrera()).ObtenerLista("[Seleccionar...]"); carId.DataBind();
                    plaId.DataValueField = "Valor"; plaId.DataTextField = "Texto"; plaId.DataSource = (new GESTIONESCOLAR.Negocio.PlanEstudio()).ObtenerLista("[Seleccionar...]"); plaId.DataBind();
                    curId.DataValueField = "Valor"; curId.DataTextField = "Texto"; curId.DataSource = (new GESTIONESCOLAR.Negocio.Curso()).ObtenerLista("[Seleccionar...]"); curId.DataBind();
                    //camId.DataValueField = "Valor"; camId.DataTextField = "Texto"; camId.DataSource = (new GESTIONESCOLAR.Negocio.Campo()).ObtenerLista("[Seleccionar...]"); camId.DataBind();
                    escId.DataValueField = "Valor"; escId.DataTextField = "Texto"; escId.DataSource = (new GESTIONESCOLAR.Negocio.EspacioCurricular()).ObtenerLista("[Seleccionar...]",insId); escId.DataBind();


                    if (Id != 0)
                    {
                        ocnUsuarioEspacioCurricular = new GESTIONESCOLAR.Negocio.UsuarioEspacioCurricular(Id);
                        this.uscAnoCursado.Text = ocnUsuarioEspacioCurricular.uscAnoCursado.ToString();
                        this.uscActivo.Checked = ocnUsuarioEspacioCurricular.uscActivo;
                        int usuIdBuscar = ocnUsuarioEspacioCurricular.usuId;
                        dt1 = ocnUsuario.ObtenerUno(usuIdBuscar);
                        if (dt1.Rows.Count > 0)
                        {
                            this.ApellidoB.Text = Convert.ToString(dt1.Rows[0]["Apellido"]);
                            this.NombreB.Text = Convert.ToString(dt1.Rows[0]["Nombre"]);
                            this.usuId.Text = Convert.ToString(dt1.Rows[0]["Id"]);
                            this.PerfilId.Text = Convert.ToString(dt1.Rows[0]["perId"]);
                        }
                        else
                        {
                            //lblCantidadRegistros.Text = "Cantidad de registros: 0";
                        }

                        //this.usuId.SelectedValue = (ocnUsuarioEspacioCurricular.usuId == 0 ? "" : ocnUsuarioEspacioCurricular.usuId.ToString());
                        this.carId.SelectedValue = (ocnUsuarioEspacioCurricular.carId == 0 ? "" : ocnUsuarioEspacioCurricular.carId.ToString());
                        this.plaId.SelectedValue = (ocnUsuarioEspacioCurricular.plaId == 0 ? "" : ocnUsuarioEspacioCurricular.plaId.ToString());
                        this.curId.SelectedValue = (ocnUsuarioEspacioCurricular.curId == 0 ? "" : ocnUsuarioEspacioCurricular.curId.ToString());
                        //this.camId.SelectedValue = (ocnUsuarioEspacioCurricular.camId == 0 ? "" : ocnUsuarioEspacioCurricular.camId.ToString());
                        this.escId.SelectedValue = (ocnUsuarioEspacioCurricular.escId == 0 ? "" : ocnUsuarioEspacioCurricular.escId.ToString());

                        /*Editar Habilitado*/
                    }
                    else
                    {


                        /*Nuevo Habilitado*/

                        /*cLoadNuevoCustom*/
                    }

                    //this.usuId.Focus();
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

    private void GrillaBuscarCargar()
    {
        try
        {
            //Session["AlumnoConsulta.PageIndex"] = PageIndex;

            #region Variables de sesion para filtros
            //[VariablesDeSesionParaFiltros1]
            #endregion
            dt = new DataTable();

            if (Convert.ToInt32(Session["Bandera"]) == 0)
            {
                dt = ocnDocentes.ObtenerUnoxNombre(TextBuscar.Text.Trim());
                this.GrillaBuscar.DataSource = dt;
                //this.Grilla.PageIndex = PageIndex;
                this.GrillaBuscar.DataBind();
            }
            else
            {
                dt = ocnDocentes.ObtenerUnoxDoc(TextBuscar.Text.Trim());
                this.GrillaBuscar.DataSource = dt;
                //this.Grilla.PageIndex = PageIndex;
                this.GrillaBuscar.DataBind();
            }


            if (dt.Rows.Count > 0)
            {
                lblCantidadRegistros.Text = "Cantidad de registros: " + dt.Rows.Count.ToString();
            }
            else
            {
                lblCantidadRegistros.Text = "Cantidad de registros: 0";
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
        foreach (GridViewRow row in GrillaBuscar.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                row.Attributes["onmouseover"] = "this.style.background = '#0BB8A1'; this.style.cursor = 'pointer'";
                row.Attributes["onmouseout"] = "this.style.background='#ffffff'";
                row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(GrillaBuscar, "Select$" + row.DataItemIndex, true);
            }
        }
        base.Render(writer);
    }


    protected void GrillaBuscar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Sort" && e.CommandName != "Page" && e.CommandName != "")
            {
                String Id = ((HyperLink)GrillaBuscar.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Controls[1]).Text;

                if (e.CommandName == "Select")
                {
                    ApellidoB.Text = ((HyperLink)GrillaBuscar.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Controls[1]).Text;
                    NombreB.Text = ((HyperLink)GrillaBuscar.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Controls[1]).Text;
                    usuId.Text = ((HyperLink)GrillaBuscar.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Controls[1]).Text;
                    PerfilId.Text = ((HyperLink)GrillaBuscar.Rows[Convert.ToInt32(e.CommandArgument)].Cells[5].Controls[1]).Text;
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

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        GrillaBuscarCargar();
    }

    protected void GrillaBuscar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor; this.style.backgroundColor='#F7F7DE';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;");
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("UsuarioEspacioCurricularConsulta.aspx", true);
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
    protected void btnCancelar2_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("UsuarioEspacioCurricularConsulta.aspx", true);
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
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        try
        {
            int usuIdC = Convert.ToInt32(usuId.Text);
            int carIdC = Convert.ToInt32((carId.SelectedValue.Trim() == "" ? "0" : carId.SelectedValue));
            int plaIdC = Convert.ToInt32((plaId.SelectedValue.Trim() == "" ? "0" : plaId.SelectedValue));
            int curIdC = Convert.ToInt32((curId.SelectedValue.Trim() == "" ? "0" : curId.SelectedValue));
            int escIdC = Convert.ToInt32((escId.SelectedValue.Trim() == "" ? "0" : escId.SelectedValue));
            DataTable dt3 = new DataTable();

            dt3 = ocnUsuarioEspacioCurricular.ObtenerUnoControlExiste(usuIdC, carIdC, plaIdC, curIdC, escIdC);
            if (dt3.Rows.Count == 0)// SI NO FUE ASIGNADO A ESE ESPACIO CURRICULAR.. LO INSERTO
            {

                int Id = 0;
                if (Request.QueryString["Id"] != null)
                {
                    Id = Convert.ToInt32(Request.QueryString["Id"]);

                    ocnUsuarioEspacioCurricular = new GESTIONESCOLAR.Negocio.UsuarioEspacioCurricular(Id);

                    ocnUsuarioEspacioCurricular.usuId = Convert.ToInt32(usuId.Text);

                    ocnUsuarioEspacioCurricular.uscAnoCursado = Convert.ToInt32(uscAnoCursado.Text);
                    ocnUsuarioEspacioCurricular.uscActivo = uscActivo.Checked;


                    ocnUsuarioEspacioCurricular.carId = Convert.ToInt32((carId.SelectedValue.Trim() == "" ? "0" : carId.SelectedValue));
                    ocnUsuarioEspacioCurricular.plaId = Convert.ToInt32((plaId.SelectedValue.Trim() == "" ? "0" : plaId.SelectedValue));
                    ocnUsuarioEspacioCurricular.curId = Convert.ToInt32((curId.SelectedValue.Trim() == "" ? "0" : curId.SelectedValue));
                    //ocnUsuarioEspacioCurricular.camId = Convert.ToInt32((camId.SelectedValue.Trim() == "" ? "0" : camId.SelectedValue));
                    ocnUsuarioEspacioCurricular.escId = Convert.ToInt32((escId.SelectedValue.Trim() == "" ? "0" : escId.SelectedValue));
                    /*....usuId = this.Master.usuId;*/


                    ocnUsuarioEspacioCurricular.uscFechaHoraCreacion = DateTime.Now;
                    ocnUsuarioEspacioCurricular.uscFechaHoraUltimaModificacion = DateTime.Now;
                    ocnUsuarioEspacioCurricular.usuIdCreacion = this.Master.usuId;
                    ocnUsuarioEspacioCurricular.usuIdUltimaModificacion = this.Master.usuId;

                    /*Validaciones*/
                    string MensajeValidacion = "";

                    if (MensajeValidacion.Trim().Length == 0)
                    {
                        if (Id == 0)
                        {
                            if ((Convert.ToInt32(PerfilId.Text) != 1) || (Convert.ToInt32(PerfilId.Text) != 6) || (Convert.ToInt32(PerfilId.Text) != 9))// Distinto a Admin Directora o Secretaria
                            {
                                if (curId.SelectedValue.Trim() != "")
                                {
                                    if ((Convert.ToInt32(PerfilId.Text) != 2) || (Convert.ToInt32(PerfilId.Text) != 5))
                                    {
                                        if (escId.SelectedValue.Trim() != "")
                                        {
                                            //Nuevo
                                            ocnUsuarioEspacioCurricular.Insertar();
                                        }
                                        else
                                        {
                                            Response.Write("MENSAJE DE ERROR:<br>" + MensajeValidacion);

                                            lblMensajeError.Text = @"<div class=""alert alert-warning alert-dismissable"">
        <button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
        <a class=""alert-link"" href=""#"">Error de Carga</a><br/>
       Debe ingresar un Espacio Curricular<br/>";
                                        }
                                    }
                                    else
                                    {
                                        ocnUsuarioEspacioCurricular.Insertar();
                                    }
                                }
                                else
                                {
                                    Response.Write("MENSAJE DE ERROR:<br>" + MensajeValidacion);

                                    lblMensajeError.Text = @"<div class=""alert alert-warning alert-dismissable"">
        <button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
        <a class=""alert-link"" href=""#"">Error de Carga</a><br/>
       Debe ingresar un Curso<br/>";
                                }
                            }

                            else
                            {
                                ocnUsuarioEspacioCurricular.Insertar();
                            }
                            Response.Redirect("UsuarioEspacioCurricularConsulta.aspx", true);

                        }
                        else
                        {
                            //Editar
                            ocnUsuarioEspacioCurricular.uscFechaHoraUltimaModificacion = DateTime.Now;
                            ocnUsuarioEspacioCurricular.usuIdUltimaModificacion = this.Master.usuId;
                            ocnUsuarioEspacioCurricular.Actualizar();


                            Response.Redirect("UsuarioEspacioCurricularConsulta.aspx", true);
                        }
                    }
                    else
                    {
                        Response.Write("MENSAJE DE ERROR:<br>" + MensajeValidacion);

                        lblMensajeError.Text = @"<div class=""alert alert-warning alert-dismissable"">
        <button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
        <a class=""alert-link"" href=""#"">Error de Sistema</a><br/>
        Se ha producido el siguiente error:<br/>" + MensajeValidacion + "</div>";
                    }
                }
            }
            else
            {
                lblMensajeError.Text = @"<div class=""alert alert-warning alert-dismissable"">
        <button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
        <a class=""alert-link"" href=""#"">Error de Carga</a><br/>
       El docente ya fue asignado a ese espacio Curricular o curso..o<br/>";

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

    protected void RbtBuscar_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ban;
        if (RbtBuscar.SelectedIndex == 1) //la busqueda será por dni
        {
            ban = 1;

        }
        else
        {
            ban = 0;// la busqueda será por nombre
        }

        Session["Bandera"] = ban;
        //aludni.Text = "";
        //aluNombre.Text = "";
        TextBuscar.Text = "";
    }

    protected void carId_SelectedIndexChanged(object sender, EventArgs e)
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
            if (Convert.ToInt32(dt.Rows[1]["TipoCarrera"]) != 4)
            {
                //camId.Enabled = false;
                //escId.Enabled = false;
            }
        }
    }


    protected void plaId_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (plaId.SelectedIndex != 0)
        {
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
        insId = Convert.ToInt32(Session["_Institucion"]);
        if (curId.SelectedIndex != 0)
        {

            //ClubB.Negocio.Evento ocnEvento = new ClubB.Negocio.Evento();
            DataTable dt = new DataTable();
            dt = ocnCampo.ObtenerListaPorUnCurso("[Seleccionar...]", Convert.ToInt32(curId.SelectedValue));

            if (dt.Rows.Count > 0)
            {
                escId.DataValueField = "Valor";
                escId.DataTextField = "Nombre";
                escId.DataSource = (new GESTIONESCOLAR.Negocio.EspacioCurricular()).ObtenerListaPorUnCurso2("[Seleccionar..]", Convert.ToInt32(curId.SelectedValue),insId);
                escId.DataBind();

            }
        }
    }


    //protected void camId_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (camId.SelectedIndex != 0)
    //    {

    //        //ClubB.Negocio.Evento ocnEvento = new ClubB.Negocio.Evento();
    //        DataTable dt = new DataTable();
    //        dt = ocnEspacioCurricular.ObtenerListaPorUnCampo("[Seleccionar...]", Convert.ToInt32(camId.SelectedValue));
    //        if (dt.Rows.Count > 0)
    //        {
    //            escId.DataValueField = "Valor";
    //            escId.DataTextField = "Texto";
    //            escId.DataSource = (new GESTIONESCOLAR.Negocio.EspacioCurricular()).ObtenerListaPorUnCampo("[Seleccionar...]", Convert.ToInt32(camId.SelectedValue));
    //            escId.DataBind();
    //        }
    //    }
}








