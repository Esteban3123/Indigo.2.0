'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán
' Created          : 23-09-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Treasury.MVP
Imports Presentation.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports DevExpress.XtraGrid
Imports Presentation.Base.Eform
Imports Domain.Base.Entities
Imports System.Text
Imports Presentation.Maintenance.MVP
Imports DevExpress.Data.Linq
Imports Presentation.Accounting.MVP
Imports Domain.Entities.Service
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common
Imports Presentation.Payments.MVP
Imports Presentation.Portfolio.MVP
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraSpreadsheet
Imports Presentation.Common.MVP
Imports System.Threading
Imports Infrastructure.Data.Xpo
Imports Presentation.Payments
Imports Presentation.Portfolio

#End Region

Public Class FrmVoucherTransactionCrossing
    Implements IVoucherTransactionCrossing, ICustomizableForm

#Region "Builder"

    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmVoucherTransactionCrossing"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        ctrTmp = New CtrVoucherTransactionCrossing()
        ctrTmp.SetFunctionCrossing(AddressOf getCrossingValue)
        ctrTmp.PrintValueCrossing()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)

        IndigoGridControl1.SetHideNoRecords(INDgcAccountRecivable, True)
        IndigoGridControl1.SetHideNoRecords(INDgcAcountPayable, True)
        IndigoGridControl1.SetHideNoRecords(INDGcCrossingAccountOtherConcepts, True)
    End Sub

#End Region

#Region "Properties and Variables"

