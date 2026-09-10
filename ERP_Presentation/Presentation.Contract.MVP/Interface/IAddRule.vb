'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface IAddRule
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene o establece la regla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RuleType As Integer?

    ''' <summary>
    ''' Obtiene o establece el tipo de condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConditionType As Integer?

    ''' <summary>
    ''' Obtiene o establece el peso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Weight As Integer

    ''' <summary>
    ''' Obtiene o establece el operador lógico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LogicOperator As Integer?

    ''' <summary>
    ''' Obtiene o establece el segundo tipo de condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConditionTypeSecond As Integer?

    ''' <summary>
    ''' Obtiene o establece la hora inicial de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property StartTimeFirst As TimeSpan?

    ''' <summary>
    ''' Obtiene o establece la hora final de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EndTimeFirst As TimeSpan?

    ''' <summary>
    ''' Obtiene o establece la especialidad de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SpecialtyIdFirst As String

    ''' <summary>
    ''' Establece el datasource de la especialidad de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SpecialtyXpoFirst As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la unidad funcional de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FunctionalUnitIdFirst As Integer?

    ''' <summary>
    ''' Establece el datasource de la unidad funcional de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FunctionalUnitXpoFirst As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del tipo de unidad de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UnitTypeIdFirst As Integer?

    ''' <summary>
    ''' Obtiene o establece la hora inicial de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property StartTimeSecond As TimeSpan?

    ''' <summary>
    ''' Obtiene o establece la hora final de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EndTimeSecond As TimeSpan?

    ''' <summary>
    ''' Obtiene o establece la especialidad de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SpecialtyIdSecond As String

    ''' <summary>
    ''' Establece el datasource de la especialidad de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SpecialtyXpoSecond As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la unidad funcional de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FunctionalUnitIdSecond As Integer?

    ''' <summary>
    ''' Establece el datasource de la unidad funcional de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FunctionalUnitXpoSecond As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del tipo de unidad de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UnitTypeIdSecond As Integer?

    ''' <summary>
    ''' Obtiene o establece el tipo de liquidacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LiquidationTypePopup As Integer?

    ''' <summary>
    ''' Obtiene o establece el valor del servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesValuePopup As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor del recargo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesValueWithSurchargePopup As Decimal

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualValidityIdPopup As Integer?

    ''' <summary>
    ''' establece el datasource de la vigencia del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualValidityXpoPopup As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualIdPopup As Integer?

    ''' <summary>
    ''' establece el datasource del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualXpoPopup As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el porcentaje de la variacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateVariationPopup As Decimal

    ''' <summary>
    ''' Obtiene o establece el operador de la primera condición
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OperatorFirst As Integer

    ''' <summary>
    ''' Obtiene o establece el operador de la segunda condición
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OperatorSecond As Integer?

    ''' <summary>
    ''' Obtiene o establece el tipo de liquidacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LiquidationType As Byte?

    ''' <summary>
    ''' Obtiene o establece el valor del servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesValue As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor subtotal del servicio del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesSubtotal As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor del IVA del servicio 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesValueIVA As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor del recargo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesValueWithSurcharge As Decimal

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualValidityId As Integer?

    ''' <summary>
    ''' establece el datasource de la vigencia del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualValidityXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualId As Integer?

    ''' <summary>
    ''' establece el datasource del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el porcentaje de la variacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateVariation As Decimal?

    ''' <summary>
    ''' Obtiene o establece el tipo de manual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ManualType As Integer?

    ''' <summary>
    ''' Obtiene o establece el tipo de manual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ManualTypePopup As Integer?

    ''' <summary>
    ''' Obtiene o establece si permite cambiar valores
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AllowValueChange As Boolean

    ''' <summary>
    ''' Obtiene o establece la hora inicial de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property StartTimePopupFirstCondition As TimeSpan?

    ''' <summary>
    ''' Obtiene o establece la hora final de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EndTimePopupFirstCondition As TimeSpan?

    ''' <summary>
    ''' Obtiene o establece el id del tipo de unidad de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UnitTypeIdPopupFirstCondition As Integer?

    ''' <summary>
    ''' Obtiene o establece el tipo de liquidacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LiquidationTypePopupFirstCondition As Integer?

    ''' <summary>
    ''' Obtiene o establece el valor del servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesValuePopupFirstCondition As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor del recargo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesValueWithSurchargePopupFirstCondition As Decimal

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualValidityIdPopupFirstCondition As Integer?

    ''' <summary>
    ''' establece el datasource de la vigencia del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualValidityXpoPopupFirstCondition As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualIdPopupFirstCondition As Integer?

    ''' <summary>
    ''' establece el datasource del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateManualXpoPopupFirstCondition As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el porcentaje de la variacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RateVariationPopupFirstCondition As Decimal

    ''' <summary>
    ''' Obtiene o establece el tipo de manual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ManualTypePopupFirstCondition As Integer?

    ''' <summary>
    ''' Establece el datasource de la especialidad del popup con una condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SpecialtyPopupFirstConditionXpo As XPCollection

    ''' <summary>
    ''' Establece el datasource de la unidad funcional del popup con una condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FunctionalUnitPopupFirstConditionXpo As XPCollection

    ''' <summary>
    ''' Establece el datasource de los RIAS del popup con una condición
    ''' </summary>
    ''' <returns></returns>
    Property RIASPopupFirstContidionXpo As XPCollection

    ''' <summary>
    ''' Establece el datasource de las descripciones del popup con una condición
    ''' </summary>
    ''' <returns></returns>
    Property DescriptionPopupFirstConditionXpo As XPCollection

End Interface
