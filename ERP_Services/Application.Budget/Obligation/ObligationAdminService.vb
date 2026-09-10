'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
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
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports Domain.Entities.Service

#End Region
Public Class ObligationAdminService
    Implements IObligationAdminService

    Private _obligationRepository As IObligationRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseBudgetDRepository
    ''' <summary>
    ''' Repositorio del control de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _BudgetControlRepository As IBudgetControlRepository

    Private _budgetRepository As IBudgetRepository

    Private _commitmentDetailRepository As ICommitmentDetailRepository


    Private _budgetService As IBudgetService

    Public Sub New(obligationRepository As IObligationRepository, secuenseDRepository As ISequenseBudgetDRepository, budgetService As IBudgetService,
                   budgetRepository As IBudgetRepository, commitmentDetailRepository As ICommitmentDetailRepository, BudgetControlRepository As IBudgetControlRepository)
        If obligationRepository Is Nothing Then
            Throw New ArgumentNullException("ObligationRepository")
        End If
        _obligationRepository = obligationRepository
        _secuenseDRepository = secuenseDRepository
        _budgetService = budgetService
        _budgetRepository = budgetRepository
        _commitmentDetailRepository = commitmentDetailRepository
        _BudgetControlRepository = BudgetControlRepository
    End Sub

    Public Function GetObligationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Obligation Implements IObligationAdminService.GetObligationByCode
        Try
            Dim Obligation = _obligationRepository.GetObligationByCode(code, BudgetaryValidityId)
            If Obligation IsNot Nothing AndAlso Obligation.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Obligation)(Obligation, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return Obligation
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Obligation
        End Try
    End Function

    Public Function GetObligationById(id As Integer) As Obligation Implements IObligationAdminService.GetObligationById
        Try
            Return _obligationRepository.GetObligationById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Obligation
        End Try
    End Function

    Public Function SaveObligation(Obligation As Obligation, listObligationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of Obligation) Implements IObligationAdminService.SaveObligation
        If Obligation Is Nothing Then
            Throw New ArgumentNullException("Obligation")
        End If

        Dim unitOfWork As IUnitWork = Me._obligationRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertObligationToXml(Obligation)
                Dim ListDeleteXml As String = ConvertListDeleteToXml(listObligationDetailDelete)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(Obligation.Id, Obligation.Status)

                Dim resultStore = Me._obligationRepository.SP_SaveObligation(EntityXml, ListDeleteXml, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of Obligation) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList, .Message = resultStore.Message}
                End If

                Obligation.Id = resultStore.Id
                Obligation.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of Obligation)(Obligation, audit, auditStatus, Obligation.OriginalValue)
                auditProcess.Execute()

                Obligation.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of Obligation) With {.StateResult = True, .ObjectEmbbeded = Obligation, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of Obligation) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Obligation) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#Region "Private Methods"

    Private Function ConvertObligationToXml(Obligation As Obligation) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<Obligation>")

        builder.Append("<Id>" & Obligation.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & Obligation.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & Obligation.Code & "</Code>")
        builder.Append("<BudgetaryValidityId>" & Obligation.BudgetaryValidityId & "</BudgetaryValidityId>")
        builder.Append("<DocumentDate>" & Obligation.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") & "</DocumentDate>")
        builder.Append("<ThirdPartyId>" & Obligation.ThirdPartyId & "</ThirdPartyId>")
        builder.Append("<Document>" & Obligation.Document & "</Document>")
        builder.Append("<ObligationType>" & Obligation.ObligationType & "</ObligationType>")
        builder.Append("<Observations>" & Obligation.Observations & "</Observations>")
        builder.Append("<Status>" & Obligation.Status & "</Status>")
        builder.Append("<AnnulmentConceptId>" & Obligation.AnnulmentConceptId & "</AnnulmentConceptId>")
        builder.Append("<AnnulmentDescription>" & Obligation.AnnulmentDescription & "</AnnulmentDescription>")
        builder.Append("<AutomaticPaymentOrder>" & Obligation.AutomaticPaymentOrder & "</AutomaticPaymentOrder>")

        For Each detail In Obligation.ObligationDetail
            builder.Append("<ObligationDetail>")

            If detail.Id > 0 Then
                builder.Append("<Id>" & detail.Id & "</Id>")
            End If
            builder.Append("<ObligationId>" & detail.ObligationId & "</ObligationId>")
            builder.Append("<CommitmentDetailId>" & detail.CommitmentDetailId & "</CommitmentDetailId>")
            builder.Append("<CategoryId>" & detail.CategoryId & "</CategoryId>")
            builder.Append("<RevenueTypeId>" & detail.RevenueTypeId & "</RevenueTypeId>")
            builder.Append("<ExpiredDate>" & detail.ExpiredDate.ToString("dd/MM/yyyy HH:mm:ss") & "</ExpiredDate>")
            builder.Append("<InitialValue>" & detail.InitialValue.ToString().Replace(",", ".") & "</InitialValue>")
            If detail.EntityId IsNot Nothing Then
                builder.Append("<EntityId>" & detail.EntityId & "</EntityId>")
                builder.Append("<EntityCode>" & detail.EntityCode & "</EntityCode>")
                builder.Append("<EntityName>" & detail.EntityName & "</EntityName>")
            End If

            builder.Append("</ObligationDetail>")
        Next

        builder.Append("</Obligation>")

        Return builder.ToString()
    End Function

    Private Function ConvertListDeleteToXml(listObligationDetailDelete As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        If listObligationDetailDelete IsNot Nothing AndAlso listObligationDetailDelete.Count > 0 Then
            For Each detail In listObligationDetailDelete
                builder.Append("<ObligationDetail>")
                builder.Append("<Id>" & detail & "</Id>")
                builder.Append("</ObligationDetail>")
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
            _obligationRepository = Nothing
            _secuenseDRepository = Nothing
            _budgetService = Nothing
            _budgetRepository = Nothing
            _commitmentDetailRepository = Nothing
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
