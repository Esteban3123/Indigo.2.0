'***********************************************************************
' Assembly         : DistributedServices.FixedAsset
' Author           : Daniel Eduardo Arévalo
' Created          : 04-01-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface IFixedAssetVinculationTypeService

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de FixedAssetVinculationType</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllVinculationType(audit As AuditMessage) As List(Of Domain.Entities.FixedAssetVinculationType)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>FixedAssetVinculationType</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetVinculationTypeByCode(Code As String) As FixedAssetVinculationType

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="VinculationType">Objeto FixedAssetVinculationType</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveVinculationType(VinculationType As Domain.Entities.FixedAssetVinculationType, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetVinculationType)

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="VinculationType">Objeto VinculationType</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteVinculationType(VinculationType As Domain.Entities.FixedAssetVinculationType, audit As AuditMessage) As ActionResult


    <OperationContract()>
    Function ChangeFixedAssetVinculationTypeState(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetVinculationType)

End Interface
