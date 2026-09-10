Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class CampaignDetailWitnessFileRepository
    Inherits GenericRepository(Of CampaignDetailWitnessFile)
    Implements ICampaignDetailWitnessFileRepository, Inject

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

End Class
