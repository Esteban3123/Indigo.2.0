Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Glosas.MVP

Public Class FrmPopupRIPS

#Region "Properties"

    Dim Model As MRadicateInvoice

    Public IdInvoiceRadicateConfirm As Integer = 0

    Public ConsecutiveRadicateInvoice As String

    Public InvoicesList As List(Of Domain.Entities.RIPSBilling)

    Public DetailPackage As Boolean = False

    ''' <summary>
    ''' Propiedad que contiene si se genera archivo plano AD
    ''' </summary>
    Private Property GenerateADPlane As Boolean
        Get
            Return INDrgGenerateADPlane.EditValue
        End Get
        Set(value As Boolean)
            INDrgGenerateADPlane.EditValue = value
        End Set
    End Property


#End Region

#Region "Events"

#Region "Load"

    Private Sub FrmPopupRIPS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Model = New MRadicateInvoice("509")
        GenerateADPlane = False
    End Sub

    Private Sub FrmPopupRIPS_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDLciGenerateMegaPlane.Visibility = If(IdInvoiceRadicateConfirm > 0, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Model = Nothing
        IdInvoiceRadicateConfirm = Nothing
        ConsecutiveRadicateInvoice = Nothing
        InvoicesList = Nothing
    End Sub

#End Region

#Region "Click"

    Private Async Sub INDBtnGenerateRIPS_Click(sender As Object, e As EventArgs) Handles INDBtnGenerateRIPS.Click
        Dim resultGenerateFile As List(Of ActionMessageResult(Of StringBuilder))
        If IdInvoiceRadicateConfirm > 0 OrElse (InvoicesList IsNot Nothing AndAlso InvoicesList.Any()) Then
            AsyncLoader(True)
            Dim InvoiceIds As New List(Of Integer)
            If InvoicesList IsNot Nothing AndAlso InvoicesList.Any() Then
                IdInvoiceRadicateConfirm = 0
                InvoiceIds = InvoicesList.Select(Function(i) i.InvoiceId).ToList()
            End If
            resultGenerateFile = Await Model.GenerateRIPSPlane(IdInvoiceRadicateConfirm, ConsecutiveRadicateInvoice, ImgCmbProductCodification.EditValue, ImgCmbServiceCode.EditValue, DetailPackage, GenerateADPlane, InvoiceIds)
            AsyncLoader(False)
            DialogGenerateFile(resultGenerateFile)
        End If
    End Sub

    Private Async Sub INDBtnGenerateFurips_Click(sender As Object, e As EventArgs) Handles INDBtnGenerateFurips.Click
        Dim resultGenerateFile As List(Of ActionMessageResult(Of StringBuilder))
        If IdInvoiceRadicateConfirm > 0 OrElse (InvoicesList IsNot Nothing AndAlso InvoicesList.Any()) Then
            AsyncLoader(True)
            resultGenerateFile = Await Model.GenerateFURIPSPlane(IdInvoiceRadicateConfirm, InvoicesList)
            AsyncLoader(False)
            DialogGenerateFile(resultGenerateFile)
        End If
    End Sub

    Private Async Sub INDBtnGenerateFurtran_Click(sender As Object, e As EventArgs) Handles INDBtnGenerateFurtran.Click
        Dim resultgeneratefile As List(Of ActionMessageResult(Of StringBuilder))
        If InvoicesList IsNot Nothing AndAlso InvoicesList.Any() Then
            AsyncLoader(True)
            resultgeneratefile = Await Model.GenerateFURTRANPlane(InvoicesList)
            AsyncLoader(False)
            DialogGenerateFile(resultgeneratefile)
        End If
    End Sub

    Private Async Sub INDBtnMegaRIPS_Click(sender As Object, e As EventArgs) Handles INDBtnMegaRIPS.Click
        Dim resultGenerateFile As ActionMessageResult(Of String)
        If IdInvoiceRadicateConfirm > 0 OrElse (InvoicesList IsNot Nothing AndAlso InvoicesList.Any()) Then
            AsyncLoader(True)
            resultGenerateFile = Await Model.GetMegaRIPSPlane(IdInvoiceRadicateConfirm, InvoicesList)
            AsyncLoader(False)
            If resultGenerateFile.StateResult = True Then
                DialogGenerateFileMegaRIPS(resultGenerateFile.ObjectEmbbeded)
            Else
                Mensaje(EeventViewerImages.Advertencia) = "Ocurrió un error obteniendo los datos del archivo: " & vbCrLf & resultGenerateFile.Message
            End If
        End If
    End Sub

    Private Async Sub INDBtnGenerateMegaPlane_Click(sender As Object, e As EventArgs) Handles INDBtnGenerateMegaPlane.Click
        Dim resultGenerateFile As List(Of ActionMessageResult(Of StringBuilder))
        If IdInvoiceRadicateConfirm > 0 OrElse (InvoicesList IsNot Nothing AndAlso InvoicesList.Any()) Then
            AsyncLoader(True)
            Dim InvoiceIds As New List(Of Integer)
            If InvoicesList IsNot Nothing AndAlso InvoicesList.Any() Then
                InvoiceIds = InvoicesList.Select(Function(i) i.InvoiceId).ToList()
            End If
            resultGenerateFile = Await Model.GenerateRIPSMegaPlane(IdInvoiceRadicateConfirm, ConsecutiveRadicateInvoice, ImgCmbProductCodification.EditValue, ImgCmbServiceCode.EditValue, InvoiceIds)
            AsyncLoader(False)
            DialogGenerateFile(resultGenerateFile)
        End If
    End Sub


    ''' <summary>
    ''' metodo para generar los rips electronicos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnGenerateERIPS_Click(sender As Object, e As EventArgs) Handles INDBtnGenerateERIPS.Click

    End Sub

#End Region

#End Region

#Region "Metodos y Funciones"

    ''' <summary>
    ''' Mensaje
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
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

    Public Sub DialogGenerateFileMegaRIPS(ByVal resultGenerateFile As String)
        Try
            Dim Folder As FolderBrowserDialog = New FolderBrowserDialog
            If Folder.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Dim pathFile As String = Path.Combine(Folder.SelectedPath, "MegaRIPS" & ConsecutiveRadicateInvoice & ".txt")
                If InvoicesList IsNot Nothing AndAlso InvoicesList.Any() Then
                    pathFile = Path.Combine(Folder.SelectedPath, "MegaRIPS" & InvoicesList(0).FechaCorte.ToString("yyyyMM") & ".txt")
                End If
                File.WriteAllText(pathFile, resultGenerateFile)
                Mensaje(EeventViewerImages.Informacion) = "Se generó el siguiente archivo: " & pathFile
                Process.Start(pathFile)
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "Ocurrió un Error Creando los Archivos"
        End Try
        Me.Close()
    End Sub

    ''' <summary>
    ''' Metodo que despliega show dialog para guardar un archivo plano
    ''' </summary>
    ''' <param name="resultGenerateFile"></param>
    ''' <remarks></remarks>
    Public Sub DialogGenerateFile(ByVal resultGenerateFile As List(Of ActionMessageResult(Of StringBuilder)))
        Try
            If resultGenerateFile.Count > 0 Then
                If resultGenerateFile.Any(Function(r) r.StateResult) Then
                    Dim Folder As FolderBrowserDialog = New FolderBrowserDialog
                    Dim ListFiles As String = String.Empty

                    If Folder.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                        For i As Integer = 0 To resultGenerateFile.Count() - 1
                            If resultGenerateFile.Item(i).StateResult = True Then
                                File.WriteAllText(Path.Combine(Folder.SelectedPath, resultGenerateFile.Item(i).Message.ToString() & ".txt"), resultGenerateFile.Item(i).ObjectEmbbeded.ToString())

                                If i = 0 Then
                                    ListFiles = resultGenerateFile.Item(i).Message.ToString()
                                Else
                                    ListFiles = ListFiles & " , " & resultGenerateFile.Item(i).Message.ToString()
                                End If
                            End If
                        Next
                        Mensaje(EeventViewerImages.Informacion) = "Se han creado los Archivos " & ListFiles & " correctamente"
                    End If
                End If

                If resultGenerateFile.Any(Function(r) Not r.StateResult) Then
                    Dim errors As String = String.Join(Environment.NewLine, resultGenerateFile.Where(Function(r) Not r.StateResult).Select(Function(r) r.Message).ToList())
                    Mensaje(EeventViewerImages.Advertencia) = "Se presentaron los siguientes errores: " & Environment.NewLine & errors
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se ha generado ningún archivo"
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "Ocurrió un Error Creando los Archivos"
        End Try

        Me.Close()
    End Sub

#End Region

End Class