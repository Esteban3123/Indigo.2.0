'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 28-10-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICashingRepository
    Inherits IRepository(Of CheckCashing)

    ''' <summary>
    ''' Obtiene un registro de cambio de cheque por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCashing(ByVal code As String) As CheckCashing

    ''' <summary>
    ''' Gets the cashing by identifier.
    ''' </summary>
    ''' Obtiene un registro de cambio de cheque por id
    ''' <returns></returns>
    Function GetCashingById(id As Integer, tracking As Boolean) As CheckCashing

End Interface