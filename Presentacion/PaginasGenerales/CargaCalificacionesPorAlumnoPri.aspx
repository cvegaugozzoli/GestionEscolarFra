<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasBasicas/Principal.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="CargaCalificacionesPorAlumnoPri.aspx.cs" Inherits="CargaCalificacionesPorAlumnoPri" %>

<%@ Register Src="../Controles/Particulares/cuFecha.ascx" TagName="cuFecha" TagPrefix="tpDatePicker" %>
<%@ MasterType TypeName="PaginasBasicas_Principal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="row">
        <div class="form-group">
            <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>

            <asp:TextBox ID="ICtext" BorderColor="Silver" class="form-control" Visible="false" runat="server"></asp:TextBox>
        </div>

        <div class="col-sm-12" style="background-color: #FFFFFF">
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
        <div class="col-sm-12" style="background-color: #FFFFFF">
            <div class="ibox-title">
                <h5><a class="collapse-link">Calificaciones:</a></h5>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-12" style="background-color: #FFFFFF">
            <div class="table-responsive">
                <div id="dvGrid">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound"
                                Width="100%" DataKeyNames="Id" PageSize="50" AllowPaging="True" OnRowEditing="OnRowEditing" OnRowUpdating="OnRowUpdating" BorderColor="#3366CC" HorizontalAlign="Center" BackColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" ItemStyle-Width="150" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="aluId" runat="server" Text='<%# Eval("aluId") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Asignatura" ItemStyle-Width="250" ItemStyle-VerticalAlign="Bottom">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAsignatura" runat="server" Text='<%# Eval("Asignatura") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="True" Width="250px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Primer Trimestre" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblPTrim" runat="server" Text='<%# Eval("PTrim") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPTrim" runat="server" Text='<%# Eval("PTrim") %>' Width="80px"></asp:TextBox>
                                        </EditItemTemplate>
                                          <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Segundo Trimestre" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTrim" runat="server" Text='<%# Eval("STrim") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSTrim" runat="server" Text='<%# Eval("STrim") %>' Width="80"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tercer Trimestre" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTTrim" runat="server" Text='<%# Eval("TTrim") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtTTrim" runat="server" Text='<%# Eval("TTrim") %>' Width="90"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Promedio Anual">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPAnual" runat="server" Text='<%# Eval("PAnual") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPAnual" runat="server" Text='<%# Eval("PAnual") %>' Width="90"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Diciembre" ItemStyle-Width="100">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiciembre" runat="server" Text='<%# Eval("NotaDic") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDiciembre" runat="server" Text='<%# Eval("NotaDic") %>' Width="90"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Marzo" ItemStyle-Width="100">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMarzo" runat="server" Text='<%# Eval("NotaMar") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtMarzo" runat="server" Text='<%# Eval("NotaMar") %>' Width="90"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Calif.Def" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblrenCalfDef" runat="server" Text='<%# Eval("renCalfDef") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtrenCalfDef" runat="server" Text='<%# Eval("renCalfDef") %>' Width="90"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <%-- <asp:CommandField  ButtonType="Link" ShowEditButton="true" ShowCancelButton="false"
                                    ItemStyle-Width="80" />--%>



                                    <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowCancelButton="false"
                                        ItemStyle-Width="150" >

                                        <ItemStyle Width="150px" />
                                    </asp:CommandField>

                                    <asp:TemplateField Visible="false">
                                        <EditItemTemplate>
                                            <asp:LinkButton Text="Cancel" runat="server" OnClick="OnCancel" />
                                        </EditItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <HeaderStyle BackColor="#003399" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="#CCCCFF" />
                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                <RowStyle BackColor="White" ForeColor="#003399" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                <SortedDescendingHeaderStyle BackColor="#002876" />
                            </asp:GridView>
                            <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />

    <div class="row" style="background-color: #FFFFFF">
        <div class="col-sm-12" style="background-color: #FFFFFF">
            <div class="form-group col-md-5">
                <label runat="server" id="LblPeriodo" class="control-label">Periodo:</label>

                <asp:DropDownList ID="PeriodoId" runat="server" class="form-control" Enabled="true" BorderColor="Silver">
                    <asp:ListItem Value="0">Primer Trim.</asp:ListItem>
                    <asp:ListItem Value="1">Segundo Trim.</asp:ListItem>
                    <asp:ListItem Value="2">Tercer Trim.</asp:ListItem>
                    <asp:ListItem Value="3">Prom. Anual</asp:ListItem>
                    <asp:ListItem Value="4">Nota Dic.</asp:ListItem>
                    <asp:ListItem Value="5">Nota Mar.</asp:ListItem>
                    <asp:ListItem Value="6">Calif. Definitiva</asp:ListItem>
                    <asp:ListItem Selected="True" Value="7">Seleccionar..</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group col-md-2">
                <label runat="server" id="lblNota" class="control-label">Nota:</label>

                <asp:TextBox ID="TextNotaAsignar" BorderColor="Silver" class="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group col-md-3" >
                   <br />
                <asp:Button ID="ButtonAsignar" class="btn btn-w-m btn-primary" runat="server" Text="Asignar" OnClick="ButtonAsignar_Click" />
            </div>
        </div>
    </div>
    <div class="row" style="background-color: #FFFFFF">
        <asp:Label ID="LblMensajeErrorGrilla" runat="server" Text="" ForeColor="#CC3300" Font-Bold="True"></asp:Label>
    </div>
    <br />
    <br />
    <br />
    <br />
</asp:Content>
