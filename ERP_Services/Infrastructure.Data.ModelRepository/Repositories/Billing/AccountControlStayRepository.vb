Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class AccountControlStayRepository
    Inherits GenericRepository(Of AccountControlStays)
    Implements IAccountControlStayRepository, Inject

#Region "Builder"

    ''' <summary>
    ''' Contexto de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de inventario
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

End Class
