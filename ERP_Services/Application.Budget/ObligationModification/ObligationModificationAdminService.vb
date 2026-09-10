'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
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

Public Class ObligationModificationAdminService
    Implements IObligationModificationAdminService

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _obligationModificationRepository As IObligationModificationRepository
    ''' <summary>
    ''' Repositorio del control de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _BudgetControlRepository As IBudgetControlRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseBudgetDRepository
    ''' <summary>
    ''' Servicio de budget
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetService As IBudgetService
    ''' <summary>
    ''' Repositorio de compromiso
    ''' </summary>
    ''' <remarks></remarks>
    Private _commitmentDetailRepository As ICommitmentDetailRepository
    ''' <summary>
    ''' Repositorio de detalle de la obligacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _obligationDetailRepository As IObligationDetailRepository

    Private _categoryRepository As IBudgetItemRepository

    Private _revenueTypeRepository As IExpenseTypeRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal obligationModificationRepository As IObligationModificationRepository, ByVal secuenseDRepository As ISequenseBudgetDRepository, budgetService As IBudgetService,
                   commitmentDetailRepository As ICommitmentDetailRepository, obligationDetailRepository As IObligationDetailRepository, categoryRepository As IBudgetItemRepository,
                   revenueTypeRepository As IExpenseTypeRepository, BudgetControlRepository As IBudgetControlRepository)
        If obligationModificationRepository Is Nothing Then
            Throw New ArgumentNullException("obligationModificationRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _obligationModificationRepository = obligationModificationRepository
        _secuenseDRepository = secuenseDRepository
        _budgetService = budgetService
        _commitmentDetailRepository = commitmentDetailRepository
        _obligationDetailRepository = obligationDetailRepository
        _categoryRepository = categoryRepository
        _revenueTypeRepository = revenueTypeRepository
        _BudgetControlRepository = BudgetControlRepository
    End Sub

    ''' <summary>
    ''' Elimina una modificacion de obligacion
    ''' </summary>
    ''' <param name="ObligationModification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteObligationModification(ObligationModification As ObligationModification, audit As AuditMessage) As ActionResult Implements IObligationModificationAdminService.DeleteObligationModification
        If ObligationModification Is Nothing Then
            Throw New ArgumentNullException("ObligationModification")
        End If
        Dim unitOfWork As IUnitWork = Me._obligationModificationRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of ObligationModification)
            auditProcess = New IndigoAuditSimpleEntity(Of ObligationModification)(ObligationModification, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._obligationModificationRepository.DeleteEntity(ObligationModification)
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
    ''' Obtiene una modificacion de obligacion por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetObligationModification(Code As String, validityId As Integer, audit As AuditMessage) As ActionResult(Of ObligationModification) Implements IObligationModificationAdminService.GetObligationModification
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ObligationModification As ObligationModification = Me._obligationModificationRepository.GetObligationModification(Code.Trim(), validityId)
            If ObligationModification IsNot Nothing AndAlso ObligationModification.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ObligationModification)(ObligationModification, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ObligationModification) With {.StateResult = True, .ObjectEmbbeded = ObligationModification}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ObligationModification) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una modificacion de obligacion por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetObligationModificationById(Id As Integer, audit As AuditMessage) As ActionResult(Of ObligationModification) Implements IObligationModificationAdminService.GetObligationModificationById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ObligationModification As ObligationModification = Me._obligationModificationRepository.GetObligationModificationById(Id)
            If ObligationModification IsNot Nothing AndAlso ObligationModification.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ObligationModification)(ObligationModification, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ObligationModification) With {.StateResult = True, .ObjectEmbbeded = ObligationModification}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ObligationModification) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una modificacion de obligacion
    ''' </summary>
    ''' <param name="ObligationModification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveObligationModification(ObligationModification As ObligationModification, listObligationModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of ObligationModification) Implements IObligationModificationAdminService.SaveObligationModification
        If ObligationModification Is Nothing Then
            Throw New ArgumentNullException("ObligationModification")
        End If

        Dim unitOfWork As IUnitWork = Me._obligationModificationRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXmlObligationModification(ObligationModification)
                Dim ListDeleteXml As String = ConvertToXmlListDelete(listObligationModificationDetailDelete)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(ObligationModification.Id, ObligationModification.Status)

                Dim resultStore = Me._obligationModificationRepository.SP_SaveObligationModification(EntityXml, ListDeleteXml, audit.CodeUser)
                If resultStore.CodeResult <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of ObligationModification) With {.StateResult = False, .MessageResult = {resultStore.MessageResult}.ToList, .Message = resultStore.MessageResult}
                End If

                ObligationModification.Id = resultStore.Id
                ObligationModification.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of ObligationModification)(ObligationModification, audit, auditStatus, ObligationModification.OriginalValue)
                auditProcess.Execute()

                ObligationModification.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of ObligationModification) With {.StateResult = True, .ObjectEmbbeded = ObligationModification, .Message = resultStore.MessageResult, .MessageAux = resultStore.AuxiliaryResult}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of ObligationModification) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of ObligationModification) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#Region "Private Methods"

    Private Function ConvertToXmlObligationModification(ObligationModification As ObligationModification) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<ObligationModification>")

        builder.Append("<Id>" & ObligationModification.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & ObligationModification.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & ObligationModification.Code & "</Code>")
        builder.Append("<BudgetaryValidityId>" & ObligationModification.BudgetaryValidityId & "</BudgetaryValidityId>")
        builder.Append("<DocumentDate>" & ObligationModification.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") & "</DocumentDate>")
        builder.Append("<ObligationId>" & ObligationModification.ObligationId & "</ObligationId>")
        builder.Append("<UpTo>" & ObligationModification.UpTo & "</UpTo>")
        builder.Append("<Document>" & ObligationModification.Document & "</Document>")
        builder.Append("<Observations>" & ObligationModification.Observations & "</Observations>")
        builder.Append("<Status>" & ObligationModification.Status & "</Status>")
        If ObligationModification.EntityId IsNot Nothing Then
            builder.Append("<EntityId>" & ObligationModification.EntityId & "</EntityId>")
            builder.Append("<EntityCode>" & ObligationModification.EntityCode & "</EntityCode>")
            builder.Append("<EntityName>" & ObligationModification.EntityName & "</EntityName>")
        End If

        For Each detail In ObligationModification.ObligationModificationDetail
            builder.Append("<ObligationModificationDetail>")

            If detail.Id > 0 Then
                builder.Append("<Id>" & detail.Id & "</Id>")
            End If
            builder.Append("<ObligationDetailId>" & detail.ObligationDetailId & "</ObligationDetailId>")
            builder.Append("<ExpiredDate>" & detail.ExpiredDate.ToString("dd/MM/yyyy HH:mm:ss") & "</ExpiredDate>")
            builder.Append("<CategoryId>" & detail.CategoryId & "</CategoryId>")
            builder.Append("<RevenueTypeId>" & detail.RevenueTypeId & "</RevenueTypeId>")
            builder.Append("<Nature>" & detail.Nature & "</Nature>")
            builder.Append("<Value>" & detail.Value.ToString().Replace(",", ".") & "</Value>")
            builder.Append("<IsLogBase>" & detail.IsLogBase & "</IsLogBase>")
            If detail.CommitmentDetailId IsNot Nothing Then
                builder.Append("<CommitmentDetailId>" & detail.CommitmentDetailId & "</CommitmentDetailId>")
            End If

            builder.Append("</ObligationModificationDetail>")
        Next

        builder.Append("</ObligationModification>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListDelete(listObligationModificationDetailDelete As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        If listObligationModificationDetailDelete IsNot Nothing AndAlso listObligationModificationDetailDelete.Count > 0 Then
            For Each detail In listObligationModificationDetailDelete
                builder.Append("<ObligationModificationDetail>")
                builder.Append("<Id>" & detail & "</Id>")
                builder.Append("</ObligationModificationDetail>")
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
            End If
            _obligationModificationRepository = Nothing
            _secuenseDRepository = Nothing
            _budgetService = Nothing
            _commitmentDetailRepository = Nothing
            _obligationDetailRepository = Nothing
            _categoryRepository = Nothing
            _revenueTypeRepository = Nothing
            _BudgetControlRepository = Nothing
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
