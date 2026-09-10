'***********************************************************************
' Assembly         : Application.Payments
' Author           : Juan Carlos Bermudez
' Created          : 16/07/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IBlockRecordPayrollAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Gets the block record payments by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Function GetBlockRecordPayrollByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordPayroll

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    Function SaveBlockRecordPayroll(ByVal blockRecordPayroll As BlockRecordPayroll) As ActionResult(Of BlockRecordPayroll)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    Function DeleteBlockRecordPayroll(ByVal blockRecordPayroll As BlockRecordPayroll) As ActionResult

End Interface
