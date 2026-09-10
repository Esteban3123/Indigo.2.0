Imports DevExpress.XtraEditors

Public Class CtrGeneric2Labels


#Region "Delegate"

#End Region

#Region "Properties"
    ''' <summary>
    ''' Texto superior
    ''' </summary>
    Public Property Label1 As LabelControl
        Get
            Return INDlbLabel1
        End Get
        Set(value As LabelControl)
            INDlbLabel1 = value
        End Set
    End Property

    ''' <summary>
    ''' Texto inferior
    ''' </summary>
    Public Property Label2 As LabelControl
        Get
            Return INDlbLabel2
        End Get
        Set(value As LabelControl)
            INDlbLabel2 = value
        End Set
    End Property

    Private _batchCodes As List(Of String)
    Public Property BatchCodes As List(Of String)
        Get
            Return _batchCodes
        End Get
        Set(value As List(Of String))
            _batchCodes = value
            INDgcBatchCodes.DataSource = value.Select(Function(m) New With {Key .BatchCode = m}).ToList()
        End Set
    End Property

#End Region

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLciLabel2)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLciLabel1)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbLabel1)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbLabel2)
    End Sub

    Private Sub CtrGeneric2Labels_Click(sender As Object, e As EventArgs) Handles MyBase.Click, INDlbLabel1.Click, INDlbLabel2.Click, INDLciLabel2.Click, INDLciLabel1.Click, INDlcgGeneric2Labels.Click
        INDpceBatchCodes.ShowPopup()
    End Sub
End Class