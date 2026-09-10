'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICheckRepository
    Inherits IRepository(Of Checkbooks)

    ''' <summary>
    ''' Obtiene una chequeda por id de cuenta bancaria
    ''' </summary>
    ''' <param name="IdEntity">The identifier entity.</param>
    ''' <returns></returns>
    Function GetCheckByIdEntityBankAccountAndStatus(ByVal IdEntity As Integer, ByVal status As Short) As Checkbooks

    Function SP_SaveCheckNumber(operatingUnitId As Integer, EntitybankAccountId As Integer, checkNumber As Long, userId As Integer) As SP_SaveCheckNumber_Result

End Interface