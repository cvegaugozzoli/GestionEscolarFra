<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasBasicas/Principal.master" AutoEventWireup="true" CodeFile="Cupones.aspx.cs" Inherits="Cupones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../Controles/Particulares/cuFecha.ascx" TagName="cuFecha" TagPrefix="tpDatePicker" %>
<%@ MasterType TypeName="PaginasBasicas_Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="form-group">
        <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblaluId" runat="server" Visible="false" Text=""></asp:Label>
    </div>
    <div class="col-sm-12" style="background-color: #FFFFFF">
        <label class="control-label col-md-12">Buscar Alumno</label>
    </div>



    <div class="row" style="background-color: #FFFFFF">
        <div class="ibox-content">
            <div class="form-group col-md-3">
                <label class="control-label">DNI</label>
                <asp:TextBox ID="TextBuscar" BorderColor="Silver" type="text" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-md-2">
                <label class="control-label">A�o Lectivo</label>
                <asp:TextBox ID="txtAnioLectivo" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
            </div>
            <%--<div class="form-group col-md-2">
                <label class="control-label">
                    Colegio</label>
                <asp:DropDownList ID="ddlColegio" runat="server" class="form-control m-b">
                </asp:DropDownList>
            </div>--%>
            <div class="form-group col-md-2">
                &nbsp;&nbsp;&nbsp;&nbsp
                  <label class="control-label">
                      <br />
                  </label>
                <asp:Button ID="Button1" class="btn btn-w-m btn-primary" runat="server" data-toggle="collapse" data-target="#collapseExample"
                    aria-expanded="false" aria-controls="collapseExample" Text="Buscar" OnClick="btnBuscar_Click" />
            </div>
            <div class="form-group col-md-2">
                &nbsp;&nbsp;&nbsp;&nbsp
                <label class="control-label">
                    <br />
                </label>
                <asp:Button ID="btnCancelarAlumno" class="btn btn-w-m btn-danger" runat="server" Text="Cancelar" OnClick="btnCancelarAlumno_Click" />
            </div>

        </div>



    </div>
    <div class="row">
        <div id="alerErroBE" visible="false" runat="server" class="alert alert-danger  alert-dismissable">
            <asp:Label ID="lblalerErroBE" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
        </div>
    </div>
    <asp:Panel ID="AlumnoSeleccionado" runat="server" Visible="false" ForeColor="#333333">
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

                <div class="form-group col-md-5">
                    <asp:Label class="control-label" ID="Label1" runat="server" Text="Link Pagos: " Font-Bold="true"></asp:Label>
                    <asp:TextBox ID="txtLinkPago" BackColor="#006699" ForeColor="White" BorderColor="Silver" type="string" class="form-control" runat="server" Enabled="False"></asp:TextBox>
                </div>


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
                        <asp:GridView ID="GrillaHistorial" runat="server" DataKeyNames="icoId,cntId,conId,TipoConcepto,Concepto,Importe,ImporteInteres,AnioLectivo,Beca,BecId,NroCuota,FchInscripcion,FechaVto,ImpPagado,FechaPago,NroCompbte,Curso" GridLines="None" CssClass="table table-striped"
                            AutoGenerateColumns="False" OnRowCommand="GrillaHistorial_RowCommand">
                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                            <Columns>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="50" FooterStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccion" runat="server" Width="50" />
                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>

                                    <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>

                                <asp:BoundField DataField="icoId" HeaderText="icoId" Visible="false" />
                                <asp:BoundField DataField="conId" HeaderText="conId" Visible="false" />
                                <asp:BoundField DataField="cntId" HeaderText="cntId" Visible="false" />
                                <asp:BoundField DataField="TipoConcepto" HeaderText="Tipo Concepto" Visible="false" />
                                <asp:BoundField DataField="Concepto" HeaderText="Concepto" />
                                <asp:BoundField DataField="Importe" HeaderText="Importe" />
                                <asp:BoundField DataField="ImporteInteres" HeaderText="Importe Interes" Visible="false" />
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
    </asp:Panel>

    <div class="row">
        <div class="ibox-content">
            <div class="form-inline">

                <div class="form-group">

                                        <asp:Label class="control-label" ID="lblFchPago" runat="server" Text="Fecha de Pago: " Font-Bold="true"></asp:Label>
                    <asp:TextBox ID="txtFchPago" BorderColor="Silver"  class="form-control" runat="server" ></asp:TextBox>

                </div>

                <div class="form-group">
                    <asp:Button ID="btnFacturar" class="btn btn-w-m btn-warning" runat="server" Text="Facturar" Visible="false" OnClick="btnFacturarClick" Width="100%" />
                </div>

            </div>
        </div>
    </div>

    <%-- <div class="row">
        <div class="ibox-content">
            <div class="form-inline col-md-10">
                <div class="form-group col-md-5">
                    <asp:Label class="control-label" ID="lblVencido" runat="server" Font-Bold="true" Visible="false">Vencido</asp:Label>
                    <asp:TextBox ID="txtRojo" BackColor="red" ForeColor="red" Visible="false" BorderColor="Silver" type="string" class="form-control" runat="server" Enabled="False"></asp:TextBox>
                </div>
                <div class="form-group col-md-5">
                    <asp:Label class="control-label" ID="lblPagado" runat="server" Font-Bold="true" Visible="false">Pagado</asp:Label>
                    <asp:TextBox ID="txtcELESTE" BackColor="LightBlue" ForeColor="LightBlue" BorderColor="Silver" Visible="false" type="string" class="form-control" runat="server" Enabled="False"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>

