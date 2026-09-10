'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBlockRecordAccountingAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Gets the block record accounting by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Function GetBlockRecordAccountingByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordGeneralLedger

    ''' <summary>
    ''' Saves the block record accounting.
    ''' </summary>
    ''' <param name="blockRecordAccounting">The block record accounting.</param>
    ''' <returns></returns>
    Function SaveBlockRecordAccounting(ByVal blockRecordAccounting As BlockRecordGeneralLedger) As ActionResult(Of BlockRecordGeneralLedger)

    ''' <summary>
    ''' Deletes the block record accounting.
    ''' </summary>
    ''' <param name="blockRecordAccounting">The block record accounting.</param>
    ''' <returns></returns>
    Function DeleteBlockRecordAccounting(ByVal blockRecordAccounting As BlockRecordGeneralLedger) As ActionResult

End Interface
