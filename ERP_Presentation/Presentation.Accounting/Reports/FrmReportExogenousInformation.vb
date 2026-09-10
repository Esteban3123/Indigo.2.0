'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/04/2017
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Presentation.Accounting.MVP
Imports Presentation.Base

#End Region

Public Class FrmReportExogenousInformation
    Implements IReportExogenousInformation

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PReportExogenousInformation

    ''' <summary>
    ''' Almacena pares de clave-valor, donde cada uno será de tipo String respectivamente
    ''' </summary>
    Dim _criterias As Dictionary(Of String, String)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad que obtiene el año de reporte de información exogena
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Year As Integer? Implements IReportExogenousInformation.Year
        Get
            Return INDcdnYear.GetYear
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que obtiene el Id del formato del reporte de información exogena
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ExogenousFormatId As Integer? Implements IReportExogenousInformation.ExogenousFormatId
        Get
            Return INDsleFormat.EditValue
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que obtiene el formato del reporte de información exogena
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Format As String Implements IReportExogenousInformation.Format
        Get
            Return INDsleFormat.Text
        End Get
    End Property

    ''' <summary>
    '''  Propiedad que obtiene la versión del reporte de información exogena
    ''' </summary>
    ''' <returns></returns>
    Private Property Version As Integer? Implements IReportExogenousInformation.Version
        Get
            Return INDtxtVersion.EditValue
        End Get
        Set(value As Integer?)
            INDtxtVersion.EditValue = value
        End Set
    End Property

    ''' <summary>
    '''  Propiedad que obtiene el concepto del reporte de información exogena
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Concept As Integer? Implements IReportExogenousInformation.Concept
        Get
            Return INDgleConcept.EditValue
        End Get
    End Property

    ''' <summary>
    '''  Propiedad que obtiene el número de envío del reporte de información exogena
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SendingNumber As Integer? Implements IReportExogenousInformation.SendingNumber
        Get
            Return INDtxtSendingNumber.EditValue
        End Get
    End Property

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Propiedad que proporciona una forma de obtener y establecer la colección de formatos exógenos a través de la interfaz IReportExogenousInformation
    ''' </summary>
    ''' <returns></returns>
    Public Property FormatXpo As XPCollection(Of Infrastructure.Data.Xpo.AccountingRepository.ExogenousFormatXpo) Implements IReportExogenousInformation.FormatXpo
        Get
            Return INDsleFormat.Properties.DataSource
        End Get
        Set(value As XPCollection(Of Infrastructure.Data.Xpo.AccountingRepository.ExogenousFormatXpo))
            INDsleFormat.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Variable que contiene la lista los conceptos
    ''' </summary>
    Private _ListConcept As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Devuelve una lista de tuplas que representan conceptos (Inserción, Reemplazo)
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property ListConcept As List(Of Tuple(Of Integer, String))
        Get
            If _ListConcept Is Nothing Then
                _ListConcept = New List(Of Tuple(Of Integer, String))
                _ListConcept.Add(New Tuple(Of Integer, String)(1, "Inserción"))
                _ListConcept.Add(New Tuple(Of Integer, String)(2, "Reemplazo"))
            End If
            Return _ListConcept
        End Get
    End Property

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
#End Region

