Imports Presentation.FixedAsset.MVP
Imports Domain.Entities
Imports Presentation.Controls
Imports Presentation.Base
Imports System.Text
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base

Public Class FrmFixedAssetEntryItemDetailPart
    Implements IFixedAssetEntryItemDetailPart

#Region "Builder"

    ''' <summary>
    ''' Inicializa una instancia de la clase
    ''' </summary>
    ''' <param name="_currency"></param>
    Public Sub New(Optional _currency As Currency = Nothing)
        InitializeComponent()
        Me._indigo = SessionValues.Instance
        Me._headCurrency = If(_currency Is Nothing, New Currency With {.Id = Me._indigo.OfficialCurrencyId,
                            .Abbreviation = Me._indigo.CurrencyISO4217}, _currency)
        Me.SetCurrencyUI(Me._headCurrency?.Abbreviation)
    End Sub

#End Region

#Region "Properties"

    Public Property DepreciationTypeId As Integer? Implements IFixedAssetEntryItemDetailPart.DepreciationTypeId
        Get
            Return INDsleDepreciationType.EditValue
        End Get
        Set(value As Integer?)
            INDsleDepreciationType.EditValue = value
        End Set
    End Property

    Public Property LegalBookId As Integer? Implements IFixedAssetEntryItemDetailPart.LegalBookId
        Get
            Return INDsleLegalBook.EditValue
        End Get
        Set(value As Integer?)
            INDsleLegalBook.EditValue = value
        End Set
    End Property

    Public Property LegalBookXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IFixedAssetEntryItemDetailPart.LegalBookXpo
        Get
            Return INDsleLegalBook.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleLegalBook.Properties.DataSource = value
        End Set
    End Property

    Public Property LifeTime As Integer Implements IFixedAssetEntryItemDetailPart.LifeTime
        Get
            Return INDseLifeUtil.EditValue
        End Get
        Set(value As Integer)
            INDseLifeUtil.EditValue = value
        End Set
    End Property

    Public Property PercentageRescue As Decimal Implements IFixedAssetEntryItemDetailPart.PercentageRescue
        Get
            Return INDsePercentageRescue.EditValue
        End Get
        Set(value As Decimal)
            INDsePercentageRescue.EditValue = value
        End Set
    End Property

    Public Property TotalProductionUnit As Long Implements IFixedAssetEntryItemDetailPart.TotalProductionUnit
        Get
            Return INDseTotalProductionUnit.EditValue
        End Get
        Set(value As Long)
            INDseTotalProductionUnit.EditValue = value
        End Set
    End Property

    Public Property UnitLifeTimeId As Integer? Implements IFixedAssetEntryItemDetailPart.UnitLifeTimeId
        Get
            Return INDsleUnitLifeUtil.EditValue
        End Get
        Set(value As Integer?)
            INDsleUnitLifeUtil.EditValue = value
        End Set
    End Property

    Public Property DepreciatePart As Boolean? Implements IFixedAssetEntryItemDetailPart.DepreciatePart
        Get
            Return INDsleDepreciatePart.EditValue
        End Get
        Set(value As Boolean?)
            INDsleDepreciatePart.EditValue = value
        End Set
    End Property

    Public Property PartAccesoriesConsumableId As Integer? Implements IFixedAssetEntryItemDetailPart.PartAccesoriesConsumableId
        Get
            Return INDslePartAccesoriesConsumibles.EditValue
        End Get
        Set(value As Integer?)
            INDslePartAccesoriesConsumibles.EditValue = value
        End Set
    End Property

    Public Property PartAccesoriesConsumableXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IFixedAssetEntryItemDetailPart.PartAccesoriesConsumableXpo
        Get
            Return INDslePartAccesoriesConsumibles.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDslePartAccesoriesConsumibles.Properties.DataSource = value
        End Set
    End Property

    Public Property ValuePart As Decimal Implements IFixedAssetEntryItemDetailPart.ValuePart
        Get
            Return INDtxtValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValue.EditValue = value
        End Set
    End Property

    Private _editMode As Boolean
    Public Property EditMode As Boolean
        Get
            Return _editMode
        End Get
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property

    Private _listCompare As List(Of FixedAssetEntryItemDetailPart)
    Public Property ListCompare As List(Of FixedAssetEntryItemDetailPart)
        Get
            Return _listCompare
        End Get
        Set(value As List(Of FixedAssetEntryItemDetailPart))
            _listCompare = value
        End Set
    End Property

    Private _fixedAssetEntryItemDetailPart As FixedAssetEntryItemDetailPart
    Public Property FixedAssetEntryItemDetailPart As FixedAssetEntryItemDetailPart
        Get
            Return _fixedAssetEntryItemDetailPart
        End Get
        Set(value As FixedAssetEntryItemDetailPart)
            _fixedAssetEntryItemDetailPart = value
        End Set
    End Property

    Private _depreciateItem As Boolean
    Public Property DepreciateItem As Boolean
        Get
            Return _depreciateItem
        End Get
        Set(value As Boolean)
            _depreciateItem = value
        End Set
    End Property

