Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Tile
Imports Domain.Security.Entities

Public Class CtrMenuForm
    Public Sub New()
        InitializeComponent()
        'InitKanban()
        titles = Base.BaseClass.GetXmlWithAggregates(Of VieTitle)(Base.eDataXml.XMLTitles).ToList()
    End Sub


    Private titles As List(Of VieTitle)
    Public Enum TaskStatus
        ToDo
        Planned
        Doing
        Testing
        Done
        Transactions
    End Enum

    'Private Sub InitKanban()
    '    TileView1.OptionsKanban.ShowGroupBackground = DefaultBoolean.True
    '    TileView1.OptionsKanban.Groups.Add(New KanbanGroup() With {.GroupValue = TaskStatus.ToDo})
    '    TileView1.OptionsKanban.Groups.Add(New KanbanGroup() With {.GroupValue = TaskStatus.Planned})
    '    TileView1.OptionsKanban.Groups.Add(New KanbanGroup() With {.GroupValue = TaskStatus.Doing})
    '    TileView1.OptionsKanban.Groups.Add(New KanbanGroup() With {.GroupValue = TaskStatus.Testing})
    '    TileView1.OptionsKanban.Groups.Add(New KanbanGroup() With {.GroupValue = TaskStatus.Done})
    '    TileView1.OptionsKanban.Groups.Add(New KanbanGroup() With {.GroupValue = TaskStatus.Transactions})
    '    TileView1.OptionsKanban.Groups(4).FooterButton.Visible = DefaultBoolean.False
    '    TileView1.OptionsKanban.GroupFooterButton.Visible = DefaultBoolean.True
    '    TileView1.OptionsKanban.GroupFooterButton.Text = "Add a new card"

    '    'AddHandler TileView1.GroupFooterButtonClick, AddressOf TileView_GroupFooterButtonClick
    '    'AddHandler TileView1.GroupHeaderContextButtonClick, AddressOf TileView_GroupHeaderContextButtonClick
    '    AddHandler TileView1.CustomColumnDisplayText, AddressOf TileView_CustomColumnDisplayText
    'End Sub

    'Private Sub TileView_CustomColumnDisplayText(ByVal sender As Object, ByVal e As Views.Base.CustomColumnDisplayTextEventArgs)
    '    If e.IsForGroupRow Then
    '        Dim kanbanGroup = TileView1.GetKanbanGroupByValue(e.Value)
    '        Dim count As Integer = TileView1.GetChildRowCount(kanbanGroup)
    '        Dim cards As String = If(count = 1, " card", " cards")
    '        e.DisplayText &= "<br><size=-2><r>" & count.ToString() & cards
    '    End If
    'End Sub

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el control contiene
    ''' datos de mis favoritos
    ''' </summary>
    ''' <value>Valor que indica si el control muestra mis favoritos</value>
    ''' <returns>Un valor que indica si el control muestra mis favoritos</returns>
    Public Property IsMyFavorites As Boolean

    Public Event ItemFormSelected(form As VieDBForm)

    Public Event ItemFormDeletedFromMyFavorites(ByVal grid As GridControl, ByVal form As VieDBForm)

    Public Event ItemFormAddToMyFavorites(ByVal grid As GridControl, ByVal form As VieDBForm)

    'Private Sub GridView1_CustomDrawGroupRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs)
    '    Dim info As GridGroupRowInfo = CType(e.Info, GridGroupRowInfo)
    '    If info.Column.Name = INDcolumnType.Name Then
    '        Select Case info.EditValue
    '            Case 0
    '                info.GroupText = "Maestros"
    '            Case 1
    '                info.GroupText = "Documentos y Transacciones"
    '            Case 2
    '                info.GroupText = "Configuración"
    '            Case 3
    '                info.GroupText = "Informes y Consultas"
    '        End Select
    '    End If
    'End Sub

    Private Sub GridControl1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridControl1.KeyDown
        Dim Grid As GridControl = CType(sender, GridControl)
        Select Case e.KeyValue
            Case 65 To 90, 192, 189, 111, 48 To 57, 96 To 105, 190, 188, 109, 110, 106
                Dim kc As KeysConverter = New KeysConverter
                Grid.Tag = String.Concat(Grid.Tag, kc.ConvertToString(e.KeyData))
                CType(Grid.MainView, TileView).FindFilterText = Grid.Tag.ToString
                If CType(Grid.MainView, TileView).RowCount <= 0 Then
                    Grid.Tag = Grid.Tag.ToString.Substring(0, Grid.Tag.ToString.Length - 1)
                    CType(Grid.MainView, TileView).FindFilterText = Grid.Tag.ToString
                End If
        End Select
        If e.KeyCode = Keys.Back Then
            If Grid.Tag IsNot String.Empty AndAlso Grid.Tag IsNot Nothing Then
                Grid.Tag = Grid.Tag.ToString.Substring(0, Grid.Tag.ToString.Length - 1)
                CType(Grid.MainView, TileView).FindFilterText = Grid.Tag.ToString
            End If
        ElseIf e.KeyCode = Keys.Enter Then
            If CType(Grid.MainView, TileView).FocusedRowHandle >= 0 Then
                Dim form As VieDBForm = TileView1.GetFocusedRow()
                RaiseEvent ItemFormSelected(form)
            End If
        ElseIf e.KeyCode = Keys.Down Then
            Dim RowIndex = CType(Grid.MainView, TileView).FocusedRowHandle + 1
            CType(Grid.MainView, TileView).SelectRow(RowIndex)
        ElseIf e.KeyCode = Keys.Up Then
            Dim RowIndex = CType(Grid.MainView, TileView).FocusedRowHandle - 1
            CType(Grid.MainView, TileView).SelectRow(RowIndex)
        ElseIf e.KeyCode = Keys.Escape Then
            Grid.Tag = String.Empty
            CType(Grid.MainView, TileView).FindFilterText = Grid.Tag.ToString
        End If
    End Sub

    Private Sub TileView1_ItemClick(sender As Object, e As Views.Tile.TileViewItemClickEventArgs) Handles TileView1.ItemClick
        Dim form As VieDBForm = TileView1.GetFocusedRow()
        RaiseEvent ItemFormSelected(form)
    End Sub

    Private Sub TileView1_ItemRightClick(sender As Object, e As TileViewItemClickEventArgs) Handles TileView1.ItemRightClick
        If Me.IsMyFavorites Then
            Me.MnuAddToFavorites.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Else
            Me.MnuDeleteFromFavorites.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
        Me.PopupMenu1.ShowPopup(MousePosition)
    End Sub

    Private Sub MnuAddToFavorites_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MnuAddToFavorites.ItemClick
        Dim form As VieDBForm = TileView1.GetFocusedRow()
        RaiseEvent ItemFormAddToMyFavorites(TileView1.GridControl, form)
    End Sub

    Private Sub MnuDeleteFromFavorites_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MnuDeleteFromFavorites.ItemClick
        Dim form As VieDBForm = TileView1.GetFocusedRow()
        RaiseEvent ItemFormDeletedFromMyFavorites(TileView1.GridControl, form)
    End Sub

    Private Sub CtrMenuForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.GridControl1.ForceInitialize()
        Me.TileView1.ColumnSet.GroupColumn = Me.INDcolumnType
        Me.TileView1.ColumnSet.GroupColumn.FieldNameSortGroup = "Title.TitleOrder"
        Me.TileView1.ColumnSet.GroupColumn.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
    End Sub
End Class
