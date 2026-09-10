#Region "Imports"

Imports DevExpress.Xpo.DB
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Helpers

#End Region

''' <summary>
''' Provee servicios para el almacenamiento de datos
''' para las consulta realizadas por el ORM Xpo
''' </summary>
Public Class XpoGateEx27

    Inherits MSSqlMultiDataStoreService

    ''' <summary>
    ''' Diccionario de conexiones
    ''' </summary>
    Public Shared dataStoreProviderDictionary As Dictionary(Of String, DataStoreProviderEntry)
    Shared Sub New()
        dataStoreProviderDictionary = New Dictionary(Of String, DataStoreProviderEntry)()
    End Sub

    ''' <summary>
    ''' Obtiene la conexión al almacén de datos
    ''' </summary>
    ''' <param name="dataBase">Base de datos a consultar</param>
    ''' <returns>Conexión al almacén de datos</returns>
    Public Shared Function FuncDataStoreProviderDictionary(ByVal dataBase As String) As DataStoreProviderEntry
        Try
            If Not dataStoreProviderDictionary.ContainsKey(dataBase) Then
                Dim connectionString As String = String.Format(Helper.GetXpoConnectionString(), dataBase)
                Dim objDisposables() As IDisposable = Nothing
                Dim dataStore As MyMSSqlConnectionProvider = MyMSSqlConnectionProvider.CreateProviderFromString(connectionString, AutoCreateOption.SchemaAlreadyExists, objDisposables)
                dataStoreProviderDictionary.Add(dataBase, New DataStoreProviderEntry(dataStore, TryCast(dataStore, ICommandChannel)))
            End If
            Return dataStoreProviderDictionary(dataBase)
        Catch ex As Exception
            Helper.HandledException(ex)
            Return Nothing
        End Try
    End Function

    Public Sub New()
        MyBase.New(AddressOf FuncDataStoreProviderDictionary)
    End Sub

End Class
