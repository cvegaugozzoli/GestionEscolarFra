<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index1.aspx.cs" Inherits="PaginasBasicas_Index1" %>

<!DOCTYPE html>
<html lang="en" class=" js no-touch">
<head runat="server">
    <title>Hermanos de la Misericordia</title>
    <link rel="stylesheet" href="../assets/css/noscript.css" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <link rel="stylesheet" href="../assets/css/main.css" />
    <link rel="shortcut icon" href="../images/LogoNuevoAso.jpg" />
    <noscript>
    </noscript>
    <style type="text/css">
        .auto-style1 {
            width: 90%;
            height: 303px;
        }

        .auto-style4 {
            left: 0px;
            top: 34px;
            height: 0px;
        }
    </style>
</head>
<body class="landing is-preload">

    <!-- Page Wrapper -->
    <div id="page-wrapper">

        <!-- Header -->
        <header id="header" class="alt">
            <h1><a href="index1.html">Hermanos de la Misericordia</a></h1>
            <nav id="nav">
                <ul>
                    <li class="special">
                        <a href="#menu" class="menuToggle"><span>Menu</span></a>
                        <div id="menu">
                            <ul>
                                <li><a href="Index1.aspx">Inicio</a></li>
                                <li><a href="#one">Misión</a></li>
                                <li><a href="#two">Instituciones</a></li>


                            </ul>
                        </div>
                    </li>
                </ul>
            </nav>
        </header>

        <!-- Banner -->
        <section id="banner">
            <div class="inner">
                <h2>Asociación Civil de Hermanos Misericordistas</h2>
                <p>"EL HONOR A DIOS, EL TRABAJO PARA MI, EL PROVECHO PARA EL PRÓJIMO"</p>

            </div>
            <a href="#one" class="more scrolly">Leer Más</a>
        </section>

        <!-- One -->
        <section id="one" class="wrapper style2" style="text-align: center">
            <div class="inner">
                <header class="major">
                    <h2 aling="center">Nosotros</h2>
                    <p>
                        La Obra de los Hermanos de Nuestra Señora de la
Misericordia en Santiago del Estero (Argentina) es la más
extensa en Latino América. Tuvo sus orígenes en el año 1933
cuando los primeros Hermanos provenientes de Europa
arribaron a estas tierras. Desde entonces unidos, hermanos y
laicos hemos desandado un largo camino encarnando el
carisma del Fundador Víctor Scheppers en el servicio a los
más pobres y necesitados desde la acción social y la
educación.
                    </p>
                </header>
                <%--	<ul class="icons major">
								<li><span class="icon fa-gem major style1"><span class="label">Lorem</span></span></li>
								<li><span class="icon fa-heart major style2"><span class="label">Ipsum</span></span></li>
								<li><span class="icon solid fa-code major style3"><span class="label">Dolor</span></span></li>
							</ul>--%>
            </div>
        </section>

        <!-- Two -->
        <section id="two" class="wrapper alt style2 ">
            <section class="spotlight">
                <div class="image">
                    <img src="../images/whh01DTQ.jpg" alt="" class="auto-style1" />
                </div>
                <div class="content">
                    <h2>Colegio San José</h2>
                    <p>El Colegio San José, nació como Institución Educativa en 1933. En la actualidad ofrece las siguientes niveles educativos: <%-- Primario -  Secundario -  Superior--%>.</p>
                    <ul class="icons">
                        <li><a href="Primario.aspx" class="button">Primario</a>
                        <li><a href="Secundario.aspx" class="button">Secundario</a>
                        <li><a href="Profesorado.aspx" class="button">Superior</a>
                     <%--   <li><a href="InscripcionActualizar.aspx?Inst=1" class="button">Inscripción</a>--%>
                    </ul>

                    <%--  <ul class="icons">    --%>                   <%--                        <li><a href="../PaginasBasicas/ContactoSanJose.aspx" class="icon solid fa-envelope">&nbsp;Contacto</a>
                              <li><a href="../PaginasBasicas/RequisitosSJ.aspx" class="icon solid fa-edit">&nbsp;Requisitos Pre-inscripción 1° Grado</a>
                              <li><a href="../Documento/FLAYERSJ.pdf" class="icon solid fa-edit">&nbsp;Requisitos Inscripción 2° a 7° Grado</a>
                                                               <li><a href="../Documento/REQUISITOSNIVELSUP.pdf" class="icon solid fa-university">&nbsp;Requisitos Inscripción Nivel Superior</a>
                                                                  
                              <li><a href="../PaginasBasicas/Profesorado.aspx" class="icon solid fa-university">&nbsp; Oferta Académica Nivel Superior</a>--%>

                    <%--   <li><a href="#" class="icon solid fa-phone">&nbsp;0385-4211579/4240161</a></li>--%>
                    <%--  </ul>--%>
                    <a href="../PaginasBasicas/ContactoSanJose.aspx" class="icon solid fa-envelope">&nbsp;Contacto</a>
                </div>
            </section>
            <section class="spotlight">
                <div class="image">
                    <img src="../images/mwtwpuRA.jpeg" alt="" />
                </div>
                <div class="content">
                    <h2>Colegio Misericordia</h2>
                    <p>
                        En el colegio Misericordia, cada alumno mediante el trabajo colaborativo
y el acompañamiento del docente, va construyendo su trayectoria
escolar. La propuesta educativa se materializa en proyectos y
secuencias didácticas destinadas a desarrollar un pensamiento
crítico, reflexivo y creativo, haciendo hincapié sobre todo en la
formación de valores inspirados en el carisma misericordista. Ofrece a la sociedad el nivel Primario.
                    </p>

                    <ul class="icons">
                    <%--    <li><a href="PrimarioMisericordia.aspx" class="button">Ingresar</a>--%>
                        <li><a href="InscripcionActualizar.aspx?Inst=2" class="button">Inscripción</a>
