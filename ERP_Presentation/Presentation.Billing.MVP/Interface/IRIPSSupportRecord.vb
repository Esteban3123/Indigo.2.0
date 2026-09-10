'***********************************************************************
' Assembly         : Presentation.Billing.MVP
' Author           : Andres Alarcon
' Created          : 21-11-2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : Interfaces para Registro Soporte RIPS
'
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Controls
#End Region

Public Interface IRIPSSupportRecord
    Inherits ICrudBase

    ReadOnly Property MyTag As Object
    ReadOnly Property MyLayoutControl As IndigoLayoutControl


    'Campos RIPS
    Property Code As String
    Property AdmissionNumber As String
    Property RecordDate As Date
    Property OperatingUnitId As Integer

    Property Sequence As BillingSequence

End Interface

Public Interface IRIPSSupportRecordDetailsView

    ' Fechas
    WriteOnly Property AdmissionDate As DateTime?
    Property StartDate As DateTime?
    Property EndDate As DateTime?

    ' Datasource de combos/lookups
    Property FunctionalUnitsDataSource As XPInstantFeedbackSource
    Property StayTypesDataSource As XPInstantFeedbackSource
    Property HealthProfessionalsDataSource As XPInstantFeedbackSource
    Property SpecialtiesDataSource As XPInstantFeedbackSource
    Property PrincipalDiagnosisDataSource As XPInstantFeedbackSource
    Property RelatedDiagnosisDataSource As XPInstantFeedbackSource
    Property DischargeConditionsDataSource As XPInstantFeedbackSource
    Property DeathCauseDiagnosisDataSource As XPInstantFeedbackSource

End Interface


