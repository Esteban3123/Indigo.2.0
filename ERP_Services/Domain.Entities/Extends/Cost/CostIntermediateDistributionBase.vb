Imports System.Runtime.Serialization

Partial Public Class CostIntermediateDistributionBase
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
    Public Function CloneEntity() As CostIntermediateDistributionBase
        Dim entity As CostIntermediateDistributionBase = DirectCast(MemberwiseClone(), CostIntermediateDistributionBase)
        Return entity
    End Function
#End Region
End Class
