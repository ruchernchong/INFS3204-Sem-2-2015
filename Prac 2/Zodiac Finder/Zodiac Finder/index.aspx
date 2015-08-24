<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/bootstrap.Master" CodeBehind="index.aspx.cs" Inherits="Zodiac_Finder.index" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div class="jumbotron">
        <h1>Zodiac Finder</h1>
        <p></p>
        <p></p>
    </div>--%>
    <form id="formZodiacFinder" class="form-inline" runat="server">
        <div class="col-lg-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Task 1 & 2: Zodiac Finder</h3>
                </div>
                <div class="panel-body">
                    <table class="table">
                        <thead></thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDateByName_InputName" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDateByName_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDateByName_ResultName" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtResultDateByName_GetDateInterval" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnDateByName_GetDate" runat="server" CssClass="btn btn-primary" OnClick="btnDateByName_GetDate_Click" />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblNameByDate_Month" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNameByDate_Month" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNameByDate_Day" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNameByDate_Day" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNameByDate_ResultName" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtResultNameByDate_GetName" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnNameByDate_GetZodiac" runat="server" CssClass="btn btn-primary" OnClick="btnNameByDate_GetZodiac_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <div class="col-lg-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Task 3: Postcode Finder</h3>
                </div>
                <div class="panel-body">
                    <table class="table">
                        <thead></thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="dropSuburb" runat="server" CssClass="form-control"></asp:DropDownList>
                                </td>
                                <td>
                                    <input type="button" id="btnShowPostcode" class="btn btn-primary" value="Show Postcode" />
                                    <%--<asp:Button ID="btnShowPostcode" runat="server" CssClass="btn btn-primary" OnClick="btnShowPostcode_Click" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPostcode" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTimestamp" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <asp:ScriptManager ID="scriptManager" runat="server">
            <Services>
                <asp:ServiceReference Path="~/webSvc_PostcodeFinder.asmx" />
            </Services>
        </asp:ScriptManager>
    </form>

    <script>
        var onSuccess = function (result) {
            $get("lblPostcode").innerHTML = result;
        }

        var onFailed = function (result) {
            $get("lblPostcode").innerHTML = "Invalid Postcode.";
        }

        $(document).ready(function () {
            $("#btnShowPostcode").click(function () {
                //alert('{"dropSuburb" : ' + JSON.stringify($("#<%=dropSuburb.ClientID %>").val()) + '}');
                $.ajax({
                    type: "POST",
                    url: "webSvc_PostcodeFinder.asmx/PostcodeFinder",
                    data: '{"dropSuburb": "' + $("#<%=dropSuburb.ClientID %>").val() + '"}',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        $("#<%=lblPostcode.ClientID %>").text("Postcode: " + JSON.stringify(response.d).replace(/"/g, ""));
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
            });
        });
    </script>
</asp:Content>
