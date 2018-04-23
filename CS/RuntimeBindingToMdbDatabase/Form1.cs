#region #reference
using System.Windows.Forms;
using System;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraReports.UI;
using DevExpress.Xpo.DB;
#endregion #reference

namespace RuntimeBindingToMdbDatabase {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

#region #code
private SqlDataSource BindToData() {
    // Create a data source with the required connection parameters.  
    Access97ConnectionParameters connectionParameters = new Access97ConnectionParameters("../../nwind.mdb", "", "");
    SqlDataSource ds = new SqlDataSource(connectionParameters);

    // Create a SELECT query.
    // Join the Categories and Products table by the CategoryID column.  
    // Return the list of categories and the number of products in each category.  
    // Sort the categories by the number of products in them.  
    // The included categories must contain a specific number of products. 
    // The chain ends with calling the Build method accepting the query name as a parameter. 
    SelectQuery query = SelectQueryFluentBuilder
        .AddTable("Categories")
        .SelectColumn("CategoryName")
        .GroupBy("CategoryName")
        .Join("Products", "CategoryID")
        .SelectColumn("ProductName", AggregationType.Count, "ProductCount")
        .SortBy("ProductName", AggregationType.Count, System.ComponentModel.ListSortDirection.Descending)
        .GroupFilter("[ProductCount] > 7")
        .Build("selectQuery");

    // Add the query to the collection and return the data source. 
    ds.Queries.Add(query);   
    ds.Fill();
    return ds;
}

private XtraReport CreateReport() {
    // Create a new report instance.
    XtraReport report = new XtraReport();

    // Assign the data source to the report.
    report.DataSource = BindToData();
    report.DataMember = "selectQuery";

    // Add a detail band to the report.
    DetailBand detailBand = new DetailBand();
    detailBand.Height = 50;
    report.Bands.Add(detailBand);

    // Add labels to the detail band.
    XRLabel label1 = new XRLabel();
    label1.DataBindings.Add("Text", null, "selectQuery.CategoryName");
    XRLabel label2 = new XRLabel();
    label2.DataBindings.Add("Text", null, "selectQuery.ProductCount");
    label2.Location = new System.Drawing.Point(200, 0);
    detailBand.Controls.AddRange(new[] { label1, label2 });

    return report;
}

private void button1_Click(object sender, EventArgs e) {
    // Show the report's print preview.
    ReportPrintTool printTool = new ReportPrintTool(CreateReport());
    printTool.ShowPreview();
}

private void button2_Click(object sender, EventArgs e) {
    // Open the report in an End-User Designer.
    ReportDesignTool designTool = new ReportDesignTool(CreateReport());
    designTool.ShowRibbonDesignerDialog();
}
#endregion #code  
    }
}
