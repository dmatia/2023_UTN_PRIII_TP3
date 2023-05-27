<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="OrdenCompra.aspx.cs" Inherits="tp3_equipo25.OrdenCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="p-4 d-flex justify-content-between">
                    <h5 class="modal-title" id="exampleModalLabel">¡Tu Compra ha sido Exitosa!</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="p-5">
                    <div class="d-flex justify-content-center ">
                        <div class="bg-success rounded-circle p-3">
                            <i class="fa-solid fa-check text-light fs-1"></i>
                        </div>
                    </div>
                    <p class="text-center fw-semibold fs-3 mt-3">Venta #<%= numeroCompra %></p>
                    <p class="text-success">Envío gratis.</p>
                    <p class="fw-light">Llega el <%= DateTime.Today.ToString("dddd") %></p>
                    <p class="fw-semibold">Total a pagar: <%= precioFinal.ToString("c")%></p>
                </div>
                <div class="p-4 d-flex justify-content-center">
                    <asp:Button OnClick="Comprar_Click" Text="Finalizar" runat="server" CssClass="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal" />
                </div>
            </div>
        </div>
    </div>

    <!-- Orden de Compra -->
    <div class="d-flex justify-content-center bg-body shadow-sm rounded p-3">
        <div class="shadow-sm rounded p-3 w-50">
            <h2 class="text-center my-3">Orden de Compra</h2>
            <p class="text-center fw-semibold fs-3">Venta #<%= numeroCompra %></p>
            <asp:Repeater ID="repeaterOrdenCompra" runat="server">
                <ItemTemplate>
                    <div class="d-flex justify-content-between gap-2 my-5">
                        <p class="fw-semibold"><%# Eval("Articulo.Nombre") %></p>
                        <p><%# String.Format("{0:C}", Eval("Articulo.Precio"))%></p>
                        <p>Cantidad: <%# Eval("Cantidad") %></p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <p class="text-success">Envío gratis. Llega <span class="fw-semibold">el <%= DateTime.Today.ToString("dddd") %></span></p>
            <p class="fs-4">Total a pagar: <span class="fw-semibold"><%= precioFinal.ToString("c")%></span></p>

            <div class="d-flex justify-content-center">
            <a class="btn btn-success" data-bs-toggle="modal" data-bs-target="#exampleModal">Finalizar compra </a>
            </div>
        </div>
    </div>
</asp:Content>

