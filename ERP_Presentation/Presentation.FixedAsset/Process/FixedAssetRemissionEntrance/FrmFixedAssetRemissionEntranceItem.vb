Imports Presentation.Base
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Presentation.FixedAsset.MVP
Imports Domain.Entities
Imports System.Drawing
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities

Public Class FrmFixedAssetRemissionEntranceItem

#Region "Events"

    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddEquipmentRemissionEventArgs(sender As Object, e As AddEquipmentRemissionEventArgs)

#End Region

#Region "Globals"

    ''' <summary>
    ''' detalle de la remision
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public InputRemissionEquipment As FixedAssetRemissionEntranceItem

    ''' <summary>
    ''' detalle de la remision
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public InputRemissionEquipmentEdit As FixedAssetRemissionEntranceItem

    ''' <summary>
    ''' listado del detalla de la remision para validar que los productos no se pepitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listInputRemissionEquipmentValidation As List(Of FixedAssetRemissionEntranceItem)

    ''' <summary>
    ''' bandera para saber que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _editMode As Boolean

    Dim listInputRemissionEquipmentDetail As List(Of FixedAssetRemissionEntranceItemDetail)

    Dim listInputRemissionEquipmentDetailDelete As List(Of FixedAssetRemissionEntranceItemDetail)

    Dim InputRemissionEquipmentDetail As FixedAssetRemissionEntranceItemDetail
    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim indexEditRecord As Integer

    Dim EquipmentTypeId As Integer

    ''' <summary>
    ''' Tag del form
    ''' </summary>
    ''' <remarks></remarks>
    Dim _MyTag As String

    ''' <summary>
    ''' Obtiene el IVA
    ''' </summary>
    ''' <remarks></remarks>
    Dim ObjIva As Decimal

    ''' <summary>
    ''' Permite saber si se puede cambiar el valor del search
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagChangedSearch As Boolean = True

    ''' <summary>
    ''' True = Cambia, False = No cambia
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagChangedItemBarButtons As Boolean = True

    ''' <summary>
    ''' Variable que toma el Tipo de Adquisición para las Validaciones Pertinentes
    ''' </summary>
    ''' <remarks></remarks>
    Public AdquisitionType As Integer

    ''' <summary>
    ''' Variable para saber si Deprecia o No de acuerdo al Tipo de Adquisición
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagDepreciation As Boolean

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

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        InputRemissionEquipment = Nothing
        InputRemissionEquipmentEdit = Nothing
        _listInputRemissionEquipmentValidation = Nothing
        _editMode = Nothing
        listInputRemissionEquipmentDetail = Nothing
        listInputRemissionEquipmentDetailDelete = Nothing
        InputRemissionEquipmentDetail = Nothing
        indexEditRecord = Nothing
        EquipmentTypeId = Nothing
        _MyTag = Nothing
        ObjIva = Nothing
        FlagChangedSearch = Nothing
        FlagChangedItemBarButtons = Nothing
        AdquisitionType = Nothing
        FlagDepreciation = Nothing
    End Sub

    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopUpEquipment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True

        IndigoGridControl1.RefreshGrid(INDGcEquipmentDetail)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvEquipmentDetail, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvEquipmentDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        'Valido el Tipo de Adquisición para saber qué controles activar o no
        If AdquisitionType = 8 OrElse AdquisitionType = 10 Then
            FlagDepreciation = False
        Else
            FlagDepreciation = True
        End If

        If ImportData = False Then 'Si viene desde el form principal
            If _editMode = True Then
                LoadControls()
            End If
        Else 'Si viene desde el form de importar
            Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Artículo", .FieldName = "ItemCodeName"}}.ToList()
            FlagChangedItemBarButtons = False
            Me.BarraBotones.FilterDataSource = ListFixedAssetRemissionEntranceItem
            FlagChangedItemBarButtons = True
            If ListFixedAssetRemissionEntranceItem.Count = 1 Then
                InputRemissionEquipment = ListFixedAssetRemissionEntranceItem(0)
                LoadControls()
            End If
        End If
    End Sub

#End Region

