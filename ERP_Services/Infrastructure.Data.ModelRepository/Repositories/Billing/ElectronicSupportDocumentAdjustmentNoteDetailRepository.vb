Imports Infrastructure.Data.Base
Imports Domain.Entities
Public Class ElectronicSupportDocumentAdjustmentNoteDetailRepository
    Inherits GenericRepository(Of ElectronicSupportDocumentAdjustmentNoteDetail)
    Implements IElectronicSupportDocumentAdjustmentNoteDetailRepository

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
