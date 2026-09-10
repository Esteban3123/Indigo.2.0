'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Andres Alarcon
' Created          : 31/10/2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Threading
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmRequestDashboard

#Region "Properties"


    ''' <summary>
    ''' Variable que almacena el datasource del Tab "Autorizados"
    ''' </summary>
    Private VListRequestDetailAuthorizedXpo As Task(Of List(Of ViewListRequestDetailAuthorizedXpo))

    ''' <summary>
    ''' Variable que se utiliza para obtener los códigos de centro de atención y realizar las consulta con los filtros
    ''' </summary>
    Private CareCenterFilters As String

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PRequestDashboard

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Cache para evitar consultas repetidas
    ''' </summary>
    Private _authCache As New Dictionary(Of String, Boolean)

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Filtro de tipo Unidad funcional / Almacén
    ''' </summary>
    ''' <remarks></remarks>
    Private _Type As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property Type As List(Of Tuple(Of Byte, String))
        Get
            If _Type Is Nothing Then
                _Type = New List(Of Tuple(Of Byte, String))
                _Type.Add(New Tuple(Of Byte, String)(1, "Unidad funcional"))
                _Type.Add(New Tuple(Of Byte, String)(2, "Almacén"))
            End If
            Return _Type
        End Get
    End Property

    ''' <summary>
    ''' Vuelve a cargar el datasource de la rejilla activa
    ''' </summary>
    Private Sub BeginReloadDatasource()

        Select Case INDtcgInformation.SelectedTabPageName
            Case INDLcUnauthorized.Name
                INDGcUnauthorized.DataSource = Nothing
                LoadUnauthorizedDetail()
            Case INDLcAuthorized.Name
                INDGcAuthorized.DataSource = Nothing
                LoadAuthorizedDetail()
        End Select
        SetGrouping()
    End Sub

    ''' <summary>
    ''' Carga las solicitudes sin autorizar
    ''' </summary>
    Private Sub LoadUnauthorizedDetail()
        Using model As New MRequestDashboard(Tag)
            INDGvUnauthorized.ShowLoadingPanel()
            INDGcUnauthorized.DataSource = model.ListRequestDetailByOperatingUnitAndType(CareCenterFilters, _selectorType.GetKeys())
            INDGvUnauthorized.HideLoadingPanel()
        End Using
    End Sub

    ''' <summary>
    ''' Consulta el paquete
    ''' </summary>
    Private Sub LoadAuthorizedDetail()
        INDGvAuthorized.ShowLoadingPanel()
        VListRequestDetailAuthorizedXpo = Task.Factory.StartNew(Function() As List(Of ViewListRequestDetailAuthorizedXpo)
                                                                    Return Presenter.ListViewRequestDetailOtherDetailAuthorizedXpo
                                                                End Function)

        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListViewRequestDetailAuthorizedXpo(1, _selectorType.GetKeys(), CareCenterFilters)

                                  Me.SafeInvoke(Sub()
                                                    INDGcAuthorized.DataSource = result
                                                    INDGvAuthorized.HideLoadingPanel()
                                                End Sub)
                              End Sub)
    End Sub

    ''' <summary>
    ''' Método que asigna la columna de acciones a las rejillas
    ''' </summary>
    Private Sub SetActionsColumns()
        BarraBotones.ActualizarPermisosBarra(Me.Tag)

        IndigoGridView1.SetListAcction(INDGvUnauthorized, {eAcciones.ConfirmItem}.ToList())

        Dim col = INDGvUnauthorized.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
        If col IsNot Nothing Then col.Width = 100
    End Sub

    Private Sub SetGrouping()

        Dim Selected = _selectorType.GetKeysToArray()

        INDGvUnauthorized.Columns.ColumnByFieldName("WarehouseCodeName").Visible = False
        INDGvUnauthorized.Columns.ColumnByFieldName("MovementTypeName").Visible = False
        INDGvUnauthorized.Columns.ColumnByFieldName("FunctionUnitCodeName").Visible = False
        INDGvUnauthorized.Columns.ColumnByFieldName("RequestTypeName").GroupIndex = 0
        INDGvAuthorized.Columns.ColumnByFieldName("WarehouseCodeName").Visible = False
        INDGvAuthorized.Columns.ColumnByFieldName("MovementTypeName").Visible = False
        INDGvAuthorized.Columns.ColumnByFieldName("FunctionUnitCodeName").Visible = False
        INDGvAuthorized.Columns.ColumnByFieldName("RequestTypeName").GroupIndex = 0

        For Each item In Selected
            If item = 1 Then
                INDGvUnauthorized.Columns.ColumnByFieldName("FunctionUnitCodeName").Visible = True
                INDGvAuthorized.Columns.ColumnByFieldName("FunctionUnitCodeName").Visible = True

            ElseIf item = 2 Then
                INDGvUnauthorized.Columns.ColumnByFieldName("WarehouseCodeName").Visible = True
                INDGvUnauthorized.Columns.ColumnByFieldName("MovementTypeName").Visible = True
                INDGvAuthorized.Columns.ColumnByFieldName("WarehouseCodeName").Visible = True
                INDGvAuthorized.Columns.ColumnByFieldName("MovementTypeName").Visible = True
            End If
        Next

        VisibleIndexGrid()
    End Sub

    Private Sub VisibleIndexGrid()
        If _selectorType.GetKeysToArray().FirstOrDefault() = 1 And _selectorType.GetKeysToArray.Count = 1 Then
            GridColumn271.ShowColumn(0)
            GridColumn02.ShowColumn(1)
            GridColumn281.ShowColumn(2)
            GridColumn291.ShowColumn(3)
            GridColumn531.ShowColumn(4)
            GridColumn451.ShowColumn(5)
            GridColumn1.ShowColumn(6)
            GridColumn9.ShowColumn(7)
            GridColumn13.ShowColumn(0)
            GridColumn14.ShowColumn(1)
            GridColumn16.ShowColumn(2)
            GridColumn17.ShowColumn(3)
            GridColumn18.ShowColumn(4)
            GridColumn22.ShowColumn(5)
            GridColumn23.ShowColumn(6)
            GridColumn46.ShowColumn(7)

        ElseIf _selectorType.GetKeysToArray.FirstOrDefault() = 2 And _selectorType.GetKeysToArray.Count = 1 Then
            GridColumn271.ShowColumn(0)
            GridColumn02.ShowColumn(1)
            GridColumn281.ShowColumn(2)
            GridColumn291.ShowColumn(3)
            GridColumn301.ShowColumn(4)
            GridColumn312.ShowColumn(5)
            GridColumn451.ShowColumn(6)
            GridColumn1.ShowColumn(7)
            GridColumn9.ShowColumn(8)
            GridColumn13.ShowColumn(0)
            GridColumn14.ShowColumn(1)
            GridColumn16.ShowColumn(2)
            GridColumn17.ShowColumn(3)
            GridColumn19.ShowColumn(4)
            GridColumn21.ShowColumn(5)
            GridColumn22.ShowColumn(6)
            GridColumn23.ShowColumn(7)
            GridColumn46.ShowColumn(8)
        Else
            GridColumn271.ShowColumn(0)
            GridColumn02.ShowColumn(1)
            GridColumn281.ShowColumn(2)
            GridColumn291.ShowColumn(3)
            GridColumn531.ShowColumn(4)
            GridColumn301.ShowColumn(5)
            GridColumn312.ShowColumn(6)
            GridColumn451.ShowColumn(7)
            GridColumn1.ShowColumn(8)
            GridColumn9.ShowColumn(9)
            GridColumn13.ShowColumn(0)
            GridColumn14.ShowColumn(1)
            GridColumn16.ShowColumn(2)
            GridColumn17.ShowColumn(3)
            GridColumn18.ShowColumn(4)
            GridColumn19.ShowColumn(5)
            GridColumn21.ShowColumn(6)
            GridColumn22.ShowColumn(7)
            GridColumn23.ShowColumn(8)
            GridColumn46.ShowColumn(9)
        End If
    End Sub

    ''' <summary>
    ''' Método para confirmar la solicitud seleccionada
    ''' </summary>
    ''' <param name="RequestDetailXpo"></param>
    Private Sub ConfirmRequestDetail(RequestDetailXpo As ViewListRequestDetailUnauthorizedXpo)

        Dim code As String = String.Empty
        Dim entityLabel As String = Nothing

        'Extraemos el código del CodeName correspondiente
        If Not GetEntityCodeAndLabel(RequestDetailXpo, code, entityLabel) Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede determinar la entidad para validar la autorización."
            Exit Sub
        End If

        'Se valida si el usuario está autorizado para hacer la solicitud según el código y la entidad
        If Not String.IsNullOrEmpty(code) AndAlso Not ValidateUserAuthorization(code, RequestDetailXpo.RequestType) Then
            Mensaje(EeventViewerImages.Advertencia) = $"No se encuentra autorizado para confirmar solicitudes de {entityLabel}"
            Exit Sub
        End If

        'Confirma las solicitudes del dashboard
        Using model As New MRequestDashboard(Tag)
            Dim result = model.UpdateQuantityAuthorizedInventoryRequestDetail(RequestDetailXpo.Id, RequestDetailXpo.Quantity)

            If result.StateResult Then
                Mensaje(EeventViewerImages.Informacion) = "El proceso se completo correctamente"
                BeginReloadDatasource()
            Else
                Mensaje(EeventViewerImages.Informacion) = result.Message
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Función para extraer el código de la unidad funcional o almacén
    ''' </summary>
    ''' <param name="row"></param>
    ''' <param name="code"></param>
    ''' <param name="entityLabel"></param>
    ''' <returns></returns>
    Private Function GetEntityCodeAndLabel(row As ViewListRequestDetailUnauthorizedXpo, ByRef code As String, ByRef entityLabel As String) As Boolean

        'Reiniciamos las variables
        code = Nothing
        entityLabel = Nothing

        Dim codeName As String = Nothing
        Select Case row.RequestType
            Case 1 ' Unidad funcional
                codeName = row.FunctionUnitCodeName
                entityLabel = "esta unidad funcional"
            Case 2 ' Almacén
                codeName = row.WarehouseCodeName
                entityLabel = "este almacén"
            Case Else
                Return False
        End Select

        If String.IsNullOrWhiteSpace(codeName) Then Return False
        Dim dash = codeName.IndexOf("-"c)
        code = If(dash >= 0, codeName.Substring(0, dash), codeName).Trim()
        Return Not String.IsNullOrEmpty(code)
    End Function

    ''' <summary>
    ''' Valida si el usuario actual está autorizado para confirmar solicitudes
    ''' </summary>
    ''' <param name="code">Código de la unidad funcional o almacen</param>
    ''' <param name="type">Tipo de solicitud 1-Unidad Funcional 2-Almacen</param>
    Private Function ValidateUserAuthorization(code As String, type As Integer) As Boolean
        'Id del usuario actual
        Dim currentUserId = indigo.UserIndigoId

        'Consultar los parámetros de autorización
        Dim authUsers = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetCollection(Of View.ViewRequestParamAuthUserXpo)(
            Nothing,
            $"Code='{code}' AND CodeType='{type}'",
            False
        )

        'Verificar si el usuario actual es autorizador principal o suplente
        For Each authUser In authUsers
            If authUser.UserPrincipalId = currentUserId OrElse authUser.UserAlternativeId = currentUserId Then
                Return True
            End If
        Next
        Return False
    End Function

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRequestDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PRequestDashboard()
        Me.ToolBar.Hide()
        SetActionsColumns()
        InitForm()
    End Sub

    ''' <summary>
    ''' Estados iniciales del formulario
    ''' </summary>
    Private Sub InitForm()

        '****Inicializar variables*****'
        INDSleType.Properties.DataSource = Type
        DefaultType(Type)
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance

    End Sub


