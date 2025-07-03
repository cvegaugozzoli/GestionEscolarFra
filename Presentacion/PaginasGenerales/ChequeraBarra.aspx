<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChequeraBarra.aspx.cs" Inherits="ChequeraBarra" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Instituto Madre Mercedes Guerra</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="Free HTML Templates" name="keywords" />
    <meta content="Free HTML Templates" name="description" />

<%--
    <!-- Favicon -->
    <link href="../cssFranciscana/img/logo_francis_pagina.ico" rel="icon" />

    <!-- Google Web Fonts -->
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <link href="https://fonts.googleapis.com/css2?family=Handlee&family=Nunito&display=swap" rel="stylesheet" />

    <!-- Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.10.0/css/all.min.css" rel="stylesheet" />

    <!-- Flaticon Font -->
    <link href="../cssFranciscana/lib/flaticon/font/flaticon.css" rel="stylesheet" />

    <!-- Libraries Stylesheet -->
    <link href="../cssFranciscana/lib/owlcarousel/assets/owl.carousel.min.css" rel="stylesheet" />
    <link href="../cssFranciscana/lib/lightbox/css/lightbox.min.css" rel="stylesheet" />
--%>
    <!-- Customized Bootstrap Stylesheet -->
    <link href="../cssFranciscana/css/style.css" rel="stylesheet" />

    <%--<link href="impresion.css" type="text/css" media="print" />--%>



</head>



<body>


    <style type="text/css">
        #barcode {
            /*font-weight: normal;
            font-style: normal;
            line-height: normal;
            sans-serif;
            font-size: 12pt;*/
        }

        .auto-style558 {
            font-size: 9pt;
        }

        .auto-style559 {
            width: 15px;
        }

        .auto-style560 {
            width: 450px;
        }

        .auto-style561 {
            width: 369px;
        }

        .auto-style562 {
            width: 58px;
            height: 56px;
        }

    /*@media print {
        @page {size: A4 portrait;}
        @page rotada {size: A4 landscape;}
        table {page: rotada; page-break-before: right;}
        @page {
         @top-left {
            content: "Título";
          }
          @top-right {
            content: "Pág. " counter(page);
          }
        }
    }*/

    @media print {
        body{
            width: 21cm;
            height: 29.7cm;
            margin: 30mm 45mm 30mm 45mm; 
            /* change the margins as you want them to be. */
       } 
    }

    </style>

    <form id="form1" runat="server">

        <div class="row">
            <div class="col-md-4">
                <div class="form-group col-md-12">
                    <div class="form-row">
                        <div class="form-group col-md-2">
                            <img src="../cssFranciscana/img/logo_francis_pagina.jpg" class="auto-style562" />
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
                            <img src="../cssFranciscana/img/logo_francis_pagina.jpg" class="auto-style562" />
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
                            <img src="../cssFranciscana/img/logo_francis_pagina.jpg" class="auto-style562" />
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


            <div class="form-row">
        <div class="col-sm-12">
            <div class="ibox-title">
                <h5>Listado |
                    <asp:Label ID="lblCantidadRegistros" runat="server" Text=""></asp:Label></h5>
            </div>
            <div class="ibox-content">
                <div class="table-responsive">
                    <asp:GridView ID="Grilla" runat="server" DataKeyNames="BARRA" GridLines="None" CssClass="table table-striped"
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:HyperLink ForeColor="Black" ID="Nombre" runat="server" Text='<%# Eval("APELLIDOYNOMBRE") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BARRA">
                                <ItemTemplate>                                  
                                        <asp:Label ID="barra" ForeColor="Black" runat="server" Text='<%# Eval("BARRA") %>' />                                  

                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <PagerSettings Position="Top" />
                        <PagerStyle HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>
        </div>
  </div>

        <asp:Table ID="myTable" runat="server" Width="100%"> 
    <asp:TableRow>
        <asp:TableCell>Nombre</asp:TableCell>
        <asp:TableCell>Barra</asp:TableCell>
       
    </asp:TableRow>
</asp:Table>  






        <script src="//code.jquery.com/jquery-latest.min.js"></script>
        <script type="text/javascript" src="../js/barcode-jquery-master/jquery-barcode.min.js"></script>

     <%--   <script type="text/javascript">

            //$(function () {     
            //    $("#barcode").barcode(, "code128");
            //});

            $(function () {           
                var text1 = $('#<%=lblCodigoBarras.ClientID%>').html();        
                $("#barcode").barcode(text1, // Valor del codigo de barras
                  "code128" // tipo (cadena)
                  );
        });
        </script>--%>
        
           <script type="text/javascript">

            //$(function () {     
            //    $("#barcode").barcode(, "code128");
            //});

            $(function () {           
                var text1 = $('#<%=lblCodigoBarras.ClientID%>').html();        
                $("#barcode").barcode(text1, // Valor del codigo de barras
                  "code128" // tipo (cadena)
                  );
        });
        </script>

    </form>


    <%--  <script type="text/javascript">
            function get_object(id) {
                var object = null;
                if (document.layers) {
                    object = document.layers[id];
                } else if (document.all) {
                    object = document.all[id];
                } else if (document.getElementById) {
                    object = document.getElementById(id);
                }
                return object;
            }
            get_object("barcode2").innerHTML = DrawHTMLBarcode_I2OF5(get_object("barcode2").innerHTML, "", "yes", "in", 0, 5, 0.5, 5, "bottom", "center", "", "black", "white");        /* ]]> */--%>

    <%--</script>--%>

    <%--   <script type="text/javascript">
            /* <![CDATA[ */

            function get_object(id) {
                var object = null;
                if (document.layers) {
                    object = document.layers[id];
                } else if (document.all) {
                    object = document.all[id];
                } else if (document.getElementById) {
                    object = document.getElementById(id);
                }
                return object;
            }
            get_object("object").innerHTML = DrawHTMLBarcode_I2OF5(get_object("object").innerHTML, "", "yes", "in", 0, 5, 0.5, 5, "bottom", "center", "", "black", "white");        /* ]]> */

        </script>--%>
</body>
</html>
