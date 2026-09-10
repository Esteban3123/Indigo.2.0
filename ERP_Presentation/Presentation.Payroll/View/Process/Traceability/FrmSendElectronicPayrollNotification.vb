Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Payroll.MVP

Public Class FrmSendElectronicPayrollNotification

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private _presenter As PElectronicPayrollTraceability

    ''' <summary>
    ''' Lista de documentos electronicos a reenviar
    ''' </summary>
    Public ListElectronicPayrolls As List(Of Domain.Payroll.Entities.ElectronicPayroll)

#End Region

#Region "Event"

    Public Event ReturnModalArgs(sender As Object, e As EventArgs)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad que almacena la lista de direcciones de correo agregadas por el usuario
    ''' </summary>
    Private Property ListEmails() As List(Of Domain.Payroll.Entities.Email)
        Get
            Return INDgcEmail.DataSource
        End Get
        Set(ByVal value As List(Of Domain.Payroll.Entities.Email))
            INDgcEmail.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Private WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

#End Region

#Region "Methods"

    Private Sub LoadEmails()
        If ListElectronicPayrolls Is Nothing OrElse ListElectronicPayrolls.Count = 0 Then
            Exit Sub
        End If

        INDgcEmailView.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Try
                                      Dim thirdPartyId As Integer = ListElectronicPayrolls.FirstOrDefault().EmployeePartyId
                                      Dim result = _presenter.ListEmailsByThirdPartyId(thirdPartyId)
                                      INDgcEmail.BeginInvoke(Sub()
                                                                 If result IsNot Nothing Then
                                                                     For Each email In result
                                                                         ListEmails.Add(New Domain.Payroll.Entities.Email With {.Email1 = email.Email1})
                                                                     Next
                                                                 End If
                                                                 INDgcEmailView.HideLoadingPanel()
                                                             End Sub)
                                  Catch ex As Exception
                                      INDgcEmail.BeginInvoke(Sub()
                                                                 INDgcEmailView.HideLoadingPanel()
                                                                 Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                             End Sub)
                                  End Try
                              End Sub)
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub FrmSendElectronicPayrollNotification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._presenter = New PElectronicPayrollTraceability(Nothing)

        Me.LoadEmails()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDpcCorreos_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDpcCorreos.ButtonClick
        If Not String.IsNullOrEmpty(INDpcCorreos.Text) Then
            ListEmails.Add(New Domain.Payroll.Entities.Email With {.Email1 = INDpcCorreos.Text.ToLower().Trim()})
            INDgcEmail.RefreshDataSource()
            INDpcCorreos.Text = String.Empty
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnEliminarEmail_Click(sender As Object, e As EventArgs) Handles INDbtnEliminarEmail.Click
        Dim _Email = TryCast(INDgcEmailView.GetFocusedRow, Domain.Payroll.Entities.Email)
        If _Email IsNot Nothing Then
            ListEmails.Remove(_Email)
            INDgcEmail.RefreshDataSource()
        End If
    End Sub

    Private Async Sub INDSbtnSendNotification_Click(sender As Object, e As EventArgs) Handles INDSbtnSendNotification.Click
        If ListEmails.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe agregar una dirección de correo electrónico para continuar"
            Exit Sub
        End If

        Dim listElectronicPayrollNotification As New List(Of Domain.Payroll.Entities.ElectronicPayrollNotification)
        For Each ElectronicPayroll In ListElectronicPayrolls
            For Each email In ListEmails
                listElectronicPayrollNotification.Add(New Domain.Payroll.Entities.ElectronicPayrollNotification With
                {
                    .ElectronicPayrollId = ElectronicPayroll.Id,
                    .Email = email.Email1,
                    .Status = 1,
                    .CreationDate = DateTime.Now
                })
            Next
        Next

        Try
            Using model As New MElectronicPayrollTraceability("")
                Me.Enabled = False
                Dim result = Await model.SendNotification(listElectronicPayrollNotification)
                Me.Enabled = True
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = "Notificación Enviada"
                    RaiseEvent ReturnModalArgs(Nothing, Nothing)
                    Me.Close()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.Enabled = True
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

#End Region

End Class