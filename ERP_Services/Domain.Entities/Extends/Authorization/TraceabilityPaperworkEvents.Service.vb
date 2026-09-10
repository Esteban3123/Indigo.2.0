Imports System.Runtime.Serialization

Partial Public Class TraceabilityPaperworkEvents

#Region "Properties"

    ''' <summary>
    ''' Listado de archivos adjuntos que se le agregan a un evento
    ''' </summary>
    <DataMember()>
    Public Property ListAttachment As List(Of Attachment)

#End Region

End Class
