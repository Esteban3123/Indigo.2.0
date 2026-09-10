'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 05-08-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Public Interface IProductionLineRepository
    Inherits IRepository(Of ProductionLine)
    ''' <summary>
    ''' Obtiene todos las lineas de producción
    ''' </summary>
    ''' <returns>Lista de tipos de dosis unitarias</returns>
    ''' <remarks></remarks>
    Function ListAllProductionLine() As List(Of ProductionLine)

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetProductionLine(code As String, Optional tracking As Boolean = True) As ProductionLine

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetProductionLineId(id As String, Optional tracking As Boolean = True) As ProductionLine
End Interface
