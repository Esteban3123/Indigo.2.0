Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Resources

Public Class CtrYesNo
    Inherits DevExpress.XtraEditors.GridLookUpEdit
    Implements ISupportInitialize

    Public Sub New()
        InitializeComponent()
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.INDGcYesNo = New DevExpress.XtraGrid.Columns.GridColumn()
    End Sub

    ''' <summary>
    ''' listado de las naturalezas de las cuentas
    ''' </summary>
    Dim listYesNo As List(Of Tuple(Of Boolean, String))
    ''' <summary>
    ''' obtiene las naturales de las cuentas 
    ''' </summary>
    ''' <value>
    ''' The nature.
    ''' </value>
    ReadOnly Property YesNo As List(Of Tuple(Of Boolean, String))
        Get
            If listYesNo Is Nothing Then
                listYesNo = New List(Of Tuple(Of Boolean, String))
                listYesNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
                listYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))
            End If
            Return listYesNo
        End Get
    End Property

    Friend WithEvents INDGcYesNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CtrYesNoView As DevExpress.XtraGrid.Views.Grid.GridView

    Public Sub BeginInit() Implements ISupportInitialize.BeginInit

    End Sub

    Public Sub EndInit() Implements ISupportInitialize.EndInit
        CtrYesNoView = Me.Properties.View
        Me.Properties.ValueMember = "Item1"
        Me.Properties.DisplayMember = "Item2"
        CtrYesNoView.Columns.Clear()
        CtrYesNoView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGcYesNo})
        CtrYesNoView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        CtrYesNoView.OptionsSelection.EnableAppearanceFocusedCell = False
        CtrYesNoView.OptionsView.ShowDetailButtons = False
        CtrYesNoView.OptionsView.ShowGroupPanel = False
        Me.Properties.View = CtrYesNoView

        Me.Properties.DataSource = YesNo

        Me.INDGcYesNo.AppearanceHeader.Options.UseTextOptions = True

        Me.INDGcYesNo.Caption = "Selección"
        Me.INDGcYesNo.FieldName = "Item2"

        Me.INDGcYesNo.Visible = True
        Me.INDGcYesNo.VisibleIndex = 0
    End Sub
End Class
