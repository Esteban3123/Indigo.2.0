'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions
Imports System.Data.Entity.Validation
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports System.Data.SqlClient

#End Region

Public Class AccountReceivableAdminService
    Implements IAccountReceivableAdminService

#Region "Fields"
    Dim _accountReceivableRepository As IAccountReceivableRepository

    Dim _sequenceDRepository As ISequensePortfolioDRepository
#End Region

#Region "Builder"
    Public Sub New(ByVal iAccountReceivableRepository As IAccountReceivableRepository, sequenceRepository As ISequensePortfolioDRepository)
        If iAccountReceivableRepository Is Nothing Then
            Throw New ArgumentNullException("_IAccountReceivableRepository")
        End If
        _accountReceivableRepository = iAccountReceivableRepository
        _sequenceDRepository = sequenceRepository
    End Sub
#End Region

    ''' <summary>
    ''' obtiene la factura por id
    ''' </summary>
    ''' <param name="idAccountReceivable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableById(idAccountReceivable As Integer) As AccountReceivable Implements IAccountReceivableAdminService.GetAccountReceivableById
        Try
            Return _accountReceivableRepository.GetAccountReceivableById(idAccountReceivable)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AccountReceivable
        End Try
    End Function

    Public Function GetAccountReceivableByInvoiceNumberAndCustomer(invoiceNumber As String, idCustomer As Integer) As AccountReceivable Implements IAccountReceivableAdminService.GetAccountReceivableByInvoiceNumberAndCustomer
        Try
            Return _accountReceivableRepository.GetAccountReceivableByInvoiceNumberAndCustomer(invoiceNumber, idCustomer)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AccountReceivable
        End Try
    End Function


    ''' <summary>
    ''' Guarda una cuenta por cobrar
    ''' </summary>
    ''' <param name="accountReceivable"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">accountReceivable</exception>
    Public Function SaveAccountReceivable(accountReceivable As AccountReceivable, audit As AuditMessage, Optional idSequence As Long = 0, Optional assignInvoiceNumber As Boolean = False) As ActionResult(Of AccountReceivable) Implements IAccountReceivableAdminService.SaveAccountReceivable
        If accountReceivable Is Nothing Then
            Throw New ArgumentNullException("accountReceivable")
        End If
        Dim unitOfWork As IUnitWork = Me._accountReceivableRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try

            If String.IsNullOrEmpty(accountReceivable.Code) Then
                Dim seq As PortfolioSequenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        accountReceivable.Code = res
                        If assignInvoiceNumber Then
                            accountReceivable.InvoiceNumber = res
                        End If
                        seq.Next += 1
                        Me._sequenceDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of AccountReceivable) With {.StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                Else
                    Return New ActionResult(Of AccountReceivable) With {.StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                End If
            End If

            Dim auxAccountReceivable As AccountReceivable = Nothing
            Dim status As Integer
            If accountReceivable.ChangeTracker.State = ObjectState.Added Then
                accountReceivable.CreationDate = Date.Now
                accountReceivable.CreationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                accountReceivable.ModificationDate = Date.Now
                accountReceivable.ModificationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxAccountReceivable = accountReceivable.OriginalValue
            End If
            Me._accountReceivableRepository.SaveEntity(accountReceivable)
            unitOfWork.Commit()
            Dim auditProcess As New IndigoAuditSimpleEntity(Of AccountReceivable)(accountReceivable, audit, status, auxAccountReceivable)
            auditProcess.Execute()
            Return New ActionResult(Of AccountReceivable) With {.StateResult = True, .ObjectEmbbeded = accountReceivable}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of AccountReceivable) With {.StateResult = False, .Message = ex.InnerException.InnerException.Message}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of AccountReceivable) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountReceivable) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function



    ''' <summary>
    ''' obtiene la Cuenta De Cobro por Código
    ''' </summary>
    ''' <param name="idAccountReceivable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableByCode(ByVal Code As String) As AccountReceivable Implements IAccountReceivableAdminService.GetAccountReceivableByCode
        Try
            Return _accountReceivableRepository.GetAccountReceivableByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AccountReceivable
        End Try
    End Function

    ''' <summary>
    ''' obtiene la Cuenta De Cobro por Número de Factura
    ''' </summary>
    ''' <param name="invoicenumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableByInvoiceNumber(ByVal invoiceNumber As String) As AccountReceivable Implements IAccountReceivableAdminService.GetAccountReceivableByInvoiceNumber
        If String.IsNullOrEmpty(invoiceNumber) Then
            Throw New ArgumentException("invoiceNumber")
        End If
        Try
            Dim accountReceivable As AccountReceivable = Me._accountReceivableRepository.GetAccountReceivableByInvoiceNumber(invoiceNumber)
            Return accountReceivable
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
            _accountReceivableRepository = Nothing
            _sequenceDRepository = Nothing
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
