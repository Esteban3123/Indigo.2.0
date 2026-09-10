Partial Public Class PortfolioInitialBalanceAccountReceivableShare
    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As PortfolioInitialBalanceAccountReceivableShare
        Dim entity As PortfolioInitialBalanceAccountReceivableShare = DirectCast(MemberwiseClone(), PortfolioInitialBalanceAccountReceivableShare)
        Return entity
    End Function
End Class
