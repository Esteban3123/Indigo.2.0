'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Microsoft.Practices.Unity
Imports Application.FixedAsset

Partial Class FixedAssetService
    Implements IFixedAssetPartsAccesoriesConsumablesService

    ''' <summary>
    ''' Función para Eliminar PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumables">Objeto PartsAccesoriesConsumables</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    ''' 
    Public Function DeletePartsAccesoriesConsumables(PartsAccesoriesConsumables As Domain.Entities.FixedAssetPartsAccesoriesConsumables, audit As AuditMessage) As ActionResult Implements IFixedAssetPartsAccesoriesConsumablesService.DeletePartsAccesoriesConsumables
        Using service As IFixedAssetPartsAccesoriesConsumablesAdminService = Container.Current.Resolve(Of IFixedAssetPartsAccesoriesConsumablesAdminService)()
            Return service.DeletePartsAccesoriesConsumables(PartsAccesoriesConsumables, audit)
        End Using
        'Return _fixedAssetPartsAccesoriesConsumablesAdminService.DeletePartsAccesoriesConsumables(PartsAccesoriesConsumables, audit)
    End Function
    'Public Function DeletePartsAccesoriesConsumables(PartsAccesoriesConsumables As Domain.Entities.FixedAssetPartsAccesoriesConsumables, session As Infrastructure.CrossCutting.Base.SessionValues) As Boolean Implements IFixedAssetPartsAccesoriesConsumablesService.DeletePartsAccesoriesConsumables
    '    'Dim PartsAccesoriesConsumablesAdmin As IPartsAccesoriesConsumablesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPartsAccesoriesConsumablesAdminService)()
    '    Return Me._fixedAssetPartsAccesoriesConsumablesAdminService.DeletePartsAccesoriesConsumables(PartsAccesoriesConsumables, session.AuditMessageWcf)
    'End Function

    ''' <summary>
    ''' Función que obtiene PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="Code">Código de PartsAccesoriesConsumables</param>
    ''' <returns>PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Public Function GetPartsAccesoriesConsumablesByCode(Code As String, session As Infrastructure.CrossCutting.Base.SessionValues) As FixedAssetPartsAccesoriesConsumables Implements IFixedAssetPartsAccesoriesConsumablesService.GetPartsAccesoriesConsumablesByCode
        Using service As IFixedAssetPartsAccesoriesConsumablesAdminService = Container.Current.Resolve(Of IFixedAssetPartsAccesoriesConsumablesAdminService)()
            Return service.GetPartsAccesoriesConsumablesByCode(Code)
        End Using
        'Dim PartsAccesoriesConsumablesAdmin As IPartsAccesoriesConsumablesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPartsAccesoriesConsumablesAdminService)()
        'Return _fixedAssetPartsAccesoriesConsumablesAdminService.GetPartsAccesoriesConsumablesByCode(Code)
    End Function

    ''' <summary>
    ''' Función que obtiene PartsAccesoriesConsumables
    ''' </summary>
    ''' <returns>Lista de PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Public Function ListAllPartsAccesoriesConsumables(session As Infrastructure.CrossCutting.Base.SessionValues) As List(Of FixedAssetPartsAccesoriesConsumables) Implements IFixedAssetPartsAccesoriesConsumablesService.ListAllPartsAccesoriesConsumables
        Using service As IFixedAssetPartsAccesoriesConsumablesAdminService = Container.Current.Resolve(Of IFixedAssetPartsAccesoriesConsumablesAdminService)()
            Return service.ListAllPartsAccesoriesConsumables()
        End Using
        'Return _fixedAssetPartsAccesoriesConsumablesAdminService.ListAllPartsAccesoriesConsumables()
    End Function


    ''' <summary>
    ''' Función para Almacenar PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumables">Objeto PartsAccesoriesConsumables</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    ''' 
    Public Function SavePartsAccesoriesConsumables(PartsAccesoriesConsumables As FixedAssetPartsAccesoriesConsumables, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetPartsAccesoriesConsumables) Implements IFixedAssetPartsAccesoriesConsumablesService.SavePartsAccesoriesConsumables
        Using service As IFixedAssetPartsAccesoriesConsumablesAdminService = Container.Current.Resolve(Of IFixedAssetPartsAccesoriesConsumablesAdminService)()
            Return service.SavePartsAccesoriesConsumables(PartsAccesoriesConsumables, audit, idSequense)
        End Using
        'Return _fixedAssetPartsAccesoriesConsumablesAdminService.SavePartsAccesoriesConsumables(PartsAccesoriesConsumables, audit, idSequense)
    End Function
    'Public Function SavePartsAccesoriesConsumables(Empresa As String, PartsAccesoriesConsumables As FixedAssetPartsAccesoriesConsumables, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IFixedAssetPartsAccesoriesConsumablesService.SavePartsAccesoriesConsumables
    '    ' Dim PartsAccesoriesConsumablesAdmin As IPartsAccesoriesConsumablesAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPartsAccesoriesConsumablesAdminService)()
    '    Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
    '    Return _fixedAssetPartsAccesoriesConsumablesAdminService.SavePartsAccesoriesConsumables(PartsAccesoriesConsumables, audit, idSequense)
    'End Function

    ''' <summary>
    ''' Función que obtiene PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="Code">Código de PartsAccesoriesConsumables</param>
    ''' <returns>PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Public Function GetPartsAccesoriesConsumablesByEquipmentType(IdEquipmentType As Integer) As List(Of FixedAssetItemTypePartsAccesories) Implements IFixedAssetPartsAccesoriesConsumablesService.GetPartsAccesoriesConsumablesByEquipmentType
        Using service As IFixedAssetPartsAccesoriesConsumablesAdminService = Container.Current.Resolve(Of IFixedAssetPartsAccesoriesConsumablesAdminService)()
            Return service.GetPartsAccesoriesConsumablesByEquipmentType(IdEquipmentType)
        End Using
        'Return _fixedAssetPartsAccesoriesConsumablesAdminService.GetPartsAccesoriesConsumablesByEquipmentType(IdEquipmentType)
    End Function


    Function ChangeFixedAssetPartsAccesoriesConsumablesState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetPartsAccesoriesConsumables) Implements IFixedAssetPartsAccesoriesConsumablesService.ChangeFixedAssetPartsAccesoriesConsumablesState
        Using service As IFixedAssetPartsAccesoriesConsumablesAdminService = Container.Current.Resolve(Of IFixedAssetPartsAccesoriesConsumablesAdminService)()
            Return service.ChangeFixedAssetPartsAccesoriesConsumablesState(code, state, audit)
        End Using
        'Return _fixedAssetPartsAccesoriesConsumablesAdminService.ChangeFixedAssetPartsAccesoriesConsumablesState(code, state, audit)
    End Function
End Class
