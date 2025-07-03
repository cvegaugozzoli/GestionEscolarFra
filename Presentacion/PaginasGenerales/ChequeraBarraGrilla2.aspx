<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChequeraBarraGrilla2.aspx.cs" Inherits="ChequeraBarraGrilla2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Instituto Madre Mercedes Guerra</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="Free HTML Templates" name="keywords" />
    <meta content="Free HTML Templates" name="description" />

    <!-- Customized Bootstrap Stylesheet -->
    <link href="../cssFranciscana/css/style.css" rel="stylesheet" />

    <%--<link href="impresion.css" type="text/css" media="print" />--%>
</head>



<body>

    <style type="text/css">
        @media print {
            body {
                width: 21cm;
                height: 29.7cm;
                margin: 10mm 20mm 10mm 20mm;
                /* change the margins as you want them to be. */
            }
        }
    </style>

    <%--    @media print {
        body{
            width: 21cm;
            height: 29.7cm;
            margin: 30mm 45mm 30mm 45mm; 
            /* change the margins as you want them to be. */
       } 
    }
    --%>

    <form id="form1" runat="server">


        <%--Primer Talón--%> 

        <div class="row">
            <div class="col-md-4">
                <div class="form-group col-md-12">
                    <div class="form-row">
                        <div class="form-group col-md-2">
                            <img src="../cssFranciscana/img/logo_francis_chequera.jpg" class="auto-style562" />
                        </div>
                        <div class="form-group col-md-10">
                            <asp:Label runat="server" Text="Inst. Madre Mercedes Guerra. " Font-Bold="True" Font-Size="Small"> </asp:Label>
                            <br />
                            <asp:Label runat="server" Text="Hnas. Terc. Franciscanas de la caridad
                            Incorporado a la enseñanza oficial
                            CUIT: 3063012583-5 IVA EXENTO"
                                Font-Bold="True" Font-Size="x-Small"> </asp:Label>
                        </div>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label4" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label5" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="lblCuota9" Text="Cuota: " runat="server"></asp:Label>
                        <asp:Label ID="Label6" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group col-md-12">
                    <div class="form-row">
                        <div class="form-group col-md-2">
                            <img src="../cssFranciscana/img/logo_francis_chequera.jpg" class="auto-style562" />
                        </div>
                        <div class="form-group col-md-10">
                            <asp:Label runat="server" Text="Inst. Madre Mercedes Guerra. " Font-Bold="True" Font-Size="Small"> </asp:Label>
                            <br />
                            <asp:Label runat="server" Text="Hnas. Terc. Franciscanas de la caridad Incorporado a la enseñanza oficial CUIT: 3063012583-5 IVA EXENTO"
                                Font-Bold="True" Font-Size="x-Small"> </asp:Label>
                        </div>
                    </div>

                    <div class="form-row">
                        <asp:Label ID="lblApellidoNombre" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="lblCurso" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="lblCuota8" Text="Cuota: " runat="server"></asp:Label>

                        <asp:Label ID="lblCuota" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group col-md-12">
                    <div class="form-row">
                        <div class="form-group col-md-2">
                            <img src="../cssFranciscana/img/logo_francis_chequera.jpg" class="auto-style562" />
                        </div>
                        <div class="form-group col-md-10">
                            <asp:Label runat="server" Text="Inst. Madre Mercedes Guerra. " Font-Bold="True" Font-Size="Small"> </asp:Label>
                            <br />
                            <asp:Label runat="server" Text="Hnas. Terc. Franciscanas de la caridad Incorporado a la enseñanza oficial CUIT: 3063012583-5 IVA EXENTO"
                                Font-Bold="True" Font-Size="x-Small"> </asp:Label>
                        </div>
                    </div>

                    <div class="form-row">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label7" Text="Cuota: " runat="server"></asp:Label>
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                    </div>

                </div>


                <div id="barcode">
                    <asp:Label ID="lblCodigoBarras" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <%--Segundo Talón--%>

        <div class="row">
            <div class="col-md-4">
                <div class="form-group col-md-12">
                    <div class="form-row">
                        <div class="form-group col-md-2">
                            <img src="../cssFranciscana/img/logo_francis_chequera.jpg" class="auto-style562" />
                        </div>
                        <div class="form-group col-md-10">
                            <asp:Label runat="server" Text="Inst. Madre Mercedes Guerra. " Font-Bold="True" Font-Size="Small"> </asp:Label>
                            <br />
                            <asp:Label runat="server" Text="Hnas. Terc. Franciscanas de la caridad
                            Incorporado a la enseñanza oficial
                            CUIT: 3063012583-5 IVA EXENTO"
                                Font-Bold="True" Font-Size="x-Small"> </asp:Label>
                        </div>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label4_2" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label5_2" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label10" Text="Cuota: " runat="server"></asp:Label>
                        <asp:Label ID="Label6_2" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group col-md-12">
                    <div class="form-row">
                        <div class="form-group col-md-2">
                            <img src="../cssFranciscana/img/logo_francis_chequera.jpg" class="auto-style562" />
                        </div>
                        <div class="form-group col-md-10">
                            <asp:Label runat="server" Text="Inst. Madre Mercedes Guerra. " Font-Bold="True" Font-Size="Small"> </asp:Label>
                            <br />
                            <asp:Label runat="server" Text="Hnas. Terc. Franciscanas de la caridad Incorporado a la enseñanza oficial CUIT: 3063012583-5 IVA EXENTO"
                                Font-Bold="True" Font-Size="x-Small"> </asp:Label>
                        </div>
                    </div>

                    <div class="form-row">
                        <asp:Label ID="lblApellidoNombre2" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="lblCurso2" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label14" Text="Cuota: " runat="server"></asp:Label>

                        <asp:Label ID="lblCuota2" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group col-md-12">
                    <div class="form-row">
                        <div class="form-group col-md-2">
                            <img src="../cssFranciscana/img/logo_francis_chequera.jpg" class="auto-style562" />
                        </div>
                        <div class="form-group col-md-10">
                            <asp:Label runat="server" Text="Inst. Madre Mercedes Guerra. " Font-Bold="True" Font-Size="Small"> </asp:Label>
                            <br />
                            <asp:Label runat="server" Text="Hnas. Terc. Franciscanas de la caridad Incorporado a la enseñanza oficial CUIT: 3063012583-5 IVA EXENTO"
                                Font-Bold="True" Font-Size="x-Small"> </asp:Label>
                        </div>
                    </div>

                    <div class="form-row">
                        <asp:Label ID="Label1_2" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label2_2" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label18" Text="Cuota: " runat="server"></asp:Label>
                        <asp:Label ID="Label3_2" runat="server"></asp:Label>
                    </div>

                </div>


                <div id="barcode2">
                    <asp:Label ID="lblCodigoBarras2" runat="server"></asp:Label>
                </div>
            </div>
        </div>


        <%--Tercer Talón--%> 

        <div class="row">
            <div class="col-md-4">
                <div class="form-group col-md-12">
                    <div class="form-row">
                        <div class="form-group col-md-2">
                            <img src="../cssFranciscana/img/logo_francis_chequera.jpg" class="auto-style562" />
                        </div>
                        <div class="form-group col-md-10">
                            <asp:Label runat="server" Text="Inst. Madre Mercedes Guerra. " Font-Bold="True" Font-Size="Small"> </asp:Label>
                            <br />
                            <asp:Label runat="server" Text="Hnas. Terc. Franciscanas de la caridad
                            Incorporado a la enseñanza oficial
                            CUIT: 3063012583-5 IVA EXENTO"
                                Font-Bold="True" Font-Size="x-Small"> </asp:Label>
                        </div>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label4_3" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label5_3" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label20" Text="Cuota: " runat="server"></asp:Label>
                        <asp:Label ID="Label6_3" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group col-md-12">
                    <div class="form-row">
                        <div class="form-group col-md-2">
                            <img src="../cssFranciscana/img/logo_francis_chequera.jpg" class="auto-style562" />
                        </div>
                        <div class="form-group col-md-10">
                            <asp:Label runat="server" Text="Inst. Madre Mercedes Guerra. " Font-Bold="True" Font-Size="Small"> </asp:Label>
                            <br />
                            <asp:Label runat="server" Text="Hnas. Terc. Franciscanas de la caridad Incorporado a la enseñanza oficial CUIT: 3063012583-5 IVA EXENTO"
                                Font-Bold="True" Font-Size="x-Small"> </asp:Label>
                        </div>
                    </div>

                    <div class="form-row">
                        <asp:Label ID="lblApellidoNombre3" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="lblCurso3" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label24" Text="Cuota: " runat="server"></asp:Label>

                        <asp:Label ID="lblCuota3" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group col-md-12">
                    <div class="form-row">
                        <div class="form-group col-md-2">
                            <img src="../cssFranciscana/img/logo_francis_chequera.jpg" class="auto-style562" />
                        </div>
                        <div class="form-group col-md-10">
                            <asp:Label runat="server" Text="Inst. Madre Mercedes Guerra. " Font-Bold="True" Font-Size="Small"> </asp:Label>
                            <br />
                            <asp:Label runat="server" Text="Hnas. Terc. Franciscanas de la caridad Incorporado a la enseñanza oficial CUIT: 3063012583-5 IVA EXENTO"
                                Font-Bold="True" Font-Size="x-Small"> </asp:Label>
                        </div>
                    </div>

                    <div class="form-row">
                        <asp:Label ID="Label1_3" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label2_3" runat="server"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label28" Text="Cuota: " runat="server"></asp:Label>
                        <asp:Label ID="Label3_3" runat="server"></asp:Label>
                    </div>

                </div>


                <div id="barcode3">
                    <asp:Label ID="lblCodigoBarras3" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <script src="//code.jquery.com/jquery-latest.min.js"></script>
        <script type="text/javascript" src="../js/barcode-jquery-master/jquery-barcode.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.5/jspdf.debug.js"></script>
        <script type="text/javascript">
            $(function () {
                var text1 = $('#<%=lblCodigoBarras.ClientID%>').html();
                $("#barcode").barcode(text1, // Valor del codigo de barras
                  "code128" // tipo (cadena)
                  );
            });
            $(function () {
                var text2 = $('#<%=lblCodigoBarras2.ClientID%>').html();
                $("#barcode2").barcode(text2, // Valor del codigo de barras
                  "code128" // tipo (cadena)
                  );
            });
            $(function () {
                var text3 = $('#<%=lblCodigoBarras3.ClientID%>').html();
                $("#barcode3").barcode(text3, // Valor del codigo de barras
                  "code128" // tipo (cadena)
                  );
            });

            $(function ImprimirPagina() {
                window.print();
                //window.close();
                setTimeout(function () { window.close(); }, 100);
            });
        </script>

    </form>

</body>
</html>
