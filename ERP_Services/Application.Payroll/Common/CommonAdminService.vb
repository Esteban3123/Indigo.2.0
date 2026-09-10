
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base

Public Class CommonAdminService
    Implements ICommonAdminService

    ' Repositorio para obtener la unidad de trabajo
    Dim _CommonRepository As IBranchOfficeRepository

    ''' <summary>
    ''' contructor que inicia el repositorio
    ''' </summary>
    ''' <param name="commonRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal commonRepository As IBranchOfficeRepository)
        If commonRepository Is Nothing Then
            Throw New ArgumentNullException("CommmonRepository vacio")
        End If
        _CommonRepository = commonRepository
    End Sub

    ''' <summary>
    ''' consulta los campos NULL para customizacion
    ''' </summary>
    ''' <returns></returns>
    Public Function ConsultarCamposNULL(TableName As String) As DataSet Implements ICommonAdminService.ConsultarCamposNULL
        Try
            Dim UnitOfWork As IUnitWork = TryCast(_CommonRepository.UnitWork, IUnitWork)
            Return UnitOfWork.ExecuteQueryDataSet("", TableName)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _CommonRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
