Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo

'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 08-09-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Public Interface IPayrollOpeningBalances

    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el listado de empleados (XPO)
    ''' </summary>
    Property EmployeeXpo As LinqInstantFeedbackSource

    ''' <summary>
    ''' Id del empleado
    ''' </summary>
    ''' <returns></returns>
    Property EmployeeId As Integer

End Interface
