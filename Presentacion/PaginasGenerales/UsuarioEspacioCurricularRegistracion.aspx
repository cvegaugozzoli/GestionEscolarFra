<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasBasicas/Principal.master" AutoEventWireup="true" CodeFile="UsuarioEspacioCurricularRegistracion.aspx.cs" Inherits="UsuarioEspacioCurricularRegistracion" %>

<%@ Register Src="../Controles/Particulares/cuFecha.ascx" TagName="cuFecha" TagPrefix="tpDatePicker" %>
<%@ MasterType TypeName="PaginasBasicas_Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="row">
        <div class="form-group">
            <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
        </div>    

        <label class="control-label col-md-10">Busqueda del Docente </label>
        <hr class="pg2-titl-bdr-btm" style="padding-bottom: 0px; margin-bottom: 0px; margin-left: 0PX" />

        <div class="col-sm-12" style="background-color: #FFFFFF">
            <div class="ibox-content">
                <div class="col-sm-10">
                    <div class="form-inline">
                        <br />
                        <label class="control-label col-md-2">
                            Buscar por:<br />
                        </label>
                        <div class="col-md-6">
                            <asp:RadioButtonList AutoPostBack="true" CssClass="radio radio-info radio-inline" RepeatLayout="Flow" ID="RbtBuscar" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbtBuscar_SelectedIndexChanged" Font-Bold="True" Font-Italic="False">
                                <asp:ListItem style="margin-left: 0px; font-weight: bold" Selected="True" Value="1">Nombre </asp:ListItem>
                                <asp:ListItem style="margin-left: 30px; font-weight: bold" Value="0"> DNI </asp:ListItem>
                            </asp:RadioButtonList>
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-sm-12" style="background-color: #FFFFFF">
            <div class="col-sm-8" style="background-color: #FFFFFF">
                <div class="form-inline">
                    <div class="col-md-4">
                        <asp:TextBox ID="TextBuscar" BorderColor="Silver" type="text" class="form-control" runat="server"></asp:TextBox>

                    </div>
                    <div class="col-md-8">
                        <asp:Button ID="Button1" formnovalidate class="btn btn-w-m btn-primary" runat="server" data-toggle="collapse" data-target="#collapseExample"
                            aria-expanded="false" aria-controls="collapseExample" Text="Buscar" OnClick="btnBuscar_Click" />
                        <%--                        <asp:Button ID="btnNuevoAlumno" class="btn btn-w-m btn-primary" runat="server" Text="Nuevo" OnClick="btnNuevoAlumno_Click" />--%>
                        <asp:Button ID="btnCancelarAlumno" formnovalidate class="btn btn-w-m btn-danger" runat="server" Text="Cancelar" OnClick="btnCancelar2_Click" />
                    </div>
                </div>
            </div>
        </div>



        <div class="col-sm-12" style="background-color: #FFFFFF">
            <div class="ibox collapsed">
                <div class="collapse" id="collapseExample">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="ibox-title">
                                <h5>Listado |
                                         <asp:Label ID="lblCantidadRegistros" runat="server" Text=""></asp:Label></h5>
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="GrillaBuscar" runat="server" GridLines="None" CssClass="table table-striped"
                                    AutoGenerateColumns="False" DataKeyName="Id" OnRowDataBound="GrillaBuscar_RowDataBound" OnRowCommand="GrillaBuscar_RowCommand"
                                    PageSize="10" AllowPaging="True">
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    <Columns>

                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/img/select.png" CommandName="Select" />
                                        <asp:TemplateField HeaderText="Id">
                                            <ItemTemplate>
                                                <asp:HyperLink ForeColor="Black" CommandName="Select" ID="Id" runat="server" Text='<%# Eval("UsuId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apellido">
                                            <ItemTemplate>
                                                <asp:HyperLink ForeColor="Black" ID="Apellido" runat="server" OnClick="redirectToFB()" Text='<%# Eval("_apellido") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre">
                                            <ItemTemplate>
                                                <asp:HyperLink ForeColor="Black" CommandName="Select" ID="Nombre" runat="server" Text='<%# Eval("_nombre") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DNI">
                                            <ItemTemplate>
                                                <asp:HyperLink ForeColor="Black" CommandName="Select" ID="dni" runat="server" Text='<%# Eval("_doc") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Perfil">
                                            <ItemTemplate>
                                                <asp:HyperLink ForeColor="Black" CommandName="Select" ID="perId" runat="server" Text='<%# Eval("perId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <PagerSettings Position="Top" />
                                    <PagerStyle HorizontalAlign="Left" />
                                    <%--	                <PagerStyle HorizontalAlign="Center" Font-Bold="True" Font-Underline="True" Height="12" />--%>
                                </asp:GridView>

                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="GrillaBuscar" />
                        </Triggers>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnCancelarAlumno" />
                        </Triggers>

                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"></asp:AsyncPostBackTrigger>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <hr class="pg2-titl-bdr-btm" style="padding-bottom: 0px; margin-bottom: 0px; margin-left: 0PX" />



        <div class="col-sm-12">
            <div class="ibox-content">
                <fieldset class="form-horizontal">


                    <asp:TextBox ID="usuId" class="form-control" runat="server" Visible="False" Font-Bold="True"></asp:TextBox>
 <asp:TextBox ID="PerfilId" class="form-control" runat="server" Visible="False" Font-Bold="True"></asp:TextBox>

                    <div class="form-group">
                        <label class="control-label col-md-2">Apellido</label><div class="col-md-6">
                            <asp:TextBox ID="ApellidoB" class="form-control" runat="server" Enabled="False" Font-Bold="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Nombre</label><div class="col-md-6">
                            <asp:TextBox ID="NombreB" Font-Bold="True" class="form-control" runat="server" Enabled="False"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Año Habilita</label><div class="col-md-6">
                            <asp:TextBox ID="uscAnoCursado" required ="" BorderColor="Silver" requeried="" type="number" step="1" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Carrera</label><div class="col-md-6">
                            <asp:DropDownList ID="carId" runat="server" BorderColor="Silver" class="form-control m-b" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="carId_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Plan Estudio</label><div class="col-md-6">
                            <asp:DropDownList ID="plaId" runat="server" BorderColor="Silver" class="form-control m-b" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="plaId_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Curso</label><div class="col-md-6">
                            <asp:DropDownList ID="curId" runat="server" BorderColor="Silver" class="form-control m-b" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="curId_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <%--   <div class="form-group">
                        <label class="control-label col-md-2">Campo</label><div class="col-md-6">
                            <asp:DropDownList ID="camId" runat="server" BorderColor="Silver" class="form-control m-b" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="camId_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>--%>
                    <div class="form-group">
                        <label class="control-label col-md-2">Espacio Curricular</label><div class="col-md-6">
                            <asp:DropDownList ID="escId" runat="server" BorderColor="Silver" class="form-control m-b" Enabled="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Activo</label><div class="col-md-6">
                            <asp:CheckBox ID="uscActivo" runat="server" Checked="True" Enabled="true"></asp:CheckBox>
                        </div>
                    </div>
                    <hr class="hr-line-dashed" />
                </fieldset>
            </div>
        </div>

        <div class="col-sm-12">
            <div class="ibox-content">
                <div class="form-inline">
                    <div class="form-group">
                        <asp:Button ID="btnAceptar1" class="btn btn-w-m btn-primary" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" Width="100%" />
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnCancelar1" formnovalidate class="btn btn-w-m btn-danger" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" Width="100%" />
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
