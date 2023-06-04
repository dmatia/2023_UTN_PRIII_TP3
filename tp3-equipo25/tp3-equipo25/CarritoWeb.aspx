<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CarritoWeb.aspx.cs" Inherits="tp3_equipo25.CarritoWeb" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Media querys--%>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="Content/CarritoStyle.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Carrito" ContentPlaceHolderID="Main" runat="server">
    <% if (ListaCarrito != null)
        {%>
    <div class="d-flex bd-highlight CarritoContenedor1">
        
    <div class="p-2 flex-fill bd-highlight contenedorMobile">
        <%--TITULO--%>
        <h2 class="CarritoWeb_titulo">Tu carrito</h2>
       
        <%--GRIDVIEW--%>
        <div class="contenedorGV table-responsive">


             <%--DROP DOWN LIST--%>

        <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                 <div>
            <asp:DropDownList ID="DDLOrdenar" CssClass="form-control" runat="server" OnSelectedIndexChanged="DDLOrdenar_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>

                <asp:GridView ID="dgvCarrito" runat="server" AutoGenerateColumns="false" OnRowDataBound="dgvCarrito_RowDataBound" CssClass="table">
                   <HeaderStyle HorizontalAlign="Center" BackColor="#a5d5e0" cssClass="celda" />
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" cssClass="celda"/>
                    <Columns>



                        <%--IMAGEN--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image runat="server"
                                    ImageUrl='<%#Eval("Articulo.Imagenes[0].UrlImagen") %>'
                                    onerror="this.src='https://www.freeiconspng.com/uploads/no-image-icon-23.jpg'"
                                    Style="width: 50px" 
                                    CommandName="Ver"/>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--NOMBRE--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button runat="server" Text='<%#Eval("Articulo.Nombre") %>' 
                                    OnClick="btnAcción" CommandName="Detalle" CssClass="btn CarritoWeb_dtb_detalle celda border-0" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- PRECIO--%>
                        <asp:BoundField HeaderText="Precio/Un." DataFormatString="{0:C}" DataField="Articulo.Precio" />

                        <%--BOTON AGREGAR--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnAgregar" runat="server" Text="+" 
                                    OnClick="btnAcción" CommandName="Agregar" CommandArgument='<%# Container.DataItemIndex %>' class="btn border-0" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--CANTIDAD--%>
                        <asp:BoundField DataField="Cantidad" />

                        <%--BOTON  QUITAR--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnQuitar" runat="server" Text="-" 
                                    OnClick="btnAcción" CommandName="Quitar" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn border-0"/>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- SUBTOTAL --%>
                        <asp:TemplateField HeaderText="Subtotal">
                            <ItemTemplate>
                                <asp:Label ID="lblSubtotal" runat="server" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--ELIMINAR DEL CARRITO--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEliminar" runat="server" Text="🗑" 
                                     OnClick="btnAcción" CommandName="Borrar" CommandArgument='<%# Container.DataItemIndex %>' class="btn border-0"/>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- VER --%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnVer" runat="server" Text="👁"
                                    OnClick="btnAcción" CommandName="Ver"  CommandArgument='<%# Container.DataItemIndex %>' class="btn border-0"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                   

                    </Columns>
                </asp:GridView>

            </ContentTemplate>
        </asp:UpdatePanel>
       </div>
       <%-- CUPÓN --%>
        <div class="cuponContenedor">
            <asp:TextBox runat="server" ID="txbxCupon" CssClass="cuponIzq btnCarrito" type="text" placeholder="¿Tenés un cupón? ¡Ingresalo acá!" aria-label="Search" />
            <asp:Button runat="server" ID="btnCupon" CssClass="cuponDer"  OnClick="btnAplicar_Click" Text="Aplicar" />
        </div>
        <%-- BOTONES --%>
        <div class="botonera">
            <asp:Button ID="btnBorrarCarrito" runat="server" CssClass="btn btn-danger btnCarrito" 
                Text="🗑" OnClick="btnBorrarCarrito_Click"/>
            <a href="Default.aspx" class="btn btn-primary btnCarrito ">Guardar</a>
            <a href="OrdenCompra.aspx" class="btn btn-primary">Check out</a>
        </div>
    </div>

        <div class="contenedor2">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Image ID="Image2" runat="server" ImageUrl='<%#Eval("ListaCarrito.Articulo.Imagenes[0].UrlImagen")%>' onerror="this.src='https://d3ugyf2ht6aenh.cloudfront.net/stores/872/502/products/carro-compras-111-51d754b8f31ee398d316701805488150-640-0.webp'" />
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
