'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Johan Sebastian Carranza Ramos
' Created          : 1/10/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


Imports System.Text
Imports DevExpress.XtraGrid.Views.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Reporter

Public Class FrmAdmissionsStadistical

#Region "Variables"
    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PAdmissionsStadistical
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Se obtienen los tipos de facturas
    ''' </summary>
    Dim invoiceTypeFilter As String
    ''' <summary>
    ''' Se obtienen los id de los terceros
    ''' </summary>
    Dim thirdPartyIdsFilter As String
    ''' <summary>
    ''' Se obtienen los id de las entidades
    ''' </summary>
    Dim healthAdministratorIdsFilter As String
    ''' <summary>
    ''' Se obtienen los id de los grupos de atención
    ''' </summary>
    Dim careGroupIdsFilter As String
    ''' <summary>
    ''' Se obtienen los códigos de los usuarios
    ''' </summary>
    Dim userCodesFilter As String
    ''' <summary>
    ''' Se obtienen los códigos de los centros de atención
    ''' </summary>
    Dim careCenterCodesFilter As String

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad para registar el mensaje en el visor
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
    ''' Terceros
    ''' </summary>
    ''' <returns></returns>
    Public Property ThirdPartyId As String
    ''' <summary>
    ''' Entidad
    ''' </summary>
    ''' <returns></returns>
    Public Property EntityId As String
    ''' <summary>
    ''' CareGroup
    ''' </summary>
    ''' <returns></returns>
    Public Property CareGroupId As String
    ''' <summary>
    ''' User
    ''' </summary>
    ''' <returns></returns>
    Public Property UserId As String
    ''' <summary>
    ''' CareCenter
    ''' </summary>
    ''' <returns></returns>
    Public Property CareCenterId As String
    ''' <summary>
    ''' Functionalunit
    ''' </summary>
    ''' <returns></returns>
    Public Property FunctionalunitId As String
    ''' <summary>
    ''' Estados
    ''' </summary>
    ''' <returns></returns>
    Public Property StatesId As String
#End Region

#Region "Events"
    ''' <summary>
    ''' Evento Load formulario estadistico de ingresos.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAdmissionsStadistical_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PAdmissionsStadistical
    End Sub
    ''' <summary>
    ''' Evento clic del boton de generar reporte.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbGenerarReporte_Click(sender As Object, e As EventArgs) Handles INDSbGenerarReporte.Click
        If ValidateControlsReport() = False Then
            Exit Sub
        End If
        AsyncLoader(True)
        Dim Reporte As New rptReportAdmissionStadistical
        Reporte.ParametrosReporte = {INDDeFechaInicial.EditValue, INDDeFechaFinal.EditValue, INDIcbeAgrupado.EditValue, INDIcbeTipoReporte.EditValue, StatesId, ThirdPartyId, EntityId, CareGroupId, UserId, CareCenterId, FunctionalunitId}
        INDDvDocumentViewer.DocumentSource = Reporte
        Await Reporte.CargarDataSource()
        Reporte.CreateDocument(True)
        AsyncLoader(False)
        If Reporte.DataSource IsNot Nothing AndAlso Reporte.DataSource.Rows.Count > 0 Then
            Me.INDPcBase.Visible = False 'Ocultamos el formulario donde estan los filtros como tal
            Me.INDCncNavigation.Visible = False ' Ocultamos en control navigation panel
            Me.INDPcDocumentViewer.Visible = True
            INDDvDocumentViewer.Show() 'Visualizamos el reporte
        Else
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Funcion del boton atras del docuement viewer 
    ''' </summary>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDPcDocumentViewer.Visible = False
        Me.INDPcBase.Visible = True
        Me.INDCncNavigation.Visible = True
    End Sub
    ''' <summary>
    ''' Evento del boton deshacer de la barra botones.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub
    ''' <summary>
    ''' Evento que carga la barra botones.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub
    ''' <summary>
    ''' Evento para exportar datos a excel.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbExportarExcel_Click(sender As Object, e As EventArgs) Handles INDSbExportarExcel.Click
        AsyncLoader(True)
        Dim Parametros = {INDDeFechaInicial.EditValue, INDDeFechaFinal.EditValue, INDIcbeAgrupado.EditValue, INDIcbeTipoReporte.EditValue, StatesId, ThirdPartyId, EntityId, CareGroupId, UserId, CareCenterId, FunctionalunitId}
        Dim ParametrosString As List(Of String) = New List(Of String)
        Dim ParametrosDate As List(Of DateTime) = New List(Of DateTime)

        'Criterios
        ParametrosString.Add(Parametros(2)) 'Parametro de agruapacion. 0
        ParametrosString.Add(Parametros(3)) 'Parametro de tipo reporte. 1 
        ParametrosString.Add(Parametros(4)) 'Parametro de Estado del ingreso. 2

        'Fechas
        ParametrosDate.Add(Parametros(0)) 'Fecha inicial
        ParametrosDate.Add(Parametros(1)) ' Fecha final

        'Filtros Sle
        ParametrosString.Add(Parametros(5)) 'Terceros 3
        ParametrosString.Add(Parametros(6)) 'Entidad 4 
        ParametrosString.Add(Parametros(7)) 'Grupo de atención 5
        ParametrosString.Add(Parametros(8)) 'Usuario 6 
        ParametrosString.Add(Parametros(9)) 'Centro de atención 7
        ParametrosString.Add(Parametros(10)) 'Unidad funcional 8

        Dim INDdtDataSource As DataTable = Await Presenter.LoadDataSourceStatistics(ParametrosString, ParametrosDate, Me.IndigoSessionValues)
        If INDdtDataSource IsNot Nothing AndAlso INDdtDataSource.Rows.Count > 0 Then

            Dim exportExcel As DevExpress.XtraGrid.GridControl
            If INDIcbeTipoReporte.EditValue = "Detallado" Then
                INDGcEstadisticoIngresos.DataSource = INDdtDataSource
                INDGvEstadisticoIngresos.OptionsView.ColumnAutoWidth = False
                INDGvEstadisticoIngresos.HorzScrollVisibility = ScrollVisibility.Always
                INDGvEstadisticoIngresos.BestFitColumns()
                exportExcel = INDGcEstadisticoIngresos
            Else
                INDGcEstadisticoIngresosResumido.DataSource = INDdtDataSource
                INDGvEstadisticoIngresosResumido.OptionsView.ColumnAutoWidth = False
                INDGvEstadisticoIngresosResumido.HorzScrollVisibility = ScrollVisibility.Always
                INDGvEstadisticoIngresosResumido.BestFitColumns()
                exportExcel = INDGcEstadisticoIngresosResumido
            End If

            If FolderBrowserDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                If FolderBrowserDialog1.SelectedPath <> String.Empty Then
                    exportExcel.ExportToXlsx(FolderBrowserDialog1.SelectedPath + "\ReporteEstadisticoIngresos.xlsx")
                    Mensaje(EeventViewerImages.Informacion) = "Archivo exportado correctamente."
                End If
                If System.IO.File.Exists(FolderBrowserDialog1.SelectedPath + "\ReporteEstadisticoIngresos.xlsx") Then
                    System.Diagnostics.Process.Start(FolderBrowserDialog1.SelectedPath + "\ReporteEstadisticoIngresos.xlsx")
                End If
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = "No hay datos para exportar a excel."
        End If
        AsyncLoader(False)
    End Sub
