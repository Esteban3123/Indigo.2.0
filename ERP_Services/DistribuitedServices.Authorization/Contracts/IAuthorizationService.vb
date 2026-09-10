'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 27/02/2020
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

#End Region

<ServiceModel.ServiceContract()>
Public Interface IAuthorizationService
    Inherits IAuthorizationBlockRecordAuthorization, IAuthorizationSequence, IAuthorizationServiceAuthorizationGroup, IAuthorizationServiceCancellationReasons,
        IAuthorizationServiceAuthorizationSource, IAuthorizationServiceAuthorizationScheduleTemplate, IAuthorizationServiceAuthorizationPortfolio,
        IAuthorizationServiceConfigurationServicesAmbulatory, IAuthorizationServiceAuthorizationSchedule, IAuthorizationServiceTraceabilityPaperwork,
        IAuthorizationServiceManagementMedicalOrder, IAuthorizationServiceDashboardContractCoverage, IAuthorizationServiceSettingsAuthorization, IAuthorizationServiceAuthorizationOutsourcedServices,
        IAuthorizationServiceReports, IAuthorizationServiceAuthorizationRejection, IAuthorizationServicePostponementReasons

End Interface
