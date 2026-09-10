#Region "Imports"
Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Payroll.MVP
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
#End Region

Public Class FrmINSReport
    Implements IVerificateAutoliquidation

#Region "Variables"
    ''' <summary>
    ''' Booleans a utilizar
    ''' </summary>
    Dim toClean As Boolean = False
    Dim isConfirmed As Boolean = False
    Dim hasFrontDefinition As Boolean = False
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadFunctionalDefinitions As BackgroundWorker
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MVerificationAutoliquidation
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PCRCashReport
    ''' <summary>
    ''' Listado del CopyPaste
    ''' </summary>
    Dim copyPasteList As List(Of List(Of String))
    ''' <summary>
    ''' coleccion de filas que se van a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rows As RowCollection
    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New List(Of ImportFileRow)()
    ''' <summary>
    ''' Guarda la ruta de las definiciones funcionales
    ''' </summary>
    Dim pathFunctionalDefinitions As String
    ''' <summary>
    ''' Objeto Background para procesar los datos masivos de nómina
    ''' </summary>
    Private bwMassivePayrollDatas As BackgroundWorker = New BackgroundWorker
    ''' <summary>
    ''' Permite saber si viene desde copiar y pegar o de importar archivo
    ''' </summary>
    Private isCopyPaste As Boolean
    ''' <summary>
    ''' Resultado de la consulta al sp
    ''' </summary>
    Private result As ActionResult(Of List(Of Tuple(Of String, Integer)))
    ''' <summary>
    ''' Listado de errores que devuelve el sp
    ''' </summary>
    Private errorsList As List(Of Tuple(Of String, Integer))
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As Domain.Entities.BlockRecord
    ''' <summary>
    ''' Nombre del modulo
    ''' </summary>
    Private _nameModule As String = "Payroll"
#End Region

