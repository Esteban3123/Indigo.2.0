Imports DevExpress.Xpo

Public Class CtrNavigationRecord

    Protected Overrides Sub OnResize(e As EventArgs)
        Me.Size = New Size(232, 24)
        Me.MinimumSize = New Size(232, 24)
        Me.MaximumSize = New Size(232, 24)
        MyBase.OnResize(e)
    End Sub


    ''' <summary>
    ''' Evento que se dispara cuando se cambia de registro en la navegacion de la barra
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <remarks></remarks>
    Public Event RecordNavigationChangeEvent(ByVal Record As Object)

#Region "GLOBALS"
    ''' <summary>
    ''' Variable que establece la posicion del registro visible
    ''' </summary>
    ''' <remarks></remarks>
    Private RecordPosition As Integer

#End Region

#Region "PROPERTIES"
    Dim _ColumnInfo As List(Of ColumnInfo) = Nothing
    ''' <summary>
    ''' Obtiene o asigna la lista de columnas que se motraran
    ''' y enlazaran en el datasource de la rejilla
    ''' </summary>
    ''' <value>Lista de columnas a enlazar</value>
    ''' <returns>Lista de columnas enlazadas</returns>
    Public Property ColumnInfo As List(Of ColumnInfo)
        Get
            Return Me._ColumnInfo
        End Get
        Set(value As List(Of ColumnInfo))
            Me._ColumnInfo = value
            INDGvDetail.Columns.Clear()
            If Me._ColumnInfo IsNot Nothing Then
                For Each ci As ColumnInfo In Me._ColumnInfo
                    INDGvDetail.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn())
                    INDGvDetail.Columns(INDGvDetail.Columns.Count - 1).Name = "Col" & INDGvDetail.Columns.Count - 1
                    INDGvDetail.Columns(INDGvDetail.Columns.Count - 1).Caption = ci.Caption.Trim()
                    INDGvDetail.Columns(INDGvDetail.Columns.Count - 1).FieldName = ci.FieldName.Trim()
                    INDGvDetail.Columns(INDGvDetail.Columns.Count - 1).Visible = ci.Visible
                    INDGvDetail.Columns(INDGvDetail.Columns.Count - 1).OptionsColumn.AllowEdit = ci.AllowEdit
                    INDGvDetail.Columns(INDGvDetail.Columns.Count - 1).AppearanceCell.TextOptions.HAlignment = ci.ColumnAligment
                    INDGvDetail.Columns(INDGvDetail.Columns.Count - 1).Width = ci.ColumnWidth

                Next
            End If
        End Set
    End Property


    ''' <summary>
    ''' Propiedad que obtiene o establece el datasource para poder navegar a traves de registros
    ''' </summary>
    Private _FilterDataSource As xpcollection
    ''' <summary>
    ''' Propiedad que obtiene o establece el datasource para poder navegar a traves de registros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilterDataSource As XPCollection
        Get
            Return _FilterDataSource
        End Get
        Set(value As XPCollection)
            RecordPosition = 0
            _FilterDataSource = value
            INDGcDetail.DataSource = value
            If value IsNot Nothing AndAlso value.Count > 1 Then
                LoadControlsNavigation()
            
            End If
        End Set
    End Property
#End Region

#Region "METHODS"
    ''' <summary>
    ''' Metodo para msotrar o ocultar los controles de navegacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControlsNavigation()
        Select Case RecordPosition
            Case FilterDataSource.Count - 1
                INDBtnNext.Enabled = False
                INDBtnLast.Enabled = False
                INDBtnBack.Enabled = True
                INDBtnFirst.Enabled = True
            Case 0
                INDBtnBack.Enabled = False
                INDBtnFirst.Enabled = False
                INDBtnNext.Enabled = True
                INDBtnLast.Enabled = True
            Case Else
                INDBtnBack.Enabled = True
                INDBtnFirst.Enabled = True
                INDBtnNext.Enabled = True
                INDBtnLast.Enabled = True
        End Select
    End Sub
#End Region

    Private Sub INDBtnFirst_Click(sender As Object, e As EventArgs) Handles INDBtnFirst.Click
        RecordPosition = 0
        LoadControlsNavigation()
        RaiseEvent RecordNavigationChangeEvent(FilterDataSource(RecordPosition))
    End Sub

    Private Sub INDBtnBack_Click(sender As Object, e As EventArgs) Handles INDBtnBack.Click
        RecordPosition -= 1
        LoadControlsNavigation()
        RaiseEvent RecordNavigationChangeEvent(FilterDataSource(RecordPosition))
    End Sub

    Private Sub INDBtnNext_Click(sender As Object, e As EventArgs) Handles INDBtnNext.Click
        RecordPosition += 1
        LoadControlsNavigation()
        RaiseEvent RecordNavigationChangeEvent(FilterDataSource(RecordPosition))
    End Sub

    Private Sub INDBtnLast_Click(sender As Object, e As EventArgs) Handles INDBtnLast.Click
        RecordPosition = FilterDataSource.Count - 1
        LoadControlsNavigation()
        RaiseEvent RecordNavigationChangeEvent(FilterDataSource(RecordPosition))
    End Sub

    Private Sub INDGcDetail_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDGcDetail.MouseDoubleClick
        Dim hitInfo = INDGvDetail.CalcHitInfo(e.Location)
        If hitInfo.InDataRow Then
            RecordPosition = FilterDataSource.IndexOf(INDGvDetail.GetFocusedRow())
            LoadControlsNavigation()
            PopupControlContainer1.HidePopup()
            RaiseEvent RecordNavigationChangeEvent(FilterDataSource(RecordPosition))
        End If

    End Sub
End Class
