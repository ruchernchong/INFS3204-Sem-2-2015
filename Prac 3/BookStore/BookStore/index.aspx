<%@ Page Title="" Language="C#" MasterPageFile="~/bootstrap.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="BookStore.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-lg-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">All Players</h3>
            </div>
            <div class="panel-body">

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
                                <asp:Label ID="lblBookID" runat="server" Text="ID: "></asp:Label>
                                <asp:TextBox ID="txtBookID" runat="server" CssClass="form-control" placeholder="Book ID"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblBookName" runat="server" Text="Name: "></asp:Label>
                                <asp:TextBox ID="txtBookName" runat="server" CssClass="form-control" placeholder="Book Name"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblBookAuthor" runat="server" Text="Author: "></asp:Label>
                                <asp:TextBox ID="txtBookAuthor" runat="server" CssClass="form-control" placeholder="Book Author"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblBookYear" runat="server" Text="Year: "></asp:Label>
                                <asp:TextBox ID="txtBookYear" runat="server" CssClass="form-control" placeholder="Year of Book published"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblBookPrice" runat="server" Text="Price($): "></asp:Label>
                                <asp:TextBox ID="txtBookPrice" runat="server" CssClass="form-control" placeholder="Price of Book"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblBookStock" runat="server" Text="Stock: "></asp:Label>
                                <asp:TextBox ID="txtBookStock" runat="server" CssClass="form-control" placeholder="Book Stock"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button type="button" ID="btnAddBooks" runat="server" CssClass="btn btn-success" OnClick="btnAddBooks_Click" Text="Add Book(s)" />
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
                                    <asp:Button type="button" ID="btnDeleteBooks" runat="server" CssClass="btn btn-danger" OnClick="btnDeleteBooks_Click" Text="Delete Book(s)" />
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
                                    <asp:Button type="button" ID="btnSearchBooks" runat="server" CssClass="btn btn-primary" OnClick="btnSearchBooks_Click" Text="Search Book(s)" />
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
                                <asp:Label ID="lblTotalBudget" runat="server" Text="Total Budget: "></asp:Label>
                                <asp:TextBox ID="txtTotalBudget" runat="server" CssClass="form-control" placeholder="Enter your budget here"></asp:TextBox>
                            </div>
                            <asp:Label ID="lblBookNumber_0" runat="server" Text="Book No: "></asp:Label>
                            <asp:TextBox ID="txtBookNumber_0" runat="server"></asp:TextBox>

                            <asp:Label ID="lblQty_0" runat="server" Text="Qty: "></asp:Label>
                            <asp:TextBox ID="txtQty_0" runat="server"></asp:TextBox>
                            <p />
                            <asp:PlaceHolder ID="placeHolderAddField" runat="server"></asp:PlaceHolder>
                            <div class="form-group">
                                <asp:Button ID="btnMore" runat="server" CssClass="btn btn-warning" OnClick="btnMore_Click" Text="Add Field(s)" />
                            </div>
                            <p />
                            <div class="form-group">
                                <asp:TextBox ID="txtPurchase" runat="server" CssClass="form-control" placeholder="Your result will be shown here"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnPurchase" runat="server" CssClass="btn btn-warning" OnClick="btnPurchase_Click" Text="Purchase Book(s)" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
