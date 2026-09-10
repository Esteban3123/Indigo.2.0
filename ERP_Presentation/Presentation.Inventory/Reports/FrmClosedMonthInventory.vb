'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Juan Carlos Bermudez
' Created          : 25-06-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities.ObjectChangeTracker
Imports Domain.Entities
Imports System.Text
Imports Presentation.Inventory.MVP
Imports DevExpress.Xpo
Imports Presentation.Reporter
Imports DevExpress.XtraBars.Docking
Imports DevExpress.Data
Imports System.Drawing
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraGrid.Columns
Imports System.Windows.Forms

#End Region

Public Class FrmClosedMonthInventory
    Implements IClosedMonthInventory

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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


    Public WriteOnly Property ActionsOnControls As Boolean Implements IClosedMonthInventory.ActionsOnControls
        Set(value As Boolean)

        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IClosedMonthInventory.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IClosedMonthInventory.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property YearClosed As Integer Implements IClosedMonthInventory.YearClosed

    Public Property MonthClosed As Integer Implements IClosedMonthInventory.MonthClosed

#End Region

#Region "Globals"

    ''' <summary>
    ''' Constante con el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Inventory"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PClosedMonthInventory

    ''' <summary>
    ''' Representa la entidad de parametros de pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim _inventorySettings As SettingInventory

    ''' <summary>
    ''' diccionario para obtenes los rangos
    ''' </summary>
    Private _filters As Dictionary(Of String, String)


    ''' <summary>
    ''' Guarda el mes parametrizado en el sistema
    ''' </summary>
    Private MonthClosedInventorySettings As Integer

#End Region

#Region "ICRUD"

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls(False)
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        ShowPannelBase()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = False
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

#End Region

