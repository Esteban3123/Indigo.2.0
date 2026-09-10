#Region "Imports"

Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Class TreasuryService

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Public Function GetSequenseByIdForm(idForm As String) As Domain.Entities.TreasurySequence Implements ITreasurySequense.GetSequenseByIdForm
        Using service As ITreasurySequenseAdminService = Container.Current.Resolve(Of ITreasurySequenseAdminService)()
            Return service.GetSequenseByIdForm(idForm)
        End Using
        'Return Me._sequenseAdminService.GetSequenseByIdForm(idForm)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Function GetNumericSequenseGroupById(id As Integer) As List(Of String) Implements ITreasurySequense.GetNumericSequenseGroupById
        Using service As ITreasurySequenseAdminService = Container.Current.Resolve(Of ITreasurySequenseAdminService)()
            Return service.GetNumericSequenseGroupById(id)
        End Using
        'Return _sequenseAdminService.GetNumericSequenseGroupById(id)
    End Function

    Public Function SaveSequence(seq As Domain.Entities.TreasurySequence) As Domain.Base.Entities.ActionResult Implements ITreasuryService.SaveSequence
        Using service As ITreasurySequenseAdminService = Container.Current.Resolve(Of ITreasurySequenseAdminService)()
            Return service.SaveSequence(seq)
        End Using
        'Return Me._sequenseAdminService.SaveSequence(seq)
    End Function

End Class
