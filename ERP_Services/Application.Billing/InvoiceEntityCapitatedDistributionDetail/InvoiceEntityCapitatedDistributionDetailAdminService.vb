#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Class InvoiceEntityCapitatedDistributionDetailAdminService
    Implements IInvoiceEntityCapitatedDistributionDetailAdminService

#Region "Fields"

    Dim _invoiceEntityCapitatedDistributionDetailRepository As IInvoiceEntityCapitatedDistributionDetailRepository

#End Region

#Region "Builder"

    Public Sub New(ByVal invoiceEntityCapitatedDistributionDetailRepository As IInvoiceEntityCapitatedDistributionDetailRepository)
        If invoiceEntityCapitatedDistributionDetailRepository Is Nothing Then
            Throw New ArgumentNullException("invoiceEntityCapitatedDistributionDetailRepository")
        End If
        _invoiceEntityCapitatedDistributionDetailRepository = invoiceEntityCapitatedDistributionDetailRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetInvoiceEntityCapitatedDistributionDetailsByInvoiceEntityCapitatedId(invoiceEntityCapitatedId As Integer, invoiceEntityCapitatedDistributionId As Integer) As ActionResult(Of List(Of InvoiceEntityCapitatedDistributionDetail)) Implements IInvoiceEntityCapitatedDistributionDetailAdminService.GetInvoiceEntityCapitatedDistributionDetailsByInvoiceEntityCapitatedId
        Dim ListInvoiceEntityCapitatedDistributionDetail As New List(Of InvoiceEntityCapitatedDistributionDetail)
        Try
            Dim resultStore = _invoiceEntityCapitatedDistributionDetailRepository.SP_GetInvoiceEntityCapitatedDistributionDetails(invoiceEntityCapitatedId, invoiceEntityCapitatedDistributionId)
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each item In resultStore
                    ListInvoiceEntityCapitatedDistributionDetail.Add(New InvoiceEntityCapitatedDistributionDetail With
                    {
                        .Id = If(item.Id Is Nothing, 0, item.Id),
                        .InvoiceEntityCapitatedDistributionId = If(item.Id Is Nothing, 0, item.InvoiceEntityCapitatedDistributionId),
                        .InvoiceId = item.InvoiceId,
                        .HealthAdministratorId = item.HealthAdministratorId,
                        .InvoiceCategoryId = item.InvoiceCategoryId,
                        .InvoiceNumber = item.InvoiceNumber,
                        .InvoiceDate = item.InvoiceDate,
                        .InvoiceValue = item.InvoiceValue,
                        .SelectOption = If(item.Id Is Nothing, False, True),
                        .HealthAdministratorName = item.HealthAdministratorName,
                        .InvoiceCategoryName = item.InvoiceCategoryName,
                        .StatusName = If(item.Status = 1, "Facturado", "Anulado"),
                        .AdmissionNumber = item.AdmissionNumber
                    })
                Next
            End If

            Return New ActionResult(Of List(Of InvoiceEntityCapitatedDistributionDetail)) With {.StateResult = True, .ObjectEmbbeded = ListInvoiceEntityCapitatedDistributionDetail}
        Catch ex As Exception
            Return New ActionResult(Of List(Of InvoiceEntityCapitatedDistributionDetail)) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If

            _invoiceEntityCapitatedDistributionDetailRepository = Nothing
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