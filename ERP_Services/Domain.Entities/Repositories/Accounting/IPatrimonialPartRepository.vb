'***********************************************************************
' Assembly         : Domain.Accounting
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IPatrimonialPartRepository
    Inherits IRepository(Of Shareholding)

    ''' <summary>
    ''' Obtiene una participación patrimonial por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetPatrimonialPart(ByVal code As String) As Shareholding

End Interface