#End Region

#Region "Editvaluechanging"
    ''' <summary>
    ''' Cantidad aprobada de la rejilla cuando se está modificando
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RepositoryItemSpinEdit_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemSpinEdit.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Dim row = INDGvUnauthorized.GetFocusedObject(Of ViewListRequestDetailUnauthorizedXpo)()

            Dim code As String = Nothing
            Dim entityLabel As String = Nothing

            If Not GetEntityCodeAndLabel(row, code, entityLabel) Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede determinar la entidad para validar la autorización."
                e.Cancel = True
                Exit Sub
            End If

            Dim cacheCode = $"{code}"
            Dim isAuthorized As Boolean

            If Not _authCache.TryGetValue(cacheCode, isAuthorized) Then
                isAuthorized = ValidateUserAuthorization(code, row.RequestType)
                _authCache(cacheCode) = isAuthorized
            End If

            If Not isAuthorized Then
                Mensaje(EeventViewerImages.Advertencia) = $"No se encuentra autorizado para aprobar cantidades de {entityLabel}."
                e.Cancel = True
                Exit Sub
            End If

            If e.NewValue > row.OriginalQuantity Then
                Mensaje(EeventViewerImages.Advertencia) = "La cantidad aprobada no puede ser mayor a la cantidad solicitada"
                e.Cancel = True
                Exit Sub
            End If
            row.QuantityRejected = row.OriginalQuantity - e.NewValue
        End If
    End Sub

