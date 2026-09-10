'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 04-08-2014
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
Imports System.Transactions
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Resources
Imports System.Text

#End Region

Public Class BudgetModificationAdminService
    Implements IBudgetModificationAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de los rubros presupuestales
    ''' </summary>
    Private _BudgetModificationRepository As IBudgetModificationRepository

    ''' <summary>
    ''' Repositorio de las vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Private _BudgetaryValidityRepository As IValidityRepository

    ''' <summary>
    ''' Repositorio del control de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _BudgetControlRepository As IBudgetControlRepository

    ''' <summary>
    ''' repositorio de sequencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _sequenseBudgetDRepository As ISequenseBudgetDRepository

    ''' <summary>
    ''' Repositorio del presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetEntryRepository As IBudgetEntryRepository

    ''' <summary>
    ''' AdminService de presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetEntryAdminService As IBudgetEntryAdminService

    ''' <summary>
    ''' repositorio de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetRepository As IBudgetRepository

    Private _budgetService As IBudgetService

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal BudgetModificationRepository As IBudgetModificationRepository, ByVal BudgetaryValidityRepository As IValidityRepository,
                   BudgetControlRepository As IBudgetControlRepository, ByVal sequenseRepository As ISequenseBudgetDRepository,
                   budgetEntryRepository As IBudgetEntryRepository, budgetEntryAdminService As IBudgetEntryAdminService, budgetService As IBudgetService, budgetRepository As IBudgetRepository)
        If BudgetModificationRepository Is Nothing Then
            Throw New ArgumentNullException("BudgetModificationRepository")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository")
        End If
        If BudgetControlRepository Is Nothing Then
            Throw New ArgumentNullException("BudgetControlRepository")
        End If
        If budgetEntryRepository Is Nothing Then
            Throw New ArgumentNullException("budgetEntryRepository")
        End If
        If budgetEntryAdminService Is Nothing Then
            Throw New ArgumentNullException("budgetEntryAdminService")
        End If
        _sequenseBudgetDRepository = sequenseRepository
        Me._BudgetaryValidityRepository = BudgetaryValidityRepository
        Me._BudgetModificationRepository = BudgetModificationRepository
        Me._BudgetControlRepository = BudgetControlRepository
        Me._budgetEntryRepository = budgetEntryRepository
        Me._budgetEntryAdminService = budgetEntryAdminService
        Me._budgetRepository = budgetRepository
        _budgetService = budgetService
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene una modificacion de presupuesto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetModification(code As String, type As Integer, budgetaryValidityId As Integer, audit As AuditMessage) As BudgetModification Implements IBudgetModificationAdminService.GetBudgetModification
        Try
            Return _BudgetModificationRepository.GetBudgetModification(code, type, budgetaryValidityId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una modificacion de presupuesto por codigo
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetModificationById(id As Integer) As BudgetModification Implements IBudgetModificationAdminService.GetBudgetModificationById
        Try
            Return _BudgetModificationRepository.GetBudgetModificationById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina una modificacion de presupuesto
    ''' </summary>
    ''' <param name="budgetModification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBudgetModification(budgetModification As BudgetModification, audit As AuditMessage) As ActionResult Implements IBudgetModificationAdminService.DeleteBudgetModification
        Dim unitOfWork As IUnitWork = Me._BudgetModificationRepository.UnitWork
        Try
            If budgetModification.BudgetModificationDetail.Count > 0 Then
                Dim index As Integer = 0
                While budgetModification.BudgetModificationDetail.Count > 0
                    index = budgetModification.BudgetModificationDetail.Count - 1
                    budgetModification.BudgetModificationDetail.Item(index).MarkAsDeleted()
                End While
                budgetModification.MarkAsDeleted()
                Me._BudgetModificationRepository.SaveEntity(budgetModification)
            End If
            unitOfWork.Commit()

            'Auditoria básica
            IndigoAuditBasic.Execute(GetType(BudgetModification).Name, audit.Functional, budgetModification.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            'Ausitoria avanzada
            Dim auditObject As New IndigoAuditSimpleEntity(Of BudgetModification)(budgetModification, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As System.Data.Entity.Infrastructure.DbUpdateConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})} 'error de concurrencia
        Catch ex As System.Data.Entity.Infrastructure.DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"c-0000"})} ' error de dependencia
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})} ' error generico
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="budgetModification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBudgetMofication(BudgetModification As BudgetModification, listBudgetModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of BudgetModification) Implements IBudgetModificationAdminService.SaveBudgetMofication
        If BudgetModification Is Nothing Then
            Throw New ArgumentNullException("BudgetModification")
        End If

        Dim unitOfWork As IUnitWork = Me._BudgetModificationRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXmlBudgetModification(BudgetModification)
                Dim ListDeleteXml As String = ConvertToXmlListDelete(listBudgetModificationDetailDelete)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(BudgetModification.Id, BudgetModification.Status)

                Dim resultStore = Me._BudgetModificationRepository.SP_SaveBudgetModification(EntityXml, ListDeleteXml, audit.CodeUser)
                If resultStore.CodeResult <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of BudgetModification) With {.StateResult = False, .MessageResult = {resultStore.MessageResult}.ToList, .Message = resultStore.MessageResult}
                End If

                BudgetModification.Id = resultStore.Id
                BudgetModification.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of BudgetModification)(BudgetModification, audit, auditStatus, BudgetModification.OriginalValue)
                auditProcess.Execute()

                BudgetModification.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of BudgetModification) With {.StateResult = True, .ObjectEmbbeded = BudgetModification, .Message = resultStore.MessageResult, .MessageAux = resultStore.AuxiliaryResult}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of BudgetModification) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of BudgetModification) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeStateBudgetModification(code As String, validity As Integer, state As Integer, budgetaryValidityId As Integer, audit As AuditMessage) As ActionResult(Of BudgetModification) Implements IBudgetModificationAdminService.ChangeStateBudgetModification
        Dim BudgetModification As BudgetModification = Me._BudgetModificationRepository.GetBudgetModification(code, validity, budgetaryValidityId)
        BudgetModification.Status = state
        Return SaveBudgetMofication(BudgetModification, Nothing, audit)
    End Function
#End Region

#Region "Private Methods"

    Private Function ConvertToXmlBudgetModification(BudgetModification As BudgetModification) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<BudgetModification>")

        builder.Append("<Id>" & BudgetModification.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & BudgetModification.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & BudgetModification.Code & "</Code>")
        builder.Append("<BudgetaryValidityId>" & BudgetModification.BudgetaryValidityId & "</BudgetaryValidityId>")
        builder.Append("<DocumentSource>" & BudgetModification.DocumentSource & "</DocumentSource>")
        builder.Append("<DocumentDate>" & BudgetModification.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") & "</DocumentDate>")
        builder.Append("<Document>" & BudgetModification.Document & "</Document>")
        builder.Append("<Observations>" & BudgetModification.Observations & "</Observations>")
        builder.Append("<Status>" & BudgetModification.Status & "</Status>")
        If BudgetModification.EntityId IsNot Nothing Then
            builder.Append("<EntityId>" & BudgetModification.EntityId & "</EntityId>")
            builder.Append("<EntityCode>" & BudgetModification.EntityCode & "</EntityCode>")
            builder.Append("<EntityName>" & BudgetModification.EntityName & "</EntityName>")
        End If

        For Each detail In BudgetModification.BudgetModificationDetail
            builder.Append("<BudgetModificationDetail>")

            If detail.Id > 0 Then
                builder.Append("<Id>" & detail.Id & "</Id>")
            End If
            builder.Append("<BudgetId>" & detail.BudgetId & "</BudgetId>")
            builder.Append("<CategoryId>" & detail.CategoryId & "</CategoryId>")
            builder.Append("<RevenueTypeId>" & detail.RevenueTypeId & "</RevenueTypeId>")
            builder.Append("<Nature>" & detail.Nature & "</Nature>")
            builder.Append("<Value>" & detail.Value.ToString().Replace(",", ".") & "</Value>")

            builder.Append("</BudgetModificationDetail>")
        Next

        builder.Append("</BudgetModification>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListDelete(listBudgetModificationDetailDelete As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        If listBudgetModificationDetailDelete IsNot Nothing AndAlso listBudgetModificationDetailDelete.Count > 0 Then
            For Each detail In listBudgetModificationDetailDelete
                builder.Append("<BudgetModificationDetail>")
                builder.Append("<Id>" & detail & "</Id>")
                builder.Append("</BudgetModificationDetail>")
            Next
        End If

        Return builder.ToString()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _budgetService.Dispose()
                _budgetEntryAdminService.Dispose()
            End If
            _sequenseBudgetDRepository = Nothing
            Me._BudgetaryValidityRepository = Nothing
            Me._BudgetModificationRepository = Nothing
            Me._BudgetControlRepository = Nothing
            Me._budgetEntryRepository = Nothing
            Me._budgetEntryAdminService = Nothing
            Me._budgetRepository = Nothing
            _budgetService = Nothing
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
