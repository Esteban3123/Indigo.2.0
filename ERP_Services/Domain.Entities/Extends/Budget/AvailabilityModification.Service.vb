#Region "imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class AvailabilityModification

    <DataMember()>
    Public Property OperatingUnitId As Integer

    ''' <summary>
    ''' Obtiene o establece el codigo de la disponibilidad
    ''' </summary>
    <DataMember()>
    Public Property CodeAvailability As String

End Class
