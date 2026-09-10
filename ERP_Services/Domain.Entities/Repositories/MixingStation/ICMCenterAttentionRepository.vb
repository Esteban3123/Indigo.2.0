'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 06-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Public Interface ICMCenterAttentionRepository
    Inherits IRepository(Of CMCenterAttention)

    ''' <summary>
    ''' Obtiene todos los parametros de configuración de central de mezclas
    ''' </summary>
    ''' <returns>Lista de Monedas</returns>
    ''' <remarks></remarks>
    Function ListAllCMCenterAttention() As List(Of CMCenterAttention)

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por codigo
    ''' </summary>
    ''' <param name="id">The code.</param>
    ''' <returns></returns>
    Function GetCMCenterAttention(id As String, Optional tracking As Boolean = True) As CMCenterAttention

End Interface