<%--                        <li><a href="Profesorado.aspx" class="button">Superior</a>--%>
                    </ul>
                    <ul class="icons">
                        <li><a href="../PaginasBasicas/ContactoMisericordia.aspx" class="icon solid fa-envelope">&nbsp;Contacto</a>
                    </ul>
                 
                </div>

            </section>
            <section class="spotlight">
                <div class="image">
                    <img src="../images/JardinMisericordiaF.jpg" alt="" />
                </div>
                <div class="content">
                    <h2>Jardín Misericordia</h2>
                    <p>En el Jardín Misericordia llevamos a cabo actividades educativa respetando las necesidades lúdicas de experimentación y expresión, asegurando una enseñanza de conocimientos significativos que amplíen y profundicen los aprendizajes. A disposición de la sociedad el nivel Inicial.</p>
                    <ul class="icons">
                       <%-- <li><a href="InicialJMisericordia.aspx" class="button">Ingresar</a>--%>
                        <li><a href="InscripcionActualizar.aspx?Inst=4" class="button">Inscripción</a>
                    </ul>
                    <ul class="icons">
                        <li><a href="../PaginasBasicas/ContactoJardinMisericordia.aspx" class="icon solid fa-envelope">&nbsp;Contacto</a></li>
                    </ul>

                    <%--  <ul class="icons">
                        <li><a href="../PaginasBasicas/RequisitosJM.aspx" class="icon solid fa-edit">&nbsp;Requisitos Pre-inscripción</a>   </li>
                        <li><a href="../Documento/Flayer.pdf" class="icon solid fa-edit">&nbsp;pdf</a></li>
                    </ul>--%>



                </div>
            </section>
            <section class="spotlight">
                <div class="image">
                    <img src="../images/PadreVICTOR.jpeg" alt="" />
                </div>
                <div class="content">
                    <h2>Jardín Padre Victor</h2>
                    <p>El Jardín Padre Victor en su nivel Inicial busca promover el juego como contenido de alto valor cultural para el desarrollo cognitivo, afectivo, ético, motor y social. Fomentar la socialización, a partir de los valores.</p>
                    <ul class="icons">
                        <li><a href="InicialJPadreVictor.aspx" class="button">Ingresar</a>
                        <%--<li><a href="InscripcionActualizar.aspx?Inst=5" class="button">Inscripción</a>--%>
                    </ul>
                    <ul class="icons">
                        <li><a href="../PaginasBasicas/ContactoPadreVictor.aspx" class="icon solid fa-envelope">&nbsp;Contacto</a>
                    </ul>


                    <%--  <ul class="icons">
                        <li><a href="../PaginasBasicas/RequisitosJPV.aspx" class="icon solid fa-edit">&nbsp;Requisitos Pre-inscripción</a>
                        <li><a href="../Documento/PLANILLAPEINSCRIPCION.pdf" class="icon solid fa-edit">&nbsp;Formulario</a></li>
                    </ul>--%>
                </div>
            </section>
            <section class="spotlight">
                <div class="image">
                    <img src="../images/SanVicenteF.jpeg" alt="" />
                </div>
                <div class="content">
                    <h2>Escuela San Vicente</h2>
                    <p>La finalidad de la Escuela San Vicente es facilitar a los alumnos y las alumnas los aprendizajes de la expresión y comprensión oral, la lectura, la escritura, el cálculo y el hábito de estudio y trabajo, con el fin de garantizar una formación integral que contribuya al pleno desarrollo de la personalidad de los alumnos y las alumnas y de prepararlos para cursar con aprovechamiento la Educación Secundaria Obligatoria.</p>
                    <%-- <img src="../images/whatsapp.png"  width="5%" />&nbsp; 3854666777  --%>
                    <ul class="icons">
                        <li><a href="PrimarioSanVicente.aspx" class="button">Ingresar</a>
                        <li><%--<a href="InscripcionActualizar.aspx?Inst=3" class="button">Inscripción</a>--%>
                    </ul>
                    <ul class="icons">
                    
                        <li><a href="../PaginasBasicas/ContactoSanVicente.aspx" class="icon solid fa-envelope">&nbsp;Contacto</a></li>
                    </ul>
                  
                    <%-- <ul class="icons">
                        <li><a href="../PaginasBasicas/RequisitosSV.aspx" class="icon solid fa-edit">&nbsp;Requisitos Pre-inscripción</a>
                    </ul>--%>
                </div>
            </section>
        </section>



        <!-- CTA -->
        <article id="main">
            <header>

                <%-- <section id="header2"   style="text-align: center; " class="auto-style4" >  --%>
                <%--  <h3> "EL HONOR A DIOS, EL TRABAJO PARA MI, EL PROVECHO PARA EL PRÓJIMO"</h3>
                    <p> &nbsp;VICTOR SCHEPPERS</p>  --%>
            </header>
        </article>

        <!-- Footer -->
        <footer id="footer">
            <ul class="icons">
                <%-- <li><a href="#" class="icon brands fa-twitter"><span class="label">Twitter</span></a></li>--%>

                <%--   <li><a href="#" class="icon brands fa-facebook"><span class="label">Facebook</span></a></li>--%>
                <%--     <li><a href="#" class="icon brands fa-dribbble"><span class="label">Dribbble</span></a></li>--%>
                <%--  <li><a href="#" class="icon solid fa-envelope"><span class="label">Email</span></a></li>--%>
            </ul>
            <div class="copyright">
<a href="ChequeraBarra.aspx">Chequera</a>
                <a href="Login.aspx?IdPage=1">Intranet</a>
            </div>
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
