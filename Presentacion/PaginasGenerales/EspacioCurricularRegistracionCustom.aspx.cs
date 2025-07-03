using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class EspacioCurricularRegistracionCustom : System.Web.UI.Page
{
    GESTIONESCOLAR.Negocio.EspacioCurricular ocnEspacioCurricular = new GESTIONESCOLAR.Negocio.EspacioCurricular();
    GESTIONESCOLAR.Negocio.PlanEstudio ocnPlanEstudio = new GESTIONESCOLAR.Negocio.PlanEstudio();
    GESTIONESCOLAR.Negocio.Curso ocnCurso = new GESTIONESCOLAR.Negocio.Curso();
    GESTIONESCOLAR.Negocio.Campo ocnCampo = new GESTIONESCOLAR.Negocio.Campo();
    int insId;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                this.Master.TituloDelFormulario = " Espacio Curricular - Registracion";
                insId = Convert.ToInt32(Session["_Institucion"]);
                //if (this.Session["_Autenticado"] == null) Response.Redirect("~/PaginasBasicas/Login.aspx", true);

                if (Request.QueryString["Ver"] != null)
                {
                    btnAceptar.Visible = false;
                    btnAceptar1.Visible = false;
                }

                int Id = 0;
                if (Request.QueryString["Id"] != null)
                {
                    Id = Convert.ToInt32(Request.QueryString["Id"]);

                    /*INCIALIZADORES*/
                    carId.DataValueField = "Valor"; carId.DataTextField = "Texto"; carId.DataSource = (new GESTIONESCOLAR.Negocio.Carrera()).ObtenerLista("[Seleccionar...]"); carId.DataBind();
                    //plaId.DataValueField = "Valor"; plaId.DataTextField = "Texto"; plaId.DataSource = (new GESTIONESCOLAR.Negocio.PlanEstudio()).ObtenerLista("[Seleccionar...]"); plaId.DataBind();
                    //curId.DataValueField = "Valor"; curId.DataTextField = "Texto"; curId.DataSource = (new GESTIONESCOLAR.Negocio.Curso()).ObtenerLista("[Seleccionar...]"); curId.DataBind();
                    //camId.DataValueField = "Valor"; camId.DataTextField = "Texto"; camId.DataSource = (new GESTIONESCOLAR.Negocio.Campo()).ObtenerLista("[Seleccionar...]"); camId.DataBind();
                    fodId.DataValueField = "Valor"; fodId.DataTextField = "Texto"; fodId.DataSource = (new GESTIONESCOLAR.Negocio.FormatoDictado()).ObtenerLista("[Seleccionar...]"); fodId.DataBind();
                    regId.DataValueField = "Valor"; regId.DataTextField = "Texto"; regId.DataSource = (new GESTIONESCOLAR.Negocio.Regimen()).ObtenerLista("[Seleccionar...]"); regId.DataBind();
                    cdnId.DataValueField = "Valor"; cdnId.DataTextField = "Texto"; cdnId.DataSource = (new GESTIONESCOLAR.Negocio.Condicion()).ObtenerLista("[Seleccionar...]"); cdnId.DataBind();

                    if (Id != 0)
                    {
                       
                        plaId.DataValueField = "Valor"; plaId.DataTextField = "Texto"; plaId.DataSource = (new GESTIONESCOLAR.Negocio.PlanEstudio()).ObtenerLista("[Seleccionar...]"); plaId.DataBind();
                        curId.DataValueField = "Valor"; curId.DataTextField = "Texto"; curId.DataSource = (new GESTIONESCOLAR.Negocio.Curso()).ObtenerLista("[Seleccionar...]"); curId.DataBind();
                        camId.DataValueField = "Valor"; camId.DataTextField = "Texto"; camId.DataSource = (new GESTIONESCOLAR.Negocio.Campo()).ObtenerLista("[Seleccionar...]"); camId.DataBind();
                        carId.Enabled = false;
                        plaId.Enabled = false;
                        curId.Enabled = false;
                        camId.Enabled = false;
                        ocnEspacioCurricular = new GESTIONESCOLAR.Negocio.EspacioCurricular(Id, insId);
                        this.escNombre.Text = ocnEspacioCurricular.escNombre;
                        this.escHorasSemanalesReloj.Text = ocnEspacioCurricular.escHorasSemanalesReloj.ToString();
                        this.escHorasSemanalesCatedra.Text = ocnEspacioCurricular.escHorasSemanalesCatedra.ToString();
                        this.escCantidadParciales.Text = ocnEspacioCurricular.escCantidadParciales.ToString();
                        this.escCantidadRecuperatorios.Text = ocnEspacioCurricular.escCantidadRecuperatorios.ToString();
                        this.escActivo.Checked = ocnEspacioCurricular.escActivo;
                        this.carId.SelectedValue = (ocnEspacioCurricular.carId == 0 ? "" : ocnEspacioCurricular.carId.ToString());
                        this.plaId.SelectedValue = (ocnEspacioCurricular.plaId == 0 ? "" : ocnEspacioCurricular.plaId.ToString());
                        this.curId.SelectedValue = (ocnEspacioCurricular.curId == 0 ? "" : ocnEspacioCurricular.curId.ToString());
                        this.camId.SelectedValue = (ocnEspacioCurricular.camId == 0 ? "" : ocnEspacioCurricular.camId.ToString());
                        this.fodId.SelectedValue = (ocnEspacioCurricular.fodId == 0 ? "" : ocnEspacioCurricular.fodId.ToString());
                        this.regId.SelectedValue = (ocnEspacioCurricular.regId == 0 ? "" : ocnEspacioCurricular.regId.ToString());
                        this.cdnId.SelectedValue = (ocnEspacioCurricular.cdnId == 0 ? "" : ocnEspacioCurricular.cdnId.ToString());

                        /*Editar Habilitado*/
                    }
                    else
                    {


                        /*Nuevo Habilitado*/

                        /*cLoadNuevoCustom*/
                    }

                    this.carId.Focus();
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

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("EspacioCurricularConsulta.aspx", true);
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
            insId = Convert.ToInt32(Session["_Institucion"]);
            string MensajeValidacion = "";
            int Id = 0;

        //    if (carId.SelectedValue.Trim() != "" & plaId.SelectedValue.Trim() != "" & plaId.SelectedValue.Trim() != "0" & curId.SelectedValue.Trim() != "" & 
        //        curId.SelectedValue.Trim() != "0" & camId.SelectedValue.Trim() != "" & camId.SelectedValue.Trim() != "0" & fodId.SelectedValue.Trim() != "" & 
        //        fodId.SelectedValue.Trim() != "0" & regId.SelectedValue.Trim() != "" & regId.SelectedValue.Trim() != "0" & conId.SelectedValue.Trim() != "" &
        //        conId.SelectedValue.Trim() != "0" & escNombre.Text.Trim() != "" & escHorasSemanalesReloj.Text.Trim() != "")
        //        // & escHorasSemanalesCatedra.Text.Trim() != "" &
        //        //                escCantidadParciales.Text.Trim() != "" & escCantidadRecuperatorios.Text.Trim() != ""
        //    {
               
        //        Response.Write("MENSAJE DE ERROR:<br>" + MensajeValidacion);

        //        lblMensajeError.Text = @"<div class=""alert alert-warning alert-dismissable"">
        //<button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
        //<a class=""alert-link"" href=""#"">Error: </a><br/> <br/>" + MensajeValidacion + "</div>";
        //        return;
        //    }


            if (Request.QueryString["Id"] != null)
            {
                Id = Convert.ToInt32(Request.QueryString["Id"]);
                ocnEspacioCurricular = new GESTIONESCOLAR.Negocio.EspacioCurricular(Id,insId);

                ocnEspacioCurricular.escNombre = escNombre.Text.Trim();
                ocnEspacioCurricular.escHorasSemanalesReloj = Convert.ToInt32(escHorasSemanalesReloj.Text.Trim());
                ocnEspacioCurricular.escHorasSemanalesCatedra = Convert.ToInt32(escHorasSemanalesCatedra.Text);
                ocnEspacioCurricular.escCantidadParciales = Convert.ToInt32(escCantidadParciales.Text);
                ocnEspacioCurricular.escCantidadRecuperatorios = Convert.ToInt32(escCantidadRecuperatorios.Text);
                ocnEspacioCurricular.escActivo = escActivo.Checked;

                ocnEspacioCurricular.carId = Convert.ToInt32((carId.SelectedValue.Trim() == "" ? "0" : carId.SelectedValue));
                ocnEspacioCurricular.plaId = Convert.ToInt32((plaId.SelectedValue.Trim() == "" ? "0" : plaId.SelectedValue));
                ocnEspacioCurricular.curId = Convert.ToInt32((curId.SelectedValue.Trim() == "" ? "0" : curId.SelectedValue));
                ocnEspacioCurricular.camId = Convert.ToInt32((camId.SelectedValue.Trim() == "" ? "0" : camId.SelectedValue));
                ocnEspacioCurricular.fodId = Convert.ToInt32((fodId.SelectedValue.Trim() == "" ? "0" : fodId.SelectedValue));
                ocnEspacioCurricular.regId = Convert.ToInt32((regId.SelectedValue.Trim() == "" ? "0" : regId.SelectedValue));
                ocnEspacioCurricular.cdnId = Convert.ToInt32((cdnId.SelectedValue.Trim() == "" ? "0" : cdnId.SelectedValue));
                /*....usuId = this.Master.usuId;*/


                ocnEspacioCurricular.escFechaHoraCreacion = DateTime.Now;
                ocnEspacioCurricular.escFechaHoraUltimaModificacion = DateTime.Now;
                ocnEspacioCurricular.usuIdCreacion = this.Master.usuId;
                ocnEspacioCurricular.usuIdUltimaModificacion = this.Master.usuId;

                /*Validaciones*/
                

                if (MensajeValidacion.Trim().Length == 0)
                {
                    if (Id == 0)
                    {
                        //Nuevo
                        ocnEspacioCurricular.Insertar();
                    }
                    else
                    {
                        //Editar
                        ocnEspacioCurricular.escFechaHoraUltimaModificacion = DateTime.Now;
                        ocnEspacioCurricular.usuIdUltimaModificacion = this.Master.usuId;
                        ocnEspacioCurricular.Actualizar();
                    }

                    Response.Redirect("EspacioCurricularConsulta.aspx", true);
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
        catch (Exception oError)
        {
            lblMensajeError.Text = @"<div class=""alert alert-danger alert-dismissable"">
<button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
<a class=""alert-link"" href=""#"">Error:</a><br/><br/>
MESSAGE:<br>" + oError.Message + "<br><br>EXCEPTION:<br>" +  //  + oError.InnerException + "<br><br>TRACE:<br>" + oError.StackTrace + "<br><br>TARGET:<br>" + oError.TargetSite 
"</div>";
        }

    }


    protected void carId_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (carId.SelectedIndex != 0)
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
        if (plaId.SelectedIndex != 0)
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
        if (curId.SelectedIndex != 0)
        {

            //ClubB.Negocio.Evento ocnEvento = new ClubB.Negocio.Evento();
            DataTable dt = new DataTable();
            dt = ocnCampo.ObtenerListaPorUnCurso("[Seleccionar...]", Convert.ToInt32(curId.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                camId.DataValueField = "Valor";
                camId.DataTextField = "Texto";
                camId.DataSource = (new GESTIONESCOLAR.Negocio.Campo()).ObtenerListaPorUnCurso("[Seleccionar...]", Convert.ToInt32(curId.SelectedValue));
                camId.DataBind();
            }
        }
    }



}
