Imports DevExpress.Xpo.DB
Imports System.Data.SqlClient
Imports DevExpress.Xpo.DB.Helpers

Public Class MyMSSqlConnectionProvider
    Inherits MSSqlConnectionProvider

    Private Const STR_WITHNOLOCK As String = " WITH(NOLOCK)"

    Public Sub New(ByVal connection As IDbConnection, ByVal autoCreateOption As AutoCreateOption)
        MyBase.New(connection, autoCreateOption)
    End Sub

    Public Shared Shadows Function CreateProviderFromString(ByVal connectionString As String, ByVal autoCreateOption As AutoCreateOption, <System.Runtime.InteropServices.Out()> ByRef objectsToDisposeOnDisconnect() As IDisposable) As IDataStore
        Dim connection_Renamed As IDbConnection = New SqlConnection(connectionString)
        objectsToDisposeOnDisconnect = New IDisposable() {connection_Renamed}

        Return CreateProviderFromConnection(connection_Renamed, autoCreateOption)
    End Function

    Public Shared Shadows Function CreateProviderFromConnection(ByVal connection As IDbConnection, ByVal autoCreateOption As AutoCreateOption) As IDataStore
        Return New MyMSSqlConnectionProvider(connection, autoCreateOption)
    End Function

    'Public Shared Shadows Function GetConnectionString(ByVal ServerName As String, ByVal database As String, ByVal userid As String, ByVal password As String) As String
    '    Dim ConString As String = String.Format("{0}={1}; data source={2};user id={4};password={5};Initial Catalog={3}", DataStoreBase.XpoProviderTypeParameterName, XpoProviderTypeString, ServerName, database, userid, password)
    '    Return ConString
    'End Function

    'Public Shared Shadows Function GetConnectionString(ByVal ServerName As String, ByVal database As String) As String
    '    Dim ConString As String = String.Format("{0}={1}; data source={2};Integrated Security=SSPI;Pooling=false;Initial Catalog={3}", DataStoreBase.XpoProviderTypeParameterName, XpoProviderTypeString, ServerName, database)
    '    Return ConString
    'End Function

    Public Overrides Function FormatTable(ByVal schema As String, ByVal tableName As String, ByVal tableAlias As String) As String
        Return String.Concat(MyBase.FormatTable(schema, tableName, tableAlias), STR_WITHNOLOCK)
    End Function

End Class
