#Region "#reference"
Imports DevExpress.DataAccess.Sql
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.XtraReports.UI
#End Region ' #reference

Namespace RuntimeBindingToMdbDatabase
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

#Region "#code"
        Private Function BindToData() As SqlDataSource
            ' Create a data source with the required connection parameters.  
            Dim connectionParameters As New Access97ConnectionParameters("../../nwind.mdb", "", "")
            Dim ds As New SqlDataSource(connectionParameters)

            ' Create a SELECT query.
            ' Join the Categories And Products table by the CategoryID column.  
            ' Return the list of categories And the number of products in each category.  
            ' Sort the categories by the number of products they contain.  
            ' The filtered categories contain a specific number of products. 
            ' Call the Build method with the query name as a parameter
            ' to end the chain of methods. 
            Dim query As SelectQuery = SelectQueryFluentBuilder.AddTable("Categories").SelectColumn("CategoryName").GroupBy("CategoryName").Join("Products", "CategoryID").SelectColumn("ProductName", AggregationType.Count, "ProductCount").SortBy("ProductName", AggregationType.Count, System.ComponentModel.ListSortDirection.Descending).GroupFilter("[ProductCount] > 7").Build("selectQuery")

            ' Add the query to the collection and return the data source. 
            ds.Queries.Add(query)
            ds.Fill()
            Return ds
        End Function

        Private Function CreateReport() As XtraReport
            ' Create a new report instance.
            Dim report As New XtraReport()

            ' Assign the data source to the report.
            report.DataSource = BindToData()
            report.DataMember = "selectQuery"

            ' Add a detail band to the report.
            Dim detailBand As New DetailBand()
            detailBand.Height = 50
            report.Bands.Add(detailBand)

            ' Create new labels.
            Dim label1 As New XRLabel()
            Dim label2 As New XRLabel()
            label2.Location = New System.Drawing.Point(200, 0)
            ' Specify labels' bindings depending on the report's data binding mode.
            Dim DesignerOptions = DevExpress.XtraReports.Configuration.Settings.Default.UserDesignerOptions
            If DesignerOptions.DataBindingMode = DataBindingMode.Bindings Then
                label1.DataBindings.Add("Text", Nothing, "selectQuery.CategoryName")
                label2.DataBindings.Add("Text", Nothing, "selectQuery.ProductCount")
            Else
                label1.ExpressionBindings.Add(New ExpressionBinding("BeforePrint", "Text", "[CategoryName]"))
                label2.ExpressionBindings.Add(New ExpressionBinding("BeforePrint", "Text", "[ProductCount]"))
            End If
            ' Add labels to the detail band.
            detailBand.Controls.AddRange({label1, label2})

            Return report
        End Function

        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
            ' Show the report's print preview.
            Dim printTool As New ReportPrintTool(CreateReport())
            printTool.ShowPreview()
        End Sub

        Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
            ' Open the report in an End-User Designer.
            Dim designTool As New ReportDesignTool(CreateReport())
            designTool.ShowRibbonDesignerDialog()
        End Sub
#End Region ' #code  
    End Class
End Namespace
