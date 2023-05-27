<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tp3_equipo25.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%--Main--%>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
  
    <div class="row">
        <div class="col-3">
        <div class="d-flex flex-column">
            <div class="d-inline-flex align-items-center">
                <asp:Label Text="Búsqueda" runat="server"></asp:Label>
            </div>
            <div class="row">
                <div class="col">
                    <asp:TextBox CssClass="form-control TxtBusqueda" ID="TxtBusqueda" runat="server" AutoPostBack="false"></asp:TextBox>
                </div>
                <div class="col-4">
                    <asp:Button CssClass="form-control btn btn-secondary btn-sm" ID="BtnBusqueda" runat="server" Text="Buscar" OnClick="BtnBusqueda_Click" />
                    <asp:CheckBox ID="ChkCheckDescripcion" CssClass="form-check" Text="Incluir descripcion" runat="server" AutoPostBack="true"/>
            </div>
            <div class="d-inline-flex align-items-center">
                <asp:Label Text="Precio" runat="server"></asp:Label>
            </div>
            <div class="row">
                <div class="col-6">
                    <asp:Label Text="Minimo:" runat="server"></asp:Label>
                    <asp:TextBox CssClass="form-control" ID="TxtPreciomin" runat="server" AutoPostBack="true"></asp:TextBox>
                  </div>
                <div class="col-6">
                    <asp:Label Text="Maximo" runat="server"></asp:Label>
                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" AutoPostBack="true"></asp:TextBox>
                  </div>
            <asp:DropDownList ID="DdlMarca" CssClass="form-control" runat="server"></asp:DropDownList>
            <asp:DropDownList ID="DdlCategoria" CssClass="form-control" runat="server" OnSelectedIndexChanged="DdlCategoria_SelectedIndexChanged"></asp:DropDownList>
        </div>
    </div>
            </div>
             </div>
    <div class="col-9">
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
                            <img src="<%#Eval("Imagenes[0].UrlImagen")%>" cssclass="card-img-top img-fluid" style="max-height: 130px" alt="imagen" onerror="this.src='https://www.freeiconspng.com/uploads/no-image-icon-23.jpg'">
                            <div class="card-body">
                                <h5 class="card-title"><%#Eval("Nombre")%></h5>
                                <h5 class="card-title">$<%#Eval("Precio")%></h5>
                                <%if (ChkCheckDescripcion.Checked)
                                    { %> 
                                
                                <p class="card-text">Descripcion:<%#Eval("Descripcion")%></p>
                                <%} %>
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
    </div>

</div> 
      
</asp:Content>
<%--Fin Main--%>