'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/10/2020
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
Imports DevExpress.XtraLayout
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmStabilityTableDetail

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddStabilityTableDetailArgs(sender As Object, e As AddStabilityTableDetail)

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PStabilityTable

    ''' <summary>
    ''' Listado de reconstitución
    ''' </summary>
    Private ListStabilityTableDetailReconstitution As List(Of StabilityTableDetailReconstitution)

    ''' <summary>
    ''' Listado de reconstitución
    ''' </summary>
    Private ListDeleteStabilityTableDetailReconstitution As List(Of StabilityTableDetailReconstitution)

    ''' <summary>
    ''' Listado de dilución
    ''' </summary>
    Private ListStabilityTableDetailDilution As List(Of StabilityTableDetailDilution)

    ''' <summary>
    ''' Listado de dilución
    ''' </summary>
    Private ListDeleteStabilityTableDetailDilution As List(Of StabilityTableDetailDilution)

    ''' <summary>
    ''' Control de validación
    ''' </summary>
    Private controlValidate As System.Windows.Forms.Control

    ''' <summary>
    ''' Permite saber si se esta modificando
    ''' </summary>
    Private EditModePopup As Boolean = False

    ''' <summary>
    ''' Permite saber si se esta editando el detalle
    ''' </summary>
    Public EditModeDetail As Boolean

    ''' <summary>
    ''' Entidad de reconstitución
    ''' </summary>
    Private StabilityTableDetailReconstitution As StabilityTableDetailReconstitution

    ''' <summary>
    ''' Entidad de reconstitución
    ''' </summary>
    Private StabilityTableDetailDilution As StabilityTableDetailDilution

    ''' <summary>
    ''' Entidad de detalle de estabilidad
    ''' </summary>
    Public StabilityTableDetail As StabilityTableDetail

    ''' <summary>
    ''' Index del listado, sirve para eliminar el registro en el listado de comparación
    ''' </summary>
    Private IndexOfEditPopup As Integer? = Nothing

    ''' <summary>
    ''' Se utiliza para validar
    ''' </summary>
    Public ListStabilityTableDetailCompare As List(Of StabilityTableDetail)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmStabilityTableDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Visible = False
        Presenter = New PStabilityTable()

        InitializeTuples()
        InitializeActions()

        If EditModeDetail Then
            LoadControls()
        End If
    End Sub

    ''' <summary>
    ''' Inicializa las acciones de las rejillas del PopUp
    ''' </summary>
    Private Sub InitializeActions()

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDviewReconstitution, ListActions)
        IndigoGridView2.SetListAcction(INDviewDilution, ListActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewReconstitution.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewDilution.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next

    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el form de medicamentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleATC_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleATC.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(343, Nothing, True)
            INDsleATC.Properties.DataSource = Presenter.InitializeATC()
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de medicamentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleATCReconstitution_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleATCReconstitution.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(343, Nothing, True)
            INDsleATCReconstitution.Properties.DataSource = Presenter.InitializeATCDiluent()
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProduct_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProduct.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(304, Nothing, True)
            If INDsleATC.EditValue IsNot Nothing Then
                INDsleProduct.Properties.DataSource = Presenter.InitializeProduct(INDsleATC.EditValue)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de tipo de dosis unitaria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleUnitTypeDoses_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleUnitTypeDoses.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2067, Nothing, True)
            INDsleUnitTypeDoses.Properties.DataSource = Presenter.InitializeUnitDoseType(INDsleATC.EditValue)
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Se dispara al desplegar el control de medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleATC_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleATC.QueryPopUp
        If INDsleATC.Properties.DataSource Is Nothing Then
            INDsleATC.Properties.DataSource = Presenter.InitializeATC()
        End If
    End Sub

    ''' <summary>
    ''' Se dispara al desplegar el control de medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleATCReconstitution_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleATCReconstitution.QueryPopUp
        If INDsleATCReconstitution.Properties.DataSource Is Nothing Then
            INDsleATCReconstitution.Properties.DataSource = Presenter.InitializeATCDiluent()
        End If
    End Sub

    ''' <summary>
    ''' Se dispara al desplegar el control de medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleATCDilution_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleATCDilution.ButtonClick
        If INDsleATCDilution.Properties.DataSource Is Nothing Then
            INDsleATCDilution.Properties.DataSource = Presenter.InitializeATCDiluent()
        End If
    End Sub

    ''' <summary>
    ''' Se dispara al desplegar el control de medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleATCDilution_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleATCDilution.QueryPopUp
        If INDsleATCDilution.Properties.DataSource Is Nothing Then
            INDsleATCDilution.Properties.DataSource = Presenter.InitializeATCDiluent()
        End If
    End Sub

    ''' <summary>
    ''' Se dispara al deslpegar el control de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProduct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProduct.QueryPopUp
        If INDsleProduct.Properties.DataSource Is Nothing AndAlso INDsleATC.EditValue IsNot Nothing Then
            INDsleProduct.Properties.DataSource = Presenter.InitializeProduct(INDsleATC.EditValue)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleATC_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleATC.EditValueChanged

        INDsleUnitTypeDoses.Properties.DataSource = Nothing
        INDsleUnitTypeDoses.EditValue = Nothing

        If INDsleATC.EditValue IsNot Nothing Then
            INDsleProduct.EditValue = Nothing
            INDsleProduct.Properties.DataSource = Nothing
            INDsleProduct.Properties.NullText = String.Empty

            INDsleUnitTypeDoses.Properties.DataSource = Await Presenter.InitializeUnitDoseType(INDsleATC.EditValue)

            Dim Result = TryCast(INDsleUnitTypeDoses.Properties.DataSource, List(Of Infrastructure.Data.Xpo.MixingStationRepository.MixinStationUnitDoseTypeXpo))
            If Result?.Any() Then
                If Result?.Count = 1 Then
                    INDsleUnitTypeDoses.EditValue = Result.FirstOrDefault().Id
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = $"El medicamento ({INDsleATC.Text}) no esta parametrizado en 'Medicamentos para producción'"
            End If
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Se dispara al presionar f4 o enter sobre el pce de reconstitución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceReconstitution_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceReconstitution.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.F4 OrElse e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceReconstitution.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Se dispara al presionar f4 o enter sobre el pce de dilución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceDilution_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceDilution.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.F4 OrElse e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceDilution.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Para cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmStabilityTableDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Se dispara al desplegar el pce de reconstitución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceReconstitution_Popup(sender As Object, e As EventArgs) Handles INDpceReconstitution.Popup
        INDsleATCReconstitution.Focus()
    End Sub

    ''' <summary>
    ''' Se dispara al desplegar el pce de dilución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceDilution_Popup(sender As Object, e As EventArgs) Handles INDpceDilution.Popup
        INDsleATCDilution.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Agrega una dilución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddDilution_Click(sender As Object, e As EventArgs) Handles INDbtnAddDilution.Click
        Dim res = ValidateField(INDlyDilution)
        If Not res.ResultStatus Then
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), res.ToString())
            Exit Sub
        End If

        If ListStabilityTableDetailDilution Is Nothing Then
            ListStabilityTableDetailDilution = New List(Of StabilityTableDetailDilution)
        End If

        'Listado para comparar los rangos
        Dim ListComparePopup As New List(Of StabilityTableDetailDilution)
        ListStabilityTableDetailDilution.ForEach(Sub(item) ListComparePopup.Add(item.CloneEntity()))

        'Si se va a actualizar, se quita del listado de comparación el item que se esta actualizando
        If EditModePopup Then
            ListComparePopup.RemoveAt(IndexOfEditPopup)
        End If

        If (From x In ListComparePopup Where x.ATCId = INDsleATCDilution.EditValue).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El medicamento seleccionado ya existe en la lista"
            Exit Sub
        End If

        If EditModePopup = False Then
            StabilityTableDetailDilution = New StabilityTableDetailDilution
        End If

        With StabilityTableDetailDilution
            .ATCId = INDsleATCDilution.EditValue
            .ATCCodeName = INDsleATCDilution.Text
            .ConcentrationMaximum = INDseConcentrationMaximum.EditValue
            .ConcentrationMinimum = INDseConcentrationMinimum.EditValue
            .Container = INDtxtContainer.EditValue
            .PhotoProtection = INDslePhotoProtectionDilution.EditValue
            .InfusionTime = INDseInfusionTime.EditValue
            .BibliographicReference = INDmemoBibliographicReference.EditValue
            .HourStability = INDseHourStabilityDilution.EditValue
            .StorageTemperatureId = INDsleStoredDilution.EditValue
            .StorageTemperatureCodeName = INDsleStoredDilution.Text
        End With

        If EditModePopup = False Then
            ListStabilityTableDetailDilution.Add(StabilityTableDetailDilution)
        End If

        INDgcDilution.DataSource = Nothing
        INDgcDilution.DataSource = ListStabilityTableDetailDilution
        If EditModePopup = False Then
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
        Else
            Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente"
        End If
        CleanControlsPopupDilution()
        INDsleATCDilution.Focus()
    End Sub

    ''' <summary>
    ''' Agrega una reconstitución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddReconstitution_Click(sender As Object, e As EventArgs) Handles INDbtnAddReconstitution.Click
        Dim res = ValidateField(INDlyReconstitution)
        If Not res.ResultStatus Then
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), res.ToString())
            Exit Sub
        End If

        If ListStabilityTableDetailReconstitution Is Nothing Then
            ListStabilityTableDetailReconstitution = New List(Of StabilityTableDetailReconstitution)
        End If

        'Listado para comparar los rangos
        Dim ListComparePopup As New List(Of StabilityTableDetailReconstitution)
        ListStabilityTableDetailReconstitution.ForEach(Sub(item) ListComparePopup.Add(item.CloneEntity()))

        'Si se va a actualizar, se quita del listado de comparación el item que se esta actualizando
        If EditModePopup Then
            ListComparePopup.RemoveAt(IndexOfEditPopup)
        End If

        If (From x In ListComparePopup Where x.ATCId = INDsleATCReconstitution.EditValue).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El medicamento seleccionado ya existe en la lista"
            Exit Sub
        End If

        If EditModePopup = False Then
            StabilityTableDetailReconstitution = New StabilityTableDetailReconstitution
        End If
        With StabilityTableDetailReconstitution
            .ATCId = INDsleATCReconstitution.EditValue
            .ATCCodeName = INDsleATCReconstitution.Text
            .Volume = INDseVolumeReconstitution.EditValue
            .StorageTemperatureId = INDsleStoredReconstitution.EditValue
            .StorageTemperatureCodeName = INDsleStoredReconstitution.Text
            .HourStability = INDseHourStability.EditValue
            .Observations = INDSeObservationsReconstitution.EditValue
        End With

        If EditModePopup = False Then
            ListStabilityTableDetailReconstitution.Add(StabilityTableDetailReconstitution)
        End If

        INDgcReconstitution.DataSource = Nothing
        INDgcReconstitution.DataSource = ListStabilityTableDetailReconstitution
        If EditModePopup = False Then
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
        Else
            Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente"
        End If
        CleanControlsPopupReconstitution()
        INDsleATCReconstitution.Focus()
    End Sub

    ''' <summary>
    ''' Agrega el detalle al form principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetail.Click
        Dim res = ValidateField(INDlyRoot)
        If Not res.ResultStatus Then
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), res.ToString())
            Exit Sub
        End If

        'Se valida que el rias que se va a agregar no exista en el formulario principal
        If ListStabilityTableDetailCompare IsNot Nothing AndAlso ListStabilityTableDetailCompare.Count > 0 Then
            If (From x In ListStabilityTableDetailCompare Where x.ATCId = INDsleATC.EditValue AndAlso x.ProductId = INDsleProduct.EditValue Select x).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El medicamento seleccionado ya existe en la rejilla principal"
                INDsleATC.Focus()
                Exit Sub
            End If
        End If

        If EditModeDetail = False Then
            StabilityTableDetail = New StabilityTableDetail
        End If
        With StabilityTableDetail
            .ATCId = INDsleATC.EditValue
            .ATCCodeName = INDsleATC.Text
            .ProductId = INDsleProduct.EditValue
            .ProductCodeName = INDsleProduct.Text
            .HourStabilityProduct = CInt(INDseStabilityHourProduct.EditValue)
            .UnitDoseTypeId = INDsleUnitTypeDoses.EditValue
            .UnitDoseTypeCodeName = INDsleUnitTypeDoses.Text
            .AllowableDoses = INDseAllowableDoses.EditValue
            .Observations = INDmemoObservations.EditValue

            .StabilityTableDetailDilution.Clear()
            .StabilityTableDetailReconstitution.Clear()

            If ListStabilityTableDetailReconstitution?.Any() Then
                ListStabilityTableDetailReconstitution.ForEach(Sub(x) .StabilityTableDetailReconstitution.Add(x))
            End If

            If ListDeleteStabilityTableDetailReconstitution?.Any() Then
                ListDeleteStabilityTableDetailReconstitution.ForEach(Sub(x) .StabilityTableDetailReconstitution.Add(x.MarkAsDeleted()))
            End If

            If ListStabilityTableDetailDilution?.Any() Then
                ListStabilityTableDetailDilution.ForEach(Sub(x) .StabilityTableDetailDilution.Add(x))
            End If

            If ListDeleteStabilityTableDetailDilution?.Any() Then
                ListDeleteStabilityTableDetailDilution.ForEach(Sub(x) .StabilityTableDetailDilution.Add(x.MarkAsDeleted()))
            End If
        End With

        Dim args As New AddStabilityTableDetail
        args.StabilityTableDetail = StabilityTableDetail
        args.EditMode = EditModeDetail
        RaiseEvent AddStabilityTableDetailArgs(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "ContextMenu"

    ''' <summary>
    ''' Rejilla reconstitución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditReconstitution()
            Case "Remove"
                DeleteReconstitution()
        End Select
    End Sub

    ''' <summary>
    ''' Rejilla reconstitución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditReconstitution()
            Case "Remove"
                DeleteReconstitution()
        End Select
    End Sub

    ''' <summary>
    ''' Rejilla dilución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditDilution()
            Case "Remove"
                DeleteDilution()
        End Select
    End Sub

    ''' <summary>
    ''' Rejilla dilución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDilution()
            Case "Remove"
                DeleteDilution()
        End Select
    End Sub

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Cuando se cierra el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceReconstitution_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceReconstitution.CloseUp
        If EditModePopup Then
            CleanControlsPopupReconstitution()
        End If
    End Sub

    ''' <summary>
    ''' Cuando se cierra el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceDilution_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceDilution.CloseUp
        If EditModePopup Then
            CleanControlsPopupDilution()
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los controles cuando se esta editando
    ''' </summary>
    Private Sub LoadControls()
        With StabilityTableDetail

            INDsleATC.EditValue = .ATCId
            INDsleATC.Properties.NullText = .ATCCodeName
            INDsleProduct.EditValue = .ProductId
            INDsleProduct.Properties.NullText = .ProductCodeName
            INDseStabilityHourProduct.EditValue = .HourStabilityProduct
            INDsleUnitTypeDoses.EditValue = .UnitDoseTypeId
            INDsleUnitTypeDoses.Properties.NullText = .UnitDoseTypeCodeName
            INDseAllowableDoses.EditValue = .AllowableDoses
            INDmemoObservations.EditValue = .Observations

            ListStabilityTableDetailReconstitution = (From x In .StabilityTableDetailReconstitution Where x.ChangeTracker.State <> ObjectState.Deleted Select x).ToList()
            INDgcReconstitution.DataSource = Nothing
            INDgcReconstitution.DataSource = ListStabilityTableDetailReconstitution

            ListStabilityTableDetailDilution = (From x In .StabilityTableDetailDilution Where x.ChangeTracker.State <> ObjectState.Deleted Select x).ToList()
            INDgcDilution.DataSource = Nothing
            INDgcDilution.DataSource = ListStabilityTableDetailDilution
        End With
    End Sub

    ''' <summary>
    ''' Edita una regla
    ''' </summary>
    Private Sub EditReconstitution()
        EditModePopup = True
        StabilityTableDetailReconstitution = DirectCast(INDviewReconstitution.GetFocusedRow(), StabilityTableDetailReconstitution)
        IndexOfEditPopup = ListStabilityTableDetailReconstitution.IndexOf(StabilityTableDetailReconstitution)

        With StabilityTableDetailReconstitution
            INDsleATCReconstitution.EditValue = .ATCId
            INDsleATCReconstitution.Properties.NullText = .ATCCodeName
            INDseVolumeReconstitution.EditValue = .Volume
            INDsleStoredReconstitution.EditValue = .StorageTemperatureId
            INDsleStoredReconstitution.Properties.NullText = .StorageTemperatureCodeName
            INDseHourStability.EditValue = .HourStability
            INDSeObservationsReconstitution.EditValue = .Observations
        End With

        INDpceReconstitution.ShowPopup()
    End Sub

    ''' <summary>
    ''' Edita una regla
    ''' </summary>
    Private Sub EditDilution()
        EditModePopup = True
        StabilityTableDetailDilution = DirectCast(INDviewDilution.GetFocusedRow(), StabilityTableDetailDilution)
        IndexOfEditPopup = ListStabilityTableDetailDilution.IndexOf(StabilityTableDetailDilution)

        With StabilityTableDetailDilution
            INDsleATCDilution.EditValue = .ATCId
            INDsleATCDilution.Properties.NullText = .ATCCodeName
            INDseConcentrationMaximum.EditValue = .ConcentrationMaximum
            INDseConcentrationMinimum.EditValue = .ConcentrationMinimum
            INDtxtContainer.EditValue = .Container
            INDsleStoredDilution.EditValue = .StorageTemperatureId
            INDsleStoredDilution.Properties.NullText = .StorageTemperatureCodeName
            INDslePhotoProtectionDilution.EditValue = .PhotoProtection
            INDseInfusionTime.EditValue = .InfusionTime
            INDseHourStabilityDilution.EditValue = .HourStability
            INDmemoBibliographicReference.EditValue = .BibliographicReference
        End With

        INDpceDilution.ShowPopup()
    End Sub

    ''' <summary>
    ''' Elimina una regla
    ''' </summary>
    Private Sub DeleteReconstitution()
        Dim entityDelete = DirectCast(INDviewReconstitution.GetFocusedRow(), StabilityTableDetailReconstitution)
        If entityDelete.Id > 0 Then
            If ListDeleteStabilityTableDetailReconstitution Is Nothing Then
                ListDeleteStabilityTableDetailReconstitution = New List(Of StabilityTableDetailReconstitution)
            End If
            ListDeleteStabilityTableDetailReconstitution.Add(entityDelete)
        End If

        ListStabilityTableDetailReconstitution.Remove(entityDelete)
        INDgcReconstitution.DataSource = Nothing
        INDgcReconstitution.DataSource = ListStabilityTableDetailReconstitution
        Mensaje(EeventViewerImages.Informacion) = "Registro eliminado de la rejilla correctamente"
    End Sub

    ''' <summary>
    ''' Elimina una regla
    ''' </summary>
    Private Sub DeleteDilution()
        Dim entityDelete = DirectCast(INDviewDilution.GetFocusedRow(), StabilityTableDetailDilution)
        If entityDelete.Id > 0 Then
            If ListDeleteStabilityTableDetailDilution Is Nothing Then
                ListDeleteStabilityTableDetailDilution = New List(Of StabilityTableDetailDilution)
            End If
            ListDeleteStabilityTableDetailDilution.Add(entityDelete)
        End If

        ListStabilityTableDetailDilution.Remove(entityDelete)
        INDgcDilution.DataSource = Nothing
        INDgcDilution.DataSource = ListStabilityTableDetailDilution
        Mensaje(EeventViewerImages.Informacion) = "Registro eliminado de la rejilla correctamente"
    End Sub

    ''' <summary>
    ''' Limpia los controles del popup de reconstitución
    ''' </summary>
    Private Sub CleanControlsPopupReconstitution()
        INDsleATCReconstitution.EditValue = Nothing
        INDsleATCReconstitution.Properties.NullText = String.Empty
        INDseVolumeReconstitution.EditValue = Nothing
        INDseHourStability.EditValue = Nothing
        INDSeObservationsReconstitution.EditValue = String.Empty
        INDsleStoredReconstitution.EditValue = Nothing
        INDsleStoredReconstitution.Properties.NullText = String.Empty
        EditModePopup = False
        StabilityTableDetailReconstitution = Nothing
        IndexOfEditPopup = Nothing
    End Sub

    ''' <summary>
    ''' Limpia los controles del popup de reconstitución
    ''' </summary>
    Private Sub CleanControlsPopupDilution()
        INDsleATCDilution.EditValue = Nothing
        INDsleATCDilution.Properties.NullText = String.Empty
        INDseConcentrationMaximum.EditValue = Nothing
        INDseConcentrationMinimum.EditValue = Nothing
        INDsleStoredDilution.EditValue = Nothing
        INDsleStoredDilution.Properties.NullText = String.Empty
        INDslePhotoProtectionDilution.EditValue = Nothing
        INDseInfusionTime.EditValue = Nothing
        INDmemoBibliographicReference.EditValue = Nothing
        INDtxtContainer.EditValue = Nothing
        INDseHourStabilityDilution.EditValue = Nothing
        EditModePopup = False
        StabilityTableDetailDilution = Nothing
        IndexOfEditPopup = Nothing
    End Sub

    ''' <summary>
    ''' Inicializa los combos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim listYesNot = New List(Of Tuple(Of Boolean, String))
        listYesNot.Add(New Tuple(Of Boolean, String)(True, "Si"))
        listYesNot.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDsleStoredReconstitution.Properties.DataSource = listYesNot

        INDsleStoredDilution.Properties.DataSource = listYesNot
        INDslePhotoProtectionDilution.Properties.DataSource = listYesNot
        INDsleStoredDilution.Properties.DataSource = Presenter.InitializeStorageTemperature()
        INDsleStoredReconstitution.Properties.DataSource = Presenter.InitializeStorageTemperature()
    End Sub

    ''' <summary>
    ''' Obtiene un valor que indica si el tipo pasado tiene como base
    ''' un control BaseEdit
    ''' </summary>
    ''' <param name="type">Tipo pasado a consultar</param>
    Private Function GetIsBaseEditBaseType(ByVal type As Type) As Boolean
        Dim result As Boolean = False
        IsBaseEditBaseType(type, result)
        Return result
    End Function

    Private Sub IsBaseEditBaseType(ByVal type As Type, ByRef result As Boolean)
        If type.BaseType.Equals(GetType(DevExpress.XtraEditors.BaseEdit)) Then
            result = True
        Else
            If Not type.BaseType.Equals(GetType(Object)) Then
                IsBaseEditBaseType(type.BaseType, result)
            Else
                result = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Valida los controles de un layout
    ''' </summary>
    ''' <param name="layoutControl"></param>
    ''' <returns></returns>
    Public Function ValidateField(layoutControl As LayoutControl) As ValidateResult
        Dim result As New ValidateResult()
        Dim refCtr As System.Windows.Forms.Control = Nothing
        'Recorremos los LayoutControlItem
        For Each item As BaseLayoutItem In (From i As BaseLayoutItem In layoutControl.Items Where i.GetType().Equals(GetType(LayoutControlItem)) Select i).ToList()
            Dim it As LayoutControlItem = DirectCast(item, LayoutControlItem)
            Dim nn As String = it.Name
            If TypeOf it.Control Is IValidable OrElse Me.GetIsBaseEditBaseType(it.Control.GetType()) Then
                Dim ctr As Object = it.Control
                If Not it.ShowInCustomizationForm OrElse Not it.AllowHide Then
                    If ctr.EditValue IsNot Nothing Then
                        If ctr.EditValue.GetType().Equals(GetType(String)) AndAlso ctr.EditValue.ToString().Trim().Equals(String.Empty) Then
                            If result.EmptyFieldNames.Count = 0 Then
                                refCtr = ctr
                            End If
                            result.EmptyFieldNames.Add(it.Text)
                        End If
                    Else
                        If result.EmptyFieldNames.Count = 0 Then
                            refCtr = ctr
                        End If
                        result.EmptyFieldNames.Add(it.Text)
                    End If
                End If
            End If
        Next
        If refCtr IsNot Nothing Then
            controlValidate = refCtr
            refCtr.Focus()
        End If
        result.ResultStatus = (result.EmptyFieldNames.Count = 0)
        Return result
    End Function

#End Region

End Class

Public Class AddStabilityTableDetail
    Inherits EventArgs

    Property StabilityTableDetail As StabilityTableDetail

    Property EditMode As Boolean

End Class