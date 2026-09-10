'***********************************************************************
' Assembly         : Presentacion.Contract
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

Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports System.Text
Imports Presentation.Contract.MVP
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Payroll
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Presentation.Accounting.MVP

#End Region

Public Class FrmAddRule
    Implements IAddRule

#Region "Builder"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
    End Sub

#End Region

#Region "PublicEvents"

    ''' <summary>
    ''' Evento publico para agregar una regla a
    ''' la rejilla del form principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddInfoToGridFormPrincipal(sender As Object, e As AddInfoToGridFormPrincipal)

#End Region

#Region "Properties"
    Private _companySettings As CompanySettings
    ''' <summary>
    ''' Establece el datasource de las descripciones del popup de la primera condición
    ''' </summary>
    ''' <returns></returns>
    Public Property DescriptionPopupFirstConditionXpo As XPCollection Implements IAddRule.DescriptionPopupFirstConditionXpo
        Get
            Return INDgcDescriptions.DataSource
        End Get
        Set(value As XPCollection)
            INDgcDescriptions.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los RIAS del popup de la primera condición
    ''' </summary>
    ''' <returns></returns>
    Public Property RIASPopupFirstContidionXpo As XPCollection Implements IAddRule.RIASPopupFirstContidionXpo
        Get
            Return INDgcRIAS.DataSource
        End Get
        Set(value As XPCollection)
            INDgcRIAS.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la unidad funcional del popup con una condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FunctionalUnitPopupFirstConditionXpo As XPCollection Implements IAddRule.FunctionalUnitPopupFirstConditionXpo
        Get
            Return INDgcFunctionalUnitPopupFirstCondition.DataSource
        End Get
        Set(value As XPCollection)
            INDgcFunctionalUnitPopupFirstCondition.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la especialidad del popup con una condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SpecialtyPopupFirstConditionXpo As XPCollection Implements IAddRule.SpecialtyPopupFirstConditionXpo
        Get
            Return INDgcSpecialtyPopupFirstCondition.DataSource
        End Get
        Set(value As XPCollection)
            INDgcSpecialtyPopupFirstCondition.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la hora final del popup con la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EndTimePopupFirstCondition As TimeSpan? Implements IAddRule.EndTimePopupFirstCondition
        Get
            If modeEditGridRates Then
                Return INDdteEndTimePopupFirstCondition.EditValue
            Else
                Return TimeSpan.Parse(CDate(INDdteEndTimePopupFirstCondition.EditValue).ToString("HH:mm"))
            End If
        End Get
        Set(value As TimeSpan?)
            INDdteEndTimePopupFirstCondition.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de liquidacion del popup con la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LiquidationTypePopupFirstCondition As Integer? Implements IAddRule.LiquidationTypePopupFirstCondition
        Get
            Return INDsleLiquidationTypePopupFirstCondition.EditValue
        End Get
        Set(value As Integer?)
            INDsleLiquidationTypePopupFirstCondition.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de manual del popup con la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ManualTypePopupFirstCondition As Integer? Implements IAddRule.ManualTypePopupFirstCondition
        Get
            Return INDsleManualTypePopupFirstCondition.EditValue
        End Get
        Set(value As Integer?)
            INDsleManualTypePopupFirstCondition.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia del manual tarifario del popup con la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualValidityIdPopupFirstCondition As Integer? Implements IAddRule.RateManualValidityIdPopupFirstCondition
        Get
            Return INDsleRateManualValidityPopupFirstCondition.EditValue
        End Get
        Set(value As Integer?)
            INDsleRateManualValidityPopupFirstCondition.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la vigencia del manual tarifario del popup con la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualValidityXpoPopupFirstCondition As XPInstantFeedbackSource Implements IAddRule.RateManualValidityXpoPopupFirstCondition
        Get
            Return INDsleRateManualValidityPopupFirstCondition.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleRateManualValidityPopupFirstCondition.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del manual tarifario del popup con la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualIdPopupFirstCondition As Integer? Implements IAddRule.RateManualIdPopupFirstCondition
        Get
            Return INDsleRateManualPopupFirstCondition.EditValue
        End Get
        Set(value As Integer?)
            INDsleRateManualPopupFirstCondition.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del manual tarifario del popup con la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualXpoPopupFirstCondition As XPInstantFeedbackSource Implements IAddRule.RateManualXpoPopupFirstCondition
        Get
            Return INDsleRateManualPopupFirstCondition.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleRateManualPopupFirstCondition.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la variacion del popup con la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateVariationPopupFirstCondition As Decimal Implements IAddRule.RateVariationPopupFirstCondition
        Get
            Return INDseRateVariationPopupFirstCondition.EditValue
        End Get
        Set(value As Decimal)
            INDseRateVariationPopupFirstCondition.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del servicio del popup de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesValuePopupFirstCondition As Decimal Implements IAddRule.SalesValuePopupFirstCondition
        Get
            Return INDtxtSalesValuePopupFirstCondition.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSalesValuePopupFirstCondition.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del recargo del popup de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesValueWithSurchargePopupFirstCondition As Decimal Implements IAddRule.SalesValueWithSurchargePopupFirstCondition
        Get
            Return INDtxtSalesValueWithSurchargePopupFirstCondition.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSalesValueWithSurchargePopupFirstCondition.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha inicial del popup con la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StartTimePopupFirstCondition As TimeSpan? Implements IAddRule.StartTimePopupFirstCondition
        Get
            If modeEditGridRates Then
                Return INDdteStartTimePopupFirstCondition.EditValue
            Else
                Return TimeSpan.Parse(CDate(INDdteStartTimePopupFirstCondition.EditValue).ToString("HH:mm"))
            End If
        End Get
        Set(value As TimeSpan?)
            INDdteStartTimePopupFirstCondition.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de unidad del popup con la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UnitTypeIdPopupFirstCondition As Integer? Implements IAddRule.UnitTypeIdPopupFirstCondition
        Get
            Return INDsleUnitTypePopupFirstCondition.EditValue
        End Get
        Set(value As Integer?)
            INDsleUnitTypePopupFirstCondition.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si permite cambiar valores
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllowValueChange As Boolean Implements IAddRule.AllowValueChange
        Get
            Return INDsleAllowValueChange.EditValue
        End Get
        Set(value As Boolean)
            INDsleAllowValueChange.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de manual del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ManualType As Integer? Implements IAddRule.ManualType
        Get
            Return INDsleManualType.EditValue
        End Get
        Set(value As Integer?)
            INDsleManualType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo manual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ManualTypePopup As Integer? Implements IAddRule.ManualTypePopup
        Get
            Return INDsleManualTypePopup.EditValue
        End Get
        Set(value As Integer?)
            INDsleManualTypePopup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el segundo tipo de condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConditionTypeSecond As Integer? Implements IAddRule.ConditionTypeSecond
        Get
            Return INDsleConditionTypeSecond.EditValue
        End Get
        Set(value As Integer?)
            INDsleConditionTypeSecond.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el operador logico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LogicOperator As Integer? Implements IAddRule.LogicOperator
        Get
            Return INDsleLogicOperator.EditValue
        End Get
        Set(value As Integer?)
            INDsleLogicOperator.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de regla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RuleType As Integer? Implements IAddRule.RuleType
        Get
            Return INDsleRuleType.EditValue
        End Get
        Set(value As Integer?)
            INDsleRuleType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConditionType As Integer? Implements IAddRule.ConditionType
        Get
            Return INDsleConditionType.EditValue
        End Get
        Set(value As Integer?)
            INDsleConditionType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el peso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Weight As Integer Implements IAddRule.Weight
        Get
            Return INDseWeight.EditValue
        End Get
        Set(value As Integer)
            INDseWeight.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica si se esta guardando o editando
    ''' </summary>
    ''' <remarks></remarks>
    Private _modeEdit As Boolean
    Public Property ModeEdit As Boolean
        Get
            Return _modeEdit
        End Get
        Set(value As Boolean)
            _modeEdit = value
        End Set
    End Property

    ''' <summary>
    ''' Representa al detalle de la definición de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Private _definitionRateDetail As DefinitionRateDetail
    Public Property DefinitionRateDetail As DefinitionRateDetail
        Get
            Return _definitionRateDetail
        End Get
        Set(value As DefinitionRateDetail)
            _definitionRateDetail = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la hora final de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EndTimeFirst As TimeSpan? Implements IAddRule.EndTimeFirst
        Get
            If modeEditGridRates Then
                Return INDdteEndTimeFirst.EditValue
            Else
                Return TimeSpan.Parse(CDate(INDdteEndTimeFirst.EditValue).ToString("HH:mm"))
            End If
        End Get
        Set(value As TimeSpan?)
            INDdteEndTimeFirst.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad funcional de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FunctionalUnitIdFirst As Integer? Implements IAddRule.FunctionalUnitIdFirst
        Get
            Return INDsleFunctionalUnitFirst.EditValue
        End Get
        Set(value As Integer?)
            INDsleFunctionalUnitFirst.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la unidad funcional de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FunctionalUnitXpoFirst As XPInstantFeedbackSource Implements IAddRule.FunctionalUnitXpoFirst
        Get
            Return INDsleFunctionalUnitFirst.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleFunctionalUnitFirst.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la especialidad de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SpecialtyIdFirst As String Implements IAddRule.SpecialtyIdFirst
        Get
            Return INDsleSpecialtyFirst.EditValue
        End Get
        Set(value As String)
            INDsleSpecialtyFirst.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la especialidad de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SpecialtyXpoFirst As XPInstantFeedbackSource Implements IAddRule.SpecialtyXpoFirst
        Get
            Return INDsleSpecialtyFirst.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleSpecialtyFirst.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la hora inicial de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StartTimeFirst As TimeSpan? Implements IAddRule.StartTimeFirst
        Get
            If modeEditGridRates Then
                Return INDdteStartTimeFirst.EditValue
            Else
                Return TimeSpan.Parse(CDate(INDdteStartTimeFirst.EditValue).ToString("HH:mm"))
            End If
        End Get
        Set(value As TimeSpan?)
            INDdteStartTimeFirst.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de unidad de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UnitTypeIdFirst As Integer? Implements IAddRule.UnitTypeIdFirst
        Get
            Return INDsleUnitTypeFirst.EditValue
        End Get
        Set(value As Integer?)
            INDsleUnitTypeFirst.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la hora final de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EndTimeSecond As TimeSpan? Implements IAddRule.EndTimeSecond
        Get
            If modeEditGridRates Then
                Return INDdteEndTimeSecond.EditValue
            Else
                Return TimeSpan.Parse(CDate(INDdteEndTimeSecond.EditValue).ToString("HH:mm"))
            End If
        End Get
        Set(value As TimeSpan?)
            INDdteEndTimeSecond.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad funcional de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FunctionalUnitIdSecond As Integer? Implements IAddRule.FunctionalUnitIdSecond
        Get
            Return INDsleFunctionalUnitSecond.EditValue
        End Get
        Set(value As Integer?)
            INDsleFunctionalUnitSecond.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la unidad funcional de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FunctionalUnitXpoSecond As XPInstantFeedbackSource Implements IAddRule.FunctionalUnitXpoSecond
        Get
            Return INDsleFunctionalUnitSecond.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleFunctionalUnitSecond.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la especialidad de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SpecialtyIdSecond As String Implements IAddRule.SpecialtyIdSecond
        Get
            Return INDsleSpecialtySecond.EditValue
        End Get
        Set(value As String)
            INDsleSpecialtySecond.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la especialidad de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SpecialtyXpoSecond As XPInstantFeedbackSource Implements IAddRule.SpecialtyXpoSecond
        Get
            Return INDsleSpecialtySecond.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleSpecialtySecond.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la hora inicial de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StartTimeSecond As TimeSpan? Implements IAddRule.StartTimeSecond
        Get
            If modeEditGridRates Then
                Return INDdteStartTimeSecond.EditValue
            Else
                Return TimeSpan.Parse(CDate(INDdteStartTimeSecond.EditValue).ToString("HH:mm"))
            End If
        End Get
        Set(value As TimeSpan?)
            INDdteStartTimeSecond.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de unidad de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UnitTypeIdSecond As Integer? Implements IAddRule.UnitTypeIdSecond
        Get
            Return INDsleUnitTypeSecond.EditValue
        End Get
        Set(value As Integer?)
            INDsleUnitTypeSecond.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de liquidación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LiquidationTypePopup As Integer? Implements IAddRule.LiquidationTypePopup
        Get
            Return INDsleLiquidationTypePopup.EditValue
        End Get
        Set(value As Integer?)
            INDsleLiquidationTypePopup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualValidityIdPopup As Integer? Implements IAddRule.RateManualValidityIdPopup
        Get
            Return INDsleRateManualValidityPopup.EditValue
        End Get
        Set(value As Integer?)
            INDsleRateManualValidityPopup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la vigencia del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualValidityXpoPopup As XPInstantFeedbackSource Implements IAddRule.RateManualValidityXpoPopup
        Get
            Return INDsleRateManualValidityPopup.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleRateManualValidityPopup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualIdPopup As Integer? Implements IAddRule.RateManualIdPopup
        Get
            Return INDsleRateManualPopup.EditValue
        End Get
        Set(value As Integer?)
            INDsleRateManualPopup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualXpoPopup As XPInstantFeedbackSource Implements IAddRule.RateManualXpoPopup
        Get
            Return INDsleRateManualPopup.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleRateManualPopup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la variacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateVariationPopup As Decimal Implements IAddRule.RateVariationPopup
        Get
            Return INDseRateVariationPopup.EditValue
        End Get
        Set(value As Decimal)
            INDseRateVariationPopup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesValuePopup As Decimal Implements IAddRule.SalesValuePopup
        Get
            Return INDtxtSalesValuePopup.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSalesValuePopup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del recargo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesValueWithSurchargePopup As Decimal Implements IAddRule.SalesValueWithSurchargePopup
        Get
            Return INDtxtSalesValueWithSurchargePopup.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSalesValueWithSurchargePopup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el operador de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OperatorFirst As Integer Implements IAddRule.OperatorFirst
        Get
            Return INDsleOperatorFirst.EditValue
        End Get
        Set(value As Integer)
            INDsleOperatorFirst.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el operador de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OperatorSecond As Integer? Implements IAddRule.OperatorSecond
        Get
            Return INDsleOperatorSecond.EditValue
        End Get
        Set(value As Integer?)
            INDsleOperatorSecond.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de liquidacion del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LiquidationType As Byte? Implements IAddRule.LiquidationType
        Get
            Return CType(INDsleLiquidationType.EditValue, Byte)
        End Get
        Set(value As Byte?)
            INDsleLiquidationType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia del manual tarifario del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualValidityId As Integer? Implements IAddRule.RateManualValidityId
        Get
            Return INDSleRateManualValidity.EditValue
        End Get
        Set(value As Integer?)
            INDSleRateManualValidity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la vigencia del manual tarifario del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualValidityXpo As XPInstantFeedbackSource Implements IAddRule.RateManualValidityXpo
        Get
            Return INDSleRateManualValidity.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleRateManualValidity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del manual tarifario del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualId As Integer? Implements IAddRule.RateManualId
        Get
            Return INDsleRateManual.EditValue
        End Get
        Set(value As Integer?)
            INDsleRateManual.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del manual tarifario del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualXpo As XPInstantFeedbackSource Implements IAddRule.RateManualXpo
        Get
            Return INDsleRateManual.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleRateManual.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la variacion del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateVariation As Decimal? Implements IAddRule.RateVariation
        Get
            Return INDseRateVariation.EditValue
        End Get
        Set(value As Decimal?)
            INDseRateVariation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor subtotal del servicio del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesSubtotal As Decimal Implements IAddRule.SalesSubtotal
        Get
            Return INDtxtSalesSubtotal.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSalesSubtotal.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del IVA del servicio 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesValueIVA As Decimal Implements IAddRule.SalesValueIVA
        Get
            Return INDtxtSalesValueIVA.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSalesValueIVA.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del servicio del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesValue As Decimal Implements IAddRule.SalesValue
        Get
            Return INDtxtSalesValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSalesValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del recargo del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesValueWithSurcharge As Decimal Implements IAddRule.SalesValueWithSurcharge
        Get
            Return INDtxtSalesValueWithSurcharge.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSalesValueWithSurcharge.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Private ListManualType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Nombre de la vista de la rejilla de los controles
    ''' </summary>
    Private Const VIEW_CONTROLS As String = "viewControlsRuleType"

    ''' <summary>
    ''' Nombre de la vista de la rejilla de especialidad
    ''' </summary>
    Private Const VIEW_SPECIALTY As String = "viewGridSpecialty"

    ''' <summary>
    ''' Nombre de la vista de la rejilla de rias
    ''' </summary>
    Private Const VIEW_RIAS As String = "viewRIAS"

    ''' <summary>
    ''' Nombre de la vista de la rejilla de descripciones
    ''' </summary>
    Private Const VIEW_DESCRIPTIONS As String = "INDviewDescriptions"

    ''' <summary>
    ''' Nombre de la vista de la rejilla de unidad funcional
    ''' </summary>
    Private Const VIEW_FUNCTIONALUNIT As String = "viewGridFunctionalUnit"

    ''' <summary>
    ''' Tupla para el tipo de regla
    ''' </summary>
    Private ListRulesType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Tupla para el tipo de condición
    ''' </summary>
    Private ListConditionType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Tupla para el segundo tipo de condición
    ''' </summary>
    Private ListConditionTypeSecond As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Tupla para los tipos de operadores lógicos
    ''' </summary>
    Private ListLogicOperator As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Private ListUnitType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Private ListLiquidationType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Private ListEqualsOperator As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado para el datasource de los controles de tipo de regla
    ''' </summary>
    ''' <remarks></remarks>
    Private ListXpCollection As XPCollection

    ''' <summary>
    ''' Presentador del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Presenter As PAddRule

    ''' <summary>
    ''' Permite saber si controlo el cambio del cambio de valor
    ''' </summary>
    ''' <remarks></remarks>
    Private controlerEditValueChanged As Boolean = False

    ''' <summary>
    ''' Listado de condiciones de los detalles
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDefinitionRateDetailCondition As List(Of DefinitionRateDetailCondition)

    ''' <summary>
    ''' Listado de eliminados de condiciones de los detalles
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDeleteDefinitionRateDetailCondition As List(Of DefinitionRateDetailCondition)

    ''' <summary>
    ''' Representa a la entidad de condiciones de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Private definitionRateDetailCondition As DefinitionRateDetailCondition

    ''' <summary>
    ''' Permite saber si se esta editando un registro
    ''' </summary>
    ''' <remarks></remarks>
    Private modeEditGridRates As Boolean = False

    ''' <summary>
    ''' Representa al listado que valida cuando se edita un item
    ''' de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public ListValidate As List(Of DefinitionRateDetail)

    ''' <summary>
    ''' Obtiene o establece el nombre de la vista de la rejilla que esta con focus
    ''' </summary>
    ''' <remarks></remarks>
    Private viewNameFocus As String

    ''' <summary>
    ''' Id del servicio ips que sirve para poder consultar los items qx que tiene asociado, 
    ''' este se saca del item seleccionado del combo servicio ips(Solo se puede seleccionar uno con presentación qx)
    ''' </summary>
    Private IPSServiceId As Integer

    ''' <summary>
    ''' Obtiene un servicio IPS
    ''' </summary>
    Private _serviceIPS As ContractIPSServiceXPO
    ''' <summary>
    ''' Variable para el formato de moneda
    ''' </summary>
    Private FormatNumber As Globalization.NumberFormatInfo

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Metodo deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        INDbtnAddRule.Text = ResourceManager.GetString("Edit")

        Try
            Me.AsyncLoaderPopUp(True)
            If DefinitionRateDetail IsNot Nothing Then
                With DefinitionRateDetail
                    INDlyAddRule.BeginUpdate()
                    controlerEditValueChanged = True

                    If .RuleType = 1 Then
                        Me._serviceIPS = Await Presenter.GetServiceIPS(.IPSServiceId)
                    End If

                    RuleType = .RuleType
                    INDpceControlRuleType.Properties.ReadOnly = True
                    INDsleRuleType.Properties.ReadOnly = True
                    INDpceControlRuleType.Text = "1 item seleccionado"
                    ConditionType = .ConditionType
                    controlerEditValueChanged = False
                    INDsleConditionType.Properties.ReadOnly = True
                    LogicOperator = .LogicalOperator
                    INDsleLogicOperator.Properties.ReadOnly = True

                    If .ConditionType2 <> 5 Then
                        ConditionTypeSecond = .ConditionType2
                    End If

                    INDsleConditionTypeSecond.Properties.ReadOnly = True
                    Weight = .Weight
                    AllowValueChange = .AllowValueChange


                    'Si el detalle viene con items qx
                    If .DefinitionRateDetailSurgicalProcedures IsNot Nothing AndAlso .DefinitionRateDetailSurgicalProcedures.Any() Then
                        IPSServiceId = .IPSServiceId
                        INDlyItemSurgicalProcedures.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDpceSurgicalProcedures.Text = .DefinitionRateDetailSurgicalProcedures.Count.ToString() + " Item Seleccionados"
                        INDgcSurgicalProcedures.DataSource = Await Presenter.ListSurgicalProcedures(IPSServiceId)

                        For Each itemSurgical In (From x In .DefinitionRateDetailSurgicalProcedures Where x.ChangeTracker.State <> ObjectState.Deleted).ToList()
                            Dim info = (From x In CType(INDgcSurgicalProcedures.DataSource, List(Of SurgicalProcedureServiceXpo)) Where x.Id = itemSurgical.SurgicalProcedureServiceId).FirstOrDefault()
                            If info IsNot Nothing Then
                                info.SelectOption = True
                            End If
                        Next
                        INDgcSurgicalProcedures.RefreshDataSource()

                    End If

                    LiquidationType = .LiquidationType
                    Select Case LiquidationType
                        Case 1 'Fija
                            RateManualId = .RateManualId
                            INDsleRateManual.Properties.NullText = .RateManualDescription
                            SalesValue = .SalesValue
                            SalesValueWithSurcharge = .SalesValueWithSurcharge

                        Case 2 'Estandar
                            ManualType = .ManualType
                            RateManualId = .RateManualId
                            RateVariation = .RateVariation
                            INDsleRateManual.Properties.NullText = .RateManualDescription

                        Case 3 'Vigencia
                            RateManualValidityId = .RateManualValidityId
                            INDSleRateManualValidity.Properties.NullText = .RateManualValidityDescription
                            RateVariation = .RateVariation

                        Case Else
                            RateManualId = .RateManualId
                            INDsleRateManual.Properties.NullText = .RateManualDescription
                    End Select

                    If ConditionType <> 5 Then
                        'Pregunto si el listado viene lleno y lo asigno al listado para realizar el datasource en la rejilla
                        If .DefinitionRateDetailCondition.Any() Then

                            ListDefinitionRateDetailCondition = .DefinitionRateDetailCondition.ToList()
                            INDgcRates.DataSource = Nothing
                            INDgcRates.DataSource = ListDefinitionRateDetailCondition

                        Else 'Si el listado viene vacio es porque ya ha sido guardado y se consulta el listado de condiciones
                            If .Id > 0 Then
                                Using model As New MDefinitionRate(Me.Tag)
                                    Dim result As ActionResult(Of List(Of DefinitionRateDetailCondition)) = Await model.GetListDefinitionRateDetailConditionByDefinitionRateDetailId(.Id)
                                    If Not result.StateResult Then
                                        AsyncLoader(False)
                                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                                        Exit Function
                                    End If

                                    If result.ObjectEmbbeded.Any() Then
                                        If ListDefinitionRateDetailCondition Is Nothing Then
                                            ListDefinitionRateDetailCondition = New List(Of DefinitionRateDetailCondition)
                                        End If

                                        For Each item In result.ObjectEmbbeded
                                            item.StartTracking()
                                            item.MarkAsUnchanged()
                                            ListDefinitionRateDetailCondition.Add(item)
                                        Next

                                        INDgcRates.DataSource = Nothing
                                        INDgcRates.DataSource = ListDefinitionRateDetailCondition
                                    End If
                                End Using
                            End If
                        End If
                    End If
                    INDlyAddRule.EndUpdate()
                End With
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        Finally
            AsyncLoaderPopUp(False)
        End Try
    End Function

    ''' <summary>
    ''' Funcion que se encarga de retornar la parametrizacion de la compañia
    ''' </summary>
    Private Async Function GetCompanySettings() As Task
        Using model As New MCompanySettings(Tag)
            _companySettings = Await model.GetCompanySettings()

            If Me._companySettings Is Nothing OrElse Me._companySettings.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de la Compañia"
                Exit Function
            End If
        End Using
    End Function

    ''' <summary>
    ''' Valida los controles del form
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsForm() As Boolean
        Dim listErrors As New StringBuilder
        'Se valida que haya al menos un item en la rejilla siempre y cuando el grupo de tarifas esta visible
        If INDlygRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ListDefinitionRateDetailCondition Is Nothing OrElse Not ListDefinitionRateDetailCondition.Any() Then
                listErrors.AppendLine("Debe ingresar al menos un item en la rejilla.")
            End If

        ElseIf INDlyItemLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If LiquidationType = 0 Then
                listErrors.AppendLine("Debe ingresar un tipo de liquidación.")
            End If

            If INDlyItemSalesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If RateManualId Is Nothing Then
                    listErrors.AppendLine("Debe ingresar un manual de tarifas.")
                End If

                If SalesValue = 0 Then
                    listErrors.AppendLine("Debe ingresar un valor de servicio.")
                End If

                If SalesValueWithSurcharge = 0 Then
                    listErrors.AppendLine("Debe ingresar un valor de recargo.")
                End If
            End If

            If INDlyItemRateManualValidity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If RateManualValidityId Is Nothing Then
                    listErrors.AppendLine("Debe ingresar una vigencia de manual de tarifas.")
                End If
            End If

            If INDlyItemRateManual.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If RateManualId Is Nothing Then
                    listErrors.AppendLine("Debe ingresar un manual de tarifas.")
                End If
            End If
        End If

        If RuleType <> 5 And Not ModeEdit Then 'Si es diferente a general
            If (From l In ListXpCollection Where l.SelectOption = True Select l).Count = 0 Then
                listErrors.AppendLine("Seleccione al menos un item de " & INDlyItemControlRuleType.Text & ".")
            End If
        End If

        'Se valida que si esta visible el control de procedimientos qx hayan seleccionado un item
        If INDlyItemSurgicalProcedures.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDgcSurgicalProcedures.DataSource Is Nothing OrElse CType(INDgcSurgicalProcedures.DataSource, List(Of SurgicalProcedureServiceXpo)) Is Nothing OrElse (From x In CType(INDgcSurgicalProcedures.DataSource, List(Of SurgicalProcedureServiceXpo)) Where x.SelectOption = True).Count = 0 Then
                listErrors.AppendLine("Debe seleccionar al menos un item de detalles del procedimiento qx")
            End If
        End If

        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Funcion que se encarga de crear los detalles para definicion de tarifas
    ''' </summary>
    Private Function GenerateDefinitionRateDetail(Optional itemXpo As Object = Nothing)
        Try
            Dim _DefinitionRateDetailTmp As New DefinitionRateDetail
            With _DefinitionRateDetailTmp

                .RuleType = RuleType
                .RuleTypeName = "0" + RuleType.ToString + " - " + INDsleRuleType.Text

                Select Case RuleType
                    Case 1 'IPSService
                        .IPSServiceId = itemXpo?.Id
                        .CUPSEntityId = itemXpo?.CUPSEntityId
                        .RuleDescription = itemXpo?.CodeName

                        If itemXpo?.CUPSEntityId IsNot Nothing AndAlso itemXpo?.CUPSEntityId > 0 Then
                            .RuleDescription = itemXpo.Code + " - " + itemXpo?.CupsEntityCode + " - " + itemXpo.Name
                        End If
                    Case 2 'CUPS
                        .CUPSEntityId = itemXpo?.Id
                        .RuleDescription = itemXpo?.CodeDescription
                    Case 3 'SubGroup
                        .CUPSSubgroupId = itemXpo?.Id
                        .RuleDescription = itemXpo?.CodeName
                    Case 4 'Group
                        .CUPSGroupId = itemXpo?.Id
                        .RuleDescription = itemXpo?.CodeName
                    Case 5
                        .RuleDescription = INDsleRuleType.Text
                End Select

                .ConditionType = ConditionType
                .LogicalOperator = LogicOperator

                If LogicOperator = 1 Then
                    .ConditionType2 = 5
                    .ConditionName = INDsleConditionType.Text
                Else 'Y u O
                    .ConditionType2 = ConditionTypeSecond
                    .ConditionName = INDsleConditionType.Text + " " + INDsleLogicOperator.Text + " " + INDsleConditionTypeSecond.Text
                End If

                .Weight = Weight
                .AllowValueChange = AllowValueChange

                .LiquidationType = LiquidationType
                Select Case LiquidationType
                    Case 1 'Fija
                        .RateManualId = RateManualId
                        .RateManualDescription = INDsleRateManual.Text
                        .SalesValue = SalesValue
						.SalesValueWithSurcharge = SalesValueWithSurcharge
						.ManualType = ManualType

					Case 2 'Estandar
                        .ManualType = ManualType
                        .RateManualId = RateManualId
						.RateVariation = RateVariation

					Case 3 'Vigencia
						.RateManualValidityId = RateManualValidityId
						.RateManualValidityDescription = INDSleRateManualValidity.Text
						.RateVariation = RateVariation

					Case Else
						.RateManualId = RateManualId
						.RateManualDescription = INDsleRateManual.Text
				End Select
			End With

			If ListDefinitionRateDetailCondition IsNot Nothing Then
				ListDefinitionRateDetailCondition.ForEach(Sub(item) _DefinitionRateDetailTmp.DefinitionRateDetailCondition.Add(item.Clone))
			End If

			'Se valida que se este recorriendo items de servicio ips y que esté visible el layout de items qx
			If itemXpo?.GetType() = GetType(ViewListIPSServiceWithHomologationsXpo) AndAlso INDlyItemSurgicalProcedures.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
				If itemXpo?.Presentation = 2 Then 'Como solo puede estar seleccionado un item de presentación qx, entonces se procede a crear los detalles con los items qx
					Dim listSurgical = (From x In CType(INDgcSurgicalProcedures.DataSource, List(Of SurgicalProcedureServiceXpo)) Where x.SelectOption = True Select x).ToList()
					If listSurgical IsNot Nothing AndAlso listSurgical.Any() Then
						For Each itemSurgical In listSurgical
							Dim drdsp As New DefinitionRateDetailSurgicalProcedures
							drdsp.SurgicalProcedureServiceId = itemSurgical.Id
							drdsp.IPSServiceId = itemSurgical.IPSServiceId.Id
							drdsp.QxCode = itemSurgical.IPSServiceId.Code
							drdsp.QxName = itemSurgical.IPSServiceId.Name
							drdsp.QxClassName = itemSurgical.IPSServiceId.ServiceClassName
							_DefinitionRateDetailTmp.DefinitionRateDetailSurgicalProcedures.Add(drdsp)
						Next
					End If
				End If
			End If
			Return _DefinitionRateDetailTmp

		Catch ex As Exception
			Throw
		End Try
	End Function

	''' <summary>
	''' Metodo que agrega la regla al form principal
	''' </summary>
	''' <remarks></remarks>
	Private Sub AddRule()

		If Not ValidateControls() Then
			Exit Sub
		End If

		If Not ValidateControlsForm() Then
			Exit Sub
		End If

		Try
			AsyncLoader(True)
			Dim ListDefinitionRateDetail As New List(Of DefinitionRateDetail)

			If Not ModeEdit Then 'Cuando se va agregar
				If RuleType <> 5 Then 'Si es diferente a general

					'Se recorre el listado para establecer la cantidad de objetos que se van a crear
					For Each itemXpo In (From l In ListXpCollection Where l.SelectOption = True Select l).ToList
						ListDefinitionRateDetail.Add(GenerateDefinitionRateDetail(itemXpo))
					Next

				Else 'Si es general
					ListDefinitionRateDetail.Add(GenerateDefinitionRateDetail())
				End If
			Else 'Cuando se va a modificar

				'Si se modifico la entidad se valida que no exista en el listado del form principal
				If Not ValidateListModify() Then
					AsyncLoader(False)
					Exit Sub
				End If

				Dim listDeleteDefinitionRateDetailSurgicalProcedures As New List(Of DefinitionRateDetailSurgicalProcedures)
				With DefinitionRateDetail

					.Weight = Weight
					.AllowValueChange = AllowValueChange

					.LiquidationType = LiquidationType
					Select Case LiquidationType
						Case 1 'Fija
							.RateManualId = RateManualId
							.RateManualDescription = INDsleRateManual.Text
							.SalesValue = SalesValue
							.SalesValueWithSurcharge = SalesValueWithSurcharge
							.ManualType = ManualType

						Case 2 'Estandar
							.ManualType = ManualType
							.RateManualId = RateManualId
							.RateVariation = RateVariation

						Case 3 'Vigencia
							.RateManualValidityId = RateManualValidityId
                            .RateManualValidityDescription = INDSleRateManualValidity.Text
                            .RateVariation = RateVariation

                        Case Else
                            .RateManualId = RateManualId
                            .RateManualDescription = INDsleRateManual.Text
                    End Select

                    If ListDefinitionRateDetailCondition IsNot Nothing AndAlso ListDefinitionRateDetailCondition.Any() Then
                        .DefinitionRateDetailCondition.Clear()
                        ListDefinitionRateDetailCondition.ForEach(Sub(x) .DefinitionRateDetailCondition.Add(x))
                    End If

                    If INDlyItemSurgicalProcedures.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        Dim listSurgical = (From x In CType(INDgcSurgicalProcedures.DataSource, List(Of SurgicalProcedureServiceXpo)) Where x.SelectOption = True Select x).ToList()
                        If listSurgical IsNot Nothing AndAlso listSurgical.Any() Then
                            Dim count As Integer = 0
                            While count < (From x In .DefinitionRateDetailSurgicalProcedures Where x.ChangeTracker.State <> ObjectState.Deleted).Count
                                If (From x In listSurgical Where x.Id = .DefinitionRateDetailSurgicalProcedures(count).SurgicalProcedureServiceId).Count = 0 AndAlso .DefinitionRateDetailSurgicalProcedures(count).Id > 0 Then
                                    listDeleteDefinitionRateDetailSurgicalProcedures.Add(.DefinitionRateDetailSurgicalProcedures(count))
                                    .DefinitionRateDetailSurgicalProcedures.Remove(.DefinitionRateDetailSurgicalProcedures(count))
                                    count = 0
                                ElseIf (From x In listSurgical Where x.Id = .DefinitionRateDetailSurgicalProcedures(count).SurgicalProcedureServiceId).Count = 0 AndAlso .DefinitionRateDetailSurgicalProcedures(count).Id = 0 Then
                                    .DefinitionRateDetailSurgicalProcedures.Remove(.DefinitionRateDetailSurgicalProcedures(count))
                                    count = 0
                                Else
                                    count += 1
                                End If
                            End While

                            For Each itemSurgical In listSurgical
                                If (From x In .DefinitionRateDetailSurgicalProcedures Where x.SurgicalProcedureServiceId = itemSurgical.Id AndAlso x.ChangeTracker.State <> ObjectState.Deleted).Count = 0 Then
                                    Dim drdsp As New DefinitionRateDetailSurgicalProcedures
                                    drdsp.SurgicalProcedureServiceId = itemSurgical.Id
                                    drdsp.IPSServiceId = itemSurgical.IPSServiceId.Id
                                    drdsp.QxCode = itemSurgical.IPSServiceId.Code
                                    drdsp.QxName = itemSurgical.IPSServiceId.Name
                                    drdsp.QxClassName = itemSurgical.IPSServiceId.ServiceClassName
                                    .DefinitionRateDetailSurgicalProcedures.Add(drdsp)
                                End If
                            Next
                        End If
                    End If

                    If .Id > 0 Then
                        .MarkAsModified()
                    End If
                End With

                If listDeleteDefinitionRateDetailSurgicalProcedures IsNot Nothing AndAlso listDeleteDefinitionRateDetailSurgicalProcedures.Any() Then
                    listDeleteDefinitionRateDetailSurgicalProcedures.ForEach(Sub(item) DefinitionRateDetail.DefinitionRateDetailSurgicalProcedures.Add(item.MarkAsDeleted()))
                End If

                ListDefinitionRateDetail.Add(DefinitionRateDetail)
            End If

            'Se instancia el objeto que se envia al evento
            Dim args As New AddInfoToGridFormPrincipal With {.ListDefinitionRateDetail = ListDefinitionRateDetail, .ReturnValueOk = True, .ModeEdit = ModeEdit, .ListDeleteDefinitionRateDetailCondition = ListDeleteDefinitionRateDetailCondition}
            RaiseEvent AddInfoToGridFormPrincipal(Nothing, args)
            AsyncLoader(False)
            If args.ReturnValueOk Then
                If ModeEdit Then
                    Me.Close()
                Else
                    CleanControlsPartial()
                    ModeEdit = False
                End If
            End If

        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Valida el listado cuando se va a modificar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateListModify() As Boolean
        Dim listErrors As New StringBuilder
        Dim count As Integer = 0
        Select Case RuleType
            Case 1 'IPSService
                If DefinitionRateDetail.DefinitionRateDetailSurgicalProcedures IsNot Nothing AndAlso DefinitionRateDetail.DefinitionRateDetailSurgicalProcedures.Any() Then
                    Dim listQxIdEdit = (From x In DefinitionRateDetail.DefinitionRateDetailSurgicalProcedures Select x.IPSServiceId).ToList()
                    Dim listQxCheckNew = (From x In CType(INDgcSurgicalProcedures.DataSource, List(Of SurgicalProcedureServiceXpo)) Where x.SelectOption = True AndAlso Not listQxIdEdit.Contains(x.IPSServiceId.Id) Select x).ToList()

                    Dim listDRDSP As New List(Of DefinitionRateDetailSurgicalProcedures)
                    For Each itemTemp In (From x In ListValidate Where x.IPSServiceId = DefinitionRateDetail.IPSServiceId AndAlso x.CUPSEntityId = DefinitionRateDetail.CUPSEntityId AndAlso x.DefinitionRateDetailSurgicalProcedures IsNot Nothing AndAlso x.DefinitionRateDetailSurgicalProcedures.Any() Select x).ToList()
                        listDRDSP.AddRange(itemTemp.DefinitionRateDetailSurgicalProcedures)
                    Next

                    If listQxCheckNew IsNot Nothing AndAlso listQxCheckNew.Any() AndAlso listDRDSP.Any() Then
                        listQxCheckNew.ForEach(Sub(m)
                                                   If listDRDSP.Exists(Function(x) x.IPSServiceId = m.IPSServiceId.Id) Then
                                                       listErrors.AppendLine("Ya existe el detalle Qx " + m.IPSServiceId.CodeName + " en el listado")
                                                   End If
                                               End Sub)
                    End If
                Else
                    count = (From l In ListValidate Where l.IPSServiceId = DefinitionRateDetail.IPSServiceId AndAlso l.CUPSEntityId = DefinitionRateDetail.CUPSEntityId AndAlso l.ConditionType = ConditionType AndAlso l.ConditionType2 = ConditionTypeSecond Select l).Count
                End If
            Case 2 'CUPS
                count = (From l In ListValidate Where l.CUPSEntityId = DefinitionRateDetail.CUPSEntityId AndAlso l.ConditionType = ConditionType AndAlso l.ConditionType2 = ConditionTypeSecond Select l).Count
            Case 3 'SubGroup
                count = (From l In ListValidate Where l.CUPSSubgroupId = DefinitionRateDetail.CUPSSubgroupId AndAlso l.ConditionType = ConditionType AndAlso l.ConditionType2 = ConditionTypeSecond Select l).Count
            Case 4 'Group
                count = (From l In ListValidate Where l.CUPSGroupId = DefinitionRateDetail.CUPSGroupId AndAlso l.ConditionType = ConditionType AndAlso l.ConditionType2 = ConditionTypeSecond Select l).Count
            Case 5 'General
                count = (From l In ListValidate Where l.RuleType = DefinitionRateDetail.RuleType AndAlso l.ConditionType = ConditionType AndAlso l.ConditionType2 = ConditionTypeSecond Select l).Count
        End Select

        If count > 0 Then
            If listErrors.ToString().Length = 0 Then
                listErrors.AppendLine("La regla " + DefinitionRateDetail.RuleDescription + " ya existe con la primera condición " + INDsleConditionType.Text + " y la segunda condición " + INDsleConditionTypeSecond.Text)
            End If
        End If

        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        RuleType = Nothing
        INDlyItemControlRuleType.Text = "Control Tipo Regla"
        INDpceControlRuleType.Text = String.Empty
        ListXpCollection = Nothing
        INDgcControlsRuleType.DataSource = Nothing
        CleanControlsPartial()
        IPSServiceId = Nothing
        INDlyItemSurgicalProcedures.HideLayout()
        INDsleRuleType.Focus()
    End Sub

    ''' <summary>
    ''' Limpia solo algunos controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPartial()
        INDsleRuleType.Properties.ReadOnly = False
        INDpceControlRuleType.Properties.ReadOnly = False
        ConditionType = Nothing
        Weight = 1
        AllowValueChange = Nothing

        If ModeEdit AndAlso DefinitionRateDetail IsNot Nothing AndAlso ListXpCollection IsNot Nothing AndAlso ListXpCollection.Count > 0 Then
            Dim itemXpo
            Select Case DefinitionRateDetail.RuleType
                Case 1 'IPSService
                    itemXpo = (From l In ListXpCollection Where l.Id = DefinitionRateDetail.IPSServiceId Select l).FirstOrDefault
                Case 2 'CUPS
                    itemXpo = (From l In ListXpCollection Where l.Id = DefinitionRateDetail.CUPSEntityId Select l).FirstOrDefault
                Case 3 'SubGroup
                    itemXpo = (From l In ListXpCollection Where l.Id = DefinitionRateDetail.CUPSSubgroupId Select l).FirstOrDefault
                Case 4 'Group
                    itemXpo = (From l In ListXpCollection Where l.Id = DefinitionRateDetail.CUPSGroupId Select l).FirstOrDefault
                Case 5 'General
                    itemXpo = Nothing
                Case Else
                    itemXpo = Nothing
            End Select

            If itemXpo IsNot Nothing Then
                itemXpo.SelectOption = True
                INDgcControlsRuleType.RefreshDataSource()
            End If
        End If

        INDgcSurgicalProcedures.DataSource = Nothing
        INDpceSurgicalProcedures.Text = "0 Item Seleccionados"

        LogicOperator = 1
        INDsleLogicOperator.Properties.ReadOnly = False
        INDsleConditionType.Properties.ReadOnly = False
        INDsleConditionTypeSecond.Properties.ReadOnly = False

        ConditionTypeSecond = Nothing

        INDlyItemRateVariation.HideLayout
        INDgcRates.DataSource = Nothing
        ListDefinitionRateDetailCondition = Nothing
        ListDeleteDefinitionRateDetailCondition = Nothing

        LiquidationType = Nothing
        ManualType = Nothing
        SalesValue = Nothing
        SalesValueWithSurcharge = Nothing
        RateManualValidityId = Nothing
        INDSleRateManualValidity.Properties.NullText = String.Empty
        RateManualId = Nothing
        INDsleRateManual.Properties.NullText = String.Empty
        RateVariation = Nothing

        CleanControlsPopup()
        CleanControlsPopupFirstCondition()

        INDlygRate.HideControl()
        INDlyItemManualType.HideLayout()
        INDlyItemSalesValue.HideLayout
        INDlyItemSalesValueWithSurcharge.HideLayout
        INDlyItemRateManualValidity.HideLayout
        HideControlsIva()

        INDsleConditionType.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()

        'Grupo Primera condicion
        StartTimeFirst = Nothing
        EndTimeFirst = Nothing
        SpecialtyIdFirst = Nothing
        FunctionalUnitIdFirst = Nothing
        UnitTypeIdFirst = Nothing
        INDsleRIASFirst.EditValue = Nothing
        INDsleDescriptionFirst.EditValue = Nothing

        If LogicOperator = 1 Then
            OperatorFirst = 1
            INDsleOperatorFirst.Properties.ReadOnly = True
        Else
            OperatorFirst = Nothing
            INDsleOperatorFirst.Properties.ReadOnly = False
        End If

        'Grupo Segunda condicion
        StartTimeSecond = Nothing
        EndTimeSecond = Nothing
        SpecialtyIdSecond = Nothing
        FunctionalUnitIdSecond = Nothing
        UnitTypeIdSecond = Nothing
        INDsleRIASSecond.EditValue = Nothing
        INDsleDescriptionSecond.EditValue = Nothing
        OperatorSecond = Nothing

        'Grupo de Liquidacion
        LiquidationTypePopup = Nothing
        ManualTypePopup = Nothing
        SalesValuePopup = Nothing
        SalesValueWithSurchargePopup = Nothing
        RateManualValidityIdPopup = Nothing
        RateManualIdPopup = Nothing
        RateVariationPopup = Nothing

        'Se limpia la entidad que se agrego
        definitionRateDetailCondition = Nothing

        'Se desbloquea los controles cuando se edita
        'Primera Condicion
        INDdteStartTimeFirst.Properties.ReadOnly = False
        INDdteEndTimeFirst.Properties.ReadOnly = False
        INDsleSpecialtyFirst.Properties.ReadOnly = False
        INDsleFunctionalUnitFirst.Properties.ReadOnly = False
        INDsleUnitTypeFirst.Properties.ReadOnly = False
        INDsleRIASFirst.Properties.ReadOnly = False
        INDsleDescriptionFirst.Properties.ReadOnly = False

        'Segunda Condicion
        INDdteStartTimeSecond.Properties.ReadOnly = False
        INDdteEndTimeSecond.Properties.ReadOnly = False
        INDsleSpecialtySecond.Properties.ReadOnly = False
        INDsleFunctionalUnitSecond.Properties.ReadOnly = False
        INDsleUnitTypeSecond.Properties.ReadOnly = False
        INDsleRIASSecond.Properties.ReadOnly = False
        INDsleDescriptionSecond.Properties.ReadOnly = False
        INDsleOperatorSecond.Properties.ReadOnly = False

        'Se limpian los nullText
        INDsleSpecialtyFirst.Properties.NullText = String.Empty
        INDsleFunctionalUnitFirst.Properties.NullText = String.Empty
        INDsleSpecialtySecond.Properties.NullText = String.Empty
        INDsleFunctionalUnitSecond.Properties.NullText = String.Empty
        INDsleRIASFirst.Properties.NullText = String.Empty
        INDsleRIASSecond.Properties.NullText = String.Empty
        INDsleDescriptionFirst.Properties.NullText = String.Empty
        INDsleDescriptionSecond.Properties.NullText = String.Empty
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles del popup con una condicion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopupFirstCondition()
        'Grupo Primera condicion
        StartTimePopupFirstCondition = Nothing
        EndTimePopupFirstCondition = Nothing

        If INDlyItemSpecialtyPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If SpecialtyPopupFirstConditionXpo IsNot Nothing AndAlso SpecialtyPopupFirstConditionXpo.Count > 0 Then
                For Each itemXpo In SpecialtyPopupFirstConditionXpo
                    itemXpo.SelectOption = False
                Next
                INDgcSpecialtyPopupFirstCondition.RefreshDataSource()
            End If
        End If

        If INDlyItemFunctionalUnitPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If FunctionalUnitPopupFirstConditionXpo IsNot Nothing AndAlso FunctionalUnitPopupFirstConditionXpo.Count > 0 Then
                For Each itemXpo In FunctionalUnitPopupFirstConditionXpo
                    itemXpo.SelectOption = False
                Next
                INDgcFunctionalUnitPopupFirstCondition.RefreshDataSource()
            End If
        End If

        If INDlyItemPceRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If RIASPopupFirstContidionXpo IsNot Nothing AndAlso RIASPopupFirstContidionXpo.Count > 0 Then
                For Each itemXpo In RIASPopupFirstContidionXpo
                    itemXpo.SelectOption = False
                Next
                INDgcRIAS.RefreshDataSource()
            End If
        End If

        If INDlyItemPceDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If DescriptionPopupFirstConditionXpo IsNot Nothing AndAlso DescriptionPopupFirstConditionXpo.Count > 0 Then
                For Each itemXpo In DescriptionPopupFirstConditionXpo
                    itemXpo.SelectOption = False
                Next
                INDgcDescriptions.RefreshDataSource()
            End If
        End If

        UnitTypeIdPopupFirstCondition = Nothing

        'Grupo de Liquidacion
        LiquidationTypePopupFirstCondition = Nothing
        ManualTypePopupFirstCondition = Nothing
        SalesValuePopupFirstCondition = Nothing
        SalesValueWithSurchargePopupFirstCondition = Nothing
        RateManualValidityIdPopupFirstCondition = Nothing
        RateManualIdPopupFirstCondition = Nothing
        RateVariationPopupFirstCondition = Nothing

        'Se limpia la entidad que se agrego
        definitionRateDetailCondition = Nothing

        'Se desbloquea los controles cuando se edita
        'Primera Condicion
        INDdteStartTimePopupFirstCondition.Properties.ReadOnly = False
        INDdteEndTimePopupFirstCondition.Properties.ReadOnly = False
        INDsleSpecialtyPopupFirstCondition.Properties.ReadOnly = False
        INDsleFunctionalUnitPopupFirstCondition.Properties.ReadOnly = False
        INDsleUnitTypePopupFirstCondition.Properties.ReadOnly = False
        INDpceRIASFirstCondition.Properties.ReadOnly = False
        INDpceDescriptionsFirstConditions.Properties.ReadOnly = False

        'Se limpian los nullText
        INDsleSpecialtyPopupFirstCondition.Text = "0 item seleccionado"
        INDsleFunctionalUnitPopupFirstCondition.Text = "0 item seleccionado"
        INDpceRIASFirstCondition.Text = "0 item seleccionado"
        INDpceDescriptionsFirstConditions.Text = "0 item seleccionado"
    End Sub

    ''' <summary>
    ''' Carga el datasource de las tuplas
    ''' </summary>
    Private Sub InitializeTuples()

        'Tipo de Reglas
        ListRulesType = New List(Of Tuple(Of Integer, String))
        ListRulesType.Add(New Tuple(Of Integer, String)(1, "Servicio IPS"))
        ListRulesType.Add(New Tuple(Of Integer, String)(2, "CUPS"))
        ListRulesType.Add(New Tuple(Of Integer, String)(3, "SubGrupo CUPS"))
        ListRulesType.Add(New Tuple(Of Integer, String)(4, "Grupo CUPS"))
        ListRulesType.Add(New Tuple(Of Integer, String)(5, "General"))
        INDsleRuleType.Properties.DataSource = ListRulesType.ToList()

        'Tipo de Condiciones Primera
        ListConditionType = New List(Of Tuple(Of Integer, String))
        ListConditionType.Add(New Tuple(Of Integer, String)(1, "Horario"))
        ListConditionType.Add(New Tuple(Of Integer, String)(2, "Especialidad"))
        ListConditionType.Add(New Tuple(Of Integer, String)(3, "Unidad Funcional"))
        ListConditionType.Add(New Tuple(Of Integer, String)(4, "Tipo de Unidad "))
        ListConditionType.Add(New Tuple(Of Integer, String)(5, "Ninguna"))
        ListConditionType.Add(New Tuple(Of Integer, String)(6, "RIAS"))
        ListConditionType.Add(New Tuple(Of Integer, String)(7, "Descripción"))
        INDsleConditionType.Properties.DataSource = ListConditionType.ToList()

        'Tipo de Condiciones Segunda
        ListConditionTypeSecond = New List(Of Tuple(Of Integer, String))
        ListConditionTypeSecond.Add(New Tuple(Of Integer, String)(1, "Horario"))
        ListConditionTypeSecond.Add(New Tuple(Of Integer, String)(2, "Especialidad"))
        ListConditionTypeSecond.Add(New Tuple(Of Integer, String)(3, "Unidad Funcional"))
        ListConditionTypeSecond.Add(New Tuple(Of Integer, String)(4, "Tipo de Unidad "))
        ListConditionTypeSecond.Add(New Tuple(Of Integer, String)(5, "Ninguna"))
        ListConditionTypeSecond.Add(New Tuple(Of Integer, String)(6, "RIAS"))
        ListConditionTypeSecond.Add(New Tuple(Of Integer, String)(7, "Descripción"))
        INDsleConditionTypeSecond.Properties.DataSource = ListConditionTypeSecond.ToList()

        'Operador Logico
        ListLogicOperator = New List(Of Tuple(Of Integer, String))
        ListLogicOperator.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("LogicOperatorNever", NAME_MODULE)))
        ListLogicOperator.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("LogicOperatorAnd", NAME_MODULE)))
        ListLogicOperator.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("LogicOperatorOr", NAME_MODULE)))
        INDsleLogicOperator.Properties.DataSource = ListLogicOperator.ToList()

        'Tipo de Unidades
        ListUnitType = New List(Of Tuple(Of Integer, String))
        ListUnitType.Add(New Tuple(Of Integer, String)(1, "Urgencias"))
        ListUnitType.Add(New Tuple(Of Integer, String)(2, "Hospitalizacion"))
        ListUnitType.Add(New Tuple(Of Integer, String)(3, "Apoyo Dx"))
        ListUnitType.Add(New Tuple(Of Integer, String)(4, "Apoyo Terapeutico"))
        ListUnitType.Add(New Tuple(Of Integer, String)(5, "Unidades de Cuidado Intensivo Adulto"))
        ListUnitType.Add(New Tuple(Of Integer, String)(6, "Unidades de Cuidado Intermedio Adulto"))
        ListUnitType.Add(New Tuple(Of Integer, String)(7, "Unidades de Cuidado Intensivo Pediatrica"))
        ListUnitType.Add(New Tuple(Of Integer, String)(8, "Unidades de Cuidado Intermedio Pediatrica"))
        ListUnitType.Add(New Tuple(Of Integer, String)(9, "Unidades de Cuidado Intensivo Neonatal"))
        ListUnitType.Add(New Tuple(Of Integer, String)(10, "Unidades de Cuidado Intermedio Neonatal"))
        ListUnitType.Add(New Tuple(Of Integer, String)(11, "Unidades de Cuidado Basico Neonatal"))
        ListUnitType.Add(New Tuple(Of Integer, String)(12, "Unidad Renal"))
        ListUnitType.Add(New Tuple(Of Integer, String)(13, "Unidad Oncologica"))
        ListUnitType.Add(New Tuple(Of Integer, String)(14, "Unidad Medicina Nuclear"))
        ListUnitType.Add(New Tuple(Of Integer, String)(15, "Consulta Externa"))
        ListUnitType.Add(New Tuple(Of Integer, String)(16, "Unidad Mental"))
        ListUnitType.Add(New Tuple(Of Integer, String)(17, "Unidad de Quemados"))
        ListUnitType.Add(New Tuple(Of Integer, String)(18, "Unidad de Cuidado Paliativo"))
        ListUnitType.Add(New Tuple(Of Integer, String)(19, "Cirugia"))
        ListUnitType.Add(New Tuple(Of Integer, String)(20, "Laboratorio"))
        ListUnitType.Add(New Tuple(Of Integer, String)(21, "Cardiologia No Invasiva"))
        ListUnitType.Add(New Tuple(Of Integer, String)(22, "Cardiologia Invasiva"))
        ListUnitType.Add(New Tuple(Of Integer, String)(23, "Gineco-Obstetricia"))
        ListUnitType.Add(New Tuple(Of Integer, String)(24, "Consulta Externa - Gineco-Obstetricia"))
        ListUnitType.Add(New Tuple(Of Integer, String)(25, "Otras"))

        'Primera condicion popup dos condiciones
        INDsleUnitTypeFirst.Properties.DataSource = ListUnitType.ToList

        'Segunda condicion popup dos condiciones
        INDsleUnitTypeSecond.Properties.DataSource = ListUnitType.ToList

        'Primera condicion popup una condicion
        INDsleUnitTypePopupFirstCondition.Properties.DataSource = ListUnitType.ToList

        'Tipo Liquidación
        ListLiquidationType = New List(Of Tuple(Of Integer, String))
        ListLiquidationType.Add(New Tuple(Of Integer, String)(1, "Fija"))
        ListLiquidationType.Add(New Tuple(Of Integer, String)(2, "Estandar"))
        ListLiquidationType.Add(New Tuple(Of Integer, String)(3, "Vigencia"))

        'Tipo liquidacion popup con dos condiciones
        INDsleLiquidationTypePopup.Properties.DataSource = ListLiquidationType.ToList()

        'Tipo liquidacion del form
        INDsleLiquidationType.Properties.DataSource = ListLiquidationType.Select(Function(m) New Tuple(Of Byte, String)(m.Item1, m.Item2)).ToList()

        'Tipo liquidacion popup con una condicion
        INDsleLiquidationTypePopupFirstCondition.Properties.DataSource = ListLiquidationType.ToList()

        'Operadores de Igualdad
        ListEqualsOperator = New List(Of Tuple(Of Integer, String))
        ListEqualsOperator.Add(New Tuple(Of Integer, String)(1, "="))
        ListEqualsOperator.Add(New Tuple(Of Integer, String)(2, "<>"))
        INDsleOperatorFirst.Properties.DataSource = ListEqualsOperator.ToList()
        INDsleOperatorSecond.Properties.DataSource = ListEqualsOperator.ToList()

        'Tipo Manual
        ListManualType = New List(Of Tuple(Of Integer, String))
        ListManualType.Add(New Tuple(Of Integer, String)(1, "ISS 2001"))
        ListManualType.Add(New Tuple(Of Integer, String)(2, "ISS 2004"))
        ListManualType.Add(New Tuple(Of Integer, String)(3, "SOAT"))
        ListManualType.Add(New Tuple(Of Integer, String)(4, "Institucional"))

        'Tipo manual form
        INDsleManualType.Properties.DataSource = ListManualType.ToList

        'Tipo manual del popup dos condiciones
        INDsleManualTypePopup.Properties.DataSource = ListManualType.ToList

        'Tipo manual del popup una condicion
        INDsleManualTypePopupFirstCondition.Properties.DataSource = ListManualType.ToList
    End Sub

    ''' <summary>
    ''' Muestra u oculta las columnas dependiendo del tipo de liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideColumnsOfGridControlsRuleType()
        INDlyItemControlRuleType.Text = ResourceManager.GetString("RuleType" + RuleType.ToString, NAME_MODULE)
        If RuleType = 5 Then
            INDpceControlRuleType.Text = "1 item seleccionado"
            INDpceControlRuleType.Properties.ReadOnly = True
        Else
            INDpceControlRuleType.Text = "0 item seleccionado"
            INDpceControlRuleType.Properties.ReadOnly = False
        End If

        Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
        Dim ListStringNames As New List(Of String)
        Select Case RuleType
            Case 1 'IPSService
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
                ListStringNames.Add("INDcolManualType")
                ListStringNames.Add("INDcolPresentation")
                ListStringNames.Add("INDcolCupsCodeName")
            Case 2 'CUPS
                ListStringNames.Add("INDcolGroupCups")
                ListStringNames.Add("INDcolSubGroupCups")
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolNameCUPS")
            Case 3 'CupsSubGroup
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
                ListStringNames.Add("INDcolGroup")
            Case 4 'CupsGroup
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
        End Select

        FieldsGrid(ListStringNames)
    End Sub

    ''' <summary>
    ''' Metodo que recorre las columnas de la rejilla y las coloca visible 
    ''' dependiendo del listado de colName que le envien
    ''' </summary>
    ''' <param name="ListStringNames"></param>
    ''' <remarks></remarks>
    Private Sub FieldsGrid(ByVal ListStringNames As List(Of String))
        'Asigno si la columna es visible o no dependiendo del listado que envien anteriormente
        For iColumns = 0 To viewControlsRuleType.Columns.Count - 1
            For iList = 0 To ListStringNames.Count - 1
                If viewControlsRuleType.Columns.Item(iColumns).Name = ListStringNames.Item(iList) Then
                    viewControlsRuleType.Columns.Item(iColumns).Visible = True
                    Exit For
                Else
                    viewControlsRuleType.Columns.Item(iColumns).Visible = False
                End If
            Next
        Next

        'Asigno los visibleIndex para que aparezcan en orden las columnas
        Dim cont As Integer = 0
        For iColumns = 0 To viewControlsRuleType.Columns.Count - 1
            If viewControlsRuleType.Columns.Item(iColumns).Visible = True Then
                viewControlsRuleType.Columns.Item(iColumns).VisibleIndex = cont
                cont += 1
            End If
        Next

        'Asigno el groupIndex a las columnas para CUPS
        If RuleType = 2 Then
            INDcolGroupCups.GroupIndex = 0
            INDcolSubGroupCups.GroupIndex = 1
        Else
            INDcolGroupCups.GroupIndex = -1
            INDcolSubGroupCups.GroupIndex = -1
        End If
    End Sub

    ''' <summary>
    ''' Carga el Datasource dependiendo del tipo de regla seleccionado
    ''' </summary>
    Private Async Function ChargueDatasourceRuleType() As Task
        ListXpCollection = Await Presenter.InitializeDataSourceGridControlsRulesType(RuleType)
        INDgcControlsRuleType.DataSource = ListXpCollection
    End Function

    ''' <summary>
    ''' Metodo que selecciona todo el grupo o todo el subGrupo de la rejilla de los controles
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOptionsGridControls(optionCheck As Integer)
        Dim view As GridView = viewControlsRuleType
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                If view.IsGroupRow(listHandlesSelected(i)) Then
                    GetChildsRows(view, listHandlesSelected(i), optionCheck)
                Else
                    Dim row = view.GetRow(listHandlesSelected(i))
                    row.SelectOption = optionCheck
                End If
            Next
        End If

        INDgcControlsRuleType.RefreshDataSource()
        Dim count = (From l In ListXpCollection Where l.SelectOption = True Select l).Count
        INDpceControlRuleType.Text = count.ToString + " item seleccionado"

        If count = ListXpCollection.Count Then
            Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.check
        Else
            Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
        End If
    End Sub

    ''' <summary>
    ''' Metodo que selecciona los items de la rejilla de especialidad
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOptionsGridSpecialty(optionCheck As Integer)
        Dim view As GridView = viewGridSpecialty
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                Dim row = view.GetRow(listHandlesSelected(i))
                row.SelectOption = optionCheck
            Next
        End If

        INDgcSpecialtyPopupFirstCondition.RefreshDataSource()
        Dim count = (From l In SpecialtyPopupFirstConditionXpo Where l.SelectOption = True Select l).Count
        INDsleSpecialtyPopupFirstCondition.Text = count.ToString + " item seleccionado"

        If count = SpecialtyPopupFirstConditionXpo.Count Then
            Me.INDcolSelectionOptionS.Image = Global.Presentation.Contract.My.Resources.Resources.check
        Else
            Me.INDcolSelectionOptionS.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
        End If
    End Sub

    ''' <summary>
    ''' Metodo que selecciona los items de la rejilla de unidad funcional
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOptionsGridFunctionalUnit(optionCheck As Integer)
        Dim view As GridView = viewGridFunctionalUnit
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                Dim row = view.GetRow(listHandlesSelected(i))
                row.SelectOption = optionCheck
            Next
        End If

        INDgcFunctionalUnitPopupFirstCondition.RefreshDataSource()
        Dim count = (From l In FunctionalUnitPopupFirstConditionXpo Where l.SelectOption = True Select l).Count
        INDsleFunctionalUnitPopupFirstCondition.Text = count.ToString + " item seleccionado"

        If count = FunctionalUnitPopupFirstConditionXpo.Count Then
            Me.INDcolSelectionOptionFU.Image = Global.Presentation.Contract.My.Resources.Resources.check
        Else
            Me.INDcolSelectionOptionFU.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
        End If
    End Sub

    ''' <summary>
    ''' Metodo que selecciona los items de la rejilla de rias
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOptionsGridRIAS(optionCheck As Integer)
        Dim view As GridView = viewRIAS
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                Dim row = view.GetRow(listHandlesSelected(i))
                row.SelectOption = optionCheck
            Next
        End If

        INDgcRIAS.RefreshDataSource()
        Dim count = (From l In RIASPopupFirstContidionXpo Where l.SelectOption = True Select l).Count
        INDpceRIASFirstCondition.Text = count.ToString + " item seleccionado"

        If count = RIASPopupFirstContidionXpo.Count Then
            Me.INDcolSelRIAS.Image = Global.Presentation.Contract.My.Resources.Resources.check
        Else
            Me.INDcolSelRIAS.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
        End If
    End Sub

    ''' <summary>
    ''' Metodo que selecciona los items de la rejilla de descripciones
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOptionsGridDescriptions(optionCheck As Integer)
        Dim view As GridView = INDviewDescriptions
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                Dim row = view.GetRow(listHandlesSelected(i))
                row.SelectOption = optionCheck
            Next
        End If

        INDgcDescriptions.RefreshDataSource()
        Dim cont = (From l In DescriptionPopupFirstConditionXpo Where l.SelectOption = True Select l).Count
        INDpceDescriptionsFirstConditions.Text = cont.ToString + " item seleccionado"

        If cont = DescriptionPopupFirstConditionXpo.Count Then
            Me.INDcolSelDescriptions.Image = Global.Presentation.Contract.My.Resources.Resources.check
        Else
            Me.INDcolSelDescriptions.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
        End If
    End Sub

    ''' <summary>
    ''' Obtiene la informacion de las filas de la rejilla
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetChildsRows(view As GridView, groupRowHandle As Integer, optionCheck As Integer)
        If Not view.IsGroupRow(groupRowHandle) Then
            Return
        End If

        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRows(view, childHandle, optionCheck)
            Else
                Dim row As Object = view.GetRow(childHandle)
                If optionCheck = 0 Then
                    row.SelectOption = False
                Else
                    row.SelectOption = True
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Oculta o visualiza los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideControls()
        If LogicOperator IsNot Nothing AndAlso ConditionType IsNot Nothing Then

            INDpceRates.Properties.ReadOnly = False 'Se desbloquea el popup de tarifa
            If LogicOperator <> 1 Then 'Y, O

                INDlyitemConditionTypeSecond.ShowLayout() 'Muestro la segunda condicion
                INDlygConditionSecond.HideControl(False) 'Muestro el grupo de la segunda condicion en el popup

                INDsleOperatorFirst.Properties.ReadOnly = False 'Quito el readOnly del control del operador de la primera condicion
                OperatorFirst = Nothing

            Else 'Ninguno

                INDlyitemConditionTypeSecond.HideLayout() 'Oculto la segunda condicion
                INDlygConditionSecond.HideControl() 'Oculto el grupo de la segunda condicion en el popup

                INDsleOperatorFirst.Properties.ReadOnly = True  'Pongo el readOnly del control del operador de la primera condicion
                OperatorFirst = 1
            End If
        Else
            INDpceRates.Properties.ReadOnly = True 'Se bloquea el popup porque no sabe cual asignarle

            INDlygConditionSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyitemConditionTypeSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyitemConditionTypeSecond.AllowHide = True
            INDsleOperatorFirst.Properties.ReadOnly = False
            OperatorFirst = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Oculta o muestra controles de la primera condición del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideControlsPopupFirstCondition()

        'Le asigno el popup indicado dependiendo de la opcion que escojan
        Dim textFirstCondition() As String
        If LogicOperator = 1 Then 'Si el operador logico es igual a ninguno se le asocia el nuevo popup con una condicion
            INDpceRates.Properties.PopupControl = INDpopupFirstCondition

            'Se coloca el parametro en el label de text del grupo del nuevo popup con una condicion
            textFirstCondition = INDlygConditionPopupFirstCondition.Text.Split(":")
            INDlygConditionPopupFirstCondition.Text = textFirstCondition(0) + ": " + INDsleConditionType.Text
        Else 'Si el operador logico es Y u O se le asocia el popup que tiene dos condiciones 
            INDpceRates.Properties.PopupControl = INDpopupRates

            'Se coloca el parametro en el label de text de la primera condicion del popup que tiene dos condiciones
            textFirstCondition = INDlygConditionFirst.Text.Split(":")
            INDlygConditionFirst.Text = textFirstCondition(0) + ": " + INDsleConditionType.Text
        End If

        Select Case ConditionType
            Case 1 'Horario
                'Se muestra la hora inicial y la hora final
                If LogicOperator = 1 Then 'Si es ninguno se ocultan los del nuevo popup con una condicion
                    INDlyItemStartTimePopupFirstCondition.ShowLayout()
                    INDlyItemEndTimePopupFirstCondition.ShowLayout()
                    'Se ocultan los demas controles
                    INDlyItemSpecialtyPopupFirstCondition.HideLayout()
                    INDlyItemFunctionalUnitPopupFirstCondition.HideLayout()
                    INDlyItemUnitTypePopupFirstCondition.HideLayout()
                    INDlyItemPceRIAS.HideLayout()
                    INDlyItemPceDescriptions.HideLayout()

                Else 'Si es Y u O se ocultan los del popup con dos condiciones
                    INDlyItemStartTimeFirst.ShowLayout()
                    INDlyItemEndTimeFirst.ShowLayout()
                    'Se ocultan los demas controles
                    INDlyItemSpecialtyFirst.HideLayout()
                    INDlyItemFunctionalUnitFirst.HideLayout()
                    INDlyItemUnitTypeFirst.HideLayout()
                    INDlyItemRIASFirst.HideLayout()
                    INDlyItemDescriptionFirst.HideLayout()

                End If
            Case 2 'Especialidad
                'Se muestra la especialidad
                If LogicOperator = 1 Then 'Si es ninguno se ocultan los del nuevo popup con una condicion
                    INDlyItemSpecialtyPopupFirstCondition.ShowLayout()
                    'Se ocultan los demas controles
                    INDlyItemStartTimePopupFirstCondition.HideLayout()
                    INDlyItemEndTimePopupFirstCondition.HideLayout()
                    INDlyItemFunctionalUnitPopupFirstCondition.HideLayout()
                    INDlyItemUnitTypePopupFirstCondition.HideLayout()
                    INDlyItemPceRIAS.HideLayout()
                    INDlyItemPceDescriptions.HideLayout()

                Else 'Si es Y u O se ocultan los del popup con dos condiciones
                    INDlyItemSpecialtyFirst.ShowLayout()
                    'Se ocultan los demas controles
                    INDlyItemStartTimeFirst.HideLayout()
                    INDlyItemEndTimeFirst.HideLayout()
                    INDlyItemFunctionalUnitFirst.HideLayout()
                    INDlyItemUnitTypeFirst.HideLayout()
                    INDlyItemRIASFirst.HideLayout()
                    INDlyItemDescriptionFirst.HideLayout()

                End If
            Case 3 'Unidad Funcional
                'Se muestra la unidad funcional
                If LogicOperator = 1 Then 'Si es ninguno se ocultan los del nuevo popup con una condicion
                    INDlyItemFunctionalUnitPopupFirstCondition.ShowLayout()
                    'Se ocultan los demas controles
                    INDlyItemStartTimePopupFirstCondition.HideLayout()
                    INDlyItemEndTimePopupFirstCondition.HideLayout()
                    INDlyItemSpecialtyPopupFirstCondition.HideLayout()
                    INDlyItemUnitTypePopupFirstCondition.HideLayout()
                    INDlyItemPceRIAS.HideLayout()
                    INDlyItemPceDescriptions.HideLayout()

                Else 'Si es Y u O se ocultan los del popup con dos condiciones
                    INDlyItemFunctionalUnitFirst.ShowLayout()
                    'Se ocultan los demas controles
                    INDlyItemStartTimeFirst.HideLayout()
                    INDlyItemEndTimeFirst.HideLayout()
                    INDlyItemSpecialtyFirst.HideLayout()
                    INDlyItemUnitTypeFirst.HideLayout()
                    INDlyItemRIASFirst.HideLayout()
                    INDlyItemDescriptionFirst.HideLayout()

                End If
            Case 4 'Tipo de Unidad
                'Se muestra el tipo de unidad
                If LogicOperator = 1 Then 'Si es ninguno se ocultan los del nuevo popup con una condicion
                    INDlyItemUnitTypePopupFirstCondition.ShowLayout()
                    'Se ocultan los demas controles
                    INDlyItemStartTimePopupFirstCondition.HideLayout()
                    INDlyItemEndTimePopupFirstCondition.HideLayout()
                    INDlyItemSpecialtyPopupFirstCondition.HideLayout()
                    INDlyItemFunctionalUnitPopupFirstCondition.HideLayout()
                    INDlyItemPceRIAS.HideLayout()
                    INDlyItemPceDescriptions.HideLayout()

                Else 'Si es Y u O se ocultan los del popup con dos condiciones
                    INDlyItemUnitTypeFirst.ShowLayout()
                    'Se ocultan los demas controles
                    INDlyItemStartTimeFirst.HideLayout()
                    INDlyItemEndTimeFirst.HideLayout()
                    INDlyItemSpecialtyFirst.HideLayout()
                    INDlyItemFunctionalUnitFirst.HideLayout()
                    INDlyItemRIASFirst.HideLayout()
                    INDlyItemDescriptionFirst.HideLayout()

                End If
            Case 6 'RIAS
                'Se muestra el RIAS
                If LogicOperator = 1 Then 'Si es ninguno se ocultan los del nuevo popup con una condición
                    INDlyItemPceRIAS.ShowLayout()
                    'Se ocultan los demas controles
                    INDlyItemUnitTypePopupFirstCondition.HideLayout()
                    INDlyItemStartTimePopupFirstCondition.HideLayout()
                    INDlyItemEndTimePopupFirstCondition.HideLayout()
                    INDlyItemSpecialtyPopupFirstCondition.HideLayout()
                    INDlyItemFunctionalUnitPopupFirstCondition.HideLayout()
                    INDlyItemPceDescriptions.HideLayout()

                Else 'Si es Y u O se ocultan los del popup con dos condiciones
                    INDlyItemRIASFirst.ShowLayout()
                    'Se ocultan los demas controles
                    INDlyItemUnitTypeFirst.HideLayout()
                    INDlyItemStartTimeFirst.HideLayout()
                    INDlyItemEndTimeFirst.HideLayout()
                    INDlyItemSpecialtyFirst.HideLayout()
                    INDlyItemFunctionalUnitFirst.HideLayout()
                    INDlyItemDescriptionFirst.HideLayout()

                End If
            Case 7 'Descripción
                'Se muestra la descripción
                If LogicOperator = 1 Then 'Si es ninguno se ocultan los del nuevo popup con una condición
                    INDlyItemPceDescriptions.ShowLayout()
                    'Se ocultan los demas controles
                    INDlyItemUnitTypePopupFirstCondition.HideLayout()
                    INDlyItemStartTimePopupFirstCondition.HideLayout()
                    INDlyItemEndTimePopupFirstCondition.HideLayout()
                    INDlyItemSpecialtyPopupFirstCondition.HideLayout()
                    INDlyItemFunctionalUnitPopupFirstCondition.HideLayout()
                    INDlyItemPceRIAS.HideLayout()

                Else 'Si es Y u O se ocultan los del popup con dos condiciones
                    INDlyItemDescriptionFirst.ShowLayout()
                    'Se ocultan los demas controles
                    INDlyItemUnitTypeFirst.HideLayout()
                    INDlyItemStartTimeFirst.HideLayout()
                    INDlyItemEndTimeFirst.HideLayout()
                    INDlyItemSpecialtyFirst.HideLayout()
                    INDlyItemFunctionalUnitFirst.HideLayout()
                    INDlyItemRIASFirst.HideLayout()
                End If
        End Select
        FormatNumber = Me.indigo.CurrencyNumbertFormat
        changeNumericFormatByCurrency(FormatNumber, INDpopupFirstCondition.Controls)
    End Sub

    ''' <summary>
    ''' Oculta o muestra controles de la segunda condición del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideControlsPopupSecondCondition()
        'Se coloca el parametro en el label de text de la segunda condicion del popup
        Dim textSecondCondition() As String = INDlygConditionSecond.Text.Split(":")
        INDlygConditionSecond.Text = textSecondCondition(0) + ": " + INDsleConditionTypeSecond.Text
        Select Case ConditionTypeSecond
            Case 1 'Horario
                'Se muestra la hora inicial y la hora final
                INDlyItemStartTimeSecond.ShowLayout()
                INDlyItemEndTimeSecond.ShowLayout()
                'Se ocultan los demas controles
                INDlyItemSpecialtySecond.HideLayout()
                INDlyItemFunctionalUnitSecond.HideLayout()
                INDlyItemUnitTypeSecond.HideLayout()
                INDlyItemRIASSecond.HideLayout()
                INDlyItemDescriptionSecond.HideLayout()

            Case 2 'Especialidad
                'Se muestra la especialidad
                INDlyItemSpecialtySecond.ShowLayout()
                'Se ocultan los demas controles
                INDlyItemStartTimeSecond.HideLayout()
                INDlyItemEndTimeSecond.HideLayout()
                INDlyItemFunctionalUnitSecond.HideLayout()
                INDlyItemUnitTypeSecond.HideLayout()
                INDlyItemRIASSecond.HideLayout()
                INDlyItemDescriptionSecond.HideLayout()

            Case 3 'Unidad Funcional
                'Se muestra la unidad funcional
                INDlyItemFunctionalUnitSecond.ShowLayout()
                'Se ocultan los demas controles
                INDlyItemStartTimeSecond.HideLayout()
                INDlyItemEndTimeSecond.HideLayout()
                INDlyItemSpecialtySecond.HideLayout()
                INDlyItemUnitTypeSecond.HideLayout()
                INDlyItemRIASSecond.HideLayout()
                INDlyItemDescriptionSecond.HideLayout()

            Case 4 'Tipo de Unidad
                'Se muestra el tipo de unidad
                INDlyItemUnitTypeSecond.ShowLayout()
                'Se ocultan los demas controles
                INDlyItemStartTimeSecond.HideLayout()
                INDlyItemEndTimeSecond.HideLayout()
                INDlyItemSpecialtySecond.HideLayout()
                INDlyItemFunctionalUnitSecond.HideLayout()
                INDlyItemRIASSecond.HideLayout()
                INDlyItemDescriptionSecond.HideLayout()

            Case 6 'RIAS
                'Se muestra el RIAS
                INDlyItemRIASSecond.ShowLayout()
                'Se ocultan los demas controles
                INDlyItemStartTimeSecond.HideLayout()
                INDlyItemEndTimeSecond.HideLayout()
                INDlyItemSpecialtySecond.HideLayout()
                INDlyItemFunctionalUnitSecond.HideLayout()
                INDlyItemUnitTypeSecond.HideLayout()
                INDlyItemDescriptionSecond.HideLayout()

            Case 7 'Descripción
                'Se muestra el Descripción
                INDlyItemDescriptionSecond.ShowLayout()
                'Se ocultan los demas controles
                INDlyItemStartTimeSecond.HideLayout()
                INDlyItemEndTimeSecond.HideLayout()
                INDlyItemSpecialtySecond.HideLayout()
                INDlyItemFunctionalUnitSecond.HideLayout()
                INDlyItemUnitTypeSecond.HideLayout()
                INDlyItemRIASSecond.HideLayout()

            Case Else
                INDlyItemUnitTypeSecond.HideLayout()
                INDlyItemStartTimeSecond.HideLayout()
                INDlyItemEndTimeSecond.HideLayout()
                INDlyItemSpecialtySecond.HideLayout()
                INDlyItemFunctionalUnitSecond.HideLayout()
                INDlyItemRIASSecond.HideLayout()
                INDlyItemDescriptionSecond.HideLayout()
        End Select
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As Boolean
        Dim listErrors As New StringBuilder
        'Se valida los campos de la primera condicion
        If INDlyItemStartTimeFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If StartTimeFirst Is Nothing Then
                listErrors.AppendLine("Debe ingresar una hora inicial en la primera condición.")
            End If
        End If

        If INDlyItemEndTimeFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If EndTimeFirst Is Nothing Then
                listErrors.AppendLine("Debe ingresar una hora final en la primera condición.")
            End If
        End If

        If INDlyItemSpecialtyFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If Object.Equals(SpecialtyIdFirst, Nothing) = True Then
                listErrors.AppendLine("Debe ingresar una especialidad en la primera condición.")
            End If
        End If

        If INDlyItemFunctionalUnitFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If FunctionalUnitIdFirst Is Nothing Then
                listErrors.AppendLine("Debe ingresar una unidad funcional en la primera condición.")
            End If
        End If

        If INDlyItemUnitTypeFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If UnitTypeIdFirst Is Nothing Then
                listErrors.AppendLine("Debe ingresar un tipo de unidad en la primera condición.")
            End If
        End If

        If INDlyItemRIASFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleRIASFirst.EditValue Is Nothing Then
                listErrors.AppendLine("Debe ingresar un RIAS en la primera condición.")
            End If
        End If

        If INDlyItemDescriptionFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleDescriptionFirst.EditValue Is Nothing Then
                listErrors.AppendLine("Debe ingresar una Descripción en la primera condición.")
            End If
        End If

        If OperatorFirst = 0 Then
            listErrors.AppendLine("Debe ingresar un operador en la primera condición.")
        End If

        'Se valida los campos de la segunda condicion si el grupo de la segunda condicion esta visible
        If INDlygConditionSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDlyitemConditionTypeSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If ConditionTypeSecond Is Nothing Then
                    listErrors.AppendLine("Debe ingresar una segunda condición.")
                End If
            End If

            If INDlyItemStartTimeSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If StartTimeSecond Is Nothing Then
                    listErrors.AppendLine("Debe ingresar una hora inicial en la segunda condición.")
                End If
            End If

            If INDlyItemEndTimeSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If EndTimeSecond Is Nothing Then
                    listErrors.AppendLine("Debe ingresar una hora final en la segunda condición.")
                End If
            End If

            If INDlyItemSpecialtySecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If Object.Equals(SpecialtyIdSecond, Nothing) = True Then
                    listErrors.AppendLine("Debe ingresar una especialidad en la segunda condición.")
                End If
            End If

            If INDlyItemFunctionalUnitSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If FunctionalUnitIdSecond Is Nothing Then
                    listErrors.AppendLine("Debe ingresar una unidad funcional en la segunda condición.")
                End If
            End If

            If INDlyItemUnitTypeSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If UnitTypeIdSecond Is Nothing Then
                    listErrors.AppendLine("Debe ingresar un tipo de unidad en la segunda condición.")
                End If
            End If

            If INDlyItemRIASSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDsleRIASSecond.EditValue Is Nothing Then
                    listErrors.AppendLine("Debe ingresar un RIAS en la segunda condición.")
                End If
            End If

            If INDlyItemDescriptionSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDsleDescriptionSecond.EditValue Is Nothing Then
                    listErrors.AppendLine("Debe ingresar una Descripción en la segunda condición.")
                End If
            End If

            If OperatorSecond Is Nothing Then
                listErrors.AppendLine("Debe ingresar un operador en la segunda condición.")
            End If
        End If

        'Se valida los controles del grupo de tipo de liquidación
        If LiquidationTypePopup Is Nothing Then
            listErrors.AppendLine("Debe ingresar un tipo de liquidación.")
        End If

        If INDlyItemManualTypePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ManualTypePopup Is Nothing Then
                listErrors.AppendLine("Debe ingresar un tipo de manual.")
            End If
        End If

        If INDlyItemSalesValuePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If SalesValuePopup = 0 Then
                listErrors.AppendLine("Debe ingresar un valor de servicio.")
            End If
        End If

        If INDlyItemSalesValueWithSurchargePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If SalesValueWithSurchargePopup = 0 Then
                listErrors.AppendLine("Debe ingresar un valor con recargo.")
            End If
        End If

        If INDlyItemRateManualValidityPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If RateManualValidityIdPopup Is Nothing Then
                listErrors.AppendLine("Debe ingresar una vigencia de manual tarifario.")
            End If
        End If

        If INDlyItemRateManualPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If RateManualIdPopup Is Nothing Then
                listErrors.AppendLine("Debe ingresar manual tarifario.")
            End If
        End If

        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Metodo que agrega una tarifa con las condiciones a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddRateWithConditions()
        If Not ValidateControlsPopup() Then
            Exit Sub
        End If

        If ListDefinitionRateDetailCondition Is Nothing Then 'Si esta vacio
            ListDefinitionRateDetailCondition = New List(Of DefinitionRateDetailCondition)
        Else 'Valido que las condiciones no esten dentro de la rejilla, siempre y cuando no se este editando
            If Not modeEditGridRates Then
                Dim result = ValidateList()
                If Not result.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Exit Sub
                End If
            End If
        End If

        'Creamos la entidad para agregar al listado, cuando es para agregar
        CreateDefinitionRateDetailCondition()

        If Not modeEditGridRates Then
            ListDefinitionRateDetailCondition.Add(definitionRateDetailCondition)
            Mensaje(EeventViewerImages.Informacion) = "Tarifa agregada correctamente."
        Else
            Mensaje(EeventViewerImages.Informacion) = "Tarifa editada correctamente."
        End If

        INDgcRates.DataSource = Nothing
        INDgcRates.DataSource = ListDefinitionRateDetailCondition
        CleanControlsPopup()
        modeEditGridRates = False

        'Se bloquea los controles del form de cond1, cond2 y logicOperator
        If ListDefinitionRateDetailCondition.Any() Then
            INDsleConditionType.Properties.ReadOnly = True
            INDsleLogicOperator.Properties.ReadOnly = True
            INDsleConditionTypeSecond.Properties.ReadOnly = True
        End If

        FocusControlPopup()
    End Sub

    ''' <summary>
    ''' Metodo que crea la entidad del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateDefinitionRateDetailCondition()
        Dim firstCondition As String = String.Empty
        Dim secondCondition As String = String.Empty
        Dim rateName As String = String.Empty
        If Not modeEditGridRates Then
            definitionRateDetailCondition = New DefinitionRateDetailCondition
        End If

        With definitionRateDetailCondition
            'Primera condicion
            .Operator = OperatorFirst

            'Le pegamos el operador al texto
            firstCondition = INDsleOperatorFirst.Text

            'Horario
            If INDlyItemStartTimeFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .StartTime = StartTimeFirst
                .EndTime = EndTimeFirst

                firstCondition = firstCondition + " (" + StartTimeFirst.ToString + " - " + EndTimeFirst.ToString + ")"
            Else
                .StartTime = Nothing
                .EndTime = Nothing
            End If

            'Especialidad
            If INDlyItemSpecialtyFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .SpecialtyId = SpecialtyIdFirst
                .SpecialtyDescriptionFirst = INDsleSpecialtyFirst.Text

                firstCondition = firstCondition + " (" + INDsleSpecialtyFirst.Text + ")"
            Else
                .SpecialtyId = Nothing
                .SpecialtyDescriptionFirst = String.Empty
            End If

            'Unidad Funcional
            If INDlyItemFunctionalUnitFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .FunctionalUnitId = FunctionalUnitIdFirst
                .FunctionalUnitDescriptionFirst = INDsleFunctionalUnitFirst.Text

                firstCondition = firstCondition + " (" + INDsleFunctionalUnitFirst.Text + ")"
            Else
                .FunctionalUnitId = Nothing
                .FunctionalUnitDescriptionFirst = String.Empty
            End If

            'Tipo Unidad
            If INDlyItemUnitTypeFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .UnitTypeId = UnitTypeIdFirst

                firstCondition = firstCondition + " (" + INDsleUnitTypeFirst.Text + ")"
            Else
                .UnitTypeId = Nothing
            End If

            'RIAS
            If INDlyItemRIASFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .RIASId = INDsleRIASFirst.EditValue
                .RIASDescriptionFirst = INDsleRIASFirst.Text

                firstCondition = firstCondition + " (" + INDsleRIASFirst.Text + ")"
            Else
                .RIASId = Nothing
                .RIASDescriptionFirst = String.Empty
            End If

            'Description
            If INDlyItemDescriptionFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ContractDescriptionId = INDsleDescriptionFirst.EditValue
                .DescriptionCodeNameFirst = INDsleDescriptionFirst.Text

                firstCondition = firstCondition + " (" + INDsleDescriptionFirst.Text + ")"
            Else
                .ContractDescriptionId = Nothing
                .DescriptionCodeNameFirst = String.Empty
            End If

            'Preguntamos si el grupo de la condicion 2 esta visible
            If INDlygConditionSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .Operator2 = OperatorSecond

                'Le pegamos el operador 2 al texto
                secondCondition = INDsleOperatorSecond.Text

                'Horario
                If INDlyItemStartTimeSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .StartTime2 = StartTimeSecond
                    .EndTime2 = EndTimeSecond

                    secondCondition = secondCondition + " (" + StartTimeSecond.ToString + " - " + EndTimeSecond.ToString + ")"
                Else
                    .StartTime2 = Nothing
                    .EndTime2 = Nothing
                End If

                'Especialidad
                If INDlyItemSpecialtySecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .SpecialtyId2 = SpecialtyIdSecond
                    .SpecialtyDescriptionSecond = INDsleSpecialtySecond.Text

                    secondCondition = secondCondition + " (" + INDsleSpecialtySecond.Text + ")"
                Else
                    .SpecialtyId2 = Nothing
                    .SpecialtyDescriptionSecond = String.Empty
                End If

                'Unidad Funcional
                If INDlyItemFunctionalUnitSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .FunctionalUnitId2 = FunctionalUnitIdSecond
                    .FunctionalUnitDescriptionSecond = INDsleFunctionalUnitSecond.Text

                    secondCondition = secondCondition + " (" + INDsleFunctionalUnitSecond.Text + ")"
                Else
                    .FunctionalUnitId2 = Nothing
                    .FunctionalUnitDescriptionSecond = String.Empty
                End If

                'Tipo Unidad
                If INDlyItemUnitTypeSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .UnitTypeId2 = UnitTypeIdSecond

                    secondCondition = secondCondition + " (" + INDsleUnitTypeSecond.Text + ")"
                Else
                    .UnitTypeId2 = Nothing
                End If

                'RIAS
                If INDlyItemRIASSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .RIASId2 = INDsleRIASSecond.EditValue
                    .RIASDescriptionSecond = INDsleRIASSecond.Text

                    secondCondition = secondCondition + " (" + INDsleRIASSecond.Text + ")"
                Else
                    .RIASId2 = Nothing
                    .RIASDescriptionSecond = String.Empty
                End If

                'Description
                If INDlyItemDescriptionSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .ContractDescriptionId2 = INDsleDescriptionSecond.EditValue
                    .DescriptionCodeNameSecond = INDsleDescriptionSecond.Text

                    secondCondition = secondCondition + " (" + INDsleDescriptionSecond.Text + ")"
                Else
                    .ContractDescriptionId2 = Nothing
                    .DescriptionCodeNameSecond = String.Empty
                End If

            Else 'Si no esta visible
                .Operator2 = Nothing
                .StartTime2 = Nothing
                .EndTime2 = Nothing
                .SpecialtyId2 = Nothing
                .FunctionalUnitId2 = Nothing
                .UnitTypeId2 = Nothing
                .RIASId2 = Nothing
                .ContractDescriptionId2 = Nothing
            End If

            'Se une todo el mensaje
            .ConditionName = firstCondition + " " + INDsleLogicOperator.Text + " " + secondCondition

            'El grupo de liquidacion
            .LiquidationType = LiquidationTypePopup

            'Se le pega a la variable el tipo de liquidacion
            rateName = INDsleLiquidationTypePopup.Text

            'Grupo de liquidacion
            If INDlyItemSalesValuePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ManualType = ManualTypePopup
                .SalesValue = SalesValuePopup
                .SalesValueWithSurcharge = SalesValueWithSurchargePopup

                rateName = rateName + " - " + INDsleManualTypePopup.Text + " - " + SalesValuePopup.ToString()
            Else
                .ManualType = Nothing
                .SalesValue = Nothing
                .SalesValueWithSurcharge = Nothing
            End If

            .RateManualValidityId = Nothing
            .RateManualValidityDescription = String.Empty
            .RateManualId = Nothing
            .RateManualDescription = String.Empty
            .RateVariation = Nothing

            If INDlyItemRateManualValidityPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .RateManualValidityId = RateManualValidityIdPopup
                .RateManualValidityDescription = INDsleRateManualValidityPopup.Text
                .RateVariation = RateVariationPopup

                rateName = rateName + " - " + INDsleRateManualValidityPopup.Text + " - " + RateVariationPopup.ToString + "%"
            End If

            If INDlyItemRateManualPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .RateManualId = RateManualIdPopup
                .RateManualDescription = INDsleRateManualPopup.Text
                .RateVariation = RateVariationPopup

                rateName = rateName + " - " + INDsleRateManualPopup.Text + " - " + RateVariationPopup.ToString + "%"
            End If
            .RateName = rateName

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Valida el listado que llena el datasource de la rejilla
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateList() As ActionResult
        Dim count As Integer = 0
        Dim message As String = String.Empty
        If LogicOperator <> 1 Then 'Si el operador logico es Y, O
            Select Case True
                Case ConditionType = 1 AndAlso ConditionTypeSecond = 2 'Horario y Especialidad
                    count = (From l In ListDefinitionRateDetailCondition
                             Where ((StartTimeFirst >= l.StartTime AndAlso StartTimeFirst <= l.EndTime) _
                                 OrElse (EndTimeFirst >= l.StartTime AndAlso EndTimeFirst <= l.EndTime) _
                                 OrElse (StartTimeFirst < l.StartTime) AndAlso (EndTimeFirst > l.EndTime)) AndAlso SpecialtyIdSecond = l.SpecialtyId2 _
                                AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2).Count
                    message = "El horario " + StartTimeFirst.ToString + " - " + EndTimeFirst.ToString + " con la especialidad " + INDsleSpecialtySecond.Text + " ya existe en la lista."
                Case ConditionType = 1 AndAlso ConditionTypeSecond = 3 'Horario y Unidad Funcional
                    count = (From l In ListDefinitionRateDetailCondition
                             Where ((StartTimeFirst >= l.StartTime AndAlso StartTimeFirst <= l.EndTime) _
                                 OrElse (EndTimeFirst >= l.StartTime AndAlso EndTimeFirst <= l.EndTime) _
                                 OrElse (StartTimeFirst < l.StartTime) AndAlso (EndTimeFirst > l.EndTime)) AndAlso FunctionalUnitIdSecond = l.FunctionalUnitId2 _
                                AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2).Count
                    message = "El horario " + StartTimeFirst.ToString + " - " + EndTimeFirst.ToString + " con la unidad funcional " + INDsleFunctionalUnitSecond.Text + " ya existe en la lista."
                Case ConditionType = 1 AndAlso ConditionTypeSecond = 4 'Horario y Tipo Unidad
                    count = (From l In ListDefinitionRateDetailCondition
                             Where ((StartTimeFirst >= l.StartTime AndAlso StartTimeFirst <= l.EndTime) _
                                 OrElse (EndTimeFirst >= l.StartTime AndAlso EndTimeFirst <= l.EndTime) _
                                 OrElse (StartTimeFirst < l.StartTime) AndAlso (EndTimeFirst > l.EndTime)) AndAlso UnitTypeIdSecond = l.UnitTypeId2 _
                                AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2).Count
                    message = "El horario " + StartTimeFirst.ToString + " - " + EndTimeFirst.ToString + " con el tipo de unidad " + INDsleUnitTypeSecond.Text + " ya existe en la lista."
                Case ConditionType = 1 AndAlso ConditionTypeSecond = 6 'Horario y RIAS
                    count = (From l In ListDefinitionRateDetailCondition
                             Where ((StartTimeFirst >= l.StartTime AndAlso StartTimeFirst <= l.EndTime) _
                                 OrElse (EndTimeFirst >= l.StartTime AndAlso EndTimeFirst <= l.EndTime) _
                                 OrElse (StartTimeFirst < l.StartTime) AndAlso (EndTimeFirst > l.EndTime)) AndAlso INDsleRIASSecond.EditValue = l.RIASId2 _
                                AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2).Count
                    message = "El horario " + StartTimeFirst.ToString + " - " + EndTimeFirst.ToString + " con el RIAS " + INDsleRIASSecond.Text + " ya existe en la lista."

                Case ConditionType = 1 AndAlso ConditionTypeSecond = 7 'Horario y Description
                    count = (From l In ListDefinitionRateDetailCondition
                             Where ((StartTimeFirst >= l.StartTime AndAlso StartTimeFirst <= l.EndTime) _
                                 OrElse (EndTimeFirst >= l.StartTime AndAlso EndTimeFirst <= l.EndTime) _
                                 OrElse (StartTimeFirst < l.StartTime) AndAlso (EndTimeFirst > l.EndTime)) AndAlso INDsleDescriptionSecond.EditValue = l.ContractDescriptionId2 _
                                AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2).Count
                    message = "El horario " + StartTimeFirst.ToString + " - " + EndTimeFirst.ToString + " con la Descripción " + INDsleDescriptionSecond.Text + " ya existe en la lista."

                Case ConditionType = 2 AndAlso ConditionTypeSecond = 1 'Especialidad y Horario
                    count = (From l In ListDefinitionRateDetailCondition
                             Where SpecialtyIdFirst = l.SpecialtyId AndAlso ((StartTimeSecond >= l.StartTime2 AndAlso StartTimeSecond <= l.EndTime2) _
                                 OrElse (EndTimeSecond >= l.StartTime2 AndAlso EndTimeSecond <= l.EndTime2) _
                                 OrElse (StartTimeSecond < l.StartTime2) AndAlso (EndTimeSecond > l.EndTime2)) _
                                AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2).Count
                    message = "La especialidad " + INDsleSpecialtyFirst.Text + " con el horario " + StartTimeSecond.ToString + " - " + EndTimeSecond.ToString + " ya existe en la lista."
                Case ConditionType = 2 AndAlso ConditionTypeSecond = 3 'Especialidad y Unidad Funcional
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.SpecialtyId = SpecialtyIdFirst AndAlso l.FunctionalUnitId2 = FunctionalUnitIdSecond _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "La especialidad " + INDsleSpecialtyFirst.Text + " con la unidad funcional " + INDsleFunctionalUnitSecond.Text + " ya existe en la lista."
                Case ConditionType = 2 AndAlso ConditionTypeSecond = 4 'Especialidad y Tipo Unidad
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.SpecialtyId = SpecialtyIdFirst AndAlso l.UnitTypeId2 = UnitTypeIdSecond _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "La especialidad " + INDsleSpecialtyFirst.Text + " con el tipo de unidad " + INDsleUnitTypeSecond.Text + " ya existe en la lista."
                Case ConditionType = 2 AndAlso ConditionTypeSecond = 6 'Especialidad y RIAS
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.SpecialtyId = SpecialtyIdFirst AndAlso l.RIASId2 = INDsleRIASSecond.EditValue _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "La especialidad " + INDsleSpecialtyFirst.Text + " con el RIAS " + INDsleRIASSecond.EditValue.Text + " ya existe en la lista."

                Case ConditionType = 2 AndAlso ConditionTypeSecond = 7 'Especialidad y Description
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.SpecialtyId = SpecialtyIdFirst AndAlso l.ContractDescriptionId2 = INDsleDescriptionSecond.EditValue _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "La especialidad " + INDsleSpecialtyFirst.Text + " con la Descripción " + INDsleDescriptionSecond.EditValue.Text + " ya existe en la lista."

                Case ConditionType = 3 AndAlso ConditionTypeSecond = 1 'Unidad Funcional y Horario
                    count = (From l In ListDefinitionRateDetailCondition
                             Where FunctionalUnitIdFirst = l.FunctionalUnitId AndAlso ((StartTimeSecond >= l.StartTime2 AndAlso StartTimeSecond <= l.EndTime2) _
                                 OrElse (EndTimeSecond >= l.StartTime2 AndAlso EndTimeSecond <= l.EndTime2) _
                                 OrElse (StartTimeSecond < l.StartTime2) AndAlso (EndTimeSecond > l.EndTime2)) _
                                AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2).Count
                    message = "La unidad funcional " + INDsleFunctionalUnitFirst.Text + " con el horario " + StartTimeSecond.ToString + " - " + EndTimeSecond.ToString + " ya existe en la lista."
                Case ConditionType = 3 AndAlso ConditionTypeSecond = 2 'Unidad Funcional y Especialidad
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.FunctionalUnitId = FunctionalUnitIdFirst AndAlso l.SpecialtyId2 = SpecialtyIdSecond _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "La unidad funcional " + INDsleFunctionalUnitFirst.Text + " con la especialidad " + INDsleSpecialtySecond.Text + " ya existe en la lista."
                Case ConditionType = 3 AndAlso ConditionTypeSecond = 4 'Unidad Funcional y Tipo Unidad
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.FunctionalUnitId = FunctionalUnitIdFirst AndAlso l.UnitTypeId2 = UnitTypeIdSecond _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "La unidad funcional " + INDsleFunctionalUnitFirst.Text + " con el tipo de unidad " + INDsleUnitTypeSecond.Text + " ya existe en la lista."
                Case ConditionType = 3 AndAlso ConditionTypeSecond = 6 'Unidad Funcional y RIAS
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.FunctionalUnitId = FunctionalUnitIdFirst AndAlso l.RIASId2 = INDsleRIASSecond.EditValue _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "La unidad funcional " + INDsleFunctionalUnitFirst.Text + " con el RIAS " + INDsleRIASSecond.Text + " ya existe en la lista."

                Case ConditionType = 3 AndAlso ConditionTypeSecond = 7 'Unidad Funcional y Description
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.FunctionalUnitId = FunctionalUnitIdFirst AndAlso l.ContractDescriptionId2 = INDsleDescriptionSecond.EditValue _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "La unidad funcional " + INDsleFunctionalUnitFirst.Text + " con la descripción " + INDsleDescriptionSecond.Text + " ya existe en la lista."

                Case ConditionType = 4 AndAlso ConditionTypeSecond = 1 'Tipo Unidad y Horario
                    count = (From l In ListDefinitionRateDetailCondition
                             Where UnitTypeIdFirst = l.UnitTypeId AndAlso ((StartTimeSecond >= l.StartTime2 AndAlso StartTimeSecond <= l.EndTime2) _
                                 OrElse (EndTimeSecond >= l.StartTime2 AndAlso EndTimeSecond <= l.EndTime2) _
                                 OrElse (StartTimeSecond < l.StartTime2) AndAlso (EndTimeSecond > l.EndTime2)) _
                                AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2).Count
                    message = "El tipo de unidad " + INDsleUnitTypeFirst.Text + " con el horario " + StartTimeSecond.ToString + " - " + EndTimeSecond.ToString + " ya existe en la lista."
                Case ConditionType = 4 AndAlso ConditionTypeSecond = 2 'Tipo Unidad y Especialidad
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.UnitTypeId = UnitTypeIdFirst AndAlso l.SpecialtyId2 = SpecialtyIdSecond _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "El tipo de unidad " + INDsleUnitTypeFirst.Text + " con la especialidad " + INDsleSpecialtySecond.Text + " ya existe en la lista."
                Case ConditionType = 4 AndAlso ConditionTypeSecond = 3 'Tipo Unidad y Unidad Funcional
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.UnitTypeId = UnitTypeIdFirst AndAlso l.FunctionalUnitId2 = FunctionalUnitIdSecond _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "El tipo de unidad " + INDsleUnitTypeFirst.Text + " con la unidad funcional " + INDsleFunctionalUnitSecond.Text + " ya existe en la lista."
                Case ConditionType = 4 AndAlso ConditionTypeSecond = 6 'Tipo Unidad y RIAS
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.UnitTypeId = UnitTypeIdFirst AndAlso l.RIASId2 = INDsleRIASSecond.EditValue _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "El tipo de unidad " + INDsleUnitTypeFirst.Text + " con el RIAS " + INDsleRIASSecond.Text + " ya existe en la lista."

                Case ConditionType = 4 AndAlso ConditionTypeSecond = 7 'Tipo Unidad y Description
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.UnitTypeId = UnitTypeIdFirst AndAlso l.ContractDescriptionId2 = INDsleDescriptionSecond.EditValue _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "El tipo de unidad " + INDsleUnitTypeFirst.Text + " con la descripción " + INDsleDescriptionSecond.Text + " ya existe en la lista."

                Case ConditionType = 6 AndAlso ConditionTypeSecond = 1 'RIAS y Horario
                    count = (From l In ListDefinitionRateDetailCondition
                             Where INDsleRIASFirst.EditValue = l.RIASId AndAlso ((StartTimeSecond >= l.StartTime2 AndAlso StartTimeSecond <= l.EndTime2) _
                                 OrElse (EndTimeSecond >= l.StartTime2 AndAlso EndTimeSecond <= l.EndTime2) _
                                 OrElse (StartTimeSecond < l.StartTime2) AndAlso (EndTimeSecond > l.EndTime2)) _
                                AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2).Count
                    message = "El RIAS " + INDsleRIASFirst.Text + " con el horario " + StartTimeSecond.ToString + " - " + EndTimeSecond.ToString + " ya existe en la lista."
                Case ConditionType = 6 AndAlso ConditionTypeSecond = 2 'RIAS y Especialidad
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.RIASId = INDsleRIASFirst.EditValue AndAlso l.SpecialtyId2 = SpecialtyIdSecond _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "El RIAS " + INDsleRIASFirst.Text + " con la especialidad " + INDsleSpecialtySecond.Text + " ya existe en la lista."
                Case ConditionType = 6 AndAlso ConditionTypeSecond = 3 'RIAS y Unidad Funcional
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.RIASId = INDsleRIASFirst.EditValue AndAlso l.FunctionalUnitId2 = FunctionalUnitIdSecond _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "El RIAS " + INDsleRIASFirst.Text + " con la unidad funcional " + INDsleFunctionalUnitSecond.Text + " ya existe en la lista."
                Case ConditionType = 6 AndAlso ConditionTypeSecond = 4 'RIAS y Tipo Unidad
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.RIASId = INDsleRIASFirst.EditValue AndAlso l.UnitTypeId2 = UnitTypeIdSecond _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "El RIAS " + INDsleRIASFirst.Text + " con el tipo de unidad " + INDsleUnitTypeSecond.Text + " ya existe en la lista."

                Case ConditionType = 7 AndAlso ConditionTypeSecond = 1 'Description y Horario
                    count = (From l In ListDefinitionRateDetailCondition
                             Where INDsleDescriptionFirst.EditValue = l.ContractDescriptionId AndAlso ((StartTimeSecond >= l.StartTime2 AndAlso StartTimeSecond <= l.EndTime2) _
                                 OrElse (EndTimeSecond >= l.StartTime2 AndAlso EndTimeSecond <= l.EndTime2) _
                                 OrElse (StartTimeSecond < l.StartTime2) AndAlso (EndTimeSecond > l.EndTime2)) _
                                AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2).Count
                    message = "La Descripción " + INDsleDescriptionFirst.Text + " con el horario " + StartTimeSecond.ToString + " - " + EndTimeSecond.ToString + " ya existe en la lista."
                Case ConditionType = 7 AndAlso ConditionTypeSecond = 2 'Description y Especialidad
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.ContractDescriptionId = INDsleDescriptionFirst.EditValue AndAlso l.SpecialtyId2 = SpecialtyIdSecond _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "La Descripción " + INDsleDescriptionFirst.Text + " con la especialidad " + INDsleSpecialtySecond.Text + " ya existe en la lista."
                Case ConditionType = 7 AndAlso ConditionTypeSecond = 3 'Description y Unidad Funcional
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.ContractDescriptionId = INDsleDescriptionFirst.EditValue AndAlso l.FunctionalUnitId2 = FunctionalUnitIdSecond _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "La Descripción " + INDsleDescriptionFirst.Text + " con la unidad funcional " + INDsleFunctionalUnitSecond.Text + " ya existe en la lista."
                Case ConditionType = 7 AndAlso ConditionTypeSecond = 4 'Description y Tipo Unidad
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.ContractDescriptionId = INDsleDescriptionFirst.EditValue AndAlso l.UnitTypeId2 = UnitTypeIdSecond _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "La Descripción " + INDsleDescriptionFirst.Text + " con el tipo de unidad " + INDsleUnitTypeSecond.Text + " ya existe en la lista."
                Case ConditionType = 7 AndAlso ConditionTypeSecond = 6 'Description y RIAS
                    count = (From l In ListDefinitionRateDetailCondition
                             Where l.ContractDescriptionId = INDsleDescriptionFirst.EditValue AndAlso l.RIASId2 = INDsleRIASSecond.EditValue _
                            AndAlso OperatorFirst = l.Operator AndAlso OperatorSecond = l.Operator2 Select l).Count
                    message = "La Descripción " + INDsleDescriptionFirst.Text + " con el RIAS " + INDsleRIASSecond.Text + " ya existe en la lista."

            End Select
        Else 'Si el operador logico es Ninguna
            Select Case ConditionType
                Case 1 'Horario
                    count = (From l In ListDefinitionRateDetailCondition
                             Where ((StartTimeFirst >= l.StartTime AndAlso StartTimeFirst <= l.EndTime) _
                                 OrElse (EndTimeFirst >= l.StartTime AndAlso EndTimeFirst <= l.EndTime) _
                                 OrElse (StartTimeFirst < l.StartTime) AndAlso (EndTimeFirst > l.EndTime))).Count
                    message = "El horario " + StartTimeFirst.ToString + " - " + EndTimeFirst.ToString + " ya existe en la lista."
                Case 2 'Especialidad
                    count = (From l In ListDefinitionRateDetailCondition Where l.SpecialtyId = SpecialtyIdFirst Select l).Count
                    message = "La especialidad " + INDsleSpecialtyFirst.Text + " ya existe en la lista."
                Case 3 'Unidad Funcional
                    count = (From l In ListDefinitionRateDetailCondition Where l.FunctionalUnitId = FunctionalUnitIdFirst Select l).Count
                    message = "La unidad funcional " + INDsleFunctionalUnitFirst.Text + " ya existe en la lista."
                Case 4 'Tipo Unidad
                    count = (From l In ListDefinitionRateDetailCondition Where l.UnitTypeId = UnitTypeIdFirst Select l).Count
                    message = "El tipo de unidad " + INDsleUnitTypeFirst.Text + " ya existe en la lista."
                Case 6 'RIAS
                    count = (From l In ListDefinitionRateDetailCondition Where l.RIASId = INDsleRIASFirst.EditValue Select l).Count
                    message = "El RIAS " + INDsleRIASFirst.Text + " ya existe en la lista."
                Case 7 'Description
                    count = (From l In ListDefinitionRateDetailCondition Where l.ContractDescriptionId = INDsleDescriptionFirst.EditValue Select l).Count
                    message = "La Descripción " + INDsleDescriptionFirst.Text + " ya existe en la lista."
            End Select
        End If

        If count > 0 Then
            Return New ActionResult With {.StateResult = False, .Message = message}
        End If
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Metodo que agrega las tarifas cuando el popup es con una sola condicion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddRatePopupFirstCondition()
        If Not ValidateControlsPopupFirstCondition() Then
            Exit Sub
        End If

        If ListDefinitionRateDetailCondition Is Nothing Then 'Si esta vacio
            ListDefinitionRateDetailCondition = New List(Of DefinitionRateDetailCondition)
        Else 'Valido que las condiciones no esten dentro de la rejilla, siempre y cuando no se este editando
            If Not modeEditGridRates Then
                Dim result = ValidateListPopupFirsCondition()
                If Not result.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Exit Sub
                End If
            End If
        End If

        If Not modeEditGridRates Then
            'Creamos la entidad para agregar al listado, cuando es para agregar
            CreateListDefinitionRateDetailConditionFirstCondition()
            Mensaje(EeventViewerImages.Informacion) = "Tarifa agregada correctamente."
        Else
            With definitionRateDetailCondition
                Dim rateName As String = String.Empty

                'El grupo de liquidacion
                .LiquidationType = LiquidationTypePopupFirstCondition

                'Se le pega a la variable el tipo de liquidacion
                rateName = INDsleLiquidationTypePopupFirstCondition.Text

                'Grupo de liquidacion
                If INDlyItemSalesValuePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .ManualType = ManualTypePopupFirstCondition
                    .SalesValue = SalesValuePopupFirstCondition
                    .SalesValueWithSurcharge = SalesValueWithSurchargePopupFirstCondition

                    rateName = rateName + " - " + INDsleManualTypePopupFirstCondition.Text + " - " + SalesValuePopupFirstCondition.ToString()
                Else
                    .ManualType = Nothing
                    .SalesValue = Nothing
                    .SalesValueWithSurcharge = Nothing
                End If

                .RateManualValidityId = Nothing
                .RateManualValidityDescription = String.Empty
                .RateManualId = Nothing
                .RateManualDescription = String.Empty
                .RateVariation = Nothing

                If INDlyItemRateManualValidityPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .RateManualValidityId = RateManualValidityIdPopupFirstCondition
                    .RateManualValidityDescription = INDsleRateManualValidityPopupFirstCondition.Text
                    .RateVariation = RateVariationPopupFirstCondition

                    rateName = rateName + " - " + INDsleRateManualValidityPopupFirstCondition.Text + " - " + RateVariationPopupFirstCondition.ToString + "%"
                End If

                If INDlyItemRateManualPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .RateManualId = RateManualIdPopupFirstCondition
                    .RateManualDescription = INDsleRateManualPopupFirstCondition.Text
                    .RateVariation = RateVariationPopupFirstCondition

                    rateName = rateName + " - " + INDsleRateManualPopupFirstCondition.Text + " - " + RateVariationPopupFirstCondition.ToString + "%"
                End If
                .RateName = rateName
            End With

            Mensaje(EeventViewerImages.Informacion) = "Tarifa editada correctamente."
        End If

        INDgcRates.DataSource = Nothing
        INDgcRates.DataSource = ListDefinitionRateDetailCondition

        CleanControlsPopupFirstCondition()
        modeEditGridRates = False

        'Se bloquea los controles del form de cond1 y logicOperator
        If ListDefinitionRateDetailCondition.Any() Then
            INDsleConditionType.Properties.ReadOnly = True
            INDsleLogicOperator.Properties.ReadOnly = True
        End If
        FocusControlPopup()
    End Sub

    ''' <summary>
    ''' Metodo que crea la entidad del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateListDefinitionRateDetailConditionFirstCondition()
        Select Case ConditionType
            Case 1, 4 'Horario o UnitType
                Dim firstCondition As String = String.Empty
                Dim rateName As String = String.Empty
                definitionRateDetailCondition = New DefinitionRateDetailCondition
                With definitionRateDetailCondition
                    'Primera condicion
                    .Operator = 1

                    'Le pegamos el operador al texto
                    firstCondition = "="

                    'Horario
                    If INDlyItemStartTimePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        .StartTime = StartTimePopupFirstCondition
                        .EndTime = EndTimePopupFirstCondition

                        firstCondition = firstCondition + " (" + StartTimePopupFirstCondition.ToString + " - " + EndTimePopupFirstCondition.ToString + ")"
                    Else
                        .StartTime = Nothing
                        .EndTime = Nothing
                    End If

                    'Tipo Unidad
                    If INDlyItemUnitTypePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        .UnitTypeId = UnitTypeIdPopupFirstCondition

                        firstCondition = firstCondition + " (" + INDsleUnitTypePopupFirstCondition.Text + ")"
                    Else
                        .UnitTypeId = Nothing
                    End If

                    'Se une todo el mensaje
                    .ConditionName = firstCondition

                    'El grupo de liquidacion
                    .LiquidationType = LiquidationTypePopupFirstCondition

                    'Se le pega a la variable el tipo de liquidacion
                    rateName = INDsleLiquidationTypePopupFirstCondition.Text

                    'Grupo de liquidacion
                    If INDlyItemSalesValuePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        .ManualType = ManualTypePopupFirstCondition
                        .SalesValue = SalesValuePopupFirstCondition
                        .SalesValueWithSurcharge = SalesValueWithSurchargePopupFirstCondition

                        rateName = rateName + " - " + INDsleManualTypePopupFirstCondition.Text + " - " + SalesValuePopupFirstCondition.ToString()
                    Else
                        .ManualType = Nothing
                        .SalesValue = Nothing
                        .SalesValueWithSurcharge = Nothing
                    End If

                    .RateManualValidityId = Nothing
                    .RateManualValidityDescription = String.Empty
                    .RateManualId = Nothing
                    .RateManualDescription = String.Empty
                    .RateVariation = Nothing

                    If INDlyItemRateManualValidityPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        .RateManualValidityId = RateManualValidityIdPopupFirstCondition
                        .RateManualValidityDescription = INDsleRateManualValidityPopupFirstCondition.Text
                        .RateVariation = RateVariationPopupFirstCondition

                        rateName = rateName + " - " + INDsleRateManualValidityPopupFirstCondition.Text + " - " + RateVariationPopupFirstCondition.ToString + "%"
                    End If

                    If INDlyItemRateManualPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        .RateManualId = RateManualIdPopupFirstCondition
                        .RateManualDescription = INDsleRateManualPopupFirstCondition.Text
                        .RateVariation = RateVariationPopupFirstCondition

                        rateName = rateName + " - " + INDsleRateManualPopupFirstCondition.Text + " - " + RateVariationPopupFirstCondition.ToString + "%"
                    End If
                    .RateName = rateName

                    If .Id > 0 Then
                        .MarkAsModified()
                    End If
                End With

                ListDefinitionRateDetailCondition.Add(definitionRateDetailCondition)
            Case 2 'Specialty
                For Each itemXpo As Infrastructure.Data.Xpo.CrystalRepository.SpecialtyXpo In (From l In SpecialtyPopupFirstConditionXpo Where l.SelectOption = True Select l).ToList
                    Dim firstCondition As String = String.Empty
                    Dim rateName As String = String.Empty
                    definitionRateDetailCondition = New DefinitionRateDetailCondition
                    With definitionRateDetailCondition
                        'Primera condicion
                        .Operator = 1

                        'Le pegamos el operador al texto
                        firstCondition = "="

                        'Especialidad
                        .SpecialtyId = itemXpo.CODESPECI
                        .SpecialtyDescriptionFirst = itemXpo.CodeName

                        firstCondition = firstCondition + " (" + itemXpo.CodeName + ")"

                        'Se une todo el mensaje
                        .ConditionName = firstCondition

                        'El grupo de liquidacion
                        .LiquidationType = LiquidationTypePopupFirstCondition

                        'Se le pega a la variable el tipo de liquidacion
                        rateName = INDsleLiquidationTypePopupFirstCondition.Text
                        'Grupo de liquidacion
                        If INDlyItemSalesValuePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            .ManualType = ManualTypePopupFirstCondition
                            .SalesValue = SalesValuePopupFirstCondition
                            .SalesValueWithSurcharge = SalesValueWithSurchargePopupFirstCondition

                            rateName = rateName + " - " + INDsleManualTypePopupFirstCondition.Text + " - " + SalesValuePopupFirstCondition.ToString()
                        Else
                            .ManualType = Nothing
                            .SalesValue = Nothing
                            .SalesValueWithSurcharge = Nothing
                        End If

                        .RateManualValidityId = Nothing
                        .RateManualValidityDescription = String.Empty
                        .RateManualId = Nothing
                        .RateManualDescription = String.Empty
                        .RateVariation = Nothing

                        If INDlyItemRateManualValidityPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            .RateManualValidityId = RateManualValidityIdPopupFirstCondition
                            .RateManualValidityDescription = INDsleRateManualValidityPopupFirstCondition.Text
                            .RateVariation = RateVariationPopupFirstCondition

                            rateName = rateName + " - " + INDsleRateManualPopupFirstCondition.Text + " - " + RateVariationPopupFirstCondition.ToString + "%"
                        End If

                        If INDlyItemRateManualPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            .RateManualId = RateManualIdPopupFirstCondition
                            .RateManualDescription = INDsleRateManualPopupFirstCondition.Text
                            .RateVariation = RateVariationPopupFirstCondition

                            rateName = rateName + " - " + INDsleRateManualPopupFirstCondition.Text + " - " + RateVariationPopupFirstCondition.ToString + "%"
                        End If
                        .RateName = rateName

                        If .Id > 0 Then
                            .MarkAsModified()
                        End If
                    End With

                    ListDefinitionRateDetailCondition.Add(definitionRateDetailCondition)
                Next
            Case 3 'FunctionalUnit
                For Each itemXpo As Infrastructure.Data.Xpo.PayrollRepository.PayrollFunctionalUnit In (From l In FunctionalUnitPopupFirstConditionXpo Where l.SelectOption = True Select l).ToList
                    Dim firstCondition As String = String.Empty
                    Dim rateName As String = String.Empty
                    definitionRateDetailCondition = New DefinitionRateDetailCondition
                    With definitionRateDetailCondition
                        'Primera condicion
                        .Operator = 1

                        'Le pegamos el operador al texto
                        firstCondition = "="
                        'Unidad Funcional
                        .FunctionalUnitId = itemXpo.Id
                        .FunctionalUnitDescriptionFirst = itemXpo.CodeDescription

                        firstCondition = firstCondition + " (" + itemXpo.CodeDescription + ")"

                        'Se une todo el mensaje
                        .ConditionName = firstCondition

                        'El grupo de liquidacion
                        .LiquidationType = LiquidationTypePopupFirstCondition

                        'Se le pega a la variable el tipo de liquidacion
                        rateName = INDsleLiquidationTypePopupFirstCondition.Text
                        'Grupo de liquidacion
                        If INDlyItemSalesValuePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            .ManualType = ManualTypePopupFirstCondition
                            .SalesValue = SalesValuePopupFirstCondition
                            .SalesValueWithSurcharge = SalesValueWithSurchargePopupFirstCondition

                            rateName = rateName + " - " + INDsleManualTypePopupFirstCondition.Text + " - " + SalesValuePopupFirstCondition.ToString()
                        Else
                            .ManualType = Nothing
                            .SalesValue = Nothing
                            .SalesValueWithSurcharge = Nothing
                        End If

                        .RateManualValidityId = Nothing
                        .RateManualValidityDescription = String.Empty
                        .RateManualId = Nothing
                        .RateManualDescription = String.Empty
                        .RateVariation = Nothing

                        If INDlyItemRateManualValidityPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            .RateManualValidityId = RateManualValidityIdPopupFirstCondition
                            .RateManualValidityDescription = INDsleRateManualValidityPopupFirstCondition.Text
                            .RateVariation = RateVariationPopupFirstCondition

                            rateName = rateName + " - " + INDsleRateManualValidityPopupFirstCondition.Text + " - " + RateVariationPopupFirstCondition.ToString + "%"
                        End If

                        If INDlyItemRateManualPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            .RateManualId = RateManualIdPopupFirstCondition
                            .RateManualDescription = INDsleRateManualPopupFirstCondition.Text
                            .RateVariation = RateVariationPopupFirstCondition

                            rateName = rateName + " - " + INDsleRateManualPopupFirstCondition.Text + " - " + RateVariationPopupFirstCondition.ToString + "%"
                        End If
                        .RateName = rateName

                        If .Id > 0 Then
                            .MarkAsModified()
                        End If
                    End With

                    ListDefinitionRateDetailCondition.Add(definitionRateDetailCondition)
                Next
            Case 6 'RIAS
                For Each itemXpo As Infrastructure.Data.Xpo.CrystalRepository.RIASXpo In (From l In RIASPopupFirstContidionXpo Where l.SelectOption = True Select l).ToList
                    Dim firstCondition As String = String.Empty
                    Dim rateName As String = String.Empty
                    definitionRateDetailCondition = New DefinitionRateDetailCondition
                    With definitionRateDetailCondition
                        'Primera condicion
                        .Operator = 1

                        'Le pegamos el operador al texto
                        firstCondition = "="
                        'Especialidad
                        .RIASId = itemXpo.ID
                        .RIASDescriptionFirst = itemXpo.CodeName

                        firstCondition = firstCondition + " (" + itemXpo.CodeName + ")"

                        'Se une todo el mensaje
                        .ConditionName = firstCondition

                        'El grupo de liquidacion
                        .LiquidationType = LiquidationTypePopupFirstCondition

                        'Se le pega a la variable el tipo de liquidacion
                        rateName = INDsleLiquidationTypePopupFirstCondition.Text
                        'Grupo de liquidacion
                        If INDlyItemSalesValuePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            .ManualType = ManualTypePopupFirstCondition
                            .SalesValue = SalesValuePopupFirstCondition
                            .SalesValueWithSurcharge = SalesValueWithSurchargePopupFirstCondition

                            rateName = rateName + " - " + INDsleManualTypePopupFirstCondition.Text + " - " + SalesValuePopupFirstCondition.ToString()
                        Else
                            .ManualType = Nothing
                            .SalesValue = Nothing
                            .SalesValueWithSurcharge = Nothing
                        End If

                        .RateManualValidityId = Nothing
                        .RateManualValidityDescription = String.Empty
                        .RateManualId = Nothing
                        .RateManualDescription = String.Empty
                        .RateVariation = Nothing

                        If INDlyItemRateManualValidityPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            .RateManualValidityId = RateManualValidityIdPopupFirstCondition
                            .RateManualValidityDescription = INDsleRateManualValidityPopupFirstCondition.Text
                            .RateVariation = RateVariationPopupFirstCondition

                            rateName = rateName + " - " + INDsleRateManualValidityPopupFirstCondition.Text + " - " + RateVariationPopupFirstCondition.ToString + "%"
                        End If

                        If INDlyItemRateManualPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            .RateManualId = RateManualIdPopupFirstCondition
                            .RateManualDescription = INDsleRateManualPopupFirstCondition.Text
                            .RateVariation = RateVariationPopupFirstCondition

                            rateName = rateName + " - " + INDsleRateManualPopupFirstCondition.Text + " - " + RateVariationPopupFirstCondition.ToString + "%"
                        End If
                        .RateName = rateName

                        If .Id > 0 Then
                            .MarkAsModified()
                        End If
                    End With

                    ListDefinitionRateDetailCondition.Add(definitionRateDetailCondition)
                Next
            Case 7 'Description
                For Each itemXpo As Infrastructure.Data.Xpo.ContractRepository.ContractDescriptionsXpo In (From l In DescriptionPopupFirstConditionXpo Where l.SelectOption = True Select l).ToList
                    Dim firstCondition As String = String.Empty
                    Dim rateName As String = String.Empty
                    definitionRateDetailCondition = New DefinitionRateDetailCondition
                    With definitionRateDetailCondition
                        'Primera condicion
                        .Operator = 1

                        'Le pegamos el operador al texto
                        firstCondition = "="
                        'Description
                        .ContractDescriptionId = itemXpo.Id
                        .DescriptionCodeNameFirst = itemXpo.CodeName

                        firstCondition = firstCondition + " (" + itemXpo.CodeName + ")"

                        'Se une todo el mensaje
                        .ConditionName = firstCondition

                        'El grupo de liquidacion
                        .LiquidationType = LiquidationTypePopupFirstCondition

                        'Se le pega a la variable el tipo de liquidacion
                        rateName = INDsleLiquidationTypePopupFirstCondition.Text
                        'Grupo de liquidacion
                        If INDlyItemSalesValuePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            .ManualType = ManualTypePopupFirstCondition
                            .SalesValue = SalesValuePopupFirstCondition
                            .SalesValueWithSurcharge = SalesValueWithSurchargePopupFirstCondition

                            rateName = rateName + " - " + INDsleManualTypePopupFirstCondition.Text + " - " + SalesValuePopupFirstCondition.ToString()
                        Else
                            .ManualType = Nothing
                            .SalesValue = Nothing
                            .SalesValueWithSurcharge = Nothing
                        End If

                        .RateManualValidityId = Nothing
                        .RateManualValidityDescription = String.Empty
                        .RateManualId = Nothing
                        .RateManualDescription = String.Empty
                        .RateVariation = Nothing

                        If INDlyItemRateManualValidityPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            .RateManualValidityId = RateManualValidityIdPopupFirstCondition
                            .RateManualValidityDescription = INDsleRateManualValidityPopupFirstCondition.Text
                            .RateVariation = RateVariationPopupFirstCondition

                            rateName = rateName + " - " + INDsleRateManualValidityPopupFirstCondition.Text + " - " + RateVariationPopupFirstCondition.ToString + "%"
                        End If

                        If INDlyItemRateManualPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            .RateManualId = RateManualIdPopupFirstCondition
                            .RateManualDescription = INDsleRateManualPopupFirstCondition.Text
                            .RateVariation = RateVariationPopupFirstCondition

                            rateName = rateName + " - " + INDsleRateManualPopupFirstCondition.Text + " - " + RateVariationPopupFirstCondition.ToString + "%"
                        End If
                        .RateName = rateName

                        If .Id > 0 Then
                            .MarkAsModified()
                        End If
                    End With

                    ListDefinitionRateDetailCondition.Add(definitionRateDetailCondition)
                Next
        End Select
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopupFirstCondition() As Boolean
        Dim listErrors As New StringBuilder

        'Se valida los campos de la primera condicion
        If INDlyItemStartTimePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If StartTimePopupFirstCondition Is Nothing Then
                listErrors.AppendLine("Debe ingresar una hora inicial.")
            End If
        End If

        If INDlyItemEndTimePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If EndTimePopupFirstCondition Is Nothing Then
                listErrors.AppendLine("Debe ingresar una hora final.")
            End If
        End If

        If INDlyItemSpecialtyPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso Not modeEditGridRates Then
            If SpecialtyPopupFirstConditionXpo Is Nothing OrElse SpecialtyPopupFirstConditionXpo.Count = 0 Then
                listErrors.AppendLine("Debe elegir un item de especialidad.")
            Else
                Dim cont = (From l In SpecialtyPopupFirstConditionXpo Where l.SelectOption = True Select l).Count
                If cont = 0 Then
                    listErrors.AppendLine("Debe elegir un item de especialidad.")
                End If
            End If
        End If

        If INDlyItemFunctionalUnitPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso Not modeEditGridRates Then
            If FunctionalUnitPopupFirstConditionXpo Is Nothing OrElse FunctionalUnitPopupFirstConditionXpo.Count = 0 Then
                listErrors.AppendLine("Debe elegir un item de unidad funcional.")
            Else
                Dim cont = (From l In FunctionalUnitPopupFirstConditionXpo Where l.SelectOption = True Select l).Count
                If cont = 0 Then
                    listErrors.AppendLine("Debe elegir un item de unidad funcional.")
                End If
            End If
        End If

        If INDlyItemUnitTypePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If UnitTypeIdPopupFirstCondition Is Nothing Then
                listErrors.AppendLine("Debe ingresar un tipo de unidad.")
            End If
        End If

        If INDlyItemPceRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso modeEditGridRates = False Then
            If RIASPopupFirstContidionXpo Is Nothing OrElse RIASPopupFirstContidionXpo.Count = 0 Then
                listErrors.AppendLine("Debe elegir un item de RIAS.")
            Else
                Dim cont = (From l In RIASPopupFirstContidionXpo Where l.SelectOption = True Select l).Count
                If cont = 0 Then
                    listErrors.AppendLine("Debe elegir un item de RIAS.")
                End If
            End If
        End If

        If INDlyItemPceDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso modeEditGridRates = False Then
            If DescriptionPopupFirstConditionXpo Is Nothing OrElse DescriptionPopupFirstConditionXpo.Count = 0 Then
                listErrors.AppendLine("Debe elegir un item de Descripción.")
            Else
                Dim cont = (From l In DescriptionPopupFirstConditionXpo Where l.SelectOption = True Select l).Count
                If cont = 0 Then
                    listErrors.AppendLine("Debe elegir un item de Descripción.")
                End If
            End If
        End If

        'Se valida los controles del grupo de tipo de liquidación
        If LiquidationTypePopupFirstCondition Is Nothing Then
            listErrors.AppendLine("Debe ingresar un tipo de liquidación.")
        End If

        If INDlyItemManualTypePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ManualTypePopupFirstCondition Is Nothing Then
                listErrors.AppendLine("Debe ingresar un tipo de manual.")
            End If
        End If

        If INDlyItemSalesValuePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If SalesValuePopupFirstCondition = 0 Then
                listErrors.AppendLine("Debe ingresar un valor de servicio.")
            End If
        End If

        If INDlyItemSalesValueWithSurchargePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If SalesValueWithSurchargePopupFirstCondition = 0 Then
                listErrors.AppendLine("Debe ingresar un valor con recargo.")
            End If
        End If

        If INDlyItemRateManualValidityPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If RateManualValidityIdPopupFirstCondition Is Nothing Then
                listErrors.AppendLine("Debe ingresar una vigencia de manual tarifario.")
            End If
        End If

        If INDlyItemRateManualPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If RateManualIdPopupFirstCondition Is Nothing Then
                listErrors.AppendLine("Debe ingresar manual tarifario.")
            End If
        End If

        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Valida el listado que llena el datasource de la rejilla
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateListPopupFirsCondition() As ActionResult
        Dim count As Integer = 0
        Dim message As New StringBuilder
        Select Case ConditionType
            Case 1 'Horario
                count = (From l In ListDefinitionRateDetailCondition
                         Where ((StartTimePopupFirstCondition >= l.StartTime AndAlso StartTimePopupFirstCondition <= l.EndTime) _
                             OrElse (EndTimePopupFirstCondition >= l.StartTime AndAlso EndTimePopupFirstCondition <= l.EndTime) _
                             OrElse (StartTimePopupFirstCondition < l.StartTime) AndAlso (EndTimePopupFirstCondition > l.EndTime))).Count
                message.AppendLine("El horario " + StartTimePopupFirstCondition.ToString + " - " + EndTimePopupFirstCondition.ToString + " ya existe en la lista.")
            Case 2 'Especialidad
                Dim ListXpo = (From s In SpecialtyPopupFirstConditionXpo Where s.SelectOption = True Select s).ToList
                For Each itemXpo As Infrastructure.Data.Xpo.CrystalRepository.SpecialtyXpo In ListXpo
                    count = (From l In ListDefinitionRateDetailCondition Where l.SpecialtyId = itemXpo.CODESPECI Select l).Count
                    If count > 0 Then
                        message.AppendLine("La especialidad " + itemXpo.CodeName + " ya existe en la lista.")
                    End If
                Next
                If message.Length > 0 Then
                    count = 1
                End If
            Case 3 'Unidad Funcional
                Dim ListXpo = (From s In FunctionalUnitPopupFirstConditionXpo Where s.SelectOption = True Select s).ToList
                For Each itemXpo As Infrastructure.Data.Xpo.PayrollRepository.PayrollFunctionalUnit In ListXpo
                    count = (From l In ListDefinitionRateDetailCondition Where l.FunctionalUnitId = itemXpo.Id Select l).Count
                    If count > 0 Then
                        message.AppendLine("La unidad funcional " + itemXpo.CodeDescription + " ya existe en la lista.")
                    End If
                Next
                If message.Length > 0 Then
                    count = 1
                End If
            Case 4 'Tipo Unidad
                count = (From l In ListDefinitionRateDetailCondition Where l.UnitTypeId = UnitTypeIdPopupFirstCondition Select l).Count
                message.AppendLine("El tipo de unidad " + INDsleUnitTypeFirst.Text + " ya existe en la lista.")

            Case 6 'RIAS
                Dim ListXpo = (From s In RIASPopupFirstContidionXpo Where s.SelectOption = True Select s).ToList
                For Each itemXpo As Infrastructure.Data.Xpo.CrystalRepository.RIASXpo In ListXpo
                    count = (From l In ListDefinitionRateDetailCondition Where l.RIASId = itemXpo.ID Select l).Count
                    If count > 0 Then
                        message.AppendLine("La RIAS " + itemXpo.CodeName + " ya existe en la lista.")
                    End If
                Next
                If message.Length > 0 Then
                    count = 1
                End If

            Case 7 'Description
                Dim ListXpo = (From s In DescriptionPopupFirstConditionXpo Where s.SelectOption = True Select s).ToList
                For Each itemXpo As Infrastructure.Data.Xpo.ContractRepository.ContractDescriptionsXpo In ListXpo
                    count = (From l In ListDefinitionRateDetailCondition Where l.ContractDescriptionId = itemXpo.Id Select l).Count
                    If count > 0 Then
                        message.AppendLine("La Descripción " + itemXpo.CodeName + " ya existe en la lista.")
                    End If
                Next
                If message.Length > 0 Then
                    count = 1
                End If
        End Select

        If count > 0 Then
            Return New ActionResult With {.StateResult = False, .Message = message.ToString}
        End If
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Edita la tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditRateWithTwoConditions()
        modeEditGridRates = True
        definitionRateDetailCondition = viewRates.GetFocusedRow
        INDbtnAddRatePopup.Text = ResourceManager.GetString("Edit")

        With definitionRateDetailCondition
            OperatorFirst = .Operator
            If .StartTime IsNot Nothing Then
                StartTimeFirst = .StartTime
                EndTimeFirst = .EndTime
            End If
            If .SpecialtyId IsNot Nothing Then
                SpecialtyIdFirst = .SpecialtyId
                INDsleSpecialtyFirst.Properties.NullText = .SpecialtyDescriptionFirst
            End If
            If .FunctionalUnitId IsNot Nothing Then
                FunctionalUnitIdFirst = .FunctionalUnitId
                INDsleFunctionalUnitFirst.Properties.NullText = .FunctionalUnitDescriptionFirst
            End If
            If .UnitTypeId IsNot Nothing Then
                UnitTypeIdFirst = .UnitTypeId
            End If
            If .RIASId IsNot Nothing Then
                INDsleRIASFirst.EditValue = .RIASId
                INDsleRIASFirst.Properties.NullText = .RIASDescriptionFirst
            End If
            If .ContractDescriptionId IsNot Nothing Then
                INDsleDescriptionFirst.EditValue = .ContractDescriptionId
                INDsleDescriptionFirst.Properties.NullText = .DescriptionCodeNameFirst
            End If
            If .Operator2 IsNot Nothing Then
                OperatorSecond = .Operator2
            End If
            If .StartTime2 IsNot Nothing Then
                StartTimeSecond = .StartTime2
            End If
            If .EndTime2 IsNot Nothing Then
                EndTimeSecond = .EndTime2
            End If
            If .SpecialtyId2 IsNot Nothing Then
                SpecialtyIdSecond = .SpecialtyId2
                INDsleSpecialtySecond.Properties.NullText = .SpecialtyDescriptionSecond
            End If
            If .FunctionalUnitId2 IsNot Nothing Then
                FunctionalUnitIdSecond = .FunctionalUnitId2
                INDsleFunctionalUnitSecond.Properties.NullText = .FunctionalUnitDescriptionSecond
            End If
            If .UnitTypeId2 IsNot Nothing Then
                UnitTypeIdSecond = .UnitTypeId2
            End If
            If .RIASId2 IsNot Nothing Then
                INDsleRIASSecond.EditValue = .RIASId2
                INDsleRIASSecond.Properties.NullText = .RIASDescriptionSecond
            End If
            If .ContractDescriptionId2 IsNot Nothing Then
                INDsleDescriptionSecond.EditValue = .ContractDescriptionId2
                INDsleDescriptionSecond.Properties.NullText = .DescriptionCodeNameSecond
            End If
            LiquidationTypePopup = .LiquidationType
            If .SalesValue IsNot Nothing Then
                ManualTypePopup = .ManualType
                SalesValuePopup = .SalesValue
                SalesValueWithSurchargePopup = .SalesValueWithSurcharge
            End If
            If .RateManualValidityId IsNot Nothing Then
                RateManualValidityIdPopup = .RateManualValidityId
                INDsleRateManualValidityPopup.Properties.NullText = .RateManualValidityDescription
                RateVariationPopup = .RateVariation
            End If
            If .RateManualId IsNot Nothing Then
                RateManualIdPopup = .RateManualId
                INDsleRateManualPopup.Properties.NullText = .RateManualDescription
                RateVariationPopup = If(.RateVariation, 0)
            End If
        End With

        'Se bloquea los controles cuando se edita
        'Primera Condicion
        INDdteStartTimeFirst.Properties.ReadOnly = True
        INDdteEndTimeFirst.Properties.ReadOnly = True
        INDsleSpecialtyFirst.Properties.ReadOnly = True
        INDsleFunctionalUnitFirst.Properties.ReadOnly = True
        INDsleUnitTypeFirst.Properties.ReadOnly = True
        INDsleRIASFirst.Properties.ReadOnly = True
        INDsleDescriptionFirst.Properties.ReadOnly = True
        INDsleOperatorFirst.Properties.ReadOnly = True
        'Segunda Condicion
        INDdteStartTimeSecond.Properties.ReadOnly = True
        INDdteEndTimeSecond.Properties.ReadOnly = True
        INDsleSpecialtySecond.Properties.ReadOnly = True
        INDsleFunctionalUnitSecond.Properties.ReadOnly = True
        INDsleUnitTypeSecond.Properties.ReadOnly = True
        INDsleRIASSecond.Properties.ReadOnly = True
        INDsleDescriptionSecond.Properties.ReadOnly = True
        INDsleOperatorSecond.Properties.ReadOnly = True

        INDpceRates.ShowPopup()
    End Sub

    ''' <summary>
    ''' Edita la tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditRateWithOneCondition()
        modeEditGridRates = True
        definitionRateDetailCondition = viewRates.GetFocusedRow()
        INDbtnAddFirstCondition.Text = ResourceManager.GetString("Edit")

        With definitionRateDetailCondition
            'Una condicion
            If .StartTime IsNot Nothing Then
                StartTimePopupFirstCondition = .StartTime
                EndTimePopupFirstCondition = .EndTime
            End If
            If .SpecialtyId IsNot Nothing Then
                INDsleSpecialtyPopupFirstCondition.Text = "1 item seleccionado"
            End If
            If .FunctionalUnitId IsNot Nothing Then
                INDsleFunctionalUnitPopupFirstCondition.Text = "1 item seleccionado"
            End If
            If .UnitTypeId IsNot Nothing Then
                UnitTypeIdPopupFirstCondition = .UnitTypeId
            End If
            If .RIASId IsNot Nothing Then
                INDpceRIASFirstCondition.Text = "1 item seleccionado"
            End If
            If .ContractDescriptionId IsNot Nothing Then
                INDpceDescriptionsFirstConditions.Text = "1 item seleccionado"
            End If
            'Grupo liquidacion
            LiquidationTypePopupFirstCondition = .LiquidationType
            If .SalesValue IsNot Nothing Then
                ManualTypePopupFirstCondition = .ManualType
                SalesValuePopupFirstCondition = .SalesValue
                SalesValueWithSurchargePopupFirstCondition = .SalesValueWithSurcharge
            End If
            If .RateManualValidityId IsNot Nothing Then
                RateManualValidityIdPopupFirstCondition = .RateManualValidityId
                INDsleRateManualValidityPopupFirstCondition.Properties.NullText = .RateManualValidityDescription
                RateVariationPopupFirstCondition = .RateVariation
            End If
            If .RateManualId IsNot Nothing Then
                RateManualIdPopupFirstCondition = .RateManualId
                INDsleRateManualPopupFirstCondition.Properties.NullText = .RateManualDescription
                RateVariationPopupFirstCondition = If(.RateVariation, 0)
            End If
        End With

        'Se bloquea los controles cuando se edita
        'Primera Condicion
        INDdteStartTimePopupFirstCondition.Properties.ReadOnly = True
        INDdteEndTimePopupFirstCondition.Properties.ReadOnly = True
        INDsleSpecialtyPopupFirstCondition.Properties.ReadOnly = True
        INDsleFunctionalUnitPopupFirstCondition.Properties.ReadOnly = True
        INDsleUnitTypePopupFirstCondition.Properties.ReadOnly = True
        INDpceRIASFirstCondition.Properties.ReadOnly = True
        INDpceDescriptionsFirstConditions.Properties.ReadOnly = True

        INDpceRates.ShowPopup()
    End Sub

    ''' <summary>
    ''' Elimina la tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteRateWithConditions()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim detail As DefinitionRateDetailCondition = viewRates.GetFocusedRow
        If detail.Id > 0 Then
            If ListDeleteDefinitionRateDetailCondition Is Nothing Then
                ListDeleteDefinitionRateDetailCondition = New List(Of DefinitionRateDetailCondition)
            End If
            detail.MarkAsDeleted()
            ListDeleteDefinitionRateDetailCondition.Add(detail)
        End If
        ListDefinitionRateDetailCondition.Remove(detail)
        INDgcRates.DataSource = Nothing
        INDgcRates.DataSource = ListDefinitionRateDetailCondition
        'Se desbloquea los controles del form de cond1, cond2 y logicOperator
        If ListDefinitionRateDetailCondition Is Nothing OrElse ListDefinitionRateDetailCondition.Count = 0 Then
            INDsleConditionType.Properties.ReadOnly = False
            INDsleLogicOperator.Properties.ReadOnly = False
            INDsleConditionTypeSecond.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que coloca el foco en el campo indicado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FocusControlPopup()
        Dim controlPopup = INDpceRates.Properties.PopupControl

        If controlPopup.Name = "INDpopupFirstCondition" Then
            Select Case True
                Case INDlyItemStartTimePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDdteStartTimePopupFirstCondition.Focus()
                Case INDlyItemSpecialtyPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDsleSpecialtyPopupFirstCondition.Focus()
                Case INDlyItemFunctionalUnitPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDsleFunctionalUnitPopupFirstCondition.Focus()
                Case INDlyItemUnitTypePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDsleUnitTypePopupFirstCondition.Focus()
                Case INDlyItemPceRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDpceRIASFirstCondition.Focus()
                Case INDlyItemPceDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDpceDescriptionsFirstConditions.Focus()
            End Select
        Else
            INDsleOperatorFirst.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que muestra o oculta los campos del segmento de liquidación segun la condicion
    ''' </summary>
    Private Sub SetVisibilityLiquidationSegments(Optional _liquidationType As Integer? = Nothing)

        INDlyItemManualType.HideLayout()
        INDlyItemRateManual.HideLayout()
        INDlyItemSalesValue.HideLayout()
        INDlyItemSalesValueWithSurcharge.HideLayout()
        INDlyItemRateVariation.HideLayout()
        INDlyItemRateManualValidity.HideLayout()

        ManualType = Nothing
        RateManualId = Nothing
        SalesValue = 0
        SalesValueWithSurcharge = 0
        RateManualValidityId = Nothing
        RateVariation = 0

        HideControlsIva()

        If _liquidationType = 1 Then 'Fija 
            INDlyItemRateManual.ShowLayout()
            INDlyItemRateManual.ShowInCustomizationForm = False

            INDlyItemSalesValue.ShowLayout()
            INDlyItemSalesValueWithSurcharge.ShowLayout()

            ValidationsBeforeCalculateValues()

        ElseIf _liquidationType = 2 Then 'Estandar
            INDlyItemRateManual.ShowLayout()
            INDlyItemRateManual.ShowInCustomizationForm = False

            INDlyItemRateVariation.ShowLayout()

        ElseIf _liquidationType = 3 Then 'Vigencia
            INDlyItemRateManualValidity.ShowLayout()
            INDlyItemRateManualValidity.ShowInCustomizationForm = False
            INDlyItemRateVariation.ShowLayout()

        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListManualType = Nothing
        ListRulesType = Nothing
        ListConditionType = Nothing
        ListConditionTypeSecond = Nothing
        ListLogicOperator = Nothing
        ListUnitType = Nothing
        ListLiquidationType = Nothing
        ListEqualsOperator = Nothing
        ListXpCollection = Nothing
        Presenter = Nothing
        controlerEditValueChanged = Nothing
        ListDefinitionRateDetailCondition = Nothing
        ListDeleteDefinitionRateDetailCondition = Nothing
        definitionRateDetailCondition = Nothing
        modeEditGridRates = Nothing
        ListValidate = Nothing
        viewNameFocus = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmAddRule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyAddRule, True)
        IndigoGridControl1.RefreshGrid(INDgcRates)
        IndigoGridControl1.RefreshGrid(INDgcControlsRuleType)
        Presenter = New PAddRule(Me)

        Await GetCompanySettings()
        SetActions()
        InitializeTuples()

        Deshacer()
        If ModeEdit Then
            Await LoadControls()
        End If
        FormatNumber = Me.indigo.CurrencyNumbertFormat
        changeNumericFormatByCurrency(FormatNumber)
    End Sub

    ''' <summary>
    ''' method to block the popup when the popup is in async mode
    ''' </summary>
    ''' <param name="value"></param>
    Private Sub AsyncLoaderPopUp(value As Boolean)
        If value Then
            Me.Cursor = BaseClass.ChangeCursorIndigo()
        Else
            Me.Cursor = DefaultCursor()
        End If
        Me.INDPanelControlBase.Enabled = Not value
    End Sub

    Private Sub SetActions()
        IndigoGridView1.SetListAcction(viewRates, {eAcciones.Edit, eAcciones.Remove}.ToList())

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewRates.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddRule_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDsleRuleType.Enabled Then
            INDsleRuleType.Focus()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de liquidacion del popup de una condicion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLiquidationTypePopupFirstCondition_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleLiquidationTypePopupFirstCondition.EditValueChanged
        If LiquidationTypePopupFirstCondition IsNot Nothing Then
            If LiquidationTypePopupFirstCondition = 1 Then 'Fija
                INDlyItemManualTypePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemSalesValuePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemSalesValueWithSurchargePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                INDlyItemRateManualValidityPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemRateManualPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemRateVariationPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf LiquidationTypePopupFirstCondition = 2 Then 'Estandar
                INDlyItemManualTypePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSalesValuePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSalesValueWithSurchargePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                INDlyItemRateManualValidityPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemRateManualPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemRateVariationPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            ElseIf LiquidationTypePopupFirstCondition = 3 Then 'Vigencia
                INDlyItemManualTypePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSalesValuePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSalesValueWithSurchargePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                INDlyItemRateManualValidityPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemRateManualPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemRateVariationPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        Else
            INDlyItemManualTypePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemSalesValuePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemSalesValueWithSurchargePopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemRateManualValidityPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemRateManualPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemRateVariationPopupFirstCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del tipo de regla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleRuleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRuleType.EditValueChanged
        If RuleType IsNot Nothing AndAlso Not controlerEditValueChanged Then

            AsyncLoaderPopUp(True)
            IPSServiceId = Nothing
            INDgcSurgicalProcedures.DataSource = Nothing
            INDlyItemSurgicalProcedures.HideLayout()
            HideColumnsOfGridControlsRuleType()
            Await ChargueDatasourceRuleType()
            If RuleType = 1 Then ValidationsBeforeCalculateValues()
            AsyncLoaderPopUp(False)

        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de condición la primera
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConditionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleConditionType.EditValueChanged
        If ConditionType IsNot Nothing Then
            INDlygLiquidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            LiquidationType = Nothing

            If ConditionType = 5 Then 'Igual a Ninguna

                ValidationsBeforeCalculateValues()
                LogicOperator = 1
                INDsleLogicOperator.Properties.ReadOnly = True
                INDlygRate.HideControl()
                INDlyItemLiquidationType.ShowLayout()
                INDlyItemRateManual.HideLayout()

            Else 'Diferente a ninguna

                INDlygLiquidation.HideControl()
                INDsleLogicOperator.Properties.ReadOnly = False
                INDlygRate.HideControl(False)
                INDlyItemLiquidationType.HideLayout()
            End If

            'Se oculta o muestra los controles de la primera condicion del popup
            HideControlsPopupFirstCondition()
            ConditionTypeSecond = 0
        End If

        HideControls()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de condición la segunda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConditionTypeSecond_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleConditionTypeSecond.EditValueChanged
        If ConditionTypeSecond IsNot Nothing Then
            HideControlsPopupSecondCondition()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de operador lógico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLogicOperator_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleLogicOperator.EditValueChanged
        HideControlsPopupFirstCondition()
        HideControls()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de liquidacion del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLiquidationTypePopup_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleLiquidationTypePopup.EditValueChanged
        If LiquidationTypePopup IsNot Nothing Then
            If LiquidationTypePopup = 1 Then 'Fija
                INDlyItemManualTypePopup.ShowLayout()
                INDlyItemSalesValuePopup.ShowLayout()
                INDlyItemSalesValueWithSurchargePopup.ShowLayout()

                INDlyItemRateManualValidityPopup.HideLayout()
                INDlyItemRateManualPopup.HideLayout()
                INDlyItemRateVariationPopup.HideLayout()
            ElseIf LiquidationTypePopup = 2 Then 'Estandar
                INDlyItemManualTypePopup.HideLayout()
                INDlyItemSalesValuePopup.HideLayout()
                INDlyItemSalesValueWithSurchargePopup.HideLayout()

                INDlyItemRateManualValidityPopup.HideLayout()
                INDlyItemRateManualPopup.ShowLayout()
                INDlyItemRateVariationPopup.ShowLayout()
            ElseIf LiquidationTypePopup = 3 Then 'Vigencia
                INDlyItemManualTypePopup.HideLayout()
                INDlyItemSalesValuePopup.HideLayout()
                INDlyItemSalesValueWithSurchargePopup.HideLayout()

                INDlyItemRateManualValidityPopup.ShowLayout()
                INDlyItemRateManualPopup.HideLayout()
                INDlyItemRateVariationPopup.ShowLayout()
            End If
        Else
            INDlyItemManualTypePopup.HideLayout()
            INDlyItemSalesValuePopup.HideLayout()
            INDlyItemSalesValueWithSurchargePopup.HideLayout()
            INDlyItemRateManualValidityPopup.HideLayout()
            INDlyItemRateManualPopup.HideLayout()
            INDlyItemRateVariationPopup.HideLayout()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de liquidacion del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLiquidationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleLiquidationType.EditValueChanged
        If LiquidationType IsNot Nothing Then
            SetVisibilityLiquidationSegments(LiquidationType)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control valor del servicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtSalesValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtSalesValue.EditValueChanging, INDtxtSalesValue.EditValueChanged
        If SalesValue > 0 Then
            ValidationsBeforeCalculateValues()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control Manual tarifario
    ''' </summary>
    Private Sub INDsleRateManual_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRateManual.EditValueChanged
        Dim obj = INDsleRateManual.GetFocusedObject(Of RateManualXpo)()

        If obj IsNot Nothing Then
            ManualType = obj.Type
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control del check de la rejilla de itemx qx
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepCheckSurgicalProcedures_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSurgicalProcedures.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Dim item = INDviewSurgicalProcedures.GetFocusedRow()
            item.SelectOption = e.NewValue
            Dim cont = (From x In CType(INDgcSurgicalProcedures.DataSource, List(Of SurgicalProcedureServiceXpo)) Where x.SelectOption = True Select x).Count
            INDpceSurgicalProcedures.Text = cont.ToString + " Item seleccionado"

            If cont = CType(INDgcSurgicalProcedures.DataSource, List(Of SurgicalProcedureServiceXpo)).Count Then
                Me.INDcolSelSurgicalProcedures.Image = Global.Presentation.Contract.My.Resources.Resources.check
            Else
                Me.INDcolSelSurgicalProcedures.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de check de las descripciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepCheckDescriptions_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckDescriptions.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Dim item = INDviewDescriptions.GetFocusedRow()
            item.SelectOption = e.NewValue
            Dim cont = (From x In DescriptionPopupFirstConditionXpo Where x.SelectOption = True Select x).Count
            INDpceDescriptionsFirstConditions.Text = cont.ToString + " item seleccionado"

            If cont = DescriptionPopupFirstConditionXpo.Count Then
                Me.INDcolSelDescriptions.Image = Global.Presentation.Contract.My.Resources.Resources.check
            Else
                Me.INDcolSelDescriptions.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de check de los rias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepCheckRIAS_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckRIAS.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Dim item = viewRIAS.GetFocusedRow()
            item.SelectOption = e.NewValue
            Dim cont = (From x In RIASPopupFirstContidionXpo Where x.SelectOption = True Select x).Count
            INDpceRIASFirstCondition.Text = cont.ToString + " item seleccionado"

            If cont = RIASPopupFirstContidionXpo.Count Then
                Me.INDcolSelRIAS.Image = Global.Presentation.Contract.My.Resources.Resources.check
            Else
                Me.INDcolSelRIAS.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de check de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepCheckControlsLiquidationType_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckControlsLiquidationType.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Dim item = viewControlsRuleType.GetFocusedRow()

            If item.GetType() = GetType(ViewListIPSServiceWithHomologationsXpo) Then
                If (From x In ListXpCollection Where x.SelectOption = True AndAlso x.Presentation = 2 Select x).Count > 0 AndAlso e.NewValue = True AndAlso item.Presentation = 2 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede seleccionar mas de un item Qx"
                    e.Cancel = True
                    Exit Sub
                End If

                If item.Presentation = 2 AndAlso e.NewValue = True Then
                    IPSServiceId = item.Id
                    INDpceSurgicalProcedures.Text = "0 Item Seleccionados"
                    INDlyItemSurgicalProcedures.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                ElseIf item.Presentation = 2 AndAlso e.NewValue = False Then
                    IPSServiceId = Nothing
                    INDgcSurgicalProcedures.DataSource = Nothing
                    INDlyItemSurgicalProcedures.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            End If

            item.SelectOption = e.NewValue
            Dim cont = (From x In ListXpCollection Where x.SelectOption = True Select x).Count
            INDpceControlRuleType.Text = cont.ToString + " item seleccionado"

            If cont > 1 Then
                HideControlsIva()
            Else
                ValidationsBeforeCalculateValues()
            End If

            If cont = ListXpCollection.Count Then
                Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.check
            Else
                Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de check de la rejilla de unidades funcionales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepCheckFunctionalUnit_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckFunctionalUnit.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Dim item = viewGridFunctionalUnit.GetFocusedRow()
            item.SelectOption = e.NewValue
            Dim cont = (From x In FunctionalUnitPopupFirstConditionXpo Where x.SelectOption = True Select x).Count
            INDsleFunctionalUnitPopupFirstCondition.Text = cont.ToString + " item seleccionado"

            If cont = FunctionalUnitPopupFirstConditionXpo.Count Then
                Me.INDcolSelectionOptionFU.Image = Global.Presentation.Contract.My.Resources.Resources.check
            Else
                Me.INDcolSelectionOptionFU.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de check de la rejilla de especialidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepCheckSpecialty_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSpecialty.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Dim item = viewGridSpecialty.GetFocusedRow()
            item.SelectOption = e.NewValue
            Dim cont = (From x In SpecialtyPopupFirstConditionXpo Where x.SelectOption = True Select x).Count
            INDsleSpecialtyPopupFirstCondition.Text = cont.ToString + " item seleccionado"

            If cont = SpecialtyPopupFirstConditionXpo.Count Then
                Me.INDcolSelectionOptionS.Image = Global.Presentation.Contract.My.Resources.Resources.check
            Else
                Me.INDcolSelectionOptionS.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

#End Region

#Region "MouseDoubleClick"

    ''' <summary>
    ''' Evento que se dispara al presionar dobleClick sobre la reijilla de itemx qx
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgcSurgicalProcedures_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcSurgicalProcedures.MouseDoubleClick
        If CType(INDgcSurgicalProcedures.DataSource, List(Of SurgicalProcedureServiceXpo)) IsNot Nothing AndAlso CType(INDgcSurgicalProcedures.DataSource, List(Of SurgicalProcedureServiceXpo)).Any() Then
            Dim hitPoint = Me.INDviewSurgicalProcedures.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelSurgicalProcedures") Then

                    Dim listFilterXpCollection = INDviewSurgicalProcedures.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In CType(INDgcSurgicalProcedures.DataSource, List(Of SurgicalProcedureServiceXpo)) Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDcolSelSurgicalProcedures.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        Me.INDcolSelSurgicalProcedures.Image = Global.Presentation.Contract.My.Resources.Resources.check
                    End If
                    Me.INDgcSurgicalProcedures.RefreshDataSource()
                    Dim contItems = (From x In CType(INDgcSurgicalProcedures.DataSource, List(Of SurgicalProcedureServiceXpo)) Where x.SelectOption = True Select x).Count
                    INDpceSurgicalProcedures.Text = contItems.ToString + " Item seleccionado"
                    Me.INDgcSurgicalProcedures.Invalidate()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar dobleclick sobre la rejilla de Descripciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgcDescriptions_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcDescriptions.MouseDoubleClick
        If DescriptionPopupFirstConditionXpo IsNot Nothing AndAlso DescriptionPopupFirstConditionXpo.Count > 0 Then
            Dim hitPoint = Me.INDviewDescriptions.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelDescriptions") Then

                    Dim listFilterXpCollection = INDviewDescriptions.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In DescriptionPopupFirstConditionXpo Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDcolSelDescriptions.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        Me.INDcolSelDescriptions.Image = Global.Presentation.Contract.My.Resources.Resources.check
                    End If
                    Me.INDgcDescriptions.RefreshDataSource()
                    Dim contItems = (From x In DescriptionPopupFirstConditionXpo Where x.SelectOption = True Select x).Count
                    INDpceDescriptionsFirstConditions.Text = contItems.ToString + " item seleccionado"
                    Me.INDgcDescriptions.Invalidate()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar dobleclick sobre la rejilla de RIAS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgcRIAS_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcRIAS.MouseDoubleClick
        If RIASPopupFirstContidionXpo IsNot Nothing AndAlso RIASPopupFirstContidionXpo.Count > 0 Then
            Dim hitPoint = Me.viewRIAS.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelRIAS") Then

                    Dim listFilterXpCollection = viewRIAS.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In RIASPopupFirstContidionXpo Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDcolSelRIAS.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        Me.INDcolSelRIAS.Image = Global.Presentation.Contract.My.Resources.Resources.check
                    End If
                    Me.INDgcRIAS.RefreshDataSource()
                    Dim contItems = (From x In RIASPopupFirstContidionXpo Where x.SelectOption = True Select x).Count
                    INDpceRIASFirstCondition.Text = contItems.ToString + " item seleccionado"
                    Me.INDgcRIAS.Invalidate()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar doble click sobre la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcControlsRuleType_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcControlsRuleType.MouseDoubleClick
        If ListXpCollection IsNot Nothing AndAlso ListXpCollection.Count > 0 Then
            Dim hitPoint = Me.viewControlsRuleType.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelectionOption") Then

                    Dim listFilterXpCollection = viewControlsRuleType.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        Me.INDcolSelectionOption.Image = Global.Presentation.Contract.My.Resources.Resources.check
                    End If
                    Me.INDgcControlsRuleType.RefreshDataSource()
                    Dim contItems = (From x In ListXpCollection Where x.SelectOption = True Select x).Count
                    INDpceControlRuleType.Text = contItems.ToString + " item seleccionado"
                    Me.INDgcControlsRuleType.Invalidate()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar doble click sobre la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcFunctionalUnitPopupFirstCondition_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcFunctionalUnitPopupFirstCondition.MouseDoubleClick
        If FunctionalUnitPopupFirstConditionXpo IsNot Nothing AndAlso FunctionalUnitPopupFirstConditionXpo.Count > 0 Then
            Dim hitPoint = Me.viewGridFunctionalUnit.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelectionOptionFU") Then

                    Dim listFilterXpCollection = viewGridFunctionalUnit.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In FunctionalUnitPopupFirstConditionXpo Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDcolSelectionOptionFU.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        Me.INDcolSelectionOptionFU.Image = Global.Presentation.Contract.My.Resources.Resources.check
                    End If
                    Me.INDgcFunctionalUnitPopupFirstCondition.RefreshDataSource()
                    Dim contItems = (From x In FunctionalUnitPopupFirstConditionXpo Where x.SelectOption = True Select x).Count
                    INDsleFunctionalUnitPopupFirstCondition.Text = contItems.ToString + " item seleccionado"
                    Me.INDgcFunctionalUnitPopupFirstCondition.Invalidate()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar docle click sobre la rejilla de especialidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcSpecialtyPopupFirstCondition_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcSpecialtyPopupFirstCondition.MouseDoubleClick
        If SpecialtyPopupFirstConditionXpo IsNot Nothing AndAlso SpecialtyPopupFirstConditionXpo.Count > 0 Then
            Dim hitPoint = Me.viewGridSpecialty.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelectionOptionS") Then

                    Dim listFilterXpCollection = viewGridSpecialty.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In SpecialtyPopupFirstConditionXpo Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDcolSelectionOptionS.Image = Global.Presentation.Contract.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        Me.INDcolSelectionOptionS.Image = Global.Presentation.Contract.My.Resources.Resources.check
                    End If
                    Me.INDgcSpecialtyPopupFirstCondition.RefreshDataSource()
                    Dim contItems = (From x In SpecialtyPopupFirstConditionXpo Where x.SelectOption = True Select x).Count
                    INDsleSpecialtyPopupFirstCondition.Text = contItems.ToString + " item seleccionado"
                    Me.INDgcSpecialtyPopupFirstCondition.Invalidate()
                End If
            End If
        End If
    End Sub

#End Region

#Region "PopupMenuShowing"

    ''' <summary>
    ''' Evento que se dispara al pintar el menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewDescriptions_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDviewDescriptions.PopupMenuShowing
        ShowMenuGridView(sender, e)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al pintar el menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub viewRIAS_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles viewRIAS.PopupMenuShowing
        ShowMenuGridView(sender, e)
    End Sub

    ''' <summary>
    ''' Evento que se dispara para pintar el menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewControlsRuleType_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles viewControlsRuleType.PopupMenuShowing
        ShowMenuGridView(sender, e)
    End Sub

    ''' <summary>
    ''' Evento que se dispara para pintar el menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewGridFunctionalUnit_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles viewGridFunctionalUnit.PopupMenuShowing
        ShowMenuGridView(sender, e)
    End Sub

    ''' <summary>
    ''' Evento que se dispara para pintar el menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewGridSpecialty_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles viewGridSpecialty.PopupMenuShowing
        ShowMenuGridView(sender, e)
    End Sub

    ''' <summary>
    ''' Metodo que despliega el menu en los diferentes gridControl
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowMenuGridView(sender As Object, e As PopupMenuShowingEventArgs)
        If e.HitInfo IsNot Nothing Then
            Dim view = CType(sender, GridView)
            viewNameFocus = view.Name
            INDbarButtonSelectAll.Caption = "Seleccionar"
            INDbarButtonUnSelectAll.Caption = "Quitar Selección"
            PopupMenuActions.Manager = BarManager
            PopupMenuActions.ShowPopup(view.GridControl.PointToScreen(e.Point))
        End If
    End Sub

#End Region

#Region "ItemClick"

    ''' <summary>
    ''' Selecciona
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbarButtonSelectAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonSelectAll.ItemClick
        Select Case viewNameFocus
            Case VIEW_CONTROLS
                SelectOptionsGridControls(1)
            Case VIEW_SPECIALTY
                SelectOptionsGridSpecialty(1)
            Case VIEW_FUNCTIONALUNIT
                SelectOptionsGridFunctionalUnit(1)
            Case VIEW_RIAS
                SelectOptionsGridRIAS(1)
            Case VIEW_DESCRIPTIONS
                SelectOptionsGridDescriptions(1)
        End Select
    End Sub

    ''' <summary>
    ''' Deselecciona
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbarButtonUnSelectAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonUnSelectAll.ItemClick
        Select Case viewNameFocus
            Case VIEW_CONTROLS
                SelectOptionsGridControls(0)
            Case VIEW_SPECIALTY
                SelectOptionsGridSpecialty(0)
            Case VIEW_FUNCTIONALUNIT
                SelectOptionsGridFunctionalUnit(0)
            Case VIEW_RIAS
                SelectOptionsGridRIAS(0)
            Case VIEW_DESCRIPTIONS
                SelectOptionsGridDescriptions(0)
        End Select
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddRule_Click(sender As Object, e As EventArgs) Handles INDbtnAddRule.Click
        AddRule()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton agregar del popup con 2 condiciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddRatePopup_Click(sender As Object, e As EventArgs) Handles INDbtnAddRatePopup.Click
        AddRateWithConditions()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del popup con una condicion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddFirstCondition_Click(sender As Object, e As EventArgs) Handles INDbtnAddFirstCondition.Click
        AddRatePopupFirstCondition()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddRule_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de tarifa base
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBaseRate_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            INDbtnAddRule.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter o f4 al popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceRates_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceRates.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceRates.ShowPopup()
        ElseIf e.KeyCode = Keys.Escape Then
            INDbtnAddRule.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de valor de recargo y % variacion del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtSalesValueWithSurchargePopup_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtSalesValueWithSurchargePopup.KeyDown, INDseRateVariationPopup.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDbtnAddRatePopup.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de valor de recargo y % variacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDseRateVariation_KeyDown(sender As Object, e As KeyEventArgs) Handles INDseRateVariation.KeyDown, INDtxtSalesValueWithSurcharge.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDbtnAddRule.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtSalesValueWithSurchargePopupFirstCondition_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtSalesValueWithSurchargePopupFirstCondition.KeyDown, INDseRateVariationPopupFirstCondition.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDbtnAddFirstCondition.Focus()
        End If
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al hacer popup sobre el control de la segunda condición
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConditionTypeSecond_Popup(sender As Object, e As EventArgs) Handles INDsleConditionTypeSecond.Popup
        If ConditionType IsNot Nothing Then
            viewSearchFirstCondition.ActiveFilterString = "Item1<>'" & ConditionType & "' And Item1<>5"
        Else
            viewSearchFirstCondition.ActiveFilterString = String.Empty
        End If
        viewSearchFirstCondition.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    End Sub

    ''' <summary>
    ''' Evento que se dispara al hacer el popup sobre el popupContainerEdit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceRates_Popup(sender As Object, e As EventArgs) Handles INDpceRates.Popup
        FocusControlPopup()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de servicios qx
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDpceSurgicalProcedures_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceSurgicalProcedures.QueryPopUp
        If INDgcSurgicalProcedures.DataSource Is Nothing AndAlso IPSServiceId > 0 Then
            INDgcSurgicalProcedures.DataSource = Await Presenter.ListSurgicalProcedures(IPSServiceId)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de descripción primera condición
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDescriptionFirst_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDescriptionFirst.QueryPopUp
        If INDsleDescriptionFirst.Properties.DataSource Is Nothing Then
            INDsleDescriptionFirst.Properties.DataSource = Presenter.ListDescriptions()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de rias primera condición
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRIASFirst_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRIASFirst.QueryPopUp
        If INDsleRIASFirst.Properties.DataSource Is Nothing Then
            INDsleRIASFirst.Properties.DataSource = Presenter.ListRIAS()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de descripción segunda condición
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDescriptionSecond_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDescriptionSecond.QueryPopUp
        If INDsleDescriptionSecond.Properties.DataSource Is Nothing Then
            INDsleDescriptionSecond.Properties.DataSource = Presenter.ListDescriptions()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de rias segunda condición
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRIASSecond_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRIASSecond.QueryPopUp
        If INDsleRIASSecond.Properties.DataSource Is Nothing Then
            INDsleRIASSecond.Properties.DataSource = Presenter.ListRIAS()
        End If
    End Sub

    ''' <summary>
    ''' Establece el datasource de los RIAS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDpceDescriptionsFirstConditions_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceDescriptionsFirstConditions.QueryPopUp
        If DescriptionPopupFirstConditionXpo Is Nothing Then
            AsyncLoaderPopUp(True)
            Await Presenter.ListDescriptionsXpCollection()
            AsyncLoaderPopUp(False)
        End If
    End Sub

    ''' <summary>
    ''' Establece el datasource de los RIAS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDpceRIASFirstCondition_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceRIASFirstCondition.QueryPopUp
        If RIASPopupFirstContidionXpo Is Nothing Then
            AsyncLoaderPopUp(True)
            Await Presenter.ListRIASXpCollection()
            AsyncLoaderPopUp(False)
        End If
    End Sub

    ''' <summary>
    ''' Establece el datasource de la especialidad de la primera condición
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSpecialtyFirst_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSpecialtyFirst.QueryPopUp
        If SpecialtyXpoFirst Is Nothing Then
            SpecialtyXpoFirst = Presenter.ListSpecialties()
        End If
    End Sub

    ''' <summary>
    ''' Establece el datasource de la unidad funcional de la primera condicion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFunctionalUnitFirst_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFunctionalUnitFirst.QueryPopUp
        If FunctionalUnitXpoFirst Is Nothing Then
            FunctionalUnitXpoFirst = Presenter.ListFunctionalUnit()
        End If
    End Sub

    ''' <summary>
    ''' Establece el datasource de la especialidad de la segunda condición
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSpecialtySecond_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSpecialtySecond.QueryPopUp
        If SpecialtyXpoSecond Is Nothing Then
            SpecialtyXpoSecond = Presenter.ListSpecialties()
        End If
    End Sub

    ''' <summary>
    ''' Establece el datasource de la unidad funcional de la segunda condicion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFunctionalUnitSecond_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFunctionalUnitSecond.QueryPopUp
        If FunctionalUnitXpoSecond Is Nothing Then
            FunctionalUnitXpoSecond = Presenter.ListFunctionalUnit()
        End If
    End Sub

    ''' <summary>
    ''' Establece el datasource de la vigencia del manual tarifario del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManualValidityPopup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRateManualValidityPopup.QueryPopUp
        If RateManualValidityXpoPopup Is Nothing Then
            RateManualValidityXpoPopup = Presenter.ListRateManualValidity()
        End If
    End Sub

    ''' <summary>
    ''' Establece el datasource del manual tarifario del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManualPopup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRateManualPopup.QueryPopUp
        If RateManualXpoPopup Is Nothing Then
            RateManualXpoPopup = Presenter.ListRateManual()
        End If
    End Sub

    ''' <summary>
    ''' Establece el datasource de la vigencia del manual tarifario del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManualValidity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleRateManualValidity.QueryPopUp
        If RateManualValidityXpo Is Nothing Then
            RateManualValidityXpo = Presenter.ListRateManualValidity()
        End If
    End Sub

    ''' <summary>
    ''' Establece el datasource del manual tarifario del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManual_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRateManual.QueryPopUp
        If RateManualXpo Is Nothing Then
            RateManualXpo = Presenter.ListRateManual()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de especialidad del popup de una condicion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleSpecialtyPopupFirstCondition_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSpecialtyPopupFirstCondition.QueryPopUp
        If SpecialtyPopupFirstConditionXpo Is Nothing Then
            AsyncLoaderPopUp(True)
            Await Presenter.ListSpecialtyXpCollection()
            AsyncLoaderPopUp(False)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de unidad funcional del popup de una condicion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleFunctionalUnitPopupFirstCondition_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFunctionalUnitPopupFirstCondition.QueryPopUp
        If FunctionalUnitPopupFirstConditionXpo Is Nothing Then
            AsyncLoaderPopUp(True)
            Await Presenter.ListFunctionalUnitXpCollection()
            AsyncLoaderPopUp(False)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de vigencia del manual tarifario del popup de una condicion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManualValidityPopupFirstCondition_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRateManualValidityPopupFirstCondition.QueryPopUp
        If RateManualValidityXpoPopupFirstCondition Is Nothing Then
            RateManualValidityXpoPopupFirstCondition = Presenter.ListRateManualValidity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de manual tarifario del popup de una condicion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManualPopupFirstCondition_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRateManualPopupFirstCondition.QueryPopUp
        If RateManualXpoPopupFirstCondition Is Nothing Then
            RateManualXpoPopupFirstCondition = Presenter.ListRateManual()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de la descripcion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDescriptionFirst_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleDescriptionFirst.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2096, Nothing, True)
            INDsleDescriptionFirst.Properties.DataSource = Presenter.ListDescriptions()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de la descripcion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDescriptionSecond_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleDescriptionSecond.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2096, Nothing, True)
            INDsleDescriptionSecond.Properties.DataSource = Presenter.ListDescriptions()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de la unidad funcional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFunctionalUnitFirst_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFunctionalUnitFirst.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmFunctionalUnit With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            FunctionalUnitXpoFirst = Presenter.ListFunctionalUnit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de la unidad funcional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFunctionalUnitSecond_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFunctionalUnitSecond.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmFunctionalUnit With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            FunctionalUnitXpoSecond = Presenter.ListFunctionalUnit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de la vigencia del manual tarifario del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManualValidityPopup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRateManualValidityPopup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmRateManualValidity With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            RateManualValidityXpoPopup = Presenter.ListRateManualValidity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form del manual tarifario del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManualPopup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRateManualPopup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmRateManual With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            RateManualXpoPopup = Presenter.ListRateManual()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de vigencia del manual tarifario del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManualValidity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleRateManualValidity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = Screen.PrimaryScreen.WorkingArea.Width * 0.78
            size.Height = Screen.PrimaryScreen.WorkingArea.Height * 0.78
            Using pop As New FrmTransparent(New FrmRateManualValidity With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            RateManualValidityXpo = Presenter.ListRateManualValidity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form del manual tarifario del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManual_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRateManual.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmRateManual With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            RateManualXpo = Presenter.ListRateManual()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de vigencia del manual tarifario del popup con una condicion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManualValidityPopupFirstCondition_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRateManualValidityPopupFirstCondition.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmRateManualValidity With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            RateManualValidityXpoPopupFirstCondition = Presenter.ListRateManualValidity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form del manual tarifario del popup con una condicion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManualPopupFirstCondition_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRateManualPopupFirstCondition.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmRateManual With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            RateManualXpoPopupFirstCondition = Presenter.ListRateManual()
        End If
    End Sub

#End Region

#Region "ContextMenu"

    ''' <summary>
    ''' Menu click derecho 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Select Case (sender.Tag.ToString)
            Case "Edit"
                Dim controlPopup = INDpceRates.Properties.PopupControl
                If controlPopup.Name = "INDpopupFirstCondition" Then
                    EditRateWithOneCondition()
                Else
                    EditRateWithTwoConditions()
                End If
            Case "Remove"
                DeleteRateWithConditions()
        End Select
    End Sub

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceRates_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceRates.CloseUp
        If modeEditGridRates Then
            CleanControlsPopup()
            CleanControlsPopupFirstCondition()
            modeEditGridRates = False
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load

    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Async Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
        If ModeEdit Then Await LoadControls()
    End Sub



    ''' <summary>
    ''' Metodo que se encarga de realizar validaciones antes de calcular los valores de IVA y subtotal
    ''' </summary>
    Private Async Sub ValidationsBeforeCalculateValues()
        If RuleType = 1 AndAlso LiquidationType = 1 AndAlso ConditionType = 5 Then
            If Not Me.ModeEdit Then
                Dim listFilterXpCollection = viewControlsRuleType.DataController.GetAllFilteredAndSortedRows()
                Dim quantitySelect = (From l In listFilterXpCollection Where l.SelectOption).Count
                If quantitySelect = 1 Then
                    Dim item = viewControlsRuleType.GetFocusedRow()
                    Me._serviceIPS = Await Presenter.GetServiceIPS(item.id)
                    If Me._serviceIPS?.TaxedProduct Then
                        Dim row As ViewListIPSServiceWithHomologationsXpo = (From l In listFilterXpCollection Where l?.SelectOption)(0)
                        Me.CalculateValues(row.IVAValue, _companySettings?.SalePriceIncludeTax, RuleType, LiquidationType, ConditionType)
                    End If
                Else
                    HideControlsIva()
                End If
            Else
                Me.CalculateValues(_serviceIPS?.IVA?.Percentage, _companySettings?.SalePriceIncludeTax, RuleType, LiquidationType, ConditionType)
            End If
        Else
            HideControlsIva()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de calcular el valor del subtotal e IVA
    ''' </summary>
    Private Sub CalculateValues(IVAValue As Decimal?, SalePriceIncludeTax As Boolean?, ruleType As Integer, liquidationType As Integer, conditionType As Integer)
        If Not IVAValue.HasValue OrElse IVAValue = 0 Then
            HideControlsIva()
        Else
            INDlyItemSubtotal.ShowLayout()
            INDlyItemValueIVA.ShowLayout()

            If SalePriceIncludeTax Then
                INDlyItemSubtotal.Text = "Subtotal"
                INDlyItemValueIVA.Text = "Valor IVA"

                Dim subTotal = SalesValue / (1 + (IVAValue / 100))

                SalesSubtotal = subTotal
                SalesValueIVA = SalesValue - subTotal
            Else
                INDlyItemSubtotal.Text = "Valor IVA"
                INDlyItemValueIVA.Text = "Valor Total"

                Dim Value = SalesValue * (IVAValue / 100)

                SalesSubtotal = Value
                SalesValueIVA = SalesValue + Value
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de ocultar los campos de IVA
    ''' </summary>
    Private Sub HideControlsIva()
        SalesSubtotal = 0
        SalesValueIVA = 0

        INDlyItemSubtotal.HideLayout()
        INDlyItemValueIVA.HideLayout()
    End Sub

#End Region
End Class

Public Class AddInfoToGridFormPrincipal
    Inherits EventArgs

    ''' <summary>
    ''' Representa al detalle de la definición de tarifa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DefinitionRateDetail As DefinitionRateDetail

    ''' <summary>
    ''' Representa al listado del detalle de la definicion de tarifa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListDefinitionRateDetail As List(Of DefinitionRateDetail)

    ''' <summary>
    ''' Establece si el retorno es satisfactorio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReturnValueOk As Boolean

    ''' <summary>
    ''' Establece si el registro es para modificar o guardar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ModeEdit As Boolean

    ''' <summary>
    ''' Listado de eliminados de condiciones de los detalles
    ''' </summary>
    ''' <remarks></remarks>
    Property ListDeleteDefinitionRateDetailCondition As List(Of DefinitionRateDetailCondition)

End Class