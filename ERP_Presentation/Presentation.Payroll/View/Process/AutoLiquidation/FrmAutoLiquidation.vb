Imports Presentation.Payroll.MVP
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Controls
Imports Domain.Payroll.Entities
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.IO
Imports System.Windows.Forms
Imports Domain.Base.Entities
Imports System.Text

Public Class FrmAutoLiquidation
    Implements IAutoliquidation

#Region "Variables"

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PAutoliquidation

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Variable para almacenar los campos customizables
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Variable para almacenar la compañia
    ''' </summary>
    ''' <remarks></remarks>
    Dim company As Company

#End Region

#Region "ICRUD"

    Public Sub AbrirBusqueda() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.CompanyPayroll
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}}.ToList
            BarraBotones.PrepareToolbar(eAction.New)
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
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDgleCompany.Text = ReturnValue
        If INDgleCompany.Text <> String.Empty Then
            LoadControls()
            If INDgleCompany.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDgleCompany.Enabled = False
        End If
    End Sub

    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        AbrirBusqueda()
    End Sub

    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        INDgleCompany.EditValue = Nothing
        INDglePeriodLiquidation.EditValue = Nothing
        INDgleWorkCenter.EditValue = Nothing
        INDcmbIsCorrection.EditValue = Nothing
        INDdeDatePayment.EditValue = Nothing
        INDtxtNumberTemplate.Text = Nothing
        ActionsOnControls = False
        INDdeDatePayment.Enabled = False
        INDtxtNumberTemplate.Enabled = False
        BarraBotones.PrepareToolbar(eAction.OnlySave)
    End Sub

    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateData() = False Then
            Return
        End If
        Using model As New MAutoliquidation(MyBase.Tag)
            AsyncLoader(True)
            Dim companyId As Integer = INDgleCompany.EditValue
            Dim periodLiquidate As String = INDglePeriodLiquidation.EditValue
            Dim workCenter As Integer = INDgleWorkCenter.EditValue
            Dim isCorecction As Boolean = INDcmbIsCorrection.EditValue
            Dim dateLiquidation As Nullable(Of Date) = INDdeDatePayment.EditValue
            Dim numberTemplate As String = INDtxtNumberTemplate.Text
            Dim contentAchive = Await model.GenerateAutoliquidationAsync(companyId, periodLiquidate, workCenter, isCorecction, dateLiquidation, numberTemplate)
            Dim count = 0
            AsyncLoader(False)
            For Each item In contentAchive
                Dim nameFile
                If count = 0 Then
                    nameFile = "Planilla_E"
                Else
                    nameFile = "Planilla_K"
                End If
                count += 1
                If item.StateResult = False AndAlso (item.MessageResult Is Nothing OrElse item.MessageResult.Count = 0) Then
                    Mensaje(EeventViewerImages.Advertencia) = item.Message
                    Exit Sub
                End If
                If item.MessageResult.Count > 0 Then
                    Dim listMessage As New List(Of Tuple(Of Byte, String))
                    For Each message As MessageResult In item.MessageResult
                        Select Case message.CodeMessage
                            Case "-001"
                                listMessage.Add(New Tuple(Of Byte, String)(1, String.Format(obtenerRecurso(EmpleadoNoTieneEPS, Eform.Autoliquidation), message.Parameters)))
                            Case "-002"
                                listMessage.Add(New Tuple(Of Byte, String)(2, String.Format(obtenerRecurso(EmpleadoNoTieneEPS, Eform.Autoliquidation), message.Parameters)))
                            Case "-003"
                                listMessage.Add(New Tuple(Of Byte, String)(2, String.Format(obtenerRecurso(EmpleadoMasFondoPension, Eform.Autoliquidation), message.Parameters)))
                            Case "-004"
                                listMessage.Add(New Tuple(Of Byte, String)(2, String.Format(obtenerRecurso(EmpleadoNoTieneFondoPension, Eform.Autoliquidation), message.Parameters)))
                            Case "-005"
                                listMessage.Add(New Tuple(Of Byte, String)(2, String.Format(obtenerRecurso(EmpleadoMasCajaCompensacion, Eform.Autoliquidation), message.Parameters)))
                            Case "-006"
                                listMessage.Add(New Tuple(Of Byte, String)(2, String.Format(obtenerRecurso(EmpleadoNoTieneCajaCompensacion, Eform.Autoliquidation), message.Parameters)))
                            Case "-007"
                                listMessage.Add(New Tuple(Of Byte, String)(2, "El Empleado " + message.Parameters(0) + " no tiene ARL Activa"))
                            Case "-008"
                                listMessage.Add(New Tuple(Of Byte, String)(2, "El Empleado " + message.Parameters(0) + " tiene dos o más ARL Activas"))
                            Case "-009"
                                listMessage.Add(New Tuple(Of Byte, String)(2, message.Parameters(0)))
                        End Select
                    Next
                    Dim frmMessage As FrmAutoLiquidationMessage = New FrmAutoLiquidationMessage()
                    frmMessage.StartPosition = FormStartPosition.CenterScreen
                    frmMessage.INDGcMessageAutoliquidation.DataSource = listMessage

                    Dim frmTransparent As New FrmTransparent(frmMessage, False)
                    frmTransparent.ShowDialog()
                    If frmMessage.Generate = True Then

                        GenerateFile(item.ObjectEmbbeded, nameFile)
                    End If
                Else
                    GenerateFile(item.ObjectEmbbeded, nameFile)
                End If
            Next
        End Using
    End Sub

    Public Sub GenerateFile(content As StringBuilder, name As String)
        Dim save As SaveFileDialog = New SaveFileDialog()
        save.Filter = "Texto|*.txt"
        save.Title = "Guardar Archivo Autoliquidacion"
        save.FileName = name + ".txt"
        save.ShowDialog()
        Dim file = save.OpenFile()
        Dim streamWrite As New StreamWriter(file)
        streamWrite.Write(content)
        streamWrite.Flush()
        streamWrite.Close()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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

    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    Public WriteOnly Property ActionsOnControls As Boolean Implements IAutoliquidation.ActionsOnControls
        Set(value As Boolean)
            INDgleCompany.Enabled = Not value
            INDglePeriodLiquidation.Enabled = value
            INDgleWorkCenter.Enabled = value
            INDcmbIsCorrection.Enabled = value
        End Set
    End Property

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Valida los datos necesarios antes de guardar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateData() As Boolean
        If INDgleCompany.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemCodeCompany.Text)
            Return False
        End If

        If INDglePeriodLiquidation.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemPeriodLiquidation.Text)
            Return False
        End If
        If INDgleWorkCenter.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemWorkCenter.Text)
            Return False
        End If
        If INDcmbIsCorrection.EditValue = True Then
            If INDdeDatePayment.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemDatePayment.Text)
                Return False
            End If
            If INDtxtNumberTemplate.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemNumberTemplate.Text)
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Sub LoadControls()
        AsyncLoader(True)
        Using model As New MAutoliquidation(MyBase.Tag)
            company = CType(INDgleCompany.GetSelectedDataRow, Company)
            If Not company Is Nothing Then
                If company.Id > 0 Then

                    INDglePeriodLiquidation.Properties.DataSource = Await model.GetDateLiquidationCompany(company.Id)
                    Dim listWorkCenter As List(Of WorkCenter) = Await model.ListAllWorkCenter()
                    listWorkCenter.Insert(0, New WorkCenter() With {.Code = 0, .Name = obtenerRecurso(ComunesTodos)})
                    INDgleWorkCenter.Properties.DataSource = listWorkCenter
                    ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Else
                    Mensaje(EeventViewerImages.Pregunta) = obtenerRecurso(ComunesNoSeEncontroDatoERP)
                End If
            Else
                Mensaje(EeventViewerImages.Pregunta) = obtenerRecurso(ComunesNoSeEncontroDatoERP)
            End If
        End Using
        AsyncLoader(False)
    End Sub

