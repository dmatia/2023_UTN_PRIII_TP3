<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CarritoWeb.aspx.cs" Inherits="tp3_equipo25.CarritoWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <div class="container-sm">

        <h2>Tu carrito:</h2>

        <asp:GridView ID="dgvCarrito" runat="server" CssClass="table" DataKeyNames="Cantidad"
            AutoGenerateColumns="false" OnSelectedIndexChanged="dgvCarrito_SelectedIndexChanged"
            OnPageIndexChanging="dgvCarrito_PageIndexChanging" AllowPaging="true" PageSize="10">
            <Columns>
                <asp:BoundField HeaderText="" DataField="Articulo.Nombre"/>
                <asp:BoundField HeaderText="Cantidad" DataField="Cantidad"/>
                <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Sacar" />
            </Columns>
        </asp:GridView>

        <a class="btn btn-primary">CHECK OUT</a>

    </div>



</asp:Content>