'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 16-04-2013
'
' Last Modified By : Cristhian Mauricio Salazar
' Last Modified On : 07-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.Data.PayrollRepository
Imports Infrastructure.CrossCutting.Base
Imports System.Data.Entity.Infrastructure
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Core

Public Class FundsAdminService
    Implements IFundsAdminService

    Private Const FORM_NAME As String = "FrmFunds"
    Private _FundsRepository As IFundsLevelRepository


    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository

    Private _customerRepository As Domain.Entities.ICustomerRepository

    Private _thirdPartyRepository As Domain.Entities.IThirdPartyRepository

    ''' <summary>
    ''' Inicializa el repositorio de FundsLevels
    ''' </summary>
    ''' <param name="repository"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IFundsLevelRepository, ByVal secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository, ByVal customerRepository As Domain.Entities.ICustomerRepository,
                   thirdPartyRepository As Domain.Entities.IThirdPartyRepository)
        If (repository Is Nothing = True) Then
            Throw New ArgumentNullException("FundsRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _FundsRepository = repository
        _secuenseDRepository = secuenseDRepository
        _customerRepository = customerRepository
        _thirdPartyRepository = thirdPartyRepository
    End Sub

    ''' <summary>
    ''' Elimina un Fondo
    ''' </summary>
    ''' <param name="Funds">Fondo</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>True o False</returns>
    Public Function DeleteFunds(Funds As Fund, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IFundsAdminService.DeleteFunds
        'Dim result As New ActionMessageResult(Of Fund)
        'result.StateResult = True
        'If (Funds Is Nothing = True) Then
        '    Throw New ArgumentNullException("Funds Vacío")
        'End If
        'Dim UnitOfWork As IUnitWork = _FundsRepository.UnitWork
        'Try
        '    _FundsRepository.DeleteEntity(Funds)
        '    UnitOfWork.Commit()
        '    '/***** Auditoria Basica ********/
        '    IndigoAuditBasic.Execute(Funds.GetType.Name, audit.Functional, Funds.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
        '    '/*****Auditoria Avanzada ******/
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of Fund)(Funds, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return result
        'Catch ex As DbUpdateException
        '    UnitOfWork.RollbackChanges()
        '    result.StateResult = False
        '    result.MessageResult.Add(New MessageResult("c-0000", Funds.Code))
        '    Return result
        'Catch ex As Exception
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    UnitOfWork.RollbackChanges()
        '    result.StateResult = False
        '    Return result
        'End Try

        If Funds Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._FundsRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Funds.ModificationUser = audit.CodeUser
                Funds.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Fund)(Funds, audit, status)

                'While invoiceCategory.InvoiceCategoriesUser.Count > 0
                '    invoiceCategory.InvoiceCategoriesUser(invoiceCategory.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                Funds.MarkAsDeleted()
                Me._FundsRepository.SaveEntity(Funds)
                UnitOfWork.Commit()
                auditProcess.Execute()
                IndigoAuditBasic.Execute(Funds.GetType.Name, audit.Functional, Funds.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un Fondo en Específico
    ''' </summary>
    ''' <param name="code">Código de Fondo</param>
    ''' <returns>Fondo</returns>
    Public Function GetFunds(code As String) As Fund Implements IFundsAdminService.GetFunds
        If (String.IsNullOrEmpty(code) = True) Then
            Throw New ArgumentNullException("Code Vacío")
        End If
        Try
            Return _FundsRepository.GetFunds(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista Todos los Fondos
    ''' </summary>
    ''' <returns>Fondos</returns>
    Public Function ListAllFunds() As List(Of Fund) Implements IFundsAdminService.ListAllFunds
        Try
            Return _FundsRepository.ListAllFunds()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba o Actualiza un Fondo y sus agregados
    ''' </summary>
    ''' <param name="Funds">Fondo</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>True o False</returns>
    Public Function SaveFunds(Funds As Fund, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequence As Int64 = 0) As ActionResult(Of Fund) Implements IFundsAdminService.SaveFunds

        If Funds Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._FundsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Dim unitOfWorkCustomer As IUnitWork = Me._customerRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(Funds.Code) Then
                    Dim seq As Domain.Entities.PayrollSequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Funds.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Fund) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PayrollSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), Funds.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Fund) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As Fund = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Fund)
                Dim status As Integer

                If Funds.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Funds.CreationUser = audit.CodeUser
                    Funds.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _FundsRepository.GetFunds(Funds.Code, False)
                    Funds.ModificationUser = audit.CodeUser
                    Funds.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Dim ObjCustomer = _customerRepository.GetCustomerByThirdPartyId(Funds.ThirdPartyId)

                If ObjCustomer IsNot Nothing And ObjCustomer.Id > 0 Then

                    If Funds.Term <> ObjCustomer.Term Then
                        Funds.Term = ObjCustomer.Term
                    End If

                Else
                    Dim ObjThirdParty = _thirdPartyRepository.GetThirdPartyById(Funds.ThirdPartyId)
                    Dim NewObjCustomer As New Domain.Entities.Customer

                    NewObjCustomer.Nit = ObjThirdParty.Nit
                    NewObjCustomer.Name = ObjThirdParty.Name
                    NewObjCustomer.EPSCode = Funds.MinistryCode
                    NewObjCustomer.ThirdPartyId = Funds.ThirdPartyId
                    NewObjCustomer.Term = Funds.Term
                    NewObjCustomer.State = 1
                    NewObjCustomer.MainAccountReceivableId = Funds.MainAccountReceivableId
                    NewObjCustomer.CreationUser = audit.CodeUser
                    NewObjCustomer.CreationDate = DateTime.Now
                    Me._customerRepository.SaveEntity(NewObjCustomer)

                    unitOfWorkCustomer.Commit()

                End If

                Me._FundsRepository.SaveEntity(Funds)
                UnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Fund)(Funds, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                Funds.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of Fund) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Funds, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWorkCustomer.RollbackChanges()
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Fund) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWorkCustomer.RollbackChanges()
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Fund) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un tercero apartir del nit
    ''' </summary>
    ''' <param name="nit">Nit del tercero</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyByNit(nit As String) As ThirdParty Implements IFundsAdminService.GetThirdPartyByNit
        If String.IsNullOrEmpty(nit) Then
            Throw New ArgumentNullException("Nit Vacio")
        End If
        Try
            Return _FundsRepository.GetThirdPartyByNit(nit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ThirdParty()
        End Try
    End Function

    Public Function ChangeStateFund(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Fund) Implements IFundsAdminService.ChangeStateFund
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Fund As Fund = Me._FundsRepository.GetFunds(code.Trim())
            If Fund IsNot Nothing AndAlso Fund.Id > 0 Then
                Fund.State = state
            End If
            Dim result = Me.SaveFunds(Fund, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Fund) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _FundsRepository = Nothing
            _secuenseDRepository = Nothing
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
