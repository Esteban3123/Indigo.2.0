Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Entities
Imports Presentation.FixedAsset.MVP
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository

Public Class FrmFixedAssetRemissionEntranceItemDetail

#Region "Globals"

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

    Dim _MyTag As String

    Dim _IdEquipment As Integer

    Dim _IdEquipmentType As Integer

    ''' <summary>
    ''' bandera para saber que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _editMode As Boolean

    ''' <summary>
    ''' listado del detalla de la remision para validar que los productos no se pepitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listPartAccesoriesConsumablesInput As List(Of FixedAssetRemissionEntranceItemDetailPart)

    Dim PartAccesoriesConsumablesInput As FixedAssetRemissionEntranceItemDetailPart

    Dim ListFixedAssetEquipmentTypePartsAccesoriesConsumibles As List(Of FixedAssetItemTypePartsAccesories)

    Public InputRemissionEquipmentDetail As FixedAssetRemissionEntranceItemDetail

    Dim ListInputRemissionEquipmentDetail As List(Of FixedAssetRemissionEntranceItemDetail)

    Dim ListPartsAccesoriesConsumiblesEquipmentTypeDelete As List(Of FixedAssetRemissionEntranceItemDetailPart)

    Dim ListPartsAccesoriesConsumiblesEquipmentInputRemission As List(Of FixedAssetRemissionEntranceItemDetailPart)

    Dim presenter As PEquipmentEntry


    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetRemissionEntranceItemDetailBook As List(Of FixedAssetRemissionEntranceItemDetailBook)

    ''' <summary>
    ''' Representa a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetRemissionEntranceItemDetailBook As FixedAssetRemissionEntranceItemDetailBook

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetRemissionEntranceItemDetailBook As List(Of FixedAssetRemissionEntranceItemDetailBook)

    ''' <summary>
    ''' Bandera para que de Acuerdo al Tipo de Adquisición me diga si permite depreciar o no
    ''' </summary>
    ''' <remarks></remarks>
    Public FlagDepreciate As Boolean

    ''' <summary>
    ''' Permite saber si se permite cambiar el search
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagChangedSearch As Boolean = True

    Private _getLocationResponsible As Integer
    Public Property GetLocationResponsible As Integer
        Get
            Return _getLocationResponsible
        End Get
        Set(value As Integer)
            _getLocationResponsible = value
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
    Public Event AddEquipmentDetailRemission(sender As Object, e As AddDetailEquipmentRemissionEventArgs)
#End Region

#Region "Properties"

    Public Property LegalBookXpo As XPInstantFeedbackSource
        Get
            Return INDsleLegalBook.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleLegalBook.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ResponsibleXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSlResponsible.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlResponsible.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property StatusXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSlStatus.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlStatus.Properties.DataSource = value
        End Set
    End Property

    Property LocationXPO As XPCollection
        Get
            Return CType(INDSlLocation.Properties.DataSource, XPCollection)
        End Get
        Set(value As XPCollection)
            INDSlLocation.Properties.DataSource = value
        End Set
    End Property

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

    Private _quantityDetails As Integer
    ''' <summary>
    ''' Establece cuantos detalles se van a crear
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property QuantityDetails As Integer
        Get
            Return _quantityDetails
        End Get
        Set(value As Integer)
            _quantityDetails = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer el id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property IdEquipmentType As Integer
        Set(value As Integer)
            _IdEquipmentType = value
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

    ''' <summary>
    ''' propiedad para establecer si se va a editar un registro 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property EditMode As Boolean
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property

#End Region

#Region "Handlers"

#Region "ContextMenu"

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos de la orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.ButtonEdit = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case button.Text
            Case "Eliminar"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        INDpceDetailsBook.Properties.ReadOnly = False
        EditBook()
    End Sub

    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        INDpceDetailsBook.Properties.ReadOnly = False
        EditBook()

    End Sub