#Region "MenuContext"

#Region "Click_ButtonAction"

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos de la orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case button.Tag.ToString
            Case "Edit"
                EditDetail()
            Case "Remove"
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
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

#End Region

#End Region

#Region "QueryPopUp"

    Private Sub INDSleEquipment_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEquipment.QueryPopUp
        If INDSleEquipment.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If EquipmentXPO Is Nothing Then
            Using model As New MEquipmentEntry(_MyTag)
                EquipmentXPO = model.ListFixedAssetItem(AdquisitionType)
            End Using
        End If
    End Sub

    Private Sub INDSleTrademark_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleTrademark.QueryPopUp
        If INDSleTrademark.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If TrademarkXPO Is Nothing Then
            Using model As New MEquipmentEntry(_MyTag)
                TrademarkXPO = model.ListTrademark()
            End Using
        End If
    End Sub

    Private Sub INDSlePolize_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePolize.QueryPopUp
        If INDSlePolize.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If PolizeXPO Is Nothing Then
            Using model As New MEquipmentEntry(_MyTag)
                PolizeXPO = model.ListPoliza()
            End Using
        End If
    End Sub

    Private Sub INDSlIVA_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlIVA.QueryPopUp
        If INDSlIVA.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If IVAXPO Is Nothing Then
            Using model As New MEquipmentEntry(_MyTag)
                IVAXPO = model.ListIVA()
            End Using
        End If
    End Sub



#End Region