#Region "Methods"

    Private Async Function LoadControls() As Task
        Using Model As New MSettingInventory(CStr(Me.Tag))
            AsyncLoader(True)
            Try
                Dim resulOperation = Await Model.GetInventorySettingsRegister(Me._idOperativeUnit)
                If resulOperation.ObjectEmbbeded Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SettingParameter", "Inventory"))
                    ActionsOnControls = False
                    Exit Function
                End If
                ActionsOnControls = True
                _inventorySettings = resulOperation.ObjectEmbbeded
                MonthClosed = _inventorySettings.Month
                YearClosed = _inventorySettings.Year
                INDdnMontClose.SetMonth = MonthClosed
                INDdnMontClose.SetYear = YearClosed
                MonthClosedInventorySettings = _inventorySettings.Month
            Catch ex As Exception
                Throw ex
            Finally
                AsyncLoader(False)
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls(ByVal BanNewOperatingUnit As Boolean)
        If BanNewOperatingUnit Then
            _inventorySettings = Nothing
        End If
        INDgcDocuments.DataSource = Nothing
        INDlcgDocumentData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDgcSummary.DataSource = Nothing
        INDgcReconciliationMovements.DataSource = Nothing
        INDgcValuedInventory.DataSource = Nothing
        INDdnMontClose.Enabled = True
        MonthClosed = INDdnMontClose.GetMonth
        YearClosed = INDdnMontClose.GetYear
    End Sub


    ''' <summary>
    ''' Carga segmentos para reporte del cierre
    ''' </summary>
    Private Sub loadReport()
        AsyncLoader(True)
        INDlcgDocumentData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = True
        ShowPannelReport()
        INDtbcMain.SelectedTabPageIndex = 0
        GetSummaryData()
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Realiza el cierre mensual
    ''' </summary>
    Private Async Function ClosedMonth() As Task
        Using Model As New MClosedMonthInventory(Me.Tag.ToString())
            Try
                AsyncLoader(True)
                Dim result = Await Model.ClosedMonthInventory(MonthClosed, YearClosed, _idOperativeUnit, True)
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                If result.StateResult Then
                    If result.ObjectEmbbeded.Documents.Count > 0 Then
                        INDgcDocuments.DataSource = result.ObjectEmbbeded.Documents
                        INDlcgDocumentData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Else
                        loadReport()
                    End If
                End If
            Catch ex As Exception
                Throw ex
            Finally
                AsyncLoader(False)
            End Try
        End Using


    End Function

    ''' <summary>
    ''' Verifica si existen documentos pendientes de confirmación para el mes y año cerrados.
    ''' Realiza la validación de manera asíncrona
    ''' </summary>
    ''' <returns></returns>
    Private Async Function VerifyUnconfirmedDocument() As Task
        Using Model As New MClosedMonthInventory(Me.Tag.ToString())
            Try
                AsyncLoader(True)
                Dim result = Await Model.VerifiyHasConfirmAllDocuments(MonthClosed, YearClosed)
                If result.StateResult Then
                    INDgcDocuments.DataSource = Nothing
                    INDlcgDocumentData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlciMontClose.Enabled = False
                    If result.ObjectEmbbeded.Count > 0 Then
                        INDgcDocuments.DataSource = result.ObjectEmbbeded
                        INDlciMontClose.Enabled = True
                    Else
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = True
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = False
                        INDlciMontClose.Enabled = False
                    End If
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            Catch ex As Exception
                INDlciMontClose.Enabled = True
                Throw ex
            Finally
                AsyncLoader(False)
            End Try
        End Using
    End Function


    ''' <summary>
    ''' Muestra el panel inicial
    ''' </summary>
    Private Sub ShowPannelBase()
        INDPanelControlBase.Visible = False
        INDPanelControlReport.Visible = False
        INDPanelControlReport.Dock = DockStyle.None
        INDPanelControlBase.Dock = DockStyle.Fill
        INDPanelControlBase.Visible = True
    End Sub


    ''' <summary>
    ''' Muestra el panel del reporte
    ''' </summary>
    Private Sub ShowPannelReport()
        INDPanelControlBase.Visible = False
        INDPanelControlReport.Visible = True
        INDPanelControlReport.Dock = Dock.Fill
        INDLcTCompany.Text = indigo.IndigoCompanyName + " - CIERRE MENSUAL DE INVENTARIOS"
        INDLcTCompany.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        INDLcTCompany.Font = New Font("Segoe UI", 26.25, FontStyle.Bold)
    End Sub



    ''' <summary>
    ''' Verifica si el mes y año indicados ya fueron cerrados o no
    ''' </summary>
    ''' <returns>True / False</returns>
    Private Function ValidateClosedMonth() As Boolean
        Dim ClosedMonth = Presenter.GetClosedMonth(YearClosed, MonthClosed)
        If ClosedMonth IsNot Nothing And ClosedMonth.Count > 0 Then
            loadReport()
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Obtiene informacion a mostrar en la pestaña Resumen
    ''' </summary>
    Private Sub GetSummaryData()
        If INDgcSummary.DataSource Is Nothing Then
            Using Model As New MClosedMonthInventory(Me.Tag.ToString())
                Dim result = Model.GetMonthlyClosureSummary(YearClosed, MonthClosed)
                INDgcSummary.DataSource = result.ObjectEmbbeded
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Obtiene informacion para mostrar en la pestaña Conciliación de movimientos
    ''' </summary>
    Private Sub GetReconciliationMovementsData()
        If INDgcReconciliationMovements.DataSource Is Nothing Then
            INDbgvReconciliationMovements.Columns(0).GroupIndex = 0
            INDgcReconciliationMovements.DataSource = Presenter.ListViewConciliationMovements(YearClosed, MonthClosed)
            ConfigureColumnSummaries(INDbgvReconciliationMovements)
            INDbgvReconciliationMovements.RefreshData()
            INDbgvReconciliationMovements.ExpandAllGroups()
        End If
    End Sub


    ''' <summary>
    ''' Obtiene informacion a mostrar en la pestaña inventario valorizado
    ''' </summary>
    Private Sub GetValuedInventoryData()
        If INDgcValuedInventory.DataSource Is Nothing Then
            INDgcValuedInventory.DataSource = Presenter.ListViewValuedInventory(YearClosed, MonthClosed)
        End If
    End Sub


    ''' <summary>
    ''' Configura las columnas resumenes
    ''' </summary>
    ''' <param name="view"></param>
    Private Sub ConfigureColumnSummaries(view As BandedGridView)
        For Each column As GridColumn In view.Columns
            column.Summary.Clear()
        Next
        Dim groupSummaryItem1 As New DevExpress.XtraGrid.GridGroupSummaryItem() With {
            .FieldName = "TotalDebitAccounting",
            .SummaryType = DevExpress.Data.SummaryItemType.Sum,
            .DisplayFormat = "{0:c2}",
            .ShowInGroupColumnFooter = view.Columns("TotalDebitAccounting")
        }

        Dim groupSummaryItem2 As New DevExpress.XtraGrid.GridGroupSummaryItem() With {
            .FieldName = "TotalCreditAccounting",
            .SummaryType = DevExpress.Data.SummaryItemType.Sum,
            .DisplayFormat = "{0:c2}",
            .ShowInGroupColumnFooter = view.Columns("TotalCreditAccounting")
        }
        Dim groupSummaryItem3 As New DevExpress.XtraGrid.GridGroupSummaryItem() With {
            .FieldName = "TotalDebitInventory",
            .SummaryType = DevExpress.Data.SummaryItemType.Sum,
            .DisplayFormat = "{0:c2}",
            .ShowInGroupColumnFooter = view.Columns("TotalDebitInventory")
        }
        Dim groupSummaryItem4 As New DevExpress.XtraGrid.GridGroupSummaryItem() With {
            .FieldName = "TotalCreditInventory",
            .SummaryType = DevExpress.Data.SummaryItemType.Sum,
            .DisplayFormat = "{0:c2}",
            .ShowInGroupColumnFooter = view.Columns("TotalCreditInventory")
        }
        Dim groupSummaryItem5 As New DevExpress.XtraGrid.GridGroupSummaryItem() With {
            .FieldName = "DifferenceDebit",
            .SummaryType = DevExpress.Data.SummaryItemType.Sum,
            .DisplayFormat = "{0:c2}",
            .ShowInGroupColumnFooter = view.Columns("DifferenceDebit")
        }
        Dim groupSummaryItem6 As New DevExpress.XtraGrid.GridGroupSummaryItem() With {
            .FieldName = "DifferenceCredit",
            .SummaryType = DevExpress.Data.SummaryItemType.Sum,
            .DisplayFormat = " {0:c2}",
            .ShowInGroupColumnFooter = view.Columns("DifferenceCredit")
        }

        Dim groupSummaryItem7 As New DevExpress.XtraGrid.GridGroupSummaryItem() With {
            .FieldName = "EntityName",
            .SummaryType = DevExpress.Data.SummaryItemType.Custom,
            .DisplayFormat = "Totales    ",
            .ShowInGroupColumnFooter = view.Columns("EntityName")
        }

        view.GroupSummary.Clear()
        ' Añade el resumen de grupo al GridView
        view.GroupSummary.Add(groupSummaryItem1)
        view.GroupSummary.Add(groupSummaryItem2)
        view.GroupSummary.Add(groupSummaryItem3)
        view.GroupSummary.Add(groupSummaryItem4)
        view.GroupSummary.Add(groupSummaryItem5)
        view.GroupSummary.Add(groupSummaryItem6)
        view.GroupSummary.Add(groupSummaryItem7)

        ' Configurar resúmenes para las columnas
        view.Columns("TotalDebitAccounting").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalDebitAccounting", "{0:c2}")
        view.Columns("TotalCreditAccounting").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalCreditAccounting", "{0:c2}")
        view.Columns("TotalDebitInventory").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalDebitInventory", "{0:c2}")
        view.Columns("TotalCreditInventory").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalCreditInventory", "{0:c2}")
        view.Columns("DifferenceDebit").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "DifferenceDebit", "{0:c2}")
        view.Columns("DifferenceCredit").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "DifferenceCredit", "{0:c2}")


    End Sub


    ''' <summary>
    ''' Obtiene informacion del informe Power BI
    ''' </summary>
    Private Sub GetFinancialPerformanceData()
        Dim embedUrl As String = $"https://app.powerbi.com/groups/me/reports/254f737c-507e-446a-abaf-407ed73dcaa3/ReportSectionbf797e187b023dbb7653?experience=power-bi"
        Dim html As String = $"
        <html>
        <body>
            <iframe width='100%' height='100%' src='{embedUrl}' frameborder='0' allowFullScreen='true'></iframe>
        </body>
        </html>"
        INDWbFinancialPerformance.DocumentText = html
    End Sub


#End Region

#Region "Handles"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        Presenter = Nothing
        _inventorySettings = Nothing
    End Sub

    Private Async Sub FrmClosedMonthInventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowPannelBase()
        ShowCustomButtons()
        INDlciMontClose.Enabled = True
        Me.LayoutControls.SetIsCustomizable(Me.INDLcClosedMonthInventory, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PClosedMonthInventory(Me)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BeginInvoke(Sub() InitializeAsyncOperations())
    End Sub


    ''' <summary>
    ''' Carga las funciones iniciales asicronicas
    ''' </summary>
    ''' <returns></returns>
    Private Async Function InitializeAsyncOperations() As Task
        Await LoadControls()
        If Not ValidateClosedMonth() Then
            Await VerifyUnconfirmedDocument()
        End If
    End Function



    ''' <summary>
    ''' Customiza los botones de la barra de botones
    ''' </summary>
    Private Sub ShowCustomButtons()
        BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Procesar, "Procesar cierre")
        BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Validar, "Verificar")
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = False
        BarraBotones.ChangeButtonLargeImageWithImage(EbuttonsWithoutPermission.Procesar, My.Resources.Procesar_Cierre)
    End Sub

