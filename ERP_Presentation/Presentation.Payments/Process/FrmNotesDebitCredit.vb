'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/03/2014
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Data.Async.Helpers
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Maintenance
Imports Presentation.Payments.MVP

#End Region

Public Class FrmNotesDebitCredit
    Implements INotesDebitCredit, ICustomizableForm

#Region "Constructor"

    Public ctrTmp As CtrDebitCredit
    Dim debit As Decimal
    Dim credit As Decimal

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ctrTmp = New CtrDebitCredit()
        ctrTmp.SetDebitAndCredit(AddressOf getDebit)
        ctrTmp.RefreshDebitCredit()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    Private Function getDebit() As Tuple(Of Decimal, Decimal)
        debit = 0
        credit = 0

        If ListAccountPayable IsNot Nothing AndAlso ListAccountPayable.Any() Then
            For Each item As AccountPayable In ListAccountPayable
                If Nature = 1 Then
                    debit = item.Adjustment + debit
                Else
                    credit = item.Adjustment + credit
                End If
            Next
        End If

        If ListAdvancePayments IsNot Nothing AndAlso ListAdvancePayments.Any() Then
            For Each item As AdvancePayments In ListAdvancePayments
                If Nature = 1 Then
                    debit = item.Adjustment + debit
                Else
                    credit = item.Adjustment + credit
                End If
            Next
        End If

        If ListAddConcept IsNot Nothing AndAlso ListAddConcept.Any() Then
            For Each item As PaymentsNoteDetails In ListAddConcept
                If item.Nature = 1 Then
                    debit = item.TotalConceptValue + debit
                Else
                    credit = item.TotalConceptValue + credit
                End If
            Next
        End If

        If paymentsNote IsNot Nothing AndAlso paymentsNote.AccountPayable IsNot Nothing Then
            debit = paymentsNote.AccountPayable.Value
            credit = debit
        End If

        Return New Tuple(Of Decimal, Decimal)(debit, credit)
    End Function

    ''' <summary>
    ''' Lista los conceptos de correción para notas de ajuste
    ''' </summary>
    Private _listConceptsAdjustment As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListConceptsAdjustment As List(Of Tuple(Of Integer, String))
        Get
            If _listConceptsAdjustment Is Nothing Then
                _listConceptsAdjustment = New List(Of Tuple(Of Integer, String))
                _listConceptsAdjustment.Add(New Tuple(Of Integer, String)(1, "Devolución parcial de los bienes y/o no aceptación parcial del servicio"))
                _listConceptsAdjustment.Add(New Tuple(Of Integer, String)(2, "Anulación del documento soporte"))
                _listConceptsAdjustment.Add(New Tuple(Of Integer, String)(3, "Rebaja o descuento parcial o total"))
                _listConceptsAdjustment.Add(New Tuple(Of Integer, String)(4, "Ajuste de precio"))
                _listConceptsAdjustment.Add(New Tuple(Of Integer, String)(5, "Otros"))
            End If
            Return _listConceptsAdjustment
        End Get
    End Property

#End Region

#Region "Constant"

    ''' <summary>
    ''' Constante que contiene el nombre del modulo
    ''' </summary>
    Private Const NAME_MODULE As String = "Payments"

    ''' <summary>
    ''' Constante que contiene el nombre del form en el que estoy
    ''' </summary>
    Private Const _form As String = "PaymentNote"

    ''' <summary>
    ''' Contiene el nombre de la entidad de CxP
    ''' </summary>
    Private Const C_accountPayable As String = "AccountPayable"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa a la entidad de parámetros de cxp
    ''' </summary>
    Public Property PaymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo Implements INotesDebitCredit.PaymentsSettingPaymentsXpo

    ''' <summary>
    ''' Listado complejo de cuentas por pagar del popupControl
    ''' </summary>
    ''' <remarks></remarks>
    Private ListAccountPayableGridPopupControl As List(Of AccountPayable)

    ''' <summary>
    ''' Listado de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private ListAccountPayable As List(Of AccountPayable)

    ''' <summary>
    ''' Listado de anticipos de proveedores
    ''' </summary>
    ''' <remarks></remarks>
    Private ListAdvancePayments As List(Of AdvancePayments)

    ''' <summary>
    ''' Listado de eliminados de conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Private listDeleteDetailConcept As List(Of PaymentsNoteDetails)

    ''' <summary>
    ''' Listado de eliminados de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private listDeleteAccountPayable As List(Of AccountPayable)

    ''' <summary>
    ''' Listado de eliminados de anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private listDeleteAdvancePayments As List(Of AdvancePayments)

    ''' <summary>
    ''' Variable que contiene la lista con la naturaleza de la cuenta
    ''' </summary>
    Private NatureType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado para agregar un concepto a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public ListAddConcept As List(Of PaymentsNoteDetails)

    ''' <summary>
    ''' Representa la entidad de detalle de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private accountPayableDetailConcept As AccountPayableDetailConcept

    ''' <summary>
    ''' Representa la entidad de detalle de notas debito/credito
    ''' </summary>
    ''' <remarks></remarks>
    Private paymentNoteDetails As PaymentsNoteDetails

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As PaymentsSecuence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' variable para instanciar el presentador
    ''' </summary>
    Private Presenter As PNotesDebitCredit

    ''' <summary>
    ''' Entidad de concepto de retencion
    ''' </summary>
    Private paymentsNote As PaymentNotes

    ''' <summary>
    ''' variable para registrar el bloqueo de los registros
    ''' </summary>
    Private record As BlockRecordPayments

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Variable para la propiedad de cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayable As AccountPayable

    ''' <summary>
    ''' Variable para la propiedad de anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private _advancePayments As AdvancePayments

    ''' <summary>
    ''' Id de la cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _id As Integer

    ''' <summary>
    ''' Variable para la propiedad de numero de factura
    ''' </summary>
    ''' <remarks></remarks>
    Private _billNumber As String

    ''' <summary>
    ''' Variable para la propiedad de listado de cuotas
    ''' </summary>
    ''' <remarks></remarks>
    Private _listAccountPayableShares As List(Of AccountPayableShares)

    ''' <summary>
    ''' Representa el id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _idSupplier As Integer

    ''' <summary>
    ''' Representa el id del tercero que viene asociado al proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _idThirdPartySupplier As Integer

    ''' <summary>
    ''' Nit y nombre del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private _descriptionThirdParty As String

    ''' <summary>
    ''' Variable para saber si se trata de una factura o de un anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private handlesBillsOrAdvance As Integer

    ''' <summary>
    ''' Variable para la propiedad de codigo del anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private _codeAdvance As String

    ''' <summary>
    ''' Variable para sumar debitos / creditos segun corresponda
    ''' </summary>
    ''' <remarks></remarks>
    Private valConcepts As Decimal

    ''' <summary>
    ''' Variable para sumar el valor del anticipo o de la factura segun corresponda
    ''' </summary>
    ''' <remarks></remarks>
    Private valBillOrAdvance As Decimal

    ''' <summary>
    ''' Variable para saber si guarda o actualiza
    ''' </summary>
    ''' <remarks></remarks>
    Private _banSaveModify As Boolean

    ''' <summary>
    ''' Variable que identifica a la cuenta contable de la factura o anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private _idMainAccountBillOrAdvance As Integer

    ''' <summary>
    ''' Variable que identifica al centro costo de la factura o anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private _idCostCenterBillOrAdvance As Integer?

    ''' <summary>
    ''' Variable que identifica al tercero de la factura o anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private _idThirdPartyBillOrAdvance As Integer

    ''' <summary>
    ''' Variable para saber si se guarda o se confirma
    ''' </summary>
    ''' <remarks></remarks>
    Private banConfirm As Boolean

    ''' <summary>
    ''' Nombre del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _nameSupplierDocumentIndexed As String = String.Empty

    ''' <summary>
    ''' Nit del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private _nitThitrdPartyDocumentIndexed As String = String.Empty

    ''' <summary>
    ''' Nombre del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private _nameThirdPartyDocumentIndexed As String = String.Empty

    ''' <summary>
    ''' Codigo del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _codeSupplierDocumentIndexed As String = String.Empty

    ''' <summary>
    ''' Variable que me permite saber en que momento realizo el cambio
    ''' en los controles de textEdit que representan a algun valor
    ''' </summary>
    ''' <remarks></remarks>
    Private controlerEditValueChanged As Boolean = True

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private varImp As Integer

    ''' <summary>
    ''' Obtiene la cuenta contable que tiene asociada la linea de distribucion
    ''' que se selecciono con el proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private MainAccountIdDistributionLine As Integer

    ''' <summary>
    ''' Formulario de Cuentas por Pagar
    ''' </summary>
    Private _frmAccountsPayable As FrmAccountsPayable

    ''' <summary>
    ''' variable que almacena el Id de la cuenta por pagar traid del formulario accountpayable
    ''' </summary>
    Private IdAccountPayable As Integer

    ''' <summary>
    ''' almacena el id del IdSupplierDistributionsLine
    ''' </summary>
    Private IdSupplierDistributionsLine As Integer

    ''' <summary>
    ''' bandera para saber si ya se ejecuto el clic de ver
    ''' </summary>
    Private ClickFlag As Boolean = True

#End Region

#Region "Properties"

    Public ReadOnly Property BudgetInterface As Boolean
        Get
            Return If(PaymentsSettingPaymentsXpo Is Nothing, False, PaymentsSettingPaymentsXpo.BudgetInterface)
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de centros de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements INotesDebitCredit.CostCenterXpo
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de cuentas por cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements INotesDebitCredit.AccountPayableXpo
        Get
            Return CType(INDSleCxP.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCxP.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCostCenter As Integer? Implements INotesDebitCredit.IdCostCenter
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CodeAdvancePayments As String Implements INotesDebitCredit.CodeAdvancePayments
        Get
            Return _codeAdvance
        End Get
        Set(value As String)
            _codeAdvance = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el ajuste del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AdjustmentAdvance As Decimal Implements INotesDebitCredit.AdjustmentAdvance
        Get
            Return INDtxtAdjustmentAdvance.EditValue
        End Get
        Set(value As Decimal)
            INDtxtAdjustmentAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el saldo del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BalanceAdvance As Decimal Implements INotesDebitCredit.BalanceAdvance
        Get
            Return INDtxtBalanceAdvance.EditValue
        End Get
        Set(value As Decimal)
            INDtxtBalanceAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDateAdvance As Date Implements INotesDebitCredit.DocumentDateAdvance
        Get
            Return INDdteDateAdvance.EditValue
        End Get
        Set(value As Date)
            INDdteDateAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAdvancePayments As Integer Implements INotesDebitCredit.IdAdvancePayments
        Get
            Return INDsleAdvance.EditValue
        End Get
        Set(value As Integer)
            INDsleAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableceel porcentaje del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PercentageAdvance As Decimal Implements INotesDebitCredit.PercentageAdvance
        Get
            Return INDsePercentageAdvance.EditValue
        End Get
        Set(value As Decimal)
            INDsePercentageAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValueAdvance As Decimal Implements INotesDebitCredit.ValueAdvance
        Get
            Return INDtxtValueAdvance.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValueAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la entidad de anticipos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property advancePayments As AdvancePayments Implements INotesDebitCredit.advancePayments
        Get
            Return _advancePayments
        End Get
        Set(value As AdvancePayments)
            _advancePayments = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource del control de anticipos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AdvancePaymentsXpo As LinqInstantFeedbackSource Implements INotesDebitCredit.AdvancePaymentsXpo
        Get
            Return CType(INDsleAdvance.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleAdvance.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Listado de proveedores con sus lineas de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SuppliersDistributionLinesXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements INotesDebitCredit.SuppliersDistributionLinesXpo
        Get
            Return CType(INDsleProvider.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleProvider.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el numero de factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillNumber As String Implements INotesDebitCredit.BillNumber
        Get
            Return _billNumber
        End Get
        Set(value As String)
            _billNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Id As Integer Implements INotesDebitCredit.Id
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la entidad de cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property accountPayable As AccountPayable Implements INotesDebitCredit.accountPayable
        Get
            Return _accountPayable
        End Get
        Set(value As AccountPayable)
            _accountPayable = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableDatasource As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements INotesDebitCredit.AccountPayableDatasource
        Get
            Return CType(INDsleBills.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDsleBills.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements INotesDebitCredit.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As PaymentsSecuence Implements INotesDebitCredit.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As PaymentsSecuence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PaymentsSecuenceDetail In Me._sequence.PaymentsSecuenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo de nota debito/credito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements INotesDebitCredit.Code
        Get
            If (INDbtnConsecutive.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnConsecutive.Text
            End If
        End Get
        Set(value As String)
            INDbtnConsecutive.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el estado del registro en la tabla
    ''' </summary>
    ''' <value>Estado del registro en la tabla</value>
    ''' <returns>El estado del registro en la tabla</returns>
    Public Property Status As Boolean Implements INotesDebitCredit.Status
        Get
            Return BarraBotones.StatusRecord
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
    ''' Obtiene o establece el id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdSupplier As Integer? Implements INotesDebitCredit.IdSupplier
        Get
            Return INDsleProvider.EditValue
        End Get
        Set(value As Integer?)
            INDsleProvider.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de proveedores
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements INotesDebitCredit.SupplierXpo
        Get
            Return CType(INDsleProvider.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleProvider.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece a que va ser aplicada la nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Apply As Byte? Implements INotesDebitCredit.Apply
        Get
            Return INDGleNoteType.EditValue
        End Get
        Set(value As Byte?)
            INDGleNoteType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los comentarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Comments As String Implements INotesDebitCredit.Comments
        Get
            Return INDmemoComments.Text
        End Get
        Set(value As String)
            INDmemoComments.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DateDocument As Date? Implements INotesDebitCredit.DateDocument
        Get
            Return INDdteDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la naturaleza de la nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Nature As Integer? Implements INotesDebitCredit.Nature
        Get
            Return If(INDgleNature.EditValue Is Nothing, Nothing, CInt(INDgleNature.EditValue))
        End Get
        Set(value As Integer?)
            INDgleNature.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el ajuste
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Adjustment As Decimal Implements INotesDebitCredit.Adjustment
        Get
            Return INDtxtAdjustment.EditValue
        End Get
        Set(value As Decimal)
            INDtxtAdjustment.EditValue = value
        End Set
    End Property

    Public Property ConceptAdjustmentId As Integer? Implements INotesDebitCredit.ConceptAdjusment
        Get
            Return INDSleConceptAdjustment.EditValue
        End Get
        Set(value As Integer?)
            INDSleConceptAdjustment.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el saldo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Balance As Decimal Implements INotesDebitCredit.Balance
        Get
            Return INDtxtBalance.EditValue
        End Get
        Set(value As Decimal)
            INDtxtBalance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el IVA Deducible
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeductibleIva As Boolean?

    ''' <summary>
    ''' Obtiene o establece el parametro de IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TaxRegistration As Integer?

    ''' <summary>
    ''' Obtiene o establece la fecha de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillDate As Date Implements INotesDebitCredit.BillDate
        Get
            Return INDdteBillDate.EditValue
        End Get
        Set(value As Date)
            INDdteBillDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de vencimiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExpiredDate As Date Implements INotesDebitCredit.ExpiredDate
        Get
            Return INDdteExpiredDate.EditValue
        End Get
        Set(value As Date)
            INDdteExpiredDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el porcentaje
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Percentage As Decimal Implements INotesDebitCredit.Percentage
        Get
            Return INDsePercentage.EditValue
        End Get
        Set(value As Decimal)
            INDsePercentage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value As Decimal Implements INotesDebitCredit.Value
        Get
            Return INDtxtValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las cuotas de la cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListAccountPayableShares As List(Of AccountPayableShares) Implements INotesDebitCredit.ListAccountPayableShares
        Get
            Return _listAccountPayableShares
        End Get
        Set(value As List(Of AccountPayableShares))
            _listAccountPayableShares = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para activar o desactivar bandera para permitir que facturas con notas se listen en pronto pago
    ''' </summary>
    ''' <returns></returns>
    Public Property AllowDiscountPromptPayment As Boolean Implements INotesDebitCredit.AllowDiscountPromptPayment
        Get
            Return IIf(INDrgAllowPromptPayment.EditValue Is Nothing, False, INDrgAllowPromptPayment.EditValue)
        End Get
        Set(value As Boolean)
            INDrgAllowPromptPayment.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' determina si el descuento afecta la base para el descuento de pronto pago 
    ''' </summary>
    ''' <returns></returns>
    Public Property AffectBaseToDiscount As Boolean Implements INotesDebitCredit.AffectBaseToDiscount
        Get
            Return IIf(INDrgAffectBaseToDiscount.EditValue Is Nothing, False, INDrgAffectBaseToDiscount.EditValue)
        End Get
        Set(value As Boolean)
            INDrgAffectBaseToDiscount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' porpiedad del Id de la moneda
    ''' </summary>
    ''' <param name="CurrencyAbbreviation"></param>
    ''' <returns></returns>
    Property CurrencyId(Optional CurrencyAbbreviation As String = Nothing) As Integer? Implements INotesDebitCredit.CurrencyId
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDsleCurrency.EditValue = value
            INDsleCurrency.Properties.NullText = CurrencyAbbreviation
            SetCurrencyUI(CurrencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' Datasource del combo de la moneda
    ''' </summary>
    ''' <returns></returns>
    Property DataSourceCurrency As XPInstantFeedbackSource Implements INotesDebitCredit.DataSourceCurrency
        Get
            Return TryCast(INDsleCurrency.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Moneda seleccionada carga registro cuando se ha desplegado el combo
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencySelected As Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo
        Get
            Return TryCast(TryCast(INDGvCurrencyAdvance.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo)
        End Get
    End Property

    ''' <summary>
    ''' propiedad para tomar la abreviacion de la moneda y guardarla temp para cuando se necesite editar
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencyAbbreviation As String
        Get
            Return INDsleCurrency.Text
        End Get
    End Property

    ''' <summary>
    ''' Moneda seleccionada carga registro cuando se ha desplegado el combo
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CxPSelected As AccountPayableXpo
        Get
            Return TryCast(TryCast(INDGvCxP.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, AccountPayableXpo)
        End Get
    End Property
#End Region

#Region "DataSource"

    ''' <summary>
    ''' listado de los tipos de notas
    ''' </summary>
    ''' <remarks></remarks>
    Private _listNoteType As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' obtiene la lista los tipos de notas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ListNoteType As List(Of Tuple(Of Byte, String))
        Get
            If _listNoteType Is Nothing Then
                _listNoteType = New List(Of Tuple(Of Byte, String))
                _listNoteType.Add(New Tuple(Of Byte, String)(0, "Factura"))
                _listNoteType.Add(New Tuple(Of Byte, String)(1, "Anticipo"))
                _listNoteType.Add(New Tuple(Of Byte, String)(2, "Reversión CXP"))
                _listNoteType.Add(New Tuple(Of Byte, String)(3, "Reversión Amortización"))
            End If
            Return _listNoteType
        End Get
    End Property

#End Region

#Region "Methods and functions"

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetDate()
        Dim dateServerVariable As DateTime
        Using model As New MAccountPayable(CStr(Tag))
            dateServerVariable = Await model.GetServerDate()
            INDdteDate.EditValue = dateServerVariable
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que reasigna el valor de las cuotas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValueProportional(ByVal acc As AccountPayable, ByVal nature As Integer)
        Dim listShare As List(Of AccountPayableShares) = acc.AccountPayableShares.ToList
        Dim adjustement As Decimal = acc.Adjustment
        If (nature = 1 OrElse nature = 2) AndAlso acc.Adjustment <= acc.Balance Then
            For Each item As AccountPayableShares In listShare
                If item.Balance <= adjustement Then
                    adjustement -= item.Balance
                    item.ValueNoteShare = item.Balance
                Else
                    item.ValueNoteShare = adjustement
                    adjustement = 0
                End If
            Next
        ElseIf nature = 2 AndAlso acc.Adjustment > acc.Balance Then
            Dim valP As Decimal = PaymentServices.CreateValueProportional(acc.Adjustment, acc.AccountPayableShares.Count, False)
            For Each item As AccountPayableShares In listShare
                item.ValueNoteShare = valP
                accountPayable.AccountPayableShares.Add(item)
            Next
        End If
        INDgcBill.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Metodo que carga los controles correspondientes de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub LoadInformationAccountPayable()
        Dim accountPayableXpo = DirectCast(DirectCast(INDsleViewBill.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableXpo)
        If accountPayableXpo?.Id > 0 Then

            If If(accountPayableXpo?.CurrencyId Is Nothing OrElse accountPayableXpo?.CurrencyId = 0 _
                , Me.indigo.OfficialCurrencyId, accountPayableXpo?.CurrencyId) <> Me.CurrencyId Then
                INDsleBills.EditValue = Nothing
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrencyDocumentDifferent")
                Exit Sub
            End If

            Using model As New MAccountPayable(CStr(Tag))
                Dim electronicSupportDocument = model.GetElectronicSupportDocumentByDocumentOrigin(accountPayableXpo.Id, C_accountPayable)
                If Nature = 1 AndAlso electronicSupportDocument?.Any() Then
                    INDLyIConceptAdjustment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If
            End Using

            With accountPayableXpo
                Id = .Id
                BillNumber = .BillNumber
                BillDate = .BillDate
                ExpiredDate = .ExpirationDate
                Value = .Value
                Balance = .Balance
                DeductibleIva = .DeductibleIva
                TaxRegistration = .TaxRegistration

                _idMainAccountBillOrAdvance = .IdAccount.Id
                If .IdCostCenter = 0 Then
                    _idCostCenterBillOrAdvance = Nothing
                Else
                    _idCostCenterBillOrAdvance = .IdCostCenter
                End If
                _idThirdPartyBillOrAdvance = .IdThirdParty.Id
            End With

            Await ValidateDeferredCausation(accountPayableXpo.Id)
            ActionsOnControlsPoppup = True
            INDtxtAdjustment.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Método que convierte valor a porcentaje y lo asigna al campo porcentaje de la rejilla
    ''' </summary>
    Private Function ConvertValueToPercent(ByVal value As Decimal, ByVal balance As Decimal) As Decimal
        Dim val As Decimal = 0
        Dim valToCompare As Decimal = 0
        If value <= 0 Then
            Return val
        End If

        Select Case Apply
            Case 1
                If value > balance AndAlso Nature = 2 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El valor del ajuste no puede ser mayor al saldo."
                    Return val
                End If

                If (value + balance) > ValueAdvance AndAlso Nature = 1 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El ajuste más el saldo no puede ser mayor al valor inicial"
                    Return val
                End If

                valToCompare = If(Nature = 2, balance, (ValueAdvance - balance))
            Case Else
                If value > balance AndAlso Nature = 1 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El valor del ajuste no puede ser mayor al saldo."
                    Return val
                End If
                valToCompare = balance
        End Select

        If valToCompare = 0 Then
            Return val
        End If

        val = TreasuryStaticServices.ConvertInvoiceValueToPercent(value, valToCompare)
        Return val
    End Function

    ''' <summary>
    ''' Método que convierte de porcentaje a valor en pesos
    ''' </summary>
    Private Function ConvertPercentToValue(ByVal percent As Decimal, ByVal balance As Decimal) As Decimal
        Dim val As Decimal = 0
        Dim valToCompare As Decimal = 0
        If percent <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El porcentaje del ajuste no puede ser negativo."
            Return val
        End If

        If percent > 100 Then
            Mensaje(EeventViewerImages.Advertencia) = "El porcentaje del ajuste no puede ser mayor al 100%."
            Return val
        End If

        Select Case Apply
            Case 1
                valToCompare = If(Nature = 2, balance, (ValueAdvance - balance))
                val = Math.Round(TreasuryStaticServices.ConvertInvoicePercentToValue(percent, valToCompare), 2, MidpointRounding.AwayFromZero)

                If val > balance AndAlso Nature = 2 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El valor del ajuste no puede ser mayor al saldo."
                    Return 0
                End If

                If (Value + balance) > ValueAdvance AndAlso Nature = 1 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El ajuste más el saldo no puede ser mayor al valor inicial"
                    Return 0
                End If

            Case Else
                val = Math.Round(TreasuryStaticServices.ConvertInvoicePercentToValue(percent, balance), 2, MidpointRounding.AwayFromZero)
                If val > balance AndAlso Nature = 1 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El valor del ajuste no puede ser mayor al saldo."
                    Return 0
                End If

        End Select

        Return val
    End Function

    ''' <summary>
    ''' Edita un concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditConcept(PaymentsNoteDetailsTmp As PaymentsNoteDetails)
        CtrConcepts1.idThird = _idThirdPartySupplier
        Dim isConfirmed As Boolean = (paymentsNote IsNot Nothing AndAlso Not {0, 1}.Contains(paymentsNote.Status))
        ' Habilitar temporalmente el popup si la nota está confirmada/anulada
        If isConfirmed Then
            INDpceConcept.Properties.ReadOnly = False
        End If

        CtrConcepts1.InitializeControler(Nothing,
                                        PaymentsNoteDetailsTmp,
                                        _form,
                                        Nothing,
                                        Nothing,
                                        ListAccountPayable:=Me.ListAccountPayable,
                                        CurrencyAbbreviation:=Me.CurrencyAbbreviation,
                                        taxRegistration:=TaxRegistration,
                                        ConfirmStatus:=isConfirmed)
        INDpceConcept.ShowPopup()
    End Sub

    ''' <summary>
    ''' Metodo para eliminar el concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteConcept(PaymentsNoteDetailsTmp As PaymentsNoteDetails)
        If PaymentsNoteDetailsTmp.Id <> 0 Then
            If listDeleteDetailConcept Is Nothing Then
                listDeleteDetailConcept = New List(Of PaymentsNoteDetails)
            End If

            PaymentsNoteDetailsTmp.MarkAsDeleted()
            listDeleteDetailConcept.Add(PaymentsNoteDetailsTmp)
        End If

        ListAddConcept.Remove(PaymentsNoteDetailsTmp)
        RefreshGridConcept()
        CtrConcepts1.CleanControls()
    End Sub

    ''' <summary>
    ''' Elimina una factura de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteBill()

        Dim acc As AccountPayable = CType(viewBill.GetFocusedRow, AccountPayable)
        If acc.IdPaymentNotesAccountPayableAdvance > 0 Then
            If listDeleteAccountPayable Is Nothing Then
                listDeleteAccountPayable = New List(Of AccountPayable)
            End If

            acc.HandlesAddModifyDelete = 3
            listDeleteAccountPayable.Add(acc)
        End If

        ListAccountPayable.Remove(acc)
        INDgcBill.DataSource = Nothing
        INDgcBill.DataSource = ListAccountPayable
        Me.ValidateCurrency()

        If Not ListAccountPayable?.Any() Then
            ActionsOnControlsConsultBillOrAdvance = False
        End If

        ctrTmp.RefreshDebitCredit()
    End Sub

    ''' <summary>
    ''' Elimina un anticipo de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteAdvance()

        Dim ap As AdvancePayments = CType(viewGridAdvance.GetFocusedRow, AdvancePayments)
        If ap.IdPaymentNotesAccountPayableAdvance > 0 Then
            If listDeleteAdvancePayments Is Nothing Then
                listDeleteAdvancePayments = New List(Of AdvancePayments)
            End If

            ap.HandlesAddModifyDelete = 3
            ap.MarkAsDeleted()
            listDeleteAdvancePayments.Add(ap)
        End If

        ListAdvancePayments.Remove(ap)
        INDgcAdvance.DataSource = Nothing
        INDgcAdvance.DataSource = ListAdvancePayments
        Me.ValidateCurrency()

        If Not ListAdvancePayments?.Any() Then
            ActionsOnControlsConsultBillOrAdvance = False
        End If
        ctrTmp.RefreshDebitCredit()
    End Sub

    ''' <summary>
    ''' Llena el control con el listado de la naturaleza de la cuenta
    ''' </summary>
    Private Sub CreateNature()
        NatureType = New List(Of Tuple(Of Integer, String))
        NatureType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("AccountNatureDebit")))
        NatureType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("AccountNatureCredit")))
        RepositoryItemGridLookUpEditNature.DataSource = NatureType.ToList()
    End Sub

    ''' <summary>
    ''' Metodo para cargar los combos de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadRepositorySearch()
        Dim model As New MBusqueda
        Dim filter() As Object = {5, True}
        RepositoryItemSearchLookUpEditMainLedger.DataSource = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        RepositoryItemSearchLookUpEditAccount.DataSource = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        RepositoryItemSearchLookUpEditCostCenter.DataSource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        RepositoryItemSearchLookUpEditConceptPayments.DataSource = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListConceptsNotes)
        CreateNature()
        INDGleNoteType.Properties.DataSource = ListNoteType
    End Sub

    ''' <summary>
    ''' Metodo para abrir el form de concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddConcept(accDetail As PaymentsNoteDetails)

        If accDetail.ChangeTracker.State = ObjectState.Added Then
            If ListAddConcept Is Nothing Then
                ListAddConcept = New List(Of PaymentsNoteDetails)
            End If

            If Not CtrConcepts1.Entity Then
                ListAddConcept.Add(accDetail)
                INDpceConcept.ShowPopup()
                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AddConceptSatisfactory", NAME_MODULE)
                CtrConcepts1.INDsleConceptNote.Focus()
            End If
        End If

        RefreshGridConcept()
    End Sub

    ''' <summary>
    ''' Metodo para refrescar la rejilla de conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshGridConcept()
        INDgcConcepts.DataSource = Nothing
        INDgcConcepts.DataSource = ListAddConcept
        CtrConcepts1.PaymentNoteDetail = Nothing
        ctrTmp.RefreshDebitCredit()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.paymentsNote IsNot Nothing AndAlso Me.paymentsNote.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Code = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Code = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements INotesDebitCredit.ActionsOnControls
        Set(value As Boolean)
            INDlyNotes.BeginUpdate()

            INDbtnConsecutive.Enabled = Not value
            INDdteDate.Enabled = value
            INDGleNoteType.Enabled = value
            INDgleNature.Enabled = value
            INDsleProvider.Enabled = value
            INDmemoComments.Enabled = value

            INDpceBill.Enabled = value
            INDEsbBill.Enabled = value
            INDgcBill.Enabled = value

            INDpceConcept.Enabled = value
            INDgcConcepts.Enabled = value

            INDrgAllowPromptPayment.Enabled = value
            INDrgAffectBaseToDiscount.Enabled = value
            INDsleCurrency.Enabled = value
            Me.BarraBotones.StatusRecordVisible = value

            INDlyNotes.EndUpdate()

            If value Then
                INDdteDate.Focus()
            Else
                INDbtnConsecutive.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

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
    ''' funcion que retorna el documento a indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.paymentsNote.Code, Me._codeSupplierDocumentIndexed, Me._nameSupplierDocumentIndexed, Me._nitThitrdPartyDocumentIndexed, Me._nameThirdPartyDocumentIndexed),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.paymentsNote.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.paymentsNote.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.paymentsNote.Code, Me._codeSupplierDocumentIndexed, Me._nameSupplierDocumentIndexed, Me._nitThitrdPartyDocumentIndexed, Me._nameThirdPartyDocumentIndexed)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.paymentsNote.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Método para limpiar los controles
    ''' </summary>
    Private Sub CleanControls()
        INDlyNotes.BeginUpdate()

        Me.CurrencyId(Me.indigo.CurrencyISO4217) = Me.indigo.OfficialCurrencyId
        ReadOnlyControls(False)
        INDgleNature.Properties.ReadOnly = False
        ActionsOnControls = False
        Code = String.Empty
        _idThirdPartySupplier = 0
        Apply = 0
        IdAccountPayable = 0
        Nature = Nothing
        IdSupplier = Nothing
        INDsleProvider.Properties.NullText = String.Empty
        INDsleCostCenter.Properties.NullText = String.Empty
        DateDocument = Nothing
        Comments = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        ListAccountPayable = Nothing
        INDgcBill.DataSource = Nothing
        ListAdvancePayments = Nothing
        INDgcAdvance.DataSource = Nothing
        ListAddConcept = Nothing
        INDgcConcepts.DataSource = Nothing
        paymentsNote = Nothing
        listDeleteAccountPayable = Nothing
        listDeleteAdvancePayments = Nothing
        listDeleteDetailConcept = Nothing
        Me.AffectBaseToDiscount = False
        Me.AllowDiscountPromptPayment = False
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        IdCostCenter = Nothing
        CleanControlsPopup()
        CleanControlsPopupAdvance()
        CtrConcepts1.CleanControls()
        ctrTmp.RefreshDebitCredit()
        BarraBotones.StatusRecord = "1"
        INDLycView.HideLayout()
        INDLcgDiscountPromptPayment.HideControl()
        INDlyIAllowPromptPayment.HideLayout()
        INDSleCxP.EditValue = Nothing
        INDSleCxP.Properties.DataSource = Nothing
        INDSleCxP.Properties.NullText = Nothing
        Me.ClickFlag = True
        IdSupplierDistributionsLine = Nothing
        Me._frmAccountsPayable = Nothing
        Me.TaxRegistration = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyNotes.EndUpdate()
    End Sub

    ''' <summary>
    ''' desbloquea el registro
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequensePayments(Me.Tag)
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo que llena la entidad con los valores de los controles
    ''' </summary>
    Private Sub AssigningValues()
        With paymentsNote
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .NoteDate = DateDocument
            .IdSupplier = _idSupplier

            If Apply <> 2 Then
                .IdSupplierDistributionLines = IdSupplier
            Else
                .IdSupplierDistributionLines = IdSupplierDistributionsLine
            End If

            If INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .IdCostCenter = IdCostCenter
            End If

            .Comment = Comments
            .Nature = Nature
            .Reinstatement = False
            .IdVoucherTransaction = Nothing
            .IndicatesBillAdvance = Apply
            .CancelCheck = Nothing
            .BudgetInterface = BudgetInterface
            .IdOperatingUnit = BarraBotones.OperatingUnit.Id
            .Status = 1
            .AllowDiscountPromptPayment = Me.AllowDiscountPromptPayment
            .AffectBaseToDiscount = Me.AffectBaseToDiscount
            .CurrencyId = Me.CurrencyId

            If Me.indigo.LanguageCulture = "es-CO" Then
                .IsNative = True
            End If

            'Le creo los detalles a la nota
            If IdAccountPayable = 0 Then
                For Each itemConcept As PaymentsNoteDetails In ListAddConcept
                    .PaymentsNoteDetails.Add(itemConcept)
                Next
            Else
                .IdAccountPayable = IdAccountPayable
            End If

            'Vuelvo e ingreso los items que fueron eliminados
            If listDeleteDetailConcept IsNot Nothing Then
                For Each item As PaymentsNoteDetails In listDeleteDetailConcept
                    .PaymentsNoteDetails.Add(item)
                Next
            End If

            If listDeleteAccountPayable IsNot Nothing Then
                For Each item As AccountPayable In listDeleteAccountPayable
                    ListAccountPayable.Add(item)
                Next
            End If

            If listDeleteAdvancePayments IsNot Nothing Then
                For Each item As AdvancePayments In listDeleteAdvancePayments
                    ListAdvancePayments.Add(item)
                Next
            End If
        End With
    End Sub

    ''' <summary>
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateFields() As Boolean
        If INDlygBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ListAccountPayable Is Nothing OrElse ListAccountPayable.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontListAccountPayable", NAME_MODULE)
                Return False
            End If
        ElseIf INDlygAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ListAdvancePayments Is Nothing OrElse ListAdvancePayments.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontListAdvanced", NAME_MODULE)
                Return False
            End If
        End If

        If Apply <> 2 Then
            If ListAddConcept Is Nothing OrElse ListAddConcept.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontListConcept", NAME_MODULE)
                Return False
            End If
        Else
            If paymentsNote.AccountPayable IsNot Nothing AndAlso paymentsNote.AccountPayable.DocumentDate > INDdteDate.DateTime Then
                Mensaje(EeventViewerImages.Advertencia) = ("La fecha de la Nota no puede ser antes de la fecha de la cuenta")
                Return False
            End If
            If ClickFlag Then
                Mensaje(EeventViewerImages.Advertencia) = ("Antes de guardar y/o Confirmar vea los detalles de la cuenta Primero ")
                Return False
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para
    ''' crear un nuevo concepto de retencion
    ''' </summary>
    Private Async Function NewNotesDebitCredit() As Task
        paymentsNote = New PaymentNotes()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.PaymentsSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.PaymentsSecuenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If

        GetDate()
    End Function

    ''' <summary>
    ''' Método para agregar la factura a la rejilla.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddBill()
        If ValidateControlsPopup() Then
            If ListAccountPayable Is Nothing Then
                ListAccountPayable = New List(Of AccountPayable)
            Else
                Dim ban As Integer = ListAccountPayable.FindAll(Function(item) item.Id = Id).Cast(Of AccountPayable).ToList().Count
                If ban > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("BillExist", NAME_MODULE)
                    INDsleBills.Focus()
                    Exit Sub
                End If

                ''valida que la nueva factura agregada no tenga o maneje un iva diferente al de la factura previa
                Dim validationIva As Integer = ListAccountPayable.FindAll(Function(item) item.TaxRegistration <> TaxRegistration).Cast(Of AccountPayable).ToList().Count
                If validationIva > 0 Then
                    TaxRegistration = ListAccountPayable?.FirstOrDefault?.TaxRegistration
                    Mensaje(EeventViewerImages.Advertencia) = "La factura que está intentado añadir maneja un IVA diferente a la factura previa añadida. Por favor, selecciona otra."
                    CleanControlsPopup()
                    INDsleBills.Focus()
                    Exit Sub
                End If
            End If

            CreateAccountPayable()
            ConsultAccountPayableShares()
            GetObligationDetailsByAccountPayable(accountPayable)
            ListAccountPayable.Add(accountPayable)
            ValidateCurrency()
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("BillAgregateSatisfactory", NAME_MODULE)
            INDgcBill.DataSource = Nothing
            INDgcBill.DataSource = ListAccountPayable
            CleanControlsPopup()
            ActionsOnControlsConsultBillOrAdvance = True
            ctrTmp.RefreshDebitCredit()
        End If
    End Sub

    ''' <summary>
    ''' Consulta las cuotas de la cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConsultAccountPayableShares()
        Using model As New MAccountPayable("")
            Dim listShares As List(Of AccountPayableShares) = model.GetAccountPayableSharesByIdAccountPayable(accountPayable.Id)

            If listShares IsNot Nothing Then
                For Each itemShare As AccountPayableShares In listShares
                    accountPayable.AccountPayableShares.Add(itemShare)
                Next
            End If

            ValueProportional(accountPayable, Nature)
        End Using
    End Sub

    ''' <summary>
    ''' Consulta las obligaciones presupuestales asociadas a una cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetObligationDetailsByAccountPayable(accountPayable As AccountPayable)
        If accountPayable.AccountPayableCommitments Is Nothing OrElse Not accountPayable.AccountPayableCommitments.Any() Then
            Using model As New MNotesDebitCredit(Me.Tag)
                Dim collectionAccountPayableCommitment = model.GetObligationDetails(accountPayable.Id, Nature)
                If collectionAccountPayableCommitment IsNot Nothing Then
                    For Each detail In collectionAccountPayableCommitment
                        accountPayable.AccountPayableCommitments.Add(New AccountPayableCommitments With {
                        .Id = detail.Id,
                        .CommitmentDetailId = detail.CommitmentDetailId,
                        .CommitmentCode = detail.CommitmentCode,
                        .CommitmentDocument = detail.CommitmentDocument,
                        .CategoryCodeName = detail.CategoryCodeName,
                        .FinancialSourceCodeName = detail.FinancialSourceCodeName,
                        .RevenueTypeCodeName = detail.RevenueTypeCodeName,
                        .Balance = detail.Balance,
                        .Value = 0
                        })
                    Next
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo para crear la entidad de cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateAccountPayable()
        accountPayable = New AccountPayable
        With accountPayable
            .Id = Id
            .BillNumber = BillNumber
            .BillDate = BillDate
            .ExpirationDate = ExpiredDate
            .Value = Value
            .Balance = Balance
            .Adjustment = Adjustment
            .ConceptAdjustmentId = ConceptAdjustmentId
            .Percentage = Percentage
            .HandlesAddModifyDelete = 1
            .DeductibleIva = DeductibleIva
            .TaxRegistration = TaxRegistration
            .IdSupplier = _idSupplier
            .IdAccount = _idMainAccountBillOrAdvance

            If _idCostCenterBillOrAdvance = 0 Then
                .IdCostCenter = Nothing
            Else
                .IdCostCenter = _idCostCenterBillOrAdvance
            End If

            .IdThirdParty = _idThirdPartyBillOrAdvance
        End With
    End Sub

    ''' <summary>
    ''' Valida los controles que tiene el popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup()
        If Adjustment = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Ingrese el ajuste."
            INDtxtAdjustment.Focus()
            Return False
        End If

        If Adjustment > Balance AndAlso Nature = 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor del ajuste no puede ser mayor al saldo."
            INDtxtAdjustment.Focus()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControlsPoppup As Boolean
        Set(value As Boolean)
            INDtxtAdjustment.Enabled = value
            INDsePercentage.Enabled = value
            INDbtnAddBill.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        INDsleBills.EditValue = Nothing
        BillDate = Nothing
        ExpiredDate = Nothing
        Value = 0
        Balance = 0
        Adjustment = 0
        Percentage = 0
        INDsleBills.Focus()
        ActionsOnControlsPoppup = False
        INDSleConceptAdjustment.EditValue = Nothing
        INDLyIConceptAdjustment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    ''' <summary>
    ''' Metodo que agrega un nuevo anticipo a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddAdvance()
        If ValidateControlsPopupAdvance() Then
            If ListAdvancePayments Is Nothing Then
                ListAdvancePayments = New List(Of AdvancePayments)
            Else
                Dim ban As Integer = ListAdvancePayments.FindAll(Function(item) item.Id = Id).Cast(Of AdvancePayments).ToList().Count
                If ban > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AdvanceExist", NAME_MODULE)
                    INDsleAdvance.Focus()
                    Exit Sub
                End If
            End If

            CreateAdvancePayments()
            ListAdvancePayments.Add(advancePayments)
            ValidateCurrency()
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AdvanceAgregateSatisfactory", NAME_MODULE)
            INDgcAdvance.DataSource = Nothing
            INDgcAdvance.DataSource = ListAdvancePayments
            CleanControlsPopupAdvance()
            ActionsOnControlsConsultBillOrAdvance = True
            ctrTmp.RefreshDebitCredit()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup de anticipos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopupAdvance()
        If AdjustmentAdvance = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Ingrese el ajuste."
            INDtxtAdjustmentAdvance.Focus()
            Return False
        End If

        'Validar que el Ajuste no supere al Saldo cuando es Credito
        If AdjustmentAdvance > BalanceAdvance AndAlso Nature = 2 Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor del ajuste no puede ser mayor al saldo."
            INDtxtAdjustmentAdvance.Focus()
            Return False
        End If

        'Validar que el valor del (ajuste con el saldo) no sea superior al saldo inicial cuando es Debito para Anticipo
        If (AdjustmentAdvance + BalanceAdvance) > ValueAdvance AndAlso Nature = 1 AndAlso Apply = 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "El Ajuste más el Saldo no puede ser mayor al Valor."
            INDtxtAdjustmentAdvance.Focus()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Metodo que crea un nuevo anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateAdvancePayments()
        advancePayments = New AdvancePayments
        With advancePayments
            .Id = Id
            .Code = CodeAdvancePayments
            .DocumentDate = DocumentDateAdvance
            .Value = ValueAdvance
            .Balance = BalanceAdvance
            .Adjustment = AdjustmentAdvance
            .Percentage = PercentageAdvance
            .HandlesAddModifyDelete = 1

            .IdSupplier = _idSupplier
            .IdAccount = _idMainAccountBillOrAdvance
            If _idCostCenterBillOrAdvance = 0 Then
                .IdCostCenter = Nothing
            Else
                .IdCostCenter = _idCostCenterBillOrAdvance
            End If

            .IdThirdParty = _idThirdPartyBillOrAdvance
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopupAdvance()
        INDsleAdvance.EditValue = Nothing
        DocumentDateAdvance = Nothing
        ValueAdvance = 0
        BalanceAdvance = 0
        AdjustmentAdvance = 0
        PercentageAdvance = 0
        INDsleAdvance.Focus()
        ActionsOnControlsPoppupAdvance = False
    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControlsPoppupAdvance As Boolean
        Set(value As Boolean)
            INDtxtAdjustmentAdvance.Enabled = value
            INDsePercentageAdvance.Enabled = value
            INDbtnAddAdvance.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControlsConsultBillOrAdvance As Boolean
        Set(value As Boolean)
            INDGleNoteType.Properties.ReadOnly = value
            INDgleNature.Properties.ReadOnly = value
            INDsleProvider.Properties.ReadOnly = value
        End Set
    End Property

    ''' <summary>
    ''' Metodo que carga los controles correspondientes de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadInformationAdvance()
        Dim advancePaymentsXpo = DirectCast(DirectCast(viewAdvance.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, AdvancePaymentsXpo)

        If If(advancePaymentsXpo?.CurrencyId Is Nothing OrElse advancePaymentsXpo?.CurrencyId = 0 _
            , Me.indigo.OfficialCurrencyId, advancePaymentsXpo?.CurrencyId) <> Me.CurrencyId Then
            Me.IdAdvancePayments = Nothing
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrencyDocumentDifferent")
            Exit Sub
        End If

        With advancePaymentsXpo
            Id = .Id
            CodeAdvancePayments = .Code
            DocumentDateAdvance = .DocumentDate
            ValueAdvance = .Value
            BalanceAdvance = .Balance

            _idSupplier = .IdSupplier.Id
            _idMainAccountBillOrAdvance = .IdAccount.Id

            If .IdCostCenter = 0 Then
                _idCostCenterBillOrAdvance = Nothing
            Else
                _idCostCenterBillOrAdvance = .IdCostCenter
            End If

            _idThirdPartyBillOrAdvance = .IdSupplier.IdThirdParty.Id
        End With

        ActionsOnControlsPoppupAdvance = True
        INDtxtAdjustmentAdvance.Focus()
    End Sub

    ''' <summary>
    ''' Genera los mensajes de error.
    ''' </summary>
    ''' <param name="errors"></param>
    ''' <remarks></remarks>
    Private Sub generateListError(errors As String)
        Dim listError As New StringBuilder()
        listError.AppendLine(ResourceManager.GetString("ErrorListMessage"))
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.Advertencia) = listError.ToString()
    End Sub

    ''' <summary>
    ''' Valida la cuota
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateShare(acc As AccountPayable)
        If Nature = 1 Then
            Dim ban As Boolean = False
            For Each itemShare As AccountPayableShares In acc.AccountPayableShares
                If itemShare.ValueNoteShare > itemShare.Balance Then
                    itemShare.ValueNoteShare = 0
                    ban = True
                    Exit For
                End If
            Next

            If ban Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor del ajuste no puede ser mayor al saldo."
            End If
        End If

        Dim val As Decimal = 0
        For Each itemShare As AccountPayableShares In acc.AccountPayableShares
            val = val + itemShare.ValueNoteShare
        Next

        acc.Adjustment = val
        INDgcBill.RefreshDataSource()
        ctrTmp.RefreshDebitCredit()
    End Sub

    ''' <summary>
    ''' Valida la causacion diferida de la cxp
    ''' </summary>
    ''' <param name="AccountPayableId"></param>
    ''' <remarks></remarks>
    Private Async Function ValidateDeferredCausation(AccountPayableId As Integer) As Task
        Using model As New MAccountPayable(Tag)
            Dim resultAccountPayable As ActionResult(Of AccountPayable) = Await model.GetValidationDeferredCausation(AccountPayableId)
            If resultAccountPayable.StateResult Then
                Mensaje(EeventViewerImages.Advertencia) = "La Cuenta por Pagar maneja causaciones diferidas. Recuerde realizar el ajuste respectivo."
            End If
        End Using
    End Function

    Private Function SetBills(result As List(Of AccountPayable)) As List(Of String)
        Dim listErrors As New List(Of String)

        If Apply = 0 Then
            If ListAccountPayable IsNot Nothing AndAlso ListAccountPayable.Count > 0 Then
                For Each item In result
                    If ListAccountPayable.Any(Function(ap) ap.Id = item.Id) Then
                        listErrors.Add("La factura " & item.BillNumber & " ya esta agregada.")
                        Continue For
                    End If

                    ListAccountPayable.Add(item)
                Next
            Else
                ListAccountPayable = result
            End If

            INDgcBill.DataSource = Nothing
            INDgcBill.DataSource = ListAccountPayable
        End If

        ActionsOnControlsConsultBillOrAdvance = True
        ctrTmp.RefreshDebitCredit()

        Return listErrors
    End Function

    Private Function SetAdvances(result As List(Of AdvancePayments)) As List(Of String)
        Dim listErrors As New List(Of String)

        If Apply = 1 Then
            If ListAdvancePayments IsNot Nothing AndAlso ListAdvancePayments.Count > 0 Then
                For Each item In result
                    If ListAdvancePayments.Any(Function(ap) ap.Id = item.Id) Then
                        listErrors.Add("El anticipo " & item.Code & " ya esta agregado.")
                        Continue For
                    End If

                    ListAdvancePayments.Add(item)
                Next
            Else
                ListAdvancePayments = result
            End If

            INDgcAdvance.DataSource = Nothing
            INDgcAdvance.DataSource = ListAdvancePayments
        End If

        ActionsOnControlsConsultBillOrAdvance = True
        ctrTmp.RefreshDebitCredit()

        Return listErrors
    End Function

    Private Sub Visibility()
        INDlygConcepts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLycView.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciCxP.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemProvider.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.INDLciCurrency.HideControl(False)
        INDgleNature.Properties.ReadOnly = False
    End Sub

    ''' <summary>
    ''' funcion que que recibe un boolean para sabe si oculta o muestra el formulario dentro de este
    ''' </summary>
    ''' <param name="Value"></param>
    Private Sub VisibleLy(Value As Boolean)
        INDlyNotes.Visible = Value
        CtrNavigationControl1.Visible = Value
        INDpcDocumentDetail.Visible = Not Value
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' metodo para setear la moneda seleccionada en la interfaz
    ''' </summary>
    ''' <param name="CurrencyAbbreviation"></param>
    Private Sub SetCurrencyUI(CurrencyAbbreviation As String)
        If String.IsNullOrEmpty(CurrencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat

        Me.INDtxtValue.Properties.Mask.Culture = _culture
        Me.INDtxtBalance.Properties.Mask.Culture = _culture
        Me.INDtxtAdjustment.Properties.Mask.Culture = _culture
        Me.INDtxtValueAdvance.Properties.Mask.Culture = _culture
        Me.INDtxtBalanceAdvance.Properties.Mask.Culture = _culture
        Me.INDtxtAdjustmentAdvance.Properties.Mask.Culture = _culture
        Me.RepositoryItemTextEditBalanceShare.Mask.Culture = _culture
        Me.RepositoryItemTextEditValueNote.Mask.Culture = _culture

        Me.GridColumn288 = Window.Utils.FormatGrid(GridColumn288, CurrencyAbbreviation)
        Me.INDColValueConcept = Window.Utils.FormatGrid(INDColValueConcept, CurrencyAbbreviation)
        Me.INDColValueInvoice = Window.Utils.FormatGrid(INDColValueInvoice, CurrencyAbbreviation)
        Me.INDColInvoiceBalance = Window.Utils.FormatGrid(INDColInvoiceBalance, CurrencyAbbreviation)
        Me.INDColAdjustment = Window.Utils.FormatGrid(INDColAdjustment, CurrencyAbbreviation)
        Me.INDColInitialValue = Window.Utils.FormatGrid(INDColInitialValue, CurrencyAbbreviation)
        Me.INDColAdvanceBalance = Window.Utils.FormatGrid(INDColAdvanceBalance, CurrencyAbbreviation)
        Me.INDColAdvanceAdjustment = Window.Utils.FormatGrid(INDColAdvanceAdjustment, CurrencyAbbreviation)
        Me.ctrTmp.CodeISO4217 = CurrencyAbbreviation
        Me.ctrTmp.RefreshDebitCredit()
    End Sub

    ''' <summary>
    ''' valida que no haya elementos en la rejilla de anticipos o si no deshabilita el control de la moneda
    ''' </summary>
    Private Sub ValidateCurrency()
        If Me.ListAccountPayable?.Any(Function(x) x.ChangeTracker?.State <> ObjectState.Deleted) OrElse Me.ListAdvancePayments?.Any(Function(x) x.ChangeTracker?.State <> ObjectState.Deleted) Then
            Me.INDsleCurrency.Enabled = False
        Else
            Me.INDsleCurrency.Enabled = True
        End If
    End Sub
#End Region

#Region "Crud Base"

    ''' <summary>
    ''' Método: Buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        Dim ListItems As New List(Of Tuple(Of String, Integer))
        ListItems.Add(New Tuple(Of String, Integer)("Registrado", 1))
        ListItems.Add(New Tuple(Of String, Integer)("Confirmado", 2))
        ListItems.Add(New Tuple(Of String, Integer)("Anulado", 3))

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "NoteDate", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo With {.Caption = "Proveedor", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo With {.Caption = "Nit", .FieldName = "Nit", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo With {.Caption = "Moneda", .FieldName = "CurrencyAbbreviation", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo With {.Caption = "Valor", .FieldName = "Value", .ColumnWidth = 200, .ColumnFormatType = DevExpress.Utils.FormatType.Custom, .ColumnFormat = "n2"}}.ToList()
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListLoadPaymentNotesDebitCredit
            .ValorSolicitado = "Code"
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbtnConsecutive.Text = ReturnValue
        If INDbtnConsecutive.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnConsecutive.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnConsecutive.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MNotesDebitCredit(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetPaymentsNote(Code)
                    paymentsNote = resultOperation.ObjectEmbbeded
                    INDlyNotes.BeginUpdate()
                    If paymentsNote IsNot Nothing AndAlso paymentsNote.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(paymentsNote.Id))
                            With paymentsNote
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                Code = .Code
                                DateDocument = .NoteDate
                                Apply = .IndicatesBillAdvance
                                Me.AllowDiscountPromptPayment = .AllowDiscountPromptPayment
                                Me.AffectBaseToDiscount = .AffectBaseToDiscount
                                IdSupplier = .IdSupplierDistributionLines
                                _idSupplier = .IdSupplier
                                INDsleProvider.Properties.NullText = .DescriptionSupplier
                                Nature = .Nature
                                Comments = .Comment
                                Me.CurrencyId(.Currency?.Abbreviation) = .CurrencyId
                                BarraBotones.StatusRecord = .Status.ToString
                                If Apply = 2 Then
                                    INDSleCxP.Properties.NullText = .DescriptionAccountPayable
                                    INDSleCxP.EditValue = .AccountPayable.Code
                                End If

                                If .Status = 1 Then
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmAnnular)
                                Else
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                End If

                                If .IdCostCenter IsNot Nothing Then
                                    IdCostCenter = .IdCostCenter
                                    INDsleCostCenter.Properties.NullText = .DescriptionCostCenter
                                Else
                                    INDsleCostCenter.Properties.NullText = String.Empty
                                End If

                                For Each item As PaymentNotesAccountPayableAdvance In .PaymentNotesAccountPayableAdvance
                                    If item.AccountPayableId IsNot Nothing Then
                                        Dim ban As Integer = 0
                                        Dim accP As AccountPayable
                                        If ListAccountPayable Is Nothing Then
                                            ListAccountPayable = New List(Of AccountPayable)
                                        Else
                                            ban = ListAccountPayable.FindAll(Function(x) x.Id = item.AccountPayableId).Cast(Of AccountPayable).ToList().Count
                                        End If
                                        If ban = 0 Then
                                            Using modelAP As New MAccountPayable("")
                                                accP = modelAP.GetAccountPayableByIdForNotes(item.AccountPayableId)
                                            End Using
                                            accP.Adjustment = item.AdjusmentValue
                                            accP.Percentage = item.PercentageValue
                                            accP.IdPaymentNotesAccountPayableAdvance = item.Id
                                            For Each itemS As AccountPayableShares In accP.AccountPayableShares
                                                If item.AccountPayableShareId = itemS.Id Then
                                                    itemS.ValueNoteShare = item.AdjustmentValueShare
                                                End If
                                            Next
                                            If BudgetInterface Then
                                                GetObligationDetailsByAccountPayable(accP)
                                                If item.PaymentNoteAccountPayableBudget IsNot Nothing Then
                                                    Dim removeDetails = New List(Of PaymentNoteAccountPayableBudget)
                                                    For Each detail In item.PaymentNoteAccountPayableBudget
                                                        Dim accountPayableCommitment = accP.AccountPayableCommitments.FirstOrDefault(Function(d) d.Id = detail.ObligationDetailId)
                                                        If accountPayableCommitment Is Nothing Then
                                                            removeDetails.Add(detail)
                                                            Continue For
                                                        End If
                                                        accountPayableCommitment.Value = detail.Value
                                                    Next

                                                    For Each detail In removeDetails
                                                        item.PaymentNoteAccountPayableBudget.Remove(detail)
                                                    Next
                                                End If
                                            End If

                                            accP.MarkAsUnchanged()
                                            ListAccountPayable.Add(accP)
                                        End If
                                    ElseIf item.AdvancePaymentId IsNot Nothing Then
                                        Dim adva As AdvancePayments
                                        Using modelAdva As New MAdvancePayments("")
                                            adva = modelAdva.GetAdvancePaymentsById(item.AdvancePaymentId)
                                        End Using
                                        If ListAdvancePayments Is Nothing Then
                                            ListAdvancePayments = New List(Of AdvancePayments)
                                        End If
                                        adva.Adjustment = item.AdjusmentValue
                                        adva.Percentage = item.PercentageValue
                                        adva.IdPaymentNotesAccountPayableAdvance = item.Id
                                        adva.MarkAsUnchanged()
                                        ListAdvancePayments.Add(adva)
                                    End If
                                Next

                                If ListAccountPayable IsNot Nothing Then
                                    INDgcBill.DataSource = Nothing
                                    INDgcBill.DataSource = ListAccountPayable
                                ElseIf ListAdvancePayments IsNot Nothing Then
                                    INDgcAdvance.DataSource = Nothing
                                    INDgcAdvance.DataSource = ListAdvancePayments
                                End If

                                ListAddConcept = paymentsNote.PaymentsNoteDetails.ToList

                                'Se consulta el proveedor por el id
                                Dim supplierTemp = Presenter.GetSupplierById(.IdSupplier)
                                _idThirdPartySupplier = supplierTemp.IdThirdParty.Id
                                _descriptionThirdParty = supplierTemp.IdThirdParty.NitName
                            End With

                            ctrTmp.RefreshDebitCredit()
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.paymentsNote.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordPayments With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = paymentsNote.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If

                            Me.BarraBotones.SetDocuments(paymentsNote.Id, Me.Tag.ToString(), Nothing, GetType(PaymentNotes).Name)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, paymentsNote.Id, 0, paymentsNote.Id)

                            AsyncLoader(False)
                            ActionsOnControls = True
                            INDgcConcepts.DataSource = Nothing
                            INDgcConcepts.DataSource = ListAddConcept
                            INDgcConcepts.Enabled = True
                            INDgcAdvance.Enabled = True
                            INDgcBill.Enabled = True
                            INDpceAdvance.Enabled = True
                            ActionsOnControlsConsultBillOrAdvance = True

                            If paymentsNote.Status <> 1 Then
                                ReadOnlyControls(True)
                                ' Habilitar el grid de conceptos para permitir visualización en modo confirmado/anulado
                                INDgcConcepts.Enabled = True
                                ' Habilitar la columna de acciones para que el botón de editar funcione
                                Dim colActionsConceptos = viewConcept.Columns.ColumnByName("colActions")
                                If colActionsConceptos IsNot Nothing Then
                                    colActionsConceptos.OptionsColumn.AllowEdit = True
                                End If
                            End If

                            Me.ValidateCurrency()
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewNotesDebitCredit()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnConsecutive.Focus()
                        End If
                    End If

                    INDlyNotes.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnConsecutive.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If paymentsNote IsNot Nothing AndAlso paymentsNote.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MNotesDebitCredit(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeletePaymentsNote(paymentsNote)
                        If result.StateResult = True Then
                            Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbtnConsecutive.Enabled = False
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
                    INDbtnConsecutive.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        Else
            If ValidateFields() = False Then
                Exit Sub
            End If
        End If
        AssigningValues()
        Try
            Using Model As New MNotesDebitCredit(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SavePaymentNotesComplete(paymentsNote, ListAccountPayable, ListAdvancePayments, banConfirm, _idCurrentSequence)
                If Result.StateResult = True Then
                    'Se descarta la secuencia numerica usada
                    If paymentsNote.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.paymentsNote = Result.ObjectEmbbeded
                    If _banSaveModify = True Then
                        If banConfirm = False Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), paymentsNote.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = Result.MessageResult(0)
                        End If
                    ElseIf _banSaveModify = False Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If

                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, paymentsNote.Id, 0, paymentsNote.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, paymentsNote.Id, 0, paymentsNote.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, paymentsNote.Id, 0, paymentsNote.Id)
                    End Select

                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnConsecutive.Enabled = False
                    If Result.MessageResult IsNot Nothing Then
                        generateListError(Result.MessageResult(0))
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnConsecutive.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Confirma la nota debito/credito
    ''' </summary>
    ''' <exception cref="System.NotImplementedException"></exception>
    Private Async Sub Confirmar()
        Try
            Using Model As New MNotesDebitCredit(Me.Tag.ToString())
                paymentsNote.IdOperatingUnit = BarraBotones.OperatingUnit.Id
                AsyncLoader(True)
                Dim Result = Await Model.ConfirmNotesDebitCredit(paymentsNote, ListAccountPayable, ListAdvancePayments, _idCurrentSequence)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = Result.MessageResult(0)
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, paymentsNote.Id, 0, paymentsNote.Id)
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnConsecutive.Enabled = False
                    If Result.MessageResult IsNot Nothing Then
                        generateListError(Result.MessageResult(0))
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnConsecutive.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewNotesDebitCredit()
        End If
    End Sub

    ''' <summary>
    ''' Metodo: Anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub Anular()
        Try
            Using model As New MNotesDebitCredit(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SavePaymentsNote(paymentsNote, _idCurrentSequence)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                    Me.BarraBotones.PrintReport(PrintReportAction.Cancel, paymentsNote.Id, 0, paymentsNote.Id)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnConsecutive.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnConsecutive.Enabled = False
            Throw ex
        End Try
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListAccountPayableGridPopupControl = Nothing
        ListAccountPayable = Nothing
        ListAdvancePayments = Nothing
        listDeleteDetailConcept = Nothing
        listDeleteAdvancePayments = Nothing
        listDeleteAccountPayable = Nothing
        NatureType = Nothing
        ListAddConcept = Nothing
        accountPayableDetailConcept = Nothing
        paymentNoteDetails = Nothing
        _sequence = Nothing
        _idOperativeUnit = Nothing
        Presenter = Nothing
        paymentsNote = Nothing
        record = Nothing
        _idCurrentSequence = Nothing
        _accountPayable = Nothing
        _advancePayments = Nothing
        _id = Nothing
        _billNumber = Nothing
        _listAccountPayableShares = Nothing
        _idSupplier = Nothing
        _idThirdPartySupplier = Nothing
        _descriptionThirdParty = Nothing
        handlesBillsOrAdvance = Nothing
        _codeAdvance = Nothing
        valConcepts = Nothing
        valBillOrAdvance = Nothing
        _banSaveModify = Nothing
        _idMainAccountBillOrAdvance = Nothing
        _idCostCenterBillOrAdvance = Nothing
        _idThirdPartyBillOrAdvance = Nothing
        banConfirm = Nothing
        _nameSupplierDocumentIndexed = Nothing
        _nitThitrdPartyDocumentIndexed = Nothing
        _nameThirdPartyDocumentIndexed = Nothing
        _codeSupplierDocumentIndexed = Nothing
        controlerEditValueChanged = Nothing
        varImp = Nothing
        MainAccountIdDistributionLine = Nothing
        _listNoteType = Nothing
    End Sub

    ''' <summary>
    ''' Evento load donde hacemos las configuraciones iniciales del formulario
    ''' </summary>
    Private Async Sub FrmNotesDebitCredit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await Me.LayoutControls.LoadDefinitionAsync()
        Me.LayoutControls.SetIsCustomizable(Me.INDlyNotes, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        '******************************'
        Me.Funct = AddressOf GenerateDoc
        Presenter = New PNotesDebitCredit(Me)

        AsyncLoader(True)
        Await Presenter.GetSequense()
        Me.LoadStatus()
        LoadRepositorySearch()
        Presenter.GetSettingsPaymentsByOperatingUnitId(Me._idOperativeUnit)
        AsyncLoader(False)
        colBudgetInterface.Visible = BudgetInterface
        colBudgetInterface.OptionsColumn.ShowInCustomizationForm = BudgetInterface

        SetActions()
        Deshacer()
        INDdteDate.Properties.MaxValue = GetDateServer()
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de setear las acciones para las diferentes rejillas
    ''' </summary>
    Private Sub SetActions()
        IndigoGridControl1.SetControlNextFocus(INDgcBill, INDpceConcept)
        IndigoGridControl1.SetControlNextFocus(INDgcAdvance, INDpceConcept)
        INDEsbBill.AddRangeColumns("Factura", "Ajuste")
        INDEsbAdvance.AddRangeColumns("Anticipo", "Ajuste")

        IndigoGridView1.SetListAcction(viewConcept, {eAcciones.Remove, eAcciones.Edit}.ToList())
        IndigoGridView2.SetListAcction(viewBill, {eAcciones.Remove}.ToList())
        IndigoGridView3.SetListAcction(viewGridAdvance, {eAcciones.Remove}.ToList())
        IndigoGridControl1.RefreshGrid(INDgcConcepts)
        IndigoGridControl1.RefreshGrid(INDgcBill)
        IndigoGridControl1.RefreshGrid(INDgcAdvance)
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que consulta una nota debito/credito por codigo
    ''' </summary>
    Private Async Sub INDbtnConsecutive_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnConsecutive.KeyDown
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
                    Await Me.NewNotesDebitCredit()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de conceptos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceConcept_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceConcept.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceConcept.ShowPopup()
            CtrConcepts1.InitializeControler(Nothing, paymentNoteDetails, _form, Nature, CurrencyAbbreviation:=Me.CurrencyAbbreviation, taxRegistration:=TaxRegistration)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara la presionar enter en el repositorio de porcentaje en la rejilla de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemSpinEditPercentage_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemSpinEditPercentage.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            SendKeys.Send("{HOME}")
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el repositorio de ajuste en la rejilla de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemTextEditAdjustment_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemTextEditAdjustment.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de ajuste
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtAdjustment_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtAdjustment.KeyDown
        If e.KeyCode = Keys.Enter Then

        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el repositorio de valor de nota
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemTextEditValueNote_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemTextEditValueNote.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            SendKeys.Send("{HOME}")
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el repositorio de ajuste de anticipos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemTextEditAdjustmentAdvance_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemTextEditAdjustmentAdvance.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter el respositorio de porcentaje de anticipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemSpinEditPercentageAdvance_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemSpinEditPercentageAdvance.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            SendKeys.Send("{HOME}")
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el popupControl de anticipos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAdvance_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceAdvance.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDsleAdvance.Focus()
            INDpceAdvance.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el popupControl de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceBill_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceBill.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDsleBills.Focus()
            INDpceBill.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape en los controles de anticipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAdvance_KeyDown(sender As Object, e As KeyEventArgs) Handles INDsleAdvance.KeyDown, INDdteDateAdvance.KeyDown, INDtxtValueAdvance.KeyDown, INDtxtBalanceAdvance.KeyDown, INDtxtAdjustmentAdvance.KeyDown, INDsePercentageAdvance.KeyDown, INDbtnAddAdvance.KeyDown
        If e.KeyCode = Keys.Escape Then
            INDpceConcept.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape en los controles de factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBills_KeyDown(sender As Object, e As KeyEventArgs) Handles INDsleBills.KeyDown, INDdteBillDate.KeyDown, INDdteExpiredDate.KeyDown, INDtxtValue.KeyDown, INDtxtBalance.KeyDown, INDtxtAdjustment.KeyDown, INDsePercentage.KeyDown, INDbtnAddBill.KeyDown
        If e.KeyCode = Keys.Escape Then
            INDpceConcept.Focus()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara la presionar click en el control de conceptos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceConcept_Click(sender As Object, e As EventArgs) Handles INDpceConcept.Click

        CtrConcepts1.idThird = _idThirdPartySupplier
        CtrConcepts1.ThirdPartyDescription = _descriptionThirdParty
        CtrConcepts1.FiscalYear = DateDocument?.Year
        CtrConcepts1.InitializeControler(Nothing, paymentNoteDetails, _form, Nature,
                                         ListAccountPayable:=ListAccountPayable, CurrencyAbbreviation:=Me.CurrencyAbbreviation, taxRegistration:=TaxRegistration)

    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddBill_Click(sender As Object, e As EventArgs) Handles INDbtnAddBill.Click
        AddBill()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar anticipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddAdvance_Click(sender As Object, e As EventArgs) Handles INDbtnAddAdvance.Click
        AddAdvance()
    End Sub

    ''' <summary>
    ''' evento click que dispara el formulario de cuentas por pagar en este formulario y con el consecutivo trae el detalle de la reversion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbViewCxP_Click(sender As Object, e As EventArgs) Handles INDSmbViewCxP.Click
        If INDSleCxP.EditValue IsNot Nothing Then
            INDpcDocuments.Controls.Clear()
            INDlblNameVoucher.Text = "Cuentas por Pagar (Detalle Reversión)"
            AsyncLoader(True)
            If _frmAccountsPayable Is Nothing Then
                _frmAccountsPayable = New FrmAccountsPayable()
                _frmAccountsPayable.TopLevel = False
                _frmAccountsPayable.Parent = INDpcDocuments
                _frmAccountsPayable.ToolBar.Dock = DockStyle.None
                _frmAccountsPayable.ViewModeEditHold = False
                _frmAccountsPayable.Dock = DockStyle.Fill
                _frmAccountsPayable.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
                AddHandler _frmAccountsPayable.Shown, AddressOf _frmAccountsPayable_Shown
                _frmAccountsPayable.Show()
            ElseIf INDSleCxP.EditValue <> _frmAccountsPayable.Consecutive Then
                _frmAccountsPayable.Parent = INDpcDocuments
                _frmAccountsPayable.Deshacer()
                _frmAccountsPayable_Shown(Nothing, Nothing)
            Else
                _frmAccountsPayable.Parent = INDpcDocuments
            End If

            VisibleLy(False)
            ClickFlag = False
        End If
    End Sub

    ''' <summary>
    ''' evento que ejecuta el load controls del formulario cuentas por pagar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub _frmAccountsPayable_Shown(sender As Object, e As EventArgs)

        If Comments Is String.Empty Then
            Comments = " Reversión Documento Cuenta por Pagar Consecutivo : " + INDSleCxP.EditValue
        End If

        _frmAccountsPayable.Consecutive = INDSleCxP.EditValue
        Await _frmAccountsPayable.LoadControls()

        IdAccountPayable = _frmAccountsPayable.IdAccountPayable

        paymentsNote.AccountPayable = CType(_frmAccountsPayable.viewBill.GetFocusedRow, AccountPayable)
        If paymentsNote.AccountPayable IsNot Nothing Then

            _idSupplier = paymentsNote.AccountPayable.IdSupplier
            Me.IdSupplierDistributionsLine = paymentsNote.AccountPayable.IdSuppliersDistributionLines
        End If

        ctrTmp.RefreshDebitCredit()
        INDLycView.HideControl(False)
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' evento clickback que se devuelve al formulario original de nota debito/credito
    ''' </summary>
    Private Sub CtrNavigationBack_ClickBack() Handles CtrNavigationBack.ClickBack
        VisibleLy(True)
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProvider_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProvider.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmSupplier With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el form de centro costo
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
    ''' Evento que abre el form de CxP
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBills_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBills.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmAccountsPayable With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

#End Region

#Region "ClosePopup"

    ''' <summary>
    ''' Evento para cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrConcepts1_ClosePopup(sender As Object, e As EventArgs) Handles CtrConcepts1.ClosePopup
        CtrConcepts1.INDsleConcept.Focus()
        CtrConcepts1.CleanControls()
        INDpceConcept.ClosePopup()
        AddConcept(CtrConcepts1.PaymentNoteDetail)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup de conceptos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceConcept_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceConcept.CloseUp
        If CtrConcepts1.Entity Then
            CtrConcepts1.CleanControls()
        End If

        ' Volver a deshabilitar el popup si la nota está confirmada/anulada
        If paymentsNote IsNot Nothing AndAlso Not {0, 1}.Contains(paymentsNote.Status) Then
            INDpceConcept.Properties.ReadOnly = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape al popup de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceBill_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceBill.CloseUp
        If e.CloseMode = PopupCloseMode.Cancel Then
            If ListAccountPayable Is Nothing Then
                INDpceConcept.Focus()
            ElseIf ListAccountPayable.Count > 0 Then
                IndigoGridControl1.ControlNextFocus = True
            Else
                INDpceConcept.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape al popup de anticipos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAdvance_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceAdvance.CloseUp
        If e.CloseMode = PopupCloseMode.Cancel Then
            If ListAdvancePayments Is Nothing Then
                INDpceConcept.Focus()
            ElseIf ListAdvancePayments.Count > 0 Then
                IndigoGridControl1.ControlNextFocus = True
            Else
                INDpceConcept.Focus()
            End If
        End If
    End Sub

#End Region

#Region "ContextMenuGridControl"

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Dim PaymentsNoteDetailsTmp As PaymentsNoteDetails = CType(viewConcept.GetFocusedRow, PaymentsNoteDetails)
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditConcept(PaymentsNoteDetailsTmp)
            Case "Remove"
                ' No permitir eliminar conceptos cuando la nota está confirmada o anulada
                If paymentsNote IsNot Nothing AndAlso Not {0, 1}.Contains(paymentsNote.Status) Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede eliminar conceptos de una nota confirmada o anulada."
                    Exit Sub
                End If
                DeleteConcept(PaymentsNoteDetailsTmp)
        End Select
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        DeleteBill()
    End Sub

    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction, IndigoGridView3.ContexMenuActions
        DeleteAdvance()
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDrptTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrptTxtValue.EditValueChanging
        If e.NewValue = String.Empty Then
            Exit Sub
        End If

        Dim detail = DirectCast(INDgvBudgetInterface.GetFocusedRow, AccountPayableCommitments)
        If e.NewValue < 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor no puede ser mayor al saldo"
            e.Cancel = True

        ElseIf e.NewValue > detail.Balance Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor no puede ser mayor al saldo"
            e.Cancel = True
        End If

        If Not e.Cancel Then
            Dim acc As AccountPayable = CType(viewBill.GetFocusedRow, AccountPayable)
            If acc.IdPaymentNotesAccountPayableAdvance > 0 Then
                acc.HandlesAddModifyDelete = 2
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBills_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBills.EditValueChanged
        If INDsleBills.EditValue IsNot Nothing Then
            LoadInformationAccountPayable()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProvider_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProvider.EditValueChanged
        If IdSupplier IsNot Nothing Then
            If paymentsNote.Id = 0 Then
                Dim focusedRow = viewSupplier.GetFocusedRow
                Dim suppplierMainAccount As Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo

                Dim supplier = TryCast(focusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)

                If supplier IsNot Nothing AndAlso supplier.OriginalRow IsNot Nothing Then
                    suppplierMainAccount = TryCast(supplier.OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo)
                Else
                    suppplierMainAccount = Presenter.GetSuppliersDistributionLinesById(INDsleProvider.EditValue)
                End If

                _idSupplier = suppplierMainAccount.IdSupplier.Id
                MainAccountIdDistributionLine = suppplierMainAccount.IdDistributionLine.IdMainAccount.Id

                _nameSupplierDocumentIndexed = suppplierMainAccount.IdSupplier.Name
                _nameThirdPartyDocumentIndexed = suppplierMainAccount.IdSupplier.IdThirdParty.Name
                _nitThitrdPartyDocumentIndexed = suppplierMainAccount.IdSupplier.IdThirdParty.Nit
                _codeSupplierDocumentIndexed = suppplierMainAccount.IdSupplier.Code
                _idThirdPartySupplier = suppplierMainAccount.IdSupplier.IdThirdParty.Id
                _descriptionThirdParty = suppplierMainAccount.IdSupplier.IdThirdParty.NitName

                If Nature IsNot Nothing AndAlso Apply IsNot Nothing Then
                    If handlesBillsOrAdvance = 0 Then
                        Presenter.InitializeAccountPayableDatasource(_idSupplier, Nature, MainAccountIdDistributionLine)
                    ElseIf handlesBillsOrAdvance = 1 Then
                        Presenter.InitializeAdvancePayments(_idSupplier, Nature)
                    End If
                End If
            End If

            'Consulto la linea de distribucion por id para saber si la cuenta contable que tiene amarrada pide el centro de costo
            Using model As New MSuppliersDistributionLines("")
                Dim sdl As SuppliersDistributionLines = model.GetSuppliersDistributionLinesById(IdSupplier)
                If sdl.DistributionLines.MainAccounts.HandlesCostCenter = True Then
                    INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemCostCenter.AllowHide = False
                Else
                    INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    IdCostCenter = Nothing
                    INDlyItemCostCenter.AllowHide = True
                End If

                If sdl.Supplier.PromptPaymentDiscount.Any OrElse (AllowDiscountPromptPayment And BarraBotones.StatusRecord <> "1") Then
                    INDLcgDiscountPromptPayment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyIAllowPromptPayment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    Me.AllowDiscountPromptPayment = False
                    INDLcgDiscountPromptPayment.HideControl()
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control naturaleza
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleNature_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleNature.EditValueChanged
        CleanControlsPopup()

        If Nature IsNot Nothing Then
            If IdSupplier IsNot Nothing AndAlso Apply IsNot Nothing Then
                If handlesBillsOrAdvance = 0 Then
                    Presenter.InitializeAccountPayableDatasource(_idSupplier, Nature, MainAccountIdDistributionLine)
                ElseIf handlesBillsOrAdvance = 1 Then
                    Presenter.InitializeAdvancePayments(_idSupplier, Nature)
                End If
            End If
            If Nature = 1 Then
                INDSleConceptAdjustment.Properties.DataSource = Me.ListConceptsAdjustment.ToList()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de aplica a:
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleNoteType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleNoteType.EditValueChanged
        INDLciCurrency.HideControl(True)

        If Apply IsNot Nothing Then
            Me.AllowDiscountPromptPayment = False
            If Apply = 0 Then
                INDlygBills.HideControl(False)
                INDlygAdvance.HideControl()
                Visibility()
                handlesBillsOrAdvance = 0

            ElseIf Apply = 1 Then
                INDlygBills.HideControl()
                INDlygAdvance.HideControl(False)
                Visibility()
                handlesBillsOrAdvance = 1

            ElseIf Apply = 2 Then
                INDlygBills.HideControl()
                INDlygAdvance.HideControl()
                INDlygConcepts.HideControl()
                INDlyItemCostCenter.HideLayout()
                INDlyItemProvider.HideLayout()
                INDLciCxP.ShowLayout()
                INDLycView.ShowLayout()
                INDLcgDiscountPromptPayment.HideControl()
                Nature = 1
                INDgleNature.Properties.ReadOnly = True
                handlesBillsOrAdvance = 2
                IdSupplier = Nothing
                INDsleProvider.Properties.NullText = String.Empty

            ElseIf Apply = 3 Then
                Mensaje(EeventViewerImages.Advertencia) = "Disponible en próxima versión"
                Apply = 0
            End If

            If IdSupplier IsNot Nothing AndAlso Nature IsNot Nothing Then
                If handlesBillsOrAdvance = 0 Then
                    Presenter.InitializeAccountPayableDatasource(_idSupplier, Nature, MainAccountIdDistributionLine)
                ElseIf handlesBillsOrAdvance = 1 Then
                    Presenter.InitializeAdvancePayments(_idSupplier, Nature)
                End If
            End If
        Else
            INDlygBills.HideControl()
            INDlygAdvance.HideControl()
            handlesBillsOrAdvance = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de anticipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAdvance_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAdvance.EditValueChanged
        If INDsleAdvance.EditValue IsNot Nothing Then
            LoadInformationAdvance()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de porcentaje
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsePercentageAdvance_EditValueChanged(sender As Object, e As EventArgs) Handles INDsePercentageAdvance.EditValueChanged
        If PercentageAdvance > 0 AndAlso controlerEditValueChanged = True Then
            If BalanceAdvance = 0 AndAlso Nature = 2 Then
                Exit Sub
            End If

            Dim val As Decimal = ConvertPercentToValue(PercentageAdvance, BalanceAdvance)
            If val > 0 Then
                controlerEditValueChanged = False
                AdjustmentAdvance = val
                controlerEditValueChanged = True
            Else
                AdjustmentAdvance = 0
                PercentageAdvance = 0
            End If

        ElseIf PercentageAdvance = 0 AndAlso controlerEditValueChanged = True Then
            AdjustmentAdvance = 0
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de porcentaje
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsePercentage_EditValueChanged(sender As Object, e As EventArgs) Handles INDsePercentage.EditValueChanged
        If Percentage > 0 AndAlso controlerEditValueChanged = True Then
            If Balance = 0 AndAlso Nature = 2 Then
                Exit Sub
            End If

            Dim val As Decimal = ConvertPercentToValue(Percentage, Balance)
            If val > 0 Then
                controlerEditValueChanged = False
                Adjustment = val
                controlerEditValueChanged = True
            Else
                Adjustment = 0
                Percentage = 0
            End If

        ElseIf Percentage = 0 AndAlso controlerEditValueChanged = True Then
            Adjustment = 0
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del ajuste en popup anticipos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtAdjustmentAdvance_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtAdjustmentAdvance.EditValueChanged
        If AdjustmentAdvance > 0 AndAlso controlerEditValueChanged = True Then
            If BalanceAdvance = 0 AndAlso Nature = 2 Then
                controlerEditValueChanged = False
                PercentageAdvance = 100
                controlerEditValueChanged = True
                Exit Sub
            End If

            Dim val As Decimal = ConvertValueToPercent(INDtxtAdjustmentAdvance.EditValue, BalanceAdvance)
            If val > 0 Then
                controlerEditValueChanged = False
                PercentageAdvance = val
                controlerEditValueChanged = True
            Else
                controlerEditValueChanged = False
                PercentageAdvance = 0
                controlerEditValueChanged = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del ajuste en popup facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtAdjustment_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtAdjustment.EditValueChanged
        If Adjustment > 0 AndAlso controlerEditValueChanged = True Then
            If Balance = 0 AndAlso Nature = 2 Then
                controlerEditValueChanged = False
                Percentage = 100
                controlerEditValueChanged = True
                Exit Sub
            End If

            Dim val As Decimal = ConvertValueToPercent(Adjustment, Balance)
            If val > 0 Then
                controlerEditValueChanged = False
                Percentage = val
                controlerEditValueChanged = True
            Else
                controlerEditValueChanged = False
                Percentage = 0
                controlerEditValueChanged = True
            End If
        End If
    End Sub

    Private Sub INDSleCxP_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCxP.EditValueChanged

        If CxPSelected IsNot Nothing AndAlso INDSleCxP.EditValue IsNot Nothing Then
            paymentsNote.AccountPayable = Nothing
            Me.CurrencyId(If(Me.CxPSelected?.Currency Is Nothing, Me.indigo.CurrencyISO4217, Me.CxPSelected?.Currency?.Abbreviation)) = If(Me.CxPSelected?.CurrencyId Is Nothing, Me.indigo.OfficialCurrencyId, Me.CxPSelected?.CurrencyId)
        End If

        If Apply = 2 AndAlso Not ClickFlag Then
            Comments = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' si la bandera de postulacion de descuento esta descativada desactiva tambien la de afectacion a la base
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrgAllowPromptPayment_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgAllowPromptPayment.EditValueChanged
        If Not AllowDiscountPromptPayment Then
            Me.AffectBaseToDiscount = False
            INDlyIAffectBaseToDiscount.HideLayout()
        Else
            INDlyIAffectBaseToDiscount.ShowLayout()
        End If
    End Sub

    ''' <summary>
    ''' evento cuando se cambia la moneda seleccionada 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCurrency.EditValueChanged
        If Me.CurrencyId Is Nothing Then
            Exit Sub
        End If

        If Me.CurrencySelected IsNot Nothing Then
            Me.SetCurrencyUI(Me.CurrencySelected?.Abbreviation)
        End If
    End Sub
#End Region

#Region "Leave"

    ''' <summary>
    ''' Evento que se dispara al perder el foco del repositorio de ajuste de la rejilla de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemTextEditAdjustment_Leave(sender As Object, e As EventArgs) Handles RepositoryItemTextEditAdjustment.Leave
        Dim acc As AccountPayable = CType(viewBill.GetFocusedRow, AccountPayable)
        If acc.Adjustment > 0 Then
            Dim val As Decimal = ConvertValueToPercent(acc.Adjustment, acc.Balance)
            If val > 0 Then
                ValueProportional(acc, Nature)
                acc.Percentage = val
            Else
                acc.Adjustment = 0
                acc.Percentage = 0
            End If
        Else
            acc.Adjustment = 0
            acc.Percentage = 0
        End If

        If acc.IdPaymentNotesAccountPayableAdvance > 0 Then
            acc.HandlesAddModifyDelete = 2
        End If

        INDgcBill.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al perder el foco del repositorio de porcentaje de la rejilla de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemSpinEditPercentage_Leave(sender As Object, e As EventArgs) Handles RepositoryItemSpinEditPercentage.Leave
        Dim acc As AccountPayable = CType(viewBill.GetFocusedRow, AccountPayable)
        If acc.Percentage > 0 Then
            Dim val As Decimal = ConvertPercentToValue(acc.Percentage, acc.Balance)
            acc.Adjustment = val
            ValueProportional(acc, Nature)
            If val = 0 Then
                acc.Percentage = 0
            End If
        Else
            acc.Adjustment = 0
        End If

        If acc.IdPaymentNotesAccountPayableAdvance > 0 Then
            acc.HandlesAddModifyDelete = 2
        End If

        INDgcBill.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Evento que se dispara la perder el foco del repositorio de valor de la nota
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemTextEditValueNote_Leave(sender As Object, e As EventArgs) Handles RepositoryItemTextEditValueNote.Leave
        Dim acc As AccountPayable = CType(viewBill.GetFocusedRow, AccountPayable)
        ValidateShare(acc)

        If acc.IdPaymentNotesAccountPayableAdvance > 0 Then
            acc.HandlesAddModifyDelete = 2
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al perder el foco del repositorio de ajuste de anticipos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemTextEditAdjustmentAdvance_Leave(sender As Object, e As EventArgs) Handles RepositoryItemTextEditAdjustmentAdvance.Leave
        Dim advan As AdvancePayments = CType(viewGridAdvance.GetFocusedRow, AdvancePayments)
        If advan.Adjustment > 0 Then
            Dim val As Decimal = ConvertValueToPercent(advan.Adjustment, advan.Balance)
            If val > 0 Then
                advan.Percentage = val
            Else
                advan.Adjustment = 0
                advan.Percentage = 0
            End If
        Else
            advan.Adjustment = 0
            advan.Percentage = 0
        End If

        If advan.IdPaymentNotesAccountPayableAdvance > 0 Then
            advan.HandlesAddModifyDelete = 2
        End If

        INDgcAdvance.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al perder el foco del repositorio de porcentaje de anticipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemSpinEditPercentageAdvance_Leave(sender As Object, e As EventArgs) Handles RepositoryItemSpinEditPercentageAdvance.Leave
        Dim advan As AdvancePayments = CType(viewGridAdvance.GetFocusedRow, AdvancePayments)
        If advan.Percentage > 0 Then
            Dim val As Decimal = ConvertPercentToValue(advan.Percentage, advan.Balance)
            advan.Adjustment = val
            If val = 0 Then
                advan.Percentage = 0
            End If
        Else
            advan.Adjustment = 0
        End If

        If advan.IdPaymentNotesAccountPayableAdvance > 0 Then
            advan.HandlesAddModifyDelete = 2
        End If

        INDgcAdvance.RefreshDataSource()
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnConsecutive.Enabled Then
            INDbtnConsecutive.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmNotesDebitCredit_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProvider_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProvider.QueryPopUp
        If INDsleProvider.Properties.DataSource Is Nothing Then
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de centro costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If INDsleCostCenter.Properties.DataSource Is Nothing Then
            Presenter.InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el popup de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrptPceBudgetInterface_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptPceBudgetInterface.QueryPopUp
        If BudgetInterface Then
            Dim accountPayable = CType(viewBill.GetFocusedRow, AccountPayable)
            INDgcBudgetInterface.DataSource = accountPayable.AccountPayableCommitments
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCxP_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCxP.QueryPopUp
        If INDSleCxP.Properties.DataSource Is Nothing Then
            Presenter.ListAccountPayableXpo()
        End If
    End Sub

    ''' <summary>
    ''' querypopup que carga el datasource de la moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If Me.DataSourceCurrency Is Nothing Then
            Presenter.InitializeCurrency()
        End If
    End Sub
#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If paymentsNote.Id > 0 AndAlso paymentsNote.Status > 1 Then
            Exit Sub
        End If

        Try
            If _idSupplier = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un proveedor."
                Exit Sub
            End If

            If Nature = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar la naturaleza de la nota."
                Exit Sub
            End If

            AsyncLoader(True)

            Dim listErrors As New List(Of String)

            If sender.Name = INDgcBill.Name Then
                Using model As New MNotesDebitCredit(MyTag)
                    Dim result = Await model.ImportBillsToPortfolioNote(e.Rows, New List(Of Object) From {Nature, _idSupplier, MainAccountIdDistributionLine})

                    AsyncLoader(False)
                    listErrors = result.MessageResult
                    If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                        result.MessageResult.AddRange(SetBills(result.ObjectEmbbeded))
                    End If
                End Using
            ElseIf sender.Name = INDgcAdvance.Name Then
                Using model As New MNotesDebitCredit(MyTag)
                    Dim result = Await model.ImportAdvancesToPortfolioNote(e.Rows, New List(Of Object) From {Nature, _idSupplier, MainAccountIdDistributionLine})

                    AsyncLoader(False)
                    listErrors = result.MessageResult
                    If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                        result.MessageResult.AddRange(SetAdvances(result.ObjectEmbbeded))
                    End If
                End Using
            End If

            If listErrors.Count > 0 Then
                Using formulario As New FrmListErrors(listErrors)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
            Me.Cursor = System.Windows.Forms.Cursors.Default

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub

#End Region

#End Region

#Region "ToolBar Events"

    ''' <summary>
    ''' Actualizar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        _banSaveModify = True
        banConfirm = True
        varImp = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Evento del boton nuevo
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _banSaveModify = False
        banConfirm = False
        varImp = 2
        Guardar()
    End Sub

    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PaymentsSecuenceDetail IsNot Nothing Then
                If Not Me._sequence.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnConsecutive.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _banSaveModify = True
        banConfirm = False
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Clcik Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, paymentsNote.Id, 0, paymentsNote.Id)
    End Sub

    ''' <summary>
    ''' Guarda y confirma el documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        _banSaveModify = True
        banConfirm = True
        varImp = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Confirma la nota debito/credito
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        paymentsNote.Status = 2
        Confirmar()
    End Sub

    ''' <summary>
    ''' Barra botones: Click anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        paymentsNote.Status = 3
        Anular()
    End Sub

#End Region

End Class