'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo
' Created          : 08-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Base

Public Class AuditoryBankFileRepository
    Inherits GenericRepository(Of AuditoryBankFile)

    Implements IAuditoryBankFileRepository

    ' Contexto de payroll
    Private _contex As IPayrollUnitOfWork

    Public Sub New(ByVal contexto As IPayrollUnitOfWork)
        MyBase.New(contexto)
        _contex = contexto
    End Sub

    ''' <summary>
    ''' Obtiene un objeto de Auditoria Archivos de Bancos
    ''' </summary>
    ''' <param name="bankId">Id del Banco</param>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="PayrollDate">Fecha Nómina</param>
    ''' <returns>AuditoryBankFile</returns>
    ''' <remarks></remarks>
    Public Function GetAuditoryBankFile(bankId As String, groupId As String, PayrollDate As Date) As List(Of AuditoryBankFile) Implements IAuditoryBankFileRepository.GetAuditoryBankFile
        Dim AuditoryBankFiles = From e In _contex.AuditoryBankFile
                               Where e.BankId = bankId And e.GroupId = groupId And e.PayrollDate = PayrollDate
                               Select e

        If AuditoryBankFiles.Count() > 0 Then
            Return AuditoryBankFiles.ToList()
        Else
            Return Nothing
        End If

    End Function
End Class
