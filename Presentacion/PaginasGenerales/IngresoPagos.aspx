<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasBasicas/Principal.master"
    AutoEventWireup="true" CodeFile="IngresoPagos.aspx.cs" Inherits="IngresoPagos" %>

<%@ Register Src="../Controles/Particulares/cuFecha.ascx" TagName="cuFecha" TagPrefix="tpDatePicker" %>
<%@ MasterType TypeName="PaginasBasicas_Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">

    <div class="form-group">
        <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
         <asp:Label ID="lblColegioId" runat="server" Visible="false" ></asp:Label>
    </div>
    <div id="Encabezado">
        <div class="form-group">
            <div class="ibox-content">
                <label class="control-label">Buscar Archivo</label>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>           
        </div>

        <div class="col-sm-12">
            <div class="ibox-content">
                <div class="form-inline">
                    <div class="form-group">
                        <asp:Button ID="btnListar" class="btn btn-w-m btn-primary" runat="server" OnClientClick="javascript:ShowProgressBar()" Text="Listar" align="left" OnClick="Listar_Click" Width="100%" />
                    </div>
                    <div id="dvProgressBar" style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.5;">
                        <asp:Image ID="Image1" runat="server" ImageUrl="../images/ajax-loader.gif" AlternateText="Cargando ..." ToolTip="Cargando ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
                    </div>

                    <div class="form-group">
                        <asp:Button ID="btnCancelar" class="btn btn-w-m btn-danger" runat="server" Text="Salir" OnClick="btnCancelar_Click" Width="100%" />
                    </div>
                    <div class="form-group">
                    <asp:Label ID="lblColegio" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12">
        <div class="ibox-title" id="listado" runat="server" visible="false">
            <h5>Listado |
                    <asp:Label ID="lblCantidadRegistros" runat="server" Text=""></asp:Label></h5>
        </div>
        <div class="ibox-content">
            <div class="table-responsive">
                <asp:GridView ID="GridView1" runat="server" GridLines="None" DataKeyNames="aluId,icoId,Nombre,NroCuota, Concepto,FechaPago,Importe,NroComprobante,Imputa,Curso"
                    CssClass="table table-striped" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="aluId" HeaderText="aluId" Visible="false" />
                        <asp:BoundField DataField="icoId" HeaderText="icoId" Visible="false" />
                        <asp:BoundField DataField="Nombre" HeaderText="Alumno" />
                        <asp:BoundField DataField="Concepto" HeaderText="Concepto" />
                        <asp:BoundField DataField="NroCuota" HeaderText="NroCuota" />
                        <asp:BoundField DataField="NroComprobante" Visible="false" HeaderText="Nro Banco" />
                        <asp:BoundField DataField="FechaPago" HeaderText="Fecha Pago" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Importe" HeaderText="Importe" />
                        <asp:BoundField DataField="Imputa" HeaderText="Imputa" />
                        <asp:BoundField DataField="Curso" HeaderText="Curso" />
                    </Columns>
                    <HeaderStyle BackColor="#ffcccc" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>
            </div>
        </div>
    </div>

    <div class="ibox-content">
        <div class="row" style="background-color: #FFFFFF">
            <div class="form-group">
                <asp:Button ID="btnImputar" Visible="false" class="btn btn-w-m btn-primary" runat="server" Text="Imputar"
                    OnClick="btnImputar_Click" />
                &nbsp;<asp:Button ID="btnImprimir" class="btn btn-w-m btn-danger" runat="server" Text="Imprimir"
                    OnClick="btnImprimir_Click" Visible="false" />
            </div>
        </div>
        <div class="row" style="background-color: #FFFFFF">
            <div id="AlerInfo" visible="false" runat="server" class="alert alert-primary  alert-dismissable" style="color: #000000">
                <p style="font-size: medium; font-weight: bold;">I = Imputado&nbsp;&nbsp;&nbsp;&nbsp; NI= No Imputado&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NE= No Existente</p>
            </div>
        </div>
    </div>
    <div class="ibox-content">
        <div class="row" style="background-color: #FFFFFF">
            <div class="form-group">
                <asp:Label ID="lblMej" runat="server" Text="" Font-Bold="True" ForeColor="#3333CC" Font-Size="Medium"></asp:Label>
                &nbsp;
            </div>
        </div>
    </div>

      <script type="text/javascript">
function printGrid() {
var gridData = document.getElementById('<%= GridView1.ClientID %>');
var windowUrl = 'about:blank';//set print document name for gridview
var uniqueName = new Date();
var windowName = 'Print_' + uniqueName.getTime();
var prtWindow = window.open(windowUrl, windowName,
'left=100,top=100,right=100,bottom=100,width=800,height=600');
prtWindow.document.write('<html><head></head>');
prtWindow.document.write('<body style="background:none !important">');
prtWindow.document.write(gridData.outerHTML);
prtWindow.document.write('</body></html>');
prtWindow.document.close();
prtWindow.focus();
prtWindow.print();
prtWindow.close();
}
</script>
</asp:Content>
