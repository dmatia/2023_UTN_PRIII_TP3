<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CarritoWeb.aspx.cs" Inherits="tp3_equipo25.CarritoWeb" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Carrito" ContentPlaceHolderID="Main" runat="server">
    <% if (ListaCarrito != null)
        {%>



    <div class="d-flex bd-highlight">

       <%-- Contenedor Izquierdo, contiene GridView y botones--%>
        <div class="p-2 flex-fill bd-highlight">
            <%-- Titulo --%>
            <h2>Tu carrito</h2>

            <asp:GridView ID="dgvCarrito" runat="server" CssClass="table Carrito" DataKeyNames=""
                AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound="GridView1_RowDataBound" ClientIDMode="AutoID">
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="WHITE" />
                <HeaderStyle HorizontalAlign="Center" BackColor="WHITE" />

                <Columns>

                    <%--Imagen en columna--%>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("Articulo.Imagenes[0].UrlImagen")%>' Style="height: 50px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--Nombre artículo con link al detalle--%>

                    <asp:TemplateField HeaderText="Articulo">
                        <ItemTemplate>
                            <asp:Button runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Articulo.Nombre") %>' CssClass="btn" OnClick="btnAcción"  CommandName="Detalle" CommandArgument="Articulo.Id" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- Precio Unitario --%>
                    <asp:BoundField HeaderText="Precio/Un." DataFormatString="{0:C}" DataField="Articulo.Precio" />


                    <%-- Controles para agregar/quitar unidades --%>
                    <asp:TemplateField HeaderText="Agregar/Quitar">
                        <ItemTemplate>
                            <asp:Button ID="btnAgregar" runat="server" Text="+" CssClass="btn" OnClick="btnAcción"  CommandName="Agregar" CommandArgument="Articulo.Id" CausesValidation="false" />
                            <asp:Button ID="btnQuitar" runat="server" Text="-" CssClass="btn" OnClick="btnAcción" CommandName="Quitar" CommandArgument="Articulo.Id" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- Cantidad de unidades por artículo--%>
                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />


                    <%-- Subtotal por artículo --%>
                    <asp:TemplateField HeaderText="Subtotal">
                        <ItemTemplate>
                            <asp:Label ID="lblSubtotal" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- Eliminar artículo del carrito --%>
                    <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:Button ID="bntBorrar" runat="server" Text="🗑" CssClass="btn"  OnClick="btnAcción"  CommandName="Borrar" CommandArgument="Articulo.Id" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- Seleccionar artículo para ver en grande --%>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Button ID="bntVer" runat="server" Text="👁" CssClass="btn" OnClick="btnAcción"  CommandName="Ver" CommandArgument="Articulo.Id" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

           <%-- Caja de botones bajo la GridView --%>
            <div class="d-flex flex-row bd-highlight mb-3">
                <div class=" p-2 bd-highlight">
                    <asp:Button ID="btnBorrarCarrito" runat="server" CssClass="btn btn-outline-danger btn-lg" Text="🗑" OnClick="btnBorrarCarrito_Click" />
                </div>
                <div class="p-2 bd-highlight">
                    <a href="Default.aspx" class="btn btn-primary btn-lg ">GUARDAR</a>
                </div>
                <div class=" p-2 bd-highlight">
                    <a href="OrdenCompra.aspx" class="btn btn-primary btn-lg">HACER CHECK OUT</a>
                </div>

            </div>

        </div>




        <%--   Contenedor derecho, contiene un visor de imagenes --%>
        <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
        <div class="p-2 flex-fill bd-highlight" style="width: 50vw">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Image ID="Image2" runat="server" ImageUrl='<%#Eval("ListaCarrito.Articulo.Imagenes[0].UrlImagen")%>' Style="width: 100%" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>





    <%}
        else
        {%>

    <div class="sinCarrito">
        <a class="sinCarritoTitulo">Tu carrito está vacio... </a>
        <div>
            <a href="Default.aspx" class="btn btn-primary sinCarritoBoton">ver catalogo</a>
        </div>
    </div>

    <%}%>
</asp:Content>
