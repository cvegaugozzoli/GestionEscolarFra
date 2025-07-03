<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasBasicas/PrincipalPadres.master" AutoEventWireup="true" CodeFile="EstadoCuentaPadres.aspx.cs" Inherits="EstadoCuentaPadres" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../Controles/Particulares/cuFecha.ascx" TagName="cuFecha" TagPrefix="tpDatePicker" %>
<%@ MasterType TypeName="PrincipalPadres" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="form-group">
        <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblaluId" runat="server" Visible="false" Text=""></asp:Label>
    </div>

    <div class="ibox-content">

        <div class="form-row" style="background-color: #FFFFFF">
            <div class="form-group col-md-2">
                <label class="control-label">Año Lectivo</label>
                <asp:TextBox ID="txtAnioLectivo" type="text" class="form-control" runat="server" BorderColor="Silver"></asp:TextBox>
            </div>
            <div class="form-group col-md-2">
                <br />
                <asp:Button ID="btnActualizar" class="btn btn-w-m btn-primary" runat="server" data-toggle="collapse" data-target="#collapseExample"
                    aria-expanded="false" aria-controls="collapseExample" Text="Actualizar" OnClick="btnActualizar_Click" />
            </div>
            <div class="form-group col-md-2">
                <br />
                <label class="control-label">Solo adeudados</label>
                <asp:CheckBox ID="ckbDeuda" Checked="true" AutoPostBack="true" runat="server" OnCheckedChanged="ckbDeuda_CheckedChanged" />
            </div>
            <div class="row">
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
                            <div id="alerErroBE" visible="false" runat="server" class="alert alert-danger alert-dismissable">
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
                            </asp:GridView>
                        </div>

                    </ContentTemplate>

                    <Triggers>
                        <asp:PostBackTrigger ControlID="Grilla" />
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
                        </div>
                    </div>
                    <asp:TextBox ID="aluId" BorderColor="Silver" type="int" Visible="false" class="form-control" runat="server"></asp:TextBox>
                    <div class="ibox-title">
                        <h2 class="control-label" style="font-weight: bold">Conceptos adeudados</h2>

                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <%-- MENSAJE PARA CELULAR: AHORA CON CLASES BOOTSTRAP PARA OCULTAR EN PC --%>
                            <p class="text-danger text-center d-block d-md-none" style="margin: 10px 0; font-weight: bold;">
                                ⚠️ Importante: Toca una fila para ver más detalles.
                           
                            </p>
                            <div class="ibox-content">
                                <div class="table-responsive">
                                    <asp:GridView ID="GrillaHistorial" runat="server" DataKeyNames="icoId,cntId,conId,TipoConcepto,Concepto,Importe,ImporteInteres,AnioLectivo,Beca,BecId,BecasInteres,NroCuota,FchInscripcion,FechaVto,ImpPagado,FechaPago,NroCompbte,Curso,Colegio" GridLines="None" CssClass="table table-striped"
                                        AutoGenerateColumns="False" OnRowDataBound="GrillaHistorial_RowDataBound" OnRowCommand="GrillaHistorial_RowCommand">
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                        <Columns>
                                            <asp:BoundField DataField="icoId" HeaderText="icoId" Visible="false" />
                                            <asp:BoundField DataField="conId" HeaderText="conId" Visible="false" />
                                            <asp:BoundField DataField="cntId" HeaderText="cntId" Visible="false" />
                                            <asp:BoundField DataField="TipoConcepto" HeaderText="Tipo Concepto" Visible="false" />
                                            <asp:BoundField DataField="Concepto" HeaderText="Concepto" ItemStyle-CssClass="col-concept" HeaderStyle-CssClass="col-concept" />
                                            <asp:BoundField DataField="Importe" HeaderText="Importe" ItemStyle-CssClass="col-importe" HeaderStyle-CssClass="col-importe" />
                                            <asp:BoundField DataField="ImporteInteres" HeaderText="Importe Interes" ItemStyle-CssClass="col-ImporteInteres" HeaderStyle-CssClass="col-ImporteInteres" />
                                            <asp:BoundField DataField="AnioLectivo" HeaderText="AnioLectivo" Visible="false" />
                                            <asp:BoundField DataField="Beca" HeaderText="Beca" ItemStyle-CssClass="col-Beca" HeaderStyle-CssClass="col-Beca" />
                                            <asp:BoundField DataField="BecId" HeaderText="BecId" Visible="false" />
                                            <asp:BoundField DataField="NroCuota" HeaderText="NroCuota" ItemStyle-CssClass="col-NroCuota" HeaderStyle-CssClass="col-NroCuota" />
                                            <asp:BoundField DataField="FchInscripcion" HeaderText="FchInscripcion" Visible="false" />
                                            <asp:BoundField DataField="FechaVto" HeaderText="FechaVto" ItemStyle-CssClass="col-FechaVto" HeaderStyle-CssClass="col-FechaVto" />
                                            <asp:BoundField DataField="ValorInteres" HeaderText="ValorInteres" Visible="false" />
                                            <asp:BoundField DataField="ImpPagado" HeaderText="ImpPagado" ItemStyle-CssClass="col-ImpPagado" HeaderStyle-CssClass="col-ImpPagado" />
                                            <asp:BoundField DataField="Colegio" HeaderText="Colegio" ItemStyle-CssClass="col-Colegio" HeaderStyle-CssClass="col-Colegio" Visible="false" />
                                            <asp:BoundField DataField="insId" HeaderText="insId" Visible="false" />
                                            <asp:BoundField DataField="FechaPago" HeaderText="FechaPago" ItemStyle-CssClass="col-FechaPago" HeaderStyle-CssClass="col-FechaPago" Visible="false" />
                                            <asp:BoundField DataField="NroCompbte" Visible="false" ItemStyle-CssClass="NroCompbte" HeaderText="NroCompbte" />
                                            <asp:BoundField DataField="Curso" ItemStyle-CssClass="col-Curso" HeaderText="Curso" Visible="false" />

                                            <%-- Columna para el Estado (si se utiliza) --%>
                                            <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEstado" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle HorizontalAlign="NotSet" />

                                        <HeaderStyle BackColor="#CCCCFF" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <SelectedRowStyle BackColor="#CCCCFF" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div id="alerError2" visible="false" runat="server" class="alert alert-danger alert-dismissable">
                                <asp:Label ID="lblError2" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            </div>
                            <asp:Label ID="lblMjerror2" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>

                <Triggers>
                    <%--<asp:PostBackTrigger ControlID="Grilla" />--%>
                    <%--<asp:PostBackTrigger ControlID="btnCancelarAlumno"></asp:PostBackTrigger>--%>
                    <%--<asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"></asp:AsyncPostBackTrigger>--%>
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>


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
                    <div class="form-group col-md-4">
                        <asp:Label class="control-label" ID="lblVencido" runat="server" Font-Bold="true" Visible="false">Vencidas</asp:Label>
                        <asp:TextBox ID="txtRojo" BackColor="red" ForeColor="red" Visible="false" BorderColor="Silver" type="string" class="form-control" runat="server" Enabled="False"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <asp:Label class="control-label" ID="lblPagado" runat="server" Font-Bold="true" Visible="false">Pagado</asp:Label>
                        <asp:TextBox ID="txtcELESTE" BackColor="LightBlue" ForeColor="LightBlue" BorderColor="Silver" Visible="false" type="string" class="form-control" runat="server" Enabled="False"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- MODAL PARA EL DETALLE --%>
    <div class="modal fade" id="detalleModal" tabindex="-1" role="dialog" aria-labelledby="detalleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title" id="detalleModalLabel">Detalle del Concepto</h2>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="card">
                        <div class="card-body">
                            <h3 class="card-title" id="modalConcepto"></h3>
                            <p class="card-text">
                                <strong>Colegio:</strong> <span id="modalColegio"></span>
                                <br />
                                <strong>Curso:</strong> <span id="modalCurso"></span>
                                <br />
                                <strong>Número de Cuota:</strong> <span id="modalNroCuota"></span>
                                <br />
                                <strong>Importe Arancel:</strong> <span id="modalImporte"></span>
                                <br />
                                <strong>Intereses:</strong> <span id="modalIntereses"></span>
                                <br />
                                <strong>Beca:</strong> <span id="modalBeca"></span>
                                <br />

                                <strong>Importe Total:</strong> <span id="modalImporteTotal"></span>
                                <br />
                                <strong>Fecha Vencimiento:</strong> <span id="modalFechaVto"></span>
                                <br />
                                <strong>Importe Pagado:</strong> <span id="modalImpPagado"></span>
                                <br />

                            </p>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>


    <%-- SCRIPTS JAVASCRIPT --%>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>

    <script>
        $(document).ready(function () {
            console.log("jQuery ready y script personalizado cargado.");

            var gridviewId = '<%= GrillaHistorial.ClientID %>';
            console.log("ID del GridView en JavaScript: " + gridviewId);

            $('#' + gridviewId).on('click', 'tbody tr', function (event) {
                console.log("Click detectado en fila de GridView.");

                if ($(event.target).is('a, button, input[type="button"], input[type="submit"]')) {
                    console.log("Click en botón o enlace, ignorando.");
                    return;
                }

                var concepto = $(this).data('concepto');
                var importe = $(this).data('importe');
                var intereses = $(this).data('importeinteres'); // Asegúrate que el DataKeyName para intereses sea 'ImporteInteres'
                var beca = $(this).data('beca');
                var nroCuota = $(this).data('nrocuota');
                var fechaVto = $(this).data('fechavto');
                var dcto = $(this).data('dcto');
                var impPagado = $(this).data('imppagado');
                var fechaPago = $(this).data('fechapago');
                var colegio = $(this).data('colegio');
                var curso = $(this).data('curso');
                var FP = $(this).data('fp');
                // IMPORTANTE: Asegúrate de que el campo "ImporteTotal" esté disponible
                // Si no existe en DataKeyNames, necesitarás calcularlo o añadirlo al origen de datos
                var importeTotal = $(this).data('importetotal') || (parseFloat(importe) + parseFloat(intereses || 0) - parseFloat(dcto || 0)).toFixed(2); // Calculo de ejemplo si no existe directamente

                console.log("Datos de la fila: ", { concepto: concepto, importe: importe, FP: FP });

                $('#modalConcepto').text(concepto);
                $('#modalImporte').text(importe);
                $('#modalIntereses').text(intereses);
                $('#modalBeca').text(beca);
                $('#modalNroCuota').text(nroCuota);
                $('#modalFechaVto').text(fechaVto);
                $('#modalImpPagado').text(impPagado);
                $('#modalFechaPago').text(fechaPago);
                $('#modalColegio').text(colegio);
                $('#modalCurso').text(curso);
                $('#modalImporteTotal').text(importeTotal); // Asignar el importe total

                $('#detalleModal').modal('show');
                console.log("Intentando mostrar el modal.");
            });
        });
    </script>

    <script>
        // Este script se encarga de ocultar/mostrar las columnas basadas en el ancho de la ventana.
        function ajustarColumnasGridView() {
            var ancho = window.innerWidth;
            var columnsToHide = [
                ".col-importe",
                ".col-ImporteInteres",
                ".col-Beca",
                ".col-NroCuota",
                ".col-FechaVto",
                ".col-ImpPagado",
                ".col-Colegio",
                ".col-FechaPago",
                ".col-Curso",

            ];

            if (ancho < 768) { // Tamaño móvil y tabletas pequeñas (hasta md-breakpoint)
                columnsToHide.forEach(selector => {
                    document.querySelectorAll("#<%= GrillaHistorial.ClientID %> " + selector).forEach(el => {
                        el.style.display = "none";
                    });
                    document.querySelectorAll("#<%= GrillaHistorial.ClientID %> th" + selector).forEach(el => {
                        el.style.display = "none";
                    });

                    document.querySelectorAll("#<%= GrillaHistorial.ClientID %> .col-NroCuota").forEach(el => {
                        el.style.display = "table-cell";
                    });
                });
            } else { // Escritorio y tabletas grandes
                columnsToHide.forEach(selector => {
                    document.querySelectorAll("#<%= GrillaHistorial.ClientID %> " + selector).forEach(el => {
                        el.style.display = "table-cell";
                    });
                    document.querySelectorAll("#<%= GrillaHistorial.ClientID %> th" + selector).forEach(el => {
                        el.style.display = "table-cell";
                    });
                });
            }
        }

        window.addEventListener("load", ajustarColumnasGridView);
        window.addEventListener("resize", ajustarColumnasGridView);
    </script>
</asp:Content>

