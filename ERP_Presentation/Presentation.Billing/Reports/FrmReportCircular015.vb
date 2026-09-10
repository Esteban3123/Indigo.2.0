#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports System.Text
Imports Presentation.CloudAgent
Imports System.IO

#End Region

Public Class FrmReportCircular015

#Region "properties"
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

#End Region

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
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        If INDDeDateStart.EditValue Is Nothing OrElse INDDeDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False
        ElseIf Me.INDDeDateStart.EditValue > INDDeDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function


    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    ''' <summary>
    ''' Retorna el Xml a generar
    ''' </summary>
    ''' <returns></returns>
    Private Async Function hargeDatasource() As Task(Of StringBuilder)

        'Variable que se devuelve para generar el arvhivo plano
        Dim result As New StringBuilder()

        'Se Obtiene el XML del Stored Procedure
        Dim Data As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReportCircular015Async(INDDeDateStart.EditValue, INDDeDateEnd.EditValue, IndigoSessionValues)

        'Se almacena el salto de linea
        Dim lineDet As String = vbCrLf

        'Se arma la cabecera del archivo plano   
        result.Append("<?xml version='1.0' encoding='utf-8'?>")
        'Se realiza un salto de linea para dar un poco de orden al XML
        result.Append(lineDet)
        result.Append("<ST006>")

        'DataTable 
        Dim dt As New DataTable
        dt = Data.Tables("ReportCircular015")

        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("Column1") IsNot DBNull.Value Then
                result.Append(lineDet)
                result.Append(dt.Rows(0).Item("Column1"))
            End If
        End If
        result.Append(lineDet)
        result.Append("</ST006>")
        Return result
    End Function



    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim DataArchive As StringBuilder

            DataArchive = Await hargeDatasource()
            AsyncLoader(False)
            DialogGenerateFile(DataArchive)
        End If
    End Sub


    ''' <summary>
    ''' Metodo que despliega show dialog para guardar un archivo Xml
    ''' </summary>
    ''' <param name="content"></param>
    ''' <remarks></remarks>
    Public Sub DialogGenerateFile(content As StringBuilder)
        Try
            Dim save As System.Windows.Forms.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
            save.Filter = "Texto|*.xml"
            save.Title = "ARCHIVO XML CIRCULAR EXTERNA 015 " & Format(CDate(INDDeDateStart.EditValue), "ddMMyyyy") & " Al " & Format(CDate(INDDeDateEnd.EditValue), "ddMMyyyy")
            save.FileName = "ARCHIVO XML CIRCULAR EXTERNA 015 " & Format(CDate(INDDeDateStart.EditValue), "ddMMyyyy") & " Al " & Format(CDate(INDDeDateEnd.EditValue), "ddMMyyyy")
            If save.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim file = save.OpenFile()
                Dim streamWrite As New StreamWriter(file)
                streamWrite.Write(content)
                streamWrite.Flush()
                streamWrite.Close()
                If MessageIndigo.Show("Desea Abrir el Archivo", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Process.Start(save.FileName)
                End If
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "Ocurrió un Error Creando el Archivo"
        End Try
    End Sub

    ''' <summary>
    '''  Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportCircular015_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDDeDateStart.Focus()
    End Sub
End Class