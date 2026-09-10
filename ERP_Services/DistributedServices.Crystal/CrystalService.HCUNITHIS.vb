'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Jhossept k. Garay
' Created          : 11-02-2015
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
Imports Application.Crystal
Imports Application.Common
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class CrystalService
    Implements ICrystalServiceHCUNITHIS

    Public Function GetHCUNITHISByUFUCODIGO(ufucodigo As String) As List(Of HCUNITHIS) Implements ICrystalServiceHCUNITHIS.GetHCUNITHISByUFUCODIGO
        Using service As IHCUNITHISAdminService = Container.Current.Resolve(Of IHCUNITHISAdminService)()
            Return service.GetHCUNITHISByUFUCODIGO(ufucodigo)
        End Using
        'Return _iHCUNITHISAdminService.GetHCUNITHISByUFUCODIGO(ufucodigo)
    End Function

    Public Function GetHCUNITHISByUFUCODIGOWithFACMECONINS(ufucodigo As String) As HCUNITHIS Implements ICrystalServiceHCUNITHIS.GetHCUNITHISByUFUCODIGOWithFACMECONINS
        Using service As IHCUNITHISAdminService = Container.Current.Resolve(Of IHCUNITHISAdminService)()
            Return service.GetHCUNITHISByUFUCODIGOWithFACMECONINS(ufucodigo)
        End Using
        'Return _iHCUNITHISAdminService.GetHCUNITHISByUFUCODIGOWithFACMECONINS(ufucodigo)
    End Function
End Class