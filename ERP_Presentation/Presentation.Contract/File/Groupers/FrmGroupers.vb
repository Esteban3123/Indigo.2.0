Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
Imports DevExpress.Utils.Menu
Imports DevExpress.Xpo
Imports DevExpress.XtraSpreadsheet
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Presentation.Base
Imports Presentation.Contract.MVP
Imports Presentation.Controls

Public Class FrmGroupers

#Region "Builder"

    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmGroupers"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "Properties and Variables"

    Private Const MODULE_NAME As String = "Contract"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Handles the Load event of the FrmOrganizationalStructure control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmOrganizationalStructure_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLyRoot, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue

        INDEsbLoadMassive.AddExcelSheets(New List(Of ExcelSheet) From {
            New ExcelSheet With {
                .Name = "Agrupador",
                .Columns = New List(Of ExcelColumn) From
                {
                    New ExcelColumn With {.Name = "Código Agrupador"},
                    New ExcelColumn With {.Name = "Nombre"},
                    New ExcelColumn With {.Name = "Código Agrupador Padre"},
                    New ExcelColumn With {.Name = "Número Usuarios"},
                    New ExcelColumn With {.Name = "Min."},
                    New ExcelColumn With {.Name = "Max."},
                    New ExcelColumn With {.Name = "CME. Proyectado"},
                    New ExcelColumn With {.Name = "Frecuencia"},
                    New ExcelColumn With {.Name = "Total Contratado"},
                    New ExcelColumn With {.Name = "Unidad de Medida", .Comment = "1 - Diario
2 - Semanal
3 - Mensual
4 - Bimensual
5 - Trimestral
6 - Semestral
7 - Anual"},
                    New ExcelColumn With {.Name = "Advertir a partir de"},
                    New ExcelColumn With {.Name = "Mensaje"},
                    New ExcelColumn With {.Name = "Restringir en rango máximo", .Comment = "0 - No
1 - Si"},
                    New ExcelColumn With {.Name = "Mensaje"}
                }
            },
            New ExcelSheet With {
                .Name = "CUPS",
                .Columns = New List(Of ExcelColumn) From
                {
                     New ExcelColumn With {.Name = "Código Agrupador"},
                     New ExcelColumn With {.Name = "Código CUPS"},
                     New ExcelColumn With {.Name = "Código Descripción"}
                }
            },
            New ExcelSheet With {
                .Name = "Actividades",
                .Columns = New List(Of ExcelColumn) From
                {
                     New ExcelColumn With {.Name = "Código Agrupador"},
                     New ExcelColumn With {.Name = "Código Actividad"}
                }
            }
        })

        LoadStructure()

        Me.BarraBotones.Minimizar(True)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        Me.BarraBotones.OperatingUnitVisible = False
        LoadStatus()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
    End Sub

#End Region

#Region "MenuContext"
    ''' <summary>
    ''' Handles the Click event of the ContexMenuActions control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ContexMenuActions_Click(sender As Object, e As EventArgs)
        Select Case (sender.Tag)
            Case "01" 'Agregar nivel
                OpenFormAddStructure(False)
            Case "02" 'Modificar nivel
                OpenFormAddStructure(True)
            Case "03" 'Eliminar nivel
                OpenFormAddStructure(True)
        End Select
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Handles the Click event of the INDsbAddOrgStruct control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDSbAddNode_Click(sender As Object, e As EventArgs) Handles INDSbAddNode.Click
        Dim grouper As GroupersXpo = CType(INDTlGroupers.GetDataRecordByNode(INDTlGroupers.FocusedNode), GroupersXpo)
        If grouper IsNot Nothing OrElse INDTlGroupers.DataSource Is Nothing OrElse CType(INDTlGroupers.DataSource, XPCollection(Of GroupersXpo)).Count = 0 Then
            'If grouper IsNot Nothing Then
            Using frm As New FrmAddGrouper()
                frm.ViewModeEditHold = True
                If grouper IsNot Nothing Then
                    frm.ParentId = grouper.Id
                End If
                AddHandler frm.RefreshDatasourceStruct, AddressOf LoadStructure
                frm.Size = New System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)
                frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(frm, False)
                transparent.ShowDialog(Me)
            End Using
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione el registro padre"
        End If
    End Sub

#End Region

#Region "PopupMenuShowing"

    ''' <summary>
    ''' Handles the PopupMenuShowing event of the INDtlStruct control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraTreeList.PopupMenuShowingEventArgs"/> instance containing the event data.</param>
    Private Sub INDtlStruct_PopupMenuShowing(sender As Object, e As DevExpress.XtraTreeList.PopupMenuShowingEventArgs) Handles INDTlGroupers.PopupMenuShowing
        If e.Menu Is Nothing Then
            Exit Sub
        End If
        e.Menu.Items.Clear()
        Dim addItem As String = "Agregar Nivel"
        Dim ItemMenuAdd As DXMenuItem = New DXMenuItem(addItem, AddressOf ContexMenuActions_Click)
        ItemMenuAdd.Tag = "01"

        Dim modifyItem As String = "Modificar Nivel"
        Dim ItemMenuModify As DXMenuItem = New DXMenuItem(modifyItem, AddressOf ContexMenuActions_Click)
        ItemMenuModify.Tag = "02"

        Dim deleteItem As String = "Eliminar Nivel"
        Dim ItemMenuDelete As DXMenuItem = New DXMenuItem(deleteItem, AddressOf ContexMenuActions_Click)
        ItemMenuDelete.Tag = "03"
        e.Menu.Items.Add(ItemMenuAdd)
        e.Menu.Items.Add(ItemMenuModify)
        e.Menu.Items.Add(ItemMenuDelete)
    End Sub

#End Region

#Region "Import File"

#Region "propiedades de la importacion"

    ''' <summary>
    ''' items que se van a enviar en cada proceso
    ''' </summary>
    ''' <remarks></remarks>
    Const itemsToSend As Integer = 100

    ''' <summary>
    ''' coleccion de filas que se van a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim workSheetRows As RowCollection

    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()

    ''' <summary>
    ''' listado de eventos validos que se presentaron al importar el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Dim listSucessImportFile As List(Of String)

    ''' <summary>
    ''' listado de eventos erroneos que se presentaron al importar el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Dim listErrorsImportFile As List(Of String)

#End Region

    Private Async Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"

        If openFileDialog1.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        Try
            AsyncLoader(True)
            Dim fileName = openFileDialog1.FileName
            If (fileName IsNot Nothing AndAlso Not fileName.Trim().Equals(String.Empty)) Then
                If MessageIndigo.Show("Desea continuar con el proceso de importación", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    listSucessImportFile = New List(Of String)
                    listErrorsImportFile = New List(Of String)

                    Await Me.LoadImportFile(fileName)

                    If listErrorsImportFile IsNot Nothing AndAlso listErrorsImportFile.Count > 0 Then
                        Mensaje(EeventViewerImages.MensajeError) = "Se ha importado la información con errores"
                        Using formulario As New FrmListErrors(listErrorsImportFile)
                            formulario.StartPosition = FormStartPosition.CenterParent
                            Dim transparent As New FrmTransparent(formulario, False)
                            transparent.SafeInvoke(Sub(f) f.ShowDialog())
                        End Using
                    Else
                        Mensaje(EeventViewerImages.Informacion) = "Se ha importado la información correctamente"
                    End If

                    LoadStructure()
                End If
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' metodo que se encarga de validar el archivo de excel
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LoadImportFile(ByVal fileName As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         Dim sddf = New SpreadsheetControl()
                                         sddf.AllowDrop = False
                                         sddf.LoadDocument(fileName)
                                         Dim workBook As IWorkbook = sddf.Document

                                         If workBook.Worksheets.Count <> 3 Then
                                             listErrorsImportFile.Add("El archivo debe tener tres hojas (Agrupadores - CUPS - Actividades)")
                                             Exit Sub
                                         End If

                                         LoadImportWorkSheet(workBook, eGrouperWorkSheetType.Grouper)
                                         LoadImportWorkSheet(workBook, eGrouperWorkSheetType.CUPS)
                                         LoadImportWorkSheet(workBook, eGrouperWorkSheetType.Activities)
                                     End Sub)
    End Function

    Private Sub LoadImportWorkSheet(ByVal workBook As IWorkbook, ByVal workSheetType As Infrastructure.CrossCutting.Base.eGrouperWorkSheetType)
        workSheetRows = workBook.Worksheets(workSheetType).Rows
        If workSheetRows.LastUsedIndex = 0 Then
            listErrorsImportFile.Add(String.Format("No se encontraron registros a importar en la hoja {0}", workBook.Worksheets(0).Name))
            Exit Sub
        End If

        Dim lastProcess As Boolean = False
        Dim totalProcessedItems = 0
        Dim totalItems = workSheetRows.LastUsedIndex


        Using trasparent = New FrmTransparent(Nothing, False)
            trasparent.SafeInvoke(Sub(f) f.ShowDialog())
            Using model As New MGroupers(Me.Tag.ToString)
                Dim indexSend As Integer = 1
                Dim positionEnd As Integer = 0
                While indexSend <= workSheetRows.LastUsedIndex
                    Try
                        positionEnd = indexSend + itemsToSend - 1
                        listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
                        lastProcess = (positionEnd >= workSheetRows.LastUsedIndex)

                        If Not lastProcess Then
                            SetRow(indexSend, positionEnd + 1)
                        Else
                            SetRow(indexSend, workSheetRows.LastUsedIndex + 1)
                            positionEnd = workSheetRows.LastUsedIndex
                        End If

                        Dim result = model.ImportGroupers(workSheetType, listRows.ToList())
                        If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                            listSucessImportFile.AddRange(result.ObjectEmbbeded)
                        End If

                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                            listErrorsImportFile.AddRange(result.MessageResult)
                        End If
                    Catch ex As Exception
                        listErrorsImportFile.Add(Utils.GetInnerExceptionMessageToString(ex))
                    Finally
                        indexSend = positionEnd + 1
                        totalProcessedItems = positionEnd
                    End Try
                End While
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' metodo para establecer las filas que se van a enviar a procesar
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    ''' <remarks></remarks>
    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  Dim row = New ImportFileRow With {.IndexRow = x, .Row = workSheetRows.Item(x).SpreadsheetRowToList(14)}
                                                  listRows.Add(row)
                                              End SyncLock
                                          End Sub)
    End Sub

#End Region

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' Loads the structure.
    ''' </summary>
    Public Sub LoadStructure()
        Using model As New MGroupers(Me.Tag.ToString())
            Dim _structure As XPCollection(Of GroupersXpo) = model.ListAllGroupersCollection()
            INDTlGroupers.DataSource = _structure
        End Using
        INDTlGroupers.RefreshDataSource()
        INDTlGroupers.ExpandToLevel(1)
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls()
        INDLyRoot.BeginUpdate()
        INDLyRoot.EndUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing

        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
    End Sub

    ''' <summary>
    ''' Método que abre el form de la estructura
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFormAddStructure(modeEdit As Boolean)
        Dim grouper As GroupersXpo = CType(INDTlGroupers.GetDataRecordByNode(INDTlGroupers.FocusedNode), GroupersXpo)
        Using frm As New FrmAddGrouper
            frm.ViewModeEditHold = True
            frm.MinimizeBox = False
            frm.MaximizeBox = False
            frm.ParentId = grouper.Id
            frm.ModeEdit = modeEdit
            frm.Code = grouper.Code
            AddHandler frm.RefreshDatasourceStruct, AddressOf LoadStructure
            frm.Size = New System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)
            frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent As New FrmTransparent(frm, False)
            transparent.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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

#End Region

#Region "BarButton Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

#End Region

End Class