#End Region

#Region "Eventos"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        PathFunctionalDefinitions = Nothing
        Presenter = Nothing
        ExistDefinitionFront = Nothing
        dtFieldsCustomizables = Nothing
        company = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se ejecuta al cargar la barra botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Me.Tag)
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmAutoLiquidation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyFind)
        '****Inicializar variables*****'
        Me._doc = Nothing
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        '******************************'
        'Me._funct = AddressOf GenerateDoc
        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollBank.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter = New PAutoliquidation(Me)
        Deshacer()
        Using model As New MAutoliquidation(MyBase.Tag)
            AsyncLoader(True)
            Dim company As Company = Await model.ListCompanyBySessionAsync()

            If company IsNot Nothing Then
                INDgleCompany.Properties.DataSource = New List(Of Company) From {company}
            End If

            AsyncLoader(False)

        End Using
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al dar click sobre el boton de deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al dibujar una celda del grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridLookUpEdit1View_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GridLookUpEdit1View.CustomDrawCell
        If e.Column.Name = INDcolDate.Name Then
            e.DisplayText = CType(e.Cell, GridCellInfo).RowInfo.RowKey
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor del combo de es correccion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDcmbIsCorrection_EditValueChanged(sender As Object, e As EventArgs) Handles INDcmbIsCorrection.EditValueChanged
        If INDcmbIsCorrection.EditValue = True Then
            INDdeDatePayment.Enabled = True
            INDtxtNumberTemplate.Enabled = True
        Else
            INDdeDatePayment.Enabled = False
            INDtxtNumberTemplate.Enabled = False
            INDdeDatePayment.EditValue = Nothing
            INDtxtNumberTemplate.Text = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Este evento se ejecuta cuando cambia de valor la compañia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleCompany_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleCompany.EditValueChanged
        If INDgleCompany.EditValue IsNot Nothing Then
            LoadControls()
        Else
            Deshacer()
        End If
    End Sub

    ''' <summary>
    ''' Este evento se ejecuta al dar click sobre guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

