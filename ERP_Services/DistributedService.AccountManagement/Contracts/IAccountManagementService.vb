'***********************************************************************
' Assembly         : DistributedService.AccountManagement
' Author           : Felix Camilo Salazar Roldan
' Created          : 01-11-2014
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
Public Interface IAccountManagementService
    Inherits IAccountManagementServiceManagementAreas, IAccountManagementSequence, IAccountManagementBlockRecordAccountManagement, IAccountManagementServiceAccountManagementParameters, IAccountManagementServiceRejectionReason, IAccountManagementServiceFolioTransfer, IAccountManagementDashboardAccountAssignment
End Interface

