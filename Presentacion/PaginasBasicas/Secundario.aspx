<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Secundario.aspx.cs" Inherits="Secundario" %>

<!DOCTYPE html>
<html lang="en" class=" js no-touch">
<head runat="server">
    <title>Asociación Civil Misericordia</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <link rel="stylesheet" href="../assets/css/main.css" />
    <link rel="shortcut icon" href="../images/LogoNuevoAso.jpg" />
</head>
<body class="is-preload">

    <!-- Page Wrapper -->
    <div id="page-wrapper">

        <!-- Header -->
        <header id="header">
            <h1><a href="index1.aspx">Hermanos de la Misericordia</a></h1>
            <nav id="nav">

                <ul>
                    <li class="special">
                        <a href="#menu" class="menuToggle"><span>Menu</span></a>
                        <div id="menu">
                            <ul>
                                <li><a href="Index1.aspx">Inicio</a></li>
                            </ul>
                        </div>
                    </li>
                </ul>
            </nav>
        </header>

        <!-- Main -->
        <article id="main">
            <header>
                <h2>Nivel Secundario </h2>
                <p>Colegio San José</p>
            </header>

            <section class="wrapper style5">
                <div class="feature" style="padding-right: 10px; padding-left: 10px">
                    <div class="ibox-content" style="padding-left: 10px; margin-left: 10px">


                        <section>

                            <div class="table-wrapper">

                          <%--      <table>
                                    <thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <h4>Ciclo Orientado - Listados de distribución de modalidades, conforme al sorteo realizado el día martes 16 de marzo.</h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>3° CS NATURALES-B </td>
                                                <td><a href="../Documento/LISTADO3CSNATURALESB.pdf" target="_blank" class="button ">Listado</a></td>
                                            <tr>
                                                <td>3° CS. NATURALES, MATEMATICA FISICA Y DISEÑO </td>
                                                <td><a href="../Documento/LISTADO3CSNATURALESMATEMATICAFISICAYDISENO.pdf" target="_blank" class="button ">Listado</a></td>
                                            </tr>
                                            <tr>
                                                <td>3° CIENCIAS SOCIALES Y HUMANIDADES</td>
                                                <td><a href="../Documento/LISTADO3CIENCIASSOCIALESYHUMANIDADES.pdf" target="_blank" class="button ">Listado</a></td>
                                            </tr>
                                            <tr>
                                                <td>3° CIENCIAS NATURALES - C </td>
                                                <td><a href="../Documento/LISTADO3CIENCIASNATURALESC.pdf" target="_blank" class="button ">Listado</a></td>
                                            </tr>
                                            <tr>
                                                <td>3° ECONOMIA Y ADMINISTRACION </td>
                                                <td><a href="../Documento/LISTADO3ECONOMIAYADMINISTRACION.pdf" target="_blank" class="button ">Listado</a></td>
                                            </tr>
                                            
                                        </tbody>
                                    </thead>
                                </table>--%>

                                <br /> 
                                <br /> 

                             <%--   <table>
                                    <thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <h4>Conformación de burbujas y Protocolos</h4>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Protocolo y Organización Ciclo Básico</td>
                                                <td><a href="../Documento/Burbujas/PROTOCOLOCICLOBASICOINICIOHORARIOS.pdf" target="_blank" class="button ">Guía</a></td>
                                            </tr>
                                            <tr>
                                                <td>7 AÑO A</td>
                                                <td><a href="../Documento/Burbujas/7ANOA.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>7 AÑO B</td>
                                                <td><a href="../Documento/Burbujas/7ANOB.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>7 AÑO C</td>
                                                <td><a href="../Documento/Burbujas/7ANOC.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>7 AÑO D</td>
                                                <td><a href="../Documento/Burbujas/7ANOD.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>7 AÑO E</td>
                                                <td><a href="../Documento/Burbujas/7ANOE.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>1 AÑO A</td>
                                                <td><a href="../Documento/Burbujas/1ANOA.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>1 AÑO B</td>
                                                <td><a href="../Documento/Burbujas/1ANOB.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>1 AÑO C Y D</td>
                                                <td><a href="../Documento/Burbujas/1ANOCYD.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>1 AÑO E</td>
                                                <td><a href="../Documento/Burbujas/1ANOE.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>1 AÑO F</td>
                                                <td><a href="../Documento/Burbujas/1ANOF.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>

                                            <tr>
                                                <td>2 AÑO A</td>
                                                <td><a href="../Documento/Burbujas/2ANOA.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>2 AÑO B</td>
                                                <td><a href="../Documento/Burbujas/2ANOB.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>2 AÑO C Y D</td>
                                                <td><a href="../Documento/Burbujas/2ANOCYD.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>2 AÑO E</td>
                                                <td><a href="../Documento/Burbujas/2ANOE.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>2 AÑO F</td>
                                                <td><a href="../Documento/Burbujas/2ANOF.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>

                                            <tr>
                                                <td>Protocolo y Organización Ciclo Orientado</td>
                                                <td><a href="../Documento/Burbujas/PROTOCOLOINICIOHORARIOSORIENTADO.pdf" target="_blank" class="button ">Guía</a></td>
                                            </tr>
                                            <tr>
                                                <td>4 AÑO A</td>
                                                <td><a href="../Documento/Burbujas/4ANOA.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>4 AÑO B</td>
                                                <td><a href="../Documento/Burbujas/4ANOB.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>4 AÑO C</td>
                                                <td><a href="../Documento/Burbujas/4ANOC.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>4 AÑO D</td>
                                                <td><a href="../Documento/Burbujas/4ANOD.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>4 AÑO E</td>
                                                <td><a href="../Documento/Burbujas/4ANOE.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>


                                            <tr>
                                                <td>5 AÑO A</td>
                                                <td><a href="../Documento/Burbujas/5ANOA.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>5 AÑO B</td>
                                                <td><a href="../Documento/Burbujas/5ANOB.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>5 AÑO C</td>
                                                <td><a href="../Documento/Burbujas/5ANOC.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>5 AÑO D</td>
                                                <td><a href="../Documento/Burbujas/5ANOD.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>
                                            <tr>
                                                <td>5 AÑO E</td>
                                                <td><a href="../Documento/Burbujas/5ANOE.pdf" target="_blank" class="button ">Burbuja</a></td>
                                            </tr>

                                        </tbody>
                                    </thead>
                                </table>--%>



                                </br>
                                </br>


                         <%--       <table>
                                    <thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <h4>Formularios para completar legajos de alumnos</h4>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>SR. PADRE O TUTOR:
                                                Preparándonos para el regreso a las clases presenciales, solicitamos a Uds. que reúnan la siguiente documentación para organizar los legajos de su hija/o. Los siguientes formularios deben descargar, imprimir y completar.(TAMBIEN PODRÁN RETIRAR IMPRESO DE LA PORTERIA DEL COLEGIO DE 8,30 A 11 HS. desde el 24 de febrero, por calle Libertad.)
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>1- FICHA MEDICA (1º pág. completada por usted y el examen clínico completada por un médico)</td>
                                                <td><a href="../Documento/FICHAMEDICA.pdf" target="_blank" class="button ">Formulario</a></td>
                                                <tr>
                                                    <td>2- DECLARACION JURADA PADRE O TUTOR </td>
                                                    <td><a href="../Documento/DECLJURADAPADRE.pdf" target="_blank" class="button ">Formulario</a></td>
                                                </tr>
                                            <tr>
                                                <td>3- DECLARACION JURADA ALUMNO</td>
                                                <td><a href="../Documento/DECLJURADAALUMNO.pdf" target="_blank" class="button ">Formulario</a></td>
                                            </tr>
                                            <tr>
                                                <td>4- FICHA DE DATOS PARA EL LEGAJO </td>
                                                <td><a href="../Documento/Fichalegajo2021.pdf" target="_blank" class="button ">Formulario</a></td>
                                            </tr>
                                        </tbody>
                                    </thead>
                                </table>

                                <table>
                                    <tr>
                                        <td>ADEMÁS, deberán adjuntar:
                                        </td>
                                    </tr>
                                </table>

                                <ul>
                                    <li>FOTOCOPIA DE DNI ACTUALIZADO</li>
                                    <li>UNA FOTO CARNET</li>
                                    <li>ORIGINAL DEL CERTIFICADO DE TERMINACIÓN DE 6º GRADO (solo para los ingresantes a 7º año)</li>
                                    <li>ACTA DE NACIMIENTO (solo para los ingresantes de 7º y 3º año)</li>
                                </ul>

                                <table>
                                    <tr>
                                        <td>La documentación deberán presentarla, en folio, hasta el 20 de Marzo, en la Institución y entregarla en Secretaria.</td>
                                    </tr>
                                </table>
                            </div>--%>

                        </section>


                   <%--     <section>
                            <h4>Inscripciones Ciclo Lectivo 2021
                                <br>
                                (solo alumnos que pertenecen a la Institución)</h4>
                        <h5>Ciclo Lectivo 2021</h5>
                            <div class="table-wrapper">
                                <table>
                                    <thead>
                                        <tr>
                                            <th>Sección</th>
													<th>Link</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>7MO GRADO</td>
                                            <td><a href="https://docs.google.com/forms/d/e/1FAIpQLSdDtyKCICtyRucMY3mNQ3ATDLgg0mAj5V3a6Fuk2CdCEw-ayA/viewform" class="button ">Formulario</a></td>

                                        </tr>
                                        <tr>
                                            <td>1ER. 2DO. 4TO. 5TO. AÑO </td>
                                            <td><a href="https://docs.google.com/forms/d/e/1FAIpQLSeDE3p41IO22ZuR8eqxlwE-0xjzonam4-mgKVpzPfJ33YIlnA/viewform" class="button ">Formulario</a></td>

                                        </tr>
                                        <tr>
                                            <td>3ER. AÑO</td>
                                            <td><a href="https://docs.google.com/forms/d/e/1FAIpQLSdNxhOFcnsu8Hm32acTNjq3m26z1GAoPw1fHCZEyPkEuWt4Qg/viewform" class="button ">Formulario</a></td>


                                        </tr>

                                    </tbody>
                                </table>
                            </div>

                        </section>



                        <section>

                            <div class="table-wrapper">
                                <table>
                                    <thead>
                                        <tr>
                                            <td>
                                                <h4>Solicitud Examen</h4>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Año 2021</td>
                                            <td><a href="https://forms.gle/obYCHDwMAvspuxzs8" class="button ">Formulario</a></td>
                                        </tr>
                                    </thead>

                                </table>
                            </div>

                        </section>--%>




                    </div>
                </div>


            </section>
        </article>

        <!-- Footer -->
        <footer id="footer">
            <ul class="icons">
                <%-- <li><a href="#" class="icon brands fa-twitter"><span class="label">Twitter</span></a></li>
                <li><a href="#" class="icon brands fa-facebook-f"><span class="label">Facebook</span></a></li>
                <li><a href="#" class="icon brands fa-instagram"><span class="label">Instagram</span></a></li>
                <li><a href="#" class="icon brands fa-dribbble"><span class="label">Dribbble</span></a></li>
                <li><a href="#" class="icon solid fa-envelope"><span class="label">Email</span></a></li>--%>
            </ul>
            <ul class="copyright">

                <%--	<li> </li><li> <a href="Login.aspx" ">Intranet</a></li>--%>
            </ul>
        </footer>
    </div>

    <!-- Scripts -->
    <script src="../assets/js/jquery.min.js"></script>
    <script src="../assets/js/jquery.scrollex.min.js"></script>
    <script src="../assets/js/jquery.scrolly.min.js"></script>
    <script src="../assets/js/browser.min.js"></script>
    <script src="../assets/js/breakpoints.min.js"></script>
    <script src="../assets/js/util.js"></script>
    <script src="../assets/js/main.js"></script>
</body>
</html>
