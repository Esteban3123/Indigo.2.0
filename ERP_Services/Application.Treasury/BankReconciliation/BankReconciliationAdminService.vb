#Region "Imports"

Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports System.Data.Entity.Validation
Imports System.Transactions

#End Region

Public Class BankReconciliationAdminService
    Implements IBankReconciliationAdminService

#Region "Properties"

    Private Const FORM_NAME As String = "FrmBankReconciliation"

    Private _sequenseDRepository As ISequenseTreasuryDRepository
    Private _BankReconciliationRepository As IBankReconciliationRepository

#End Region

#Region "Builder"

    Public Sub New(sequenseDRepository As ISequenseTreasuryDRepository, BankReconciliationRepository As IBankReconciliationRepository)
        If sequenseDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenseDRepository Vacio")
        End If
        If BankReconciliationRepository Is Nothing Then
            Throw New ArgumentNullException("BankReconciliationRepository Vacio")
        End If

        _sequenseDRepository = sequenseDRepository
        _BankReconciliationRepository = BankReconciliationRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetBankReconciliationById(id As Integer, audit As AuditMessage) As ActionResult(Of BankReconciliation) Implements IBankReconciliationAdminService.GetBankReconciliationById
        Try
            Dim BankReconciliation As BankReconciliation = Me._BankReconciliationRepository.GetBankReconciliationById(id)
            If BankReconciliation IsNot Nothing AndAlso BankReconciliation.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of BankReconciliation)(BankReconciliation, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of BankReconciliation) With {.StateResult = True, .ObjectEmbbeded = BankReconciliation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BankReconciliation) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetBankReconciliationByCode(code As String, audit As AuditMessage) As ActionResult(Of BankReconciliation) Implements IBankReconciliationAdminService.GetBankReconciliationByCode
        Try
            Dim BankReconciliation As BankReconciliation = Me._BankReconciliationRepository.GetBankReconciliationByCode(code)
            If BankReconciliation IsNot Nothing AndAlso BankReconciliation.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of BankReconciliation)(BankReconciliation, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of BankReconciliation) With {.StateResult = True, .ObjectEmbbeded = BankReconciliation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BankReconciliation) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SaveBankReconciliation(BankReconciliation As BankReconciliation, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of BankReconciliation) Implements IBankReconciliationAdminService.SaveBankReconciliation
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = BankReconciliation.ToXML()

                Dim result = _BankReconciliationRepository.GenerateBankReconciliationSP(xml, audit.CodeUser).ToList().ElementAt(0)

                If result.CodeMessage = "999" Then
                    Return New ActionResult(Of BankReconciliation) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = result.Message}
                Else

                    Dim BankReconciliationReturn = _BankReconciliationRepository.GetBankReconciliationById(result.BankReconciliationId)

                    Transaction.Complete()
                    If result.Message.Length = 0 Then
                        Return New ActionResult(Of BankReconciliation) With {.StateResult = True, .ObjectEmbbeded = BankReconciliationReturn}
                    Else
                        Return New ActionResult(Of BankReconciliation) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = BankReconciliationReturn}
                    End If
                End If

                Return New ActionResult(Of BankReconciliation) With {.StatusCode = eStatusResult.SUCCESS, .Message = "Proceso Finalizado Correctamente"}
            Catch ex As OptimisticConcurrencyException
                Transaction.Dispose()
                Return New ActionResult(Of BankReconciliation) With {.StatusCode = eStatusResult.EXCEPTION, .StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            Catch ex As DbEntityValidationException
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of BankReconciliation) With {.StatusCode = eStatusResult.EXCEPTION, .StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of BankReconciliation) With {.StatusCode = eStatusResult.EXCEPTION, .StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function

    Public Function GetBankReconciliationDetails(criterias As Dictionary(Of String, String)) As ActionResult(Of List(Of BankReconciliationDetail)) Implements IBankReconciliationAdminService.GetBankReconciliationDetails
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)
            Dim result = Me._BankReconciliationRepository.SP_GetBankReconciliationDetails(xmlCriterias)

            Dim listBankReconciliationDetail As New List(Of BankReconciliationDetail)
            For Each detail In result
                listBankReconciliationDetail.Add(New BankReconciliationDetail With
                {
                    .Id = detail.Id,
                    .DocumentType = detail.DocumentType,
                    .Nature = detail.Nature,
                    .Value = detail.Value,
                    .EntityId = detail.EntityId,
                    .EntityCode = detail.EntityCode,
                    .EntityName = detail.EntityName,
                    .Reconciled = detail.Reconciled,
                    .DocumentDate = detail.DocumentDate,
                    .ThirdPartyNitName = detail.ThirdPartyNitName,
                    .DocumentNumber = detail.DocumentNumber,
                    .Observations = detail.Observations,
                    .CreationUser = detail.CreationUser,
                    .ConfirmationUser = detail.ConfirmationUser
                })
            Next

            Return New ActionResult(Of List(Of BankReconciliationDetail)) With {.StateResult = True, .ObjectEmbbeded = listBankReconciliationDetail}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of BankReconciliationDetail)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
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

            _sequenseDRepository = Nothing
            _BankReconciliationRepository = Nothing
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
