'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Sumit Sarkar
' Created          : 29/04/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository

#End Region

Public Class PQuotation

#Region "Fields"

    Dim view As IQuotation

    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    Public Sub New(ByVal _view As IQuotation)
        Me.view = _view
    End Sub

    Public Sub New()

    End Sub

#End Region

#Region "Methods"

    Public Async Function GetSequense() As Task
        Using model As New MBlockRecordAndSequense(Me.view.MyTag)
            Me.view.Sequense = Await model.GetSequense()
            Return
        End Using
    End Function

    Public Sub ListPatientThirdParty()
        Me.view.PatientThirdPartyXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListThirdPartyByState(True)
    End Sub

    Public Function ListQuotationServiceOrderDetails(QuotationId As Integer) As List(Of QuotationServiceOrderDetailXpo)
        Dim filter As String = "QuotationId.Id = " & QuotationId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of QuotationServiceOrderDetailXpo)(Nothing, filter)
    End Function

    Public Function ListQuotationPharmaceuticalDispensingDetails(QuotationId As Integer) As List(Of QuotationPharmaceuticalDispensingDetailXpo)
        Dim filter As String = "QuotationId.Id = " & QuotationId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of QuotationPharmaceuticalDispensingDetailXpo)(Nothing, filter)
    End Function

    Public Function ListQuotationImportInfo() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListQuotation()
    End Function

#End Region

End Class
