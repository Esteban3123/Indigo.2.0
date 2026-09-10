'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/07/2014
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
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Presentation.Resources
Imports Presentation.Common
Imports Presentation.Payroll
Imports Presentation.Accounting
Imports Presentation.Accounting.MVP
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils.Menu
Imports System.Windows.Forms
Imports Domain.Entities.Service
Imports Presentation.Controls.MVP
Imports Presentation.Maintenance.MVP
Imports Presentation.Common.MVP
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.CommonRepository
Imports DevExpress.Spreadsheet

#End Region

Public Class FrmPopupDeferredCausation
    Implements IPopupDeferredCausation

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la fecha de la factura
    ''' </summary>
    ''' <remarks></remarks>
    Public Property DateBill As DateTime Implements IPopupDeferredCausation.DateBill
        Get
            Return INDdteInitialDate.EditValue
        End Get
        Set(value As DateTime)
            INDdteInitialDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de eliminados de los detalles de la causacion diferida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property listDeleteDeferredCausationDetail As List(Of DeferredCausationDetails)
        Get
            Return _listDeleteDeferredCausationDetail
        End Get
        Set(value As List(Of DeferredCausationDetails))
            _listDeleteDeferredCausationDetail = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable de la cabecera
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdMainAccountCabecera As Integer
        Get
            Return _idMainAccountCabecera
        End Get
        Set(value As Integer)
            _idMainAccountCabecera = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo de la cabecera
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCostCenterCabecera As Integer
        Get
            Return _idCostCenterCabecera
        End Get
        Set(value As Integer)
            _idCostCenterCabecera = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la forma de cerrar el form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BanClose As Boolean
        Get
            Return _banClose
        End Get
        Set(value As Boolean)
            _banClose = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la entidad de tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyCommon As Domain.Entities.ThirdParty Implements IPopupDeferredCausation.ThirdPartyCommon
        Get
            Return _thirdPartyCommon
        End Get
        Set(value As Domain.Entities.ThirdParty)
            _thirdPartyCommon = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la entidad de tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdParty As Domain.Entities.ThirdParty Implements IPopupDeferredCausation.ThirdParty
        Get
            Return _thirdParty
        End Get
        Set(value As Domain.Entities.ThirdParty)
            _thirdParty = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdSupplier As Integer Implements IPopupDeferredCausation.IdSupplier
        Get
            Return _idSupplier
        End Get
        Set(value As Integer)
            _idSupplier = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado del repositorio de centros de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterRepositoryXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IPopupDeferredCausation.CostCenterRepositoryXpo
        Get
            Return RepositoryItemSearchLookUpEditCostCenter.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            RepositoryItemSearchLookUpEditCostCenter.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado del repositorio de cuentas contables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainAccountRepositoryXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IPopupDeferredCausation.MainAccountRepositoryXpo
        Get
            Return RepositoryItemSearchLookUpEditMainAccount.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            RepositoryItemSearchLookUpEditMainAccount.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de cuentas contables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IPopupDeferredCausation.MainAccountXpo
        Get
            Return INDsleMainAccounts.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleMainAccounts.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de centros de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IPopupDeferredCausation.CostCenterXpo
        Get
            Return INDsleCostCenter.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el numero de factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillNumber As String Implements IPopupDeferredCausation.BillNumber
        Get
            Return INDtxtBillNumber.Text
        End Get
        Set(value As String)
            INDtxtBillNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainAccount As String Implements IPopupDeferredCausation.MainAccount
        Get
            Return INDtxtMainAccounts.Text
        End Get
        Set(value As String)
            INDtxtMainAccounts.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenters As String Implements IPopupDeferredCausation.CostCenters
        Get
            Return INDtxtCostCenter.Text
        End Get
        Set(value As String)
            INDtxtCostCenter.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de los debitos de los conceptos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value As Decimal Implements IPopupDeferredCausation.Value
        Get
            Return INDtxtValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValuePopup As Decimal Implements IPopupDeferredCausation.ValuePopup
        Get
            Return INDtxtValuePopup.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValuePopup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdThirdParty As Integer Implements IPopupDeferredCausation.IdThirdParty
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdMainAccount As Integer Implements IPopupDeferredCausation.IdMainAccount
        Get
            Return INDsleMainAccounts.EditValue
        End Get
        Set(value As Integer)
            INDsleMainAccounts.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCostCenter As Integer Implements IPopupDeferredCausation.IdCostCenter
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el numero de periodos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PeriodNumbers As Integer Implements IPopupDeferredCausation.PeriodNumbers
        Get
            Return INDsePeriodNumbers.EditValue
        End Get
        Set(value As Integer)
            INDsePeriodNumbers.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la entidad de causacion diferida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property deferredCausation As DeferredCausation Implements IPopupDeferredCausation.deferredCausation
        Get
            Return _deferredCausation
        End Get
        Set(value As DeferredCausation)
            _deferredCausation = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de terceros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IPopupDeferredCausation.ThirdPartyXpo
        Get
            Return INDsleThirdParty.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property


