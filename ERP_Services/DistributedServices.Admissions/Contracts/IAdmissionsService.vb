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
Public Interface IAdmissionsService
    Inherits IAdmissionsSequence

End Interface
