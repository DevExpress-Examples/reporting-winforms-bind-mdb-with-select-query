# How to Bind a Report to a Microsoft Access Database Using SelectQuery in Code


This example creates the data source that retrieves data from the .MDB file using the SQL query created in code, and displays data in a simple report created at runtime.

The [SqlDataSource](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Sql.SqlDataSource) instance is created at runtime with the connection string that specifies the MDB file location. The data source needs a query to retrieve data. In this example, a query is the [SelectQuery](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Sql.SelectQuery) instance. The [SelectQueryFluentBuilder](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Sql.SelectQueryFluentBuilder) API allows method chaining for query creation. The query selects columns from two data tables with an inner join relation, sorts, groups, filters, and aggregates data. The newly created query is added to the [SqlDataSource.Queries](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Sql.SqlDataSource.Queries) collection. 

A report is the [XtraReport](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XtraReport) class instance that contains a [Detailband](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.DetailBand) and a [Label](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XRLabel) control. Depending on the [Data Binding mode](https://docs.devexpress.com/XtraReports/119236/detailed-guide-to-devexpress-reporting/use-expressions/data-binding-modes), the control uses the [DataBindings](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XRControl.DataBindings) or the [ExpressionBindings](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XRControl.ExpressionBindings) property to bind to data.

Use the report's [DataSource](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XtraReportBase.DataSource) and [DataMember](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XtraReportBase.DataMemeber) properties to assign the data source to the report.

## Files to Look At

* [Form1.cs](CS/RuntimeBindingToMdbDatabase/Form1.cs) (VB: [Form1.vb](VB/RuntimeBindingToMdbDatabase/Form1.vb))

## Documentation

* [Bind Reports to Data](https://docs.devexpress.com/XtraReports/15034/detailed-guide-to-devexpress-reporting/bind-reports-to-data)

