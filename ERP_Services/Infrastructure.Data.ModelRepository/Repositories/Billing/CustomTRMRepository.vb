Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class CustomTRMRepository
    Inherits GenericRepository(Of CustomTRM)
    Implements ICustomTRMRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
End Class