#Region "Variables"

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenCxCAsync As CancellationTokenSource

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenCxPAsync As CancellationTokenSource

    ''' <summary>
    ''' Listado de eliminados para las cxc
    ''' </summary>
    Private listDeleteCxC As List(Of CrossingAccountDetailCxC)

    ''' <summary>
    ''' Listado de eliminados para las cxc
    ''' </summary>
    Private listDeleteCxP As List(Of CrossingAccountDetailCxP)

    ''' <summary>
    ''' Constante con el nombre del módulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Treasury"
    ''' <summary>
    ''' contiene el id del proveedor asignado al tercero seleccionado
    ''' </summary>
    Private _suppierId As Integer
    ''' <summary>
    ''' valor total de CxP
    ''' </summary>
    Private _CxPValue As Decimal
    ''' <summary>
    ''' valor total de CxC
    ''' </summary>
    Private _CxCValue As Decimal
    ''' <summary>
    ''' control de cruce de compr egreso y anticipos
    ''' </summary>
    Private ctrTmp As CtrVoucherTransactionCrossing
    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.TreasurySequence
    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    '''' <summary>
    '''' variable que contiene la entidad
    '''' </summary>
    'Dim voucherTransactionCrossing As VoucherTransactionCrossing
    '''' <summary>
    '''' variable que se utiliza para saber si el frontal entra por modo busqueda o modo edicion
    '''' </summary>
    'Dim _searchMode As Boolean
    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Dim _presenter As PVoucherTransactionCrossing
    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Dim record As BlockRecordTreasury
    ''' <summary>
    ''' variable que contiene el objeto xpo de la factura CxP seleccionada
    ''' </summary>
    Private _accountPayableXpo As Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableXpo
    ''' <summary>
    ''' variable que contiene el objeto xpo de la factura CxC seleccionada
    ''' </summary>
    Private _accountRecivableAccountingXpo As Infrastructure.Data.Xpo.PortfolioRepository.PortfolioAccountReceivableAccountingXpo
    ''' <summary>
    ''' variable de la entidad principal
    ''' </summary>
    Private _crossingAccount As CrossingAccount
    ''' <summary>
    ''' The _CXC value changing
    ''' </summary>
    Private _cxcValueChanging As Boolean
    ''' <summary>
    ''' The _CXC percent changing
    ''' </summary>
    Private _cxcPercentChanging As Boolean
    ''' <summary>
    ''' The _CXP value changing
    ''' </summary>
    Private _cxpValueChanging As Boolean
    ''' <summary>
    ''' The _CXP percent changing
    ''' </summary>
    Private _cxpPercentChanging As Boolean
    '''' <summary>
    ' ''' diccionario que almacena las facturas para hacer validaciones
    ' ''' </summary>
    'Private _dictionaryShares As Dictionary(Of String, Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableSharesXpo)
    ''' <summary>
    ''' valor total de CxP
    ''' </summary>
    Dim reportType As Decimal
    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer
    ''' <summary>
    ''' Permite saber si se esta cargando desde el loadControls
    ''' </summary>
    Dim IsLoadControls As Boolean = False
    ''' <summary>
    ''' 
    ''' </summary>
    Public CallNote As Boolean = False
    ''' <summary>
    ''' Lista de la tasa de cambio
    ''' </summary>
    Private _listTRM As List(Of TRM)
    ''' <summary>
    ''' standart ISO4217
    ''' </summary>
    Private _currencyAbbreviation As String
#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IVoucherTransactionCrossing.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IVoucherTransactionCrossing.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    Public Property Sequense As TreasurySequence Implements IVoucherTransactionCrossing.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As TreasurySequence)
            _sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.TreasurySequenceDetail In Me._sequence.TreasurySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property
    ''' <summary>
    ''' Consecutivo
    ''' </summary>
    Public Property Code As String Implements IVoucherTransactionCrossing.Code
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Tipo de cruce
    ''' </summary>
    ''' <value>
    ''' The type of the crossing.
    ''' </value>
    Public Property CrossingType As Byte Implements IVoucherTransactionCrossing.CrossingType
        Get
            Return CType(INDgleCrossingType.EditValue, Byte)
        End Get
        Set(value As Byte)
            INDgleCrossingType.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Gets the type of the datasource crossing.
    ''' </summary>
    ''' <value>
    ''' The type of the datasource crossing.
    ''' </value>
    Public ReadOnly Property DatasourceCrossingType As List(Of Tuple(Of Byte, String))
        Get
            Dim _listCrossingType As New List(Of Tuple(Of Byte, String))()
            _listCrossingType.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("CrossingTypeSameThird", MODULE_NAME)))
            _listCrossingType.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("CrossingTypeDifferentThird", MODULE_NAME)))
            Return _listCrossingType
        End Get
    End Property
    ''' <summary>
    ''' Obtiene o establece el detalle del cruce
    ''' </summary>
    ''' <value>
    ''' The detail.
    ''' </value>
    Public Property Detail As String Implements IVoucherTransactionCrossing.Detail
        Get
            Return INDmeDetail.Text
        End Get
        Set(value As String)
            INDmeDetail.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value>
    ''' The document date.
    ''' </value>
    Public Property DocumentDate As DateTime Implements IVoucherTransactionCrossing.DocumentDate
        Get
            Return CType(INDdeDate.EditValue, DateTime)
        End Get
        Set(value As DateTime)
            INDdeDate.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value>
    ''' The third party identifier.
    ''' </value>
    Public Property ThirdPartyId As Integer Implements IVoucherTransactionCrossing.ThirdPartyId
        Get
            Return CType(INDsleThirdParty.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleThirdParty.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value>
    ''' The status.
    ''' </value>
    Public Property Status As String Implements IVoucherTransactionCrossing.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
            BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el datasource de terceros
    ''' </summary>
    Public Property ThirdPartyDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IVoucherTransactionCrossing.ThirdPartyDatasource
        Get
            Return CType(INDsleThirdParty.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el datasource de cuentas por pagar
    ''' </summary>
    Public Property AccountPayableDatasource As LinqInstantFeedbackSource Implements IVoucherTransactionCrossing.AccountPayableDatasource
        Get
            Return CType(INDsleInvoiceCxP.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleInvoiceCxP.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de cuentas por pagar
    ''' </summary>
    Public Property AccountRecivableDatasource As LinqInstantFeedbackSource Implements IVoucherTransactionCrossing.AccountRecivableDatasource
        Get
            Return CType(INDsleInvoiceCxC.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleInvoiceCxC.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el id de la cuota de la cuenta por pagar
    ''' </summary>
    Public Property AccountPayableShareId As Integer
        Get
            Return CType(INDsleInvoiceCxP.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleInvoiceCxP.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el id de la cuota de la cuenta por pagar
    ''' </summary>
    Public Property AccountRecivableId As Integer
        Get
            Return CType(INDsleInvoiceCxC.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleInvoiceCxC.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el valor a cruzar de CxP
    ''' </summary>
    Public Property CrossingValueCxP As Decimal
        Get
            Return CType(INDtxtCrossingValueCxP.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDtxtCrossingValueCxP.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el porcentaje a cruzar
    ''' </summary>
    Public Property CrossingPercentCxP As Decimal
        Get
            Return CType(INDtxtCrossingPercentCxP.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDtxtCrossingPercentCxP.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el valor a cruzar de CxC
    ''' </summary>
    Public Property CrossingValueCxC As Decimal
        Get
            Return CType(INDtxtCrossingValueCxC.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDtxtCrossingValueCxC.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el porcentaje a cruzar CxC
    ''' </summary>
    Public Property CrossingPercentCxC As Decimal
        Get
            Return CType(INDspCrossingPercentCxC.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDspCrossingPercentCxC.EditValue = value
        End Set
    End Property

    Private _invoiceBalanceCxP As Decimal

    ''' <summary>
    ''' Obtiene o establece el saldo de la factura CxP
    ''' </summary>
    Public Property InvoiceBalanceCxP(Optional Abbreviation As String = Nothing) As Decimal
        Get
            Return _invoiceBalanceCxP
        End Get
        Set(value As Decimal)
            _invoiceBalanceCxP = value
            INDlbBalance.Text = Utils.GetMoneyWithISO4217(value, Abbreviation)
        End Set
    End Property

    Private _invoiceBalanceCxC As Decimal

    ''' <summary>
    ''' Obtiene o establece el saldo de la factura CxC
    ''' </summary>
    Public Property InvoiceBalanceCxC(Optional Abbreviation As String = Nothing) As Decimal
        Get
            Return _invoiceBalanceCxC
        End Get
        Set(value As Decimal)
            _invoiceBalanceCxC = value
            INDlbBalanceCxC.Text = Utils.GetMoneyWithISO4217(value, Abbreviation)
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece las facturas CxP a cruzar agregadas a la rejilla
    ''' </summary>
    ''' <value>
    ''' The list account payable shares.
    ''' </value>
    Public Property ListCrossingAccountDetailCxP As List(Of CrossingAccountDetailCxP)
        Get
            Return CType(INDgcAcountPayable.DataSource, List(Of CrossingAccountDetailCxP))
        End Get
        Set(value As List(Of CrossingAccountDetailCxP))
            INDgcAcountPayable.DataSource = value
        End Set
    End Property
    Public Property ListCrossingAccountOtherConcepts As List(Of CrossingAccountDetailOtherConcept)
        Get
            Return CType(INDGcCrossingAccountOtherConcepts.DataSource, List(Of CrossingAccountDetailOtherConcept))
        End Get
        Set(value As List(Of CrossingAccountDetailOtherConcept))
            INDGcCrossingAccountOtherConcepts.DataSource = Nothing
            INDGcCrossingAccountOtherConcepts.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece las facturas CxC a cruzar agregadas a la rejilla
    ''' </summary>
    ''' <value>
    ''' The list account payable shares.
    ''' </value>
    Public Property ListCrossingAccountDetailCxC As List(Of CrossingAccountDetailCxC)
        Get
            Return CType(INDgcAccountRecivable.DataSource, List(Of CrossingAccountDetailCxC))
        End Get
        Set(value As List(Of CrossingAccountDetailCxC))
            INDgcAccountRecivable.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el id de concepto de flujo de caja
    ''' </summary>
    ''' <returns></returns>
    Public Property IdCashFlowConceptCxC As Integer? Implements IVoucherTransactionCrossing.IdCashFlowConceptCxC
        Get
            Return INDSleCashFlowConceptCxC.EditValue
        End Get
        Set(value As Integer?)
            INDSleCashFlowConceptCxC.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el id de concepto de flujo de caja
    ''' </summary>
    ''' <returns></returns>
    Public Property IdCashFlowConceptCxP As Integer? Implements IVoucherTransactionCrossing.IdCashFlowConceptCxP
        Get
            Return INDSleCashFlowConceptCxP.EditValue
        End Get
        Set(value As Integer?)
            INDSleCashFlowConceptCxP.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece/Obtiene el datasource de conceptos de flujo de efectivo
    ''' </summary>
    Public Property CashFlowConceptCxCDataSource As DevExpress.Xpo.XPInstantFeedbackSource Implements IVoucherTransactionCrossing.CashFlowConceptCxCDataSource
        Get
            Return INDSleCashFlowConceptCxC.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCashFlowConceptCxC.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece/Obtiene el datasource de conceptos de flujo de efectivo
    ''' </summary>
    Public Property CashFlowConceptCxPDataSource As DevExpress.Xpo.XPInstantFeedbackSource Implements IVoucherTransactionCrossing.CashFlowConceptCxPDataSource
        Get
            Return INDSleCashFlowConceptCxP.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCashFlowConceptCxP.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property CashFlowConceptCodeNameOther As String Implements IVoucherTransactionCrossing.CashFlowConceptCodeNameOther
        Get
            Return INDLbCashFlowConceptOther.Text
        End Get
        Set(ByVal value As String)
            INDLbCashFlowConceptOther.Text = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property CashFlowConceptIdOther As Integer Implements IVoucherTransactionCrossing.CashFlowConceptIdOther
        Get
            Return INDLbCashFlowConceptOther.Tag
        End Get
        Set(ByVal value As Integer)
            INDLbCashFlowConceptOther.Tag = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad de la moneda del anticipo seleccionado
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer? Implements IVoucherTransactionCrossing.CurrencyId
        Get
            Return INDSleCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDSleCurrency.EditValue = value
            Me._setFormatGeneralControls(_currencyAbbreviation)
            ctrTmp.CodeISO4217 = INDSleCurrency.Text
            ctrTmp.PrintValueCrossing()
        End Set
    End Property

    ''' <summary>
    ''' codigo standart de la moneda ISO4217
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CurrencyAbbreviation As String Implements IVoucherTransactionCrossing.CurrencyAbbreviation
        Get
            Return _currencyAbbreviation
        End Get
    End Property
#End Region

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' funcion delegada que retorna los valores del cruce
    ''' </summary>
    ''' <returns></returns>
    Public Function getCrossingValue() As Tuple(Of Decimal, Decimal)
        Return New Tuple(Of Decimal, Decimal)(_CxPValue, _CxCValue)
    End Function

    ''' <summary>
    ''' News the voucher transaction crossing.
    ''' </summary>
    Private Async Function NewVoucherTransactionCrossing() As Task
        _crossingAccount = New CrossingAccount()
        INDgleCrossingType.EditValue = 1
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.TreasurySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.TreasurySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MCommonTreasury(CStr(Me.Tag))
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
        DocumentDate = Me.GetDateServer()
    End Function

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15},
                              New ColumnInfo() With {.Caption = "Fecha del Documento", .FieldName = "DocumentDate", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15},
                              New ColumnInfo() With {.Caption = "Tercero", .FieldName = "ThirdPartyId.NitName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.6},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCrossingAccount
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
                Await ModelCommonTreasury.DeleteBlockRecordTreasury(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IVoucherTransactionCrossing.ActionsOnControls
        Set(value As Boolean)
            INDlycRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDsleThirdParty.Enabled = value
            INDdeDate.Enabled = value
            INDmeDetail.Enabled = value
            INDpceAddInvoiceCxC.Enabled = value
            INDpceAddInvoiceCxP.Enabled = value
            INDPceAddConcept.Enabled = value
            INDgleCrossingType.Enabled = value

            INDgcAcountPayable.Enabled = value
            INDgcAccountRecivable.Enabled = value
            INDGcCrossingAccountOtherConcepts.Enabled = value

            INDSleCashFlowConceptCxC.Enabled = value
            INDSleCashFlowConceptCxP.Enabled = value

            Me.BarraBotones.StatusRecordVisible = value
            INDlycRoot.EndUpdate()

            If value Then
                INDdeDate.Focus()
                INDdeDate.Properties.ReadOnly = True
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements IVoucherTransactionCrossing.CleanControls
        INDlycRoot.BeginUpdate()
        ReadOnlyControls(False)
        INDgleCrossingType.EditValue = Nothing
        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        INDgcAccountRecivable.DataSource = Nothing
        INDgcAcountPayable.DataSource = Nothing
        INDGcCrossingAccountOtherConcepts.DataSource = Nothing
        listDeleteCxP = Nothing
        listDeleteCxC = Nothing

        INDlcgRoot.BeginUpdate()
        INDbteCode.Text = String.Empty
        INDsleThirdParty.EditValue = Nothing
        INDmeDetail.Text = String.Empty
        INDgleCrossingType.EditValue = Nothing
        INDlcgRoot.EndUpdate()

        _CxCValue = 0
        _CxPValue = 0
        _cxcPercentChanging = False
        _cxcValueChanging = False
        _cxpPercentChanging = False
        _cxpValueChanging = False

        INDcolCxPBalance.Visible = True
        INDcolCxCBalance.Visible = True
        Me.CurrencyId(indigo.CurrencyISO4217) = indigo.OfficialCurrencyId
        ctrTmp.CodeISO4217 = indigo.CurrencyISO4217
        ctrTmp.PrintValueCrossing()
        INDsleThirdParty.Properties.ReadOnly = False
        _suppierId = 0
        CleanControlsInvoiceCxC()
        CleanControlsInvoiceCxP()
        CleanControlsPopupOtherConcept()
        INDBtnImportFileCxC.Enabled = False
        INDBtnImportFileCxP.Enabled = False
        _crossingAccount = Nothing
        IdCashFlowConceptCxC = Nothing
        IdCashFlowConceptCxP = Nothing
        CashFlowConceptCodeNameOther = String.Empty
        CashFlowConceptIdOther = 0

        INDgvAcountPayable.HideLoadingPanel()
        INDgvAccountRecivable.HideLoadingPanel()
        ListCrossingAccountDetailCxP = New List(Of CrossingAccountDetailCxP)
        ListCrossingAccountDetailCxC = New List(Of CrossingAccountDetailCxC)
        validateAddDetails()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlycRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatusReverse"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()

        With _crossingAccount
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Description = Detail
            .ThirdPartyId = ThirdPartyId
            .DocumentDate = DocumentDate
            .OperatingUnitId = _idOperativeUnit
            .CrossingType = CrossingType
            .Status = If(String.IsNullOrEmpty(Status), 1, CByte(Status))
            .CurrencyId = CurrencyId
            If ListCrossingAccountDetailCxP IsNot Nothing AndAlso ListCrossingAccountDetailCxP.Count > 0 Then
                ListCrossingAccountDetailCxP.ForEach(Sub(x) .CrossingAccountDetailCxP.Add(x))
            End If

            If listDeleteCxP IsNot Nothing AndAlso listDeleteCxP.Count > 0 Then
                listDeleteCxP.ForEach(Sub(x) .CrossingAccountDetailCxP.Add(x.MarkAsDeleted()))
            End If

            If ListCrossingAccountDetailCxC IsNot Nothing AndAlso ListCrossingAccountDetailCxC.Count > 0 Then
                ListCrossingAccountDetailCxC.ForEach(Sub(x) .CrossingAccountDetailCxC.Add(x))
            End If

            If listDeleteCxC IsNot Nothing AndAlso listDeleteCxC.Count > 0 Then
                listDeleteCxC.ForEach(Sub(x) .CrossingAccountDetailCxC.Add(x.MarkAsDeleted()))
            End If
        End With

    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Public Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Using Model As New MVoucherTransactionCrossing(Me.Tag)
                AsyncLoader(True)
                Dim resultOperation = Await Model.GetCrossingAccount(Me.Code, True)
                INDlycRoot.BeginUpdate()
                _crossingAccount = resultOperation.ObjectEmbbeded
                If Not _crossingAccount Is Nothing Then
                    If _crossingAccount.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
                            record = Await ModelCommonTreasury.GetBlockRecordTreasury(Me.Tag, _crossingAccount.Id)
                            With _crossingAccount
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Code = .Code
                                Detail = .Description
                                DocumentDate = .DocumentDate
                                ThirdPartyId = .ThirdPartyId
                                CrossingType = .CrossingType
                                Status = .Status.ToString()
                                If String.IsNullOrEmpty(.Currency.Abbreviation) Then
                                    INDSleCurrency.Properties.NullText = indigo.CurrencyISO4217
                                    CurrencyId(indigo.CurrencyISO4217) = indigo.OfficialCurrencyId
                                Else
                                    INDSleCurrency.Properties.NullText = .Currency.Abbreviation
                                    CurrencyId(.Currency.Abbreviation) = .CurrencyId
                                End If
                                CurrencyId() = .CurrencyId
                            End With
                            Me.GetDocumentIndexed(Me.Tag & "_" & Me._crossingAccount.Code)
                            If record.Id = 0 Then
                                record = (Await ModelCommonTreasury.SaveBlockRecordTreasury(
                                    New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _crossingAccount.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_crossingAccount.Id, Me.Tag.ToString(), Nothing, GetType(CrossingAccount).Name)
                            If _crossingAccount.Status = 2 OrElse _crossingAccount.Status = 3 Or _crossingAccount.Status = 4 Then 'estado confirmado
                                ReadOnlyControls(True)
                                INDcolCxPBalance.Visible = False
                                INDcolCxCBalance.Visible = False
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                            End If
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            If (INDgleCrossingType.EditValue = 1) Then
                                reportType = 1
                            Else
                                reportType = 2
                            End If
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _crossingAccount.Id, 0, _crossingAccount.Id, reportType)
                            AsyncLoader(False)
                            IsLoadControls = True
                            ActionsOnControls = True
                            IsLoadControls = False
                            INDbteCode.Enabled = False
                            LoadCxCInvoices()
                            LoadCxPInvoices()
                            LoadConcepts()
                            If CallNote Then
                                RaiseEvent LoadControlsFinish()
                            End If

                        End Using
                    Else
                        'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                        'Me.Code = String.Empty
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewVoucherTransactionCrossing()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Me.Code = String.Empty
                            Deshacer()
                            INDbteCode.Focus()
                        End If
                    End If
                Else
                    'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                    'Me.Code = String.Empty
                    'Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Await Me.NewVoucherTransactionCrossing()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                        Me.Code = String.Empty
                        Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                        Deshacer()
                        INDbteCode.Focus()
                    End If
                End If
            End Using
        End If
        INDlycRoot.EndUpdate()
    End Function

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        'Dim dateServer = Me.GetDateServer()
        'If Me._doc Is Nothing Then
        '    Me._doc = New IndexedDocument2 With { _
        '        .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.CashRegister.Code, Me.CashRegister.Name, Me.CashRegister.InitialDate), _
        '        .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
        '        .IdEntity =  "$#" & Me.Tag & "_" & Me.CashRegister.Code & "#$", .IdForm = Me.Tag, _
        '        .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.CashRegister.Code), _
        '        .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        '    Return Me._doc
        'Else
        '    Me._doc.Update = dateServer
        '    Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
        '    Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.CashRegister.Code, Me.CashRegister.Name, Me.CashRegister.InitialDate)
        '    Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.CashRegister.Code)
        '    Return Me._doc
        'End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Loads the data share invoice CXP.
    ''' </summary>
    ''' <param name="invoiceCxP">The invoice cx p.</param>
    Private Async Sub LoadDataShareInvoiceCxp(invoiceCxP As Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableXpo)
        Dim TRMValue As Decimal = 1

        If Not Await Me.CurrencyExchangeActions(True, Me.CurrencyId, Me.CurrencyAbbreviation, invoiceCxP?.CurrencyId) Then
            CleanControlsInvoiceCxP()
            Exit Sub
        End If

        If Me.CurrencyId <> invoiceCxP?.CurrencyId Then
            TRMValue = _listTRM.FirstOrDefault(Function(x) x.CurrencyId = Me.CurrencyId AndAlso x.OfficialCurrencyId = invoiceCxP?.CurrencyId)?.Value
        End If

        INDlbDateExpire.Text = _accountPayableXpo.ExpirationDate 'INDGvInvoiceShare.GetFocusedRowCellValue("DateExpires")
        InvoiceBalanceCxP(Me._currencyAbbreviation) = Math.Round(invoiceCxP.Balance / TRMValue, 2)
        If ListCrossingAccountDetailCxP IsNot Nothing AndAlso ListCrossingAccountDetailCxP.Count > 0 Then
            If ListCrossingAccountDetailCxP.FindAll(Function(x) x.BillNumber.Equals(_accountPayableXpo.BillNumber)).Cast(Of CrossingAccountDetailCxP).ToList().Count > 0 Then
                If (_crossingAccount.Status = 0 OrElse _crossingAccount.Status = 1) Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvoiceExists", MODULE_NAME)
                End If
                CleanControlsInvoiceCxP()
            End If
        End If
    End Sub

    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._crossingAccount IsNot Nothing AndAlso Me._crossingAccount.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Loads the data share invoice cx c.
    ''' </summary>
    ''' <param name="accountRecivableXpo">The account recivable xpo.</param>
    Private Async Sub LoadDataShareInvoiceCxC(accountRecivableXpo As Infrastructure.Data.Xpo.PortfolioRepository.PortfolioAccountReceivableAccountingXpo)
        Dim TRMValue As Decimal = 1

        If Not Await Me.CurrencyExchangeActionsCXC(True, Me.CurrencyId, Me.CurrencyAbbreviation, accountRecivableXpo?.CurrencyId) Then
            CleanControlsInvoiceCxP()
            Exit Sub
        End If

        If Me.CurrencyId <> accountRecivableXpo?.CurrencyId Then
            TRMValue = _listTRM.FirstOrDefault(Function(x) x.CurrencyId = Me.CurrencyId AndAlso x.OfficialCurrencyId = accountRecivableXpo?.CurrencyId)?.Value
        End If

        INDlbInvoiceDateCxC.Text = accountRecivableXpo.AccountReceivableId.ExpiredDate
        InvoiceBalanceCxC(Me.CurrencyAbbreviation) = Math.Round(accountRecivableXpo.Balance / TRMValue, 2)
        If ListCrossingAccountDetailCxC IsNot Nothing AndAlso ListCrossingAccountDetailCxC.Count > 0 Then
            If ListCrossingAccountDetailCxC.FindAll(Function(x) x.BillNumber.Equals(accountRecivableXpo.AccountReceivableId.InvoiceNumber) AndAlso x.MainAccountId = accountRecivableXpo.MainAccountId.Id).Cast(Of CrossingAccountDetailCxC).ToList().Count > 0 Then
                If (_crossingAccount.Status = 0 OrElse _crossingAccount.Status = 1) Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvoiceExists", MODULE_NAME)
                End If
                CleanControlsInvoiceCxC()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles de factura cxp
    ''' </summary>
    Public Sub CleanControlsInvoiceCxP()
        AccountPayableShareId = 0
        INDlbBalance.Text = String.Empty
        INDlbDateExpire.Text = String.Empty
        CrossingPercentCxP = 0
        CrossingValueCxP = 0
        _cxpPercentChanging = False
        _cxpValueChanging = False
        IdCashFlowConceptCxP = Nothing
        INDLciExchange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDTxtExchange.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Elimina un CxP de la rejilla
    ''' </summary>
    Private Sub DeleteAccountPayableShare()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _crossingAccountDetailCxP As CrossingAccountDetailCxP = DirectCast(INDgvAcountPayable.GetFocusedRow(), CrossingAccountDetailCxP)
            If _crossingAccount.ChangeTracker.State <> ObjectState.Added Then
                _crossingAccount.MarkAsModified()
            End If

            If _crossingAccountDetailCxP.Id > 0 Then
                If listDeleteCxP Is Nothing Then
                    listDeleteCxP = New List(Of CrossingAccountDetailCxP)
                End If
                listDeleteCxP.Add(_crossingAccountDetailCxP)
            End If

            ListCrossingAccountDetailCxP.Remove(_crossingAccountDetailCxP)
            _CxPValue = ListCrossingAccountDetailCxP.Sum(Function(x) x.CrossingValue)
            validateAddDetails()
            ctrTmp.PrintValueCrossing()
            INDgcAcountPayable.RefreshDataSource()
        End If
    End Sub

    Private Sub DeleteAccountConcept()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _crossingAccountDetailConcept As CrossingAccountDetailOtherConcept = DirectCast(INDGvCrossingConcepts.GetFocusedRow(), CrossingAccountDetailOtherConcept)
            _crossingAccountDetailConcept.MarkAsDeleted()
            If _crossingAccount.ChangeTracker.State <> ObjectState.Added Then
                _crossingAccount.MarkAsModified()
            End If
            ListCrossingAccountOtherConcepts = _crossingAccount.CrossingAccountDetailOtherConcept.ToList()

            _CxPValue = ListCrossingAccountDetailCxP.Sum(Function(x) x.CrossingValue) + _crossingAccount.CrossingAccountDetailOtherConcept.Where(Function(x) x.Nature = 1).Sum(Function(o) o.Value)
            _CxCValue = ListCrossingAccountDetailCxC.Sum(Function(x) x.CrossingValue) + _crossingAccount.CrossingAccountDetailOtherConcept.Where(Function(x) x.Nature = 2).Sum(Function(o) o.Value)

            ctrTmp.PrintValueCrossing()
            INDgcAcountPayable.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Elimina un CxC de la rejilla
    ''' </summary>
    Private Sub DeleteAccountReceivable()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _crossingAccountDetailCxC As CrossingAccountDetailCxC = DirectCast(INDgvAccountRecivable.GetFocusedRow(), CrossingAccountDetailCxC)
            If _crossingAccount.ChangeTracker.State <> ObjectState.Added Then
                _crossingAccount.MarkAsModified()
            End If

            If _crossingAccountDetailCxC.Id > 0 Then
                If listDeleteCxC Is Nothing Then
                    listDeleteCxC = New List(Of CrossingAccountDetailCxC)
                End If
                listDeleteCxC.Add(_crossingAccountDetailCxC)
            End If

            ListCrossingAccountDetailCxC.Remove(_crossingAccountDetailCxC)
            _CxCValue = ListCrossingAccountDetailCxC.Sum(Function(x) x.CrossingValue)
            validateAddDetails()
            ctrTmp.PrintValueCrossing()
            INDgcAccountRecivable.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Adds the account payable share cx p to list.
    ''' </summary>
    Private Sub AddAccountPayableCxPToList()
        Dim _crossingAccountDetailCxP As New CrossingAccountDetailCxP()
        With _crossingAccountDetailCxP
            .AccountPayableId = _accountPayableXpo.Id
            .MainAccountId = _accountPayableXpo.IdAccount.Id
            .CrossingValue = Math.Round(CrossingValueCxP, 2, MidpointRounding.AwayFromZero)

            .MainAccountDescription = _accountPayableXpo.IdAccount.NumberName
            .BillNumber = _accountPayableXpo.BillNumber
            .Value = _accountPayableXpo.Value
            .Balance = _accountPayableXpo.Balance
            .ThirdPartyDescription = _accountPayableXpo.ThirdPartyFullName
            .Detail = "Nota de Tesoreria : {0}, Factura CxP : " & _accountPayableXpo.BillNumber
            .IdCashFlowConcept = IdCashFlowConceptCxP
            .CodeNameCashFlowConcept = INDSleCashFlowConceptCxP.Text
            .InvoiceCurrencyId = _accountPayableXpo?.Currency.Id
            .InvoiceCurrencyAbbreviation = _accountPayableXpo?.Currency.Abbreviation
        End With
        ListCrossingAccountDetailCxP.Add(_crossingAccountDetailCxP)
        If _crossingAccount.ChangeTracker.State <> ObjectState.Added Then
            _crossingAccount.MarkAsModified()
        End If
        _CxPValue = ListCrossingAccountDetailCxP.Sum(Function(x) x.CrossingValue)
        validateAddDetails()
        ctrTmp.PrintValueCrossing()
        INDgcAcountPayable.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Adds the account payable share cx c to list.
    ''' </summary>
    Private Sub AddAccountReceivableCxCToList()
        Dim _crossingAccountDetailCxC As New CrossingAccountDetailCxC()
        With _crossingAccountDetailCxC
            .AccountReceivableId = _accountRecivableAccountingXpo.AccountReceivableId.Id
            .AccountReceivableAccountingId = _accountRecivableAccountingXpo.Id
            .MainAccountId = _accountRecivableAccountingXpo.MainAccountId.Id
            .CrossingValue = Math.Round(CrossingValueCxC, 2, MidpointRounding.AwayFromZero)

            .MainAccountDescription = _accountRecivableAccountingXpo.MainAccountId.NumberName
            .BillNumber = _accountRecivableAccountingXpo.AccountReceivableId.InvoiceNumber
            .Value = _accountRecivableAccountingXpo.Value
            .Balance = _accountRecivableAccountingXpo.Balance
            .ThirdPartyDescription = _accountRecivableAccountingXpo?.ThirdPartyId?.NitName
            .Detail = "Nota de Tesoreria : {0}, Factura CxC : " & _accountRecivableAccountingXpo.AccountReceivableId.InvoiceNumber
            .IdCashFlowConcept = IdCashFlowConceptCxC
            .CodeNameCashFlowConcept = INDSleCashFlowConceptCxC.Text
            .InvoiceCurrencyId = _accountRecivableAccountingXpo?.CurrencyId
            .InvoiceCurrencyAbbreviation = _accountRecivableAccountingXpo?.CurrencyAbbreviation
        End With
        ListCrossingAccountDetailCxC.Add(_crossingAccountDetailCxC)
        If _crossingAccount.ChangeTracker.State <> ObjectState.Added Then
            _crossingAccount.MarkAsModified()
        End If
        _CxCValue = ListCrossingAccountDetailCxC.Sum(Function(x) x.CrossingValue)
        validateAddDetails()
        ctrTmp.PrintValueCrossing()
        INDgcAccountRecivable.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Valida los controles de añadir facturas de CxP
    ''' </summary>
    ''' <returns></returns>
    Private Function validateControlsInvoiceShareCxP() As Tuple(Of Boolean, String)
        Dim errorList As New StringBuilder()
        If AccountPayableShareId = 0 Then
            errorList.AppendLine(INDliInvoiceShareCxP.Text)
        End If
        If InvoiceBalanceCxP = 0 Then
            errorList.AppendLine(INDliBalanceCxP.Text)
        End If
        If INDlbDateExpire.Text = String.Empty Then
            errorList.AppendLine(INDliDateExpireCxP.Text)
        End If
        If CrossingValueCxP = 0 Then
            errorList.AppendLine(INDliCrossingValueCxP.Text)
        End If
        'If CrossingPercentCxP = 0 Then
        '    errorList.AppendLine(INDliCrossingPercentCxP.Text)
        'End If
        Return New Tuple(Of Boolean, String)(errorList.Length = 0, errorList.ToString())
    End Function

    ''' <summary>
    ''' Validates the controls invoice share CxC.
    ''' </summary>
    ''' <returns></returns>
    Private Function validateControlsInvoiceShareCxC() As Tuple(Of Boolean, String)
        Dim errorList As New StringBuilder()
        If AccountRecivableId = 0 Then
            errorList.AppendLine(INDliInvoiceCxC.Text)
        End If
        If InvoiceBalanceCxC = 0 Then
            errorList.AppendLine(INDliBalanceCxC.Text)
        End If
        If INDlbInvoiceDateCxC.Text = String.Empty Then
            errorList.AppendLine(INDliInvoiceDateCxC.Text)
        End If
        If CrossingValueCxC = 0 Then
            errorList.AppendLine(INDliCrossingValueCxC.Text)
        End If
        'If CrossingPercentCxC = 0 Then
        '    errorList.AppendLine(INDliCrossingPercentCxC.Text)
        'End If
        Return New Tuple(Of Boolean, String)(errorList.Length = 0, errorList.ToString())
    End Function

    ''' <summary>
    ''' Cleans the controls invoice CxC.
    ''' </summary>
    Private Sub CleanControlsInvoiceCxC()
        AccountRecivableId = 0
        INDlbBalanceCxC.Text = String.Empty
        INDlbInvoiceDateCxC.Text = String.Empty
        CrossingPercentCxC = 0
        CrossingValueCxC = 0
        _cxcValueChanging = False
        _cxcPercentChanging = False
        IdCashFlowConceptCxC = Nothing
        INDLciExchangeCXC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDTxtExchangeCXC.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Searches the supplier.
    ''' </summary>
    ''' <returns></returns>
    Private Function SearchSupplier() As Task
        Return System.Threading.Tasks.Task.Factory.StartNew(Sub()
                                                                Using Model As New MSupplier(Me.Tag)
                                                                    Dim Supplier As Supplier = (Model.GetSupplierByIdThirdParty(ThirdPartyId))
                                                                    If Supplier IsNot Nothing AndAlso Supplier.Id > 0 Then
                                                                        _suppierId = Supplier.Id
                                                                    Else
                                                                        _suppierId = 0
                                                                    End If
                                                                End Using
                                                            End Sub)
    End Function

    ''' <summary>
    ''' Carga las facturas de CxC en la rejilla
    ''' </summary>
    Private Sub LoadCxCInvoices()
        INDgvAccountRecivable.ShowLoadingPanel()
        tokenCxCAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Try
                                      Dim result = _presenter.ListDetailsCxC(_crossingAccount.Id)
                                      If Not tokenCxCAsync.IsCancellationRequested Then
                                          INDgcAccountRecivable.SafeInvoke(Sub()
                                                                               If result IsNot Nothing AndAlso result.Count > 0 Then
                                                                                   If ListCrossingAccountDetailCxC Is Nothing Then
                                                                                       ListCrossingAccountDetailCxC = New List(Of CrossingAccountDetailCxC)
                                                                                   End If
                                                                                   For Each item In result
                                                                                       Dim entity As New CrossingAccountDetailCxC
                                                                                       entity.StartTracking()
                                                                                       With entity
                                                                                           .Id = item.Id
                                                                                           .CrossingAccountId = item.CrossingAccountId
                                                                                           .AccountReceivableId = item.AccountReceivableId
                                                                                           .AccountReceivableAccountingId = item.AccountReceivableAccountingId
                                                                                           .MainAccountId = item.MainAccountId
                                                                                           .CrossingValue = item.CrossingValue
                                                                                           .Detail = item.Detail
                                                                                           .IdCashFlowConcept = item.IdCashFlowConcept
                                                                                           .MainAccountDescription = item.MainAccountDescription
                                                                                           .BillNumber = item.BillNumber
                                                                                           .Value = item.Value
                                                                                           .Balance = item.Balance
                                                                                           .ThirdPartyDescription = item.ThirdPartyDescription
                                                                                           .CodeNameCashFlowConcept = item.CodeNameCashFlowConcept
                                                                                           If Not item.CurrencyId > 0 Then
                                                                                               .InvoiceCurrencyAbbreviation = indigo.CurrencyISO4217
                                                                                               .InvoiceCurrencyId = indigo.OfficialCurrencyId
                                                                                           Else
                                                                                               .InvoiceCurrencyAbbreviation = item.CurrencyAbbreviation
                                                                                               .InvoiceCurrencyId = item.CurrencyId
                                                                                           End If
                                                                                       End With
                                                                                       entity.MarkAsUnchanged()
                                                                                       ListCrossingAccountDetailCxC.Add(entity)
                                                                                   Next
                                                                               End If

                                                                               INDgvAccountRecivable.HideLoadingPanel()
                                                                               If ListCrossingAccountDetailCxC IsNot Nothing AndAlso ListCrossingAccountDetailCxC.Count > 0 Then
                                                                                   INDgcAccountRecivable.RefreshDataSource()
                                                                                   _CxCValue = ListCrossingAccountDetailCxC.Sum(Function(x) x.CrossingValue) + _crossingAccount.CrossingAccountDetailOtherConcept.Where(Function(x) x.Nature = 2).Sum(Function(o) o.Value)
                                                                                   ctrTmp.PrintValueCrossing()
                                                                                   If CallNote And _CxPValue > 0 Then
                                                                                       RaiseEvent LoadControlsFinish()
                                                                                   End If
                                                                                   INDsleThirdParty.Properties.ReadOnly = True
                                                                               End If
                                                                               validateAddDetails()
                                                                           End Sub)
                                      End If
                                  Catch ex As Exception
                                      If Not tokenCxCAsync.IsCancellationRequested Then
                                          INDgcAccountRecivable.SafeInvoke(Sub()
                                                                               INDgvAccountRecivable.HideLoadingPanel()
                                                                               Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                           End Sub)
                                      End If
                                  End Try
                              End Sub, tokenCxCAsync.Token)
    End Sub

    ''' <summary>
    ''' Carga las facturas de CxP en la rejilla
    ''' </summary>
    Private Sub LoadCxPInvoices()
        INDgvAcountPayable.ShowLoadingPanel()
        tokenCxPAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Try
                                      Dim result = _presenter.ListDetailsCxP(_crossingAccount.Id)
                                      If Not tokenCxPAsync.IsCancellationRequested Then
                                          INDgcAcountPayable.SafeInvoke(Sub()
                                                                            If result IsNot Nothing AndAlso result.Count > 0 Then
                                                                                If ListCrossingAccountDetailCxP Is Nothing Then
                                                                                    ListCrossingAccountDetailCxP = New List(Of CrossingAccountDetailCxP)
                                                                                End If
                                                                                For Each item In result
                                                                                    Dim entity As New CrossingAccountDetailCxP
                                                                                    entity.StartTracking()
                                                                                    With entity
                                                                                        .Id = item.Id
                                                                                        .CrossingAccountId = item.CrossingAccountId
                                                                                        .AccountPayableId = item.AccountPayableId
                                                                                        .MainAccountId = item.MainAccountId
                                                                                        .CrossingValue = item.CrossingValue
                                                                                        .Detail = item.Detail
                                                                                        .IdCashFlowConcept = item.IdCashFlowConcept
                                                                                        .MainAccountDescription = item.MainAccountDescription
                                                                                        .BillNumber = item.BillNumber
                                                                                        .Value = item.Value
                                                                                        .Balance = item.Balance
                                                                                        .ThirdPartyDescription = item.ThirdPartyDescription
                                                                                        .CodeNameCashFlowConcept = item.CodeNameCashFlowConcept
                                                                                        If Not item.CurrencyId > 0 Then
                                                                                            .InvoiceCurrencyAbbreviation = indigo.CurrencyISO4217
                                                                                            .InvoiceCurrencyId = indigo.OfficialCurrencyId
                                                                                        Else
                                                                                            .InvoiceCurrencyAbbreviation = item.CurrencyAbbreviation
                                                                                            .InvoiceCurrencyId = item.CurrencyId
                                                                                        End If
                                                                                    End With
                                                                                    entity.MarkAsUnchanged()
                                                                                    ListCrossingAccountDetailCxP.Add(entity)
                                                                                Next
                                                                            End If

                                                                            INDgvAcountPayable.HideLoadingPanel()
                                                                            If ListCrossingAccountDetailCxP IsNot Nothing AndAlso ListCrossingAccountDetailCxP.Count > 0 Then
                                                                                INDgcAcountPayable.RefreshDataSource()
                                                                                _CxPValue = ListCrossingAccountDetailCxP.Sum(Function(x) x.CrossingValue) + _crossingAccount.CrossingAccountDetailOtherConcept.Where(Function(x) x.Nature = 1).Sum(Function(o) o.Value)
                                                                                ctrTmp.PrintValueCrossing()
                                                                                If CallNote And _CxCValue > 0 Then
                                                                                    RaiseEvent LoadControlsFinish()
                                                                                End If
                                                                            End If
                                                                            validateAddDetails()
                                                                        End Sub)
                                      End If
                                  Catch ex As Exception
                                      If Not tokenCxPAsync.IsCancellationRequested Then
                                          INDgcAcountPayable.SafeInvoke(Sub()
                                                                            INDgvAcountPayable.HideLoadingPanel()
                                                                            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                        End Sub)
                                      End If
                                  End Try
                              End Sub, tokenCxPAsync.Token)
    End Sub

    Private Sub LoadConcepts()
        If ListCrossingAccountOtherConcepts Is Nothing Then
            ListCrossingAccountOtherConcepts = New List(Of CrossingAccountDetailOtherConcept)()
        End If
        ListCrossingAccountOtherConcepts = _crossingAccount.CrossingAccountDetailOtherConcept.ToList()
        If ListCrossingAccountOtherConcepts IsNot Nothing AndAlso ListCrossingAccountOtherConcepts.Count > 0 Then
            INDGcCrossingAccountOtherConcepts.RefreshDataSource()

            _CxPValue = ListCrossingAccountDetailCxP.Sum(Function(x) x.CrossingValue) + _crossingAccount.CrossingAccountDetailOtherConcept.Where(Function(x) x.Nature = 1).Sum(Function(o) o.Value)
            _CxCValue = ListCrossingAccountDetailCxC.Sum(Function(x) x.CrossingValue) + _crossingAccount.CrossingAccountDetailOtherConcept.Where(Function(x) x.Nature = 2).Sum(Function(o) o.Value)

            ctrTmp.PrintValueCrossing()
        End If
    End Sub

    ''' <summary>
    ''' Deletes the item cx c with other third.
    ''' </summary>
    Private Sub DeleteItemCxCWithOtherThird()
        Dim lstItemToDelete As New List(Of CrossingAccountDetailCxC)()
        Dim model As New MAccountReceivable(Me.Tag)
        Dim accReceivable As AccountReceivable = Nothing
        For Each item In ListCrossingAccountDetailCxC
            accReceivable = model.GetAccountReceivableById(item.AccountReceivableId)
            If accReceivable.ThirdPartyId <> ThirdPartyId Then
                lstItemToDelete.Add(item)
            End If
        Next
        If lstItemToDelete IsNot Nothing AndAlso lstItemToDelete.Count > 0 Then
            If listDeleteCxC Is Nothing Then
                listDeleteCxC = New List(Of CrossingAccountDetailCxC)
            End If
            lstItemToDelete.ForEach(Sub(x)
                                        If x.Id > 0 Then
                                            listDeleteCxC.Add(x)
                                        End If
                                        ListCrossingAccountDetailCxC.Remove(x)
                                    End Sub)
        End If
        validateAddDetails()
        INDgcAccountRecivable.RefreshDataSource()
    End Sub

    Private Sub DeleteItemConceptWithOtherThird()
        Dim lstItemToDelete As New List(Of CrossingAccountDetailOtherConcept)()
        For Each item In _crossingAccount.CrossingAccountDetailOtherConcept
            If item.ThirdPartyId <> ThirdPartyId Then
                lstItemToDelete.Add(item)
            End If
        Next
        lstItemToDelete.ForEach(Function(o) _crossingAccount.CrossingAccountDetailOtherConcept.ToList().Where(Function(x) x.Equals(o)).FirstOrDefault().MarkAsDeleted())
        ListCrossingAccountOtherConcepts = _crossingAccount.CrossingAccountDetailOtherConcept.ToList()
        INDGcCrossingAccountOtherConcepts.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Deletes the item cx p with other third.
    ''' </summary>
    Private Sub DeleteItemCxPWithOtherThird()
        Dim lstItemToDelete As New List(Of CrossingAccountDetailCxP)()
        Dim model As New MAccountPayable(Me.Tag)
        Dim accPayable As AccountPayable = Nothing
        For Each item In ListCrossingAccountDetailCxP
            accPayable = model.GetAccountPayableById(item.AccountPayableId)
            If accPayable.IdThirdParty <> ThirdPartyId Then
                lstItemToDelete.Add(item)
            End If
        Next
        If lstItemToDelete IsNot Nothing AndAlso lstItemToDelete.Count > 0 Then
            If listDeleteCxP Is Nothing Then
                listDeleteCxP = New List(Of CrossingAccountDetailCxP)
            End If
            lstItemToDelete.ForEach(Sub(x)
                                        If x.Id > 0 Then
                                            listDeleteCxP.Add(x)
                                        End If
                                        ListCrossingAccountDetailCxP.Remove(x)
                                    End Sub)
        End If
        validateAddDetails()
        INDgcAcountPayable.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="tipo"></param>
    Public Sub GetCashFlowConcept(ByVal tipo As String)
        Dim _presenter As New PCashFlowConcept
        If tipo = 1 Then
            CashFlowConceptCxCDataSource = _presenter.GetCashFlowConcept(New String() {"1", tipo})
        Else
            CashFlowConceptCxPDataSource = _presenter.GetCashFlowConcept(New String() {"1", tipo})
        End If
        _presenter = Nothing
    End Sub

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
    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Confirma el cruce de cuentas
    ''' </summary>
    Public Async Sub Confirmar()
        Dim resultOption As DialogResult
        Try
            Using Model As New MVoucherTransactionCrossing(Me.Tag.ToString())
                Dim auxCrossingAccount As CrossingAccount = (Await Model.GetCrossingAccountById(_crossingAccount.Id))
                If ValidateEntity(auxCrossingAccount) Then
                    resultOption = MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo)
                Else
                    resultOption = MessageIndigo.Show(ResourceManager.GetString("ConfirmMessageWithUpdate"), MessageType.Question, Me.Text, Botones.SiNo)
                End If
                If resultOption = System.Windows.Forms.DialogResult.Yes Then
                    AsyncLoader(True)
                    Dim Result = Await Model.ConfirmCrossingAccount(Me._crossingAccount.Id)
                    If Result.StateResult = True Then
                        Dim consecutive = Result.ObjectEmbbeded
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("DocumentConfirmWithConsecutive"), consecutive)
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _crossingAccount.Id, 0, _crossingAccount.Id, reportType)
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        'VoucherTransaction.VoucherTransactionDetails.Clear()
                        If Result.Message IsNot Nothing Then
                            generateListError(Result.Message)
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' valida la entidad para ver si hubu cambios
    ''' </summary>
    Private Function ValidateEntity(ByVal auxCrossingAccount As CrossingAccount) As Boolean
        If (auxCrossingAccount.Id = _crossingAccount.Id AndAlso auxCrossingAccount.Code.Equals(_crossingAccount.Code) AndAlso auxCrossingAccount.ThirdPartyId = _crossingAccount.ThirdPartyId AndAlso
            auxCrossingAccount.Description.Equals(_crossingAccount.Description) AndAlso auxCrossingAccount.Status = _crossingAccount.Status) Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar(withConfirm As Boolean)
        If ValidateControls() = False Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MVoucherTransactionCrossing(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SaveCrossingAccount(Me._crossingAccount, withConfirm, Me._idCurrentSequence)
                If Result.StateResult = True Then
                    Me._crossingAccount = Result.ObjectEmbbeded
                    If withConfirm Then
                        If String.IsNullOrEmpty(Result.Message) Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("DocumentSaveWithConsecutive"),
                                                                                    Result.ObjectEmbbeded.Code,
                                                                                    String.Join(", ", Result.MessageResult))
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _crossingAccount.Id, 0, _crossingAccount.Id, reportType)
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SavedButNotConfirmed", MODULE_NAME), Result.ObjectEmbbeded.Code, Result.Message)
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _crossingAccount.Id, 0, _crossingAccount.Id, reportType)
                        End If
                    Else
                        If _crossingAccount.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._sequence.TreasurySequenceDetail(0).Id).RemoveAt(0)
                            End If
                            If Me._sequence.Sequential Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                            End If
                        ElseIf _crossingAccount.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _crossingAccount.Id, 0, _crossingAccount.Id, reportType)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _crossingAccount.Id, 0, _crossingAccount.Id, reportType)
                    End Select
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    If Result.Message IsNot Nothing Then
                        generateListError(Result.Message)
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Anulars this instance.
    ''' </summary>
    Public Async Sub Anular()
        If Me._crossingAccount IsNot Nothing AndAlso Me._crossingAccount.Id > 0 Then
            AssigningValues()
            If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    _crossingAccount.Status = 3
                    Using Model As New MVoucherTransactionCrossing(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.SaveCrossingAccount(Me._crossingAccount, False, Me._idCurrentSequence)
                        If result.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            Me._crossingAccount = result.ObjectEmbbeded
                            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _crossingAccount.Id, 0, _crossingAccount.Id, reportType)
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            'VoucherTransaction.VoucherTransactionDetails.Clear()
                            If result.Message IsNot Nothing Then
                                generateListError(result.Message)
                            End If
                        End If
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Sub Guardar() Implements Base.ICrudBase.Guardar

    End Sub

    ''' <summary>
    ''' Genera el mensaje de error
    ''' </summary>
    ''' <param name="errors">The errors.</param>
    Private Sub generateListError(errors As String)
        Dim listError As New StringBuilder()
        listError.AppendLine(ResourceManager.GetString("ErrorListMessage"))
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.Advertencia) = listError.ToString()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewVoucherTransactionCrossing()
        End If
    End Sub

#End Region

#Region "Events"

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown
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
                    Await Me.NewVoucherTransactionCrossing()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
#End Region

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _suppierId = Nothing
        _CxPValue = Nothing
        _CxCValue = Nothing
        ctrTmp = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        _presenter = Nothing
        record = Nothing
        _accountPayableXpo = Nothing
        _accountRecivableAccountingXpo = Nothing
        _crossingAccount = Nothing
        _cxcValueChanging = Nothing
        _cxcPercentChanging = Nothing
        _cxpValueChanging = Nothing
        _cxpPercentChanging = Nothing
        reportType = Nothing
        varImp = Nothing
        IsLoadControls = Nothing
        IdCashFlowConceptCxC = Nothing
        IdCashFlowConceptCxP = Nothing
        CashFlowConceptCodeNameOther = Nothing
        CashFlowConceptCxCDataSource = Nothing
        CashFlowConceptCxPDataSource = Nothing
    End Sub
    ''' <summary>
    ''' Handles the Load event of the FrmVoucherTransactionCrossing control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmVoucherTransactionCrossing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDBtnExportBillsStructureCxP.AddRangeColumns("Cuenta Por Pagar", "Valor", "Concepto flujo efectivo")
        INDBtnExportBillsStructureCxC.AddRangeColumns("Número de Factura", "Cuenta Contable", "Valor", "Concepto flujo efectivo")
        'INDBtnExportBillsStructureCxP.AddColumnFormat("Concepto flujo efectivo", DevExpress.Export.Xl.XlNumberFormat.Text)
        'INDBtnExportBillsStructureCxC.AddColumnFormat("Concepto flujo efectivo", DevExpress.Export.Xl.XlNumberFormat.Text)

        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PVoucherTransactionCrossing(Me)
        _presenter.GetSequence()
        _presenter.LoadDefinitionLayout()
        _presenter.InitializeThirdParty()
        _presenter.LoadDateServer(Me.GetDateServer())

        Dim _listActions As New List(Of eAcciones)
        _listActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvAcountPayable, _listActions)
        For Each col As GridColumn In INDgvAcountPayable.Columns
            If col.Name.Equals("colActions") Then
                col.Width = 200
            End If
        Next
        IndigoGridView1.MoreInfoColunmns(INDgvAcountPayable)

        IndigoGridView2.SetListAcction(INDgvAccountRecivable, _listActions)
        For Each col As GridColumn In INDgvAccountRecivable.Columns
            If col.Name.Equals("colActions") Then
                col.Width = 100
            End If
        Next

        IndigoGridView3.SetListAcction(INDGvCrossingConcepts, _listActions)
        For Each col As GridColumn In INDGvCrossingConcepts.Columns
            If col.Name.Equals("colActions") Then
                col.Width = 100
            End If
        Next

        IndigoGridView2.MoreInfoColunmns(INDgvAccountRecivable)
        IndigoGridControl1.RefreshGrid(INDgcAcountPayable)
        IndigoGridControl1.RefreshGrid(INDgcAccountRecivable)

        INDgleCrossingType.Properties.DataSource = DatasourceCrossingType
        INDSleNoteConcept.Properties.PopupFormSize = New Size(700, 500)
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmVoucherTransactionCrossing control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmVoucherTransactionCrossing_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If tokenCxCAsync IsNot Nothing Then
            tokenCxCAsync.Cancel()
        End If
        If tokenCxPAsync IsNot Nothing Then
            tokenCxPAsync.Cancel()
        End If
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the INDpceAddInvoiceCxP control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddInvoiceCxP_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceAddInvoiceCxP.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDpceAddInvoiceCxP.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDpceAddInvoiceCxC control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddInvoiceCxC_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceAddInvoiceCxC.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDpceAddInvoiceCxC.ShowPopup()
        End If
    End Sub

    Private Sub INDtxtCrossingValueCxC_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtCrossingValueCxC.KeyDown
        If e.KeyCode <> Keys.Enter Then
            _cxcValueChanging = True
            _cxcPercentChanging = False
        End If
    End Sub

    Private Sub INDspCrossingPercentCxC_KeyDown(sender As Object, e As KeyEventArgs) Handles INDspCrossingPercentCxC.KeyDown
        If e.KeyCode <> Keys.Enter Then
            _cxcValueChanging = False
            _cxcPercentChanging = True
        End If
    End Sub

    Private Sub INDtxtCrossingValueCxP_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtCrossingValueCxP.KeyDown
        If e.KeyCode <> Keys.Enter Then
            _cxpValueChanging = True
            _cxpPercentChanging = False
        End If
    End Sub

    Private Sub INDspCrossingPercentCxP_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtCrossingPercentCxP.KeyDown
        If e.KeyCode <> Keys.Enter Then
            _cxpValueChanging = False
            _cxpPercentChanging = True
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleThirdParty control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleThirdParty_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleThirdParty.EditValueChanged
        CleanControlsInvoiceCxP()
        CleanControlsInvoiceCxC()
        CleanControlsPopupOtherConcept()
        If ThirdPartyId <> 0 Then
            INDBtnImportFileCxC.Enabled = True
            INDBtnImportFileCxP.Enabled = True
            INDSleThirdPartyConcept.EditValue = INDsleThirdParty.EditValue
            Await SearchSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleInvoiceShareCxP control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleInvoiceShareCxP_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleInvoiceCxP.EditValueChanged
        If AccountPayableShareId <> 0 Then
            _accountPayableXpo = DirectCast(DirectCast(INDGvInvoiceShare.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableXpo)
            LoadDataShareInvoiceCxp(_accountPayableXpo)
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleInvoiceCxC control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleInvoiceCxC_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleInvoiceCxC.EditValueChanged
        If AccountRecivableId <> 0 Then
            If INDgvInvoiceCxC.GetFocusedRow.GetType().Equals((GetType(DevExpress.Data.NotLoadedObject))) Then
                Exit Sub
            End If
            _accountRecivableAccountingXpo = DirectCast(DirectCast(INDgvInvoiceCxC.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PortfolioRepository.PortfolioAccountReceivableAccountingXpo)
            LoadDataShareInvoiceCxC(_accountRecivableAccountingXpo)
        End If
    End Sub

    Private Sub INDgleCrossingType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleCrossingType.EditValueChanged
        INDsleInvoiceCxC.Properties.DataSource = Nothing
        INDsleInvoiceCxP.Properties.DataSource = Nothing
        'Mostrar mensaje de loading en las rejillas
        If CrossingType <> 0 Then
            If CrossingType = eCrossingType.SameThird Then
                If IsLoadControls = False Then
                    DeleteItemCxCWithOtherThird()
                    DeleteItemCxPWithOtherThird()
                    DeleteItemConceptWithOtherThird()
                End If
                INDcolThirdPCxP.Visible = False
                INDcolThirdPCxC.Visible = False
                INDSleThirdPartyConcept.Properties.ReadOnly = True
                INDSleThirdPartyConcept.EditValue = INDsleThirdParty.EditValue
            ElseIf CrossingType = eCrossingType.OtherThird Then
                INDcolThirdPCxP.Visible = True
                INDcolThirdPCxC.Visible = True
                INDSleThirdPartyConcept.Properties.ReadOnly = False
            End If
        Else
            INDcolThirdPCxP.Visible = False
            INDcolThirdPCxC.Visible = False
            INDSleThirdPartyConcept.Properties.ReadOnly = False
            INDSleThirdPartyConcept.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IINDSleCashFlowConceptCxC_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCashFlowConceptCxC.EditValueChanged
        If String.IsNullOrEmpty(INDSleCashFlowConceptCxC.EditValue) Then
            INDSleCashFlowConceptCxC.Properties.NullText = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IINDSleCashFlowConceptCxP_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCashFlowConceptCxP.EditValueChanged
        If String.IsNullOrEmpty(INDSleCashFlowConceptCxP.EditValue) Then
            INDSleCashFlowConceptCxP.Properties.NullText = String.Empty
        End If
    End Sub


    ''' <summary>
    ''' evento que se dispara al cambiar la moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCurrency_Properties_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCurrency.Properties.EditValueChanged
        If Me.CurrencyId Is Nothing Then
            Me.LoadDefaultCurrency()
            Exit Sub
        Else
            Me.CurrencyId(If(INDSleCurrency Is Nothing, indigo.CurrencyISO4217, INDSleCurrency.Text)) = If(INDSleCurrency.EditValue Is Nothing, indigo.OfficialCurrencyId, INDSleCurrency.EditValue)
        End If

    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleInvoiceShareCxP control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleInvoiceShareCxP_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleInvoiceCxP.QueryPopUp
        If INDsleInvoiceCxP.Properties.DataSource Is Nothing AndAlso (_crossingAccount.Status = 0 OrElse _crossingAccount.Status = 1) Then
            If CrossingType <> 0 Then
                If CrossingType = eCrossingType.SameThird Then
                    If _suppierId <> 0 Then
                        _presenter.LoadInvoiceCxPBySupplierId(_suppierId)
                    Else
                        AccountPayableDatasource = Nothing
                    End If
                ElseIf CrossingType = eCrossingType.OtherThird Then
                    _presenter.LoadInvoiceCxP()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleInvoiceCxC control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleInvoiceCxC_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleInvoiceCxC.QueryPopUp
        If INDsleInvoiceCxC.Properties.DataSource Is Nothing AndAlso (_crossingAccount.Status = 0 OrElse _crossingAccount.Status = 1) Then
            If CrossingType = eCrossingType.SameThird Then
                If ThirdPartyId <> 0 Then
                    _presenter.LoadInvoiceCxCBySupplierId(ThirdPartyId)
                Else
                    AccountRecivableDatasource = Nothing
                End If
            ElseIf CrossingType = eCrossingType.OtherThird Then
                _presenter.LoadInvoiceCxC()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDSleNoteConcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleNoteConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleNoteConcept.QueryPopUp
        If INDSleNoteConcept.Properties.DataSource Is Nothing AndAlso (_crossingAccount.Status = 0 OrElse _crossingAccount.Status = 1) Then
            Using model As New MNoteConcepts(Me.Tag)
                INDSleNoteConcept.Properties.DataSource = model.ListAllNoteConceptXpo()
            End Using
        End If
    End Sub

    Private Sub INDSleMainAccountConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleMainAccountConcept.QueryPopUp
        If INDSleMainAccountConcept.Properties.DataSource Is Nothing AndAlso (_crossingAccount.Status = 0 OrElse _crossingAccount.Status = 1) Then
            Using model As New MNoteConcepts(Me.Tag)
                INDSleMainAccountConcept.Properties.DataSource = model.ListMainAccountXpo()
            End Using
        End If
    End Sub

    Private Sub SearchLookUpEdit1_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdPartyConcept.QueryPopUp
        If INDSleThirdPartyConcept.Properties.DataSource Is Nothing AndAlso (_crossingAccount.Status = 0 OrElse _crossingAccount.Status = 1) Then
            Using model As New MNoteConcepts(Me.Tag)
                INDSleThirdPartyConcept.Properties.DataSource = model.ListThirdPartyXpo()
            End Using
        End If
    End Sub

    Private Sub INDSLeCostCenterConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSLeCostCenterConcept.QueryPopUp
        If INDSLeCostCenterConcept.Properties.DataSource Is Nothing AndAlso (_crossingAccount.Status = 0 OrElse _crossingAccount.Status = 1) Then
            Using model As New MNoteConcepts(Me.Tag)
                INDSLeCostCenterConcept.Properties.DataSource = model.ListCostCenterXpo()
            End Using
        End If
    End Sub

    Private Sub INDPceAddConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDPceAddConcept.QueryPopUp
        If INDsleThirdParty.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un tercero en los datos de Cruce"
            e.Cancel = True
            Exit Sub
        End If
        INDSleMainAccountConcept_QueryPopUp(Nothing, Nothing)
        SearchLookUpEdit1_QueryPopUp(Nothing, Nothing)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCashFlowConceptCxC_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCashFlowConceptCxC.QueryPopUp
        If INDSleCashFlowConceptCxC.Properties.DataSource Is Nothing Then
            GetCashFlowConcept("1")
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCashFlowConceptCxP_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCashFlowConceptCxP.QueryPopUp
        If INDSleCashFlowConceptCxP.Properties.DataSource Is Nothing Then
            GetCashFlowConcept("2")
        End If
    End Sub


    Private Sub INDSleCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCurrency.QueryPopUp
        If INDSleCurrency.Properties.DataSource Is Nothing Then
            INDSleCurrency.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetCurrency()
        End If
    End Sub


#End Region

#Region "Leave"
    ''' <summary>
    ''' Handles the LostFocus event of the INDdeDate control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDdeDate_Leave(sender As Object, e As EventArgs) Handles INDdeDate.Leave
        If String.IsNullOrEmpty(DocumentDate) Then
            Using Model As New MDocumentAccount(Me.Tag)
                If Not Await Model.ValidatePeriod(DocumentDate.Month, DocumentDate.Year) Then
                    Dim periods As List(Of ClosedMonth) = Model.GetOpenPeriod()
                    If periods IsNot Nothing AndAlso periods.Count > 0 Then
                        Dim perOpen As String = AccountingServices.GetListOpenPeriods(periods)
                        INDdeDate.EditValue = Nothing
                        Mensaje(EeventViewerImages.Advertencia) = perOpen
                        INDdeDate.Focus()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OpenPeriodNothing", "Accounting")
                        INDdeDate.EditValue = Nothing
                        INDdeDate.Focus()
                    End If
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the Leave event of the INDtxtCrossingPercentCxP control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtCrossingPercentCxP_Leave(sender As Object, e As EventArgs) Handles INDtxtCrossingPercentCxP.Leave
        If AccountPayableShareId <> 0 Then
            If CrossingValueCxP > InvoiceBalanceCxP Then
                CrossingValueCxP = 0
                CrossingPercentCxP = 0
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CrossingValueMoreAccountPayableValue", "Payments")
            Else
                If _cxpPercentChanging Then
                    CrossingValueCxP = TreasuryStaticServices.ConvertInvoicePercentToValue(CrossingPercentCxP, InvoiceBalanceCxP)
                End If
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPopupBills_BillNumberEmpty", "Payments")
        End If
    End Sub

    ''' <summary>
    ''' Handles the Leave event of the INDspCrossingPercentCxC control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDspCrossingPercentCxC_Leave(sender As Object, e As EventArgs) Handles INDspCrossingPercentCxC.Leave
        If AccountRecivableId <> 0 Then
            If CrossingValueCxC > InvoiceBalanceCxC Then
                CrossingValueCxC = 0
                CrossingPercentCxC = 0
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CrossingValueMoreAccountPayableValue", "Payments")
            Else
                If _cxcPercentChanging Then
                    CrossingValueCxC = TreasuryStaticServices.ConvertInvoicePercentToValue(CrossingPercentCxC, InvoiceBalanceCxC)
                End If
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPopupBills_BillNumberEmpty", "Payments")
        End If
    End Sub

    ''' <summary>
    ''' Handles the LostFocus event of the INDtxtPayValue control.
    ''' </summary>
    Private Sub INDtxtCrossingValueCxP_Leave(sender As Object, e As EventArgs) Handles INDtxtCrossingValueCxP.Leave
        If AccountPayableShareId <> 0 Then
            If CrossingValueCxP > InvoiceBalanceCxP Then
                CrossingValueCxP = 0
                CrossingPercentCxP = 0
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("PayValueMoreAccountPayableValue", MODULE_NAME)
            Else
                If _cxpValueChanging Then
                    CrossingPercentCxP = TreasuryStaticServices.ConvertInvoiceValueToPercent(CrossingValueCxP, InvoiceBalanceCxP)
                End If
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPopupBills_BillNumberEmpty", "Payments")
        End If
    End Sub

    ''' <summary>
    ''' Handles the Leave event of the INDtxtCrossingValueCxC control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtCrossingValueCxC_Leave(sender As Object, e As EventArgs) Handles INDtxtCrossingValueCxC.Leave
        If AccountRecivableId <> 0 Then
            If CrossingValueCxC > InvoiceBalanceCxC Then
                CrossingValueCxC = 0
                CrossingPercentCxC = 0
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CrossingValueMoreAccountPayableValue", "Payments")
            Else
                If _cxcValueChanging Then
                    CrossingPercentCxC = TreasuryStaticServices.ConvertInvoiceValueToPercent(CrossingValueCxC, InvoiceBalanceCxC)
                End If
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPopupBills_BillNumberEmpty", "Payments")
        End If
    End Sub

    ''' <summary>
    ''' Handles the Leave event of the RepositoryItemCrossingValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RepositoryItemCrossingValueCxP_Leave(sender As Object, e As EventArgs) Handles RepositoryItemCrossingValue.Leave
        Dim invoiceCxP As CrossingAccountDetailCxP = CType(INDgvAcountPayable.GetFocusedRow(), CrossingAccountDetailCxP)
        If invoiceCxP.CrossingValue > invoiceCxP.Balance Then
            invoiceCxP.CrossingValue = invoiceCxP.Balance
            INDgcAcountPayable.RefreshDataSource()
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("PayValueMoreAccountPayableValue", MODULE_NAME)
        End If
        If _crossingAccount.ChangeTracker.State <> ObjectState.Added Then
            _crossingAccount.MarkAsModified()
        End If
        _CxPValue = ListCrossingAccountDetailCxP.Sum(Function(x) x.CrossingValue)
        ctrTmp.PrintValueCrossing()
    End Sub

    ''' <summary>
    ''' Handles the Leave event of the RepositoryItemCrossingVal control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RepositoryItemCrossingValCxC_Leave(sender As Object, e As EventArgs) Handles RepositoryItemCrossingVal.Leave
        Dim invoiceCxC As CrossingAccountDetailCxC = CType(INDgvAccountRecivable.GetFocusedRow(), CrossingAccountDetailCxC)
        If invoiceCxC.CrossingValue > invoiceCxC.Balance Then
            invoiceCxC.CrossingValue = invoiceCxC.Balance
            INDgcAccountRecivable.RefreshDataSource()
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CrossingValueMoreAccountPayableValue", "Payments")
        End If
        If _crossingAccount.ChangeTracker.State <> ObjectState.Added Then
            _crossingAccount.MarkAsModified()
        End If
        _CxCValue = ListCrossingAccountDetailCxC.Sum(Function(x) x.CrossingValue) 'invoiceCxC.CrossingValue
        ctrTmp.PrintValueCrossing()
    End Sub

