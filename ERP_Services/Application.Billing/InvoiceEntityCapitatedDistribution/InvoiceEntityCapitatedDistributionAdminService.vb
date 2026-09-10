#Region "Imports"

Imports System.Text
Imports System.Transactions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class InvoiceEntityCapitatedDistributionAdminService
    Implements IInvoiceEntityCapitatedDistributionAdminService

#Region "Fields"

    Dim _invoiceEntityCapitatedDistributionRepository As IInvoiceEntityCapitatedDistributionRepository

#End Region

#Region "Builder"

    Public Sub New(ByVal invoiceEntityCapitatedDistributionRepository As IInvoiceEntityCapitatedDistributionRepository)
        If invoiceEntityCapitatedDistributionRepository Is Nothing Then
            Throw New ArgumentNullException("invoiceEntityCapitatedDistributionRepository")
        End If
        _invoiceEntityCapitatedDistributionRepository = invoiceEntityCapitatedDistributionRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetInvoiceEntityCapitatedDistributionById(id As Integer, audit As AuditMessage) As ActionResult(Of InvoiceEntityCapitatedDistribution) Implements IInvoiceEntityCapitatedDistributionAdminService.GetInvoiceEntityCapitatedDistributionById
        Try
            Dim invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution = Me._invoiceEntityCapitatedDistributionRepository.GetInvoiceEntityCapitatedDistributionById(id)
            Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = True, .ObjectEmbbeded = invoiceEntityCapitatedDistribution}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetInvoiceEntityCapitatedDistributionByCode(code As String, audit As AuditMessage) As ActionResult(Of InvoiceEntityCapitatedDistribution) Implements IInvoiceEntityCapitatedDistributionAdminService.GetInvoiceEntityCapitatedDistributionByCode
        Try
            Dim invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution = Me._invoiceEntityCapitatedDistributionRepository.GetInvoiceEntityCapitatedDistributionByCode(code)
            Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = True, .ObjectEmbbeded = invoiceEntityCapitatedDistribution}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SaveInvoiceEntityCapitatedDistribution(invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution, ListInvoiceEntityCapitatedDistributionDetail As List(Of Integer), session As SessionValues, Optional idSequense As Long = 0) As ActionResult(Of InvoiceEntityCapitatedDistribution) Implements IInvoiceEntityCapitatedDistributionAdminService.SaveInvoiceEntityCapitatedDistribution
        If invoiceEntityCapitatedDistribution Is Nothing Then
            Throw New ArgumentNullException("invoiceEntityCapitatedDistribution")
        End If

        If ListInvoiceEntityCapitatedDistributionDetail Is Nothing Then
            Throw New ArgumentNullException("ListInvoiceEntityCapitatedDistributionDetail")
        End If

        Dim UnitOfWork As IUnitWork = _invoiceEntityCapitatedDistributionRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim InvoiceEntityCapitatedDistributionXml As String = ConvertToXmlListInvoiceEntityCapitatedDistribution(invoiceEntityCapitatedDistribution)
                Dim InvoiceEntityCapitatedDistributionDetailXml As String = ConvertToXmlListInvoiceEntityCapitatedDistributionDetail(ListInvoiceEntityCapitatedDistributionDetail)

                Dim resultStore = Me._invoiceEntityCapitatedDistributionRepository.SP_SaveInvoiceEntityCapitatedDistribution(InvoiceEntityCapitatedDistributionXml, InvoiceEntityCapitatedDistributionDetailXml, session.AuditMessageWcf.CodeUser)
                If resultStore Is Nothing OrElse resultStore.Count = 0 Or resultStore.Any(Function(r) r.CodeMessage <> 0) Then
                    UnitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = False, .Message = resultStore(0).Message}
                End If

                invoiceEntityCapitatedDistribution.Id = resultStore(0).Id
                invoiceEntityCapitatedDistribution.Code = resultStore(0).Code

                transaction.Complete()
                Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = True, .ObjectEmbbeded = invoiceEntityCapitatedDistribution}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Function ConfirmInvoiceEntityCapitatedDistribution(invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution, session As SessionValues) As ActionResult(Of InvoiceEntityCapitatedDistribution) Implements IInvoiceEntityCapitatedDistributionAdminService.ConfirmInvoiceEntityCapitatedDistribution
        If invoiceEntityCapitatedDistribution Is Nothing Then
            Throw New ArgumentNullException("invoiceEntityCapitatedDistribution")
        End If

        Dim UnitOfWork As IUnitWork = _invoiceEntityCapitatedDistributionRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim resultStore = _invoiceEntityCapitatedDistributionRepository _
                    .ExecuteStoredProcedure(Of SP_ConfirmInvoiceEntityCapitatedDistribution_Result)("Billing.SP_ConfirmInvoiceEntityCapitatedDistribution",
                        {
                            ("@InvoiceEntityCapitatedDistributionId", invoiceEntityCapitatedDistribution.Id),
                            ("@CodeUser", session.AuditMessageWcf.CodeUser)
                        })

                If resultStore Is Nothing OrElse resultStore.Count = 0 Or resultStore.Any(Function(r) r.CodeMessage <> 0) Then
                    UnitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = False, .Message = resultStore(0).Message}
                End If

                Dim consecutives As String = String.Join(", ", resultStore.Select(Function(r) r.Consecutive).ToArray())

                transaction.Complete()
                Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = True, .ObjectEmbbeded = invoiceEntityCapitatedDistribution, .Message = consecutives}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Function SaveAndConfirmInvoiceEntityCapitatedDistribution(invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution, ListInvoiceEntityCapitatedDistributionDetail As List(Of Integer), session As SessionValues, Optional idSequense As Long = 0) As ActionResult(Of InvoiceEntityCapitatedDistribution) Implements IInvoiceEntityCapitatedDistributionAdminService.SaveAndConfirmInvoiceEntityCapitatedDistribution
        Dim statusPrevious = invoiceEntityCapitatedDistribution.Status
        invoiceEntityCapitatedDistribution.Status = If(statusPrevious = 2, 1, invoiceEntityCapitatedDistribution.Status)
        Dim result = Me.SaveInvoiceEntityCapitatedDistribution(invoiceEntityCapitatedDistribution, ListInvoiceEntityCapitatedDistributionDetail, session, idSequense)
        If result.StateResult
            If statusPrevious = 2 Then
                Dim resultConfirm = Me.ConfirmInvoiceEntityCapitatedDistribution(result.ObjectEmbbeded, session)
                If resultConfirm.StateResult Then
                    Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = True, .ObjectEmbbeded = result.ObjectEmbbeded, .Message = String.Format(ResourceManager.GetString("SavedAndConfirmWithJournalVoucher", "Billing"), result.ObjectEmbbeded.Code, resultConfirm.Message)}
                Else
                    Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = False, .ObjectEmbbeded = result.ObjectEmbbeded, .Message = String.Format(ResourceManager.GetString("SaveNoConfirm", "Billing"), result.ObjectEmbbeded.Code, resultConfirm.Message)}
                End If
            Else
                If invoiceEntityCapitatedDistribution.Status = 3 Then
                    Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = True, .ObjectEmbbeded = result.ObjectEmbbeded, .Message = String.Format(ResourceManager.GetString("AnnulledWithCode"), result.ObjectEmbbeded.Code)}
                Else
                    Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = True, .ObjectEmbbeded = result.ObjectEmbbeded, .Message = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)}
                End If                
            End If
        Else
            Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("NoSaved", "Billing"), result.Message)}
        End If
    End Function

    Public Function ReverseInvoiceEntityCapitatedDistribution(invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution, session As SessionValues) As ActionResult(Of InvoiceEntityCapitatedDistribution) Implements IInvoiceEntityCapitatedDistributionAdminService.ReverseInvoiceEntityCapitatedDistribution
        If invoiceEntityCapitatedDistribution Is Nothing Then
            Throw New ArgumentNullException("invoiceEntityCapitatedDistribution")
        End If

        Dim UnitOfWork As IUnitWork = _invoiceEntityCapitatedDistributionRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim resultStore = Me._invoiceEntityCapitatedDistributionRepository.SP_ReverseInvoiceEntityCapitatedDistribution(invoiceEntityCapitatedDistribution.Id, session.AuditMessageWcf.CodeUser)
                If resultStore Is Nothing OrElse resultStore.Count = 0 Or resultStore.Any(Function(r) r.CodeMessage <> 0) Then
                    UnitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = False, .Message = resultStore(0).Message}
                End If

                Dim consecutives As String = String.Join(", ",resultStore.Select(Function(r) r.Consecutive).ToArray())

                transaction.Complete()
                Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = True, .ObjectEmbbeded = invoiceEntityCapitatedDistribution, .Message = String.Format(ResourceManager.GetString("ReverseWithJournalVoucher", "Billing"), invoiceEntityCapitatedDistribution.Code, consecutives)}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return New ActionResult(Of InvoiceEntityCapitatedDistribution) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#End Region

