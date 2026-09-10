'***********************************************************************
' Assembly         : Presentation.Payroll
' Author           : Mariana Gonzalez Calderon
' Created          : 2025-12-03
'
' Description      : Formulario de reporte de talento humano
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Reporter
Imports Presentation.Payroll.MVP
Imports Presentation.CloudAgent

#End Region


Public Class FrmReportHumanTalent
    Implements IReportHumanTalent


#Region "Variables"
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

#End Region


#Region "Properties"

    Public Property EmployeeId As Integer
        Get
            If INDSleemployee.EditValue Is Nothing OrElse String.IsNullOrEmpty(INDSleemployee.EditValue.ToString()) Then
                Return 0
            End If
            Dim result As Integer = 0
            Integer.TryParse(INDSleemployee.EditValue.ToString(), result)
            Return result
        End Get
        Set(value As Integer)
            If value = 0 Then
                INDSleemployee.EditValue = Nothing
            Else
                INDSleemployee.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtencion de nit del empleado
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property EmployeeNit As String
        Get
            If INDSleemployee.EditValue Is Nothing OrElse String.IsNullOrEmpty(INDSleemployee.EditValue.ToString()) Then
                Return String.Empty
            End If
            Dim view = INDSleemployee.Properties.View
            If view IsNot Nothing Then
                Dim rowHandle = view.FocusedRowHandle
                Dim nitValue = view.GetRowCellValue(rowHandle, "Nit")
                If nitValue IsNot Nothing AndAlso Not IsDBNull(nitValue) Then
                    Return nitValue.ToString()
                End If
            End If
            Return String.Empty
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

    ''' <summary>
    ''' Establece fuente al dataSource
    ''' </summary>
    Public WriteOnly Property HumanTalentDataSource As Object Implements IReportHumanTalent.HumanTalentDataSource
        Set(value As Object)
            INDgcHumanTalent.DataSource = value

            ' Deshabilitar auto-ancho para permitir scroll horizontal cuando las columnas sean más anchas
            INDgvHumanTalent.OptionsView.ColumnAutoWidth = False

            ' Configurar todas las columnas para permitir ajuste completo
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvHumanTalent.Columns
                If col.Visible Then
                    col.OptionsColumn.FixedWidth = False
                End If
            Next

            ' Ajustar todas las columnas al contenido (True = forzar ajuste completo)
            INDgvHumanTalent.BestFitColumns(True)

            ' Limpiar summary items existentes antes de agregar nuevos
            INDgvHumanTalent.Columns("DocumentType").Summary.Clear()
            INDgvHumanTalent.Columns("BasicSalary").Summary.Clear()

            ' Agregar total de registros en la columna Tipo Documento
            INDgvHumanTalent.Columns("DocumentType").Summary.Add(DevExpress.Data.SummaryItemType.Count, "DocumentType", "Total Registros: {0}")

            ' Agregar total de salarios en la columna Salario Basico
            INDgvHumanTalent.Columns("BasicSalary").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "BasicSalary", "Total: {0:c2}")
        End Set
    End Property

#End Region

#Region "Load"
    Private Sub FrmReportHumanTalent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BarraBotones.PrepareToolbar(eAction.OnlyNavigationControl)
        ShowPannelBase()
        Me.BarraBotones.Minimizar(True)
    End Sub
#End Region


#Region "Handlers"
    '' <summary>
    ''' metodo para Cargar el data source Del Control INDSleEmployee
    ''' <remarks></remarks>
    Private Sub LoadXpoEmployee()
        Using msearch As New MBusqueda
            Dim resEmployee = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllEmployee)
            INDSleemployee.Properties.DataSource = resEmployee
        End Using
    End Sub

    ''' <summary>
    ''' Se ejecuta en el evento QueryPopUp del Control INDSleEmployees
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Se ejecuta cuando se despliega el control empleado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleemployee_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleemployee.QueryPopUp
        If EmployeeId = 0 Then
            LoadXpoEmployee()
        End If
    End Sub


#End Region


#Region "Methods"

    ''' <summary>
    ''' Evento generacion reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        Await loadReport()
    End Sub


    ''' <summary>
    ''' Limpiar controles
    ''' </summary>
    Public Sub Deshacer()
        EmployeeId = 0
        INDDateStart.EditValue = Nothing
        INDDateEnd.EditValue = Nothing
        ShowPannelBase()
        Me.BarraBotones.Minimizar(True)
    End Sub


    ''' <summary>
    ''' Carga reporte de talento humano
    ''' </summary>
    Private Async Function loadReport() As Task
        Try
            If INDDateStart.EditValue Is Nothing OrElse INDDateEnd.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar la fecha inicial y fecha final"
                Return
            End If

            Dim initialDate As Date = CDate(INDDateStart.EditValue)
            Dim finalDate As Date = CDate(INDDateEnd.EditValue)

            If initialDate > finalDate Then
                Mensaje(EeventViewerImages.Advertencia) = "La fecha inicial no puede ser mayor que la fecha final"
                Return
            End If

            AsyncLoader(True)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
            Me.BarraBotones.Minimizar(False)

            Dim employeeIdParam As Integer? = If(EmployeeId > 0, CType(EmployeeId, Integer?), Nothing)

            Using Model As New MEmployee(MEmployee.TAG)
                Dim result = Await Model.GetReportHumanTalentAsync(initialDate, finalDate, employeeIdParam)

                If result IsNot Nothing AndAlso result.StateResult Then
                    If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                        HumanTalentDataSource = result.ObjectEmbbeded
                        ShowPannelReport()
                    Else
                        Mensaje(EeventViewerImages.Informacion) = "No se encontraron datos para el rango de fechas seleccionado"
                        ShowPannelBase()
                    End If
                Else
                    Dim errorMessage = If(result IsNot Nothing AndAlso Not String.IsNullOrEmpty(result.Message),
                                    result.Message,
                                    "Error al obtener el reporte")
                    Mensaje(EeventViewerImages.MensajeError) = errorMessage
                    ShowPannelBase()
                End If
            End Using

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = "Error al cargar el reporte: " & ex.Message
            ShowPannelBase()
        Finally
            AsyncLoader(False)
        End Try
    End Function


    ''' <summary>
    ''' Muestra el panel inicial
    ''' </summary>
    Private Sub ShowPannelBase()
        INDPanelControlBase.Visible = False
        INDPanelControlReport.Visible = False
        INDPanelControlReport.Dock = DockStyle.None
        INDPanelControlBase.Dock = DockStyle.Fill
        INDPanelControlBase.BringToFront()
        INDPanelControlBase.Visible = True
    End Sub


    ''' <summary>
    ''' Muestra el panel del reporte
    ''' </summary>
    Private Sub ShowPannelReport()
        INDPanelControlBase.Visible = False
        INDPanelControlReport.Visible = False
        INDPanelControlReport.Dock = DockStyle.Fill
        INDPanelControlReport.BringToFront()
        INDPanelControlReport.Visible = True
    End Sub

#End Region

End Class