#End Region

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListUnitLifeUtil = Nothing
        ListDepreciationType = Nothing
        FlagEditModePopupBook = Nothing
        _MyTag = Nothing
        _IdEquipment = Nothing
        _IdEquipmentType = Nothing
        _editMode = Nothing
        _listPartAccesoriesConsumablesInput = Nothing
        PartAccesoriesConsumablesInput = Nothing
        ListFixedAssetEquipmentTypePartsAccesoriesConsumibles = Nothing
        InputRemissionEquipmentDetail = Nothing
        ListInputRemissionEquipmentDetail = Nothing
        ListPartsAccesoriesConsumiblesEquipmentTypeDelete = Nothing
        ListPartsAccesoriesConsumiblesEquipmentInputRemission = Nothing
        presenter = Nothing
        ListFixedAssetRemissionEntranceItemDetailBook = Nothing
        FixedAssetRemissionEntranceItemDetailBook = Nothing
        ListDeleteFixedAssetRemissionEntranceItemDetailBook = Nothing
        FlagDepreciate = Nothing
        FlagChangedSearch = Nothing
    End Sub


    Private Sub FrmPopUpDetailEquipment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If FlagDepreciate = True Then
            INDSleDeprecia.Enabled = True
        Else
            INDSleDeprecia.Enabled = False
            INDSleDeprecia.EditValue = False
        End If

        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True

        INDDeAdquisitionDate.Properties.MaxValue = Date.Now()
        InitializeTuple()

        AddActionsColumns()

        presenter = New PEquipmentEntry()

        ItemBook(True)

        IndigoGridControl1.RefreshGrid(INDgcDetailsBook)
        Dim ListActionsBook As New List(Of eAcciones)
        'ListActionsBook.Add(eAcciones.Remove)
        ListActionsBook.Add(eAcciones.Edit)

        IndigoGridView2.SetListAcction(ViewDetailsBook, ListActionsBook)


        If GetLocationResponsible = 1 Then 'Si es general se ocultan
            INDLciResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciResponsible.AllowHide = True
            INDLciLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciLocation.AllowHide = True
        Else 'Si es especifico se muestran
            INDLciResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciResponsible.AllowHide = False
            INDLciLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciLocation.AllowHide = False

            If LocationXPO Is Nothing Then
                Using model As New MEquipmentEntry(_MyTag)
                    LocationXPO = model.ListLocationXpo()
                End Using
            End If

        End If

        'Se definen los campos que van en el datasource del control de la barra botones de siguiente siguiente
        Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Placa", .FieldName = "Plate"},
                                      New ColumnInfo With {.Caption = "Serie", .FieldName = "Serie"}}.ToList()

        If _editMode = True Then
            ListInputRemissionEquipmentDetail = Nothing
            LoadControls()
        Else
            CreateCountEntities()
            InputRemissionEquipmentDetail = ListInputRemissionEquipmentDetail(0)
        End If
        FlagChangedSearch = False
        Me.BarraBotones.FilterDataSource = ListInputRemissionEquipmentDetail
        FlagChangedSearch = True
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

