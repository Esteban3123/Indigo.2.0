Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Billing.MVP

Public Class FrmSendElectronicDocumentNotification

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private _presenter As PElectronicDocumentTraceability

    ''' <summary>
    ''' Lista de documentos electronicos a reenviar
    ''' </summary>
    Public ListElectronicDocuments As List(Of Domain.Entities.ElectronicDocument)

#End Region

#Region "Event"

    Public Event ReturnModalArgs(sender As Object, e As EventArgs)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad que almacena la lista de direcciones de correo agregadas por el usuario
    ''' </summary>
    Private Property ListEmails() As List(Of Email)
        Get
            Return INDgcEmail.DataSource
        End Get
        Set(ByVal value As List(Of Email))
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
        If ListElectronicDocuments Is Nothing OrElse ListElectronicDocuments.Count = 0 Then
            Exit Sub
        End If

        INDgcEmailView.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Try
                                      Dim thirdPartyId As Integer = ListElectronicDocuments.FirstOrDefault().CustomerPartyId
                                      Dim result = _presenter.ListEmailsByThirdPartyId(thirdPartyId)
                                      INDgcEmail.BeginInvoke(Sub()
                                                                 ListEmails = If(result IsNot Nothing AndAlso result.Count > 0, result, Nothing)
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

    Private Sub FrmSendElectronicDocumentNotification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._presenter = New PElectronicDocumentTraceability(Nothing)

        Me.LoadEmails()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDpcCorreos_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDpcCorreos.ButtonClick
        If Not String.IsNullOrEmpty(INDpcCorreos.Text) Then
            If ListEmails Is Nothing Then ListEmails = New List(Of Email)
            ListEmails.Add(New Email With {.Email1 = INDpcCorreos.Text.ToLower().Trim()})
            INDgcEmail.RefreshDataSource()
            INDpcCorreos.Text = String.Empty
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnEliminarEmail_Click(sender As Object, e As EventArgs) Handles INDbtnEliminarEmail.Click
        Dim _Email = TryCast(INDgcEmailView.GetFocusedRow, Email)
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

        Dim listElectronicDocumentNotification As New List(Of ElectronicDocumentNotification)
        For Each electronicDocument In ListElectronicDocuments
            For Each email In ListEmails
                listElectronicDocumentNotification.Add(New ElectronicDocumentNotification With
                {
                    .ElectronicDocumentId = electronicDocument.Id,
                    .Email = email.Email1,
                    .Status = 1,
                    .CreationDate = DateTime.Now
                })
            Next
        Next

        Try
            Using model As New MElectronicDocumentTraceability("")
                Me.Enabled = False
                Dim result = Await model.SendNotification(listElectronicDocumentNotification)
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