#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa a la entidad compleja de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _deferredCausation As DeferredCausation

    ''' <summary>
    ''' Contiene el listado de detalles de causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeferredCausationDetail As List(Of DeferredCausationDetails)

    ''' <summary>
    ''' Representa al presentador de causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PPopupDeferredCausation

    ''' <summary>
    ''' Contiene el id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _idSupplier As Integer

    ''' <summary>
    ''' Obtiene o establece la entidad de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _thirdParty As Domain.Entities.ThirdParty

    ''' <summary>
    ''' Obtiene o establece la entidad de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _thirdPartyCommon As Domain.Entities.ThirdParty

    ''' <summary>
    ''' Contiene el nombre del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Dim _CodeNameThirdParty As String

    ''' <summary>
    ''' Contiene el id del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idThirParty As Integer

    ''' <summary>
    ''' Contiene el valor de la cuota
    ''' </summary>
    ''' <remarks></remarks>
    Dim valShare As Decimal

    ''' <summary>
    ''' Contiene el tipo de redondeo
    ''' </summary>
    ''' <remarks></remarks>
    Dim _roundingType As Decimal? = Nothing

    ''' <summary>
    ''' Variable para saber de que forma se cierra el form
    ''' </summary>
    ''' <remarks></remarks>
    Dim _banClose As Boolean

    ''' <summary>
    ''' Variable para el id de la cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idMainAccountCabecera As Integer

    ''' <summary>
    ''' Variable para el id del centro de costo
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idCostCenterCabecera As Integer

    ''' <summary>
    ''' Listado de eliminados de detalles de la causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Private _listDeleteDeferredCausationDetail As List(Of DeferredCausationDetails)

    ''' <summary>
    ''' Listado de eliminados que no tienen id
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteDontId As List(Of DeferredCausationDetails)


    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim myStream As String = Nothing

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
#End Region

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payments"

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para agregar la causacion diferida al form de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDeferredCausationToAccountPayable()
        If ValidateControls() = True Then
            Dim ban As Boolean = False
            If INDrgTypeDistribution.EditValue = 2 Then
                Dim val As Decimal = PaymentServices.SumValues(ListDeferredCausationDetail, 0, False)
                If val <> valShare Then
                    ban = True
                End If
            End If
            If ban = False Then
                AssigningValues()
                BanClose = True
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Close()
            Else
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontEqualsValue", NAME_MODULE)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty", "Commons")
        End If
    End Sub

    ''' <summary>
    ''' Metodo para asignar los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With deferredCausation
            .BillNumber = BillNumber
            .IdMainAccount = IdMainAccountCabecera
            .PeriodsNumber = PeriodNumbers
            .TypeDistribution = INDrgTypeDistribution.EditValue
            .InitialDate = DateBill
            .EndDate = DateAdd(DateInterval.Month, .PeriodsNumber - 1, DateBill)
            .IdThirdParty = IdThirdParty
            If IdCostCenterCabecera <> Nothing AndAlso IdCostCenterCabecera > 0 Then
                .IdCostCenter = IdCostCenterCabecera
            End If
            .Status = 0
            .ValueCreditPeriod = Value

            If ListDeleteDontId IsNot Nothing AndAlso ListDeleteDontId.Count > 0 AndAlso .DeferredCausationDetails.Count > 0 Then
                For Each item As DeferredCausationDetails In ListDeleteDontId
                    If .DeferredCausationDetails.Contains(item) Then
                        .DeferredCausationDetails.Remove(item)
                    End If
                Next
            End If

            If ListDeferredCausationDetail IsNot Nothing AndAlso ListDeferredCausationDetail.Count > 0 Then
                For Each item As DeferredCausationDetails In ListDeferredCausationDetail
                    .DeferredCausationDetails.Add(item)
                Next
            End If

            AddDeferredCausationShare()
        End With
    End Sub

    ''' <summary>
    ''' Agrega las cuotas de la causacion (DeferredCausationShare)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDeferredCausationShare()
        Select Case deferredCausation.ChangeTracker.State
            Case ObjectState.Added, ObjectState.Modified
                If deferredCausation.ChangeTracker.State = ObjectState.Modified Then
                    While deferredCausation.DeferredCausationShare.Count > 0
                        deferredCausation.DeferredCausationShare.Item(0).MarkAsDeleted()
                    End While
                Else
                    deferredCausation.DeferredCausationShare.Clear()
                End If
                Dim month As Integer = DateBill.Month
                Dim year As Integer = DateBill.Year
                getRounding()
                Dim listValueShares As List(Of Decimal) = New List(Of Decimal)
                If _roundingType Is Nothing Then
                    listValueShares = CommonService.GetValueShare(deferredCausation.ValueCreditPeriod, deferredCausation.PeriodsNumber)
                Else
                    listValueShares = addItemToList(deferredCausation.ValueCreditPeriod, deferredCausation.PeriodsNumber)
                End If
                For i As Integer = 0 To deferredCausation.PeriodsNumber - 1
                    Dim dcs As New DeferredCausationShare
                    With dcs
                        If i > 0 Then
                            If month = 12 Then
                                month = 1
                                year += 1
                            Else
                                month += 1
                            End If
                        End If
                        .PaymentYear = year
                        .PaymentMonth = month
                        .Value = listValueShares.Item(i)
                        .Amortized = False

                        .CreationUser = indigo.UserIndigo
                        .CreationDate = DateTime.Now
                    End With
                    deferredCausation.DeferredCausationShare.Add(dcs)
                Next
            Case ObjectState.Deleted
                While deferredCausation.DeferredCausationShare.Count > 0
                    deferredCausation.DeferredCausationShare.Item(0).MarkAsDeleted()
                End While
        End Select
    End Sub

    Public Sub getRounding()
        If deferredCausation.CurrencyId > 0 Then
            Using modelCurrency As New MCurrency("")
                Dim currency As CommonCurrencyXpo = modelCurrency.GetCurrencybyIdXpo(deferredCausation.CurrencyId)
                Dim roundC = currency.RoundingType
                Select Case roundC
                    Case 1
                        _roundingType = 0.01
                    Case 2
                        _roundingType = 0.1
                    Case 3
                        _roundingType = 1
                    Case 4
                        _roundingType = 10
                    Case 5
                        _roundingType = 100
                    Case 6
                        _roundingType = 1000
                End Select
            End Using
        End If
    End Sub

    Public Function addItemToList(value As Decimal, valueShares As Integer) As List(Of Decimal)
        Dim saldo As Decimal = value
        Dim quote = Utils.RoundValue(value / valueShares, CDec(_roundingType))
        Dim list As List(Of Decimal) = New List(Of Decimal)
        For i = 1 To valueShares
            saldo -= quote
            list.Add(quote)
        Next
        If saldo > 0 Then
            Dim index = valueShares - 1
            list(index) += saldo
        End If
        If saldo < 0 Then
            Dim index = valueShares - 1
            saldo = saldo * -1
            list(index) -= saldo
        End If
        Return list
    End Function

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDtxtShare.Text = 0
        PeriodNumbers = 0
        INDrgTypeDistribution.EditValue = Nothing
        IdThirdParty = Nothing
        INDsleThirdParty.Properties.NullText = String.Empty
        IdThirdParty = _idThirParty
        INDsleThirdParty.Properties.NullText = _CodeNameThirdParty
        IdMainAccount = Nothing
        IdCostCenter = Nothing
        ValuePopup = 0
        INDlyItemCostCenterPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemValuePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ListDeferredCausationDetail = Nothing
        ListDeleteDontId = Nothing
        INDgcDetailDeferredCausation.DataSource = Nothing
        INDrgTypeDistribution.Enabled = True
        INDsePeriodNumbers.Enabled = True
        INDEsbCausantionDetails.Enabled = False
        INDBtnImportFile.Enabled = False
        viewDetailDeferredCausation.OptionsView.ShowFooter = False
    End Sub

    ''' <summary>
    ''' Establece el tercero por id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConsultThirdByIdSupplier()
        Presenter.InitializeThirdPartyByIdSupplier(IdSupplier)
        INDsleThirdParty.Properties.NullText = ThirdParty.Nit + " - " + ThirdParty.Name
        IdThirdParty = ThirdParty.Id
        _CodeNameThirdParty = ThirdParty.Nit + " - " + ThirdParty.Name
        _idThirParty = ThirdParty.Id
    End Sub

    ''' <summary>
    ''' Establece el tercero por su id
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConsultThirdPartyById(ByVal id As Integer)
        Presenter.InitializeThirdPartyById(id)
        INDsleThirdParty.Properties.NullText = ThirdPartyCommon.Nit + " - " + ThirdPartyCommon.Name
        IdThirdParty = ThirdPartyCommon.Id
    End Sub

    ''' <summary>
    ''' Metodo para limpiar los controles del popup control
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanPopup()
        IdMainAccount = Nothing
        IdCostCenter = Nothing
        ValuePopup = Nothing
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup()
        If IdMainAccount = 0 OrElse IdMainAccount = Nothing Then
            Return False
        End If
        If INDlyItemCostCenterPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If IdCostCenter = 0 OrElse IdCostCenter = Nothing Then
                Return False
            End If
        End If
        If INDlyItemValuePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ValuePopup = 0 Then
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Metodo que valida los controles del form
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControls() As Boolean
        If PeriodNumbers = 0 Then
            Return False
        End If
        If INDrgTypeDistribution.EditValue Is Nothing Then
            Return False
        End If
        If IdThirdParty = 0 OrElse IdThirdParty = Nothing Then
            Return False
        End If
        If ListDeferredCausationDetail Is Nothing OrElse ListDeferredCausationDetail.Count = 0 Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Metodo que carga la informacion de lo que viene del objeto complejo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadInformation()
        If deferredCausation.IdThirdParty = 0 Then
            ConsultThirdByIdSupplier()
        Else
            ConsultThirdPartyById(deferredCausation.IdThirdParty)
        End If
        With deferredCausation
            SetCurrencyFormatUI(.CurrencyAbbreviation)
            BillNumber = .BillNumber
            If .IdCostCenter IsNot Nothing Then
                INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                IdCostCenterCabecera = .IdCostCenter
                INDtxtCostCenter.Text = .DescriptionCostCenter
            Else
                INDtxtCostCenter.Text = String.Empty
                INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            IdMainAccountCabecera = .IdMainAccount
            INDtxtMainAccounts.Text = .NumberNameMainAccount
            Value = .ValueCreditPeriod

            'Cuando edito la causacion
            If .PeriodsNumber <> Nothing Then
                valShare = .ValueCreditPeriod / .PeriodsNumber
                PeriodNumbers = .PeriodsNumber
                INDrgTypeDistribution.EditValue = .TypeDistribution
                IdThirdParty = .IdThirdParty
                INDgcDetailDeferredCausation.DataSource = Nothing
                ListDeferredCausationDetail = New List(Of DeferredCausationDetails)(.DeferredCausationDetails.ToArray)
                INDgcDetailDeferredCausation.DataSource = ListDeferredCausationDetail
                INDbtnDefer.Text = "Editar"
                INDsePeriodNumbers.Enabled = False
                INDrgTypeDistribution.Enabled = False
                viewDetailDeferredCausation.OptionsView.ShowFooter = True
                INDtxtShare.Text = 0
                DateBill = .InitialDate
            End If

        End With
    End Sub

    ''' <summary>
    ''' Agrega un detalle a la causacion diferida a la rejilla de causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDetailDeferredCausationDetail()
        If PeriodNumbers = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NumberPeriodsZero", NAME_MODULE)
            INDpceAddDetail.ClosePopup()
            INDsePeriodNumbers.Focus()
            Exit Sub
        End If
        If INDrgTypeDistribution.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("TypeDistribution", NAME_MODULE)
            INDpceAddDetail.ClosePopup()
            INDrgTypeDistribution.Focus()
            Exit Sub
        End If
        If PeriodNumbers > Value Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NumberPeriodsInvalid", NAME_MODULE)
            PeriodNumbers = 0
            INDpceAddDetail.ClosePopup()
            INDsePeriodNumbers.Focus()
            Exit Sub
        End If
        If ValidateControlsPopup() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPopupBills_FieldEmpty", NAME_MODULE)
            INDsleMainAccounts.Focus()
            Exit Sub
        End If

        If ListDeferredCausationDetail Is Nothing Then
            ListDeferredCausationDetail = New List(Of DeferredCausationDetails)
        End If
        Dim deferredCausationDetail As New DeferredCausationDetails
        With deferredCausationDetail
            .IdMainAccount = IdMainAccount
            .NumberNameMainAccount = INDsleMainAccounts.Text
            If IdCostCenter > 0 Then
                .IdCostCenter = IdCostCenter
                .DescriptionCostCenter = INDsleCostCenter.Text
            Else
                .DescriptionCostCenter = String.Empty
                .IdCostCenter = Nothing
            End If
            .Nature = 1
            .DateNextPeriod = DateTime.Now
            If INDlyItemValuePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If ListDeferredCausationDetail.Count = 0 Then
                    If ValuePopup > valShare Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ValueExceeded", NAME_MODULE)
                        INDtxtValuePopup.Focus()
                        ValuePopup = 0
                        Exit Sub
                    Else
                        .Value = ValuePopup
                        INDtxtShare.Text = valShare - ValuePopup
                    End If
                Else
                    Dim val As Decimal = PaymentServices.SumValues(ListDeferredCausationDetail, ValuePopup, True)
                    If val > valShare Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ValueExceeded", NAME_MODULE)
                        INDtxtValuePopup.Focus()
                        ValuePopup = 0
                        Exit Sub
                    Else
                        .Value = ValuePopup
                        INDtxtShare.Text = CInt(INDtxtShare.EditValue) - ValuePopup
                    End If
                End If
            Else
                If ListDeferredCausationDetail.Count = 0 Then
                    .Value = valShare
                    INDtxtShare.Text = 0
                Else
                    getRounding()
                    Dim listValueShares As List(Of Decimal) = New List(Of Decimal)
                    If _roundingType Is Nothing Then
                        listValueShares = CommonService.GetValueShare(valShare, ListDeferredCausationDetail.Count + 1)
                    Else
                        listValueShares = addItemToList(valShare, ListDeferredCausationDetail.Count + 1)
                    End If
                    .Value = listValueShares.Item(ListDeferredCausationDetail.Count)
                    Dim cont As Integer = 0
                    For Each item As DeferredCausationDetails In ListDeferredCausationDetail
                        item.Value = listValueShares.Item(cont)
                        cont += 1
                    Next
                End If
            End If
        End With
        ListDeferredCausationDetail.Add(deferredCausationDetail)
        INDrgTypeDistribution.Enabled = False
        INDsePeriodNumbers.Enabled = False
        viewDetailDeferredCausation.OptionsView.ShowFooter = True
        INDgcDetailDeferredCausation.DataSource = Nothing
        INDgcDetailDeferredCausation.DataSource = ListDeferredCausationDetail
        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("DistributionLinesDetailAgregateSatisfactory")
        CleanPopup()
        INDsleMainAccounts.Focus()
    End Sub

    ''' <summary>
    ''' Elimina un detalle de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetailDeferredCausation()
        Dim detail As DeferredCausationDetails = CType(viewDetailDeferredCausation.GetFocusedRow, DeferredCausationDetails)
        ListDeferredCausationDetail.Remove(detail)

        'Se llena el listado ListDeleteDontId que representa a la entidad de detalles
        ' y no tienen id
        If ListDeleteDontId Is Nothing Then
            ListDeleteDontId = New List(Of DeferredCausationDetails)
        End If
        ListDeleteDontId.Add(detail)

        If detail.Id <> Nothing Then
            'detail.MarkAsDeleted()
            If listDeleteDeferredCausationDetail Is Nothing Then
                listDeleteDeferredCausationDetail = New List(Of DeferredCausationDetails)
            End If
            listDeleteDeferredCausationDetail.Add(detail)
        End If
        INDgcDetailDeferredCausation.DataSource = Nothing
        INDgcDetailDeferredCausation.DataSource = ListDeferredCausationDetail
        If ListDeferredCausationDetail Is Nothing OrElse ListDeferredCausationDetail.Count = 0 Then
            INDrgTypeDistribution.Enabled = True
            INDsePeriodNumbers.Enabled = True
            viewDetailDeferredCausation.OptionsView.ShowFooter = False
        End If
        If INDrgTypeDistribution.EditValue = 1 Then
            If ListDeferredCausationDetail.Count > 0 Then
                getRounding()
                Dim listValueShares As List(Of Decimal) = New List(Of Decimal)
                If _roundingType Is Nothing Then
                    listValueShares = CommonService.GetValueShare(valShare, ListDeferredCausationDetail.Count)
                Else
                    listValueShares = addItemToList(valShare, ListDeferredCausationDetail.Count)
                End If
                Dim valProportionalValue As Decimal = PaymentServices.CreateValueProportional(valShare, ListDeferredCausationDetail.Count, False)
                Dim cont As Integer = 0
                For Each item As DeferredCausationDetails In ListDeferredCausationDetail
                    item.Value = listValueShares.Item(cont)
                    cont += 1
                Next
                INDgcDetailDeferredCausation.RefreshDataSource()
            Else
                INDtxtShare.Text = valShare
            End If
        Else
            INDtxtShare.Text = CInt(INDtxtShare.EditValue) + detail.Value
        End If
    End Sub

    ''' <summary>
    ''' Calcula la cuota
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub CalculeShare()
        getRounding()
        Dim listSharePivot As List(Of Decimal) = New List(Of Decimal)
        If _roundingType Is Nothing Then
            listSharePivot = CommonService.GetValueShare(Value, PeriodNumbers)
        Else
            listSharePivot = addItemToList(Value, PeriodNumbers)
        End If
        valShare = listSharePivot(0)
        INDtxtShare.Text = valShare
        INDtxtTotalShare.Text = valShare
    End Sub

    ''' <summary>
    ''' metodo que establece la cultura de la moneda en los controles del form
    ''' </summary>
    ''' <param name="currencyAbbreviation"></param>
    Private Sub SetCurrencyFormatUI(currencyAbbreviation As String)
        currencyAbbreviation = If(String.IsNullOrEmpty(currencyAbbreviation), SessionValues.Instance.CurrencyISO4217, currencyAbbreviation)

        Dim culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        culture.NumberFormat = New Globalization.CultureInfo(currencyAbbreviation.GetCultureId()).NumberFormat
        changeNumericFormatByCurrency(culture.NumberFormat)
        INDtxtTotalShare.Properties.Mask.Culture = culture
        INDtxtShare.Properties.Mask.Culture = culture
        INDtxtValuePopup.Properties.Mask.Culture = culture
        GridColumn3 = Window.Utils.FormatGrid(GridColumn3, currencyAbbreviation)
    End Sub
