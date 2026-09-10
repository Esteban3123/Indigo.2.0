Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class ElectronicSupportDocumentDetailRepository
    Inherits GenericRepository(Of ElectronicSupportDocumentDetail)
    Implements IElectronicSupportDocumentDetailRepository

    ' contexto del repositorio
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

End Class
