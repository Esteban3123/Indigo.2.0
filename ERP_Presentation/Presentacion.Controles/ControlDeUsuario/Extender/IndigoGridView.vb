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
<ProvideProperty("TemaIndigoMetro", GetType(GridView))>
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
    ''' Occurs when [query pop up action buttons].
    ''' </summary>
    Public Event QueryPopUpActionButtons(ByVal sender As Object, ByVal e As QueryPopUpActionButtonsEventArgs)

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
    ''' Indica si se ordena los items del menu contextual
    ''' </summary>
    Private _sortMenuContext As Boolean
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

    Public WithEvents RepositoryItemPopupContainerEdit As New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Private WithEvents popupContainer As New PopupContainerControl

    Friend WithEvents PopupMenuActions As DevExpress.XtraBars.PopupMenu
    Public WithEvents BarManagerActions As DevExpress.XtraBars.BarManager
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
    <Description("Propiedad que especifica si la rejilla tiene tema indigo metro")>
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
    Public Event QueryPopupActions()

    ''' <summary>
    ''' metodo para crear los botones de las acciones
    ''' IMPORTANTE: Para que este método funcione la vista debe ser editable, al igual que la columna.
    ''' </summary>
    ''' <param name="_view"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub SetListAcction(ByVal _view As GridView, value As List(Of Presentation.Base.eAcciones), Optional createMenuContext As Boolean = True, Optional sortMenuContext As Boolean = True)
        Dim internalValue As New List(Of Presentation.Base.eAcciones)
        For Each item In value
            internalValue.Add(item)
        Next

        'Asignacion de la lista de acciones al menu contextual
        If createMenuContext Then
            _sortMenuContext = sortMenuContext
            SetListContextMenu(_view, internalValue)
            CreatePopUpMenu(CType(_view.GridControl, DevExpress.XtraGrid.GridControl).TopLevelControl)
        End If

        If _view.Columns.Any(Function(m) m.Name.Equals("colActions")) Then
            _view.Columns.Remove(_view.Columns.ColumnByName("colActions"))
        End If

        AddColumnActions(_view)
        Dim col As GridColumn
        If internalValue Is Nothing Then
            _view.Columns.ColumnByName("colActions").Visible = False
        ElseIf internalValue.Count = 1 Then
            Dim RepositoryItemButtonEdit1 As New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
            Dim font_ As New Font("Segoe UI", 9.75, FontStyle.Underline, GraphicsUnit.Point, 1)

            col = _view.Columns.ColumnByName("colActions")
            col.ColumnEdit = RepositoryItemButtonEdit1
            col.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
            col.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
            col.AppearanceCell.Options.UseFont = True
            col.AppearanceCell.Options.UseForeColor = True
            col.AppearanceCell.Options.UseTextOptions = True
            col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            RepositoryItemButtonEdit1.Buttons.RemoveAt(0)
            RepositoryItemButtonEdit1.Appearance.Font = font_
            RepositoryItemButtonEdit1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
            RepositoryItemButtonEdit1.Appearance.Options.UseFont = True
            RepositoryItemButtonEdit1.Appearance.Options.UseForeColor = True
            RepositoryItemButtonEdit1.Appearance.Options.UseTextOptions = True
            RepositoryItemButtonEdit1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            RepositoryItemButtonEdit1.AppearanceDisabled.Font = font_
            RepositoryItemButtonEdit1.AppearanceDisabled.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
            RepositoryItemButtonEdit1.AppearanceDisabled.Options.UseFont = True
            RepositoryItemButtonEdit1.AppearanceDisabled.Options.UseForeColor = True
            RepositoryItemButtonEdit1.AppearanceDisabled.Options.UseTextOptions = True
            RepositoryItemButtonEdit1.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            RepositoryItemButtonEdit1.AppearanceFocused.Font = font_
            RepositoryItemButtonEdit1.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
            RepositoryItemButtonEdit1.AppearanceFocused.Options.UseFont = True
            RepositoryItemButtonEdit1.AppearanceFocused.Options.UseForeColor = True
            RepositoryItemButtonEdit1.AppearanceFocused.Options.UseTextOptions = True
            RepositoryItemButtonEdit1.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            RepositoryItemButtonEdit1.AppearanceReadOnly.Font = font_
            RepositoryItemButtonEdit1.AppearanceReadOnly.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
            RepositoryItemButtonEdit1.AppearanceReadOnly.Options.UseFont = True
            RepositoryItemButtonEdit1.AppearanceReadOnly.Options.UseForeColor = True
            RepositoryItemButtonEdit1.AppearanceReadOnly.Options.UseTextOptions = True
            RepositoryItemButtonEdit1.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            RepositoryItemButtonEdit1.AutoHeight = False
            RepositoryItemButtonEdit1.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
            RepositoryItemButtonEdit1.NullText = Infrastructure.CrossCutting.Resources.ResourceManager.GetString(internalValue(0).ToString)
            RepositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            RepositoryItemButtonEdit1.Name = "RepositoryItemButtonEditActions"
            RepositoryItemButtonEdit1.Tag = internalValue(0).ToString()
            If RepositoryItemButtonEdit1 IsNot Nothing Then
                AddHandler RepositoryItemButtonEdit1.Click, AddressOf BtnActions_Click
            End If

        Else
            RepositoryItemPopupContainerEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
            RepositoryItemPopupContainerEdit.Buttons(0).Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Combo

            internalValue.Reverse()
            Dim width As Integer = 0
            For Each action As eAcciones In internalValue
                btn = New SimpleButton
                Dim nombreAccion As String
                nombreAccion = action.ToString()
                btn.Text = Infrastructure.CrossCutting.Resources.ResourceManager.GetString(nombreAccion)

                If btn.Text.Length > width Then
                    width = btn.Text.Length
                End If

                btn.Size = New System.Drawing.Size(200, 36)
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
            _sizePopup = 36 * internalValue.Count
            popupContainer.Size = New System.Drawing.Size(width * 6, _sizePopup)
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

        If _view.Columns.Any(Function(o) o.Name.Equals("MoreInfo")) Then
            ColMoreInfo = _view.Columns.FirstOrDefault(Function(o) o.Name.Equals("MoreInfo"))
            rowPadre = vGridControl.Rows.Item(0)
            rowPadre.ChildRows.Clear()
            popupContainerMoreInfo = vGridControl.Parent
            RepositoriPopupMoreInfo = ColMoreInfo.ColumnEdit
        End If

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
                    RepositoriPopupMoreInfo.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.True
                    RepositoriPopupMoreInfo.ShowPopupCloseButton = False
                    RepositoriPopupMoreInfo.PopupSizeable = False
                    RepositoriPopupMoreInfo.PopupControl = popupContainerMoreInfo
                    ColMoreInfo.ColumnEdit = RepositoriPopupMoreInfo

                    vGridControl.Appearance.RowHeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    ''vGridControl.Appearance.RowHeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
                    vGridControl.Appearance.RowHeaderPanel.ForeColor = Color.White

                    vGridControl.Appearance.VertLine.BackColor = Color.White
                    ''vGridControl.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
                    vGridControl.Appearance.RowHeaderPanel.Options.UseFont = True
                    vGridControl.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
                    vGridControl.RecordWidth = 195
                    vGridControl.RowHeaderWidth = 150
                    vGridControl.Padding = New Padding(5)
                    vGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                    rowPadre.Properties.Caption = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("CaptionRowCategory", Me.GetType())
                    rowPadre.OptionsRow.DblClickExpanding = False
                    rowPadre.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
                    ''rowPadre.Appearance.BackColor = Color.MistyRose

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
        Me.BarManagerActions.MaxItemId = 3
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "BarButtonItem1"
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Tag = "BarButtonItem1"
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "BarButtonItem2"
        Me.BarButtonItem2.Id = 1
        Me.BarButtonItem2.Tag = "BarButtonItem2"
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "BarButtonItem3"
        Me.BarButtonItem3.Id = 2
        Me.BarButtonItem3.Tag = "BarButtonItem3"
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'ImageCollection1
        '

        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.IconoEliminar)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.IconoEditar_)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Acceptance)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Reject)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.IconoEliminar)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Acceptance)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_24x24_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)
        Me.ImageCollection1.Images.Add(Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue)

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
            'e.Appearance.BackColor = Color.LightGray
            'e.Appearance.BackColor2 = Color.LightGray
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
                    Dim value As Decimal
                    If Not IsNumeric(numberDigits) OrElse Not Decimal.TryParse(e.CellValue.ToString, value) Then
                        e.DisplayText = e.CellValue.ToString()
                    Else
                        e.Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                        e.Column.AppearanceCell.Options.UseTextOptions = True
                        e.DisplayText = value.ToString("C" & numberDigits, e.Column.DisplayFormat.Format)
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
        If TypeOf view Is Views.BandedGrid.BandedGridView Then
            Dim colAccion As New Views.BandedGrid.BandedGridColumn
            colAccion.Name = "colActions"
            colAccion.Visible = True
            colAccion.Caption = "Acciones"
            view.Columns.Add(colAccion)
        Else
            Dim colAccion As New GridColumn
            colAccion.Name = "colActions"
            colAccion.Visible = True
            colAccion.Caption = "Acciones"
            view.Columns.Add(colAccion)
        End If
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

#End Region

#Region "Handlers"

    Private Sub PopUpContainerEdit_QueryPopUp(sender As Object, e As CancelEventArgs)
        Dim popUp As DevExpress.XtraEditors.PopupContainerEdit = CType(sender, DevExpress.XtraEditors.PopupContainerEdit)
        If popUp.Parent IsNot Nothing Then

            Dim view As GridView = CType(CType(popUp.Parent, GridControl).MainView, GridView)
            Dim vgrid As DevExpress.XtraVerticalGrid.VGridControl = popUp.Properties.PopupControl.Controls(0)
            vgrid.Rows.Item(0).ChildRows.Clear()

            Dim counter As Integer = 0

            For Each colInvisible In (From c In view.Columns Where c.Visible = False Select c).ToList()

                rowHija = New DevExpress.XtraVerticalGrid.Rows.EditorRow
                rowHija.Properties.FieldName = colInvisible.FieldName
                rowHija.Properties.RowEdit = colInvisible.ColumnEdit
                rowHija.Properties.ReadOnly = True
                rowHija.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                vgrid.Rows.Item(0).ChildRows.Add(rowHija)
                rowHija.Properties.Caption = colInvisible.Caption
                rowHija.Expanded = True
                rowHija.OptionsRow.AllowSize = False
                counter += 1
            Next
            vgrid.DataSource = {view.GetFocusedRow()}.ToList()
            vgrid.RefreshDataSource()

            CType(vgrid.Parent, PopupContainerControl).Size = New System.Drawing.Size(350, (counter * 50) + 8)

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
        For Each de As DictionaryEntry In Hashtable
            p = TryCast(de.Key, GridView)
            'If DesignMode Then
            p.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            p.Appearance.GroupRow.Options.UseFont = True
            p.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            p.Appearance.HeaderPanel.Options.UseFont = True
            p.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
            p.Appearance.Row.Options.UseFont = True
            p.OptionsView.EnableAppearanceEvenRow = True
            p.OptionsView.EnableAppearanceOddRow = True
            p.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, FontStyle.Bold)
            p.Appearance.FocusedRow.BorderColor = Color.LightGray
            p.OptionsView.ShowAutoFilterRow = True
            If DesignMode = False Then
                AddHandler p.CustomDrawCell, AddressOf INDGridControl_CustomRowCellEdit
                AddHandler p.RowStyle, AddressOf INDGridControl_RowStyle
            End If
        Next
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

    Public Property RaiseMenuPopUp As Boolean = True

    Private Sub CreatePopUpMenu(_parentForm As XtraForm)
        Dim DockManager = New DevExpress.XtraBars.Docking.DockManager(Me.Container)
        Me.PopupMenuActions.Manager = Me.BarManagerActions
        Me.PopupMenuActions.Name = "PopupMenuActions"

        Me.PopupMenuActions.ItemLinks.Clear()

        Dim listActions As New List(Of Presentation.Base.eAcciones)
        For Each item In _listActions
            listActions.Add(item)
        Next
        If _sortMenuContext Then
            listActions = listActions.OrderBy(Function(x) Infrastructure.CrossCutting.Resources.ResourceManager.GetString(x.ToString())).ToList()
        End If

        For Each action As Presentation.Base.eAcciones In listActions
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
        If Not e.HitInfo.InFilterPanel AndAlso Not e.HitInfo.InGroupColumn AndAlso Not e.HitInfo.InGroupPanel AndAlso Not e.HitInfo.InGroupRow AndAlso e.HitInfo.InRow AndAlso e.HitInfo.InDataRow Then
            If RaiseMenuPopUp Then
                Dim view = CType(sender, GridView)
                Dim hitInfo As GridHitInfo = view.CalcHitInfo(e.Point)
                If hitInfo.InRowCell Then
                    view.FocusedRowHandle = hitInfo.RowHandle
                    PopupMenuActions.Manager = BarManagerActions
                    PopupMenuActions.ShowPopup(view.GridControl.PointToScreen(e.Point))
                End If
            End If
        End If
    End Sub

    Private Sub ContexMenuActions_PopUp_Click(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        If RaiseMenuPopUp Then
            RaiseEvent ContexMenuActions(e.Item, e)
        End If
    End Sub

    Private Sub RepositoryItemPopupContainerEdit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupContainerEdit.QueryPopUp
        Dim list As New List(Of SimpleButton)()
        For Each c As Control In popupContainer.Controls
            If TypeOf c Is SimpleButton Then
                list.Add(CType(c, SimpleButton))
            End If
        Next
        RaiseEvent QueryPopUpActionButtons(RepositoryItemPopupContainerEdit, New QueryPopUpActionButtonsEventArgs(list.ToArray()))
    End Sub
End Class

''' <summary>
''' Encapsula los argumentos del evento QueryPopUpActionButtons
''' </summary>
Public Class QueryPopUpActionButtonsEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Initializes a new instance of the <see cref="QueryPopUpActionButtonsEventArgs"/> class.
    ''' </summary>
    ''' <param name="buttons">The buttons.</param>
    Public Sub New(ByVal buttons As SimpleButton())
        _buttons = buttons
    End Sub

    Private _buttons As SimpleButton()
    ''' <summary>
    ''' Obtiene o asigna el arreglo de botones en las opciones
    ''' </summary>
    ''' <value>
    ''' The buttons.
    ''' </value>
    Public ReadOnly Property Buttons As SimpleButton()
        Get
            Return _buttons
        End Get
    End Property

End Class