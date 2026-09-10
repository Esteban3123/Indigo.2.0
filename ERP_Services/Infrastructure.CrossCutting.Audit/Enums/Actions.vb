'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.AuditData
' Author           : Juan Diego Diaz M.
' Created          : 13-11-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Enumeracion utilizada para determinar las acciones a auditar
''' </summary>
Public Enum Actions As Integer
    ''' <summary>
    ''' Accion de INSERT
    ''' </summary>
    Insert = 1
    ''' <summary>
    ''' Accion de UPDATE
    ''' </summary>
    Update = 2
    ''' <summary>
    ''' accion de DELETE
    ''' </summary>
    Delete = 3
    ''' <summary>
    ''' accion de SELECT (para reportes)
    ''' </summary>
    Print = 4
    ''' <summary>
    ''' Accion de confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Confirm = 5
    ''' <summary>
    ''' Accion para anular un documento
    ''' </summary>
    ''' <remarks></remarks>
    Annular = 6
    ''' <summary>
    ''' Desconfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Disconfirm = 7
End Enum