#Region "EditValueChanged"

    Private Sub INDsleDeprecia_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleDeprecia.EditValueChanged
        If INDSleDeprecia.EditValue Then 'Si deprecia muestra el grupo de los libros
            INDLcgDepreciationDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            If FlagChangedSearch Then
                If ListFixedAssetRemissionEntranceItemDetailBook Is Nothing OrElse ListFixedAssetRemissionEntranceItemDetailBook.Count = 0 Then
                    ItemBook(False)
                End If
            End If
        Else 'Si no deprecia no muestra el grupo de los libros
            INDLcgDepreciationDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub INDsleDepreciationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDepreciationType.EditValueChanged

        Dim DepreciationTypeId = INDsleDepreciationType.EditValue

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

    Private Sub IndCtrYesNoWarranty_EditValueChanged(sender As Object, e As EventArgs) Handles IndCtrYesNoWarranty.EditValueChanged
        If IndCtrYesNoWarranty.EditValue = True Then
            INDLciWarrantyExpirationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciWarrantyExpirationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Async Sub INDSlResponsible_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlResponsible.EditValueChanged
        Await InitializeFuntionalUnit()
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleLegalBook_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleLegalBook.QueryPopUp
        If LegalBookXpo Is Nothing Then
            LegalBookXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).AccountingService.ListBookByStatus(True)
        End If
    End Sub

    Private Sub INDSlResponsible_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlResponsible.QueryPopUp
        If INDSlResponsible.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If ResponsibleXPO Is Nothing Then
            Using model As New MEquipmentEntry(_MyTag)
                ResponsibleXPO = model.ListResponsible()
            End Using
        End If
    End Sub

    Private Sub INDSlStatus_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlStatus.QueryPopUp
        If INDSlStatus.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If StatusXPO Is Nothing Then
            Using model As New MEquipmentEntry(_MyTag)
                StatusXPO = model.ListStatusAsset()
            End Using
        End If
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmFixedAssetRemissionEntranceItemDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDpceDetailsBook_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceDetailsBook.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.F4 OrElse e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceDetailsBook.ShowPopup()
        ElseIf e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDbtnAddDetail.Focus()
        End If
    End Sub

    Private Sub INDBtnAddParts_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            AddDetail()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAddDetailsBook_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetailsBook.Click
        AddDetailsBook()
    End Sub

    Private Sub INDBtnAddParts_Click(sender As Object, e As EventArgs)
        Try
            AsyncLoader(True)
            INDBtnAddParts.Enabled = False

            '  SetValuesPopUp()

            If _listPartAccesoriesConsumablesInput Is Nothing Then
                'Es nuevo
                _listPartAccesoriesConsumablesInput = New List(Of FixedAssetRemissionEntranceItemDetailPart)
            End If
            _listPartAccesoriesConsumablesInput.Add(PartAccesoriesConsumablesInput)

            INDGcPartsAccesories.DataSource = Nothing
            INDGcPartsAccesories.DataSource = _listPartAccesoriesConsumablesInput

            AsyncLoader(False)
            INDBtnAddParts.Enabled = True
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnAddParts.Enabled = True
            Throw ex
        End Try
    End Sub

    Private Sub INDBtnAddPart_Click(sender As Object, e As EventArgs) Handles INDBtnAddParts.Click
        Using formulario As New FrmFixedAssetRemissionEntranceItemDetailPart
            AddHandler formulario.AddPartsAccesoriesConsumiblesEventArgs, AddressOf ReturnAddRemissionEquipmentParts
            formulario.Size = New Drawing.Size(837, 696)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.MyTag = _MyTag

            'formulario.FlagDepreciate = INDSleDepreciaPartes.EditValue
            formulario.ListFixedAssetPartAccesoriesConsumiblesInputRemission = _listPartAccesoriesConsumablesInput
            'formulario.operatingUnitId = Me.BarraBotones.OperatingUnitValue
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDSlResponsible_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlResponsible.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1711, Nothing, True)
            Using model As New MEquipmentEntry(_MyTag)
                ResponsibleXPO = model.ListResponsible()
            End Using
        End If
    End Sub

    Private Async Sub INDSlFunctinalUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(523, Nothing, True)
            Await InitializeFuntionalUnit()
        End If
    End Sub

    Private Sub INDSlLocation_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(590, Nothing, True)
            Using model As New MEquipmentEntry(_MyTag)
                LocationXPO = model.ListLocationXpo()
            End Using
        End If
    End Sub

    Private Sub INDSlStatus_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlStatus.ButtonClick

    End Sub

#End Region

#End Region

