'***********************************************************************
' Assembly         : DistributedService.Xpo.Deployment
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-03-05
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB.Exceptions
Imports DevExpress.Xpo.DB.Helpers
Imports System.IO
#End Region

''' <summary>
''' clase que implenenta la inteface para el manejo de entidades XPO a traves de wcf
''' </summary>
Public Class XpoGate
    Implements IXpoGate

    'Private Shared dataStore As IDataStore
    Private Shared connectionDictionary As Dictionary(Of String, IDataLayer)
    Shared Sub New()
        connectionDictionary = New Dictionary(Of String, IDataLayer)()
    End Sub

#Region "IXpoGate Members"


    Public Function GetAutoCreateOption() As DevExpress.Xpo.DB.AutoCreateOption Implements IXpoGate.GetAutoCreateOption
        'para que no se realizen cambios en la BD        
        Return AutoCreateOption.SchemaAlreadyExists
    End Function

    Public Function ModifyData(ByVal company As String, ByVal ParamArray dmlStatements() As DevExpress.Xpo.DB.ModificationStatement) As DevExpress.Xpo.DB.ModificationResult Implements IXpoGate.ModifyData

        Conectar(company)
        'Return XpoDefault.DataLayer.ModifyData(dmlStatements)
        Return New ModificationResult()
    End Function

    Public Function SelectData(ByVal company As String, ByVal ParamArray selects() As DevExpress.Xpo.DB.SelectStatement) As DevExpress.Xpo.DB.SelectedData Implements IXpoGate.SelectData
        Try
            Conectar(company)
            Dim result = XpoDefault.DataLayer.SelectData(selects)
            'Dim connectionProvider = CType(CType(XpoDefault.DataLayer, DevExpress.Xpo.ThreadSafeDataLayer).ConnectionProvider, DevExpress.Xpo.DB.MSSqlConnectionProvider)
            'connectionProvider.Connection.Close()
            Return result
        Catch ex As Exception
            Dim msg = ex.Message.ToString
            Return Nothing
        End Try
    End Function

    Public Function UpdateSchema(ByVal dontCreateIfFirstTableNotExist As Boolean, ByVal ParamArray tables() As DevExpress.Xpo.DB.DBTable) As DevExpress.Xpo.DB.UpdateSchemaResult Implements IXpoGate.UpdateSchema
        'para que no se realizen cambios en la BD
        Return UpdateSchemaResult.SchemaExists
    End Function

#End Region


    Private _IndigoBDEncriptadaPassword As String
    ''' <summary>
    ''' Propiedad que contiene el password de encriptacion de la BD
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IndigoBDEncriptadaPassword As String
        Get
            Return _IndigoBDEncriptadaPassword
        End Get
        Set(ByVal value As String)
            _IndigoBDEncriptadaPassword = value
        End Set
    End Property

    Private Sub Conectar(ByVal company As String)
        'Inicio la conexión
        'XpoDefault.DataLayer = Nothing
        'XpoDefault.Session = Nothing
        If connectionDictionary.ContainsKey(company) Then
            XpoDefault.DataLayer = connectionDictionary(company)
        Else
            Dim ConnectionString = String.Format(ConfigurationManager.ConnectionStrings("XpoConnection").ToString, company)
            Dim store = XpoDefault.GetConnectionProvider(ConnectionString, AutoCreateOption.None)
            XpoDefault.DataLayer = New ThreadSafeDataLayer(Nothing, store)
            connectionDictionary.Add(company, XpoDefault.DataLayer)
        End If
    End Sub

End Class
