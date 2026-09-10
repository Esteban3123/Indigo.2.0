'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 27/05/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Payments.MVP
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common
Imports Presentation.Accounting
Imports Presentation.Accounting.MVP
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports Domain.Entities.Service
Imports System.Text
Imports Presentation.Common.MVP


#End Region

Public Class CtrConcepts

#Region "Builder"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Evento que llama al cierre del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ClosePopup(sender As Object, e As EventArgs)

    ''' <summary>
    ''' Evento que no permite cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event DontClosePopup(sender As Object, e As EventArgs)

    ''' <summary>
    ''' Obtiene el estado de la cuenta por pagar
    ''' </summary>
    Public OriginStatus As Byte

    ''' <summary>
    ''' Obtiene el código de la cuenta por pagar / nota / documento
    ''' </summary>
    Public OriginCode As String

    ''' <summary>
    ''' Obtiene número de la factura
    ''' </summary>
    Public BillNumber As String

    ''' <summary>
    ''' obtiene la entidad AccountPayableConcepts 
    ''' </summary>
    Public _accountPayableConcepts As AccountPayableConcepts

    ''' <summary>
    ''' Representa la entidad de conceptos de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _accountPayableConcept As AccountPayableDetailConcept
    Public Property AccountPayableDetail As AccountPayableDetailConcept
        Get
            Return _accountPayableConcept
        End Get
        Set(value As AccountPayableDetailConcept)
            _accountPayableConcept = value
        End Set
    End Property

    ''' <summary>
    ''' Representa la entidad de conceptos de notas debito/credito
    ''' </summary>
    ''' <remarks></remarks>
    Dim _paymentsNoteDetail As PaymentsNoteDetails
    Public Property PaymentNoteDetail As PaymentsNoteDetails
        Get
            Return _paymentsNoteDetail
        End Get
        Set(value As PaymentsNoteDetails)
            _paymentsNoteDetail = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de las cuentas contables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountListXpo As XPInstantFeedbackSource
        Get
            Return CType(INDsleAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el valor de si maneja o no causacion diferida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HandlesDeferredCausation As Boolean
        Get
            Return _handlesDeferredCausation
        End Get
        Set(value As Boolean)
            _handlesDeferredCausation = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor base
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BaseValue As Decimal
        Get
            Return INDtxtBaseValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtBaseValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si se maneja iva descontable o no 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DiscountableIVA As Boolean
        Get
            Return INDDiscountableIVA.EditValue
        End Get
        Set(value As Boolean)
            INDDiscountableIVA.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la tarifa de IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdGeneralLedgerIVA As Integer?
        Get
            Return INDsleRateIva.EditValue
        End Get
        Set(value As Integer?)
            INDsleRateIva.EditValue = value
        End Set
    End Property

	''' <summary>
	''' Obtiene o establece el valor del iva
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Property IVAValue As Decimal?
		Get
			Return INDtxtIvaValue.EditValue
		End Get
		Set(value As Decimal?)
			INDtxtIvaValue.EditValue = value
		End Set
	End Property

    ''' <summary>
    ''' Obtiene o establece el total del concepto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TotalConceptValue As Decimal?
        Get
            Return INDtxtTotalConcept.EditValue
        End Get
        Set(value As Decimal?)
            If INDtxtTotalConcept IsNot Nothing Then
                INDtxtTotalConcept.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los comentarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Comments As String
        Get
            Return INDmemoComments.Text
        End Get
        Set(value As String)
            INDmemoComments.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor facturado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoicedValue As Decimal
        Get
            Return CDec(INDtxtInvoicedValue.EditValue)
        End Get
        Set(value As Decimal)
            INDtxtInvoicedValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la naturaleza
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Nature As Integer
        Get
            Return CInt(INDgleNature.EditValue)
        End Get
        Set(value As Integer)
            INDgleNature.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el porcentaje
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Percentage As Decimal
        Get
            Return CDec(INDsePercentage.EditValue)
        End Get
        Set(value As Decimal)
            INDsePercentage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RetentionValue As Decimal
        Get
            Return CDec(INDtxtRetentionValue.EditValue)
        End Get
        Set(value As Decimal)
            INDtxtRetentionValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Lista los conceptos de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConceptPaymentsListXpo As DevExpress.Xpo.XPCollection
        Get
            Return INDsleConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Lista los conceptos de notas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConceptNotesListXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDsleConceptNote.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleConceptNote.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Lista los conceptos de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConceptRetentionListXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDsleConceptRetention.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleConceptRetention.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Lista los centros de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterListXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Lista los terceros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyListXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDsleThirdParty.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAccount As Integer?
        Get
            Return INDsleAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdConceptPayments As Integer?
        Get
            Return INDsleConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de notas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdConceptNotes As Integer?
        Get
            Return INDsleConceptNote.EditValue
        End Get
        Set(value As Integer?)
            INDsleConceptNote.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdConceptRetention As Integer?
        Get
            Return INDsleConceptRetention.EditValue
        End Get
        Set(value As Integer?)
            INDsleConceptRetention.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCostCenter As Integer?
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdThirdParty As Integer?
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer?)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el año gravable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FiscalYear As Integer

    ''' <summary>
    ''' Fecha del documento que viene de la cabecera
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As DateTime

    ''' <summary>
    ''' Fecha de creación del documento que viene de la cabecera
    ''' </summary>
    Public Property CreationDate As DateTime

    ''' <summary>
    ''' Lista de tarifas iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateIvaXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDsleRateIva.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleRateIva.Properties.DataSource = value
        End Set
    End Property



    ''' <summary>
    ''' Propiedad para los mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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
    ''' propiedad que obtiene o establece el tipo de registro del IVA parametro de empresa se llena desde (cxp)
    ''' del parametro de empresa
    ''' </summary>
    ''' <returns>   1- IVA Costo (Control Fiscal)
    '''             2- IVA Descontable
    '''             3- IVA Mixto
    '''             4- IVA Costo
    '''             NULL - no parametrizado o Cxp en estado Diferente a registrado </returns>
    Private Property TaxRegistration As Byte?
        Get
            Return _taxRegistration
        End Get
        Set(value As Byte?)
            _taxRegistration = value
        End Set
    End Property

#End Region

#Region "Constant"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payments"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Establece la forma como se cierra la calculadora (True=Aceptando, False=Con X o ESC)
    ''' </summary>
    ''' <remarks></remarks>
    Private FlagCloseLiquidator As Boolean = True

    ''' <summary>
    ''' Permite saber si se esta calculando desde el liquidador o no
    ''' </summary>
    ''' <remarks></remarks>
    Private FlagCalculateWithLiquidator As Boolean = False

    ''' <summary>
    ''' Listado de conceptos de pago de tipo retencion que trae la linea de distribucion asociada al proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private listConceptCxpXpo As List(Of Object)

    ''' <summary>
    ''' Variable que contiene la lista con la naturaleza de la cuenta
    ''' </summary>
    Private NatureType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Establece si la entidad viene para modificar o agregar
    ''' </summary>
    ''' <remarks></remarks>
    Public Entity As Boolean

    ''' <summary>
    ''' Obtiene el id del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Public idThird As Integer

    ''' <summary>
    ''' Establece la descripción del tercero en el null text
    ''' </summary>
    ''' <remarks></remarks>
    Public ThirdPartyDescription As String

    ''' <summary>
    ''' Obtiene los conceptos de retencion
    ''' </summary>
    ''' <remarks></remarks>
    Private retentionConcept As RetentionConcepts

    ''' <summary>
    ''' Variable para saber si maneja causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Public _handlesDeferredCausation As Boolean

    ''' <summary>
    ''' Variable para saber si maneja iva descontable
    ''' </summary>
    ''' <remarks></remarks>
    Public hasDeductibleIva As Boolean?

    ''' <summary>
    ''' Variable para saber si maneja iva descontable
    ''' </summary>
    ''' <remarks></remarks>
    Public showDeductibleIva As Boolean

    ''' <summary>
    ''' Variable que obtiene el nombre del form a la cual es llamado el concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private _form As String

    ''' <summary>
    ''' Controla el cambio de valor de los controles search
    ''' </summary>
    ''' <remarks></remarks>
    Public banSearch As Boolean = False

    ''' <summary>
    ''' Representa el tipo de retencion de la cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Private banRetentionType As Integer

    ''' <summary>
    ''' Controla el cambio de valor del control de concepto de cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Dim banSearchCxP As Boolean

    ''' <summary>
    ''' Variable para saber si el concepto de pago maneja o no retencion
    ''' </summary>
    ''' <remarks></remarks>
    Private banHandlesRetentionConceptCxP As Boolean

    ''' <summary>
    ''' Listado de rangos de retenciones 384
    ''' </summary>
    ''' <remarks></remarks>
    Private ListRangeRetention384 As List(Of RetentionConceptRanges)

    ''' <summary>
    ''' Listado de rangos de retenciones 383
    ''' </summary>
    ''' <remarks></remarks>
    Private ListRangeRetention383 As List(Of RetentionConceptRanges)

    ''' <summary>
    ''' Representa a la entidad de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private Supplier As Infrastructure.Data.Xpo.PaymentsRepository.Maintenance_Supplier

    ''' <summary>
    ''' Representa a la entidad de liquidacion de detalles de cxp
    ''' </summary>
    ''' <remarks></remarks>
    Private accountPayableDetailConceptLiquidation As AccountPayableDetailConceptLiquidation

    ''' <summary>
    ''' Permite saber si se viene de edicion desde form popupBills
    ''' </summary>
    ''' <remarks></remarks>
    Public optionEdit As Boolean = False

    ''' <summary>
    ''' Id del concepto de retencion 383
    ''' </summary>
    ''' <remarks></remarks>
    Private RetentionConceptId383 As Integer?

    ''' <summary>
    ''' Codigo y nombre del concepto de retencion 383
    ''' </summary>
    ''' <remarks></remarks>
    Private RetentionConceptDescription383 As String

    ''' <summary>
    ''' Id del concepto de retencion 384
    ''' </summary>
    ''' <remarks></remarks>
    Private RetentionConceptId384 As Integer?

    ''' <summary>
    ''' Codigo y nombre del concepto de retencion 384
    ''' </summary>
    ''' <remarks></remarks>
    Private RetentionConceptDescription384 As String

    ''' <summary>
    ''' Representa a al clone del detalle de la cxp
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableDetailConceptClone As AccountPayableDetailConcept

    ''' <summary>
    ''' datos de la cuenta debito de la tarifa de iva 
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountDebit As Infrastructure.Data.Xpo.PaymentsRepository.GeneralLedgerMainAccountsXpo

    ''' <summary>
    ''' datos de la cuenta credito de la tarifa de iva 
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountCredit As Infrastructure.Data.Xpo.PaymentsRepository.GeneralLedgerMainAccountsXpo

    ''' <summary>
    ''' datos de la cuenta  del iva descontable
    ''' </summary>
    ''' <remarks></remarks>
    Private _acountDescountable As Infrastructure.Data.Xpo.PaymentsRepository.GeneralLedgerMainAccountsXpo

    ''' <summary>
    ''' datos de la cuenta  del concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountConcept As ActionResult(Of AccountPayableConceptNotes)

    ''' <summary>
    ''' datos de la tarifa de iva
    ''' </summary>
    ''' <remarks></remarks>
    Private _generalLedgerIVA As ActionResult(Of GeneralLedgerIVA)

    ''' <summary>
    ''' datos de para el tercero 
    ''' </summary>
    ''' <remarks></remarks>
    Private _thirdParty As ThirdParty

    ''' <summary>
    ''' Variable con la que se redondea los deciamales en las operaciones
    ''' </summary>
    Private decimals As Integer = 2

    ''' <summary>
    ''' variable que contiene el valor tipo de iva de parametro de empresa
    ''' </summary>
    Private _taxRegistration As Byte?

    ''' <summary>
    ''' variable que indica el estado del registro
    ''' </summary>
    Private _NotRegisterStatus As Boolean

#End Region

#Region "Methods"

    ''' <summary>
    ''' Calculates the retention.
    ''' </summary>
    Private Sub CalculateRetention()
        Try
            Dim resulCalculateRetention As Decimal = 0
            If retentionConcept.Retention = 1 Then 'base
                resulCalculateRetention = AccountingServices.CalculateRetention(BaseValue, retentionConcept)
            ElseIf retentionConcept.Retention = 3 Then 'variable
                If Percentage > 0 Then
                    resulCalculateRetention = AccountingServices.CalculateRetention(BaseValue, 0, Percentage)
                End If
            End If

            RetentionValue = resulCalculateRetention.ToString("C2")
        Catch ex As ArgumentNullException
            Mensaje(EeventViewerImages.Advertencia) = ex.ParamName
        Catch ex As ArgumentOutOfRangeException
            Mensaje(EeventViewerImages.Advertencia) = ex.ParamName
        Catch ex As InvalidOperationException
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        Catch ex As IndexOutOfRangeException
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Metodo que carga la informacion de los conceptos de "Cuentas por pagar"
    ''' </summary>
    Private Sub LoadInformationAccountPayable()
        With AccountPayableDetail

            ''se agrega la bandera en true para cuando va un concepto que utiliza la calculadora
            ''este mismo muestre el boton y la abra
            HandlesDeferredCausation = .DeferredCausation
            .HandlesDeferredCausation = False

            IdConceptPayments = .IdConceptAccountPayable
            INDsleConcept.Properties.NullText = .DescriptionPaymentConcept
            banSearch = True
            banSearchCxP = False
            IdAccount = .IdAccount
            INDsleAccount.Properties.NullText = .NumberNameMainAccount
            banSearchCxP = True
            IdThirdParty = .IdThirdParty
            INDsleThirdParty.Properties.NullText = .DescriptionThirdParty

            BaseValue = .BaseValue
            If .RateIva > 0 Then
                HidenControlIVA(True)
                INDsleRateIva.Properties.NullText = .NameIVA
                IdGeneralLedgerIVA = .RateIva
                IVAValue = .IvaValue
            Else
                HidenControlIVA(False)
            End If
            Me.TotalConceptValue = .TotalConcept

            If .IdCostCenter IsNot Nothing Then
                IdCostCenter = .IdCostCenter
                INDsleCostCenter.Properties.NullText = .DescriptionCostCenter
                INDlyItemCostCenter.ShowLayout()
            Else
                INDsleCostCenter.Properties.NullText = String.Empty
                INDlyItemCostCenter.HideLayout()
            End If

            If .IdRetentionConcept IsNot Nothing Then

                If accountPayableDetailConceptLiquidation IsNot Nothing Then
                    FlagCalculateWithLiquidator = True
                End If

                IdConceptRetention = .IdRetentionConcept
                INDsleConceptRetention.Properties.NullText = .DescriptionRetentionConcept
                Percentage = .Percentage
                RetentionValue = .Value
                INDlygConceptRetention.HideControl(False)

                If .BillingValue > 0 AndAlso accountPayableDetailConceptLiquidation Is Nothing Then
                    INDlyItemInvoicedValue.ShowLayout()
                    InvoicedValue = .BillingValue
                End If

                FlagCalculateWithLiquidator = False
            Else
                INDlygConceptRetention.HideControl()
            End If

            Comments = .Detail
            Nature = .Nature
        End With
    End Sub

    ''' <summary>
    ''' Metodo que carga la informacion de los conceptos de "Notas Debito/Credito"
    ''' </summary>
    Private Sub LoadInformationPaymentNote()
        With PaymentNoteDetail
            IdConceptNotes = .IdAccountPayableConceptNotes
            INDsleConceptNote.Properties.NullText = .DescriptionNoteConcept
            banSearch = True
            banSearchCxP = False
            IdAccount = .IdAccount
            INDsleAccount.Properties.NullText = .NumberNameMainAccount
            banSearchCxP = True
            IdThirdParty = .IdThirdParty
            INDsleThirdParty.Properties.NullText = .DescriptionThirdParty

            If .IdCostCenter IsNot Nothing Then
                IdCostCenter = CInt(.IdCostCenter)
                INDsleCostCenter.Properties.NullText = .DescriptionCostCenter
                INDlyItemCostCenter.ShowLayout()
            Else
                INDsleCostCenter.Properties.NullText = String.Empty
                INDlyItemCostCenter.HideLayout()
            End If

            ''esta validacion cuenta para los 3 tipos de conceptos, ya sea un concepto que no maneje ni retencion o iva, un concepto que solo maneje retencion o un copncepto que maneje solo iva
            ''en esta primera parte se valida si es un concepto que maneja retencion
            If .IdRetentionConcept IsNot Nothing Then
                ''oculto los campos del iva
                HidenControlIVA(False)
                IdConceptRetention = .IdRetentionConcept
                Using model As New MRetentionConcept("")
                    Dim retention As RetentionConcepts = model.GetRetentionByIdSimple(IdConceptRetention)
                    INDsleConceptRetention.Properties.NullText = retention.Code + " - " + retention.Name
                End Using

                Percentage = .Percentage
                RetentionValue = .Value
                INDlygConceptRetention.HideControl(False)

                If .BillingValue > 0 Then
                    INDlyItemInvoicedValue.ShowLayout()
                    InvoicedValue = .BillingValue
                End If
            Else
                INDlygConceptRetention.HideControl()

                ''si el registro tiene una tarifa de iva es porque maneja un concepto de iva ademas se valida que tenga el id del tercero
                If .IdGeneralLedgerIVA IsNot Nothing AndAlso idThird > 0 Then
                    LoadDataSourceGeneralLedgerIVA()
                    ''se valida si se deben mostrar o no los campos de iva 
                    If ValidateControlIva() Then
                        IdGeneralLedgerIVA = .IdGeneralLedgerIVA
                        IVAValue = .IVAValue
                        TotalConceptValue = .TotalConceptValue
                    End If

                Else
                    ''si el concepto no maneja ni retencion ni iva oculto los campos de iva y ademas le asigno el total value 
                    HidenControlIVA(False)
                    TotalConceptValue = .TotalConceptValue
                End If
            End If

            Comments = .Comments
            Nature = .Nature
            BaseValue = .BaseValue

            If .HandlesMainAccountByConcept = 1 Then
                INDsleAccount.Properties.ReadOnly = False
            Else
                INDsleAccount.Properties.ReadOnly = True
            End If
        End With
    End Sub

    Public Sub LoadDataSourceGeneralLedgerIVA()
        If INDsleRateIva.Properties.DataSource Is Nothing Then
            InitializeRateIva()
        End If
    End Sub
    ''' <summary>
    ''' Método para cargar la información en los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UploadingInformation()
        If _form = "AccountPayable" Then
            LoadInformationAccountPayable()
        ElseIf _form = "PaymentNote" Then
            LoadInformationPaymentNote()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para agregar un concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddConcept()

        If _form = "PaymentNote" AndAlso INDlyItemRateIva.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always _
                        AndAlso (Me.TaxRegistration Is Nothing OrElse Me.TaxRegistration = 0) Then

            Mensaje(EeventViewerImages.Advertencia) = "El registro de IVA está vacío. Verifique que se hayan agregado facturas en el segmento correspondiente."
            Exit Sub
        End If

        If ValidateFields() Then
            AssigningValues()

            If optionEdit Then
                Entity = True
            Else
                Entity = False
            End If

            RaiseEvent ClosePopup(Nothing, EventArgs.Empty)
            banSearch = True
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Faltan Campos por Diligenciar."
        End If
    End Sub

    ''' <summary>
    ''' Valida que los controles esten correctamente diligenciados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateFields() As Boolean

        If INDlyItemConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If IdConceptPayments Is Nothing Then
                Return False
            End If

        ElseIf INDlyItemConceptNote.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If IdConceptNotes Is Nothing Then
                Return False
            End If
        End If
        If INDlyItemConceptRetention.Visible Then
            If IdConceptRetention Is Nothing Then
                Return False
            End If
        End If

        If IdAccount Is Nothing Then
            Return False
        End If

        If IdThirdParty Is Nothing Then
            Return False
        End If

        If Nature = 0 Then
            Return False
        End If

        If BaseValue = 0 Then
            Return False
        End If

        If INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleCostCenter.Enabled = True Then
                If IdCostCenter Is Nothing Then
                    Return False
                End If
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' Asigna los valores para el concepto de una cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()

        If _form = "AccountPayable" Then
            With AccountPayableDetail
                .IdConceptAccountPayable = IdConceptPayments
                .DescriptionPaymentConcept = INDsleConcept.Text
                .IdAccount = IdAccount
                .NumberNameMainAccount = INDsleAccount.Text
                .IdThirdParty = IdThirdParty
                .DescriptionThirdParty = INDsleThirdParty.Text
                .DeferredCausation = HandlesDeferredCausation
                .HandlesDeferredCausation = HandlesDeferredCausation

                If INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .IdCostCenter = IdCostCenter
                    .DescriptionCostCenter = INDsleCostCenter.Text
                Else
                    .IdCostCenter = Nothing
                    .DescriptionCostCenter = String.Empty
                    AccountPayableDetail.CostCenter = Nothing
                End If

                .Detail = Comments

                If INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .IdRetentionConcept = IdConceptRetention
                    .Percentage = Percentage
                    .DescriptionRetentionConcept = INDsleConceptRetention.Text
                    .Value = RetentionValue
                    If INDlyItemInvoicedValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        .BillingValue = InvoicedValue
                    Else
                        .BillingValue = 0
                    End If
                Else
                    .DescriptionRetentionConcept = String.Empty
                    .IdRetentionConcept = Nothing
                    .Value = BaseValue
                    .BillingValue = 0
                End If

                .Nature = CByte(Nature)
                .BaseValue = Me.BaseValue
                .RateIva = IdGeneralLedgerIVA
                .NameIVA = INDsleRateIva.Text
                .IvaValue = IVAValue
                .TotalConcept = Me.TotalConceptValue
                .AccountPayabbleDetailChild = New List(Of AccountPayableDetailConcept)

                If accountPayableDetailConceptLiquidation IsNot Nothing Then
                    .BillingValue = accountPayableDetailConceptLiquidation.TotalIncome
                    If .AccountPayableDetailConceptLiquidation.Count = 0 Then
                        .AccountPayableDetailConceptLiquidation.Add(accountPayableDetailConceptLiquidation)
                    Else
                        With .AccountPayableDetailConceptLiquidation(0)
                            'Ingresos
                            .TotalIncome = accountPayableDetailConceptLiquidation.TotalIncome
                            'Ingresos no constitutivos de renta ni ganancia ocasional
                            .PaymentCompulsoryHealth = accountPayableDetailConceptLiquidation.PaymentCompulsoryHealth
                            .PaymentCompulsoryHealthReal = accountPayableDetailConceptLiquidation.PaymentCompulsoryHealthReal
                            .PensionFundContribution = accountPayableDetailConceptLiquidation.PensionFundContribution
                            .PensionFundContributionReal = accountPayableDetailConceptLiquidation.PensionFundContributionReal
                            .SolidarityPensionFund = accountPayableDetailConceptLiquidation.SolidarityPensionFund
                            .SolidarityPensionFundReal = accountPayableDetailConceptLiquidation.SolidarityPensionFundReal
                            .PensionByIndividualSavingsRegime = accountPayableDetailConceptLiquidation.PensionByIndividualSavingsRegime
                            .PensionByIndividualSavingsRegimeReal = accountPayableDetailConceptLiquidation.PensionByIndividualSavingsRegimeReal
                            'Deducciones
                            .HousingLoanInterest = accountPayableDetailConceptLiquidation.HousingLoanInterest
                            .HousingLoanInterestReal = accountPayableDetailConceptLiquidation.HousingLoanInterestReal
                            .PaymentForDependent = accountPayableDetailConceptLiquidation.PaymentForDependent
                            .PaymentForDependentReal = accountPayableDetailConceptLiquidation.PaymentForDependentReal
                            .PaymentPrepaidMedical = accountPayableDetailConceptLiquidation.PaymentPrepaidMedical
                            .PaymentPrepaidMedicalReal = accountPayableDetailConceptLiquidation.PaymentPrepaidMedicalReal
                            .OccupationalRiskContribution = accountPayableDetailConceptLiquidation.OccupationalRiskContribution
                            .OccupationalRiskContributionReal = accountPayableDetailConceptLiquidation.OccupationalRiskContributionReal
                            'Total deducciones
                            .TotalDeduction = accountPayableDetailConceptLiquidation.TotalDeduction
                            .TotalDeductionReal = accountPayableDetailConceptLiquidation.TotalDeductionReal
                            'Rentas Exentas
                            .VoluntaryPensionFundContribution = accountPayableDetailConceptLiquidation.VoluntaryPensionFundContribution
                            .VoluntaryPensionFundContributionReal = accountPayableDetailConceptLiquidation.VoluntaryPensionFundContributionReal
                            .ContributionAccountAFC = accountPayableDetailConceptLiquidation.ContributionAccountAFC
                            .ContributionAccountAFCReal = accountPayableDetailConceptLiquidation.ContributionAccountAFCReal
                            'Total Rentas exentas
                            .TotalIncomeExempt = accountPayableDetailConceptLiquidation.TotalIncomeExempt
                            .TotalIncomeExemptReal = accountPayableDetailConceptLiquidation.TotalIncomeExemptReal
                            'Renta Exenta del 25%
                            .ExemptIncome = accountPayableDetailConceptLiquidation.ExemptIncome
                            'SubTotal antes de validar el tope de deducciones y rentas exentas
                            .SubTotal = accountPayableDetailConceptLiquidation.SubTotal
                            'Valor máximo que podrá restarse por rentas exentas y deducciones
                            .MaxDeductionsAndRentExents = accountPayableDetailConceptLiquidation.MaxDeductionsAndRentExents
                            'Base gravable
                            .TaxableBase = accountPayableDetailConceptLiquidation.TaxableBase
                            'Valor Retenido
                            .ApplyRetention = accountPayableDetailConceptLiquidation.ApplyRetention
                            .RetentionValue383 = accountPayableDetailConceptLiquidation.RetentionValue383
                            .RetentionValue384 = accountPayableDetailConceptLiquidation.RetentionValue384
                            'Valores necesarios para los calculos
                            .UVT = accountPayableDetailConceptLiquidation.UVT
                            .SMLV = accountPayableDetailConceptLiquidation.SMLV
                        End With
                    End If

                    .RetentionConceptId383 = RetentionConceptId383
                    .RetentionConceptDescription383 = RetentionConceptDescription383
                    .RetentionConceptId384 = RetentionConceptId384
                    .RetentionConceptDescription384 = RetentionConceptDescription384
                End If

                ''En caso de que tenga iva
                If AccountPayableDetail.RateIva IsNot Nothing Then
                    ''Se actualiza el total para que no tome el value sino el total del concepto
                    .Value = .TotalConcept

                    If TaxRegistration IsNot Nothing Then

                        If TaxRegistration = 2 OrElse (TaxRegistration = 3 AndAlso DiscountableIVA) Then 'IVA descontable / IVA mixto e IVA descontable
                            Using modelGeneral As New MAccountPayable(Me.Tag)
                                Dim accountInfo = modelGeneral.GetMainAccountById(_generalLedgerIVA.ObjectEmbbeded.IdAccountPurchaseService)

                                .AccountPayabbleDetailChild.Add(New AccountPayableDetailConcept With {
                                        .Value = Me.IVAValue,
                                        .IdAccount = _generalLedgerIVA.ObjectEmbbeded.IdAccountPurchaseService,
                                        .DescriptionPaymentConcept = INDsleConcept.Text,
                                        .Nature = 1,
                                        .NumberNameMainAccount = accountInfo.NumberName,
                                        .Detail = $"IVA {_generalLedgerIVA.ObjectEmbbeded.Percentage} % - {_generalLedgerIVA.ObjectEmbbeded.Name}"
                                    })
                            End Using

                        ElseIf Me.TaxRegistration = 1 OrElse (TaxRegistration = 3 AndAlso Not DiscountableIVA) Then 'IVA al costo - control fiscal / IVA mixto con IVA fiscal seleccionada

                            'Item Debito
                            Using modelGeneral As New MAccountPayable(Me.Tag)
                                Dim accountInfo = modelGeneral.GetMainAccountById(_generalLedgerIVA.ObjectEmbbeded.IdAccountDebitControlFiscal)

                                .AccountPayabbleDetailChild.Add(New AccountPayableDetailConcept With {
                                        .Value = Me.IVAValue,
                                        .IdAccount = _generalLedgerIVA.ObjectEmbbeded.IdAccountDebitControlFiscal,
                                        .DescriptionPaymentConcept = INDsleConcept.Text,
                                        .Nature = 1,
                                        .NumberNameMainAccount = accountInfo.NumberName,
                                        .Detail = $"IVA {_generalLedgerIVA.ObjectEmbbeded.Percentage} % - {_generalLedgerIVA.ObjectEmbbeded.Name}"
                                    })
                            End Using

                            'Item Credito
                            Using modelGeneral As New MAccountPayable(Me.Tag)
                                Dim accountInfo = modelGeneral.GetMainAccountById(_generalLedgerIVA.ObjectEmbbeded.IdAccountCreditControlFiscal)


                                .AccountPayabbleDetailChild.Add(New AccountPayableDetailConcept With {
                                        .Value = Me.IVAValue,
                                        .IdAccount = _generalLedgerIVA.ObjectEmbbeded.IdAccountCreditControlFiscal,
                                        .DescriptionPaymentConcept = INDsleConcept.Text,
                                        .Nature = 2,
                                        .NumberNameMainAccount = accountInfo.NumberName,
                                        .Detail = $"IVA {_generalLedgerIVA.ObjectEmbbeded.Percentage} % - {_generalLedgerIVA.ObjectEmbbeded.Name}"
                                    })
                            End Using

                        ElseIf Me.TaxRegistration = 4 Then 'IVA al costo
                            .AccountPayabbleDetailChild.Add(New AccountPayableDetailConcept With {
                                    .Value = Me.IVAValue,
                                    .IdAccount = IdAccount,
                                    .DescriptionCostCenter = .DescriptionCostCenter,
                                    .DescriptionPaymentConcept = INDsleConcept.Text,
                                    .NumberNameMainAccount = INDsleAccount.Text,
                                    .Nature = Nature,
                                    .Detail = $"IVA {_generalLedgerIVA.ObjectEmbbeded.Percentage} % - {_generalLedgerIVA.ObjectEmbbeded.Name}"
                                })
                        End If
                    End If
                End If

                'se agrega el row regular
                Dim row As AccountPayableDetailConcept = New AccountPayableDetailConcept
                If INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    row.Value = RetentionValue
                Else
                    If Me.TaxRegistration = 1 OrElse (TaxRegistration = 3 AndAlso Not DiscountableIVA) Then 'IVA al costo - control fiscal / IVA mixto con IVA fiscal seleccionada
                        row.Value = .TotalConcept
                    Else
                        row.Value = Me.BaseValue
                    End If
                End If

                row.IdAccount = IdAccount
                row.DescriptionCostCenter = .DescriptionCostCenter
                row.NumberNameMainAccount = INDsleAccount.Text
                row.Nature = Nature
                .AccountPayabbleDetailChild.Add(row)
            End With

            ValidateRecalculateDeferredCausation()

        ElseIf _form = "PaymentNote" Then
            With PaymentNoteDetail
                .DiscountableIVA = DiscountableIVA
                .TaxRegistration = TaxRegistration
                .IdAccountPayableConceptNotes = IdConceptNotes
                .DescriptionNoteConcept = INDsleConceptNote.Text
                .IdAccount = IdAccount
                .NumberNameMainAccount = INDsleAccount.Text
                .IdThirdParty = IdThirdParty
                .DescriptionThirdParty = INDsleThirdParty.Text

                If INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .IdCostCenter = IdCostCenter
                    .DescriptionCostCenter = INDsleCostCenter.Text
                Else
                    .DescriptionCostCenter = String.Empty
                    .CostCenter = Nothing
                End If

                .Comments = Comments
                If INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .IdRetentionConcept = IdConceptRetention
                    .Percentage = Percentage
                    .DescriptionRetentionConcept = INDsleConceptRetention.Text
                    .Value = RetentionValue

                    ''cuando el concepto maneja retencion el valor  del concepto es el valor total que se obtiene del campo valor retencion
                    .TotalConceptValue = RetentionValue
                    If INDlyItemInvoicedValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        .BillingValue = InvoicedValue
                    Else
                        .BillingValue = Me.BaseValue
                    End If

                Else
                    .DescriptionRetentionConcept = String.Empty
                    .IdRetentionConcept = Nothing
                    .Value = BaseValue
					.BillingValue = 0
					.IdGeneralLedgerIVA = IdGeneralLedgerIVA
					.IVAValue = IVAValue

					'validacion si existe una tarifa de iva se llenan los datos para activar la child list
					If IdGeneralLedgerIVA IsNot Nothing Then
						.TotalConceptValue = Me.TotalConceptValue

						PaymentNoteDetail.PaymentsNoteDetailsAccountInfo.Clear()
                        Dim NatureName = IIf(Nature = 1, "Debito", "Credito")
                        Dim Observations = $"IVA {_generalLedgerIVA.ObjectEmbbeded.Percentage} % - {_generalLedgerIVA.ObjectEmbbeded.Name}"

                        'Concepto
                        PaymentNoteDetail.PaymentsNoteDetailsAccountInfo.Add(New PaymentsNoteDetailsAccountInfo With {
                            .MainAccountCodeName = INDsleAccount.Text,
                            .CostCenterCodeName = INDsleCostCenter.Text,
                            .NatureName = NatureName,
                            .ValueDetailConcept = Me.BaseValue
                        })

                        'Tarifa de IVA
                        If TaxRegistration = 2 Then 'IVA descontable
                            Using Model As New MAccountPayable(Me.Tag)
                                _acountDescountable = Model.GetMainAccountById(_generalLedgerIVA?.ObjectEmbbeded?.IdAccountPurchaseService)
                            End Using

                            PaymentNoteDetail.PaymentsNoteDetailsAccountInfo.Add(New PaymentsNoteDetailsAccountInfo With {
                                .MainAccountCodeName = _acountDescountable.NumberName,
                                .CostCenterCodeName = "",
                                .Observations = Observations,
                                .NatureName = NatureName,
                                .ValueDetailConcept = IVAValue
                            })

                        ElseIf TaxRegistration = 1 Then 'IVA al costo - control fiscal
                            Using Model As New MAccountPayable(Me.Tag)
                                _accountDebit = Model.GetMainAccountById(_generalLedgerIVA?.ObjectEmbbeded?.IdAccountDebitControlFiscal)
                                _accountCredit = Model.GetMainAccountById(_generalLedgerIVA?.ObjectEmbbeded?.IdAccountCreditControlFiscal)
                            End Using

                            PaymentNoteDetail.PaymentsNoteDetailsAccountInfo.Add(New PaymentsNoteDetailsAccountInfo With {
                                .MainAccountCodeName = _accountDebit.NumberName,
                                .CostCenterCodeName = "",
                                .NatureName = NatureName,
                                .ValueDetailConcept = IVAValue,
                                .Observations = Observations
                            })

                            PaymentNoteDetail.PaymentsNoteDetailsAccountInfo.Add(New PaymentsNoteDetailsAccountInfo With {
                                .MainAccountCodeName = _accountCredit.NumberName,
                                .CostCenterCodeName = "",
                                .NatureName = IIf(Nature = 1, "Credito", "Debito"),
                                .ValueDetailConcept = IVAValue,
                                .Observations = Observations
                            })

                        ElseIf TaxRegistration = 4 Then 'IVA al costo
                            PaymentNoteDetail.PaymentsNoteDetailsAccountInfo.Add(New PaymentsNoteDetailsAccountInfo With {
                                .MainAccountCodeName = INDsleAccount.Text,
                                .CostCenterCodeName = INDsleCostCenter.Text,
                                .NatureName = NatureName,
                                .ValueDetailConcept = IVAValue,
                                .Observations = Observations
                            })

                        End If
                    Else
						.TotalConceptValue = Math.Round(Me.BaseValue + If(Me.IVAValue, 0), decimals)
					End If
                End If

                .Nature = CByte(Nature)
                .BaseValue = Me.BaseValue

                If INDsleAccount.Properties.ReadOnly Then
                    .HandlesMainAccountByConcept = 2
                Else
                    .HandlesMainAccountByConcept = 1
                End If
            End With
        End If
    End Sub

    ''' <summary>
    ''' Compara el clone con la entidad de detalle para saber si se modifico y se recalcula
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateRecalculateDeferredCausation()
        Dim val As Boolean
        If HandlesDeferredCausation Then
            Select Case True
                Case _accountPayableDetailConceptClone.Nature <> AccountPayableDetail.Nature
                    val = True
                Case _accountPayableDetailConceptClone.IdAccount <> AccountPayableDetail.IdAccount
                    val = True
                Case _accountPayableDetailConceptClone.IdCostCenter <> AccountPayableDetail.IdCostCenter
                    val = True
                Case _accountPayableDetailConceptClone.Value <> AccountPayableDetail.Value
                    val = True
                Case Else
                    val = False
            End Select
        Else
            val = False
        End If
        AccountPayableDetail.HandlesDeferredCausation = val
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControls()
        If INDlyItemConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            IdConceptPayments = Nothing

            If INDsleConcept.Properties.Buttons.Count = 3 OrElse INDsleConcept.Properties.Buttons.Count = 2 Then
                Dim info = (From item As DevExpress.XtraEditors.Controls.EditorButton In INDsleConcept.Properties.Buttons Where item.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph).FirstOrDefault
                If info IsNot Nothing Then
                    INDsleConcept.Properties.Buttons.RemoveAt(info.Index)
                End If
            End If

            INDsleConcept.Properties.ReadOnly = False
            accountPayableDetailConceptLiquidation = Nothing
            INDsleConcept.Focus()

        ElseIf INDlyItemConceptNote.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            IdConceptNotes = Nothing
            INDsleConceptNote.Focus()
        End If

        INDsleConceptRetention.Properties.ReadOnly = False
        INDtxtBaseValue.Properties.ReadOnly = False
        optionEdit = False
        banSearch = False
        IdAccount = Nothing
        INDsleAccount.Properties.ReadOnly = True
        IdThirdParty = idThird
        INDsleThirdParty.Properties.NullText = ThirdPartyDescription
        INDsleThirdParty.Properties.ReadOnly = False
        IdCostCenter = Nothing
        Comments = String.Empty
        IdConceptRetention = Nothing
        Percentage = 0
        Nature = Nothing
        BaseValue = 0
        InvoicedValue = 0
        RetentionValue = 0
        INDlyItemCostCenter.HideLayout()
        INDlygConceptRetention.HideControl()
        INDtxtIvaValue.Properties.NullText = String.Empty
        INDsleConcept.Properties.NullText = String.Empty
        INDsleConceptNote.Properties.NullText = String.Empty
        INDsleCostCenter.Properties.NullText = String.Empty
        INDsleConceptRetention.Properties.NullText = String.Empty
        INDsleAccount.Properties.NullText = String.Empty
        IdGeneralLedgerIVA = Nothing
        INDsleRateIva.Properties.NullText = String.Empty
        IVAValue = 0
        TotalConceptValue = Nothing
        retentionConcept = Nothing
    End Sub

    ''' <summary>
    ''' Activa o desactiva los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)

            INDsleConcept.Enabled = value
            INDsleThirdParty.Enabled = value
            INDsleAccount.Enabled = value
            INDsleRateIva.Enabled = value
            INDmemoComments.Enabled = value
            INDgleNature.Enabled = value
            INDtxtBaseValue.Enabled = value
            INDbtnAdd.Enabled = value
            INDbtnCancel.Enabled = value
            INDsleConceptRetention.Enabled = value
            INDtxtTotalConcept.Enabled = value
            INDtxtIvaValue.Enabled = value
            INDDiscountableIVA.Enabled = value
            INDtxtInvoicedValue.Enabled = value

            If value Then
                INDsleAccount.Focus()
            Else
                INDsleConcept.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Llena el control con el listado de la naturaleza de la cuenta
    ''' </summary>
    Private Sub CreateNature()
        NatureType = New List(Of Tuple(Of Integer, String))
        NatureType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("AccountNatureDebit")))
        NatureType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("AccountNatureCredit")))
        INDgleNature.Properties.DataSource = NatureType.ToList()
    End Sub

    ''' <summary>
    ''' Metodo para cargar de informacion los combos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeThirdParty()
        Using msearch As New MBusqueda
            ThirdPartyListXpo = CType(msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ThirdParty), XPInstantFeedbackSource)
            CreateNature()
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar de informacion los combos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeRateIva()
        Using msearch As New MBusqueda
            RateIvaXpo = CType(msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListGeneralLedgerIva), XPInstantFeedbackSource)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar la informacion del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub getDataThirdParty(IdThirdPartySupplier)
        Using Model As New MThirdParty(Me.Tag)
            _thirdParty = Model.GetThirdPartyByIdSimple(IdThirdPartySupplier)
        End Using
    End Sub

    ''' <summary>
    ''' Funcion para validar por si se deben mostrar los campos del iva dependiendo de si el tercero es responsable de iva 
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateControlIva()
        ''se obtiene la informacion del tercero
        getDataThirdParty(idThird)

        If _thirdParty.PersonType = 1 And _thirdParty.ContributionType = 0 Then
            HidenControlIVA(False)
            Return False
        Else
            HidenControlIVA(True)
            Return True
        End If
    End Function

    ''' <summary>
    ''' Metodo que se ejecuta cuando el CtrConcepts se despliega desde "Cuentas por pagar"
    ''' </summary>
    Private Sub LoadAccountPayable(_accountPayableDetail As AccountPayableDetailConcept,
                                     _supplier As Infrastructure.Data.Xpo.PaymentsRepository.Maintenance_Supplier,
                                     _showDeductibleIva As Boolean,
                                     _hasDeductibleIva As Boolean,
                                     _list As List(Of Object),
                                     _taxRegistration As Byte?)

        If _NotRegisterStatus Then
            ActionsOnControls = False
            AccountPayableDetail = _accountPayableDetail
            UploadingInformation()
            Exit Sub
        End If

        Supplier = _supplier
        showDeductibleIva = _showDeductibleIva
        hasDeductibleIva = _hasDeductibleIva
        listConceptCxpXpo = _list
        Me.TaxRegistration = _taxRegistration
        INDlyItemConcept.ShowLayout()
        INDlyItemConceptNote.HideLayout()
        INDliDiscountableIVA.HideLayout()

        If _accountPayableDetail Is Nothing Then
            banSearch = True
            AccountPayableDetail = New AccountPayableDetailConcept
            RegisterAdd()

            If ValidateControlIva() Then
                If _accountPayableConcepts IsNot Nothing AndAlso hasDeductibleIva IsNot Nothing Then
                    INDlyItemRateIva.Visibility = If(hasDeductibleIva.HasValue(), If(_accountPayableConcepts.HandleTaxes AndAlso Not _accountPayableConcepts.HandlesRetention, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never), DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                    INDlyItemIvaValue.Visibility = If(hasDeductibleIva.HasValue(), If(_accountPayableConcepts.HandleTaxes AndAlso Not _accountPayableConcepts.HandlesRetention, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never), DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                    INDlyItemTotalConcept.Visibility = If(hasDeductibleIva.HasValue(), If(_accountPayableConcepts.HandleTaxes AndAlso Not _accountPayableConcepts.HandlesRetention, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never), DevExpress.XtraLayout.Utils.LayoutVisibility.Never)

                    INDlyItemIvaValue.Enabled = False
                    INDlyItemTotalConcept.Enabled = False
                End If
            End If
        Else
            If _accountPayableDetail.AccountPayableDetailConceptLiquidation IsNot Nothing AndAlso _accountPayableDetail.AccountPayableDetailConceptLiquidation.Any() Then
                accountPayableDetailConceptLiquidation = _accountPayableDetail.AccountPayableDetailConceptLiquidation(0).CloneEntity()

                RetentionConceptId383 = _accountPayableDetail.RetentionConceptId383
                RetentionConceptDescription383 = _accountPayableDetail.RetentionConceptDescription383
                RetentionConceptId384 = _accountPayableDetail.RetentionConceptId384
                RetentionConceptDescription384 = _accountPayableDetail.RetentionConceptDescription384

                'Se obtiene la info de si ya existe un botón de calculadora
                Dim info = (From item As DevExpress.XtraEditors.Controls.EditorButton In INDsleConcept.Properties.Buttons Where item.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph).FirstOrDefault
                If info Is Nothing Then 'Si no existe un boton de calculadora se crea uno nuevo
                    Dim editor As New DevExpress.XtraEditors.Controls.EditorButton
                    editor.Image = Presentation.Payments.My.Resources.Resources.Calculadora_16x16_01
                    editor.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
                    editor.Visible = True
                    INDsleConcept.Properties.Buttons.Add(editor)
                End If
            End If

            AccountPayableDetail = _accountPayableDetail
            RegisterModify()
        End If

        'Le asigno a la entidad del clone el detalle real
        _accountPayableDetailConceptClone = AccountPayableDetail.CloneEntity()

        If accountPayableDetailConceptLiquidation IsNot Nothing Then
            INDsleConcept.Properties.ReadOnly = True
            If INDsleConcept.Properties.Buttons.Count = 3 Then
                INDsleConcept.Properties.Buttons(2).Visible = True
            ElseIf INDsleConcept.Properties.Buttons.Count > 1 Then
                INDsleConcept.Properties.Buttons(1).Visible = True
            End If

            INDtxtBaseValue.Properties.ReadOnly = True
            INDsleConceptRetention.Properties.ReadOnly = True
        Else
            INDsleConcept.Properties.ReadOnly = False
        End If

        INDsleConcept.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que se ejecuta cuando el CtrConcepts se despliega desde "Notas Debito/Credito" del modulo de cuentas por pagar
    ''' </summary>
    Private Sub LoadPaymentNote(_paymentsNoteDetailtmp As PaymentsNoteDetails, Optional ByVal ListAccountPayable As List(Of AccountPayable) = Nothing,
                                Optional ByVal _Nature As Integer = Nothing)

        ' Validación para modo visualización cuando la nota está confirmada o anulada
        If _NotRegisterStatus Then
            INDlyItemConcept.HideLayout()
            INDlyItemConceptNote.ShowLayout()
            PaymentNoteDetail = _paymentsNoteDetailtmp
            LoadInformationPaymentNote()
            INDbtnAdd.Enabled = False
            INDbtnCancel.Enabled = False
            Exit Sub
        End If

        INDlyItemConcept.HideLayout()
        INDlyItemConceptNote.ShowLayout()

        INDtxtIvaValue.ReadOnly = True
        INDtxtTotalConcept.ReadOnly = True

        'Se valida si las facturas tienen un tipo de registro de IVA
        If ListAccountPayable?.Any(Function(x) x.TaxRegistration IsNot Nothing) Then
            If ListAccountPayable.All(Function(x) x.TaxRegistration IsNot Nothing And x.TaxRegistration = 2) Then
                Me.DiscountableIVA = True
                Me.TaxRegistration = 2
                INDDiscountableIVA.Enabled = False

            ElseIf ListAccountPayable.All(Function(x) x.TaxRegistration IsNot Nothing And x.TaxRegistration = 1) Then
                Me.DiscountableIVA = False
                Me.TaxRegistration = 1
                INDDiscountableIVA.Enabled = False

            ElseIf ListAccountPayable.All(Function(x) x.TaxRegistration IsNot Nothing And x.TaxRegistration = 4) Then
                Me.DiscountableIVA = False
                Me.TaxRegistration = 4
                INDDiscountableIVA.Enabled = False
            End If
        Else
            Me.TaxRegistration = Nothing
            INDliDiscountableIVA.HideLayout()
        End If

        If IdGeneralLedgerIVA IsNot Nothing AndAlso idThird > 0 Then
            ValidateControlIva()
        End If

        If _paymentsNoteDetailtmp Is Nothing Then
            banSearch = True
            PaymentNoteDetail = New PaymentsNoteDetails
            RegisterAdd()

            If _Nature = 1 Then
                Nature = 2
            ElseIf _Nature = 2 Then
                Nature = 1
            End If
        Else
            PaymentNoteDetail = _paymentsNoteDetailtmp
            RegisterModify()
        End If

        INDsleConceptNote.Focus()
    End Sub

    ''' <summary>
    ''' Metodo para inicializar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeControler(_accountPayableDetail As AccountPayableDetailConcept,
                                   _paymentsNoteDetail As PaymentsNoteDetails,
                                   form As String,
                                    Optional ByVal Nature As Integer = Nothing,
                                    Optional ByVal list As List(Of Object) = Nothing, Optional ByVal _supplier As Infrastructure.Data.Xpo.PaymentsRepository.Maintenance_Supplier = Nothing,
                                    Optional ByVal _showDeductibleIva As Boolean = Nothing, Optional ByVal _hasDeductibleIva As Boolean = Nothing,
                                    Optional ByVal ListAccountPayable As List(Of AccountPayable) = Nothing, Optional ByVal CurrencyAbbreviation As String = Nothing,
                                    Optional ByVal taxRegistration As Byte? = Nothing, Optional ByVal ConfirmStatus As Boolean = False)

        _form = form
        _NotRegisterStatus = ConfirmStatus
        CreateNature()
        Me.SetCurrencyUI(CurrencyAbbreviation)

        If form = "AccountPayable" Then
            LoadAccountPayable(_accountPayableDetail, _supplier, _showDeductibleIva, _hasDeductibleIva, list, taxRegistration)
        ElseIf form = "PaymentNote" Then
            LoadPaymentNote(_paymentsNoteDetail, ListAccountPayable, Nature)
        End If
    End Sub

    ''' <summary>
    ''' funcion para ocultar o mostrar los campos del iva 
    ''' </summary>
    ''' <param name="type"></param>
    Public Sub HidenControlIVA(type As Boolean)
        If type Then
            INDlyItemRateIva.ShowLayout()
            INDlyItemIvaValue.ShowLayout()
            INDlyItemTotalConcept.ShowLayout()

            If showDeductibleIva Then
                INDliDiscountableIVA.ShowLayout()
            End If
        Else
            INDlyItemRateIva.HideLayout()
            INDlyItemIvaValue.HideLayout()
            INDlyItemTotalConcept.HideLayout()
			INDliDiscountableIVA.HideLayout()
			IdGeneralLedgerIVA = Nothing
			IVAValue = Nothing
			DiscountableIVA = Nothing
		End If
    End Sub

    ''' <summary>
    ''' Cuando el registro es para agregar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RegisterAdd()
        IdThirdParty = idThird
        INDsleThirdParty.Properties.NullText = ThirdPartyDescription
        Entity = False
        INDbtnAdd.Text = "Agregar"
        INDbtnCancel.Enabled = True
    End Sub

    ''' <summary>
    ''' Cuando el registro es para modificar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RegisterModify()
        If AccountPayableDetail IsNot Nothing Then

            ''se valida si el concepto usa la calculadora para que cuando esta misma se
            ''cierra los datos queden cargados en el popup
            Dim presenter = New PAccountPayable()
            Dim accountPayableConceptXpo = presenter.GetAccountPayableConceptById(AccountPayableDetail.IdConceptAccountPayable)

            If accountPayableConceptXpo.EmployeeCategoryRetention Then
                Entity = False
            Else
                Entity = True
            End If
        Else
            optionEdit = True
        End If

        UploadingInformation()
        INDbtnAdd.Text = "Editar"
        INDbtnCancel.Enabled = False
    End Sub

    ''' <summary>
    ''' Metodo que calcula la retencion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateCalculateRetention()
        If retentionConcept IsNot Nothing AndAlso BaseValue > 0 Then
            CalculateRetention()
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeConcept()
        Using model As New MAccountPayable("")
            ConceptPaymentsListXpo = model.InitializeAccountPayableCOncepts(True, False)
        End Using

        Dim listPivot As New List(Of Object)
        Dim filterPivot() As Object = {True, True}
        Dim list As XPCollection
        Using model As New MAccountPayable("")
            list = model.InitializeAccountPayableCOncepts(True, True, ConceptPaymentsListXpo.Session)
        End Using

        If list IsNot Nothing AndAlso list.Count > 0 AndAlso listConceptCxpXpo IsNot Nothing AndAlso listConceptCxpXpo.Count > 0 Then
            For Each item In listConceptCxpXpo
                Dim info = (From e In list Where e.Id = item.Id Select e).FirstOrDefault
                If info IsNot Nothing Then
                    listPivot.Add(info)
                End If
            Next
        End If

        If listPivot IsNot Nothing AndAlso listPivot.Count > 0 Then
            ConceptPaymentsListXpo.AddRange(listPivot)
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de centros de costo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeCostCenter()
        Using msearch As New MBusqueda
            CostCenterListXpo = CType(msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetCostCenterByState, True), XPInstantFeedbackSource)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de conceptos de retencion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeConceptRetention()
        Using msearch As New MBusqueda
            ConceptRetentionListXpo = CType(msearch.ConsultarEntidades(eDataSource.ListRetentionConceptByStatus, "True"), XPInstantFeedbackSource)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de conceptos de notas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeConceptNote()
        Using msearch As New MBusqueda
            ConceptNotesListXpo = CType(msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListConceptsNotesByStatus, True), XPInstantFeedbackSource)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeAccount()
        Using msearch As New MBusqueda
            Dim filter() As Object = {5, True}
            AccountListXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que oculta los campos segun corresponda
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideFields()
        Select Case banRetentionType
            Case 0 'Ninguna
                INDlygConceptRetention.HideControl()
                IdConceptRetention = Nothing
                INDtxtBaseValue.Enabled = True

            Case 1 'ReteFuente
                If _form = "AccountPayable" Then
                    Nature = 2
                End If

                INDlygConceptRetention.HideControl(False)
                INDlyItemInvoicedValue.HideLayout()

            Case 2 'ReteIva
                If _form = "AccountPayable" Then
                    Nature = 2
                End If

                INDlygConceptRetention.HideControl(False)
                INDlyItemInvoicedValue.ShowLayout()

            Case 3 'ReteIca
                If _form = "AccountPayable" Then
                    Nature = 2
                End If

                INDlygConceptRetention.HideControl(False)
                INDlyItemInvoicedValue.HideLayout()

            Case 4 'Otras
                If _form = "AccountPayable" Then
                    Nature = 2
                End If

                INDlygConceptRetention.HideControl(False)
                INDlyItemInvoicedValue.HideLayout()
        End Select
    End Sub

    ''' <summary>
    ''' Metodo que abre el formulario liquidador
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function OpenFormLiquidator() As Task
        Me.Cursor = ChangeCursorIndigo()

        'Consulto los parametros de empresa en contabilidad para poder obtener el salario minimo y los uvt
        Using model As New MCompanySettings("")
            Dim CompanySettings As CompanySettings
            CompanySettings = Await model.GetCompanySettings()
            If CompanySettings IsNot Nothing AndAlso CompanySettings.Id > 0 Then
                'consulto con entity la informacion de concepto y cuentas
                Dim paymentConcept As AccountPayableConcepts = Nothing
                Using _Model As New MAccountPayable(CStr(Me.Tag))
                    Dim resultOperation = Await _Model.GetPaymentConceptById(_accountPayableConcepts?.Id)
                    paymentConcept = resultOperation.ObjectEmbbeded
                End Using

                RetentionConceptId383 = paymentConcept.ThreeEightThreeRetentionConceptId
                RetentionConceptDescription383 = paymentConcept.RetentionConceptThreeDescription
                RetentionConceptId384 = paymentConcept.ThreeEightFourRetentionConceptId
                RetentionConceptDescription384 = paymentConcept.RetentionConceptFourDescription

                Dim listErrors As String = Await CreateListRanges()
                If listErrors.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = listErrors
                    Exit Function
                End If

                Using Formulario As New FrmLiquidator(CompanySettings, ListRangeRetention383, ListRangeRetention384, accountPayableDetailConceptLiquidation)
                    Formulario.ThirdPartyId = idThird
                    Formulario.FiscalYear = FiscalYear
                    Formulario.DocumentDate = DocumentDate
                    Formulario.CreationDate = CreationDate
                    Formulario.OriginStatus = OriginStatus
                    Formulario.OriginCode = OriginCode
                    Formulario.BillNumber = BillNumber
                    Formulario.ViewModeEditHold = True
                    Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                    Formulario.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                    Formulario.Size = New System.Drawing.Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.45, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.9)
                    Dim transparent As New FrmTransparent(Formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog()

                    If Not Formulario.DialogResult = System.Windows.Forms.DialogResult.OK Then
                        If Formulario.CleanConcept Then 'Si toca limpiar el control

                            If INDsleConcept.Properties.Buttons.Count = 3 OrElse INDsleConcept.Properties.Buttons.Count = 2 Then
                                Dim info = (From item As DevExpress.XtraEditors.Controls.EditorButton In INDsleConcept.Properties.Buttons Where item.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph).FirstOrDefault
                                If info IsNot Nothing Then
                                    INDsleConcept.Properties.Buttons.RemoveAt(info.Index)
                                End If
                            End If

                            IdConceptPayments = Nothing
                            INDsleConcept.Properties.NullText = String.Empty
                        End If

                        FlagCloseLiquidator = False
                        Exit Function
                    End If

                    FlagCloseLiquidator = True
                    INDsleConcept.Properties.ReadOnly = True

                    accountPayableDetailConceptLiquidation = Formulario._accountPayableDetailConceptLiquidation
                    accountPayableDetailConceptLiquidation.TaxableBase = Formulario.TaxBaseGeneral
                    accountPayableDetailConceptLiquidation.RetentionValue383 = Formulario.FinalRetention
                    accountPayableDetailConceptLiquidation.RetentionValue384 = Formulario.Retention384

                    BaseValue = Formulario.TaxBaseGeneral
                    INDtxtBaseValue.Properties.ReadOnly = True
                    INDsleConceptRetention.Properties.ReadOnly = True
                    FlagCalculateWithLiquidator = True
                    INDsePercentage.Enabled = False

                    If Formulario.TypeRetentionAplicated = 0 Then
                        IdConceptRetention = RetentionConceptId383
                        INDsleConceptRetention.Properties.NullText = RetentionConceptDescription383
                        Percentage = Formulario.PercentageFinally
                        RetentionValue = Formulario.FinalRetention
                        accountPayableDetailConceptLiquidation.ApplyRetention = Formulario.FinalRetention

                        'tomamos la cuenta del concepto de retencion 383
                        If paymentConcept IsNot Nothing Then
                            banSearchCxP = False
                            INDsleAccount.Properties.ReadOnly = False
                            IdAccount = paymentConcept.MainAccounts1.Id
                            INDsleAccount.Properties.NullText = paymentConcept.NumberNameAccountThreeEightThree
                            INDsleAccount.Properties.ReadOnly = True
                            banSearchCxP = True

                            If IdAccount IsNot Nothing Then
                                HideCostCenter(paymentConcept.MainAccounts1.HandlesCostCenter, paymentConcept.MainAccounts1.RetencionType)
                            End If
                        End If
                    Else
                        IdConceptRetention = RetentionConceptId384
                        INDsleConceptRetention.Properties.NullText = RetentionConceptDescription384
                        Percentage = Formulario.PercentageFinally
                        RetentionValue = Formulario.Retention384
                        accountPayableDetailConceptLiquidation.ApplyRetention = Formulario.Retention384

                        'tomamos la cuenta del concepto de retencion 384
                        If paymentConcept IsNot Nothing Then
                            banSearchCxP = False
                            INDsleAccount.Properties.ReadOnly = False
                            IdAccount = paymentConcept.MainAccounts2.Id
                            INDsleAccount.Properties.NullText = paymentConcept.NumberNameAccounthreeEightFour
                            INDsleAccount.Properties.ReadOnly = True
                            banSearchCxP = True

                            If IdAccount IsNot Nothing Then
                                HideCostCenter(paymentConcept.MainAccounts2.HandlesCostCenter, paymentConcept.MainAccounts2.RetencionType)
                            End If
                        End If
                    End If

                    FlagCalculateWithLiquidator = False
                End Using
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No existe parámetros de empresa para realizar el cálculo de retenciones."
            End If
        End Using
    End Function

    ''' <summary>
    ''' Metodo que crea los listados de rangos para retenciones 383 y 384
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function CreateListRanges() As Task(Of String)
        Dim listErrors As New StringBuilder
        Using model As New MRetentionConcept("")
            Dim result As ActionResult(Of List(Of RetentionConceptRanges)) = Await model.GetListRetentionRangeByRetentionConceptId(RetentionConceptId383)

            If Not result.StateResult Then
                listErrors.AppendLine(result.MessageResult.ToString)
                Return listErrors.ToString
            End If

            If result.ObjectEmbbeded IsNot Nothing Then
                ListRangeRetention383 = result.ObjectEmbbeded
            Else
                listErrors.AppendLine("La retención 383 no tiene parametrizados los rangos.")
            End If
        End Using

        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' establece el formato de moneda en los controles
    ''' </summary>
    ''' <param name="Abbreviation"></param>
    Private Sub SetCurrencyUI(Abbreviation As String)
        If String.IsNullOrEmpty(Abbreviation) Then
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = Abbreviation.GetNumberFormat()
        INDtxtBaseValue.Properties.Mask.Culture = _culture
        INDtxtIvaValue.Properties.Mask.Culture = _culture
        INDtxtTotalConcept.Properties.Mask.Culture = _culture
        INDtxtInvoicedValue.Properties.Mask.Culture = _culture
        INDtxtRetentionValue.Properties.Mask.Culture = _culture
    End Sub
#End Region

#Region "Events"

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de concepto de pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleConcept.EditValueChanged
        If IdConceptPayments IsNot Nothing AndAlso banSearch Then
            Using model As New MAccountPayable("")
                Dim result = Await model.GetPaymentConceptById(IdConceptPayments)
                _accountPayableConcepts = result.ObjectEmbbeded
            End Using

            RaiseEvent DontClosePopup(Nothing, EventArgs.Empty)

            HandlesDeferredCausation = _accountPayableConcepts.DeferredCausation
            banHandlesRetentionConceptCxP = _accountPayableConcepts.HandlesRetention

            banSearchCxP = False
            If _accountPayableConcepts.IdAccount IsNot Nothing Then
                IdAccount = _accountPayableConcepts.IdAccount
                INDsleAccount.Properties.NullText = String.Concat(_accountPayableConcepts.MainAccounts.Number, " - ", _accountPayableConcepts.MainAccounts.Name)
                INDsleAccount.Properties.ReadOnly = True
                Nature = _accountPayableConcepts.MainAccounts.MainAccountClasses.Nature
            Else
                IdAccount = Nothing
                INDsleAccount.Properties.NullText = String.Empty
                INDsleAccount.Properties.ReadOnly = False
            End If
            banSearchCxP = True

            If IdAccount IsNot Nothing Then
                HideCostCenter(_accountPayableConcepts.MainAccounts.HandlesCostCenter, _accountPayableConcepts.MainAccounts.RetencionType)
            End If

            If _accountPayableConcepts.EmployeeCategoryRetention Then

                Dim editor As New DevExpress.XtraEditors.Controls.EditorButton
                editor.Image = Presentation.Payments.My.Resources.Resources.Calculadora_16x16_01
                editor.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
                editor.Visible = True

                Dim validateButtonGlyph = INDsleConcept.Properties.Buttons.Any(Function(t) t.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)

                If Not validateButtonGlyph Then
                    INDsleConcept.Properties.Buttons.Add(editor)
                End If

                Await OpenFormLiquidator()
                If FlagCloseLiquidator Then
                    Me.BeginInvoke(New Action(Sub() RaiseEvent DontClosePopup(Nothing, EventArgs.Empty)))
                End If

            Else
                If _accountPayableConcepts.HandlesRetention Then
                    IdConceptRetention = _accountPayableConcepts.RetentionConceptId
                    INDsleConceptRetention.Properties.NullText = _accountPayableConcepts.RetentionConcepts.Code + " - " + _accountPayableConcepts.RetentionConcepts.Name
                    INDsleConceptRetention.Properties.ReadOnly = True
                End If
            End If

            ''se valida si se deben mostrar o no los campos de iva 
            If ValidateControlIva() Then
                INDlyItemRateIva.Visibility = If(hasDeductibleIva.HasValue(), If(_accountPayableConcepts.HandleTaxes AndAlso Not _accountPayableConcepts.HandlesRetention, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never), DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                INDlyItemIvaValue.Visibility = If(hasDeductibleIva.HasValue(), If(_accountPayableConcepts.HandleTaxes AndAlso Not _accountPayableConcepts.HandlesRetention, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never), DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                INDlyItemTotalConcept.Visibility = If(hasDeductibleIva.HasValue(), If(_accountPayableConcepts.HandleTaxes AndAlso Not _accountPayableConcepts.HandlesRetention, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never), DevExpress.XtraLayout.Utils.LayoutVisibility.Never)

                INDlyItemIvaValue.Enabled = False
                INDlyItemTotalConcept.Enabled = False
            End If

        End If
    End Sub

    ''' <summary>
    ''' Oculta o muestra el centro costo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideCostCenter(handlesCostCenter As Boolean, retentionType As Integer)
        If _form = "AccountPayable" Then
            If AccountPayableDetail Is Nothing Then
                AccountPayableDetail = New AccountPayableDetailConcept
            End If
        Else
            If PaymentNoteDetail Is Nothing Then
                PaymentNoteDetail = New PaymentsNoteDetails
            End If
        End If

        If handlesCostCenter Then
            INDlyItemCostCenter.ShowLayout()
        Else
            INDlyItemCostCenter.HideLayout()
        End If

        banRetentionType = retentionType
        HideFields()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de conceptos de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleConceptRetention_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleConceptRetention.EditValueChanged
        If IdConceptRetention IsNot Nothing AndAlso Not FlagCalculateWithLiquidator Then
            Using model As New MRetentionConcept(CStr(Tag))
                retentionConcept = model.GetRetentionByIdSimple(IdConceptRetention)

                If retentionConcept IsNot Nothing Then
                    Select Case retentionConcept.Retention
                        Case 1 'base
                            RetentionValue = 0
                            Percentage = retentionConcept.Rate
                            INDsePercentage.Enabled = False

                        Case 2 'rango
                            RetentionValue = 0
                            Dim percentageRange As Decimal = Await CalculatePercentageRange()
                            Percentage = percentageRange
                            INDsePercentage.Enabled = False

                        Case 3 'variable
                            INDsePercentage.Enabled = True
                            Percentage = retentionConcept.Rate
                            RetentionValue = 0
                    End Select

                    If Percentage > 0 AndAlso retentionConcept.Retention <> 2 Then
                        ValidateCalculateRetention()
                    End If
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Calcula el valor del porcentaje del rango
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function CalculatePercentageRange() As Task(Of Decimal)
        Dim percentageRange As Decimal = 0
        If retentionConcept IsNot Nothing Then
            If retentionConcept.RetentionConceptRanges IsNot Nothing AndAlso retentionConcept.RetentionConceptRanges.Any() Then

                'Se consulta los parametros de empresa para poder sacar el valor del uvt unitario
                Dim CompanySettings As CompanySettings
                Dim UVTBaseValue As Decimal
                Using model As New MCompanySettings("")
                    CompanySettings = Await model.GetCompanySettings()
                    If CompanySettings IsNot Nothing AndAlso CompanySettings.Id > 0 Then
                        'Se calcula el valor del UVT, el valor base sobre el valor en pesos del UVT
                        UVTBaseValue = Decimal.Round(Me.BaseValue / CompanySettings.UVT, 1)
                    Else
                        IdConceptRetention = Nothing
                        Mensaje(EeventViewerImages.Advertencia) = "No existe parámetros de empresa para realizar el cálculo de retenciones."
                        Return 0
                    End If
                End Using

                Dim rcr As RetentionConceptRanges = (From r In retentionConcept.RetentionConceptRanges Where UVTBaseValue >= r.ValueInitial AndAlso UVTBaseValue <= r.ValueFinish Select r).FirstOrDefault
                If rcr IsNot Nothing Then
                    'Se asigna el porcentaje en el control
                    percentageRange = rcr.Percentage

                    'Se calcula el porcentaje para poder sacar el valor a cobrar
                    Dim resulCalculateRetention As Decimal = Utils.RoundValue((((UVTBaseValue - rcr.ValueDeducted) * rcr.Percentage / 100) + rcr.UVTIncrement) * CompanySettings.UVT, retentionConcept.TypeRounding)
                    RetentionValue = resulCalculateRetention.ToString("C2")
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "El valor base ingresado no existe en los rangos de la retención."
                    percentageRange = 0
                End If
            End If
        End If
        Return percentageRange
    End Function

    ''' <summary>
    ''' Metodo para cargar la informacion del concepto
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub getAccountConcept(IdAccount)
        Using Model As New MConceptsNotes(Me.Tag)
            _accountConcept = Model.GetPaymentNoteConceptById(IdAccount)
        End Using
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del concepto de nota
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConceptNote_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleConceptNote.EditValueChanged
        If IdConceptNotes IsNot Nothing AndAlso banSearch Then

            Dim accountPayableConceptNoteXpo = DirectCast(DirectCast(viewSearchNote.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PaymentsRepository.ConceptsNotesXpo)

            banSearchCxP = False
            If accountPayableConceptNoteXpo.IdAccount IsNot Nothing Then
                IdAccount = accountPayableConceptNoteXpo.IdAccount.Id
                INDsleAccount.Properties.NullText = accountPayableConceptNoteXpo.IdAccount.NumberName
                INDsleAccount.Properties.ReadOnly = True
            Else
                IdAccount = Nothing
                INDsleAccount.Properties.NullText = String.Empty
                INDsleAccount.Properties.ReadOnly = False
            End If

            banSearchCxP = True
            If IdAccount IsNot Nothing Then
                HideCostCenter(accountPayableConceptNoteXpo.IdAccount.HandlesCostCenter, accountPayableConceptNoteXpo.IdAccount.RetencionType)
            End If

            getAccountConcept(accountPayableConceptNoteXpo.Id)

            'validacion para ocultar campos segun datos del concepto
            If Not _accountConcept.ObjectEmbbeded.ManageTax Or _accountConcept.ObjectEmbbeded.ManageRetention Then
                HidenControlIVA(False)
            Else
                HidenControlIVA(True)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccount.EditValueChanged
        If IdAccount IsNot Nothing AndAlso banSearchCxP And AccountListXpo IsNot Nothing Then
            Dim accountXpo = CtrPUC1View.GetFocusedObject(Of Infrastructure.Data.Xpo.AccountingRepository.PUCServiceXpo)

            If _form = "AccountPayable" AndAlso banHandlesRetentionConceptCxP AndAlso accountXpo?.RetencionType = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontRetention", NAME_MODULE)
                INDsleAccount.Properties.NullText = String.Empty
                IdAccount = Nothing
            End If

            HideCostCenter(accountXpo?.HandlesCostCenter, accountXpo?.RetencionType)
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de concepto de cuentas por pagar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleConcept.QueryPopUp
        If INDsleConcept.Properties.DataSource Is Nothing Then
            InitializeConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThirdParty.QueryPopUp
        If INDsleThirdParty.Properties.DataSource Is Nothing Then
            InitializeThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de tarifa iva
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateIva_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRateIva.QueryPopUp
        LoadDataSourceGeneralLedgerIVA()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If INDsleCostCenter.Properties.DataSource Is Nothing Then
            InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de conceptos de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConceptRetention_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleConceptRetention.QueryPopUp
        If INDsleConceptRetention.Properties.DataSource Is Nothing Then
            InitializeConceptRetention()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de condeptos de nota
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConceptNote_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleConceptNote.QueryPopUp
        If INDsleConceptNote.Properties.DataSource Is Nothing Then
            InitializeConceptNote()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccount.QueryPopUp
        If INDsleAccount.Properties.DataSource Is Nothing Then
            InitializeAccount()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AddConcept()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de cancelar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnCancel_Click(sender As Object, e As EventArgs) Handles INDbtnCancel.Click
        CleanControls()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el formulario indicado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmConceptsAccountsPayable With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            InitializeConcept()
        ElseIf e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
            e.Button.Enabled = False
            Entity = False
            Await OpenFormLiquidator()
            If FlagCloseLiquidator Then
                If optionEdit Then
                    Entity = True
                End If
                RaiseEvent DontClosePopup(Nothing, EventArgs.Empty)
            End If
            e.Button.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Abre el formulario indicado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            INDsleAccount.Properties.DataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Abre el formulario indicado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New Presentation.Common.FrmThirdParty With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using

            InitializeThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Abre el formulario indicado
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

            InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Abre el formulario indicado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConceptRetention_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleConceptRetention.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmRetentionConcept With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using

            InitializeConceptRetention()
        End If
    End Sub

    ''' <summary>
    ''' Abre el formulario indicado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConceptNote_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleConceptNote.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmConceptsNotes With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            InitializeConceptNote()
        End If
    End Sub

#End Region

#Region "Leave"

    ''' <summary>
    ''' Evento que se dispara al quitar el foco del control de valor base
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDtxtBaseValue_Leave(sender As Object, e As EventArgs) Handles INDtxtBaseValue.Leave
        If INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDtxtBaseValue.Properties.ReadOnly = False Then
            If retentionConcept IsNot Nothing Then
                If retentionConcept.Retention = 2 Then
                    Percentage = Await CalculatePercentageRange()
                End If
            End If
        End If

        If INDtxtBaseValue.Properties.ReadOnly = False AndAlso retentionConcept IsNot Nothing AndAlso retentionConcept.Retention <> 2 Then
            ValidateCalculateRetention()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al quitar el foco del control de porcentaje
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsePercentage_Leave(sender As Object, e As EventArgs) Handles INDsePercentage.Leave
        If Percentage > 0 Then
            ValidateCalculateRetention()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter del control de valor base
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtBaseValue_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtBaseValue.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDlygConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDsleConceptRetention.Focus()
            Else
                INDbtnAdd.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter del control de porcentaje
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsePercentage_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDsePercentage.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDlyItemInvoicedValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                INDbtnAdd.Focus()
            Else
                INDtxtInvoicedValue.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter del control de concepto de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConceptRetention_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDsleConceptRetention.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDsePercentage.Enabled = False Then
                If INDlyItemInvoicedValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                    INDbtnAdd.Focus()
                Else
                    INDtxtInvoicedValue.Focus()
                End If
            Else
                INDsePercentage.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de valor facturado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtInvoicedValue_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtInvoicedValue.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDbtnAdd.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para actualizar el valor del iva y el total del concepto segun la tarifa de iva
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChangeValueIVAConcept(taxId As Integer?)
        _generalLedgerIVA = Nothing

        If taxId Is Nothing OrElse taxId = 0 Then
            Exit Sub
        End If

		Using Model As New MGeneralLedgerIVA(Me.Tag)
			_generalLedgerIVA = Model.GetGeneralLedgerIVAById(taxId)
			If _generalLedgerIVA.StateResult Then
				Me.IVAValue = Math.Round(CDec((Me.BaseValue * _generalLedgerIVA.ObjectEmbbeded?.Percentage) / 100), 2, MidpointRounding.AwayFromZero)
				Me.TotalConceptValue = Math.Round(Me.BaseValue + If(Me.IVAValue, 0), decimals)

				If _generalLedgerIVA.ObjectEmbbeded?.IdAccountPurchaseService Is Nothing OrElse
					_generalLedgerIVA.ObjectEmbbeded?.IdAccountDebitControlFiscal Is Nothing OrElse
					_generalLedgerIVA.ObjectEmbbeded?.IdAccountCreditControlFiscal Is Nothing Then

					Mensaje(EeventViewerImages.Advertencia) = "La tarifa de IVA seleccionada no tiene una o más cuentas parametrizadas necesarias"
					INDbtnAdd.Enabled = False
				End If
			End If
		End Using
	End Sub

    Private Sub INDsleRateIva_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRateIva.EditValueChanged
        ChangeValueIVAConcept(IdGeneralLedgerIVA)
    End Sub

    Private Sub INDtxtBaseValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtBaseValue.EditValueChanged
        'Valido que tenga Valor Base 
        If BaseValue = 0 Then
            Exit Sub
        End If

        'Se valida para saber que tipo de concepto es y asi se actualize en tiempo real el valor en el campo que debe ser dependiendo si es retencion o iva
        'Si no tiene IVA
        If IdGeneralLedgerIVA Is Nothing Then
            ''para cuando el concepto maneja retencion
            ValidateCalculateRetention()
            Me.TotalConceptValue = Math.Round(Me.BaseValue, decimals)
            Exit Sub
        End If

        If IdGeneralLedgerIVA IsNot Nothing Then
            ChangeValueIVAConcept(IdGeneralLedgerIVA)
        Else
			Me.TotalConceptValue = Math.Round(Me.BaseValue + If(Me.IVAValue, 0), decimals)
		End If
    End Sub

#End Region

#End Region

End Class
