<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="OrdenCompra.aspx.cs" Inherits="tp3_equipo25.OrdenCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <asp:ScriptManager runat="server" ID="SM_Detalle"></asp:ScriptManager>

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
                    <hr class="hr my-3" />
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="D-flex flex-column">
                                <div class="mt-2">
                                    <asp:Label runat="server" class="text-success" ID="lbModoEnvio"></asp:Label>
                                </div>
                                <div class="mt-2">
                                    <p>Pagas con <asp:Label CssClass="fw-semibold" runat="server" class="text-success" ID="lbModoPago"> </asp:Label></p>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <p class="fw-semibold mt-2">A pagar: <%= precioFinal.ToString("c")%></p>

                </div>
                <div class="p-4 d-flex justify-content-center">
                    <asp:Button OnClick="Comprar_Click" Text="Finalizar" runat="server" CssClass="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal -->

    <!-- Orden de Compra -->
    <div class="d-flex justify-content-center bg-body shadow-sm rounded p-3">
        <div class="shadow-sm rounded p-3 w-50">
            <h2 class="text-center my-3">Orden de Compra</h2>
            <p class="text-center fw-semibold fs-3 mb-5">Venta #<%= numeroCompra %></p>
            <!-- Detalle -->
            <asp:Repeater ID="repeaterOrdenCompra" runat="server">
                <ItemTemplate>
                    <div class="row row-cols-3">
                        <p class="fw-semibold"><%# Eval("Articulo.Nombre") %></p>
                        <p class="text-center">Cantidad: <%# Eval("Cantidad") %></p>
                        <p class="text-end"><%# String.Format("{0:C}", Eval("Articulo.Precio"))%></p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <hr class="hr" />
            <p class="fs-5 text-end">Total a pagar: <span class="fw-semibold"><%= precioFinal.ToString("c")%></span></p>
            <!-- Detalle -->
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <h5 class="text-muted">Modo de Pago</h5>
                    <asp:DropDownList runat="server" ID="modoPago" CssClass="btn btn-primary" OnSelectedIndexChanged="modoPago_Change" AutoPostBack="true"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <h5 class="text-muted mt-3">Modo de Envío</h5>
                    <asp:DropDownList runat="server" ID="modoEnvio" CssClass="btn btn-primary" OnSelectedIndexChanged="modoEnvio_Change" AutoPostBack="true"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="d-flex justify-content-center mt-5">
                <a class="btn btn-success" data-bs-toggle="modal" data-bs-target="#exampleModal">Finalizar compra </a>
            </div>
        </div>
    </div>
    <!-- Orden de Compra -->
</asp:Content>

