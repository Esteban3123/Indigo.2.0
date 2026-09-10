'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
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

Public Class MarketingUnitAdminService
    Implements IMarketingUnitAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _marketingUnitRepository As IMarketingUnitRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseContractDRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal marketingUnitRepository As IMarketingUnitRepository, ByVal secuenseDRepository As ISequenseContractDRepository)
        If marketingUnitRepository Is Nothing Then
            Throw New ArgumentNullException("marketingUnitRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _marketingUnitRepository = marketingUnitRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateMarketingUnit(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of MarketingUnit) Implements IMarketingUnitAdminService.ChangeStateMarketingUnit
        Dim MarketingUnit As MarketingUnit = _marketingUnitRepository.GetMarketingUnit(code)
        MarketingUnit.Status = state
        Return SaveMarketingUnit(MarketingUnit, audit)
    End Function

    ''' <summary>
    ''' Elimina una unidad de mercadeo
    ''' </summary>
    ''' <param name="MarketingUnit"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMarketingUnit(MarketingUnit As MarketingUnit, audit As AuditMessage) As ActionResult Implements IMarketingUnitAdminService.DeleteMarketingUnit
        If MarketingUnit Is Nothing Then
            Throw New ArgumentNullException("MarketingUnit")
        End If
        Dim unitOfWork As IUnitWork = Me._marketingUnitRepository.UnitWork
        Try
            MarketingUnit.StartTracking()
            While MarketingUnit.MarketingUnitCups.Count > 0
                MarketingUnit.MarketingUnitCups.Item(0).MarkAsDeleted()
            End While
            MarketingUnit.MarkAsDeleted()
            Dim auditProcess As IndigoAuditSimpleEntity(Of MarketingUnit)
            auditProcess = New IndigoAuditSimpleEntity(Of MarketingUnit)(MarketingUnit, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._marketingUnitRepository.SaveEntity(MarketingUnit)
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
    ''' Obtiene una unidad de mercadeo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMarketingUnit(code As String, audit As AuditMessage) As ActionResult(Of MarketingUnit) Implements IMarketingUnitAdminService.GetMarketingUnit
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim MarketingUnit As MarketingUnit = Me._marketingUnitRepository.GetMarketingUnit(code.Trim())
            If MarketingUnit IsNot Nothing AndAlso MarketingUnit.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of MarketingUnit)(MarketingUnit, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of MarketingUnit) With {.StateResult = True, .ObjectEmbbeded = MarketingUnit}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MarketingUnit) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una unidad de mercadeo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMarketingUnitById(id As Integer, audit As AuditMessage) As ActionResult(Of MarketingUnit) Implements IMarketingUnitAdminService.GetMarketingUnitById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim MarketingUnit As MarketingUnit = Me._marketingUnitRepository.GetMarketingUnitById(id)
            If MarketingUnit IsNot Nothing AndAlso MarketingUnit.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of MarketingUnit)(MarketingUnit, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of MarketingUnit) With {.StateResult = True, .ObjectEmbbeded = MarketingUnit}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MarketingUnit) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una unidad de medida
    ''' </summary>
    ''' <param name="MarketingUnit"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveMarketingUnit(MarketingUnit As MarketingUnit, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MarketingUnit) Implements IMarketingUnitAdminService.SaveMarketingUnit
        If MarketingUnit Is Nothing Then
            Throw New ArgumentNullException("MarketingUnit")
        End If
        Dim unitOfWork As IUnitWork = Me._marketingUnitRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As ContractSequenceDetail = Nothing
            If MarketingUnit.Code Is Nothing OrElse MarketingUnit.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        MarketingUnit.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of MarketingUnit) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of MarketingUnit) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxMarketingUnit As MarketingUnit = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of MarketingUnit)
            Dim status As Integer

            If MarketingUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                MarketingUnit.CreationUser = audit.CodeUser
                MarketingUnit.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxMarketingUnit = MarketingUnit.OriginalValue
                MarketingUnit.ModificationUser = audit.CodeUser
                MarketingUnit.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._marketingUnitRepository.SaveEntity(MarketingUnit)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of MarketingUnit)(MarketingUnit, audit, status, auxMarketingUnit)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            MarketingUnit.MarkAsUnchanged()

            Return New ActionResult(Of MarketingUnit) With {.StateResult = True, .ObjectEmbbeded = MarketingUnit}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of MarketingUnit) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MarketingUnit) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _marketingUnitRepository = Nothing
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
