<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tp3_equipo25.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%--Main--%>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div class="row row-cols-1 row-cols-md-3 g-4" style="flex-direction: row; justify-content: flex-start; align-content: space-evenly;">
        <%--Repeater--%>
        <asp:Repeater ID="RepCards" runat="server">
            <ItemTemplate>
                <div class="-col">
                    <%--Card--%>
                    <div class="card" style="width: 18rem;">
                        <img src="<%#Eval("Imagenes[0].UrlImagen")%>" class="card-img-top" style="height: 150px" alt="...">
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre")%></h5>
                            <p class="card-text">Descripcion:<%#Eval("Descripcion")%></p>
                        </div>
                        <div class="card-body">
                            <asp:Button Text="Detalle" CssClass="btn btn-primary" runat="server" ID="BtnDetalle" CommandArgument='<%#Eval("Id")%>' CommandName="ArticuloId" OnClick="BtnDetalle_Click" />
                        </div>
                    </div>
                    <%--Card--%>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <%--Repeater--%>
    </div>
</asp:Content>
<%--Fin Main--%>