'***********************************************************************
' Assembly         : Presentation.Controls
' Author           : Jorge Leonardo Vernaza
' Created          : 03-07-2013
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports System.ComponentModel
Imports System.Collections
Imports System.Diagnostics
Imports DevExpress.XtraEditors
Imports System.Drawing
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Base.Extension
Imports Microsoft.VisualBasic
Imports Presentation.Base
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

#End Region

''' <summary>
''' Clase con toda la funcionalidad del control extendido GridView
''' </summary>
<ProvideProperty("TemaIndigoMetro", GetType(GridView))> _
<ProvideProperty("ListAcction", GetType(GridView))>
Public Class IndigoGridView
    Inherits System.ComponentModel.Component
    Implements IExtenderProvider
    Implements ISupportInitialize

#Region "Eventos Publicos"
    ''' <summary>
    ''' evento que se dispara al hacer click en el boton de la accion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event Click_ButtonAction(sender As Object, e As EventArgs)
    ''' <summary>
    ''' evento que se dispara al hacer click en un item del menu contextual
    ''' </summary>
    Public Event ContexMenuActions(sender As Object, e As EventArgs)
#End Region

#Region "Variables Globales"
    ''' <summary>
    ''' Listado que contiene las acciones de las rejillas
    ''' </summary>
    Private _listActions As List(Of eAcciones)
    ''' <summary>
    ''' Variable hashtable que contiene los controles de tipo textedit
    ''' </summary>
    Private Hashtable As Hashtable
    ''' <summary>
    ''' The contenedor
    ''' </summary>
    Private Contenedor As System.ComponentModel.Container = Nothing
    ''' <summary>
    ''' boton que se usa para las acciones de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim btn As SimpleButton
    Friend WithEvents PopupMenuActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents BarManagerActions As DevExpress.XtraBars.BarManager
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Private components As System.ComponentModel.IContainer
    ''' <summary>
    ''' rejilla que se usa para ele xtendido de mas info
    ''' </summary>
    ''' <remarks></remarks>
    Dim vGridControl As DevExpress.XtraVerticalGrid.VGridControl = Nothing
#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Clase con las propiedades adicionales del control
    ''' </summary>
    Private Class Propiedades
        Public TemaIndigoMetro As Boolean
        Public ListAcction As List(Of eAction)
    End Class

    ''' <summary>
    ''' Metodo para agregar las propiedades adcionales a cada control de tipo gridcontrol
    ''' </summary>
    ''' <param name="key">The key.</param>
    ''' <returns></returns>
    Private Function EnsurePropertiesExists(ByVal key As Object) As Propiedades
        Dim p As Propiedades = DirectCast(Hashtable(key), Propiedades)
        If p Is Nothing Then
            p = New Propiedades()
            Hashtable(key) = p
        End If
        Return p
    End Function

#Region "TemaIndigoMetro"
    ''' <summary>
    ''' Funcion que retorna si la rejilla tiene tema indigo metro
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad que especifica si la rejilla tiene tema indigo metro")> _
    Public Function GetTemaIndigoMetro(ByVal p As GridView) As Boolean
        Return EnsurePropertiesExists(p).TemaIndigoMetro
    End Function

    ''' <summary>
    ''' Metodo que establece si la rejilla tiene tema indigo metro
    ''' </summary>
    ''' <param name="Obj">The obj.</param>
    ''' <param name="Value">if set to <c>true</c> [value].</param>
    Public Sub SetTemaIndigoMetro(ByVal Obj As GridView, Value As Boolean)
        EnsurePropertiesExists(Obj).TemaIndigoMetro = Value
    End Sub

#End Region

