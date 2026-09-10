'***********************************************************************
' Assembly         : DistributedService.Crystal
' Author           : Diego A. Roldan
' Created          : 09-12-2015
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities

#End Region
<ServiceContract()> _
Public Interface ICrystalServiceHCUNITHIS

    <OperationContract()> _
    Function GetHCUNITHISByUFUCODIGO(ufucodigo As String) As List(Of HCUNITHIS)

    <OperationContract()> _
    Function GetHCUNITHISByUFUCODIGOWithFACMECONINS(ufucodigo As String) As HCUNITHIS

End Interface