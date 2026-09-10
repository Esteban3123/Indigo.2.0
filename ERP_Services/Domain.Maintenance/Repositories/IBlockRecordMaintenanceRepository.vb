'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IBlockRecordMaintenanceRepository
    Inherits IRepository(Of BlockRecordMaintenance)

    ''' <summary>
    ''' Gets the block record payments by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetBlockRecordMaintenanceByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String, Optional tracking As Boolean = True) As BlockRecordMaintenance

End Interface
