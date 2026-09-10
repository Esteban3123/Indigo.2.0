Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Billing.MVP
Imports DevExpress.Xpo
Imports System.Drawing

Public Class FrmAuthorizationBilling

#Region "Properties"

    ''' <summary>
    ''' Id de la autorización
    ''' </summary>
    ''' <value>
    ''' The billing authorization identifier.
    ''' </value>
    Private Property BillingAuthorizationId As Integer
        Get
            Return CType(INDsleAuthorization.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleAuthorization.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de las autorizaciones de facturación
    ''' </summary>
    Private Property BillingAuthorizationDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleAuthorization.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAuthorization.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' número de autorización
    ''' </summary>
    Public Event AddBillingAuthorization(ByVal AuthorizationId As Integer)

    ''' <summary>
    ''' Estado abierto del popUp
    ''' </summary>
    Private _stateOpenPopUp As Boolean

    ''' <summary>
    ''' bandera para permitir cerrar el form sin mostrar el mensaje de pantalla
    ''' </summary>
    Private _flagCanClose As Boolean

#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _stateOpenPopUp = Nothing
        _flagCanClose = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmAuthorizationBilling control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmAuthorizationBilling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CleanControls()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAcept_Click(sender As Object, e As EventArgs) Handles INDsbAcept.Click
        If BillingAuthorizationId <> 0 Then
            _flagCanClose = True
            RaiseEvent AddBillingAuthorization(BillingAuthorizationId)
            Me.Close()
        Else
            Me.ShowMenssage(EeventViewerImages.Advertencia) = ResourceManager.GetString("AuthorizationSelect", GetType(CtrFolio).Name)
            INDsleAuthorization.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the FrmAuthorizationBilling control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub FrmAuthorizationBilling_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleAuthorization control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAuthorization_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAuthorization.ButtonClick
        Using Form As New FrmBillingAuthorization
            Form.ViewModeEditHold = True
            Form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Form.MinimizeBox = False
            Form.MaximizeBox = False
            Form.Size = New Size(800, 700)
            Form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Dim transparent As New FrmTransparent(Form, False)
            transparent.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmAuthorizationBilling control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmAuthorizationBilling_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not _flagCanClose AndAlso BillingAuthorizationId <> 0 AndAlso Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleAuthorization control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAuthorization_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAuthorization.QueryPopUp
        If Not _stateOpenPopUp Then
            _stateOpenPopUp = True
            LoadAuthorization()
        End If
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property ShowMenssage(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Carga las autorizaciones de facturación
    ''' </summary>
    Private Sub LoadAuthorization()
        Using Model As New MLiquidation()
            BillingAuthorizationDatasource = Model.LoadBillingAuthorization()
        End Using
    End Sub

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        _stateOpenPopUp = False
        _flagCanClose = False
    End Sub
#End Region

End Class