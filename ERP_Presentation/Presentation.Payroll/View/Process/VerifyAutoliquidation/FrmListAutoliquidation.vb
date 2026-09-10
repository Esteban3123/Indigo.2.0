'***********************************************************************
' Assembly         : Presentacion.Payrol
' Author           : Daniel Eduardo Arévalo
' Created          : 17-07-2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Payroll.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.Spreadsheet

#End Region
Public Class FrmListAutoliquidation

    Implements IVerificateAutoliquidation

    Private bwMassivePayrollDatas As BackgroundWorker = New BackgroundWorker


    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MVerificationAutoliquidation
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PListAutoliquidation
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues

    Dim ObjVerofyAutoliquidation As VerifyAutoliquidationFile

    Dim ConfirmFlag As Boolean = False

    ''' <summary>
    ''' Resultado de la consulta al sp
    ''' </summary>
    Private result As ActionResult(Of List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' Listado de errores que devuelve el sp
    ''' </summary>
    Private ListErrors As List(Of Tuple(Of String, Integer))

    ''' <summary>
    ''' coleccion de filas que se van a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rows As RowCollection

    ''' <summary>
    ''' Representa al listado de los registros de copiar y pegar
    ''' </summary>
    Private RowsPasteToGrid As List(Of List(Of String))

    ''' <summary>
    ''' Listado del CopyPaste
    ''' </summary>
    Dim ListCopyPaste As List(Of List(Of String))

    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New List(Of ImportFileRow)()

    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim myStream As String = Nothing

    ''' <summary>
    ''' Permite saber si viene desde copiar y pegar o de importar archivo
    ''' </summary>
    Private IsCopyPaste As Boolean

    Private ListVerifyAutoliquidationFile As List(Of VerifyAutoliquidationFile)

    Dim WorkCenter As WorkCenter

    Dim listWorkCenter As List(Of WorkCenter)


    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        AddHandler bwMassivePayrollDatas.DoWork, AddressOf bwListPayments_DoWork
        AddHandler bwMassivePayrollDatas.RunWorkerCompleted, AddressOf bwInitialBalance_RunWorkerCompleted
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Model = Nothing
        Presenter = Nothing
        record = Nothing
        ObjEmployee = Nothing
        Idgroup = Nothing
        IsCopyPaste = Nothing
    End Sub
    Private Async Sub FrmListAutoliquidation_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(GridView1, ListActions)
        IndigoGridControl1.RefreshGrid(INDGcListEmployee)

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Model = New MVerificationAutoliquidation(Me.Tag)
        Me.indigo = SessionValues.Instance
        '******************************'

        Me.Funct = AddressOf GenerateDoc
        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloGlosas.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter = New PListAutoliquidation(Me)
        Deshacer()
        Me.LoadStatus()
        Me.BarraBotones.StatusRecordVisible = True
        'Me.ActionsOnControls = False
        CurrencyAbbreviation = Presenter.LoadPayrollSettings.CurrencyId.Abbreviation
        SetCurrencyFormat(CurrencyAbbreviation)
        BarraBotones.RibbonPageProcesos.Visible = False

        Using model As New MAutoliquidation(MyBase.Tag)
            AsyncLoader(True)
            Dim company As Company = Await model.ListCompanyBySessionAsync()
            If company IsNot Nothing Then
                INDSlCompany.Properties.DataSource = New List(Of Company) From {company}
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se encontró la empresa de la sesión"
            End If
            AsyncLoader(False)
        End Using

    End Sub

#Region "ICRUD"

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        If Not ModoBusqueda Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        End If
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        Me.BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

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

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Public WriteOnly Property ActionsOnControls As Boolean Implements IVerificateAutoliquidation.ActionsOnControls
        Set(value As Boolean)
            INDLcAutoliquidation.BeginUpdate()
            INDSlCompany.Enabled = Not value
            INDSlPeriodDate.Enabled = value
            INDSlWorkCenter.Enabled = value
            INDBtnProccess.Enabled = value
            INDLcAutoliquidation.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Indica la moneda oficial
    ''' </summary>
    Private CurrencyAbbreviation As String

    ''' <summary>
    ''' Cambia el estado del formulario para indicar que se esta llevando a cabo una operacion asincrona
    ''' </summary>
    ''' <param name="State">Valor que indica si se lleva a cabo la operacion</param>
    Public Overrides Sub AsyncLoader(State As Boolean) Implements IVerificateAutoliquidation.AsyncLoader
        MyBase.AsyncLoader(State)
    End Sub
