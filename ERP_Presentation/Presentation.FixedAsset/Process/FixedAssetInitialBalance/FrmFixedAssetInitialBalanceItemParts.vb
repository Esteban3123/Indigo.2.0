'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/04/2016
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
Imports System.Text

#End Region

Public Class FrmFixedAssetInitialBalanceItemParts
    Implements IFixedAssetInitialBalanceItemParts

#Region "Globals"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PFixedAssetInitialBalanceItemParts

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
    Dim ListFixedAssetInitialBalanceItemPartsDetailBook As List(Of FixedAssetInitialBalanceItemPartsDetailBook)

    ''' <summary>
    ''' Representa a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetInitialBalanceItemPartsDetailBook As FixedAssetInitialBalanceItemPartsDetailBook

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetInitialBalanceItemPartsDetailBook As List(Of FixedAssetInitialBalanceItemPartsDetailBook)

#End Region

#Region "Properties"

    Public Property DepreciatedDays As Integer Implements IFixedAssetInitialBalanceItemParts.DepreciatedDays
        Get
            Return INDseDepreciatedDays.EditValue
        End Get
        Set(value As Integer)
            INDseDepreciatedDays.EditValue = value
        End Set
    End Property

    Public Property DepreciatedValue As Decimal Implements IFixedAssetInitialBalanceItemParts.DepreciatedValue
        Get
            Return INDtxtDepreciatedValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtDepreciatedValue.EditValue = value
        End Set
    End Property

    Public Property DepreciationTypeId As Integer? Implements IFixedAssetInitialBalanceItemParts.DepreciationTypeId
        Get
            Return INDsleDepreciationType.EditValue
        End Get
        Set(value As Integer?)
            INDsleDepreciationType.EditValue = value
        End Set
    End Property

    Public Property LegalBookId As Integer? Implements IFixedAssetInitialBalanceItemParts.LegalBookId
        Get
            Return INDsleLegalBook.EditValue
        End Get
        Set(value As Integer?)
            INDsleLegalBook.EditValue = value
        End Set
    End Property

    Public Property LegalBookXpo As XPInstantFeedbackSource Implements IFixedAssetInitialBalanceItemParts.LegalBookXpo
        Get
            Return INDsleLegalBook.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleLegalBook.Properties.DataSource = value
        End Set
    End Property

    Public Property LifeTime As Integer Implements IFixedAssetInitialBalanceItemParts.LifeTime
        Get
            Return INDseLifeUtil.EditValue
        End Get
        Set(value As Integer)
            INDseLifeUtil.EditValue = value
        End Set
    End Property

    Public Property PercentageRescue As Decimal Implements IFixedAssetInitialBalanceItemParts.PercentageRescue
        Get
            Return INDsePercentageRescue.EditValue
        End Get
        Set(value As Decimal)
            INDsePercentageRescue.EditValue = value
        End Set
    End Property

    Public Property TotalProductionUnit As Long Implements IFixedAssetInitialBalanceItemParts.TotalProductionUnit
        Get
            Return INDseTotalProductionUnit.EditValue
        End Get
        Set(value As Long)
            INDseTotalProductionUnit.EditValue = value
        End Set
    End Property

    Public Property UnitLifeTimeId As Integer? Implements IFixedAssetInitialBalanceItemParts.UnitLifeTimeId
        Get
            Return INDsleUnitLifeUtil.EditValue
        End Get
        Set(value As Integer?)
            INDsleUnitLifeUtil.EditValue = value
        End Set
    End Property

    Public Property DepreciatePart As Boolean? Implements IFixedAssetInitialBalanceItemParts.DepreciatePart
        Get
            Return INDsleDepreciatePart.EditValue
        End Get
        Set(value As Boolean?)
            INDsleDepreciatePart.EditValue = value
        End Set
    End Property

    Public Property HistoricalValuePart As Decimal Implements IFixedAssetInitialBalanceItemParts.HistoricalValuePart
        Get
            Return INDtxtHistoricalValuePart.EditValue
        End Get
        Set(value As Decimal)
            INDtxtHistoricalValuePart.EditValue = value
        End Set
    End Property

    Public Property PartAccesoriesConsumableId As Integer? Implements IFixedAssetInitialBalanceItemParts.PartAccesoriesConsumableId
        Get
            Return INDslePartAccesoriesConsumables.EditValue
        End Get
        Set(value As Integer?)
            INDslePartAccesoriesConsumables.EditValue = value
        End Set
    End Property

    Public Property PartAccesoriesConsumableXpo As XPInstantFeedbackSource Implements IFixedAssetInitialBalanceItemParts.PartAccesoriesConsumableXpo
        Get
            Return INDslePartAccesoriesConsumables.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePartAccesoriesConsumables.Properties.DataSource = value
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

    Private _listCompare As List(Of FixedAssetInitialBalanceItemParts)
    Public Property ListCompare As List(Of FixedAssetInitialBalanceItemParts)
        Get
            Return _listCompare
        End Get
        Set(value As List(Of FixedAssetInitialBalanceItemParts))
            _listCompare = value
        End Set
    End Property

    Private _fixedAssetInitialBalanceItemParts As FixedAssetInitialBalanceItemParts
    Public Property FixedAssetInitialBalanceItemParts As FixedAssetInitialBalanceItemParts
        Get
            Return _fixedAssetInitialBalanceItemParts
        End Get
        Set(value As FixedAssetInitialBalanceItemParts)
            _fixedAssetInitialBalanceItemParts = value
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
    Public Event AddFixedAssetInitialBalanceItemPartsEventArgs(sender As Object, e As AddFixedAssetInitialBalanceItemParts)

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        ListUnitLifeUtil = Nothing
        ListDepreciationType = Nothing
        FlagEditModePopupBook = Nothing
        ListFixedAssetInitialBalanceItemPartsDetailBook = Nothing
        FixedAssetInitialBalanceItemPartsDetailBook = Nothing
        ListDeleteFixedAssetInitialBalanceItemPartsDetailBook = Nothing
    End Sub

    Private Sub FrmFixedAssetInitialBalanceItemParts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeTuple()
        Presenter = New PFixedAssetInitialBalanceItemParts(Me)
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True

        IndigoGridControl1.RefreshGrid(INDgcDetailsBook)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(ViewDetailsBook, ListActions)

        If EditMode Then 'Si esta en modo edición
            LoadControls()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDslePartAccesoriesConsumables_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDslePartAccesoriesConsumables.QueryPopUp
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

    Private Sub INDslePartAccesoriesConsumables_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePartAccesoriesConsumables.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmFixedAssetPartsAccesoriesConsumables()
                form.ViewModeEditHold = True
                form.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.6
                form.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.6
                form.StartPosition = Windows.Forms.FormStartPosition.CenterParent
                form.MaximizeBox = False
                form.MinimizeBox = False
                Dim transparent = New FrmTransparent(form, False)
                transparent.ShowDialog()
            End Using
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

