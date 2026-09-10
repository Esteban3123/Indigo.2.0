'***********************************************************************
' Assembly         : DistributedServices.SelftService
' Author           : Faiber Julian Mora
' Created          : 19-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.IOC
Imports Application.SelfService
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
'Imports Domain.Entities
'Imports Domain.Base.Entities
#End Region
Partial Public Class SelfServiceService

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="EmployeeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationPeriod(EmployeeId As Integer) As String Implements ISelfServiceRequestVacation.GetVacationPeriod
        Return _selftServiceAdminService.GetVacationPeriod(EmployeeId)
    End Function

End Class
