'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 03-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IIncreaseSalaryDomain
    Inherits IDisposable

    Function IncreaseSalary(ContractList As List(Of Contract), PercentageIncrease As Decimal) As Dynamic.ExpandoObject

    Function ExecuteRetroactive(ContractList As List(Of Contract), InitialDate As Date, PercentageIncrease As Decimal, Group As Group, PayrollPaid As Byte, IndigoSessionValues As SessionValues, Optional SpecificEmployeeId As Integer = 0) As List(Of RetroactiveC)

End Interface
