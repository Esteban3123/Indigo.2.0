Imports System.Runtime.Serialization

Partial Public Class CustomerRetention

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre del concepto de nota de cartera
    ''' </summary>
    <DataMember()>
    Public Property PortfolioNoteConceptCodeName As String

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre del concepto de retención
    ''' </summary>
    <DataMember()>
    Public Property RetentionConceptCodeName As String

#End Region

End Class
