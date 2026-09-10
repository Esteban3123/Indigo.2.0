Imports System.Runtime.Serialization

Partial Public Class CostDistributionBase

    <DataMember()>
    Property DistributionTypeName As String
    <DataMember()>
    Property MeasureUnitName As String
    <DataMember()>
    Property DistributionBaseName As String

#Region "Cloneable"
    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As CostDistributionBase
        Dim entity As CostDistributionBase = DirectCast(MemberwiseClone(), CostDistributionBase)
        Return entity
    End Function
#End Region

End Class
