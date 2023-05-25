<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CarritoWeb.aspx.cs" Inherits="tp3_equipo25.CarritoWeb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <div class="container-sm">

        <h2>Tu carrito:</h2>
        <div class="row">
            <div class="col-6">
                <asp:GridView ID="dgvCarrito" runat="server" CssClass="table mb-3" DataKeyNames="Cantidad"
                    AutoGenerateColumns="false" OnSelectedIndexChanged="dgvCarrito_SelectedIndexChanged"
                    OnPageIndexChanging="dgvCarrito_PageIndexChanging" AllowPaging="true" PageSize="10">
                    <Columns>

                        <%--Imagen en columna--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("Articulo.Imagenes[0].UrlImagen")%>' Style="height: 100px" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- <asp:BoundField HeaderText="" DataField="Articulo.Imagenes[0].UrlImagen"/>--%>
                        <asp:BoundField DataField="Articulo.Nombre" />
                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />

                        <%--CONTROLES--%>
                        <asp:TemplateField HeaderText="Agregar/Quitar">
                            <ItemTemplate>
                                <asp:Button ID="btnAgregar" runat="server" Text="+" OnClick="btnAgregar_Click" />
                                <asp:Button ID="btnQuitar" runat="server" Text="-" OnClick="btnQuitar_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Total" DataField="Articulo.Precio" />


                    </Columns>
                </asp:GridView>
            </div>

            <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
            <div class="col-6">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Image ID="Image2" runat="server" ImageUrl='<%#Eval("Articulo.Imagenes[0].UrlImagen")%>' Style="width: 100%" />
                    </ContentTemplate>

                </asp:UpdatePanel>


            </div>




        </div>

        <a class="btn btn-primary">CHECK OUT</a>

    </div>



</asp:Content>
