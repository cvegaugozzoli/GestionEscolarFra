using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Net;

public partial class GenerarArchivoBanco : System.Web.UI.Page
{
    GESTIONESCOLAR.Negocio.Usuario ocnUsuario = new GESTIONESCOLAR.Negocio.Usuario();
    GESTIONESCOLAR.Negocio.InscripcionConcepto ocnInscripcionConcepto = new GESTIONESCOLAR.Negocio.InscripcionConcepto();
    GESTIONESCOLAR.Negocio.ConceptosIntereses ocnConceptosIntereses = new GESTIONESCOLAR.Negocio.ConceptosIntereses();
    GESTIONESCOLAR.Negocio.ArchivoBanco ocnArchivoBanco = new GESTIONESCOLAR.Negocio.ArchivoBanco();
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    DataTable dt2 = new DataTable();
    int insId;
    protected void Page_Load(object sender, EventArgs e)
    {
        //imgUpdateProgress.Visible = false;
        try
        {
            if (!Page.IsPostBack)
            {

                Mje.Visible=false;
                this.Master.TituloDelFormulario = "Generar archivo Banco";
                int usuario = Convert.ToInt32(Session["_usuId"].ToString());
                dt = ocnUsuario.ObtenerUno(usuario);
                this.conAnioLectivo.Text = DateTime.Now.ToString("yyyy");
                txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtHasta.Text = "31/12/" + DateTime.Now.ToString("yyyy");
                insId = Convert.ToInt32(Session["_Institucion"]);
                lblInsId.Text = Convert.ToString(Session["_Institucion"]);
                //ddlInstitucion.DataValueField = "Valor"; ddlInstitucion.DataTextField = "Texto"; ddlInstitucion.DataSource = (new GESTIONESCOLAR.Negocio.Instituciones()).ObtenerLista("[Seleccionar...]"); ddlInstitucion.DataBind();
                //ColegioId.DataValueField = "Valor"; ColegioId.DataTextField = "Texto"; ColegioId.DataSource = (new GESTIONESCOLAR.Negocio.Instituciones()).ObtenerLista("[Seleccionar...]"); ColegioId.DataBind();
                cntId.DataValueField = "Valor";
                cntId.DataTextField = "Texto";
                cntId.DataSource = (new GESTIONESCOLAR.Negocio.ConceptosTipos()).ObtenerLista("[Seleccionar...]");
                cntId.DataBind();
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
    
    protected void btnDescargar_Click(object sender, EventArgs e)
    {

        string ruta;
    
        ruta = Server.MapPath("../PaginasGenerales/ArchivosCaja/" + lbLNombreArchivo.Text);
        String prueba;
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "txt";
        prueba = Path.GetFileName(ruta).ToString();
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + prueba);
        HttpContext.Current.Response.TransmitFile(ruta);
        HttpContext.Current.Response.End();



        //string _open = "window.open('ftp://madremercedesguerra.com.ar/public_html/PaginasGenerales/ArchivosCaja/', '_newtab');";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

        //FtpWebRequest Request = (FtpWebRequest)WebRequest.Create("ftp://obramisericordista.com.ar/");
        //Request.Method = WebRequestMethods.Ftp.ListDirectory;
        //Request.Credentials = new NetworkCredential("mmguerra", "MaMercedes445");
        //FtpWebResponse Response = (FtpWebResponse)Request.GetResponse();
        //Stream ResponseStream = Response.GetResponseStream();
        //StreamReader Reader = new StreamReader(ResponseStream);

        //var ListBox1 = new List<string>();
        ////ListBox1.Items.Add(Response.WelcomeMessage);
        //ListBox1.Add(Response.WelcomeMessage);
        //while (!Reader.EndOfStream)//Read file name   
        //{
        //    //ListBox1.Items.Add(Reader.ReadLine().ToString());
        //    ListBox1.Add(Reader.ReadLine().ToString());
        //}
        //Response.Close();
        //ResponseStream.Close();
        //Reader.Close();
    }



protected void btnAceptar_Click(object sender, EventArgs e)
    {
        lbLNombreArchivo.Text= "";
        Mje.Visible = false;
        btnDescargar.Visible = true;
        //imgUpdateProgress.Visible = true;
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
        //Decimal[] ImpMes;
        //ImpMes = new Decimal[3];

        float[] ImpMes;
        ImpMes = new float[3];


        String[] AplicaBeca;
        AplicaBeca = new String[3];
        String[] AplicaInteresAbierto;
        AplicaInteresAbierto = new String[3];

        String ApellidoyNombre;
        String Cuerpo, barra, DiasVtoEntreUnoyDos;

        ocnArchivoBanco.Eliminar();

        Int32 TotRegistros;

        dt1 = ocnInscripcionConcepto.ArchivoBancoObtenerporVarios(insId, Convert.ToInt32(cntId.SelectedValue), Convert.ToInt32(conAnioLectivo.Text));
        if (dt1.Rows.Count > 0)
        {

            String NombreArchivo = dt1.Rows[0]["InsCodigo"].ToString().PadLeft(4, '0') + DateTime.Now.ToString("MMyyyy") + ".txt";
            lbLNombreArchivo.Text = NombreArchivo;
            String Cabecera;
            Cabecera = dt1.Rows[0]["InsCodigo"].ToString().PadLeft(4, '0') + DateTime.Now.ToString("ddMMyy"); 
            ocnArchivoBanco.Cuerpo = Cabecera;
            ocnArchivoBanco.InsertarCuerpo();

            TotRegistros = dt1.Rows.Count + 2;

            //Decimal intMensual = '0';
            Single intMensual = '0';
            //Single ImpConBecaConInteresMensualCalculado = '0';
            //Single interesMensualCalculado = '0';
            Single ImpConBeca = '0';
            int NroCuota = 0;
            int NroCuotaSegunFechaEmision = 0;
            int MultiplicadorCuotas = 0;
            int CantVtos = 0;
            foreach (DataRow row in dt1.Rows)
            {
                // Cambio 09042025
                //NroCuota = Convert.ToInt32(row["icoNroCuota"].ToString());
                //NroCuotaSegunFechaEmision = Convert.ToInt32(txtDesde.Text.ToString().Substring(3, 2)) - 3;
                //if (NroCuotaSegunFechaEmision <= 0)
                //{
                //    NroCuotaSegunFechaEmision = 1;
                //}
                //MultiplicadorCuotas = (NroCuotaSegunFechaEmision + 1) - NroCuota;
                //

                NroCuota = Convert.ToInt32(row["icoNroCuota"].ToString());
                NroCuotaSegunFechaEmision = Convert.ToInt32(txtDesde.Text.ToString().Substring(3, 2)) - Convert.ToInt32(row["conMesInicio"].ToString());
                if (NroCuotaSegunFechaEmision <= 0)
                {
                    NroCuotaSegunFechaEmision = 1;
                }
                //MultiplicadorCuotas = (NroCuota +1) - NroCuotaSegunFechaEmision;
                MultiplicadorCuotas = (NroCuotaSegunFechaEmision + 1) - NroCuota;
                if (MultiplicadorCuotas < 0)
                {
                    MultiplicadorCuotas = 1;
                }



                //ImpConBecaConInteresMensualCalculado = Convert.ToSingle(row["ImpConBecaConInteresMensualCalculado"].ToString());
                intMensual = Convert.ToSingle(row["ConInteresMensual"].ToString());
                //interesMensualCalculado = Convert.ToSingle(row["InteresMensualCalculado"].ToString());
                ImpConBeca = Convert.ToSingle(row["ImpConBeca"].ToString());
                CantVtos = Convert.ToInt32(row["conCantVtos"].ToString());


                Cuotas[0] = row["IcoNroCuota"].ToString();
                Cuotas[1] = row["conMesInicio"].ToString();
                Cuotas[2] = String.Format(row["IcoNroCuota"].ToString(), "00");
                Int32 C = 0;

                String pok = "0";
                String pdni = row["aluDoc"].ToString();
                if (pdni == "57441092") // || pdni == "45659869"
                {
                    pok = "1";
                }
                Session.Add("aluDoc", pdni);

                AplicaBeca[0] = "";
                AplicaBeca[1] = "";
                AplicaBeca[2] = "";
                AplicaInteresAbierto[0] = "";
                AplicaInteresAbierto[1] = "";
                AplicaInteresAbierto[2] = "";
                Vtos[0] = "";
                Vtos[1] = "";
                Vtos[2] = "";
                ImpMes[0] = 0;
                ImpMes[1] = 0;
                ImpMes[2] = 0;
                //if (CantVtos == 2 )
                //{
                //    Vtos[2] = txtHasta.Text.Trim();
                //}

                dt2 = ocnConceptosIntereses.ObtenerListaxconIdxNroCuota(Convert.ToInt32(row["conId"].ToString()), (Convert.ToInt32(row["IcoNroCuota"].ToString())));
                if (dt2.Rows.Count > 0)
                {
                    foreach (DataRow row2 in dt2.Rows)
                    {
                        AplicaBeca[C] = row2["coiAplicaBeca"].ToString();
                        AplicaInteresAbierto[C] = row2["coiAplicaInteresAbierto"].ToString();

                        Vtos[C] = row2["FechaVto"].ToString();
                        Recargo += Convert.ToSingle(row2["coiValorInteres"].ToString());
                        if (row2["coiAplicaBeca"].ToString() == "1")
                        {
                            ImpMes[C] = Convert.ToSingle(row["ImpConBeca"].ToString()) + Convert.ToSingle(row2["coiValorInteres"].ToString());
                        } else
                        {
                            ImpMes[C] = Convert.ToSingle(row["icoImporte"].ToString()) + Convert.ToSingle(row2["coiValorInteres"].ToString());
                        }
                        C += 1;
                    }
                } else
                {
                    int ppppp = 1;
                }
                if (C == 2 && Vtos[2] == "")  // Si solo tiene 2 Vtos definidos pongo como 3 Vto al 2 Vto para evaluar más abajo si aplica interés mensual o no
                {
                    Vtos[2] = Vtos[1];
                }
                if (row["conTieneVtoAbierto"].ToString() == "1")
                {
                    if (AplicaInteresAbierto[C-1] == "0")
                    {
                        if (AplicaBeca[C-1] == "1")
                        {
                            Recargo = Convert.ToSingle(row["ImpConBeca"].ToString());
                            ImpMes[C] = Convert.ToSingle(row["ImpConBeca"].ToString());
                        }
                        else
                        {
                            Recargo = Convert.ToSingle(row["icoImporte"].ToString());
                            ImpMes[C] = Convert.ToSingle(row["icoImporte"].ToString());
                        }
                    }
                    else
                    {
                        Recargo = Convert.ToSingle(row["icoImporte"].ToString()) + Convert.ToSingle(row["conRecargoVtoAbierto"].ToString());
                        ImpMes[C] = Convert.ToSingle(row["icoImporte"].ToString()) + Convert.ToSingle(row["conRecargoVtoAbierto"].ToString());
                    }
                } else
                {
                    // Modificado 08/02/2024
                    if (C < 3 && C > 0)
                    {
                        //string qq = pdni;
                        ImpMes[C] = ImpMes[C - 1];
                    }
                    //Modificado 08/02/2024
                    // Si tiene interés mensual y la fecha de generación del archivo es mayor que la del último vto, entonces, pongo en el importe
                    // de esa cuota, en el último vto, el importe de la cuota + el interés mensual
                    if (intMensual > 0 && Convert.ToDateTime(txtDesde.Text.ToString()) > Convert.ToDateTime(Vtos[2].ToString()) && CantVtos == 2)
                    {
                        // Modificado 26-03-2025
                        //float impmes2 = (((ImpMes[1] * intMensual) / 100) * MultiplicadorCuotas) + ImpMes[1];
                        //ImpMes[2] = (float)Math.Round(impmes2, 2);
                        //Vtos[2] = txtHasta.Text;
                        ////

                        // Agregado 26-03-2025
                        Single ImporteConcepto = Convert.ToSingle(dt2.Rows[0]["conImporte"].ToString());
                        ImpMes[2] = (((ImporteConcepto * intMensual) / 100) * MultiplicadorCuotas) + ImpMes[2];

                        //Convert.ToDateTime(txtDesde.Text).
                        DateTime oPrimerDiaDelMes = new DateTime(Convert.ToDateTime(txtDesde.Text).Year, Convert.ToDateTime(txtDesde.Text).Month, 1);
                        DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
                        Vtos[2] = oUltimoDiaDelMes.ToString("dd/MM/yyyy");
                        ///

                    }
                }
                
                 
                //if (Vtos[2] == null || Vtos[2] == "")
                //{
                //    Vtos[2] = txtHasta.Text;
                //}

                if (intMensual > 0 && Convert.ToDateTime(txtDesde.Text.ToString()) > Convert.ToDateTime(Vtos[2].ToString()))
                {
                    Vtos[2] = txtHasta.Text;
                }



                int MesArchivo = Convert.ToInt32(row["ConMesInicio"]) + Convert.ToInt32(row["IcoNroCuota"]) -1;

                String mesanio = Convert.ToDateTime(MesArchivo.ToString() + "/" + row["icuAnoCursado"]).ToString("MMMM yyyy");

                ApellidoyNombre = row["aluNombre"].ToString();
                if(row["cntId"].ToString() == "1")
                {
                    ApellidoyNombre = "Inscripción " + row["icuAnoCursado"].ToString() + " - " + ApellidoyNombre;
                } else
                {
                    ApellidoyNombre = mesanio + " - " + ApellidoyNombre;
                }

                if (ApellidoyNombre.Length < 40 )
                {
                    ApellidoyNombre = ApellidoyNombre.PadRight(40);
                }
                if (ApellidoyNombre.Length > 40)
                {
                    ApellidoyNombre = ApellidoyNombre.Substring(0,40);
                }

                //string.Format("{0:000000000000}",  dt1.Rows[0]["aluDoc"].ToString());
                //myString.PadLeft(myString.Length + 5, '0');
                Cuerpo = row["aluDoc"].ToString().PadLeft(12, '0');  // Format("000000000000");
                Cuerpo = Cuerpo + ApellidoyNombre;
                Cuerpo = Cuerpo + Convert.ToDateTime(Vtos[0]).ToString("ddMMyy");

                Double ImpMes0 = Math.Round(ImpMes[0], 2) * 100;
                string ImpMes0Str = ImpMes0.ToString("F0"); // Elimina el punto decimal
                Cuerpo = Cuerpo + ImpMes0Str.PadLeft(10, '0');
                Cuerpo = Cuerpo + Convert.ToDateTime(Vtos[1]).ToString("ddMMyy");
                Double ImpMes1 = Math.Round(ImpMes[1], 2) * 100;
                string ImpMes1Str = ImpMes1.ToString("F0"); // Elimina el punto decimal
                Cuerpo = Cuerpo + ImpMes1Str.PadLeft(10, '0');
                Cuerpo = Cuerpo + Convert.ToDateTime(Vtos[2]).ToString("ddMMyy");
                
                //Modificado 10-04-2025
                Double ImpMes2 = Math.Round(ImpMes[2], 2) * 100;
                string ImpMes2Str = ImpMes2.ToString("F0"); // Elimina el punto decimal
                Cuerpo = Cuerpo + ImpMes2Str.PadLeft(10, '0');


                barra = "4" + row["insCodigo"].ToString().PadLeft(3, '0');   // string.Format("{0:C3}", dt1.Rows[0]["insCodigo"].ToString());
                barra = barra + row["insId"].ToString().PadLeft(3, '0');   //string.Format("{0:C3}", dt1.Rows[0]["insId"].ToString());
                barra = barra + row["aluId"].ToString().PadLeft(5, '0');   //string.Format("{0:C5}", dt1.Rows[0]["aluId"].ToString());
                barra = barra + Cuotas[2].ToString().PadLeft(2, '0');     //string.Format("{0:C2}", Cuotas[2]);
                //String pp = (Convert.ToInt32(Convert.ToDateTime(Vtos[0]).ToString("yyyyMMdd")) - Convert.ToInt32(Convert.ToDateTime("1/1/1996").ToString("yyyyMMdd"))).ToString().PadLeft(4, '0');
                
                if (Convert.ToInt32((Convert.ToDateTime(Vtos[1]) - Convert.ToDateTime("1/1/1996")).TotalDays.ToString()) <= 9999)
                {
                    String barra1 = (Convert.ToDateTime(Vtos[0]) - Convert.ToDateTime("01/01/1996")).TotalDays.ToString().PadLeft(4, '0');
                    barra = barra + (Convert.ToDateTime(Vtos[0]) - Convert.ToDateTime("01/01/1996")).TotalDays.ToString().PadLeft(4, '0');
                } else
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

                // 11-02-2024 Aquí está el error porque $2700 *10 = 27000 (5 posiciones y debería mandar 4...
                barra = barra + ((Convert.ToSingle(ImpMes[1]) - Convert.ToSingle(ImpMes[0]))*10).ToString().PadLeft(5, '0').Substring(0, 5);
                String DiferenciaImporte2 = ((Convert.ToSingle(ImpMes[1]) - Convert.ToSingle(ImpMes[0])) * 10).ToString().PadLeft(5, '0');   //.PadLeft(5, '0');
                if (DiferenciaImporte2.Length > 5)
                {
                    DiferenciaImporte2 = DiferenciaImporte2.Substring(0, 5);
                }
                barra = barra + DiferenciaImporte2;

                String fechaVto2 = Vtos[2];
                String fechaVto0 =  Vtos[0];
            
                if (Convert.ToInt32((Convert.ToDateTime(Vtos[2]) - Convert.ToDateTime(Vtos[0])).TotalDays.ToString()) <= 999)
                {
                    // barra = barra + (Convert.ToInt32(Convert.ToDateTime(Vtos[2]).ToString("yyyyMMdd")) - Convert.ToInt32(Convert.ToDateTime(Vtos[0]).ToString("yyyyMMdd"))).ToString().PadLeft(3, '0');   //Format(CInt((CDate(Vtos(3)) - CDate(Vtos(1)))), "000")
                    barra = barra + (Convert.ToDateTime(Vtos[2]) - Convert.ToDateTime(Vtos[0])).TotalDays.ToString().PadLeft(3, '0');   //Format(CInt((CDate(Vtos(3)) - CDate(Vtos(1)))), "000")
                } else
                {
                    barra = barra + "999";
                }
                // IMPORTANTE !!!!
                // Tener en cuenta que en el año 2024 si la diferencia en $ es mayor a 9.999 entonces el valor se expresa sin decimales por la multiplicación por 10
                // Estos datos q viajan en el código de barra no se utilizan en la caja para leer los importes
                ////////////////

                // Cambio a 5 dígitos 17/02/2024
                //barra = barra + ((Convert.ToSingle(ImpMes[1]) - Convert.ToSingle(ImpMes[0])) * 10).ToString().PadLeft(4, '0');

                //Double DiferenciaImporte3 = Math.Round(ImpMes[0] * 100, 2);
                //string ImpMes0Str = (ImpMes0 * 100).ToString("F0"); // Elimina el punto decimal
                //Cuerpo = Cuerpo + ImpMes0Str.PadLeft(10, '0');

                //String DiferenciaImporte3 = ((Convert.ToSingle(ImpMes[2]) - Convert.ToSingle(ImpMes[0])) * 10).ToString("F0").PadLeft(5, '0');   //.PadLeft(5, '0');

                Double DiferenciaImporte3 = Math.Round((Convert.ToSingle(ImpMes[2]) - Convert.ToSingle(ImpMes[0])) * 10, 2);
                string DiferenciaImporte3Str = "";
                if (DiferenciaImporte3 > 0 )
                {
                    DiferenciaImporte3Str = DiferenciaImporte3.ToString("F0"); // Elimina el punto decimal
                    DiferenciaImporte3Str = DiferenciaImporte3Str.PadLeft(5, '0');
                    if (DiferenciaImporte3Str.Length > 5)
                    {
                        DiferenciaImporte3Str = DiferenciaImporte3Str.Substring(0, 5);
                    }
                } else
                {
                    DiferenciaImporte3Str = "00000";
                }

                barra = barra + DiferenciaImporte3Str;   // Format(((ImpsMes(3) - CSng(ImpsMes(1))) * 10), "0000")

                Cuerpo = Cuerpo + barra;

                ocnArchivoBanco.barra = barra;
                ocnArchivoBanco.codcole = Convert.ToInt32(row["insId"].ToString());
                ocnArchivoBanco.codalumno = Convert.ToInt32(row["aluId"].ToString());
                ocnArchivoBanco.apellidoynombre = row["aluNombre"].ToString();
                ocnArchivoBanco.telef = row["aluTelefono"].ToString();
                ocnArchivoBanco.dni = row["aluDoc"].ToString();
                ocnArchivoBanco.numcuota = row["icoNroCuota"].ToString();
                ocnArchivoBanco.privto = Convert.ToDateTime(Vtos[0]).ToString("dd/MM/yy"); //string.Format("{0:dd/mm/yy}", Vtos[0]);
                ocnArchivoBanco.priimporte = Convert.ToDecimal(ImpMes[0]);
                ocnArchivoBanco.segvto = Convert.ToDateTime(Vtos[1]).ToString("dd/MM/yy");
                ocnArchivoBanco.segimporte = Convert.ToDecimal(ImpMes[1]);
                ocnArchivoBanco.tervto = Convert.ToDateTime(Vtos[2]).ToString("dd/MM/yy");
                ocnArchivoBanco.impabierto = Convert.ToDecimal(ImpMes[2]);
                ocnArchivoBanco.concepto = row["conNombre"].ToString();
                ocnArchivoBanco.curso = row["curNombre"].ToString();
                ocnArchivoBanco.aniolectivo = Convert.ToInt32(row["conAniolectivo"].ToString());
                ocnArchivoBanco.beca = row["becNombre"].ToString();
                ocnArchivoBanco.Cuerpo = Cuerpo;
                ocnArchivoBanco.Insertar();
         
            }


            Double TotalPrimerVto, TotalSegVto, TotalTerVto;
            TotalPrimerVto = 0;
            TotalSegVto = 0;
            TotalTerVto = 0;
            dtTot = ocnArchivoBanco.ObtenerTotales();



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
            ocnArchivoBanco.Cuerpo = Pie;
            ocnArchivoBanco.InsertarCuerpo();


            String Path;

            Path = "~/PaginasGenerales/ArchivosCaja/";
            Path = MapPath(Path);  //System.Web.UI.Page.Server.MapPath(NomRep);
            Mje.Visible = true;
            dtCA = ocnArchivoBanco.ObtenerTodo();
            if (dtCA.Rows.Count > 0)
            {
                FuncionesUtiles.crearArchivoTxt(dtCA, Path, NombreArchivo);
                btnDescargar.Visible = true;
            }             
        }
        else
        {
            btnDescargar.Visible = false;

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
            lblMensajeError.Text = Session["aluDoc"] + @"<div class=""alert alert-danger alert-dismissable"">
<button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
<a class=""alert-link"" href=""#"">Error de Sistema</a><br/>
Se ha producido el siguiente error:<br/>
MESSAGE:<br>" + oError.Message + "<br><br>EXCEPTION:<br>" + oError.InnerException + "<br><br>TRACE:<br>" + oError.StackTrace + "<br><br>TARGET:<br>" + oError.TargetSite +
"</div>";
            //            lblMensajeError.Text = @"<div class=""alert alert-danger alert-dismissable"">
            //<button aria-hidden=""true"" data-dismiss=""alert"" class=""close"" type=""button"">x</button>
            //<a class=""alert-link"" href=""#"">Error de Sistema</a><br/>
            //Se ha producido el siguiente error:<br/>
            //MESSAGE:<br>" + oError.Message + "<br><br>EXCEPTION:<br>" + oError.InnerException + "<br><br>TRACE:<br>" + oError.StackTrace + "<br><br>TARGET:<br>" + oError.TargetSite +
            //    "</div>";
        }
    }
}