#Region "Acciones"
    ''' <summary>
    ''' metodo para ecrear los botones de las acciones
    ''' </summary>
    ''' <param name="_view"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub SetListAcction(ByVal _view As GridView, value As List(Of Presentation.Base.eAcciones))
        'Asignacion de la lista de acciones al menu contextual
        SetListContextMenu(_view, value)
        AddColumnActions(_view)
        CreatePopUpMenu(CType(_view.GridControl, DevExpress.XtraGrid.GridControl).TopLevelControl)
        Dim col As GridColumn
        If value Is Nothing Then
            _view.Columns.ColumnByName("colActions").Visible = False
        ElseIf value.Count = 1 Then
            Dim RepositoryItemButtonEdit1 As New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
            RepositoryItemButtonEdit1.Buttons.RemoveAt(0)
            RepositoryItemButtonEdit1.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
            RepositoryItemButtonEdit1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            RepositoryItemButtonEdit1.Appearance.Options.UseFont = True
            RepositoryItemButtonEdit1.Appearance.Options.UseForeColor = True
            RepositoryItemButtonEdit1.Appearance.Options.UseTextOptions = True

            Dim font_ As New Font("Segoe UI", 9.75, FontStyle.Underline, GraphicsUnit.Point, 1)
            RepositoryItemButtonEdit1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
            RepositoryItemButtonEdit1.AppearanceFocused.BackColor = Color.White
            RepositoryItemButtonEdit1.Appearance.Font = font_
            RepositoryItemButtonEdit1.NullText = Infrastructure.CrossCutting.Resources.ResourceManager.GetString(value(0).ToString)
            RepositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            col = _view.Columns.ColumnByName("colActions")
            col.AppearanceCell.Options.UseFont = True
            col.AppearanceCell.Options.UseForeColor = True
            col.AppearanceCell.Font = font_
            col.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
            col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col.ColumnEdit = RepositoryItemButtonEdit1
            If RepositoryItemButtonEdit1 IsNot Nothing Then
                AddHandler RepositoryItemButtonEdit1.Click, AddressOf BtnActions_Click
            End If

        Else
            Dim popupContainer As New PopupContainerControl
            Dim RepositoryItemPopupContainerEdit As New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
            RepositoryItemPopupContainerEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
            RepositoryItemPopupContainerEdit.Buttons(0).Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Combo
            For Each action As eAcciones In value
                btn = New SimpleButton
                btn.Size = New System.Drawing.Size(200, 36)
                Dim nombreAccion As String
                nombreAccion = action.ToString()
                btn.Text = Infrastructure.CrossCutting.Resources.ResourceManager.GetString(nombreAccion)
                popupContainer.Controls.Add(btn)
                btn.Dock = DockStyle.Top
                btn.Tag = nombreAccion
                AddHandler btn.Click, AddressOf BtnActions_Click
            Next
            RepositoryItemPopupContainerEdit.ShowPopupCloseButton = False
            RepositoryItemPopupContainerEdit.PopupSizeable = False
            RepositoryItemPopupContainerEdit.PopupControl = popupContainer
            RepositoryItemPopupContainerEdit.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.True
            col = _view.Columns.ColumnByName("colActions")
            col.ColumnEdit = RepositoryItemPopupContainerEdit
            Dim _sizePopup As Integer
            _sizePopup = 36 * value.Count
            popupContainer.Size = New System.Drawing.Size(200, _sizePopup)
        End If
    End Sub

#End Region

