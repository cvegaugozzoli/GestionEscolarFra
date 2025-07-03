using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class InscripcionExamenRegistracion : System.Web.UI.Page
{
    GESTIONESCOLAR.Negocio.InscripcionExamen ocnInscripcionExamen = new GESTIONESCOLAR.Negocio.InscripcionExamen();
    GESTIONESCOLAR.Negocio.RegistracionCalificaciones ocnRegistracionCalificaciones = new GESTIONESCOLAR.Negocio.RegistracionCalificaciones();
    GESTIONESCOLAR.Negocio.InscripcionCursado ocnInscripcionCursado = new GESTIONESCOLAR.Negocio.InscripcionCursado();
    GESTIONESCOLAR.Negocio.EspacioCurricular ocnEspacioCurricular = new GESTIONESCOLAR.Negocio.EspacioCurricular();
    GESTIONESCOLAR.Negocio.PlanEstudio ocnPlanEstudio = new GESTIONESCOLAR.Negocio.PlanEstudio();
    GESTIONESCOLAR.Negocio.Curso ocnCurso = new GESTIONESCOLAR.Negocio.Curso();
    GESTIONESCOLAR.Negocio.Campo ocnCampo = new GESTIONESCOLAR.Negocio.Campo();
    GESTIONESCOLAR.Negocio.Alumno ocnAlumno = new GESTIONESCOLAR.Negocio.Alumno();
    GESTIONESCOLAR.Negocio.FormatoDictado ocnFormatoDictado = new GESTIONESCOLAR.Negocio.FormatoDictado();
    GESTIONESCOLAR.Negocio.InscripcionExamenTipo ocnInscripcionExamenTipo = new GESTIONESCOLAR.Negocio.InscripcionExamenTipo();
    int insId;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                insId = Convert.ToInt32(Session["_Institucion"]);
                this.Master.TituloDelFormulario = " Inscripcion Examen - Registracion";

				//if (this.Session["_Autenticado"] == null) Response.Redirect("~/PaginasBasicas/Login.aspx", true);

                if (Request.QueryString["Ver"] != null)
                {
                    btnAceptar1.Visible = false; 
                    btnAceptar1.Visible = false;
                }
                fodId.DataValueField = "Valor"; fodId.DataTextField = "Texto"; fodId.DataSource = (new GESTIONESCOLAR.Negocio.FormatoDictado()).ObtenerLista("[Seleccionar...]"); fodId.DataBind();
                extId.DataValueField = "Valor"; extId.DataTextField = "Texto"; extId.DataSource = (new GESTIONESCOLAR.Negocio.ExamenTipo()).ObtenerLista("[Seleccionar...]"); extId.DataBind();
                itpId.DataValueField = "Valor"; itpId.DataTextField = "Texto"; itpId.DataSource = (new GESTIONESCOLAR.Negocio.InscripcionExamenTipo()).ObtenerLista("[Seleccionar...]"); itpId.DataBind();

                int Id = 0;
                if (Request.QueryString["Id"] != null)
                {
                    Id = Convert.ToInt32(Request.QueryString["Id"]);
					/*INCIALIZADORES*/
                    if (Id != 0)
                    {
                        carId.DataValueField = "Valor"; carId.DataTextField = "Texto"; carId.DataSource = (new GESTIONESCOLAR.Negocio.Carrera()).ObtenerLista("[Seleccionar...]"); carId.DataBind();
                        plaId.DataValueField = "Valor"; plaId.DataTextField = "Texto"; plaId.DataSource = (new GESTIONESCOLAR.Negocio.PlanEstudio()).ObtenerLista("[Seleccionar...]"); plaId.DataBind();
                        curId.DataValueField = "Valor"; curId.DataTextField = "Texto"; curId.DataSource = (new GESTIONESCOLAR.Negocio.Curso()).ObtenerLista("[Seleccionar...]"); curId.DataBind();
                        camId.DataValueField = "Valor"; camId.DataTextField = "Texto"; camId.DataSource = (new GESTIONESCOLAR.Negocio.Campo()).ObtenerLista("[Seleccionar...]"); camId.DataBind();
                        escId.DataValueField = "Valor"; escId.DataTextField = "Texto"; escId.DataSource = (new GESTIONESCOLAR.Negocio.EspacioCurricular()).ObtenerLista("[Seleccionar...]",insId); escId.DataBind();

                        DataTable dt = new DataTable();
                        dt = ocnInscripcionExamen.ObtenerUnoPorAlumno(Id);
                        if (dt.Rows.Count > 0)
                        {
                            //ocnInscripcionExamen = new GESTIONESCOLAR.Negocio.InscripcionExamen(Id);
                            this.ixaFechaExamen.Text = Convert.ToDateTime(dt.Rows[0]["ixaFechaExamen"].ToString()); //   ocnInscripcionExamen.ixaFechaExamen;
                            Decimal calificacion = 0;
                            calificacion = Convert.ToDecimal(dt.Rows[0]["Calificacion"].ToString());   // FuncionesUtiles.StringToDecimal(
                            this.ixaCalificacion.Text = FuncionesUtiles.DecimalToString(calificacion); // FuncionesUtiles.DecimalToString(ocnInscripcionExamen.ixaCalificacion);
                            this.ixaNumeroActa.Text = dt.Rows[0]["ixaNumeroActa"].ToString();  // ocnInscripcionExamen.ixaNumeroActa.ToString();
                            this.ixaNumeroLibro.Text = dt.Rows[0]["ixaNumeroLibro"].ToString();  
                            //this.icuActivo.Checked = ocnInscripcionCursado.icuActivo;
                            if (dt.Rows[0]["ixaActivo"].ToString() == "1" )
                            {
                                this.ixaActivo.Checked = true;  //  ocnInscripcionExamen.ixaActivo;
                            }
                            else
                            {
                                this.ixaActivo.Checked = false;  //  ocnInscripcionExamen.ixaActivo;
                            }
                            this.carId.SelectedValue = dt.Rows[0]["carId"].ToString();  // (ocnInscripcionExamen.carId == 0 ? "" : ocnInscripcionExamen.carId.ToString());
                            this.plaId.SelectedValue = dt.Rows[0]["plaId"].ToString();  // (ocnInscripcionExamen.plaId == 0 ? "" : ocnInscripcionExamen.plaId.ToString());
                            this.curId.SelectedValue = dt.Rows[0]["curId"].ToString();  //  (ocnInscripcionExamen.curId == 0 ? "" : ocnInscripcionExamen.curId.ToString());
                            this.camId.SelectedValue = dt.Rows[0]["camId"].ToString();  //  (ocnInscripcionExamen.camId == 0 ? "" : ocnInscripcionExamen.camId.ToString());
                            this.escId.SelectedValue = dt.Rows[0]["escId"].ToString();  //  (ocnInscripcionExamen.escId == 0 ? "" : ocnInscripcionExamen.escId.ToString());
                            this.extId.SelectedValue = dt.Rows[0]["extId"].ToString();  // (ocnInscripcionExamen.extId == 0 ? "" : ocnInscripcionExamen.extId.ToString());
                            this.itpId.SelectedValue = dt.Rows[0]["itpId"].ToString();  // (ocnInscripcionExamen.extId == 0 ? "" : ocnInscripcionExamen.extId.ToString());

                            //this.fodId.SelectedValue = (ocnInscripcionExamen.fodId == 0 ? "" : ocnInscripcionExamen.fodId.ToString());
                            this.aluId.Text = dt.Rows[0]["aluId"].ToString();  //  ocnInscripcionExamen.aluId.ToString();
                            this.aluNombre.Text = dt.Rows[0]["Alumno"].ToString();
                            this.fodId.SelectedValue = dt.Rows[0]["fodId"].ToString();
                            this.aludni.Text = dt.Rows[0]["AluDNI"].ToString();
                            this.aluLegajoNumero.Text = dt.Rows[0]["AluLegNro"].ToString();
                            this.carId.Enabled = false;
                            this.plaId.Enabled = false;
                            this.curId.Enabled = false;
                            this.camId.Enabled = false;
                            this.escId.Enabled = false;
                            this.extId.Enabled = false;
                            this.fodId.Enabled = false;
                            this.aludni.Enabled = false;
                            this.aluNombre.Enabled = false;
                            this.aluLegajoNumero.Enabled = false;
                            this.itpId.Enabled = false;

                            btnBuscar.Enabled = false;
                            btnCancelarAlumno.Enabled = false;
                            this.ixaCalificacion.Focus();

                        }
                        /*Editar Habilitado*/
                    }
                    else
                    {
                        ixaFechaExamen.Text = DateTime.Now;
                        /*Nuevo Habilitado*/
                        /*cLoadNuevoCustom*/
                        this.aluId.Focus();
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
		
	protected void btnCancelar_Click(object sender, EventArgs e)
	{
        try
        {
            Response.Redirect("InscripcionExamenConsulta.aspx", true);
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
        DataTable dt = new DataTable();
        dt = ocnAlumno.ObtenerUnoporDoc( aludni.Text.Trim().ToString());
        if (dt.Rows.Count > 0)
        {
            aluNombre.Text = dt.Rows[0]["aluNombre"].ToString();
            aluNombre.Enabled = false;
            aluLegajoNumero.Text = dt.Rows[0]["aluLegajoNumero"].ToString();
            aluLegajoNumero.Enabled = false;
            aluId.Text = dt.Rows[0]["aluId"].ToString();

            //Cargo el combo de Carreras donde esta inscripto
            carId.DataValueField = "Valor";
            carId.DataTextField = "Texto";
            carId.DataSource = (new GESTIONESCOLAR.Negocio.Carrera()).ObtenerListaporAlumno("[Seleccionar...]", Convert.ToInt32(aluId.Text.ToString()));
            carId.DataBind();

        }
    }


    protected void btnCancelarAlumno_Click(object sender, EventArgs e)
    {
        aluNombre.Text = "";
        aluNombre.Enabled = false;
        aluLegajoNumero.Text = "";
        aluLegajoNumero.Enabled = false;
        aludni.Text = "";
        btnBuscar.Enabled = true;
        //carId.SelectedValue = "0";
        //plaId.SelectedValue = "0";
        //curId.SelectedValue = "0";
        //camId.SelectedValue = "0";
        //escId.SelectedValue = "0";
        aludni.Focus();
    }



    protected void btnAceptar_Click(object sender, EventArgs e)
	{
        DataTable dt2 = new DataTable();
        try
        {
		    int Id = 0;
		    if (Request.QueryString["Id"] != null)
		    {
			    Id = Convert.ToInt32(Request.QueryString["Id"]);
			    ocnInscripcionExamen = new GESTIONESCOLAR.Negocio.InscripcionExamen(Id);

			    ocnInscripcionExamen.ixaFechaExamen = Convert.ToDateTime(ixaFechaExamen.Text); 
                if (ixaCalificacion.Text.Trim() == "")
                {
                    ocnInscripcionExamen.ixaCalificacion = 0;
                } else
                {
                    ocnInscripcionExamen.ixaCalificacion = FuncionesUtiles.StringToDecimal(ixaCalificacion.Text);
                }
			    if (ixaNumeroActa.Text.Trim() == "")
                {
                    ocnInscripcionExamen.ixaNumeroActa = 0;
                } else
                {
                    ocnInscripcionExamen.ixaNumeroActa = Convert.ToInt32(ixaNumeroActa.Text);
                }

                if (ixaNumeroLibro.Text.Trim() == "")
                {
                    ocnInscripcionExamen.ixaNumeroLibro = "";
                }
                else
                {
                    ocnInscripcionExamen.ixaNumeroLibro = ixaNumeroLibro.Text;
                }



                ocnInscripcionExamen.aluId = Convert.ToInt32(aluId.Text);
                ocnInscripcionExamen.ixaActivo = ixaActivo.Checked; 
				ocnInscripcionExamen.carId = Convert.ToInt32((carId.SelectedValue.Trim() == "" ? "0" : carId.SelectedValue)); 
				ocnInscripcionExamen.plaId = Convert.ToInt32((plaId.SelectedValue.Trim() == "" ? "0" : plaId.SelectedValue)); 
				ocnInscripcionExamen.curId = Convert.ToInt32((curId.SelectedValue.Trim() == "" ? "0" : curId.SelectedValue)); 
				ocnInscripcionExamen.camId = Convert.ToInt32((camId.SelectedValue.Trim() == "" ? "0" : camId.SelectedValue)); 
				ocnInscripcionExamen.escId = Convert.ToInt32((escId.SelectedValue.Trim() == "" ? "0" : escId.SelectedValue)); 
				ocnInscripcionExamen.extId = Convert.ToInt32((extId.SelectedValue.Trim() == "" ? "0" : extId.SelectedValue));
                ocnInscripcionExamen.itpId = Convert.ToInt32((itpId.SelectedValue.Trim() == "" ? "0" : itpId.SelectedValue));
                /*....usuId = this.Master.usuId;*/

                ocnInscripcionExamen.ixaFechaHoraCreacion = DateTime.Now;
				ocnInscripcionExamen.ixaFechaHoraUltimaModificacion = DateTime.Now;
				ocnInscripcionExamen.usuIdCreacion = this.Master.usuId;
				ocnInscripcionExamen.usuIdUltimaModificacion = this.Master.usuId;

                /*Validaciones*/
			    string MensajeValidacion = "";
                Decimal calificacion = 0;

                if (MensajeValidacion.Trim().Length == 0)
			    {
				    if (Id == 0)
				    {
					    //Nuevo
					    ocnInscripcionExamen.Insertar();
                        // Si la calificación es mayor que 6

                        if (ocnInscripcionExamen.ixaCalificacion >= 6)
                        {
                            calificacion = ocnInscripcionExamen.ixaCalificacion;
                        }
                        dt2 = ocnInscripcionCursado.ObtenerUnoporAlumnoporEspacioCurricular(Convert.ToInt32(aluId.Text.Trim().ToString()), Convert.ToInt32(escId.SelectedValue.Trim()));
                        if (dt2.Rows.Count > 0)
                        {
                            ocnRegistracionCalificaciones.ActualizarCalificacionExamen(Int32.Parse(dt2.Rows[0]["icuId"].ToString()), calificacion);
                        }


                    }
                    else
				    {
					    //Editar
				        ocnInscripcionExamen.ixaFechaHoraUltimaModificacion = DateTime.Now;
				        ocnInscripcionExamen.usuIdUltimaModificacion = this.Master.usuId;
					    ocnInscripcionExamen.Actualizar();

                        if (ocnInscripcionExamen.ixaCalificacion >= 6)
                        {
                            calificacion = ocnInscripcionExamen.ixaCalificacion;
                        }
                        dt2 = ocnInscripcionCursado.ObtenerUnoporAlumnoporEspacioCurricular(Convert.ToInt32(aluId.Text.Trim().ToString()), Convert.ToInt32(escId.SelectedValue.Trim()));
                        if (dt2.Rows.Count > 0)
                        {                            
                            ocnRegistracionCalificaciones.ActualizarCalificacionExamen(Int32.Parse(dt2.Rows[0]["icuId"].ToString()), calificacion);
                        }
                        

                    }

                    Response.Redirect("InscripcionExamenConsulta.aspx", true);
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
<a class=""alert-link"" href=""#"">Error de Sistema</a><br/>
Se ha producido el siguiente error:<br/>
MESSAGE:<br>" + oError.Message + "<br><br>EXCEPTION:<br>" + oError.InnerException + "<br><br>TRACE:<br>" + oError.StackTrace + "<br><br>TARGET:<br>" + oError.TargetSite +
"</div>";
		}
	}






    protected void escId_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (escId.SelectedIndex != 0)
        {
            insId = Convert.ToInt32(Session["_Institucion"]);
            DataTable dt = new DataTable();
            dt = ocnInscripcionCursado.ObtenerUno(Convert.ToInt32(escId.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                this.icuId.Text = dt.Rows[0]["icuId"].ToString();
                this.fodId.SelectedValue = dt.Rows[0]["fodId"].ToString();
                this.fodId.Enabled = false;

                btnAceptar1.Enabled = true;
                int ControlNoAprobado = 0;
                int ControlExiste = 0;
                ///Controlo que el alumno no se encuentre inscripto en el examen de ese espacio curricular en esa fecha de examen
                DataTable dt3 = new DataTable();
                dt3 = ocnInscripcionExamen.ObtenerUnoControlExiste(Convert.ToInt32(aluId.Text.Trim().ToString()), Convert.ToInt32(escId.SelectedValue), ixaFechaExamen.Text);
                if (dt3.Rows.Count > 0)
                {
                    ControlExiste = 1;
                }

                string MensajeValidacion = "";
                //Controlo Correlaticas
                if (ControlExiste == 0)
                {
                    //Traer Correlativas para RENDIR del Espacio Curricular que estoy inscribiendo 
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    dt1 = ocnEspacioCurricular.ObtenerCorrelativas(Convert.ToInt32(escId.SelectedValue), 3,insId);
                    if (dt1.Rows.Count > 0)
                    {
                        // Por Cada Correlativa para RENDIR controlo que exista en InscripcionExamen cargada con el atributo Aprobado.
                        foreach (DataRow row in dt1.Rows)
                        {
                            //dt2 = ocnInscripcionExamen.ObtenerUnoporAprobado(Convert.ToInt32(aluId.Text.Trim().ToString()), Convert.ToInt32(dt1.Rows[0]["Id"].ToString()));
                            dt2 = ocnInscripcionCursado.ObtenerUnoporAprobadooPromocionado(Convert.ToInt32(aluId.Text.Trim().ToString()), Convert.ToInt32(dt1.Rows[0]["Id"].ToString()));
                            if (dt2.Rows.Count == 0)
                            {
                                ControlNoAprobado = 1;
                            } 
                        }
                        if (ControlNoAprobado == 1)
                        {
                            Response.Write("MENSAJE DE ERROR:<br>" + MensajeValidacion);
                            lblMensajeError.Text = @"<div class=""alert alert-warning alert-dismissable"">
                            <button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
                            <a class=""alert-link"" href=""#"">El alumno no posee las asignaturas correlativas aprobadas necesarias para rendir..<br/>
                            Verifique!!</a><br/>" + MensajeValidacion + "</div>";
                            btnAceptar1.Enabled = false;
                        }
                    }
                    //else
                    //{
                    //    Response.Write("MENSAJE DE ERROR:<br>" + MensajeValidacion);
                    //    lblMensajeError.Text = @"<div class=""alert alert-warning alert-dismissable"">
                    //    <button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
                    //    <a class=""alert-link"" href=""#"">El alumno no posee las asignaturas correlativas cargadas o no tiene correlativas..<br/>
                    //    Verifique!!</a><br/>" + MensajeValidacion + "</div>";
                    //    btnAceptar1.Enabled = false;
                    //}

                } else
                {
                Response.Write("MENSAJE DE ERROR:<br>" + MensajeValidacion);
                lblMensajeError.Text = @"<div class=""alert alert-warning alert-dismissable"">
                        <button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
                        <a class=""alert-link"" href=""#"">El alumno ya se encuentra inscripto para rendir en la asignatura seleccionada..<br/>
                        Verifique!!</a><br/>" + MensajeValidacion + "</div>";
                 btnAceptar1.Enabled = false;
                }



            }
        }
    }






    protected void carId_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.itpId.SelectedIndex == 0)
        {
            Response.Write("MENSAJE DE ERROR:<br>");
            lblMensajeError.Text = @"<div class=""alert alert-warning alert-dismissable"">
                    <button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
                    <a class=""alert-link"" href=""#"">Seleccione Tipo de Inscripción de Examen: Regular o Libre ..<br/>
                    Verifique!!</a><br/></div>";
            this.itpId.Focus();

        }
        else
        {

            if (carId.SelectedIndex != 0)
            {

                //ClubB.Negocio.Evento ocnEvento = new ClubB.Negocio.Evento();
                DataTable dt = new DataTable();
                dt = ocnPlanEstudio.ObtenerListaPorUnaCarreraporAlumno("[Seleccionar...]", Convert.ToInt32(carId.SelectedValue), Convert.ToInt32(aluId.Text.ToString()));
                if (dt.Rows.Count > 0)
                {
                    plaId.DataValueField = "Valor";
                    plaId.DataTextField = "Texto";
                    plaId.DataSource = (new GESTIONESCOLAR.Negocio.PlanEstudio()).ObtenerListaPorUnaCarreraporAlumno("[Seleccionar...]", Convert.ToInt32(carId.SelectedValue), Convert.ToInt32(aluId.Text.ToString()));
                    plaId.DataBind();
                }

            }
            else
            {
                plaId.SelectedValue = "0";
            }
            curId.SelectedValue = "0";
            camId.SelectedValue = "0";
            escId.SelectedValue = "0";
            fodId.SelectedValue = "0";
        }
    }


    protected void plaId_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ClubB.Negocio.Evento ocnEvento = new ClubB.Negocio.Evento();
        DataTable dt = new DataTable();

        if (plaId.SelectedIndex != 0)
        {
            if (this.itpId.SelectedIndex == 1)
            {
                dt = ocnCurso.ObtenerListaPorUnPlanEstudio("[Seleccionar...]", Convert.ToInt32(plaId.SelectedValue));
            }
            else
            {
                dt = ocnCurso.ObtenerListaPorUnPlanEstudioporAlumno("[Seleccionar...]", Convert.ToInt32(plaId.SelectedValue), Convert.ToInt32(aluId.Text.ToString()));
            }
            if (dt.Rows.Count > 0)
            {
                curId.DataValueField = "Valor";
                curId.DataTextField = "Texto";
                if (this.itpId.SelectedIndex == 1)
                {
                    curId.DataSource = (new GESTIONESCOLAR.Negocio.Curso()).ObtenerListaPorUnPlanEstudio("[Seleccionar...]", Convert.ToInt32(plaId.SelectedValue));
                } 
                else
                {
                    curId.DataSource = (new GESTIONESCOLAR.Negocio.Curso()).ObtenerListaPorUnPlanEstudioporAlumno("[Seleccionar...]", Convert.ToInt32(plaId.SelectedValue), Convert.ToInt32(aluId.Text.ToString()));
                }
                curId.DataBind();
            }
        } else
        {
            curId.SelectedValue = "0";
        }
        camId.SelectedValue = "0";
        escId.SelectedValue = "0";
        fodId.SelectedValue = "0";
    }

    protected void curId_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (curId.SelectedIndex != 0)
        {

            //ClubB.Negocio.Evento ocnEvento = new ClubB.Negocio.Evento();
            DataTable dt = new DataTable();
            if (this.itpId.SelectedIndex == 1)
            {
                dt = ocnCampo.ObtenerListaPorUnCurso("[Seleccionar...]", Convert.ToInt32(curId.SelectedValue));
            } else
            {
                dt = ocnCampo.ObtenerListaPorUnCursoporAlumno("[Seleccionar...]", Convert.ToInt32(curId.SelectedValue), Convert.ToInt32(aluId.Text.ToString()));
            }
            if (dt.Rows.Count > 0)
            {
                camId.DataValueField = "Valor";
                camId.DataTextField = "Texto";
                if (this.itpId.SelectedIndex == 1)
                {
                    camId.DataSource = (new GESTIONESCOLAR.Negocio.Campo()).ObtenerListaPorUnCurso("[Seleccionar...]", Convert.ToInt32(curId.SelectedValue));
                }
                else
                {
                    camId.DataSource = (new GESTIONESCOLAR.Negocio.Campo()).ObtenerListaPorUnCursoporAlumno("[Seleccionar...]", Convert.ToInt32(curId.SelectedValue), Convert.ToInt32(aluId.Text.ToString()));
                }
                camId.DataBind();
            }
        } else
        {
            camId.SelectedValue = "0";
        }
        escId.SelectedValue = "0";
        fodId.SelectedValue = "0";
    }


    protected void camId_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (camId.SelectedIndex != 0)
        {

            //ClubB.Negocio.Evento ocnEvento = new ClubB.Negocio.Evento();
            DataTable dt = new DataTable();
            if (this.itpId.SelectedIndex == 1)
            {
                dt = ocnEspacioCurricular.ObtenerListaPorUnCampo("[Seleccionar...]", Convert.ToInt32(camId.SelectedValue),insId);
            } else
            {
                dt = ocnEspacioCurricular.ObtenerListaPorUnCampoporAlumno("[Seleccionar...]", Convert.ToInt32(camId.SelectedValue), Convert.ToInt32(aluId.Text.ToString()),insId);
            }
            if (dt.Rows.Count > 0)
            {
                escId.DataValueField = "Valor";
                escId.DataTextField = "Texto";
                if (this.itpId.SelectedIndex == 1)
                {
                    escId.DataSource = (new GESTIONESCOLAR.Negocio.EspacioCurricular()).ObtenerListaPorUnCampo("[Seleccionar...]", Convert.ToInt32(camId.SelectedValue),insId);
                } else
                {
                    escId.DataSource = (new GESTIONESCOLAR.Negocio.EspacioCurricular()).ObtenerListaPorUnCampoporAlumno("[Seleccionar...]", Convert.ToInt32(camId.SelectedValue), Convert.ToInt32(aluId.Text.ToString()),insId);
                }
                escId.DataBind();
            }
        } else
        {
            escId.SelectedValue = "0";
        }
        fodId.SelectedValue = "0";
    }







    protected void itpId_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMensajeError.Text = "";
        carId.SelectedValue = "0";
        plaId.SelectedValue = "0";
        curId.SelectedValue = "0";
        camId.SelectedValue = "0";
        escId.SelectedValue = "0";
        fodId.SelectedValue = "0";

    }

}