'***********************************************************************
' Assembly         : Application.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 04-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

Public Interface IFixedAssetVinculationTypeAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Function ListAllVinculationType() As List(Of FixedAssetVinculationType)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetVinculationTypeByCode(Code As String) As FixedAssetVinculationType

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="FixedAssetVinculationType">Objeto FixedAssetVinculationType</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveVinculationType(FixedAssetVinculationType As FixedAssetVinculationType, audit As AuditMessage, Optional idSequense As Long = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetVinculationType)

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="FixedAssetVinculationType">Objeto FixedAssetVinculationType</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteVinculationType(FixedAssetVinculationType As FixedAssetVinculationType, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Cambiar el Estado del Estado de Activos Fijos
    ''' </summary>
    ''' <param name="code">Code</param>
    ''' <param name="state">State</param>
    ''' <param name="audit">Audit</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function ChangeFixedAssetVinculationTypeState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetVinculationType)

End Interface
