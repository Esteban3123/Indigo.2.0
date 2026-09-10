Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities.Service
Imports Application.Treasury
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Base

Public Class VReportConsignmentTransferAdminService
    Implements IVReportConsignmentTransferAdminService

    ''' <summary>
    ''' Variable tipo repositorio para un traslado o consignación
    ''' </summary>
    ''' <remarks></remarks>
    Private _vReportConsignmentTransfer As IVReportConsignmentTransferRepository

    Public Sub New(ByVal vReportConsignmentTransfer As IVReportConsignmentTransferRepository)
        If vReportConsignmentTransfer Is Nothing Then
            Throw New ArgumentNullException("vReportConsignmentTransfer Vacio")
        End If
        _vReportConsignmentTransfer = vReportConsignmentTransfer
    End Sub

    ''' <summary>
    ''' obtiene un traslado o consignación por codigo
    ''' </summary>
    ''' <param name="Code">codigo del traslado o consignación</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVReportConsignmentTransferByCode(Code As String) As Domain.Entities.VReportConsignmentTransfer Implements IVReportConsignmentTransferAdminService.GetVReportConsignmentTransferByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Code")
        End If
        Try
            Dim VreportConsignmentTransfer As VReportConsignmentTransfer = Me._vReportConsignmentTransfer.GetVReportConsignmentTransferByCode(Code.Trim())
            Return VreportConsignmentTransfer
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
            _vReportConsignmentTransfer = Nothing
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
