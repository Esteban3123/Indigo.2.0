Imports System.Runtime.Serialization

Partial Public Class Hemocomponent
    <DataMember()>
    Public Property Id As Integer

    <DataMember()>
    Public Property Code As String

    <DataMember()>
    Public Property Description As String

    <DataMember()>
    Public Property ProfessionalId As string

    <DataMember()>
    Public Property Professional As String

    <DataMember()>
    Public Property Quantity As Integer

    <DataMember()>
    Public Property SpecialityCode As String
    <DataMember()>
    Public Property Details As List(Of HemocomponentDetail)

    <DataMember()>
    Public Property IdAGASICITA As Integer?

    <DataMember()>
    Public Property VolumenComponent As Decimal

    ''' <summary>
    ''' Obtiene o establece el ID de un hemocomponente extramural de la tabla HCORHEMCO
    ''' </summary>
    <DataMember()>
    Public Property IdHCORHEMCO As Integer

End Class
