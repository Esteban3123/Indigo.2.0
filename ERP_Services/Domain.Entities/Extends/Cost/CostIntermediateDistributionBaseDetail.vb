Public Class CostIntermediateDistributionBaseDetail
#Region "Cloneable"
    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As CostIntermediateDistributionBaseDetail
        Dim entity As CostIntermediateDistributionBaseDetail = DirectCast(MemberwiseClone(), CostIntermediateDistributionBaseDetail)
        Return entity
    End Function
#End Region
End Class
