'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/07/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollResumptionHoliday

    ''' <summary>
    ''' Obtiene la entidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetResumptionHoliday(EmployeeId As Integer, session As SessionValues) As ActionResult(Of List(Of ResumptionHoliday))

    ''' <summary>
    ''' Guarda una entidad
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function SaveResumptionHoliday(ResumptionHoliday As ResumptionHoliday, session As SessionValues) As ActionResult(Of ResumptionHoliday)

End Interface
