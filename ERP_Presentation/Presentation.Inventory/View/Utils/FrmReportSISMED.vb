#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports Presentation.Inventory.MVP
Imports Infrastructure.Data.Xpo
Imports System.Text
Imports System.IO
Imports Domain.Base.Entities
Imports System.Windows.Forms

#End Region

Public Class FrmReportSISMED

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim INDDateStart As DateTime
    Dim INDDateEnd As DateTime

    Private _FillingtypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingtypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingtypeReport Is Nothing Then
                _FillingtypeReport = New List(Of Tuple(Of Integer, String))
                _FillingtypeReport.Add(New Tuple(Of Integer, String)(1, "Venta"))
                _FillingtypeReport.Add(New Tuple(Of Integer, String)(2, "Compras"))
                _FillingtypeReport.Add(New Tuple(Of Integer, String)(3, "Todos Circular 004"))
                _FillingtypeReport.Add(New Tuple(Of Integer, String)(4, "Circular 006"))
                _FillingtypeReport.Add(New Tuple(Of Integer, String)(5, "SISDIS Circular 002"))
                _FillingtypeReport.Add(New Tuple(Of Integer, String)(6, "Circular 015 Dispositivos"))
            End If
            Return _FillingtypeReport
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
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

    ''' <summary>
    ''' Metodo que despliega show dialog para guardar un archivo plano
    ''' </summary>
    ''' <param name="content"></param>
    ''' <remarks></remarks>
    Public Sub DialogGenerateFile(content As List(Of ActionResult(Of StringBuilder)))
        Try
            If content.Count > 0 Then

                Dim Folder As FolderBrowserDialog = New FolderBrowserDialog
                Dim ListFiles As String = String.Empty

                If Folder.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    For i As Integer = 0 To content.Count() - 1
                        File.WriteAllText(Path.Combine(Folder.SelectedPath, content.Item(i).Message.ToString() & ".txt"), content.Item(i).ObjectEmbbeded.ToString())

                        If i = 0 Then
                            ListFiles = content.Item(i).Message.ToString()
                        Else
                            ListFiles = ListFiles & " , " & content.Item(i).Message.ToString()
                        End If
                    Next
                    Mensaje(EeventViewerImages.Informacion) = "Se han creado los Archivos " & ListFiles & " correctamente"
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se ha generado ningún archivo"
            End If
            'Dim save As System.Windows.Forms.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
            'save.Filter = "Texto|*.txt"
            'save.Title = title
            'save.FileName = title
            'If save.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    Dim file = save.OpenFile()
            '    Dim streamWrite As New StreamWriter(file, Encoding.Unicode)
            '    'streamWrite.Write(content)
            '    streamWrite.Flush()
            '    streamWrite.Close()
            'End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "Ocurrió un Error Creando el Archivo"
        End Try
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        'validaciones controles de fecha
        If INDDateStart = Nothing Or INDDateEnd = Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDeMonthStart.Focus()
            Validations = False
        ElseIf Me.INDDateStart > INDDateEnd Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDeMonthStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        INDDateStart = Nothing
        INDDateEnd = Nothing
        _FillingtypeReport = Nothing
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    Private Sub FrmReportAuxiliary_Load(sender As Object, e As EventArgs) Handles Me.Load

        INDGleTypeReport.Properties.DataSource = FillingtypeReport
        INDSpnYear.EditValue = Year(Me.GetDateServer())
        INDGleTypeReport.EditValue = 3

    End Sub

    ''' <summary>
    ''' se ejecuta en el evento click del boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        Dim DataArchive As List(Of ActionResult(Of StringBuilder)) = New List(Of ActionResult(Of StringBuilder))
        INDDateStart = "01/" & INDDeMonthStart.GetMonth & "/" & INDSpnYear.EditValue & " 00:00:00"
        INDDateEnd = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, "01/" & INDDeMonthEnd.GetMonth & "/" & INDSpnYear.EditValue & " 23:59:59"))
        If ValidateControlsReports() = True Then
            Using model As New MInventoryProduct(Me.Tag.ToString())
                'genera el archivo Sismed de ventas
                If INDGleTypeReport.EditValue = 1 Then
                    AsyncLoader(True)
                    Dim DataArchiveVenta = Await model.GenerateFileSismedVentas(INDDateStart, INDDateEnd)
                    AsyncLoader(False)
                    If DataArchiveVenta.StateResult = True Then
                        DataArchive.Add(DataArchiveVenta)
                        DialogGenerateFile(DataArchive)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = DataArchiveVenta.Message.ToString()
                    End If

                    'genera el archivo sismed de compras
                ElseIf INDGleTypeReport.EditValue = 2 Then
                    AsyncLoader(True)
                    Dim DataArchiveCompra = Await model.GenerateFileSismedCompras(INDDateStart, INDDateEnd)
                    AsyncLoader(False)
                    If DataArchiveCompra.StateResult = True Then
                        DataArchive.Add(DataArchiveCompra)
                        DialogGenerateFile(DataArchive)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = DataArchiveCompra.Message.ToString()
                    End If
                    'si selecciona todos genera los 2 archivos
                ElseIf INDGleTypeReport.EditValue = 3 Then
                    'Archivo Venta
                    AsyncLoader(True)
                    Dim DataArchiveVenta = Await model.GenerateFileSismedVentas(INDDateStart, INDDateEnd)

                    'Archivo Compra
                    Dim DataArchiveCompra = Await model.GenerateFileSismedCompras(INDDateStart, INDDateEnd)
                    AsyncLoader(False)
                    If DataArchiveVenta.StateResult = True AndAlso DataArchiveCompra.StateResult = True Then
                        DataArchive.Add(DataArchiveVenta)
                        DataArchive.Add(DataArchiveCompra)
                        DialogGenerateFile(DataArchive)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = DataArchiveVenta.Message.ToString()
                    End If
                    'genera el archivo sismed de compras y ventas unificado.
                ElseIf INDGleTypeReport.EditValue = 4 Then
                    AsyncLoader(True)
                    Dim DataArchiveCompra = Await model.GenerateFileSismedRes006(INDDateStart, INDDateEnd)
                    AsyncLoader(False)
                    If DataArchiveCompra.StateResult = True Then
                        DataArchive.Add(DataArchiveCompra)
                        DialogGenerateFile(DataArchive)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = DataArchiveCompra.Message.ToString()
                    End If
                    'genera el archivo SISDIS Circular 002
                ElseIf INDGleTypeReport.EditValue = 5 Then
                    AsyncLoader(True)
                    Dim DataArchiveCompra = Await model.GenerateSISDIS002(INDDateStart, INDDateEnd)
                    AsyncLoader(False)
                    If DataArchiveCompra.StateResult = True Then
                        DataArchive.Add(DataArchiveCompra)
                        DialogGenerateFile(DataArchive)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = DataArchiveCompra.Message.ToString()
                    End If
                ElseIf INDGleTypeReport.EditValue = 6 Then 'Archivo SISDIS Circular 015
                    AsyncLoader(True)
                    Dim ReportSISDIS015 = Await model.GenerateSISDIS015(INDDateStart, INDDateEnd)
                    AsyncLoader(False)

                    If ReportSISDIS015.StateResult Then
                        DataArchive.Add(ReportSISDIS015)
                        DialogGenerateFile(DataArchive)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = ReportSISDIS015.Message.ToString()
                    End If
                End If
            End Using
        End If
    End Sub

#End Region

End Class