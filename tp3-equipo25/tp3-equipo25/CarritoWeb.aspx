<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CarritoWeb.aspx.cs" Inherits="tp3_equipo25.CarritoWeb" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Carrito" ContentPlaceHolderID="Main" runat="server" >
    <% if (ListaCarrito != null)
        {%>



    <div class="d-flex bd-highlight">
        <div class="p-2 flex-fill bd-highlight">
            <h2>Tu carrito</h2>

            <asp:GridView ID="dgvCarrito" runat="server" CssClass="table Carrito" DataKeyNames=""
                AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound="GridView1_RowDataBound"
                ClientIDMode="AutoID">


                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="WHITE" />
                <HeaderStyle HorizontalAlign="Center" BackColor="WHITE" />
                <Columns>

                    <%--Imagen en columna--%>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server"
                                ImageUrl='<%#Eval("Articulo.Imagenes[0].UrlImagen")%>' Style="height: 50px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- <asp:BoundField HeaderText="" DataField="Articulo.Imagenes[0].UrlImagen"/>--%>
<%--                    <asp:BoundField HeaderText="Artículo" DataField="Articulo.Nombre" />--%>

                    <asp:TemplateField HeaderText="Articulo">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Articulo.Nombre") %>'
                                OnClick="btnDetalle_Click"
                                CommandArgument="Articulo.Id"
                                CausesValidation="false"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- PRECIO --%>
                    <asp:BoundField HeaderText="Precio/Un." DataFormatString="{0:C}" DataField="Articulo.Precio" />


                    <%--CONTROLES--%>
                    <asp:TemplateField HeaderText="Agregar/Quitar">
                        <ItemTemplate>
                            <asp:Button runat="server" Text="+"
                                OnClick="btnAgregar_Click"
                                CssClass="btn" CommandArgument="Articulo.Id"
                                CausesValidation="false" />

                            <asp:Button ID="btnQuitar" runat="server" Text="-" CssClass="btn" OnClick="btnQuitar_Click"
                                CommandArgument="Articulo.Id"
                                CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- CANTIDAD--%>
                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />


                    <%-- TOTAL /// VER CONFIGURACION carrito.cantidad * articulo.precio--%>
                    <asp:TemplateField HeaderText="Subtotal">
                        <ItemTemplate>
                            <asp:Label ID="lblSubtotal" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:Button ID="bntBorrar" runat="server" Text="🗑" CssClass="btn" OnClick="bntBorrar_Click"
                                CommandArgument="Articulo.Id" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Button ID="bntVer" runat="server" Text="👁" CssClass="btn" OnClick="bntVer_Click"
                                CommandArgument="Articulo.Id" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
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




        <%--   PANTALLA DERECHA --%>

        <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>

        <div class="p-2 flex-fill bd-highlight" style="width:50vw">

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
            <a href="Default.aspx" class="sinCarritoBoton">ver catalogo</a>
        </div>
    </div>

    <%}%>
</asp:Content>
