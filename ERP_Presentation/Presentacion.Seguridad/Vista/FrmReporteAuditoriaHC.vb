'***********************************************************************
' Assembly         : Presentacion.Seguridad
' Author           : Johan Sebastian Carranza Ramos
' Created          : 17-05-2019
'
' Last Modified By : Johan Sebastian Carranza Ramos
' Last Modified On : 17-05-2019
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Liberias Importadas"

Imports System.Data
Imports Presentation.Security.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Domain.Security.Entities
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Reporter
Imports DevExpress.Xpo


#End Region

Public Class FrmReporteAuditoriaHC
    Inherits Presentation.Controls.FormBase
    Implements IReporteAuditoriaHC


# Region "Variables"
    ''' <summary>
    ''' Variable que instancia al presentador
    ''' </summary>
    Dim presenter As PReporteAuditoriaHC

    #End Region

#Region "Propiedades"
    Public Property INDPaciente As String Implements IReporteAuditoriaHC.INDPaciente

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

    Public Property User As User Implements IReporteAuditoriaHC.User

    Public Sub Buscar() Implements ICrudBase.Buscar
        Throw New NotImplementedException()
    End Sub


    ''' <summary>
    ''' Metodo que Deshace todas las operaciones realizadas y Limpia los controles utilizados
    ''' </summary>
    Public Sub Deshacer() Implements IReporteAuditoriaHC.Deshacer
        presenter.Deshacer()
        INDtxtUserName.Text = String.Empty
        INDbteUserCode.Text = String.Empty
        INDdeFechaInicial.Text = String.Empty 
        INDdeFechaFinal.Text = String.Empty
        INDbePaciente.Text = String.Empty
        INDteNamePatient.Text = String.Empty
        INDbteUserCode.Enabled = True
        INDbePaciente.Enabled = True
        INDbteUserCode.Focus()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub Guardar() Implements IReporteAuditoriaHC.Guardar
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IReporteAuditoriaHC.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Property ProoftCloseXpoAccount As XPInstantFeedbackSource

    Public Property InitialDate As Date? Implements IReporteAuditoriaHC.InitialDate
        Get
            Return INDdeFechaInicial.EditValue
        End Get
        Set(value As Date?)
            INDdeFechaInicial.EditValue=value
        End Set
    End Property

    Public Property FinalDate As Date? Implements IReporteAuditoriaHC.FinalDate
        Get
            Return INDdeFechaFinal.EditValue
        End Get
        Set(value As Date?)
            INDdeFechaFinal.EditValue=value
        End Set
    End Property

    ''' <summary>
    ''' Metodo para abrir el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        Dim tipoCrud As Infrastructure.CrossCutting.Base.eDataSource = Infrastructure.CrossCutting.Base.eDataSource.UsersCRUD
        Dim listColumns As List(Of ColumnInfo) = {New ColumnInfo With {.Caption = "Código", .FieldName = "UserCode"}, New ColumnInfo With {.Caption = "Nombre", .FieldName = "PersonFullName"}, New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnAligment = DevExpress.Utils.HorzAlignment.Center}}.ToList()

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = listColumns
            .ValorSolicitado = "UserCode"
            .ListadoOrigenDatos = tipoCrud
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Private Sub IcrudBase_Deshacer() Implements IcrudBase.Deshacer
        Throw New NotImplementedException()
    End Sub

    Private Sub IcrudBase_Guardar() Implements IcrudBase.Guardar
        Throw New NotImplementedException()
    End Sub

    Private Sub IcrudBase_LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

    Private Sub IReporteAuditoriaHC_AsyncLoader(valor As Boolean) Implements IReporteAuditoriaHC.AsyncLoader
    End Sub

    #End Region
    
#Region "Eventos Barra Botones"

# End Region

#Region "Validacion Controles"
    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()

        Dim Validations As Boolean = True
        
        'Se consulta que los campos de fechas tengan algo
        If INDdeFechaInicial.EditValue IsNot Nothing Or INDdeFechaFinal.EditValue IsNot Nothing Then
                If Me.INDdeFechaInicial.EditValue < INDdeFechaFinal.EditValue Then
                    Validations = true
                Else
                    'Fechas no son logicas
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("La fecha final debe ser mayor a la fecha inicial")
                    Me.INDdeFechaFinal.Focus()
                    Validations = False
                End If
        Else
                'Se consulta si tiene codigo de usuario
                If INDbteUserCode.EditValue IsNot Nothing
                    'Se consulta si tiene paciente
                    If INDbePaciente.EditValue IsNot Nothing
                            Validations = true
                    Else 
                            Mensaje(EeventViewerImages.Advertencia) = String.Format("Ingrese Paciente")
                            Me.INDbePaciente.Focus()
                            Validations = False
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("Ingrese Usuario")
                    Me.INDbteUserCode.Focus()
                    Validations = False
                End If
        End If 
        Return Validations
    End Function

    #End Region

