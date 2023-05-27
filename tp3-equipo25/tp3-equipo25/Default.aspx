<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tp3_equipo25.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%--Main--%>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
     
    <asp:Label Text="Filtrar" runat="server"> </asp:Label>
    <asp:Textbox Id="TxtBusqueda" runat="server"></asp:Textbox>
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
                            <h5 class="card-title"><%#Eval("Precio")%></h5>
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