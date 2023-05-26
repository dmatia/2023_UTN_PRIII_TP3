<%@ Page Title="Detalle" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="tp3_equipo25.Detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%--Main--%>
<asp:Content ID="Detalle" ContentPlaceHolderID="Main" runat="server">
    <div class="d-flex justify-content-center">

        <% if (articulo != null)
            {%>

        <div class="row row-cols-md-3 row-cols-1 w-75">

            <%--Carousel--%>
            <div id="carouselExampleIndicators" class="carousel slide">
                <div class="carousel-indicators">
                    <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
                    <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="1" aria-label="Slide 2"></button>
                    <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="2" aria-label="Slide 3"></button>
                </div>
                <div class="carousel-inner">

                    <%@ Import Namespace="Dominio" %>
                    <% foreach (Imagen img in articulo.Imagenes)
                        {%>
                    <div class="carousel-item active rounded border p-1">
                        <img class="d-block w-100 rounded" src="<%= img.UrlImagen%>">
                    </div>
                    <%} %>
                </div>
                <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Previous</span>
                </button>
                <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Next</span>
                </button>
            </div>
            <%--Carousel--%>


            <%-- Detalle --%>
            <div>
                <div class="d-flex text-secondary fw-light flex-wrap">
                    <p class="me-1">Nuevo | </p>
                    <p class="me-1"><%= articulo.Categoria%></p>
                    <p class="me-1">> </p>
                    <p><%= articulo.Marca%></p>
                </div>
                <h2 class="fs-4"><%= articulo.Nombre%></h2>
                <p class="fs-2 fw-light"><%= articulo.Precio.ToString("c")%></p>
                <p class="badge text-bg-primary fw-normal text-uppercase"> <small>oferta del día</small></p>
                <p class="fs-6 text-muted"><%= articulo.Descripcion%></p>
            </div>
            <%-- Detalle --%>

            <%-- Button --%>
            <div class="border border-muted rounded p-3">
                <div class=" fs-6">
                    <p class="small text text-success"><i class="fa-solid fa-truck me-2"></i>Llega gratis <span class="fw-semibold">el <%= dia.ToString("dddd") %></span></p>
                    <p class="fs-6 fw-semibold">Stock disponible</p>
                    <p class="small fw-light">Tienda oficial <a href="Nosotros.aspx" class="text-decoration-none">Grupo 25</a></p>
                    <div class="input-group input-group-sm mb-3">
                        <span class="fs-6 d-flex align-items-center me-2">Cantidad:</span>
                        <asp:TextBox runat="server" Type="number" ID="tb_cantidad" min="0" name="quantity" value="1"
                            CssClass="form-control fw-semibold border-0  w-50" />
                    </div>
                </div>
                <div class="d-grid">
                    <asp:Button runat="server" CssClass="btn btn-primary btn-block fs-6 text-uppercase" OnClick="AgregarCarrito" Text="Agregar al carrito" />
                </div>
            </div>
            <%-- Button --%>
        </div>

        <%}
            else
            {%>

        <h2>NO HAY ARTICULOS</h2>

        <%}%>
    </div>
</asp:Content>
<%--Fin Main--%>
