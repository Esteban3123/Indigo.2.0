'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/05/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.FixedAsset.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.Xpo
Imports System.ComponentModel

#End Region

Public Class FrmFixedAssetActiveOutputDetail
    Implements IFixedAssetActiveOutputDetail

#Region "Builder"

    ''' <summary>
    ''' Inicializa una instancia de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(Optional _currency As Currency = Nothing)
        InitializeComponent()
        Me._indigo = SessionValues.Instance
        Me._headCurrency = If(_currency Is Nothing, New Currency With {.Id = Me._indigo.OfficialCurrencyId,
                            .Abbreviation = Me._indigo.CurrencyISO4217}, _currency)
        Me.SetCurrencyUI(Me._headCurrency?.Abbreviation)
    End Sub

#End Region

#Region "Properties"

    Private _editMode As Boolean
    Public Property EditMode As Boolean
        Get
            Return _editMode
        End Get
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property

    Private _listCompare As List(Of FixedAssetActiveOutputDetail)
    Public Property ListCompare As List(Of FixedAssetActiveOutputDetail)
        Get
            Return _listCompare
        End Get
        Set(value As List(Of FixedAssetActiveOutputDetail))
            _listCompare = value
        End Set
    End Property

    Private _FixedAssetActiveOutputDetail As FixedAssetActiveOutputDetail
    Public Property FixedAssetActiveOutputDetail As FixedAssetActiveOutputDetail
        Get
            Return _FixedAssetActiveOutputDetail
        End Get
        Set(value As FixedAssetActiveOutputDetail)
            _FixedAssetActiveOutputDetail = value
        End Set
    End Property

    Public Property LowType As Integer? Implements IFixedAssetActiveOutputDetail.LowType
        Get
            Return INDsleLowType.EditValue
        End Get
        Set(value As Integer?)
            INDsleLowType.EditValue = value
        End Set
    End Property

    Public Property LowTypeXpo As XPInstantFeedbackSource Implements IFixedAssetActiveOutputDetail.LowTypeXpo
        Get
            Return INDsleLowType.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleLowType.Properties.DataSource = value
        End Set
    End Property

    Public Property OutputType As Integer? Implements IFixedAssetActiveOutputDetail.OutputType
        Get
            Return INDsleOutputType.EditValue
        End Get
        Set(value As Integer?)
            INDsleOutputType.EditValue = value
        End Set
    End Property

    Public Property PhysicalAssetId As Integer? Implements IFixedAssetActiveOutputDetail.PhysicalAssetId
        Get
            Return INDslePhysicalAsset.EditValue
        End Get
        Set(value As Integer?)
            INDslePhysicalAsset.EditValue = value
        End Set
    End Property

    Public Property PhysicalAssetXpo As XPInstantFeedbackSource Implements IFixedAssetActiveOutputDetail.PhysicalAssetXpo
        Get
            Return INDslePhysicalAsset.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePhysicalAsset.Properties.DataSource = value
        End Set
    End Property

    Public Property SalesValue As Decimal Implements IFixedAssetActiveOutputDetail.SalesValue
        Get
            Return INDseSalesValue.EditValue
        End Get
        Set(value As Decimal)
            INDseSalesValue.EditValue = value
        End Set
    End Property

    Public Property ThirdPartyId As Integer? Implements IFixedAssetActiveOutputDetail.ThirdPartyId
        Get
            Return INDsleThirdPartyId.EditValue
        End Get
        Set(value As Integer?)
            INDsleThirdPartyId.EditValue = value
        End Set
    End Property

    Public Property ThirdPartyXpo As XPInstantFeedbackSource Implements IFixedAssetActiveOutputDetail.ThirdPartyXpo
        Get
            Return INDsleThirdPartyId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleThirdPartyId.Properties.DataSource = value
        End Set
    End Property

    Public Property ActiveType As Integer? Implements IFixedAssetActiveOutputDetail.ActiveType
        Get
            Return INDsleActiveType.EditValue
        End Get
        Set(value As Integer?)
            INDsleActiveType.EditValue = value
        End Set
    End Property

    Public Property PhysicalAssetPartId As Integer? Implements IFixedAssetActiveOutputDetail.PhysicalAssetPartId
        Get
            Return INDslePhysicalAssetPart.EditValue
        End Get
        Set(value As Integer?)
            INDslePhysicalAssetPart.EditValue = value
        End Set
    End Property

    Public Property PhysicalAssetPartXpo As XPInstantFeedbackSource Implements IFixedAssetActiveOutputDetail.PhysicalAssetPartXpo
        Get
            Return INDslePhysicalAssetPart.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePhysicalAssetPart.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Globals"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PFixedAssetActiveOutputDetail

    ''' <summary>
    ''' Representa el Id de la cuenta contable del activo fijo
    ''' </summary>
    Private MainAccountId As Integer

    ''' <summary>
    ''' Representa el numero de la cuenta contable
    ''' </summary>
    Private MainAccountNumberName As String

    ''' <summary>
    ''' Contiene los tipos de salidas de activos fijos
    ''' </summary>
    Private ListOutputType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Contiene los tipos de activos fijos
    ''' </summary>
    Private ListType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Bandera que indica que se esta cargando la informacion
    ''' </summary>
    Private FlagChangedSearch As Boolean = True

    ''' <summary>
    ''' Representa el valor historico del activo seleccionado
    ''' </summary>
    Private HistoricalValue As Decimal

    ''' <summary>
    ''' Inidica la clasificacion del activo o parte
    ''' </summary>
    Private ClassificationFixedAsset As Integer

    ''' <summary>
    ''' Moneda de la cabecera del documento
    ''' </summary>
    Private _headCurrency As Currency

	''' <summary>
	''' variable de session
	''' </summary>
	Private _indigo As SessionValues

	''' <summary>
	''' Valida lo que se agrega en primera instancia
	''' </summary>
	Private ListAddActive As New List(Of FixedAssetActiveOutputDetail)
