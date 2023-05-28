<%@  Page EnableEventValidation="false"  Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CarritoWeb.aspx.cs" Inherits="tp3_equipo25.CarritoWeb" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <% if (ListaCarrito != null)
        {%>
    <div class="container-sm">

        <div class="row align-items-center">
            <div class="col-6">
                <h2>Tu carrito</h2>
                <asp:GridView ID="dgvCarrito" runat="server" CssClass="table mb-3" DataKeyNames=""
                    AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound="GridView1_RowDataBound"
                    ClientIDMode="AutoID">

                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="WHITE" />
                    <HeaderStyle HorizontalAlign="Center" BackColor="WHITE" />
                    <Columns>

                        <%--Imagen en columna--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("Articulo.Imagenes[0].UrlImagen")%>' Style="height: 50px" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- <asp:BoundField HeaderText="" DataField="Articulo.Imagenes[0].UrlImagen"/>--%>
                        <asp:BoundField HeaderText="Artículo" DataField="Articulo.Nombre" />

                        <%-- PRECIO --%>
                        <asp:BoundField HeaderText="Precio" DataFormatString="{0:C}" DataField="Articulo.Precio" />



                        <%--CONTROLES--%>
                        <asp:TemplateField HeaderText="Agregar/Quitar">
                            <ItemTemplate>
                                <asp:Button runat="server" Text="+" 
                                    OnClick="btnAgregar_Click" 
                                    CssClass="btn" CommandArgument="Articulo.Id"
                                    CausesValidation="false"/>


                                <asp:Button ID="btnQuitar" runat="server" Text="-" CssClass="btn" 
                                    OnClick="btnQuitar_Click" 
                                    CommandArgument="Articulo.Id"
                                    CausesValidation="false"/>
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






                    </Columns>

                </asp:GridView>

                <a href="OrdenCompra.aspx" class="btn btn-primary">CHECK OUT</a>


            </div>

            <%--   PANTALLA DERECHA --%>

            <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>

            <div class="col-6">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Image ID="Image2" runat="server" ImageUrl='<%#Eval("ListaCarrito.Articulo.Imagenes[0].UrlImagen")%>' Style="width: 100%" />
                    </ContentTemplate>


                </asp:UpdatePanel>
            </div>




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
