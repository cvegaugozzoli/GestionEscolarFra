<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasBasicas/Principal.master" AutoEventWireup="true" CodeFile="EstadoCuenta.aspx.cs" Inherits="EstadoCuenta" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../Controles/Particulares/cuFecha.ascx" TagName="cuFecha" TagPrefix="tpDatePicker" %>
<%@ MasterType TypeName="PaginasBasicas_Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="form-group">
        <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblaluId" runat="server" Visible="false" Text=""></asp:Label>
    </div>

    <div class="ibox-content">

        <div class="form-row" style="background-color: #FFFFFF">
            <div class="form-group col-md-4" style="background-color: #FFFFFF">
                <label>Buscar por:&nbsp;&nbsp;&nbsp; </label>
                <asp:RadioButtonList AutoPostBack="true" CssClass="form-check form-check-inline" RepeatLayout="Flow" ID="RbtBuscar" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbtBuscar_SelectedIndexChanged" Font-Bold="True" Font-Italic="False">
                    <asp:ListItem style="margin-left: 0px; font-weight: bold" Selected="True" Value="0">Nombre </asp:ListItem>
                    <asp:ListItem style="margin-left: 30px; font-weight: bold" Value="1"> DNI </asp:ListItem>
                </asp:RadioButtonList>
                <asp:TextBox ID="TextBuscar" BorderColor="Silver" type="text" class="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group col-md-2">
                <label class="control-label">Año Lectivo</label>
                <asp:TextBox ID="txtAnioLectivo" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
            </div>

            <div class="form-group col-md-2">
                <br />
                <label class="control-label">Solo adeudados</label>
                <asp:CheckBox ID="ckbDeuda" Checked="true" AutoPostBack="true" runat="server" OnCheckedChanged="ckbDeuda_CheckedChanged" />
            </div>
            <div class="row">
                <div class="form-group col-md-2">
                    <br />
                    <asp:Button ID="Button1" class="btn btn-w-m btn-primary" runat="server" data-toggle="collapse" data-target="#collapseExample"
                        aria-expanded="false" aria-controls="collapseExample" Text="Buscar" OnClick="btnBuscar_Click" />
                </div>
                <div class="form-group col-md-2">
                    <br />
                    <asp:Button ID="btnCancelarAlumno" class="btn btn-w-m btn-danger" runat="server" Text="Cancelar" OnClick="btnCancelarAlumno_Click" />
                </div>
            </div>
        </div>


        <div class="form-row">
            <div class="ibox collapsed">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

                    <ContentTemplate>
                        <div class="row">
                            <div id="alerErroBE" visible="false" runat="server" class="alert alert-danger  alert-dismissable">
                                <asp:Label ID="lblalerErroBE" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            </div>
                        </div>


                        <div class="ibox-title" runat="server" id="CanReg" visible="false">
                            <h5>Listado |
                    <asp:Label ID="lblCantidadRegistros2" runat="server" Text=""></asp:Label></h5>
                        </div>

                        <div class="table-responsive">
                            <asp:GridView ID="Grilla" runat="server" GridLines="None" CssClass="table table-striped"
                                AutoGenerateColumns="False" DataKeyName="Id" OnRowDataBound="Grilla_RowDataBound" OnRowCommand="Grilla_RowCommand"
                                PageSize="5" OnPageIndexChanging="Grilla_PageIndexChanging" AllowPaging="True" Width="100%">
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                <Columns>

                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/img/select.png" CommandName="Ver" />
                                    <asp:TemplateField HeaderText="Id">
                                        <ItemTemplate>
                                            <asp:HyperLink ForeColor="Black" CommandName="Select" ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <ItemTemplate>
                                            <asp:HyperLink ForeColor="Black" ID="Nombre" runat="server" OnClick="redirectToFB()" Text='<%# Eval("Nombre") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DNI">
                                        <ItemTemplate>
                                            <asp:HyperLink ForeColor="Black" CommandName="Select" ID="Doc" runat="server" Text='<%# Eval("Doc") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Legajo">
                                        <ItemTemplate>
                                            <asp:HyperLink ForeColor="Black" CommandName="Select" ID="Legajo" runat="server" Text='<%# Eval("LegajoNumero") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Activo">
                                        <ItemTemplate>
                                            <asp:HyperLink ForeColor="Black" ID="Activo" runat="server" Text='<%# Eval("Activo") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#CCCCFF" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <SelectedRowStyle BackColor="#CCCCFF" />
                                <FooterStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />

                                <%--	                <PagerStyle HorizontalAlign="Center" Font-Bold="True" Font-Underline="True" Height="12" />--%>
                            </asp:GridView>

                        </div>

                    </ContentTemplate>

                    <Triggers>
                        <asp:PostBackTrigger ControlID="Grilla" />
                        <asp:PostBackTrigger ControlID="btnCancelarAlumno"></asp:PostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"></asp:AsyncPostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:Panel ID="AlumnoSeleccionado" runat="server" Visible="false" ForeColor="#333333">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">

                <ContentTemplate>
                    <div class="col-sm-12">
                        <label class="control-label col-md-12">Alumno Seleccionado</label>
                        <hr class="pg2-titl-bdr-btm" width="100%" />
                    </div>
                    <div class="ibox-content">
                        <div class="row" style="background-color: #FFFFFF">

                            <div class="form-group col-md-2">
                                <asp:Label ID="lblDNI" runat="server" Text="DNI:" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="aludni" BackColor="#006699" ForeColor="White" BorderColor="Silver" type="string" class="form-control" runat="server" Enabled="False"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-5">
                                <asp:Label class="control-label" ID="LblApe" runat="server" Text="Nombre: " Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="aluNombre" BackColor="#006699" ForeColor="White" BorderColor="Silver" type="string" class="form-control" runat="server" Enabled="False"></asp:TextBox>
                            </div>

                            <%--                <div class="form-group col-md-5">
                    <asp:Label class="control-label" ID="Label1" runat="server" Text="Link Pagos: " Font-Bold="true"></asp:Label>
                    <asp:TextBox ID="txtLinkPago" BackColor="#006699" ForeColor="White" BorderColor="Silver" type="string" class="form-control" runat="server" Enabled="False"></asp:TextBox>
                </div>--%>


                            <%--   <div class="form-group col-md-3">
                <asp:Label class="control-label" ID="lblColegio" runat="server" Text="Colegio: " Font-Bold="true"></asp:Label>
                <asp:TextBox ID="aluColegio" BackColor="#006699" ForeColor="White" BorderColor="Silver" type="string" class="form-control" runat="server" Enabled="False"></asp:TextBox>
            </div>--%>
                        </div>


                    </div>
                    <asp:TextBox ID="aluId" BorderColor="Silver" type="int" Visible="false" class="form-control" runat="server"></asp:TextBox>
                    <div class="ibox-title">
                        <h2 class="control-label" style="font-weight: bold">Conceptos adeudados</h2>

                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="ibox-content">
                                <div class="table-responsive">
                                    <asp:GridView ID="GrillaHistorial" runat="server" DataKeyNames="icoId,cntId,conId,TipoConcepto,Concepto,Importe,ImporteInteres,AnioLectivo,Beca,BecId,BecasInteres,NroCuota,FchInscripcion,FechaVto,ImpPagado,FechaPago,NroCompbte,Curso,Colegio" GridLines="None" CssClass="table table-striped"
                                        AutoGenerateColumns="False" OnRowCommand="GrillaHistorial_RowCommand">
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                        <Columns>
                                            <asp:BoundField DataField="icoId" HeaderText="icoId" Visible="false" />
                                            <asp:BoundField DataField="conId" HeaderText="conId" Visible="false" />
                                            <asp:BoundField DataField="cntId" HeaderText="cntId" Visible="false" />
                                            <asp:BoundField DataField="TipoConcepto" HeaderText="Tipo Concepto" Visible="false" />
                                            <asp:BoundField DataField="Concepto" HeaderText="Concepto" />
                                            <asp:BoundField DataField="Importe" HeaderText="Importe" />
                                            <asp:BoundField DataField="ImporteInteres" HeaderText="Importe Interes" Visible="true" />
                                            <asp:BoundField DataField="AnioLectivo" HeaderText="AnioLectivo" Visible="false" />
                                            <asp:BoundField DataField="Beca" HeaderText="Beca" />
                                            <asp:BoundField DataField="BecId" HeaderText="BecId" Visible="false" />
                                            <asp:BoundField DataField="NroCuota" HeaderText="NroCuota" />
                                            <asp:BoundField DataField="FchInscripcion" HeaderText="FchInscripcion" Visible="false" />
                                            <asp:BoundField DataField="FechaVto" HeaderText="FechaVto" />
                                            <asp:BoundField DataField="ValorInteres" HeaderText="ValorInteres" Visible="false" />
                                            <asp:BoundField DataField="ImpPagado" HeaderText="ImpPagado" />
                                            <asp:BoundField DataField="Colegio" HeaderText="Colegio" />
                                            <asp:BoundField DataField="insId" HeaderText="insId" Visible="false" />
                                            <asp:BoundField DataField="FechaPago" HeaderText="FechaPago" ItemStyle-CssClass="FechaPago" />
                                            <asp:BoundField DataField="NroCompbte" Visible="false" ItemStyle-CssClass="NroCompbte" HeaderText="NroCompbte" />
                                            <asp:BoundField DataField="Curso" ItemStyle-CssClass="Curso" HeaderText="Curso" />

                                            <%--      <asp:ButtonField ButtonType="Link" CommandName="Pagar" Text="Pagar" HeaderText="" />--%>
                                        </Columns>
                                        <FooterStyle HorizontalAlign="NotSet" />

                                        <HeaderStyle BackColor="#CCCCFF" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <SelectedRowStyle BackColor="#CCCCFF" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div id="alerError2" visible="false" runat="server" class="alert alert-danger  alert-dismissable">
                                <asp:Label ID="lblError2" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            </div>
                            <asp:Label ID="lblMjerror2" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>

                <Triggers>
                    <%--<asp:PostBackTrigger ControlID="Grilla" />--%>
                    <%--<asp:PostBackTrigger ControlID="btnCancelarAlumno"></asp:PostBackTrigger>--%>
                    <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"></asp:AsyncPostBackTrigger>
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>

    </div>

    <div class="ibox-content">
        <div class="form-group col-md-2">
            <br />
            <asp:Button ID="btnImprimir2" class="btn btn-w-m btn-warning" runat="server" Text="Imprimir" Visible="false" OnClick="btnImprimirClick" Width="100%" />
        </div>

        <div class="form-group col-md-3">
            <asp:Label ID="lblCuotas" runat="server" Text="Total Cuotas Vencidas:" Font-Bold="true" Visible="false"></asp:Label>
            <asp:TextBox ID="TexCuotas" Visible="false" BackColor="#cc0000" ForeColor="White" BorderColor="Silver" type="string" class="form-control" runat="server" Enabled="False"></asp:TextBox>
        </div>
        <div class="form-group col-md-3">
            <asp:Label ID="lblInt" Visible="false" runat="server" Text="Total Intereses:" Font-Bold="true"></asp:Label>
            <asp:TextBox ID="txtIntereses" Visible="false" BackColor="#cc0000" ForeColor="White" BorderColor="Silver" type="string" class="form-control" runat="server" Enabled="False"></asp:TextBox>
        </div>

        <div class="form-group col-md-3">
            <asp:Label ID="lblTot" Visible="false" runat="server" Text="Total:" Font-Bold="true"></asp:Label>
            <asp:TextBox ID="txtTot" Visible="false" BackColor="#cc0000" ForeColor="White" BorderColor="Silver" type="string" class="form-control" runat="server" Enabled="False"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <div class="ibox-content">
            <div class="form-inline col-md-10">
                <div class="form-group col-md-5">
                    <asp:Label class="control-label" ID="lblVencido" runat="server" Font-Bold="true" Visible="false">Vencidas</asp:Label>
                    <asp:TextBox ID="txtRojo" BackColor="red" ForeColor="red" Visible="false" BorderColor="Silver" type="string" class="form-control" runat="server" Enabled="False"></asp:TextBox>
                </div>
                <div class="form-group col-md-5">
                    <asp:Label class="control-label" ID="lblPagado" runat="server" Font-Bold="true" Visible="false">Pagado</asp:Label>
                    <asp:TextBox ID="txtcELESTE" BackColor="LightBlue" ForeColor="LightBlue" BorderColor="Silver" Visible="false" type="string" class="form-control" runat="server" Enabled="False"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

