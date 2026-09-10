'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 31-03-2014
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

Public Class MoneyAdvanceAdminService
    Implements IMoneyAdvanceAdminService

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _moneyAdvanceRepository As IMoneyAdvanceRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequensePaymentsDRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal moneyAdvanceRepository As IMoneyAdvanceRepository, ByVal secuenseDRepository As ISequensePaymentsDRepository)
        If moneyAdvanceRepository Is Nothing Then
            Throw New ArgumentNullException("moneyAdvanceRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _moneyAdvanceRepository = moneyAdvanceRepository
        Me._secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Elimina un anticipo
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMoneyAdvance(moneyAdvance As AdvancePayments, audit As AuditMessage, Optional ByVal withCommit As Boolean = True) As ActionResult Implements IMoneyAdvanceAdminService.DeleteMoneyAdvance
        If moneyAdvance Is Nothing Then
            Throw New ArgumentNullException("moneyAdvance")
        End If
        Dim unitOfWork As IUnitWork = Me._moneyAdvanceRepository.UnitWork
        Try
            If moneyAdvance.ChangeTracker.State = ObjectState.Deleted Then
                Me._moneyAdvanceRepository.DeleteEntity(moneyAdvance)
                If withCommit Then
                    unitOfWork.Commit()
                End If
                'Auditoria básica
                IndigoAuditBasic.Execute(GetType(AdvancePayments).Name, audit.Functional, moneyAdvance.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                'Ausitoria avanzada
                Dim auditObject As New IndigoAuditSimpleEntity(Of AdvancePayments)(moneyAdvance, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()
                Return New ActionResult With {.StateResult = True}
            Else
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            End If
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un anticipo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMoneyAdvance(code As String, audit As AuditMessage) As AdvancePayments Implements IMoneyAdvanceAdminService.GetMoneyAdvance
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim moneyAdvance As AdvancePayments = Me._moneyAdvanceRepository.GetMoneyAdvance(code.Trim())
            If moneyAdvance IsNot Nothing AndAlso moneyAdvance.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AdvancePayments)(moneyAdvance, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return moneyAdvance
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un anticipo
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveMoneyAdvance(moenyAdvance As AdvancePayments, audit As AuditMessage, Optional idSequense As Long = 0, Optional ByVal withCommit As Boolean = True) As ActionResult(Of AdvancePayments) Implements IMoneyAdvanceAdminService.SaveMoneyAdvance
        If moenyAdvance Is Nothing Then
            Throw New ArgumentNullException("moenyAdvance")
        End If
        Dim unitOfWork As IUnitWork = Me._moneyAdvanceRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As PaymentsSecuenceDetail = Nothing
            If moenyAdvance.Code Is Nothing OrElse moenyAdvance.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        moenyAdvance.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of AdvancePayments) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of AdvancePayments) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxMoneyAdvance As AdvancePayments = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of AdvancePayments)
            Dim status As Integer

            If moenyAdvance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                moenyAdvance.CreationUser = audit.CodeUser
                moenyAdvance.CreationDate = DateTime.Now
                moenyAdvance.ConfirmationUser = audit.IdUser
                moenyAdvance.ConfirmationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Confirm
            End If

            If moenyAdvance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse moenyAdvance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._moneyAdvanceRepository.SaveEntity(moenyAdvance)
            End If
            If withCommit Then
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
            End If
            auditProcess = New IndigoAuditSimpleEntity(Of AdvancePayments)(moenyAdvance, audit, status, auxMoneyAdvance)
            auditProcess.Execute()

            Return New ActionResult(Of AdvancePayments) With {.StateResult = True, .ObjectEmbbeded = moenyAdvance}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of AdvancePayments) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AdvancePayments) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AdvancePayments) Implements IMoneyAdvanceAdminService.ChangeState
        Dim moneyAdvance As AdvancePayments = _moneyAdvanceRepository.GetMoneyAdvance(code)
        moneyAdvance.Status = state
        Return SaveMoneyAdvance(moneyAdvance, audit)
    End Function

    ''' <summary>
    ''' Obtiene el anticipo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMoneyAdvanceById(id As Integer, audit As AuditMessage) As AdvancePayments Implements IMoneyAdvanceAdminService.GetMoneyAdvanceById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim moneyAdvance As AdvancePayments = Me._moneyAdvanceRepository.GetMoneyAdvanceById(id)
            If moneyAdvance IsNot Nothing AndAlso moneyAdvance.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AdvancePayments)(moneyAdvance, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return moneyAdvance
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los avances por tercero
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">ThirdId</exception>
    Public Function ListAdvancePaymentByThirdId(ThirdId As Integer) As ActionResult(Of List(Of AdvancePayments)) Implements IMoneyAdvanceAdminService.ListAdvancePaymentByThirdId
        If ThirdId = 0 Then
            Throw New ArgumentNullException("ThirdId")
        End If
        Try
            Dim moneyAdvance As List(Of AdvancePayments) = Me._moneyAdvanceRepository.ListAdvancePaymentByThirdId(ThirdId)
            Return New ActionResult(Of List(Of AdvancePayments)) With {.StateResult = True, .ObjectEmbbeded = moneyAdvance}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of AdvancePayments)) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el anticipo por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAdvanceByCode(ByVal code As String) As AdvancePayments Implements IMoneyAdvanceAdminService.GetAdvanceByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        Try
            Dim moneyAdvance As AdvancePayments = Me._moneyAdvanceRepository.GetAdvanceByCode(code.Trim())
            'If moneyAdvance IsNot Nothing AndAlso moneyAdvance.Id > 0 Then
            '    IndigoAuditSimpleEntity(Of AdvancePayments).Execute(moneyAdvance, Nothing, Infrastructure.CrossCutting.Audit.Actions.Print, Nothing)
            'End If
            Return moneyAdvance
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
            _moneyAdvanceRepository = Nothing
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
