'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
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
Imports Domain.InteropCost
Imports Domain.InteropCost.Entities
Imports System.Transactions

Public Class GeneralExpenseAdminService
    Implements IGeneralExpenseAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de gastos generales
    ''' </summary>
    Private _generalExpenseRepository As IGeneralExpensesRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As IInteropCostSequenceDetailRepository

    ''' <summary>
    ''' repositorio de cuentas contables
    ''' </summary>
    Private _mainAccountRepository As IMainAccountRepository

    Private _costCenterRepository As ICostCenterRepository
#End Region

#Region "Methods"

    Public Sub New(ByVal generalExpenseRepository As IGeneralExpensesRepository, ByVal sequenceDRepository As IInteropCostSequenceDetailRepository, ByVal mainAccountRepository As IMainAccountRepository,
                   costCenterRepository As ICostCenterRepository)
        If generalExpenseRepository Is Nothing Then
            Throw New ArgumentNullException("generalExpenseRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        If mainAccountRepository Is Nothing Then
            Throw New ArgumentNullException("mainAccountRepository")
        End If
        _generalExpenseRepository = generalExpenseRepository
        _sequenceDRepository = sequenceDRepository
        _mainAccountRepository = mainAccountRepository
        _costCenterRepository = costCenterRepository
    End Sub

    ''' <summary>
    ''' Elimina un centro de produccion
    ''' </summary>
    Public Function DeleteGeneralExpense(generalExpense As GeneralExpense, audit As AuditMessage) As ActionResult Implements IGeneralExpenseAdminService.DeleteGeneralExpense
        If generalExpense Is Nothing Then
            Throw New ArgumentNullException("productionCenter")
        End If
        Dim unitOfWork As IUnitWork = Me._generalExpenseRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While generalExpense.DistributionBase.Count > 0
                    Dim _distribBase As DistributionBase = generalExpense.DistributionBase.Item(generalExpense.DistributionBase.Count() - 1)
                    If _distribBase.DistributionBaseDetail IsNot Nothing AndAlso _distribBase.DistributionBaseDetail.Count > 0 Then
                        While _distribBase.DistributionBaseDetail.Count > 0
                            _distribBase.DistributionBaseDetail.Item(_distribBase.DistributionBaseDetail.Count() - 1).MarkAsDeleted()
                        End While
                    End If
                    _distribBase.MarkAsDeleted()
                End While

                generalExpense.MarkAsDeleted()
                generalExpense.ModificationUser = audit.CodeUser
                generalExpense.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of GeneralExpense)(generalExpense, audit, status)

                Me._generalExpenseRepository.SaveEntity(generalExpense)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
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

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    Public Function SaveGeneralExpense(generalExpense As GeneralExpense, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of GeneralExpense) Implements IGeneralExpenseAdminService.SaveGeneralExpense
        If generalExpense Is Nothing Then
            Throw New ArgumentNullException("generalExpense")
        End If
        Dim unitOfWork As IUnitWork = Me._generalExpenseRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(generalExpense.Code) Then
                    Dim seq As InteropCostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.InteropCostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            generalExpense.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of GeneralExpense) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                        MessageResult = If(seq.InteropCostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), generalExpense.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of GeneralExpense) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxExpenseConcept As GeneralExpense = Nothing
                Dim status As Integer
                If generalExpense.ChangeTracker.State = ObjectState.Added Then
                    If String.IsNullOrEmpty(MessageResult) Then
                        MessageResult = String.Format(ResourceManager.GetString("SavedWithCode"), generalExpense.Code)
                    End If
                    generalExpense.CreationDate = Date.Now
                    generalExpense.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    generalExpense.ModificationDate = Date.Now
                    generalExpense.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxExpenseConcept = generalExpense.OriginalValue
                End If

                Me._generalExpenseRepository.SaveEntity(generalExpense)
                unitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of GeneralExpense)(generalExpense, audit, status, auxExpenseConcept)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of GeneralExpense) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = generalExpense, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of GeneralExpense) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GeneralExpense) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    Public Function GetGeneralExpenseById(id As Integer) As GeneralExpense Implements IGeneralExpenseAdminService.GetGeneralExpenseById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._generalExpenseRepository.GetGeneralExpenseById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ListGeneralExpenseByStatus(status As Boolean) As List(Of GeneralExpense) Implements IGeneralExpenseAdminService.ListGeneralExpenseByStatus
        Try
            Return Me._generalExpenseRepository.ListGeneralExpenseByStatus(status)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of GeneralExpense)()
        End Try
    End Function

    ''' <summary>
    ''' Lista los gastos generales por id de cuenta contable
    ''' </summary>
    Public Function GetGeneralExpenseByMainAccountId(MainAccountId As Integer) As GeneralExpense Implements IGeneralExpenseAdminService.GetGeneralExpenseByMainAccountId
        If MainAccountId = 0 Then
            Throw New ArgumentNullException("MainAccountId")
        End If
        Try
            Return Me._generalExpenseRepository.GetGeneralExpenseByMainAccountId(MainAccountId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    Public Function GetGeneralExpense(code As String, audit As AuditMessage) As ActionResult(Of GeneralExpense) Implements IGeneralExpenseAdminService.GetGeneralExpense
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim generalExpense As GeneralExpense = Me._generalExpenseRepository.GetGeneralExpense(code.Trim())
            If generalExpense IsNot Nothing AndAlso generalExpense.Id > 0 Then

                Dim dictionaryMainAccount As New Dictionary(Of Integer, String)
                Dim dictionaryCostCenter As New Dictionary(Of Integer, String)

                For Each item In generalExpense.DistributionBase
                    For Each itemDetail In item.DistributionBaseDetail
                        'obtengo los nombres de las cuentas
                        If dictionaryMainAccount.ContainsKey(itemDetail.MainAccountId) Then
                            itemDetail.CodeNameMainAccount = dictionaryMainAccount(itemDetail.MainAccountId)
                        Else
                            Dim account = _mainAccountRepository.GetMainAccountById(itemDetail.MainAccountId)
                            itemDetail.CodeNameMainAccount = account.CUECODIGO & " - " & account.CUENOMBRE
                            dictionaryMainAccount.Add(itemDetail.MainAccountId, itemDetail.CodeNameMainAccount)
                        End If
                        'si tiene centro de costo se asigna el nombre y el codigo
                        If itemDetail.CostCenterId IsNot Nothing Then
                            If dictionaryCostCenter.ContainsKey(itemDetail.CostCenterId) Then
                                itemDetail.CodeNameCostCenter = dictionaryCostCenter(itemDetail.CostCenterId)
                            Else
                                Dim costCenter = _costCenterRepository.GetCostCenterById(itemDetail.CostCenterId)
                                itemDetail.CodeNameCostCenter = costCenter.CCCODIGO & " - " & costCenter.CCNOMBRE
                                dictionaryCostCenter.Add(itemDetail.CostCenterId, itemDetail.CodeNameCostCenter)
                            End If
                        End If
                    Next
                Next

                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of GeneralExpense)(generalExpense, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of GeneralExpense) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = generalExpense}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GeneralExpense) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function


    Public Function UpdateStateGeneralExpense(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of GeneralExpense) Implements IGeneralExpenseAdminService.UpdateStateGeneralExpense
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
            Dim generalExpense = Me._generalExpenseRepository.GetGeneralExpense(code.Trim())
            If generalExpense IsNot Nothing AndAlso generalExpense.Id > 0 Then
                generalExpense.Status = state
            End If
            Return Me.SaveGeneralExpense(generalExpense, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GeneralExpense) With {.StateResult = False}
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
            _sequenceDRepository = Nothing
            _mainAccountRepository = Nothing
            _costCenterRepository = Nothing
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