'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 06-05-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface IPayrollCommon
    ''' <summary>
    ''' consulta los campos NULL para customizacion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetFieldsNULL(TableName As String, session As SessionValues) As DataSet
End Interface
