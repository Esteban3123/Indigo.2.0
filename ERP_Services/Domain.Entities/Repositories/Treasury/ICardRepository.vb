'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 27-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICardRepository
    Inherits IRepository(Of Cards)

    ''' <summary>
    ''' Obtener un registro de tarjeta
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCard(ByVal code As String) As Cards
    ''' <summary>
    ''' metodo para obtener una tarjeta por el id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCardById(id As Integer) As Cards

End Interface
