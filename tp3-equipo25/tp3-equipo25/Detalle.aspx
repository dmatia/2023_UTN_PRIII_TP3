<%@ Page Title="Detalle" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="tp3_equipo25.Detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%--Main--%>
<asp:Content ID="Detalle" ContentPlaceHolderID="Main" runat="server">
    <div class="d-flex justify-content-center bg-body shadow-sm rounded p-3">

        <% if (articulo != null)
            {%>

        <div class="row row-cols-md-3 row-cols-1 w-75">

            <%--Carousel--%>
            <div id="carouselExampleControls" class="carousel slide" data-bs-ride="carousel">
                <div class="carousel-inner">

                    <% foreach (Dominio.Imagen img in articulo.Imagenes)
                        {%>
                    <div class="carousel-item active rounded border p-1">
                        <img class="d-block w-100 rounded" src="<%= img.UrlImagen%>">
                    </div>
                    <%} %>

                </div>
                <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Previous</span>
                </button>
                <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Next</span>
                </button>
            </div>
        <%-- Fin Carousel--%>


            <%-- Detalle --%>
            <div>
                <div class="d-flex text-secondary fw-light flex-wrap">
                    <p class="me-1">Nuevo | </p>
                    <p class="me-1"> <%= articulo.Categoria%> </p>
                    <p class="me-1">> </p>
                    <p><%= articulo.Marca%></p>
                </div>
                <h2 class="fs-4"><%= articulo.Nombre%></h2>
                <p class="small fw-light lh-1"><s><%= (articulo.Precio * 2).ToString("c")%></s></p>
                <div class="d-flex">
                    <p class="fs-2 fw-light lh-1"><%= articulo.Precio.ToString("c")%></p>
                    <small class="text-green text-success ms-1">50% OFF</small>
                </div>
                <p class="badge text-bg-primary fw-normal text-uppercase"><small>oferta del día</small></p>
                <p class="fw-semibold small mt-3">Lo que tenés que saber de este producto</p>
                <p class="fs-6 text-muted"><%= articulo.Descripcion%></p>
            </div>
            <%-- Fin Detalle --%>

            <%-- Button --%>
            <div>

                <div class="border border-muted rounded p-3 row">

                    <div class="row fs-6">
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

                <div class="border border-muted rounded p-3 mt-3 row gap-2">

                    <p class="fs-5 fw-semibold">Medios de Pago</p>

                    <h6 class="fw-light">Pago Electrónico</h6>
                    <div class="d-flex gap-1 fs-2 text-muted">
                        <i class="fa-brands fa-cc-stripe"></i>
                        <i class="fa-brands fa-cc-paypal"></i>
                        <i class="fa-brands fa-cc-apple-pay"></i>
                        <i class="fa-brands fa-cc-amazon-pay"></i>
                    </div>

                    <h6 class="fw-light">Tarjetas de Crédito</h6>
                    <div class="d-flex gap-1 fs-2 text-muted">
                        <i class="fa-brands fa-cc-mastercard"></i>
                        <i class="fa-brands fa-cc-visa"></i>
                        <i class="fa-brands fa-cc-jcb"></i>
                        <i class="fa-brands fa-cc-amex"></i>
                        <i class="fa-brands fa-cc-diners-club"></i>
                    </div>

                </div>
            </div>
            <%-- Fin Button --%>
        </div>

         <%-- No hay articulos --%>
        <%}
            else
            {%>

        <h2>NO HAY ARTICULOS</h2>

        <%}%>
    </div>
     <%-- Fin No hay articulos --%>

    <!-- Articulos Relacionados -->
    <div class="my-3">
        <h5 class="text-muted">También te puede interesar</h5>
    </div>

    <div class="bg-body shadow-sm hover-shadow p-3 rounded w-100" style="height: auto">
        <div class=" d-flex gap-3 flex-sm-row flex-column justify-content-center justify-content-lg-start w-100">
            <asp:Repeater ID="repeaterArticulosRelacionados" runat="server">
                <ItemTemplate>
                    <%-- Cards Relacionados --%>
                    <div class="card rounded shadow-sm" style="width: 15rem; height:auto">
                        <div class="">
                            <img src="<%# Eval("Imagenes[0].UrlImagen") %>" class=" w-100">
                        </div>
                        <div class="card-body">
                            <p class="fs-4 fw-light"><%# String.Format("{0:C}", Eval("Precio"))%></p>
                            <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            <p class="card-text"><%#  Eval("Descripcion") %></p>
                            <asp:Button Text="Detalle" CssClass="btn btn-primary" runat="server" ID="BtnDetalle" CommandArgument='<%#Eval("Id")%>' CommandName="ArticuloId" OnClick="BtnDetalle_Click" />
                        </div>
                    </div>
                    <%-- Cards Relacionados --%>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <!-- Fin Articulos Relacionados -->
</asp:Content>
<%--Fin Main--%>
