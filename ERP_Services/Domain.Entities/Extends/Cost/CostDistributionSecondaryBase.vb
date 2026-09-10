Imports System.Runtime.Serialization

Partial Public Class CostDistributionSecondaryBase

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
    Public Function CloneEntity() As CostDistributionSecondaryBase
        Dim entity As CostDistributionSecondaryBase = DirectCast(MemberwiseClone(), CostDistributionSecondaryBase)
        Return entity
    End Function
#End Region

End Class
