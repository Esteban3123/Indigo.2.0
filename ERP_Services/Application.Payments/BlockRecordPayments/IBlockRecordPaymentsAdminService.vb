'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBlockRecordPaymentsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Gets the block record payments by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Function GetBlockRecordPaymentsByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordPayments

    ''' <summary>
    ''' Gets the block record payments by idform and consecutive.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="Consecutive">Consecutive.</param>
    ''' <returns></returns>
    Function GetBlockRecordPaymentsByIdformAndConsecutive(ByVal IdForm As String, ByVal Consecutive As String) As BlockRecordPayments

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    Function SaveBlockRecordPayments(ByVal blockRecordPayments As BlockRecordPayments) As ActionResult(Of BlockRecordPayments)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    Function DeleteBlockRecordPayments(ByVal blockRecordPayments As BlockRecordPayments) As ActionResult

End Interface
