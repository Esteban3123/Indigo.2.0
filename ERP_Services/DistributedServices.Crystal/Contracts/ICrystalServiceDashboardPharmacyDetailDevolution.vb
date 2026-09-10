'***********************************************************************
' Assembly         : DistributedService.Crystal
' Author           : Carlos Cordoba
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

#End Region

<ServiceContract()> _
Public Interface ICrystalServiceDashboardPharmacyDetailDevolution
    ''' <summary>
    ''' lista los detalle de farmacia
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="admission"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListDashboardPharmacyDetailDevolution(consecutive As Decimal, patientCode As String, admission As String) As List(Of ViewDashboardPharmacyDetailDevolution)
End Interface
