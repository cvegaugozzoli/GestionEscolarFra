<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasBasicas/Principal.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="CargaCalificacionesPorAlumnoSec.aspx.cs" Inherits="CargaCalificacionesPorAlumnoSec" %>

<%@ Register Src="../Controles/Particulares/cuFecha.ascx" TagName="cuFecha" TagPrefix="tpDatePicker" %>
<%@ MasterType TypeName="PaginasBasicas_Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="row">
        <div class="form-group">
            <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
            <asp:TextBox ID="ICtext" BorderColor="Silver" class="form-control" Visible="false" runat="server"></asp:TextBox>
        </div>

        <div class="col-sm-12">
            <div class="ibox collapsed">
                <div class="ibox-title">
                    <h5><a class="collapse-link">Filtros</a></h5>
                    <div class="ibox-tools">
                        <a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </div>
                </div>


                <div class="ibox-content">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <fieldset class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-md-2">Año de cursado:</label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="anioCur" BorderColor="Silver" type="string" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <hr class="hr-line-dashed" />
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <fieldset class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-4 col-md-offset-0">
                                <asp:Button ID="btnBuscar" class="btn btn-w-m btn-primary" runat="server" Text="Aplicar" OnClick="btnBuscar_Click" />

                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-12">
            <div class="ibox-title">
                <h5><a class="collapse-link">Calificaciones:</a></h5>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-12">

            <div id="dvGrid" style="padding: 10px; width: 950px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" GridLines="None" CssClass="table table-striped" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound"
                            Width="100%" DataKeyNames="Id" PageSize="50" AllowPaging="true" OnRowEditing="OnRowEditing" OnRowUpdating="OnRowUpdating"
                            BorderColor="#3399FF" HorizontalAlign="Center">
                            <Columns>

                                <asp:TemplateField HeaderText="Id" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Asignatura" ItemStyle-Width="150px" ItemStyle-VerticalAlign="Bottom">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAsignatura" runat="server" Text='<%# Eval("Asignatura") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="True" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Primer Cuatrimestre" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPCuatr" runat="server" Text='<%# Eval("PCuatr") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPCuatr" runat="server" Text='<%# Eval("PCuatr") %>' Width="100"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Segundo Cuatrimestre" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSCuatr" runat="server" Text='<%# Eval("SCuatr") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSCuatr" runat="server" Text='<%# Eval("SCuatr") %>' Width="90"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Promedio Anual" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPAnual" runat="server" Text='<%# Eval("PAnual") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPAnual" runat="server" Text='<%# Eval("PAnual") %>' Width="90"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Diciembre" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiciembre" runat="server" Text='<%# Eval("NotaDic") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDiciembre" runat="server" Text='<%# Eval("NotaDic") %>' Width="90"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marzo" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarzo" runat="server" Text='<%# Eval("NotaMar") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtMarzo" runat="server" Text='<%# Eval("NotaMar") %>' Width="90"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Calif. Definitiva" ItemStyle-Width="100px">
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
                            </Columns>
                            <HeaderStyle BackColor="#CCCCFF" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                        <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <br />
            
        </div>

        <div class="col-sm-12">
            <div class="form-group">
                <label runat="server" id="LblPeriodo" class="control-label col-md-1">Periodo:</label>
                <div class="col-md-3">
                    <asp:DropDownList ID="PeriodoId" runat="server" class="form-control m-b" Enabled="true" BorderColor="Silver">
                        <asp:ListItem Value="0">Primer Cuat.</asp:ListItem>
                        <asp:ListItem Value="1">Segundo Cuatr.</asp:ListItem>
                        <asp:ListItem Value="2">Prom. Anual</asp:ListItem>
                        <asp:ListItem Value="3">Nota Dic.</asp:ListItem>
                        <asp:ListItem Value="4">Nota Mar.</asp:ListItem>
                        <asp:ListItem Value="5">Calif. Definitiva</asp:ListItem>
                        <asp:ListItem Selected="True" Value="6">Seleccionar..</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <label runat="server" id="lblNota" class="control-label col-md-1">Nota:</label>
                <div class="col-md-2">
                    <asp:TextBox ID="TextNotaAsignar" BorderColor="Silver" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="ButtonAsignar" class="btn btn-w-m btn-primary" runat="server" Text="Asignar" OnClick="ButtonAsignar_Click" />
                </div>
            </div>
        </div>
    </div>
             <asp:Label ID="LblMensajeErrorGrilla" runat="server" Text="" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:Label>


    <div class="row">
        <div style="padding: 10px; width: 950px">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="ibox-title">
                        <h5><a class="collapse-link" style="color: red; font-weight: bold;">Cantidad de Previas:  </a>
                            <asp:Label Font-Bold="true" ForeColor="Red" ID="lblCantidadPrevias" runat="server" Text=""></asp:Label></h5>
                    </div>
                    <asp:GridView ID="GridView2" runat="server" GridLines="None" CssClass="table table-striped"
                        AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound2" OnRowUpdating="OnRowUpdating2"
                        DataKeyNames="Id,RegNotaId,icuId,Asignatura,Curso,Calificacion,Anio" PageSize="50" AllowPaging="True"
                        OnRowEditing="OnRowEditing2" OnRowCancelingEdit="OnCancel2" BorderColor="#3399FF"
                        HorizontalAlign="Center" OnRowCommand="GridView2_RowCommand">
                        <Columns>

                            <asp:TemplateField HeaderText="Id" ItemStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdRegNPrev" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RegNota" ItemStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdRegN" runat="server" Text='<%# Eval("RegNotaId") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="icuId" ItemStyle-Width="80px" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblicuId" runat="server" Text='<%# Eval("icuId") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Asignatura" ItemStyle-Width="150" ItemStyle-VerticalAlign="Bottom">
                                <ItemTemplate>
                                    <asp:Label ID="lblAsignatura" runat="server" Text='<%# Eval("Asignatura") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="True" Width="250px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Curso" ItemStyle-Width="80px" ItemStyle-VerticalAlign="Bottom">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurso" runat="server" Text='<%# Eval("Curso") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="True" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Año" ItemStyle-Width="80px" ItemStyle-VerticalAlign="Bottom">
                                <ItemTemplate>
                                    <asp:Label ID="lblAnio" runat="server" Text='<%# Eval("Anio") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="True" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Calificación" ItemStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label ID="lblCalPrev" runat="server" Text='<%# Eval("Calificacion") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCalPrev" runat="server" Text='<%# Eval("Calificacion") %>' Width="80"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha" ItemStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("Fecha") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFecha" runat="server" Text='<%# Eval("Fecha") %>' Width="80"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                            </asp:TemplateField>

                            <asp:CommandField ButtonType="Link" ShowEditButton="TRUE" ShowCancelButton="false"
                                ItemStyle-Width="100" HeaderText="Edición" EditText="Modificar" />

                              <asp:TemplateField >
                                <EditItemTemplate>
                                    <asp:LinkButton Text="Cancel" runat="server" OnClick="OnCancel2" />
                                </EditItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:CommandField  ButtonType="Link" ShowInsertButton="TRUE" HeaderText="Inscripción" NewText="Rendirla Nuevamente" ItemStyle-Width="200" />

                            <asp:TemplateField>
                                <ItemTemplate>

                                    <asp:ImageButton ID="imbDelete" runat="server" ToolTip="Eliminar"
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Eliminar"
                                        ImageUrl="~/img/delete-309165_960_720.png" Width="20px" />

                                </ItemTemplate>
                                <ItemStyle Width="13%" />
                            </asp:TemplateField>
                                                    


                        </Columns>
                        <HeaderStyle BackColor="#CCCCFF" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <SelectedRowStyle BackColor="#CCCCFF" />
                    </asp:GridView>   

           
                </ContentTemplate>
            
            </asp:UpdatePanel>

            <br />
            <fieldset class="form-horizontal">
                <div class="form-group">

                    <div class="col-md-2">
                        <asp:Button ID="BtnMostrar" class="btn btn-w-m btn-primary" runat="server" Text="Historial" OnClick="BtnMostrar_Click" />
                    </div>


                    <div class="col-md-2">
                        <asp:Button ID="BtnSoloPrevias" class="btn btn-w-m btn-primary" runat="server" Text="Previas" OnClick="BtnMostrarPrevias_Click" Visible="False" />
                    </div>
                </div>
                <hr class="hr-line-dashed" />
            </fieldset>

        </div>
    </div>
</asp:Content>
