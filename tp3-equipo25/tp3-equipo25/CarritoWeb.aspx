<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CarritoWeb.aspx.cs" Inherits="tp3_equipo25.CarritoWeb" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Carrito" ContentPlaceHolderID="Main" runat="server">
    <% if (ListaCarrito != null)
        {%>



    <div class="d-flex bd-highlight CarritoContenedor1">

        <%-- Contenedor Izquierdo, contiene GridView y botones--%>
        <div class="p-2 flex-fill bd-highlight">
            <%-- Titulo --%>
            <h2 class="CarritoWeb_titulo">Tu carrito</h2>

            <div>
                <asp:DropDownList ID="DDLOrdenar" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLOrdenar_SelectedIndexChanged"></asp:DropDownList>
            </div>


            <div class="contenedorGV">

            <asp:GridView ID="dgvCarrito" runat="server" CssClass="table" DataKeyNames=""
                AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound="GridView1_RowDataBound" ClientIDMode="AutoID">
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle"  cssClass="celda"/>
                <HeaderStyle HorizontalAlign="Center" BackColor="WHITE" cssClass="celda" />

                <Columns>

                    <%--Imagen en columna--%>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("Articulo.Imagenes[0].UrlImagen")%>' onerror="this.src='https://www.freeiconspng.com/uploads/no-image-icon-23.jpg'" Style="height: 50px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--Nombre artículo con link al detalle--%>

                    <asp:TemplateField HeaderText="Articulo">
                        <ItemTemplate>
                            <asp:Button runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Articulo.Nombre") %>' CssClass="btn CarritoWeb_dtb_detalle" OnClick="btnAcción" CommandName="Detalle" CommandArgument="Articulo.Id" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- Precio Unitario --%>
                    <asp:BoundField HeaderText="Precio/Un." DataFormatString="{0:C}" DataField="Articulo.Precio" />


                    <%-- Controles para agregar/quitar unidades --%>
                    <asp:TemplateField HeaderText="Agregar/Quitar">
                        <ItemTemplate>
                            <asp:Button ID="btnAgregar" runat="server" Text="+" CssClass="btn" OnClick="btnAcción" CommandName="Agregar" CommandArgument="Articulo.Id" CausesValidation="false" />
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
                            <asp:Button ID="bntBorrar" runat="server" Text="🗑" CssClass="btn" OnClick="btnAcción" CommandName="Borrar" CommandArgument="Articulo.Id" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- Seleccionar artículo para ver en grande --%>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Button ID="bntVer" runat="server" Text="👁" CssClass="btn" OnClick="btnAcción" CommandName="Ver" CommandArgument="Articulo.Id" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
            </div>

            <div class="cuponContenedor">
                    <asp:TextBox runat="server" ID="txbxCupon" CssClass="cuponIzq btnCarrito" type="text" placeholder="¿Tenés un cupón? ¡Ingresalo acá!" aria-label="Search" />
                    <asp:Button runat="server" ID="btnCupon" CssClass="cuponDer" OnClick="btnAplicar_Click" Text="Aplicar" />
                </div>

            <%-- Caja de botones bajo la GridView --%>
            <div class="botonera">
                    <asp:Button ID="btnBorrarCarrito" runat="server" CssClass="btn btn-danger btnCarrito" Text="🗑" OnClick="btnBorrarCarrito_Click" />
                    <a href="Default.aspx" class="btn btn-primary btn-lg btnCarrito ">GUARDAR</a>
                    <a href="OrdenCompra.aspx" class="btn btn-primary btn-lg">HACER CHECK OUT</a>
            </div>

        </div>




        <%--   Contenedor derecho, contiene un visor de imagenes --%>
        <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
        <div class="p-2 flex-fill bd-highlight contenedor2" style="width: 50vw">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Image ID="Image2" runat="server" ImageUrl='<%#Eval("ListaCarrito.Articulo.Imagenes[0].UrlImagen")%>' onerror="this.src='https://d3ugyf2ht6aenh.cloudfront.net/stores/872/502/products/carro-compras-111-51d754b8f31ee398d316701805488150-640-0.webp'" Style="width: 100%;" />
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
