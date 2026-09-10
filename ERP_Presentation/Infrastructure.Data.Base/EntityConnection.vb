Imports Infrastructure.CrossCutting.Base

Public Class EntityConnection
    Implements IEntityConnection

    Public ServerName As String = "192.168.0.25"
    Public DataBaseName As String = "GENESIS" + indigo.IndigoCompany
    Public DataBaseNameSecurity As String = "GENESISSECURITY"

    Public indigo As SessionValues = SessionValues.Instance

    Public Property connectionCommon As String Implements IEntityConnection.connectionCommon
        Get
            Return "metadata=res://*/Model.CommonModel.csdl|res://*/Model.CommonModel.ssdl|res://*/Model.CommonModel.msl;provider=System.Data.SqlClient;provider connection string=';data source=" + ServerName + ";initial catalog=" + DataBaseName + ";persist security info=True;user id=sa;password=123;multipleactiveresultsets=True;App=EntityFramework'"
        End Get
        Set(value As String)

        End Set
    End Property

    Public Property connectionGlosas As String Implements IEntityConnection.connectionGlosas
        Get
            Return "metadata=res://*/Model.GlosasModel.csdl|res://*/Model.GlosasModel.ssdl|res://*/Model.GlosasModel.msl;provider=System.Data.SqlClient;provider connection string=';data source=" + ServerName + ";initial catalog=" + DatabaseName + ";persist security info=True;user id=sa;password=123;multipleactiveresultsets=True;App=EntityFramework'"
        End Get
        Set(value As String)

        End Set
    End Property

    Public Property connectionPayroll As String Implements IEntityConnection.connectionPayroll
        Get
            Return "metadata=res://*/Model.PayrollModel.csdl|res://*/Model.PayrollModel.ssdl|res://*/Model.PayrollModel.msl;provider=System.Data.SqlClient;provider connection string=';data source=" + ServerName + ";initial catalog=" + DataBaseName + ";persist security info=True;user id=sa;password=123;multipleactiveresultsets=True;App=EntityFramework'"
        End Get
        Set(value As String)

        End Set
    End Property

    Public Property connectionSecurity As String Implements IEntityConnection.connectionSecurity
        Get
            Return "metadata=res://*/Model.SecurityModel.csdl|res://*/Model.SecurityModel.ssdl|res://*/Model.SecurityModel.msl;provider=System.Data.SqlClient;provider connection string=';data source=" + ServerName + ";initial catalog=" + DataBaseNameSecurity + ";persist security info=True;user id=sa;password=123;multipleactiveresultsets=True;App=EntityFramework'"
        End Get
        Set(value As String)

        End Set
    End Property

End Class
