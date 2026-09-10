Imports Presentation.FixedAsset.MVP
Imports Domain.Entities
Imports Presentation.Controls
Imports Presentation.Base
Imports System.Text
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.FixedAssetRepository

Public Class FrmFixedAssetRemissionEntranceItemDetailPart

#Region "Globals"

    ''' <summary>
    ''' Permite saber si el registro esta en modo de edición
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagEditModePopupBook As Boolean = False

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListUnitLifeUtil As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListDepreciationType As New List(Of Tuple(Of Integer, String))

    Dim _ListFixedAssetEquipmentTypePartsAccesoriesConsumibles As List(Of FixedAssetItemTypePartsAccesories)

    Dim _MyTag As String

    Dim _FlagDepreciate As Boolean

    Dim _IdEquipment As Integer

    Dim _ListPartsRemission As List(Of FixedAssetRemissionEntranceItemDetailPart)

    Dim PartAccesoriesConsumablesInput As FixedAssetRemissionEntranceItemDetailPart

    Dim presenter As PEquipmentEntry

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetRemissionEntranceItemDetailPartBook As List(Of FixedAssetRemissionEntranceItemDetailPartBook)

    ''' <summary>
    ''' Representa a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetRemissionEntranceItemDetailPartBook As FixedAssetRemissionEntranceItemDetailPartBook

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetRemissionEntranceItemDetailPartBook As List(Of FixedAssetRemissionEntranceItemDetailPartBook)

    Public Property DepreciatePart As Boolean?
        Get
            Return INDSleDepreciaParte.EditValue
        End Get
        Set(value As Boolean?)
            INDSleDepreciaParte.EditValue = value
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

#End Region

#Region "Events"

    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddPartsAccesoriesConsumiblesEventArgs(sender As Object, e As AddPartsAccesoriesConsumiblesEventArgs)

#End Region

#Region "Properties"


    ''' <summary>
    ''' propiedad para establecer el id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property IdEquipment As Integer
        Set(value As Integer)
            _IdEquipment = value
        End Set
    End Property


    ''' <summary>
    ''' propiedad para establecer el id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListFixedAssetEquipmentTypePartsAccesoriesConsumibles As List(Of FixedAssetItemTypePartsAccesories)
        Set(value As List(Of FixedAssetItemTypePartsAccesories))
            _ListFixedAssetEquipmentTypePartsAccesoriesConsumibles = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer el id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListFixedAssetPartAccesoriesConsumiblesInputRemission As List(Of FixedAssetRemissionEntranceItemDetailPart)
        Set(value As List(Of FixedAssetRemissionEntranceItemDetailPart))
            _ListPartsRemission = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer el id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MyTag As String
        Set(value As String)
            _MyTag = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer el id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property FlagDepreciate As Boolean
        Set(value As Boolean)
            _FlagDepreciate = value
        End Set
    End Property

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

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        FlagEditModePopupBook = Nothing
        ListUnitLifeUtil = Nothing
        ListDepreciationType = Nothing
        _ListFixedAssetEquipmentTypePartsAccesoriesConsumibles = Nothing
        _MyTag = Nothing
        _FlagDepreciate = Nothing
        _IdEquipment = Nothing
        _ListPartsRemission = Nothing
        PartAccesoriesConsumablesInput = Nothing
        presenter = Nothing
        ListFixedAssetRemissionEntranceItemDetailPartBook = Nothing
        FixedAssetRemissionEntranceItemDetailPartBook = Nothing
        ListDeleteFixedAssetRemissionEntranceItemDetailPartBook = Nothing
    End Sub

    Private Sub FrmPopUpPartsAccesoriesConsumiblesRemission_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True

        presenter = New PEquipmentEntry()

        ItemBook()


        IndigoGridControl1.RefreshGrid(INDgcDetailsBook)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(ViewDetailsBook, ListActions)


    End Sub

#End Region

#Region "QueryPopUp"
    Private Sub INDSlParts_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDslPartsAccesoriesConsumibles.QueryPopUp
        If INDslPartsAccesoriesConsumibles.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If _ListFixedAssetEquipmentTypePartsAccesoriesConsumibles Is Nothing Then
            Using model As New MEquipmentEntry(_MyTag)
                INDslPartsAccesoriesConsumibles.Properties.DataSource = model.ListPartsAccesoriesConsumibles()
            End Using
        End If
    End Sub
#End Region

