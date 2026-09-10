'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Logging
' Author           : WalterSierra
' Created          : 15-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-26
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


''' <summary>
''' enumeracion necesaria para la priorizacion de los mensajes
''' </summary>
Public Enum Priority As Integer
    ''' <summary>
    ''' 
    ''' </summary>
    VeryLow = 0
    ''' <summary>
    ''' 
    ''' </summary>
    Low = 1
    ''' <summary>
    ''' 
    ''' </summary>
    Normal = 2
    ''' <summary>
    ''' 
    ''' </summary>
    High = 3
    ''' <summary>
    ''' 
    ''' </summary>
    VeryHigh = 4
End Enum

''' <summary>
''' clase con las constantes para la categorizacion de las trazas
''' </summary>
Friend NotInheritable Class Categories
    Public Const General As String = "General"
    Public Const Trazas As String = "Trazas"
End Class
