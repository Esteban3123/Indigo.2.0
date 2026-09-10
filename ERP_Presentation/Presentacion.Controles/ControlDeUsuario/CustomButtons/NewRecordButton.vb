#Region "Imports"

Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

#End Region

''' <summary>
''' Representa un botón que exporta los datos
''' de un GridControl a Microsoft Excel
''' </summary>
<System.ComponentModel.ToolboxItem(False)>
Public Class NewRecordButton
    Inherits DevExpress.XtraEditors.SimpleButton

#Region "Consts"

    ''' <summary>
    ''' Icono de Microsoft Excel
    ''' </summary>
    Private Const IMAGE_PLUS As String =
        "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAAmAA
AAJgBosiCmAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAADTSURBVDiNpZO9CgIxEIS/1YCCzal
gIyj4DD6L76gvYm1pJdaKNsL5g7cWyWFMchJxIIQMm9nMMhFVJYSsdAMMAQXE7Udgrgt5+rUmcVmAsRPw0QHawIdAK2p
vufhZFlWqOIcDayVLoPacQsQLy2oNTAK+CwwC7gxcnEjl9p0BZsCooaOPvls+ek1+c6H/CjROPBsGuGFTVqeu5oug9gT
cvbMAVwmjLCstgC3xYA/A1IkbbCrLKMp8saULKXOKf5rLL38hmc6UhQt2qA/esRZgnxJ4AflkM2bCodEHAAAAAElFTkSuQmCC"
    '    "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABmJLR0QA/wD/AP+gvaeTAAACFUlEQVQ4jZV
    'TS2gTURQ980mTmTeTTHTSgpaAQgoRDZSuajeCC3cuiu1CsAvBVFcquCpdDAhuFOJKjXQlCmIoCOJS6bKCCiqk8QtKECQxmc970ySTZFxoxmgSaM/yvnPOPfdxL4cR
    'yOYNuYVmEgBYvPG1sJjbHsbj/i9cenhtXtfUbJSQdJyo4wBgus4Pi7JStebcuXl65fFQg2OGIc5Ok7XMgYMLe6OaPKxb1bLZ2y8fC5tv3HMbhtEGALH3ODtN1o4e
    'OrIUDoUCUxL67cM8FwCgx6Jk7nBmiePe+RvAWQAQerFnplKrqkzG+jtOyAlIYgR2ywlqoiBwqiSl9h/PFF8Unr/nAUDX1Oyo2MOgxzQloUbPAwB/5t4VoqlKeqfiH
    'jRFTWfzhiz6Hj8Zk5REb+Z4WPtLimgDwnrTBPNcaERJNLjmJM93BB8c/N0m6Po++FDbFyN+uGwyWp2I70kyzw1+vB9l+n2gZjNaYaRd5u8uG65JneJuE9QZLRYWc9
    's8APys0XzVstlOxRXLpHVq3wb+7MHm+rNS6sRMap+eyIiCECxSx++g0WnA63qBuNFqdV9/2rp/fX7lRmAAAGJ67imEelKVpJQciYwBgNf1/hFXLJO++lB68Ln1crl
    'YKPrAkGO6uH71pE5iFzSipGNEHQcH36JOxWR0q0btW7lTq0/6+QMGPSw8uiyFnVASAJqq923UOf8CLgPKLeTeF58AAAAASUVORK5CYII="

#End Region

#Region "Fields"

    ''' <summary>
    ''' GridControl del cual se va a exportar
    ''' los datos a Microsoft Excel
    ''' </summary>
    Private _gridControl As GridControl

    ''' <summary>
    ''' GridView del cual se va exportar
    ''' los datos a Microsoft Excel
    ''' </summary>
    Private _gridView As GridView

    Public Event NewRecordClick(sender As Object, e As EventArgs)
#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el control es visible
    ''' </summary>
    ''' <value>Valor que indica si el control se hace visible</value>
    ''' <returns>Un valor que indica si el control es visible</returns>
    Public Overloads Property Visible As Boolean
        Get
            Return MyBase.Visible
        End Get
        Set(value As Boolean)
            If value Then
                If Me._gridView IsNot Nothing AndAlso Me._gridView.OptionsView.ShowFooter Then
                    MyBase.Visible = value
                End If
            Else
                MyBase.Visible = value
            End If
        End Set
    End Property

#End Region

#Region "Builders"
    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="grid">GridControl que se va a exportar</param>
    Public Sub New(ByVal grid As GridControl)
        MyBase.New()
        Me.Inicializate()
        Me._gridControl = grid
        Me._gridView = grid.MainView
        AddHandler _gridView.CustomDrawFooter, AddressOf GridView_CustomDrawFooter
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa los controles comunes
    ''' </summary>
    Private Sub Inicializate()
        Me.Name = Me.GetType().Name & "_" & DateTime.Now.Ticks.ToString()
        Text = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("AddRecord")
        Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
        Me.Image = New Bitmap(New System.IO.MemoryStream(Convert.FromBase64String(IMAGE_PLUS)))
        'Me.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        'Me.Size = New System.Drawing.Size(34, 28)
        Me.ToolTip = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("AddRecord")
    End Sub

    ''' <summary>
    ''' Agrega y posiciona el botón en el GridControl
    ''' </summary>
    Private Sub AddButtonOnGrid(ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs)
        If Me._gridControl IsNot Nothing Then
            Me._gridControl.Controls.Add(Me)
        End If
        If Me._gridView IsNot Nothing Then
            Dim pInfo = Me._gridView.GetType().GetField("fViewInfo", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
            Dim xWidth As Integer = 0
            Dim xHeight As Integer = 0
            If pInfo IsNot Nothing Then
                Dim col = DirectCast(pInfo.GetValue(Me._gridView), GridViewInfo)
                xWidth = col.ViewRects.IndicatorWidth
                xHeight = col.ViewRects.RowsTotalHeight
            End If
            If Me._gridControl IsNot Nothing Then
                Me.Anchor = AnchorStyles.Top Or AnchorStyles.Left
                Dim distance = (e.Bounds.Height - Me.Height) / 2

                Me.Location = New Point(e.Bounds.Width - Me.Width - distance, e.Bounds.Location.Y + distance) 'New Point(4, 4) '
            End If
            Me.BringToFront()
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta el exportado de los datos
    ''' </summary>
    Protected Overrides Sub OnClick(e As EventArgs)
        RaiseEvent NewRecordClick(_gridControl, e)
        MyBase.OnClick(e)
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se muestra el botón
    ''' </summary>
    Private Sub GridView_CustomDrawFooter(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs)
        Me.AddButtonOnGrid(e)
    End Sub
#End Region

End Class
