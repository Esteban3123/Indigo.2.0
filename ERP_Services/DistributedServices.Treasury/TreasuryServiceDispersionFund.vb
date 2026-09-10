'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 11-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class TreasuryService
    Implements ITreasuryServiceDispersionFund


    ''' <summary>
    ''' Efectúa los pagos de la programación
    ''' </summary>
    ''' <returns></returns>
    Public Function MakeSchedulePayment(SchedulePayment As SchedulePayment, audit As AuditMessage, idSequence As Int64, sequenceC As Domain.Entities.TreasurySequence) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements ITreasuryServiceDispersionFund.MakeSchedulePayment
        Using service As IDispersionFundAdminService = Container.Current.Resolve(Of IDispersionFundAdminService)()
            Return service.MakeSchedulePayment(SchedulePayment, audit, idSequence, sequenceC)
        End Using
        'Return Me._dispersionFundAdminService.MakeSchedulePayment(SchedulePayment, audit, idSequence, sequenceC)
    End Function

    ''' <summary>
    ''' metodo para generar el archivo para pagos en bancos
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <param name="bankId"></param>
    ''' <returns></returns>
    Public Function GenerateBankFile(SchedulePayment As SchedulePayment, bankId As Integer, companyNIT As String, companyName As String, Optional optionalParameters As List(Of String) = Nothing) As ActionResult(Of String) Implements ITreasuryServiceDispersionFund.GenerateBankFile
        Using service As IDispersionFundAdminService = Container.Current.Resolve(Of IDispersionFundAdminService)()
            Return service.GenerateBankFile(SchedulePayment, bankId, companyNIT, companyName, optionalParameters)
        End Using
        'Return Me._dispersionFundAdminService.GenerateBankFile(SchedulePayment, bankId, companyNIT, companyName, optionalParameters)
    End Function
End Class
