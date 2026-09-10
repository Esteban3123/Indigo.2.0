#Region "Imports"

Imports Application.Portfolio
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Class PortfolioService

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Public Function GetSequenseByIdForm(idForm As String) As Domain.Entities.PortfolioSequence Implements IPortfolioSequense.GetSequenseByIdForm
        Using service As IPortfolioSequenseAdminService = Container.Current.Resolve(Of IPortfolioSequenseAdminService)()
            Return service.GetSequenseByIdForm(idForm)
        End Using
        'Return Me._sequenseAdminService.GetSequenseByIdForm(idForm)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Function GetNumericSequenseGroupById(id As Integer) As List(Of String) Implements IPortfolioSequense.GetNumericSequenseGroupById
        Using service As IPortfolioSequenseAdminService = Container.Current.Resolve(Of IPortfolioSequenseAdminService)()
            Return service.GetNumericSequenseGroupById(id)
        End Using
        'Return _sequenseAdminService.GetNumericSequenseGroupById(id)
    End Function

    Public Function SaveSequence(seq As Domain.Entities.PortfolioSequence) As Domain.Base.Entities.ActionResult Implements IPortfolioService.SaveSequence
        Using service As IPortfolioSequenseAdminService = Container.Current.Resolve(Of IPortfolioSequenseAdminService)()
            Return service.SaveSequence(seq)
        End Using
        'Return Me._sequenseAdminService.SaveSequence(seq)
    End Function

End Class
