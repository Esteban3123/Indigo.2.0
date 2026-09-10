#Region "Imports"

Imports DevExpress.XtraSplashScreen
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Billing.MVP

#End Region

Public Class FrmRelatedInvoices

#Region "Properties"

    ''' <summary>
    ''' Numero del Ingreso
    ''' </summary>
    ''' <returns></returns>
    Public Property AdmissionNumber As String

    ''' <summary>
    ''' Id del Folio
    ''' </summary>
    ''' <returns></returns>
    Public Property RevenueControlDetailId As Integer

    ''' <summary>
    ''' Se dispara cuando se desee refrescar el formulario de liquidacion padre
    ''' </summary>
    Public Event BeginReloadLiquidationForm()

    ''' <summary>
    ''' Formulario de espera
    ''' </summary>
    Private waitForm As New SplashScreenManager(Me, GetType(Controls.wfMain), False, True)

    ''' <summary>
    ''' Sets the show message.
    ''' </summary>
    ''' <value>
    ''' The show message.
    ''' </value>
    Public WriteOnly Property ShowMessage(ByVal Icon As Base.EeventViewerImages) As String
        Set(value As String)
            If Icon = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento encargado de liberar recursos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        AdmissionNumber = Nothing
        RevenueControlDetailId = Nothing
        waitForm = Nothing
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Handles the Shown event of the FrmAssociateInvoice control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmAssociateInvoice_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDSleInvoice.Focus()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmAssociateInvoice_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Handles the QueryPopUp event of the SleFindAdmission control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleInvoice_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInvoice.QueryPopUp
        If INDSleInvoice.Properties.DataSource Is Nothing Then
            Using model As New MLiquidation()
                Me.INDSleInvoice.Properties.DataSource = model.ListViewRelatedInvoices(AdmissionNumber)
            End Using
        End If
    End Sub

#End Region

#Region "Click"

    Private Async Sub INDBtnAssociate_Click(sender As Object, e As EventArgs) Handles INDBtnAssociate.Click
        If INDSleInvoice.EditValue Is Nothing Then
            ShowMessage(EeventViewerImages.Advertencia) = "Debe seleccionar una factura"
            Exit Sub
        End If

        If MessageIndigo.Show("¿Está seguro que desea asociar la Factura al Folio?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If Not waitForm.IsSplashFormVisible Then
                waitForm.ShowWaitForm()
            End If

            Using model As New MLiquidation()
                Dim res = Await model.AssociateInvoice(RevenueControlDetailId, INDSleInvoice.EditValue)
                waitForm.CloseWaitForm()
                If res IsNot Nothing Then
                    If res.StateResult Then
                        Me.ShowMessage(EeventViewerImages.Informacion) = res.Message
                        RaiseEvent BeginReloadLiquidationForm()
                        Me.Close()
                    Else
                        Me.ShowMessage(EeventViewerImages.Advertencia) = res.Message
                    End If
                End If
            End Using
        End If
    End Sub

#End Region

#End Region

End Class