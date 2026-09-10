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

Imports Application.Crystal
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Microsoft.Practices.Unity

#End Region
Partial Public Class CrystalService
    Implements ICrystalServiceBedRate

    Public Function DeleteBedRate(bedRate As CHGENTARI) As ActionResult Implements ICrystalServiceBedRate.DeleteBedRate
        Using service As IBedRateAdminService = Container.Current.Resolve(Of IBedRateAdminService)()
            Return service.DeleteBedRate(bedRate)
        End Using
        'Return _bedRateAdminService.DeleteBedRate(bedRate)
    End Function

    Public Function GetBedRatebyBedCode(bedCode As Integer) As ActionResult(Of List(Of CHGENTARI)) Implements ICrystalServiceBedRate.GetBedRatebyBedCode
        Using service As IBedRateAdminService = Container.Current.Resolve(Of IBedRateAdminService)()
            Return service.GetBedRatebyBedCode(bedCode)
        End Using
        'Return _bedRateAdminService.GetBedRatebyBedCode(bedCode)
    End Function

    Public Function GetBedRatebyCode(code As Integer) As CHGENTARI Implements ICrystalServiceBedRate.GetBedRatebyCode
        Using service As IBedRateAdminService = Container.Current.Resolve(Of IBedRateAdminService)()
            Return service.GetBedRatebyCode(code)
        End Using
        'Return _bedRateAdminService.GetBedRatebyCode(code)
    End Function

    Public Function SaveBedRate(bedRate As CHGENTARI) As ActionResult(Of CHGENTARI) Implements ICrystalServiceBedRate.SaveBedRate
        Using service As IBedRateAdminService = Container.Current.Resolve(Of IBedRateAdminService)()
            Return service.SaveBedRate(bedRate)
        End Using
        'Return _bedRateAdminService.SaveBedRate(bedRate)
    End Function

End Class