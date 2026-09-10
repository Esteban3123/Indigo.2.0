'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 12-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface ITurnRepository
    Inherits IRepository(Of Turn)

    ''' <summary>
    ''' Obtiene todos los turnos
    ''' </summary>
    ''' <returns>Lista de Monedas</returns>
    ''' <remarks></remarks>
    Function ListAllTurn() As List(Of Turn)

    ''' <summary>
    ''' Obtiene un turno por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetTurn(code As String, Optional tracking As Boolean = True) As Turn

    ''' <summary>
    ''' Obtiene un turno por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetTurnById(id As String, Optional tracking As Boolean = True) As Turn
End Interface
