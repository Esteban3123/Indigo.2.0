'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 20-01-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Drawing
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Utils
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP

#End Region

Public Class FrmEquipmentCatalog
    Implements IEquipmentCatalog, ICustomizableForm

#Region "Globals"

    ''' <summary>
    ''' Listado que establece si o no
    ''' </summary>
    Private ListYesNot As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' bandera para indicar que se está cargando un registro
    ''' </summary>
    Private _isLoading As Boolean

    ''' <summary>
    ''' índice del registro que se esta editando para luego insertarlo en la misma posición que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim indexEditRecord As Integer
    ' ''' <summary>
    ' ''' presenter de remisión de entrada
    ' ''' </summary>
    ' ''' <remarks></remarks>
    Dim presenter As PEquipmentCatalog

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const NAME_MODULE = "FixedAssets"
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues
    ''' <summary>
    ''' Secuencia numérica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.FixedAssetSequence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo búsqueda
    ''' </summary>
    Private _searchMode As Boolean
    ''' <summary>
    ''' Id de la configuración de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordFixedAsset

    ''' <summary>
    ''' entidad de remisión de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetEquipmentCatalog As FixedAssetItemCatalog
    ''' <summary>
    ''' listado del detalle de la remisión
    ''' </summary>
    ''' <remarks></remarks>
    Dim listFixedAssetEquipmentCatalogDetail As List(Of FixedAssetItemCatalogDetail) = New List(Of FixedAssetItemCatalogDetail)
    ''' <summary>
    ''' listado del detalle de la remisión para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listFixedAssetEquipmentCatalogDetailDelete As List(Of FixedAssetItemCatalogDetail)
    ''' <summary>
    ''' entidad del detalle de la remisión
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetEquipmentCatalogDetail As FixedAssetItemCatalogDetail

    Dim FlagType As String

    Dim IdIngressAccount As Integer

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListAdquisitionType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de detalles de tipo de adquisición del catálogo
    ''' </summary>
    Dim ListFixedAssetItemCatalogAdquisitionType As List(Of FixedAssetItemCatalogAdquisitionType)

    ''' <summary>
    ''' Listado de eliminados del detalle de tipo de adquisición del catálogo
    ''' </summary>
    Dim ListDeleteFixedAssetItemCatalogAdquisitionType As List(Of FixedAssetItemCatalogAdquisitionType)

    ''' <summary>
    ''' Bandera para saber si el formulario ha sido cargado
    ''' </summary>
    Dim IsLoaded As Boolean = False

    ''' <summary>
    ''' Bandera para deshabilitar temporalmente el evento
    ''' </summary>
    Private disableEventFlag As Boolean

    ''' <summary>
    ''' Almacena Datos importados correctos
    ''' </summary>
    Private _equipmentCatalogDetail As SP_SetFixedAssetItemCatalogDetailFromFile_Result()

    ''' <summary>
    ''' Almacena datos importados incorrectos
    ''' </summary>
    Private _equipmentCatalogDetailError As SP_SetFixedAssetItemCatalogDetailFromFile_Result()
#End Region

#Region "Properties"
    Public Property Classification As Byte Implements IEquipmentCatalog.Classification
        Get
            Return INDGleClassification.EditValue
        End Get
        Set(value As Byte)
            INDGleClassification.EditValue = value
        End Set
    End Property

    Public Property AffectBudget As Boolean Implements IEquipmentCatalog.AffectBudget
        Get
            Return INDsleAffectBudget.EditValue
        End Get
        Set(value As Boolean)
            INDsleAffectBudget.EditValue = value
        End Set
    End Property

    Public Property BudgetaryEntityId As Integer? Implements IEquipmentCatalog.BudgetaryEntityId
        Get
            Return INDsleBudgetaryEntityId.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetaryEntityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryEntityXpo As XPInstantFeedbackSource Implements IEquipmentCatalog.BudgetaryEntityXpo
        Get
            Return INDsleBudgetaryEntityId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudgetaryEntityId.Properties.DataSource = value
        End Set
    End Property

    Public Property BudgetaryValidityId As Integer? Implements IEquipmentCatalog.BudgetaryValidityId
        Get
            Return INDsleBudgetaryValidityId.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetaryValidityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryValidityXpo As XPInstantFeedbackSource Implements IEquipmentCatalog.BudgetaryValidityXpo
        Get
            Return INDsleBudgetaryValidityId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudgetaryValidityId.Properties.DataSource = value
        End Set
    End Property

    Public Property BudgetId As Integer? Implements IEquipmentCatalog.BudgetId
        Get
            Return INDsleBudget.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudget.EditValue = value
        End Set
    End Property

    Public Property BudgetXpo As XPInstantFeedbackSource Implements IEquipmentCatalog.BudgetXpo
        Get
            Return INDsleBudget.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudget.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de adquisición
    ''' </summary>
    ''' <returns></returns>
    Public Property AdquisitionType As Integer Implements IEquipmentCatalog.AdquisitionType
        Get
            Return INDsleAdquisitionType.EditValue
        End Get
        Set(value As Integer)
            INDsleAdquisitionType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del libro
    ''' </summary>
    ''' <returns></returns>
    Public Property LegalBookId As Integer Implements IEquipmentCatalog.LegalBookId
        Get
            Return INDsleLegalBook.EditValue
        End Get
        Set(value As Integer)
            INDsleLegalBook.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del libro
    ''' </summary>
    ''' <returns></returns>
    Public Property LegalBookXpo As XPInstantFeedbackSource Implements IEquipmentCatalog.LegalBookXpo
        Get
            Return INDsleLegalBook.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleLegalBook.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <returns></returns>
    Public Property MainAccountId As Integer Implements IEquipmentCatalog.MainAccountId
        Get
            Return INDsleMainAccount.EditValue
        End Get
        Set(value As Integer)
            INDsleMainAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de la cuenta contable
    ''' </summary>
    ''' <returns></returns>
    Public Property MainAccountXpo As XPInstantFeedbackSource Implements IEquipmentCatalog.MainAccountXpo
        Get
            Return INDsleMainAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleMainAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVAAccountId As Integer? Implements IEquipmentCatalog.IVAAccountId
        Get
            Return INDPucIVAAccount.EditValue
        End Get
        Set(value As Integer?)
            INDPucIVAAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del cuentas para iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVAAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IEquipmentCatalog.IVAAccountXpo
        Get
            Return INDPucIVAAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDPucIVAAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingTaxAccountId As Integer? Implements IEquipmentCatalog.WithholdingTaxAccountId
        Get
            Return INDPucWithholdingTaxAccount.EditValue
        End Get
        Set(value As Integer?)
            INDPucWithholdingTaxAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del cuentas para iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingTaxAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IEquipmentCatalog.WithholdingTaxAccountXpo
        Get
            Return INDPucWithholdingTaxAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDPucWithholdingTaxAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingICAAccountId As Integer? Implements IEquipmentCatalog.WithholdingICAAccountId
        Get
            Return INDPucWithholdingICAAccount.EditValue
        End Get
        Set(value As Integer?)
            INDPucWithholdingICAAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del cuentas para iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingICAAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IEquipmentCatalog.WithholdingICAAccountXpo
        Get
            Return INDPucWithholdingICAAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDPucWithholdingICAAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de retencion de la fuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingTaxConceptId As Integer? Implements IEquipmentCatalog.WithholdingTaxConceptId
        Get
            Return INDSleWithholdingTaxConcept.EditValue
        End Get
        Set(value As Integer?)
            INDSleWithholdingTaxConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los conceptos de retencion de la fuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingTaxConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IEquipmentCatalog.WithholdingTaxConceptXpo
        Get
            Return INDSleWithholdingTaxConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleWithholdingTaxConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de retencion de ica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingICAConceptId As Integer? Implements IEquipmentCatalog.WithholdingICAConceptId
        Get
            Return INDSleWithholdingICAConcept.EditValue
        End Get
        Set(value As Integer?)
            INDSleWithholdingICAConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los conceptos de retencion para ica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingICAConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IEquipmentCatalog.WithholdingICAConceptXpo
        Get
            Return INDSleWithholdingICAConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleWithholdingICAConcept.Properties.DataSource = value
        End Set
    End Property

    Public Property AccumulatedDeteriorationAccountId As Integer? Implements IEquipmentCatalog.AccumulatedDeteriorationAccountId
        Get
            Return INDsleAccumulatedDeteriorationAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccumulatedDeteriorationAccount.EditValue = value
        End Set
    End Property

    Public Property LoanLeasingAccountId As Integer? Implements IEquipmentCatalog.LoanLeasingAccountId
        Get
            Return INDsleLoanLeasingAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleLoanLeasingAccount.EditValue = value
        End Set
    End Property

    Public Property FinancialRentingAccountId As Integer? Implements IEquipmentCatalog.FinancialRentingAccountId
        Get
            Return INDsleFinancialRenting.EditValue
        End Get
        Set(value As Integer?)
            INDsleFinancialRenting.EditValue = value
        End Set
    End Property

    Public Property DepreciationLeasingAccountId As Integer? Implements IEquipmentCatalog.DepreciationLeasingAccountId
        Get
            Return INDsleDepreciationLeasingAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleDepreciationLeasingAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Estado del registro
    ''' </summary>
    ''' <returns></returns>
    Public Property Status As Boolean Implements IEquipmentCatalog.Status
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
    ''' Permite saber si maneja IVA deducible para bienes de capital
    ''' </summary>
    ''' <returns></returns>
    Public Property IVADeductible As Boolean Implements IEquipmentCatalog.IVADeductible
        Get
            Return INDsleIVADeductible.EditValue
        End Get
        Set(value As Boolean)
            INDsleIVADeductible.EditValue = value
        End Set
    End Property

    Public Property MaintenanceAssetsMainAccountId As Integer? Implements IEquipmentCatalog.MaintenanceAssetsMainAccountId
        Get
            Return INDsleMaintenanceAssetsMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleMaintenanceAssetsMainAccount.EditValue = value
        End Set
    End Property

    Public Property WarehouseAssetsMainAccountId As Integer? Implements IEquipmentCatalog.WarehouseAssetsMainAccountId
        Get
            Return INDsleWarehouseAssetsMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleWarehouseAssetsMainAccount.EditValue = value
        End Set
    End Property

    Public Property LossMainAccountId As Integer? Implements IEquipmentCatalog.LossMainAccountId
        Get
            Return INDsleLossMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleLossMainAccount.EditValue = value
        End Set
    End Property

    Public Property NetIncomeAccountId As Integer? Implements IEquipmentCatalog.NetIncomeAccountId
        Get
            Return INDsleNetIncomeAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleNetIncomeAccount.EditValue = value
        End Set
    End Property

    Public Property ReplacementCreditMainAccountId As Integer? Implements IEquipmentCatalog.ReplacementCreditMainAccountId
        Get
            Return INDsleReplacementCreditMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleReplacementCreditMainAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que indica si maneja depreciación por distribución
    ''' </summary>
    ''' <returns></returns>
    Public Property HandlesDepreciationbyDistribution As Boolean?
        Get
            Return INDccbeHandlesDepreciationbyDistribution.EditValue
        End Get
        Set(value As Boolean?)
            INDccbeHandlesDepreciationbyDistribution.EditValue = value
        End Set
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements IEquipmentCatalog.ActionsOnControls
        Set(value As Boolean)
            INDlcEquipmentCatalog.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDTxtDescription.Enabled = value
            INDsleIVADeductible.Enabled = value
            INDRgStatus.Enabled = value
            INDSlIngressLeasingAccount.Enabled = value
            INDSlLoanDebitAccount.Enabled = value
            INDSlLoanCreditAccount.Enabled = value
            INDSlDepreciationAccount.Enabled = value
            INDsleNetIncomeAccount.Enabled = value
            INDsleLossMainAccount.Enabled = value
            INDsleReplacementCreditMainAccount.Enabled = value
            INDSlDebitValorizationAccount.Enabled = value
            INDSlCreditEquipmentPlantAcumulatedAccount.Enabled = value
            INDSlDebitDevaluationAccount.Enabled = value
            INDSlCreditDevaluationAccount.Enabled = value
            INDccbeHandlesDepreciationbyDistribution.Enabled = value
            INDSlPropertyAccount.Enabled = value
            INDSlCreditValorizationAccount.Enabled = value
            INDsleWarehouseAssetsMainAccount.Enabled = value
            INDsleMaintenanceAssetsMainAccount.Enabled = value
            INDsleDepreciationLeasingAccount.Enabled = value
            INDSlNotDeclarantRetetion.Enabled = value
            INDSlDeclarantRetentionAccount.Enabled = value
            INDSLAccountPayableId.Enabled = value
            INDsleLoanLeasingAccount.Enabled = value
            INDsleFinancialRenting.Enabled = value
            INDsleAccumulatedDeteriorationAccount.Enabled = value

            INDPucIVAAccount.Enabled = value
            INDSleWithholdingTaxConcept.Enabled = value
            INDPucWithholdingTaxAccount.Enabled = value
            INDSleWithholdingICAConcept.Enabled = value
            INDPucWithholdingICAAccount.Enabled = value

            INDsleAffectBudget.Enabled = value
            INDsleBudgetaryEntityId.Enabled = value
            INDsleBudgetaryValidityId.Enabled = value
            INDsleBudget.Enabled = value
            INDGleClassification.Enabled = value

            INDBtnAddAdministrative.Enabled = value
            INDGcAccountAdministrative.Enabled = value

            INDBtnAddAdministrativeByDistribution.Enabled = value
            INDGcAccountAdministrativeByDistribution.Enabled = value
            INDBtnExportStructure.Enabled = value

            INDpceInfo.Enabled = value
            INDgcInfo.Enabled = value
            INDlcEquipmentCatalog.EndUpdate()

            If value = False Then
                INDBteCode.Focus()
            Else
                INDGleClassification.Focus()
            End If
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IEquipmentCatalog.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IEquipmentCatalog.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property Sequence As FixedAssetSequence Implements IEquipmentCatalog.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As FixedAssetSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.FixedAssetSequenceDetail In Me._sequence.FixedAssetSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public Property Code As String Implements IEquipmentCatalog.Code
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    Public Property DepreciationAccountId As Integer? Implements IEquipmentCatalog.DepreciationAccountId
        Get
            Return INDSlDepreciationAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSlDepreciationAccount.EditValue = value
        End Set
    End Property

    Public Property Description As String Implements IEquipmentCatalog.Description
        Get
            Return INDTxtDescription.Text
        End Get
        Set(value As String)
            INDTxtDescription.Text = value
        End Set
    End Property

    Public Property IngressLeasingAccountId As Integer? Implements IEquipmentCatalog.IngressLeasingAccountId
        Get
            Return INDSlIngressLeasingAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSlIngressLeasingAccount.EditValue = value
        End Set
    End Property

    Public Property LoanCreditAccountId As Integer? Implements IEquipmentCatalog.LoanCreditAccountId
        Get
            Return INDSlLoanCreditAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSlLoanCreditAccount.EditValue = value
        End Set
    End Property

    Public Property CatalogActive As Boolean Implements IEquipmentCatalog.CatalogActive
        Get
            Return INDRgStatus.EditValue
        End Get
        Set(value As Boolean)
            INDRgStatus.EditValue = value
        End Set
    End Property

    Public Property LoanDebitAccountId As Integer? Implements IEquipmentCatalog.LoanDebitAccountId
        Get
            Return INDSlLoanDebitAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSlLoanDebitAccount.EditValue = value
        End Set
    End Property

    Public Property DevaluationCreditAccountId As Integer? Implements IEquipmentCatalog.DevaluationCreditAccountId
        Get
            Return INDSlCreditDevaluationAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSlCreditDevaluationAccount.EditValue = value
        End Set
    End Property

    Public Property DevaluationDebitAccountId As Integer? Implements IEquipmentCatalog.DevaluationDebitAccountId
        Get
            Return INDSlDebitDevaluationAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSlDebitDevaluationAccount.EditValue = value
        End Set
    End Property

    Public Property ValorizationCreditAccountId As Integer? Implements IEquipmentCatalog.ValorizationCreditAccountId
        Get
            Return INDSlCreditValorizationAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSlCreditValorizationAccount.EditValue = value
        End Set
    End Property

    Public Property ValorizationDebitAccountId As Integer? Implements IEquipmentCatalog.ValorizationDebitAccountId
        Get
            Return INDSlDebitValorizationAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSlDebitValorizationAccount.EditValue = value
        End Set
    End Property

    Public Property DeclarantRetentionAccountPayableConceptId As Integer? Implements IEquipmentCatalog.DeclarantRetentionAccountPayableConceptId
        Get
            Return INDSlDeclarantRetentionAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSlDeclarantRetentionAccount.EditValue = value
        End Set
    End Property

    Public Property IncomeAccountPayableConceptId As Integer? Implements IEquipmentCatalog.IncomeAccountPayableConceptId
        Get
            Return INDSLAccountPayableId.EditValue
        End Get
        Set(value As Integer?)
            INDSLAccountPayableId.EditValue = value
        End Set
    End Property

    Public Property NotDeclarantRetentionAccountPayableConceptId As Integer? Implements IEquipmentCatalog.NotDeclarantRetentionAccountPayableConceptId
        Get
            Return INDSlNotDeclarantRetetion.EditValue
        End Get
        Set(value As Integer?)
            INDSlNotDeclarantRetetion.EditValue = value
        End Set
    End Property

    Public Property IncomeAccountId As Integer Implements IEquipmentCatalog.IncomeAccountId
        Get
            Return INDSlPropertyAccount.EditValue
        End Get
        Set(value As Integer)
            INDSlPropertyAccount.EditValue = value
        End Set
    End Property

    Public Property CreditEquipmentPlantAcumulatedAccountId As Integer? Implements IEquipmentCatalog.CreditEquipmentPlantAcumulatedAccountId
        Get
            Return INDSlCreditEquipmentPlantAcumulatedAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSlCreditEquipmentPlantAcumulatedAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de Cuentas de Ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IngressAccountXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' datasource de Cuentas de Ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IngressLeasingAccountXPO As XPInstantFeedbackSource

    Property LoanDebitAccountXPO As XPInstantFeedbackSource

    Property LoanCreditAccountXPO As XPInstantFeedbackSource

    Property LoanLeasingAccountXpo As XPInstantFeedbackSource

    Property FinancialRentingAccountXpo As XPInstantFeedbackSource

    Property AccumulatedDeteriorationAccountXpo As XPInstantFeedbackSource

    Property DepreciationAccountXPO As XPInstantFeedbackSource

    Property ValorizationDebitAccountXpo As XPInstantFeedbackSource

    Property ValorizationCreditAccountXpo As XPInstantFeedbackSource

    Property DevaluationDebitAccountXpo As XPInstantFeedbackSource

    Property DevaluationCreditAccountXpo As XPInstantFeedbackSource

    Property IncomeAccountPayableConceptXpo As XPInstantFeedbackSource

    Property NotDeclarantRetentionAccountPayableConceptXpo As XPInstantFeedbackSource

    Property DeclarantRetentionAccountPayableConceptXpo As XPInstantFeedbackSource

    Property NetIncomeAccountXpo As XPInstantFeedbackSource

    Property LossMainAccountXpo As XPInstantFeedbackSource

    Property ReplacementCreditMainAccountXpo As XPInstantFeedbackSource

    Property WarehouseAssetsMainAccountXpo As XPInstantFeedbackSource

    Property MaintenanceAssetsMainAccountXpo As XPInstantFeedbackSource

    Property DepreciationLeasingAccountXpo As XPInstantFeedbackSource

    Property PropertyAccountXpo As XPInstantFeedbackSource

    Property CreditEquipmentPlantAcumulatedAccountXpo As XPInstantFeedbackSource

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Me.FixedAssetEquipmentCatalog IsNot Nothing AndAlso Me.FixedAssetEquipmentCatalog.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MEquipmentCatalog(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteEquipmentCatalog(FixedAssetEquipmentCatalog, _idCurrentSequence, Me._sequence)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Await Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If FixedAssetEquipmentCatalog IsNot Nothing AndAlso FixedAssetEquipmentCatalog.Status < 3 Then

            Dim errors = ValidatingControls()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If

            If Classification <> 3 Then
                If HandlesDepreciationbyDistribution Then
                    If GridView110.RowCount = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No ha agregado ningún detalle a las Rejilla"
                        Exit Sub
                    End If
                Else
                    If GridView1.RowCount = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No ha agregado ningún detalle a las Rejilla"
                        Exit Sub
                    End If
                End If
            Else
                If IncomeAccountPayableConceptId Is Nothing OrElse IncomeAccountPayableConceptId = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede confirmar Ingreso de Activo ya que no existen parámetros de Concepto de Pago en el Catálogo de Artículos"
                    Exit Sub
                End If
            End If
            AssigningValues()
        End If
        Try
            Using model As New MEquipmentCatalog(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveEquipmentCatalog(FixedAssetEquipmentCatalog, _idCurrentSequence, Me._sequence)
                AsyncLoader(False)
                If result.StateResult = True Then
                    If FixedAssetEquipmentCatalog.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numérica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            If Me.DicSequense(Me._sequence.FixedAssetSequenceDetail(0).Id).Count > 0 Then
                                Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                            End If
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                    Else
                        If FixedAssetEquipmentCatalog.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Me.FixedAssetEquipmentCatalog = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    _searchMode = False
                    IsLoaded = False
                    Me.Deshacer()
                Else
                    If result.StateResult = False And result.StateResultAux = False Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If Not FixedAssetEquipmentCatalog.Id > 0 Then
                        FixedAssetEquipmentCatalog = New FixedAssetItemCatalog
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewEquipmentCatalog()
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga el parametro que me indica si se realiza la interfaz
    ''' </summary>
    ''' <returns></returns>m
    Private Async Function LoadParameters() As Task
        Try
            AsyncLoader(True)
            Using model As New Presentation.Payments.MVP.MParameters(MyTag)
                Dim budgetInterface As Boolean = False
                Dim _parameter = Await model.GetSettingPaymentsByIdOperatingUnit(_idOperativeUnit)
                If _parameter IsNot Nothing AndAlso _parameter.ObjectEmbbeded IsNot Nothing AndAlso _parameter.StateResult Then
                    budgetInterface = _parameter.ObjectEmbbeded.BudgetInterface
                End If

                INDlyItemAffectBudget.AllowHide = Not budgetInterface
                INDlyItemBudget.ShowInCustomizationForm = Not budgetInterface
                INDlygBudgetInterface.Visibility = If(budgetInterface, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "Se presentó un error al cargar los datos de la unidad operativa"
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Limpia los controles de presupuesto
    ''' </summary>
    ''' <param name="level"></param>
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
            BudgetId = Nothing
            INDsleBudget.Properties.NullText = String.Empty
            BudgetXpo = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Elimina la info del tipo de adquisición
    ''' </summary>
    Private Sub DeleteInfoAdquisitionType()
        Dim info = DirectCast(INDgvInfo.GetFocusedRow(), FixedAssetItemCatalogAdquisitionType)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If info.Id > 0 Then
                If ListDeleteFixedAssetItemCatalogAdquisitionType Is Nothing Then
                    ListDeleteFixedAssetItemCatalogAdquisitionType = New List(Of FixedAssetItemCatalogAdquisitionType)
                End If
                info.MarkAsDeleted()
                ListDeleteFixedAssetItemCatalogAdquisitionType.Add(info)
            End If

            ListFixedAssetItemCatalogAdquisitionType.Remove(info)
            INDgcInfo.DataSource = Nothing
            INDgcInfo.DataSource = ListFixedAssetItemCatalogAdquisitionType
        End If
    End Sub

    ''' <summary>
    ''' Agrega un item a la rejilla
    ''' </summary>
    Private Sub AddInfoAdquisitionType()
        'Se valida los controles del popup
        Dim errors As String = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        'Si viene nothin el listado se instancia
        If ListFixedAssetItemCatalogAdquisitionType Is Nothing Then
            ListFixedAssetItemCatalogAdquisitionType = New List(Of FixedAssetItemCatalogAdquisitionType)
        End If

        'Se valida que la info no este ya en la rejilla
        If (From x In ListFixedAssetItemCatalogAdquisitionType Where x.LegalBookId = LegalBookId Select x).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El libro seleccionado ya existe en la rejilla"
            Exit Sub
        End If

        'Se agrega la info a la rejilla
        Dim info As New FixedAssetItemCatalogAdquisitionType
        With info
            .AdquisitionType = AdquisitionType
            .LegalBookId = LegalBookId
            .LegalBookDescription = INDsleLegalBook.Text
            .MainAccountId = MainAccountId
            .MainAccountDescription = INDsleMainAccount.Text
        End With
        ListFixedAssetItemCatalogAdquisitionType.Add(info)
        INDgcInfo.DataSource = Nothing
        INDgcInfo.DataSource = ListFixedAssetItemCatalogAdquisitionType
        Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
        CleanControlsPopup()
        INDsleLegalBook.Focus()
    End Sub

    ''' <summary>
    ''' Valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder

        If AdquisitionType = Nothing Then
            errors.AppendLine("Debe seleccionar un tipo de adquisición")
        End If

        If LegalBookId = Nothing Then
            errors.AppendLine("Debe seleccionar un libro")
        End If

        If MainAccountId = Nothing Then
            errors.AppendLine("Debe seleccionar una cuenta contable")
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Limpia los controles del popup
    ''' </summary>
    Private Sub CleanControlsPopup()
        LegalBookId = Nothing
        MainAccountId = Nothing
    End Sub

    ''' <summary>
    ''' Inicializa el search de tipo de adquisición
    ''' </summary>
    Private Sub InitializeTuple()
        ListAdquisitionType = New List(Of Tuple(Of Integer, String))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(1, "Compra Directa"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(3, "Comodato"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(8, "Comodato Tercerizado"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(4, "Donación"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(5, "Traspaso de Bienes"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(6, "Otro Concepto"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(7, "Leasing Financiero"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(9, "Renting Financiero"))
        ListAdquisitionType.Add(New Tuple(Of Integer, String)(10, "Renting Operativo"))
        INDsleAdquisitionType.Properties.DataSource = ListAdquisitionType.ToList

        AdquisitionType = 9

        ListYesNot = New List(Of Tuple(Of Boolean, String))
        ListYesNot.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListYesNot.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDsleAffectBudget.Properties.DataSource = ListYesNot

        INDGleClassification.Properties.DataSource = {
            New Tuple(Of Byte, String)(1, "Activo fijo"),
            New Tuple(Of Byte, String)(2, "Intangibles"),
            New Tuple(Of Byte, String)(3, "Bienes Controlables")
        }.ToList()
    End Sub

    ''' <summary>
    ''' valida los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidatingControls() As String
        Dim errors As New StringBuilder

        If Classification <> 3 Then
            If INDSlIngressLeasingAccount.EditValue = 0 Then
                errors.AppendLine(INDLciIngressLeasingAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDSlLoanDebitAccount.EditValue = 0 Then
                errors.AppendLine(INDLciLoanDebitAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDSlLoanCreditAccount.EditValue = 0 Then
                errors.AppendLine(INDLciLoanCreditAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDSlDepreciationAccount.EditValue = 0 Then
                errors.AppendLine(INDLciDepreciationAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDsleNetIncomeAccount.EditValue = 0 Then
                errors.AppendLine(INDLciNetIncomeAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDsleLossMainAccount.EditValue = 0 Then
                errors.AppendLine(INDLciLossMainAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDsleReplacementCreditMainAccount.EditValue = 0 Then
                errors.AppendLine(INDLciReplacementCreditMainAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDSlDebitValorizationAccount.EditValue = 0 Then
                errors.AppendLine(INDLciDebitValorizationAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDSlCreditValorizationAccount.EditValue = 0 Then
                errors.AppendLine(INDLciCreditValorizationAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDSlDebitDevaluationAccount.EditValue = 0 Then
                errors.AppendLine(INDLciDebitDevaluationAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDSlCreditDevaluationAccount.EditValue = 0 Then
                errors.AppendLine(INDLciCreditDevaluationAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDsleWarehouseAssetsMainAccount.EditValue = 0 Then
                errors.AppendLine(INDLciWarehouseAssetsMainAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDsleMaintenanceAssetsMainAccount.EditValue = 0 Then
                errors.AppendLine(INDLciMaintenanceAssetsMainAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDsleDepreciationLeasingAccount.EditValue = 0 Then
                errors.AppendLine(INDLciDepreciationLeasingAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDSLAccountPayableId.EditValue = 0 Then
                errors.AppendLine(INDLciAccountPayableId.Text + ResourceManager.GetString("Empty"))
            End If

            If INDSlDeclarantRetentionAccount.EditValue = 0 Then
                errors.AppendLine(INDLciDeclarantRetentionAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDSlNotDeclarantRetetion.EditValue = 0 Then
                errors.AppendLine(INDLciNotDeclarantRetetion.Text + ResourceManager.GetString("Empty"))
            End If

            If INDSlCreditEquipmentPlantAcumulatedAccount.EditValue = 0 Then
                errors.AppendLine(INDLciEquipmentPlantDeterioraionCreditAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDPucIVAAccount.EditValue = 0 Then
                errors.AppendLine(INDLciIVAAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDSleWithholdingTaxConcept.EditValue = 0 Then
                errors.AppendLine(INDLciWithholdingTaxConcept.Text + ResourceManager.GetString("Empty"))
            End If

            If INDPucWithholdingTaxAccount.EditValue = 0 Then
                errors.AppendLine(INDLciWithholdingTaxAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If INDSleWithholdingICAConcept.EditValue = 0 Then
                errors.AppendLine(INDLciWithholdingICAConcept.Text + ResourceManager.GetString("Empty"))
            End If

            If INDPucWithholdingICAAccount.EditValue = 0 Then
                errors.AppendLine(INDLciWithholdingICAAccount.Text + ResourceManager.GetString("Empty"))
            End If

            If HandlesDepreciationbyDistribution Is Nothing Then
                errors.AppendLine(INDlyHandlesDepreciationByDistribution.Text + ResourceManager.GetString("Empty"))
            End If

            If HandlesDepreciationbyDistribution Then
                If listFixedAssetEquipmentCatalogDetail.Sum(Function(detail) If(detail.DistributionPercentage.HasValue, detail.DistributionPercentage.Value, 0)) <> 100 Then
                    errors.AppendLine("La suma del porcentaje de distribución debe ser 100")
                End If
            End If
        End If

        If INDTxtDescription.Text = String.Empty Then
            errors.AppendLine(INDLciDescripcion.Text + ResourceManager.GetString("Empty"))
        End If

        If INDSlPropertyAccount.EditValue = 0 Then
            errors.AppendLine(INDLciPropertyAccount.Text + ResourceManager.GetString("Empty"))
        End If

        Return errors.ToString()
    End Function

    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch

        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Description"}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentCatalog
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Método para obtener el valor del formulario de búsqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Método para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumnsAdministrative()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(GridView1, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In GridView1.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        Dim ListActions2 As New List(Of eAcciones)
        ListActions2.Add(eAcciones.Remove)
        IndigoGridView2.SetListAcction(INDgvInfo, ListActions2)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvInfo.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        Dim ListActions3 As New List(Of eAcciones)
        ListActions3.Add(eAcciones.Remove)
        ListActions3.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(GridView110, ListActions3)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In GridView110.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Método que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequenceFixedAsset(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Método para mostrar los formulario en el evento buttonclick
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog(Me)
    End Sub

    ''' <summary>
    ''' Método para generar secuencia numérica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function NewEquipmentCatalog() As Task
        FixedAssetEquipmentCatalog = New FixedAssetItemCatalog() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
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
    ''' Método para obtener lo que se retorna del formulario modal de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddRemissionEntranceDetail(sender As Object, e As AddEquipmentCatalogEventArgs)
        If listFixedAssetEquipmentCatalogDetail Is Nothing Then
            listFixedAssetEquipmentCatalogDetail = New List(Of FixedAssetItemCatalogDetail)
        End If
        If e.EditMode = True Then
            listFixedAssetEquipmentCatalogDetail.Remove(FixedAssetEquipmentCatalogDetail)
            listFixedAssetEquipmentCatalogDetail.Insert(indexEditRecord, e.FixedAssetItemCatalogDetail)
        ElseIf e.ImportDataMode = True Then
            listFixedAssetEquipmentCatalogDetail.AddRange(e.FixedAssetItemCatalogDetail)
        Else
            listFixedAssetEquipmentCatalogDetail.Add(e.FixedAssetItemCatalogDetail)
        End If
        If HandlesDepreciationbyDistribution Then
            INDGcAccountAdministrativeByDistribution.DataSource = Nothing
            INDGcAccountAdministrativeByDistribution.DataSource = listFixedAssetEquipmentCatalogDetail
        Else
            INDGcAccountAdministrative.DataSource = Nothing
            INDGcAccountAdministrative.DataSource = listFixedAssetEquipmentCatalogDetail
        End If
    End Sub

    ''' <summary>
    ''' carga los controles con la informacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            _isLoading = True
            Try
                Using Model As New MEquipmentCatalog(CStr(Me.Tag))
                    AsyncLoader(True)
                    INDlcEquipmentCatalog.BeginUpdate()
                    FixedAssetEquipmentCatalog = Await Model.GetEquipmentCatalogAsync(Code)
                    If FixedAssetEquipmentCatalog IsNot Nothing AndAlso FixedAssetEquipmentCatalog.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        listFixedAssetEquipmentCatalogDetail = FixedAssetEquipmentCatalog.FixedAssetItemCatalogDetail.ToList()
                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(FixedAssetEquipmentCatalog.Id))
                            IsLoaded = False
                            With FixedAssetEquipmentCatalog
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                Code = .Code
                                Classification = .Classification
                                Description = .Description
                                IVADeductible = .IVADeductible
                                CatalogActive = .Active


                                Status = .Status

                                IncomeAccountId = .IncomeAccountId
                                INDSlPropertyAccount.Properties.NullText = .NumberNameIngressAccountingAccount

                                INDSlIngressLeasingAccount.Properties.NullText = .NumberNameIngressLeasingAccountingAccount
                                INDSlIngressLeasingAccount.EditValue = .IncomeLeasingAccountId

                                INDSlLoanDebitAccount.Properties.NullText = .NumberNameDebitLoanAccountingAccount
                                INDSlLoanDebitAccount.EditValue = .DebitLoanAccountId

                                INDSlLoanCreditAccount.Properties.NullText = .NumberNameCreaditLoanAccountingAccount
                                INDSlLoanCreditAccount.EditValue = .CreditLoanAccountId

                                INDSlDepreciationAccount.Properties.NullText = .NumberNameDepreciationAccountingAccount
                                INDSlDepreciationAccount.EditValue = .DepreciationAccountId

                                INDsleNetIncomeAccount.EditValue = .NetIncomeAccountId
                                INDsleNetIncomeAccount.Properties.NullText = .NetIncomeAccountNumberName

                                INDsleLossMainAccount.EditValue = .LossMainAccountId
                                INDsleLossMainAccount.Properties.NullText = .LossMainAccountNumberName

                                INDsleReplacementCreditMainAccount.EditValue = .ReplacementCreditMainAccountId
                                INDsleReplacementCreditMainAccount.Properties.NullText = .ReplacementCreditMainAccountNumberName

                                INDSlCreditDevaluationAccount.EditValue = .CreditDevaluationAccountId
                                INDSlCreditDevaluationAccount.Properties.NullText = .NumberNameCreditDevaluationAccountingAccount

                                HandlesDepreciationbyDistribution = .HandlesDepreciationbyDistribution

                                INDSlCreditValorizationAccount.EditValue = .CreditValorizationAccountId
                                INDSlCreditValorizationAccount.Properties.NullText = .NumberNameCreditValorizationAccountingAccount

                                INDSlDebitDevaluationAccount.EditValue = .DebitDevaluationAccountId
                                INDSlDebitDevaluationAccount.Properties.NullText = .NumberNameDebitDevaluationAccountingAccount

                                INDSlDebitValorizationAccount.EditValue = .DebitValorizationAccountId
                                INDSlDebitValorizationAccount.Properties.NullText = .NumberNameDebitValorizationAccountingAccount

                                INDsleWarehouseAssetsMainAccount.EditValue = .WarehouseAssetsMainAccountId
                                INDsleWarehouseAssetsMainAccount.Properties.NullText = .WarehouseAssetsMainAccountNumberName

                                INDsleMaintenanceAssetsMainAccount.EditValue = .MaintenanceAssetsMainAccountId
                                INDsleMaintenanceAssetsMainAccount.Properties.NullText = .MaintenanceAssetsMainAccountNumberName

                                DepreciationLeasingAccountId = .DepreciationLeasingAccountId
                                INDsleDepreciationLeasingAccount.Properties.NullText = .DepreciationLeasingAccountNumberName

                                IncomeAccountPayableConceptId = .IncomeAccountPayableConceptId
                                INDSLAccountPayableId.Properties.NullText = .CodeNameIncomeAccountPayableConcept

                                LoanLeasingAccountId = .LoanLeasingAccountId
                                INDsleLoanLeasingAccount.Properties.NullText = .LoanLeasingAccountDescription

                                FinancialRentingAccountId = .FinancialRentingAccountId
                                INDsleFinancialRenting.Properties.NullText = .FinancialRentingAccountDescription

                                INDSlDeclarantRetentionAccount.EditValue = .DeclarantRetentionAccountPayableConceptId
                                INDSlDeclarantRetentionAccount.Properties.NullText = .CodeNameDeclarantRetentionAccountPayableConcept

                                INDSlNotDeclarantRetetion.EditValue = .NotDeclarantRetentionAccountPayableConceptId
                                INDSlNotDeclarantRetetion.Properties.NullText = .CodeNameNotDeclarantRetentionAccountPayableConcept

                                INDSlCreditEquipmentPlantAcumulatedAccount.EditValue = .CreditEquipmentPlantAcumulatedAccountId
                                INDSlCreditEquipmentPlantAcumulatedAccount.Properties.NullText = .NumberNameCreditEquipmentPlantAcumulatedAccountingAccount

                                AccumulatedDeteriorationAccountId = .AccumulatedDeteriorationAccountId
                                INDsleAccumulatedDeteriorationAccount.Properties.NullText = .AccumulatedDeteriorationAccountDescription

                                IVAAccountId = .IVAAccountId
                                INDPucIVAAccount.Properties.NullText = .CodeNameIVAAccount

                                WithholdingTaxAccountId = .WithholdingTaxAccountId
                                INDPucWithholdingTaxAccount.Properties.NullText = .CodeNameWithholdingTaxAccount

                                WithholdingICAAccountId = .WithholdingICAAccountId
                                INDPucWithholdingICAAccount.Properties.NullText = .CodeNameWithholdingICAAccount

                                WithholdingTaxConceptId = .WithholdingTaxConceptId

                                INDSleWithholdingTaxConcept.Properties.NullText = .CodeNameWithholdingTaxConcept
                                WithholdingICAConceptId = .WithholdingICAConceptId
                                INDSleWithholdingICAConcept.Properties.NullText = .CodeNameWithholdingICAConcept




                                AffectBudget = .AffectBudget
                                If AffectBudget Then
                                    BudgetaryEntityId = .BudgetaryEntityId
                                    INDsleBudgetaryEntityId.Properties.NullText = .BudgetaryEntityDescription
                                    BudgetaryValidityId = .BudgetaryValidityId
                                    INDsleBudgetaryValidityId.Properties.NullText = .BudgetaryValidityDescription
                                    BudgetId = .BudgetId
                                    INDsleBudget.Properties.NullText = .BudgetDescription
                                End If

                                ListFixedAssetItemCatalogAdquisitionType = .FixedAssetItemCatalogAdquisitionType.ToList()
                                INDgcInfo.DataSource = Nothing
                                INDgcInfo.DataSource = ListFixedAssetItemCatalogAdquisitionType

                            End With

                            If FixedAssetEquipmentCatalog.HandlesDepreciationbyDistribution Then
                                INDGcAccountAdministrativeByDistribution.DataSource = Nothing
                                INDGcAccountAdministrativeByDistribution.DataSource = FixedAssetEquipmentCatalog.FixedAssetItemCatalogDetail
                                INDLcgAdministrativeAccountingDetailInformationByDistribution.HideControl(False)
                                INDLcgAdministrativeAccountingDetailInformation.HideControl(True)
                            Else
                                INDGcAccountAdministrative.DataSource = Nothing
                                INDGcAccountAdministrative.DataSource = FixedAssetEquipmentCatalog.FixedAssetItemCatalogDetail
                                INDLcgAdministrativeAccountingDetailInformationByDistribution.HideControl(True)
                                INDLcgAdministrativeAccountingDetailInformation.HideControl(Classification = 3)
                            End If

                            If FixedAssetEquipmentCatalog.Status <> 1 Then
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
                                INDBtnAddAdministrative.Enabled = False
                            Else
                                INDBtnAddAdministrative.Enabled = True
                            End If
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.FixedAssetEquipmentCatalog.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = FixedAssetEquipmentCatalog.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(FixedAssetEquipmentCatalog.Id, Me.Tag.ToString(), Nothing, GetType(FixedAssetItemCatalog).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                        IsLoaded = True
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewEquipmentCatalog()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDlcEquipmentCatalog.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
        _isLoading = False
    End Function
    ''' <summary>
    ''' limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlcEquipmentCatalog.BeginUpdate()
        ReadOnlyControls(False)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True

        'Limpiar controles
        INDTxtDescription.EditValue = String.Empty
        IVADeductible = False
        INDRgStatus.EditValue = -1

        INDSlIngressLeasingAccount.EditValue = 0
        INDSlLoanDebitAccount.EditValue = 0
        INDSlLoanCreditAccount.EditValue = 0
        INDSlDepreciationAccount.EditValue = 0
        INDsleNetIncomeAccount.EditValue = 0
        INDsleLossMainAccount.EditValue = 0
        INDsleReplacementCreditMainAccount.EditValue = 0

        INDSlCreditDevaluationAccount.EditValue = 0
        INDSlCreditValorizationAccount.EditValue = 0
        INDSlDebitDevaluationAccount.EditValue = 0
        INDSlDebitValorizationAccount.EditValue = 0
        INDSlCreditEquipmentPlantAcumulatedAccount.EditValue = 0

        INDsleWarehouseAssetsMainAccount.EditValue = 0
        INDsleMaintenanceAssetsMainAccount.EditValue = 0

        DepreciationLeasingAccountId = Nothing
        INDsleDepreciationLeasingAccount.Properties.NullText = String.Empty

        INDSLAccountPayableId.EditValue = 0
        INDSlNotDeclarantRetetion.EditValue = 0
        INDSlDeclarantRetentionAccount.EditValue = 0

        INDSlIngressLeasingAccount.Properties.NullText = String.Empty
        INDSlLoanDebitAccount.Properties.NullText = String.Empty
        INDSlLoanCreditAccount.Properties.NullText = String.Empty
        INDSlDepreciationAccount.Properties.NullText = String.Empty
        INDsleWarehouseAssetsMainAccount.Properties.NullText = String.Empty
        INDsleMaintenanceAssetsMainAccount.Properties.NullText = String.Empty
        INDsleNetIncomeAccount.Properties.NullText = String.Empty
        INDsleLossMainAccount.Properties.NullText = String.Empty
        INDsleReplacementCreditMainAccount.Properties.NullText = String.Empty

        INDSlCreditDevaluationAccount.Properties.NullText = String.Empty
        INDSlCreditValorizationAccount.Properties.NullText = String.Empty
        INDSlDebitDevaluationAccount.Properties.NullText = String.Empty
        INDSlDebitValorizationAccount.Properties.NullText = String.Empty
        INDSlCreditEquipmentPlantAcumulatedAccount.Properties.NullText = String.Empty

        INDSLAccountPayableId.Properties.NullText = String.Empty
        INDSlDeclarantRetentionAccount.Properties.NullText = String.Empty
        INDSlNotDeclarantRetetion.Properties.NullText = String.Empty

        INDSlPropertyAccount.Properties.NullText = String.Empty
        INDSlPropertyAccount.EditValue = 0

        LoanLeasingAccountId = 0
        INDsleLoanLeasingAccount.Properties.NullText = String.Empty

        FinancialRentingAccountId = 0
        INDsleFinancialRenting.Properties.NullText = String.Empty

        AccumulatedDeteriorationAccountId = 0
        INDsleAccumulatedDeteriorationAccount.Properties.NullText = String.Empty
        Classification = CByte(1)
        IVAAccountId = Nothing
        INDPucIVAAccount.Properties.NullText = String.Empty

        WithholdingTaxAccountId = Nothing
        INDPucWithholdingTaxAccount.Properties.NullText = String.Empty

        WithholdingICAAccountId = Nothing
        INDPucWithholdingICAAccount.Properties.NullText = String.Empty

        WithholdingTaxConceptId = Nothing
        INDSleWithholdingTaxConcept.Properties.NullText = String.Empty

        WithholdingICAConceptId = Nothing
        INDSleWithholdingICAConcept.Properties.NullText = String.Empty

        INDGcAccountAdministrative.DataSource = Nothing
        INDGcAccountAdministrativeByDistribution.DataSource = Nothing
        Code = String.Empty
        Description = String.Empty
        CatalogActive = False
        IdIngressAccount = 0
        IngressLeasingAccountId = 0
        LoanDebitAccountId = 0
        LoanCreditAccountId = 0
        DepreciationAccountId = 0

        FixedAssetEquipmentCatalog = Nothing
        listFixedAssetEquipmentCatalogDetail = Nothing
        listFixedAssetEquipmentCatalogDetailDelete = Nothing
        FixedAssetEquipmentCatalogDetail = Nothing
        HandlesDepreciationbyDistribution = Nothing

        AffectBudget = Nothing
        BudgetaryEntityId = Nothing
        INDsleBudgetaryEntityId.Properties.NullText = String.Empty
        BudgetaryValidityId = Nothing
        INDsleBudgetaryValidityId.Properties.NullText = String.Empty
        BudgetId = Nothing
        INDsleBudget.Properties.NullText = String.Empty
        Classification = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        CleanControlsPopup()
        INDgcInfo.DataSource = Nothing
        ListFixedAssetItemCatalogAdquisitionType = Nothing
        ListDeleteFixedAssetItemCatalogAdquisitionType = Nothing

        INDLcgAdministrativeAccountingDetailInformationByDistribution.HideControl(True)
        INDLcgAdministrativeAccountingDetailInformation.HideControl(True)

        INDlcEquipmentCatalog.EndUpdate()
        IsLoaded = True
        DeleteBlockedRecord()

    End Sub

    ''' <summary>
    ''' asigna los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With FixedAssetEquipmentCatalog
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Description = Description
            .IVADeductible = IVADeductible
            .Active = Status
            .IncomeAccountId = IncomeAccountId
            'Si se va a guardar Bienes Controlables limpiamos Ids que no se necesitan
            If Classification = 3 Then
                SetControllableAssetValues()
            End If
            .IncomeLeasingAccountId = IngressLeasingAccountId
            .DebitLoanAccountId = LoanDebitAccountId
            .CreditLoanAccountId = LoanCreditAccountId
            .DepreciationAccountId = DepreciationAccountId
            .NetIncomeAccountId = NetIncomeAccountId
            .LossMainAccountId = LossMainAccountId
            .ReplacementCreditMainAccountId = ReplacementCreditMainAccountId
            .CreditDevaluationAccountId = DevaluationCreditAccountId
            .CreditValorizationAccountId = ValorizationCreditAccountId
            .DebitDevaluationAccountId = DevaluationDebitAccountId
            .DebitValorizationAccountId = ValorizationDebitAccountId
            .WarehouseAssetsMainAccountId = WarehouseAssetsMainAccountId
            .MaintenanceAssetsMainAccountId = MaintenanceAssetsMainAccountId
            .DepreciationLeasingAccountId = DepreciationLeasingAccountId
            .IncomeAccountPayableConceptId = IncomeAccountPayableConceptId
            .DeclarantRetentionAccountPayableConceptId = DeclarantRetentionAccountPayableConceptId
            .NotDeclarantRetentionAccountPayableConceptId = NotDeclarantRetentionAccountPayableConceptId
            .CreditEquipmentPlantAcumulatedAccountId = CreditEquipmentPlantAcumulatedAccountId
            .LoanLeasingAccountId = LoanLeasingAccountId
            .FinancialRentingAccountId = FinancialRentingAccountId
            .AccumulatedDeteriorationAccountId = AccumulatedDeteriorationAccountId
            .HandlesDepreciationbyDistribution = HandlesDepreciationbyDistribution

            .IVAAccountId = IVAAccountId
            .WithholdingTaxAccountId = WithholdingTaxAccountId
            .WithholdingICAAccountId = WithholdingICAAccountId
            .WithholdingTaxConceptId = WithholdingTaxConceptId
            .WithholdingICAConceptId = WithholdingICAConceptId

            .AffectBudget = AffectBudget
            .BudgetId = BudgetId
            .Classification = Classification

            If listFixedAssetEquipmentCatalogDetail IsNot Nothing Then
                For Each item In listFixedAssetEquipmentCatalogDetail
                    .FixedAssetItemCatalogDetail.Add(item)
                Next
                If listFixedAssetEquipmentCatalogDetailDelete IsNot Nothing Then
                    For Each item In listFixedAssetEquipmentCatalogDetail
                        .FixedAssetItemCatalogDetail.Add(item)
                    Next
                End If
            End If

            If ListFixedAssetItemCatalogAdquisitionType IsNot Nothing AndAlso ListFixedAssetItemCatalogAdquisitionType.Count > 0 Then
                ListFixedAssetItemCatalogAdquisitionType.ForEach(Sub(x) .FixedAssetItemCatalogAdquisitionType.Add(x))
            End If

            If ListDeleteFixedAssetItemCatalogAdquisitionType IsNot Nothing AndAlso ListDeleteFixedAssetItemCatalogAdquisitionType.Count > 0 Then
                ListDeleteFixedAssetItemCatalogAdquisitionType.ForEach(Sub(x) .FixedAssetItemCatalogAdquisitionType.Add(x))
            End If

        End With
    End Sub

    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetailAdministrative()
        If HandlesDepreciationbyDistribution Then
            FixedAssetEquipmentCatalogDetail = DirectCast(GridView110.GetFocusedRow(), FixedAssetItemCatalogDetail)
        Else
            FixedAssetEquipmentCatalogDetail = DirectCast(GridView1.GetFocusedRow(), FixedAssetItemCatalogDetail)
        End If
        indexEditRecord = listFixedAssetEquipmentCatalogDetail.IndexOf(FixedAssetEquipmentCatalogDetail)
        Using formulario As New FrmPopUpAddAccountingDepreciation(HandlesDepreciationbyDistribution, Classification)
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddEquipmentCatalogEventArgs, AddressOf ReturnAddRemissionEntranceDetail
            formulario.Size = New Drawing.Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.FixedAssetEquipmentCatalogDetailInformationEdit = FixedAssetEquipmentCatalogDetail
            formulario.EditMode = True
            formulario.FlagTypeCatalog = "1"
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub



    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetailAdministrative()
        If HandlesDepreciationbyDistribution Then
            FixedAssetEquipmentCatalogDetail = DirectCast(GridView110.GetFocusedRow(), FixedAssetItemCatalogDetail)
        Else
            FixedAssetEquipmentCatalogDetail = DirectCast(GridView1.GetFocusedRow(), FixedAssetItemCatalogDetail)
        End If
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If FixedAssetEquipmentCatalogDetail.Id > 0 Then
                If listFixedAssetEquipmentCatalogDetailDelete Is Nothing Then
                    listFixedAssetEquipmentCatalogDetailDelete = New List(Of FixedAssetItemCatalogDetail)
                End If
                FixedAssetEquipmentCatalogDetail.MarkAsDeleted()
                listFixedAssetEquipmentCatalogDetailDelete.Add(FixedAssetEquipmentCatalogDetail)
            End If
            listFixedAssetEquipmentCatalogDetail.Remove(FixedAssetEquipmentCatalogDetail)
            If HandlesDepreciationbyDistribution Then
                INDGcAccountAdministrativeByDistribution.DataSource = Nothing
                INDGcAccountAdministrativeByDistribution.DataSource = listFixedAssetEquipmentCatalogDetail
            Else
                INDGcAccountAdministrative.DataSource = Nothing
                INDGcAccountAdministrative.DataSource = listFixedAssetEquipmentCatalogDetail
            End If
        End If
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me.FixedAssetEquipmentCatalog.Code) Then
            Try
                Using model As New MEquipmentCatalog(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not FixedAssetEquipmentCatalog.Status
                    Dim result As ActionResult(Of FixedAssetItemCatalog) = Await model.ChangeState(Me.FixedAssetEquipmentCatalog.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.FixedAssetEquipmentCatalog = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDBteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.FixedAssetEquipmentCatalog IsNot Nothing AndAlso Me.FixedAssetEquipmentCatalog.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        indexEditRecord = Nothing
        presenter = Nothing
        _sequence = Nothing
        _idOperativeUnit = Nothing
        _searchMode = Nothing
        _idCurrentSequence = Nothing
        record = Nothing
        FixedAssetEquipmentCatalog = Nothing
        listFixedAssetEquipmentCatalogDetail = Nothing
        listFixedAssetEquipmentCatalogDetailDelete = Nothing
        FixedAssetEquipmentCatalogDetail = Nothing
        FlagType = Nothing
        IdIngressAccount = Nothing
    End Sub

    Private Async Sub FrmEquipmentCatalog_LoadAsync(sender As Object, e As EventArgs) Handles MyBase.Load
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        _indigoSession = SessionValues.Instance
        presenter = New PEquipmentCatalog(Me)
        presenter.GetSequense()
        Await Me.LoadParameters()
        InitializeTuple()
        IndigoGridView1.MoreInfoColunmns(GridView110)
        AddActionsColumnsAdministrative()
        LoadStatus()
        Deshacer()

        INDBtnExportStructure.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Centro de Costo", .Comment = "Digite el código del Centro de Costos", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Porcentaje de distribución", .Comment = "Digite el porcentaje de distribución", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Cuenta gasto depreciación", .Comment = "Digite el número de la cuenta", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Cuenta gasto leasing", .Comment = "Digite el número de la cuenta", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Cuenta gasto comodato", .Comment = "Digite el número de la cuenta", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Cuenta gasto renting financiero", .Comment = "Digite el número de la cuenta", .Type = ExcelColumnType.Text}
                        }
                    })
        InitializeTuples()
        INDLcgAdministrativeAccountingDetailInformation.HideControl(True)
        INDLcgAdministrativeAccountingDetailInformationByDistribution.HideControl(True)
        IsLoaded = True
    End Sub
#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetaryEntityId_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleBudgetaryEntityId.QueryPopUp
        If BudgetaryEntityXpo Is Nothing Then
            presenter.InitializeBudgetaryEntity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetaryValidityId_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleBudgetaryValidityId.QueryPopUp
        If BudgetaryValidityXpo Is Nothing Then
            presenter.InitializeBudgetaryValidity(BudgetaryEntityId)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de rubro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudget_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleBudget.QueryPopUp
        If BudgetXpo Is Nothing Then
            presenter.InitializeBudget(BudgetaryValidityId)
        End If
    End Sub

    Private Sub INDsleFinancialRenting_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleFinancialRenting.QueryPopUp
        If FinancialRentingAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                FinancialRentingAccountXpo = model.ListAccount()
                INDsleFinancialRenting.Properties.DataSource = FinancialRentingAccountXpo
            End Using
        End If
    End Sub

    Private Sub INDsleLegalBook_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleLegalBook.QueryPopUp
        If LegalBookXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                LegalBookXpo = model.ListLegalBook()
            End Using
        End If
    End Sub

    Private Sub INDsleMainAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleMainAccount.QueryPopUp
        If MainAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                MainAccountXpo = model.ListAccount()
            End Using
        End If
    End Sub

    Private Sub INDPucIVAAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPucIVAAccount.QueryPopUp
        If IVAAccountXpo Is Nothing Then
            presenter.InitializeIVAAccount()
        End If
    End Sub

    Private Sub INDPucWithholdingTaxAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPucWithholdingTaxAccount.QueryPopUp
        If WithholdingTaxAccountXpo Is Nothing Then
            presenter.InitializeWithholdingTaxAccount()
        End If
    End Sub

    Private Sub INDPucWithholdingICAAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPucWithholdingICAAccount.QueryPopUp
        If WithholdingICAAccountXpo Is Nothing Then
            presenter.InitializeWithholdingICAAccount()
        End If
    End Sub

    Private Sub INDSleWithholdingTaxConcept_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWithholdingTaxConcept.QueryPopUp
        If WithholdingTaxConceptXpo Is Nothing Then
            presenter.InitializeWithholdingTaxConcept()
        End If
    End Sub

    Private Sub INDSleWithholdingICAConcept_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWithholdingICAConcept.QueryPopUp
        If WithholdingICAConceptXpo Is Nothing Then
            presenter.InitializeWithholdingICAConcept()
        End If
    End Sub

    Private Sub INDsleAccumulatedDeteriorationAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAccumulatedDeteriorationAccount.QueryPopUp
        If AccumulatedDeteriorationAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                AccumulatedDeteriorationAccountXpo = model.ListAccount()
                INDsleAccumulatedDeteriorationAccount.Properties.DataSource = AccumulatedDeteriorationAccountXpo
            End Using
        End If
    End Sub

    Private Sub INDsleLoanLeasingAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleLoanLeasingAccount.QueryPopUp
        If LoanLeasingAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                LoanLeasingAccountXpo = model.ListAccount()
                INDsleLoanLeasingAccount.Properties.DataSource = LoanLeasingAccountXpo
            End Using
        End If
    End Sub

    Private Sub INDsleDepreciationLeasingAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleDepreciationLeasingAccount.QueryPopUp
        If DepreciationLeasingAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                DepreciationLeasingAccountXpo = model.ListAccount()
                INDsleDepreciationLeasingAccount.Properties.DataSource = DepreciationLeasingAccountXpo
            End Using
        End If
    End Sub

    Private Sub INDsleWarehouseAssetsMainAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleWarehouseAssetsMainAccount.QueryPopUp
        If WarehouseAssetsMainAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                WarehouseAssetsMainAccountXpo = model.ListAccount()
                INDsleWarehouseAssetsMainAccount.Properties.DataSource = WarehouseAssetsMainAccountXpo
            End Using
        End If
    End Sub

    Private Sub INDsleMaintenanceAssetsMainAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleMaintenanceAssetsMainAccount.QueryPopUp
        If MaintenanceAssetsMainAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                MaintenanceAssetsMainAccountXpo = model.ListAccount()
                INDsleMaintenanceAssetsMainAccount.Properties.DataSource = MaintenanceAssetsMainAccountXpo
            End Using
        End If
    End Sub

    Private Sub INDSlIngressLeasingAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlIngressLeasingAccount.QueryPopUp
        If INDSlIngressLeasingAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If IngressLeasingAccountXPO Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                IngressLeasingAccountXPO = model.ListAccount()
            End Using
        End If
        INDSlIngressLeasingAccount.Properties.DataSource = IngressLeasingAccountXPO
    End Sub

    Private Sub INDSlLoanDebitAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlLoanDebitAccount.QueryPopUp
        If INDSlLoanDebitAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If LoanDebitAccountXPO Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                LoanDebitAccountXPO = model.ListAccount()
            End Using
        End If
        INDSlLoanDebitAccount.Properties.DataSource = LoanDebitAccountXPO
    End Sub

    Private Sub INDSlLoanCreditAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlLoanCreditAccount.QueryPopUp
        If INDSlLoanCreditAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If LoanCreditAccountXPO Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                LoanCreditAccountXPO = model.ListAccount()
            End Using
        End If
        INDSlLoanCreditAccount.Properties.DataSource = LoanCreditAccountXPO
    End Sub

    Private Sub INDSlDepreciationAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlDepreciationAccount.QueryPopUp
        If INDSlDepreciationAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If DepreciationAccountXPO Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                DepreciationAccountXPO = model.ListAccount()
            End Using
        End If
        INDSlDepreciationAccount.Properties.DataSource = DepreciationAccountXPO
    End Sub

    Private Sub INDSlDebitValorizationAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlDebitValorizationAccount.QueryPopUp
        If INDSlDebitValorizationAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If ValorizationDebitAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                ValorizationDebitAccountXpo = model.ListAccount()
            End Using
        End If
        INDSlDebitValorizationAccount.Properties.DataSource = ValorizationDebitAccountXpo
    End Sub

    Private Sub INDSlCreditValorizationAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlCreditValorizationAccount.QueryPopUp
        If INDSlCreditValorizationAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If ValorizationCreditAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                ValorizationCreditAccountXpo = model.ListAccount()
            End Using
        End If
        INDSlCreditValorizationAccount.Properties.DataSource = ValorizationCreditAccountXpo
    End Sub

    Private Sub INDSlDebitDevaluationAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlDebitDevaluationAccount.QueryPopUp
        If INDSlDebitDevaluationAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If DevaluationDebitAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                DevaluationDebitAccountXpo = model.ListAccount()
            End Using
        End If
        INDSlDebitDevaluationAccount.Properties.DataSource = DevaluationDebitAccountXpo
    End Sub

    Private Sub INDSlCreditDevaluationAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlCreditDevaluationAccount.QueryPopUp
        If INDSlCreditDevaluationAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If DevaluationCreditAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                DevaluationCreditAccountXpo = model.ListAccount()
            End Using
        End If
        INDSlCreditDevaluationAccount.Properties.DataSource = DevaluationCreditAccountXpo
    End Sub

    Private Sub INDSLAccountPayableId_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSLAccountPayableId.QueryPopUp
        If INDSLAccountPayableId.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If IncomeAccountPayableConceptXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                IncomeAccountPayableConceptXpo = model.IncomeAccountPayableConcept()
            End Using
        End If
        INDSLAccountPayableId.Properties.DataSource = IncomeAccountPayableConceptXpo
    End Sub

    Private Sub INDSlDeclarantRetentionAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlDeclarantRetentionAccount.QueryPopUp
        If INDSlDeclarantRetentionAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If DeclarantRetentionAccountPayableConceptXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                DeclarantRetentionAccountPayableConceptXpo = model.IncomeAccountPayableConceptDeclarantRetention()
            End Using
        End If
        INDSlDeclarantRetentionAccount.Properties.DataSource = DeclarantRetentionAccountPayableConceptXpo

    End Sub

    Private Sub INDSlNotDeclarantRetetion_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlNotDeclarantRetetion.QueryPopUp

        If INDSlNotDeclarantRetetion.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If NotDeclarantRetentionAccountPayableConceptXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                NotDeclarantRetentionAccountPayableConceptXpo = model.IncomeAccountPayableConceptNotDeclarantRetention()
            End Using
        End If
        INDSlNotDeclarantRetetion.Properties.DataSource = NotDeclarantRetentionAccountPayableConceptXpo

    End Sub

    Private Sub INDsleNetIncomeAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleNetIncomeAccount.QueryPopUp
        If NetIncomeAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                NetIncomeAccountXpo = model.ListAccount()
                INDsleNetIncomeAccount.Properties.DataSource = NetIncomeAccountXpo
            End Using
        End If
    End Sub

    Private Sub INDsleLossMainAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleLossMainAccount.QueryPopUp
        If LossMainAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                LossMainAccountXpo = model.ListAccount()
                INDsleLossMainAccount.Properties.DataSource = LossMainAccountXpo
            End Using
        End If
    End Sub

    Private Sub INDsleReplacementCreditMainAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleReplacementCreditMainAccount.QueryPopUp
        If ReplacementCreditMainAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                ReplacementCreditMainAccountXpo = model.ListAccount()
                INDsleReplacementCreditMainAccount.Properties.DataSource = ReplacementCreditMainAccountXpo
            End Using
        End If
    End Sub

    Private Sub INDSlPropertyAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlPropertyAccount.QueryPopUp
        If PropertyAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                PropertyAccountXpo = model.ListAccount()
                INDSlPropertyAccount.Properties.DataSource = PropertyAccountXpo
            End Using
        End If
    End Sub

    Private Sub INDSlCreditEquipmentPlantAcumulatedAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlCreditEquipmentPlantAcumulatedAccount.QueryPopUp
        If CreditEquipmentPlantAcumulatedAccountXpo Is Nothing Then
            Using model As New MEquipmentCatalog(MyTag)
                CreditEquipmentPlantAcumulatedAccountXpo = model.ListAccount()
                INDSlCreditEquipmentPlantAcumulatedAccount.Properties.DataSource = CreditEquipmentPlantAcumulatedAccountXpo
            End Using
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleFinancialRenting_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFinancialRenting.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MEquipmentCatalog(MyTag)
                FinancialRentingAccountXpo = model.ListAccount()
            End Using
            INDsleFinancialRenting.Properties.DataSource = FinancialRentingAccountXpo
        End If
    End Sub

    Private Sub INDsleLegalBook_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleLegalBook.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1682, Nothing, True)
            Using model As New MEquipmentCatalog(MyTag)
                LegalBookXpo = model.ListLegalBook()
            End Using
        End If
    End Sub

    Private Sub INDsleMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(699, Nothing, True)
            Using model As New MEquipmentCatalog(MyTag)
                MainAccountXpo = model.ListAccount()
            End Using
        End If
    End Sub

    Private Sub INDsleAccumulatedDeteriorationAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccumulatedDeteriorationAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MEquipmentCatalog(MyTag)
                AccumulatedDeteriorationAccountXpo = model.ListAccount()
            End Using
            INDsleAccumulatedDeteriorationAccount.Properties.DataSource = AccumulatedDeteriorationAccountXpo
        End If
    End Sub

    Private Sub INDsleLoanLeasingAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleLoanLeasingAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MEquipmentCatalog(MyTag)
                LoanLeasingAccountXpo = model.ListAccount()
            End Using
            INDsleLoanLeasingAccount.Properties.DataSource = LoanLeasingAccountXpo
        End If
    End Sub

    Private Sub INDsleDepreciationLeasingAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleDepreciationLeasingAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MEquipmentCatalog(MyTag)
                DepreciationLeasingAccountXpo = model.ListAccount()
            End Using
            INDsleDepreciationLeasingAccount.Properties.DataSource = DepreciationLeasingAccountXpo
        End If
    End Sub

    Private Sub INDsleWarehouseAssetsMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleWarehouseAssetsMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MEquipmentCatalog(MyTag)
                WarehouseAssetsMainAccountXpo = model.ListAccount()
            End Using
            INDsleWarehouseAssetsMainAccount.Properties.DataSource = WarehouseAssetsMainAccountXpo
        End If
    End Sub

    Private Sub INDsleMaintenanceAssetsMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMaintenanceAssetsMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MEquipmentCatalog(MyTag)
                MaintenanceAssetsMainAccountXpo = model.ListAccount()
            End Using
            INDsleMaintenanceAssetsMainAccount.Properties.DataSource = MaintenanceAssetsMainAccountXpo
        End If
    End Sub

    Private Sub INDSlIngressLeasingAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlIngressLeasingAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            If IngressLeasingAccountXPO Is Nothing Then
                Using model As New MEquipmentCatalog(MyTag)
                    IngressLeasingAccountXPO = model.ListAccount()
                End Using
            End If
            INDSlIngressLeasingAccount.Properties.DataSource = IngressLeasingAccountXPO
        End If
    End Sub

    Private Sub INDSlLoanDebitAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlLoanDebitAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            If LoanDebitAccountXPO Is Nothing Then
                Using model As New MEquipmentCatalog(MyTag)
                    LoanDebitAccountXPO = model.ListAccount()
                End Using
            End If
            INDSlLoanDebitAccount.Properties.DataSource = LoanDebitAccountXPO
        End If
    End Sub

    Private Sub INDSlLoanCreditAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlLoanCreditAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            If LoanCreditAccountXPO Is Nothing Then
                Using model As New MEquipmentCatalog(MyTag)
                    LoanCreditAccountXPO = model.ListAccount()
                End Using
            End If
            INDSlLoanCreditAccount.Properties.DataSource = LoanCreditAccountXPO
        End If
    End Sub

    Private Sub INDSlDepreciationAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlDepreciationAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            If DepreciationAccountXPO Is Nothing Then
                Using model As New MEquipmentCatalog(MyTag)
                    DepreciationAccountXPO = model.ListAccount()
                End Using
            End If
            INDSlDepreciationAccount.Properties.DataSource = DepreciationAccountXPO
        End If
    End Sub

    Private Sub INDsleNetIncomeAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleNetIncomeAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MEquipmentCatalog(MyTag)
                NetIncomeAccountXpo = model.ListAccount()
                INDsleNetIncomeAccount.Properties.DataSource = NetIncomeAccountXpo
            End Using
        End If
    End Sub

    Private Sub INDsleLossMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleLossMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MEquipmentCatalog(MyTag)
                LossMainAccountXpo = model.ListAccount()
                INDsleLossMainAccount.Properties.DataSource = LossMainAccountXpo
            End Using
        End If
    End Sub

    Private Sub INDsleReplacementCreditMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleReplacementCreditMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MEquipmentCatalog(MyTag)
                ReplacementCreditMainAccountXpo = model.ListAccount()
                INDsleReplacementCreditMainAccount.Properties.DataSource = ReplacementCreditMainAccountXpo
            End Using
        End If
    End Sub

    Private Sub INDSlDebitValorizationAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlDebitValorizationAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            If ValorizationDebitAccountXpo Is Nothing Then
                Using model As New MEquipmentCatalog(MyTag)
                    ValorizationDebitAccountXpo = model.ListAccount()
                End Using
            End If
            INDSlDebitValorizationAccount.Properties.DataSource = ValorizationDebitAccountXpo
        End If
    End Sub

    Private Sub INDSlCreditValorizationAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlCreditValorizationAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            If ValorizationCreditAccountXpo Is Nothing Then
                Using model As New MEquipmentCatalog(MyTag)
                    ValorizationCreditAccountXpo = model.ListAccount()
                End Using
            End If
            INDSlCreditValorizationAccount.Properties.DataSource = ValorizationCreditAccountXpo
        End If
    End Sub

    Private Sub INDSlDebitDevaluationAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlDebitDevaluationAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            If DevaluationDebitAccountXpo Is Nothing Then
                Using model As New MEquipmentCatalog(MyTag)
                    DevaluationDebitAccountXpo = model.ListAccount()
                End Using
            End If
            INDSlDebitDevaluationAccount.Properties.DataSource = DevaluationDebitAccountXpo
        End If
    End Sub

    Private Sub INDSlCreditDevaluationAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlCreditDevaluationAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            If DevaluationCreditAccountXpo Is Nothing Then
                Using model As New MEquipmentCatalog(MyTag)
                    DevaluationCreditAccountXpo = model.ListAccount()
                End Using
            End If
            INDSlCreditDevaluationAccount.Properties.DataSource = DevaluationCreditAccountXpo
        End If
    End Sub

    Private Sub INDSlPropertyAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlPropertyAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            If PropertyAccountXpo Is Nothing Then
                Using model As New MEquipmentCatalog(MyTag)
                    PropertyAccountXpo = model.ListAccount()
                End Using
            End If
            INDSlPropertyAccount.Properties.DataSource = PropertyAccountXpo
        End If
    End Sub

    Private Sub INDSlCreditEquipmentPlantAcumulatedAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlCreditEquipmentPlantAcumulatedAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            If CreditEquipmentPlantAcumulatedAccountXpo Is Nothing Then
                Using model As New MEquipmentCatalog(MyTag)
                    CreditEquipmentPlantAcumulatedAccountXpo = model.ListAccount()
                End Using
            End If
            INDSlCreditEquipmentPlantAcumulatedAccount.Properties.DataSource = PropertyAccountXpo
        End If
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmEquipmentCatalog_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDBteCode.Focus()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub INDpceInfo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceInfo.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpceInfo.ShowPopup()
        End If
    End Sub

    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
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
                    Await Me.NewEquipmentCatalog()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    Private Sub Frm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAddInfo_Click(sender As Object, e As EventArgs) Handles INDbtnAddInfo.Click
        AddInfoAdquisitionType()
    End Sub
    ''' <summary>
    ''' Llama al PopUp para agregar Información Contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAddAdministrative.Click
        If HandlesDepreciationbyDistribution Is Nothing Then
            Dim message As String = If(Classification = 1, "Depreciación", "Amortización")
            Mensaje(EeventViewerImages.MensajeError) = $"Por favor, complete el campo 'Maneja {message} por Distribución' antes de continuar."
            Exit Sub
        End If
        Using formulario As New FrmPopUpAddAccountingDepreciation(HandlesDepreciationbyDistribution, Classification)
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddEquipmentCatalogEventArgs, AddressOf ReturnAddRemissionEntranceDetail
            formulario.Size = New Drawing.Size(800, 750)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.ListFixedAssetEquipmentCatalogDetailImportInfo = listFixedAssetEquipmentCatalogDetail
            formulario.ListFixedAssetEquipmentCatalogDetailValidation = listFixedAssetEquipmentCatalogDetail
            formulario.MyTag = MyTag
            formulario.FlagTypeCatalog = "1"
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Llama al PopUp para agregar Información Contable 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAddAdministrativeByDistribution_Click(sender As Object, e As EventArgs) Handles INDBtnAddAdministrativeByDistribution.Click
        Using formulario As New FrmPopUpAddAccountingDepreciation(HandlesDepreciationbyDistribution, Classification)
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddEquipmentCatalogEventArgs, AddressOf ReturnAddRemissionEntranceDetail
            formulario.Size = New Drawing.Size(800, 750)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.ListFixedAssetEquipmentCatalogDetailImportInfo = listFixedAssetEquipmentCatalogDetail
            formulario.ListFixedAssetEquipmentCatalogDetailValidation = listFixedAssetEquipmentCatalogDetail
            formulario.MyTag = MyTag
            formulario.FlagTypeCatalog = "1"
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Evento que da la opción de eliminar o modificar los registros de productos de la orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case button.Tag.ToString
            Case "Edit"
                EditDetailAdministrative()
            Case "Remove"
                DeleteDetailAdministrative()
        End Select
    End Sub

    ''' <summary>
    ''' Evento para pegar archivo a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        Try
            AsyncLoader(True)
            Using Model As New MEquipmentCatalog(Me.Tag.ToString())
                Dim ObjResult = Await Model.SetFixedAssetItemCatalogDetailFromFile(Nothing, e.Rows)

                ' Filtrar Result con elementos con StatusField = '001'
                Dim resultadosFiltrados = ObjResult.Where(Function(x) x.StatusField = "001").ToArray()

                ' Verificar si hay elementos con StatusField = '001'
                If resultadosFiltrados.Any(Function(x) x.StatusField = "001") Then
                    _equipmentCatalogDetail = resultadosFiltrados
                End If

                ' Filtrar Result con elementos con StatusField = '777'
                Dim ErroresFiltrados = ObjResult.Where(Function(x) x.StatusField = "777").ToArray()
                ' Verificar si hay elementos con StatusField = '777'
                If ErroresFiltrados.Any(Function(x) x.StatusField = "777" Or x.StatusField = "333") Then
                    _equipmentCatalogDetailError = ErroresFiltrados
                End If

            End Using

            ValidateDataPaste()

        Catch ex As Exception
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Método para validar y administrar los Datos pegados en la rejilla
    ''' </summary>
    Private Sub ValidateDataPaste()
        If _equipmentCatalogDetail IsNot Nothing Then
            If listFixedAssetEquipmentCatalogDetail Is Nothing Then
                listFixedAssetEquipmentCatalogDetail = New List(Of FixedAssetItemCatalogDetail)()
            End If

            'Convertir el resultado del SP a listFixedAssetEquipmentCatalogDetail para posteriormente poder editar o eliminar información

            ' Iterar sobre cada elemento en _equipmentCatalogDetail
            For Each item As SP_SetFixedAssetItemCatalogDetailFromFile_Result In _equipmentCatalogDetail
                ' Crear un nuevo objeto de tipo FixedAssetItemCatalogDetail y asignar las propiedades coincidentes
                Dim newItem As New FixedAssetItemCatalogDetail()

                newItem.ExpenseLoanAccountDescription = item.ExpenseLoanAccountDescription
                newItem.NumberNameLoanLeasingAccountingAccount = item.NumberNameLoanLeasingAccountingAccount
                newItem.NumberNameLoanSpendAccountingAccount = item.NumberNameLoanSpendAccountingAccount
                newItem.CodeNameAccountingStructure = item.CodeNameAccountingStructure
                newItem.LoanFinancialRentingAccountDescription = item.LoanFinancialRentingAccountDescription
                newItem.DistributionPercentage = item.DistributionPercentage
                newItem.LoanFinancialRentingAccountId = item.LoanFinancialRentingAccountId
                newItem.ExpenseLoanAccountId = item.ExpenseLoanAccountId
                newItem.LoanLeasingSpendAccountId = item.LoanLeasingSpendAccountId
                newItem.LoanSpendAccountId = item.LoanSpendAccountId
                newItem.AccountingStructureId = item.AccountingStructureId
                newItem.CostCenterId = item.CostCenterId
                newItem.NumberNameCostCenter = item.NumberNameCostCenter

                ' Agregar el objeto a la lista
                listFixedAssetEquipmentCatalogDetail.Add(newItem)
            Next

        End If

        If _equipmentCatalogDetail IsNot Nothing And _equipmentCatalogDetailError Is Nothing Then
            Mensaje(EeventViewerImages.Informacion) = _equipmentCatalogDetail.FirstOrDefault.MessageField

            ' Cargar el DataSource con los resultados filtrados y asignar InitialDate
            INDGcAccountAdministrativeByDistribution.DataSource = Nothing
            INDGcAccountAdministrativeByDistribution.DataSource = listFixedAssetEquipmentCatalogDetail

            ' Valida que hayan registros correctos y errores
        ElseIf _equipmentCatalogDetail IsNot Nothing And _equipmentCatalogDetailError IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = _equipmentCatalogDetailError.FirstOrDefault.MessageField

            ' Cargar el DataSource con los resultados filtrados y asignar InitialDate
            INDGcAccountAdministrativeByDistribution.DataSource = Nothing
            INDGcAccountAdministrativeByDistribution.DataSource = listFixedAssetEquipmentCatalogDetail

            ' Valida que solo existan errores
        ElseIf _equipmentCatalogDetailError IsNot Nothing And _equipmentCatalogDetail Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = _equipmentCatalogDetailError.FirstOrDefault.MessageField
            INDGcAccountAdministrativeByDistribution.DataSource = Nothing
        End If

        ' Lista los errores
        If _equipmentCatalogDetailError IsNot Nothing AndAlso _equipmentCatalogDetailError.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = $"El archivo presentó error en {_equipmentCatalogDetailError.Length} registros"

            ' Crear una lista para almacenar los errores como cadenas
            Dim ListError As New List(Of String)

            If _equipmentCatalogDetailError IsNot Nothing AndAlso _equipmentCatalogDetailError.Length > 0 Then
                ' Iterar sobre cada elemento en _equipmentCatalogDetailError y agregar información relevante a ListError
                For Each errorItem As SP_SetFixedAssetItemCatalogDetailFromFile_Result In _equipmentCatalogDetailError
                    ' Construir una cadena con información relevante del errorItem
                    Dim errorString As String = $"{errorItem.MessageField} - {errorItem.NumberNameCostCenter}"

                    ' Agregar la cadena a ListError
                    ListError.Add(errorString)
                Next
            End If

            Using formulario As New FrmListErrors(ListError)
                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If


    End Sub

    ''' <summary>
    ''' Crea el menú contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetailAdministrative()
            Case "Remove"
                DeleteDetailAdministrative()
        End Select
    End Sub

    ''' <summary>
    ''' Evento que da la opción de eliminar o modificar los registros de productos de la orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs)
        DeleteInfoAdquisitionType()
    End Sub

    ''' <summary>
    ''' Crea el menú contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs)
        DeleteInfoAdquisitionType()
    End Sub

#End Region

#Region "Activated"
    Private Sub FrmEquipmentCatalog_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled = True Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region

#Region "Popup"

    Private Sub INDpceInfo_Popup(sender As Object, e As EventArgs) Handles INDpceInfo.Popup
        INDsleLegalBook.Focus()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de si afecta presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAffectBudget_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAffectBudget.EditValueChanged
        INDlyItemBudgetaryEntity.Visibility = If(AffectBudget, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyItemBudgetaryEntity.AllowHide = Not AffectBudget
        INDlyItemBudgetaryEntity.ShowInCustomizationForm = Not AffectBudget

        INDlyItemBudgetaryValidity.Visibility = If(AffectBudget, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyItemBudgetaryValidity.AllowHide = Not AffectBudget
        INDlyItemBudgetaryValidity.ShowInCustomizationForm = Not AffectBudget

        INDlyItemBudget.Visibility = If(AffectBudget, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyItemBudget.AllowHide = Not AffectBudget
        INDlyItemBudget.ShowInCustomizationForm = Not AffectBudget

        If Not AffectBudget Then
            CleanBudgetInterface(0)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetaryEntityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetaryEntityId.EditValueChanged
        If _isLoading Then
            Exit Sub
        End If

        CleanBudgetInterface(1)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetaryValidityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetaryValidityId.EditValueChanged
        If _isLoading Then
            Exit Sub
        End If

        CleanBudgetInterface(2)
    End Sub

#End Region

#End Region

#Region "BarButtons"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _searchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        BarraBotones.Focus()
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        BarraBotones.Focus()
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnitAsync(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            Await Me.LoadParameters()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.FixedAssetSequenceDetail IsNot Nothing Then
                If Not Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el botón imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
    End Sub

    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
    End Sub

    Private Sub INDGleClassification_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleClassification.EditValueChanged
        ChangeTextAmortizationAccount()
    End Sub
    ''' <summary>
    ''' Metodo Cambia el nombre de los  campos del segmento tres y ocho dependiento el estado.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChangeTextAmortizationAccount()
        If Classification = 1 Then
            SetTextToFixedAsset()
        ElseIf Classification = 2 Then
            SetTextToIntangible()
        ElseIf Classification = 3 Then
            SetTextToControllableAsset()
        End If
    End Sub

    ''' <summary>
    ''' Método que cambia los controles cuando el parámetro de clasificación es bienes controlados
    ''' </summary>
    Private Sub SetTextToControllableAsset()
        INDlyItemAccountsEntry.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ShowControlsVisibility(DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyItemAccountsEntry.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlcgAccountInformation.Visibility = LayoutVisibility.Always
        INDLciDeclarantRetentionAccount.Visibility = LayoutVisibility.Never
        INDLciNotDeclarantRetetion.Visibility = LayoutVisibility.Never
        INDLciReplacementCreditMainAccount.Visibility = LayoutVisibility.Never
        INDlyHandlesDepreciationByDistribution.Visibility = LayoutVisibility.Never
    End Sub

    ''' <summary>
    ''' Limpia los valores de otros campos cuando es un bien controlable
    ''' </summary>
    Private Sub SetControllableAssetValues()
        IngressLeasingAccountId = Nothing
        LoanDebitAccountId = Nothing
        LoanCreditAccountId = Nothing
        DepreciationAccountId = Nothing
        NetIncomeAccountId = Nothing
        LossMainAccountId = Nothing
        ReplacementCreditMainAccountId = Nothing
        DevaluationCreditAccountId = Nothing
        ValorizationCreditAccountId = Nothing
        DevaluationDebitAccountId = Nothing
        ValorizationDebitAccountId = Nothing
        WarehouseAssetsMainAccountId = Nothing
        MaintenanceAssetsMainAccountId = Nothing
        DepreciationLeasingAccountId = Nothing
        DeclarantRetentionAccountPayableConceptId = Nothing
        NotDeclarantRetentionAccountPayableConceptId = Nothing
        CreditEquipmentPlantAcumulatedAccountId = Nothing
        LoanLeasingAccountId = Nothing
        FinancialRentingAccountId = Nothing
        AccumulatedDeteriorationAccountId = Nothing
        HandlesDepreciationbyDistribution = False
    End Sub

    ''' <summary>
    ''' Este metodo se realiza para reutilizar el código de habilitación de los LayoutControlGroups que se ocultan al parametrizar
    ''' bienes contables en la clasificación
    ''' </summary>
    ''' <param name="visible"></param>
    Private Sub ShowControlsVisibility(visible As LayoutVisibility)

        Dim groups As New List(Of DevExpress.XtraLayout.LayoutControlGroup) From {
            INDlyAccountsDepreciation,
            INDlcgAccountInformation,
            INDlcgValorizationDevaluation,
            INDLcgBasicBilling,
            INDlygAdquisitionType,
            INDlyItemAccountsEntry,
            INDLcgAdministrativeAccountingDetailInformation
        }

        For Each groupItem In groups
            If groupItem.Name <> "INDlyItemAccountsEntry" Then
                groupItem.Visibility = visible
            Else
                UpdateItemControlsVisibility(groupItem, visible)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Este metodo se realiza para reutilizar el código de habilitación de los LayoutControlItems del grupo
    ''' de cuentas de ingresos de activos que se ocultan al parametrizar bienes contables en la clasificación
    ''' </summary>
    ''' <param name="groupItem"></param>
    ''' <param name="visible"></param>
    Private Sub UpdateItemControlsVisibility(groupItem As LayoutControlGroup, visible As LayoutVisibility)
        For Each controlItem In groupItem.Items.OfType(Of DevExpress.XtraLayout.LayoutControlItem)()
            If controlItem.Name <> "INDLciPropertyAccount" Then
                controlItem.Visibility = visible
            Else
                controlItem.Text = If(visible = DevExpress.XtraLayout.Utils.LayoutVisibility.Always,
                                      "Cuenta de Propiedad, Planta y Equipo",
                                      "Cuenta de Gasto de Bienes Controlables")
            End If
        Next
    End Sub

    ''' <summary>
    ''' Método que cambia los nombres de controles cuando el parámetro de clasificación es Intangible
    ''' </summary>
    Private Sub SetTextToIntangible()
        INDlyItemAccountsEntry.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyAccountsDepreciation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyAccountsDepreciation.Text = "Cuentas de Amortización"
        INDLciDepreciationAccount.Text = "Cuenta Amortización"
        INDLciDepreciationLeasingAccount.Text = "Cuenta Amortización Leasing"
        INDlyItemLoanLeasingAccount.Text = "Cuenta Amortización Comodato"
        INDlyItemFinancialRenting.Text = "Cuenta Amortización Renting Financiero"
        INDlyItemAccumulatedDeteriorationAccount.Text = "Cuenta Deterioro"
        INDLciEquipmentPlantDeterioraionCreditAccount.Text = "Cuenta Crédito Deterioro"
        INDLcgAdministrativeAccountingDetailInformation.Text = "Información Contable Amortización"
        INDColDepreciationSpendAccount.Caption = "CC Gasto Amortización"
        INDlyHandlesDepreciationByDistribution.Text = "Maneja Amortización por Distribución"
        INDLcgAdministrativeAccountingDetailInformationByDistribution.Text = "Información contable amortización por distribución"
        ShowControlsVisibility(DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        INDlyItemAccountsEntry.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDColDepreciationExpense.Caption = "CC Gasto Amortización"
    End Sub

    Private Sub SetTextToFixedAsset()
        INDlyItemAccountsEntry.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyAccountsDepreciation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyAccountsDepreciation.Text = "Cuentas de Depreciación"
        INDLciDepreciationAccount.Text = "Cuenta Depreciación"
        INDLciDepreciationLeasingAccount.Text = "Cuenta Depreciación Leasing"
        INDlyItemLoanLeasingAccount.Text = "Cuenta Depreciación Comodato"
        INDlyItemFinancialRenting.Text = "Cuenta Depreciación Renting Financiero"
        INDlyItemAccumulatedDeteriorationAccount.Text = "Cuenta Deterioro Propiedad, Planta y Equipo"
        INDLciEquipmentPlantDeterioraionCreditAccount.Text = "Cuenta Crédito Deterioro Acum. Prop. Planta y Equipo"
        INDLcgAdministrativeAccountingDetailInformation.Text = "Información Contable Depreciación"
        INDColDepreciationSpendAccount.Caption = "CC Gasto Depreciación"
        INDlyHandlesDepreciationByDistribution.Text = "Maneja Depreciación por Distribución"
        INDLcgAdministrativeAccountingDetailInformationByDistribution.Text = "Información contable depreciación por distribución"
        ShowControlsVisibility(DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        INDlyItemAccountsEntry.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDColDepreciationExpense.Caption = "CC Gasto Depreciación"
    End Sub

    ''' <summary>
    ''' Carga la Tupla del Sí o No
    ''' </summary>
    Private Sub InitializeTuples()
        Dim listYesNo = New List(Of Tuple(Of Boolean, String))
        listYesNo.Add(New Tuple(Of Boolean, String)(True, "Sí"))
        listYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDccbeHandlesDepreciationbyDistribution.Properties.DataSource = listYesNo.ToList()
    End Sub

    ''' <summary>
    ''' Evento que se dispara para Mostrar/Ocultar el segmento Información contable de depreciación por distribución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDccbeHandlesDepreciationbyDistribution_EditValueChanged(sender As Object, e As EventArgs) Handles INDccbeHandlesDepreciationbyDistribution.EditValueChanged
        If IsLoaded Then
            If listFixedAssetEquipmentCatalogDetail IsNot Nothing AndAlso Not disableEventFlag Then
                If listFixedAssetEquipmentCatalogDetail.Count > 0 Then
                    If MessageIndigo.Show("Eliminará los detalle de la Rejilla Información Contable Depreciación. ¿Desea continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        listFixedAssetEquipmentCatalogDetail.Clear()
                        INDGcAccountAdministrative.DataSource = Nothing
                        INDGcAccountAdministrativeByDistribution.DataSource = Nothing
                    Else
                        disableEventFlag = True ' Deshabilitar temporalmente el evento para evitar el ciclo infinito
                        HandlesDepreciationbyDistribution = Not HandlesDepreciationbyDistribution
                        disableEventFlag = False ' Volver a habilitar el evento
                        Exit Sub
                    End If
                End If
            End If
            If HandlesDepreciationbyDistribution Then
                INDLcgAdministrativeAccountingDetailInformationByDistribution.HideControl(False)
                INDLcgAdministrativeAccountingDetailInformation.HideControl(True)
            Else
                INDLcgAdministrativeAccountingDetailInformationByDistribution.HideControl(True)
                INDLcgAdministrativeAccountingDetailInformation.HideControl(False)
            End If
        End If
    End Sub
#End Region

End Class