#Region "Click"

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Try
            AsyncLoader(True)
            INDBtnAdd.Enabled = False

            Dim errors = ValidateControlsPopup()
            If errors.Length > 0 Then
                AsyncLoader(False)
                INDBtnAdd.Enabled = True
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If

            SetValue()

            Dim args As New AddPartsAccesoriesConsumiblesEventArgs

            If _ListPartsRemission Is Nothing Then
                _ListPartsRemission = New List(Of FixedAssetRemissionEntranceItemDetailPart)
            End If

            If _ListPartsRemission.Any(Function(x) x.PartAccesoriesConsumiblesId = PartAccesoriesConsumablesInput.PartAccesoriesConsumiblesId) = True Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = "Ya existe esa parte en el listado"
                Exit Sub
            End If


            args.PartsAccesoriesConsumiblesInputRemission = PartAccesoriesConsumablesInput

            RaiseEvent AddPartsAccesoriesConsumiblesEventArgs(Nothing, args)

            AsyncLoader(False)
            INDBtnAdd.Enabled = True
            CleanControls()
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnAdd.Enabled = True
            Throw ex
        End Try
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDslPartsAccesoriesConsumibles_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslPartsAccesoriesConsumibles.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1704, Nothing, True)
            Using model As New MEquipmentEntry(_MyTag)

                INDslPartsAccesoriesConsumibles.Properties.DataSource = (model.ListPartsAccesoriesConsumibles())

            End Using
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' establece los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValue()

        PartAccesoriesConsumablesInput = New FixedAssetRemissionEntranceItemDetailPart

        With PartAccesoriesConsumablesInput
            .PartAccesoriesConsumiblesId = INDslPartsAccesoriesConsumibles.EditValue
            .NamePartsAccesoriesConsumibles = INDslPartsAccesoriesConsumibles.Text
            .Value = CDec(INDTxtValuePart.EditValue)
            .DepreciatePart = INDSleDepreciaParte.EditValue

            If ListFixedAssetRemissionEntranceItemDetailPartBook IsNot Nothing Then
                For Each item In ListFixedAssetRemissionEntranceItemDetailPartBook
                    .FixedAssetRemissionEntranceItemDetailPartBook.Add(item)
                Next
                If ListDeleteFixedAssetRemissionEntranceItemDetailPartBook IsNot Nothing Then
                    For Each item In ListFixedAssetRemissionEntranceItemDetailPartBook
                        .FixedAssetRemissionEntranceItemDetailPartBook.Add(item)
                    Next
                End If
            End If

            If ListDeleteFixedAssetRemissionEntranceItemDetailPartBook IsNot Nothing Then
                For Each item In ListDeleteFixedAssetRemissionEntranceItemDetailPartBook
                    .FixedAssetRemissionEntranceItemDetailPartBook.Add(item)
                Next
                If ListDeleteFixedAssetRemissionEntranceItemDetailPartBook IsNot Nothing Then
                    For Each item In ListFixedAssetRemissionEntranceItemDetailPartBook
                        .FixedAssetRemissionEntranceItemDetailPartBook.Add(item)
                    Next
                End If
            End If



            '.itemi = _IdEquipment
        End With

    End Sub

    Private Sub CleanControls()
        INDslPartsAccesoriesConsumibles.EditValue = 0
        INDTxtValuePart.EditValue = 0
        INDSleDepreciaParte.EditValue = False
        INDslPartsAccesoriesConsumibles.Focus()
    End Sub

    ''' <summary>
    ''' valida los controles del formualario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder

        If INDslPartsAccesoriesConsumibles.Text = String.Empty Then
            errors.AppendLine(INDLciParts.Text + ResourceManager.GetString("Empty"))
        End If

        If _FlagDepreciate = True Then
            If INDSleDepreciaParte.Text = String.Empty Then
                errors.AppendLine(INDLciDepreciate.Text + ResourceManager.GetString("Empty"))
            End If
        Else
            INDSleDepreciaParte.EditValue = False
        End If

        If INDTxtValuePart.Text = String.Empty Then
            errors.AppendLine(INDLciPartValue.Text + ResourceManager.GetString("Empty"))
        End If

        Return errors.ToString()
    End Function

    Private Sub ItemBook()

        'Se obtiene el articulo por id
        Dim itemXpo = presenter.GetFixedAssetItemById(_IdEquipment, indigo)
        If itemXpo IsNot Nothing AndAlso itemXpo.Count > 0 Then 'Si viene con datos
            'Se pasa a una entidad xpo para que sea mas facil manipularlo
            Dim EntityXpo As FixedAssetEquipmentXpo = itemXpo(0)
            'Se asigna el mismo valor
            'Depreciate = EntityXpo.AllowDepreciate
            If EntityXpo.AllowDepreciate Then 'Permite depreciar
                'Si deprecia postulo el mismo valor que trae el articulo y permito cambiar el del form
                'INDsleDepreciate.Properties.ReadOnly = False

                'Se crea las entidades que van en la rejilla del libro del articulo de este form
                If EntityXpo.FixedAssetItemDetailXpo IsNot Nothing AndAlso EntityXpo.FixedAssetItemDetailXpo.Count > 0 Then
                    ListFixedAssetRemissionEntranceItemDetailPartBook = New List(Of FixedAssetRemissionEntranceItemDetailPartBook)
                    For Each item As FixedAssetItemDetailXpo In EntityXpo.FixedAssetItemDetailXpo
                        Dim _fixedAssetInitialBalanceItemDetailBook As New FixedAssetRemissionEntranceItemDetailPartBook
                        With _fixedAssetInitialBalanceItemDetailBook
                            .LegalBookId = item.LegalBookId.Id
                            '.LegalBookCodeName = item.LegalBookId.CodeName
                            .LifeTime = item.LifeTime
                            .UnitLifeTime = item.UnitLifeTime
                            .DepreciationType = item.DepreciationType
                            .TotalProductionUnit = item.TotalProductionUnit
                            .PercentageRescue = item.PercentageRescue
                        End With
                        ListFixedAssetRemissionEntranceItemDetailPartBook.Add(_fixedAssetInitialBalanceItemDetailBook)
                    Next
                    INDgcDetailsBook.DataSource = Nothing
                    INDgcDetailsBook.DataSource = ListFixedAssetRemissionEntranceItemDetailPartBook
                End If

            Else 'No permite depreciar
                'Si no deprecia postulo el mismo valor que trae el articulo y no permito cambiar el del form
                'INDsleDepreciate.Properties.ReadOnly = True
            End If
        End If

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

