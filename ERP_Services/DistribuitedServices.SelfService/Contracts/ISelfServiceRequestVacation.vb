'***********************************************************************
' Assembly         : DistributedServices.SelfService
' Author           : Faiber Julian Mora
' Created          : 19-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
#End Region

<ServiceContract()> _
Public Interface ISelfServiceRequestVacation

    ''' <summary>
    ''' metodo que retorna un objeto Json
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetVacationPeriod(EmployeeId As Integer) As String

End Interface