#End Region

#Region "Selector"

    ''' <summary>
    ''' Seleccion de todos los filtros de tipo cuando se inicia el formulario
    ''' </summary>
    Private Sub DefaultType(Datasource As List(Of Tuple(Of Byte, String)))
        For Each x In Datasource
            _selectorType.SetValue(x)
            INDGvType.RefreshData()
        Next

        INDSleType.Properties.NullText = _selectorType.ToString()
    End Sub

    Private _selectorType As SelectorCache = New SelectorCache("Item1", "Item2")
    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvType.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvType" Then
                e.Value = _selectorType.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles INDGvType.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvType" Then
                selector = _selectorType
            End If

            If e.RowHandle >= 0 Then
                Dim row = view.GetRow(e.RowHandle)
                selector.SetValue(row)
            Else
                selector.Clear()
            End If
            view.RefreshData()
        End If
    End Sub

    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleType.Closed
        Dim searchLookupEdit = TryCast(sender, SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleType" Then
            searchLookupEdit.Properties.NullText = _selectorType.ToString()
        End If

        BeginReloadDatasource()
    End Sub
#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRequestDashboard_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCenterAttention.Properties.PopupFormMinSize = New System.Drawing.Size(INDsleCenterAttention.Size.Width - 11, 0)
        INDsleCenterAttention.Properties.PopupFormSize = New System.Drawing.Size(INDsleCenterAttention.Size.Width - 11, 0)

        INDtcgInformation.SelectedTabPageIndex = 0
        INDsleCenterAttention.Focus()
    End Sub

    ''' <summary>
    ''' Eventos para asignar la relacion y el dataSource a la sub-rejilla 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDGvAuthorized_MasterRowGetChildList(sender As Object, e As MasterRowGetChildListEventArgs) Handles INDGvAuthorized.MasterRowGetChildList
        INDGvAuthorizedDetail.ShowLoadingPanel()
        Dim data = (Await VListRequestDetailAuthorizedXpo)
        If e.ChildList Is Nothing Then
            Dim obj = INDGvAuthorized.GetFocusedObject(Of ViewListRequestDetailAuthorizedXpo)
            e.ChildList = data.FindAll(Function(m) m.InventoryRequestDetailOtherId = obj.InventoryRequestDetailOtherId)
            INDGvAuthorizedDetail.HideLoadingPanel()
        End If
    End Sub

    Private Sub INDviewMaster_MasterRowGetRelationName(sender As Object, e As MasterRowGetRelationNameEventArgs) Handles INDGvAuthorized.MasterRowGetRelationName
        e.RelationName = "Movimientos"
    End Sub

    Private Sub INDviewMaster_MasterRowGetRelationCount(sender As Object, e As MasterRowGetRelationCountEventArgs) Handles INDGvAuthorized.MasterRowGetRelationCount
        e.RelationCount = 1
    End Sub

#End Region

#Region "SelectPageChanged"

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia la pagina de los tabs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtcgInformation_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDtcgInformation.SelectedPageChanged
        If CareCenterFilters = String.Empty Then
            Exit Sub
        End If

        Select Case e.Page.Name
            Case INDLcUnauthorized.Name
                INDGcUnauthorized.DataSource = Nothing
                LoadUnauthorizedDetail()
            Case INDLcAuthorized.Name
                INDGcAuthorized.DataSource = Nothing
                LoadAuthorizedDetail()
        End Select
        SetGrouping()
    End Sub

#End Region

#Region "MenuContex"

    ''' <summary>
    ''' evento para mostrar o no los botones de acciones segun la informacion de la vista
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_QueryPopUpActionButtons(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        ConfirmRequestDetail(INDGvUnauthorized.GetFocusedObject(Of ViewListRequestDetailUnauthorizedXpo))
    End Sub

    Private Sub INDSbRefresh_Click(sender As Object, e As EventArgs) Handles INDSbRefresh.MouseClick
        BeginReloadDatasource()
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDsleCenterAttention_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCenterAttention.QueryPopUp
        If INDsleCenterAttention.Properties.DataSource Is Nothing Then
            Using model = New MRequestDashboard(Tag)
                INDsleCenterAttention.Properties.DataSource = model.ListAllOperatingUnit()
            End Using
        End If
    End Sub

    Private _firstSelected As Boolean = False
    Private Sub INDSleType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleType.QueryPopUp
        If Not _firstSelected Then
            INDSleType.Properties.View.SelectAll()
            _firstSelected = True
        End If
    End Sub

#End Region

#Region "CloseUp"
    Private Sub INDSleType_CloseUp(sender As Object, e As EventArgs) Handles INDSleType.CloseUp
        _selectorType.Clear()
        INDSleType.Properties.View.GetSelectedRows().ToList() _
                .ForEach(Sub(m) _selectorType.SetValue(INDSleType.Properties.View.GetRow(m)))

    End Sub

    Private Sub INDsleCenterAttention_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDsleCenterAttention.CloseUp
        'Se obtienen los códigos de las unidades operativas seleccionadas
        CareCenterFilters = String.Join(", ", (From x In INDviewSearchCareCenter.GetSelectedRows() Select "'" & DirectCast(INDviewSearchCareCenter.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.Id & "'"))

        If String.IsNullOrEmpty(CareCenterFilters) Then
            INDsleCenterAttention.Properties.NullText = "Seleccione una unidad operativa"
        ElseIf INDviewSearchCareCenter.GetSelectedRows().Count() = 1 Then
            INDsleCenterAttention.Properties.NullText = String.Join(",", (From x In INDviewSearchCareCenter.GetSelectedRows() Select DirectCast(INDviewSearchCareCenter.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.CodeName.ToString().Trim()))
        Else
            INDsleCenterAttention.Properties.NullText = INDviewSearchCareCenter.GetSelectedRows().Count().ToString() + " Item Seleccionados"
        End If

        BeginReloadDatasource()
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
#End Region

#End Region

End Class