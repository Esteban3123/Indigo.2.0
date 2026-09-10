'************************************************************
' Assembly         : Domain.Entities
' Author           : Juan F. Tamayo
' Created          : 2014-12-18
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports System.Runtime.Serialization

#End Region

''' <summary>
''' Enumera los diferentes métodos disponibles como acciones
''' en el frontal de liquidación, módulo de facturación
''' </summary>
<DataContract()>
Public Enum LiquidationActionMethod

    ''' <summary>
    ''' Marcar servicio para calcular o no cuota de recuperación
    ''' </summary>
    <EnumMember()>
    SetRecoveryFeeOnService
    ''' <summary>
    ''' Liquidar cuota de recuperación
    ''' </summary>
    <EnumMember()>
    LiquidateRecoveryFee
    ''' <summary>
    ''' Bloquear folio
    ''' </summary>
    <EnumMember()>
    BlockFolio
    ''' <summary>
    ''' Desbloquear folio
    ''' </summary>
    <EnumMember()>
    UnblockFolio
    ''' <summary>
    ''' Cambia el precio de un servicio manualmete
    ''' </summary>
    <EnumMember()>
    ChangeServiceValue
    ' ''' <summary>
    ' ''' Distribuye los detalles de un folio en otro
    ' ''' </summary>
    '<EnumMember()>
    'DistributeFolio
    ''' <summary>
    ''' Obtiene los Ids de los folios de un ingreso
    ''' </summary>
    <EnumMember()>
    GetIdsFolios
    ''' <summary>
    ''' Elimina un folio vacio
    ''' </summary>
    <EnumMember()>
    DeleteEmptyFolio
    ''' <summary>
    ''' Retarifica los servicios de un folio
    ''' </summary>
    <EnumMember()>
    ChangeRateServices
    ''' <summary>
    ''' Agrega un nuevo folio
    ''' </summary>
    <EnumMember()>
    AddNewFolio
    ''' <summary>
    ''' Actualiza el valor unitario
    ''' </summary>
    <EnumMember()>
    UpdateUnitValue
    ''' <summary>
    ''' Liquida el folio actual
    ''' </summary>
    <EnumMember()>
    LiquidateFolio
    ''' <summary>
    ''' Aumenta al total del folio
    ''' </summary>
    <EnumMember()>
    AddTotalFolio
    ''' <summary>
    ''' Anula folios
    ''' </summary>
    <EnumMember()>
    AnulateFolio
    ''' <summary>
    ''' Valida que el item se pueda distribuir y retarificar sin problemas
    ''' </summary>
    <EnumMember()>
    ValidateItemDistribution
    ''' <summary>
    ''' Empaqueta los items seleccionados
    ''' </summary>
    <EnumMember()>
    PackageItems
    ''' <summary>
    ''' Desempaqueta los items seleccionados
    ''' </summary>
    <EnumMember()>
    UnPackageItems

    ''' <summary>
    ''' Recalcula los valores del folio
    ''' </summary>
    <EnumMember()>
    RecalculateFolio
    ''' <summary>
    ''' Actualiza el tercero del folio
    ''' </summary>
    <EnumMember()>
    UpdateThirdPartyFolio
    ''' <summary>
    ''' Actualiza la entidad administradora del folio
    ''' </summary>
    <EnumMember()>
    UpdateHealthAdministratorFolio
    ''' <summary>
    ''' Actualiza el numero de autorización
    ''' </summary>
    <EnumMember()>
    UpdateAuthorizationNumber
    ''' <summary>
    ''' Incluir en otro servicio
    ''' </summary>
    <EnumMember()>
    IncludeInOtherService
    ''' <summary>
    ''' Excluir del servicio
    ''' </summary>
    <EnumMember()>
    ExcludeOutService
    <EnumMember()> _
    UpdateNoPos
    <EnumMember()> _
    UpdateDescriptionFolio
    <EnumMember()> _
    UpdateCategoryFolio
    <EnumMember()> _
    UpdateObservationFolio
    <EnumMember()> _
    SavePatientBonus
    <EnumMember()> _
    RemovePatientBonus
    <EnumMember()>
    RemoveNoPOSLiquidation

    ''' <summary>
    ''' Actualiza el concepto del estado del folio
    ''' </summary>
    <EnumMember()>
    UpdateStatusFolio
    <EnumMember>
    SavePatientQuotaResponsible
    ''' <summary>
    ''' Crea el registro de Sin recaudo de cuota
    ''' </summary>
    <EnumMember>
    CreateFeeNotCollected
    ''' <summary>
    ''' elimina el registro de Sin recaudo de cuota
    ''' </summary>
    <EnumMember>
    DeleteFeeNotCollected
End Enum

<DataContract()>
Public Enum eLiquidateRecoveryType
    ''' <summary>
    ''' Liquidar cuota a paciente de todos los detalles de un folio
    ''' </summary>
    <EnumMember()>
    AllItems
    ''' <summary>
    ''' Solo liquida la cuota a Paciente a un item de un folio
    ''' </summary>
    <EnumMember()>
    OnlyOne
    ''' <summary>
    ''' Liquida la cuota a paciente a varios items seleccionados
    ''' </summary>
    <EnumMember()>
    MultiSelectItems
End Enum

<DataContract()> _
Public Enum eLiquidateStayOption
    <EnumMember()> _
    MayorValor
    <EnumMember()> _
    CamaUltimoIngreso
    <EnumMember()> _
    DefectoManualGrupoAtencion
End Enum