#Region "Methods"

    Private Sub CreateCountEntities()
        ListInputRemissionEquipmentDetail = New List(Of FixedAssetRemissionEntranceItemDetail)
        For i = 1 To QuantityDetails Step 1
            Dim entity As New FixedAssetRemissionEntranceItemDetail
            If ListFixedAssetRemissionEntranceItemDetailBook IsNot Nothing AndAlso ListFixedAssetRemissionEntranceItemDetailBook.Count > 0 Then
                ListFixedAssetRemissionEntranceItemDetailBook.ForEach(Sub(item) entity.FixedAssetRemissionEntranceItemDetailBook.Add(item.Clone))
            End If
            ListInputRemissionEquipmentDetail.Add(entity)
        Next
    End Sub

    Private Sub EditBook()
        FlagEditModePopupBook = True
        FixedAssetRemissionEntranceItemDetailBook = DirectCast(ViewDetailsBook.GetFocusedRow(), FixedAssetRemissionEntranceItemDetailBook)
        With FixedAssetRemissionEntranceItemDetailBook
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

    Private Sub ItemBook(EventShown As Boolean)
        'Se obtiene el articulo por id
        Dim itemXpo = presenter.GetFixedAssetItemById(_IdEquipment, indigo)
        If itemXpo IsNot Nothing AndAlso itemXpo.Count > 0 Then 'Si viene con datos
            'Se pasa a una entidad xpo para que sea mas facil manipularlo
            Dim EntityXpo As FixedAssetEquipmentXpo = itemXpo(0)
            'Se asigna el mismo valor
            'Depreciate = EntityXpo.AllowDepreciate
            If EntityXpo.AllowDepreciate Then 'Permite depreciar
                If EventShown Then
                    'No se bloquea el combo de deprecia
                    INDSleDeprecia.Properties.ReadOnly = False
                End If

                If _editMode = False OrElse EventShown = False Then 'Si esta agregando el detalle
                    'Se crea las entidades que van en la rejilla del libro del articulo de este form
                    If EntityXpo.FixedAssetItemDetailXpo IsNot Nothing AndAlso EntityXpo.FixedAssetItemDetailXpo.Count > 0 Then
                        ListFixedAssetRemissionEntranceItemDetailBook = New List(Of FixedAssetRemissionEntranceItemDetailBook)
                        For Each item As FixedAssetItemDetailXpo In EntityXpo.FixedAssetItemDetailXpo
                            Dim _fixedAssetInitialBalanceItemDetailBook As New FixedAssetRemissionEntranceItemDetailBook
                            With _fixedAssetInitialBalanceItemDetailBook
                                .LegalBookId = item.LegalBookId.Id
                                .LegalBookCodeName = item.LegalBookId.CodeName
                                .LifeTime = item.LifeTime
                                .UnitLifeTime = item.UnitLifeTime
                                .DepreciationType = item.DepreciationType
                                .TotalProductionUnit = item.TotalProductionUnit
                                .PercentageRescue = item.PercentageRescue
                            End With
                            ListFixedAssetRemissionEntranceItemDetailBook.Add(_fixedAssetInitialBalanceItemDetailBook)
                        Next
                        INDgcDetailsBook.DataSource = Nothing
                        INDgcDetailsBook.DataSource = ListFixedAssetRemissionEntranceItemDetailBook
                    End If
                End If

            Else 'No permite depreciar
                If EventShown Then
                    'Se bloquea el combo de deprecia
                    INDSleDeprecia.Properties.ReadOnly = True
                End If
            End If
        End If

        If FlagDepreciate = False Then
            INDSleDeprecia.Properties.ReadOnly = True
            INDSleDeprecia.EditValue = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim listErrors As New StringBuilder
        If INDsleLegalBook.EditValue Is Nothing Then
            listErrors.AppendLine("Debe seleccionar un libro")
        End If
        If INDseLifeUtil.EditValue Is Nothing OrElse INDseLifeUtil.EditValue = 0 Then
            listErrors.AppendLine("Debe ingresar una vida útil")
        End If
        If INDsleUnitLifeUtil.EditValue Is Nothing Then
            listErrors.AppendLine("Debe seleccionar una unida de vida útil")
        End If
        If INDsleDepreciationType.EditValue Is Nothing Then
            listErrors.AppendLine("Debe seleccionar un tipo de depreciación")
        End If
        If INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDseTotalProductionUnit.EditValue Is Nothing OrElse INDseTotalProductionUnit.EditValue = 0 Then
                listErrors.AppendLine("Debe ingresar total unidades producidas")
            End If
        End If
        If INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsePercentageRescue.EditValue Is Nothing OrElse INDsePercentageRescue.EditValue = 0 Then
                listErrors.AppendLine("Debe ingresar un % de salvamento")
            End If
        End If
        Return listErrors.ToString
    End Function

    Private Sub AddDetailsBook()
        Dim errors = ValidateControlsPopup()
        If errors.Length > 0 Then 'Se validan que los controles esten diligenciados
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If FlagEditModePopupBook = False Then 'Si se esta agregando el registro
            If ListFixedAssetRemissionEntranceItemDetailBook Is Nothing Then
                ListFixedAssetRemissionEntranceItemDetailBook = New List(Of FixedAssetRemissionEntranceItemDetailBook)
            Else
                'Se valida que el libro que se esta agregando no exista en la rejilla
                Dim cont = (From l In ListFixedAssetRemissionEntranceItemDetailBook Where l.LegalBookId = INDsleLegalBook.EditValue Select l).Count
                If cont > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El libro " + INDsleLegalBook.Text + " ya existe en la lista."
                    Exit Sub
                End If
            End If
        End If

        'Se asigna los valores de la entidad
        SetValuesDetail()

        If FlagEditModePopupBook = False Then 'Si se esta agregando el registro
            ListFixedAssetRemissionEntranceItemDetailBook.Add(FixedAssetRemissionEntranceItemDetailBook)
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Si se esta modificando el registro
            Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente."
        End If

        INDgcDetailsBook.DataSource = Nothing
        INDgcDetailsBook.DataSource = ListFixedAssetRemissionEntranceItemDetailBook
        CleanControlsPopup()
        INDsleLegalBook.Focus()
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

    Private Sub CleanControlsPopup()
        FlagEditModePopupBook = False
        INDsleLegalBook.EditValue = Nothing
        INDsleLegalBook.Properties.NullText = String.Empty
        INDseLifeUtil.EditValue = 0
        INDsleUnitLifeUtil.EditValue = Nothing
        INDsleDepreciationType.EditValue = Nothing
        INDseTotalProductionUnit.EditValue = 0
        INDsePercentageRescue.EditValue = 0
        INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleLegalBook.Properties.ReadOnly = False
    End Sub

    ''' <summary>
    ''' Inicializa el combo de unidad funcional
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function InitializeFuntionalUnit() As Task

        If INDSlResponsible.EditValue Is Nothing OrElse CInt(INDSlResponsible.EditValue) = 0 Then
            Exit Function
        End If

        Dim FunctionalUnit As List(Of ResponsibleFunctionalUnit)

        Using model As New MEquipmentEntry(_MyTag)
            FunctionalUnit = Await model.GetResponsibleFunctionalUnitAsync(INDSlResponsible.EditValue)

            If FunctionalUnit IsNot Nothing Then
                'INDSlFunctinalUnit.Properties.DataSource = FunctionalUnit
            End If

        End Using
    End Function

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvParts, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvParts.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    Private Sub SetValuesDetail()
        If FlagEditModePopupBook = False Then
            FixedAssetRemissionEntranceItemDetailBook = New FixedAssetRemissionEntranceItemDetailBook
        End If
        With FixedAssetRemissionEntranceItemDetailBook
            .LegalBookId = INDsleLegalBook.EditValue
            .LegalBookCodeName = INDsleLegalBook.Text
            .LifeTime = INDseLifeUtil.EditValue
            .UnitLifeTime = INDsleUnitLifeUtil.EditValue
            .DepreciationType = INDsleDepreciationType.EditValue
            If INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .TotalProductionUnit = INDseTotalProductionUnit.EditValue
            Else
                .TotalProductionUnit = 0
            End If
            If INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .PercentageRescue = INDsePercentageRescue.EditValue
            Else
                .PercentageRescue = 0
            End If

        End With

    End Sub

    Private Sub AddDetail()
        Try
            AsyncLoader(True)
            INDbtnAddDetail.Enabled = False

            SetValues()

            Dim args As New AddDetailEquipmentRemissionEventArgs
            If _editMode = True Then
                If ValidateControls() = False Then
                    AsyncLoader(False)
                    INDbtnAddDetail.Enabled = True
                    Exit Sub
                End If
                args.InputRemissionEquipmentDetail = InputRemissionEquipmentDetail
            Else
                Dim errors = ValidateControlsForms()
                If errors.Length > 0 Then
                    AsyncLoader(False)
                    INDbtnAddDetail.Enabled = True
                    Mensaje(EeventViewerImages.Advertencia) = errors
                    Exit Sub
                End If
                args.ListInputRemissionEquipmentDetail = New List(Of FixedAssetRemissionEntranceItemDetail)
                args.ListInputRemissionEquipmentDetail.AddRange(ListInputRemissionEquipmentDetail)
            End If
            args.EditMode = _editMode

            RaiseEvent AddEquipmentDetailRemission(Nothing, args)

            AsyncLoader(False)
            INDbtnAddDetail.Enabled = True
            CleanControls()
            Me.Close()
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnAddDetail.Enabled = True
            Throw ex
        End Try
    End Sub

    Private Sub INDbtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetail.Click
        AddDetail()
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()

        INDTxtPlaca.EditValue = String.Empty
        INDTxtSerie.EditValue = String.Empty
        INDSlResponsible.EditValue = 0
        'INDSlFunctinalUnit.EditValue = 0
        INDSlLocation.EditValue = 0
        INDDeAdquisitionDate.EditValue = Date.Now()
        INDSleDeprecia.EditValue = 0
        'INDSleDepreciaPartes.EditValue = 0
        INDGcPartsAccesories.DataSource = Nothing

    End Sub

    Private Sub SetValues()
        'If _editMode = False Then
        '    InputRemissionEquipmentDetail = New FixedAssetRemissionEntranceItemDetail
        'End If

        With InputRemissionEquipmentDetail
            .Plate = INDTxtPlaca.EditValue
            .Serie = INDTxtSerie.EditValue

            If INDLciResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ResponsibleId = INDSlResponsible.EditValue
                .NameResponsible = INDSlResponsible.Text
            Else
                .ResponsibleId = Nothing
                .NameResponsible = String.Empty
            End If

            If INDLciLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .LocationId = INDSlLocation.EditValue
                .NameLocation = INDSlLocation.Text
            Else
                .LocationId = Nothing
                .NameLocation = String.Empty
            End If
            
            .AdquisitionDate = INDDeAdquisitionDate.EditValue
            .Depreciate = INDSleDeprecia.EditValue
            .HandlesWarranty = IndCtrYesNoWarranty.EditValue
            .WarrantyExpirationDate = INDDeWarrrantyExpirationDate.EditValue
            .StatusAssetId = INDSlStatus.EditValue
            .NameStatus = INDSlStatus.Text

            If _listPartAccesoriesConsumablesInput IsNot Nothing AndAlso _listPartAccesoriesConsumablesInput.Count > 0 Then
                For Each item In _listPartAccesoriesConsumablesInput
                    .FixedAssetRemissionEntranceItemDetailPart.Add(item)
                Next
            End If

            If ListPartsAccesoriesConsumiblesEquipmentTypeDelete IsNot Nothing AndAlso ListPartsAccesoriesConsumiblesEquipmentTypeDelete.Count > 0 Then
                For Each item In ListPartsAccesoriesConsumiblesEquipmentTypeDelete
                    .FixedAssetRemissionEntranceItemDetailPart.Add(item)
                Next
            End If
        End With
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()

        Dim FixedAssetPartAccesoriesConsumiblesInputRemission = DirectCast(INDGvParts.GetFocusedRow(), FixedAssetRemissionEntranceItemDetailPart)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If FixedAssetPartAccesoriesConsumiblesInputRemission.Id > 0 Then
                If ListPartsAccesoriesConsumiblesEquipmentTypeDelete Is Nothing Then
                    ListPartsAccesoriesConsumiblesEquipmentTypeDelete = New List(Of FixedAssetRemissionEntranceItemDetailPart)
                End If
                ListPartsAccesoriesConsumiblesEquipmentTypeDelete.Add(FixedAssetPartAccesoriesConsumiblesInputRemission)

                FixedAssetPartAccesoriesConsumiblesInputRemission.MarkAsDeleted()
                _listPartAccesoriesConsumablesInput.Remove(FixedAssetPartAccesoriesConsumiblesInputRemission)


                INDGcPartsAccesories.DataSource = Nothing
                INDGcPartsAccesories.DataSource = _listPartAccesoriesConsumablesInput

            Else
                _listPartAccesoriesConsumablesInput.Remove(FixedAssetPartAccesoriesConsumiblesInputRemission)
                INDGcPartsAccesories.DataSource = Nothing
                INDGcPartsAccesories.DataSource = _listPartAccesoriesConsumablesInput
            End If
        End If
    End Sub

    ''' metodo para cargar los controles con la informacion requerida
    ''' </summary>
    ''' <param name="AdministrativeLoanAccountingInformationTmp"></param>
    ''' <remarks></remarks>
    Private Sub LoadControls()
        If _editMode = True Then
            INDbtnAddDetail.Text = ResourceManager.GetString("Edit")
        End If
        With InputRemissionEquipmentDetail
            INDTxtPlaca.EditValue = .Plate
            INDTxtSerie.EditValue = .Serie

            INDSlResponsible.EditValue = .ResponsibleId
            INDSlResponsible.Properties.NullText = .NameResponsible

            INDSlLocation.EditValue = .LocationId

            INDDeAdquisitionDate.EditValue = .AdquisitionDate

            INDSleDeprecia.EditValue = .Depreciate

            IndCtrYesNoWarranty.EditValue = .HandlesWarranty

            INDDeWarrrantyExpirationDate.EditValue = .WarrantyExpirationDate

            INDSlStatus.EditValue = .StatusAssetId
            INDSlStatus.Properties.NullText = .NameStatus

            INDGcPartsAccesories.DataSource = (From l In .FixedAssetRemissionEntranceItemDetailPart Where l.ChangeTracker.State <> ObjectState.Deleted Select l).ToList
            _listPartAccesoriesConsumablesInput = (From l In .FixedAssetRemissionEntranceItemDetailPart Where l.ChangeTracker.State <> ObjectState.Deleted Select l).ToList

            INDgcDetailsBook.DataSource = .FixedAssetRemissionEntranceItemDetailBook
            ListFixedAssetRemissionEntranceItemDetailBook = .FixedAssetRemissionEntranceItemDetailBook.ToList()

        End With
    End Sub

    ''' <summary>
    ''' valida los controles del formualario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsForms() As String
        Dim errors As New StringBuilder
        For Each item In ListInputRemissionEquipmentDetail
            If item.Plate Is Nothing OrElse item.Plate = String.Empty Then
                errors.AppendLine("Debe ingresar una placa del item " + ListInputRemissionEquipmentDetail.IndexOf(item).ToString)
            End If

            If item.Serie Is Nothing OrElse item.Serie = String.Empty Then
                errors.AppendLine("Debe ingresar una serie del item " + ListInputRemissionEquipmentDetail.IndexOf(item).ToString)
            End If

            If INDLciResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If item.ResponsibleId = Nothing OrElse item.ResponsibleId = 0 Then
                    errors.AppendLine("Debe ingresar un responsable del item " + ListInputRemissionEquipmentDetail.IndexOf(item).ToString)
                End If
            End If

            If INDLciLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If item.LocationId = Nothing OrElse item.LocationId = 0 Then
                    errors.AppendLine("Debe ingresar una localización del item " + ListInputRemissionEquipmentDetail.IndexOf(item).ToString)
                End If
            End If

            If item.AdquisitionDate = Nothing Then
                errors.AppendLine("Debe ingresar una fecha de adquisición del item " + ListInputRemissionEquipmentDetail.IndexOf(item).ToString)
            End If

            If item.Depreciate.ToString = String.Empty Then
                errors.AppendLine("Debe ingresar si deprecia del item " + ListInputRemissionEquipmentDetail.IndexOf(item).ToString)
            End If

            If item.HandlesWarranty.ToString = String.Empty Then
                errors.AppendLine("Debe ingresar si maneja garantía del item " + ListInputRemissionEquipmentDetail.IndexOf(item).ToString)
            Else
                If item.HandlesWarranty Then
                    If item.WarrantyExpirationDate Is Nothing Then
                        errors.AppendLine("Debe ingresar una fecha de vencimiento de garantía del item " + ListInputRemissionEquipmentDetail.IndexOf(item).ToString)
                    End If
                End If
            End If
        Next

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' metodo para obtener lo que se retorna del formulario modal de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddRemissionEquipmentParts(sender As Object, e As AddPartsAccesoriesConsumiblesEventArgs)
        If _listPartAccesoriesConsumablesInput Is Nothing Then
            _listPartAccesoriesConsumablesInput = New List(Of FixedAssetRemissionEntranceItemDetailPart)
        End If

        _listPartAccesoriesConsumablesInput.Add(e.PartsAccesoriesConsumiblesInputRemission)

        INDGcPartsAccesories.DataSource = Nothing
        INDGcPartsAccesories.DataSource = _listPartAccesoriesConsumablesInput
        'ctrTmp.PrintInfo()
    End Sub

#End Region

#Region "BarButtons Events"

    ''' <summary>
    ''' Evento para limpiar los valores de los controles al darle click
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Evento del siguiente siguiente de la barra botones
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        If FlagChangedSearch = False Then
            Exit Sub
        End If
        SetValues()
        InputRemissionEquipmentDetail = CType(Record, FixedAssetRemissionEntranceItemDetail)
        LoadControls()
    End Sub

#End Region

End Class