#End Region

#Region "Click"
    ''' <summary>
    ''' Handles the Click event of the INDsbAddInvoiceShare control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddInvoiceShare_Click(sender As Object, e As EventArgs) Handles INDsbAddInvoiceShare.Click
        Dim resultValidate As Tuple(Of Boolean, String) = validateControlsInvoiceShareCxP()
        If resultValidate.Item1 Then
            AddAccountPayableCxPToList()
            CleanControlsInvoiceCxP()
            INDsleInvoiceCxP.Focus()
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("EmptyFields", MODULE_NAME), resultValidate.Item2)
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAddCrossingCxC control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddCrossingCxC_Click(sender As Object, e As EventArgs) Handles INDsbAddCrossingCxC.Click
        Dim resultValidate As Tuple(Of Boolean, String) = validateControlsInvoiceShareCxC()
        If resultValidate.Item1 Then
            AddAccountReceivableCxCToList()
            CleanControlsInvoiceCxC()
            INDsleInvoiceCxC.Focus()
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("EmptyFields", MODULE_NAME), resultValidate.Item2)
        End If
    End Sub
#End Region

#Region "CloseUp"
    ''' <summary>
    ''' Handles the CloseUp event of the INDpceAddInvoiceCxP control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.CloseUpEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddInvoiceCxP_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceAddInvoiceCxP.CloseUp
        CleanControlsInvoiceCxP()
        INDpceAddInvoiceCxC.Focus()
    End Sub

    ''' <summary>
    ''' Handles the CloseUp event of the INDpceAddInvoiceCxC control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.CloseUpEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddInvoiceCxC_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceAddInvoiceCxC.CloseUp
        CleanControlsInvoiceCxC()
    End Sub
