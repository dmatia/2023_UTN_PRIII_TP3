<%@ Page Title="Detalle" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="tp3_equipo25.Detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%--Main--%>
<asp:Content ID="Detalle" ContentPlaceHolderID="Main" runat="server">
    <div class="d-flex justify-content-center">

        <% if (articulo != null)
           {%>

                <%-- Detalle --%>
                <div class="row row-cols-2 w-50">
                    <div>
                        <h2><%= articulo.Nombre%></h2>
                        <p><%= articulo.Descripcion%></p>
                        <p><%= articulo.Categoria%></p>
                        <p><%= articulo.Marca%></p>
                        <p><%= articulo.Precio%></p>

                        <asp:TextBox runat="server" Type="number" ID="tb_cantidad" min="0" name="quantity" value="1"
                            class="form-control form-control-sm w-50" />

                        <asp:Button runat="server" CssClass="btn btn-primary mt-3" OnClick="AgregarCarrito" Text="Agregar a carrito" />
                    </div>
                    <%-- Detalle --%>

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
                            <div class="carousel-item active">
                                <img class="d-block w-100" src="<%= img.UrlImagen%>">
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
                </div>
                <%--Carousel--%>

        <%}
         else
        {%>

            <h2>NO HAY ARTICULOS</h2>

        <%}%>

    </div>
</asp:Content>
<%--Fin Main--%>
