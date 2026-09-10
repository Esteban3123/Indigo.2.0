'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/10/2014
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

    Public Function DeleteDefinitionRateDetail(DefinitionRateDetail As Domain.Entities.DefinitionRateDetail, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractServiceDefinitionRateDetail.DeleteDefinitionRateDetail
        Using service As IDefinitionRateDetailAdminService = Container.Current.Resolve(Of IDefinitionRateDetailAdminService)()
            Return service.DeleteDefinitionRateDetail(DefinitionRateDetail, audit)
        End Using
    End Function

    Public Function GetDefinitionRateDetailById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DefinitionRateDetail) Implements IContractServiceDefinitionRateDetail.GetDefinitionRateDetailById
        Using service As IDefinitionRateDetailAdminService = Container.Current.Resolve(Of IDefinitionRateDetailAdminService)()
            Return service.GetDefinitionRateDetailById(id, audit)
        End Using
    End Function

    Public Function SaveDefinitionRateDetail(DefinitionRateDetail As Domain.Entities.DefinitionRateDetail, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DefinitionRateDetail) Implements IContractServiceDefinitionRateDetail.SaveDefinitionRateDetail
        Using service As IDefinitionRateDetailAdminService = Container.Current.Resolve(Of IDefinitionRateDetailAdminService)()
            Return service.SaveDefinitionRateDetail(DefinitionRateDetail, audit)
        End Using
    End Function

    Public Function GetListDefinitionRateDetailConditionByDefinitionRateDetailId(definitionRateDetailId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.DefinitionRateDetailCondition)) Implements IContractServiceDefinitionRateDetail.GetListDefinitionRateDetailConditionByDefinitionRateDetailId
        Using service As IDefinitionRateDetailAdminService = Container.Current.Resolve(Of IDefinitionRateDetailAdminService)()
            Return service.GetListDefinitionRateDetailConditionByDefinitionRateDetailId(definitionRateDetailId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Servicio que exporta la estructura limpia del detalle de definicion de tarifas
    ''' </summary>
    ''' <returns></returns>
    Public Function ExportCleanStructure() As ActionResult(Of Byte()) Implements IContractServiceDefinitionRateDetail.ExportCleanStructure
        Using service As IDefinitionRateDetailAdminService = Container.Current.Resolve(Of IDefinitionRateDetailAdminService)()
            Return service.ExportCleanStructure()
        End Using
    End Function

    ''' <summary>
    ''' importa datos de una estructura excel y retorna la lista de la entidad DefinitionRateDetail
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ImportDataToAdd(dataImportFile As List(Of ImportFileRow), audit As AuditMessage) As ActionResult(Of List(Of DefinitionRateDetail)) Implements IContractServiceDefinitionRateDetail.ImportDataToAdd
        Using service As IDefinitionRateDetailAdminService = Container.Current.Resolve(Of IDefinitionRateDetailAdminService)()
            Return service.ImportDataToAdd(dataImportFile, audit)
        End Using
    End Function

    ''' <summary>
    ''' servicio para exportar la estructura de DefinitionRateDetailCondition por codigo de la definicion de tarifas
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function ExportConditionStructureByCode(code As String) As ActionResult(Of Byte()) Implements IContractServiceDefinitionRateDetail.ExportConditionStructureByCode
        Using service As IDefinitionRateDetailAdminService = Container.Current.Resolve(Of IDefinitionRateDetailAdminService)()
            Return service.ExportConditionStructureByCode(code)
        End Using
    End Function

    ''' <summary>
    ''' servicio para exportar la estructura de DefinitionRateDetailCondition por Id de la definicion de tarifas
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function ExportConditionStructureById(id As Integer) As ActionResult(Of Byte()) Implements IContractServiceDefinitionRateDetail.ExportConditionStructureById
        Using service As IDefinitionRateDetailAdminService = Container.Current.Resolve(Of IDefinitionRateDetailAdminService)()
            Return service.ExportConditionStructureById(id)
        End Using
    End Function

    ''' <summary>
    ''' Servicio que importa las condiciones de las reglas de definicion de tarifas
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ImportConditionDataToAdd(dataImportFile As List(Of ImportFileRow), audit As AuditMessage) As ActionResult(Of List(Of DefinitionRateDetailCondition)) Implements IContractServiceDefinitionRateDetail.ImportConditionDataToAdd
        Using service As IDefinitionRateDetailAdminService = Container.Current.Resolve(Of IDefinitionRateDetailAdminService)()
            Return service.ImportConditionDataToAdd(dataImportFile, audit)
        End Using
    End Function
End Class
