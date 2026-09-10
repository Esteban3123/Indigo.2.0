'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IDispersionFundAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Efectúa los pagos de la programación
    ''' </summary>
    ''' <returns></returns>
    Function MakeSchedulePayment(ByVal schedulePayment As SchedulePayment, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0, Optional ByVal sequenceC As TreasurySequence = Nothing) As ActionResult(Of List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' metodo para generar el archivo para pagos en bancos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateBankFile(SchedulePayment As SchedulePayment, bankId As Integer, companyNIT As String, companyName As String, Optional optionalParameters As List(Of String) = Nothing) As ActionResult(Of String)
End Interface
