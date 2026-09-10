Imports System.Runtime.Serialization

Partial Public Class StabilityTableDetailDilution

    <DataMember()>
    Public Property ATCCodeName As String

    <DataMember()>
    Public Property StorageTemperatureCodeName

    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As StabilityTableDetailDilution
        Dim entity As StabilityTableDetailDilution = DirectCast(MemberwiseClone(), StabilityTableDetailDilution)
        Return entity
    End Function

End Class