#End Region


#Region "Customize"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyAutoliquidation.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LoadhronousDefinitions.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(PathFunctionalDefinitions) = True Then
            ExistDefinitionFront = True
        End If
    End Sub

    ''' <summary>
    ''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadhronousDefinitions.RunWorkerCompleted
        If ExistDefinitionFront = True Then
            INDlyAutoliquidation.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyAutoliquidation.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MAutoliquidation(MyBase.Tag)
                Dim dsFields As DataSet = model.GetFieldsNULL()
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyAutoliquidation.Items.Count - 1
                        INDlyAutoliquidation.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyAutoliquidation.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyAutoliquidation.Items.Item(j).Tag.ToString.Trim Then
                                    INDlyAutoliquidation.Items.Item(j).AllowHide = True
                                End If
                            End If
                        Next
                    Next
                End If
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyAutoliquidation.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyAutoliquidation.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyAutoliquidation.SaveLayoutToXml(PathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDlyAutoliquidation.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

    Private Async Sub INDBtnExportExcel_Click(sender As Object, e As EventArgs) Handles INDBtnExportExcel.Click

        Try

            If INDgleCompany.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione una Empresa"
                INDgleCompany.Focus()
                Exit Sub
            End If

            If INDglePeriodLiquidation.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Periodo de Liquidación"
                INDglePeriodLiquidation.Focus()
                Exit Sub
            End If

            AsyncLoader(True)
            Using model As New MAutoliquidation(MyBase.Tag)
                Dim ds As DataSet = Await (model.GenerateReportAutoliquidation(INDglePeriodLiquidation.EditValue, INDgleWorkCenter.EditValue))

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dtReportAuxiliar As DataTable = ds.Tables("Autoliquidation")
                    INDGcExportExcell.DataSource = dtReportAuxiliar
                    If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                        generateExcel()
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontraron datos para generar el excel"
                End If
            End Using
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try


        'Using model As New MAutoliquidation(MyBase.Tag)
        '    AsyncLoader(True)
        '    Me.INDGcExportExcell.DataSource = Await (model.GenerateReportAutoliquidation(INDglePeriodLiquidation.EditValue, INDgleWorkCenter.EditValue))
        '    AsyncLoader(False)
        'End Using
        'If Me.INDGcExportExcell.DataSource IsNot Nothing Then
        '    generateExcel()
        'End If
    End Sub

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