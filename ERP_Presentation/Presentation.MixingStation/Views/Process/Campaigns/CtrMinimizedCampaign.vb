Imports Infrastructure.CrossCutting.Base
Imports System.Drawing
Imports Presentation.Base.Extension

Public Class CtrMinimizedCampaign

    Public WriteOnly Property CampaignNumber As String
        Set(value As String)
            LblInvoice.Text = value
            CType(LblInvoice.SuperTip.Items(0), DevExpress.Utils.ToolTipTitleItem).Text = value
        End Set
    End Property

    Dim _productionLineCodeName As String
    Public WriteOnly Property ProductionLineCodeName As String
        Set(value As String)
            _productionLineCodeName = value
        End Set
    End Property

    Dim _unitDoseTypeCodeName As String
    Public WriteOnly Property UnitDoseTypeCodeName As String
        Set(value As String)
            _unitDoseTypeCodeName = value
        End Set
    End Property

    Dim _status As Byte
    Public Property Status As Byte
        Get
            Return _status
        End Get
        Set(value As Byte)
            _status = value
            Select Case value
                Case 1 'Registrado
                    Window.Utils.SetValueToProperty(Me, "BackColor", Color.FromArgb(0, 70, 109))
                Case 2 'Facturado
                    Window.Utils.SetValueToProperty(Me, "BackColor", Color.Green)
                Case 3, 4 'Bloqueado
                    Window.Utils.SetValueToProperty(Me, "BackColor", Color.FromArgb(202, 81, 0))
                Case 5 'Reconocimiento
                    Window.Utils.SetValueToProperty(Me, "BackColor", Color.FromArgb(0, 121, 107))
                Case Else
                    'Window.Utils.SetValueToProperty(Me, "Appearance.ImageIndex", 2)
            End Select
        End Set
    End Property

    Private Sub CtrMinimizedFolio_MouseEnter(sender As Object, e As EventArgs) Handles LblInvoice.MouseEnter
        Select Case _status
            Case 1 'Registrado
                Window.Utils.SetValueToProperty(Me, "BackColor", Color.FromArgb(0, 90, 120))
            Case 2 'Facturado
                Window.Utils.SetValueToProperty(Me, "BackColor", Color.FromArgb(0, 155, 0))
            Case 3, 4 'Bloqueado
                Window.Utils.SetValueToProperty(Me, "BackColor", Color.FromArgb(220, 95, 0))
            Case 5 'Reconocimiento
                Window.Utils.SetValueToProperty(Me, "BackColor", Color.FromArgb(0, 141, 127))
            Case Else
                'Window.Utils.SetValueToProperty(Me, "Appearance.ImageIndex", 2)
        End Select
    End Sub

    Private Sub CtrMinimizedFolio_MouseLeave(sender As Object, e As EventArgs) Handles LblInvoice.MouseLeave
        Select Case _status
            Case 1 'Registrado
                Window.Utils.SetValueToProperty(Me, "BackColor", Color.FromArgb(0, 70, 109))
            Case 2 'Facturado
                Window.Utils.SetValueToProperty(Me, "BackColor", Color.Green)
            Case 3, 4 'Bloqueado
                Window.Utils.SetValueToProperty(Me, "BackColor", Color.FromArgb(202, 81, 0))
            Case 5 'Reconocimiento
                Window.Utils.SetValueToProperty(Me, "BackColor", Color.FromArgb(0, 121, 107))
            Case Else
                'Window.Utils.SetValueToProperty(Me, "Appearance.ImageIndex", 2)
        End Select
    End Sub

    Private Sub LblInvoice_MouseHover(sender As Object, e As EventArgs) Handles LblInvoice.MouseHover
        'Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(0, Byte), Integer))
    End Sub

    Private Sub CtrMinimizedFolio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CType(LblInvoice.SuperTip.Items(1), DevExpress.Utils.ToolTipItem).Text = String.Format("Línea Producción {0}" & vbCrLf & "Tipo Dosis Unitaria {1}", _productionLineCodeName, _unitDoseTypeCodeName)
    End Sub

End Class
