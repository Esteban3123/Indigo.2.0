'***********************************************************************
' Assembly         : Presentacion.Authorization.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/07/2020
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
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports Infrastructure.Data.Xpo.BillingRepository

#End Region

Public Class PAuthorizationOutsourcedServices

#Region "Fields"

    Dim view As IAuthorizationOutsourcedServices

    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    Public Sub New(ByVal _view As IAuthorizationOutsourcedServices)
        Me.view = _view
    End Sub

    Public Sub New()

    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.view.MyTag)
            Me.view.Sequense = Await model.GetSequense()
        End Using
    End Sub

    Public Sub ListPatientThirdParty()
        Me.view.PatientThirdPartyXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListThirdPartyByState(True)
    End Sub

    Public Function ListServiceOrderDetails(QuotationId As Integer) As List(Of AuthorizationOutsourcedServicesServiceOrderDetailXpo)
        Dim filter As String = "AuthorizationOutsourcedServicesId.Id = " & QuotationId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of AuthorizationOutsourcedServicesServiceOrderDetailXpo)(Nothing, filter)
    End Function

    Public Function ListPharmaceuticalDispensingDetails(QuotationId As Integer) As List(Of AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailXpo)
        Dim filter As String = "AuthorizationOutsourcedServicesId.Id = " & QuotationId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailXpo)(Nothing, filter)
    End Function

    Public Function ListAOSImportInfo() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.ListAuthorizationOutsourcedServices()
    End Function

#End Region

End Class
