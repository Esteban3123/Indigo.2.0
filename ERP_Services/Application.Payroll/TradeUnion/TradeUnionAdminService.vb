'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 15-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Payroll.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Payroll

#End Region

Public Class TradeUnionAdminService
    Implements ITradeUnionAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _tradeUnionRepository As ITradeUnionRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IPayrollSequenceDetailRepository


#End Region

#Region "Build"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal tradeUnionRepository As ITradeUnionRepository, ByVal secuenseDRepository As IPayrollSequenceDetailRepository)
        If tradeUnionRepository Is Nothing Then
            Throw New ArgumentNullException("tradeUnionRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _tradeUnionRepository = tradeUnionRepository
        Me._secuenseDRepository = secuenseDRepository
    End Sub

#End Region


    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of TradeUnion) Implements ITradeUnionAdminService.ChangeState
        Dim tradeUnion As TradeUnion = _tradeUnionRepository.GetTradeUnionByCode(code, True)
        tradeUnion.Status = state
        Return SaveTradeUnion(tradeUnion, audit)
    End Function

    ''' <summary>
    ''' Elimina un sindicato de pago
    ''' </summary>
    ''' <param name="TradeUnion"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteTradeUnion(tradeUnion As TradeUnion, audit As AuditMessage) As ActionResult Implements ITradeUnionAdminService.DeleteTradeUnion
        If tradeUnion Is Nothing Then
            Throw New ArgumentNullException("tradeUnion")
        End If
        Dim unitOfWork As IUnitWork = Me._tradeUnionRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of TradeUnion)
            auditProcess = New IndigoAuditSimpleEntity(Of TradeUnion)(tradeUnion, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._tradeUnionRepository.DeleteEntity(tradeUnion)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
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
    ''' Obtiene un sindicato de pago por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTradeUnionByCode(code As String, tracking As Boolean, audit As AuditMessage) As ActionResult(Of TradeUnion) Implements ITradeUnionAdminService.GetTradeUnionByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim tradeUnion As TradeUnion = Me._tradeUnionRepository.GetTradeUnionByCode(code.Trim(), tracking)
            If tradeUnion IsNot Nothing AndAlso tradeUnion.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of TradeUnion)(tradeUnion, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of TradeUnion) With {.StateResult = True, .ObjectEmbbeded = tradeUnion}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TradeUnion) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    '''  Obtiene un sindicato de pago por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTradeUnionById(id As Integer, tracking As Boolean, audit As AuditMessage) As ActionResult(Of TradeUnion) Implements ITradeUnionAdminService.GetTradeUnionById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim tradeUnion As TradeUnion = Me._tradeUnionRepository.GetTradeUnionById(id, tracking)
            If tradeUnion IsNot Nothing AndAlso tradeUnion.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of TradeUnion)(tradeUnion, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of TradeUnion) With {.StateResult = True, .ObjectEmbbeded = tradeUnion}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TradeUnion) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un sindicato
    ''' </summary>
    ''' <param name="TradeUnion"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveTradeUnion(tradeUnion As TradeUnion, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of TradeUnion) Implements ITradeUnionAdminService.SaveTradeUnion
        If tradeUnion Is Nothing Then
            Throw New ArgumentNullException("paymentConcept")
        End If
        Dim unitOfWork As IUnitWork = Me._tradeUnionRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As PayrollSequenceDetail = Nothing
            If tradeUnion.Code Is Nothing OrElse tradeUnion.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        tradeUnion.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of TradeUnion) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of TradeUnion) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxTradeUnion As TradeUnion = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of TradeUnion)
            Dim status As Integer

            If tradeUnion.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                tradeUnion.CreationUser = audit.CodeUser
                tradeUnion.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxTradeUnion = _tradeUnionRepository.GetTradeUnionByCode(tradeUnion.Code, False)
                tradeUnion.ModificationUser = audit.CodeUser
                tradeUnion.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._tradeUnionRepository.SaveEntity(tradeUnion)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of TradeUnion)(tradeUnion, audit, status, auxTradeUnion)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            tradeUnion.MarkAsUnchanged()

            Return New ActionResult(Of TradeUnion) With {.StateResult = True, .ObjectEmbbeded = tradeUnion}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of TradeUnion) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TradeUnion) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _tradeUnionRepository = Nothing
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
