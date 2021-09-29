# You can now perform query execution in a single line code with our program.
    Features of our program:
    1) Written in C#
    2) Can be used with Windows application with form validation
    3) Supports Bunifu Framework
    4) Enables design validation

# Ripple Community
    We hope to create a positive ripple effect through this program and to keep the waves going your contribution is valuable. Reach us out at: sahil@ripplecommunity.in

# How to use

## DB Connection string
    App.config
    <connectionStrings>
        <add name="Sahil"
            connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=YourDBName;Integrated Security=True"
            providerName="System.Data.SqlClient" />
    </connectionStrings>

## Functions use
    Sahil.BindCombo("Name", "Units", cmbMeasurment); // bind combo box 

    Sahil.AutoComplete("Name", "Customers", txtPartyName); // autocomplete text box

    DataTable invoice = Sahil.ExecuteDataTable("Select * from tableName where Id=" + id + "");

    Sahil.ExecuteNonQueryWithoutMsg("delete from CustomersTransaction where id='" + txtid.Text + "'");

    Sahil.IsNull(textboxid)  // check text value null or not

# Contact
    Sahil Aslam Kalaigar 
    Mail:- sahil@ripplecommunity.in


# Website
Website. [Ripple community](https://ripplecommunity.in/)