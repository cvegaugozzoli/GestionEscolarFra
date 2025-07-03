<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasBasicas/Principal.master" AutoEventWireup="true" CodeFile="InscripcionExamenRegistracion.aspx.cs" Inherits="InscripcionExamenRegistracion" %>

<%@ Register Src="../Controles/Particulares/cuFecha.ascx" TagName="cuFecha" TagPrefix="tpDatePicker" %>
<%@ MasterType TypeName="PaginasBasicas_Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <div class="row">
        <div class="form-group">
            <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
        </div>


        <div class="col-sm-12">
            <div class="ibox-content">
                <div class="form-inline">
                    <asp:CheckBox ID="ixaActivo" visible ="false" runat="server" Checked="True" Enabled="true"></asp:CheckBox>

                    <label class="control-label col-md-2">Buscar alumno por DNI:</label>
                    <asp:TextBox ID="aludni" type="string" step="1" class="form-control" runat="server" Width="20%" onClick="this.select();"></asp:TextBox>
                    <asp:Button ID="btnBuscar" class="btn btn-w-m btn-primary" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Width="10%" />
                    <asp:Button ID="btnCancelarAlumno" class="btn btn-w-m btn-primary" runat="server" Text="Cancelar" OnClick="btnCancelarAlumno_Click" Width="10%" />
                </div>
                <br />
                <table width="100%">
                    <tr>
                        <td width="10%" align="left">
                            <label class="control-label col-md-1">Nombre</label>
                        </td>
                        <td width="25%" align="left">
                            <asp:TextBox ID="aluNombre" type="string" class="form-control" runat="server" Enabled ="false"></asp:TextBox>
                        </td>
                        <td width="2%" align="left"></td>
                        <td width="6%" align="left">
                            <label>Legajo Nro</label>
                        </td>
                        <td width="15%" align="left">
                            <asp:TextBox ID="aluLegajoNumero" type="string" class="form-control" runat="server" Enabled ="false"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="aluId" type="int" Visible="false" class="form-control" runat="server"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="icuId" type="int" Visible="false" class="form-control" runat="server"></asp:TextBox>
                        </td>                       
                    </tr>
                </table>
            </div>
        </div>


        <div class="col-sm-12">
            <div class="ibox-content">
                <fieldset class="form-horizontal">

                    <div class="form-group">
                        <label class="control-label col-md-2">Fecha Examen</label><div class="col-md-6">
                            <tpDatePicker:cuFecha ID="ixaFechaExamen" runat="server" Enabled="true" AllowType="False" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Tipo Inscripción</label><div class="col-md-2">
                            <asp:DropDownList ID="itpId" runat="server" class="form-control m-b" Enabled="true"  AutoPostBack="True" OnSelectedIndexChanged="itpId_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Examen Tipo</label><div class="col-md-2">
                            <asp:DropDownList ID="extId" runat="server" class="form-control m-b" Enabled="true" ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Carrera</label><div class="col-md-6">
                            <asp:DropDownList ID="carId" runat="server" class="form-control m-b" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="carId_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Plan Estudio</label><div class="col-md-6">
                            <asp:DropDownList ID="plaId" runat="server" class="form-control m-b" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="plaId_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Curso</label><div class="col-md-6">
                            <asp:DropDownList ID="curId" runat="server" class="form-control m-b" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="curId_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Campo</label><div class="col-md-6">
                            <asp:DropDownList ID="camId" runat="server" class="form-control m-b" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="camId_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Espacio Curricular</label><div class="col-md-6">
                            <asp:DropDownList ID="escId" runat="server" class="form-control m-b" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="escId_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-md-2">Formato Dictado</label><div class="col-md-6">
                            <asp:DropDownList ID="fodId" runat="server" class="form-control m-b" Enabled="true" ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Número Acta</label><div class="col-md-6">
                            <asp:TextBox ID="ixaNumeroActa" type="number" step="1" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Número Libro</label><div class="col-md-6">
                            <asp:TextBox ID="ixaNumeroLibro" type="number" step="1" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Calificacion</label><div class="col-md-6">
                            <asp:TextBox ID="ixaCalificacion" type="number" step="1" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                </fieldset>
            </div>
        </div>





        <div class="col-sm-12">
            <div class="ibox-content">
                <div class="form-inline">
                    <div class="form-group">
                        <asp:Button ID="btnAceptar1" class="btn btn-w-m btn-primary" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" Width="100%" />
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnCancelar1" class="btn btn-w-m btn-danger" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" Width="100%" />
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
