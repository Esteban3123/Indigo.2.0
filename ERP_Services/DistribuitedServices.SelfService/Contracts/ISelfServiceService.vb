'***********************************************************************
' Assembly         : DistributedService.SelfService
' Author           : Faiber Julian Moras
' Created          : 19-12-2014
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
Public Interface ISelfServiceService
    Inherits ISelfServiceRequestVacation

End Interface
