#Region "Imports"

Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Class AccountingService

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Public Function GetSequenseByIdForm(idForm As String) As Domain.Entities.GeneralLedgerSequence Implements IAccountingSequense.GetSequenseByIdForm
        Using service As IAccountingSequenseAdminService = Container.Current.Resolve(Of IAccountingSequenseAdminService)()
            Return service.GetSequenseByIdForm(idForm)
        End Using
        'Return Me._sequenseAdminService.GetSequenseByIdForm(idForm)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Function GetNumericSequenseGroupById(id As Integer) As List(Of String) Implements IAccountingSequense.GetNumericSequenseGroupById
        Using service As IAccountingSequenseAdminService = Container.Current.Resolve(Of IAccountingSequenseAdminService)()
            Return service.GetNumericSequenseGroupById(id)
        End Using
        'Return _sequenseAdminService.GetNumericSequenseGroupById(id)
    End Function

    Public Function SaveSequence(seq As Domain.Entities.GeneralLedgerSequence) As Domain.Base.Entities.ActionResult Implements IAccountingService.SaveSequence
        Using service As IAccountingSequenseAdminService = Container.Current.Resolve(Of IAccountingSequenseAdminService)()
            Return service.SaveSequence(seq)
        End Using
        'Return Me._sequenseAdminService.SaveSequence(seq)
    End Function

End Class
