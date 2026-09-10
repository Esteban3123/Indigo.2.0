'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBlockRecordMedicalFeesAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Gets the block record payments by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Function GetBlockRecordMedicalFeesByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordMedicalFees

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    Function SaveBlockRecordMedicalFees(ByVal BlockRecordMedicalFees As BlockRecordMedicalFees) As ActionResult(Of BlockRecordMedicalFees)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    Function DeleteBlockRecordMedicalFees(ByVal BlockRecordMedicalFees As BlockRecordMedicalFees) As ActionResult

End Interface
