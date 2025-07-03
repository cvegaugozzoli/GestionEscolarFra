<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasBasicas/Principal.master"
    AutoEventWireup="true" CodeFile="GenerarArchivoBanco.aspx.cs" Inherits="GenerarArchivoBanco" %>

<%@ Register Src="../Controles/Particulares/cuFecha.ascx" TagName="cuFecha" TagPrefix="tpDatePicker" %>
<%@ MasterType TypeName="PaginasBasicas_Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="row">
        <div class="form-group">
            <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblInsId" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lbLNombreArchivo" runat="server" Visible="false" Text=""></asp:Label>
        </div>

        <div class="col-sm-12" style="background-color: #FFFFFF">
            <div class="ibox-content">
                <div class="form-row" style="background-color: #FFFFFF">
                    <div class="form-group col-md-3">
                        <label class="control-label">Conceptos Tipos</label>
                        <asp:DropDownList ID="cntId" runat="server" class="form-control m-b" Enabled="true"></asp:DropDownList>
                    </div>

                    <div class="form-group col-md-3">
                        <label class="control-label">Año Lectivo</label>
                        <asp:TextBox ID="conAnioLectivo" type="number" step="1" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-3">
                        <label class="control-label">Fecha Emisión:</label>
                        <asp:TextBox ID="txtDesde" type="DateTimePicker" placeholder="dd/mm/aaaa" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-3">
                        <label class="control-label ">Fecha Vto Abierto:</label>
                        <asp:TextBox ID="txtHasta" type="DateTimePicker" placeholder="dd/mm/aaaa" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
                    </div>


                    <hr class="hr-line-dashed" />
                </div>
            </div>
        </div>


        <div class="col-sm-12">
            <div class="ibox-content">
                <div class="form-inline">
                    <div class="form-group">
                        <asp:Button ID="btnAceptar" class="btn btn-w-m btn-primary" runat="server" OnClientClick="javascript:ShowProgressBar()" Text="Generar" OnClick="btnAceptar_Click" Width="100%" />
                    </div>
                    <div id="dvProgressBar" style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../images/ajax-loader.gif" AlternateText="Cargando ..." ToolTip="Cargando ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
                    </div>

                    <div class="form-group">
                        <asp:Button ID="btnCancelar" class="btn btn-w-m btn-danger" runat="server" Text="Salir" OnClick="btnCancelar_Click" Width="100%" />
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnDescargar" class="btn btn-w-m btn-primary" runat="server" Text="Descargar" OnClick="btnDescargar_Click" Width="100%" Visible="false" />

                        <%--    <a href="ftp://obramisericordista.com.ar/public_html/PaginasGenerales/ArchivosCaja/" target="_blank">                                
                            </a>--%>
                    </div>
                </div>
                <br />
                <div class="form-group">
                    <div id="Mje" visible="false" runat="server" class="alert alert-info  alert-dismissable">
                        <h5 style="font-weight: bold; font-size: medium">"Archivo Generado con exito!!.."</h5>
                    </div>
                    <div id="alerError" visible="false" runat="server" class="alert alert-danger  alert-dismissable">
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    </div>

                </div>
            </div>
        </div>



    </div>



</asp:Content>
