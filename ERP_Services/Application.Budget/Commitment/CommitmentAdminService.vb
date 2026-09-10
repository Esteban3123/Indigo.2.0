'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 02-09-2015
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
Imports Domain.Entities.Service
Imports System.Text

#End Region

Public Class CommitmentAdminService
    Implements ICommitmentAdminService

#Region "Properties"

    ''' <summary>
    ''' repositorio de traslado de pac
    ''' </summary>
    ''' <remarks></remarks>
    Private _commitmentRepository As ICommitmentRepository

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

    Private _budgetService As IBudgetService

    ''' <summary>
    ''' Variable tipo repositorio para presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetRepository As IBudgetRepository

    ''' <summary>
    ''' repositorio de los detalles de la disponibilidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _availabilityDetailRepository As IAvailabilityDetailRepository

#End Region

#Region "Builder"

    Public Sub New(commitmentRepository As ICommitmentRepository, budgetControlRepository As IBudgetControlRepository,
                   sequenseBudgetDRepository As ISequenseBudgetDRepository, budgetService As IBudgetService, budgetRepository As IBudgetRepository,
                   availabilityDetailRepository As IAvailabilityDetailRepository)
        If commitmentRepository Is Nothing Then
            Throw New ArgumentNullException("commitmentRepository")
        End If
        _commitmentRepository = commitmentRepository
        _BudgetControlRepository = budgetControlRepository
        _sequenseBudgetDRepository = sequenseBudgetDRepository
        _budgetService = budgetService
        _budgetRepository = budgetRepository
        _availabilityDetailRepository = availabilityDetailRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene un compromiso por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommitmentByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Commitment Implements ICommitmentAdminService.GetCommitmentByCode
        Try
            Dim commitment = _commitmentRepository.GetCommitmentByCode(code, BudgetaryValidityId)
            If commitment IsNot Nothing AndAlso commitment.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Commitment)(commitment, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return commitment
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Commitment
        End Try
    End Function

    ''' <summary>
    ''' obtiene un compromiso por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommitmentById(id As Integer) As Commitment Implements ICommitmentAdminService.GetCommitmentById
        Try
            Return _commitmentRepository.GetCommitmentById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Commitment
        End Try
    End Function

    ''' <summary>
    ''' Guarda un compromiso
    ''' </summary>
    ''' <param name="commitment"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCommitment(commitment As Commitment, listCommitmentDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of Commitment) Implements ICommitmentAdminService.SaveCommitment
        If commitment Is Nothing Then
            Throw New ArgumentNullException("commitment")
        End If

        Dim unitOfWork As IUnitWork = Me._commitmentRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertCommitmentToXml(commitment)
                Dim ListDeleteXml As String = ConvertListDeleteToXml(listCommitmentDetailDelete)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(commitment.Id, commitment.Status)

                Dim resultStore = Me._commitmentRepository.SP_SaveCommitment(EntityXml, ListDeleteXml, audit.CodeUser)
                If resultStore.CodeResult <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of Commitment) With {.StateResult = False, .MessageResult = {resultStore.MessageResult}.ToList, .Message = resultStore.MessageResult}
                End If

                commitment.Id = resultStore.Id
                commitment.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of Commitment)(commitment, audit, auditStatus, commitment.OriginalValue)
                auditProcess.Execute()

                commitment.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of Commitment) With {.StateResult = True, .ObjectEmbbeded = commitment, .Message = resultStore.MessageResult}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of Commitment) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Commitment) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#End Region

#Region "Private Methods"

    Private Function ConvertCommitmentToXml(Commitment As Commitment) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<Commitment>")

        builder.Append("<Id>" & Commitment.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & Commitment.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & Commitment.Code & "</Code>")
        builder.Append("<BudgetaryValidityId>" & Commitment.BudgetaryValidityId & "</BudgetaryValidityId>")
        builder.Append("<DocumentDate>" & Commitment.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") & "</DocumentDate>")
        builder.Append("<ThirdPartyId>" & Commitment.ThirdPartyId & "</ThirdPartyId>")
        builder.Append("<DocumentSource>" & Commitment.DocumentSource & "</DocumentSource>")
        builder.Append("<Document>" & Commitment.Document & "</Document>")
        builder.Append("<CommitmentType>" & Commitment.CommitmentType & "</CommitmentType>")
        builder.Append("<Observations>" & Commitment.Observations & "</Observations>")
        builder.Append("<Status>" & Commitment.Status & "</Status>")
        builder.Append("<AnnulmentConceptId>" & Commitment.AnnulmentConceptId & "</AnnulmentConceptId>")
        builder.Append("<AnnulmentDescription>" & Commitment.AnnulmentDescription & "</AnnulmentDescription>")
        builder.Append("<AutomaticObligation>" & Commitment.AutomaticObligation & "</AutomaticObligation>")
        builder.Append("<AutomaticPaymentOrder>" & Commitment.AutomaticPaymentOrder & "</AutomaticPaymentOrder>")
        If Commitment.EntityId IsNot Nothing Then
            builder.Append("<EntityId>" & Commitment.EntityId & "</EntityId>")
            builder.Append("<EntityCode>" & Commitment.EntityCode & "</EntityCode>")
            builder.Append("<EntityName>" & Commitment.EntityName & "</EntityName>")
        End If

        For Each detail In Commitment.CommitmentDetail
            builder.Append("<CommitmentDetail>")

            If detail.Id > 0 Then
                builder.Append("<Id>" & detail.Id & "</Id>")
            End If
            builder.Append("<CommitmentId>" & detail.CommitmentId & "</CommitmentId>")
            builder.Append("<AvailabilityDetailId>" & detail.AvailabilityDetailId & "</AvailabilityDetailId>")
            builder.Append("<CategoryId>" & detail.CategoryId & "</CategoryId>")
            builder.Append("<RevenueTypeId>" & detail.RevenueTypeId & "</RevenueTypeId>")
            builder.Append("<ExpiredDate>" & detail.ExpiredDate.ToString("dd/MM/yyyy HH:mm:ss") & "</ExpiredDate>")
            builder.Append("<InitialValue>" & detail.InitialValue.ToString().Replace(",", ".") & "</InitialValue>")


            builder.Append("</CommitmentDetail>")
        Next

        builder.Append("</Commitment>")

        Return builder.ToString()
    End Function

    Private Function ConvertListDeleteToXml(listCommitmentDetailDelete As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        If listCommitmentDetailDelete IsNot Nothing AndAlso listCommitmentDetailDelete.Count > 0 Then
            For Each detail In listCommitmentDetailDelete
                builder.Append("<CommitmentDetail>")
                builder.Append("<Id>" & detail & "</Id>")
                builder.Append("</CommitmentDetail>")
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
            _commitmentRepository = Nothing
            _BudgetControlRepository = Nothing
            _sequenseBudgetDRepository = Nothing
            _budgetService = Nothing
            _budgetRepository = Nothing
            _availabilityDetailRepository = Nothing
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
