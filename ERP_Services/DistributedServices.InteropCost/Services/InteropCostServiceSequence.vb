'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán lozano
' Created          : 09-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.InteropCost
Imports Microsoft.Practices.Unity

Partial Class InteropCostService
    Implements IInteropCostServiceSequence

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>
    ''' Grupo de secuencias numericas
    ''' </returns>
    Public Function GetNumericSequenseGroupById(id As Integer) As List(Of String) Implements IInteropCostServiceSequence.GetNumericSequenseGroupById
        Using service As IInteropCostSequenceAdminService = Container.Current.Resolve(Of IInteropCostSequenceAdminService)()
            Return service.GetNumericSequenseGroupById(id)
        End Using
        'Return _sequenceAdminService.GetNumericSequenseGroupById(id)
    End Function

    ''' <summary>
    ''' Obtiene la cabecera de la secuencia por id del frontal
    ''' </summary>
    ''' <param name="idForm"></param>
    ''' <returns></returns>
    Public Function GetSequenseByIdForm(idForm As String) As InteropCostSecuence Implements IInteropCostServiceSequence.GetSequenseByIdForm
        Using service As IInteropCostSequenceAdminService = Container.Current.Resolve(Of IInteropCostSequenceAdminService)()
            Return service.GetSequenseByIdForm(idForm)
        End Using
        'Return _sequenceAdminService.GetSequenseByIdForm(idForm)
    End Function

    Public Function SaveSequence(seq As Domain.Entities.InteropCostSecuence) As Domain.Base.Entities.ActionResult Implements IInteropCostServiceSequence.SaveSequence
        Using service As IInteropCostSequenceAdminService = Container.Current.Resolve(Of IInteropCostSequenceAdminService)()
            Return service.SaveSequence(seq)
        End Using
        'Return _sequenceAdminService.SaveSequence(seq)
    End Function

End Class