'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 26-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IBlockRecordMixingStationRepository
    Inherits IRepository(Of BlockRecordMixingStation)

    ''' <summary>
    ''' Obtiene el bloque del registro de central de mezclas por IdForm y registro del identificador
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetBlockRecordMixingStationByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String, Optional tracking As Boolean = True) As BlockRecordMixingStation

End Interface
