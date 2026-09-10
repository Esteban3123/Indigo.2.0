Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class UserNoveltiesRepository
    Inherits GenericRepository(Of UserNovelties)
    Implements IUserNoveltiesRepository, Inject

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
End Class
