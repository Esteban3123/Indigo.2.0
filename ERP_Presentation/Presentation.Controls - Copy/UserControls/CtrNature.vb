Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Resources

Public Class CtrNature
    Inherits DevExpress.XtraEditors.GridLookUpEdit
    Implements ISupportInitialize

    Public Sub New()
        InitializeComponent()
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.INDGcNature = New DevExpress.XtraGrid.Columns.GridColumn()
    End Sub
    ''' <summary>
    ''' listado de las naturalezas de las cuentas
    ''' </summary>
    Dim listNature As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' obtiene las naturales de las cuentas 
    ''' </summary>
    ''' <value>
    ''' The nature.
    ''' </value>
    ReadOnly Property Nature As List(Of Tuple(Of Byte, String))
        Get
            If listNature Is Nothing Then
                listNature = New List(Of Tuple(Of Byte, String))
                listNature.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("AccountNatureDebit")))
                listNature.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("AccountNatureCredit")))
            End If
            Return listNature
        End Get
    End Property

    Friend WithEvents INDGcNature As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CtrNatureView As DevExpress.XtraGrid.Views.Grid.GridView

    Public Sub BeginInit() Implements ISupportInitialize.BeginInit

    End Sub

    Public Sub EndInit() Implements ISupportInitialize.EndInit
        CtrNatureView = Me.Properties.View
        Me.Properties.ValueMember = "Item1"
        Me.Properties.DisplayMember = "Item2"
        CtrNatureView.Columns.Clear()
        Me.CtrNatureView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGcNature})
        Me.CtrNatureView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CtrNatureView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CtrNatureView.OptionsView.ShowDetailButtons = False
        Me.CtrNatureView.OptionsView.ShowGroupPanel = False
        Me.Properties.View = CtrNatureView

        Me.Properties.DataSource = Nature

        Me.INDGcNature.AppearanceHeader.Options.UseTextOptions = True

        Me.INDGcNature.Caption = "Naturaleza"
        Me.INDGcNature.FieldName = "Item2"

        Me.INDGcNature.Visible = True
        Me.INDGcNature.VisibleIndex = 0
    End Sub
End Class
