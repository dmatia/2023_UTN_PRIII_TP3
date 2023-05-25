<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tp3_equipo25.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="row row-cols-1 row-cols-md-3 g-4" style="flex-direction:row; justify-content:flex-start; align-content:space-evenly;">
    <asp:Repeater ID="RepCards" runat="server">
        <ItemTemplate>
            <div class="-col">
            <div class="card" style="width: 18rem;">
                        
                    <img src="<%#Eval("Imagenes[0].UrlImagen")%>" class="card-img-top" style="height:150px" alt="...">
                    <div class="card-body">
                        <h5 class="card-title"><%#Eval("Nombre")%></h5>
                        <p class="card-text">Descripcion:<%#Eval("Descripcion")%></p>
                            </div>
                       <div class="card-body">
            <asp:Button Text="Detalle" Cssclass="btn btn-primary" runat="server" ID="BtnDetalle" CommandArgument='<%#Eval("Id")%>' CommandName="ArticuloId"/>

     </div>
                    </div>
              </div>
        </ItemTemplate>




    </asp:Repeater>
    </div>
</asp:Content>