#Region "BarButtons"

    ''' <summary>
    ''' Load de la barra botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Methods"


    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        Dim errors As New StringBuilder

        If Year Is Nothing Then
            errors.AppendLine("Debe seleccionar un año")
        End If

        If ExogenousFormatId Is Nothing Then
            errors.AppendLine("Debe seleccionar un formato")
        End If

        If Concept Is Nothing Then
            errors.AppendLine("Debe seleccionar un concepto")
        End If

        If SendingNumber Is Nothing Then
            errors.AppendLine("Debe indicar el número de envío")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        _criterias = New Dictionary(Of String, String)
        _criterias.Add("Year", Year)
        _criterias.Add("ExogenousFormatId", ExogenousFormatId)
        _criterias.Add("Format", Format)
        _criterias.Add("Version", Version)
        _criterias.Add("Concept", Concept)
        _criterias.Add("SendingNumber", SendingNumber)

        Return True
    End Function

    ''' <summary>
    ''' creamos un datatable para generar el excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasource(ByVal dtReportEstimateCosts As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Nombre")
        dt.Columns.Add("Tipo de Centro")
        dt.Columns.Add("Gastos Directos", GetType(Decimal))
        dt.Columns.Add("Gastos Variables", GetType(Decimal))
        dt.Columns.Add("Mano de Obra Directa", GetType(Decimal))
        dt.Columns.Add("Mano de Obra Indirecta", GetType(Decimal))
        dt.Columns.Add("Activos Fijos", GetType(Decimal))
        dt.Columns.Add("Dispensación", GetType(Decimal))
        dt.Columns.Add("Consumo", GetType(Decimal))
        dt.Columns.Add("Total", GetType(Decimal))
        dt.Columns.Add("Ventas", GetType(Decimal))

        For Each item In dtReportEstimateCosts.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = item("Code")
            row.Item("Nombre") = item("Name")
            row.Item("Tipo de Centro") = item("CenterTypeName")
            row.Item("Gastos Directos") = item("DirectCostDistribution")
            row.Item("Gastos Variables") = item("AutoCostDistribution")
            row.Item("Mano de Obra Directa") = item("ManPowerDistributionDirect")
            row.Item("Mano de Obra Indirecta") = item("ManPowerDistributionInDirect")
            row.Item("Activos Fijos") = item("FixedAssetDistribution")
            row.Item("Dispensación") = item("DispensingDistribution")
            row.Item("Consumo") = item("TransferDistribution")
            row.Item("Total") = item("InitialDistribution")
            row.Item("Ventas") = item("TotalSales")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmReportExogenousInformation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia
        _presenter = New PReportExogenousInformation(Me)

        'Cargar GridLookUpEdit        
        INDgleConcept.Properties.DataSource = ListConcept

        'Dar un valor por defecto a los GridLookEdit        
        INDgleConcept.EditValue = 1
        INDtxtSendingNumber.EditValue = 1
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando el formulario es eliminado y sus recursos deben ser liberados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _criterias = Nothing
        _ListConcept = Nothing
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmReportExogenousInformation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDcdnYear.SetYear = GetDateServer().Year - 1
        INDsleFormat.Focus()
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDsleFormat
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFormat_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleFormat.QueryPopUp
        If INDsleFormat.Properties.DataSource Is Nothing Then
            _presenter.ListCollectionExogenousFormat()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDsleFormat
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFormat_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFormat.EditValueChanged
        If ExogenousFormatId IsNot Nothing Then
            Dim exogenousFormat = DirectCast(INDsleFormat.GetSelectedObject(), Infrastructure.Data.Xpo.AccountingRepository.ExogenousFormatXpo)
            If exogenousFormat Is Nothing Then
                exogenousFormat = _presenter.GetExogenousFormatById(ExogenousFormatId)
            End If
            Me.Version = exogenousFormat.Version
        Else
            Me.Version = Nothing
        End If
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDbtnGenerate.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim ListResult As New List(Of ActionResult(Of String))
            Using Modelo As New MExogenousFormat(Me.Tag)
                ListResult.Add(Await Modelo.GenerateFormatExogena(_criterias))
            End Using
            AsyncLoader(False)

            DialogGenerateFile(ListResult)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que despliega show dialog para guardar un archivo plano
    ''' </summary>
    ''' <param name="resultGenerateFile"></param>
    ''' <remarks></remarks>
    Public Sub DialogGenerateFile(ByVal resultGenerateFile As List(Of ActionResult(Of String)))
        Try
            If resultGenerateFile.Count > 0 Then
                Dim Folder As New FolderBrowserDialog
                Folder.Description = "Seleccione ruta donde se guardarám XML exogena"
                Folder.ShowNewFolderButton = True
                Folder.SelectedPath = Environment.SpecialFolder.ProgramFiles
                If Folder.ShowDialog() = DialogResult.OK Then
                    Dim ListFiles As String = String.Empty
                    For i As Integer = 0 To resultGenerateFile.Count() - 1
                        If resultGenerateFile.Item(i).StateResult = True Then
                            File.WriteAllText(Path.Combine(Folder.SelectedPath, resultGenerateFile.Item(i).Message.ToString() & ".xml"), resultGenerateFile.Item(i).ObjectEmbbeded.ToString())
                            If i = 0 Then
                                ListFiles = resultGenerateFile.Item(i).Message.ToString()
                            Else
                                ListFiles = ListFiles & " - " & resultGenerateFile.Item(i).Message.ToString()
                            End If
                        End If
                    Next
                    Mensaje(EeventViewerImages.Informacion) = "Se han creado los Archivos Exogena: " & ListFiles & " correctamente"
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se ha generado ningún archivo"
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "Ocurrió un Error Creando los Archivos"
        End Try
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Using model As New MExogenousFormat(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportExogenousFormat(_criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Me.INDGcExportExcell.DataSource = ds.Tables("ReportExogenousFormat")

                        If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                            generateExcel()
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    End If
                End Using
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & String.Format("F{0} (V-{1})", Format, Version) & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcell.DataSource = Nothing
        Me.INDGcExportExcell.RefreshDataSource()
    End Sub

#End Region

#End Region

End Class