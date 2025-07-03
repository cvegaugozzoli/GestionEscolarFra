<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasBasicas/Principal.master" AutoEventWireup="true" CodeFile="DocentesConsulta.aspx.cs" Inherits="DocentesConsulta" %>

<%@ Register Src="../Controles/Particulares/cuFecha.ascx" TagName="cuFecha" TagPrefix="tpDatePicker" %>
<%@ MasterType TypeName="PaginasBasicas_Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="row">
        <div class="form-group">
            <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
        </div>
 

        <div class="col-sm-12" >
            <div class="ibox-content" >
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
                            <label class="control-label col-md-2">
                                Nombre</label><div class="col-md-6">
                                    <asp:TextBox ID="Nombre" type="text" class="form-control" runat="server" placeholder="Buscar por Nombre"
                                       ></asp:TextBox></div>
                        </div>
                        <hr class="hr-line-dashed" />
                        <div class="form-group">
                            <div class="col-md-4 col-md-offset-0">
                                <asp:Button ID="btnAplicar" class="btn btn-w-m btn-info" runat="server" Text="Aplicar"
                                    OnClick="btnAplicar_Click" />
                            </div>
                        </div>
                    </fieldset>
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
	                <Columns>
		                
                        <asp:TemplateField HeaderText="Id">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="_id" runat="server" NavigateUrl='<%# "DocentesRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"DocId").ToString() %>' Text='<%# Eval("DocId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dni">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="_doc" runat="server" NavigateUrl='<%# "DocentesRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"DocId").ToString() %>' Text='<%# Eval("_doc") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="_nombre" runat="server" NavigateUrl='<%# "DocentesRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"DocId").ToString() %>' Text='<%# Eval("_nombre") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Apellido">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="_apellido" runat="server" NavigateUrl='<%# "DocentesRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"DocId").ToString() %>' Text='<%# Eval("_apellido") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Domicilio">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="_domicilio" runat="server" NavigateUrl='<%# "DocentesRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"DocId").ToString() %>' Text='<%# Eval("_domicilio") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Telefono">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="_telef" runat="server" NavigateUrl='<%# "DocentesRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"DocId").ToString() %>' Text='<%# Eval("_telef") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>           
                         <asp:TemplateField HeaderText="Perfil">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="Black" ID="Perfil" runat="server" NavigateUrl='<%# "DocentesRegistracion.aspx?Id=" + DataBinder.Eval(Container.DataItem,"DocId").ToString() %>' Text='<%# Eval("Perfil") %>' />
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
                </asp:GridView></div>
		    </div>
	    </div>
    </div>
</asp:Content>