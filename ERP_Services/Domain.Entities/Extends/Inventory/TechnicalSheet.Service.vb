Imports System.Runtime.Serialization

Public Class TechnicalSheet
    ''' <summary>
    ''' Obtiene o establece el codigo del diagnostico
    ''' </summary>
    <DataMember()>
    Public Property DiagnosticCode As String

    ''' <summary>
    ''' Obtiene o establece el nombre del diagnostico
    ''' </summary>
    <DataMember()>
    Public Property DiagnosticName As String
End Class
