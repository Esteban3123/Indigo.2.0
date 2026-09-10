Partial Public Class TransferJuridicalDebtCollectionD

    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As TransferJuridicalDebtCollectionD
        Dim entity As TransferJuridicalDebtCollectionD = DirectCast(MemberwiseClone(), TransferJuridicalDebtCollectionD)
        Return entity
    End Function

End Class
