Imports Infrastructure.CrossCutting.Base
Imports System.Drawing
Imports Presentation.Base.Extension

Public Class CtrMinimizedFolio

    Public WriteOnly Property FolioNumber As String
        Set(value As String)
            LblInvoice.Text = value
            CType(LblInvoice.SuperTip.Items(0), DevExpress.Utils.ToolTipTitleItem).Text = value
        End Set
    End Property
    Dim _totalEntity As Decimal
    Public WriteOnly Property TotalEntity As Decimal
        Set(value As Decimal)
            _totalEntity = value
        End Set
    End Property
    Dim _totalPatient As Decimal
    Public WriteOnly Property TotalPatient As Decimal
        Set(value As Decimal)
            _totalPatient = value
        End Set
    End Property
    Dim _folioId As Integer
    Property FolioId As Integer
        Get
            Return _folioId
        End Get
        Set(value As Integer)
            _folioId = value
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
        CType(LblInvoice.SuperTip.Items(1), DevExpress.Utils.ToolTipItem).Text = String.Format("Total Entidad {0}" & vbCrLf & "Total Paciente {1}", _totalEntity.MoneyFormat(0), _totalPatient.MoneyFormat(0))
    End Sub

End Class
