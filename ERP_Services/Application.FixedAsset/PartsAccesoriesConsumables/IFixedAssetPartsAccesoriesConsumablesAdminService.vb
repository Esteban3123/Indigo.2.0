'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

Public Interface IFixedAssetPartsAccesoriesConsumablesAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Función que obtiene todos PartsAccesoriesConsumables
    ''' </summary>
    ''' <returns>Lista de PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Function ListAllPartsAccesoriesConsumables() As List(Of FixedAssetPartsAccesoriesConsumables)

    ''' <summary>
    ''' Función que obtiene PartsAccesoriesConsumables por Código
    ''' </summary>
    ''' <param name="Code">Código de PartsAccesoriesConsumables</param>
    ''' <returns>PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Function GetPartsAccesoriesConsumablesByCode(Code As String) As FixedAssetPartsAccesoriesConsumables

    ''' <summary>
    ''' Función para Almacenar PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumables">Objeto PartsAccesoriesConsumables</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SavePartsAccesoriesConsumables(PartsAccesoriesConsumables As FixedAssetPartsAccesoriesConsumables, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetPartsAccesoriesConsumables)

    ''' <summary>
    ''' Función para Eliminar PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumables">Objeto PartsAccesoriesConsumables</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeletePartsAccesoriesConsumables(PartsAccesoriesConsumables As FixedAssetPartsAccesoriesConsumables, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    Function GetPartsAccesoriesConsumablesByEquipmentType(IdEquipmentType As Integer) As List(Of FixedAssetItemTypePartsAccesories)

    ''' <summary>
    ''' Cambiar el Estado del Estado
    ''' </summary>
    ''' <param name="code">Code</param>
    ''' <param name="state">State</param>
    ''' <param name="audit">Audit</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function ChangeFixedAssetPartsAccesoriesConsumablesState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetPartsAccesoriesConsumables)


End Interface
