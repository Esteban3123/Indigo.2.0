#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class JournalVoucherDetailAdminService
    Implements IJournalVoucherDetailAdminService

#Region "Fields"

    Private _journalVoucherDetailRepository As IJournalVoucherDetailRepository

#End Region

#Region "Builder"

    Public Sub New(ByVal journalVoucherDetailRepository As IJournalVoucherDetailRepository)
        If journalVoucherDetailRepository Is Nothing Then
            Throw New ArgumentNullException("journalVoucherDetailRepository")
        End If

        _journalVoucherDetailRepository = journalVoucherDetailRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetJournalVoucherDetails(journalVoucherId As Integer) As ActionResult(Of List(Of JournalVoucherDetails)) Implements IJournalVoucherDetailAdminService.GetJournalVoucherDetails
        Try
            Dim journalVoucherDetails As List(Of JournalVoucherDetails) = Me._journalVoucherDetailRepository.GetJournalVoucherDetails(journalVoucherId)
            Return New ActionResult(Of List(Of JournalVoucherDetails)) With {.StateResult = True, .ObjectEmbbeded = journalVoucherDetails}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of JournalVoucherDetails)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If

            _journalVoucherDetailRepository = Nothing
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
