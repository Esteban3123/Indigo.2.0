Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base

Public Class BankFileDetailAdminService
    Implements IBankFileDetailAdminService

#Region "Properties"

    Private _BankFileDetailRepository As IBankFileDetailRepository

#End Region

#Region "Builder"

    Public Sub New(BankFileDetailRepository As IBankFileDetailRepository)
        If BankFileDetailRepository Is Nothing Then
            Throw New ArgumentNullException("BankFileDetailRepository")
        End If

        Me._BankFileDetailRepository = BankFileDetailRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetBankFileDetailByBankFileId(id As Integer) As List(Of BankFileDetail) Implements IBankFileDetailAdminService.GetBankFileDetailByBankFileId
        Try
            Return Me._BankFileDetailRepository.GetBankFileDetailByBankFileId(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of BankFileDetail)
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

            Me._BankFileDetailRepository = Nothing
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
