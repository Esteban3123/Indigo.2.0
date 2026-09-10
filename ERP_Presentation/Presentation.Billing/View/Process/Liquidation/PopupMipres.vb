Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Base.BaseClass

Public Class PopupMipres

#Region "Properties"

    ''' <summary>
    ''' Listado de órdenes de servicio selecionadas
    ''' </summary>
    ''' <returns></returns>
    Public Property ServiceOrderDetailIds As List(Of Integer)

    ''' <summary>
    ''' DataSource
    ''' </summary>
    ''' <returns></returns>
    Private Property DataSource As List(Of MipresCode)
        Get
            Return INDGcMipres.DataSource
        End Get
        Set(value As List(Of MipresCode))
            INDGcMipres.DataSource = value
            INDGcMipres.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Mipres code
    ''' </summary>
    ''' <returns></returns>
    Private Property Code As String
        Get
            Return INDTeCode.EditValue
        End Get
        Set(value As String)
            INDTeCode.EditValue = value
        End Set
    End Property

    Private Property IdMipres As String
        Get
            Return INDTeIdMipres.EditValue
        End Get
        Set(value As String)
            INDTeIdMipres.EditValue = value
        End Set
    End Property
#End Region

#Region "Functions"
    ''' <summary>
    ''' Muestra un mensaje en el frontal
    ''' </summary>
    ''' <param name="Icon">Icono del tipo de mensaje</param>
    ''' <value>Mensaje a mostrar</value>
    Public WriteOnly Property ShowMessage(ByVal Icon As Base.EeventViewerImages) As String
        Set(value As String)
            If Icon = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me.Owner)
            ElseIf Icon = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me.Owner)
            ElseIf Icon = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Carga los códigos si existe
    ''' </summary>
    ''' <returns></returns>
    Public Async Function LoadMipresCodes() As Task
        If ServiceOrderDetailIds IsNot Nothing AndAlso ServiceOrderDetailIds.Count = 1 Then
            IsAsyncOperation()
            Using model As New MLiquidation()
                Dim mipres = Await model.GetMipresCodesByServiceOrderDetailIdAsync(ServiceOrderDetailIds(0))
                If mipres IsNot Nothing AndAlso mipres.Any() Then
                    DataSource = mipres
                End If
            End Using
            IsAsyncOperation(False)
            INDTeCode.Focus()
        End If
    End Function

    ''' <summary>
    ''' Agrega un nuevo código a la regilla
    ''' </summary>
    Private Sub AddCode()
        If INDTeCode.EditValue Is Nothing Then
            ShowMessage(EeventViewerImages.Advertencia) = "Por favor digite un número de Mipres"
            Exit Sub
        End If
        If DataSource Is Nothing Then
            DataSource = New List(Of MipresCode)()
        End If

        If DataSource.Any(Function(m) m.Code = Code) Then
            ShowMessage(EeventViewerImages.Advertencia) = "El código ya se encuentra en el listado"
            Exit Sub
        End If
        DataSource.Add(New MipresCode With {.Code = Code, .IdMipres = IdMipres})
        INDGcMipres.RefreshDataSource()
        Code = Nothing
        IdMipres = Nothing
        INDTeCode.Focus()
    End Sub

    ''' <summary>
    ''' async loader
    ''' </summary>
    ''' <param name="value"></param>
    Private Sub IsAsyncOperation(Optional value As Boolean = True)
        INDTeCode.Enabled = Not value
        INDSbAdd.Enabled = Not value
        INDSbAccept.Enabled = Not value
        If value Then
            INDGvMipres.ShowLoadingPanel()
        Else
            INDGvMipres.HideLoadingPanel()
        End If
    End Sub

    ''' <summary>
    ''' Elimina un mipres
    ''' </summary>
    Private Async Sub RemoveMipres()
        If MessageIndigo.Show("¿Está seguro que desea eliminar el registro?", MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Try
            Dim mipres = INDGvMipres.GetFocusedObject(Of MipresCode)()

            If mipres.Id = 0 Then
                DataSource.Remove(mipres)
                INDGcMipres.RefreshDataSource()
                Exit Sub
            End If

            IsAsyncOperation()
            Using model As New MLiquidation()
                Dim res = Await model.DeleteMipresAsync(mipres.Id)
                If res.StateResult Then
                    DataSource.Remove(mipres)
                    INDGcMipres.RefreshDataSource()
                Else
                    ShowMessage(EeventViewerImages.Advertencia) = res.Message
                End If
            End Using
            IsAsyncOperation(False)
        Catch ex As Exception
            IsAsyncOperation(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Guarda los códigos
    ''' </summary>
    Private Async Sub SaveMipres()
        If DataSource.Any(Function(m) m.Id = 0) Then
            If MessageIndigo.Show("¿Está seguro que desea guarda los códigos?", MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If
            Try
                Me.Cursor = ChangeCursorIndigo()
                IsAsyncOperation()
                Using model As New MLiquidation()
                    Dim res = Await model.SaveMipresCode(ServiceOrderDetailIds, DataSource)
                    If res.StateResult Then
                        ShowMessage(EeventViewerImages.Informacion) = "Acción ejecutada correctamente"
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                    Else
                        ShowMessage(EeventViewerImages.Advertencia) = res.Message
                    End If
                    Me.Cursor = Windows.Forms.Cursors.Default
                    IsAsyncOperation(False)
                End Using
            Catch ex As Exception
                IsAsyncOperation(False)
                Throw ex
            End Try
        Else
            Me.Close()
        End If
    End Sub
#End Region

#Region "Events"
    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub PopupMipres_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IndigoGridView1.SetListAcction(INDGvMipres, {eAcciones.Remove}.ToList())
        Dim col = INDGvMipres.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
        If col IsNot Nothing Then col.Width = 30
        Await LoadMipresCodes()
    End Sub

    ''' <summary>
    ''' Keydown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopupMipres_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopupMipres_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDTeCode.Focus()
    End Sub

    ''' <summary>
    ''' Add
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        AddCode()
    End Sub

    ''' <summary>
    ''' save
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAccept_Click(sender As Object, e As EventArgs) Handles INDSbAccept.Click
        SaveMipres()
    End Sub

    ''' <summary>
    ''' Remove mipres
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        RemoveMipres()
    End Sub

#End Region

End Class