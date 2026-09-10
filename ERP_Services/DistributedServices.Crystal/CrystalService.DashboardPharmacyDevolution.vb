'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Carlos Cordoba
' Created          : 11-02-2015
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Crystal
Imports Domain.Crystal.Entities
Imports Microsoft.Practices.Unity

#End Region

Partial Class CrystalService

    ''' <summary>
    ''' Lists the dashboard pharmacy detail devolution.
    ''' </summary>
    ''' <param name="consecutive">The consecutive.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admission">The admission.</param>
    ''' <returns></returns>
    Public Function ListDashboardPharmacyDetailDevolution(consecutive As Decimal, patientCode As String, admission As String) As List(Of ViewDashboardPharmacyDetailDevolution) Implements ICrystalServiceDashboardPharmacyDetailDevolution.ListDashboardPharmacyDetailDevolution
        Using service As IDashboardPharmacyDetailDevolutionAdminService = Container.Current.Resolve(Of IDashboardPharmacyDetailDevolutionAdminService)()
            Return service.ListDashboardPharmacyDetailDevolution(consecutive, patientCode, admission)
        End Using
        'Return _dashboardPharmacyDevolutionAdminService.ListDashboardPharmacyDetailDevolution(consecutive, patientCode, admission)
    End Function
End Class
