#Region "Imports"

Imports System.ServiceModel.Activation
Imports DevExpress.Xpo.DB
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Calse base para servicios WCF que proveen acceso a datos via DataStore
''' para multiples bases de datos MS SqlServer
''' </summary>
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)> _
Public Class MSSqlMultiDataStoreService
    Inherits ServiceBase
    Implements IXpoGateEx

#Region "Fields"

    ''' <summary>
    ''' Delegado a la función encargada de obtener el proveedor
    ''' de acceso a la base de datos
    ''' </summary>
    Private _dataStoreProvider As Func(Of String, DataStoreProviderEntry)

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="dataStoreProvider">Función que me retorna el proveedor de datos</param>
    Public Sub New(ByVal dataStoreProvider As Func(Of String, DataStoreProviderEntry))
        Me._dataStoreProvider = dataStoreProvider

        AddHandler Me.ServiceExceptionThrown, Function(sender, e) HandledException(sender, e)
    End Sub

#End Region

#Region "Methods"

    Public Function GetAutoCreateOption() As OperationResult(Of AutoCreateOption) Implements IXpoGateEx.GetAutoCreateOption
        Try
            Return New OperationResult(Of AutoCreateOption)(AutoCreateOption.SchemaAlreadyExists)
        Catch ex As Exception
            Helper.HandledException(ex)
            Return Nothing
        End Try
    End Function

    Public Function ModifyData(company As String, ParamArray dmlStatements() As ModificationStatement) As OperationResult(Of ModificationResult) Implements IXpoGateEx.ModifyData
        Try
            Return New OperationResult(Of ModificationResult) 'MyBase.Execute(Of ModificationResult)(Function() CType(Me._dataStoreProvider(company).Provider, IDataStore).ModifyData(dmlStatements))
        Catch ex As Exception
            Helper.HandledException(ex)
            Return Nothing
        End Try
    End Function

    Public Function SelectData(company As String, ParamArray selects() As SelectStatement) As OperationResult(Of SelectedData) Implements IXpoGateEx.SelectData
        Try
            Return MyBase.Execute(Of SelectedData)(Function() CType(Me._dataStoreProvider(company).Provider, IDataStore).SelectData(selects))
        Catch ex As Exception
            Helper.HandledException(ex)
            Return Nothing
        End Try
    End Function

    Public Function UpdateSchema(dontCreateIfFirstTableNotExist As Boolean, ParamArray tables() As DBTable) As OperationResult(Of UpdateSchemaResult) Implements IXpoGateEx.UpdateSchema
        Try
            Return MyBase.Execute(Of UpdateSchemaResult)(Function() UpdateSchemaResult.SchemaExists)
        Catch ex As Exception
            Helper.HandledException(ex)
            Return Nothing
        End Try
    End Function

    Private Function HandledException(ByVal sender As Object, ByVal e As ServiceExceptionEventArgs) As Boolean
        Return Helper.HandledException(e.Exception)
    End Function

#End Region

End Class
