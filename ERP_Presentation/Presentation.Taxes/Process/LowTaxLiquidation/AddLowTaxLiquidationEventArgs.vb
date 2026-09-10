Imports Domain.Entities

Public Class AddLowTaxLiquidationEventArgs
    Inherits EventArgs
    Property ListDetails As List(Of LowTaxLiquidationDetail)

    Property entity As LowTaxLiquidationDetail

    Property EditMode As Boolean

End Class
