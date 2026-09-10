'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Diego A. Roldán L.
' Created          : 2023-03-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IRequestParamRepository
    Inherits IRepository(Of RequestParam)

    ''' <summary>
    ''' Get by code
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Function GetRequestParamByCode(code As String) As RequestParam

End Interface
