Public Class CostDistributionSecondaryBaseDetail

#Region "Cloneable"
    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As CostDistributionSecondaryBaseDetail
        Dim entity As CostDistributionSecondaryBaseDetail = DirectCast(MemberwiseClone(), CostDistributionSecondaryBaseDetail)
        Return entity
    End Function
#End Region


End Class
