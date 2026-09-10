Imports System.Runtime.Serialization
Public Class CostDistributionBaseMeasurementUnit

    <DataMember()>
    Public Property CodeNameMeasureUnit As String

#Region "Cloneable"
    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As CostDistributionBaseMeasurementUnit
        Dim entity As CostDistributionBaseMeasurementUnit = DirectCast(MemberwiseClone(), CostDistributionBaseMeasurementUnit)
        Return entity
    End Function
#End Region

End Class