#End Region

#Region "DatasourceChanged"
    ''' <summary>
    ''' Handles the DataSourceChanged event of the INDgcAcountPayable control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgcAcountPayable_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcAcountPayable.DataSourceChanged, INDgcAccountRecivable.DataSourceChanged
        INDsleThirdParty.Properties.ReadOnly = (ListCrossingAccountDetailCxP IsNot Nothing AndAlso ListCrossingAccountDetailCxP.Count > 0) OrElse (ListCrossingAccountDetailCxC IsNot Nothing AndAlso ListCrossingAccountDetailCxC.Count > 0)
    End Sub
#End Region

#Region "Actions"
    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteAccountPayableShare()
    End Sub

    ''' <summary>
    ''' Handles the ContexMenuActions event of the IndigoGridView1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteAccountPayableShare()
    End Sub

    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction, IndigoGridView3.ContexMenuActions
        DeleteAccountConcept()
    End Sub

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView2_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        DeleteAccountReceivable()
    End Sub

    ''' <summary>
    ''' Handles the ContexMenuActions event of the IndigoGridView2 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        DeleteAccountReceivable()
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleThirdParty control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmThirdParty
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeThirdParty()
            End Using
        End If
    End Sub

    Private Sub INDSleNoteConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleNoteConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("677", Nothing, True)
        End If
    End Sub

    Private Sub INDSleMainAccountConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleMainAccountConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("602", Nothing, True)
        End If
    End Sub

    Private Sub SearchLookUpEdit1_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleThirdPartyConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("532", Nothing, True)
        End If
    End Sub

    Private Sub INDSLeCostCenterConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSLeCostCenterConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("517", Nothing, True)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCashFlowConceptCxC_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCashFlowConceptCxC.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCashFlowConcept
                Formulario.ViewModeEditHold = True
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                GetCashFlowConcept("1")
            End Using
        End If
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCashFlowConceptCxP_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCashFlowConceptCxP.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCashFlowConcept
                Formulario.ViewModeEditHold = True
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                GetCashFlowConcept("2")
            End Using
        End If
    End Sub

    Private Sub INDsleInvoiceCxP_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleInvoiceCxP.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmAccountsPayable
                Formulario.ViewModeEditHold = True
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub


    Private Sub INDsleInvoiceCxC_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleInvoiceCxC.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmAccountReceivableDocument
                Formulario.ViewModeEditHold = True
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub

