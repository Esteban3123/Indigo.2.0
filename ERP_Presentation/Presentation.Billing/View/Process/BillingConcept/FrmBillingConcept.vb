'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 07/10/2014
'
' Last Modified By : Carlos Mario Arias Rubiano
' Last Modified On : 21/11/2014
' Last Modified By : Diego Andrés Roldán Lozano
' Last Modified On : 29/08/2015
' Description      : Cambiando Formulario de Grupos de Servicios Ips a Conceptos de Facturación
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Contract.MVP
Imports Presentation.Controls

#End Region

Public Class FrmBillingConcept
    Implements IIPSServiceGroup, ICustomizableForm

#Region "GLOBALS"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean
    ''' <summary>
    ''' Representa el presentador de grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PIPSServiceGroup

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.BillingSequence
    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordContract
    ''' <summary>
    ''' representa la entidad de grupos de servicios ips
    ''' </summary>
    ''' <remarks></remarks>
    Private billingConcept As BillingConcept

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListObtainCostCenter As New List(Of Tuple(Of Integer, String))

    Dim _FlagEditMode As Boolean

    Dim _BillingConceptCostCenter As BillingConceptCostCenter

    Dim _ListBillingConceptCostCenter As List(Of BillingConceptCostCenter)

    Dim _ListDeleteBillingConceptCostCenter As List(Of BillingConceptCostCenter)

    Dim _settingsBilling As SettingsBilling

    ''' <summary>
    ''' Array que contiene los tipos de unidades funcionales
    ''' </summary>
    Private _lUnitType As Integer() = {1, 2, 3, 4}

#End Region

#Region "PROPERTIES"

    ''' <summary>
    ''' Cuenta nif para reconocimiento de ingresos
    ''' </summary>
    ''' <returns></returns>
    Public Property IncomeRecognitionPendingBillingMainAccountId As Integer? Implements IIPSServiceGroup.IncomeRecognitionPendingBillingMainAccountId
        Get
            Return INDsleIncomeRecognitionPendingBillingMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleIncomeRecognitionPendingBillingMainAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource para cuenta nif para reconocimiento de ingresos
    ''' </summary>
    ''' <returns></returns>
    Public Property IncomeRecognitionPendingBillingMainAccountXpo As XPInstantFeedbackSource Implements IIPSServiceGroup.IncomeRecognitionPendingBillingMainAccountXpo
        Get
            Return INDsleIncomeRecognitionPendingBillingMainAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIncomeRecognitionPendingBillingMainAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterId As Integer? Implements IIPSServiceGroup.CostCenterId
        Get
            Return INDsleCostCenterSpecific.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenterSpecific.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AlternativeCode As String Implements IIPSServiceGroup.AlternativeCode
        Get
            Return INDTxtAlternativeCode.EditValue
        End Get
        Set(value As String)
            INDTxtAlternativeCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterSpecificXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSServiceGroup.CostCenterSpecificXpo
        Get
            Return INDsleCostCenterSpecific.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenterSpecific.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el modo en que se obtiene el centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ObtainCostCenter As Integer? Implements IIPSServiceGroup.ObtainCostCenter
        Get
            Return INDsleObtainCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleObtainCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IIPSServiceGroup.ActionsOnControls
        Set(value As Boolean)
            INDLcIPSServiceGroup.BeginUpdate()

            INDBteCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDgleBillingType.Enabled = value
            INDsleObtainCostCenter.Enabled = value
            INDsleCostCenterSpecific.Enabled = value
            INDSleEntityIncomeAccount.Enabled = value
            INDSleIndividualIncomeAccount.Enabled = value
            INDSleDiscountAccount.Enabled = value
            INDSleFeesExpensesAccount.Enabled = value
            INDgleAccountingType.Enabled = value
            INDsleIncomeRecognitionPendingBillingMainAccount.Enabled = value
            INDPucIVAAccount.Enabled = value
            INDPucWithholdingTaxAccount.Enabled = value
            INDPucWithholdingICAAccount.Enabled = value
            INDSleIVA.Enabled = value
            INDSleWithholdingTaxConcept.Enabled = value
            INDSleWithholdingICAConcept.Enabled = value
            INDTxtPrice.Enabled = value
            INDsleEconomicActivity.Enabled = value

            INDGcBillingConceptAccount.Enabled = value
            INDgcAccountingPackage.Enabled = value

            INDgleServiceType.Enabled = value
            INDTxtAlternativeCode.Enabled = value
            INDSleCopayMainAccount.Enabled = value
            INDSleRecoveryFixedAmountMainAccount.Enabled = value

            INDLcIPSServiceGroup.EndUpdate()
            If value Then
                INDTxtName.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el consecutivo del grupo
    ''' </summary>
    Public Property Code As String Implements IIPSServiceGroup.Code
        Get
            If (INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la cuenta de descuento
    ''' </summary>
    Public Property DiscountAccountId As Integer? Implements IIPSServiceGroup.DiscountAccountId
        Get
            Return INDSleDiscountAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSleDiscountAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la cuenta contable de ingresos de la entidad
    ''' </summary>
    Public Property EntityIncomeAccountId As Integer? Implements IIPSServiceGroup.EntityIncomeAccountId
        Get
            Return INDSleEntityIncomeAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSleEntityIncomeAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la cuenta de gastos de honorarios
    ''' </summary>
    Public Property FeesExpensesAccountId As Integer? Implements IIPSServiceGroup.FeesExpensesAccountId
        Get
            Return INDSleFeesExpensesAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSleFeesExpensesAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la cuenta contable para ingresos a particulares
    ''' </summary>
    Public Property IndividualIncomeAccountId As Integer? Implements IIPSServiceGroup.IndividualIncomeAccountId
        Get
            Return INDSleIndividualIncomeAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSleIndividualIncomeAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de Concepto
    ''' </summary>
    ''' <value>
    ''' The type of the concept.
    ''' </value>
    Public Property ConceptType As Byte
        Get
            Return CType(INDgleBillingType.EditValue, Byte)
        End Get
        Set(value As Byte)
            INDgleBillingType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de contabilización
    ''' </summary>
    ''' <value>
    ''' The type of the accounting.
    ''' </value>
    Public Property AccountingType As Byte
        Get
            Return CType(INDgleAccountingType.EditValue, Byte)
        End Get
        Set(value As Byte)
            INDgleAccountingType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IIPSServiceGroup.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IIPSServiceGroup.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' nombre del grupo ips
    ''' </summary>
    Public Property NameIpsServiceGroup As String Implements IIPSServiceGroup.NameIpsServiceGroup
        Get
            Return INDTxtName.Text
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    Public Property Sequence As BillingSequence Implements IIPSServiceGroup.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As BillingSequence)
            'Me._sequense = value
            'Me.DicSequense.Clear()
            'For Each seq As Domain.Entities.BillingSequenceDetail In Me._sequense.BillingSequenceDetail
            '    Me.DicSequense.Add(seq.Id, New List(Of String)())
            'Next
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.BillingSequenceDetail In Me._sequence.BillingSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Public Property Status As Boolean Implements IIPSServiceGroup.Status
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
    ''' Obtiene o establece la actividad economica
    ''' </summary>
    ''' <returns></returns>
    Public Property EconomicActivity As Integer?
        Get
            Return INDsleEconomicActivity.EditValue
        End Get
        Set(value As Integer?)
            INDsleEconomicActivity.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el datasource de la actividad economica
    ''' </summary>
    ''' <returns></returns>
    Public Property EconomicActivityDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleEconomicActivity.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEconomicActivity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource para cuenta de descuento
    ''' </summary>
    Public Property DiscountAccountXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSServiceGroup.DiscountAccountXPO
        Get
            Return CType(INDSleDiscountAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleDiscountAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasour para cuenta contable de ingresos de la entidad
    ''' </summary>
    Public Property EntityIncomeAccountXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSServiceGroup.EntityIncomeAccountXPO
        Get
            Return CType(INDSleEntityIncomeAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleEntityIncomeAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource para cuenta de gastos de honorarios
    ''' </summary>
    Public Property FeesExpensesAccountXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSServiceGroup.FeesExpensesAccountXPO
        Get
            Return CType(INDSleFeesExpensesAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleFeesExpensesAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource para cuenta contable para ingresos a particulares
    ''' </summary>
    Public Property IndividualIncomeAccountXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSServiceGroup.IndividualIncomeAccountXPO
        Get
            Return CType(INDSleIndividualIncomeAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleIndividualIncomeAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVAAccountId As Integer? Implements IIPSServiceGroup.IVAAccountId
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
    Public Property IVAAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSServiceGroup.IVAAccountXpo
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
    Public Property WithholdingTaxAccountId As Integer? Implements IIPSServiceGroup.WithholdingTaxAccountId
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
    Public Property WithholdingTaxAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSServiceGroup.WithholdingTaxAccountXpo
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
    Public Property WithholdingICAAccountId As Integer? Implements IIPSServiceGroup.WithholdingICAAccountId
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
    Public Property WithholdingICAAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSServiceGroup.WithholdingICAAccountXpo
        Get
            Return INDPucWithholdingICAAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDPucWithholdingICAAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVAId As Integer? Implements IIPSServiceGroup.IVAId
        Get
            Return INDSleIVA.EditValue
        End Get
        Set(value As Integer?)
            INDSleIVA.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVAXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSServiceGroup.IVAXpo
        Get
            Return INDSleIVA.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleIVA.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de retencion de la fuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingTaxConceptId As Integer? Implements IIPSServiceGroup.WithholdingTaxConceptId
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
    Public Property WithholdingTaxConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSServiceGroup.WithholdingTaxConceptXpo
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
    Public Property WithholdingICAConceptId As Integer? Implements IIPSServiceGroup.WithholdingICAConceptId
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
    Public Property WithholdingICAConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSServiceGroup.WithholdingICAConceptXpo
        Get
            Return INDSleWithholdingICAConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleWithholdingICAConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el precio unitario de venta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Price As Decimal Implements IIPSServiceGroup.Price
        Get
            Return INDTxtPrice.EditValue
        End Get
        Set(value As Decimal)
            INDTxtPrice.EditValue = value
        End Set
    End Property

    Private newPropertyValue As String
    Public Property NewProperty() As String
        Get
            Return newPropertyValue
        End Get
        Set(ByVal value As String)
            newPropertyValue = value
        End Set
    End Property

    Dim fListBillingConceptAccount As List(Of BillingConceptAccount)
    Public Property ListBillingConceptAccount As List(Of BillingConceptAccount)
        Get
            If fListBillingConceptAccount Is Nothing OrElse fListBillingConceptAccount.Count = 0 Then
                fListBillingConceptAccount = New List(Of BillingConceptAccount)()
                For Each i In _lUnitType
                    Dim billingAccount As New BillingConceptAccount()
                    billingAccount.UnitType = i
                    billingAccount.UnitTypeName = GetUnitTypeName(i)
                    fListBillingConceptAccount.Add(billingAccount)
                Next
            End If
            Return fListBillingConceptAccount
        End Get
        Set(value As List(Of BillingConceptAccount))
            If value IsNot Nothing AndAlso value.Count > 0 Then
                For Each i In _lUnitType
                    Dim billingAccount As BillingConceptAccount = value.FirstOrDefault(Function(s) s.UnitType = i)
                    If billingAccount Is Nothing Then
                        Continue For
                    End If
                    billingAccount.UnitTypeName = GetUnitTypeName(i)
                Next
                fListBillingConceptAccount = value
                INDGcBillingConceptAccount.DataSource = value.OrderBy(Function(x) x.UnitType)
            End If
        End Set
    End Property

    Dim fListAccountingPackage As List(Of BillingConceptAccountingPackage)
    Public Property ListAccountingPackage As List(Of BillingConceptAccountingPackage)
        Get
            If fListAccountingPackage Is Nothing OrElse fListAccountingPackage.Count = 0 Then
                fListAccountingPackage = New List(Of BillingConceptAccountingPackage)()
                For Each i In _lUnitType
                    Dim billingAccountingPackage As New BillingConceptAccountingPackage()
                    billingAccountingPackage.UnitType = i
                    billingAccountingPackage.UnitTypeName = GetUnitTypeName(i)
                    fListAccountingPackage.Add(billingAccountingPackage)
                Next
            End If
            Return fListAccountingPackage
        End Get
        Set(value As List(Of BillingConceptAccountingPackage))
            If value IsNot Nothing AndAlso value.Count > 0 Then
                For Each i In _lUnitType
                    Dim billingAccountingPackage As BillingConceptAccountingPackage = value.FirstOrDefault(Function(s) s.UnitType = i)
                    If billingAccountingPackage Is Nothing Then
                        Continue For
                    End If
                    billingAccountingPackage.UnitTypeName = GetUnitTypeName(i)
                Next
                fListAccountingPackage = value
                INDgcAccountingPackage.DataSource = value.OrderBy(Function(x) x.UnitType)
            End If
        End Set
    End Property

    Public ReadOnly Property GetUnitTypeName(unitType As Byte) As String
        Get
            Select Case unitType
                Case 1
                    Return "Urgencias"
                Case 2
                    Return "Hospitalización"
                Case 3
                    Return "Quirófanos"
                Case 4
                    Return "Servicios Ambulatorios"
                Case Else
                    Return ""
            End Select
        End Get
    End Property

    ''' <summary>
    ''' Establece el datasource de sucursales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BranchOfficeXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSServiceGroup.BranchOfficeXpo
        Get
            Return INDSleBranchOffice.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleBranchOffice.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de unidades funcionales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FunctionalUnitXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSServiceGroup.FunctionalUnitXpo
        Get
            Return INDSleFunctionalUnit.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleFunctionalUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSServiceGroup.CostCenterXpo
        Get
            Return INDSleCostCenter.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TypeService As Boolean Implements IIPSServiceGroup.TypeService
        Get
            Return INDgleServiceType.EditValue
        End Get
        Set(value As Boolean)
            INDgleServiceType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de los Servicio principal asociado que sean de tipo "Servicio principal"
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AssociatedMainServiceId As Integer? Implements IIPSServiceGroup.AssociatedMainServiceId
        Get
            Return CType(INDgleAssociatedMainService.EditValue, Integer)
        End Get
        Set(value As Integer?)
            INDgleAssociatedMainService.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de los Servicio principal asociado que sean de tipo "Servicio principal"
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AssociatedMainServiceXpo As XPInstantFeedbackSource Implements IIPSServiceGroup.AssociatedMainServiceXpo
        Get
            Return TryCast(INDgleAssociatedMainService.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDgleAssociatedMainService.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establese el estado de carga de los servicios secundarios
    ''' </summary>
    ''' <returns></returns>
    Private Property _IsLoading As Boolean

#End Region

#Region "CRUD"
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Me.billingConcept IsNot Nothing AndAlso Me.billingConcept.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MIPSServiceGroup(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteIPSServiceGroup(Me.billingConcept)
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
        If Not ValidateControls() Then
            Exit Sub
        End If

        Dim validateErrors As New StringBuilder()
        If ConceptType = 1 Then
            If Not Price > 0 Then
                validateErrors.AppendLine(String.Format("El Precio Unitario de Venta debe ser mayor a 0"))
            End If
        ElseIf ConceptType = 2 Then
            If AccountingType = 1 Then
                If IndividualIncomeAccountId Is Nothing OrElse IndividualIncomeAccountId = 0 Then
                    validateErrors.AppendLine(String.Format("Debe agregar la cuenta de Ingreso a Particular"))
                End If
                If EntityIncomeAccountId Is Nothing OrElse EntityIncomeAccountId = 0 Then
                    validateErrors.AppendLine(String.Format("Debe agregar la cuenta de Entidad a Particular"))
                End If
                If FeesExpensesAccountId Is Nothing OrElse FeesExpensesAccountId = 0 Then
                    validateErrors.AppendLine(String.Format("Debe agregar la cuenta de Gastos de Honorarios"))
                End If
            ElseIf AccountingType = 2 Then
                If billingConcept.BillingConceptAccount IsNot Nothing AndAlso billingConcept.BillingConceptAccount.Count > 0 Then
                    billingConcept.BillingConceptAccount.ToList().ForEach(Sub(o)
                                                                              If o.IndividualIncomeAccountId = 0 Then
                                                                                  validateErrors.AppendLine(String.Format("Debe agregar la Cuenta de Ingreso a Particular para el Tipo de Unidad {0}", o.UnitTypeName))
                                                                              End If
                                                                              If o.EntityIncomeAccountId = 0 Then
                                                                                  validateErrors.AppendLine(String.Format("Debe agregar la Cuenta de Ingreso a Entidad para el Tipo de Unidad {0}", o.UnitTypeName))
                                                                              End If
                                                                              If o.FeesExpensesAccountId Is Nothing OrElse o.FeesExpensesAccountId = 0 Then
                                                                                  validateErrors.AppendLine(String.Format("Debe agregar la Cuenta de Gastos de Honorarios para el Tipo de Unidad {0}", o.UnitTypeName))
                                                                              End If
                                                                              If o.IncomeRecognitionMainAccountId Is Nothing OrElse o.IncomeRecognitionMainAccountId = 0 Then
                                                                                  validateErrors.AppendLine(String.Format("Debe agregar la Cuenta NIIF Reconocimiento Ingresos para el Tipo de Unidad {0}", o.UnitTypeName))
                                                                              End If
                                                                          End Sub)
                End If
            End If
        End If

        If ObtainCostCenter = 3 Then
            If Me._ListBillingConceptCostCenter Is Nothing OrElse Me._ListBillingConceptCostCenter.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe agregar al menos un centro de costo"
                Exit Sub
            End If
        End If

        'Validación paquetes
        If INDlcgAccountingPackage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If billingConcept.BillingConceptAccountingPackage IsNot Nothing AndAlso billingConcept.BillingConceptAccountingPackage.Count > 0 Then
                billingConcept.BillingConceptAccountingPackage.ToList().ForEach(Sub(s)
                                                                                    If s.ConceptMainAccountId = 0 Then
                                                                                        validateErrors.AppendLine(String.Format("Debe agregar la Cuenta de Concepto para el Tipo de Unidad {0} ", s.UnitTypeName))
                                                                                    ElseIf s.FavorableDeviationMainAccountId = 0 Then
                                                                                        validateErrors.AppendLine(String.Format("Debe agregar la Cuenta Desviación Favorable para el Tipo de Unidad {0} ", s.UnitTypeName))
                                                                                    ElseIf s.VariationPVMainAccountId = 0 Then
                                                                                        validateErrors.AppendLine(String.Format("Debe agregar la Cuenta de Variación de precio para el Tipo de Unidad {0} ", s.UnitTypeName))
                                                                                    End If
                                                                                End Sub)
                If validateErrors.Length = 0 Then
                    For Each i In Me._lUnitType
                        If billingConcept.BillingConceptAccountingPackage.Any(Function(f) f.UnitType = i AndAlso (f.ConceptMainAccountId = f.FavorableDeviationMainAccountId OrElse f.ConceptMainAccountId = f.VariationPVMainAccountId OrElse f.FavorableDeviationMainAccountId = f.VariationPVMainAccountId)) Then
                            validateErrors.AppendLine(String.Format("No se puede agregar la misma información contable para el Tipo de Unidad {0}", Me.GetUnitTypeName(i)))
                        End If
                    Next
                End If
            End If
        End If
        'Validamos que el control sea digilenciado si es visible
        If INDliEconomicActivity.Visible AndAlso EconomicActivity Is Nothing Then
            validateErrors.AppendLine(String.Format("No tiene una Actividad Económica registrada "))
        End If

        If validateErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = validateErrors.ToString()
            Exit Sub
        End If

        AssigningValues()
        Try
            Using Model As New MIPSServiceGroup(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of BillingConcept) = Await Model.SaveIPSServiceGroup(Me.billingConcept, Me._idCurrentSequense)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If billingConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequense).RemoveAt(0)
                        End If
                    End If
                    Me.billingConcept = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
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
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewiPSServiceGroup()
        End If
    End Sub
#End Region

#Region "METHODS"

    ''' <summary>
    ''' Metodo que inicializa la tupla del control obtener centro costo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        ListObtainCostCenter = New List(Of Tuple(Of Integer, String))
        ListObtainCostCenter.Add(New Tuple(Of Integer, String)(1, "Unidad Funcional del Paciente"))
        ListObtainCostCenter.Add(New Tuple(Of Integer, String)(2, "Centro de Costo Específico"))
        ListObtainCostCenter.Add(New Tuple(Of Integer, String)(3, "Centro de Costo por Sucursal y Unidad Funcional"))
        INDsleObtainCostCenter.Properties.DataSource = ListObtainCostCenter.ToList

        Dim ListConceptType As New List(Of Tuple(Of Byte, String))()
        ListConceptType.Add(New Tuple(Of Byte, String)(1, "Facturacion Basica"))
        ListConceptType.Add(New Tuple(Of Byte, String)(2, "Facturacion Servicios de Salud"))
        ListConceptType.Add(New Tuple(Of Byte, String)(3, "Facturacion copagos y cuotas moderadoras"))
        INDgleBillingType.Properties.DataSource = ListConceptType

        Dim listAccountingType As New List(Of Tuple(Of Byte, String))()
        listAccountingType.Add(New Tuple(Of Byte, String)(1, "Cuenta Unica de Ingreso"))
        listAccountingType.Add(New Tuple(Of Byte, String)(2, "Cuenta por Tipo de Unidad"))
        INDgleAccountingType.Properties.DataSource = listAccountingType

        Dim listTypeService As New List(Of Tuple(Of Byte, String))()
        listTypeService.Add(New Tuple(Of Byte, String)(0, "Servicio principal"))
        listTypeService.Add(New Tuple(Of Byte, String)(1, "Servicio secundario"))
        INDgleServiceType.Properties.DataSource = listTypeService
    End Sub

    Private Sub SetActionsGrid()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvCostCenterDetail, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvCostCenterDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                              New ColumnInfo() With {.Caption = "Tipo Concepto", .FieldName = "ConceptTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListBillingConcept
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDBteCode.Text = ReturnValue
        If INDBteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        'If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
        '    Using Model As New MBlockRecordAndSequense(MyTag)
        '        Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '        Await Model.DeleteBlockRecord(record)
        '        record = Nothing
        '    End Using
        'Else
        '    Me.BarraBotones.EnableBarItems()
        'End If

        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDLcIPSServiceGroup.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        _IsLoading = False

        Code = String.Empty
        NameIpsServiceGroup = String.Empty
        INDgleBillingType.EditValue = Nothing
        EconomicActivity = Nothing
        TypeService = 0
        INDgleServiceType.Properties.NullText = String.Empty
        AssociatedMainServiceId = Nothing
        AssociatedMainServiceXpo = Nothing
        INDgleAssociatedMainService.Properties.NullText = String.Empty

        ObtainCostCenter = Nothing
        INDlyItemObtainCostCenter.HideControl()
        CostCenterId = Nothing
        INDsleCostCenterSpecific.Properties.NullText = String.Empty
        INDlyItemCostCenterSpecific.HideControl()
        INDgleAccountingType.EditValue = Nothing
        INDliAccountingType.HideControl()

        EntityIncomeAccountId = Nothing
        INDSleEntityIncomeAccount.Properties.NullText = String.Empty
        INDLciEntityIncomeAccount.HideControl(False)
        IndividualIncomeAccountId = Nothing
        INDSleIndividualIncomeAccount.Properties.NullText = String.Empty
        INDLciIndividualIncomeAccount.HideControl()
        DiscountAccountId = Nothing
        INDSleDiscountAccount.Properties.NullText = Nothing
        INDLciFeesExpensesAccount.HideControl()
        FeesExpensesAccountId = Nothing
        INDSleFeesExpensesAccount.Properties.NullText = String.Empty
        IncomeRecognitionPendingBillingMainAccountId = Nothing
        INDsleIncomeRecognitionPendingBillingMainAccount.Properties.NullText = String.Empty
        INDlyItemIncomeRecognitionPendingBillingMainAccount.HideControl()
        IVAAccountId = Nothing
        INDPucIVAAccount.Properties.NullText = String.Empty
        INDLciIVAAccount.HideControl()
        WithholdingTaxAccountId = Nothing
        INDPucWithholdingTaxAccount.Properties.NullText = String.Empty
        INDLciWithholdingTaxAccount.HideControl()
        WithholdingICAAccountId = Nothing
        INDPucWithholdingICAAccount.Properties.NullText = String.Empty
        INDLciWithholdingICAAccount.HideControl()

        INDLcgBasicBilling.HideControl()
        IVAId = Nothing
        INDSleIVA.Properties.NullText = String.Empty
        INDLciIVA.HideControl()
        WithholdingTaxConceptId = Nothing
        INDSleWithholdingTaxConcept.Properties.NullText = String.Empty
        INDLciWithholdingTaxConcept.HideControl()
        WithholdingICAConceptId = Nothing
        INDSleWithholdingICAConcept.Properties.NullText = String.Empty
        INDLciWithholdingICAConcept.HideControl()
        Price = 0
        INDLciPrice.HideControl()
        AlternativeCode = Nothing
        fListBillingConceptAccount = Nothing
        INDGcBillingConceptAccount.DataSource = Nothing
        INDlcgAccounts.HideControl()

        fListAccountingPackage = Nothing
        INDgcAccountingPackage.DataSource = Nothing

        INDGcCostCenterDetail.DataSource = Nothing
        INDLcgCostCenter.HideControl()

        INDLciCopayMainAccount.HideLayout()
        INDLciRecoveryFixedAmountMainAccount.HideLayout()

        CleanControlsPopup()

        billingConcept = Nothing
        _FlagEditMode = False
        _BillingConceptCostCenter = Nothing
        _ListBillingConceptCostCenter = Nothing
        _ListDeleteBillingConceptCostCenter = Nothing
        Status = True

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDLcIPSServiceGroup.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' funcion que limpia los modulos de informacion contable y facturacion basica
    ''' </summary>
    Private Sub CleanInformation()
        EntityIncomeAccountId = Nothing
        DiscountAccountId = Nothing
        WithholdingTaxAccountId = Nothing
        WithholdingICAAccountId = Nothing
        IVAId = Nothing
        WithholdingTaxConceptId = Nothing
        WithholdingICAConceptId = Nothing
        Price = Nothing

        INDSleEntityIncomeAccount.Properties.NullText = String.Empty
        INDSleDiscountAccount.Properties.NullText = String.Empty
        INDPucWithholdingTaxAccount.Properties.NullText = String.Empty
        INDPucWithholdingICAAccount.Properties.NullText = String.Empty
        INDSleIVA.Properties.NullText = String.Empty
        INDSleWithholdingTaxConcept.Properties.NullText = String.Empty
        INDSleWithholdingICAConcept.Properties.NullText = String.Empty
    End Sub

    ''' <summary>
    ''' establece o carga la informacion del servicio secundario seleccionado
    ''' </summary>
    ''' <param name="Billing"></param>
    Private Sub SetInformation(Billing As BillingConcept)

        EntityIncomeAccountId = Billing.EntityIncomeAccountId
        DiscountAccountId = Billing.DiscountAccountId
        WithholdingTaxAccountId = Billing.WithholdingTaxAccountId
        WithholdingICAAccountId = Billing.WithholdingICAAccountId
        IVAId = Billing.IVAId
        WithholdingTaxConceptId = Billing.WithholdingTaxConceptId
        WithholdingICAConceptId = Billing.WithholdingICAConceptId
        Price = Billing.Price

        INDSleEntityIncomeAccount.Properties.NullText = Billing.CodeNameEntityIncomeAccount
        INDSleDiscountAccount.Properties.NullText = Billing.CodeNameDiscountAccount
        INDPucWithholdingTaxAccount.Properties.NullText = Billing.CodeNameWithholdingTaxAccount
        INDPucWithholdingICAAccount.Properties.NullText = Billing.CodeNameWithholdingICAAccount
        INDSleIVA.Properties.NullText = Billing.CodeNameIVA
        INDSleWithholdingTaxConcept.Properties.NullText = Billing.CodeNameWithholdingTaxConcept
        INDSleWithholdingICAConcept.Properties.NullText = Billing.CodeNameWithholdingICAConcept

    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(MyTag, Me.billingConcept.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordContract With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .FormId = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .RecordId = Me.billingConcept.Id}
                Dim operation = Await model.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.billingConcept.Code, Me.billingConcept.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.billingConcept.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.billingConcept.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.billingConcept.Code, Me.billingConcept.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.billingConcept.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me.billingConcept.Code) Then
            Try
                Using model As New MIPSServiceGroup(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me.billingConcept.Status
                    Dim result As ActionResult(Of BillingConcept) = Await model.ChangeStateIPSServiceGroup(Me.billingConcept.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.billingConcept = result.ObjectEmbbeded
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

    ''' <summary>
    ''' metodo para asignar valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With billingConcept
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameIpsServiceGroup
            .ConceptType = ConceptType
            .ObtainCostCenter = ObtainCostCenter
            .CostCenterId = CostCenterId
            .AccountingType = AccountingType

            .EntityIncomeAccountId = EntityIncomeAccountId
            .IndividualIncomeAccountId = IndividualIncomeAccountId
            .DiscountAccountId = DiscountAccountId
            .FeesExpensesAccountId = FeesExpensesAccountId
            .IncomeRecognitionPendingBillingMainAccountId = IncomeRecognitionPendingBillingMainAccountId
            .IVAAccountId = IVAAccountId
            .WithholdingTaxAccountId = WithholdingTaxAccountId
            .WithholdingICAAccountId = WithholdingICAAccountId

            .IVAId = IVAId
            .WithholdingTaxConceptId = WithholdingTaxConceptId
            .WithholdingICAConceptId = WithholdingICAConceptId
            .Price = Price
            .AlternativeCode = If(AlternativeCode Is Nothing, String.Empty, AlternativeCode)
            .CopayMainAccountId = INDSleCopayMainAccount.EditValue
            .RecoveryFixedAmountMainAccountId = INDSleRecoveryFixedAmountMainAccount.EditValue
            .EconomicActivityId = EconomicActivity

            If INDliServiceType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .TypeService = INDgleServiceType.EditValue
            End If

            .AssociatedMainServiceId = Nothing
            If INDliAssociatedMainService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AssociatedMainServiceId = AssociatedMainServiceId
            End If

            If INDLcgCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If _ListBillingConceptCostCenter IsNot Nothing AndAlso _ListBillingConceptCostCenter.Count > 0 Then
                    _ListBillingConceptCostCenter.ForEach(Sub(item)
                                                              .BillingConceptCostCenter.Add(item)
                                                          End Sub)
                End If

                If _ListDeleteBillingConceptCostCenter IsNot Nothing AndAlso _ListDeleteBillingConceptCostCenter.Count > 0 Then
                    _ListDeleteBillingConceptCostCenter.ForEach(Sub(item)
                                                                    .BillingConceptCostCenter.Add(item)
                                                                End Sub)
                End If
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' metodo para preparar el formulario y crear un registro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function NewiPSServiceGroup() As Task
        _IsLoading = True
        billingConcept = New BillingConcept() With {.Status = True}
        _ListBillingConceptCostCenter = New List(Of BillingConceptCostCenter)
        If Me._settingsBilling?.AccountingPackage Then
            If billingConcept.BillingConceptAccountingPackage IsNot Nothing AndAlso billingConcept.BillingConceptAccountingPackage.Count = 0 Then
                'Cargo el dataSource
                INDgcAccountingPackage.DataSource = ListAccountingPackage
                ListAccountingPackage.ForEach(Sub(o)
                                                  billingConcept.BillingConceptAccountingPackage.Add(o)
                                              End Sub)
            End If
        End If

        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequence.BillingSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.BillingSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequense = Me._sequence.BillingSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New Presentation.Billing.MVP.MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
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
    ''' metodo para cargar los controles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MIPSServiceGroup(CStr(Me.Tag))
                    AsyncLoader(True)
                    billingConcept = Await Model.GetIPSServiceGroup(INDBteCode.Text.Trim)
                    INDLcIPSServiceGroup.BeginUpdate()
                    If billingConcept IsNot Nothing AndAlso billingConcept.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(billingConcept.Id))
                            _IsLoading = False
                            With billingConcept
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                NameIpsServiceGroup = .Name
                                ConceptType = .ConceptType
                                ObtainCostCenter = .ObtainCostCenter
                                CostCenterId = .CostCenterId
                                INDsleCostCenterSpecific.Properties.NullText = .CodeNameCostCenter
                                AccountingType = .AccountingType
                                If .EconomicActivityId IsNot Nothing Then
                                    EconomicActivity = .EconomicActivityId
                                End If

                                EntityIncomeAccountId = .EntityIncomeAccountId
                                INDSleEntityIncomeAccount.Properties.NullText = .CodeNameEntityIncomeAccount
                                IndividualIncomeAccountId = .IndividualIncomeAccountId
                                INDSleIndividualIncomeAccount.Properties.NullText = .CodeNameIndividualIncomeAccount
                                DiscountAccountId = .DiscountAccountId
                                INDSleDiscountAccount.Properties.NullText = .CodeNameDiscountAccount
                                FeesExpensesAccountId = .FeesExpensesAccountId
                                INDSleFeesExpensesAccount.Properties.NullText = .CodeNameFeesExpensesAccount
                                IncomeRecognitionPendingBillingMainAccountId = .IncomeRecognitionPendingBillingMainAccountId
                                INDsleIncomeRecognitionPendingBillingMainAccount.Properties.NullText = .IncomeRecognitionPendingBillingMainAccountDescription
                                IVAAccountId = .IVAAccountId
                                INDPucIVAAccount.Properties.NullText = .CodeNameIVAAccount
                                WithholdingTaxAccountId = .WithholdingTaxAccountId
                                INDPucWithholdingTaxAccount.Properties.NullText = .CodeNameWithholdingTaxAccount
                                WithholdingICAAccountId = .WithholdingICAAccountId
                                INDPucWithholdingICAAccount.Properties.NullText = .CodeNameWithholdingICAAccount

                                IVAId = .IVAId
                                INDSleIVA.Properties.NullText = .CodeNameIVA
                                WithholdingTaxConceptId = .WithholdingTaxConceptId
                                INDSleWithholdingTaxConcept.Properties.NullText = .CodeNameWithholdingTaxConcept
                                WithholdingICAConceptId = .WithholdingICAConceptId
                                INDSleWithholdingICAConcept.Properties.NullText = .CodeNameWithholdingICAConcept
                                Price = .Price

                                TypeService = .TypeService

                                AssociatedMainServiceId = .AssociatedMainServiceId
                                INDgleAssociatedMainService.Properties.NullText = .AssociatedMainServiceName
                                AlternativeCode = .AlternativeCode
                                INDSleCopayMainAccount.EditValue = .CopayMainAccountId
                                INDSleRecoveryFixedAmountMainAccount.EditValue = .RecoveryFixedAmountMainAccountId
                                INDSleCopayMainAccount.Properties.NullText = .CopayMainAccountDescription
                                INDSleRecoveryFixedAmountMainAccount.Properties.NullText = .RecoveryFixedAmountMainAccountDescription
                                Status = .Status
                            End With
                            _IsLoading = True
                            ListBillingConceptAccount = billingConcept.BillingConceptAccount.ToList()
                            If billingConcept.BillingConceptAccount IsNot Nothing AndAlso billingConcept.BillingConceptAccount.Count > 0 Then
                                INDrptAccountParticular_QueryPopUp(Me, Nothing)
                                INDrptSleAccountEntity_QueryPopUp(Me, Nothing)
                                INDrptSleExpensesAccountEntity_QueryPopUp(Me, Nothing)
                                INDrptSleIncomeRecognition_QueryPopUp(Me, Nothing)
                                INDRiSleDiscountAccount_QueryPopUp(Me, Nothing)
                            End If
                            _ListBillingConceptCostCenter = billingConcept.BillingConceptCostCenter.ToList()
                            INDGcCostCenterDetail.DataSource = Nothing
                            INDGcCostCenterDetail.DataSource = _ListBillingConceptCostCenter
                            'Cargo información de paquete
                            If billingConcept.BillingConceptAccountingPackage?.Any() Then
                                INDrptSleAccountConcept_QueryPopUp(Me, Nothing)
                                INDrptSleFavorableAccount_QueryPopUp(Me, Nothing)
                                INDrptSleVariationAccount_QueryPopUp(Me, Nothing)
                                ListAccountingPackage = billingConcept.BillingConceptAccountingPackage.ToList()
                            ElseIf Me._settingsBilling?.AccountingPackage Then
                                INDgcAccountingPackage.DataSource = ListAccountingPackage
                                ListAccountingPackage.ForEach(Sub(o)
                                                                  billingConcept.BillingConceptAccountingPackage.Add(o)
                                                              End Sub)
                            End If

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.billingConcept.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = billingConcept.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(billingConcept.Id, Me.Tag.ToString(), Nothing, GetType(BillingConcept).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewiPSServiceGroup()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLcIPSServiceGroup.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' establece los datasorce de los combos cuando se agrega un registro desde el boton mas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDatasources()
        If EntityIncomeAccountXPO IsNot Nothing Then
            Presenter.InitializeEntityIncomeAccountXPO()
        End If
        If IndividualIncomeAccountXPO IsNot Nothing Then
            Presenter.InitializeIndividualIncomeAccountXPO()
        End If
        If DiscountAccountXPO IsNot Nothing Then
            Presenter.InitializeDiscountAccountXPO()
        End If
        If FeesExpensesAccountXPO IsNot Nothing Then
            Presenter.InitializeFeesExpensesAccountXPO()
        End If
    End Sub

    ''' <summary>
    ''' Función para cargar parametros
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadParameters() As Task
        Using model As New Presentation.Billing.MVP.MSettingBilling(CStr(Me.Tag))
            Dim result = Await model.GetSettingsBillingByIdUnitOperative(Me._idOperativeUnit)
            If Not result.StateResult Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoExistenParametrosFacturacion", "Facturacion")
                Exit Function
            End If
            Me._settingsBilling = result.ObjectEmbbeded
        End Using
    End Function

#Region "BillingConceptCostCenter"

    Private Sub CleanControlsPopup()
        INDSleBranchOffice.EditValue = Nothing
        INDSleBranchOffice.Properties.NullText = String.Empty
        INDSleBranchOffice.Properties.ReadOnly = False

        INDSleFunctionalUnit.EditValue = Nothing
        INDSleFunctionalUnit.Properties.NullText = String.Empty
        INDSleFunctionalUnit.Properties.ReadOnly = False

        INDSleCostCenter.EditValue = Nothing
        INDSleCostCenter.Properties.NullText = String.Empty

        _FlagEditMode = False
    End Sub

    Private Function ValidateControlsPopup() As String
        Dim listErrors As New StringBuilder
        If INDSleBranchOffice.EditValue Is Nothing Then
            listErrors.AppendLine("Debe seleccionar una Sucursal.")
        End If
        If INDSleFunctionalUnit.EditValue Is Nothing Then
            listErrors.AppendLine("Debe seleccionar una Unidad Funcional.")
        End If
        If INDSleCostCenter.EditValue Is Nothing Then
            listErrors.AppendLine("Debe seleccionar un Centro de Costo.")
        End If
        Return listErrors.ToString
    End Function

    Private Sub AddBillingConceptCostCenter()
        'Se valida que los controles esten diligenciados
        Dim errors As String = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If _FlagEditMode = False Then 'Si se esta guardando
            If _ListBillingConceptCostCenter.Any(Function(d) d.BranchOfficeId = INDSleBranchOffice.EditValue AndAlso d.FunctionalUnitId = INDSleFunctionalUnit.EditValue) Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format("La sucursal {0} con la unidad funcional {1} ya existe en la lista.", INDSleBranchOffice.Text, INDSleFunctionalUnit.Text)
                INDSleBranchOffice.Focus()
                Exit Sub
            End If

            _BillingConceptCostCenter = New BillingConceptCostCenter
            'Se crea la nueva entidad para agregarlo al listado
            With _BillingConceptCostCenter
                .BranchOfficeId = INDSleBranchOffice.EditValue
                .BranchOfficeCodeName = INDSleBranchOffice.Text
                .FunctionalUnitId = INDSleFunctionalUnit.EditValue
                .FunctionalUnitCodeName = INDSleFunctionalUnit.Text
                .CostCenterId = INDSleCostCenter.EditValue
                .CostCenterCodeName = INDSleCostCenter.Text
            End With
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
            _ListBillingConceptCostCenter.Add(_BillingConceptCostCenter)
        Else 'Si se esta editando
            With _BillingConceptCostCenter
                .CostCenterId = INDSleCostCenter.EditValue
                .CostCenterCodeName = INDSleCostCenter.Text
            End With
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
        End If

        INDGcCostCenterDetail.DataSource = Nothing
        INDGcCostCenterDetail.DataSource = _ListBillingConceptCostCenter
        CleanControlsPopup()
        INDPceAddCostCenter.ShowPopup()
        INDSleBranchOffice.Focus()
    End Sub

    Private Sub EditBillingConceptCostCenter()
        _BillingConceptCostCenter = CType(INDGvCostCenterDetail.GetFocusedRow, BillingConceptCostCenter)

        INDSleBranchOffice.EditValue = _BillingConceptCostCenter.BranchOfficeId
        INDSleBranchOffice.Properties.NullText = _BillingConceptCostCenter.BranchOfficeCodeName
        INDSleBranchOffice.Properties.ReadOnly = True

        INDSleFunctionalUnit.EditValue = _BillingConceptCostCenter.FunctionalUnitId
        INDSleFunctionalUnit.Properties.NullText = _BillingConceptCostCenter.FunctionalUnitCodeName
        INDSleFunctionalUnit.Properties.ReadOnly = True

        INDSleCostCenter.EditValue = _BillingConceptCostCenter.CostCenterId
        INDSleCostCenter.Properties.NullText = _BillingConceptCostCenter.CostCenterCodeName

        _FlagEditMode = True
        INDPceAddCostCenter.ShowPopup()
        INDSleCostCenter.Focus()
    End Sub

    Private Sub DeleteCostInventoryGroupDetail()
        _BillingConceptCostCenter = CType(INDGvCostCenterDetail.GetFocusedRow, BillingConceptCostCenter)
        _ListBillingConceptCostCenter.Remove(_BillingConceptCostCenter)

        If _BillingConceptCostCenter.Id > 0 Then
            If _ListDeleteBillingConceptCostCenter Is Nothing Then
                _ListDeleteBillingConceptCostCenter = New List(Of BillingConceptCostCenter)
            End If
            _BillingConceptCostCenter.MarkAsDeleted()
            _ListDeleteBillingConceptCostCenter.Add(_BillingConceptCostCenter)
        End If

        INDGcCostCenterDetail.DataSource = Nothing
        INDGcCostCenterDetail.DataSource = _ListBillingConceptCostCenter
    End Sub

    Private Async Function CopyAndPasteBillingConceptCostCenter(data As List(Of List(Of String))) As Task
        INDGvCostCenterDetail.ShowLoadingPanel()
        Me.Cursor = BaseClass.ChangeCursorIndigo()

        Using model As New MIPSServiceGroup(MyTag)
            Dim result = Await model.CopyAndPasteBillingConceptCostCenter(data)
            If result.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                Me.Cursor = System.Windows.Forms.Cursors.Default
                INDGvCostCenterDetail.HideLoadingPanel()
                Exit Function
            End If
            If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                For Each BillingConceptCostCenter In result.ObjectEmbbeded
                    If Me._ListBillingConceptCostCenter.Any(Function(d) d.BranchOfficeId = BillingConceptCostCenter.BranchOfficeId AndAlso d.FunctionalUnitId = BillingConceptCostCenter.FunctionalUnitId) Then
                        If result.MessageResult Is Nothing Then
                            result.MessageResult = New List(Of String)
                        End If

                        result.MessageResult.Add(String.Format("La sucursal {0} con la unidad funcional {1} ya existe en la lista.", BillingConceptCostCenter.BranchOfficeCodeName, BillingConceptCostCenter.FunctionalUnitCodeName))
                        Continue For
                    End If

                    _ListBillingConceptCostCenter.Add(BillingConceptCostCenter)
                Next

                INDGcCostCenterDetail.DataSource = Nothing
                INDGcCostCenterDetail.DataSource = _ListBillingConceptCostCenter
            End If
            If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                Using formulario As New FrmListErrors(result.MessageResult)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
        End Using

        Me.Cursor = System.Windows.Forms.Cursors.Default
        INDGvCostCenterDetail.HideLoadingPanel()
    End Function

#End Region

#End Region

#Region "HANDLES"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _searchMode = Nothing
        Presenter = Nothing
        _sequence = Nothing
        _idCurrentSequense = Nothing
        record = Nothing
        billingConcept = Nothing
        ListObtainCostCenter = Nothing

        _FlagEditMode = Nothing
        _BillingConceptCostCenter = Nothing
        _ListBillingConceptCostCenter = Nothing
        _ListDeleteBillingConceptCostCenter = Nothing
        _settingsBilling = Nothing

        TypeService = Nothing
        AssociatedMainServiceId = Nothing
        AssociatedMainServiceXpo = Nothing

    End Sub


    Private Async Sub FrmBillingConcept_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDLcBillingGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PIPSServiceGroup(Me)
        Presenter.GetSequense()
        Try
            AsyncLoader(True)
            Await Me.LoadParameters()
            If Me._settingsBilling?.AccountingPackage Then
                INDlcgAccountingPackage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDlcgAccountingPackage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        Catch ex As Exception
            Me.Mensaje(EeventViewerImages.Advertencia) = ex.Message
        Finally
            AsyncLoader(False)
        End Try
        Using model As New MCompanySettings(Tag)
            Dim _companySettings = Await model.GetCompanySettings()
            If _companySettings IsNot Nothing Then
                If _companySettings.TransactionEconomicActivity Then
                    INDliEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = "No se encontró campo parametrizado de Actividad Económica"

            End If
        End Using
        InitializeTuples()
        Me.InitializeEconomicActivity()
        IndigoGridControl1.RefreshGrid(INDGcBillingConceptAccount)
        'Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
        SetActionsGrid()
    End Sub

#End Region

#Region "Activated"
    Private Sub FrmIPSServiceGroups_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmBillingConcept_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "KeyDown"
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
                    Await Me.NewiPSServiceGroup()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
#End Region

#Region "ButtonClick"

    Private Sub INDsleIncomeRecognitionPendingBillingMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIncomeRecognitionPendingBillingMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeIncomeRecognition()
        End If
    End Sub

    Private Sub INDBteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenterSpecific_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenterSpecific.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCostCenter With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeCostCenterSpecific()
        End If
    End Sub

    Private Sub INDSleEntityIncomeAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleEntityIncomeAccount.ButtonClick, INDSleIndividualIncomeAccount.ButtonClick, INDSleDiscountAccount.ButtonClick, INDSleFeesExpensesAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPopupPUC
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(800, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                SetDatasources()
            End Using
        End If
    End Sub
#End Region

#Region "IdEntityLoaded"
    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    ''' 
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.billingConcept IsNot Nothing AndAlso Me.billingConcept.Id > 0 Then
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

#Region "QueryPopUp"
    Private Sub INDSleEntityIncomeAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleEntityIncomeAccount.QueryPopUp
        If EntityIncomeAccountXPO Is Nothing Then
            Presenter.InitializeEntityIncomeAccountXPO()
        End If
    End Sub

    Private Sub INDSleIndividualIncomeAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleIndividualIncomeAccount.QueryPopUp
        If IndividualIncomeAccountXPO Is Nothing Then
            Presenter.InitializeIndividualIncomeAccountXPO()
        End If
    End Sub

    Private Sub INDSleDiscountAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleDiscountAccount.QueryPopUp
        If DiscountAccountXPO Is Nothing Then
            Presenter.InitializeDiscountAccountXPO()
        End If
    End Sub

    Private Sub INDSleFeesExpensesAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleFeesExpensesAccount.QueryPopUp
        If FeesExpensesAccountXPO Is Nothing Then
            Presenter.InitializeFeesExpensesAccountXPO()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de centro costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenterSpecific_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenterSpecific.QueryPopUp
        If CostCenterSpecificXpo Is Nothing Then
            Presenter.InitializeCostCenterSpecific()
        End If
    End Sub

    Private Sub INDrptSleAccountEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptSleAccountEntity.QueryPopUp
        If INDrptSleAccountEntity.DataSource Is Nothing Then
            Using model As New MIPSServiceGroup(Me.Tag)
                INDrptSleAccountEntity.DataSource = model.GetMainAccounts()
            End Using
        End If
    End Sub

    Private Sub INDrptSleIncomeRecognition_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptSleIncomeRecognition.QueryPopUp
        If INDrptSleIncomeRecognition.DataSource Is Nothing Then
            Using model As New MIPSServiceGroup(Me.Tag)
                INDrptSleIncomeRecognition.DataSource = model.GetMainAccounts()
            End Using
        End If
    End Sub

    Private Sub INDrptAccountParticular_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptAccountParticular.QueryPopUp
        If INDrptAccountParticular.DataSource Is Nothing Then
            Using model As New MIPSServiceGroup(Me.Tag)
                INDrptAccountParticular.DataSource = model.GetMainAccounts()
            End Using
        End If
    End Sub

    Private Sub INDrptSleExpensesAccountEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptSleExpensesAccountEntity.QueryPopUp
        If INDrptSleExpensesAccountEntity.DataSource Is Nothing Then
            Using model As New MIPSServiceGroup(Me.Tag)
                INDrptSleExpensesAccountEntity.DataSource = model.GetMainAccounts()
            End Using
        End If
    End Sub

    Private Sub INDrptSleAccountConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptSleAccountConcept.QueryPopUp
        If INDrptSleAccountConcept.DataSource Is Nothing Then
            Using model As New MIPSServiceGroup(Me.Tag)
                INDrptSleAccountConcept.DataSource = model.GetMainAccounts()
            End Using
        End If
    End Sub

    Private Sub INDrptSleFavorableAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptSleFavorableAccount.QueryPopUp
        If INDrptSleFavorableAccount.DataSource Is Nothing Then
            Using model As New MIPSServiceGroup(Me.Tag)
                INDrptSleFavorableAccount.DataSource = model.GetMainAccounts()
            End Using
        End If
    End Sub

    Private Sub INDrptSleVariationAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptSleVariationAccount.QueryPopUp
        If INDrptSleVariationAccount.DataSource Is Nothing Then
            Using model As New MIPSServiceGroup(Me.Tag)
                INDrptSleVariationAccount.DataSource = model.GetMainAccounts()
            End Using
        End If
    End Sub

    Private Sub INDsleIncomeRecognitionPendingBillingMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIncomeRecognitionPendingBillingMainAccount.QueryPopUp
        If IncomeRecognitionPendingBillingMainAccountXpo Is Nothing Then
            Presenter.InitializeIncomeRecognition()
        End If
    End Sub

    Private Sub INDPucIVAAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPucIVAAccount.QueryPopUp
        If IVAAccountXpo Is Nothing Then
            Presenter.InitializeIVAAccount()
        End If
    End Sub

    Private Sub INDPucWithholdingTaxAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPucWithholdingTaxAccount.QueryPopUp
        If WithholdingTaxAccountXpo Is Nothing Then
            Presenter.InitializeWithholdingTaxAccount()
        End If
    End Sub

    Private Sub INDPucWithholdingICAAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPucWithholdingICAAccount.QueryPopUp
        If WithholdingICAAccountXpo Is Nothing Then
            Presenter.InitializeWithholdingICAAccount()
        End If
    End Sub

    Private Sub INDSleIVA_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleIVA.QueryPopUp
        If IVAXpo Is Nothing Then
            Presenter.InitializeIVA()
        End If
    End Sub

    Private Sub INDSleWithholdingTaxConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleWithholdingTaxConcept.QueryPopUp
        If WithholdingTaxConceptXpo Is Nothing Then
            Presenter.InitializeWithholdingTaxConcept()
        End If
    End Sub

    Private Sub INDSleWithholdingICAConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleWithholdingICAConcept.QueryPopUp
        If WithholdingICAConceptXpo Is Nothing Then
            Presenter.InitializeWithholdingICAConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de sucursales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleBranchOffice_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBranchOffice.QueryPopUp
        If BranchOfficeXpo Is Nothing Then
            Presenter.InitializeBranchOffice()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de unidades funcionales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleFunctionalUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleFunctionalUnit.QueryPopUp
        If FunctionalUnitXpo Is Nothing Then
            Presenter.InitializeFunctionalUnit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de centro costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCostCenter.QueryPopUp
        If CostCenterXpo Is Nothing Then
            Presenter.InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' evento que consulta el datasource de las cuentas para descuento de clase de tipo resultado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiSleDiscountAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDRiSleDiscountAccount.QueryPopUp
        If INDRiSleDiscountAccount.DataSource Is Nothing Then
            Using model As New MIPSServiceGroup(Me.Tag)
                INDRiSleDiscountAccount.DataSource = model.GetMainAccounts(ClassType:=2)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el servicio principal asociado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgleAssociatedMainService_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDgleAssociatedMainService.QueryPopUp
        If INDgleAssociatedMainService.Properties.DataSource Is Nothing And billingConcept IsNot Nothing Then
            Presenter.InitializeAssociatedMainService(billingConcept.Id)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de obtencion de centro costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleObtainCostCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleObtainCostCenter.EditValueChanged
        If ObtainCostCenter IsNot Nothing Then
            If ObtainCostCenter = 3 Then
                INDsleCostCenterSpecific.EditValue = Nothing
                INDlyItemCostCenterSpecific.HideControl()
                INDLcgCostCenter.HideControl(False)
            ElseIf ObtainCostCenter = 2 Then
                INDlyItemCostCenterSpecific.HideControl(False)
                INDLcgCostCenter.HideControl()
            Else
                INDsleCostCenterSpecific.EditValue = Nothing
                INDlyItemCostCenterSpecific.HideControl()
                INDLcgCostCenter.HideControl()
            End If
        Else
            INDsleCostCenterSpecific.EditValue = Nothing
            INDlyItemCostCenterSpecific.HideControl()
            INDLcgCostCenter.HideControl()
        End If
    End Sub

    Private Sub INDgleBillingType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleBillingType.EditValueChanged
        If ConceptType <> 0 Then
            HideControls()
        End If
        If ConceptType = 1 Then
            INDliServiceType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciAlternativeCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        ElseIf ConceptType = 2 Then
            INDliServiceType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciAlternativeCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDgleServiceType.EditValue = 0
        ElseIf ConceptType = 3 Then
            INDLciCopayMainAccount.ShowLayout()
            INDLciRecoveryFixedAmountMainAccount.ShowLayout()
        End If
    End Sub

    Private Sub INDgleServiceType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleServiceType.EditValueChanged
        If TypeService = 0 Then
            INDliAssociatedMainService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDliAssociatedMainService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

    End Sub
    ''' <summary>
    ''' Inicializa el datasource de la actividad economica
    ''' </summary>
    Private Sub InitializeEconomicActivity()
        EconomicActivityDatasource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).TreasuryService.ListEconomicActivity()
    End Sub

    Private Async Sub INDgleAssociatedMainService_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleAssociatedMainService.EditValueChanged
        Try
            If AssociatedMainServiceId IsNot Nothing And _IsLoading Then
                AsyncLoader(True)
                CleanInformation()
                Using model As New MIPSServiceGroup(Me.Tag)

                    Dim BillingConcept = Await model.GetIPSServiceGroupById(AssociatedMainServiceId)
                    If BillingConcept IsNot Nothing Then
                        SetInformation(BillingConcept)
                        AsyncLoader(False)
                    End If
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Private Sub INDgleAccountingType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleAccountingType.EditValueChanged
        HideControlsAccountingType()
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAddCostCenterDetail_Click(sender As Object, e As EventArgs) Handles INDBtnAddCostCenterDetail.Click
        AddBillingConceptCostCenter()
    End Sub
    ''' <summary>
    ''' Metodo para abrir el frm de actividades economicas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEconomicActivity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEconomicActivity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmEconomicActivity
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False

                Dim proportionWidth As Double = 0.6 ' 60% del ancho de la pantalla
                Dim proportionHeight As Double = 0.8 ' 80% de la altura de la pantalla

                ' Calcula el tamaño proporcional
                Dim screenWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width 'Calculamos el ancho de la pantalla actual
                Dim screenHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height 'Obtenemos la altura de la pantalla actual
                'Nuevas medidas de nuestro popup
                Dim popUpWidth As Integer = CInt(screenWidth * proportionWidth)
                Dim popUpHeight As Integer = CInt(screenHeight * proportionHeight)

                ' Asignamos el nuevo tamaño al formulario de actividades Economicas
                Formulario.Size = New Size(popUpWidth, popUpHeight)
                Formulario.StartPosition = FormStartPosition.CenterParent 'Centramos el frm

                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub

#End Region

#Region "ContexMenuActions"

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Edit"
                EditBillingConceptCostCenter()
            Case "Remove"
                DeleteCostInventoryGroupDetail()
        End Select
    End Sub

#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If e.Rows.Count = 0 Then
            Exit Sub
        End If
        If e.Rows.Count > 300 Then
            Mensaje(EeventViewerImages.Advertencia) = "Se puede procesar máximo 300 registros"
            Exit Sub
        End If

        Try
            If sender.Name = INDGcCostCenterDetail.Name Then
                Await Me.CopyAndPasteBillingConceptCostCenter(e.Rows)
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

#End Region

#Region "BAR BUTTONS"

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
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString.Replace("_", ""))
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _searchMode = False
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
#End Region


    Private Async Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.BillingSequenceDetail IsNot Nothing Then
                If Not Me._sequence.BillingSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
            Try
                AsyncLoader(True)
                Await Me.LoadParameters()
                If Me._settingsBilling?.AccountingPackage Then
                    INDlcgAccountingPackage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDlcgAccountingPackage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            Catch ex As Exception
                Me.Mensaje(EeventViewerImages.Advertencia) = ex.Message
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub

#Region "Enums"
    Public Enum eConceptType
        FacturacionBasica = 1
        FacturacionServiciosSalud = 2
        FacturacionCopago
    End Enum
#End Region

    Private Sub HideControls()
        If ConceptType = eConceptType.FacturacionBasica Then
            INDlyItemObtainCostCenter.HideControl()
            INDsleObtainCostCenter.EditValue = Nothing

            INDlyItemCostCenterSpecific.HideControl()
            INDsleCostCenterSpecific.EditValue = Nothing
            INDsleCostCenterSpecific.Properties.NullText = String.Empty

            INDliAccountingType.HideControl()
            INDgleAccountingType.EditValue = Nothing

            INDLciIndividualIncomeAccount.HideControl()
            INDSleIndividualIncomeAccount.EditValue = Nothing
            INDSleIndividualIncomeAccount.Properties.NullText = String.Empty

            INDLciFeesExpensesAccount.HideControl()
            INDSleFeesExpensesAccount.EditValue = Nothing
            INDSleFeesExpensesAccount.Properties.NullText = String.Empty

            INDlyItemIncomeRecognitionPendingBillingMainAccount.HideControl()
            INDsleIncomeRecognitionPendingBillingMainAccount.EditValue = Nothing
            INDsleIncomeRecognitionPendingBillingMainAccount.Properties.NullText = String.Empty

            INDLciEntityIncomeAccount.HideControl(False)
            INDLciIVAAccount.HideControl(True)
            INDLciWithholdingTaxAccount.HideControl(False)
            INDLciWithholdingICAAccount.HideControl(False)

            INDLcgBasicBilling.HideControl(False)
            INDLciIVA.HideControl(False)
            INDLciWithholdingTaxConcept.HideControl(False)
            INDLciWithholdingICAConcept.HideControl(False)
            INDLciPrice.HideControl(False)

            INDlcgAccounts.HideControl()
            INDLciCopayMainAccount.HideLayout()
            INDLciRecoveryFixedAmountMainAccount.HideLayout()
            INDSleCopayMainAccount.EditValue = Nothing
            INDSleRecoveryFixedAmountMainAccount.EditValue = Nothing
            INDSleCopayMainAccount.Properties.NullText = String.Empty
            INDSleRecoveryFixedAmountMainAccount.Properties.NullText = String.Empty
            INDLciDiscountAccount.ShowLayout()
        ElseIf ConceptType = eConceptType.FacturacionServiciosSalud Then
            INDlyItemObtainCostCenter.HideControl(False)
            INDliAccountingType.HideControl(False)

            INDLciIVAAccount.HideControl()
            INDPucIVAAccount.EditValue = Nothing
            INDPucIVAAccount.Properties.NullText = String.Empty

            INDLciWithholdingTaxAccount.HideControl()
            INDPucWithholdingTaxAccount.EditValue = Nothing
            INDPucWithholdingTaxAccount.Properties.NullText = String.Empty

            INDLciWithholdingICAAccount.HideControl()
            INDPucWithholdingICAAccount.EditValue = Nothing
            INDPucWithholdingICAAccount.Properties.NullText = String.Empty

            INDLcgBasicBilling.HideControl()

            INDLciIVA.HideControl()
            INDSleIVA.EditValue = Nothing
            INDSleIVA.Properties.NullText = String.Empty

            INDLciWithholdingTaxConcept.HideControl()
            INDSleWithholdingTaxConcept.EditValue = Nothing
            INDSleWithholdingTaxConcept.Properties.NullText = String.Empty

            INDLciWithholdingICAConcept.HideControl()
            INDSleWithholdingICAConcept.EditValue = Nothing
            INDSleWithholdingICAConcept.Properties.NullText = String.Empty

            Price = 0
            INDLciPrice.HideControl()
            INDLciCopayMainAccount.HideLayout()
            INDLciRecoveryFixedAmountMainAccount.HideLayout()
            INDSleCopayMainAccount.EditValue = Nothing
            INDSleRecoveryFixedAmountMainAccount.EditValue = Nothing
            INDSleCopayMainAccount.Properties.NullText = String.Empty
            INDSleRecoveryFixedAmountMainAccount.Properties.NullText = String.Empty
            INDLciDiscountAccount.ShowLayout()
        ElseIf ConceptType = eConceptType.FacturacionCopago Then
            INDlyItemObtainCostCenter.HideControl()
            INDsleObtainCostCenter.EditValue = Nothing

            INDlyItemCostCenterSpecific.HideControl()
            INDsleCostCenterSpecific.EditValue = Nothing
            INDsleCostCenterSpecific.Properties.NullText = String.Empty

            INDliAccountingType.HideControl()
            INDgleAccountingType.EditValue = Nothing

            INDLciIndividualIncomeAccount.HideControl()
            INDSleIndividualIncomeAccount.EditValue = Nothing
            INDSleIndividualIncomeAccount.Properties.NullText = String.Empty

            INDLciFeesExpensesAccount.HideControl()
            INDSleFeesExpensesAccount.EditValue = Nothing
            INDSleFeesExpensesAccount.Properties.NullText = String.Empty

            INDlyItemIncomeRecognitionPendingBillingMainAccount.HideControl()
            INDsleIncomeRecognitionPendingBillingMainAccount.EditValue = Nothing
            INDsleIncomeRecognitionPendingBillingMainAccount.Properties.NullText = String.Empty

            INDLciEntityIncomeAccount.HideControl()
            INDLciIVAAccount.HideControl()
            INDLciWithholdingTaxAccount.HideControl()
            INDLciWithholdingICAAccount.HideControl()

            INDLcgBasicBilling.HideControl()
            INDLciIVA.HideControl()
            INDLciWithholdingTaxConcept.HideControl()
            INDLciWithholdingICAConcept.HideControl()
            INDLciAlternativeCode.ShowLayout()
            INDLciPrice.HideControl()
            INDlcgAccounts.HideControl()
            INDliServiceType.HideLayout()
            INDLciDiscountAccount.HideLayout()
            INDSleDiscountAccount.EditValue = Nothing
            INDSleDiscountAccount.Properties.NullText = String.Empty
        End If
    End Sub

    Private Sub HideControlsAccountingType()
        If AccountingType = 1 Then
            'Cuenta unica de ingresos
            INDlcgAccounts.HideControl()

            INDLciEntityIncomeAccount.HideControl(False)
            INDLciIndividualIncomeAccount.HideControl(False)
            INDLciFeesExpensesAccount.HideControl(False)

            For i As Integer = billingConcept.BillingConceptAccount.Count - 1 To 0 Step -1
                billingConcept.BillingConceptAccount(i).MarkAsDeleted()
            Next
            fListBillingConceptAccount = Nothing
            INDGcBillingConceptAccount.DataSource = Nothing

            INDlyItemIncomeRecognitionPendingBillingMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemIncomeRecognitionPendingBillingMainAccount.AllowHide = False
        ElseIf AccountingType = 2 Then
            'Cuenta por tipo de unidad
            INDlcgAccounts.HideControl(False)

            INDLciEntityIncomeAccount.HideControl()
            INDSleEntityIncomeAccount.EditValue = Nothing
            INDSleEntityIncomeAccount.Properties.NullText = String.Empty

            INDLciIndividualIncomeAccount.HideControl()
            INDSleIndividualIncomeAccount.EditValue = Nothing
            INDSleIndividualIncomeAccount.Properties.NullText = String.Empty

            INDLciFeesExpensesAccount.HideControl()
            INDSleFeesExpensesAccount.EditValue = Nothing
            INDSleFeesExpensesAccount.Properties.NullText = String.Empty

            If billingConcept.BillingConceptAccount IsNot Nothing AndAlso billingConcept.BillingConceptAccount.Count = 0 Then
                INDGcBillingConceptAccount.DataSource = ListBillingConceptAccount
                ListBillingConceptAccount.ForEach(Sub(o)
                                                      billingConcept.BillingConceptAccount.Add(o)
                                                  End Sub)
            End If

            INDlyItemIncomeRecognitionPendingBillingMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemIncomeRecognitionPendingBillingMainAccount.AllowHide = True
        End If
    End Sub

    Private Sub INDSleCopayMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCopayMainAccount.QueryPopUp
        If INDSleCopayMainAccount.Properties.DataSource Is Nothing Then
            INDSleCopayMainAccount.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).AccountingService.ListAccountsByLevel(5, True, 0)
        End If
    End Sub

    Private Sub INDSleRecoveryFixedAmountMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleRecoveryFixedAmountMainAccount.QueryPopUp
        If INDSleRecoveryFixedAmountMainAccount.Properties.DataSource Is Nothing Then
            INDSleRecoveryFixedAmountMainAccount.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).AccountingService.ListAccountsByLevel(5, True, 0)
        End If
    End Sub
End Class