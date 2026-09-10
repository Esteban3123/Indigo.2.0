'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/10/2014
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
Imports System.Text
Imports System.Transactions

Public Class ContractAdminService
    Implements IContractAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractRepository As IContractRepository
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
    Public Sub New(ByVal contractRepository As IContractRepository, ByVal secuenseDRepository As ISequenseContractDRepository)
        If contractRepository Is Nothing Then
            Throw New ArgumentNullException("contractRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _contractRepository = contractRepository
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
    Public Function ChangeStateContract(code As String, state As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.Contract) Implements IContractAdminService.ChangeStateContract
        Dim Contract As Domain.Entities.Contract = _contractRepository.GetContract(code)
        Contract.Status = state
        Return SaveContract(Contract, audit)
    End Function

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="Contract"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteContract(Contract As Domain.Entities.Contract, audit As AuditMessage) As ActionResult Implements IContractAdminService.DeleteContract
        If Contract Is Nothing Then
            Throw New ArgumentNullException("Contract")
        End If
        Dim unitOfWork As IUnitWork = Me._contractRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of Domain.Entities.Contract)
            auditProcess = New IndigoAuditSimpleEntity(Of Domain.Entities.Contract)(Contract, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._contractRepository.DeleteEntity(Contract)
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
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContract(code As String, audit As AuditMessage) As ActionResult(Of Domain.Entities.Contract) Implements IContractAdminService.GetContract
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Contract As Domain.Entities.Contract = Me._contractRepository.GetContract(code.Trim())
            If Contract IsNot Nothing AndAlso Contract.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.Contract)(Contract, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Domain.Entities.Contract) With {.StateResult = True, .ObjectEmbbeded = Contract}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Entities.Contract) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractById(id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.Contract) Implements IContractAdminService.GetContractById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Contract As Domain.Entities.Contract = Me._contractRepository.GetContractById(id)
            If Contract IsNot Nothing AndAlso Contract.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.Contract)(Contract, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Domain.Entities.Contract) With {.StateResult = True, .ObjectEmbbeded = Contract}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Entities.Contract) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractByIdWithAggregates(id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.Contract) Implements IContractAdminService.GetContractByIdWithAggregates
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Contract As Domain.Entities.Contract = Me._contractRepository.GetContractByIdWithAggregates(id)
            If Contract IsNot Nothing AndAlso Contract.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.Contract)(Contract, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Domain.Entities.Contract) With {.StateResult = True, .ObjectEmbbeded = Contract}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Entities.Contract) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="Contract"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveContract(Contract As Domain.Entities.Contract, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of Domain.Entities.Contract) Implements IContractAdminService.SaveContract
        If Contract Is Nothing Then
            Throw New ArgumentNullException("Quotation")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(Contract, audit)

                Dim result = _contractRepository.SP_SaveContract(xml, audit.CodeUser)
                If result.CodeMessage <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of Domain.Entities.Contract) With {.StateResult = False, .Message = result.Message}
                End If

                Contract.Id = result.ContractId
                Contract.Code = result.ContractCode

                scope.Complete()
                Return New ActionResult(Of Domain.Entities.Contract) With {.StateResult = True, .ObjectEmbbeded = Contract, .Message = result.Message}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Domain.Entities.Contract) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Convierte la entidad en xml
    ''' </summary>
    ''' <param name="Contract"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Private Function ConvertEntityToXml(Contract As Domain.Entities.Contract, audit As AuditMessage) As Object
        Dim builder As New StringBuilder
        Dim rowId As Integer = 1

        builder.Append("<Contract>")

        With Contract
            builder.Append("<Id>" & .Id & "</Id>")
            builder.Append("<ContractEntityId>" & .ContractEntityId & "</ContractEntityId>")
            builder.Append("<HealthAdministratorId>" & .HealthAdministratorId & "</HealthAdministratorId>")
            builder.Append("<Code>" & .Code & "</Code>")
            builder.Append("<ContractValue>" & .ContractValue & "</ContractValue>")
            builder.Append("<ExecuteValue>" & .ExecuteValue & "</ExecuteValue>")
            builder.Append("<ContractObject>" & .ContractObject & "</ContractObject>")
            builder.Append("<Status>" & .Status & "</Status>")
            builder.Append("<OperatingUnitId>" & .OperatingUnitId & "</OperatingUnitId>")

            If .ContractDetail IsNot Nothing AndAlso .ContractDetail.Count > 0 Then
                For Each itemDetail In .ContractDetail
                    builder.Append("<ContractDetail>")

                    builder.Append("<RowId>" & rowId & "</RowId>")
                    builder.Append("<Id>" & itemDetail.Id & "</Id>")
                    builder.Append("<ContractId>" & itemDetail.ContractId & "</ContractId>")
                    builder.Append("<ContractName>" & itemDetail.ContractName & "</ContractName>")
                    builder.Append("<ContractNumber>" & itemDetail.ContractNumber & "</ContractNumber>")
                    builder.Append("<Type>" & itemDetail.Type & "</Type>")
                    builder.Append("<InitialDate>" & itemDetail.InitialDate.ToString("dd/MM/yyyy HH:mm:ss") & "</InitialDate>")
                    builder.Append("<EndDate>" & itemDetail.EndDate.ToString("dd/MM/yyyy HH:mm:ss") & "</EndDate>")
                    builder.Append("<BillingInitialDate>" & itemDetail.BillingInitialDate.ToString("dd/MM/yyyy HH:mm:ss") & "</BillingInitialDate>")
                    builder.Append("<BillingEndDate>" & itemDetail.BillingEndDate.ToString("dd/MM/yyyy HH:mm:ss") & "</BillingEndDate>")
                    builder.Append("<Legalized>" & itemDetail.Legalized & "</Legalized>")

                    If itemDetail.DateLegalization IsNot Nothing AndAlso itemDetail.DateLegalization <> "1/1/0001 12:00:00 AM" Then
                        builder.Append("<DateLegalization>" & itemDetail.DateLegalization.Value.ToString("dd/MM/yyyy HH:mm:ss") & "</DateLegalization>")
                    Else
                        builder.Append("<DateLegalization>" & "" & "</DateLegalization>")
                    End If

                    builder.Append("<RadicatedBillingDate>" & itemDetail.RadicatedBillingDate.ToString("dd/MM/yyyy HH:mm:ss") & "</RadicatedBillingDate>")
                    builder.Append("<Observations>" & itemDetail.Observations & "</Observations>")
                    builder.Append("<PermanentObservationOfTheInvoice>" & itemDetail.PermanentObservationOfTheInvoice & "</PermanentObservationOfTheInvoice>")
                    builder.Append("<PrintingMode>" & itemDetail.PrintingMode & "</PrintingMode>")
                    builder.Append("<TerminationControl>" & itemDetail.TerminationControl & "</TerminationControl>")
                    builder.Append("<NotificationValueType>" & itemDetail.NotificationValueType & "</NotificationValueType>")
                    builder.Append("<PercentageNotification>" & itemDetail.PercentageNotification & "</PercentageNotification>")
                    builder.Append("<NotificationValue>" & itemDetail.NotificationValue & "</NotificationValue>")
                    builder.Append("<NotificationTimeType>" & itemDetail.NotificationTimeType & "</NotificationTimeType>")
                    builder.Append("<NotificationDays>" & itemDetail.NotificationDays & "</NotificationDays>")
                    builder.Append("<PercentageApplyPaymentSoon>" & itemDetail.PercentageApplyPaymentSoon & "</PercentageApplyPaymentSoon>")
                    builder.Append("<AgesPortfolioId>" & itemDetail.AgesPortfolioId & "</AgesPortfolioId>")
                    builder.Append("<ValidRecord>" & itemDetail.ValidRecord & "</ValidRecord>")
                    builder.Append("<IsDelete>" & If(itemDetail.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

                    If itemDetail.ContractDetailNovelty IsNot Nothing AndAlso itemDetail.ContractDetailNovelty.Count > 0 Then
                        For Each itemDetailNovelty In itemDetail.ContractDetailNovelty
                            builder.Append("<ContractDetailNovelty>")
                            builder.Append("<RowId>" & rowId & "</RowId>")
                            builder.Append("<Id>" & itemDetailNovelty.Id & "</Id>")
                            builder.Append("<ContractDetailId>" & itemDetailNovelty.ContractDetailId & "</ContractDetailId>")
                            builder.Append("<NoveltyDate>" & itemDetailNovelty.NoveltyDate.ToString("dd/MM/yyyy HH:mm:ss") & "</NoveltyDate>")
                            builder.Append("<NoveltySource>" & itemDetailNovelty.NoveltySource & "</NoveltySource>")
                            builder.Append("<Name>" & itemDetailNovelty.Name & "</Name>")
                            builder.Append("<Description>" & itemDetailNovelty.Description & "</Description>")
                            builder.Append("<Status>" & itemDetailNovelty.Status & "</Status>")
                            builder.Append("</ContractDetailNovelty>")
                        Next
                    End If

                    If itemDetail.ContractDetailPolicy IsNot Nothing AndAlso itemDetail.ContractDetailPolicy.Count > 0 Then
                        For Each itemDetailPolicy In itemDetail.ContractDetailPolicy
                            builder.Append("<ContractDetailPolicy>")
                            builder.Append("<RowId>" & rowId & "</RowId>")
                            builder.Append("<Id>" & itemDetailPolicy.Id & "</Id>")
                            builder.Append("<ContractDetailId>" & itemDetailPolicy.ContractDetailId & "</ContractDetailId>")
                            builder.Append("<FixedAssetPolicyId>" & itemDetailPolicy.FixedAssetPolicyId & "</FixedAssetPolicyId>")
                            builder.Append("<FixedAssetInsuranceId>" & itemDetailPolicy.FixedAssetInsuranceId & "</FixedAssetInsuranceId>")
                            builder.Append("<PolicyNumber>" & itemDetailPolicy.PolicyNumber & "</PolicyNumber>")
                            builder.Append("<EmissionDate>" & itemDetailPolicy.EmissionDate.ToString("dd/MM/yyyy HH:mm:ss") & "</EmissionDate>")
                            builder.Append("<AmountInsured>" & itemDetailPolicy.AmountInsured & "</AmountInsured>")
                            builder.Append("<CoveragePercentage>" & itemDetailPolicy.CoveragePercentage & "</CoveragePercentage>")
                            builder.Append("<Observation>" & itemDetailPolicy.Observation & "</Observation>")
                            builder.Append("<Status>" & itemDetailPolicy.Status & "</Status>")
                            builder.Append("</ContractDetailPolicy>")
                        Next
                    End If

                    builder.Append("</ContractDetail>")

                    rowId += 1
                Next
            End If
        End With

        builder.Append("</Contract>")

        Return builder.ToString()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _contractRepository = Nothing
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