#End Region

    Private Sub INDSleDepreciaParte_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleDepreciaParte.EditValueChanged
        If DepreciatePart IsNot Nothing Then
            If DepreciatePart Then 'Si deprecia muestra el grupo de los libros
                INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else 'Si no deprecia no muestra el grupo de los libros
                INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

    Private Async Sub INDslPartsAccesoriesConsumibles_EditValueChanged(sender As Object, e As EventArgs) Handles INDslPartsAccesoriesConsumibles.EditValueChanged
        Dim CodeParts = INDslPartsAccesoriesConsumibles.Properties.View.GetFocusedRowCellValue("Code")
        Dim Parts As FixedAssetPartsAccesoriesConsumables
        Using model As New MEquipmentEntry(_MyTag)
            AsyncLoader(True)
            Parts = Await model.GetPartsAccesoriesConsumablesByCodeAsync(CodeParts)
            AsyncLoader(False)
        End Using

        If Parts IsNot Nothing Then
            DepreciatePart = Parts.AllowDepreciate

            If DepreciatePart Then

                If Parts.FixedAssetPartsAccesoriesConsumablesDetail IsNot Nothing Then
                    ListFixedAssetRemissionEntranceItemDetailPartBook = New List(Of FixedAssetRemissionEntranceItemDetailPartBook)
                    For Each item As FixedAssetPartsAccesoriesConsumablesDetail In Parts.FixedAssetPartsAccesoriesConsumablesDetail
                        Dim _FixedAssetRemissionEntranceItemDetailPartBook As New FixedAssetRemissionEntranceItemDetailPartBook
                        With _FixedAssetRemissionEntranceItemDetailPartBook
                            .LegalBookId = item.LegalBookId
                            .LegalBookCodeName = item.CodeNameLegalBook
                            .LifeTime = item.LifeTime
                            .UnitLifeTime = item.UnitLifeTime
                            .DepreciationType = item.DepreciationType
                            .TotalProductionUnit = item.TotalProductionUnit
                            .PercentageRescue = item.PercentageRescue
                        End With
                        ListFixedAssetRemissionEntranceItemDetailPartBook.Add(_FixedAssetRemissionEntranceItemDetailPartBook)
                    Next

                    INDgcDetailsBook.DataSource = Nothing
                    INDgcDetailsBook.DataSource = ListFixedAssetRemissionEntranceItemDetailPartBook


                End If
                INDSleDepreciaParte.ReadOnly = False
            Else
                INDSleDepreciaParte.ReadOnly = True
            End If
        End If



    End Sub
    Private Sub EditBook()
        FlagEditModePopupBook = True
        FixedAssetRemissionEntranceItemDetailPartBook = DirectCast(ViewDetailsBook.GetFocusedRow(), FixedAssetRemissionEntranceItemDetailPartBook)
        With FixedAssetRemissionEntranceItemDetailPartBook
            INDsleLegalBook.EditValue = .LegalBookId
            INDsleLegalBook.Properties.NullText = .LegalBookCodeName
            INDseLifeUtil.EditValue = .LifeTime
            INDsleUnitLifeUtil.EditValue = .UnitLifeTime
            INDsleDepreciationType.EditValue = .DepreciationType
            INDseTotalProductionUnit.EditValue = .TotalProductionUnit
            INDsePercentageRescue.EditValue = .PercentageRescue
        End With
        INDsleLegalBook.Properties.ReadOnly = True
        INDpceDetailsBook.ShowPopup()
    End Sub


    Private Sub INDbtnAddDetailsBook_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetailsBook.Click

    End Sub

#Region "MenuContext"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.ButtonEdit
        btn = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case btn.Text.ToString
            Case "Editar"
                EditBook()
        End Select
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.text.ToString)
            Case "Editar"
                EditBook()
        End Select
    End Sub

#End Region

    Private Sub FrmFixedAssetRemissionEntranceItemDetailPart_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

End Class