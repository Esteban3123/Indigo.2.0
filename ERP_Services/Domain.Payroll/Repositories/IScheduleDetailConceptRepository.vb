'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 07-10-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IScheduleDetailConceptRepository
    Inherits IRepository(Of ScheduleDetailConcept)

End Interface
