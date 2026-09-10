'***********************************************************************
' Assembly         : Domain.Entities
' Author           : Andrés Steven Rojas
' Created          : 31/10/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICardCollectionsRepository
    Inherits IRepository(Of CardCollections)

    ''' <summary>
    ''' Obtener un registro de recaudo de tarjetas por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCardCollections(ByVal code As String) As CardCollections

    ''' <summary>
    ''' Obtener un registro de recaudo de tarjetas por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetCardCollectionsById(id As Integer) As CardCollections

End Interface


