Imports Infrastructure.Data.Base
Imports Domain.Entities
Public Class ExemptIncomeRepository
    Inherits GenericRepository(Of ExemptIncome)
    Implements IExemptIncomeRepository

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio rentas exentas el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

End Class
