Imports System.Runtime.Serialization

Partial Public Class TraceabilityPaperwork

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el id del anexo generado
    ''' </summary>
    <DataMember()>
    Public Property AnnexId As Integer

    ''' <summary>
    ''' Obtiene o establece el consecutivo del anexo generado
    ''' </summary>
    <DataMember()>
    Public Property AnnexesConsecutives As String

    ''' <summary>
    ''' Obtiene o establece el orden del detalle para su asignación
    ''' </summary>
    <DataMember()>
    Public Property Order As Integer

    ''' <summary>
    ''' Obtiene o establece el id del último registro de postergado del trámite
    ''' </summary>
    <DataMember()>
    Public Property TraceabilityPaperworkPostponementReasonsId As Integer = 0

#End Region

End Class
