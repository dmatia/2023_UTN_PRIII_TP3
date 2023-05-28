<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Nav.ascx.cs" Inherits="tp3_equipo25.Layouts.Nav" %>

<%--Nav--%>

<nav class="navbar navbar-expand-lg bg-body-tertiary">
    <div class="container container-fluid">
        <a class="navbar-brand" href="Default.aspx">Store</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav me-auto mb-2 mb-lg-0">
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
<%--            <div class="d-flex" role="search">
                <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
                <button class="btn btn-outline-success" type="submit">Search</button>
            </div>--%>
            <asp:LinkButton runat="server" class="nav-link text-bg-secondary p-2 rounded-2" href="CarritoWeb.aspx">
                    <i class="fa-solid fa-cart-shopping mx-1"></i>
                    <span class="mx-1 badge bg-transparent rounded-pill"><%: carrito %></span>
            </asp:LinkButton>
        </div>
    </div>
</nav>

<%--Fin Nav--%>