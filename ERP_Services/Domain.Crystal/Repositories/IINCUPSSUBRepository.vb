'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Hector Rodriguez
' Created          : 28/01/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Public Interface IINCUPSSUBRepository
    Inherits IRepository(Of INCUPSSUB)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Function GetSubCupsImaging() As ActionResult(Of List(Of INCUPSSUB))

End Interface