#Region "Click"

    Private Sub INDbtnAddParts_Click(sender As Object, e As EventArgs) Handles INDbtnAddParts.Click
        AddParts()
    End Sub

    Private Sub INDbtnAddDetailsBook_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetailsBook.Click
        AddDetailsBook()
    End Sub
    
#End Region

#Region "KeyDown"

    Private Sub FrmFixedAssetInitialBalanceItemParts_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDpceDetailsBook_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceDetailsBook.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.F4 OrElse e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceDetailsBook.ShowPopup()
        End If
    End Sub

    Private Sub INDtxtHistoricalValuePart_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtHistoricalValuePart.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                INDbtnAddParts.Focus()
            Else
                INDpceDetailsBook.Focus()
            End If
        End If
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmFixedAssetInitialBalanceItemParts_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDslePartAccesoriesConsumables.Focus()
    End Sub

#End Region

#Region "Popup"

    Private Sub INDpceDetailsBook_Popup(sender As Object, e As EventArgs) Handles INDpceDetailsBook.Popup
        INDsleLegalBook.Focus()
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

#Region "EditValueChanged"

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

    Private Sub INDsleDepreciatePart_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDepreciatePart.EditValueChanged
        If DepreciatePart IsNot Nothing Then
            If DepreciatePart Then 'Si deprecia muestra el grupo de los libros
                INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else 'Si no deprecia no muestra el grupo de los libros
                INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

    Private Async Sub INDsleLegalBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleLegalBook.EditValueChanged
        If LegalBookId Is Nothing Then Exit Sub

        Dim LegalBook As Infrastructure.Data.Xpo.AccountingRepository.BookXpo = Nothing
        If LegalBookXpo Is Nothing Then
            LegalBook = Await Presenter.GetLegalBookById(LegalBookId)
        Else
            LegalBook = TryCast(TryCast(INDGvLegalBooks.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.AccountingRepository.BookXpo)
        End If

        SetCurrencyUI(LegalBook?.CommonCurrency?.Abbreviation)
    End Sub

    ''' <summary>
    ''' Establece el formato moneda en los controles del PopUp
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat

        Me.INDtxtDepreciatedValue.Properties.Mask.Culture = _culture
    End Sub

#End Region

#Region "CustomColumnDisplayText"

    Private Sub ViewDetailsBook_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles ViewDetailsBook.CustomColumnDisplayText
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

#Region "CloseUp"

    Private Sub INDpceDetailsBook_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceDetailsBook.CloseUp
        If FlagEditModePopupBook Then
            CleanControlsPopup()
        End If
        If e.CloseMode = DevExpress.XtraEditors.PopupCloseMode.Cancel Then
            INDbtnAddParts.Focus()
        End If
    End Sub

#End Region

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

    Private Sub AddParts()
        If ValidateControls() = False Then 'Se validan que los controles esten diligenciados
            Exit Sub
        End If

        If EditMode = False Then 'Esta guardando el item
            If ListCompare IsNot Nothing AndAlso ListCompare.Count > 0 Then 'Se valida que el articulo no se encuentre en el form principal
                Dim cont = (From l In ListCompare Where l.PartAccesoriesConsumablesId = PartAccesoriesConsumableId Select l).Count
                If cont > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "La parte " + INDslePartAccesoriesConsumables.Text + " ya se encuentra en el listado."
                    Exit Sub
                End If
            End If
        End If

        'Se asigna los valores
        AssigningValues()

        'Se crea el objeto que se va a devolver
        Dim args As New AddFixedAssetInitialBalanceItemParts
        args.FixedAssetInitialBalanceItemParts = FixedAssetInitialBalanceItemParts
        args.ListDeleteFixedAssetInitialBalanceItemPartsDetailBook = ListDeleteFixedAssetInitialBalanceItemPartsDetailBook
        args.EditMode = EditMode
        RaiseEvent AddFixedAssetInitialBalanceItemPartsEventArgs(Nothing, args)
        CleanControls()
        INDslePartAccesoriesConsumables.Focus()
    End Sub

    Private Sub AssigningValues()
        If EditMode = False Then 'Si se esta guardando el item se instancia el objeto
            FixedAssetInitialBalanceItemParts = New FixedAssetInitialBalanceItemParts
        End If
        With FixedAssetInitialBalanceItemParts
            .PartAccesoriesConsumablesId = PartAccesoriesConsumableId
            .PartAccesoriesConsumablesCodeName = INDslePartAccesoriesConsumables.Text
            .DepreciatePart = DepreciatePart
            .HistoricalValue = HistoricalValuePart

            If INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then 'Si hay detalles de libros
                .FixedAssetInitialBalanceItemPartsDetailBook.Clear()
                If ListFixedAssetInitialBalanceItemPartsDetailBook IsNot Nothing AndAlso ListFixedAssetInitialBalanceItemPartsDetailBook.Count > 0 Then
                    For Each item In ListFixedAssetInitialBalanceItemPartsDetailBook
                        .FixedAssetInitialBalanceItemPartsDetailBook.Add(item)
                    Next
                End If
            Else 'Si no hay detalles de libros
                If .FixedAssetInitialBalanceItemPartsDetailBook IsNot Nothing AndAlso .FixedAssetInitialBalanceItemPartsDetailBook.Count > 0 Then
                    For Each item In (From l In .FixedAssetInitialBalanceItemPartsDetailBook Where l.Id > 0 Select l).ToList
                        If ListDeleteFixedAssetInitialBalanceItemPartsDetailBook Is Nothing Then
                            ListDeleteFixedAssetInitialBalanceItemPartsDetailBook = New List(Of FixedAssetInitialBalanceItemPartsDetailBook)
                        Else
                            If ListDeleteFixedAssetInitialBalanceItemPartsDetailBook.Contains(item) Then
                                Continue For
                            End If
                        End If
                        ListDeleteFixedAssetInitialBalanceItemPartsDetailBook.Add(item)
                    Next
                    .FixedAssetInitialBalanceItemPartsDetailBook.Clear()
                End If
            End If

        End With
    End Sub

    Private Sub CleanControls()
        FixedAssetInitialBalanceItemParts = Nothing
        EditMode = False
        PartAccesoriesConsumableId = Nothing
        INDslePartAccesoriesConsumables.Properties.NullText = String.Empty
        DepreciatePart = Nothing
        HistoricalValuePart = Nothing
        INDslePartAccesoriesConsumables.Properties.ReadOnly = False
        INDgcDetailsBook.DataSource = Nothing
        ListFixedAssetInitialBalanceItemPartsDetailBook = Nothing
        ListDeleteFixedAssetInitialBalanceItemPartsDetailBook = Nothing
        INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    Private Sub LoadControls()
        With FixedAssetInitialBalanceItemParts
            PartAccesoriesConsumableId = .PartAccesoriesConsumablesId
            INDslePartAccesoriesConsumables.Properties.NullText = .PartAccesoriesConsumablesCodeName
            DepreciatePart = .DepreciatePart
            HistoricalValuePart = .HistoricalValue

            If .DepreciatePart Then
                ListFixedAssetInitialBalanceItemPartsDetailBook = .FixedAssetInitialBalanceItemPartsDetailBook.ToList
                INDgcDetailsBook.DataSource = Nothing
                INDgcDetailsBook.DataSource = ListFixedAssetInitialBalanceItemPartsDetailBook
            End If
        End With
        INDslePartAccesoriesConsumables.Properties.ReadOnly = True
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
            If ListFixedAssetInitialBalanceItemPartsDetailBook Is Nothing Then
                ListFixedAssetInitialBalanceItemPartsDetailBook = New List(Of FixedAssetInitialBalanceItemPartsDetailBook)
            Else
                'Se valida que el libro que se esta agregando no exista en la rejilla
                Dim cont = (From l In ListFixedAssetInitialBalanceItemPartsDetailBook Where l.LegalBookId = LegalBookId Select l).Count
                If cont > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El libro " + INDsleLegalBook.Text + " ya existe en la lista."
                    Exit Sub
                End If
            End If
        End If

        'Se asigna los valores de la entidad
        SetValues()

        If FlagEditModePopupBook = False Then 'Si se esta agregando el registro
            ListFixedAssetInitialBalanceItemPartsDetailBook.Add(FixedAssetInitialBalanceItemPartsDetailBook)
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Si se esta modificando el registro
            Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente."
        End If

        INDgcDetailsBook.DataSource = Nothing
        INDgcDetailsBook.DataSource = ListFixedAssetInitialBalanceItemPartsDetailBook
        CleanControlsPopup()
        INDsleLegalBook.Focus()
    End Sub

    Private Sub CleanControlsPopup()
        FlagEditModePopupBook = False
        LegalBookId = Nothing
        INDsleLegalBook.Properties.NullText = String.Empty
        LifeTime = Nothing
        UnitLifeTimeId = Nothing
        DepreciatedDays = Nothing
        DepreciationTypeId = Nothing
        TotalProductionUnit = Nothing
        PercentageRescue = Nothing
        DepreciatedValue = Nothing
        INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleLegalBook.Properties.ReadOnly = False
    End Sub

    Private Sub SetValues()
        If FlagEditModePopupBook = False Then
            FixedAssetInitialBalanceItemPartsDetailBook = New FixedAssetInitialBalanceItemPartsDetailBook
        End If
        With FixedAssetInitialBalanceItemPartsDetailBook
            .LegalBookId = LegalBookId
            .LegalBookCodeName = INDsleLegalBook.Text
            .LifeTime = LifeTime
            .UnitLifeTime = UnitLifeTimeId
            .DaysPendingDepreciate = 0
            .DepreciatedDays = DepreciatedDays
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
            .DepreciatedValue = DepreciatedValue
            .ResidualValue = .DepreciatedValue
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
        If DepreciatedDays = Nothing OrElse DepreciatedDays = 0 Then
            errors.AppendLine("Debe ingresar los días depreciados.")
        End If
        If DepreciatedValue = Nothing OrElse DepreciatedValue = 0 Then
            errors.AppendLine("Debe ingresar un valor depreciado.")
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

    Private Async Sub EditBook()
        FlagEditModePopupBook = True
        FixedAssetInitialBalanceItemPartsDetailBook = DirectCast(ViewDetailsBook.GetFocusedRow(), FixedAssetInitialBalanceItemPartsDetailBook)
        With FixedAssetInitialBalanceItemPartsDetailBook
            LegalBookId = .LegalBookId
            INDsleLegalBook.Properties.NullText = .LegalBookCodeName
            LifeTime = .LifeTime
            UnitLifeTimeId = .UnitLifeTime
            DepreciatedDays = .DepreciatedDays
            DepreciationTypeId = .DepreciationType
            DepreciatedValue = .DepreciatedValue
            TotalProductionUnit = .TotalProductionUnit
            PercentageRescue = .PercentageRescue
        End With

        Dim LegalBook = Await Presenter.GetLegalBookById(FixedAssetInitialBalanceItemPartsDetailBook.LegalBookId)
        SetCurrencyUI(LegalBook?.CommonCurrency?.Abbreviation)

        INDsleLegalBook.Properties.ReadOnly = True
        INDpceDetailsBook.ShowPopup()
    End Sub

    Private Sub RemoveBook()
        FixedAssetInitialBalanceItemPartsDetailBook = CType(ViewDetailsBook.GetFocusedRow, FixedAssetInitialBalanceItemPartsDetailBook)
        ListFixedAssetInitialBalanceItemPartsDetailBook.Remove(FixedAssetInitialBalanceItemPartsDetailBook)

        If FixedAssetInitialBalanceItemPartsDetailBook.Id > 0 Then
            If ListDeleteFixedAssetInitialBalanceItemPartsDetailBook Is Nothing Then
                ListDeleteFixedAssetInitialBalanceItemPartsDetailBook = New List(Of FixedAssetInitialBalanceItemPartsDetailBook)
            End If
            ListDeleteFixedAssetInitialBalanceItemPartsDetailBook.Add(FixedAssetInitialBalanceItemPartsDetailBook)
        End If

        INDgcDetailsBook.DataSource = Nothing
        INDgcDetailsBook.DataSource = ListFixedAssetInitialBalanceItemPartsDetailBook
    End Sub

#End Region

End Class