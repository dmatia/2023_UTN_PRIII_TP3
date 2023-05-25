<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="tp3_equipo25.Carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div  class="container-sm">
           <h2>Tu carrito</h2>
    <asp:GridView ID="dgvCarrito" runat="server" CssClass="table" DataKeyNames="Id"
        AutogenerateColumns="false" OnSelectedIndexChanged="dgvCarrito_SelectedIndexChanged" 
        OnPageIndexChanging="dgvCarrito_PageIndexChanging" AllowPaging="true" PageSize="10">
        <Columns>
            <asp:BoundField HeaderText="Artículo" DataField="Nombre" />
            <asp:BoundField HeaderText="Cantidad" DataField="Numero" />
            <asp:BoundField HeaderText="Precio total" DataField="Descripcion" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Sacar" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Ver Detalle" />
        </Columns>
    </asp:GridView>
    </div>
      
</asp:Content>