#Region "ButtonClick"

    Private Sub INDSleEquipment_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleEquipment.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(572, Nothing, True)
            Using model As New MEquipmentEntry(_MyTag)
                EquipmentXPO = model.ListFixedAssetItem(AdquisitionType)
            End Using
        End If
    End Sub

    Private Sub INDSleTrademark_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleTrademark.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1700, Nothing, True)
            Using model As New MEquipmentEntry(_MyTag)
                TrademarkXPO = model.ListTrademark()
            End Using
        End If
    End Sub

    Private Sub INDSlePolize_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlePolize.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1702, Nothing, True)
            Using model As New MEquipmentEntry(_MyTag)
                PolizeXPO = model.ListPoliza()
            End Using
        End If
    End Sub

    Private Sub INDSlIVA_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlIVA.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1509, Nothing, True)
            Using model As New MEquipmentEntry(_MyTag)
                IVAXPO = model.ListIVA()
            End Using
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Try
            AsyncLoader(True)
            INDBtnAdd.Enabled = False

            If ValidateControls() = False Then 'Se validan que los controles esten diligenciados
                INDBtnAdd.Enabled = True
                AsyncLoader(False)
                Exit Sub
            Else
                If ImportData = False Then 'Si viene desde el form principal
                    If listInputRemissionEquipmentDetail Is Nothing OrElse listInputRemissionEquipmentDetail.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe agregar un detalle."
                        INDBtnAdd.Enabled = True
                        AsyncLoader(False)
                        Exit Sub
                    End If
                    If INDseQuantity.EditValue <> listInputRemissionEquipmentDetail.Count Then
                        Mensaje(EeventViewerImages.Advertencia) = "La cantidad especificada es diferente a la cantidad de detalles."
                        INDBtnAdd.Enabled = True
                        AsyncLoader(False)
                        Exit Sub
                    End If
                Else 'Si viene desde el form de importar
                    'Se asigna los valores
                    SetValues()

                    Dim errors As String = ValidateList()
                    If errors.Length > 0 Then
                        INDBtnAdd.Enabled = True
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = errors
                        Exit Sub
                    End If
                End If
            End If

            If ImportData = False Then
                SetValues()

                If _listInputRemissionEquipmentValidation Is Nothing Then
                    _listInputRemissionEquipmentValidation = New List(Of FixedAssetRemissionEntranceItem)
                End If
                Dim args As New AddEquipmentRemissionEventArgs
                If _editMode = True Then
                    args.InputRemissionEquipment = InputRemissionEquipment
                    args.EditMode = True
                Else
                    args.InputRemissionEquipment = InputRemissionEquipment
                    _listInputRemissionEquipmentValidation.Add(InputRemissionEquipment)
                End If

                RaiseEvent AddEquipmentRemissionEventArgs(Nothing, args)

                AsyncLoader(False)
                INDBtnAdd.Enabled = True
                Me.Close()
            Else
                'Se crea el objeto que se va a devolver
                Dim args As New AddEquipmentRemissionEventArgs
                args.ListFixedAssetRemissionEntranceItem = ListFixedAssetRemissionEntranceItem
                RaiseEvent AddEquipmentRemissionEventArgs(Nothing, args)
                Me.Close()
            End If
            CleanControls()
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnAdd.Enabled = True
            Throw ex
        End Try
    End Sub

    Private Sub INDBtnAddDetailEquipment_Click(sender As Object, e As EventArgs) Handles INDBtnAddDetailEquipment.Click
        'Se valida que haya ingresado una cantidad de articulo
        If INDseQuantity.EditValue Is Nothing OrElse INDseQuantity.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar una cantidad"
            Exit Sub
        End If
        If listInputRemissionEquipmentDetail IsNot Nothing AndAlso listInputRemissionEquipmentDetail.Count > 0 Then
            If INDseQuantity.EditValue = listInputRemissionEquipmentDetail.Count Then
                Mensaje(EeventViewerImages.Advertencia) = "No puede agregar más detalles porque la cantidad especificada es igual a la cantidad del listado."
                Exit Sub
            End If
        End If
        OpenFormDetail(False)
    End Sub

    ''' <summary>
    ''' Abre el formulario de detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFormDetail(editMode As Boolean)
        Using formulario As New FrmFixedAssetRemissionEntranceItemDetail
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddEquipmentDetailRemission, AddressOf ReturnAddRemissionEquipmentDetail
            formulario.MyTag = _MyTag
            formulario.EditMode = editMode
            formulario.IdEquipment = INDSleEquipment.EditValue
            formulario.GetLocationResponsible = GetLocationResponsible
            formulario.FlagDepreciate = FlagDepreciation

            If editMode Then
                formulario.InputRemissionEquipmentDetail = InputRemissionEquipmentDetail
            Else
                Dim cont = 0
                If listInputRemissionEquipmentDetail IsNot Nothing AndAlso listInputRemissionEquipmentDetail.Count > 0 Then
                    cont = listInputRemissionEquipmentDetail.Count
                End If
                cont = INDseQuantity.EditValue - cont
                formulario.QuantityDetails = cont
            End If

            formulario.Size = New Drawing.Size(1090, 750)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopUpEquipment_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'If ImportData = False Then
        '    If InputRemissionEquipment IsNot Nothing Then
        '        If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '            e.Cancel = True
        '        End If
        '    End If
        'End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDTxtTaxesValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtTaxesValue.EditValueChanged, INDTxtDiscountValue.EditValueChanged
        CalculateTotalValue()
    End Sub

    Private Sub INDTxtSubTotal_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtSubTotal.EditValueChanged, INDSpeIvaPercentage.EditValueChanged
        CalculateIVAValue()
        CalculateTotalValue()
    End Sub

    Private Sub INDseQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDseQuantity.EditValueChanged, INDTxtEquipmentValue.EditValueChanged
        CalculateValue()
    End Sub

    Private Sub INDSlIVA_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlIVA.EditValueChanged
        If INDSlIVA.EditValue IsNot Nothing Then
            INDSpeIvaPercentage.EditValue = INDSlIVA.Properties.View.GetFocusedRowCellValue("Percentage")
        End If

    End Sub

    Private Sub INDSleEquipment_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleEquipment.EditValueChanged
        If INDSleEquipment.EditValue IsNot Nothing AndAlso FlagChangedSearch = True Then
            'Dim itemXpo = DirectCast(DirectCast(ViewItemSearch.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetEquipmentXpo)
            Dim modelXPO As New MEquipmentEntry(Me.Tag)
            Dim itemXpo = modelXPO.GetEquipmentById(INDSleEquipment.EditValue)
            If itemXpo IsNot Nothing Then
                If itemXpo.AllowDepreciate Then 'Si el articulo permite depreciar
                    If itemXpo.FixedAssetItemDetailXpo Is Nothing OrElse itemXpo.FixedAssetItemDetailXpo.Count = 0 Then 'Si el articulo no tiene detalles de contabilidad
                        Mensaje(EeventViewerImages.Advertencia) = "El articulo " + INDSleEquipment.Text.Trim + " no se puede seleccionar porque permite depreciar y no tiene detalles contables."
                        INDTxtEquipmentValue.EditValue = Nothing
                        INDSleEquipment.Properties.NullText = String.Empty
                        INDSleEquipment.EditValue = Nothing
                        Exit Sub
                    End If
                End If
                INDTxtEquipmentValue.EditValue = itemXpo.LastCostItem

                INDSlIVA.EditValue = itemXpo.IVAId.Id
                INDSlIVA.Properties.NullText = itemXpo.IVAId.Code + " - " + itemXpo.IVAId.Name
                INDSpeIvaPercentage.EditValue = itemXpo.IVAId.Percentage

            End If
        End If
    End Sub

    Private Sub INDSleEquipmentType_EditValueChanged(sender As Object, e As EventArgs)
        If INDSleEquipment.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If EquipmentXPO Is Nothing Then
            Using model As New MEquipmentEntry(_MyTag)
                EquipmentXPO = model.ListFixedAssetItem(AdquisitionType)
            End Using
        End If
    End Sub

    Private Sub INDSpDiscountPercentage_EditValueChanged(sender As Object, e As EventArgs) Handles INDSpDiscountPercentage.EditValueChanged
        If INDSpDiscountPercentage.EditValue IsNot Nothing AndAlso INDSpDiscountPercentage.EditValue > 0 AndAlso INDTxtSubTotal.EditValue AndAlso INDTxtSubTotal.EditValue > 0 Then
            INDTxtDiscountValue.EditValue = (INDTxtSubTotal.EditValue * INDSpDiscountPercentage.EditValue) / 100
        Else
            INDTxtDiscountValue.EditValue = 0
        End If
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmFixedAssetRemissionEntranceItem_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region

