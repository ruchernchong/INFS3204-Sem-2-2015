<%@ Page Title="" Language="C#" MasterPageFile="~/bootstrap.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="BookStore.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-lg-12">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Data Table</h3>
            </div>
            <div class="panel panel-body">
                <asp:DataGrid ID="dataGrid_FileOutput" runat="server" CssClass="table table-striped table-hover">
                </asp:DataGrid>
            </div>
        </div>
        <form id="formBook" runat="server">
            <div class="col-lg-6">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Add Books</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:Label ID="lblBookID" runat="server"></asp:Label>
                            <asp:TextBox ID="txtBookID" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblBookName" runat="server"></asp:Label>
                            <asp:TextBox ID="txtBookName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblBookAuthor" runat="server"></asp:Label>
                            <asp:TextBox ID="txtBookAuthor" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblBookYear" runat="server"></asp:Label>
                            <asp:TextBox ID="txtBookYear" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblBookPrice" runat="server"></asp:Label>
                            <asp:TextBox ID="txtBookPrice" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblBookStock" runat="server"></asp:Label>
                            <asp:TextBox ID="txtBookStock" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Button type="button" ID="btnAddBooks" runat="server" CssClass="btn btn-primary" OnClick="btnAddBooks_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Delete Books</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:DropDownList ID="dropDelete" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtNum" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Button type="button" ID="btnDeleteBooks" runat="server" CssClass="btn btn-primary" OnClick="btnDeleteBooks_Click" />
                        </div>
                    </div>
                </div>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Search Books</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:DropDownList ID="dropSearch" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Button type="button" ID="btnSearchBooks" runat="server" CssClass="btn btn-primary" OnClick="btnSearchBooks_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="footer" ContentPlaceHolderID="Footer" runat="server">
    <div class="col-lg-12">
        <div class="panel panel-body">
            <asp:Label ID="DebuggerInfo" runat="server">Debugger Info</asp:Label>
        </div>
    </div>
</asp:Content>
