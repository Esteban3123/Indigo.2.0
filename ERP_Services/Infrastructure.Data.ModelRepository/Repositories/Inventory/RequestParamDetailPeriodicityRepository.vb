Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class RequestParamDetailPeriodicityRepository
    Inherits GenericRepository(Of RequestParamDetailPeriodicity)
    Implements IRequestParamDetailPeriodicityRepository, Inject


    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
End Class
