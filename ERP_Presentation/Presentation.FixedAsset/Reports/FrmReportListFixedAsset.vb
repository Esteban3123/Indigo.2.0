#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
#End Region

Public Class FrmReportListFixedAsset
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon
#End Region

#Region "Properties"
    Public Property ProoftCloseXpoResponsible As XPInstantFeedbackSource
    Public Property ProoftCloseXpoClassification As XPInstantFeedbackSource
    Public Property ProoftCloseXpoStatusAsset As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEquipmentType As XPInstantFeedbackSource
    Public Property ProoftCloseXpoItem As XPInstantFeedbackSource
    Public Property ProoftCloseXpoPlate As XPInstantFeedbackSource
    Public Property ProoftCloseXpoLocation As XPCollection

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private List As List(Of FixedAssetPhysicalAssetReportXpo)

    Private _FillingGroupBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingGroupBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGroupBy Is Nothing Then
                _FillingGroupBy = New List(Of Tuple(Of Integer, String))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(1, "Catalogo"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(2, "Responsable"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(3, "Artículo"))
            End If
            Return _FillingGroupBy
        End Get
    End Property

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Detallado"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Resumido"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        ''Valida Fecha
        'If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
        '    Me.INDDateStart.Focus()
        '    Validations = False
        'ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
        '    Me.INDDateStart.Focus()
        '    Validations = False
        'End If

        'Valida si las fechas son nulas y si el Radicado esta vacio y la Factura esta vacia
        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            If Me.INDSleResponsible.EditValue Is Nothing And Me.INDSleClassification.EditValue Is Nothing And Me.INDSleStatusAsset.EditValue Is Nothing And Me.INDSleItemStart.EditValue Is Nothing And Me.INDSleEquipmentTypeStart.EditValue Is Nothing And Me.INDSlePlateStart.EditValue Is Nothing And Me.INDSleLocationStart.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
                Me.INDDateStart.Focus()
                Validations = False
            End If
        End If

        'Valida si la fecha inicial es mayor a la inicial
        If INDDateStart.EditValue IsNot Nothing And INDDateEnd.EditValue IsNot Nothing Then
            If Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
                Me.INDDateEnd.Focus()
                Validations = False
            End If
            'valida si alguna de las fechas tiene informacion  y el radiacado tiene datos ..para informar a obligar a completar las dos fechas
        ElseIf ((INDDateStart.EditValue IsNot Nothing Or INDDateEnd.EditValue IsNot Nothing) And Me.INDSleResponsible.EditValue IsNot Nothing) Or ((INDDateStart.EditValue IsNot Nothing Or INDDateEnd.EditValue IsNot Nothing) And Me.INDSleClassification.EditValue IsNot Nothing) Or ((INDDateStart.EditValue IsNot Nothing Or INDDateEnd.EditValue IsNot Nothing) And Me.INDSleStatusAsset.EditValue IsNot Nothing) Or ((INDDateStart.EditValue IsNot Nothing Or INDDateEnd.EditValue IsNot Nothing) And Me.INDSleItemStart.EditValue IsNot Nothing) Or ((INDDateStart.EditValue IsNot Nothing Or INDDateEnd.EditValue IsNot Nothing) And Me.INDSleEquipmentTypeStart.EditValue IsNot Nothing) Or ((INDDateStart.EditValue IsNot Nothing Or INDDateEnd.EditValue IsNot Nothing) And Me.INDSlePlateStart.EditValue IsNot Nothing) Or ((INDDateStart.EditValue IsNot Nothing Or INDDateEnd.EditValue IsNot Nothing) And Me.INDSleLocationStart.EditValue IsNot Nothing) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        End If

        'Valida Tipo de equipo
        If INDSleEquipmentTypeStart.EditValue Is Nothing And INDSleEquipmentTypeEnd.EditValue IsNot Nothing Or INDSleEquipmentTypeEnd.EditValue Is Nothing And INDSleEquipmentTypeStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblEquipmentType.Text)
            Me.INDSleEquipmentTypeStart.Focus()
            Validations = False
        ElseIf INDSleEquipmentTypeEnd.EditValue < INDSleEquipmentTypeStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblEquipmentType.Text)
            Me.INDSleEquipmentTypeStart.Focus()
            Validations = False
        End If

        'Valida Plate
        If INDSlePlateStart.EditValue Is Nothing And INDSlePlateEnd.EditValue IsNot Nothing Or INDSlePlateEnd.EditValue Is Nothing And INDSlePlateStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblPlate.Text)
            Me.INDSlePlateStart.Focus()
            Validations = False
        ElseIf INDSlePlateEnd.EditValue < INDSlePlateStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblPlate.Text)
            Me.INDSlePlateStart.Focus()
            Validations = False
        End If

        'Valida Location
        If INDSleLocationStart.EditValue Is Nothing And INDSleLocationEnd.EditValue IsNot Nothing Or INDSleLocationEnd.EditValue Is Nothing And INDSleLocationStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblLocation.Text)
            Me.INDSleLocationStart.Focus()
            Validations = False
        ElseIf INDSleLocationEnd.EditValue < INDSleLocationStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblLocation.Text)
            Me.INDSleLocationStart.Focus()
            Validations = False
        End If

        'Valida Artículo
        If INDSleItemStart.EditValue Is Nothing And INDSleItemEnd.EditValue IsNot Nothing Or INDSleItemEnd.EditValue Is Nothing And INDSleItemStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblItem.Text)
            Me.INDSleItemStart.Focus()
            Validations = False
        ElseIf INDSleItemEnd.EditValue < INDSleItemStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblItem.Text)
            Me.INDSleItemStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function

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

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla del tipo de reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingAdquisitionType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingAdquisitionType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingAdquisitionType Is Nothing Then
                _FillingAdquisitionType = New List(Of Tuple(Of Integer, String))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(0, "Todos"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(1, "Compra Directa"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(3, "Comodato"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(8, "Comodato Tercerizado"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(4, "Donación"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(5, "Traspaso de Bienes"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(6, "Otro Concepto"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(7, "Leasing Financiero"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(9, "Renting Financiero"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(10, "Renting Operativo"))
            End If
            Return _FillingAdquisitionType
        End Get
    End Property
#End Region

#Region "Eventos"
    ''' <summary>
    ''' cargamos el datasource de Responsable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleResponsible_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleResponsible.QueryPopUp
        If INDSleResponsible.Datasource Is Nothing Then
            LoadXpoResponsible()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource de Clasificación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleClassification_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleClassification.QueryPopUp
        If INDSleClassification.Datasource Is Nothing Then
            LoadXpoClassification()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource de Esatdo Activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleStatusAsset_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleStatusAsset.QueryPopUp
        If INDSleStatusAsset.Datasource Is Nothing Then
            LoadXpoStatusAsset()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource del Artículo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleItemStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleItemStart.QueryPopUp
        If INDSleItemStart.Datasource Is Nothing Then
            LoadXpoItemStart()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource del Artículo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleItemEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleItemEnd.QueryPopUp
        If INDSleItemEnd.Datasource Is Nothing Then
            LoadXpoItemEnd()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource del Tipo de Equipo Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleEquipmentTypeStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEquipmentTypeStart.QueryPopUp
        If INDSleEquipmentTypeStart.Datasource Is Nothing Then
            LoadXpoEquipmentTypeStart()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource del Tipo de Equipo Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks> 
    Private Sub INDSleEquipmentTypeEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEquipmentTypeEnd.QueryPopUp
        If INDSleEquipmentTypeEnd.Datasource Is Nothing Then
            LoadXpoEquipmentTypeEnd()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource de Placa Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlePlateStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePlateStart.QueryPopUp
        If INDSlePlateStart.Datasource Is Nothing Then
            LoadXpoPlateStart()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource de Placa Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlePlateEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePlateEnd.QueryPopUp
        If INDSlePlateEnd.Datasource Is Nothing Then
            LoadXpoPlateEnd()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource de Ubicación Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleLocationStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleLocationStart.QueryPopUp
        If INDSleLocationStart.Datasource Is Nothing Then
            LoadXpoLocationStart()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource de Ubicación Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleLocationEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleLocationEnd.QueryPopUp
        If INDSleLocationEnd.Datasource Is Nothing Then
            LoadXpoLocationEnd()
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoResponsible = Nothing
        ProoftCloseXpoClassification = Nothing
        ProoftCloseXpoStatusAsset = Nothing
        ProoftCloseXpoEquipmentType = Nothing
        ProoftCloseXpoItem = Nothing
        ProoftCloseXpoPlate = Nothing
        ProoftCloseXpoLocation = Nothing
        List = Nothing
        _FillingGroupBy = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Me.INDGcExportExcel.DataSource = chargueDatasource()
            If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                generateExcel()
            End If
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable

        Dim filtroConsulta As String = Nothing

        'filtro por fechas
        If INDDateStart.EditValue IsNot Nothing And INDDateEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "AdquisitionDate >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd") & "# AND AdquisitionDate <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd") & "#"
        End If

        'filtro por responsable
        If INDSleResponsible.EditValue IsNot Nothing Then
            If filtroConsulta Is Nothing Then
                filtroConsulta &= " ResponsibleId.ThirdPartyId.Nit = '" & INDSleResponsible.EditValue & "'"
            Else
                filtroConsulta &= " AND ResponsibleId.ThirdPartyId.Nit = '" & INDSleResponsible.EditValue & "'"
            End If
        End If

        'filtro por Catalogo
        If INDSleClassification.EditValue IsNot Nothing Then
            If filtroConsulta Is Nothing Then
                filtroConsulta &= " ItemId.ItemCatalogId.Code = '" & INDSleClassification.EditValue & "'"
            Else
                filtroConsulta &= " AND ItemId.ItemCatalogId.Code = '" & INDSleClassification.EditValue & "'"
            End If
        End If

        'filtro por Estados de Activos
        If INDSleStatusAsset.EditValue IsNot Nothing Then
            If filtroConsulta Is Nothing Then
                filtroConsulta &= " StatusAssetId.Id = " & INDSleStatusAsset.EditValue
            Else
                filtroConsulta &= " AND StatusAssetId.Id = " & INDSleStatusAsset.EditValue
            End If
        End If

        'filtro por Tipo de Equipo
        If INDSleEquipmentTypeStart.EditValue IsNot Nothing And INDSleEquipmentTypeEnd.EditValue IsNot Nothing Then
            If filtroConsulta Is Nothing Then
                filtroConsulta &= " ItemId.ItemTypeId.Code >= '" & INDSleEquipmentTypeStart.EditValue & "' AND ItemId.ItemTypeId.Code <= '" & INDSleEquipmentTypeEnd.EditValue & "'"
            Else
                filtroConsulta &= " AND ItemId.ItemTypeId.Code >= '" & INDSleEquipmentTypeStart.EditValue & "' AND ItemId.ItemTypeId.Code <= '" & INDSleEquipmentTypeEnd.EditValue & "'"
            End If
        End If

        'filtro por Placa
        If INDSlePlateStart.EditValue IsNot Nothing And INDSlePlateEnd.EditValue IsNot Nothing Then
            If filtroConsulta Is Nothing Then

                filtroConsulta &= " Plate >= '" & INDSlePlateStart.EditValue & "' AND Plate <= '" & INDSlePlateEnd.EditValue & "'"
            Else
                filtroConsulta &= " AND Plate >= '" & INDSlePlateStart.EditValue & "' AND Plate <= '" & INDSlePlateEnd.EditValue & "'"
            End If
        End If

        'filtro por Ubicación
        If INDSleLocationStart.EditValue IsNot Nothing And INDSleLocationEnd.EditValue IsNot Nothing Then
            If filtroConsulta Is Nothing Then
                filtroConsulta &= " LocationId.Code >= '" & INDSleLocationStart.EditValue & "' AND LocationId.Code <= '" & INDSleLocationEnd.EditValue & "'"
            Else
                filtroConsulta &= " AND LocationId.Code >= '" & INDSleLocationStart.EditValue & "' AND LocationId.Code <= '" & INDSleLocationEnd.EditValue & "'"
            End If
        End If

        'filtro por Artículo
        If INDSleItemStart.EditValue IsNot Nothing And INDSleItemEnd.EditValue IsNot Nothing Then
            If filtroConsulta Is Nothing Then
                filtroConsulta &= " ItemId.Code >= '" & INDSleItemStart.EditValue & "' AND ItemId.Code <= '" & INDSleItemEnd.EditValue & "'"
            Else
                filtroConsulta &= " AND ItemId.Code >= '" & INDSleItemStart.EditValue & "' AND ItemId.Code <= '" & INDSleItemEnd.EditValue & "'"
            End If
        End If

        'filtro por tipo de adquisición
        If INDGleAdquisitionType.EditValue <> 0 Then
            If filtroConsulta Is Nothing Then
                filtroConsulta &= " AdquisitionType = " & INDGleAdquisitionType.EditValue
            Else
                filtroConsulta &= " AND AdquisitionType = " & INDGleAdquisitionType.EditValue
            End If
        End If

        List = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetPhysicalAssetReportXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Placa")
        dt.Columns.Add("Artículo")
        dt.Columns.Add("Catálogo Bienes y Servicios")
        dt.Columns.Add("Tipo Adquisición")
        dt.Columns.Add("Serie")
        dt.Columns.Add("Marca")
        dt.Columns.Add("Modelo")
        dt.Columns.Add("Fecha Adquisición")
        dt.Columns.Add("Responsable")
        dt.Columns.Add("Ubicación")
        dt.Columns.Add("Catálogo")
        dt.Columns.Add("Fecha de la Compra")
        dt.Columns.Add("Número del ingreso")
        dt.Columns.Add("Número del comprobante de egreso")
        dt.Columns.Add("Fecha Salida")

        Dim columValueInvoice As DataColumn = New DataColumn
        columValueInvoice.DataType = System.Type.GetType("System.Decimal")
        columValueInvoice.AllowDBNull = False
        columValueInvoice.Caption = "Costo Total"
        columValueInvoice.ColumnName = "Costo Total"
        dt.Columns.Add(columValueInvoice)


        For Each itemView In List
            Dim row As DataRow = dt.NewRow()
            row.Item("Placa") = itemView.Plate
            row.Item("Responsable") = itemView.ResponsibleId.ThirdPartyId.NitName
            row.Item("Tipo Adquisición") = IIf(itemView.AdquisitionType = 1, "Compra Directa",
                                           IIf(itemView.AdquisitionType = 3, "Comodato",
                                           IIf(itemView.AdquisitionType = 4, "Donacion",
                                           IIf(itemView.AdquisitionType = 5, "Traspaso de Bienes",
                                           IIf(itemView.AdquisitionType = 6, "Otro Concepto",
                                           IIf(itemView.AdquisitionType = 7, "Leasing Financiero",
                                           IIf(itemView.AdquisitionType = 8, "Comodato Tercerizado",
                                           IIf(itemView.AdquisitionType = 9, "Renting Financiero",
                                           IIf(itemView.AdquisitionType = 10, "Renting Operativo",
                                               "Todos")))))))))
            row.Item("Ubicación") = itemView.LocationId.Code & " - " & itemView.LocationId.Name
            row.Item("Catálogo") = itemView.ItemId.ItemCatalogId.Code & " - " & itemView.ItemId.ItemCatalogId.Description
            row.Item("Catálogo Bienes y Servicios") = itemView.ItemId.CatalogOfPropertyandServicesId?.CodeDescription
            row.Item("Artículo") = itemView.ItemId.Code & " - " & itemView.ItemId.Description
            row.Item("Fecha Adquisición") = itemView.AdquisitionDate
            row.Item("Fecha de la Compra") = itemView.PurchaseDate
            row.Item("Número del ingreso") = itemView.EntryNumber
            row.Item("Número del comprobante de egreso") = itemView.VoucherTransactionNumber
            row.Item("Fecha Salida") = itemView.OutputDate
            row.Item("Serie") = itemView.Serie
            row.Item("Marca") = itemView.TrademarkId.Code & " - " & itemView.TrademarkId.Name
            row.Item("Modelo") = itemView.Model
            row.Item("Costo Total") = itemView.HistoricalValue

            dt.Rows.Add(row)
        Next
        AsyncLoader(False)
        If dt IsNot Nothing Then
            Return dt
        Else
            Return New DataTable
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDDateStart.Focus()
        End If
    End Function

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcel
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


    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            If INDGleTypeReport.EditValue = 1 Then
                AsyncLoader(True)
                Dim reporte As New rptListFixedAsset
                reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                          INDDateEnd.EditValue,
                                                          INDSleResponsible.EditValue,
                                                          INDSleClassification.EditValue,
                                                          INDSleStatusAsset.EditValue,
                                                          INDSleEquipmentTypeStart.EditValue,
                                                          INDSleEquipmentTypeEnd.EditValue,
                                                          INDSlePlateStart.EditValue,
                                                          INDSlePlateEnd.EditValue,
                                                          INDSleLocationStart.EditValue,
                                                          INDSleLocationEnd.EditValue,
                                                          INDSleItemStart.EditValue,
                                                          INDSleItemEnd.EditValue,
                                                          INDGleGroupBy.EditValue,
                                                          Me.BarraBotones.OperatingUnit.Id,
                                                          INDGleAdquisitionType.EditValue}

                INDDvViewReport.DocumentSource = reporte
                reporte.CargarDataSource()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateStart.Focus()
                End If
                AsyncLoader(False)
            Else
                AsyncLoader(True)
                Dim reporte As New rptListFixedAssetR
                reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                          INDDateEnd.EditValue,
                                                          INDSleResponsible.EditValue,
                                                          INDSleClassification.EditValue,
                                                          INDSleStatusAsset.EditValue,
                                                          INDSleEquipmentTypeStart.EditValue,
                                                          INDSleEquipmentTypeEnd.EditValue,
                                                          INDSlePlateStart.EditValue,
                                                          INDSlePlateEnd.EditValue,
                                                          INDSleLocationStart.EditValue,
                                                          INDSleLocationEnd.EditValue,
                                                          INDSleItemStart.EditValue,
                                                          INDSleItemEnd.EditValue,
                                                          INDGleGroupBy.EditValue,
                                                          Me.BarraBotones.OperatingUnit.Id,
                                                          INDGleAdquisitionType.EditValue}

                INDDvViewReport.DocumentSource = reporte
                reporte.CargarDataSource()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateStart.Focus()
                End If
                AsyncLoader(False)
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
        Me.INDDateStart.Focus()
    End Sub

    ''' <summary>
    ''' Evento Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportListFixedAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        'Me.INDSleSubAccountStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        'Me.INDSleSubAccountEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        'Me.INDSleGroupStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
        'Me.INDSleGroupEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode

        'Cargar GridLookUpEdit
        Me.INDGleGroupBy.Properties.DataSource = FillingGroupBy
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport

        'Dar un valor por defecto a los GridLookEdit
        INDGleGroupBy.EditValue = 1
        INDGleTypeReport.EditValue = 1

        INDSleEquipmentTypeStart.View.OptionsView.ShowGroupPanel = False
        INDSleEquipmentTypeEnd.View.OptionsView.ShowGroupPanel = False
        INDSlePlateStart.View.OptionsView.ShowGroupPanel = False
        INDSlePlateEnd.View.OptionsView.ShowGroupPanel = False
        INDSleLocationStart.View.OptionsView.ShowGroupPanel = False
        INDSleLocationEnd.View.OptionsView.ShowGroupPanel = False
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleResponsible
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoResponsible()
        Using msearch As New MBusqueda
            ProoftCloseXpoResponsible = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetResponsible)
            INDSleResponsible.Datasource = ProoftCloseXpoResponsible
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleClassification
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoClassification()
        Using msearch As New MBusqueda
            ProoftCloseXpoClassification = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentCatalog)
            INDSleClassification.Datasource = ProoftCloseXpoClassification
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleStatusAsset
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoStatusAsset()
        Using msearch As New MBusqueda
            ProoftCloseXpoStatusAsset = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetStatusAsset)
            INDSleStatusAsset.Datasource = ProoftCloseXpoStatusAsset
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleItemStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoItemStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoItem = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipment)
            INDSleItemStart.Datasource = ProoftCloseXpoItem
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleItemEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoItemEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoItem = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipment)
            INDSleItemEnd.Datasource = ProoftCloseXpoItem
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleEquipmentTypeStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoEquipmentTypeStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoEquipmentType = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentType)
            INDSleEquipmentTypeStart.Datasource = ProoftCloseXpoEquipmentType
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleEquipmentTypeEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoEquipmentTypeEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoEquipmentType = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentType)
            INDSleEquipmentTypeEnd.Datasource = ProoftCloseXpoEquipmentType
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlePlateStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoPlateStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoPlate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPhysicalAsset)
            INDSlePlateStart.Datasource = ProoftCloseXpoPlate
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlePlateEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoPlateEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoPlate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPhysicalAsset)
            INDSlePlateEnd.Datasource = ProoftCloseXpoPlate
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleLocationStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoLocationStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoLocation = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetLocation)
            INDSleLocationStart.Datasource = ProoftCloseXpoLocation
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleLocationEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoLocationEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoLocation = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetLocation)
            INDSleLocationEnd.Datasource = ProoftCloseXpoLocation
        End Using
    End Sub

    Private Sub FrmReportListFixedAsset_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleAdquisitionType.Properties.DataSource = FillingAdquisitionType
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleAdquisitionType.EditValue = 0
    End Sub
#End Region
End Class