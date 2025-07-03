﻿<%@ Page Language="C#" MasterPageFile="~/PaginasBasicas/Principal.master" AutoEventWireup="true" CodeFile="Preinscripcion.aspx.cs" Inherits="Preinscripcion" %>

<%@ Register Src="../Controles/Particulares/cuFecha.ascx" TagName="cuFecha" TagPrefix="tpDatePicker" %>

<%@ MasterType TypeName="PaginasBasicas_Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <asp:ScriptManagerProxy runat="server"></asp:ScriptManagerProxy>

    <div id="wrapper">
        <asp:Label ID="aluId" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblicuId" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblAfiliacion" runat="server" Text="" Font-Bold="True" Font-Size="Medium" ForeColor="#293955"></asp:Label>


        <div class="row">
            <div class="col-lg-12">
                <div class="row wrapper border-bottom white-bg page-heading">
                    <div class="col-lg-10">
                        <h2>Validación de Formulario</h2>
                    </div>
                </div>
                <div class="ibox-content" style="background-color: #FFFFFF">
                    <%--  <h4>Busqueda Filiación</h4>--%>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:RadioButtonList AutoPostBack="true" CssClass="radio radio-info radio-inline" RepeatLayout="Flow" ID="RbtBuscar" runat="server" RepeatDirection="Vertical" OnSelectedIndexChanged="RbtBuscar_SelectedIndexChanged" Font-Bold="True" Font-Italic="False">
                                <asp:ListItem style="font-weight: bold" Selected="True" Value="0">Hermano en Institución</asp:ListItem>
                                <asp:ListItem style="font-weight: bold" Value="1">Padre o Madre en Institución </asp:ListItem>
                            </asp:RadioButtonList>
                            <br />
                            <br />
                        </div>
                    </div>
                    <h4>Ingrese DNI del Hermano o familiar de la Institución</h4>
                    <div class="row">
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:TextBox ID="TextBuscar" placeholder="DNI afiliación" BorderColor="Silver" type="text" class="form-control" runat="server"></asp:TextBox>
                                &nbsp;
                                 <div class="form-group">
                                     <asp:Button ID="Button1" class="btn btn-w-m btn-primary" runat="server" data-toggle="collapse" data-target="#collapseExample"
                                         aria-expanded="false" formnovalidate aria-controls="collapseExample" Text="Buscar" OnClick="btnBuscar_Click" />
                                 </div>
                            </div>
                            <div class="form-group">
                                &nbsp;&nbsp;&nbsp;
                                                  <a href=""><i class="fa fa-user fa-3x"></i></a>
                            </div>
                            &nbsp;&nbsp;
                            <div class="form-group">
                                <label>Nombre</label>
                                <asp:TextBox ID="txtNombre" type="text" class="form-control" runat="server" BorderColor="Silver" Enabled="false"></asp:TextBox>
                                &nbsp;
                            </div>
                            <div class="form-group">
                                <label>DNI</label>
                                <asp:TextBox ID="txtDni" type="text" class="form-control" runat="server" BorderColor="Silver" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="alerDNI" visible="false" runat="server" class="alert alert-danger  alert-dismissable">
                    <h3>"No se encontró DNI ingresado"</h3>
                </div>
                <div id="aletRepite" visible="false" runat="server" class="alert alert-danger  alert-dismissable">
                    <h3>"Ese DNI ya fue usado para una preinscripción..</h3>
                </div>
                <asp:Label ID="lblMensajeError" runat="server" Text="" Font-Bold="True" Font-Size="Medium" ForeColor="#293955"></asp:Label>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="true" ForeColor="#333333" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPreincribir"
                    EventName="Click" />
            </Triggers>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rblCurso"
                    EventName="SelectedIndexChanged" />
            </Triggers>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rblTurno"
                    EventName="SelectedIndexChanged" />
            </Triggers>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TXTdni2"
                    EventName="TextChanged" />
            </Triggers>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DniPadre"
                    EventName="TextChanged" />
            </Triggers>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DniMadre"
                    EventName="TextChanged" />
            </Triggers>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DniOtro"
                    EventName="TextChanged" />
            </Triggers>
            <ContentTemplate>

                <asp:Panel ID="Panel1" runat="server" Visible="false" ForeColor="#333333">
                    <%-- <div class="col-lg-12">
                        <h2>Curso y Turno</h2>
                    </div>--%>
                    <div id="alerSnJose" visible="false" runat="server" class="alert alert-info  alert-dismissable">
                        <h3>"Sr tutor Ud podrá pre-inscribir a su hijo en 1° Grado del Colegio San Jose.."</h3>
                    </div>
                    <div id="alerSanVicente" visible="false" runat="server" class="alert alert-info  alert-dismissable">
                        <h3>"Sr tutor Ud podrá pre-inscribir a su hijo en 1° Grado del Colegio San Vicente.."</h3>
                    </div>
                    <div id="alerMisericordia" visible="false" runat="server" class="alert alert-info  alert-dismissable">
                        <h3>"Sr tutor Ud podrá pre-inscribir a su hijo en 1° Grado del Colegio Misericordia, seleccione el Turno.."</h3>
                    </div>
                    <div id="alerJardin" visible="false" runat="server" class="alert alert-info  alert-dismissable">
                        <h3>"Sr tutor Ud podrá pre-inscribir a su hijo en esta institución, seleccione el Curso y Turno.."</h3>
                    </div>
                    <div class="row" style="background-color: #FFFFFF">
                        <div class="col-lg-12">
                            <div class="ibox-content" style="background-color: #FFFFFF">

                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblCurso" runat="server" Visible="false" Text="Curso" Font-Bold="True" Font-Size="Medium" ForeColor="#293955"></asp:Label>
                                    </div>
                                    <asp:RadioButtonList AutoPostBack="true" Visible="false" Enabled="true" runat="server" CssClass="radio radio-info radio-inline" RepeatLayout="Flow" ID="rblCurso" Font-Bold="True" Font-Italic="False" OnSelectedIndexChanged="rblCurso_SelectedIndexChanged1">
                                        <asp:ListItem Text="1 Grado" Value="1" />
                                        <asp:ListItem Text="Sala de 3" Value="2" />
                                        <asp:ListItem Text="Sala de 4" Value="3" />
                                        <asp:ListItem Text="Sala de 5" Value="4" />
                                    </asp:RadioButtonList>
                                </div>


                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblTurno" runat="server" Text="Turno" Visible="false" Font-Bold="True" Font-Size="Medium" ForeColor="#293955"></asp:Label>
                                    </div>
                                    <asp:RadioButtonList AutoPostBack="true" Enabled="true" Visible="false" runat="server" CssClass="radio radio-info radio-inline" RepeatLayout="Flow" ID="rblTurno" Font-Bold="True" Font-Italic="False" OnSelectedIndexChanged="rblTurno_SelectedIndexChanged1">
                                        <asp:ListItem Text="Turno Mañana" Value="1" />
                                        <asp:ListItem Text="Turno Tarde" Value="2" />
                                    </asp:RadioButtonList>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div id="alerCupo" visible="false" runat="server" class="alert alert-danger  alert-dismissable">
                        <h3>"Está cubierto el cupo para ese Curso.."</h3>
                    </div>
                    <%--                    <asp:Label ID="lblMejeColegiosCursos" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#CC0000"></asp:Label>--%>

                    <div class="row" style="background-color: #FFFFFF">
                        <div class="col-lg-12">
                            <h2>Datos del alumno ingresante</h2>
                        </div>
                    </div>


                    <div class="col-lg-12" style="background-color: #FFFFFF">
                        <div class="ibox-content" style="background-color: #FFFFFF">
                            <div class="form-row" style="background-color: #FFFFFF">
                                <div class="form-group col-md-3">
                                    <label>DNI</label>
                                    <asp:TextBox ID="TXTdni2" placeholder="Sin punto" class="form-control " AutoPostBack="true" required runat="server" BorderColor="Silver" Enabled="true" MaxLength="8" ValidationGroup="[0-9]" OnTextChanged="TXTdni2_textChanged" onblur="Page_ClientValidate()"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" BackColor="Transparent"
                                        BorderColor="Red" ControlToValidate="TXTdni2" Display="Dynamic" ErrorMessage="Dato Inválido.."
                                        Font-Size="Small" ValidationExpression="[0-9]{6,}" Width="90px"></asp:RegularExpressionValidator>
                                </div>

                                <div class="form-group col-md-3">
                                    <label>Nombre </label>
                                    <asp:TextBox placeholder="Apellido y Nombre" ID="txtAlu" type="text" class="form-control" required runat="server" BorderColor="Silver" Enabled="true"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-3">
                                    <label class="control-label ">CUIL</label>
                                    <asp:TextBox ID="aluCuit" type="text" placeholder="Sin espacio, ni guión" MaxLength="11" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" BackColor="Transparent"
                                        BorderColor="Red" ControlToValidate="aluCuit" Display="Dynamic" ErrorMessage="Datos Invalidos.. reingrese."
                                        Font-Size="Small" ValidationExpression="^[0-9]*$" Width="90px"></asp:RegularExpressionValidator>

                                </div>
                                <div class="form-group col-md-3">
                                    <label>Fecha de Nacimiento</label>
                                    <asp:TextBox ID="txtFecha" type="DateTimePicker" required class="form-control " runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>

                                <div class="form-row">
                                    <div class="form-group col-md-3">
                                        <label class="control-label">Domicilio</label>
                                        <asp:TextBox ID="aluDomicilio" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label class="control-label">Departamento</label>
                                        <asp:DropDownList ID="DepartamentoId" runat="server" class="form-control m-b" Enabled="true" BorderColor="Silver"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label class="control-label">Sexo</label>
                                        <asp:DropDownList ID="sexId" BorderColor="Silver" runat="server" class="form-control m-b" Enabled="true"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label class="control-label">Telefono </label>
                                        <asp:TextBox ID="txtTelFijo" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                    </div>
                                </div>

                                <%--    <div class="row">
                                        <div class="col-lg-12">
                                            <h3>DNI del Alumno Pre-inscripto</h3>
                                        </div>
                                    </div>
                                    <div class="form-group" style="background-color: white;">
                                        <%--  <p style="font-weight: bold">Sr. Tutor seleccione la foto del DNI.. </p>--%>
                                <%-- <div class="form-group">
                                            <asp:FileUpload ID="fupDni" runat="server" />
                                            <p class="cta-sub-title"></p>
                                            <asp:Button ID="Button2" formnovalidate runat="server" Text="Guardar" class="btn btn-w-m btn-primary" align="left" OnClick="Guardar_Click" />
                                            <asp:Label ID="Label2" runat="server" Text="" Visible="False"></asp:Label>
                                        </div>
                                    </div>--%>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-12" style="background-color: #FFFFFF">
                        <h2>Datos Familiares</h2>
                        <p>Debe completar Todos los datos del Tutor, abajo seleccionado..</p>
                    </div>

                    <div class="col-sm-12" style="background-color: #FFFFFF">
                        <br />
                        <label class="control-label col-md-12">Padre</label>
                        <hr class="pg-titl-bdr-btm" style="padding-bottom: 0px; margin-bottom: 0px; margin-left: 0PX" />
                    </div>
                    <div class="col-sm-12" style="background-color: #FFFFFF">
                        <div class="ibox-content" style="background-color: #FFFFFF">

                            <div class="form-row" style="background-color: #FFFFFF">
                                <div class="form-group col-md-2">
                                    <label class="control-label">DNI</label>
                                    <asp:TextBox ID="DniPadre" placeholder="Sin punto" type="text" class="form-control" runat="server" MaxLength="8" ValidationGroup="[0-9]" AutoPostBack="true" BorderColor="Silver" OnTextChanged="DniPadre_TextChanged" onblur="Page_ClientValidate()"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" BackColor="Transparent"
                                        BorderColor="Red" ControlToValidate="DniPadre" Display="Dynamic" ErrorMessage="Dato Inválido.. reingrese."
                                        Font-Size="Small" ValidationExpression="[0-9]{6,}" Width="90px"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label class="control-label">Apellido</label>
                                    <asp:TextBox ID="ApePadre" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4">
                                    <label class="control-label">Nombre</label>
                                    <asp:TextBox ID="NomPadre" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>

                                <div class="form-group col-md-2">
                                    <label class="control-label ">Celular</label>
                                    <asp:TextBox ID="TelefP" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-row" style="background-color: #FFFFFF">
                                <div class="form-group col-md-2">
                                    <label class="control-label ">Telf. Fijo</label>
                                    <asp:TextBox ID="txtFijoP" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-3">
                                    <label class="control-label">CUIL</label>
                                    <asp:TextBox ID="CuitP" placeholder="Sin espacio, ni guión" MaxLength="11" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" BackColor="Transparent"
                                        BorderColor="Red" ControlToValidate="CuitP" Display="Dynamic" ErrorMessage="Datos Invalidos.. reingrese."
                                        Font-Size="Small" ValidationExpression="^[0-9]*$" Width="90px"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group col-md-3">
                                    <label class="control-label">Ocupación</label>
                                    <asp:TextBox ID="OcupPadre" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4">
                                    <label class="control-label">Mail</label>
                                    <asp:TextBox ID="MailP" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-3">
                                    <label class="control-label col-md-1" runat="server" visible="false">Usuario</label>
                                    <asp:TextBox ID="UsuarioP" type="text" class="form-control" Visible="false" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12" style="background-color: #FFFFFF">
                        <br />
                        <label class="control-label col-md-12">Madre</label>
                        <hr class="pg-titl-bdr-btm" style="padding-bottom: 0px; margin-bottom: 0px; margin-left: 0PX" />
                    </div>
                    <div class="col-sm-12" style="background-color: #FFFFFF">
                        <div class="ibox-content" style="background-color: #FFFFFF">
                            <div class="form-row" style="background-color: #FFFFFF">
                                <div class="form-group col-md-2">
                                    <label class="control-label">DNI</label>
                                    <asp:TextBox ID="DniMadre" placeholder="Sin punto" MaxLength="8" ValidationGroup="[0-9]" AutoPostBack="true" type="text" class="form-control" runat="server" BorderColor="Silver" OnTextChanged="DniMadre_TextChanged" onblur="Page_ClientValidate()"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" BackColor="Transparent"
                                        BorderColor="Red" ControlToValidate="DniMadre" Display="Dynamic" ErrorMessage="Dato Inválido.. reingrese."
                                        Font-Size="Small" ValidationExpression="[0-9]{6,}" Width="90px"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label class="control-label">Apellido</label>
                                    <asp:TextBox ID="ApeMadre" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4">
                                    <label class="control-label">Nombre</label>
                                    <asp:TextBox ID="NomMadre" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-2">
                                    <label class="control-label">Celular</label>
                                    <asp:TextBox ID="TelefM" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>

                                <div class="form-row" style="background-color: #FFFFFF">
                                    <div class="form-group col-md-2">
                                        <label class="control-label ">Telf. Fijo</label>
                                        <asp:TextBox ID="txtFijoM" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-3">

                                        <label class="control-label">CUIL</label>
                                        <asp:TextBox ID="CuitM" placeholder="Sin espacio, ni guión" MaxLength="11" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" BackColor="Transparent"
                                            BorderColor="Red" ControlToValidate="CuitM" Display="Dynamic" ErrorMessage="Datos Invalidos.. reingrese."
                                            Font-Size="Small" ValidationExpression="^[0-9]*$" Width="90px"></asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group col-md-3">
                                        <label class="control-label">Ocupación</label>
                                        <asp:TextBox ID="OcupMadre" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label class="control-label">Mail</label>
                                        <asp:TextBox ID="MailM" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-3">
                                        <label runat="server" class="control-label col-md-1" visible="false">Usuario</label>
                                        <asp:TextBox ID="UsuarioM" type="text" Visible="false" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12" style="background-color: #FFFFFF">
                        <br />
                        <label class="control-label col-md-12">Otro</label>
                        <hr class="pg-titl-bdr-btm" style="padding-bottom: 0px; margin-bottom: 0px; margin-left: 0PX" />
                    </div>
                    <div class="col-sm-12" style="background-color: #FFFFFF">
                        <div class="ibox-content" style="background-color: #FFFFFF">
                            <div class="form-row" style="background-color: #FFFFFF">
                                <div class="form-group col-md-2">
                                    <label class="control-label col-md-1">DNI</label>
                                    <asp:TextBox ID="DniOtro" placeholder="Sin punto" AutoPostBack="true" MaxLength="8" ValidationGroup="[0-9]" type="text" class="form-control" runat="server" BorderColor="Silver" OnTextChanged="DniOtro_TextChanged" onblur="Page_ClientValidate()"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" BackColor="Transparent"
                                        BorderColor="Red" ControlToValidate="DniOtro" Display="Dynamic" ErrorMessage="Dato Inválido.. reingrese."
                                        Font-Size="Small" ValidationExpression="[0-9]{6,}" Width="90px"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label class="control-label">Apellido</label>
                                    <asp:TextBox ID="ApeOtro" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4">
                                    <label class="control-label col-md-1">Nombre</label>
                                    <asp:TextBox ID="NombOtro" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>

                                <div class="form-group col-md-2">
                                    <label class="control-label">Parentesco</label>
                                    <asp:DropDownList BorderColor="Silver" ID="ParentescoId" runat="server" class="form-control m-b" Enabled="true">

                                        <asp:ListItem Value="3">Abuelo</asp:ListItem>
                                        <asp:ListItem Value="4">Abuela</asp:ListItem>
                                        <asp:ListItem Value="5">Tío</asp:ListItem>
                                        <asp:ListItem Value="6">Tía</asp:ListItem>
                                        <asp:ListItem Value="7">Hermano/a</asp:ListItem>
                                        <asp:ListItem Value="8">Otro</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row" style="background-color: #FFFFFF">
                                <div class="form-group col-md-2">
                                    <label class="control-label">Ocupación</label>
                                    <asp:TextBox ID="OcupOtro" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>

                                </div>
                                <div class="form-group col-md-2">
                                    <label class="control-label">Celular</label>
                                    <asp:TextBox ID="TelefO" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-2">
                                    <label class="control-label">Telf. Fijo</label>
                                    <asp:TextBox ID="txtFijoO" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-3">
                                    <label class="control-label">CUIL</label>
                                    <asp:TextBox ID="CuitO" placeholder="Sin espacio, ni guión" MaxLength="11" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" BackColor="Transparent"
                                        BorderColor="Red" ControlToValidate="CuitO" Display="Dynamic" ErrorMessage="Datos Invalidos.. reingrese."
                                        Font-Size="Small" ValidationExpression="^[0-9]*$" Width="90px"></asp:RegularExpressionValidator>
                                </div>

                                <div class="form-group col-md-3">
                                    <label class="control-label col-md-1">Mail</label>
                                    <asp:TextBox ID="MailO" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-2">
                                    <label class="control-label" runat="server" visible="false">Usuario</label>
                                    <asp:TextBox ID="UsuarioO" type="text" Visible="false" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12" style="background-color: #FFFFFF">
                        <br />
                        <label class="control-label col-md-12">Seleccionar Tutor:</label>
                        <hr class="pg-titl-bdr-btm" style="padding-bottom: 0px; margin-bottom: 0px; margin-left: 0PX" />
                    </div>
                    <div class="col-sm-12" style="background-color: #FFFFFF">
                        <div class="ibox-content">
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:RadioButtonList ID="EsTutor" runat="server" Height="25px" RepeatDirection="Horizontal" Width="398px">
                                        <asp:ListItem Selected="True" Value="0">Madre</asp:ListItem>
                                        <asp:ListItem Value="1"> Padre</asp:ListItem>
                                        <asp:ListItem Value="2">Otro</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr class="hr-line-dashed" />


                    <div class="row">
                        <div class="ibox-content" style="background-color: #FFFFFF">
                            <asp:Label ID="lblMje3" runat="server" Text="" Font-Bold="True" Font-Size="Medium" ForeColor="#293955"></asp:Label>
                        </div>
                    </div>
                    <div id="alerEdad" visible="false" runat="server" class="alert alert-danger  alert-dismissable">
                        <h3>"La edad del Alumno no coincide con el curso seleccionado.. Ver Fecha de Nacimiento o seleccionar curso correcto."</h3>
                    </div>
                    <div id="alertExito" visible="true" runat="server" class="alert alert-primary  alert-dismissable">

                        <h3>"El alumno fue pre inscripto correctamente!! .."</h3>
                    </div>

                    <div id="alert" visible="false" runat="server" class="alert alert-danger  alert-dismissable">
                        <h3>"El alumno ya estaba registrado en este curso.."</h3>
                    </div>
                    <div class="row">
                        <div class="form-inline">
                            <div class="ibox-content" style="background-color: #FFFFFF">
                                <div class="form-group">
                                    <asp:Button ID="btnPreincribir" class="btn btn-w-m btn-warning" runat="server" Text="Preinscribir" OnClick="btnPreinscribir_Click" />
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnImprimir" formnovalidate Visible="false" class="btn btn-w-m btn-warning" runat="server" Text="Constancia" OnClick="btnImprimir_Click" />
                                </div>
                                 <div class="form-group">
                                    <%--<asp:Button ID="btnVolver" formnovalidate Visible="false" class="btn btn-w-m btn-warning" runat="server" Text="Preincripción Nueva" OnClick="btnVolver_Click" />--%>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnFinalizar" formnovalidate Visible="false" class="btn btn-w-m btn-warning" runat="server" Text="Finalizar" OnClick="btnFinalizar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <br />
        <br />
        <br />
        <br />
        <br />

        <div class="footer">

            <div>
                <strong>Asociación Civil de Hermanos Misericordistas</strong>
            </div>

        </div>
        <%--  </div>--%>
        <%--        </form>--%>


        <script type="text/javascript">
            function CheckOnlyOneCheckBox(checkbox) {
                var RadioButtonList = RadioButtonList.parentNode.parentNode.parentNode;
                var list = RadioButtonList.getElementsByTagName("input");
                for (var i = 0; i < list.length; i++) {
                    if (list[i] != checkbox && checkbox.checked) {
                        list[i].checked = false;
                    }
                }
            }
        </script>


        <script type="text/javascript">
            function CheckOnlyOneCheckBox2(checkbox) {
                var RadioButtonList = RadioButtonList.parentNode.parentNode.parentNode;
                var list = RadioButtonList.getElementsByTagName("input");
                for (var i = 0; i < list.length; i++) {
                    if (list[i] != checkbox && checkbox.checked) {
                        list[i].checked = false;
                    }
                }
            }
        </script>


        <script type="text/javascript">
            function CheckOnlyOneCheckBox3(checkbox) {
                var RadioButtonList = RadioButtonList.parentNode.parentNode.parentNode;
                var list = RadioButtonList.getElementsByTagName("input");
                for (var i = 0; i < list.length; i++) {
                    if (list[i] != checkbox && checkbox.checked) {
                        list[i].checked = false;
                    }
                }
            }
        </script>
        <br />
        <br />
        <br />
        <br />

    </div>
</asp:Content>