#Region "Eventos y funciones"
    Public Sub New()
        InitializeComponent()
    End Sub



    ''' <summary>
    ''' Evento clic en el boton buscar del caja de texto de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbteUserCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteUserCode.ButtonClick
        OpenSearch()
    End Sub



    ''' <summary>
    ''' Metodo para consultar el usuario
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Threading.Tasks.Task
        Using modelo = New MUsuario
            AsyncLoader(True)
            User = Await modelo.ConsultarUsuario(INDbteUserCode.Text)
            AsyncLoader(False)
        End Using
        If User Is Nothing OrElse User.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("Usuario no encontrado")
            Me.INDbteUserCode.Focus()
        Else
            BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
            INDtxtUserName.Text = User.Person.Fullname
            INDbteUserCode.Enabled = False
        End If
    End Function
    
    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbteUserCode.Text = ReturnValue
        If INDbteUserCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbteUserCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteUserCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Evento cuando presiona una tecla en la caja de texto del codigo de usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteUserCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteUserCode.KeyDown
        If e.KeyCode = Keys.Enter AndAlso INDbteUserCode.Text <> String.Empty Then
            Await LoadControls()
        End If
    End Sub

    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        ''' <summary>
        ''' Se ejecuta al darle clic al Botón INDSbGenerateReport para generar el reporte
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        If Me.ValidateControlsReports Then
            AsyncLoader(True) 'Para que suspenda el resto de la aplicacion mientras se ejecutan las siguientes lineas.
            Dim reporte As New rptReporteAuditoriaHC 'Instancia del reporte de auditoria 

            reporte.ParametrosReporte = {INDdeFechaInicial.EditValue, INDdeFechaFinal.EditValue,INDbteUserCode.EditValue,INDbePaciente.EditValue}
            
            INDDvDocumentViewer.DocumentSource = reporte
            reporte.CargarDataSource()
            reporte.CreateDocument(True) 
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcBase.Visible = False 'Ocultamos el formulario donde estan los filtros como tal
                Me.INDCncNavigation.visible = False ' Ocultamos en control navigaiton panel
                Me.INDPcDocumentViewer.Visible = True 
                INDDvDocumentViewer .Show() 'Visualizamos el reporte
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDbteUserCode.Focus()
            End If

        End If
    End Sub

    Private Sub INDbteUserCode_EditValueChanged(sender As Object, e As EventArgs) Handles INDbteUserCode.EditValueChanged

    End Sub

    Private Sub ButtonEdit1_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbePaciente.ButtonClick
        OpenSearch1() 
    End Sub

    Public Sub OpenSearch1() 
        
            FormSearchObjects = New FrmBusqueda
            AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue1
            With FormSearchObjects
                .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "IPCODPACI", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                                  New ColumnInfo() With {.Caption = "Nombre", .FieldName = "IPNOMCOMP", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7)}}.ToList
                .ValorSolicitado = "IPCODPACI"
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllPatients
                BarraBotones.PrepareToolbar(eAction.OnlyFind)
                .FormParent = Me
                .ShowSearch()
            End With
        
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue1(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbePaciente.Text = ReturnValue
        If INDbePaciente.Text <> String.Empty Then
            If INDbePaciente.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbePaciente.Enabled = False
            Dim INDpaciente As PatientXpo 
            Using modelo = New MReporteAuditoriaHC
                INDPaciente = modelo.GetPatientXpo(INDbePaciente.Text)
                INDteNamePatient.Text = INDpaciente.IPNOMCOMP
            End Using
        End If

    End Sub

    Private Sub FrmReporteAuditoriaHC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        presenter = New PReporteAuditoriaHC (Me)
        BarraBotones.ActualizarPermisosBarra(CStr(Me.Tag))

        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
            
            INDLcFechaInicial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcFechaInicial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcUser.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always 
            INDLcUserName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always 
            INDLcPaciente.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcExportExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    

    Private Sub INDbePaciente_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbePaciente.KeyDown
        Dim INDpaciente As PatientXpo 
        If e.KeyCode = Keys.Enter AndAlso INDbePaciente.Text <> String.Empty Then
            Using modelo = New MReporteAuditoriaHC
                INDPaciente = modelo.GetPatientXpo(INDbePaciente.Text)
                If INDpaciente IsNot Nothing  Then
                        INDbePaciente.Text = INDPaciente.IPCODPACI
                        INDbePaciente.Enabled = False 
                        INDteNamePatient.Text = INDpaciente.IPNOMCOMP
                Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format("Paciente no encontrado")
                        Me.INDbePaciente.Focus()
                End If


            End Using
        End If
       
    End Sub

    'Funcion del boton atras del docuement viewer 
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Deshacer()
        Me.INDDvDocumentViewer.Visible = False
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
    End Sub

    'Evento para exportar a Excel
    Private Sub INDsbGenerateExcel_Click(sender As Object, e As EventArgs) Handles INDsbGenerateExcel.Click
        Dim INDdtReportAuditoriaE As List(Of HCAUDITORIAXpo ) = New List(Of HCAUDITORIAXpo )
        Try
            AsyncLoader(True)
            Using modelo = New MReporteAuditoriaHC
                INDdtReportAuditoriaE = modelo.GetReporteAuditoria(INDdeFechaInicial.EditValue, INDdeFechaFinal.EditValue,INDbteUserCode.EditValue,INDbePaciente.EditValue)
            End Using
            
            If INDdtReportAuditoriaE IsNot Nothing Then
                If INDdtReportAuditoriaE.Count > 0 Then
                    INDGcExportExcell.DataSource = INDdtReportAuditoriaE
                    If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                        generateExcel()
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontraron datos para generar el excel"
                End If
                AsyncLoader(False)
            Else
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un segundo criterio de busqueda"
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    
    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
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

End Class