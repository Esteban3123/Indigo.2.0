Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class HighRiskDrugsRepository
    Inherits GenericRepository(Of HighRiskDrugs)
    Implements IHighRiskDrugsRepository, Inject

#Region "Builder"

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal Context As IGlobalModelUnitOfWork)
        MyBase.New(Context)
        _context = Context
    End Sub

#End Region

End Class
