<%@ Page EnableEventValidation="true" Title="" Language="C#" MasterPageFile="~/PaginasBasicas/Principal.master"  AutoEventWireup="true" CodeFile="CargaCalifxEspCPri.aspx.cs" Inherits="CargaCalifxEspCPri" %>

<%@ Register Src="../Controles/Particulares/cuFecha.ascx" TagName="cuFecha" TagPrefix="tpDatePicker" %>
<%@ MasterType TypeName="PaginasBasicas_Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="row">
        <div class="form-group">
            <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
            <asp:TextBox ID="TextIC" Visible="false" runat="server"></asp:TextBox>
            <asp:TextBox ID="TextTC" Visible="false" runat="server"></asp:TextBox>
        </div>
        <div class="col-sm-12">
            <%-- <div class="ibox collapsed">
                <div class="ibox-title">
                    <h5><a class="collapse-link">Filtros</a></h5>
                    <div class="ibox-tools">
                        <a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </div>
                </div>--%>

            <div class="ibox-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <fieldset class="form-horizontal">
                            <div class="form-group">
                                <label class="control-label col-md-1">Carrera</label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="carId" runat="server" BorderColor="Silver" class="form-control m-b" Enabled="true" AutoPostBack="true" OnSelectedIndexChanged="carId_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <label class="control-label col-md-2">Plan Estudio</label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="plaId" runat="server" BorderColor="Silver" class="form-control m-b" Enabled="true" AutoPostBack="true" OnSelectedIndexChanged="plaId_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <%-- <label class="control-label col-md-1">Campo</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="camId" runat="server" BorderColor="Silver" class="form-control m-b" Enabled="true" AutoPostBack="true"></asp:DropDownList>
                                    </div>--%>
                                <label class="control-label col-md-1">Curso</label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="curId" runat="server" BorderColor="Silver" class="form-control m-b" Enabled="true" AutoPostBack="true" OnSelectedIndexChanged="curId_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-2">
                                        Año de Cursado</label><div class="col-md-2">
                                            <asp:TextBox ID="AnioCursado" type="text" required="" class="form-control" BorderColor="Silver" runat="server" placeholder="Buscar por Año"
                                                AutoPostBack="false" OnTextChanged="Nombre_TextChanged"></asp:TextBox>
                                        </div>
                                </div>

                                <label class="control-label col-md-2">Espacio Curricular</label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="escId" runat="server" BorderColor="Silver" class="form-control m-b" Enabled="true"></asp:DropDownList>
                                </div>

                            </div>


                            <hr class="hr-line-dashed" />

                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <fieldset class="form-horizontal">
                    <div class="form-group">
                        <div class="col-md-4 col-md-offset-0">
                            <asp:Button ID="btnAplicar" class="btn btn-w-m btn-info" runat="server" Text="Aplicar" OnClick="btnAplicar_Click" />
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>

        <%--            <div class="ibox-content">
                <div class="form-inline">
                    <div class="form-group">
                        <asp:Button ID="btnNuevo" class="btn btn-w-m btn-warning" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" Width="100%" />
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnExportarAExcel" class="btn btn-w-m btn-success" runat="server" Text="Exportar a Excel" OnClick="btnExportarAExcel_Click" Width="100%" />
                    </div>
                   
                </div>
            </div>--%>
    </div>


    <div class="row">
        <div class="col-sm-12">

            <div id="dvGrid" style="padding: 10px; width: 450px">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>

                        <asp:GridView ID="GridView1" runat="server" GridLines="None" CssClass="table table-striped"
                             AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound"
                            DataKeyNames="Id" PageSize="50" AllowPaging="true" OnRowEditing="OnRowEditing" 
                            OnRowUpdating="OnRowUpdating"
                            Width="100%" HorizontalAlign="Center">
                            <Columns>

                                <asp:TemplateField HeaderText="renId" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrenId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Alumno" ItemStyle-Width="250" ItemStyle-VerticalAlign="Bottom">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAlumno" runat="server" Text='<%# Eval("aluNombre") %>' Width="150"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="True" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Primer Trimestre" ItemStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPTrim" runat="server" Text='<%# Eval("PTrim") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPTrim" runat="server" Text='<%# Eval("PTrim") %>' Width="100"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Segundo Trimestre" ItemStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSTrim" runat="server" Text='<%# Eval("STrim") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSTrim" runat="server" Text='<%# Eval("STrim") %>' Width="90"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tercer Trimestre" ItemStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTTrim" runat="server" Text='<%# Eval("TTrim") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTTrim" runat="server" Text='<%# Eval("TTrim") %>' Width="90"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Promedio Anual" ItemStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPAnual" runat="server" Text='<%# Eval("PAnual") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPAnual" runat="server" Text='<%# Eval("PAnual") %>' Width="90"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Diciembre" ItemStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiciembre" runat="server" Text='<%# Eval("NotaDic") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDiciembre" runat="server" Text='<%# Eval("NotaDic") %>' Width="90"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marzo" ItemStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarzo" runat="server" Text='<%# Eval("NotaMar") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtMarzo" runat="server" Text='<%# Eval("NotaMar") %>' Width="90"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calif. Definitiva" ItemStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrenCalfDef" runat="server" Text='<%# Eval("renCalfDef") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtrenCalfDef" runat="server" Text='<%# Eval("renCalfDef") %>' Width="90"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>

                                <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowCancelButton="false"
                                    ItemStyle-Width="150" />

                                <asp:TemplateField Visible="false">
                                    <EditItemTemplate>
                                        <asp:LinkButton Text="Cancel" runat="server" OnClick="OnCancel" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="FDictado" ItemStyle-Width="150" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFDictadoId" runat="server" Text='<%# Eval("FDictado") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#ffcccc" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                        <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <br />
            <br />
            <br />
        </div>

        <div class="col-sm-12">
            <div class="form-group">
                <div class="col-md-1">
                    <label id="lblPeriodo" runat="server" class="control-label col-md-1" visible="False">Periodo:</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="PeriodoId" requeried="" runat="server" class="form-control m-b" Enabled="true" BorderColor="Silver" OnSelectedIndexChanged="PeriodoId_SelectedIndexChanged" Visible="False">
                        <asp:ListItem Value="0">Primer Trimestre.</asp:ListItem>
                        <asp:ListItem Value="1">Segundo Trimestre.</asp:ListItem>
                        <asp:ListItem Value="2">Tercer Trimestre.</asp:ListItem>
                        <asp:ListItem Value="3">Prom. Anual</asp:ListItem>
                        <asp:ListItem Value="4">Nota Dic.</asp:ListItem>
                        <asp:ListItem Value="5">Nota Mar.</asp:ListItem>
                        <asp:ListItem Value="6">Calif. Definitiva</asp:ListItem>
                        <asp:ListItem Selected="True" Value="7" Enabled="False">Seleccionar..</asp:ListItem>

                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <label id="lblNota" runat="server" class="control-label col-md-1" visible="False">Nota:</label>
                </div>
                <div class="col-md-1">
                    <asp:TextBox ID="TextNotaAsignar" BorderColor="Silver" class="form-control" runat="server" Visible="False"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="ButtonAsignar" class="btn btn-w-m btn-primary" runat="server" Text="Asignar" OnClick="ButtonAsignar_Click" Visible="False" />
                    &nbsp;&nbsp;
                </div>
                <div class="col-md-2">
                    &nbsp;&nbsp
                    <asp:Button ID="ButtonImprimir" class="btn btn-w-m btn-primary" runat="server" Text="Imprimir" OnClick="ButtonImprimir_Click" Visible="False" />
                </div>
            </div>
        </div>
    </div>

    <asp:Label ID="LblMensajeErrorGrilla" runat="server" Text="" ForeColor="#CC3300" Font-Bold="True"></asp:Label>


</asp:Content>
