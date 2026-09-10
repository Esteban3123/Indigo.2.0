Imports System.IO
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.MixingStation.MVP
Imports Infrastructure.CrossCutting.Resources

Public Class FrmAttachWitnesses
    Private files As List(Of CampaignDetailWitnessFile)

    Public Property CampaignDetailId As Integer

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

    Private Sub FrmAttachWitnesses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.PrepareToolbar(Presentation.Controls.eAction.None)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
        setActionColums()
        loadFiles()
    End Sub

    Private Async Sub loadFiles()
        AsyncLoader(True)
        Using model As New MCampaign(Tag)
            Dim res = Await model.GetAllCampaignDetailWitnessFilesAsync(CampaignDetailId)
            AsyncLoader(False)

            If res.StateResult Then
                files = res.ObjectEmbbeded
                INDGcDocuments.DataSource = files
                INDGcDocuments.RefreshDataSource()
            Else
                Mensaje(EeventViewerImages.Advertencia) = res.Message
            End If
        End Using
    End Sub

    Public Overrides Sub AsyncLoader(State As Boolean)
        MyBase.AsyncLoader(State)
        If State Then
            INDGvDocuments.ShowLoadingPanel()
        Else
            INDGvDocuments.HideLoadingPanel()
        End If
    End Sub

    Private Sub setActionColums()
        IndigoGridView1.SetListAcction(INDGvDocuments, {eAcciones.View, eAcciones.Remove}.ToList())
    End Sub

    Private Sub FrmAttachWitnesses_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub INDSbLoad_Click(sender As Object, e As EventArgs) Handles INDSbLoad.Click
        If String.IsNullOrEmpty(INDTeName.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione el nombre del archivo"
            Return
        End If

        If XtraOpenFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Dim file As New CampaignDetailWitnessFile With {
                .CampaignDetailId = CampaignDetailId,
                .File = IO.File.ReadAllBytes(XtraOpenFileDialog1.FileName),
                .Name = INDTeName.EditValue,
                .Extension = Path.GetExtension(XtraOpenFileDialog1.FileName)
            }

            files.Add(file)

            INDTeName.EditValue = Nothing
            INDGcDocuments.DataSource = files
            INDGcDocuments.RefreshDataSource()
        End If
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If sender.Tag = "View" Then
            ViewDocument()
        Else
            RemoveDocument()
        End If
    End Sub

    Private Sub RemoveDocument()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
            Return
        End If
        Dim row = INDGvDocuments.GetFocusedObject(Of CampaignDetailWitnessFile)()

        If row IsNot Nothing Then
            row.MarkAsDeleted()
            INDGcDocuments.DataSource = files.FindAll(Function(m) m.ChangeTracker.State <> ObjectState.Deleted)
            INDGcDocuments.RefreshDataSource()
        End If
    End Sub

    Private Sub ViewDocument()
        Dim row = INDGvDocuments.GetFocusedObject(Of CampaignDetailWitnessFile)()

        If row IsNot Nothing Then
            If row.Extension.Equals(".pdf") Then
                INDLciPdfViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciImageViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                INDPdfFile.DetachStreamAfterLoadComplete = True
                Using ms As New MemoryStream(row.File)
                    INDPdfFile.LoadDocument(ms)
                End Using
            Else
                INDLciPdfViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciImageViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                INDPeFile.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray
                INDPeFile.EditValue = row.File
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    Private Async Sub Guardar()
        If files Is Nothing Then files = New List(Of CampaignDetailWitnessFile)()

        AsyncLoader(True)
        Using model As New MCampaign(Tag)
            Dim res = Await model.SaveCampaignDetailWitnessFileAsync(files)
            AsyncLoader(False)

            If res.StateResult Then
                Mensaje(EeventViewerImages.Informacion) = "Información registrada exitosamente"
                Close()
            Else
                Mensaje(EeventViewerImages.Advertencia) = res.Message
            End If
        End Using
    End Sub
End Class