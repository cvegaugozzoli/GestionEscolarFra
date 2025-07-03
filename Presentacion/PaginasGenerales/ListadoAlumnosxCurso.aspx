<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasBasicas/Principal.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ListadoAlumnosxCurso.aspx.cs" Inherits="ListadoAlumnosxCurso" %>

<%@ Register Src="../Controles/Particulares/cuFecha.ascx" TagName="cuFecha" TagPrefix="tpDatePicker" %>
<%@ MasterType TypeName="PaginasBasicas_Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">

    <div class="form-group">
        <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblInsId" Visible="false" runat="server" Text=""></asp:Label>
        <asp:TextBox ID="TextIC" Visible="false" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextTC" Visible="false" runat="server"></asp:TextBox>
    </div>

    <div class="ibox-content" style="background-color: #FFFFFF">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="form-row" style="background-color: #FFFFFF">
                    <div class="form-group col-md-4">
                        <label class="control-label">Nivel</label>
                        <asp:DropDownList ID="NivelID" runat="server" BorderColor="Silver" class="form-control" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="NivelID_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="form-group col-md-4">
                        <label class="control-label">Carrera</label>
                        <asp:DropDownList ID="ddlcarId" runat="server" BorderColor="Silver" class="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="carId_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="form-group col-md-4">
                        <label class="control-label ">Plan Estudio</label>
                        <asp:DropDownList ID="plaId" runat="server" BorderColor="Silver" class="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="plaId_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-row" style="background-color: #FFFFFF">
                    <div class="form-group col-md-3">
                        <label class="control-label ">Curso</label>
                        <asp:DropDownList ID="ddlcurId" runat="server" BorderColor="Silver" class="form-control" Enabled="true" AutoPostBack="true" OnSelectedIndexChanged="curId_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="form-group col-md-2">
                        <label class="control-label">Año de Cursado</label>
                        <asp:TextBox ID="AnioCursado" type="text" class="form-control" required="" BorderColor="Silver" runat="server" placeholder="Buscar por Año"
                            AutoPostBack="false" OnTextChanged="Nombre_TextChanged"></asp:TextBox>
                    </div>
                         <div class="form-group col-md-3">
                                    <label class="control-label">Tipo Concepto</label>
                                    <asp:DropDownList ID="ddlTipoConc" runat="server" BorderColor="Silver" class="form-control m-b" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="ddlTipoConc_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-3">
                                    <label class="control-label">Concepto</label>
                                    <asp:DropDownList ID="ddlConc" runat="server" BorderColor="Silver" class="form-control m-b" Enabled="true"></asp:DropDownList>
                                </div>
                    <div class="form-group col-md-7">
                        <br />
                        <asp:RadioButtonList AutoPostBack="true" class="form-check form-check-inline" RepeatLayout="Table" ID="RbtSeleccion" runat="server" RepeatDirection="Horizontal" Font-Italic="False" Width="350" Font-Size="11pt" ViewStateMode="Disabled" Visible="false">
                            <asp:ListItem Selected="True" Value="1"> &nbsp; &nbsp; Todos</asp:ListItem>
                            <asp:ListItem Value="2">  &nbsp; &nbsp;Sin Confirmar </asp:ListItem>
                            <asp:ListItem Value="3">  &nbsp; &nbsp;Confirmados </asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>

                <hr class="hr-line-dashed" />

            </ContentTemplate>

        </asp:UpdatePanel>

        <div class="ibox-content">
            <div class="form-row" style="background-color: #FFFFFF">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="alerError" visible="false" runat="server" class="alert alert-danger  alert-dismissable">
                            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="NivelID" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlcurId" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="ibox-content">
            <div class="form-row" style="background-color: #FFFFFF">

                <div class="form-group">
                    <asp:Button ID="btnAplicar" class="btn btn-w-m btn-primary" runat="server" Text="Aplicar" OnClick="btnAplicar_Click" />
                    <asp:Button ID="btnCancelar" class="btn btn-w-m btn-danger" formnovalidate="formnovalidate" runat="server" Text="Salir" OnClick="btnCancelar_Click" />
                </div>

            </div>
        </div>
        <div class="row">        
            <div class="ibox-content">
                <div class="table-responsive">
                    <asp:GridView ID="Grilla" runat="server" GridLines="None" CssClass="table table-striped"
                        AutoGenerateColumns="False" OnRowDataBound="Grilla_RowDataBound" OnRowCommand="Grilla_RowCommand"
                        PageSize="12" AllowPaging="True" OnPageIndexChanging="Grilla_PageIndexChanging">
                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                        <Columns>
                            <asp:TemplateField HeaderText="IC" Visible="false">
                                <ItemTemplate>
                                    <asp:HyperLink ForeColor="Black" ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:HyperLink ForeColor="Black" ID="Nombre" runat="server" Text='<%# Eval("Alumno") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DNI">
                                <ItemTemplate>
                                    <asp:HyperLink ForeColor="Black" ID="PlanEstudio" runat="server" Text='<%# Eval("Dni") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Curso">
                                <ItemTemplate>
                                    <asp:HyperLink ForeColor="Black" ID="Activo" runat="server" Text='<%# Eval("Curso") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:HyperLink ForeColor="Black" ID="Estado" runat="server" Text='<%# Eval("EstadoDesc") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TipoCarrera" Visible="false">
                                <ItemTemplate>
                                    <asp:HyperLink ForeColor="Black" ID="TipoCarrera" runat="server" Text='<%# Eval("TipoCarrera") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Link" Visible="false" HeaderText="Calificaciones" CommandName="Ver" Text="Ver" HeaderStyle-Width="100">



                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonField>


                            <asp:TemplateField Visible="false" HeaderText=" Eliminar">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbuEliminar" runat="server" OnClick="lbuEliminar_Click" ToolTip="Elimina de forma permanente el registro seleccionado">X</asp:LinkButton>
                                    <asp:Button ID="btnEliminarAceptar" runat="server" Text="Si" Visible="False"
                                        OnClick="btnEliminarAceptar_Click" />
                                    <asp:Button ID="btnEliminarCancelar" runat="server" Text="No" Visible="False"
                                        OnClick="btnEliminarCancelar_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>


                        <FooterStyle HorizontalAlign="NotSet" />


                        <HeaderStyle BackColor="#CCCCFF" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <SelectedRowStyle BackColor="#CCCCFF" />
                    </asp:GridView>
                </div>
            </div>
                             
                <div class="ibox-content">
                    <div class="table-responsive">
                        <asp:GridView ID="GrillaConFicha" runat="server" GridLines="None" CssClass="table table-striped"
                            AutoGenerateColumns="False" OnRowDataBound="GrillaConFicha_RowDataBound" OnRowCommand="GrillaConFicha_RowCommand"
                            PageSize="12" AllowPaging="True" OnPageIndexChanging="GrillaConFicha_PageIndexChanging">
                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                            <Columns>
                                <asp:TemplateField HeaderText="IC" Visible="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ForeColor="Black" ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="aluId" Visible="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ForeColor="Black" ID="aluId" runat="server" Text='<%# Eval("aluId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <asp:HyperLink ForeColor="Black" ID="Nombre" runat="server" Text='<%# Eval("Alumno") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DNI">
                                    <ItemTemplate>
                                        <asp:HyperLink ForeColor="Black" ID="PlanEstudio" runat="server" Text='<%# Eval("Dni") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Curso">
                                    <ItemTemplate>
                                        <asp:HyperLink ForeColor="Black" ID="Activo" runat="server" Text='<%# Eval("Curso") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:HyperLink ForeColor="Black" ID="Estado" runat="server" Text='<%# Eval("EstadoDesc") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TipoCarrera" Visible="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ForeColor="Black" ID="TipoCarrera" runat="server" Text='<%# Eval("TipoCarrera") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField ButtonType="Link" Visible="true" HeaderText="Ficha Medica" CommandName="Ver" Text="Ver" HeaderStyle-Width="100">



                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>


                                <asp:TemplateField Visible="false" HeaderText=" Eliminar">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbuEliminar" runat="server" OnClick="lbuEliminar_Click" ToolTip="Elimina de forma permanente el registro seleccionado">X</asp:LinkButton>
                                        <asp:Button ID="btnEliminarAceptar" runat="server" Text="Si" Visible="False"
                                            OnClick="btnEliminarAceptar_Click" />
                                        <asp:Button ID="btnEliminarCancelar" runat="server" Text="No" Visible="False"
                                            OnClick="btnEliminarCancelar_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>


                            <FooterStyle HorizontalAlign="NotSet" />


                            <HeaderStyle BackColor="#CCCCFF" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <SelectedRowStyle BackColor="#CCCCFF" />
                        </asp:GridView>
                    </div>
                </div>
            
            <div class="ibox-content">
                <div class="table-responsive">
                    <asp:GridView ID="gridtemp" runat="server" GridLines="None" CssClass="table table-striped"
                        AutoGenerateColumns="False">

                        <Columns>
                            <asp:TemplateField HeaderText="IC" Visible="false">
                                <ItemTemplate>
                                    <asp:HyperLink ForeColor="Black" ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre" Visible="false">
                                <ItemTemplate>
                                    <asp:HyperLink ForeColor="Black" ID="Nombre" runat="server" Text='<%# Eval("Alumno") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DNI" Visible="false">
                                <ItemTemplate>
                                    <asp:HyperLink ForeColor="Black" ID="PlanEstudio" runat="server" Text='<%# Eval("Dni") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Curso" Visible="false">
                                <ItemTemplate>
                                    <asp:HyperLink ForeColor="Black" ID="Activo" runat="server" Text='<%# Eval("Curso") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estado" Visible="false">
                                <ItemTemplate>
                                    <asp:HyperLink ForeColor="Black" ID="Estado" runat="server" Text='<%# Eval("EstadoDesc") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TipoCarrera" Visible="false">
                                <ItemTemplate>
                                    <asp:HyperLink ForeColor="Black" ID="TipoCarrera" runat="server" Text='<%# Eval("TipoCarrera") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Link" Visible="false" HeaderText="Calificaciones" CommandName="Ver" Text="Ver" HeaderStyle-Width="100">



                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonField>


                        </Columns>


                        <FooterStyle HorizontalAlign="NotSet" />


                        <HeaderStyle BackColor="#CCCCFF" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <SelectedRowStyle BackColor="#CCCCFF" />
                    </asp:GridView>
                </div>
            </div>

       
            <%--      <div class="form-group">
                <div class="form-group">
                    <asp:Label runat="server" ID="LblEC" Text="Espacio Curricular: " class="control-label col-md-2"></asp:Label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="EspCurBuscarId" runat="server" BorderColor="Silver" class="form-control m-b" Enabled="true">
                        </asp:DropDownList>
                    </div>
                </div>--%>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <asp:Button ID="ListadoSinDatos" class="btn btn-w-m btn-warning" runat="server" Text="Imprimir s/Datos" OnClick="btnImprimirSinDatos" />
                        <asp:Button ID="ListadoConDatos" class="btn btn-w-m btn-warning" runat="server" Text="Imprimir c/Datos" OnClick="btnImprimirConDatos" />
                        <asp:Button ID="ListadoSinDetalle" class="btn btn-w-m btn-warning" runat="server" Text="Imprimir con Cantidad" OnClick="ListadoSinDetalle_clik" />

                        <asp:Button ID="Exportar" Style="background-color: firebrick" Visible="false" runat="server" Text="Exportar a Excel" CssClass="btn btn-w-m btn-info" formnovalidate="formnovalidate" OnClick="Exportar_Click" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblMensajeError2" runat="server" Text=""></asp:Label>
            </div>
        </div>
</asp:Content>