#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _deferredCausation = Nothing
        ListDeferredCausationDetail = Nothing
        Presenter = Nothing
        _idSupplier = Nothing
        _thirdParty = Nothing
        _thirdPartyCommon = Nothing
        _CodeNameThirdParty = Nothing
        _idThirParty = Nothing
        valShare = Nothing
        _banClose = Nothing
        _idMainAccountCabecera = Nothing
        _idCostCenterCabecera = Nothing
        _listDeleteDeferredCausationDetail = Nothing
        ListDeleteDontId = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el formulario de causacion diferida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupDeferredCausation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BarraBotones.OperatingUnitVisible = False
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        INDEsbCausantionDetails.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Cuenta Contable", .Type = ExcelColumnType.Text, .Comment = "Digite el número de la cuenta contable"},
                            New ExcelColumn With {.Name = "Centro de Costo", .Type = ExcelColumnType.Text, .Comment = "Digite el código del centro de costo"},
                            New ExcelColumn With {.Name = "Valor", .Type = ExcelColumnType.Number}
                        }
                    })
        Presenter = New PPopupDeferredCausation(Me)
        Presenter.LoadRepositorySearchLookUp()
        LoadInformation()
        viewDetailDeferredCausation.OptionsView.ShowFooter = False
        IndigoGridControl1.RefreshGrid(INDgcDetailDeferredCausation)
        BarraBotones.Minimizar(True)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewDetailDeferredCausation, ListActions)
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape en el formulario de causacion diferida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupDeferredCausation_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            BanClose = False
            Close()
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario de causacion diferida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupDeferredCausation_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDsePeriodNumbers.EditValue = 0 Then
            INDsePeriodNumbers.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThirdParty.QueryPopUp
        If INDsleThirdParty.Properties.DataSource Is Nothing Then
            Presenter.LoadSearchLookUpThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccounts_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMainAccounts.QueryPopUp
        If INDsleMainAccounts.Properties.DataSource Is Nothing Then
            Presenter.LoadSearchLookUpMainAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If INDsleCostCenter.Properties.DataSource Is Nothing Then
            Presenter.LoadSearchLookUpCostCenter()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de distribucion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrgTypeDistribution_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgTypeDistribution.EditValueChanged
        If INDrgTypeDistribution.EditValue IsNot Nothing Then
            If INDrgTypeDistribution.EditValue = 1 Then
                INDlyItemValuePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDEsbCausantionDetails.Enabled = False
                INDBtnImportFile.Enabled = False
            ElseIf INDrgTypeDistribution.EditValue = 2 Then
                INDlyItemValuePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDEsbCausantionDetails.Enabled = True
                INDBtnImportFile.Enabled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de cuentas contables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccounts_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMainAccounts.EditValueChanged
        If IdMainAccount > 0 Then
            Using model As New MPUC(CStr(Tag))
                Dim puc As MainAccounts
                puc = model.GetAccountId(IdMainAccount)
                If puc.HandlesCostCenter = True Then
                    INDlyItemCostCenterPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDlyItemCostCenterPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de periodo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsePeriodNumbers_EditValueChanged(sender As Object, e As EventArgs) Handles INDsePeriodNumbers.EditValueChanged
        If PeriodNumbers <> 0 Then
            CalculeShare()
        Else
            INDtxtShare.Text = 0
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de tercero
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
            Presenter.LoadSearchLookUpThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AddDetailDeferredCausationDetail()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccounts_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMainAccounts.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.LoadSearchLookUpMainAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de centro costo
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
            Presenter.LoadSearchLookUpCostCenter()
        End If
    End Sub


    Private Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        If valShare > 0 Then

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
                        Dim sddf = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
                        sddf.AllowDrop = False
                        sddf.LoadDocument(myStream)
                        Dim workBook As IWorkbook = sddf.Document

                        rows = workBook.Worksheets(0).Rows
                        If rows.LastUsedIndex = 0 Then
                            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                            AsyncLoader(False)
                            Exit Sub
                        End If

                        Using Model As New MAccountPayable(Me.Tag.ToString())
                            listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()

                            SetRow(1, rows.LastUsedIndex + 1)

                            Dim result = Model.SetCopyPasteOrImportFileDeferredCausation(listRows.ToList(), Nothing, valShare)

                            'si ocurrio un error
                            If result.StatusCode = eStatusResult.EXCEPTION Or result.MessageResult.Count > 0 Then
                                Using formulario As New FrmListErrors(result.MessageResult)
                                    formulario.StartPosition = FormStartPosition.CenterParent
                                    Dim transparent As New FrmTransparent(formulario, False)
                                    Me.Cursor = System.Windows.Forms.Cursors.Default
                                    transparent.ShowDialog(Me)
                                End Using
                                AsyncLoader(False)
                            End If

                            If ListDeferredCausationDetail IsNot Nothing AndAlso ListDeferredCausationDetail.Count > 0 Then
                                For Each item In ListDeferredCausationDetail
                                    ListDeferredCausationDetail.Add(item)
                                Next
                            Else
                                If result.ObjectEmbbeded.Count > 0 Then
                                    ListDeferredCausationDetail = result.ObjectEmbbeded
                                    Dim sumValues = ListDeferredCausationDetail.Sum(Function(x) x.Value)
                                    INDtxtShare.Text = valShare - sumValues
                                    INDlyItemPeriodNumbers.Enabled = False
                                    INDlyItemTypeDistribution.Enabled = False
                                    viewDetailDeferredCausation.OptionsView.ShowFooter = True
                                    INDgcDetailDeferredCausation.DataSource = Nothing
                                    INDgcDetailDeferredCausation.DataSource = ListDeferredCausationDetail
                                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("DistributionLinesDetailAgregateSatisfactory")
                                    CleanPopup()
                                End If
                            End If

                            Me.Cursor = System.Windows.Forms.Cursors.Default
                        End Using
                    End If
                    AsyncLoader(False)
                Catch ex As Exception
                    AsyncLoader(False)
                End Try
            End If
            AsyncLoader(False)
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Digite numero de cuotas"
            AsyncLoader(False)
            Exit Sub
        End If
    End Sub

    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(14)})
                                              End SyncLock
                                          End Sub)
    End Sub
