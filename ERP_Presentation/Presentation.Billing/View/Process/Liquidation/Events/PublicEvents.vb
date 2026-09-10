Imports Domain.Entities

Public Class RunLiquidateFolioEventArgs
    Inherits EventArgs

    Property revenueControlDetailCrossingList As List(Of RevenueControlDetailCrossing)

End Class

Public Class RunIncludePortfolioAdvanceArgs
    Inherits EventArgs

    Property ListPortfolioAdvance As List(Of PortfolioAdvance)
End Class
