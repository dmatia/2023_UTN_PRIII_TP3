<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tp3_equipo25.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%--Main--%>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

<div class="row">
        <div class="d-inline-flex">
        <asp:Label Text="Busqueda" runat="server"></asp:Label>
         </div>
    <div class="col">
        <asp:Textbox CssClass="form-control TxtBusqueda" OnTextChanged="TxtBusqueda_TextChanged" Id="TxtBusqueda" runat="server" AutoPostBack="true"></asp:Textbox>
    </div>
    <div class="col">
        <asp:DropDownList Id="DropDownList1" CssClass="form-control" runat="server"></asp:DropDownList>
    </div>
       <div class="col">
           
             <asp:DropDownList Id="DdlCategoria" CssClass="form-control" runat="server" OnSelectedIndexChanged="DdlCategoria_SelectedIndexChanged" ></asp:DropDownList>
    </div>
      <div class="col">
        <asp:DropDownList Id="DdlMarca" CssClass="form-control" runat="server"></asp:DropDownList>
    </div>
    
</div>
    <div class="row">
        Ordenar por. Vista
        </div>
    <div class="row row-cols-1 row-cols-sm-2 row-cols-md-4">
        <%--Repeater--%>
        <asp:Repeater ID="RepCards" runat="server">
            <ItemTemplate>
                  <div class="col-6 mt-4">
                        <%--Card--%>
                    <div class="card" style="width: 18rem;">
                     <img src="<%#Eval("Imagenes[0].UrlImagen")%>" Cssclass="card-img-top img-fluid" style="max-height: 130px" alt="imagen"onerror="this.src='https://www.freeiconspng.com/uploads/no-image-icon-23.jpg'">
                       <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre")%></h5>
                            <h5 class="card-title">$<%#Eval("Precio")%></h5>
                           </div>
                        <div class="card-body">
                            <div class="d-grid gap-2">
                            <asp:Button Text="Ver Detalle" runat="server" class="btndetalle" ID="BtnDetalle" CommandArgument='<%#Eval("Id")%>' CommandName="ArticuloId" OnClick="BtnDetalle_Click" />
                            </div>
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