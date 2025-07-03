﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class LoginPadres : System.Web.UI.Page
{
    GESTIONESCOLAR.Negocio.Usuario ocnUsuario = new GESTIONESCOLAR.Negocio.Usuario();
    GESTIONESCOLAR.Negocio.Instituciones ocnInstituciones = new GESTIONESCOLAR.Negocio.Instituciones();


    DataTable dtUsuario = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int IdPage = 0;
            Page.Title = System.Configuration.ConfigurationSettings.AppSettings["ClienteNombre"].ToString();
            lblClienteNombre.Text = System.Configuration.ConfigurationSettings.AppSettings["ClienteNombre"].ToString();
            if (!Page.IsPostBack)
            {
                ColegioId.DataValueField = "Valor";
                ColegioId.DataTextField = "Texto";
                ColegioId.DataSource = (new GESTIONESCOLAR.Negocio.Instituciones()).ObtenerLista("[Seleccionar...]");
                ColegioId.DataBind();

                this.lblInstitucion.Visible = false;
                IdPage = Convert.ToInt32(Request.QueryString["IdPage"]);
                if (IdPage == 1)
                {
                    ima.ImageUrl = "../images/LogoNuevoAso.jpg";
                    LinkPage.Href = "../images/LogoNuevoAso.jpg";

                }
                else
                {
                    ColegioId.SelectedIndex = 1;
                    ColegioId.Enabled = false;
                    ima.ImageUrl = "../cssFranciscana/img/logo_francis_pagina.png";
                    LinkPage.Href = "../cssFranciscana/img/logo_francis_pagina.ico";

                }
                if (Session["_Autenticado"] != null) Session.Remove("_Autenticado");
                if (Session["_usuNombreIngreso"] != null) Session.Remove("_usuNombreIngreso");
                if (Session["_usuApellido"] != null) Session.Remove("_usuApellido");
                if (Session["_usuNombre"] != null) Session.Remove("_usuNombre");
                if (Session["_usuId"] != null) Session.Remove("_usuId");
                if (Session["_perId"] != null) Session.Remove("_perId");
                if (Session["_perNombre"] != null) Session.Remove("_perNombre");
                if (Session["_perEsAdministrador"] != null) Session.Remove("_perEsAdministrador");
                if (Session["_PaginasPermitidas"] != null) Session.Remove("_PaginasPermitidas");
                if (Session["_CambiarClave"] != null) Session.Remove("_CambiarClave");
                if (Session["_Institucion"] != null) Session.Remove("_Institucion");

                #region Alto y Ancho de Logo
                GESTIONESCOLAR.Negocio.Parametro ocnParametro = new GESTIONESCOLAR.Negocio.Parametro();
                //string Logo_Alto = "150";
                //string Logo_Ancho = "300";
                //Logo_Alto = ocnParametro.ObtenerValor("Logo_Alto");
                //Logo_Ancho = ocnParametro.ObtenerValor("Logo_Ancho");
                //ima.Height = Unit.Pixel(Convert.ToInt32(Logo_Alto));
                //ima.Width = Unit.Pixel(Convert.ToInt32(Logo_Ancho));
                #endregion

                this.btnIngresar.Focus();
            }

            this.txtUsuario.Focus();
        }
        catch (Exception oError)
        {
            Response.Write("MESSAGE:<br>" + oError.Message + "<br><br>EXCEPTION:<br>" + oError.InnerException + "<br><br>TRACE:<br>" + oError.StackTrace + "<br><br>TARGET:<br>" + oError.TargetSite);
        }
    }

    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        try
        {
            int insId = Convert.ToInt32(ColegioId.SelectedValue);
            dtUsuario = GESTIONESCOLAR.Negocio.SeguridadAsociacion.Autenticar(insId, txtUsuario.Text, txtClave.Text);
            if (Convert.ToInt32(ColegioId.SelectedValue) != 0)
            {
                if (dtUsuario != null)
                {
                    if (dtUsuario.Rows.Count != 0)
                    {
                        if (dtUsuario.Rows.Count > 0)
                        {
                            if (dtUsuario.Rows[0]["insId"].ToString() == ColegioId.SelectedValue)
                            {
                                Session.Add("_Autenticado", true);
                                int usuario = Convert.ToInt32(dtUsuario.Rows[0]["usuId"].ToString());
                                Session.Add("_usuId", dtUsuario.Rows[0]["usuId"].ToString());
                                Session.Add("_usuNombreIngreso", dtUsuario.Rows[0]["usuNombreIngreso"].ToString());
                                Session.Add("_usuApellido", dtUsuario.Rows[0]["usuApellido"].ToString());
                                Session.Add("_usuNombre", dtUsuario.Rows[0]["usuNombre"].ToString());
                                Session.Add("_perId", dtUsuario.Rows[0]["perId"].ToString());
                                Session.Add("_perNombre", dtUsuario.Rows[0]["perNombre"].ToString());
                                Session.Add("_Institucion", ColegioId.SelectedValue);
                                Session.Add("_InstitucionNombre", ColegioId.SelectedItem);
                                Session.Add("_perEsAdministrador", (dtUsuario.Rows[0]["perEsAdministrador"].ToString() == "1" ? true : false));
                                Session.Add("_PaginasPermitidas", "");

                                #region Si Clave es igual a 1, obligo a cambiarla
                                ocnUsuario = new GESTIONESCOLAR.Negocio.Usuario(Convert.ToInt32(Session["_usuId"]));
                                if (ocnUsuario.usuClave == "hJsvP2MyI0P93Fo8jajwfkA07k1eshClrZ254ztq7BjeqBg2qH+SJqRjbGx3iTsL00CPbR/iJbsJB8VWqBETVQ==")
                                {
                                    Session.Add("_CambiarClave", "1");
                                    Response.Redirect("UsuarioCambiarClave.aspx", false);
                                }
                                else
                                {
                                  
                                        Session.Add("_CambiarClave", "0");
                                        Response.Redirect("SesionPadres.aspx", false);
                                    
                                }
                                #endregion
                                //int per = Convert.ToInt32(dtUsuario.Rows[0]["perId"].ToString());
                                //if (Convert.ToInt32(dtUsuario.Rows[0]["perId"].ToString()) == 14)
                                //{

                                //    Response.Redirect("../PaginasGenerales/Preinscripcion.aspx", false);
                                //}


                            }
                            else
                            {
                                this.lblErrorInstitucion.Visible = true;
                                return;

                            }
                        }
                        else
                        {
                            UsuarioNoValido();
                        }
                    }
                    else
                    {
                        UsuarioNoValido();
                    }
                }
                else
                {
                    UsuarioNoValido();
                }
            }
            else
            {
                this.lblInstitucion.Visible = true;
                return;
            }
        }
        catch (Exception oError)
        {
            Response.Write("MESSAGE:<br>" + oError.Message + "<br><br>EXCEPTION:<br>" + oError.InnerException + "<br><br>TRACE:<br>" + oError.StackTrace + "<br><br>TARGET:<br>" + oError.TargetSite);
        }
    }

    private void UsuarioNoValido()
    {
        try
        {
            this.lblUsuarioNoValido.Visible = true;

            this.txtUsuario.Text = "";
            this.txtClave.Text = "";
        }
        catch (Exception oError)
        {
            Response.Write("MESSAGE:<br>" + oError.Message + "<br><br>EXCEPTION:<br>" + oError.InnerException + "<br><br>TRACE:<br>" + oError.StackTrace + "<br><br>TARGET:<br>" + oError.TargetSite);
        }
    }

    protected void ColegioId_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}