Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class AverageStandardCostDetailsRepository
    Inherits GenericRepository(Of StandarCostDetails)
    Implements IAverageStandardCostDetailsRepository

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
End Class
