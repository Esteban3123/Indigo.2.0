'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Common.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

Public Interface IBlockRecordMaintenanceAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Gets the block record payments by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Function GetBlockRecordMaintenanceByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordMaintenance

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>ActionResult</returns>
    Function SaveBlockRecordMaintenance(ByVal blockRecord As BlockRecordMaintenance) As ActionResult(Of BlockRecordMaintenance)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>ActionResult</returns>
    Function DeleteBlockRecordMaintenance(ByVal blockRecord As BlockRecordMaintenance) As ActionResult

End Interface
