Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class UsersAssignmentRepository
    Inherits GenericRepository(Of UsersAssignment)
    Implements IUsersAssignmentRepository, Inject
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
End Class