#End Region

#Region "Event"

    ''' <summary>
    ''' Evento para agregar una parte al detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddFixedAssetEntryItemPartEventArgs(sender As Object, e As AddFixedAssetEntryItemDetailPart)

#End Region

#Region "Globals"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PFixedAssetEntryItemDetailPart

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListUnitLifeUtil As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListDepreciationType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Permite saber si el registro esta en modo de edición
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagEditModePopupBook As Boolean = False

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetEntryItemDetailPartBook As List(Of FixedAssetEntryItemDetailPartBook)

    ''' <summary>
    ''' Representa a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetEntryItemDetailPartBook As FixedAssetEntryItemDetailPartBook

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetEntryItemDetailPartBook As List(Of FixedAssetEntryItemDetailPartBook)

    ''' <summary>
    ''' variable de session
    ''' </summary>
    Private _indigo As SessionValues

    ''' <summary>
    ''' Moneda de la cabecera del documento
    ''' </summary>
    Private _headCurrency As Currency

#End Region

#Region "ICrud"

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

        Me.INDtxtValue.Properties.Mask.Culture = _culture
    End Sub

    Private Sub AddParts()
        If ValidateControls() = False Then 'Se validan que los controles esten diligenciados
            Exit Sub
        End If

        If EditMode = False Then 'Esta guardando el item
            If ListCompare IsNot Nothing AndAlso ListCompare.Count > 0 Then 'Se valida que el articulo no se encuentre en el form principal
                Dim cont = (From l In ListCompare Where l.PartAccesoriesConsumiblesId = PartAccesoriesConsumableId Select l).Count
                If cont > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "La parte " + INDslePartAccesoriesConsumibles.Text + " ya se encuentra en el listado."
                    Exit Sub
                End If
            End If
        End If

        'Se asigna los valores
        AssigningValues()

        'Se crea el objeto que se va a devolver
        Dim args As New AddFixedAssetEntryItemDetailPart
        args.FixedAssetEntryItemDetailPart = FixedAssetEntryItemDetailPart
        args.EditMode = EditMode
        args.ListDeleteFixedAssetEntryItemDetailPartBook = ListDeleteFixedAssetEntryItemDetailPartBook
        RaiseEvent AddFixedAssetEntryItemPartEventArgs(Nothing, args)
        CleanControls()
        INDslePartAccesoriesConsumibles.Focus()
    End Sub

    Private Sub AssigningValues()
        If EditMode = False Then 'Si se esta guardando el item se instancia el objeto
            FixedAssetEntryItemDetailPart = New FixedAssetEntryItemDetailPart
        End If
        With FixedAssetEntryItemDetailPart
            .PartAccesoriesConsumiblesId = PartAccesoriesConsumableId
            .PartAccesoriesConsumablesCodeName = INDslePartAccesoriesConsumibles.Text
            .DepreciatePart = DepreciatePart
            .Value = ValuePart

            If INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then 'Si hay detalles de libros
                .FixedAssetEntryItemDetailPartBook.Clear()
                If ListFixedAssetEntryItemDetailPartBook IsNot Nothing AndAlso ListFixedAssetEntryItemDetailPartBook.Count > 0 Then
                    For Each item In ListFixedAssetEntryItemDetailPartBook
                        .FixedAssetEntryItemDetailPartBook.Add(item)
                    Next
                End If
            Else 'Si no hay detalles de libros
                If .FixedAssetEntryItemDetailPartBook IsNot Nothing AndAlso .FixedAssetEntryItemDetailPartBook.Count > 0 Then
                    For Each item In (From l In .FixedAssetEntryItemDetailPartBook Where l.Id > 0 Select l).ToList
                        If ListDeleteFixedAssetEntryItemDetailPartBook Is Nothing Then
                            ListDeleteFixedAssetEntryItemDetailPartBook = New List(Of FixedAssetEntryItemDetailPartBook)
                        Else
                            If ListDeleteFixedAssetEntryItemDetailPartBook.Contains(item) Then
                                Continue For
                            End If
                        End If
                        ListDeleteFixedAssetEntryItemDetailPartBook.Add(item)
                    Next
                    .FixedAssetEntryItemDetailPartBook.Clear()
                End If
            End If
        End With
    End Sub

    Private Sub CleanControls()
        FixedAssetEntryItemDetailPart = Nothing
        EditMode = False
        PartAccesoriesConsumableId = Nothing
        INDslePartAccesoriesConsumibles.Properties.NullText = String.Empty
        If DepreciateItem = True Then
            DepreciatePart = Nothing
            INDsleDepreciatePart.Properties.ReadOnly = False
        End If
        ValuePart = Nothing
        INDgcDetailsBook.DataSource = Nothing
        ListFixedAssetEntryItemDetailPartBook = Nothing
        INDslePartAccesoriesConsumibles.Properties.ReadOnly = False
        INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    Private Sub LoadControls()
        With FixedAssetEntryItemDetailPart
            PartAccesoriesConsumableId = .PartAccesoriesConsumiblesId
            INDslePartAccesoriesConsumibles.Properties.NullText = .PartAccesoriesConsumablesCodeName
            DepreciatePart = .DepreciatePart
            ValuePart = .Value

            If .DepreciatePart Then
                ListFixedAssetEntryItemDetailPartBook = .FixedAssetEntryItemDetailPartBook.ToList
                INDgcDetailsBook.DataSource = Nothing
                INDgcDetailsBook.DataSource = ListFixedAssetEntryItemDetailPartBook
            End If
        End With
        INDslePartAccesoriesConsumibles.Properties.ReadOnly = True
    End Sub

    Private Sub InitializeTuple()
        'Unidad vida util
        ListUnitLifeUtil = New List(Of Tuple(Of Integer, String))
        ListUnitLifeUtil.Add(New Tuple(Of Integer, String)(1, "Año"))
        ListUnitLifeUtil.Add(New Tuple(Of Integer, String)(2, "Mes"))
        ListUnitLifeUtil.Add(New Tuple(Of Integer, String)(3, "Día"))
        INDsleUnitLifeUtil.Properties.DataSource = ListUnitLifeUtil.ToList
        'Tipo de depreciación
        ListDepreciationType = New List(Of Tuple(Of Integer, String))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(1, "Línea Recta"))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(2, "Suma de Dígitos"))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(3, "Reducción de Saldos"))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(4, "Unidades de Producción"))
        INDsleDepreciationType.Properties.DataSource = ListDepreciationType.ToList
    End Sub

    Private Sub AddDetailsBook()
        Dim errors = ValidateControlsPopup()
        If errors.Length > 0 Then 'Se validan que los controles esten diligenciados
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If FlagEditModePopupBook = False Then 'Si se esta agregando el registro
            If ListFixedAssetEntryItemDetailPartBook Is Nothing Then
                ListFixedAssetEntryItemDetailPartBook = New List(Of FixedAssetEntryItemDetailPartBook)
            Else
                'Se valida que el libro que se esta agregando no exista en la rejilla
                Dim cont = (From l In ListFixedAssetEntryItemDetailPartBook Where l.LegalBookId = LegalBookId Select l).Count
                If cont > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El libro " + INDsleLegalBook.Text + " ya existe en la lista."
                    Exit Sub
                End If
            End If
        End If

        'Se asigna los valores de la entidad
        SetValues()

        If FlagEditModePopupBook = False Then 'Si se esta agregando el registro
            ListFixedAssetEntryItemDetailPartBook.Add(FixedAssetEntryItemDetailPartBook)
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Si se esta modificando el registro
            Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente."
        End If

        INDgcDetailsBook.DataSource = Nothing
        INDgcDetailsBook.DataSource = ListFixedAssetEntryItemDetailPartBook
        CleanControlsPopup()
        INDsleLegalBook.Focus()
    End Sub

    Private Sub SetValues()
        If FlagEditModePopupBook = False Then
            FixedAssetEntryItemDetailPartBook = New FixedAssetEntryItemDetailPartBook
        End If
        With FixedAssetEntryItemDetailPartBook
            .LegalBookId = LegalBookId
            .LegalBookCodeName = INDsleLegalBook.Text
            .LifeTime = LifeTime
            .UnitLifeTime = UnitLifeTimeId
            .DepreciationType = DepreciationTypeId
            If INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .TotalProductionUnit = TotalProductionUnit
            Else
                .TotalProductionUnit = 0
            End If
            If INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .PercentageRescue = PercentageRescue
            Else
                .PercentageRescue = 0
            End If
        End With
    End Sub

    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        If LegalBookId Is Nothing Then
            errors.AppendLine("Debe seleccionar un libro oficial.")
        End If
        If LifeTime = Nothing OrElse LifeTime = 0 Then
            errors.AppendLine("Debe seleccionar una vida útil.")
        End If
        If UnitLifeTimeId Is Nothing Then
            errors.AppendLine("Debe seleccionar una unidad de vida útil.")
        End If
        If DepreciationTypeId Is Nothing Then
            errors.AppendLine("Debe ingresar un tipo de depreciación.")
        End If
        If INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If TotalProductionUnit = Nothing OrElse TotalProductionUnit = 0 Then
                errors.AppendLine("Debe ingresar total de unidades producidas.")
            End If
        End If
        If INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If PercentageRescue = Nothing Then
                errors.AppendLine("Debe ingresar un % de salvamento.")
            End If
        End If
        Return errors.ToString
    End Function

    Private Sub CleanControlsPopup()
        FlagEditModePopupBook = False
        LegalBookId = Nothing
        INDsleLegalBook.Properties.NullText = String.Empty
        LifeTime = Nothing
        UnitLifeTimeId = Nothing
        DepreciationTypeId = Nothing
        TotalProductionUnit = Nothing
        PercentageRescue = Nothing
        INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleLegalBook.Properties.ReadOnly = False
    End Sub

    Private Sub EditBook()
        FlagEditModePopupBook = True
        FixedAssetEntryItemDetailPartBook = DirectCast(INDviewDetailBook.GetFocusedRow(), FixedAssetEntryItemDetailPartBook)
        With FixedAssetEntryItemDetailPartBook
            LegalBookId = .LegalBookId
            INDsleLegalBook.Properties.NullText = .LegalBookCodeName
            LifeTime = .LifeTime
            UnitLifeTimeId = .UnitLifeTime
            DepreciationTypeId = .DepreciationType
            TotalProductionUnit = .TotalProductionUnit
            PercentageRescue = .PercentageRescue
        End With
        INDsleLegalBook.Properties.ReadOnly = True
        INDpceDetailsBook.ShowPopup()
    End Sub

    Private Sub RemoveBook()
        FixedAssetEntryItemDetailPartBook = CType(INDviewDetailBook.GetFocusedRow, FixedAssetEntryItemDetailPartBook)
        ListFixedAssetEntryItemDetailPartBook.Remove(FixedAssetEntryItemDetailPartBook)

        If FixedAssetEntryItemDetailPartBook.Id > 0 Then
            If ListDeleteFixedAssetEntryItemDetailPartBook Is Nothing Then
                ListDeleteFixedAssetEntryItemDetailPartBook = New List(Of FixedAssetEntryItemDetailPartBook)
            End If
            ListDeleteFixedAssetEntryItemDetailPartBook.Add(FixedAssetEntryItemDetailPartBook)
        End If

        INDgcDetailsBook.DataSource = Nothing
        INDgcDetailsBook.DataSource = ListFixedAssetEntryItemDetailPartBook
    End Sub

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        ListUnitLifeUtil = Nothing
        ListDepreciationType = Nothing
        FlagEditModePopupBook = Nothing
        ListFixedAssetEntryItemDetailPartBook = Nothing
        FixedAssetEntryItemDetailPartBook = Nothing
        ListDeleteFixedAssetEntryItemDetailPartBook = Nothing
    End Sub

    Private Sub FrmFixedAssetEntryItemDetailPart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeTuple()
        Presenter = New PFixedAssetEntryItemDetailPart(Me)
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True

        IndigoGridControl1.RefreshGrid(INDgcDetailsBook)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDviewDetailBook, ListActions)

        If EditMode Then 'Si esta en modo edición
            LoadControls()
        End If

        'Si el articulo no deprecia la parte tampoco deprecia y no se puede cambiar
        If DepreciateItem = False Then
            DepreciatePart = DepreciateItem
            INDsleDepreciatePart.Properties.ReadOnly = True
        End If

    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDslePartAccesoriesConsumibles_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDslePartAccesoriesConsumibles.QueryPopUp
        If PartAccesoriesConsumableXpo Is Nothing Then
            Presenter.InitializePartAccesoriesConsumables()
        End If
    End Sub

    Private Sub INDsleLegalBook_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleLegalBook.QueryPopUp
        If LegalBookXpo Is Nothing Then
            Presenter.InitializeLegalBook()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDslePartAccesoriesConsumibles_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePartAccesoriesConsumibles.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1704, Nothing, True)
            Presenter.InitializePartAccesoriesConsumables()
        End If
    End Sub

    Private Sub INDsleLegalBook_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleLegalBook.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1682, Nothing, True)
            Presenter.InitializeLegalBook()
        End If
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmFixedAssetEntryItemDetailPart_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDslePartAccesoriesConsumibles.Focus()
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        AddParts()
    End Sub

    Private Sub INDbtnAddDetailsBook_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetailsBook.Click
        AddDetailsBook()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmFixedAssetEntryItemDetailPart_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDtxtValue_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtValue.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                INDBtnAdd.Focus()
            Else
                INDpceDetailsBook.Focus()
            End If
        End If
    End Sub

    Private Sub INDpceDetailsBook_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceDetailsBook.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.F4 OrElse e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceDetailsBook.ShowPopup()
        End If
    End Sub