#Region "More Info"

    ''' <summary>
    ''' metodo para crear la columna de mas info
    ''' </summary>
    ''' <param name="_view"></param>
    ''' <remarks></remarks>
    Public Sub MoreInfoColunmns(ByVal _view As GridView)
        Dim ColMoreInfo As GridColumn = Nothing
        Dim popupContainerMoreInfo As PopupContainerControl = Nothing
        Dim RepositoriPopupMoreInfo As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = Nothing
        Dim rowPadre As DevExpress.XtraVerticalGrid.Rows.CategoryRow = Nothing
        Dim rowHija As DevExpress.XtraVerticalGrid.Rows.EditorRow
        Dim ContadorColVacias As Integer
        Dim heightPopup As Integer
        For Each col As GridColumn In _view.Columns
            If col.Visible = False Then
                If ColMoreInfo Is Nothing Then
                    ColMoreInfo = New GridColumn
                    popupContainerMoreInfo = New PopupContainerControl
                    RepositoriPopupMoreInfo = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
                    AddHandler RepositoriPopupMoreInfo.QueryPopUp, AddressOf PopUpContainerEdit_QueryPopUp
                    AddHandler RepositoriPopupMoreInfo.CloseUp, AddressOf PopUpContainerEdit_CloseUp
                    vGridControl = New DevExpress.XtraVerticalGrid.VGridControl
                    rowPadre = New DevExpress.XtraVerticalGrid.Rows.CategoryRow
                    vGridControl.Rows.Add(rowPadre)
                    popupContainerMoreInfo.Controls.Add(vGridControl)
                    'popupContainerMoreInfo.Padding = New System.Windows.Forms.Padding(10)
                    RepositoriPopupMoreInfo.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.True
                    RepositoriPopupMoreInfo.ShowPopupCloseButton = False
                    RepositoriPopupMoreInfo.PopupSizeable = False
                    RepositoriPopupMoreInfo.PopupControl = popupContainerMoreInfo
                    ColMoreInfo.ColumnEdit = RepositoriPopupMoreInfo

                    'vGridControl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
                    vGridControl.Appearance.RowHeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    vGridControl.Appearance.RowHeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
                    vGridControl.Appearance.RowHeaderPanel.ForeColor = Color.White

                    vGridControl.Appearance.VertLine.BackColor = Color.White
                    vGridControl.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
                    vGridControl.Appearance.RowHeaderPanel.Options.UseFont = True
                    'vGridControl.Appearance.RowHeaderPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
                    vGridControl.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
                    'vGridControl.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
                    'vGridControl.Appearance.FocusedRow.ForeColor = Color.White
                    vGridControl.RecordWidth = 195
                    vGridControl.RowHeaderWidth = 150
                    vGridControl.Padding = New Padding(5)
                    vGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                    rowPadre.Properties.Caption = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("CaptionRowCategory", Me.GetType())
                    rowPadre.OptionsRow.DblClickExpanding = False
                    rowPadre.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
                    rowPadre.Appearance.BackColor = Color.White

                End If
                ContadorColVacias = ContadorColVacias + 1
                rowHija = New DevExpress.XtraVerticalGrid.Rows.EditorRow
                rowHija.Properties.FieldName = col.FieldName
                rowHija.Properties.RowEdit = col.ColumnEdit
                rowHija.Properties.ReadOnly = True
                rowHija.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                rowPadre.ChildRows.Add(rowHija)
                rowHija.Properties.Caption = col.Caption
                rowHija.Expanded = True
                rowHija.OptionsRow.AllowSize = False
            End If
        Next
        If ColMoreInfo IsNot Nothing Then
            ColMoreInfo.Caption = "+ Info"
            ColMoreInfo.Visible = True
            ColMoreInfo.Name = "MoreInfo"
            _view.Columns.Add(ColMoreInfo)
            vGridControl.Dock = DockStyle.Fill
            If ContadorColVacias < 5 Then
                heightPopup = ContadorColVacias * 50
            Else
                heightPopup = ContadorColVacias * 25
            End If

            popupContainerMoreInfo.Size = New System.Drawing.Size(350, heightPopup + 8)
            RepositoriPopupMoreInfo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
            RepositoriPopupMoreInfo.Buttons(0).Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Combo
        End If
    End Sub

#End Region

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Initializes a new instance of the <see cref="IndigoGridControl"/> class.
    ''' </summary>
    Public Sub New()
        Hashtable = New Hashtable()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="IndigoGridControl"/> class.
    ''' </summary>
    ''' <param name="container">The container.</param>
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        Me.New()
        If container IsNot Nothing Then
            container.Add(Me)
        End If
    End Sub

    ''' <summary>
    ''' Initializes the component.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IndigoGridView))
        Me.PopupMenuActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.BarManagerActions = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        CType(Me.PopupMenuActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManagerActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PopupMenuActions
        '
        Me.PopupMenuActions.Manager = Me.BarManagerActions
        Me.PopupMenuActions.Name = "PopupMenuActions"
        '
        'BarManagerActions
        '
        Me.BarManagerActions.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3})
        Me.BarManagerActions.MaxItemId = 3
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "BarButtonItem1"
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "BarButtonItem2"
        Me.BarButtonItem2.Id = 1
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "BarButtonItem3"
        Me.BarButtonItem3.Id = 2
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'ImageCollection1
        '
        'Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        'Me.ImageCollection1.InsertGalleryImage("add_16x16.png", "images/actions/add_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/add_16x16.png"), 0)

        'Me.ImageCollection1.Images.SetKeyName(0, "add_16x16.png")

        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.IconoEliminar)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.IconoEditar)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Acceptance)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Reject)
        CType(Me.PopupMenuActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManagerActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    ''' <summary>
    ''' Libera los recursos no administrados que utiliza <see cref="T:System.ComponentModel.Component" /> y libera los recursos administrados de forma opcional.
    ''' </summary>
    ''' <param name="disposing">Es true para liberar tanto recursos administrados como no administrados; es false para liberar únicamente recursos no administrados.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Contenedor IsNot Nothing Then
                Contenedor.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ''' <summary>
    ''' Especifica si este objeto puede proporcionar las propiedades Extender al objeto especificado.
    ''' </summary>
    ''' <param name="extendee"><see cref="T:System.Object" /> para recibir las propiedades Extender.</param>
    ''' <returns>
    ''' truesi este objeto puede proporcionar propiedades extensoras al objeto especificado; en caso contrario, false.
    ''' </returns>
    Public Function CanExtend(ByVal extendee As Object) As Boolean Implements System.ComponentModel.IExtenderProvider.CanExtend
        If TypeOf extendee Is GridView Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Evento que se utiliza dar la apariencia al autofilterrow
    ''' </summary>
    Private Sub INDGridControl_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        If e.RowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            e.Appearance.BackColor = Color.LightGray
            e.Appearance.BackColor2 = Color.LightGray
        End If
    End Sub

    ''' <summary>
    ''' Valido que todas las columnas que esten tratando de formatear a currency lo hagan con la cultura del archivo de configuracion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGridControl_CustomRowCellEdit(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs)
        If e IsNot Nothing Then
            If e.Column.DisplayFormat.FormatString IsNot Nothing AndAlso e.Column.DisplayFormat.FormatString.Length > 0 AndAlso e.Column.DisplayFormat.FormatString.Substring(0, 1).Equals("C", StringComparison.OrdinalIgnoreCase) Then
                Dim numberDigits = e.Column.DisplayFormat.FormatString.Substring(1)
                If numberDigits.Length = 0 Then
                    numberDigits = "0"
                End If
                If e.CellValue IsNot Nothing Then
                    If Not IsNumeric(numberDigits) Or Not IsNumeric(e.CellValue) Then
                        e.DisplayText = e.CellValue.ToString()
                    Else
                        Dim value = Decimal.Parse(e.CellValue)
                        e.Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                        e.Column.AppearanceCell.Options.UseTextOptions = True
                        e.DisplayText = value.MoneyFormat(numberDigits)
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' metodo para agregar la columna de acciones a la vista de la rejilla
    ''' </summary>
    ''' <param name="view"></param>
    ''' <remarks></remarks>
    Private Sub AddColumnActions(ByVal view As GridView)
        Dim colAccion As New GridColumn
        colAccion.Name = "colActions"
        colAccion.Visible = True
        colAccion.Caption = "Acciones"
        view.Columns.Add(colAccion)
    End Sub

    ''' <summary>
    ''' metodo para hacer el llamado del evento click dek boton de la accion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnActions_Click(sender As Object, e As EventArgs)
        RaiseEvent Click_ButtonAction(sender, e)
    End Sub

    ' ''' <summary>
    ' ''' metodo diseñado para agregar las columnas de mas info
    ' ''' </summary>
    ' ''' <param name="_view"></param>
    ' ''' <remarks></remarks>
    'Public Sub MoreInfoColunmns(ByVal _view As GridView)
    '    Dim ColMoreInfo As GridColumn = Nothing
    '    Dim popupContainerMoreInfo As PopupContainerControl = Nothing
    '    Dim RepositoriPopupMoreInfo As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = Nothing
    '    Dim vGridControl As DevExpress.XtraVerticalGrid.VGridControl = Nothing
    '    Dim rowPadre As DevExpress.XtraVerticalGrid.Rows.EditorRow = Nothing
    '    Dim rowHija As DevExpress.XtraVerticalGrid.Rows.CategoryRow
    '    For Each col As GridColumn In _view.Columns
    '        If col.Visible = False Then
    '            If ColMoreInfo Is Nothing Then
    '                ColMoreInfo = New GridColumn
    '                popupContainerMoreInfo = New PopupContainerControl
    '                RepositoriPopupMoreInfo = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    '                vGridControl = New DevExpress.XtraVerticalGrid.VGridControl
    '                rowPadre = New DevExpress.XtraVerticalGrid.Rows.EditorRow
    '                vGridControl.Rows.Add(rowPadre)
    '                popupContainerMoreInfo.Controls.Add(vGridControl)
    '                vGridControl.Dock = DockStyle.Fill
    '                RepositoriPopupMoreInfo.ShowPopupCloseButton = False
    '                RepositoriPopupMoreInfo.PopupSizeable = False
    '                RepositoriPopupMoreInfo.PopupControl = popupContainerMoreInfo
    '                ColMoreInfo.ColumnEdit = RepositoriPopupMoreInfo

    '            End If
    '            rowHija = New DevExpress.XtraVerticalGrid.Rows.CategoryRow
    '            rowHija.Properties.FieldName = col.FieldName
    '            rowHija.Properties.RowEdit = col.ColumnEdit
    '            rowPadre.ChildRows.Add(rowHija)
    '            rowHija.Properties.Caption = col.Caption
    '        Else
    '            Continue For
    '        End If
    '    Next
    '    If ColMoreInfo IsNot Nothing Then
    '        ColMoreInfo.Caption = "Mas Info"
    '        ColMoreInfo.Visible = True
    '        _view.Columns.Add(ColMoreInfo)
    '    End If
    'End Sub
#End Region

#Region "Handlers"

    Private Sub PopUpContainerEdit_QueryPopUp(sender As Object, e As CancelEventArgs)
        Dim popUp As DevExpress.XtraEditors.PopupContainerEdit = CType(sender, DevExpress.XtraEditors.PopupContainerEdit)
        If popUp.Parent IsNot Nothing Then
            Dim view As GridView = CType(CType(popUp.Parent, GridControl).MainView, GridView)
            Dim obj() As Object = New Object() {If(view.GetFocusedRow().GetType().Equals(GetType(DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)), CType(view.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, view.GetFocusedRow())}
            If popUp.Properties.PopupControl IsNot Nothing AndAlso popUp.Properties.PopupControl.Controls IsNot Nothing AndAlso popUp.Properties.PopupControl.Controls.Count > 0 AndAlso popUp.Properties.PopupControl.Controls(0).GetType().Equals(GetType(DevExpress.XtraVerticalGrid.VGridControl)) Then
                Dim vgrid As DevExpress.XtraVerticalGrid.VGridControl = CType(popUp.Properties.PopupControl.Controls(0), DevExpress.XtraVerticalGrid.VGridControl)
                vgrid.DataSource = obj
                vgrid.RefreshDataSource()
            End If
        End If
    End Sub

    Private Sub PopUpContainerEdit_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs)
        Dim popUp As DevExpress.XtraEditors.PopupContainerEdit = CType(sender, DevExpress.XtraEditors.PopupContainerEdit)
        If popUp.Parent IsNot Nothing Then
            Dim view As GridView = CType(CType(popUp.Parent, GridControl).MainView, GridView)
            If popUp.Properties.PopupControl IsNot Nothing AndAlso popUp.Properties.PopupControl.Controls IsNot Nothing AndAlso popUp.Properties.PopupControl.Controls.Count > 0 AndAlso popUp.Properties.PopupControl.Controls(0).GetType().Equals(GetType(DevExpress.XtraVerticalGrid.VGridControl)) Then
                Dim vgrid As DevExpress.XtraVerticalGrid.VGridControl = CType(popUp.Properties.PopupControl.Controls(0), DevExpress.XtraVerticalGrid.VGridControl)
                vgrid.DataSource = Nothing
            End If
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Indica al objeto que se ha completado la inicialización.
    ''' </summary>
    Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        Dim p As GridView = Nothing
        'Debug.WriteLine("")
        'Debug.WriteLine("/****************IndigoGridView***************/")
        For Each de As DictionaryEntry In Hashtable
            p = TryCast(de.Key, GridView)
            Debug.WriteLine(Hashtable.Count().ToString() & " - " & p.Name)
            p.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            p.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
            p.Appearance.GroupRow.Options.UseFont = True
            p.Appearance.GroupRow.Options.UseForeColor = True
            p.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            p.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
            p.Appearance.HeaderPanel.Options.UseFont = True
            p.Appearance.HeaderPanel.Options.UseForeColor = True
            p.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
            p.Appearance.Row.Options.UseFont = True
            p.OptionsView.EnableAppearanceEvenRow = True
            p.OptionsView.EnableAppearanceOddRow = True
            p.OptionsView.ShowAutoFilterRow = True
            If DesignMode = False Then
                AddHandler p.CustomDrawCell, AddressOf INDGridControl_CustomRowCellEdit
                AddHandler p.RowStyle, AddressOf INDGridControl_RowStyle
            End If
        Next
        'Debug.WriteLine("/*********************************************/")
        'Debug.WriteLine("")
    End Sub

    ''' <summary>
    ''' Indica al objeto que está comenzando la inicialización.
    ''' </summary>
    Public Sub BeginInit() Implements ISupportInitialize.BeginInit

    End Sub

    ''' <summary>
    ''' Método publico utilizado para crear el menú contextual
    ''' </summary>
    ''' <param name="view">The view.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    Private Sub SetListContextMenu(ByVal view As GridView, value As List(Of Presentation.Base.eAcciones))
        _listActions = value
        AddHandler view.PopupMenuShowing, AddressOf INDGridView_PopupMenuShowing
    End Sub

    Private Sub CreatePopUpMenu(_parentForm As XtraForm)
        Dim DockManager = New DevExpress.XtraBars.Docking.DockManager(Me.Container)
        Me.PopupMenuActions.Manager = Me.BarManagerActions
        Me.PopupMenuActions.Name = "PopupMenuActions"
        '_listActions.Reverse()
        For Each action As Presentation.Base.eAcciones In _listActions
            Dim BarButtonItem = New DevExpress.XtraBars.BarButtonItem()
            BarButtonItem.Name = "BBI" & Convert.ToInt32(action)
            BarButtonItem.Caption = Infrastructure.CrossCutting.Resources.ResourceManager.GetString(action.ToString())
            BarButtonItem.Glyph = ImageCollection1.Images(action)
            BarButtonItem.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
            BarButtonItem.ItemAppearance.Disabled.Options.UseFont = True
            BarButtonItem.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
            BarButtonItem.ItemAppearance.Hovered.Options.UseFont = True
            BarButtonItem.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
            BarButtonItem.ItemAppearance.Normal.Options.UseFont = True
            BarButtonItem.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
            BarButtonItem.ItemAppearance.Pressed.Options.UseFont = True
            BarButtonItem.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
            BarButtonItem.ItemInMenuAppearance.Disabled.Options.UseFont = True
            BarButtonItem.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
            BarButtonItem.ItemInMenuAppearance.Hovered.Options.UseFont = True
            BarButtonItem.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
            BarButtonItem.ItemInMenuAppearance.Normal.Options.UseFont = True
            BarButtonItem.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
            BarButtonItem.ItemInMenuAppearance.Pressed.Options.UseFont = True

            'BarButtonItem.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E Or Keys.A))

            BarButtonItem.Tag = action.ToString()

            BarButtonItem.Width = 300
            BarButtonItem.LargeWidth = 300
            BarButtonItem.SmallWithoutTextWidth = 300
            BarButtonItem.SmallWithTextWidth = 300
            Me.PopupMenuActions.ItemLinks.Add(BarButtonItem)
            BarManagerActions.Items.Add(BarButtonItem)

            AddHandler BarButtonItem.ItemClick, AddressOf ContexMenuActions_PopUp_Click
        Next
        Me.BarManagerActions.DockManager = DockManager
        Me.BarManagerActions.Form = _parentForm

        DockManager.Form = _parentForm
        DockManager.MenuManager = BarManagerActions
        DockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})

    End Sub

    ''' <summary>
    ''' Handles the PopupMenuShowing event of the INDGridControl control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs"/> instance containing the event data.</param>
    Private Sub INDGridView_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs)
        Dim view = CType(sender, GridView)
        Dim hitInfo As GridHitInfo = view.CalcHitInfo(e.Point)
        If hitInfo.InRowCell Then
            view.FocusedRowHandle = hitInfo.RowHandle
            PopupMenuActions.Manager = BarManagerActions
            PopupMenuActions.ShowPopup(view.GridControl.PointToScreen(e.Point))
        End If
        'Dim view As GridView = CType(sender, GridView)
        'If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
        '    e.Menu.Items.Clear()
        '    _listActions.Reverse()
        '    For Each action As eAcciones In _listActions
        '        Dim nombreAccion As String
        '        nombreAccion = action.ToString()
        '        Dim nombreTexto As String = Infrastructure.CrossCutting.Resources.ResourceManager.GetString(nombreAccion)
        '        Dim ItemMenu As DXMenuItem = New DXMenuItem(nombreTexto, AddressOf ContexMenuActions_Click)
        '        ItemMenu.Tag = Infrastructure.CrossCutting.Resources.ResourceManager.GetString(nombreAccion)
        '        e.Menu.Items.Add(ItemMenu)
        '    Next
        'End If
    End Sub

    ' ''' <summary>
    ' ''' Handles the Click event of the ContexMenuActions control.
    ' ''' </summary>
    ' ''' <param name="sender">The source of the event.</param>
    ' ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    'Private Sub ContexMenuActions_Click(sender As Object, e As EventArgs)
    '    RaiseEvent ContexMenuActions(sender, e)
    'End Sub

    Private Sub ContexMenuActions_PopUp_Click(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        RaiseEvent ContexMenuActions(e.Item, e)
    End Sub

End Class