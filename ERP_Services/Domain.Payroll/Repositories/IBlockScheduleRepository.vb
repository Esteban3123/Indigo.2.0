'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 16-08-2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IBlockScheduleRepository

    Inherits IRepository(Of BlockSchedule)

    ''' <summary>
    ''' Lista todos los Bloqueos de las Unidades Funcionales
    ''' </summary>
    ''' <returns></returns>
    Function ListAllBlockSchedule() As Tuple(Of BlockScheduleC, List(Of BlockSchedule))

End Interface