#Region "Init"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler bwMassivePayrollDatas.DoWork, AddressOf bwListPayments_DoWork
        AddHandler bwMassivePayrollDatas.RunWorkerCompleted, AddressOf bwInitialBalance_RunWorkerCompleted
    End Sub
    ''' <summary>
    ''' Propiedad que contiene el numero de poliza
    ''' </summary>
    ''' <returns></returns>
    Public Property PolicyNumber As String
        Get
            Return INDtxtPolicyNumber.EditValue
        End Get
        Set(value As String)
            INDtxtPolicyNumber.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Cuando al formulario se le ha hecho disposed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        hasFrontDefinition = Nothing
        pathFunctionalDefinitions = Nothing
        Model = Nothing
        Presenter = Nothing
        _record = Nothing
        ObjEmployee = Nothing
        Idgroup = Nothing
        isCopyPaste = Nothing
    End Sub
    ''' <summary>
    ''' Cuando se carga el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub FrmINSReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        INDGcMain.RefreshGrid(INDGcListEmployee)

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Model = New MVerificationAutoliquidation(Me.Tag)
        Me.indigo = SessionValues.Instance
        '******************************'

        'Cargamos de manera asincrona definiciones del funcional
        pathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloGlosas.", Me.Name, ".xml")
        LoadFunctionalDefinitions = New BackgroundWorker
        If Not LoadFunctionalDefinitions.IsBusy Then
            LoadFunctionalDefinitions.RunWorkerAsync()
        End If

        'Otros procesos
        Presenter = New PCRCashReport(Me)
        Deshacer()
        Me.LoadStatus()
        Me.BarraBotones.StatusRecordVisible = True
        SetCurrencyFormat(Presenter.LoadPayrollSettings().CurrencyId.Abbreviation)
        BarraBotones.RibbonPageProcesos.Visible = False

        Using model As New MAutoliquidation(MyBase.Tag)
            AsyncLoader(True)
            Dim ListCompany As List(Of Company) = Await model.ListAllCompany()
            If ListCompany?.Count > 0 Then
                If ListCompany.Any(Function(x) x.PayrollType) Then
                    INDSlCompany.Properties.DataSource = ListCompany.Where(Function(x) x.PayrollType).ToList()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No existe ninguna empresa de 'Tipo Nómina' creada."
                End If
            End If
            AsyncLoader(False)
        End Using
    End Sub
    ''' <summary>
    ''' Cerrando el formulario
    ''' </summary>
    Private Sub FrmINSReport_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
        If toClean Then
            If ShowWarningMessage() = DialogResult.No Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "ICRUD"
    ''' <summary>
    ''' Activa o deshabilita los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IVerificateAutoliquidation.ActionsOnControls
        Set(value As Boolean)
            INDLcAutoliquidation.BeginUpdate()
            INDSlCompany.Enabled = Not value
            INDtxtPolicyNumber.Enabled = value
            INDSlPeriodDate.Enabled = value
            INDSlWorkCenter.Enabled = value
            INDBtnProccess.Enabled = value
            INDLcAutoliquidation.EndUpdate()
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para mostrar los mensajes del sistema
    ''' </summary>
    ''' <param name="Icono"></param>
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

    ''' <summary>
    ''' Barra de carga
    ''' </summary>
    ''' <param name="State"></param>
    Public Overrides Sub AsyncLoader(State As Boolean) Implements IVerificateAutoliquidation.AsyncLoader
        MyBase.AsyncLoader(State)
    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo
    End Sub
    ''' <summary>
    ''' Verificar se se ha hecho alguna consulta y proceder a limpiar los controles
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        If toClean Then
            If ShowWarningMessage() = System.Windows.Forms.DialogResult.Yes Then
                CleanControls()
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlyGenerateFile)
            End If
        Else
            CleanControls()
        End If
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar
    End Sub

    Public Sub Buscar() Implements ICrudBase.Buscar
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Await Model.DeleteBlockRecord(_record)
            _record = Nothing
        End If
    End Sub
    ''' <summary>
    ''' Obtiene el registro de la fila y lo inserta en el listado
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        For i As Integer = indexSend To indexEnd
            If rows.Item(i).Any(Function(info) info.Value.ToObject() IsNot Nothing) Then
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
        For i As Integer = indexSend To Math.Min(indexEnd, copyPasteList.Count - 1)
            listRows.Add(New ImportFileRow With {.IndexRow = i + 1, .Row = ConvertInfo(copyPasteList(i))})
        Next
    End Sub
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
        INDColIngress = Window.Utils.FormatGrid(INDColIngress, _currencyAbbreviation)
    End Sub
    ''' <summary>
    ''' Actualiza el texto del GridView para el periodo de liquidación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvDateLiquidated_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDGvDateLiquidated.CustomDrawCell
        If e.Column.Name = INDcolDate1.Name Then
            e.DisplayText = CType(e.Cell, GridCellInfo).RowInfo.RowKey
        End If
    End Sub
    ''' <summary>
    ''' Genera un archivo excel en la ruta por defecto
    ''' </summary>
    Private Sub GenerateExcel()
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
    ''' Limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDLcAutoliquidation.BeginUpdate()

        INDSlCompany.EditValue = Nothing
        PolicyNumber = Nothing
        INDSlWorkCenter.EditValue = Nothing
        INDSlPeriodDate.EditValue = Nothing
        INDGcListEmployee.DataSource = Nothing
        INDLcExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciImportButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        BarraBotones.RibbonPageProcesos.Visible = False
        INDLcAutoliquidation.EndUpdate()
        Me.ActionsOnControls = False
    End Sub
    ''' <summary>
    ''' Cargar controles
    ''' </summary>
    Private Async Sub LoadControls()
        AsyncLoader(True)
        Using model As New MAutoliquidation(MyBase.Tag)
            Dim Company = CType(INDSlCompany.GetSelectedDataRow, Company)
            If Not Company Is Nothing Then
                If Company.Id > 0 Then
                    listDateLiquidation = Await model.GetDateLiquidationCompany(Company.Id)
                    INDSlPeriodDate.Properties.DataSource = listDateLiquidation
                    listWorkCenter = Await model.ListAllWorkCenter()
                    INDSlWorkCenter.Properties.DataSource = listWorkCenter
                    ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Else
                    Mensaje(EeventViewerImages.Pregunta) = BaseClass.obtenerRecurso(Eresources.ComunesNoSeEncontroDatoERP)
                End If
            Else
                Mensaje(EeventViewerImages.Pregunta) = BaseClass.obtenerRecurso(Eresources.ComunesNoSeEncontroDatoERP)
            End If
        End Using
        AsyncLoader(False)
    End Sub
    ''' <summary>
    ''' Importar un archivo
    ''' </summary>
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
            Dim sprdSheetControl = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
            sprdSheetControl.AllowDrop = False
            sprdSheetControl.LoadDocument(myStream)
            Dim workBook As IWorkbook = sprdSheetControl.Document

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

        'Se ejecuta el asyncrono
        isCopyPaste = False
        bwMassivePayrollDatas.RunWorkerAsync()
    End Sub

    ''' <summary>
    ''' Metodo para construir el Txt
    ''' </summary>
    ''' <param name="content"></param>
    Public Sub GenerateFile(content As StringBuilder)
        Dim save As SaveFileDialog = New SaveFileDialog()
        save.Filter = "Texto|*.txt"
        save.Title = "Guardar Archivo Plano Reporte INS"
        Dim dateLiquidation = CType(INDSlPeriodDate.EditValue, String)
        Dim Company = CType(INDSlCompany.GetSelectedDataRow, Company)
        save.FileName = "r_ins_" + dateLiquidation + ".txt"
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
#End Region

#Region "Functions"

    ''' <summary>
    ''' Metodo que generara el Archivo Plano
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GenerateFileINS() As Task

        AsyncLoader(True)
        If INDGcListEmployee.DataSource IsNot Nothing AndAlso INDGcListEmployee.DataSource.count > 0 Then

            Dim resultGenerateFileINS As ActionMessageResult(Of StringBuilder)
            Using model As New MAutoliquidation(MyBase.Tag)
                Dim Company = CType(INDSlCompany.GetSelectedDataRow, Company)
                Dim policyNumber = CType(INDtxtPolicyNumber.Text, String)
                Dim workCenter = CType(INDSlWorkCenter.GetSelectedDataRow, WorkCenter)
                Dim dateLiquidation = CType(INDSlPeriodDate.EditValue, String)
                resultGenerateFileINS = Await model.GenerateINS(Company.Id, policyNumber, workCenter.Id, dateLiquidation)

            End Using
            If resultGenerateFileINS.StateResult = True And resultGenerateFileINS.ObjectEmbbeded IsNot Nothing Then
                Me.GenerateFile(resultGenerateFileINS.ObjectEmbbeded)
                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SavedSuccessfile", _nameModule)
            Else
                Mensaje(EeventViewerImages.Advertencia) = resultGenerateFileINS.Message
            End If
        Else
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("NoResultFile", _nameModule)

        End If

        AsyncLoader(False)

    End Function

    ''' <summary>
    ''' Muestra un mensaje de advertencia para confirmar si se deben perder los cambios.
    ''' </summary>
    ''' <returns>Devuelve el resultado de la respuesta del usuario</returns>
    Private Function ShowWarningMessage() As DialogResult
        Return MessageIndigo.Show("Perderá todos los cambios ¿desea continuar?", MessageType.Question, Me.Text, Botones.SiNo)
    End Function
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
    ''' <summary>
    ''' Valida que los datos para procesar estén seleccionados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateData() As Boolean

        If INDSlCompany.EditValue Is Nothing Then
            INDSlCompany.Focus()
            Return False
        End If

        If PolicyNumber Is Nothing Then
            INDtxtPolicyNumber.Focus()
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
    ''' Validar los campos a procesar
    ''' </summary>
    Private Async Function GenerateValidationAutoliquidation() As Task
        Try
            Using model As New MAutoliquidation(MyBase.Tag)
                AsyncLoader(True)

                Dim companyId As Integer = INDSlCompany.EditValue
                Dim periodLiquidate As String = INDSlPeriodDate.EditValue
                Dim workCenter As Integer = INDSlWorkCenter.EditValue

                Dim contentArchive = Await model.GenerateValidationAutoliquidationCR(workCenter, periodLiquidate)

                If contentArchive.Count > 0 Then

                    If contentArchive.Item(0).CodeMessage = "0" Then
                        Dim ObjWorkCenter = DirectCast(INDGvWorkCenter.GetFocusedRow(), WorkCenter)

                        Using modelVerify As New MVerificationAutoliquidation(MyBase.Tag)
                            Dim ListVerifyAuto = modelVerify.ListVerifiyAutoliquidationINS(ObjWorkCenter.Code, periodLiquidate)


                            If ListVerifyAuto.Any() Then
                                BarraBotones.StatusRecord = ListVerifyAuto.FirstOrDefault.RegisterStatus
                            End If
                            INDGcListEmployee.DataSource = ListVerifyAuto
                        End Using

                        Me.BarraBotones.PrepareToolbar(eAction.OnlyGenerateFile)

                    ElseIf contentArchive.Item(0).CodeMessage = "999" Then
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = contentArchive.Item(0).Message
                        CleanControls()
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)

                    ElseIf contentArchive.Item(0).CodeMessage = "888" Then
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = contentArchive.Item(0).Message
                        Dim ObjWorkCenter = DirectCast(INDGvWorkCenter.GetFocusedRow(), WorkCenter)
                        Using modelVerify As New MVerificationAutoliquidation(MyBase.Tag)
                            INDGcListEmployee.DataSource = modelVerify.ListVerifiyAutoliquidationINS(ObjWorkCenter.Code, periodLiquidate)
                        End Using

                        Me.BarraBotones.PrepareToolbar(eAction.OnlyGenerateFile)
                    End If
                    AsyncLoader(False)
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
        End Try
    End Function
    ''' <summary>
    ''' Función para confirmar una liquidación en la bd
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ConfirmLiquidation() As Task
        Try
            Using model As New MAutoliquidation(MyBase.Tag)
                AsyncLoader(True)
                Dim companyId As Integer = INDSlCompany.EditValue
                Dim periodLiquidate As String = INDSlPeriodDate.EditValue
                Dim workCenter As Integer = INDSlWorkCenter.EditValue
                Dim Confirm = Await model.ConfirmVerifyAutoliquidation(workCenter, periodLiquidate, isConfirmed)

                If Confirm.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = Confirm.Message
                    If isConfirmed Then
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = False
                    Else
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
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

#End Region

#Region "Events"
    Private Sub bwListPayments_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Try
            e.Cancel = False

            'Se instancia el listado que se va a enviar para validar la info
            listRows = New List(Of ImportFileRow)

            'Se agrega las filas del excel al listado que se va a enviar
            If isCopyPaste Then 'Si viene desde CopyPaste
                SetRowCopyPaste(0, copyPasteList.Count - 1)
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
                    If errorsList IsNot Nothing AndAlso errorsList.Count > 0 Then
                        Using formulario As New FrmListErrors(errorsList)
                            formulario.StartPosition = FormStartPosition.CenterParent
                            Dim transparent As New FrmTransparent(formulario, False)
                            Me.Cursor = Cursors.Default
                            transparent.ShowDialog(Me)
                        End Using
                    End If
                End If

                'Limpiamos el listado de errores
                errorsList = Nothing
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

            'Limpiamos y ocultamos la rejilla
            INDGcListEmployee.DataSource = Nothing
            INDGcExportExcel.DataSource = Nothing
            INDLcExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciImportButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Carga los datos cuando se selecciona una compañía y los limpia cuando es vaciado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlCompany_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlCompany.EditValueChanged
        If INDSlCompany.EditValue IsNot Nothing Then
            LoadControls()
        Else
            toClean = False
            Deshacer()
        End If
    End Sub
    ''' <summary>
    ''' Permite que solo se digiten números
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtPolicyNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles INDtxtPolicyNumber.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Consulta todos los campos asociados a los parámetros seleccionados para agregarlos a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnProccess_Click(sender As Object, e As EventArgs) Handles INDBtnProccess.Click
        If ValidateData() = False Then
            Mensaje(EeventViewerImages.Advertencia) = "Faltan llenar algunos campos"
            Return
        End If
        Try
            Using model As New MVerificationAutoliquidation(MyBase.Tag)
                AsyncLoader(True)
                Dim periodLiquidate As String = INDSlPeriodDate.EditValue
                Dim WorkCenter = DirectCast(INDGvWorkCenter.GetFocusedRow(), WorkCenter)

                Dim TmpListVerify = model.ListVerifiyAutoliquidationINS(WorkCenter.Code, periodLiquidate)
                INDGcListEmployee.DataSource = TmpListVerify

                If INDGcListEmployee.DataSource IsNot Nothing AndAlso INDGcListEmployee.DataSource.count > 0 Then

                    INDLcExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciImportButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    If TmpListVerify.Any(Function(x) x.RegisterStatus = False) Then
                        If MessageIndigo.Show("Ya existe generado un archivo con esta fecha sin confirmar. Desea volverlo a generar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                            Await GenerateValidationAutoliquidation()
                        Else
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyGenerateFile)
                            INDGcExportExcel.DataSource = TmpListVerify
                            BarraBotones.StatusRecord = TmpListVerify.FirstOrDefault().RegisterStatus
                            AsyncLoader(False)
                        End If
                    Else
                        Await GenerateValidationAutoliquidation()
                    End If
                Else
                    Await GenerateValidationAutoliquidation()
                End If
                toClean = True
                AsyncLoader(False)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
        End Try
    End Sub
    ''' <summary>
    ''' Evento que se dispara al clickear el botón de exportar en excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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

            Dim WorkCenter = DirectCast(INDGvWorkCenter.GetFocusedRow(), WorkCenter)

            If INDGcExportExcel.DataSource Is Nothing Then
                INDGcExportExcel.DataSource = Model.ListVerifiyAutoliquidationByWorkCenterAndPayrollDate(WorkCenter.Code, INDSlPeriodDate.EditValue)
            End If

            If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                GenerateExcel()
            End If

        Catch ex As Exception
            INDLciImportButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub
    Private Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        ImportFile()
    End Sub
#End Region

#Region "Barra Botones"
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
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = "Sin confirmar", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(122, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = "Confirmado", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        Me.BarraBotones.States = listStates
        Me.BarraBotones.StatusRecordEnabled = False
    End Sub
    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.Deshacer()
    End Sub
    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub
    ''' <summary>
    ''' Confirmar una liquidación
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        If MessageIndigo.Show("Desea Confirmar el Proceso para Generar el Archivo?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            isConfirmed = True
            Await ConfirmLiquidation()
        End If
    End Sub
    ''' <summary>
    ''' Desconfirma una liquidación
    ''' </summary>
    Private Async Sub BarraBotones_ClickDesconfirmar() Handles BarraBotones.Click_Desconfirmar
        If MessageIndigo.Show("Desea Desconfirmar el Proceso?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            isConfirmed = False
            Await ConfirmLiquidation()
        End If
    End Sub
    ''' <summary>
    ''' Evento cuando se le da click a generar archivo plano
    ''' </summary>
    Private Async Sub BarraBotones_GenerarArchivo() Handles BarraBotones.Click_GenerateFile
        Await GenerateFileINS()
    End Sub

#End Region

#Region "Customize"
    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDLcAutoliquidation.ShowCustomizationForm()
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Async Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLcAutoliquidation.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Dim dsFields As DataSet = Await Model.GetFieldsNULL
            If dsFields IsNot Nothing Then
                dtFieldsCustomizables = dsFields.Tables(0)
                For j As Integer = 0 To INDLcAutoliquidation.Items.Count - 1
                    INDLcAutoliquidation.Items.Item(j).AllowHide = False
                    For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                        If Object.Equals(INDLcAutoliquidation.Items.Item(j).Tag, Nothing) = False Then
                            If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDLcAutoliquidation.Items.Item(j).Tag.ToString.Trim Then
                                INDLcAutoliquidation.Items.Item(j).AllowHide = True
                            End If
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = BaseClass.obtenerRecurso(Eresources.ComunesErrorGuardarDefinicion)
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
                    INDLcAutoliquidation.SaveLayoutToXml(pathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = BaseClass.obtenerRecurso(Eresources.ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.MensajeError) = BaseClass.obtenerRecurso(Eresources.ComunesErrorGuardarDefinicion)
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Rejillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDLcAutoliquidation.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = BaseClass.obtenerRecurso(Eresources.ComunesLayoutRestablecido)
        End If
    End Sub
    ''' <summary>
    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LoadFunctionalDefinitions.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(pathFunctionalDefinitions) = True Then
            hasFrontDefinition = True
        End If
    End Sub

    ''' <summary>
    ''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadFunctionalDefinitions.RunWorkerCompleted
        If hasFrontDefinition = True Then
            INDLcAutoliquidation.RestoreLayoutFromXml(pathFunctionalDefinitions)
        End If
    End Sub
#End Region

End Class