#End Region

#Region "CopyPaste"

    Private Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If valShare > 0 Then
            AsyncLoader(True)
            Using Model As New MAccountPayable(Me.Tag.ToString())
                Dim result = Model.SetCopyPasteOrImportFileDeferredCausation(Nothing, e.Rows, valShare)

                'si ocurrio un error
                If result.StatusCode = eStatusResult.EXCEPTION Or result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                    AsyncLoader(False)
                End If
                If ListDeferredCausationDetail IsNot Nothing AndAlso ListDeferredCausationDetail.Count > 0 Then
                    For Each item In result.ObjectEmbbeded
                        ListDeferredCausationDetail.Add(item)
                    Next
                Else
                    If result.ObjectEmbbeded.Count > 0 Then
                        ListDeferredCausationDetail = result.ObjectEmbbeded
                        Dim sumValues = ListDeferredCausationDetail.Sum(Function(x) x.Value)
                        INDtxtShare.Text = valShare - sumValues
                        INDlyItemPeriodNumbers.Enabled = False
                        INDlyItemTypeDistribution.Enabled = False
                        viewDetailDeferredCausation.OptionsView.ShowFooter = True
                        INDgcDetailDeferredCausation.DataSource = Nothing
                        INDgcDetailDeferredCausation.DataSource = ListDeferredCausationDetail
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("DistributionLinesDetailAgregateSatisfactory")
                        CleanPopup()
                    End If
                End If

                Me.Cursor = System.Windows.Forms.Cursors.Default
            End Using
            AsyncLoader(False)
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Digite numero de cuotas"
            AsyncLoader(False)
            Exit Sub
        End If
    End Sub

