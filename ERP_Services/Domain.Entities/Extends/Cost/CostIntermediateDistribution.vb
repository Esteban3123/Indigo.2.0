Imports System.Runtime.Serialization

Partial Public Class CostIntermediateDistribution

    <DataMember()>
    Property FullNameProductionCenter As String
    <DataMember()>
    Property Checked As Boolean

#Region "Cloneable"
    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As CostIntermediateDistribution
        Dim entity As CostIntermediateDistribution = DirectCast(MemberwiseClone(), CostIntermediateDistribution)
        Return entity
    End Function
#End Region
End Class
