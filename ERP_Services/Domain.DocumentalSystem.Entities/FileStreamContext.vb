'************************************************************
' Assembly         : Domain.DocumentalSystem.Entities
' Author           : Juan Diego Diaz
' Created          : 10-09-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

''' <summary>
''' Clase para manejar información del contexto 
''' de la transaccion de un documento
''' </summary>
''' <remarks></remarks>
Public Class FileStreamContext

    Public Property InternalPath As String
    Public Property TransactionContext As Byte()
    Public Property FileExtension As String

End Class
