'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities.Service

Public Class InvoiceAdminService
    Implements IInvoiceAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de facturas
    ''' </summary>
    Private _invoiceRepository As IInvoiceRepository
    Private _invoiceDetailRepository As IBillingInvoiceDetailRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal invoiceRepository As IInvoiceRepository, invoiceDetailRepository As IBillingInvoiceDetailRepository)
        If invoiceRepository Is Nothing Then
            Throw New ArgumentNullException("invoiceRepository")
        End If
        _invoiceRepository = invoiceRepository
        _invoiceDetailRepository = invoiceDetailRepository
    End Sub

    ''' <summary>
    ''' Anula una factura
    ''' </summary>
    Public Function AnularInvoice(invoice As Invoice, audit As AuditMessage) As ActionResult Implements IInvoiceAdminService.AnularInvoice
        If invoice Is Nothing Then
            Throw New ArgumentNullException("distributionManpower")
        End If
        Dim unitOfWork As IUnitWork = Me._invoiceRepository.UnitWork
        Try
            While invoice.InvoiceDetail.Count > 0
                invoice.InvoiceDetail.Item(invoice.InvoiceDetail.Count() - 1).MarkAsDeleted()
            End While
            invoice.MarkAsDeleted()
            Me._invoiceRepository.SaveEntity(invoice)
            unitOfWork.Commit()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una factura por id
    ''' </summary>
    Public Function GetInvoiceById(Id As Integer) As Invoice Implements IInvoiceAdminService.GetInvoiceById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._invoiceRepository.GetInvoiceById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda una factura
    ''' </summary>
    Public Function SaveInvoice(invoice As Invoice, audit As AuditMessage) As ActionResult(Of Invoice) Implements IInvoiceAdminService.SaveInvoice
        If invoice Is Nothing Then
            Throw New ArgumentNullException("invoice")
        End If
        Dim unitOfWork As IUnitWork = Me._invoiceRepository.UnitWork
        Try
            Dim status As Integer
            If invoice.ChangeTracker.State = ObjectState.Added Then
                invoice.InvoicedDate = Date.Now
                invoice.InvoicedUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If
            If _invoiceDetailRepository.SaveListInvoiceDetail(invoice.InvoiceDetail.ToList()) Then
                Me._invoiceRepository.SaveEntity(invoice)
                unitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Invoice)(invoice, audit, status)
                auditProcess.Execute()
                Return New ActionResult(Of Invoice) With {.StateResult = True, .ObjectEmbbeded = invoice}
            Else
                Return New ActionResult(Of Invoice) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            End If
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Invoice) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Invoice) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Lista los ids de las facturas anuladas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAnnullateInvoiceIdByAdmission(admission As String) As List(Of Integer) Implements IInvoiceAdminService.ListAnnullateInvoiceIdByAdmission
        Try
            Return _invoiceRepository.ListAnnullateInvoiceIdByAdmission(admission)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una factura por numero de factura
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInvoiceByInvoiceNumber(invoiceNumber As String, ByVal audit As AuditMessage) As Invoice Implements IInvoiceAdminService.GetInvoiceByInvoiceNumber
        Try
            Dim invoice = _invoiceRepository.GetInvoiceByInvoiceNumber(invoiceNumber)
            If invoice IsNot Nothing AndAlso invoice.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Invoice)(invoice, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return invoice
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Invoice
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _invoiceRepository = Nothing
            _invoiceDetailRepository = Nothing
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