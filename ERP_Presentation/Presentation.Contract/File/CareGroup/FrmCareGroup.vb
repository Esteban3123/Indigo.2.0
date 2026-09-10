'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/11/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraLayout
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Contract.MVP
Imports Presentation.Controls

#End Region

Public Class FrmCareGroup
    Implements ICareGroup, ICustomizableForm

#Region "Properties"

    Private _attentionGroupsDictionary As Dictionary(Of String, List(Of Tuple(Of Integer, String)))
    Private ReadOnly Property AttentionGroupsDictionary As Dictionary(Of String, List(Of Tuple(Of Integer, String)))
        Get
            If _attentionGroupsDictionary Is Nothing Then
                _attentionGroupsDictionary = New Dictionary(Of String, List(Of Tuple(Of Integer, String)))()
                _attentionGroupsDictionary.Add("es-CO", {
                    New Tuple(Of Integer, String)(1, "EAPB con contrato"),
                    New Tuple(Of Integer, String)(2, "EAPB sin contrato"),
                    New Tuple(Of Integer, String)(3, "Particulares"),
                    New Tuple(Of Integer, String)(4, "Aseguradoras")
                }.ToList())
                _attentionGroupsDictionary.Add("es-CR", {
                    New Tuple(Of Integer, String)(1, "Clientes con Contrato"),
                    New Tuple(Of Integer, String)(2, "Clientes sin Contrato"),
                    New Tuple(Of Integer, String)(3, "Particulares"),
                    New Tuple(Of Integer, String)(4, "Aseguradoras")
                }.ToList())
            End If

            Return _attentionGroupsDictionary
        End Get
    End Property
    ''' <summary>
    ''' Elementos del combo del segmento liquidacion de mezclas
    ''' </summary>
    Dim itemsUnitDoses As String() = GetItemsComboBoxUnitDoseTypes()

    ''' <summary>
    ''' Método Reporte Recaudo Monto Fijo
    ''' </summary>
    ''' <returns></returns>
    Public Property CollectionMethod As Integer Implements ICareGroup.CollectionMethod
        Get
            If INDsleCollectionMethod.EditValue Is Nothing Then
                Return 2
            End If
            Return Convert.ToInt32(INDsleCollectionMethod.EditValue)
        End Get
        Set(value As Integer)
            INDsleCollectionMethod.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Permite saber si requiere autorización
    ''' </summary>
    ''' <returns></returns>
    Public Property AuthorizationRequired As Boolean Implements ICareGroup.AuthorizationRequired
        Get
            Return INDsleAuthorizationRequired.EditValue
        End Get
        Set(value As Boolean)
            INDsleAuthorizationRequired.EditValue = value
        End Set
    End Property

    Public Property ContractAccountingStructureId As Integer? Implements ICareGroup.ContractAccountingStructureId
        Get
            Return INDsleContractAccountingStructure.EditValue
        End Get
        Set(value As Integer?)
            INDsleContractAccountingStructure.EditValue = value
        End Set
    End Property

    Public Property ContractAccountingStructureXpo As XPInstantFeedbackSource Implements ICareGroup.ContractAccountingStructureXpo
        Get
            Return INDsleContractAccountingStructure.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleContractAccountingStructure.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la definicion de tarifa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DefinitionRateId As Integer Implements ICareGroup.DefinitionRateId
        Get
            Return INDsleDefinitionRate.EditValue
        End Get
        Set(value As Integer)
            INDsleDefinitionRate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la definicion de tarifa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DefinitionRateXpo As XPInstantFeedbackSource Implements ICareGroup.DefinitionRateXpo
        Get
            Return INDsleDefinitionRate.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleDefinitionRate.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha final
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EndDate As Date? Implements ICareGroup.EndDate
        Get
            Return INDdteEndDate.EditValue
        End Get
        Set(value As Date?)
            INDdteEndDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InitialDate As Date? Implements ICareGroup.InitialDate
        Get
            Return INDdteInitialDate.EditValue
        End Get
        Set(value As Date?)
            INDdteInitialDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del Servicio no facturable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillingItemsRestrictionId As Integer Implements ICareGroup.BillingItemsRestrictionId
        Get
            Return INDsleBillingItemsRestriction.EditValue
        End Get
        Set(value As Integer)
            INDsleBillingItemsRestriction.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del Servicio no facturable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillingItemsRestrictionXpo As XPInstantFeedbackSource Implements ICareGroup.BillingItemsRestrictionXpo
        Get
            Return INDsleBillingItemsRestriction.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBillingItemsRestriction.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterId As Integer? Implements ICareGroup.CostCenterId
        Get
            Return CInt(INDsleCostCenter.EditValue)
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Porpiedad que contiene el listado de centros de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICareGroup.CostCenterXpo
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las horas de recuperacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HoursOfRecoveryIncluded As Int16 Implements ICareGroup.HoursOfRecoveryIncluded
        Get
            Return INDseHoursOfRecoveryIncluded.EditValue
        End Get
        Set(value As Int16)
            INDseHoursOfRecoveryIncluded.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las horas maximas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MaximumObservationTime As Int16 Implements ICareGroup.MaximumObservationTime
        Get
            Return INDseMaximumObservationTime.EditValue
        End Get
        Set(value As Int16)
            INDseMaximumObservationTime.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las horas minimas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MinimumObservationTime As Int16 Implements ICareGroup.MinimumObservationTime
        Get
            Return INDseMinimumObservationTime.EditValue
        End Get
        Set(value As Int16)
            INDseMinimumObservationTime.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de liquidacion para estancias de urgencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TypeLiquidationEmergencyStays As Integer? Implements ICareGroup.TypeLiquidationEmergencyStays
        Get
            Return INDsleTypeLiquidationEmergencyStays.EditValue
        End Get
        Set(value As Integer?)
            INDsleTypeLiquidationEmergencyStays.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si liquida dia de egreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LiquidateDayDischarge As Boolean Implements ICareGroup.LiquidateDay
        Get
            Return INDsleLiquidateDay.EditValue
        End Get
        Set(value As Boolean)
            INDsleLiquidateDay.EditValue = value
        End Set
    End Property



    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements ICareGroup.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As ContractSequence Implements ICareGroup.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As ContractSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.ContractSequenceDetail In Me._sequence.ContractSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements ICareGroup.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements ICareGroup.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre del grupo de atencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameCG As String Implements ICareGroup.NameCG
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICareGroup.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el periodo facturado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillingPeriod As Integer? Implements ICareGroup.BillingPeriod
        Get
            Return INDsleBillingPeriod.EditValue
        End Get
        Set(value As Integer?)
            INDsleBillingPeriod.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de grupo de atencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CareGroupType As Byte? Implements ICareGroup.CareGroupType
        Get
            Return INDsleCareGroupType.EditValue
        End Get
        Set(value As Byte?)
            INDsleCareGroupType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el concepto a facturar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConceptToBill As Integer? Implements ICareGroup.ConceptToBill
        Get
            Return INDsleConceptToBill.EditValue
        End Get
        Set(value As Integer?)
            INDsleConceptToBill.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractId As Integer? Implements ICareGroup.ContractId
        Get
            Return INDsleContractId.EditValue
        End Get
        Set(value As Integer?)
            INDsleContractId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de contratos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICareGroup.ContractXpo
        Get
            Return INDsleContractId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleContractId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EntityType As Byte? Implements ICareGroup.EntityType
        Get
            Return CByte(INDsleEntityType.EditValue)
        End Get
        Set(value As Byte?)
            INDsleEntityType.EditValue = value
        End Set
    End Property

    Public Property ExtramuralPharmaceuticalDispensing As Boolean? Implements ICareGroup.ExtramuralPharmaceuticalDispensing
        Get
            Return INDrgExtramuralPharmaceuticalDispensing.EditValue
        End Get
        Set(value As Boolean?)
            INDrgExtramuralPharmaceuticalDispensing.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el porcentaje de descuento especifico al cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DiscountContractedCustomer As Decimal Implements ICareGroup.DiscountContractedCustomer
        Get
            Return INDseDiscountContractedCustomer.EditValue
        End Get
        Set(value As Decimal)
            INDseDiscountContractedCustomer.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el plazo de factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoiceDeadlines As Integer Implements ICareGroup.InvoiceDeadlines
        Get
            Return INDseInvoiceDeadlines.EditValue
        End Get
        Set(value As Integer)
            INDseInvoiceDeadlines.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de liquidacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LiquidationType As Integer? Implements ICareGroup.LiquidationType
        Get
            Return INDsleLiquidationType.EditValue
        End Get
        Set(value As Integer?)
            INDsleLiquidationType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tope maximo de facturacion individual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MaximumIndividualBilling As Decimal Implements ICareGroup.MaximumIndividualBilling
        Get
            Return INDtxtMaximumIndividualBilling.EditValue
        End Get
        Set(value As Decimal)
            INDtxtMaximumIndividualBilling.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tope maximo de la facturacion por periodo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PeriodMaximumBilling As Decimal Implements ICareGroup.PeriodMaximumBilling
        Get
            Return INDtxtPeriodMaximumBilling.EditValue
        End Get
        Set(value As Decimal)
            INDtxtPeriodMaximumBilling.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la plantilla de procedimientos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProcedureTemplateId As Integer? Implements ICareGroup.ProcedureTemplateId
        Get
            Return INDsleProcedureTemplateId.EditValue
        End Get
        Set(value As Integer?)
            INDsleProcedureTemplateId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la plantilla de procedimientos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProcedureTemplateXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICareGroup.ProcedureTemplateXpo
        Get
            Return INDsleProcedureTemplateId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleProcedureTemplateId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la plantilla de producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProductRateId As Integer? Implements ICareGroup.ProductRateId
        Get
            Return INDsleProductTemplateId.EditValue
        End Get
        Set(value As Integer?)
            INDsleProductTemplateId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la plantilla de productos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProductTemplateXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICareGroup.ProductTemplateXpo
        Get
            Return INDsleProductTemplateId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleProductTemplateId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la plantilla de requerimientos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RequirementsTemplateId As Integer? Implements ICareGroup.RequirementsTemplateId
        Get
            Return INDsleRequirementsTemplateId.EditValue
        End Get
        Set(value As Integer?)
            INDsleRequirementsTemplateId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de liquidación de oxígeno
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TypeLiquidationOxygen As Integer Implements ICareGroup.TypeLiquidationOxygen
        Get
            Return INDsleTypeLiquidationOxygen.EditValue
        End Get
        Set(value As Integer)
            INDsleTypeLiquidationOxygen.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de las plantillas de requerimientos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RequirementsTemplateXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICareGroup.RequirementsTemplateXpo
        Get
            Return INDsleRequirementsTemplateId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleRequirementsTemplateId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de Paquetes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractPackageXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICareGroup.ContractPackageXpo
        Get
            Return INDSlePackages.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlePackages.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la nota tecnica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TechnicalNoteId As Integer? Implements ICareGroup.TechnicalNoteId
        Get
            Return INDSleTechnicalNote.EditValue
        End Get
        Set(value As Integer?)
            INDSleTechnicalNote.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de notas tecnicas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TechnicalNoteXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICareGroup.TechnicalNoteXpo
        Get
            Return INDSleTechnicalNote.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleTechnicalNote.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los datos de tipo de entidad
    ''' </summary>
    ''' <returns></returns>
    Public Property EntityTypeXpo As XPInstantFeedbackSource Implements ICareGroup.EntityTypeXpo
        Get
            Return TryCast(INDsleEntityType.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEntityType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' tipo del tipo de entidad
    ''' </summary>
    ''' <returns></returns>
    Public Property TypeOfCompanyType As Byte?
        Get
            Return _typeOfCompanyType
        End Get
        Set(value As Byte?)
            _typeOfCompanyType = value
        End Set
    End Property
#Region "Budget Interface"

    Public Property AffectBudget As Boolean Implements ICareGroup.AffectBudget
        Get
            Return INDsleAffectBudget.EditValue
        End Get
        Set(value As Boolean)
            INDsleAffectBudget.EditValue = value
        End Set
    End Property

    Public Property BudgetaryEntityId As Integer? Implements ICareGroup.BudgetaryEntityId
        Get
            Return INDsleBudgetaryEntityId.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetaryEntityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryEntityXpo As XPInstantFeedbackSource Implements ICareGroup.BudgetaryEntityXpo
        Get
            Return INDsleBudgetaryEntityId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudgetaryEntityId.Properties.DataSource = value
        End Set
    End Property

    Public Property BudgetaryValidityId As Integer? Implements ICareGroup.BudgetaryValidityId
        Get
            Return INDsleBudgetaryValidityId.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetaryValidityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryValidityXpo As XPInstantFeedbackSource Implements ICareGroup.BudgetaryValidityXpo
        Get
            Return INDsleBudgetaryValidityId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudgetaryValidityId.Properties.DataSource = value
        End Set
    End Property

    Public Property BillingBudgetId As Integer? Implements ICareGroup.BillingBudgetId
        Get
            Return INDsleBillingBudgetId.EditValue
        End Get
        Set(value As Integer?)
            INDsleBillingBudgetId.EditValue = value
        End Set
    End Property

    Public Property BillingBudgetXpo As XPInstantFeedbackSource Implements ICareGroup.BillingBudgetXpo
        Get
            Return INDsleBillingBudgetId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBillingBudgetId.Properties.DataSource = value
        End Set
    End Property

    Public Property PromissoryNoteBudgetId As Integer? Implements ICareGroup.PromissoryNoteBudgetId
        Get
            Return INDslePromissoryNoteBudgetId.EditValue
        End Get
        Set(value As Integer?)
            INDslePromissoryNoteBudgetId.EditValue = value
        End Set
    End Property

    Public Property PromissoryNoteBudgetXpo As XPInstantFeedbackSource Implements ICareGroup.PromissoryNoteBudgetXpo
        Get
            Return INDslePromissoryNoteBudgetId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePromissoryNoteBudgetId.Properties.DataSource = value
        End Set
    End Property

    Public Property AccountReceivablePreviousValidityBudgetId As Integer? Implements ICareGroup.AccountReceivablePreviousValidityBudgetId
        Get
            Return INDsleAccountReceivablePreviousValidityBudgetId.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountReceivablePreviousValidityBudgetId.EditValue = value
        End Set
    End Property

    Public Property AccountReceivablePreviousValidityBudgetXpo As XPInstantFeedbackSource Implements ICareGroup.AccountReceivablePreviousValidityBudgetXpo
        Get
            Return INDsleAccountReceivablePreviousValidityBudgetId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccountReceivablePreviousValidityBudgetId.Properties.DataSource = value
        End Set
    End Property

    Public Property PortfolioRecoveryBudgetId As Integer? Implements ICareGroup.PortfolioRecoveryBudgetId
        Get
            Return INDslePortfolioRecoveryBudgetId.EditValue
        End Get
        Set(value As Integer?)
            INDslePortfolioRecoveryBudgetId.EditValue = value
        End Set
    End Property

    Public Property PortfolioRecoveryBudgetXpo As XPInstantFeedbackSource Implements ICareGroup.PortfolioRecoveryBudgetXpo
        Get
            Return INDslePortfolioRecoveryBudgetId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePortfolioRecoveryBudgetId.Properties.DataSource = value
        End Set
    End Property

#End Region

#End Region

#Region "Variables"

    ''' <summary>
    ''' Listado que establece si o no
    ''' </summary>
    Private ListYesNot As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim GroupersCareGroup As GroupersCareGroup

    ''' <summary>
    ''' Listado de detalles del ingreso de activo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListGroupersCareGroup As List(Of GroupersCareGroup)

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PCareGroup

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.ContractSequence

    ''' <summary>
    ''' Representa la entidad de grupos de atencion 
    ''' </summary>
    ''' <remarks></remarks>
    Dim careGroup As CareGroup

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordContract

    ''' <summary>
    ''' Parametros de Facturación
    ''' </summary>
    Private _parameterBilling As SettingsBilling

    ''' <summary>
    ''' bandera para indicar que se está cargando un registro
    ''' </summary>
    Private _isLoading As Boolean


    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListTypeLiquidationEmergencyStays As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la selección de liquida dia de egreso
    ''' </summary>
    Dim ListLiquidateDays As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListLiquidationType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListBillingPeriod As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListConceptToBill As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de liquidación de oxígeno
    ''' </summary>
    Dim ListTypeLiquidationOxygen As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de las filas de la rejilla cuando le dan click derecho/agregar tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListRows As List(Of CareGroupRate)

    ''' <summary>
    ''' Listado de detalles de grupos de atencion
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListCareGroupRate As List(Of CareGroupRate)

    ''' <summary>
    ''' Listado de detalles de grupos de atencion para enviar a guardar
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListCareGroupRateSave As List(Of CareGroupRate)

    ''' <summary>
    ''' Listado para el repositorio de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListLiquidationTypeCareGroupRate As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de eliminados del detalle de grupos de atencion
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteCareGroupRate As List(Of CareGroupRate)

    ''' <summary>
    ''' Listado de eliminados de definiciones de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListCareGroupDefinitionRate As List(Of CareGroupDefinitionRate)

    ''' <summary>
    ''' Listado de definiciones de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteCareGroupDefinitionRate As List(Of CareGroupDefinitionRate)

    ''' <summary>
    ''' True = modificar y False = guardar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim modeModify As Boolean = False

    ''' <summary>
    ''' Entidad del detalle de grupo de atencion
    ''' </summary>
    ''' <remarks></remarks>
    Dim careGroupDefinitionRate As CareGroupDefinitionRate

    ''' <summary>
    ''' Listado para comparar cuando se este editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListCompare As List(Of CareGroupDefinitionRate)

    ''' <summary>
    ''' Listado de eliminados de servicios no facturados
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListCareGroupBillingItemsRestriction As List(Of CareGroupBillingItemsRestriction)

    ''' <summary>
    ''' Listado de definiciones de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteCareGroupBillingItemsRestriction As List(Of CareGroupBillingItemsRestriction)

    ''' <summary>
    ''' Listado para comparar cuando se este editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListCompareSNF As List(Of CareGroupBillingItemsRestriction)

    ''' <summary>
    ''' Listado de opciones para seleccionar el método de reporte de recaudo monto fijo
    ''' </summary>
    Dim ListCollectionMethod As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Entidad del detalle de grupo de atencion
    ''' </summary>
    ''' <remarks></remarks>
    Dim careGroupBillingItemsRestriction As CareGroupBillingItemsRestriction


#Region "Listado de Eliminados Popup"

    'Estos listados vienen del Popup al momento de que algun item de los rangos se haya eliminado

    ''' <summary>
    ''' Listado de eliminados de la entidad de tipo de unidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteRateByUnitType As List(Of RateByUnitType)

    ''' <summary>
    ''' Listado de eliminados de la entidad de especialidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteRateBySpecialty As List(Of RateBySpecialty)

    ''' <summary>
    ''' Listado de eliminados de la entidad de unidades funcionales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteRateByFunctionalUnit As List(Of RateByFunctionalUnit)

    ''' <summary>
    ''' Listado de eliminados de la entidad fija
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteRateByFixed As List(Of RateByFixed)

    ''' <summary>
    ''' Listado de eliminados de la entidad estandard
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteRateByStandard As List(Of RateByStandard)

    ''' <summary>
    ''' Listado de eliminados de la entidad de formula
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteRateByFormula As List(Of RateByFormula)

#End Region

    Dim listCareGroupInvoiceCategories As List(Of CareGroupInvoiceCategories)

    Dim listCareGroupInvoiceCategoriesDelete As List(Of CareGroupInvoiceCategories)

    Dim INDdtControlTipoUF As New Domain.Entities.TrackableCollection(Of ControlByTypeFunctionalUnit)

    Private _typeOfCompanyType As Byte?

    Dim listCareGroupMixLiquidation As List(Of CareGroupMixLiquidation)

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If careGroup IsNot Nothing AndAlso careGroup.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MCareGroup(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteCareGroup(careGroup.Id, indigo.TransactionalContainer)
                        If result.StateResult = True Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbtnCode.Enabled = False
                            If result.MessageResult(0) = "-999" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                            ElseIf result.MessageResult(0) = "-000" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                            End If
                        End If
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If

        If ListCareGroupDefinitionRate Is Nothing OrElse ListCareGroupDefinitionRate.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe tener al menos una definición de tarifa."
            Exit Sub
        End If

        If listCareGroupInvoiceCategories Is Nothing OrElse listCareGroupInvoiceCategories.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe tener al menos una categoria de factura"
            Exit Sub
        End If
        Try
            AssigningValues()
            Using model As New MCareGroup(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveCareGroup(careGroup, _idCurrentSequence)
                If Result.StateResult = True Then
                    If careGroup.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf careGroup.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.careGroup = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    ElseIf Result.MessageResult(0) = "-000" Then
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewCareGroup()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Tipo Grupo Atención", .FieldName = "CareGroupTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Tarifas", .FieldName = "NumberDetail", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCareGroup
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Methods"

    Private Async Function LoadParameterBilling() As Task
        Using model As New Presentation.Contract.MVP.MBilling(MyTag)
            Dim budgetInterface As Boolean = False
            Me._parameterBilling = Await model.GetSettingsBillingByIdUnitOperative(_idOperativeUnit, False)
            If Me._parameterBilling IsNot Nothing Then
                budgetInterface = Me._parameterBilling.BudgetInterface
            End If

            INDlyItemAffectBudget.AllowHide = Not budgetInterface
            INDlyItemBillingBudgetId.ShowInCustomizationForm = Not budgetInterface
            INDlygBudgetInterface.Visibility = If(budgetInterface, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        End Using
    End Function

    ''' <summary>
    ''' Método para abir el form de dashboard pgp
    ''' </summary>
    Private Sub OpenFormDashboardPGP()
        Using formulario As New FrmDashboardPGP
            Me.Cursor = ChangeCursorIndigo()
            formulario.IdCareGroup = careGroup.Id
            formulario.ToolBar.Visible = False
            formulario.Size = New System.Drawing.Size(1270, 750)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que inicializa los datasources de los search que se cargan con tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        ListTypeLiquidationEmergencyStays = New List(Of Tuple(Of Integer, String))
        ListTypeLiquidationEmergencyStays.Add(New Tuple(Of Integer, String)(1, "CAMA MAYOR VALOR"))
        ListTypeLiquidationEmergencyStays.Add(New Tuple(Of Integer, String)(2, "CAMA ÚLTIMO INGRESO"))
        INDsleTypeLiquidationEmergencyStays.Properties.DataSource = ListTypeLiquidationEmergencyStays.ToList

        ListLiquidationType = New List(Of Tuple(Of Integer, String))
        ListLiquidationType.Add(New Tuple(Of Integer, String)(1, "Pago por servicios"))
        ListLiquidationType.Add(New Tuple(Of Integer, String)(2, "Capitación"))
        'ListLiquidationType.Add(New Tuple(Of Integer, String)(3, "Factura Global"))
        'ListLiquidationType.Add(New Tuple(Of Integer, String)(4, "Capitación Global"))
        ListLiquidationType.Add(New Tuple(Of Integer, String)(5, "Pago Global Prospectivo - PGP"))
        ListLiquidationType.Add(New Tuple(Of Integer, String)(6, "Pago individual por caso, conjunto integral de atenciones, paquete o canasta"))
        ListLiquidationType.Add(New Tuple(Of Integer, String)(7, "Grupo Relacionado a Diagnóstico"))
        ListLiquidationType.Add(New Tuple(Of Integer, String)(8, "Otra"))
        ListLiquidationType.Add(New Tuple(Of Integer, String)(9, "Conciliaciones"))

        INDsleLiquidationType.Properties.DataSource = ListLiquidationType.ToList()

        ListCollectionMethod = New List(Of Tuple(Of Integer, String))
        ListCollectionMethod.Add(New Tuple(Of Integer, String)(1, "Recaudo real (Copagos y Cuotas Moderadoras)"))
        ListCollectionMethod.Add(New Tuple(Of Integer, String)(2, "Descuento pactado sobre Monto Fijo (No contable)"))

        INDsleCollectionMethod.Properties.DataSource = ListCollectionMethod.ToList()


        ListBillingPeriod = New List(Of Tuple(Of Integer, String))
        ListBillingPeriod.Add(New Tuple(Of Integer, String)(1, "Semanal"))
        ListBillingPeriod.Add(New Tuple(Of Integer, String)(2, "Quincenal"))
        ListBillingPeriod.Add(New Tuple(Of Integer, String)(3, "Mensual"))
        ListBillingPeriod.Add(New Tuple(Of Integer, String)(4, "BiMensual"))
        ListBillingPeriod.Add(New Tuple(Of Integer, String)(5, "Semestral"))
        ListBillingPeriod.Add(New Tuple(Of Integer, String)(6, "Anual"))
        INDsleBillingPeriod.Properties.DataSource = ListBillingPeriod.ToList

        ListConceptToBill = New List(Of Tuple(Of Integer, String))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(1, "Plan de Beneficios en Salud Financiado con UPC Contributivo"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(2, "Plan Complementario en Salud"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(3, "Plan de Beneficios en Salud Financiado con UPC Subsidiado"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(4, "Servicios Salud EPS privadas"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(5, "Plan medicina prepagada"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(6, "Otras Pólizas en salud"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(7, "Particular"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(8, "Cobertura Salud Pública"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(9, "Cobertura Régimen Especial o Excepción"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(10, "Cobertura entidad territorial, recursos de oferta"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(11, "Cobertura ARL"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(12, "Cuota recuperacion vinculados"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(13, "Cobertura Póliza SOAT"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(14, "Cobertura ADRES"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(15, "Convenios ADRES Trauma Mayor Desplazados"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(16, "Min Salud Recursos IVA Social"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(17, "Otras cuentas por cobrar servicio salud"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(18, "Presupuesto Máximo"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(19, "Urgencia Población Migrante"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(20, "Prima EPS / EOC, no asegurados SOAT"))
        ListConceptToBill.Add(New Tuple(Of Integer, String)(21, "Cobertura Fondo Nacional de Salud de las Personas Privadas de la Libertad"))
        INDsleConceptToBill.Properties.DataSource = ListConceptToBill.ToList

        ListTypeLiquidationOxygen = New List(Of Tuple(Of Integer, String))
        ListTypeLiquidationOxygen.Add(New Tuple(Of Integer, String)(1, "Litros"))
        ListTypeLiquidationOxygen.Add(New Tuple(Of Integer, String)(2, "Horas"))
        INDsleTypeLiquidationOxygen.Properties.DataSource = ListTypeLiquidationOxygen.ToList

        INDGleThirdCopayFixedAmount.Properties.DataSource = {New Tuple(Of Byte, String)(0, "Tercero del grupo de atención"), New Tuple(Of Byte, String)(1, "Tercero responsable del pago")}.ToList()

        ListYesNot = New List(Of Tuple(Of Boolean, String))
        ListYesNot.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListYesNot.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDsleApplyRIAS.Properties.DataSource = ListYesNot
        INDsleAffectBudget.Properties.DataSource = ListYesNot
        INDsleAuthorizationRequired.Properties.DataSource = ListYesNot
        INDsleLiquidateDay.Properties.DataSource = ListYesNot
        getSettingsContract()
    End Sub

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICareGroup.ActionsOnControls
        Set(value As Boolean)
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleCareGroupType.Enabled = value
            INDsleCostCenter.Enabled = value
            INDsleApplyRIAS.Enabled = value
            INDsleContractId.Enabled = value
            INDsleAuthorizationRequired.Enabled = value
            INDsleLiquidationType.Enabled = value
            INDsleBillingPeriod.Enabled = value
            INDtxtMaximumIndividualBilling.Enabled = value
            INDtxtPeriodMaximumBilling.Enabled = value
            INDsleEntityType.Enabled = value
            INDrgExtramuralPharmaceuticalDispensing.Enabled = value
            INDseDiscountContractedCustomer.Enabled = value
            INDsleRequirementsTemplateId.Enabled = value
            INDseInvoiceDeadlines.Enabled = value
            INDsleProcedureTemplateId.Enabled = value
            INDsleProductTemplateId.Enabled = value
            INDsleConceptToBill.Enabled = value
            INDsleContractAccountingStructure.Enabled = value
            INDpceDefinitionRate.Enabled = value
            INDgcDefinitionRate.Enabled = value
            INDpceBillingItemsRestriction.Enabled = value
            INDgcBillingItemsRestriction.Enabled = value
            INDsleTypeLiquidationEmergencyStays.Enabled = value
            INDsleLiquidateDay.Enabled = value
            INDseHoursOfRecoveryIncluded.Enabled = value
            INDSleInvoiceCategory.Enabled = value
            INDBtnAddInvoiceCategory.Enabled = value
            INDGcInvoiceCategory.Enabled = value
            INDGcUniFun.Enabled = value
            INDsleAffectBudget.Enabled = value
            INDsleBudgetaryEntityId.Enabled = value
            INDsleBudgetaryValidityId.Enabled = value
            INDsleBillingBudgetId.Enabled = value
            INDslePromissoryNoteBudgetId.Enabled = value
            INDsleAccountReceivablePreviousValidityBudgetId.Enabled = value
            INDslePortfolioRecoveryBudgetId.Enabled = value
            INDSlePackages.Enabled = value
            INDGcPackages.Enabled = value
            INDSbAddPackage.Enabled = value
            INDSleTechnicalNote.Enabled = value
            INDsleTypeLiquidationOxygen.Enabled = value
            INDgcMixLiquidation.Enabled = value
            INDSleBillingConceptCopay.Enabled = value
            INDGleThirdCopayFixedAmount.Enabled = value

            If value Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.careGroup IsNot Nothing AndAlso Me.careGroup.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
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

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.careGroup.Code, Me.careGroup.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.careGroup.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.careGroup.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.careGroup.Code, Me.careGroup.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.careGroup.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyCareGroup.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        NameCG = String.Empty
        CareGroupType = Nothing
        CostCenterId = Nothing
        INDsleCostCenter.Properties.NullText = String.Empty
        ContractId = Nothing
        INDsleContractId.Properties.NullText = String.Empty
        AuthorizationRequired = Nothing
        INDsleApplyRIAS.EditValue = Nothing
        LiquidationType = Nothing
        BillingPeriod = Nothing
        MaximumIndividualBilling = 0
        PeriodMaximumBilling = 0
        EntityType = Nothing
        INDsleEntityType.Properties.NullText = String.Empty
        EntityTypeXpo = Nothing
        INDsleEntityType.Properties.ReadOnly = False
        DiscountContractedCustomer = 0
        RequirementsTemplateId = Nothing
        INDsleRequirementsTemplateId.Properties.NullText = String.Empty
        ListCareGroupRate = Nothing
        ListCareGroupRateSave = Nothing
        ListDeleteCareGroupRate = Nothing
        InvoiceDeadlines = 0
        ProcedureTemplateId = Nothing
        INDsleProcedureTemplateId.Properties.NullText = String.Empty
        ProductRateId = Nothing
        INDsleProductTemplateId.Properties.NullText = String.Empty
        ConceptToBill = Nothing
        ContractAccountingStructureId = Nothing
        INDsleContractAccountingStructure.Properties.NullText = String.Empty
        TechnicalNoteId = Nothing
        INDSleTechnicalNote.Properties.NullText = String.Empty
        TypeLiquidationOxygen = 1
        INDLciTechnicalNote.HideControl(True)

        AffectBudget = Nothing
        BudgetaryEntityId = Nothing
        INDsleBudgetaryEntityId.Properties.NullText = String.Empty
        BudgetaryValidityId = Nothing
        INDsleBudgetaryValidityId.Properties.NullText = String.Empty
        BillingBudgetId = Nothing
        INDsleBillingBudgetId.Properties.NullText = String.Empty
        PromissoryNoteBudgetId = Nothing
        INDslePromissoryNoteBudgetId.Properties.NullText = String.Empty
        AccountReceivablePreviousValidityBudgetId = Nothing
        INDsleAccountReceivablePreviousValidityBudgetId.Properties.NullText = String.Empty
        PortfolioRecoveryBudgetId = Nothing
        INDslePortfolioRecoveryBudgetId.Properties.NullText = String.Empty
        INDSleBillingConceptCopay.Properties.NullText = String.Empty
        INDSleBillingConceptCopay.EditValue = Nothing
        INDGleThirdCopayFixedAmount.EditValue = CByte(0)

        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDgcDefinitionRate.DataSource = Nothing
        INDgcBillingItemsRestriction.DataSource = Nothing
        INDlyItemApplyRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemApplyRIAS.AllowHide = True
        INDlyItemContractId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemContractId.AllowHide = True
        INDlyItemMinimumObservationTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemMinimumObservationTime.AllowHide = True
        INDlyItemMaximumObservationTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemMaximumObservationTime.AllowHide = True
        ListDeleteRateByUnitType = Nothing
        ListDeleteRateBySpecialty = Nothing
        ListDeleteRateByFunctionalUnit = Nothing
        ListDeleteRateByFixed = Nothing
        ListDeleteRateByStandard = Nothing
        ListDeleteRateByFormula = Nothing

        TypeLiquidationEmergencyStays = Nothing
        LiquidateDayDischarge = Nothing
        MinimumObservationTime = 0
        MaximumObservationTime = 0
        HoursOfRecoveryIncluded = 0

        careGroupDefinitionRate = Nothing
        ListCareGroupDefinitionRate = Nothing
        ListDeleteCareGroupDefinitionRate = Nothing

        careGroupBillingItemsRestriction = Nothing
        ListCareGroupBillingItemsRestriction = Nothing
        ListDeleteCareGroupBillingItemsRestriction = Nothing
        CleanControlsPopup()


        ListGroupersCareGroup = Nothing

        INDSleInvoiceCategory.EditValue = Nothing
        INDGcInvoiceCategory.DataSource = Nothing
        listCareGroupInvoiceCategories = Nothing
        listCareGroupInvoiceCategoriesDelete = Nothing
        INDlyCareGroup.EndUpdate()

        careGroup = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()

        INDGcPackages.DataSource = Nothing
        ContractPackageXpo = Nothing
        INDSlePackages.Properties.NullText = String.Empty
        _selectorContractPackages.Clear()

        INDsleProcedureTemplateId.Properties.ReadOnly = False

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        INDGcUniFun.DataSource = Nothing
        INDgcMixLiquidation.DataSource = Nothing
        CollectionMethod = Nothing
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        GetDataMixLiquidation()
        With careGroup
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameCG
            .CareGroupType = CareGroupType
            .LiquidationType = LiquidationType
            .CostCenterId = CostCenterId
            .AuthorizationRequired = AuthorizationRequired
            If .Id = 0 Then
                .OperativeUnitId = Me.BarraBotones.OperatingUnitValue
            End If
            If INDlyItemApplyRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ApplyRIAS = INDsleApplyRIAS.EditValue
            Else
                .ApplyRIAS = Nothing
            End If
            If INDlyItemContractId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ContractId = ContractId
            Else
                .ContractId = Nothing
            End If
            .BillingPeriod = BillingPeriod
            .MaximumIndividualBilling = MaximumIndividualBilling
            .PeriodMaximumBilling = PeriodMaximumBilling
            .RequirementsTemplateId = RequirementsTemplateId
            .InvoiceDeadlines = InvoiceDeadlines
            .ProcedureTemplateId = ProcedureTemplateId
            .ProductRateId = ProductRateId
            .ConceptToBill = ConceptToBill
            .ContractAccountingStructureId = ContractAccountingStructureId
            .EntityType = EntityType
            .ExtramuralPharmaceuticalDispensing = ExtramuralPharmaceuticalDispensing
            .DiscountContractedCustomer = DiscountContractedCustomer
            .TypeLiquidationOxygen = TypeLiquidationOxygen
            .MethodFixedAmountCollectionReport = CollectionMethod

            If INDLciTechnicalNote.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .TechnicalNoteId = TechnicalNoteId
            Else
                .TechnicalNoteId = Nothing
            End If


            If ListGroupersCareGroup IsNot Nothing AndAlso ListGroupersCareGroup.Count > 0 Then
                For Each ObjGroupersCareGroup As GroupersCareGroup In ListGroupersCareGroup
                    .GroupersCareGroup.Add(ObjGroupersCareGroup)
                Next
            End If

            .BillingConceptCopayId = INDSleBillingConceptCopay.EditValue
            .AffectBudget = AffectBudget
            .BillingBudgetId = BillingBudgetId
            .PromissoryNoteBudgetId = PromissoryNoteBudgetId
            .AccountReceivablePreviousValidityBudgetId = AccountReceivablePreviousValidityBudgetId
            .PortfolioRecoveryBudgetId = PortfolioRecoveryBudgetId
            .ThirdPartyInvoiceCopayFixedAmount = INDGleThirdCopayFixedAmount.EditValue
            .TypeLiquidationEmergencyStays = TypeLiquidationEmergencyStays
            .LiquidateDayDischarge = LiquidateDayDischarge
            If INDlyItemMinimumObservationTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .MinimumObservationTime = MinimumObservationTime
            Else
                .MinimumObservationTime = 0
            End If
            If INDlyItemMaximumObservationTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .MaximumObservationTime = MaximumObservationTime
            Else
                .MaximumObservationTime = 0
            End If
            .HoursOfRecoveryIncluded = HoursOfRecoveryIncluded

            If ListCareGroupDefinitionRate IsNot Nothing AndAlso ListCareGroupDefinitionRate.Count > 0 Then
                For Each item In ListCareGroupDefinitionRate
                    .CareGroupDefinitionRate.Add(item)
                Next
            End If

            If ListDeleteCareGroupDefinitionRate IsNot Nothing AndAlso ListDeleteCareGroupDefinitionRate.Count > 0 Then
                For Each item In ListDeleteCareGroupDefinitionRate
                    .CareGroupDefinitionRate.Add(item)
                Next
            End If

            If ListCareGroupBillingItemsRestriction IsNot Nothing AndAlso ListCareGroupBillingItemsRestriction.Count > 0 Then
                For Each item In ListCareGroupBillingItemsRestriction
                    .CareGroupBillingItemsRestriction.Add(item)
                Next
            End If

            If ListDeleteCareGroupBillingItemsRestriction IsNot Nothing AndAlso ListDeleteCareGroupBillingItemsRestriction.Count > 0 Then
                For Each item In ListDeleteCareGroupBillingItemsRestriction
                    .CareGroupBillingItemsRestriction.Add(item)
                Next
            End If
            ''kkk
            For Each item In listCareGroupInvoiceCategories
                .CareGroupInvoiceCategories.Add(item)
            Next

            If listCareGroupInvoiceCategoriesDelete IsNot Nothing Then
                For Each item In listCareGroupInvoiceCategoriesDelete
                    .CareGroupInvoiceCategories.Add(item)
                Next
            End If

            Dim ListDeleteCareGroupMixLiquidation As List(Of CareGroupMixLiquidation) = .CareGroupMixLiquidation.ToList()
            .CareGroupMixLiquidation.Clear()

            listCareGroupMixLiquidation.ForEach(Sub(x)
                                                    .CareGroupMixLiquidation.Add(x)
                                                End Sub)

            ListDeleteCareGroupMixLiquidation.ForEach(Sub(x)
                                                          x.MarkAsDeleted
                                                          .CareGroupMixLiquidation.Add(x)
                                                      End Sub)


            If .Id > 0 Then
                .MarkAsModified()
            End If
            ''Logica para guardar parametrizacion de autorizacion con tipo de UF
            'For Each itemDetail As ControlByTypeFunctionalUnit In INDGcUniFun.DataSource
            '    .ControlByTypeFunctionalUnit.Add(itemDetail) 
            'Next
        End With

    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            _isLoading = True
            Try
                Me.BarraBotones.StatusRecordVisible = True
                Using Model As New MCareGroup(CStr(Me.Tag))
                    AsyncLoader(True)
                    careGroup = (Await Model.GetCareGroup(INDbtnCode.Text.Trim)).ObjectEmbbeded
                    INDlyCareGroup.BeginUpdate()
                    If careGroup IsNot Nothing AndAlso careGroup.Id > 0 Then

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(careGroup.Id))
                            With careGroup
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                NameCG = .Name
                                CareGroupType = .CareGroupType
                                INDsleApplyRIAS.EditValue = .ApplyRIAS
                                ContractId = .ContractId
                                INDsleContractId.Properties.NullText = .ContractDescription
                                LiquidationType = .LiquidationType
                                CostCenterId = .CostCenterId
                                INDsleCostCenter.Properties.NullText = .CostCenterDescription
                                AuthorizationRequired = .AuthorizationRequired
                                CollectionMethod = .MethodFixedAmountCollectionReport
                                BillingPeriod = .BillingPeriod
                                MaximumIndividualBilling = .MaximumIndividualBilling
                                PeriodMaximumBilling = .PeriodMaximumBilling
                                EntityType = .EntityType
                                INDsleEntityType.Properties.NullText = .NameEntityType
                                ExtramuralPharmaceuticalDispensing = .ExtramuralPharmaceuticalDispensing
                                DiscountContractedCustomer = .DiscountContractedCustomer

                                INDseDiscountContractedCustomer.Properties.MaxLength = 6
                                INDseDiscountContractedCustomer.Properties.MaxValue = 100

                                RequirementsTemplateId = .RequirementsTemplateId
                                INDsleRequirementsTemplateId.Properties.NullText = .RequirementTemplateDescription
                                InvoiceDeadlines = .InvoiceDeadlines

                                ProcedureTemplateId = .ProcedureTemplateId
                                INDsleProcedureTemplateId.Properties.NullText = .ProcedureTemplateDescription

                                ProductRateId = .ProductRateId
                                INDsleProductTemplateId.Properties.NullText = .ProductTemplateDescription
                                ConceptToBill = .ConceptToBill
                                ContractAccountingStructureId = .ContractAccountingStructureId
                                INDsleContractAccountingStructure.Properties.NullText = .ContractAccountingStructureDescription

                                TechnicalNoteId = .TechnicalNoteId
                                INDSleTechnicalNote.Properties.NullText = .TechnicalNoteDescription

                                TypeLiquidationOxygen = .TypeLiquidationOxygen

                                AffectBudget = .AffectBudget
                                If AffectBudget Then
                                    BudgetaryEntityId = .BudgetaryEntityId
                                    INDsleBudgetaryEntityId.Properties.NullText = .BudgetaryEntityDescription
                                    BudgetaryValidityId = .BudgetaryValidityId
                                    INDsleBudgetaryValidityId.Properties.NullText = .BudgetaryValidityDescription
                                    BillingBudgetId = .BillingBudgetId
                                    INDsleBillingBudgetId.Properties.NullText = .BillingBudgetDescription
                                    PromissoryNoteBudgetId = .PromissoryNoteBudgetId
                                    INDslePromissoryNoteBudgetId.Properties.NullText = .PromissoryNoteBudgetDescription
                                    AccountReceivablePreviousValidityBudgetId = .AccountReceivablePreviousValidityBudgetId
                                    INDsleAccountReceivablePreviousValidityBudgetId.Properties.NullText = .AccountReceivablePreviousValidityBudgetDescription
                                    PortfolioRecoveryBudgetId = .PortfolioRecoveryBudgetId
                                    INDslePortfolioRecoveryBudgetId.Properties.NullText = .PortfolioRecoveryBudgetDescription
                                End If

                                TypeLiquidationEmergencyStays = .TypeLiquidationEmergencyStays
                                LiquidateDayDischarge = .LiquidateDayDischarge
                                MinimumObservationTime = .MinimumObservationTime
                                MaximumObservationTime = .MaximumObservationTime
                                HoursOfRecoveryIncluded = .HoursOfRecoveryIncluded
                                INDGleThirdCopayFixedAmount.EditValue = .ThirdPartyInvoiceCopayFixedAmount

                                INDSleBillingConceptCopay.EditValue = .BillingConceptCopayId
                                INDSleBillingConceptCopay.Properties.NullText = .BillingConceptCopayCodeName
                                Status = .Status

                                ListCareGroupDefinitionRate = .CareGroupDefinitionRate.ToList
                                ListCareGroupBillingItemsRestriction = .CareGroupBillingItemsRestriction.ToList

                                listCareGroupInvoiceCategories = Await Model.listCareGroupInvoiceCategoriesByCareGroupId(.Id)
                                INDGcInvoiceCategory.DataSource = listCareGroupInvoiceCategories
                                INDGcInvoiceCategory.RefreshDataSource()
                                INDgcDefinitionRate.DataSource = Nothing
                                INDgcDefinitionRate.DataSource = ListCareGroupDefinitionRate
                                INDgcBillingItemsRestriction.DataSource = Nothing
                                INDgcBillingItemsRestriction.DataSource = ListCareGroupBillingItemsRestriction

                                ListGroupersCareGroup = .GroupersCareGroup.ToList()
                                If .ControlByTypeFunctionalUnit IsNot Nothing AndAlso .ControlByTypeFunctionalUnit.Count > 0 Then
                                    INDGcUniFun.DataSource = .ControlByTypeFunctionalUnit
                                    'careGroup.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                                Else
                                    'Logica para cargar valores por defecto para autorizacion por tipo de unidad funcional.
                                    CargarDataSourceTipoUF()
                                End If

                                If .CareGroupPackage IsNot Nothing AndAlso .CareGroupPackage.Count > 0 Then
                                    INDGcPackages.DataSource = .CareGroupPackage.ToList()
                                End If
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.careGroup.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = careGroup.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If

                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            Me.BarraBotones.SetDocuments(careGroup.Id, Me.Tag.ToString(), Nothing, GetType(CareGroup).Name)
                            'Se oculta el boton de importar que sirve para el Dashboard PGP
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True

                            'Si el tipo de liquidación es de capitacion(PgP) y el usuario tiene el permiso, se habilita el botón
                            If LiquidationType = 5 AndAlso (From x In Me.BarraBotones.PermissionsForm Where x.Key = 83 Select x).Count > 0 Then
                                'Se muestra el botón
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                                'Se cambia el nombre al botón
                                Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.ImportarInformacion, "Dashboard PGP")
                            End If

                            If LiquidationType = 2 OrElse LiquidationType = 5 Then
                                INDlyItemCollectionMethod.ShowLayout()
                            Else
                                INDlyItemCollectionMethod.HideLayout()
                            End If

                            INDsleLiquidationType_EditValueChanged(Nothing, Nothing)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewCareGroup()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    LoadUnitDoseTypes(careGroup.CareGroupMixLiquidation)
                    INDlyCareGroup.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
        _isLoading = False
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewCareGroup() As Task
        CargarDataSourceTipoUF()
        careGroup = New CareGroup() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.ContractSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.ContractSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.ContractSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If careGroup.Id > 0 Then
            Using model As New MCareGroup(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not careGroup.Status
                Dim Result = Await model.ChangeState(careGroup.Id, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    Me.careGroup = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Else
                    INDbtnCode.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Obtiene la informacion de las filas de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetChildsRows(view As GridView, groupRowHandle As Integer, ListRows As List(Of CareGroupRate))
        If Not view.IsGroupRow(groupRowHandle) Then
            Return
        End If

        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRows(view, childHandle, ListRows)
            Else
                Dim row As Object = view.GetRow(childHandle)
                If Not ListRows.Contains(row) Then
                    ListRows.Add(row)
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Obtiene los childsRowHandles
    ''' </summary>
    ''' <param name="view"></param>
    ''' <param name="groupRowHandle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetChildRowsHandles(view As GridView, groupRowHandle As Integer) As Integer
        Dim childRows As Integer = 0
        If Not view.IsGroupRow(groupRowHandle) Then
            childRows = 1
            Return childRows
        End If
        Return childRows
    End Function

    ''' <summary>
    ''' Metodo que agrega una definicion de tarifa a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDefinitionRate()
        Dim errors As String = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If ListCareGroupDefinitionRate Is Nothing Then
            ListCareGroupDefinitionRate = New List(Of CareGroupDefinitionRate)
        Else
            Dim cont As Integer = 0
            If modeModify = False Then 'Cuando se agrega se valida con el listado que esta en la rejilla
                cont = ValidateList(ListCareGroupDefinitionRate)
            Else 'Cuando se modifica se valida con el listado emergente
                cont = ValidateList(ListCompare)
            End If
            If cont > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La definicion de tarifa " + INDsleDefinitionRate.Text + " ya existe en la lista con las fechas " + InitialDate.ToString + " - " + EndDate.ToString
                Exit Sub
            End If
        End If

        'Se instancia la entidad siempre y cuando se este agregando
        If modeModify = False Then
            careGroupDefinitionRate = New CareGroupDefinitionRate
        End If
        With careGroupDefinitionRate
            .DefinitionRateId = DefinitionRateId
            .DefinitionRateDescription = INDsleDefinitionRate.Text
            .InitialDate = InitialDate
            .EndDate = EndDate
        End With
        If modeModify = False Then
            ListCareGroupDefinitionRate.Add(careGroupDefinitionRate)
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
        End If
        INDgcDefinitionRate.DataSource = Nothing
        INDgcDefinitionRate.DataSource = ListCareGroupDefinitionRate
        CleanControlsPopup()
        INDsleDefinitionRate.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que agrega un servicio no facturado a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddBillingItemsRestriction()
        Dim errors As String = ValidateControlsPopupOfBillingItemsRestriction()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If ListCareGroupBillingItemsRestriction Is Nothing OrElse ListCareGroupBillingItemsRestriction.Count <= 0 Then
            ListCareGroupBillingItemsRestriction = New List(Of CareGroupBillingItemsRestriction)
        Else
            Dim cont As Integer = 0
            If modeModify = False Then 'Cuando se agrega se valida con el listado que esta en la rejilla
                cont = ValidateListSNF(ListCareGroupBillingItemsRestriction)
            Else 'Cuando se modifica se valida con el listado emergente
                cont = ValidateListSNF(ListCompareSNF)
            End If
            If cont > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El servicio no facturado " + INDsleBillingItemsRestriction.Text + " ya existe en la lista."
                Exit Sub
            End If
        End If

        'Se instancia la entidad siempre y cuando se este agregando
        If modeModify = False Then
            careGroupBillingItemsRestriction = New CareGroupBillingItemsRestriction
        End If
        With careGroupBillingItemsRestriction
            .BillingItemsRestrictionId = BillingItemsRestrictionId
            .BillingItemsRestrictionDescription = INDsleBillingItemsRestriction.Text
        End With
        If modeModify = False Then
            ListCareGroupBillingItemsRestriction.Add(careGroupBillingItemsRestriction)
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
        End If
        INDgcBillingItemsRestriction.DataSource = Nothing
        INDgcBillingItemsRestriction.DataSource = ListCareGroupBillingItemsRestriction
        CleanControlsPopup()
        INDsleBillingItemsRestriction.Focus()
    End Sub

    ''' <summary>
    ''' Limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        DefinitionRateId = Nothing
        INDsleDefinitionRate.Properties.NullText = String.Empty
        InitialDate = Nothing
        EndDate = Nothing
        modeModify = False
        'servicios no facturados
        BillingItemsRestrictionId = Nothing
        INDsleBillingItemsRestriction.Properties.NullText = String.Empty
    End Sub

    ''' <summary>
    ''' Valida que en el listado no exista la misma definicion de tarifa y fechas
    ''' </summary>
    ''' <param name="listValidate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateList(listValidate As List(Of CareGroupDefinitionRate)) As Integer
        Dim list As List(Of CareGroupDefinitionRate) = listValidate.FindAll(Function(item) ((InitialDate >= item.InitialDate AndAlso InitialDate <= item.EndDate) OrElse (EndDate >= item.InitialDate AndAlso EndDate <= item.EndDate) OrElse (InitialDate < item.InitialDate) AndAlso (EndDate > item.EndDate)))
        Return list.Count
        'Se cambia a peticion de cristian que se valide que no pueda existir el mismo rango de fechas para cualquier definicion de tarifa
        'Dim list As List(Of CareGroupDefinitionRate) = listValidate.FindAll(Function(item) ((InitialDate >= item.InitialDate AndAlso InitialDate <= item.EndDate) OrElse (EndDate >= item.InitialDate AndAlso EndDate <= item.EndDate) OrElse (InitialDate < item.InitialDate) AndAlso (EndDate > item.EndDate)) AndAlso (DefinitionRateId = item.DefinitionRateId))
        'Return list.Count
    End Function

    ''' <summary>
    ''' Valida que en el listado no exista el misma servicio no devuelto y fechas
    ''' </summary>
    ''' <param name="listValidate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateListSNF(listValidate As List(Of CareGroupBillingItemsRestriction)) As Integer
        Dim list As List(Of CareGroupBillingItemsRestriction) = listValidate.FindAll(Function(item) BillingItemsRestrictionId = BillingItemsRestrictionId)
        Return list.Count
    End Function

    ''' <summary>
    ''' Valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim listErrors As New StringBuilder
        If DefinitionRateId = 0 Then
            listErrors.AppendLine("Debe ingresar una definición de tarifa.")
        End If
        If Object.Equals(InitialDate, Nothing) = True Then
            listErrors.AppendLine("Debe ingresar una fecha inicial.")
        End If
        If Object.Equals(EndDate, Nothing) = True Then
            listErrors.AppendLine("Debe ingresar una fecha final.")
        End If

        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Valida los controles del popup de servicios no facturados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopupOfBillingItemsRestriction() As String
        Dim listErrors As New StringBuilder

        If BillingItemsRestrictionId = 0 Then
            listErrors.AppendLine("Debe ingresar un servicio no facturado.")
        End If

        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Metodo que edita la definicion de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDefinitionRate()
        modeModify = True
        careGroupDefinitionRate = viewDefinitionRate.GetFocusedRow
        With careGroupDefinitionRate
            DefinitionRateId = .DefinitionRateId
            INDsleDefinitionRate.Properties.NullText = .DefinitionRateDescription
            InitialDate = .InitialDate
            EndDate = .EndDate
            GenerateListCompare()
        End With

        INDpceDefinitionRate.ShowPopup()
    End Sub

    ''' <summary>
    ''' Metodo que edita el servicio no facturado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditBillingItemsRestriction()
        modeModify = True
        careGroupBillingItemsRestriction = viewBillingItemsRestriction.GetFocusedRow
        With careGroupBillingItemsRestriction
            BillingItemsRestrictionId = .BillingItemsRestrictionId
            INDsleBillingItemsRestriction.Properties.NullText = .BillingItemsRestrictionDescription
            'InitialDate = .InitialDateS
            'EndDate = .EndDate
            GenerateListCompareSNF()
        End With

        INDpceBillingItemsRestriction.ShowPopup()
    End Sub

    ''' <summary>
    ''' Genera el listado para comparar cuando se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GenerateListCompare()
        ListCompare = New List(Of CareGroupDefinitionRate)(ListCareGroupDefinitionRate)
        ListCompare.Remove(careGroupDefinitionRate)
    End Sub

    ''' <summary>
    ''' Genera el listado para comparar cuando se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GenerateListCompareSNF()
        ListCompareSNF = New List(Of CareGroupBillingItemsRestriction)(ListCareGroupBillingItemsRestriction)
        ListCompareSNF.Remove(careGroupBillingItemsRestriction)
    End Sub

    ''' <summary>
    ''' Metodo que elimina la definicion de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDefinitionRate()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        careGroupDefinitionRate = viewDefinitionRate.GetFocusedRow
        ListCareGroupDefinitionRate.Remove(careGroupDefinitionRate)

        If careGroupDefinitionRate.Id > 0 Then
            If ListDeleteCareGroupDefinitionRate Is Nothing Then
                ListDeleteCareGroupDefinitionRate = New List(Of CareGroupDefinitionRate)
            End If
            careGroupDefinitionRate.MarkAsDeleted()
            ListDeleteCareGroupDefinitionRate.Add(careGroupDefinitionRate)
        End If

        INDgcDefinitionRate.DataSource = Nothing
        INDgcDefinitionRate.DataSource = ListCareGroupDefinitionRate
    End Sub

    ''' <summary>
    ''' Metodo que elimina los servicios no facturados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteBillingItemsRestriction()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        careGroupBillingItemsRestriction = viewBillingItemsRestriction.GetFocusedRow
        ListCareGroupBillingItemsRestriction.Remove(careGroupBillingItemsRestriction)

        If careGroupBillingItemsRestriction.Id > 0 Then
            If ListDeleteCareGroupBillingItemsRestriction Is Nothing Then
                ListDeleteCareGroupBillingItemsRestriction = New List(Of CareGroupBillingItemsRestriction)
            End If
            careGroupBillingItemsRestriction.MarkAsDeleted()
            ListDeleteCareGroupBillingItemsRestriction.Add(careGroupBillingItemsRestriction)
        End If

        INDgcBillingItemsRestriction.DataSource = Nothing
        INDgcBillingItemsRestriction.DataSource = ListCareGroupBillingItemsRestriction
    End Sub

    ''' <summary>
    ''' evento para eliminar un registro de la rejilla y la entidad
    ''' </summary>
    ''' <param name="CareGroupPackage"></param>
    Private Sub DeletePackage(CareGroupPackage As Domain.Entities.CareGroupPackage)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        CareGroupPackage.MarkAsDeleted()
        Me.careGroup.CareGroupPackage.Remove(CareGroupPackage)
        INDGcPackages.DataSource = Me.careGroup.CareGroupPackage.ToList()
        INDGvPackage.RefreshData()
    End Sub

    Private Sub CleanBudgetInterface(level As Integer)
        If level < 1 Then
            BudgetaryEntityId = Nothing
            INDsleBudgetaryEntityId.Properties.NullText = String.Empty
        End If
        If level < 2 Then
            BudgetaryValidityId = Nothing
            INDsleBudgetaryValidityId.Properties.NullText = String.Empty
            BudgetaryValidityXpo = Nothing
        End If
        If level < 3 Then
            BillingBudgetId = Nothing
            INDsleBillingBudgetId.Properties.NullText = String.Empty
            BillingBudgetXpo = Nothing

            PromissoryNoteBudgetId = Nothing
            INDslePromissoryNoteBudgetId.Properties.NullText = String.Empty
            PromissoryNoteBudgetXpo = Nothing

            AccountReceivablePreviousValidityBudgetId = Nothing
            INDsleAccountReceivablePreviousValidityBudgetId.Properties.NullText = String.Empty
            AccountReceivablePreviousValidityBudgetXpo = Nothing

            PortfolioRecoveryBudgetId = Nothing
            INDslePortfolioRecoveryBudgetId.Properties.NullText = String.Empty
            PortfolioRecoveryBudgetXpo = Nothing
        End If
    End Sub

    Private Async Sub getSettingsContract()
        Using model As New MSettingsContract(Tag)
            Dim res = Await model.GetSettingsContractByOperatingUnitId(Me._idOperativeUnit)

            If res Is Nothing OrElse Not res?.StateResult Then
                Mensaje(EeventViewerImages.Advertencia) = "Error al Consultar parámetros de Contratos"
                Exit Sub
            End If

            INDsleCareGroupType.Properties.DataSource = res.ObjectEmbbeded.SettingContractCareGroupType?.Select(Function(m)
                                                                                                                    m.CareGroupTypeName = AttentionGroupsDictionary(indigo.LanguageCulture).Find(Function(o) o.Item1 = m.CareGroupType).Item2
                                                                                                                    Return m

                                                                                                                End Function).ToList()
        End Using
    End Sub

    ''' <summary>
    ''' Carga los tipos de dosis unitarias creadas, si no existen no se muestra el 
    ''' segmento "liquidacion de mezclas"
    ''' Se cargaran que no sean tipo
    ''' tipo Reempaque - tipo Reenvase                                                                                                                 
    ''' </summary>
    Private Sub LoadUnitDoseTypes(listCareGroupMixLiquidation)

        Dim gridView As GridView = CType(INDgcMixLiquidation.MainView, GridView)
        gridView.Columns.Clear()
        gridView.GridControl.DataSource = Nothing
        INDlygMixLiquidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Dim ListUnitDoseType = Presenter.ListUnitDoseTypeNotRepackage
        If ListUnitDoseType IsNot Nothing And ListUnitDoseType.Count > 0 Then
            INDgcMixLiquidation.DataSource = CreateTableUnitDoseTypes(gridView, ListUnitDoseType, listCareGroupMixLiquidation)
        Else
            INDlygMixLiquidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Public Function GetItemsComboBoxUnitDoseTypes() As Object
        Return New String() {"Aplicación en paciente", "Terminación de campaña"}
    End Function

    Public Function CreateTableUnitDoseTypes(Table, ListUnitDoseType, listCareGroupMixLiquidation) As DataTable
        ' Agregar ComboBoxEditor a la columna "Cobro"
        Dim comboBoxEditor As New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        comboBoxEditor.Items.AddRange(GetItemsComboBoxUnitDoseTypes)
        ' Accede a la propiedad TextEditStyle
        Dim textEditStyle As DevExpress.XtraEditors.Controls.TextEditStyles = comboBoxEditor.TextEditStyle
        ' Establece TextEditStyles en DisableTextEditor
        textEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        ' Asigna la propiedad TextEditStyle modificada
        comboBoxEditor.TextEditStyle = textEditStyle

        Dim col As GridColumn = Table.Columns.AddField("Código")
        col.VisibleIndex = Table.Columns.Count
        col.Caption = "Código"

        Dim col2 As GridColumn = Table.Columns.AddField("Nombre")
        col2.VisibleIndex = Table.Columns.Count
        col2.Caption = "Nombre"

        Dim col3 As GridColumn = Table.Columns.AddField("Estado")
        col3.VisibleIndex = Table.Columns.Count
        col3.Caption = "Estado"

        Dim col4 As GridColumn = Table.Columns.AddField("Cobro")
        col4.VisibleIndex = Table.Columns.Count
        col4.Caption = "Cobro"
        col4.ColumnEdit = comboBoxEditor

        Dim col5 As GridColumn = Table.Columns.AddField("UnitDoseTypeId")
        col5.VisibleIndex = Table.Columns.Count
        col5.Caption = "UnitDoseTypeId"
        col5.Visible = False

        Dim dt As New DataTable
        dt.Columns.Add("Código").ReadOnly = True
        dt.Columns.Add("Nombre").ReadOnly = True
        dt.Columns.Add("Estado").ReadOnly = True
        dt.Columns.Add("Cobro", GetType(String))
        dt.Columns.Add("UnitDoseTypeId", GetType(Integer))

        For Each itemView In ListUnitDoseType
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = itemView.Code
            row.Item("Nombre") = itemView.Description
            row.Item("Estado") = itemView.WordState
            If itemView.State = False Then
                row.Item("Cobro") = itemsUnitDoses(0)
            Else
                row.Item("Cobro") = Nothing
            End If
            row.Item("UnitDoseTypeId") = itemView.Id

            ''Validacion cuando se esta cargando el listado loadControls
            If listCareGroupMixLiquidation IsNot Nothing Then
                For Each item As CareGroupMixLiquidation In listCareGroupMixLiquidation

                    If item.UnitDoseTypeId = itemView.Id Then
                        If item.TypePayment = 1 Then
                            row.Item("Cobro") = itemsUnitDoses(0)
                        ElseIf item.TypePayment = 2 Then
                            row.Item("Cobro") = itemsUnitDoses(1)
                        End If
                    End If
                Next
            End If

            dt.Rows.Add(row)
        Next


        Return dt
    End Function

    ''' <summary>
    '''  Recorre el listado liquidacion de mezclas, validando que se haya escogido 
    '''  un tipo de cobro, para posteriormente  guardar la información
    ''' </summary>
    ''' <returns></returns>
    Public Function GetDataMixLiquidation()
        Dim gridView As GridView = CType(INDgcMixLiquidation.MainView, GridView)
        listCareGroupMixLiquidation = New List(Of CareGroupMixLiquidation)
        For i As Integer = 0 To gridView.RowCount - 1
            Dim row As DataRowView = TryCast(gridView.GetRow(i), DataRowView)
            If row IsNot Nothing Then
                If Not (row.Row.ItemArray(3) Is DBNull.Value OrElse row.Row.ItemArray(3) Is Nothing) Then
                    Dim ObjCareGroupMixLiquidation As New CareGroupMixLiquidation
                    ObjCareGroupMixLiquidation.CareGroupId = If(Me.careGroup.Id > 0, Me.careGroup.Id, 0)
                    If row.Row.ItemArray(3) = itemsUnitDoses(0) Then
                        ObjCareGroupMixLiquidation.TypePayment = 1
                    End If
                    If row.Row.ItemArray(3) = itemsUnitDoses(1) Then
                        ObjCareGroupMixLiquidation.TypePayment = 2
                    End If
                    ObjCareGroupMixLiquidation.UnitDoseTypeId = row.Row.ItemArray(4)
                    If listCareGroupMixLiquidation Is Nothing Then
                        listCareGroupMixLiquidation = New List(Of CareGroupMixLiquidation)
                    End If
                    listCareGroupMixLiquidation.Add(ObjCareGroupMixLiquidation)
                End If
            End If
        Next
    End Function

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        GroupersCareGroup = Nothing
        ListGroupersCareGroup = Nothing
        Presenter = Nothing
        _sequence = Nothing
        _parameterBilling = Nothing
        careGroup = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        record = Nothing
        _isLoading = Nothing
        ListTypeLiquidationEmergencyStays = Nothing
        ListLiquidateDays = Nothing
        ListLiquidationType = Nothing
        ListBillingPeriod = Nothing
        ListConceptToBill = Nothing
        EntityTypeXpo = Nothing
        EntityType = Nothing
        ListRows = Nothing
        ListCareGroupRate = Nothing
        ListCareGroupRateSave = Nothing
        ListLiquidationTypeCareGroupRate = Nothing
        ListDeleteCareGroupRate = Nothing
        ListCareGroupDefinitionRate = Nothing
        ListDeleteCareGroupDefinitionRate = Nothing
        modeModify = Nothing
        careGroupDefinitionRate = Nothing
        ListCompare = Nothing
        ListTypeLiquidationOxygen = Nothing
        CollectionMethod = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmCareGroup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyCareGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PCareGroup(Me)
        Presenter.GetSequense()
        LoadStatus()
        Deshacer()
        InitializeTuples()

        AsyncLoader(True)
        Using model As New MCareGroup(MyTag)
            INDSleInvoiceCategory.Properties.DataSource = model.ListInvoiceCategories()
        End Using
        Await Me.LoadParameterBilling()
        AsyncLoader(False)

        IndigoGridControl1.RefreshGrid(INDgcDefinitionRate)
        IndigoGridControl1.RefreshGrid(INDGcInvoiceCategory)
        IndigoGridControl1.RefreshGrid(INDgcBillingItemsRestriction)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView2.SetListAcction(INDGvInvoiceCategory, ListActions)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(viewDefinitionRate, ListActions)
        IndigoGridView4.SetListAcction(viewBillingItemsRestriction, ListActions)
        Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Confirmar, "Parámetros PGP")
        Dim ListActionsPackages As New List(Of eAcciones)
        ListActionsPackages.Add(eAcciones.Remove)
        IndigoGridView3.SetListAcction(INDGvPackage, ListActionsPackages)
        INDlyItemAuthorizationRequired.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemCollectionMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LayoutControlGroup4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub





#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCareGroup_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewCareGroup()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter o f4 sobre el control de popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceDefinitionRate_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceDefinitionRate.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceDefinitionRate.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter o f4 sobre el control de popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceBillingItemsRestriction_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceBillingItemsRestriction.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceBillingItemsRestriction.ShowPopup()
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleContractAccountingStructure_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleContractAccountingStructure.QueryPopUp
        If ContractAccountingStructureXpo Is Nothing Then
            Presenter.InitializeContractAccountingStructure()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centro costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If CostCenterXpo Is Nothing Then
            Presenter.InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleContractId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleContractId.QueryPopUp
        If INDsleContractId.Properties.DataSource Is Nothing Then
            Presenter.InitializeContract()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de plantilla de requerimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRequirementsTemplateId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRequirementsTemplateId.QueryPopUp
        If INDsleRequirementsTemplateId.Properties.DataSource Is Nothing Then
            Presenter.InitializeRequirementTemplate()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de plantilla de procedimientos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProcedureTemplateId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProcedureTemplateId.QueryPopUp
        If INDsleProcedureTemplateId.Properties.DataSource Is Nothing Then
            Presenter.InitializeProcedureTemplate()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de plantilla de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProductTemplateId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProductTemplateId.QueryPopUp
        If INDsleProductTemplateId.Properties.DataSource Is Nothing Then
            Presenter.InitializeProductTemplate()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de definicion de tarifas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDefinitionRate_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDefinitionRate.QueryPopUp
        If DefinitionRateXpo Is Nothing Then
            Presenter.InitializeDefinitionRate()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de servicios no facturables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBillingItemsRestriction_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBillingItemsRestriction.QueryPopUp
        If BillingItemsRestrictionXpo Is Nothing Then
            Presenter.InitializeBillingItemsRestriction()
        End If
    End Sub

    Private Sub INDsleBudgetaryEntityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetaryEntityId.QueryPopUp
        If BudgetaryEntityXpo Is Nothing Then
            Presenter.InitializeBudgetaryEntity()
        End If
    End Sub

    Private Sub INDsleBudgetaryValidityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetaryValidityId.QueryPopUp
        If BudgetaryValidityXpo Is Nothing Then
            Presenter.InitializeBudgetaryValidity(BudgetaryEntityId)
        End If
    End Sub

    Private Sub INDsleBillingBudgetId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBillingBudgetId.QueryPopUp
        If BillingBudgetXpo Is Nothing Then
            Presenter.InitializeBillingBudget(BudgetaryValidityId)
        End If
    End Sub

    Private Sub INDslePromissoryNoteBudgetId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePromissoryNoteBudgetId.QueryPopUp
        If PromissoryNoteBudgetXpo Is Nothing Then
            Presenter.InitializePromissoryNoteBudget(BudgetaryValidityId)
        End If
    End Sub

    Private Sub INDsleAccountReceivablePreviousValidityBudgetId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountReceivablePreviousValidityBudgetId.QueryPopUp
        If AccountReceivablePreviousValidityBudgetXpo Is Nothing Then
            Presenter.InitializeAccountReceivablePreviousValidityBudget(BudgetaryValidityId)
        End If
    End Sub

    Private Sub INDslePortfolioRecoveryBudgetId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePortfolioRecoveryBudgetId.QueryPopUp
        If PortfolioRecoveryBudgetXpo Is Nothing Then
            Presenter.InitializePortfolioRecoveryBudget(BudgetaryValidityId)
        End If
    End Sub

    Private Sub INDSlePackages_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlePackages.QueryPopUp
        If ContractPackageXpo Is Nothing Then
            Presenter.InitializeContractPackages()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleTechnicalNote_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleTechnicalNote.QueryPopUp
        If INDSleTechnicalNote.Properties.DataSource Is Nothing Then
            Presenter.InitializeTechnicalNote()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleContractAccountingStructure_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleContractAccountingStructure.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1926, Nothing, True)
            Presenter.InitializeContractAccountingStructure()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton para abrir el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCostCenter With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del control de contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleContractId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleContractId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmContract With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeContract()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del control de plantillas de requerimientos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRequirementsTemplateId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRequirementsTemplateId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmRequirementTemplate With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeRequirementTemplate()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del control de plantillas de procedimientos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProcedureTemplateId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProcedureTemplateId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmProcedureTemplate With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeProcedureTemplate()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del control de tarifas de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProductTemplateId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProductTemplateId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(306, "", True)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de definicion de tarifas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDefinitionRate_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleDefinitionRate.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1530, Nothing, True)
            Presenter.InitializeDefinitionRate()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de los servicios no facturables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBillingItemsRestriction_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBillingItemsRestriction.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1530, Nothing, True)
            Presenter.InitializeBillingItemsRestriction()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del control de nota tecnica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleTechnicalNote_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleTechnicalNote.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmTechnicalNote With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeContract()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Delegado
    ''' </summary>
    ''' <param name="ListView"></param>
    ''' <param name="ListCareGroupRateCollection"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Delegate Function LoadInfoDelegate(ListView As XPCollection, ListCareGroupRateCollection As XPCollection) As List(Of CareGroupRate)

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de plantilla de procedimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProcedureTemplateId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProcedureTemplateId.EditValueChanged
        If ProcedureTemplateId IsNot Nothing Then

        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de grupo de atencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCareGroupType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCareGroupType.EditValueChanged
        If CareGroupType IsNot Nothing Then
            INDlyCareGroup.BeginUpdate()
            If CareGroupType = 1 Then
                INDlyItemContractId.HideControl(False)
                INDlyItemApplyRIAS.HideControl(False)
            Else
                INDlyItemContractId.HideControl()
                INDlyItemApplyRIAS.HideControl()
                INDsleContractId.EditValue = Nothing
                INDsleApplyRIAS.EditValue = Nothing
            End If
            SetEntityType(CareGroupType)
            INDlyCareGroup.EndUpdate()
        End If
    End Sub

    ''' <summary>
    ''' metodo para establecer los tipos de entidad en el control
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetEntityType(_careGroupType As Byte)
        INDsleEntityType.Properties.NullText = String.Empty
        EntityType = Nothing
        EntityTypeXpo = Nothing
        Select Case _careGroupType
            Case 1, 2
                Me.TypeOfCompanyType = 1
            Case 3
                Me.TypeOfCompanyType = 2
            Case 4
                Me.TypeOfCompanyType = 3
        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de liquidacion para estancias de urgencias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTypeLiquidationEmergencyStays_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTypeLiquidationEmergencyStays.EditValueChanged
        If TypeLiquidationEmergencyStays IsNot Nothing Then
            If TypeLiquidationEmergencyStays = 1 Then
                INDlyItemMinimumObservationTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemMinimumObservationTime.AllowHide = False
                INDlyItemMaximumObservationTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemMaximumObservationTime.AllowHide = True
            Else
                INDlyItemMinimumObservationTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemMinimumObservationTime.AllowHide = True
                INDlyItemMaximumObservationTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemMaximumObservationTime.AllowHide = False
            End If
        Else
            INDlyItemMinimumObservationTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemMinimumObservationTime.AllowHide = True
            INDlyItemMaximumObservationTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemMaximumObservationTime.AllowHide = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de fecha inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdteInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteInitialDate.EditValueChanged
        If InitialDate IsNot Nothing Then
            INDdteEndDate.Properties.MinValue = InitialDate
        End If
    End Sub

    Private Sub INDsleAffectBudget_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAffectBudget.EditValueChanged
        INDlyItemBudgetaryEntityId.Visibility = If(AffectBudget, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyItemBudgetaryEntityId.AllowHide = Not AffectBudget
        INDlyItemBudgetaryEntityId.ShowInCustomizationForm = Not AffectBudget

        INDlyItemBudgetaryValidityId.Visibility = If(AffectBudget, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyItemBudgetaryValidityId.AllowHide = Not AffectBudget
        INDlyItemBudgetaryValidityId.ShowInCustomizationForm = Not AffectBudget

        INDlyItemBillingBudgetId.Visibility = If(AffectBudget, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyItemBillingBudgetId.AllowHide = Not AffectBudget
        INDlyItemBillingBudgetId.ShowInCustomizationForm = Not AffectBudget

        INDlyItemPromissoryNoteBudgetId.Visibility = If(AffectBudget, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyItemPromissoryNoteBudgetId.AllowHide = Not AffectBudget
        INDlyItemPromissoryNoteBudgetId.ShowInCustomizationForm = Not AffectBudget

        INDlyiAccountReceivablePreviousValidityBudgetId.Visibility = If(AffectBudget, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyiAccountReceivablePreviousValidityBudgetId.AllowHide = Not AffectBudget
        INDlyiAccountReceivablePreviousValidityBudgetId.ShowInCustomizationForm = Not AffectBudget

        INDlyiPortfolioRecoveryBudgetId.Visibility = If(AffectBudget, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyiPortfolioRecoveryBudgetId.AllowHide = Not AffectBudget
        INDlyiPortfolioRecoveryBudgetId.ShowInCustomizationForm = Not AffectBudget

        If Not AffectBudget Then
            CleanBudgetInterface(0)
        End If
    End Sub

    Private Sub INDsleBudgetaryEntityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetaryEntityId.EditValueChanged
        If _isLoading Then
            Exit Sub
        End If

        CleanBudgetInterface(1)
    End Sub

    Private Sub INDsleBudgetaryValidityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetaryValidityId.EditValueChanged
        If _isLoading Then
            Exit Sub
        End If

        CleanBudgetInterface(2)
    End Sub

#End Region

#Region "EditvalueChanging"
    Private Sub INDsleContractId_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDsleContractId.EditValueChanging
        If _isLoading Then
            Exit Sub
        End If
        If e.NewValue IsNot Nothing Then
            Dim contr As ViewListContractsSearchLookUpXpo = CType(CType(GvContract.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewListContractsSearchLookUpXpo)
            If contr.Status <> 1 Then
                Mensaje(EeventViewerImages.Advertencia) = "No puede seleccionar este contrato debido a que no se encuentra vigente"
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "MenuContextual"

    ''' <summary>
    ''' Menu que se despliega al presionar click sobre los botones de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString()
            Case "Remove"
                DeleteDefinitionRate()
            Case "Edit"
                EditDefinitionRate()
        End Select
    End Sub

    ''' <summary>
    ''' Menu que se despliega al presionar click sobre los botones de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction
        DeletePackage(DirectCast(INDGvPackage.GetFocusedRow, Domain.Entities.CareGroupPackage))
    End Sub

    ''' <summary>
    ''' Menu que se despliega al presionar click derecho sobre la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDefinitionRate()
            Case "Remove"
                DeleteDefinitionRate()
        End Select
    End Sub

    ''' <summary>
    ''' Menu que se despliega al presionar click sobre los botones de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView4_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView4.Click_ButtonAction, IndigoGridView4.ContexMenuActions
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString()
            Case "Remove"
                DeleteBillingItemsRestriction()
            Case "Edit"
                EditBillingItemsRestriction()
        End Select
    End Sub

    Private Sub INDBtnAddInvoiceCategory_Click(sender As Object, e As EventArgs) Handles INDBtnAddInvoiceCategory.Click
        If INDSleInvoiceCategory.EditValue Is Nothing Then
            Exit Sub
        End If
        If listCareGroupInvoiceCategories IsNot Nothing Then
            Dim categoryAdd = listCareGroupInvoiceCategories.Find(Function(x) x.InvoiceCategoriesId = INDSleInvoiceCategory.EditValue)
            If categoryAdd IsNot Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "La categoria seleccionada ya se encuentra agregada"
                Exit Sub
            End If
        End If
        Using model As New MCareGroup(MyTag)
            Dim category As Infrastructure.Data.Xpo.BillingRepository.InvoiceCategoriesXpo = model.GetInvoiceCategoriesById(INDSleInvoiceCategory.EditValue)
            Dim CareGroupInvoiceCategories As New CareGroupInvoiceCategories
            CareGroupInvoiceCategories.CodeNameInvoiceCategory = category.CodeName
            CareGroupInvoiceCategories.InvoiceCategoriesId = category.Id
            If listCareGroupInvoiceCategories Is Nothing Then
                listCareGroupInvoiceCategories = New List(Of CareGroupInvoiceCategories)
            End If
            listCareGroupInvoiceCategories.Add(CareGroupInvoiceCategories)
            INDGcInvoiceCategory.DataSource = listCareGroupInvoiceCategories
            INDGcInvoiceCategory.RefreshDataSource()
            INDSleInvoiceCategory.EditValue = Nothing
            INDSleInvoiceCategory.Focus()
        End Using
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim category = DirectCast(INDGvInvoiceCategory.GetFocusedRow, CareGroupInvoiceCategories)
            If category.Id > 0 Then
                If listCareGroupInvoiceCategoriesDelete Is Nothing Then
                    listCareGroupInvoiceCategoriesDelete = New List(Of CareGroupInvoiceCategories)
                End If
                listCareGroupInvoiceCategoriesDelete.Add(category.MarkAsDeleted())
            End If
            listCareGroupInvoiceCategories.Remove(category)
            INDGcInvoiceCategory.DataSource = listCareGroupInvoiceCategories
            INDGcInvoiceCategory.RefreshDataSource()
        End If
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceDefinitionRate_Popup(sender As Object, e As EventArgs) Handles INDpceDefinitionRate.Popup
        INDsleDefinitionRate.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceBillingItemsRestriction_Popup(sender As Object, e As EventArgs) Handles INDpceBillingItemsRestriction.Popup
        INDsleBillingItemsRestriction.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddDefinitionRate_Click(sender As Object, e As EventArgs) Handles INDbtnAddDefinitionRate.Click
        AddDefinitionRate()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddBillingItemsRestriction_Click(sender As Object, e As EventArgs) Handles INDbtnAddBillingItemsRestriction.Click
        AddBillingItemsRestriction()
    End Sub

    ''' <summary>
    ''' Evento para agregar Paquetes a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAddPackage_Click(sender As Object, e As EventArgs) Handles INDSbAddPackage.Click
        If _selectorContractPackages.GetKeys.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe Seleccionar almenos un paquete"
            Exit Sub
        End If
        Dim ListOfIds = _selectorContractPackages.GetKeys.Split(",").Select(Function(x) x.AsInt).ToList()

        If Me.careGroup.CareGroupPackage.Where(Function(x) ListOfIds.Contains(x.ContractPackageId)).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Uno de los paquete ya se encuentra agregado"
            Exit Sub
        End If

        Dim ListSelectedPacakge = Presenter.ListContractPackage(_selectorContractPackages.GetKeys)

        ListSelectedPacakge.ForEach(Sub(i As ContractPackageXpo)
                                        Dim CareGPackage = New Domain.Entities.CareGroupPackage
                                        With CareGPackage
                                            If Me.careGroup.Id > 0 Then
                                                .CareGroupId = Me.careGroup.Id
                                            End If
                                            .ContractPackageId = i.Id
                                            .PackageCodeName = i.CodeName
                                        End With
                                        Me.careGroup.CareGroupPackage.Add(CareGPackage)
                                    End Sub)
        INDGcPackages.DataSource = Me.careGroup.CareGroupPackage.ToList()
        INDGvPackage.RefreshData()
        INDSlePackages.Properties.NullText = String.Empty
        _selectorContractPackages.Clear()
    End Sub
#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceDefinitionRate_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceDefinitionRate.CloseUp
        If modeModify Then
            CleanControlsPopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceBillingItemsRestriction_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceBillingItemsRestriction.CloseUp
        If modeModify Then
            CleanControlsPopup()
        End If
    End Sub

#End Region

#End Region

#Region "Selector"
    Private _selectorContractPackages As SelectorCache = New SelectorCache("Id", "CodeName")
    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDSleGridViewPackage.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDSleGridViewPackage" Then
                e.Value = _selectorContractPackages.GetValue(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDSleGridViewPackage.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDSleGridViewPackage" Then
                selector = _selectorContractPackages
            End If
            If e.RowHandle >= 0 Then
                Dim row = view.GetRow(e.RowHandle)
                selector.SetValue(row)
            Else
                selector.Clear()
            End If
            view.RefreshData()
        End If
    End Sub

    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSlePackages.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSlePackages" Then
            searchLookupEdit.Properties.NullText = _selectorContractPackages.ToString()
        End If
    End Sub

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Barra botones: se habilita el importar para que funcione como el Dashboard PGP
    ''' </summary>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        OpenFormDashboardPGP()
    End Sub

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        GroupersCareGroup = Nothing
        OpenFormGroupersCareGroup()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            Await Me.LoadParameterBilling()
            getSettingsContract()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.ContractSequenceDetail IsNot Nothing Then
                If Not Me._sequence.ContractSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs)
        GroupersCareGroup = Nothing
        OpenFormGroupersCareGroup()
    End Sub

    Private Sub OpenFormGroupersCareGroup()
        Using formulario As New FrmGrouperCareGroup
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddAddGroupersCareGroupEventArgs, AddressOf ReturnAddEventArgs
            formulario.Groupers = ListGroupersCareGroup
            formulario.IdCareGroup = careGroup.Id
            'formulario.Size = New System.Drawing.Size(1090, 750)
            'formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub ReturnAddEventArgs(sender As Object, e As AddGroupersCareGroup)

        'Nuevo
        If ListGroupersCareGroup Is Nothing Then
            ListGroupersCareGroup = New List(Of GroupersCareGroup)
        End If
        'ListGroupersCareGroup.Add(e.GrouperCareGroup)

        If e.GrouperCareGroupList IsNot Nothing Then
            For Each i In e.GrouperCareGroupList
                If Not ListGroupersCareGroup.Any(Function(o) o.GroupersId = i.GroupersId) Then
                    ListGroupersCareGroup.Add(i)
                End If
            Next
        End If
    End Sub

    Private Sub INDsleLiquidationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleLiquidationType.EditValueChanged
        Dim liquidationType As Integer = Convert.ToInt32(INDsleLiquidationType.EditValue)

        Select Case liquidationType
            Case 2, 5
                INDlyItemCollectionMethod.ShowLayout()
                INDLciThirdCopayFixedAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                If liquidationType = 5 Then
                    INDLciTechnicalNote.HideControl(False)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                End If
            Case Else
                INDlyItemCollectionMethod.HideLayout()
                INDLciTechnicalNote.HideControl(True)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                INDLciThirdCopayFixedAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End Select
    End Sub
#End Region

#Region "Cargar tabla parametrizacion tipo de unidad funcional."
    Private Sub CargarDataSourceTipoUF()
        INDdtControlTipoUF = New Domain.Entities.TrackableCollection(Of ControlByTypeFunctionalUnit)
        For i As Integer = 1 To 4 'Numero de tipos de unidades funcionales.
            Dim ObjetoControlByTUF As ControlByTypeFunctionalUnit = New ControlByTypeFunctionalUnit
            ObjetoControlByTUF.UnitType = i
            ObjetoControlByTUF.MandatoryAuthorization = False
            INDdtControlTipoUF.Add(ObjetoControlByTUF)
        Next
        INDGcUniFun.DataSource = INDdtControlTipoUF
    End Sub

    Private Sub INDsleEntityType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleEntityType.QueryPopUp
        If EntityTypeXpo Is Nothing AndAlso Me.TypeOfCompanyType IsNot Nothing Then
            Presenter.InitializeEntityTypeXpo(Type:=Me.TypeOfCompanyType)
        End If
    End Sub

    Private Sub INDSleBillingConceptCopay_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBillingConceptCopay.QueryPopUp
        If INDSleBillingConceptCopay.Properties.DataSource Is Nothing Then
            INDSleBillingConceptCopay.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BillingService.ListXPInstantFeedbackSource(Of BillingRepository.BillingConceptXpo)("ConceptType=3 And Status=True")
        End If
    End Sub

    Private Sub INDgcMixLiquidation_Click(sender As Object, e As EventArgs)

    End Sub
#End Region
End Class