#End Region

#Region "Eventos Barra Botones"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.RibbonPageProcesos.Visible = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub
    ''' <summary>
    ''' Confirmar un convenio
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        If MessageIndigo.Show("Desea Confirmar el Proceso para Generar el Archivo?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            ConfirmFlag = True
            Await ConfirmLiquidation()
        End If
    End Sub

    Private Async Sub BarraBotones_ClickDesconfirmar() Handles BarraBotones.Click_Desconfirmar
        If MessageIndigo.Show("Desea Desconfirmar el Proceso?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            ConfirmFlag = False
            Await ConfirmLiquidation()
        End If
    End Sub
    ''' <summary>
    ''' Anualr un convenio
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular

    End Sub
    ''' <summary>
    ''' Suspender un contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickSuspender() Handles BarraBotones.ClickSuspender

    End Sub
    ''' <summary>
    ''' Click boton reactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickReactivar() Handles BarraBotones.ClickReactivar

    End Sub

#End Region

#Region "Metodos Funciones Propiedades"
    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Await Model.DeleteBlockRecord(record)
            record = Nothing
        End If
    End Sub

    Private Function GenerateDoc() As IndexedDocument2

    End Function

    ''' <summary>
    ''' Limpiar controles
    ''' </summary>
    ''' <remarks></remarks>65
    Private Sub CleanControls()
        INDLcAutoliquidation.BeginUpdate()

        INDSlCompany.EditValue = Nothing
        INDSlWorkCenter.EditValue = Nothing
        INDSlPeriodDate.EditValue = Nothing
        INDGcListEmployee.DataSource = Nothing
        INDLcExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciImportButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        BarraBotones.RibbonPageProcesos.Visible = False
        INDLcAutoliquidation.EndUpdate()
        Me.ActionsOnControls = False
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        'ValidateControls = True
        If INDSlCompany.EditValue = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe Seleccionar una Empresa"
            INDSlCompany.Focus()
            Return False
        End If
        If INDSlPeriodDate.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una fecha"
            INDSlPeriodDate.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub FrmListAutoliquidation_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = "Sin confirmar", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(122, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = "Confirmado", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        Me.BarraBotones.States = listStates
        Me.BarraBotones.StatusRecordEnabled = False
        ' Me.BarraBotones.Enabled = False
    End Sub

    Private Function ValidateData() As Boolean

        If INDSlCompany.EditValue Is Nothing Then
            INDSlCompany.Focus()
            Return False
        End If

        If INDSlPeriodDate.EditValue Is Nothing Then
            INDSlPeriodDate.Focus()
            Return False
        End If

        If INDSlWorkCenter.EditValue Is Nothing Then
            INDSlWorkCenter.Focus()
            Return False
        End If

        Return True

    End Function
    ''' <summary>
    ''' Establece a  los controles la moneda parametrizada
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        changeNumericFormatByCurrency(numberFormat)
    End Sub

#End Region

#Region "Customizar"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDLcAutoliquidation.ShowCustomizationForm()
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
            INDLcAutoliquidation.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Async Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLcAutoliquidation.ShowCustomization
        Try
            'Ejecuatamos la consulta
            AsyncLoader(True)
            Dim dsFields As DataSet = Await Model.GetFieldsNULL
            AsyncLoader(False)
            If dsFields IsNot Nothing Then
                dtFieldsCustomizables = dsFields.Tables(0)
                For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                    For j As Integer = 0 To INDLcAutoliquidation.Items.Count - 1
                        If Object.Equals(INDLcAutoliquidation.Items.Item(j).Tag, Nothing) = False Then
                            If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDLcAutoliquidation.Items.Item(j).Tag.ToString.Trim Then
                                INDLcAutoliquidation.Items.Item(j).AllowHide = True
                            End If
                        End If
                    Next
                Next
            End If
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
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLcAutoliquidation.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDLcAutoliquidation.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDLcAutoliquidation.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDLcAutoliquidation.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDSlCompany_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlCompany.EditValueChanged
        If INDSlCompany.EditValue IsNot Nothing Then
            LoadControls()
        Else
            Deshacer()
        End If
    End Sub
#End Region

#Region "Functions"
    Private Async Sub LoadControls()
        AsyncLoader(True)
        Using model As New MAutoliquidation(MyBase.Tag)
            Dim Company = CType(INDSlCompany.GetSelectedDataRow, Company)
            If Not Company Is Nothing Then
                If Company.Id > 0 Then
                    INDSlPeriodDate.Properties.DataSource = Await model.GetDateLiquidationCompany(Company.Id)
                    listWorkCenter = Await model.ListAllWorkCenter()
                    INDSlWorkCenter.Properties.DataSource = listWorkCenter
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

    Private Async Sub EditDetail()
        ObjVerifyAutoliquidation = DirectCast(GridView1.GetFocusedRow(), Infrastructure.Data.Xpo.PayrollRepository.PayrollViewVerifyAutoliquidationFileXpo)
        Using model As New MVerificationAutoliquidation(MyBase.Tag)
            Dim tmpVerifyAutoliquidationFile = Await model.GetVerifyAutoliquidationByID(ObjVerifyAutoliquidation.id)
            OpenFormEditAutoliquidation(tmpVerifyAutoliquidationFile)
        End Using

    End Sub

    ''' <summary>
    ''' Metodo que abre el form de saldo inicial articulo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFormEditAutoliquidation(ObjVerifyAutoliquidation As VerifyAutoliquidationFile)

        Try

            If ObjVerifyAutoliquidation.RegisterStatus = True Then
                Mensaje(EeventViewerImages.Advertencia) = "El registro se encuentra confirmado y no puede ser editado. Debe desconfirmar primero."
                Return
            End If

            Using formulario As New FrmEditAutoliquidation
                formulario.VerifyAutoliquidation = ObjVerifyAutoliquidation
                formulario.CurrencyAbbreviation = CurrencyAbbreviation
                formulario.Size = New Drawing.Size(1090, 750)
                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent = New Base.FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using

            Using model As New MVerificationAutoliquidation(MyBase.Tag)
                AsyncLoader(True)
                Dim companyId As Integer = INDSlCompany.EditValue
                Dim periodLiquidate As String = INDSlPeriodDate.EditValue

                Dim WorkCenter = DirectCast(GridView2.GetFocusedRow(), WorkCenter)

                INDGcListEmployee.DataSource = Nothing
                INDGcListEmployee.DataSource = model.ListVerifiyAutoliquidationByWorkCenterAndPayrollDate(WorkCenter.Code, periodLiquidate)
                AsyncLoader(False)
            End Using

        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
            Return
        End Try

    End Sub

#End Region

#Region "Click"
    Private Async Sub INDBtnProccess_Click(sender As Object, e As EventArgs) Handles INDBtnProccess.Click
        If ValidateData() = False Then
            Mensaje(EeventViewerImages.Advertencia) = "Faltan llenar algunos campos"
            Return
        End If
        Try
            Using model As New MVerificationAutoliquidation(MyBase.Tag)
                AsyncLoader(True)
                Dim companyId As Integer = INDSlCompany.EditValue
                Dim periodLiquidate As String = INDSlPeriodDate.EditValue
                Dim WorkCenter = DirectCast(GridView2.GetFocusedRow(), WorkCenter)

                Dim TmpListVerify = model.ListVerifiyAutoliquidationByWorkCenterAndPayrollDate(WorkCenter.Code, periodLiquidate)
                INDGcListEmployee.DataSource = TmpListVerify

                If INDGcListEmployee.DataSource IsNot Nothing AndAlso INDGcListEmployee.DataSource.count > 0 Then

                    INDLcExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciImportButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    If TmpListVerify.Any(Function(x) x.RegisterStatus = False) Then
                        If MessageIndigo.Show("Ya existe generado un archivo con esta fecha sin confirmar. Desea volverlo a generar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                            Await GenerateValidationAutoliquidation()
                        Else
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyLiquidate)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                            INDGcExportExcell.DataSource = TmpListVerify
                            'ListVerifyAutoliquidationFile = TmpListVerify
                            BarraBotones.StatusRecord = TmpListVerify.FirstOrDefault().RegisterStatus
                            AsyncLoader(False)
                        End If
                    Else
                        Await GenerateValidationAutoliquidation()
                    End If
                Else
                    Await GenerateValidationAutoliquidation()
                End If
                AsyncLoader(False)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
        End Try

    End Sub

    Private Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        ImportFile()
    End Sub

    Private Sub ImportFile()
        'Configuramos el cuadro de dialogo para importar el archivo
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = False
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)

        'Si el ususario cancela la operación
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then
            AsyncLoader(False)
            Exit Sub
        End If

        Try
            'Obtengo la ruta del archivo
            myStream = openFileDialog1.FileName
            If (myStream Is Nothing OrElse myStream.Trim().Equals(String.Empty)) Then 'Si la ruta es vacía
                Mensaje(EeventViewerImages.Advertencia) = "Ruta de archivo vacía"
                AsyncLoader(False)
                Exit Sub
            End If

            'Se obtiene el documento excel
            Dim sddf = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
            sddf.AllowDrop = False
            sddf.LoadDocument(myStream)
            Dim workBook As IWorkbook = sddf.Document

            rows = workBook.Worksheets(0).Rows
            If rows.LastUsedIndex = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                AsyncLoader(False)
                Exit Sub
            End If

        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo"
            Exit Sub
        End Try

        'Se llama el asyncrono para validar el excel siempre y cuando no haya un asyncrono en ejecución
        If bwMassivePayrollDatas.IsBusy Then
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = "No puede continuar porque hay un proceso que no ha terminado su ejecución"
            Exit Sub
        End If

        'Si asigna el valor correspondiente a importacion
        IsCopyPaste = False

        'Se ejecuta el asyncrono
        bwMassivePayrollDatas.RunWorkerAsync()
    End Sub



#End Region

    Private Async Function GenerateValidationAutoliquidation() As Task
        Try
            Using model As New MAutoliquidation(MyBase.Tag)
                AsyncLoader(True)

                Dim companyId As Integer = INDSlCompany.EditValue
                Dim periodLiquidate As String = INDSlPeriodDate.EditValue
                Dim workCenter As Integer = INDSlWorkCenter.EditValue

                Dim contentAchive = Await model.GenerateValidationAutoliquidation(workCenter, periodLiquidate)

                If contentAchive.Count > 0 Then
					Dim listMessage As New List(Of Tuple(Of Byte, String))

					Dim TmpListVerify As New List(Of VerifyAutoliquidationFile)

                    If contentAchive.Item(0).CodeMessage = "0" Then
                        Dim ObjWorkCenter = DirectCast(GridView2.GetFocusedRow(), WorkCenter)
                        Using modelVerify As New MVerificationAutoliquidation(MyBase.Tag)
                            INDGcListEmployee.DataSource = modelVerify.ListVerifiyAutoliquidationByWorkCenterAndPayrollDate(ObjWorkCenter.Code, periodLiquidate)

                            Dim ListVerifyAuto = modelVerify.ListVerifiyAutoliquidationByWorkCenterAndPayrollDate(ObjWorkCenter.Code, periodLiquidate)

                            If INDGcListEmployee.DataSource IsNot Nothing AndAlso INDGcListEmployee.DataSource.Count > 0 Then
                                BarraBotones.StatusRecord = ListVerifyAuto.FirstOrDefault.RegisterStatus
                            End If

                        End Using

                        Me.BarraBotones.PrepareToolbar(eAction.OnlyLiquidate)
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                    ElseIf contentAchive.Item(0).CodeMessage = "999" Then
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = contentAchive.Item(0).Message
                        CleanControls()
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    ElseIf contentAchive.Item(0).CodeMessage = "888" Then
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = contentAchive.Item(0).Message
                        Dim ObjWorkCenter = DirectCast(GridView2.GetFocusedRow(), WorkCenter)
                        Using modelVerify As New MVerificationAutoliquidation(MyBase.Tag)
                            INDGcListEmployee.DataSource = modelVerify.ListVerifiyAutoliquidationByWorkCenterAndPayrollDate(ObjWorkCenter.Code, periodLiquidate)
                        End Using
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyDisconfirm)
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
						BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = False
						BarraBotones.StatusRecord = True
					End If

                    AsyncLoader(False)

                End If

            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
        End Try
    End Function

    Private Sub GridView3_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GridView3.CustomDrawCell
        If e.Column.Name = INDcolDate.Name Then
            e.DisplayText = CType(e.Cell, GridCellInfo).RowInfo.RowKey
        End If
    End Sub

    Private Async Function ConfirmLiquidation() As Task
        Try
            Using model As New MAutoliquidation(MyBase.Tag)
                AsyncLoader(True)
                Dim companyId As Integer = INDSlCompany.EditValue
                Dim periodLiquidate As String = INDSlPeriodDate.EditValue
                Dim workCenter As Integer = INDSlWorkCenter.EditValue
                Dim Confirm = Await model.ConfirmVerifyAutoliquidation(workCenter, periodLiquidate, ConfirmFlag)

                If Confirm.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = Confirm.Message

                    If ConfirmFlag = False Then
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
						BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
						BarraBotones.StatusRecord = False
					Else
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
						BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = False
						BarraBotones.StatusRecord = True
					End If

                    INDGcListEmployee.DataSource = Confirm.ObjectEmbbeded.ToList()

                Else
                    Mensaje(EeventViewerImages.Advertencia) = Confirm.Message
                End If

                BarraBotones.StatusRecord = Confirm

                AsyncLoader(False)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
        End Try

    End Function

#Region "MenuContext"

    ''' <summary>
    ''' Despliega los botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.ButtonEdit
        btn = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case btn.Text.ToString
            Case "Editar"
                EditDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Accion de click derecho del mouse
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
        End Select
    End Sub

#Region "Click"
    Private Sub INDBtnExport_Click(sender As Object, e As EventArgs) Handles INDBtnExport.Click
        Try

            If INDSlCompany.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione una Empresa"
                INDSlCompany.Focus()
                Exit Sub
            End If

            If INDSlPeriodDate.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Periodo de Liquidación"
                INDSlPeriodDate.Focus()
                Exit Sub
            End If

            If INDSlWorkCenter.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Centro de Trabajo"
                INDSlWorkCenter.Focus()
                Exit Sub
            End If

            Dim WorkCenter = DirectCast(GridView2.GetFocusedRow(), WorkCenter)

            If INDGcExportExcell.DataSource Is Nothing Then
                INDGcExportExcell.DataSource = Model.ListVerifiyAutoliquidationByWorkCenterAndPayrollDate(WorkCenter.Code, INDSlPeriodDate.EditValue)
            End If

            If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                generateExcel()
            End If

        Catch ex As Exception
            INDLciImportButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub



#End Region

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

    Private Sub bwListPayments_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)

        Try
            e.Cancel = False

            'Se instancia el listado que se va a enviar para validar la info
            listRows = New List(Of ImportFileRow)

            'Se agrega las filas del excel al listado que se va a enviar
            If IsCopyPaste Then 'Si viene desde CopyPaste
                SetRowCopyPaste(0, ListCopyPaste.Count - 1)
            Else 'Si viene desde importar archivo
                SetRow(1, rows.LastUsedIndex + 1)
            End If

            If listRows Is Nothing OrElse listRows.Count = 0 Then 'Validamos que haya valores en el listado
                e.Cancel = True
                Exit Sub
            End If

            If listRows(0).Row.Count <> 70 Then
                Mensaje(EeventViewerImages.Advertencia) = "La estructura del Archivo es la incorrecta"
                Exit Sub
            End If

            Using model As New MAutoliquidation(Me.Tag)

                'Consumimos el sp que se creó para validar el archivo de excel

                result = model.SaveMassiveVerifyAutoliquidation(listRows)

                If result.StateResult = False Then
                    If ListErrors IsNot Nothing AndAlso ListErrors.Count > 0 Then
                        Using formulario As New FrmListErrors(ListErrors)
                            formulario.StartPosition = FormStartPosition.CenterParent
                            Dim transparent As New FrmTransparent(formulario, False)
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            transparent.ShowDialog(Me)
                        End Using
                    End If
                End If

                'Limpiamos el listado de errores
                ListErrors = Nothing
            End Using

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
        End Try
    End Sub

    Private Sub bwInitialBalance_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)

        Try
            If e.Cancelled Then 'Si hubo alguna cancelación
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = "No hay datos en el listado para poder validar"
                Exit Sub
            End If

            If result Is Nothing Then
                Exit Sub
            End If
            'Si hubo error en la ejecución del sp se devuelve
            If result.StateResult = False Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = result.Message

                'Se valida que si hay errores se despliegue el form de errores con los mensajes
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If

                Exit Sub
            Else
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    Using formulario As New FrmListErrors(result.ObjectEmbbeded)
                        formulario.Title = "Información"
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If

            End If

            AsyncLoader(False)

            'Se asigna el listado del sp a la rejilla
            INDGcListEmployee.DataSource = Nothing
            INDGcExportExcell.DataSource = Nothing
            INDLcExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciImportButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
        End Try


    End Sub

    ''' <summary>
    ''' Obtiene el registro de la fila y lo inserta en el listado
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        For i As Integer = indexSend To indexEnd
            If (From info In rows.Item(i) Where info.Value.ToObject() IsNot Nothing Select info).Count > 0 Then
                listRows.Add(New ImportFileRow With {.IndexRow = i + 1, .Row = rows.Item(i).SpreadsheetRowToList(70)})
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo que convierte el listado del CopyPaste al listado que se envia al sp para que valide la info
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    Private Sub SetRowCopyPaste(indexSend As Integer, indexEnd As Integer)
        For i As Integer = indexSend To indexEnd
            listRows.Add(New ImportFileRow With {.IndexRow = i + 1, .Row = ConvertInfo(ListCopyPaste(i))})
        Next
    End Sub

    ''' <summary>
    ''' Convierte la información del CopyPaste a objeto de la entidad del listado que se envia al sp
    ''' </summary>
    ''' <returns></returns>
    Private Function ConvertInfo(data As List(Of String)) As List(Of Object)
        Dim res As New List(Of Object)
        For x As Integer = 0 To data.Count - 1
            res.Add(data(x))
        Next
        Return res
    End Function

    Private Sub INDSlWorkCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlWorkCenter.EditValueChanged
        If INDSlWorkCenter.EditValue IsNot Nothing Then

            WorkCenter = DirectCast(GridView2.GetFocusedRow(), WorkCenter)
        End If
    End Sub