#End Region

#Region "Custom"
    Public Event LoadControlsFinish()
#End Region

#End Region

#Region "Enums"
    Public Enum eCrossingType
        SameThird = 1
        OtherThird = 2
    End Enum
#End Region

#Region "Bar Button Events"
    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        varImp = 2
        Guardar(False)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click confirmar.
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Confirmar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        varImp = 1
        Guardar(False)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular.
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        Anular()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ guardar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        Guardar(True)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ actualizar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        Guardar(True)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _crossingAccount.Id, 0, _crossingAccount.Id, reportType)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.TreasurySequenceDetail IsNot Nothing Then
                If Not Me._sequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

    Private Sub INDSleNoteConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleNoteConcept.EditValueChanged
        If INDSleNoteConcept.EditValue IsNot Nothing Then
            Dim _noteConcept = CType(INDGvSleNoteConcept.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
            If _noteConcept IsNot Nothing AndAlso _noteConcept.IdMainAccount IsNot Nothing Then
                INDSleMainAccountConcept.EditValue = _noteConcept.IdMainAccount.Id
                If _noteConcept.IdMainAccount.HandlesThirdParty Then
                    INDlyItemThirdParty.HideControl(False)
                    If CrossingType = eCrossingType.SameThird Then
                        INDSleThirdPartyConcept.EditValue = INDsleThirdParty.EditValue

                    End If
                Else
                    INDlyItemThirdParty.HideControl()
                    INDsleThirdParty.EditValue = Nothing
                End If
                If _noteConcept.IdMainAccount.HandlesCostCenter Then
                    INDLciCostCenterConcept.HideControl(False)
                Else
                    INDLciCostCenterConcept.HideControl()
                    INDSLeCostCenterConcept.EditValue = Nothing
                End If
                INDGLeNatureConcept.EditValue = CByte(_noteConcept.NatureValue)
                If _noteConcept.IdCashFlowConcept IsNot Nothing Then
                    CashFlowConceptCodeNameOther = String.Format("{0} - {1}", _noteConcept.IdCashFlowConcept.Code, _noteConcept.IdCashFlowConcept.NameConcept)
                    CashFlowConceptIdOther = _noteConcept.IdCashFlowConcept.Id
                Else
                    CashFlowConceptCodeNameOther = String.Empty
                    CashFlowConceptIdOther = 0
                End If
            End If
        Else
            INDlyItemThirdParty.HideControl()
            INDLciCostCenterConcept.HideControl()
            INDSleThirdPartyConcept.EditValue = Nothing
            INDSLeCostCenterConcept.EditValue = Nothing
        End If
    End Sub

    Private Sub INDBtnAddConcept_Click(sender As Object, e As EventArgs) Handles INDBtnAddConcept.Click
        Dim errors = ValidateControlsPopupOtherConcept()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        Dim _crossingAccountOtherConcept As New CrossingAccountDetailOtherConcept()
        With _crossingAccountOtherConcept
            .TreasuryNoteConceptId = INDSleNoteConcept.EditValue
            .MainAccountId = INDSleMainAccountConcept.EditValue
            .ThirdPartyId = INDSleThirdPartyConcept.EditValue
            .CostCenterId = INDSLeCostCenterConcept.EditValue
            .Value = INDTxtValueOtherConcept.EditValue
            .Nature = INDGLeNatureConcept.EditValue
            .MainAccountCodeName = INDSleMainAccountConcept.Text
            .ConceptCodeName = INDSleNoteConcept.Text
            .ThirdPartyNitName = INDSleThirdPartyConcept.Text
            .CostCenterCodeName = INDSLeCostCenterConcept.Text
            .NatureName = INDGLeNatureConcept.Text
            .Detail = "Nota de Tesoreria : {0}, Concepto : " & INDSleNoteConcept.Text.Trim().Split("-")(0).Trim()
            If CashFlowConceptIdOther > 0 Then
                .IdCashFlowConcept = CashFlowConceptIdOther
                .CodeNameCashFlowConcept = CashFlowConceptCodeNameOther
            Else
                .IdCashFlowConcept = Nothing
                .CodeNameCashFlowConcept = String.Empty
            End If
        End With
        _crossingAccount.CrossingAccountDetailOtherConcept.Add(_crossingAccountOtherConcept)
        If _crossingAccount.ChangeTracker.State <> ObjectState.Added Then
            _crossingAccount.MarkAsModified()
        End If

        _CxPValue = ListCrossingAccountDetailCxP.Sum(Function(x) x.CrossingValue) + _crossingAccount.CrossingAccountDetailOtherConcept.Where(Function(x) x.Nature = 1).Sum(Function(o) o.Value)
        _CxCValue = ListCrossingAccountDetailCxC.Sum(Function(x) x.CrossingValue) + _crossingAccount.CrossingAccountDetailOtherConcept.Where(Function(x) x.Nature = 2).Sum(Function(o) o.Value)

        ctrTmp.PrintValueCrossing()
        ListCrossingAccountOtherConcepts = _crossingAccount.CrossingAccountDetailOtherConcept.ToList()
        INDGcCrossingAccountOtherConcepts.RefreshDataSource()

        CleanControlsPopupOtherConcept()
        INDSleNoteConcept.Focus()
    End Sub

    Private Sub CleanControlsPopupOtherConcept()
        INDSleNoteConcept.EditValue = Nothing
        INDSleMainAccountConcept.EditValue = Nothing
        INDSleMainAccountConcept.Properties.NullText = String.Empty
        INDSleThirdPartyConcept.EditValue = Nothing
        INDSleThirdPartyConcept.Properties.NullText = String.Empty
        INDSLeCostCenterConcept.EditValue = Nothing
        INDlyItemThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciCostCenterConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDGLeNatureConcept.EditValue = Nothing
        INDTxtValueOtherConcept.EditValue = 0
    End Sub

    ''' <summary>
    ''' metodo para validar el popup de otros conceptos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopupOtherConcept() As String
        Dim errors As New StringBuilder
        If INDSleNoteConcept.EditValue = Nothing Then
            errors.AppendLine("Concepto Vacio")
        End If
        If INDSleMainAccountConcept.EditValue = Nothing Then
            errors.AppendLine("Cuenta Contable Vacia")
        End If
        If INDlyItemThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDSleThirdPartyConcept.EditValue = Nothing Then
                errors.AppendLine("Tercero Vacio")
            End If
        End If
        If INDLciCostCenterConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDSLeCostCenterConcept.EditValue = Nothing Then
                errors.AppendLine("Centro de Costo Vacio")
            End If
        End If
        If INDGLeNatureConcept.EditValue = Nothing Then
            errors.AppendLine("Naturaleza Vacia")
        End If
        If INDTxtValueOtherConcept.EditValue Is Nothing OrElse INDTxtValueOtherConcept.EditValue = 0 Then
            errors.AppendLine("El valor debe ser superior a 0")
        End If
        Return errors.ToString()
    End Function

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If _crossingAccount.Status = 2 Then
            Mensaje(EeventViewerImages.Advertencia) = "El documento esta confirmado"
            Exit Sub
        End If
        If INDsleThirdParty.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un tercero"
            Exit Sub
        End If
        AsyncLoader(True)
        Using model As New MVoucherTransactionCrossing(MyTag)
            Dim result As ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)) = Nothing
            If sender.Name = INDgcAcountPayable.Name Then 'cuenta por pagar 
                Me.Cursor = ChangeCursorIndigo()
                result = Await model.SetDocumentsCrossingCopyPaste(e.Rows, INDsleThirdParty.EditValue, INDgleCrossingType.EditValue, 1)
                'si ocurrio un error
                If result.StatusCode = eStatusResult.EXCEPTION Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    AsyncLoader(False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    Exit Sub
                End If


                For Each item In result.ObjectEmbbeded
                    Dim accountPayable = ListCrossingAccountDetailCxP.Where(Function(x) x.AccountPayableId = item.AccountPayableId).FirstOrDefault()
                    If accountPayable Is Nothing Then
                        ListCrossingAccountDetailCxP.Add(item)
                    Else
                        result.MessageResult.Add("La cuenta por pagar " & accountPayable.BillNumber & " ya esta agregada")
                    End If
                Next
                validateAddDetails()
                INDgcAcountPayable.RefreshDataSource()

            ElseIf sender.Name = INDgcAccountRecivable.Name Then 'cuenta por cobrar
                result = Await model.SetDocumentsCrossingCopyPaste(e.Rows, INDsleThirdParty.EditValue, INDgleCrossingType.EditValue, 2)
                'si ocurrio un error
                If result.StatusCode = eStatusResult.EXCEPTION Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    AsyncLoader(False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    Exit Sub
                End If


                For Each item In result.ObjectEmbbededAux
                    Dim accountReceivable = ListCrossingAccountDetailCxC.Where(Function(x) x.AccountReceivableAccountingId = item.AccountReceivableAccountingId).FirstOrDefault()
                    If accountReceivable Is Nothing Then
                        ListCrossingAccountDetailCxC.Add(item)
                    Else
                        result.MessageResult.Add("La factura " & accountReceivable.BillNumber & " con la cuenta contable" & accountReceivable.MainAccountDescription & " ya esta agregada")
                    End If
                Next
                validateAddDetails()
                INDgcAccountRecivable.RefreshDataSource()
            End If

            If result.MessageResult.Count > 0 Then
                Using formulario As New FrmListErrors(result.MessageResult)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
            Me.Cursor = System.Windows.Forms.Cursors.Default
            _CxPValue = ListCrossingAccountDetailCxP.Sum(Function(x) x.CrossingValue) + _crossingAccount.CrossingAccountDetailOtherConcept.Where(Function(x) x.Nature = 1).Sum(Function(o) o.Value)
            _CxCValue = ListCrossingAccountDetailCxC.Sum(Function(x) x.CrossingValue) + _crossingAccount.CrossingAccountDetailOtherConcept.Where(Function(x) x.Nature = 2).Sum(Function(o) o.Value)

            ctrTmp.PrintValueCrossing()

        End Using
        AsyncLoader(False)
    End Sub

    Private Sub SetRow(indexSend As Integer, indexEnd As Integer, proccesType As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  If proccesType = 1 Then
                                                      listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(3)})
                                                  Else
                                                      listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(4)})
                                                  End If
                                              End SyncLock
                                          End Sub)
    End Sub

    Private Async Sub INDBtnImportFileCxP_Click(sender As Object, e As EventArgs) Handles INDBtnImportFileCxP.Click, INDBtnImportFileCxC.Click
        If _crossingAccount.Status = 2 Then
            Mensaje(EeventViewerImages.Advertencia) = "El documento esta confirmado"
            Exit Sub
        End If
        If INDsleThirdParty.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un tercero"
            Exit Sub
        End If
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                'obtengo la rura del archivo
                myStream = openFileDialog1.FileName
                If (myStream IsNot Nothing AndAlso Not myStream.Trim().Equals(String.Empty)) Then
                    Dim sddf = New SpreadsheetControl()
                    sddf.AllowDrop = False
                    sddf.LoadDocument(myStream)
                    Dim workBook As IWorkbook = sddf.Document

                    rows = workBook.Worksheets(0).Rows
                    If rows.LastUsedIndex = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                        AsyncLoader(False)
                        Exit Sub
                    End If


                    listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()


                    Using model As New MVoucherTransactionCrossing(MyTag)
                        Dim result As ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)) = Nothing
                        If sender.Name = INDBtnImportFileCxP.Name Then
                            SetRow(1, rows.LastUsedIndex + 1, 1)

                            result = Await model.SetDocumentsCrossingImportFile(listRows.ToList(), INDsleThirdParty.EditValue, INDgleCrossingType.EditValue, 1)

                            'si ocurrio un error
                            If result.StatusCode = eStatusResult.EXCEPTION Then
                                Mensaje(EeventViewerImages.Advertencia) = result.Message
                                AsyncLoader(False)
                                Me.Cursor = System.Windows.Forms.Cursors.Default
                                Exit Sub
                            End If


                            For Each item In result.ObjectEmbbeded
                                Dim accountPayable = ListCrossingAccountDetailCxP.Where(Function(x) x.AccountPayableId = item.AccountPayableId).FirstOrDefault()
                                If accountPayable Is Nothing Then
                                    ListCrossingAccountDetailCxP.Add(item)
                                Else
                                    result.MessageResult.Add("La cuenta por pagar " & accountPayable.BillNumber & " ya esta agregada")
                                End If
                            Next

                            INDgcAcountPayable.RefreshDataSource()
                            validateAddDetails()
                        Else
                            SetRow(1, rows.LastUsedIndex + 1, 2)
                            'result = Await model.SetDocumentsCrossingImportFile(listRows.ToList(), INDsleThirdParty.EditValue, INDgleCrossingType.EditValue, 1)

                            result = Await model.SetDocumentsCrossingImportFile(listRows.ToList(), INDsleThirdParty.EditValue, INDgleCrossingType.EditValue, 2)
                            'si ocurrio un error
                            If result.StatusCode = eStatusResult.EXCEPTION Then
                                Mensaje(EeventViewerImages.Advertencia) = result.Message
                                AsyncLoader(False)
                                Me.Cursor = System.Windows.Forms.Cursors.Default
                                Exit Sub
                            End If


                            For Each item In result.ObjectEmbbededAux
                                Dim accountReceivable = ListCrossingAccountDetailCxC.Where(Function(x) x.AccountReceivableAccountingId = item.AccountReceivableAccountingId).FirstOrDefault()
                                If accountReceivable Is Nothing Then
                                    ListCrossingAccountDetailCxC.Add(item)
                                Else
                                    result.MessageResult.Add("La factura " & accountReceivable.BillNumber & " con la cuenta contable" & accountReceivable.MainAccountDescription & " ya esta agregada")
                                End If
                            Next
                            validateAddDetails()
                            INDgcAccountRecivable.RefreshDataSource()

                        End If

                        If result.MessageResult.Count > 0 Then
                            Using formulario As New FrmListErrors(result.MessageResult)
                                formulario.StartPosition = FormStartPosition.CenterParent
                                Dim transparent As New FrmTransparent(formulario, False)
                                Me.Cursor = System.Windows.Forms.Cursors.Default
                                transparent.ShowDialog(Me)
                            End Using
                        End If
                    End Using

                    AsyncLoader(False)
                    _CxPValue = ListCrossingAccountDetailCxP.Sum(Function(x) x.CrossingValue) + _crossingAccount.CrossingAccountDetailOtherConcept.Where(Function(x) x.Nature = 1).Sum(Function(o) o.Value)
                    _CxCValue = ListCrossingAccountDetailCxC.Sum(Function(x) x.CrossingValue) + _crossingAccount.CrossingAccountDetailOtherConcept.Where(Function(x) x.Nature = 2).Sum(Function(o) o.Value)

                    ctrTmp.PrintValueCrossing()
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
                AsyncLoader(False)
            End Try
        Else
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' carga por defecto la moneda oficial en los controles
    ''' </summary>
    Private Async Sub LoadDefaultCurrency()
        Me.CurrencyId(indigo.CurrencyISO4217) = indigo.OfficialCurrencyId
        Await Me.CurrencyExchangeActions(False)
        Await Me.CurrencyExchangeActionsCXC(False)
    End Sub

    ''' <summary>
    ''' Funcion para ocultar/mostrar el control de tasa de cambio, establece el valor del trm y el texto del control
    ''' </summary>
    ''' <param name="value">True- Activa validaciones y acciones del control del TRM.; False - Oculta el control</param>
    ''' <param name="_currencyId"></param>
    ''' <param name="_currencyAbbreviation"></param>
    ''' <param name="ToCurrencyId"></param>
    ''' <returns></returns>
    Private Async Function CurrencyExchangeActions(value As Boolean, Optional _currencyId As Integer? = Nothing, Optional _currencyAbbreviation As String = Nothing, Optional ToCurrencyId As Integer? = Nothing) As Task(Of Boolean)
        'Si el valor es esta en False se oculta o si no pasa alguna de las otras condiciones
        If Not value OrElse CurrencyId Is Nothing OrElse String.IsNullOrEmpty(_currencyAbbreviation) OrElse ToCurrencyId Is Nothing OrElse (_currencyId = ToCurrencyId) Then
            Me.ShowOrHideExchangeControl(False)
            Return (Not value OrElse _currencyId = ToCurrencyId)
        End If

        'si pasa la validacion se muestra el control
        Me.ShowOrHideExchangeControl(True)

        'si ya existe el TRM se consulta el guardado para no perder tiempo en ir a consultarlo de nuevo debido a que el trm es diario
        If _listTRM IsNot Nothing AndAlso _listTRM.Any(Function(x) x.CurrencyId = _currencyId AndAlso x.OfficialCurrencyId = ToCurrencyId AndAlso x.MeasurementDate.Date = GetDateServer().Date) Then
            INDTxtExchange.Text = $"{_currencyAbbreviation} - TRM:{String.Format("{0:n2}", Utils.VisibleTRM(_listTRM.FirstOrDefault(Function(x) x.CurrencyId = _currencyId AndAlso
                                                                                                          x.OfficialCurrencyId = ToCurrencyId AndAlso
                                                                                                          x.MeasurementDate.Date = GetDateServer().Date).Value))}"
            Return True
        End If

        'si no se aha consultado previamente, se manda a consultar
        Using Model As New MPortfolioTransfers("")
            Dim Result = Await Model.GetTRMbyCurrencyId(_currencyId, ToCurrencyId)

            If Result Is Nothing OrElse Not Result?.StateResult Then
                Me.ShowOrHideExchangeControl(False)
                Me.Mensaje(EeventViewerImages.Advertencia) = Result?.Message
                Return False
            End If

            _listTRM = If(_listTRM Is Nothing, New List(Of TRM), _listTRM)
            _listTRM.Add(Result.ObjectEmbbeded)
            Me.Mensaje(EeventViewerImages.Informacion) = Result?.Message
            INDTxtExchange.Text = $"{_currencyAbbreviation} - TRM: {String.Format("{0:n2}", Utils.VisibleTRM(Result.ObjectEmbbeded.Value))}"
            Return True
        End Using
    End Function

    ''' <summary>
    ''' oculta o muestra el control de tasa de cambio
    ''' </summary>
    ''' <param name="Value"></param>
    Private Sub ShowOrHideExchangeControl(Value As Boolean)
        INDLciExchange.Visibility = If(Not Value, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        INDTxtExchange.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Funcion para ocultar/mostrar el control de tasa de cambio, establece el valor del trm y el texto del control
    ''' </summary>
    ''' <param name="value">True- Activa validaciones y acciones del control del TRM.; False - Oculta el control</param>
    ''' <param name="_currencyId"></param>
    ''' <param name="_currencyAbbreviation"></param>
    ''' <param name="ToCurrencyId"></param>
    ''' <returns></returns>
    Private Async Function CurrencyExchangeActionsCXC(value As Boolean, Optional _currencyId As Integer? = Nothing, Optional _currencyAbbreviation As String = Nothing, Optional ToCurrencyId As Integer? = Nothing) As Task(Of Boolean)
        'Si el valor es esta en False se oculta o si no pasa alguna de las otras condiciones
        If Not value OrElse CurrencyId Is Nothing OrElse String.IsNullOrEmpty(_currencyAbbreviation) OrElse ToCurrencyId Is Nothing OrElse (_currencyId = ToCurrencyId) Then
            Me.ShowOrHideExchangeControlCXC(False)
            Return (Not value OrElse _currencyId = ToCurrencyId)
        End If

        'si pasa la validacion se muestra el control
        Me.ShowOrHideExchangeControlCXC(True)

        'si ya existe el TRM se consulta el guardado para no perder tiempo en ir a consultarlo de nuevo debido a que el trm es diario
        If _listTRM IsNot Nothing AndAlso _listTRM.Any(Function(x) x.CurrencyId = _currencyId AndAlso x.OfficialCurrencyId = ToCurrencyId AndAlso x.MeasurementDate.Date = GetDateServer().Date) Then
            INDTxtExchangeCXC.Text = $"{_currencyAbbreviation} - TRM:{String.Format("{0:n2}", Utils.VisibleTRM(_listTRM.FirstOrDefault(Function(x) x.CurrencyId = _currencyId AndAlso
                                                                                                          x.OfficialCurrencyId = ToCurrencyId AndAlso
                                                                                                          x.MeasurementDate.Date = GetDateServer().Date).Value))}"
            Return True
        End If

        'si no se aha consultado previamente, se manda a consultar
        Using Model As New MPortfolioTransfers("")
            Dim Result = Await Model.GetTRMbyCurrencyId(_currencyId, ToCurrencyId)

            If Result Is Nothing OrElse Not Result?.StateResult Then
                Me.ShowOrHideExchangeControl(False)
                Me.Mensaje(EeventViewerImages.Advertencia) = Result?.Message
                Return False
            End If

            _listTRM = If(_listTRM Is Nothing, New List(Of TRM), _listTRM)
            _listTRM.Add(Result.ObjectEmbbeded)
            Me.Mensaje(EeventViewerImages.Informacion) = Result?.Message
            INDTxtExchangeCXC.Text = $"{_currencyAbbreviation} - TRM: {String.Format("{0:n2}", Utils.VisibleTRM(Result.ObjectEmbbeded.Value))}"
            Return True
        End Using
    End Function

    ''' <summary>
    ''' oculta o muestra el control de tasa de cambio
    ''' </summary>
    ''' <param name="Value"></param>
    Private Sub ShowOrHideExchangeControlCXC(Value As Boolean)
        INDLciExchangeCXC.Visibility = If(Not Value, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        INDTxtExchangeCXC.Text = String.Empty
    End Sub

    ''' <summary>
    ''' setea el formato moneda de los campos y columnas del formulario
    ''' </summary>
    ''' <param name="Abbreviation"></param>
    Private Sub _setFormatGeneralControls(Abbreviation As String)
        If String.IsNullOrEmpty(Abbreviation) Then
            Exit Sub
        End If
        Me._currencyAbbreviation = Abbreviation
        Me.INDSleCurrency.Properties.NullText = $"{Abbreviation}"
        Me.BandedGridColumn9 = Window.Utils.FormatGrid(BandedGridColumn9, Abbreviation)
        Me.GridColumn4 = Window.Utils.FormatGrid(GridColumn4, Abbreviation)
        ''---------------------------------------------------------------------------'
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = New Globalization.CultureInfo(Abbreviation.GetCultureId()).NumberFormat
        INDtxtCrossingValueCxC.Properties.Mask.Culture = _culture
        INDtxtCrossingValueCxP.Properties.Mask.Culture = _culture
        INDTxtValueOtherConcept.Properties.Mask.Culture = _culture
    End Sub

    ''' <summary>
    ''' valida que exista al menos un detalle en cxp o cxc para bloquear el campo de moneda
    ''' </summary>
    Private Sub validateAddDetails()
        INDSleCurrency.Enabled = True
        ctrTmp.CodeISO4217 = INDSleCurrency.Text
        If ListCrossingAccountDetailCxP.Any() Then
            INDSleCurrency.Enabled = False
        End If
        If ListCrossingAccountDetailCxC.Any() Then
            INDSleCurrency.Enabled = False
        End If
    End Sub


    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rows As RowCollection
    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()
    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim myStream As String = Nothing

End Class