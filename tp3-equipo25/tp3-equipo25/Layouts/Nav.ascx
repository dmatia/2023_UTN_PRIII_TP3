<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Nav.ascx.cs" Inherits="tp3_equipo25.Layouts.Nav" %>

<%--Nav--%>

<nav class="navbar navbar-expand-lg bg-body-tertiary" style="box-shadow: 0 0 1px 1px rgba(0, 0, 0, 0.2);">
    <div class="container container-fluid">
        <a class="navbar-brand d-flex align-items-center" href="Default.aspx"><i class="fa-solid fa-store fs-3 me-2 pe-2 border-2 border-end border-dark"></i><span><span class="fs-4 me-1">Grupo</span><span class="fs-4 fw-bold" >25</span></span></a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent" ">

            <ul class="navbar-nav me-auto mb-2 mb-lg-0" ">
                <li class="nav-item">
                    <a class="nav-link active" aria-current="page" href="Default.aspx">Inicio</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link active" aria-current="page" href="CarritoWeb.aspx">Carrito</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link active" aria-current="page" href="Nosotros.aspx">Nosotros</a>
                </li>
            </ul>

            <!-- Muestrta buscador en todas las páginas, menos en la prinicipal -->
            <% if (HttpContext.Current.Request.Url.AbsolutePath.ToLower() != "/default.aspx")
                { %>
            <div class="d-flex me-3 mb-2 mb-lg-0" role="search">
                <asp:TextBox runat="server" ID="lbSearch" CssClass="form-control me-2" Width="400px" type="text" placeholder="Buscar por producto, marcas, categoria..." aria-label="Search" />
                <asp:Button runat="server" CssClass="btn btn-outline-primary" OnClick="BtnBusquedaRapida_Click" Text="Buscar" />
            </div>
            <%} %>

            <asp:LinkButton runat="server" class="nav-link " href="CarritoWeb.aspx">
                <div class="text-bg-secondary p-2 rounded-2">
                    <i class="fa-solid fa-cart-shopping mx-1"></i>
                    <span class="mx-1 badge bg-transparent rounded-pill"><%: carrito %></span>
                    </div>
            </asp:LinkButton>

        </div>
    </div>
</nav>

<%--Fin Nav--%>