#End Region

#Region "Click"


#End Region

#Region "GridControl"


    Private Sub INDtbcMain_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDtbcMain.SelectedPageChanged
        Me.Cursor = ChangeCursorIndigo()
        If INDtbcMain.SelectedTabPageIndex = 0 Then
            GetSummaryData()
        ElseIf INDtbcMain.SelectedTabPageIndex = 1 Then
            GetReconciliationMovementsData()
        ElseIf INDtbcMain.SelectedTabPageIndex = 2 Then
            GetValuedInventoryData()
        ElseIf INDtbcMain.SelectedTabPageIndex = 3 Then
            GetFinancialPerformanceData()
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    ''' <summary>
    ''' Evento para mostrar la diferencia de las dos columnas de la tabla de resumen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvSummary_CustomSummaryCalculate(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles INDgvSummary.CustomSummaryCalculate
        Dim view = sender
        If e.SummaryProcess = CustomSummaryProcess.Finalize Then
            Dim rowHandle1 As Integer = view.GetRowHandle(0) ' Fila 1
            Dim rowHandle2 As Integer = view.GetRowHandle(1) ' Fila 2
            Dim value1 As Decimal = Convert.ToDecimal(view.GetRowCellValue(rowHandle1, "NewBalance"))
            Dim value2 As Decimal = Convert.ToDecimal(view.GetRowCellValue(rowHandle2, "NewBalance"))
            e.TotalValue = value1 - value2
        End If
    End Sub


    ''' <summary>
    ''' Envento para cambiar propiedades de apariencia de  fila totales 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbgvReconciliationMovements_CustomDrawRowFooterCell(sender As Object, e As Views.Grid.FooterCellCustomDrawEventArgs) Handles INDbgvReconciliationMovements.CustomDrawRowFooterCell
        If e.Column.FieldName = "EntityName" Then
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
            e.DefaultDraw()
            e.Handled = True
        End If
    End Sub
#End Region


#End Region

#Region "BarButtons"

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            BarraBotones.StatusRecordVisible = False
            CleanControls(True)
            Await LoadControls()
            If _inventorySettings IsNot Nothing AndAlso _inventorySettings.Id > 0 Then
                _inventorySettings.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
        End If
    End Sub

    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDLcClosedMonthInventory.Visible = True
        Me.CtrNavigationControl1.Visible = True
        Me.ToolBars.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDCnReport.Visible = False
    End Sub

    Private Async Function BarraBotones_ClickProcesar() As Task Handles BarraBotones.ClickProcesar
        Await ClosedMonth()
    End Function

    Private Async Function BarraBotones_Click_ValidarAsync() As Task Handles BarraBotones.Click_Validar
        Dim currentDate As DateTime = DateTime.Now
        If Not ValidateClosedMonth() Then
            If MonthClosed < MonthClosedInventorySettings Then
                Mensaje(EeventViewerImages.Advertencia) = "No se ha encontrado información del cierre con el periodo indicado"
                Exit Function
            Else
                Await VerifyUnconfirmedDocument()
            End If
        End If
    End Function

    Private Sub INDdnMontClose_OnChangeDate(sender As Object, e As EventArgs) Handles INDdnMontClose.OnChangeDate
        Dim currentDate As DateTime = DateTime.Now
        MonthClosed = INDdnMontClose.GetMonth
        YearClosed = INDdnMontClose.GetYear
        ' Verifica si el año o el mes seleccionado es mayor al actual
        If YearClosed > currentDate.Year OrElse (YearClosed = currentDate.Year AndAlso MonthClosed > currentDate.Month) Then
            INDdnMontClose.SetMonth = currentDate.Month
            INDdnMontClose.SetYear = currentDate.Year

            MonthClosed = INDdnMontClose.GetMonth
            YearClosed = INDdnMontClose.GetYear
            Exit Sub
        End If
    End Sub



#End Region

End Class