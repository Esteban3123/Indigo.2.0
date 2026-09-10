'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 08-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IAuditoryBankFileRepository
    Inherits IRepository(Of AuditoryBankFile)

    ''' <summary>
    ''' Obtiene un objeto de Auditoria Archivos de Bancos
    ''' </summary>
    ''' <param name="bankId">Id del Banco</param>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="PayrollDate">Fecha Nómina</param>
    ''' <returns>AuditoryBankFile</returns>
    ''' <remarks></remarks>
    Function GetAuditoryBankFile(ByVal bankId As String, ByVal groupId As String, PayrollDate As Date) As List(Of AuditoryBankFile)



End Interface