#Region "Eventos Query Pop-Up"
    ''' <summary>
    ''' Evento query pop up del control de estado.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleEstados_QueryPopUp_1(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEstados.QueryPopUp
        If INDSleEstados.Properties.DataSource Is Nothing Then
            Dim INDdtEstado As DataTable = New DataTable
            INDdtEstado.TableName = "DataSourceEstado"
            INDdtEstado.Columns.Add("Id", GetType(String))
            INDdtEstado.Columns.Add("Name", GetType(String))
            For i As Integer = 0 To 4
                Dim INDdrEstado = INDdtEstado.NewRow()
                Select Case i
                    Case 0
                        INDdrEstado.Item("Id") = " ,T" 'Se envia la "T" para que tome el espacio en blanco del ingreso abierto.
                        INDdrEstado.Item("Name") = "Abierto"
                    Case 1
                        INDdrEstado.Item("Id") = "P"
                        INDdrEstado.Item("Name") = "Parcial"
                    Case 2
                        INDdrEstado.Item("Id") = "F"
                        INDdrEstado.Item("Name") = "Facturado"
                    Case 3
                        INDdrEstado.Item("Id") = "C"
                        INDdrEstado.Item("Name") = "Cerrado"
                    Case 4
                        INDdrEstado.Item("Id") = "A"
                        INDdrEstado.Item("Name") = "Anulado"
                End Select
                INDdtEstado.Rows.Add(INDdrEstado)
            Next
            INDSleEstados.Properties.DataSource = INDdtEstado
        End If
    End Sub
    ''' <summary>
    ''' Evento query pop up del control de terceros.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleTerceros_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleTerceros.QueryPopUp
        If INDSleTerceros.Properties.DataSource Is Nothing Then
            INDSleTerceros.Properties.DataSource = Presenter.LoadDatasourceThirdParty()
        End If
    End Sub
    ''' <summary>
    ''' Evento query pop up del control de entidades.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleEntidad_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEntidad.QueryPopUp
        If INDSleEntidad.Properties.DataSource Is Nothing Then
            INDSleEntidad.Properties.DataSource = Presenter.LoadDatasourceHealthAdministrator()
        End If
    End Sub
    ''' <summary>
    ''' Evento query pop up del control de grupos de atención.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleGruposAtencion_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGrupoAtencion.QueryPopUp
        If INDSleGrupoAtencion.Properties.DataSource Is Nothing Then
            INDSleGrupoAtencion.Properties.DataSource = Presenter.LoadDatasourceCareGroup()
        End If
    End Sub
    ''' <summary>
    ''' Evento query pop up del control de usuarios.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleUsuario_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleUsuario.QueryPopUp
        If INDSleUsuario.Properties.DataSource Is Nothing Then
            INDSleUsuario.Properties.DataSource = Presenter.LoadDatasourceUsers()
        End If
    End Sub
    ''' <summary>
    ''' Evento del query pop up del control de centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCentroAtencion_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCentroAtencion.QueryPopUp
        If INDSleCentroAtencion.Properties.DataSource Is Nothing Then
            INDSleCentroAtencion.Properties.DataSource = Presenter.LoadDatasourceCareCenter()
        End If
    End Sub
    ''' <summary>
    ''' Evento del query pop up del control de Unidad funcional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleUnidadFuncional_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleUnidadFuncional.QueryPopUp
        If INDSleUnidadFuncional.Properties.DataSource Is Nothing Then
            INDSleUnidadFuncional.Properties.DataSource = Presenter.LoadDatasourceFunctionalUnit()
        End If
    End Sub

    Private Sub INDSleTerceros_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleTerceros.CloseUp
        Me.ThirdPartyId = RecuperarSeleccionados(sender, "Nit", "Nit")
    End Sub
    Private Sub INDSleEntidad_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleEntidad.CloseUp
        Me.EntityId = RecuperarSeleccionados(sender, "Code", "Code")
    End Sub
    Private Sub INDSleGrupoAtencion_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleGrupoAtencion.CloseUp
        Me.CareGroupId = RecuperarSeleccionados(sender, "Code", "Code")
    End Sub
    Private Sub INDSleUsuario_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleUsuario.CloseUp
        Me.UserId = RecuperarSeleccionados(sender, "UserCode", "UserCode")
    End Sub
    Private Sub INDSleCentroAtencion_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleCentroAtencion.CloseUp
        Me.CareCenterId = RecuperarSeleccionados(sender, "CODCENATE", "CodeName")
    End Sub
    Private Sub INDSleUnidadFuncional_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleUnidadFuncional.CloseUp
        Me.FunctionalunitId = RecuperarSeleccionados(sender, "UFUCODIGO", "CodeName")
    End Sub
    Private Sub INDSleEstados_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleEstados.CloseUp
        Me.StatesId = RecuperarSeleccionados(sender, "Id", "Name")
    End Sub
#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Valida los controles del reporte
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsReport() As Boolean
        Dim errors As New StringBuilder
        If INDDeFechaInicial.EditValue Is Nothing Then
            errors.AppendLine("Debe ingresar una fecha inicial")
        End If
        If INDDeFechaFinal.EditValue Is Nothing Then
            errors.AppendLine("Debe ingresar una fecha final")
        End If
        If INDIcbeTipoReporte.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un tipo de reporte")
        End If
        If INDIcbeAgrupado.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una agrupación")
        End If
        If StatesId Is Nothing AndAlso StatesId = String.Empty Then
            errors.AppendLine("Debe seleccionar un estado")
        End If
        If INDDeFechaInicial.EditValue > INDDeFechaFinal.EditValue Then
            errors.AppendLine("La fecha final debe ser mayor a la fecha inicial")
        End If
        If errors.ToString().Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If
        Return True
    End Function
    ''' <summary>
    ''' Metodo que Deshace todas las operaciones realizadas y Limpia los controles utilizados
    ''' </summary>
    Public Sub Deshacer()
        INDDeFechaInicial.Text = String.Empty
        INDDeFechaFinal.Text = String.Empty
        INDDeFechaInicial.EditValue = Nothing
        INDDeFechaFinal.EditValue = Nothing
        INDSleEstados.EditValue = Nothing
        INDIcbeAgrupado.EditValue = Nothing
        INDIcbeTipoReporte.EditValue = Nothing
        INDSleCentroAtencion.EditValue = Nothing
        INDSleEntidad.EditValue = Nothing
        INDSleGrupoAtencion.EditValue = Nothing
        INDSleTerceros.EditValue = Nothing
        INDSleTerceros.EditValue = Nothing
        INDSleUnidadFuncional.EditValue = Nothing
        INDSleUsuario.EditValue = Nothing
        INDDeFechaInicial.Focus()
    End Sub

    ''' <summary>
    ''' Metodo para recuperar seleccionados.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="keyField"></param>
    ''' <param name="descripcionField"></param>
    ''' <returns></returns>
    Private Function RecuperarSeleccionados(sender As Object, keyField As String, descripcionField As String) As String
        Dim edit As DevExpress.XtraEditors.SearchLookUpEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim identificadores As String = String.Empty
        Dim identificador As String = String.Empty
        Dim descripciones As String = String.Empty
        Dim separador As String = String.Empty
        Dim selectedRows As Integer() = edit.Properties.View.GetSelectedRows()
        For Each selectionRow As Integer In selectedRows
            'identificadores += separador & edit.Properties.View.GetRow(selectionRow).Row(keyField).ToString()
            'descripciones += separador & edit.Properties.View.GetRow(selectionRow).Row(descripcionField).ToString()
            identificador = edit.Properties.View.GetRowCellValue(selectionRow, keyField).ToString()
            If Not String.IsNullOrEmpty(identificador) Then
                identificadores += separador & identificador
                descripciones += separador & edit.Properties.View.GetRowCellValue(selectionRow, descripcionField).ToString()
                separador = ","
            End If
        Next
        edit.Properties.NullText = descripciones.ToString()
        edit.ToolTip = descripciones.ToString()
        Return identificadores.ToString()
    End Function



#End Region

End Class