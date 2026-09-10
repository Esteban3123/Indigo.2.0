#Region "Imports"

Imports Application.Common
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC

#End Region

Partial Class CommonERPService

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Public Function GetSequenseByIdForm(idForm As String, session As SessionValues) As Domain.Entities.CommonSequence Implements ICommonSequense.GetSequenseByIdForm
        Using _sequenseAdminService As ICommonSequenseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICommonSequenseAdminService)()
            Return _sequenseAdminService.GetSequenseByIdForm(idForm)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Function GetNumericSequenseGroupById(id As Integer, session As SessionValues) As List(Of String) Implements ICommonSequense.GetNumericSequenseGroupById
        Using _sequenseAdminService As ICommonSequenseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICommonSequenseAdminService)()
            Return _sequenseAdminService.GetNumericSequenseGroupById(id)
        End Using
    End Function

    Public Function ListSequences(session As SessionValues) As List(Of Domain.Entities.Sequense) Implements ICommonSequense.ListSequences
        Using _sequenseAdminService As ICommonSequenseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICommonSequenseAdminService)()
            Return _sequenseAdminService.ListSequences()
        End Using
    End Function

    Public Function SaveSequence(seq As Domain.Entities.CommonSequence) As Domain.Base.Entities.ActionResult Implements ICommonERPService.SaveSequence
        Using _sequenseAdminService As ICommonSequenseAdminService = IocFactory.Instance(Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer).CurrentContainer.Resolve(Of ICommonSequenseAdminService)()
            Return _sequenseAdminService.SaveSequence(seq)
        End Using
    End Function

    Public Function SavePatternSequence(seq As Domain.Entities.Sequense, session As SessionValues) As Domain.Base.Entities.ActionResult Implements ICommonSequense.SavePatternSequence
        Using _sequenseAdminService As ICommonSequenseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICommonSequenseAdminService)()
            Return _sequenseAdminService.SavePatternSequence(seq)
        End Using
    End Function

    Public Function GetPatternSequence(idSeq As Integer, session As SessionValues) As Domain.Entities.Sequense Implements ICommonSequense.GetPatternSequence
        Using _sequenseAdminService As ICommonSequenseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICommonSequenseAdminService)()
            Return _sequenseAdminService.GetPatternSequence(idSeq)
        End Using
    End Function

    Public Function GetPatternSequenceByName(nameSeq As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Entities.Sequense Implements ICommonSequense.GetPatternSequenceByName
        Using _sequenseAdminService As ICommonSequenseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICommonSequenseAdminService)()
            Return _sequenseAdminService.GetPatternSequenceByName(nameSeq)
        End Using
    End Function

End Class
