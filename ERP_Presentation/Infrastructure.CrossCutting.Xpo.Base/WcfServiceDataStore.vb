' ***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Xpo.Base
' Author           : Oscar Sierra
' Created          : 2011-08-03
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-25
' 
' Copyright        : (c) . All rights reserved.
' ***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo.DB
Imports System.Diagnostics
Imports System.ServiceModel
Imports DevExpress.Xpo.DB.Helpers
Imports DevExpress.Xpo.DB.Exceptions
Imports System.ServiceModel.Channels
Imports Infrastructure.CrossCutting.Base
#End Region

''' <summary>
''' 
''' </summary>
Partial Public Class WCFServiceDataStore
    Inherits ClientBase(Of IXpoGate)
    Implements IDataStore

    Private INDCompany As String

#Region "Constructores"

    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal endpointConfigurationName As String)
        MyBase.New(endpointConfigurationName)
    End Sub

    Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
        MyBase.New(endpointConfigurationName, remoteAddress)
    End Sub

    Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String, ByVal company As String)
        MyBase.New(endpointConfigurationName, remoteAddress)
        Me.INDCompany = company
    End Sub

    Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As EndpointAddress)
        MyBase.New(endpointConfigurationName, remoteAddress)
    End Sub

    Public Sub New(ByVal binding As Binding, ByVal remoteAddress As EndpointAddress)
        MyBase.New(binding, remoteAddress)
    End Sub

#End Region

#Region "IDataStore Members"

    Public ReadOnly Property AutoCreateOption As DevExpress.Xpo.DB.AutoCreateOption Implements DevExpress.Xpo.DB.IDataStore.AutoCreateOption
        Get
            'evitar que se hagan cambios en la BD
            Return AutoCreateOption.SchemaAlreadyExists
        End Get
    End Property

    Public Function ModifyData(ByVal ParamArray dmlStatements() As DevExpress.Xpo.DB.ModificationStatement) As DevExpress.Xpo.DB.ModificationResult Implements DevExpress.Xpo.DB.IDataStore.ModifyData
        Try
            Return Channel.ModifyData(INDCompany, dmlStatements)
        Catch ex As FaultException(Of LockingException)
            Throw ex.Detail
        End Try
    End Function

    Public Function SelectData(ByVal ParamArray selects() As DevExpress.Xpo.DB.SelectStatement) As DevExpress.Xpo.DB.SelectedData Implements DevExpress.Xpo.DB.IDataStore.SelectData
        Try
            Return Channel.SelectData(INDCompany, selects)
        Catch ex As Exception
            Dim gg = ex.Message
        End Try
    End Function

    Public Function UpdateSchema(ByVal dontCreateIfFirstTableNotExist As Boolean, ByVal ParamArray tables() As DevExpress.Xpo.DB.DBTable) As DevExpress.Xpo.DB.UpdateSchemaResult Implements DevExpress.Xpo.DB.IDataStore.UpdateSchema
        Return Channel.UpdateSchema(dontCreateIfFirstTableNotExist, tables)
    End Function

#End Region
End Class
