Imports System.Runtime.Serialization

Partial Public Class StabilityTableDetailReconstitution

    <DataMember()>
    Public Property ATCCodeName As String

    <DataMember()>
    Public Property StorageTemperatureCodeName

    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As StabilityTableDetailReconstitution
        Dim entity As StabilityTableDetailReconstitution = DirectCast(MemberwiseClone(), StabilityTableDetailReconstitution)
        Return entity
    End Function

End Class
