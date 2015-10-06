<%@ Page Title="" Language="C#" MasterPageFile="~/bootstrap.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="BookStore.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-lg-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">Data Table</h3>
            </div>
            <div class="panel-body">
                <asp:Panel ID="divEmptyResults" CssClass="alert alert-warning" runat="server"></asp:Panel>
                <asp:DataGrid ID="dataGrid_DisplayData" runat="server" CssClass="table table-striped table-hover"></asp:DataGrid>
            </div>
        </div>
        <asp:Panel ID="divErrorMessage" CssClass="alert alert-danger" runat="server"></asp:Panel>
        <form id="formBookService" class="form" runat="server">
            <div class="row">
                <div class="col-lg-4">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Add Books</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <asp:Label ID="lblBookID" runat="server" CssClass="control-label"></asp:Label>
                                <asp:TextBox ID="txtBookID" runat="server" CssClass="form-control" placeholder="Book ID"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblBookName" runat="server" CssClass="control-label"></asp:Label>
                                <asp:TextBox ID="txtBookName" runat="server" CssClass="form-control" placeholder="Book Name"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblBookAuthor" runat="server" CssClass="control-label"></asp:Label>
                                <asp:TextBox ID="txtBookAuthor" runat="server" CssClass="form-control" placeholder="Book Author"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblBookYear" runat="server" CssClass="control-label"></asp:Label>
                                <asp:TextBox ID="txtBookYear" runat="server" CssClass="form-control" placeholder="Year of Book published"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblBookPrice" runat="server" CssClass="control-label"></asp:Label>
                                <asp:TextBox ID="txtBookPrice" runat="server" CssClass="form-control" placeholder="Price of Book"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblBookStock" runat="server" CssClass="control-label"></asp:Label>
                                <asp:TextBox ID="txtBookStock" runat="server" CssClass="form-control" placeholder="Book Stock"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button type="button" ID="btnAddBooks" runat="server" CssClass="btn btn-success" OnClick="btnAddBooks_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="panel panel-danger">
                        <div class="panel-heading">
                            <h3 class="panel-title">Delete Books</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <asp:DropDownList ID="dropDelete" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtDelete" runat="server" CssClass="form-control" placeholder="Keyword"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button type="button" ID="btnDeleteBooks" runat="server" CssClass="btn btn-danger" OnClick="btnDeleteBooks_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Search Books</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <asp:DropDownList ID="dropSearch" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button type="button" ID="btnSearchBooks" runat="server" CssClass="btn btn-primary" OnClick="btnSearchBooks_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-8">
                    <div class="panel panel-warning">
                        <div class="panel panel-heading">
                            <h3 class="panel-title">Purchase Book</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <asp:Label ID="lblTotalBudget" runat="server" CssClass="control-label"></asp:Label>
                                <asp:TextBox ID="txtTotalBudget" runat="server" CssClass="form-control" placeholder="Enter your budget here"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblBookNo" runat="server" CssClass="control-label"></asp:Label>
                                <asp:TextBox ID="txtBookNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Panel ID="panelFormGroupBookNumber" runat="server">
                                    <asp:PlaceHolder ID="placeholderLblBookNumber" runat="server"></asp:PlaceHolder>
                                    <asp:PlaceHolder ID="placeholderTxtBookNumber" runat="server"></asp:PlaceHolder>
                                </asp:Panel>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblQuantity" runat="server" CssClass="control-label"></asp:Label>
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Panel ID="panelFormGroupQuantity" runat="server">
                                    <asp:PlaceHolder ID="placeholderLblQuantity" runat="server"></asp:PlaceHolder>
                                    <asp:PlaceHolder ID="placeholderTxtQuantity" runat="server"></asp:PlaceHolder>
                                </asp:Panel>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnMore" runat="server" CssClass="btn btn-warning" OnClick="btnMore_Click" />
                                <asp:PlaceHolder ID="placeHolderAddField" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtPurchase" runat="server" CssClass="form-control" placeholder="Your result will be shown here"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnPurchase" runat="server" CssClass="btn btn-warning" OnClick="btnPurchase_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
