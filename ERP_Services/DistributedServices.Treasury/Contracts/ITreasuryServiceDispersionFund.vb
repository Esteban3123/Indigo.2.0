'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceDispersionFund

    ''' <summary>
    ''' Efectúa los pagos de la programación
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function MakeSchedulePayment(SchedulePayment As SchedulePayment, audit As AuditMessage, idSequence As Int64, sequenceC As Domain.Entities.TreasurySequence) As ActionResult(Of List(Of Tuple(Of String, Integer)))
    ''' <summary>
    ''' metodo para generar el archivo para pagos en bancos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GenerateBankFile(SchedulePayment As SchedulePayment, bankId As Integer, companyNIT As String, companyName As String, Optional optionalParameters As List(Of String) = Nothing) As ActionResult(Of String)
End Interface