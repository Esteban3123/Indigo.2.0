Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base

Public Interface IRIPSInvoice

#Region "Properties"

    ReadOnly Property MyTag As String

    WriteOnly Property ShowMessage(Icono As EeventViewerImages) As String

#End Region

#Region "Datasources"

    Property CareCenterDatasource As XPInstantFeedbackSource

    Property ContractDatasource As XPInstantFeedbackSource

    Property HealthAdministratorDatasource As XPInstantFeedbackSource

    Property CareGroupDatasource As XPInstantFeedbackSource

    Property InvoiceCategoryDatasource As XPInstantFeedbackSource

    Property PopulationGroupDatasource As XPInstantFeedbackSource

    Property AdmissionDatasource As XPInstantFeedbackSource

    Property ListInvoices As XPCollection(Of ViewRIPSInvoice)

    Property IncomeCauseDatasource As XPInstantFeedbackSource

#End Region

End Interface
