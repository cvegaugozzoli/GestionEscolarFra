<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasBasicas/Principal.master" AutoEventWireup="true" CodeFile="InscripcionExamenConsulta.aspx.cs" Inherits="InscripcionExamenConsulta" %>

<%@ Register Src="../Controles/Particulares/cuFecha.ascx" TagName="cuFecha" TagPrefix="tpDatePicker" %>
<%@ MasterType TypeName="PaginasBasicas_Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="row">
        <div class="form-group">
            <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
        </div>
        <div class="col-sm-12" >

            <div class="ibox collapsed">
                <div class="ibox-title">
                    <h5><a class="collapse-link">Filtros</a></h5>
                    <div class="ibox-tools">
                        <a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </div>
                </div>

                <div class="ibox-content">
                    <fieldset class="form-horizontal">
                        <div class="form-group">
                            <label class="control-label col-md-1">Nombre</label>
                            <div class="col-md-2">
                            <asp:TextBox ID="alunombre" type="text" class="form-control" runat="server" placeholder="Buscar por Alumno"
                                        AutoPostBack="True" OnTextChanged="alunombre_TextChanged"></asp:TextBox>
                            </div>
                            <label class="control-label col-md-1">Espacio Curricular:</label>
                            <div class="col-md-2">
                            <asp:TextBox ID="espaciocurricular" type="text" class="form-control" runat="server" placeholder="Buscar por Espacio Currricular"
                                        AutoPostBack="True" OnTextChanged="espaciocurricular_TextChanged"></asp:TextBox>
                            </div>
                            
                            <div class="form-group">
                                <label class="control-label col-md-1">
                                    Aplica filtro fecha</label><div class="col-md-2">
                                    <asp:CheckBox ID="aplicafiltrofecha" runat="server" AutoPostBack ="true"  OnCheckedChanged ="aplicaplicafiltrofecha_SelectedIndexChanged"  Checked="False" Enabled="true"></asp:CheckBox></div>
                            </div>

                            <label class="control-label col-md-1">Fecha Desde</label>
                            <div class="col-md-2">
                                <tpDatePicker:cuFecha ID="ixaFechaExamenDesde" runat="server" Enabled="true" AllowType="False" />
                            </div>
                            <label class="control-label col-md-1">Fecha Hasta</label>
                            <div class="col-md-2">
                                <tpDatePicker:cuFecha ID="ixaFechaExamenHasta" runat="server" Enabled="true" AllowType="False" />
                            </div>



                        </div>
                        <hr class="hr-line-dashed" />
                        <div class="form-group">
                            <div class="col-md-4 col-md-offset-0">
                                <asp:Button ID="btnAplicar" class="btn btn-w-m btn-info" runat="server" Text="Aplicar" OnClick="btnAplicar_Click" />
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
            
            
                <div class="form-inline" >
                    <div class="form-group" >
                        <asp:Button ID="btnNuevo" class="btn btn-w-m btn-warning" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" Width="100%" />
                    </div>
                    <div class="form-group" >
                        <asp:Button ID="btnExportarAExcel" class="btn btn-w-m btn-success" runat="server" Text="Exportar a Excel" OnClick="btnExportarAExcel_Click" Width="100%" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    

    <div class="row">
	    <div class="col-sm-12">
            <div class="ibox-title"><h5>Listado | <asp:Label ID="lblCantidadRegistros" runat="server" Text=""></asp:Label></h5></div>
		    <div class="ibox-content">
                <div class="table-responsive"><asp:GridView ID="Grilla" runat="server" GridLines="None" CssClass="table table-striped"
	                AutoGenerateColumns="False" OnRowDataBound="Grilla_RowDataBound" OnRowCommand="Grilla_RowCommand"
	                PageSize="12" AllowPaging="True" OnPageIndexChanging="Grilla_PageIndexChanging">
                    <PagerStyle HorizontalAlign = "Center" CssClass = "GridPager" />
	                <Columns>		                
                        <asp:TemplateField HeaderText="Id">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="Id" runat="server" NavigateUrl='<%# "InscripcionExamenRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"Id").ToString() %>' Text='<%# Eval("Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Alumno">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="Alumno" runat="server" NavigateUrl='<%# "InscripcionExamenRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"Id").ToString() %>' Text='<%# Eval("Alumno") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Carrera">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="Carrera" runat="server" NavigateUrl='<%# "InscripcionExamenRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"Id").ToString() %>' Text='<%# Eval("Carrera") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PlanEstudio">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="PlanEstudio" runat="server" NavigateUrl='<%# "InscripcionExamenRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"Id").ToString() %>' Text='<%# Eval("PlanEstudio") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Curso">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="Curso" runat="server" NavigateUrl='<%# "InscripcionExamenRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"Id").ToString() %>' Text='<%# Eval("Curso") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Campo">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="Campo" runat="server" NavigateUrl='<%# "InscripcionExamenRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"Id").ToString() %>' Text='<%# Eval("Campo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EspacioCurricular">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="EspacioCurricular" runat="server" NavigateUrl='<%# "InscripcionExamenRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"Id").ToString() %>' Text='<%# Eval("EspacioCurricular") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ExamenTipo">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="ExamenTipo" runat="server" NavigateUrl='<%# "InscripcionExamenRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"Id").ToString() %>' Text='<%# Eval("ExamenTipo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FechaExamen">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="FechaExamen" runat="server" NavigateUrl='<%# "InscripcionExamenRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"Id").ToString() %>' Text='<%# Eval("FechaExamen") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Calificacion">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="Calificacion" runat="server" NavigateUrl='<%# "InscripcionExamenRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"Id").ToString() %>' Text='<%# Eval("Calificacion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NumeroActa">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="NumeroActa" runat="server" NavigateUrl='<%# "InscripcionExamenRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"Id").ToString() %>' Text='<%# Eval("NumeroActa") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Activo">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="Activo" runat="server" NavigateUrl='<%# "InscripcionExamenRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"Id").ToString() %>' Text='<%# Eval("Activo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
		                <asp:TemplateField HeaderText="">
			                <ItemTemplate>
				                <asp:LinkButton ID="lbuEliminar" runat="server" OnClick="lbuEliminar_Click" ToolTip="Elimina de forma permanente el registro seleccionado">X</asp:LinkButton>
				                <asp:Button ID="btnEliminarAceptar" runat="server" Text="Si" Visible="False"
					                OnClick="btnEliminarAceptar_Click" />
				                <asp:Button ID="btnEliminarCancelar" runat="server" Text="No" Visible="False"
					                OnClick="btnEliminarCancelar_Click" />
			                </ItemTemplate>
		                </asp:TemplateField>
	                </Columns>
                      <HeaderStyle BackColor="#CCCCFF" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <SelectedRowStyle BackColor="#CCCCFF" />
	                <FooterStyle HorizontalAlign="Left" />
	                <HeaderStyle HorizontalAlign="Left" />
	                <PagerSettings Position="Top" />
	                <PagerStyle HorizontalAlign="Left" />
                </asp:GridView></div>
		    </div>
	    </div>
    </div>
</asp:Content>