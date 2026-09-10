'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 16-08-2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IBlockScheduleAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los bancos
    ''' </summary>
    ''' <returns>Lista de bancos</returns>
    Function ListAllBlockSchedule() As Tuple(Of BlockScheduleC, List(Of BlockSchedule))

    ''' <summary>
    ''' Lista las Unidades Funcionales con Bloqueo
    ''' </summary>
    ''' <param name="ListBlockSchedule"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveBlockSchedule(ObjblockScheduleC As BlockScheduleC, ByVal ListBlockSchedule As List(Of BlockSchedule), ByVal audit As AuditMessage) As ActionResult(Of Tuple(Of BlockScheduleC, List(Of BlockSchedule)))

End Interface
