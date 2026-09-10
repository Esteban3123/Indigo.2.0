Imports System.Data.Entity.Core
Imports Application.Base
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class InvoiceCopayAdminService
    Implements IInvoiceCopayAdminService, Inject

    Private ReadOnly _invoiceCopayRepository As IInvoiceCopayRepository

    Public Sub New(invoiceCopayRepository As IInvoiceCopayRepository)
        _invoiceCopayRepository = invoiceCopayRepository
    End Sub


    Public Function Create(invoiceCopay As InvoiceCopay, audit As AuditMessage) As ActionResult(Of InvoiceCopay) Implements IInvoiceCopayAdminService.Create
        Dim uow = _invoiceCopayRepository.UnitWork
        Try
            invoiceCopay.CreationUser = audit.CodeUser
            invoiceCopay.CreationDate = Date.Now

            _invoiceCopayRepository.SaveEntity(invoiceCopay)
            uow.Commit()

            Return New ActionResult(Of InvoiceCopay) With {.ObjectEmbbeded = invoiceCopay, .StateResult = True, .StatusCode = eStatusResult.SUCCESS}
        Catch ex As OptimisticConcurrencyException
            uow.RollbackChanges()
            Return New ActionResult(Of InvoiceCopay) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            uow.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of InvoiceCopay) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function Remove(invoiceCopay As InvoiceCopay) As ActionResult Implements IInvoiceCopayAdminService.Remove
        Dim unitOfWork As IUnitWork = Me._invoiceCopayRepository.UnitWork
        Try
            invoiceCopay.MarkAsDeleted()
            Me._invoiceCopayRepository.SaveEntity(invoiceCopay)
            unitOfWork.Commit()
            Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    ' Protected Overrides Sub Finalize()
    '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

End Class