#Region "PasteToGrid"

    ''' <summary>
    ''' Evento que se dispara al presionar control + v sobre la rejilla de información
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewInfo_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then 'Ctrl+V
            If Clipboard.ContainsText Then
                If Clipboard.GetText() IsNot Nothing AndAlso Not Clipboard.GetText().Trim().Equals(String.Empty) Then
                    RowsPasteToGrid = New List(Of List(Of String))
                    GetRows()
                    PasteToGrid()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Obtiene los registros del excel
    ''' </summary>
    Private Sub GetRows()
        'Se obtiene el objeto del clip board
        Dim dataObject = Clipboard.GetDataObject()

        'Se convierte el objeto en formato html, se hace de esta manera porque de otras formas no me traía las celdas finales en blanco
        Dim dataHtml As String = dataObject.GetData(DataFormats.Html)

        'Si no encuentra la tabla dentro de lo copiado se sale
        If dataHtml.IndexOf("<tr") < 0 Then
            Exit Sub
        End If

        'Se obtiene la nueva cadena de tr
        Dim trHtmlString As String = dataHtml.Substring(dataHtml.IndexOf("<tr"), dataHtml.LastIndexOf("/tr>") - dataHtml.IndexOf("<tr") + "/tr>".Length)

        'Se obtiene las lineas con el tr
        Dim linesHtml() As String = Split(trHtmlString, "/tr>" + Microsoft.VisualBasic.Constants.vbCrLf)

        'Se recorre el string de tr
        For Each line As String In linesHtml
            'Se obtiene la nueva cadena de td
            Dim tdHtmlString As String = line.Substring(line.IndexOf("<td"), line.LastIndexOf("/td>") - line.IndexOf("<td") + "/td>".Length)

            'Se obtiene el array con los td para poder sacar los valores
            Dim items() As String = Split(tdHtmlString, "</td>")

            'Array final que se agrega al listado
            Dim finallyArray As New List(Of String)

            'Se recorre los items para poder armar el array final
            For Each lineTd As String In items
                If Not (lineTd IsNot Nothing AndAlso lineTd IsNot String.Empty) Then 'Si la linea trae vacío
                    Continue For
                End If
                'Se declara el valor para el listado final
                Dim valueString As String = ""
                If ((lineTd.Length - 1) - lineTd.LastIndexOf(">")) > 0 Then 'Si trae el valor al final
                    valueString = lineTd.Substring(lineTd.LastIndexOf(">"), (lineTd.Length - 1) - lineTd.LastIndexOf(">") + 1) 'Se obtiene el valor
                    valueString = valueString.Replace(">", "") 'Se reemplaza el valor de > en el valor final con vacio
                    valueString = valueString.Replace("&nbsp;", "")
                End If
                'Se agrega al listado
                finallyArray.Add(valueString)
            Next
            'Se agrega al listado final el listado de string que se armó
            RowsPasteToGrid.Add(New List(Of String)(finallyArray))
        Next
    End Sub

    ''' <summary>
    ''' Método que se ejecuta al momento de pegar info a la rejilla desde un excel
    ''' </summary>
    Private Sub PasteToGrid()
        AsyncLoader(True)

        'Se asigna el listado del CopyPaste
        ListCopyPaste = RowsPasteToGrid

        'Si el listado no trae nd se sale del método
        If Not (ListCopyPaste IsNot Nothing AndAlso ListCopyPaste.Count > 0) Then
            AsyncLoader(False)
            Exit Sub
        End If

        'Se llama el asyncrono para validar el excel siempre y cuando no haya un asyncrono en ejecución
        If bwMassivePayrollDatas.IsBusy Then
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = "No puede continuar porque hay un proceso que no ha terminado su ejecución"
            Exit Sub
        End If

        'Si asigna el valor correspondiente a CopyPaste
        IsCopyPaste = True

        'Se ejecuta el asyncrono
        bwMassivePayrollDatas.RunWorkerAsync()
    End Sub

#End Region
End Class