#Region "Properties"

    Property EquipmentXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleEquipment.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleEquipment.Properties.DataSource = value
        End Set
    End Property

    Property TrademarkXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleTrademark.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleTrademark.Properties.DataSource = value
        End Set
    End Property

    Property PolizeXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSlePolize.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlePolize.Properties.DataSource = value
        End Set
    End Property

    Property IVAXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSlIVA.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlIVA.Properties.DataSource = value
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

    Private _importData As Boolean
    Public Property ImportData As Boolean
        Get
            Return _importData
        End Get
        Set(value As Boolean)
            _importData = value
        End Set
    End Property

    Private _listFixedAssetRemissionEntranceItem As List(Of FixedAssetRemissionEntranceItem)
    Public Property ListFixedAssetRemissionEntranceItem As List(Of FixedAssetRemissionEntranceItem)
        Get
            Return _listFixedAssetRemissionEntranceItem
        End Get
        Set(value As List(Of FixedAssetRemissionEntranceItem))
            _listFixedAssetRemissionEntranceItem = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Valida las propiedades de las entidades del listado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateList() As String
        Dim ListErrors As New StringBuilder
        For Each remissionEntranceItem In ListFixedAssetRemissionEntranceItem
            If remissionEntranceItem.FixedAssetRemissionEntranceItemDetail Is Nothing OrElse remissionEntranceItem.FixedAssetRemissionEntranceItemDetail.Count = 0 Then
                ListErrors.AppendLine("Debe agregar un detalle del item " + (ListFixedAssetRemissionEntranceItem.IndexOf(remissionEntranceItem) + 1).ToString)
            End If
            If remissionEntranceItem.Quantity <> remissionEntranceItem.FixedAssetRemissionEntranceItemDetail.Count Then
                ListErrors.AppendLine("La cantidad del item " + (ListFixedAssetRemissionEntranceItem.IndexOf(remissionEntranceItem) + 1).ToString + " es diferente a la cantidad de sus detalles")
            End If
            If remissionEntranceItem.ItemId = Nothing Then
                ListErrors.AppendLine("Debe seleccionar un articulo del item " + (ListFixedAssetRemissionEntranceItem.IndexOf(remissionEntranceItem) + 1).ToString)
            End If
            If remissionEntranceItem.TrademarkId = Nothing Then
                ListErrors.AppendLine("Debe seleccionar una marca del item " + (ListFixedAssetRemissionEntranceItem.IndexOf(remissionEntranceItem) + 1).ToString)
            End If
            If remissionEntranceItem.Model = String.Empty Then
                ListErrors.AppendLine("Debe ingresar un modelo del item " + (ListFixedAssetRemissionEntranceItem.IndexOf(remissionEntranceItem) + 1).ToString)
            End If
            If remissionEntranceItem.PolicyId = Nothing Then
                ListErrors.AppendLine("Debe seleccionar una poliza del item " + (ListFixedAssetRemissionEntranceItem.IndexOf(remissionEntranceItem) + 1).ToString)
            End If
            If remissionEntranceItem.IVAId = Nothing Then
                ListErrors.AppendLine("Debe seleccionar un IVA del item " + (ListFixedAssetRemissionEntranceItem.IndexOf(remissionEntranceItem) + 1).ToString)
            End If
            If remissionEntranceItem.Quantity = Nothing Then
                ListErrors.AppendLine("Debe ingresar una cantidad del item " + (ListFixedAssetRemissionEntranceItem.IndexOf(remissionEntranceItem) + 1).ToString)
            End If
        Next
        Return ListErrors.ToString
    End Function

    ''' <summary>
    ''' Realiza la operación con la cantidad y el valor del activo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TotalValue()
        INDTxtTotalValue.EditValue = INDTxtSubTotal.EditValue - INDTxtDiscountValue.EditValue + INDTxtTaxesValue.EditValue
    End Sub

    ''' <summary>
    ''' Metodo que calcula el valor total
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateTotalValue()
        INDTxtTotalValue.EditValue = INDTxtSubTotal.EditValue - INDTxtDiscountValue.EditValue + INDTxtTaxesValue.EditValue
    End Sub

    ''' <summary>
    ''' Metodo que calcula los valores para los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateValue()
        'Se calcula el SubTotal
        If INDseQuantity.EditValue IsNot Nothing AndAlso INDseQuantity.EditValue > 0 AndAlso INDseQuantity.EditValue IsNot Nothing AndAlso INDseQuantity.EditValue > 0 Then
            INDTxtSubTotal.EditValue = INDseQuantity.EditValue * INDTxtEquipmentValue.EditValue
        Else
            INDTxtSubTotal.EditValue = 0
        End If
    End Sub

    ''' <summary>
    ''' Metodo que calcula el valor del IVA
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateIVAValue()
        'Se calcula el valor del IVA
        If INDTxtSubTotal.EditValue IsNot Nothing AndAlso INDTxtSubTotal.EditValue > 0 AndAlso INDSpeIvaPercentage.EditValue IsNot Nothing AndAlso INDSpeIvaPercentage.EditValue > 0 Then
            INDTxtTaxesValue.EditValue = (INDTxtSubTotal.EditValue * INDSpeIvaPercentage.EditValue) / 100
        Else
            INDTxtTaxesValue.EditValue = 0
        End If
    End Sub

    ''' <summary>
    ''' metodo para obtener lo que se retorna del formulario modal de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddRemissionEquipmentDetail(sender As Object, e As AddDetailEquipmentRemissionEventArgs)
        If listInputRemissionEquipmentDetail Is Nothing Then
            listInputRemissionEquipmentDetail = New List(Of FixedAssetRemissionEntranceItemDetail)
        End If
        If e.EditMode = True Then
            listInputRemissionEquipmentDetail.Remove(InputRemissionEquipmentDetail)
            listInputRemissionEquipmentDetail.Insert(indexEditRecord, e.InputRemissionEquipmentDetail)
        Else
            listInputRemissionEquipmentDetail.AddRange(e.ListInputRemissionEquipmentDetail)
        End If

        INDGcEquipmentDetail.DataSource = Nothing
        INDGcEquipmentDetail.DataSource = listInputRemissionEquipmentDetail
    End Sub

    ''' <summary>
    ''' establece los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValues()
        If ImportData = False Then
            If _editMode = False Then
                InputRemissionEquipment = New FixedAssetRemissionEntranceItem
            End If
        End If

        With InputRemissionEquipment
            If ImportData = False Then
                If _editMode = False Then
                    .RemissionSource = 1 'Ninguna
                End If
            End If
            .ItemId = INDSleEquipment.EditValue
            .NameEquipment = INDSleEquipment.Text
            .Quantity = INDseQuantity.EditValue
            .IVAId = INDSlIVA.EditValue
            .NameIva = INDSlIVA.Text
            .TrademarkId = INDSleTrademark.EditValue
            .NameTrademark = INDSleTrademark.Text
            .Model = INDTxtModel.EditValue
            .PolicyId = INDSlePolize.EditValue
            .NamePoliza = INDSlePolize.Text
            .UnitValue = INDTxtEquipmentValue.EditValue
            .SubTotalValue = INDTxtSubTotal.EditValue
            .IvaPercentage = INDSpeIvaPercentage.EditValue
            .TotalValue = INDTxtTotalValue.EditValue
            .IvaValue = INDTxtTaxesValue.EditValue
            .OutstandingQuantity = .Quantity
            .DiscountPercentage = INDSpDiscountPercentage.EditValue
            .DiscountValue = INDTxtDiscountValue.EditValue

            If listInputRemissionEquipmentDetail IsNot Nothing AndAlso listInputRemissionEquipmentDetail.Count > 0 Then
                For Each item In listInputRemissionEquipmentDetail
                    .FixedAssetRemissionEntranceItemDetail.Add(item)
                Next
            End If

            If listInputRemissionEquipmentDetailDelete IsNot Nothing AndAlso listInputRemissionEquipmentDetailDelete.Count > 0 Then
                For Each item In listInputRemissionEquipmentDetail
                    .FixedAssetRemissionEntranceItemDetail.Add(item)
                Next
            End If

        End With

    End Sub

    ''' <summary>
    ''' valida los controles del formualario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder

        If INDSleEquipment.EditValue = 0 Then
            errors.AppendLine(INDLciEquipment.Text + ResourceManager.GetString("Empty"))
        End If

        If INDseQuantity.Text = String.Empty Then
            errors.AppendLine(INDLciQuantity.Text + ResourceManager.GetString("Empty"))
        End If

        If INDSleTrademark.EditValue = 0 Then
            errors.AppendLine(INDLciTrademark.Text + ResourceManager.GetString("Empty"))
        End If

        If INDTxtModel.Text = String.Empty Then
            errors.AppendLine(INDLciModel.Text + ResourceManager.GetString("Empty"))
        End If

        If INDSlePolize.EditValue = 0 Then
            errors.AppendLine(INDLciPoliza.Text + ResourceManager.GetString("Empty"))
        End If

        If INDTxtEquipmentValue.Text = String.Empty Then
            errors.AppendLine(INDLciEquipmentValue.Text + ResourceManager.GetString("Empty"))
        End If

        If INDTxtTotalValue.Text = String.Empty Then
            errors.AppendLine(INDLciTotalValue.Text + ResourceManager.GetString("Empty"))
        End If

        If INDSlIVA.EditValue = 0 Then
            errors.AppendLine(INDLciTotalValue.Text + ResourceManager.GetString("Empty"))
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        InputRemissionEquipmentDetail = DirectCast(INDGvEquipmentDetail.GetFocusedRow(), FixedAssetRemissionEntranceItemDetail)
        indexEditRecord = listInputRemissionEquipmentDetail.IndexOf(InputRemissionEquipmentDetail)
        OpenFormDetail(True)
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        InputRemissionEquipmentDetail = DirectCast(INDGvEquipmentDetail.GetFocusedRow(), FixedAssetRemissionEntranceItemDetail)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If InputRemissionEquipment.Id > 0 Then
                If listInputRemissionEquipmentDetailDelete Is Nothing Then
                    listInputRemissionEquipmentDetailDelete = New List(Of FixedAssetRemissionEntranceItemDetail)
                End If
                While InputRemissionEquipment.FixedAssetRemissionEntranceItemDetail.Count > 0
                    If InputRemissionEquipment.FixedAssetRemissionEntranceItemDetail(0).Id > 0 Then
                        InputRemissionEquipment.FixedAssetRemissionEntranceItemDetail(0).MarkAsDeleted()
                    Else
                        InputRemissionEquipment.FixedAssetRemissionEntranceItemDetail.Remove(InputRemissionEquipment.FixedAssetRemissionEntranceItemDetail(0))
                    End If
                End While
                InputRemissionEquipmentDetail.MarkAsDeleted()
                listInputRemissionEquipmentDetailDelete.Add(InputRemissionEquipmentDetail)
            Else
                InputRemissionEquipment.FixedAssetRemissionEntranceItemDetail.Remove(InputRemissionEquipmentDetail)
            End If
            listInputRemissionEquipmentDetail.Remove(InputRemissionEquipmentDetail)

            INDGcEquipmentDetail.DataSource = Nothing
            INDGcEquipmentDetail.DataSource = listInputRemissionEquipmentDetail

        End If
    End Sub

    ''' metodo para cargar los controles con la informacion requerida
    ''' </summary>
    ''' <param name="AdministrativeLoanAccountingInformationTmp"></param>
    ''' <remarks></remarks>
    Private Sub LoadControls()
        With InputRemissionEquipment

            FlagChangedSearch = False
            INDSleEquipment.EditValue = .ItemId
            INDSleEquipment.Properties.NullText = .NameEquipment
            FlagChangedSearch = True

            INDSleTrademark.EditValue = .TrademarkId
            INDSleTrademark.Properties.NullText = .NameTrademark

            INDTxtModel.EditValue = .Model

            INDSlePolize.EditValue = .PolicyId
            INDSlePolize.Properties.NullText = .NamePoliza

            INDSlIVA.EditValue = .IVAId
            INDSlIVA.Properties.NullText = .NameIva

            INDseQuantity.EditValue = If(.Quantity <> .OutstandingQuantity, .OutstandingQuantity, .Quantity)

            INDSpeOutstandingQuantity.EditValue = .OutstandingQuantity

            INDTxtEquipmentValue.EditValue = .UnitValue

            INDTxtSubTotal.EditValue = .SubTotalValue

            INDSpeIvaPercentage.EditValue = .IvaPercentage

            INDTxtTaxesValue.EditValue = .IvaValue

            INDSpDiscountPercentage.EditValue = .DiscountPercentage

            INDTxtDiscountValue.EditValue = .DiscountValue

            INDTxtTotalValue.EditValue = .TotalValue

            listInputRemissionEquipmentDetail = .FixedAssetRemissionEntranceItemDetail.ToList()
            INDGcEquipmentDetail.DataSource = listInputRemissionEquipmentDetail

        End With
    End Sub

    ''' <summary>
    ''' metodo para mostrar los formulario en el evento buttonclik
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog(Me)
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDSleEquipment.EditValue = 0
        INDseQuantity.EditValue = 0
        INDSleTrademark.EditValue = 0
        INDTxtModel.EditValue = 0
        INDSlePolize.EditValue = 0
        INDTxtEquipmentValue.EditValue = 0
        INDTxtTotalValue.EditValue = 0
        INDSlIVA.EditValue = 0
        InputRemissionEquipment = Nothing

    End Sub

#End Region

#Region "BarButtons"

    ''' <summary>
    ''' Evento para limpiar los valores de los controles al darle click
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        If FlagChangedItemBarButtons Then
            SetValues()
        End If
        InputRemissionEquipment = Record
        LoadControls()
    End Sub

#End Region

End Class