'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Rafael Patiño
' Created          : 2018-08-31
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IDashboardPharmacyDetailSurgicalPackageRepository
    Inherits IRepository(Of ViewDashBoardPharmacy_SurgicalPackageDeatils)
    ''' <summary>
    ''' lista los detalle de los paquete QX
    ''' </summary>
    ''' <param name="ConsecutivoProgramacion"></param>
    ''' <param name="patientCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListDashboardPharmacyDetailSurgicalPackage(ConsecutivoProgramacion As Decimal, patientCode As String) As List(Of ViewDashBoardPharmacy_SurgicalPackageDeatils)

    ''' <summary>
    ''' Devuelve un detalle de la solicitud del paquete QX
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="productCode"></param>
    ''' <returns></returns>
    Function DashboardPharmacyDetailSurgicalPackage(consecutive As Decimal, patientCode As String, productCode As String) As ViewDashBoardPharmacy_SurgicalPackageDeatils

End Interface
