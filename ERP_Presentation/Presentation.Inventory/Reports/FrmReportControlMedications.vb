#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Inventory.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportControlMedications

#Region "Variables"

    Private _idOperativeUnit As Integer
    Private _inventorySettings As SettingInventory

    Private _criterias As Dictionary(Of String, String)

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Async Sub FrmReportControlMedications_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Await LoadControls()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _inventorySettings = Nothing
        _criterias = Nothing
    End Sub

#End Region

#Region "Report"

    Private Async Sub INDSbGenrateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenrateReport.Click
        If ValidateControlsReports() = True Then
            Try
                AsyncLoader(True)

                Dim reporte As New rptControlMedications
                reporte.ParametrosReporte = New Object() {_criterias}
                INDDvReport.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                AsyncLoader(False)
                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDCdnDate.Focus()
                End If
            Catch ex As Exception
                AsyncLoader(False)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack en el control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnNavigationReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcReport.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Using model As New Presentation.Inventory.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportControlMedications(_criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportControlMedications As DataTable = ds.Tables("ReportControlMedications")

                        Await Task.Factory.StartNew(Sub()
                                                        chargueDatasource(dtReportControlMedications)
                                                    End Sub)

                        If Me.INDGcExportExcel.DataSource IsNot Nothing Then
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

#End Region

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
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        Using Model As New MSettingInventory(CStr(Me.Tag))
            AsyncLoader(True)
            Try
                Dim resulOperation = Await Model.GetInventorySettingsRegister(Me._idOperativeUnit)
                If resulOperation.ObjectEmbbeded Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SettingParameter", "Inventory"))
                    Exit Function
                End If

                _inventorySettings = resulOperation.ObjectEmbbeded
                INDCdnDate.SetMaxDate = New Date(_inventorySettings.Year, _inventorySettings.Month, 1)
                INDCdnDate.SetYear = _inventorySettings.Year
                INDCdnDate.SetMonth = _inventorySettings.Month
            Catch ex As Exception
                Throw ex
            Finally
                AsyncLoader(False)
            End Try
        End Using
    End Function

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim errors As New StringBuilder

        If _inventorySettings Is Nothing Then
            errors.AppendLine("No se encontraron parámetros de inventarios")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        _criterias = New Dictionary(Of String, String)
        _criterias.Add("Year", INDCdnDate.GetYear)
        _criterias.Add("Month", INDCdnDate.GetMonth)
        _criterias.Add("OperativeUnitId", _idOperativeUnit)

        Return True
    End Function

#Region "ToExcel"

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasource(ByVal dtReportControlMedications As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Nombre Genérico")
        dt.Columns.Add("Nombre Comercial")
        dt.Columns.Add("Concentración")
        dt.Columns.Add("Forma Farmaceutica")
        dt.Columns.Add("Saldo Anterior", GetType(Decimal))
        dt.Columns.Add("Cantidad Entradas", GetType(Decimal))
        dt.Columns.Add("Lab. Framaceutico / Mayorista")
        dt.Columns.Add("Cantidad Salidas", GetType(Decimal))
        dt.Columns.Add("No. Formulas", GetType(Decimal))
        dt.Columns.Add("Nuevo Saldo", GetType(Decimal))
        dt.Columns.Add(" ")

        For Each item In dtReportControlMedications.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Nombre Genérico") = item("GenericName")
            row.Item("Nombre Comercial") = item("TradeName")
            row.Item("Concentración") = item("Concentration")
            row.Item("Forma Farmaceutica") = item("PharmaceuticalForm")
            row.Item("Saldo Anterior") = item("PreviousBalance")
            row.Item("Cantidad Entradas") = item("QuantityIn")
            row.Item("Lab. Framaceutico / Mayorista") = item("PharmaceuticalLab")
            row.Item("Cantidad Salidas") = item("QuantityOut")
            row.Item("No. Formulas") = item("FormulaNumbers")
            row.Item("Nuevo Saldo") = item("NewBalance")
            row.Item(" ") = item("TypeName")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcel
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcel.DataSource = Nothing
        Me.INDGcExportExcel.RefreshDataSource()
    End Sub

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

#End Region

#End Region

End Class