'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Giovanny Plazas Lozano
' Created          : 1-09-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.Xpo
#End Region

Public Interface IDilutionFactors
    Inherits ICrudBase
#Region "Properties"
    ''' <summary>
    ''' Establece u obtiene el codigo del formulario
    ''' </summary>
    ''' <returns></returns>
    Property Code As String

    ''' <summary>
    ''' Id del atc
    ''' </summary>
    ''' <returns></returns>
    Property ATCId As Integer?

    ''' <summary>
    ''' Unidad de medida
    ''' </summary>
    ''' <returns></returns>
    Property MeasurementUnitId As Integer?

    ''' <summary>
    ''' forma farmaceutica
    ''' </summary>
    WriteOnly Property PharmaceuticalFormName As String

    ''' <summary>
    ''' id de la forma farmaceutica
    ''' </summary>
    ''' <returns></returns>
    Property PharmaceuticalFormId As Integer?

    ''' <summary>
    ''' presentacion del medicamento
    ''' </summary>
    Property PresentationMed As String

    ''' <summary>
    ''' Peso estándar concatenado con la unidad de medida
    ''' </summary>
    Property WeightStandarTxt As String

    ''' <summary>
    ''' Peso estándar
    ''' </summary>
    ''' <returns></returns>
    Property WeightStandar As Decimal

    ''' <summary>
    ''' peso del medicamento
    ''' </summary>
    ''' <returns></returns>
    Property WeightATC As Decimal

    ''' <summary>
    ''' abreviacion de la unidad de medida del medicamento
    ''' </summary>
    ''' <returns></returns>
    Property WeightATCAbbrebiation As String

    ''' <summary>
    ''' entidad factores de dilucion
    ''' </summary>
    ''' <returns></returns>
    Property _dilutionFactors As DilutionFactors

    ''' <summary>
    ''' Propiedad que contiene la configuración de secuencia asignada al formulario
    ''' </summary>
    Property Sequence As MixingStationSequence

    ''' <summary>
    ''' Datsource tabla ATC
    ''' </summary>
    ''' <returns></returns>
    Property ATCDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' lista  detalle de factores de dilucion
    ''' </summary>
    ''' <returns></returns>
    Property ListDilutionFactorsDetails As List(Of DilutionFactorsDetail)

    ''' <summary>
    ''' obtiene el objeto inicial para precargar la unidad de medida miligramos
    ''' </summary>
    Property MeasurementUnit As Infrastructure.Data.Xpo.InventoryRepository.MeasureUnitXpo

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    Sub ActionsOnControls(Action As Boolean, Optional _flagLoadControl As Boolean = False)

#End Region
End Interface