#End Region

#Region "Event"

	Public Event AddFixedAssetActiveOutputDetailEventArgs(sender As Object, e As AddFixedAssetActiveOutputDetail)

#End Region

#Region "Icrud"

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Methods"

    Private Sub InitializeTuple()

        'Tipo de salida
        ListOutputType = New List(Of Tuple(Of Integer, String))
        ListOutputType.Add(New Tuple(Of Integer, String)(1, "Baja"))
        ListOutputType.Add(New Tuple(Of Integer, String)(2, "Venta"))
        INDsleOutputType.Properties.DataSource = ListOutputType.ToList

        ''Tipo
        ListType = New List(Of Tuple(Of Integer, String))
        ListType.Add(New Tuple(Of Integer, String)(1, "Activo"))
        ListType.Add(New Tuple(Of Integer, String)(2, "Parte"))
        INDsleActiveType.Properties.DataSource = ListType.ToList
    End Sub

    Private Sub LoadControls()
        With FixedAssetActiveOutputDetail
            ActiveType = .ActiveType
            FlagChangedSearch = False
            PhysicalAssetId = .PhysicalAssetId
            INDslePhysicalAsset.Properties.NullText = .PhysicalAssetDescription
            PhysicalAssetPartId = .PhysicalAssetPartsId
            INDslePhysicalAssetPart.Properties.NullText = .PhysicalAssetPartDescription
            FlagChangedSearch = True
            HistoricalValue = .HistoricalValue
            MainAccountId = .MainAccountId
            INDtxtMainAccount.EditValue = .MainAccountNumberName
            MainAccountNumberName = .MainAccountNumberName
            OutputType = .OutputType
            FlagChangedSearch = False
            LowType = .LowType
            INDsleLowType.Properties.NullText = .LowTypeName
            FlagChangedSearch = True
            SalesValue = .SalesValue
            ThirdPartyId = .ThirdPartyId
            INDsleThirdPartyId.Properties.NullText = .ThirdPartyNitName
        End With

        INDsleActiveType.Properties.ReadOnly = True
        INDslePhysicalAsset.Properties.ReadOnly = True
        INDslePhysicalAssetPart.Properties.ReadOnly = True
    End Sub

    Private Sub CleanControls()
        FixedAssetActiveOutputDetail = Nothing
        EditMode = False
        PhysicalAssetId = Nothing
        INDslePhysicalAsset.Properties.NullText = String.Empty
        PhysicalAssetPartId = Nothing
        INDslePhysicalAssetPart.Properties.NullText = String.Empty
        MainAccountId = 0
        INDtxtMainAccount.EditValue = Nothing
        OutputType = Nothing
        LowType = Nothing
        LowTypeXpo = Nothing
        SalesValue = Nothing
        ThirdPartyId = Nothing
        INDsleThirdPartyId.Properties.NullText = String.Empty
        INDlygInformation.HideControl()

        INDlyItemLowType.HideLayout()
        INDlyItemSalesValue.HideLayout()
        INDlyItemThirdPartyId.HideLayout()

        If ActiveType = 1 Then 'Activo
            INDlyItemPhysicalAssetPart.HideLayout()
        Else 'Parte
            INDlyItemPhysicalAsset.HideLayout()
        End If

        INDsleActiveType.Properties.ReadOnly = False
        INDslePhysicalAsset.Properties.ReadOnly = False
        INDslePhysicalAssetPart.Properties.ReadOnly = False
    End Sub

	Private Sub AddDetail()
		If ValidateControls() = False Then 'Se validan que los controles esten diligenciados
			Exit Sub
		End If

		If EditMode = False Then 'Esta guardando el item
			' Validación de ListCompare
			If Not ValidateAssetInList(ListCompare, PhysicalAssetId, PhysicalAssetPartId, ClassificationFixedAsset, IIf(ActiveType = 1, INDslePhysicalAsset.Text, INDslePhysicalAssetPart.Text)) Then
				Exit Sub
			End If
			' Validación de ListAddActive
			If Not ValidateAssetInList(ListAddActive, PhysicalAssetId, PhysicalAssetPartId, ClassificationFixedAsset, IIf(ActiveType = 1, INDslePhysicalAsset.Text, INDslePhysicalAssetPart.Text)) Then
				Exit Sub
			End If
		End If

		'Se asigna los valores
		AssigningValues()

		'Se crea el objeto que se va a devolver
		Dim args As New AddFixedAssetActiveOutputDetail
		args.FixedAssetActiveOutputDetail = FixedAssetActiveOutputDetail
		args.EditMode = EditMode
		RaiseEvent AddFixedAssetActiveOutputDetailEventArgs(Nothing, args)
		CleanControls()
		INDsleActiveType.Focus()

		If args.EditMode Then
			Me.Close()
		End If
	End Sub

	Private Function ValidateAssetInList(list As List(Of FixedAssetActiveOutputDetail), assetId As Integer?, physicalAssetPartId As Integer?, classificationFixedAsset As Integer, assetName As String) As Boolean
		Dim showMessage As Boolean = False
		If list IsNot Nothing AndAlso list.Any() Then
			' Comprueba si todos los elementos tienen la misma clasificación (3)
			If classificationFixedAsset = 3 Then
				If Not list.All(Function(item) item?.ClassificationFixedAsset = 3) Then
					showMessage = True
				End If
			Else
				' Verifica si existe al menos un elemento con clasificación 3 en la lista
				If list.Any(Function(item) item?.ClassificationFixedAsset = 3) Then
					showMessage = True
				End If
			End If
			If ActiveType = 1 Then
				If list.Any(Function(l) l.PhysicalAssetId = assetId) Then
					Mensaje(EeventViewerImages.Advertencia) = $"El activo {assetName} ya existe en la lista."
					Return False
				End If
				' Mostrar mensaje si es necesario
				If showMessage Then
					Mensaje(EeventViewerImages.Advertencia) = $"El activo {assetName} ; no cuenta con la misma clasificación en el Catálogo de artículos. " &
														 "Por favor, ajuste el detalle y asegúrese de que todos los activos a retirar tengan la clasificación de Bienes Controlables."
					Return False
				End If
			Else
				If list.Any(Function(l) l.PhysicalAssetPartsId = physicalAssetPartId) Then
					Mensaje(EeventViewerImages.Advertencia) = "La parte " + assetName + " ya existe en la lista."
					Return False
				End If
				If showMessage Then
					Mensaje(EeventViewerImages.Advertencia) = $"La parte {assetName} ; no cuenta con la misma clasificación. " &
														 "Por favor, ajuste el detalle y asegúrese de que todas las partes a retirar se relacionen a un articulo con la clasificación de Bienes Controlables."
					Return False
				End If
			End If
		End If
		Return True
	End Function

	Private Sub AssigningValues()
        If Not EditMode Then 'Si se esta guardando el item se instancia el objeto
            FixedAssetActiveOutputDetail = New FixedAssetActiveOutputDetail
        End If

		With FixedAssetActiveOutputDetail
			.HistoricalValue = HistoricalValue
			.ActiveType = ActiveType
			.ClassificationFixedAsset = ClassificationFixedAsset

			If INDlyItemPhysicalAsset.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
				.PhysicalAssetId = PhysicalAssetId
				.PhysicalAssetDescription = INDslePhysicalAsset.Text
			Else
				.PhysicalAssetId = Nothing
				.PhysicalAssetDescription = String.Empty
			End If

			If INDlyItemPhysicalAssetPart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
				.PhysicalAssetPartsId = PhysicalAssetPartId
				.PhysicalAssetPartDescription = INDslePhysicalAssetPart.Text
			Else
				.PhysicalAssetPartsId = Nothing
				.PhysicalAssetPartDescription = String.Empty
			End If

			.MainAccountId = MainAccountId
			.MainAccountNumberName = MainAccountNumberName
			.OutputType = OutputType
			If INDlyItemLowType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
				.LowType = LowType
				.LowTypeName = INDsleLowType.Text
			End If

			If INDlyItemSalesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
				.SalesValue = SalesValue
			Else
				.SalesValue = 0
			End If

			If INDlyItemThirdPartyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
				.ThirdPartyId = ThirdPartyId
				.ThirdPartyNitName = INDsleThirdPartyId.Text
			Else
				.ThirdPartyId = Nothing
				.ThirdPartyNitName = String.Empty
			End If
		End With
		If ListCompare Is Nothing Then
			If ListAddActive Is Nothing Then
				ListAddActive = New List(Of FixedAssetActiveOutputDetail)
			End If
			ListAddActive.Add(FixedAssetActiveOutputDetail)
		Else
			ListAddActive = Nothing
		End If
	End Sub

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        MainAccountId = Nothing
        MainAccountNumberName = Nothing
        ListOutputType = Nothing
        LowType = Nothing
        LowTypeXpo = Nothing
        ListType = Nothing
        FlagChangedSearch = Nothing
        HistoricalValue = Nothing
        ClassificationFixedAsset = Nothing
    End Sub

    Private Sub FrmFixedAssetActiveOutputDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeTuple()
        Presenter = New PFixedAssetActiveOutputDetail(Me)
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True

        If EditMode Then 'Si esta en modo edición
            LoadControls()
        End If
    End Sub

    ''' <summary>
    ''' Establece el formato moneda en los controles del formulario
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat

        Me.INDseSalesValue.Properties.Mask.Culture = _culture
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmFixedAssetActiveOutputDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDslePhysicalAsset_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePhysicalAsset.QueryPopUp
        If PhysicalAssetXpo Is Nothing Then
            Presenter.InitializePhysicalAsset()
        End If
    End Sub

    Private Sub INDsleThirdPartyId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThirdPartyId.QueryPopUp
        If ThirdPartyXpo Is Nothing Then
            Presenter.InitializeThirdParty()
        End If
    End Sub

    Private Sub INDsleLowType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleLowType.QueryPopUp
        If LowTypeXpo Is Nothing Then
            Presenter.InitializeLowType()
        End If
    End Sub

    Private Sub INDslePart_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePhysicalAssetPart.QueryPopUp
        If PhysicalAssetPartXpo Is Nothing Then
            Presenter.InitializePhysicalAssetPart()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleThirdPartyId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdPartyId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(532, Nothing, True)
            Presenter.InitializeThirdParty()
        End If
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmFixedAssetActiveOutputDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleActiveType.Focus()
    End Sub

