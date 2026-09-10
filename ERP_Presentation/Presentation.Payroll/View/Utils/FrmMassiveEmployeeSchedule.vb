Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Design
Imports DevExpress.XtraEditors
Imports DevExpress.Data
Imports Presentation.Payroll.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Presentation.Controls
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base.BaseClass
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Spreadsheet
Imports System.Text
Imports Domain.Entities
Imports System.IO
Imports DevExpress.CodeParser
Imports DevExpress.XtraGrid.Columns

Public Class FrmMassiveEmployeeSchedule

    Private bwMassivePayrollDatas As BackgroundWorker = New BackgroundWorker

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
    Dim contractPath As String = Nothing

    ''' <summary>
    ''' Permite saber si viene desde copiar y pegar o de importar archivo
    ''' </summary>
    Private IsCopyPaste As Boolean

    ''' <summary>
    ''' Listado que devuelve el sp que valida el excel con los registros en OK
    ''' </summary>
    Private ListSP_ImportFileAgreements_Result As List(Of SP_ValidateMassiveManualConcepts_Result)

    ''' <summary>
    ''' Listado de errores que devuelve el sp
    ''' </summary>
    Private ListErrors As List(Of Tuple(Of String, Integer))
    ''' <summary>
    ''' Listado de notificaciones al importar.
    ''' </summary>
    Private ListMessages As List(Of Tuple(Of String, Integer))

    Private ListOutput As List(Of Tuple(Of String, Integer))

    ''' <summary>
    ''' Bandera para determinar que va ejecutar el background worker.
    ''' </summary>
    Private FlagConfirm As Boolean

    ''' <summary>
    ''' Resultado de la consulta al sp
    ''' </summary>
    Private validationResult As List(Of SP_ValidateMassiveEmployeeSchedule_Result)

    Private validationResultError As List(Of SP_ValidateMassiveEmployeeSchedule_Result)

    Private validationResultMessage As List(Of SP_ValidateMassiveEmployeeSchedule_Result)

    Private _MassiveManualConcepts As List(Of SP_ValidateMassiveEmployeeSchedule_Result)

    Private _DTExcel As DataTable

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PayrollSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    Dim Action As String

    Dim Status As Byte = 1

    Dim ObjEmployeeScheduleC As EmployeeScheduleC

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordPayroll

    Public Property Sequense As PayrollSequence
        Get
            Return Me._sequence
        End Get
        Set(value As PayrollSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PayrollSequenceDetail In Me._sequence.PayrollSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public Property Code As String
        Get
            If (INDbtnCode.Text.Trim().Equals(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

#Region "Builder"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        AddHandler bwMassivePayrollDatas.DoWork, AddressOf bwListMassiveEmployeeSchedule_DoWork
        AddHandler bwMassivePayrollDatas.RunWorkerCompleted, AddressOf bwListMassiveEmployeeSchedule_RunWorkerCompleted
    End Sub

#End Region

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        bwMassivePayrollDatas = Nothing
        rows = Nothing
        RowsPasteToGrid = Nothing
        ListCopyPaste = Nothing
        listRows = Nothing
        myStream = Nothing
        IsCopyPaste = Nothing
        ListSP_ImportFileAgreements_Result = Nothing
        ListErrors = Nothing
        validationResult = Nothing
        validationResultError = Nothing
        _idCurrentSequence = Nothing
        Sequense = Nothing
    End Sub


    Private Async Sub FrmMassiveManualConcepts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDviewInfo, ListActions)
        BarButtonsConfigure()
        CleanControls()
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue

        Using model As New MBlockRecordAndSequensePayroll(Me.Tag)
            Sequense = Await model.GetSequense()
        End Using

        LoadStatus()

    End Sub

#Region "Backgroundworker"
    ''' <summary>
    ''' Inicia el backgroundWorker para consultar el listado de novedades masivo.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwListMassiveEmployeeSchedule_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        e.Cancel = False

        If FlagConfirm = False Then
            'Se instancia el listado que se va a enviar para validar la info

            listRows = New List(Of ImportFileRow)

            If ObjEmployeeScheduleC.Id > 0 Then
                listRows = CargarListRow(INDgcInformation.DataSource)
            End If

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
            Using model As New MMassiveContract(Me.Tag)
                Action = "Hola"
                Dim ListaGenerica As New GenericListEmployeeSchedule
                ListaGenerica.List_ImportFileRow = listRows
                validationResult = model.ValidateMassiveEmployeeSchedule(ListaGenerica, Action, Code, INDTxtDescription.EditValue, Status, 0, 0)
                validationResultError = validationResult.Where(Function(x) x.Tipo = "Error").ToList()
                validationResultMessage = validationResult.Where(Function(x) x.Tipo = "Info").ToList()
                If validationResultError.Count > 0 Then
                    ListErrors = New List(Of Tuple(Of String, Integer))
                    ListMessages = New List(Of Tuple(Of String, Integer))
                    For Each linea As SP_ValidateMassiveEmployeeSchedule_Result In validationResultError
                        Dim mensaje As String = String.Format("{0}. {1}. Columna: {2}. Valor: {3}", linea.Linea, linea.Mensaje, linea.Columna, linea.Valor)
                        ListErrors.Add(New Tuple(Of String, Integer)(mensaje, 2))
                    Next
                    _MassiveManualConcepts = New List(Of SP_ValidateMassiveEmployeeSchedule_Result)
                Else
                    If validationResultMessage.Count > 0 Then
                        ListMessages = New List(Of Tuple(Of String, Integer))
                        For Each linea As SP_ValidateMassiveEmployeeSchedule_Result In validationResultMessage
                            Dim mensaje As String = String.Format("{0}. {1}. Columna: {2}. Valor: {3}", linea.Linea, linea.Mensaje, linea.Columna, linea.Valor)
                            ListMessages.Add(New Tuple(Of String, Integer)(mensaje, 1))
                        Next
                    End If
                    ListErrors = Nothing
                    _MassiveManualConcepts = validationResult.Where(Function(x) x.Tipo = "Datos").ToList()
                End If
            End Using
        Else
            If INDgcInformation.DataSource IsNot Nothing AndAlso INDgcInformation.DataSource.Count > 0 Then

                Using model As New MMassiveContract(Me.Tag)
                    Dim ConfirmParam As SP_ValidateMassiveEmployeeSchedule_Result = New SP_ValidateMassiveEmployeeSchedule_Result With {
                            .Mensaje = "Confirmar"
                            }
                    INDgcInformation.DataSource.Add(ConfirmParam)
                    Dim ListaGenerica As New GenericListEmployeeSchedule With {
                            .ListSP_ValidateMassiveEmployeeSchedule = INDgcInformation.DataSource
                            }
                    Dim ListModel = model.ValidateMassiveEmployeeSchedule(ListaGenerica, Action, Code, INDTxtDescription.EditValue, Status, 0, 0)
                    ListOutput = New List(Of Tuple(Of String, Integer))
                    For Each linea As SP_ValidateMassiveEmployeeSchedule_Result In ListModel
                        Dim mensaje As String = String.Format("{0}. {1}. Columna: {2}. Valor: {3}", linea.Linea, linea.Mensaje, linea.Columna, linea.Valor)
                        ListOutput.Add(New Tuple(Of String, Integer)(mensaje, 1))
                    Next
                End Using

            Else
                Mensaje(EeventViewerImages.Advertencia) = "Por favor importe los datos."
            End If
        End If

    End Sub

    Private Function CargarListRow(List As List(Of SP_ValidateMassiveEmployeeSchedule_Result)) As List(Of ImportFileRow)
        Dim listRows As New List(Of ImportFileRow)


        For Each ObjSP As SP_ValidateMassiveEmployeeSchedule_Result In List

            Dim NewRows As New ImportFileRow

            NewRows.Row = New List(Of Object)

            Dim ListObj As New List(Of Object)

            Dim InitialYearDate As Integer = ObjSP.InitialDate.Substring(0, 4)
            Dim InitialMonthDate As Integer = ObjSP.InitialDate.Substring(5, 2)
            Dim DayInitialDate As Integer = ObjSP.InitialDate.Substring(8, 2)

            Dim EndYearDate As Integer = ObjSP.EndDate.Substring(0, 4)
            Dim EndMonthDate As Integer = ObjSP.EndDate.Substring(5, 2)
            Dim DayEndDate As Integer = ObjSP.EndDate.Substring(8, 2)

            ListObj.Add(ObjSP.Nit)
            ListObj.Add(New Date(InitialYearDate, InitialMonthDate, DayInitialDate))
            ListObj.Add(ObjSP.InitialHourDate)
            ListObj.Add(New Date(EndYearDate, EndMonthDate, DayEndDate))
            ListObj.Add(ObjSP.EndHourDate)

            NewRows.Row = ListObj

            listRows.Add(NewRows)

        Next

        Return listRows

    End Function

    Private Sub bwListMassiveEmployeeSchedule_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        If e.Cancelled Then 'Si hubo alguna cancelación
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = "No hay datos en el listado para poder validar"
            Exit Sub
        End If

        AsyncLoader(False)

        If FlagConfirm = False Then

            'Se asigna el listado del sp a la rejilla
            INDgcInformation.DataSource = Nothing
            INDgcInformation.DataSource = _MassiveManualConcepts
            INDviewInfo.BestFitColumns()
            'Se valida que si hay errores se despliegue el form de errores con los mensajes
            If ListErrors IsNot Nothing AndAlso ListErrors.Count > 0 Then
                Using formulario As New FrmListErrors(ListErrors)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, True)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
            'Se muestran las notificaciones de la importacion.
            If ListMessages IsNot Nothing AndAlso ListMessages.Count > 0 Then
                Using formulario As New FrmListErrors(ListMessages)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    formulario.Title = "Confirmacion de importación de novedades."
                    Dim transparent As New FrmTransparent(formulario, True)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
        Else
            If ListOutput IsNot Nothing AndAlso ListOutput.Count > 0 Then
                Using formulario As New FrmListErrors(ListOutput)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    formulario.Title = "Confirmacion carga de novedades."
                    Dim transparent As New FrmTransparent(formulario, True)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
            'Mensaje(EeventViewerImages.Informacion) = "Prórrogas salvadas exitosamente"
            INDgcInformation.DataSource = Nothing

        End If

    End Sub
#End Region

    ''' <summary>
    ''' Obtiene el registro de la fila y lo inserta en el listado
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        For i As Integer = indexSend To indexEnd
            If (From info In rows.Item(i) Where info.Value.ToObject() IsNot Nothing Select info).Count > 0 Then
                listRows.Add(New ImportFileRow With {.IndexRow = i + 1, .Row = rows.Item(i).SpreadsheetRowToList(55)})
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


#Region "PasteToGrid"

    ''' <summary>
    ''' Evento que se dispara al presionar control + v sobre la rejilla de información
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewInfo_KeyDown(sender As Object, e As KeyEventArgs) Handles INDviewInfo.KeyDown
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

        FlagConfirm = False
        'Se ejecuta el asyncrono
        bwMassivePayrollDatas.RunWorkerAsync()
    End Sub

    ''' <summary>
    ''' Deshace los cambios en el form
    ''' </summary>
    Private Sub Deshacer()
        BarButtonsConfigure()
        CleanControls()
    End Sub

    ''' <summary>
    ''' Configura los botones de la barra botones
    ''' </summary>
    Private Sub BarButtonsConfigure()
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        ' Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
        'Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
    End Sub


    ''' <summary>
    ''' Metodo que importa los items del excel a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
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
            contractPath = openFileDialog1.FileName
            If (contractPath Is Nothing OrElse contractPath.Trim().Equals(String.Empty)) Then 'Si la ruta es vacía
                Mensaje(EeventViewerImages.Advertencia) = "Ruta de archivo vacía"
                AsyncLoader(False)
                Exit Sub
            End If

            'Se obtiene el documento excel
            Dim sddf = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
            sddf.AllowDrop = False
            sddf.LoadDocument(contractPath)
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

        FlagConfirm = False
        'Se ejecuta el asyncrono
        bwMassivePayrollDatas.RunWorkerAsync()
    End Sub


#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu con botón en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteItem()
    End Sub

    ''' <summary>
    ''' Menu desplegable en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteItem()
    End Sub

#End Region

    Private Async Sub DeleteItem()
        If MessageIndigo.Show("Está seguro de eliminar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        If INDviewInfo.GetSelectedRows IsNot Nothing AndAlso INDviewInfo.GetSelectedRows.Length > 0 Then

            If ObjEmployeeScheduleC.Id > 0 And ObjEmployeeScheduleC.Status = 1 Then

                Dim tmpObjDetail = CType(INDviewInfo.GetFocusedRow, SP_ValidateMassiveEmployeeSchedule_Result)

                AsyncLoader(True)
                Using model As New MMassiveContract(Me.Tag)
                    Dim ListaGenerica As New GenericListEmployeeSchedule
                    ListaGenerica.List_ImportFileRow = listRows
                    Dim VarResult = Await model.DeleteEmployeeScheduleDetail(tmpObjDetail.Linea, Nothing, "Consultar", ObjEmployeeScheduleC.Code, ObjEmployeeScheduleC.Description, ObjEmployeeScheduleC.Status, ObjEmployeeScheduleC.Id, 0)

                    If VarResult.StateResult = True Then
                        Await LoadControls()
                    End If

                    AsyncLoader(False)

                    validationResultMessage = validationResult.Where(Function(x) x.Tipo = "Info").ToList()
                    If validationResultMessage.Count > 0 Then
                        If validationResultMessage.Any(Function(x) x.Valor = "1") Then
                            Mensaje(EeventViewerImages.Informacion) = "Se ha Confirmado Correctamente"
                            CleanControls()
                        End If
                    End If
                End Using

            ElseIf ObjEmployeeScheduleC.Id = 0 Then

                Dim tmpObjDetail = CType(INDviewInfo.GetFocusedRow, SP_ValidateMassiveEmployeeSchedule_Result)
                validationResult.Remove(tmpObjDetail)

                Dim ObjListoRow = listRows.Where(Function(x) x.Row(0) = tmpObjDetail.Nit And x.Row(1) = tmpObjDetail.InitialDate And x.Row(2) = tmpObjDetail.InitialHourDate).FirstOrDefault()

                listRows.Remove(ObjListoRow)

                INDgcInformation.DataSource = Nothing

                If validationResult.Any(Function(x) x.Tipo = "Datos") Then
                    INDgcInformation.DataSource = validationResult.Where(Function(x) x.Tipo = "Datos").ToList()
                End If

            End If

        End If
    End Sub

#Region "Click"
    Private Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        ImportFile()
    End Sub
#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Deshacer de la barra botones
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Guarda los saldos iniciales
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        ConfirmNovelties()
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        SaveEmployeeSchedule()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        AnularEmployeeScheduleDetail()
    End Sub

    Private Sub BarraBotones_ClickGuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        ConfirmNovelties()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        SaveEmployeeSchedule()
    End Sub

    Private Sub ConfirmNovelties()

        Try
            AsyncLoader(True)
            If bwMassivePayrollDatas.IsBusy Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = "No puede continuar porque hay un proceso que no ha terminado su ejecución"
                Exit Sub
            End If

            If ValidateControl() = False Then
                Exit Sub
            End If

            Action = "Confirmar"
            Status = 2

            Using model As New MMassiveContract(Me.Tag)
                Dim ListaGenerica As New GenericListEmployeeSchedule
                ListaGenerica.List_ImportFileRow = listRows
                validationResult = model.ValidateMassiveEmployeeSchedule(ListaGenerica, Action, Code, INDTxtDescription.EditValue, Status, ObjEmployeeScheduleC.Id, 0)
                AsyncLoader(False)

                validationResultMessage = validationResult.Where(Function(x) x.Tipo = "Info").ToList()
                If validationResultMessage.Count > 0 Then
                    If validationResultMessage.Any(Function(x) x.Valor = "1") Then
                        Mensaje(EeventViewerImages.Informacion) = "Se ha Confirmado Correctamente"
                        CleanControls()
                    End If
                End If
            End Using


        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try

    End Sub

    Private Sub AnularEmployeeScheduleDetail()

        Try
            AsyncLoader(True)
            If bwMassivePayrollDatas.IsBusy Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = "No puede continuar porque hay un proceso que no ha terminado su ejecución"
                Exit Sub
            End If

            Action = "Confirmar"
            Status = 3

            Using model As New MMassiveContract(Me.Tag)
                Dim ListaGenerica As New GenericListEmployeeSchedule
                ListaGenerica.List_ImportFileRow = listRows
                validationResult = model.ValidateMassiveEmployeeSchedule(ListaGenerica, Action, Code, INDTxtDescription.EditValue, Status, ObjEmployeeScheduleC.Id, 0)
                AsyncLoader(False)

                validationResultMessage = validationResult.Where(Function(x) x.Tipo = "Info").ToList()
                If validationResultMessage.Count > 0 Then
                    If validationResultMessage.Any(Function(x) x.Valor = "1") Then
                        Mensaje(EeventViewerImages.Informacion) = "Se ha Anulado Correctamente"
                        CleanControls()
                    End If
                End If
            End Using


        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try

    End Sub

    Private Sub SaveEmployeeSchedule()
        AsyncLoader(True)
        If bwMassivePayrollDatas.IsBusy Then
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = "No puede continuar porque hay un proceso que no ha terminado su ejecución"
            Exit Sub
        End If

        If ValidateControl() = False Then
            Exit Sub
        End If

        Action = "Confirmar"
        Status = 1

        Using model As New MMassiveContract(Me.Tag)
            Dim ListaGenerica As New GenericListEmployeeSchedule
            ListaGenerica.List_ImportFileRow = listRows
            validationResult = model.ValidateMassiveEmployeeSchedule(ListaGenerica, Action, Code, INDTxtDescription.EditValue, Status, ObjEmployeeScheduleC.Id, 0)
            validationResultMessage = validationResult.Where(Function(x) x.Tipo = "Info").ToList()
            If validationResultMessage.Count > 0 Then
                ListMessages = New List(Of Tuple(Of String, Integer))
                For Each linea As SP_ValidateMassiveEmployeeSchedule_Result In validationResultMessage
                    Dim mensaje As String = String.Format("{0}. {1}. Columna: {2}. Valor: {3}", linea.Linea, linea.Mensaje, linea.Columna, linea.Valor)
                    ListMessages.Add(New Tuple(Of String, Integer)(mensaje, 1))
                Next
            End If
        End Using
        AsyncLoader(False)

        If ListMessages IsNot Nothing AndAlso ListMessages.Count > 0 Then
            Using formulario As New FrmListErrors(ListMessages)
                formulario.StartPosition = FormStartPosition.CenterParent
                formulario.Title = "Confirmacion de importación de novedades."
                Dim transparent As New FrmTransparent(formulario, True)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If

        CleanControls()

    End Sub

    Private Function ValidateControl() As Boolean
        If INDgcInformation.DataSource.count <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha agregado ningún detalle"
            Return False
        End If

        If INDTxtDescription.EditValue = Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha llenado la Descripción"
            Return False
        End If


        Return True
    End Function

#End Region


    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

    Private Sub INDEsbBills_Click(sender As Object, e As EventArgs) Handles INDEsbBills.Click
        For Each item As GridColumn In INDviewInfo.Columns
            If item.Name = "ColCedulaEmpleado" Then
                item.Visible = True
                item.VisibleIndex = 0
            ElseIf item.Name = "ColEmpleado" Then
                item.Visible = False
            ElseIf item.Name = "ColFechaEntrada" Then
                item.VisibleIndex = 1
            ElseIf item.Name = "ColHoraEntrada" Then
                item.VisibleIndex = 2
            ElseIf item.Name = "ColFechaSalida" Then
                item.VisibleIndex = 3
            ElseIf item.Name = "ColHoraSalida" Then
                item.VisibleIndex = 4
            ElseIf item.Name = "ColHours" Then
                item.Visible = False
            ElseIf item.Name = "ColObservaciones" Then
                item.Visible = False
            ElseIf item.Name = "colActions" Then
                item.Visible = False
            End If
        Next
        INDviewInfo.BestFitColumns()
        Dim _gridView = INDviewInfo
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If

        For Each item As GridColumn In INDviewInfo.Columns
            If item.Name = "ColCedulaEmpleado" Then
                item.Visible = False
            ElseIf item.Name = "ColEmpleado" Then
                item.Visible = True
                item.VisibleIndex = 0
            ElseIf item.Name = "ColFechaEntrada" Then
                item.VisibleIndex = 1
            ElseIf item.Name = "ColHoraEntrada" Then
                item.VisibleIndex = 2
            ElseIf item.Name = "ColFechaSalida" Then
                item.VisibleIndex = 3
            ElseIf item.Name = "ColHoraSalida" Then
                item.VisibleIndex = 4
            ElseIf item.Name = "ColHours" Then
                item.VisibleIndex = 5
            ElseIf item.Name = "ColObservaciones" Then
                item.VisibleIndex = 6
            ElseIf item.Name = "colActions" Then
                item.VisibleIndex = 7
            End If
        Next
        INDviewInfo.BestFitColumns()
        INDgcInformation.DataSource = Nothing
        INDgcInformation.RefreshDataSource()
    End Sub

    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(INDbtnCode.EditValue.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await NewScheduleEmployee()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch()
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Descripcion", .FieldName = "Descripcion", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Codigo"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListEmployeeScheduleC
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    Private Async Function NewScheduleEmployee() As Task

        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.PayrollSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PayrollSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.PayrollSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If

        ObjEmployeeScheduleC = New EmployeeScheduleC()
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MMassiveContract(CStr(Me.Tag))
                    AsyncLoader(True)
                    ObjEmployeeScheduleC = Await Model.GetEmployeeScheduleC(INDbtnCode.Text.Trim)

                    LayoutControl1.BeginUpdate()
                    If ObjEmployeeScheduleC IsNot Nothing AndAlso ObjEmployeeScheduleC.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(ObjEmployeeScheduleC.Id))
                            FlagLoad = True

                            With ObjEmployeeScheduleC
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)

                                Code = .Code
                                INDTxtDescription.EditValue = .Description
                                Action = "Consultar"
                                BarraBotones.StatusRecord = .Status.ToString

                                Dim ListaGenerica As New GenericListEmployeeSchedule
                                ListaGenerica.List_ImportFileRow = listRows
                                validationResult = Model.ValidateMassiveEmployeeSchedule(ListaGenerica, Action, Code, INDTxtDescription.EditValue, Status, ObjEmployeeScheduleC.Id, 0)

                                If validationResult IsNot Nothing AndAlso validationResult.Count > 0 Then
                                    If validationResult.Any(Function(x) x.Tipo = "Info") Then
                                        validationResultMessage = validationResult.Where(Function(x) x.Tipo = "Info").ToList()
                                        If validationResultMessage.Count > 0 Then
                                            INDgcInformation.DataSource = validationResultMessage
                                        End If
                                    End If
                                End If
                            End With
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordPayroll With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = ObjEmployeeScheduleC.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            Me.BarraBotones.SetDocuments(ObjEmployeeScheduleC.Id, Me.Tag.ToString(), Nothing, GetType(EmployeeScheduleC).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            If ObjEmployeeScheduleC.Status = 1 Then
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                            ElseIf ObjEmployeeScheduleC.Status = 2 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAnnular)
                                ReadOnlyControls(True)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                ReadOnlyControls(True)
                            End If
                            FlagLoad = False

                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewScheduleEmployee()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    LayoutControl1.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            LayoutControl1.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDTxtDescription.Enabled = value
            INDgcInformation.Enabled = value
            INDEsbBills.Enabled = value
            INDBtnImportFile.Enabled = value

            LayoutControl1.EndUpdate()

        End Set
    End Property

    Private Sub CleanControls()
        LayoutControl1.BeginUpdate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        Code = String.Empty
        INDTxtDescription.EditValue = String.Empty
        INDgcInformation.DataSource = Nothing
        ListSP_ImportFileAgreements_Result = Nothing
        ListErrors = Nothing
        myStream = String.Empty
        rows = Nothing
        listRows = Nothing
        validationResult = Nothing
        ListCopyPaste = Nothing
        RowsPasteToGrid = Nothing
        FlagConfirm = False

        LayoutControl1.EndUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True

        Me.ObjEmployeeScheduleC = New EmployeeScheduleC()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

End Class