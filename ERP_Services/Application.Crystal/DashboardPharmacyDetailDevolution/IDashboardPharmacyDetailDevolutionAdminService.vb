'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos E. Cordoba
' Created          : 2014-11-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Dynamic
Imports Domain.Crystal.Entities

Public Interface IDashboardPharmacyDetailDevolutionAdminService
    Inherits IDisposable
    ''' <summary>
    ''' lista los detalle de farmacia
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="admission"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListDashboardPharmacyDetailDevolution(consecutive As Decimal, patientCode As String, admission As String) As List(Of ViewDashboardPharmacyDetailDevolution)
End Interface
