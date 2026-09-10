'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 08-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IAuditoryBankFileAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Graba o Actualiza una Auditoria de Archivos de Bancos
    ''' </summary>
    ''' <param name="auditoryBankFile">auditoryBankFile</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveAuditoryBankFile(ByVal auditoryBankFile As AuditoryBankFile, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene un objeto de Auditoria Archivos de Bancos
    ''' </summary>
    ''' <param name="bankId">Id del Banco</param>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="PayrollDate">Fecha Nómina</param>
    ''' <returns>AuditoryBankFile</returns>
    ''' <remarks></remarks>
    Function GetAuditoryBankFile(bankId As String, groupId As String, PayrollDate As Date) As List(Of AuditoryBankFile)

End Interface
