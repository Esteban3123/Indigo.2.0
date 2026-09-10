Imports Presentation.Controls
Imports Domain.Payroll.Entities
Imports Presentation.Base
Imports Presentation.Payroll.MVP
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports System.Windows.Forms
Imports Domain.Base.Entities
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports System.IO

Public Class FrmFileForeclousure

    Implements INationalSavingsFund

    ''' <summary>
    ''' variable para controlar el presentador del formulario
    ''' </summary>
    ''' <remarks></remarks>
    'Dim presenter As PNational

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String

    ''' <summary>
    ''' Variable para almacenar la compañia
    ''' </summary>
    ''' <remarks></remarks>
    Dim company As Company

#Region "Properties"


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
#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        PathFunctionalDefinitions = Nothing
        company = Nothing
    End Sub
    Private Async Sub FrmNationalSavingsFund_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '****Inicializar variables*****'
        Me._doc = Nothing
        '******************************'
        'Me._funct = AddressOf GenerateDoc
        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollBank.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If

        'presenter = New PBankFile(Me)
        'presenter.Initializes()

        AsyncLoader(True)
        Using modelLiquidation As New MPayrollLiquidation
            INDgleLastLiquidationDate.Properties.DataSource = Await modelLiquidation.GetLiquidationDatesConfirmPayroll()
        End Using
        AsyncLoader(False)

        Using model As New MBankFile(Me.Tag)
            Dim ListCompany = Await model.ListAllCompany()
            INDgleCompany.Properties.DataSource = ListCompany.Where(Function(x) x.PayrollType = True).ToList()
        End Using
    End Sub

    Private Sub FrmNationalSavingsFund_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDgleCompany.Enabled Then
            INDgleCompany.Focus()
        End If
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Sub CleanControls()
        INDgleCompany.EditValue = Nothing
        INDgleLastLiquidationDate.Text = String.Empty
        INDgleLastLiquidationDate.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Metodo para validar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Function ValidateControls() As Boolean

        If Not (INDgleLastLiquidationDate.EditValue IsNot Nothing AndAlso CStr(INDgleLastLiquidationDate.EditValue) <> "") Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Eform.Incapacidades), INDlyItemLastLiquidationDate.Text)
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Metodo que despliega show dialog para guardar un archivo plano
    ''' </summary>
    ''' <param name="content"></param>
    ''' <remarks></remarks>
    Public Sub DialogGenerateFile(content As StringBuilder)
        Dim save As SaveFileDialog = New SaveFileDialog()
        save.Filter = "Texto|*.txt"
        'save.Title = INDlyGrBankFile.Text
        Dim dateLiquidation As Date = CType(INDgleLastLiquidationDate.EditValue, Date).Date
        company = CType(INDgleCompany.GetSelectedDataRow, Company)
        save.FileName = "GD" & dateLiquidation.Year & Utils.StringPad(dateLiquidation.Month, 2, 0, Utils.PadType.STR_PAD_LEFT) & Utils.StringPad(dateLiquidation.Day, 2, 0, Utils.PadType.STR_PAD_LEFT) & Utils.StringPad(company.ThirdParty.Nit & company.ThirdParty.DigitVerification, 11, 0, Utils.PadType.STR_PAD_LEFT) & "_01" & ".txt"
        If save.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim file = save.OpenFile()
            Dim streamWrite As New StreamWriter(file)
            streamWrite.Write(content)
            streamWrite.Flush()
            streamWrite.Close()
            If MessageIndigo.Show(obtenerRecurso(GuardadoDeseaAbrir, Eform.ArchivoBanco), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Process.Start(save.FileName)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se ejecuta cuando se desea generar el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GenerateFile() As Task
        Try
            Dim dateLiquidation As Date = CType(INDgleLastLiquidationDate.EditValue, Date).Date
            Dim companyId As Integer = INDgleCompany.EditValue
            Dim MessageCode As String = ""
            If ValidateControls() = False Then
                Exit Function
            End If
            AsyncLoader(True)
            Dim resultGenerateFile As ActionMessageResult(Of StringBuilder)
            Using model As New MFileForeclousure
                resultGenerateFile = Await model.GenerateFileForeclousureFundAsync(dateLiquidation, companyId)
            End Using
            If resultGenerateFile.StateResult = True Then
                For i As Integer = 0 To resultGenerateFile.MessageResult.Count() - 1
                    MessageCode = resultGenerateFile.MessageResult.Item(i).CodeMessage
                Next

                If MessageCode = "-002" Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ExisteAuditoriaArchivoBanco, Eform.ArchivoBanco)
                End If

                DialogGenerateFile(resultGenerateFile.ObjectEmbbeded)
            Else
                If resultGenerateFile.MessageResult IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = resultGenerateFile.Message
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                End If
            End If
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
        End Try

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
                    INDgleLastLiquidationDate.Properties.DataSource = Await model.GetDateLiquidationCompany(company.Id)
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

#Region "ICRUD"

    Public Sub Buscar() Implements IcrudBase.Buscar
        AbrirBusqueda()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

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

    End Sub

#End Region

#Region "Bar buttons events"

    ''' <summary>
    ''' Evento que se ejecuta al cargar la barra botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Me.Tag)
        'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyGenerateFile)
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
        Deshacer()
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
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barra de botones Click Generar archivo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_GenerateFile() Handles BarraBotones.Click_GenerateFile
        Await GenerateFile()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
    End Sub

#End Region

End Class