#Region "Function Privates"

    Private Function ConvertToXmlListInvoiceEntityCapitatedDistribution(invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<InvoiceEntityCapitatedDistribution>")

        builder.Append("<Id>" & invoiceEntityCapitatedDistribution.Id & "</Id>")
        builder.Append("<Code>" & invoiceEntityCapitatedDistribution.Code & "</Code>")
        builder.Append("<OperatingUnitId>" & invoiceEntityCapitatedDistribution.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<DocumentDate>" & invoiceEntityCapitatedDistribution.DocumentDate.ToString("dd/MM/yyyy") & "</DocumentDate>")
        builder.Append("<InvoiceEntityCapitatedId>" & invoiceEntityCapitatedDistribution.InvoiceEntityCapitatedId & "</InvoiceEntityCapitatedId>")
        builder.Append("<Observation>" & invoiceEntityCapitatedDistribution.Observation & "</Observation>")
        builder.Append("<Status>" & invoiceEntityCapitatedDistribution.Status & "</Status>")

        builder.Append("</InvoiceEntityCapitatedDistribution>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListInvoiceEntityCapitatedDistributionDetail(ListInvoiceEntityCapitatedDistributionDetail As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        For Each item In ListInvoiceEntityCapitatedDistributionDetail
            builder.Append("<Invoice>")
            builder.Append("<Id>" & item & "</Id>")
            builder.Append("</Invoice>")
        Next

        Return builder.ToString()
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If

            _invoiceEntityCapitatedDistributionRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class