#End Region

#Region "EditValueChanged"

    Private Async Sub INDslePhysicalAsset_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePhysicalAsset.EditValueChanged
        If PhysicalAssetId IsNot Nothing AndAlso FlagChangedSearch Then

            Dim xpo = Await Presenter.GetPhysicalById(PhysicalAssetId)
            If xpo IsNot Nothing Then

                'Se valida que el activo tenga asociado una poliza
                If xpo.PolicyId Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "El activo seleccionado no tiene asociado una poliza."
                    INDtxtMainAccount.EditValue = String.Empty
                    MainAccountId = 0
                    MainAccountNumberName = String.Empty
                    PhysicalAssetId = Nothing
                    HistoricalValue = 0
                    ClassificationFixedAsset = 0
                    Exit Sub
                End If

                INDtxtMainAccount.EditValue = xpo.MainAccountId.NumberName
                MainAccountId = xpo.MainAccountId.Id
                MainAccountNumberName = xpo.MainAccountId.NumberName
                HistoricalValue = xpo.HistoricalValue
                ClassificationFixedAsset = xpo.ItemId.ItemCatalogId.Classification
            End If
        End If
    End Sub

	Private Async Sub INDslePhysicalAssetPart_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePhysicalAssetPart.EditValueChanged
		If PhysicalAssetPartId IsNot Nothing AndAlso FlagChangedSearch Then
			'Se obtiene el xpo para consultar la cuenta contable
			Dim xpo = DirectCast(DirectCast(INDviewSearchPart.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetPhysicalAssetPartsXpo)
			If xpo IsNot Nothing Then
				If xpo.PhysicalAssetId IsNot Nothing Then
					Dim physicalAssetId As Integer = Convert.ToInt32(xpo.PhysicalAssetId.Id)
					Dim PhysicalAsset = Await Presenter.GetPhysicalById(physicalAssetId)
					ClassificationFixedAsset = PhysicalAsset.ItemId.ItemCatalogId.Classification
				End If
				'Se valida que la cabecera de la parte tenga asociado una poliza
				If xpo.PhysicalAssetId.PolicyId.Id = 0 Then
					Mensaje(EeventViewerImages.Advertencia) = "La parte no se puede seleccionar porque no tiene asociado una poliza."
					INDtxtMainAccount.EditValue = String.Empty
					MainAccountId = 0
					MainAccountNumberName = String.Empty
					PhysicalAssetPartId = Nothing
					HistoricalValue = 0
					ClassificationFixedAsset = 0
					Exit Sub
				End If

				INDtxtMainAccount.EditValue = xpo.PhysicalAssetId.MainAccountId.NumberName
				MainAccountId = xpo.PhysicalAssetId.MainAccountId.Id
				MainAccountNumberName = xpo.PhysicalAssetId.MainAccountId.NumberName
				HistoricalValue = xpo.HistoricalValue
			End If
		End If
	End Sub

	Private Sub INDsleOutputType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleOutputType.EditValueChanged
        If OutputType IsNot Nothing Then
            Select Case OutputType
                Case 1 'Baja
                    INDlygInformation.HideControl(False)
                    INDlyItemLowType.ShowLayout()
                    INDlyItemSalesValue.HideLayout()
                    INDlyItemThirdPartyId.HideLayout()
                    LowType = Nothing

                Case 2 'Venta
                    INDlygInformation.HideControl(False)
                    INDlyItemLowType.HideLayout()
                    INDlyItemSalesValue.ShowLayout()
                    INDlyItemThirdPartyId.ShowLayout()
                    INDlyItemSalesValue.Text = "Valor de Venta"
            End Select
        End If
    End Sub

    Private Sub INDsleLowType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleLowType.EditValueChanged
        If LowType IsNot Nothing Then
            If LowType = 3 Then 'Perdida Reposición
                INDlyItemSalesValue.ShowLayout()
                INDlyItemSalesValue.Text = "Valor de Reposición"

            ElseIf FlagChangedSearch Then
                INDlyItemSalesValue.HideLayout()
            End If
        End If
    End Sub

    Private Sub INDsleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleActiveType.EditValueChanged
        If ActiveType IsNot Nothing Then
            INDtxtMainAccount.EditValue = Nothing
            MainAccountId = 0
            MainAccountNumberName = String.Empty

            If ActiveType = 1 Then 'Activo
                INDlyItemPhysicalAsset.ShowLayout()
                INDlyItemPhysicalAssetPart.HideLayout()

            ElseIf ActiveType = 2 Then 'Parte
                INDlyItemPhysicalAsset.HideLayout()
                INDlyItemPhysicalAssetPart.ShowLayout()
            End If
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetail.Click
        AddDetail()
    End Sub

#End Region

#End Region

End Class