#End Region


#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el popup de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpopupAddDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceAddDetail.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceAddDetail.ShowPopup()
        End If
    End Sub

#End Region

#Region "ContextMenu"

    ''' <summary>
    ''' Evento que se dispara al presionar eliminar del menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Dim action As DXMenuItem = DirectCast(sender, DXMenuItem)
        Select Case (action.Tag.ToString)
            Case Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                DeleteDetailDeferredCausation()
        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presiona eliminar en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteDetailDeferredCausation()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnDefer_Click(sender As Object, e As EventArgs) Handles INDbtnDefer.Click
        AddDeferredCausationToAccountPayable()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupDeferredCausation_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
            If ListDeleteDontId IsNot Nothing AndAlso ListDeleteDontId.Count > 0 AndAlso listDeleteDeferredCausationDetail IsNot Nothing AndAlso listDeleteDeferredCausationDetail.Count > 0 Then
                For Each item As DeferredCausationDetails In ListDeleteDontId
                    If listDeleteDeferredCausationDetail.Contains(item) Then
                        listDeleteDeferredCausationDetail.Remove(item)
                    End If
                Next
            End If
        End If
        BanClose = False
    End Sub

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAddDetail_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceAddDetail.CloseUp
        INDbtnDefer.Focus()
    End Sub

#End Region

#End Region

#Region "BarraBotones"

    ''' <summary>
    ''' Barra Botones: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

#End Region

#Region "Interfaz"

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Metodo que asigna el mensaje
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

#End Region

End Class