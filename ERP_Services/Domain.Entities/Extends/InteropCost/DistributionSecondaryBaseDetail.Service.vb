Public Class DistributionSecondaryBaseDetail

#Region "Cloneable"
    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As DistributionSecondaryBaseDetail
        Dim entity As DistributionSecondaryBaseDetail = DirectCast(MemberwiseClone(), DistributionSecondaryBaseDetail)
        Return entity
    End Function
#End Region

End Class
