'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 08-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollAuditoryBankFile

    ''' <summary>
    ''' Obtiene un objeto de Auditoria Archivos de Bancos
    ''' </summary>
    ''' <param name="bankId">Id del Banco</param>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="PayrollDate">Fecha Nómina</param>
    ''' <returns>AuditoryBankFile</returns>
    ''' <remarks></remarks>
    <OperationContract> _
    Function GetAuditoryBankFile(bankId As String, groupId As String, PayrollDate As Date, session As SessionValues) As List(Of AuditoryBankFile)

    ''' <summary>
    ''' Almacena el Objeto Auditoria Archivos de Bancos
    ''' </summary>
    ''' <param name="AuditoryBankFile">Objeto AuditoryBankFile</param>
    ''' <param name="session">Variable Sesion</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract> _
    Function SaveAuditoryBankFile(ByVal AuditoryBankFile As AuditoryBankFile, ByVal session As SessionValues) As Boolean

End Interface
