'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IBlockRecordPaymentsRepository
    Inherits IRepository(Of BlockRecordPayments)

    ''' <summary>
    ''' Gets the block record payments by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetBlockRecordPaymentsByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String, Optional tracking As Boolean = True) As BlockRecordPayments

    ''' <summary>
    ''' Gets the block record payments by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="Consecutive">Consecutive.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetBlockRecordPaymentsByIdformAndConsecutive(ByVal IdForm As String, ByVal Consecutive As String, Optional tracking As Boolean = True) As BlockRecordPayments

End Interface
