'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Carlos Cordoba
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IBedRepository
    Inherits IRepository(Of CHCAMASHO)
    ''' <summary>
    ''' obtiene una cama por codigo
    ''' </summary>
    ''' <param name="bedCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBedbyCode(bedCode As Integer) As CHCAMASHO
End Interface
