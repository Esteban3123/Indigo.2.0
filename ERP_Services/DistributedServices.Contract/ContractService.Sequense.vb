'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Contract
Imports Microsoft.Practices.Unity

Partial Class ContractService

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Function GetNumericSequenseGroupById(id As Integer) As List(Of String) Implements IContractSequense.GetNumericSequenseGroupById
        Using service As IContractSequenseAdminService = Container.Current.Resolve(Of IContractSequenseAdminService)()
            Return service.GetNumericSequenseGroupById(id)
        End Using
        'Return _sequenseAdminService.GetNumericSequenseGroupById(id)
    End Function

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Public Function GetSequenseByIdForm(idForm As String) As Domain.Entities.ContractSequence Implements IContractSequense.GetSequenseByIdForm
        Using service As IContractSequenseAdminService = Container.Current.Resolve(Of IContractSequenseAdminService)()
            Return service.GetSequenseByIdForm(idForm)
        End Using
        'Return Me._sequenseAdminService.GetSequenseByIdForm(idForm)
    End Function

    Public Function SaveSequence(seq As Domain.Entities.ContractSequence) As Domain.Base.Entities.ActionResult Implements IContractService.SaveSequence
        Using service As IContractSequenseAdminService = Container.Current.Resolve(Of IContractSequenseAdminService)()
            Return service.SaveSequence(seq)
        End Using
        'Return Me._sequenseAdminService.SaveSequence(seq)
    End Function

End Class
