<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tp3_equipo25.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<%--Main--%>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
       
<div class="row">

    <div class="col-2">
        <label class="form-check-label" for="ChkBusquedaAvanzada">Búsqueda Avanzada</label>
    </div>
    <div class="col-1">
        <asp:CheckBox ID="ChkBusquedaAvanzada" runat="server" AutoPostBack="true" role="switch" />
    </div>

      <div class="col-5">
         <asp:TextBox ID="TxtBusquedaRapida" type="text" CssClass="form-control" placeholder="Ingresa una búsqueda rápida" runat="server" OnTextChanged="TxtBusquedaRapida_TextChanged" AutoPostBack="true"></asp:TextBox>
    </div>
    <div class="col-1">
       <asp:Button ID="BtnBusquedaRapida" CssClass="form-control" runat="server" Text="Lupa" OnClick="BtnBusquedaRapida_Click"/>
    </div>
           
    

    <% if (ChkBusquedaAvanzada.Checked)
        { %>
    <div class="row">
   <div class="col-lg-3 col-md-6 col-sm-12">
    <div class="d-flex flex-column">
        <div class="row">
            <!-- Primera fila: TextBox ocupando todo el ancho -->
            <div class="col">
                <asp:TextBox CssClass="form-control TxtBusqueda" ID="TxtBusqueda" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <!-- Segunda fila: TextBox de incluir descripción y botón de búsqueda -->
            <div class="col">
                <asp:CheckBox ID="ChkCheckDescripcion" CssClass="form-check" Text="Incluir descripción" runat="server" />
            </div>
            <div class="col-4">
                <asp:Button CssClass="form-control btn btn-secondary btn-sm" ID="BtnBusqueda" runat="server" Text="Buscar" OnClick="BtnBusqueda_Click" Autopostback="true" />
            </div>
        </div>
        <div class="row">
            <!-- Filas adicionales con otros elementos -->
            <div class="col">
                <asp:Label Text="Precio" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-6">
                <asp:Label Text="Mínimo:" runat="server"></asp:Label>
                <asp:TextBox CssClass="form-control" type="number" ID="TxtPreciomin" runat="server"></asp:TextBox>
            </div>
            <div class="col-6">
                <asp:Label Text="Máximo" runat="server"></asp:Label>
                <asp:TextBox CssClass="form-control" type="number" ID="TxtPreciomax" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:DropDownList ID="DdlMarca" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:DropDownList ID="DdlCategoria" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>
    </div>

         </div>
        
    <% } %>

     <div class="col-lg-<% if (ChkBusquedaAvanzada.Checked)
        { %>9<% }
        else
        { %>12<% } %> col-md-6 col-sm-12">
        <div class="row row-cols-1 row-cols-md-<% if (ChkBusquedaAvanzada.Checked)
            { %>3<% }
            else
            { %>4<% } %>">
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
                                <% if (ChkCheckDescripcion.Checked)
                                    { %>
                                <p class="card-text">Descripción: <%#Eval("Descripcion")%></p>
                                <% } %>
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
         <% if (ChkBusquedaAvanzada.Checked)
             { %>
              </ div >
                           
               <%}%>  
</div>
</asp:Content>
<%--Fin Main--%>