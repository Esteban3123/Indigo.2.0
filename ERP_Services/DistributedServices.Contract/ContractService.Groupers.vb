'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 11/07/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.Contract
Imports Microsoft.Practices.Unity

Partial Class ContractService

    ''' <summary>
    ''' Guarda o Actualiza un rango uvr
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function SaveGroupers(ByVal Groupers As Groupers, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Groupers) Implements IContractGroupers.SaveGroupers
        Using service As IGroupersAdminService = Container.Current.Resolve(Of IGroupersAdminService)()
            Return service.SaveGroupers(Groupers, audit, idSequense)
        End Using
        'Return Me._groupersAdminService.SaveGroupers(Groupers, audit, idSequense)
    End Function

    ''' <summary>
    ''' Elimina un rango uvr
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function DeleteGroupers(ByVal Groupers As Groupers, audit As AuditMessage) As ActionResult Implements IContractGroupers.DeleteGroupers
        Using service As IGroupersAdminService = Container.Current.Resolve(Of IGroupersAdminService)()
            Return service.DeleteGroupers(Groupers, audit)
        End Using
        'Return Me._groupersAdminService.DeleteGroupers(Groupers, audit)
    End Function

    ''' <summary>
    ''' Obtiene un rango uvr por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetGroupers(ByVal code As String, audit As AuditMessage) As ActionResult(Of Groupers) Implements IContractGroupers.GetGroupers
        Using service As IGroupersAdminService = Container.Current.Resolve(Of IGroupersAdminService)()
            Return service.GetGroupers(code, audit)
        End Using
        'Return Me._groupersAdminService.GetGroupers(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene un rango uvr por id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGroupersById(ByVal id As Integer, audit As AuditMessage) As ActionResult(Of Groupers) Implements IContractGroupers.GetGroupersById
        Using service As IGroupersAdminService = Container.Current.Resolve(Of IGroupersAdminService)()
            Return service.GetGroupersById(id, audit)
        End Using
        'Return Me._groupersAdminService.GetGroupersById(id, audit)
    End Function

    Public Function CopyAndPasteGroupersCups(data As List(Of List(Of String))) As ActionResult(Of List(Of GroupersCups)) Implements IContractGroupers.CopyAndPasteGroupersCups
        Using service As IGroupersAdminService = Container.Current.Resolve(Of IGroupersAdminService)()
            Return service.CopyAndPasteGroupersCups(data)
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateGroupers(ByVal code As String, ByVal state As Boolean, audit As AuditMessage) As ActionResult(Of Groupers) Implements IContractGroupers.ChangeStateGroupers
        Using service As IGroupersAdminService = Container.Current.Resolve(Of IGroupersAdminService)()
            Return service.ChangeStateGroupers(code, state, audit)
        End Using
        'Return Me._groupersAdminService.ChangeStateGroupers(code, state, audit)
    End Function

    ''' <summary>
    ''' Importa registros de los agrupadores
    ''' </summary>
    ''' <returns></returns>
    Public Function ImportGroupers(ByVal workSheetType As eGrouperWorkSheetType, data As List(Of ImportFileRow), ByVal audit As AuditMessage) As ActionResult(Of List(Of String)) Implements IContractGroupers.ImportGroupers
        Using service As IGroupersAdminService = Container.Current.Resolve(Of IGroupersAdminService)()
            Return service.ImportGroupers(workSheetType, data, audit)
        End Using
    End Function

End Class