#End Region

#Region "Popup"

    Private Sub INDpceDetailsBook_Popup(sender As Object, e As EventArgs) Handles INDpceDetailsBook.Popup
        INDsleLegalBook.Focus()
    End Sub

#End Region

#Region "CloseUp"

    Private Sub INDpceDetailsBook_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceDetailsBook.CloseUp
        If FlagEditModePopupBook Then
            CleanControlsPopup()
        End If
        If e.CloseMode = DevExpress.XtraEditors.PopupCloseMode.Cancel Then
            INDBtnAdd.Focus()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleDepreciatePart_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDepreciatePart.EditValueChanged
        If DepreciatePart IsNot Nothing Then
            If DepreciatePart Then 'Si deprecia muestra el grupo de los libros
                INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else 'Si no deprecia no muestra el grupo de los libros
                INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

    Private Sub INDsleDepreciationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDepreciationType.EditValueChanged
        If DepreciationTypeId IsNot Nothing Then
            If DepreciationTypeId = 4 Then 'Unidades de producción
                INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            ElseIf DepreciationTypeId = 3 Then 'Reducción de saldos
                INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

#End Region

#Region "MenuContext"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditBook()
            Case "Remove"
                RemoveBook()
        End Select
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditBook()
            Case "Remove"
                RemoveBook()
        End Select
    End Sub

#End Region

#Region "CustomColumnDisplayText"

    Private Sub INDviewDetailBook_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDviewDetailBook.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDcolUnitLifeTime.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Año"
                Case 2
                    e.DisplayText = "Mes"
                Case 3
                    e.DisplayText = "Día"
                Case Else

            End Select
        End If
        If e.Column.Name = INDcolDepreciationType.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Línea Recta"
                Case 2
                    e.DisplayText = "Suma de Dígitos"
                Case 3
                    e.DisplayText = "Reducción de Saldos"
                Case 4
                    e.DisplayText = "Unidades de Producción"
                Case Else

            End Select
        End If
    End Sub

#End Region

#End Region

End Class