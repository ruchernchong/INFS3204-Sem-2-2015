<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/bootstrap.Master" CodeBehind="index.aspx.cs" Inherits="Prac_1.index" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Basic Calculator</h1>
        <p>Performing basic calculator operations and converting the result from base-10 to base-2.</p>
        <p>Counting the numbers of digitals in base-2 number</p>
    </div>
    <form id="frmCalculator" class="form-inline" runat="server">
        <div class="col-lg-7">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Basic Calculator</h3>
                </div>
                <div class="panel-body">
                    <table class="table">
                        <tr>
                            <td></td>
                            <td>
                                <asp:TextBox ID="txtInputOne" placeholder="Input One" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                <asp:DropDownList ID="dropOperators" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:TextBox ID="txtInputTwo" placeholder="Input Two" runat="server" TabIndex="2" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblBase10" runat="server" Text="lblBase10"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtResultBase10" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblBase2" runat="server" Text="lblBase2"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtResultBase2" runat="server" CssClass="form-control"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnCalculate" runat="server" OnClick="btnCalculate_Click" CssClass="btn btn-warning" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

        </div>
        <div class="col-lg-5">
            <div class="panel panel-primary">
                <div class="panel panel-heading">
                    <h3 class="panel-title">Counting</h3>
                </div>
                <div class="panel-body">
                    <table class="table">
                        <tr>
                            <td>
                                <asp:Label ID="lblNumOfZero" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumOfZero" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNumOfOne" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumOfOne" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnCount" runat="server" OnClick="btnCount_Click" class="btn btn-warning" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
