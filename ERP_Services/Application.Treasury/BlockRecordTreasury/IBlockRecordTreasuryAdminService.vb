'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 31-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBlockRecordTreasuryAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Gets the block record treasury by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Function GetBlockRecordTreasuryByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordTreasury

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>ActionResult</returns>
    Function SaveBlockRecordTreasury(ByVal blockRecordTreasury As BlockRecordTreasury) As ActionResult(Of BlockRecordTreasury)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>ActionResult</returns>
    Function DeleteBlockRecordTreasury(ByVal blockRecordTreasury As BlockRecordTreasury) As ActionResult

End Interface
