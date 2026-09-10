Imports DevExpress.Xpo
Imports DevExpress.XtraSplashScreen
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base
Imports Presentation.Billing.MVP

Public Class FrmAdmissionRelated

#Region "Properties"
    Public Property AdmissionCode As String
    Public Property PatientCode As String
    Public Property RevenueControlParentId As Integer

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

    ''' <summary>
    ''' Gets or sets a value indicating whether this instance is busy.
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
    ''' </value>
    Public Property IsBusy As Boolean
        Get
            Return MarqueeProgressBarControl1.Visible
        End Get
        Set(value As Boolean)
            INDSleAdmissionNumber2.Enabled = Not value
            MarqueeProgressBarControl1.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Formulario de espera
    ''' </summary>
    Private waitForm As New SplashScreenManager(Me, GetType(Controls.wfMain), False, True)

    ''' <summary>
    ''' Se dispara cuando se desee refrescar el formulario de liquidacion padre
    ''' </summary>
    Public Event BeginReloadLiquidationForm()
#End Region

#Region "Handlers"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        AdmissionCode = Nothing
        PatientCode = Nothing
        RevenueControlParentId = Nothing
        waitForm = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmAdmissionRelated control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmAdmissionRelated_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDSleAdmissionNumber2.ShowButtonOk = True
        INDSleAdmissionNumber2.ShowButtonMoreInfo = False
        Me.INDSleAdmissionNumber2.FuncQueryOnKeyEnterPressed = AddressOf Me.INDSleAdmissionNumber2_KeyDown
        AddHandler Me.INDSleAdmissionNumber2.ButtonOk, AddressOf Me.INDSleAdmissionNumber2_ButtonOk
    End Sub

    ''' <summary>
    ''' Handles the Shown event of the FrmAdmissionRelated control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmAdmissionRelated_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDSleAdmissionNumber2.Focus()
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDSleAdmissionNumber2 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Controls.EditValueChangedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleAdmissionNumber2_EditValueChanged(sender As Object, e As Controls.EditValueChangedEventArgs) Handles INDSleAdmissionNumber2.EditValueChanged
        If e.NewValue IsNot Nothing Then
            'OpenLiquidationForm(e.NewValue)
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the SleFindAdmission control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub SleFindAdmission_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAdmissionNumber2.QueryPopUp
        If INDSleAdmissionNumber2.Datasource Is Nothing Then
            Using m As New MLiquidation()
                Me.INDSleAdmissionNumber2.Datasource = m.ListAdmissionsToLiquidationOncologycal(AdmissionCode, PatientCode)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Inds the sle admission number2 key down.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Private Function INDSleAdmissionNumber2_KeyDown(code As String) As Object
        Task.Factory.StartNew(Sub()
                                  Using model As New MLiquidation()
                                      Dim admissionTempKeyDown = model.GetAdmissionsToLiquidationCollection(code)
                                      INDSleAdmissionNumber2.SafeInvoke(Sub()
                                                                            Try
                                                                                If admissionTempKeyDown IsNot Nothing AndAlso admissionTempKeyDown.Count > 0 Then
                                                                                    'If admissionTempKeyDown IsNot Nothing AndAlso admissionTempKeyDown(0).GetType().GetProperties().Any(Function(p) p.Name.Equals("AdmissionCode")) Then
                                                                                    '    Dim evt As New EditValueChangedEventArgs(Nothing, admissionTempKeyDown(0))
                                                                                    '    INDSleAdmissionNumber2_NewSelectedValue(Me, evt)
                                                                                    'End If
                                                                                    Me.IsBusy = False
                                                                                Else
                                                                                    ShowMessage(EeventViewerImages.Advertencia) = "El número del ingreso no existe"
                                                                                    INDSleAdmissionNumber2.EditValue = Nothing
                                                                                    INDSleAdmissionNumber2.DisplayNullText = String.Empty
                                                                                    INDSleAdmissionNumber2.Focus()
                                                                                End If
                                                                            Catch
                                                                                Me.IsBusy = False
                                                                                INDSleAdmissionNumber2.EditValue = Nothing
                                                                                INDSleAdmissionNumber2.DisplayNullText = String.Empty
                                                                                INDSleAdmissionNumber2.Focus()
                                                                            End Try
                                                                        End Sub)
                                  End Using
                              End Sub)
        Return Nothing
    End Function
#End Region

#Region "Methods"
    ''' <summary>
    ''' Opens the liquidation form.
    ''' </summary>
    Private Sub OpenLiquidationForm(numIngres As String)
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If
        Using frmLiquidation As New FrmLiquidation()
            frmLiquidation.RevenueControlParentId = RevenueControlParentId
            frmLiquidation.Size = New Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.6)
            frmLiquidation.MinimizeBox = False
            frmLiquidation.MaximizeBox = False
            frmLiquidation.IsOncologycalMode = True
            AddHandler frmLiquidation.BeginReloadLiquidationForm, AddressOf LiquidationReload
            Using m As New MControlOutpatientServices(Me.Tag)
                Dim admissionCollection As ViewLiquidationGetAdmissionAll = m.GetAdmissionObjectAllByNumIngres(numIngres)
                frmLiquidation.AuxAdmissionToReload = admissionCollection
            End Using
            frmLiquidation.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            AddHandler frmLiquidation.Shown, AddressOf HideLoaderForm
            Dim transparent As New FrmTransparent(frmLiquidation, False)
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub LiquidationReload()
        RaiseEvent BeginReloadLiquidationForm()
    End Sub

    ''' <summary>
    ''' Hides the loader form.
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub HideLoaderForm(sender As Object, e As EventArgs)
        waitForm.CloseWaitForm()
    End Sub

    Private Sub INDSleAdmissionNumber2_ButtonOk(sender As Object, e As EventArgs)
        If INDSleAdmissionNumber2.EditValue IsNot Nothing Then
            'OpenLiquidationForm(e.NewValue)
            OpenLiquidationForm(INDSleAdmissionNumber2.EditValue)
        End If
    End Sub

    Private Sub FrmAdmissionRelated_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

End Class