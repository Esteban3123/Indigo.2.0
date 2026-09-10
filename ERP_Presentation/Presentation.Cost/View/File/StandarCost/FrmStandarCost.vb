'***********************************************************************
' Assembly         : Presentation.Cost
' Author           : Juan David Capera
' Created          : 13-12-2023
'
' Last Modified By : Andrés Steven Rojas
' Last Modified On : 31-10-2024
' Description      : Refactorización del formulario
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Cost.MVP
#End Region

Public Class FrmStandarCost
    Implements IAverageStandardCost, ICustomizableForm

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        _presenter = New PAverageStandardCost(Me)
    End Sub

#Region "Bar-Buttons"
    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Clic boton guardar de la barra botones
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Clic boton actualizar de la barra botones
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Clic boton activar-inactivar de la barra botones
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Guardar()
    End Sub
    Private Sub BarraBotones_Click_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.Deshacer()
    End Sub
#End Region

#Region "ICrud"
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

    Public Sub Buscar() Implements ICrudBase.Buscar
        Me.OpenSearch()
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        If Me.details?.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Por favor agrege minimo un registro en la rejilla"
            Exit Sub
        End If
        AsyncLoader(True)
        Try
            AssignValues()
            Using model As New MAverageStandardCost(Me.tagForm)
                Dim result = Await model.SaveAverageStandardCostAsync(Me._standarCost)
                If result Is Nothing OrElse Not result?.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = If(String.IsNullOrEmpty(result?.Message), ResourceManager.GetString("ErrorUnknown"), result?.Message)
                    Exit Sub
                End If

                If result.StateResult Then
                    If _sequenceCost.ChangeTracker.State = ObjectState.Added Then
                        If Not Me.costSequence.IsManual AndAlso Not Me.costSequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                End If

                Mensaje(EeventViewerImages.Informacion) = result.Message
                Deshacer()
            End Using
        Catch ex As Exception
            Deshacer()
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequenceCost?.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        ElseIf Me._sequenceCost.IsManual Then
            If Not String.IsNullOrEmpty(Code.Trim()) Then
                Deshacer()
            Else
                Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
            End If
        Else
            If String.IsNullOrEmpty(Code) Then
                Me.NewStandarCost()
            Else
                Deshacer()
            End If
        End If
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If Not BarraBotones.PermiteConsultar Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {
                                New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                                New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.6},
                                New ColumnInfo() With {.Caption = "Vigencia", .FieldName = "Validity", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2}
                             }.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = eDataSource.ListStandarCost
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub
#End Region

#Region "Properties"
    ''' <summary>
    ''' Obtiene o establece el código 
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements IAverageStandardCost.Code
        Get
            If (INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbteCode.EditValue
            End If
        End Get
        Set(value As String)
            INDbteCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    ''' <returns></returns>
    Private Property AverageStandardCost_Name As String Implements IAverageStandardCost.Name
        Get
            Return INDtxtName.EditValue
        End Get
        Set(value As String)
            INDtxtName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de inicio
    ''' </summary>
    ''' <returns></returns>
    Public Property StartDate As Date Implements IAverageStandardCost.StartDate
        Get
            Return INDdeStartDate.EditValue
        End Get
        Set(value As Date)
            INDdeStartDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de fin
    ''' </summary>
    ''' <returns></returns>
    Public Property EndDate As Date Implements IAverageStandardCost.EndDate
        Get
            Return INDdeEndDate.EditValue
        End Get
        Set(value As Date)
            INDdeEndDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    ''' <returns></returns>
    Public Property Status As Boolean Implements IAverageStandardCost.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estable del dataSouce de la rejilla
    ''' </summary>
    ''' <returns></returns>
    Public Property details As List(Of StandarCostDetails)
        Get
            Return INDgcCostStandarDetail.DataSource
        End Get
        Set(value As List(Of StandarCostDetails))
            INDgcCostStandarDetail.SafeInvoke(Sub()
                                                  INDgcCostStandarDetail.DataSource = value
                                              End Sub)
        End Set
    End Property

    ''' <summary>
    ''' Secuencia de costos
    ''' </summary>
    ''' <returns></returns>
    Public Property costSequence As CostSecuence Implements IAverageStandardCost.costSequence
        Get
            Return _sequenceCost
        End Get
        Set(value As CostSecuence)
            Me._sequenceCost = value
            Me.DicSequense.Clear()
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq In Me._sequenceCost.CostSecuenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' tag del frontal
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property tagForm As String Implements IAverageStandardCost.tagForm
        Get
            Return If(Me.Tag IsNot Nothing, Me.Tag.ToString(), Nothing)
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public Sub ActionsOnControls(value As Boolean)
        INDliCode.Enabled = Not value
        INDbteCode.Enabled = Not value
        INDtxtName.Enabled = value
        INDdeStartDate.Enabled = value
        INDdeEndDate.Enabled = value
        INDsbAddDetail.Enabled = value
        INDBtnImportFile.Enabled = value
        LayoutControlItem3.Enabled = value
        If value Then
            INDtxtName.Focus()
        Else
            INDbteCode.Focus()
        End If
    End Sub

#Region "for indexing"
    ''' <summary>
    ''' Propiedad de solo escritura para establecer un valor al tipo Func
    ''' </summary>
    Protected WriteOnly Property Funct As Func(Of IndexedDocument2)
        Set(value As Func(Of IndexedDocument2))
            Me._funct = value
        End Set
    End Property
#End Region
#End Region

#Region "Fields"
    ''' <summary>
    ''' nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Cost"

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordCost

    ''' <summary>
    ''' Documento para indexar
    ''' </summary>
    Public _doc As IndexedDocument2

    ''' <summary>
    ''' Función para construir el documento de indexación
    ''' </summary>
    Protected _funct As Func(Of IndexedDocument2)

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Protected indigo As SessionValues

    ''' <summary>
    ''' Presentador de la entidad
    ''' </summary>
    Private _presenter As PAverageStandardCost

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Secuencia de costos
    ''' </summary>
    Private _sequenceCost As CostSecuence

    ''' <summary>
    ''' variable de la entidad
    ''' </summary>
    Private _standarCost As StandarCost

    ''' <summary>
    ''' variable de la entidad
    ''' </summary>
    Private _stanCostDetail As StandarCostDetails

    ''' <summary>
    ''' Bandera editar registro
    ''' </summary>
    Private flagEdit As Boolean = False

    ''' <summary>
    ''' Indica la moneda oficial
    ''' </summary>
    Private CurrencyAbbreviation As String
#Region "For import"
    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Private rows As RowCollection

    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Private listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()

    ''' <summary>
    ''' Total items Procesados
    ''' </summary>
    Private totalProcessedItems As Integer

    ''' <summary>
    ''' Total items
    ''' </summary>
    Private totalItems As Integer

    ''' <summary>
    ''' Cantidad de registros mandados a procesar
    ''' </summary>
    Private Const itemsSend As Integer = 700

    ''' <summary>
    ''' Almacena el listado de errores al importar
    ''' </summary>
    Private listErrorsImportFile As List(Of String)

    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Private myStream As String = Nothing
#End Region

#End Region

#Region "Methods"
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Not String.IsNullOrEmpty(Code) Then
            Me.LoadControls()
            ActionsOnControls(True)
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Asignan valores a la entidad
    ''' </summary>
    Private Sub AssignValues()
        If Me._standarCost Is Nothing Then Me._standarCost = New StandarCost()
        With _standarCost
            .Code = Code
            .Validity = StartDate 'Ahora es fecha de inicio
            .EndDate = EndDate
            .Name = AverageStandardCost_Name
            .Status = Status
            .IdSequence = Me._idCurrentSequence
            For Each itemAdd In Me.details.Where(Function(w) w.ChangeTracker.State = ObjectState.Added)
                .StandarCostDetails.Add(itemAdd)
            Next
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina un registro bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using modelCost As New MCommonCost(Me.tagForm)
                Await modelCost.DeleteBlockRecordCost(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Consulta los datos de la entidad
    ''' </summary>
    Private Async Sub LoadControls()
        AsyncLoader(True)
        Try
            If Not String.IsNullOrEmpty(Code) Then
                Using model As New MAverageStandardCost(Me.tagForm)
                    Dim result = Await model.GetAverageStandardCostByCodeAsync(Code)
                    If result?.StateResult AndAlso result?.ObjectEmbbeded?.Id > 0 Then
                        INDlcRoot.BeginUpdate()
                        Me.BarraBotones.StatusRecordVisible = True
                        Me._standarCost = result.ObjectEmbbeded
                        Me.BlockRecord()
                        With Me._standarCost
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Code = .Code
                            AverageStandardCost_Name = .Name
                            StartDate = .Validity 'Validity es Fecha de inicio
                            EndDate = .EndDate
                            Me.details = .StandarCostDetails.ToList()
                        End With
                        INDgcCostStandarDetail.RefreshDataSource()
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                        INDlcRoot.EndUpdate()
                    Else
                        Me.NewStandarCost()
                    End If
                End Using
            End If
        Catch ex As Exception
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Bloquea el registro
    ''' </summary>
    Private Async Sub BlockRecord()
        Using modelCost As New MCommonCost(Me.tagForm)
            _record = Await modelCost.GetBlockRecordCostByIdformAndIdRecord(CStr(Me.tagForm), CStr(Me._standarCost.Id))
            If _record.Id = 0 Then
                _record = (Await modelCost.SaveBlockRecordCost(New BlockRecordCost With {
                                                              .IdForm = Me.tagForm,
                                                              .IdRecord = Me._standarCost.Id,
                                                              .BlockDate = Date.Now(),
                                                              .CodUser = Me.indigo.UserIndigo,
                                                              .NameUser = Me.indigo.UserIndigoName,
                                                              .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added}
                                                              })).ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(BaseClass.obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Agrega las columnas a la estructura del archivo de excel
    ''' </summary>
    Private Sub GenerateExcelStructure()
        INDExportStructure.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Código Actividad", .Type = ExcelColumnType.Text, .Comment = "Digite el código exacto de la actividad"},
                            New ExcelColumn With {.Name = "Valor Activos Fijos"},
                            New ExcelColumn With {.Name = "Valor Nómina"},
                            New ExcelColumn With {.Name = "Valor Inventario"},
                            New ExcelColumn With {.Name = "Valor Costo Adicional"},
                            New ExcelColumn With {.Name = "Observación", .Type = ExcelColumnType.Text}
                            }
                        })
    End Sub

    ''' <summary>
    ''' Carga las acciones de la rejilla
    ''' </summary>
    Private Sub AddListActions()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView11.SetListAcction(INDgvCostStandarDetail, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvCostStandarDetail.Columns.Where(Function(w) w.Name = "colActions")
            col.Width = 100
        Next
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Limpia los objetos del form
    ''' </summary>
    Private Sub CleanControls()
        INDlcgRoot.BeginUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        'Limpio controles
        Code = Nothing
        AverageStandardCost_Name = Nothing
        INDgcCostStandarDetail.DataSource = Nothing
        _standarCost = Nothing
        _stanCostDetail = Nothing
        Me.StartDate = GetDateServer()
        Me.EndDate = GetDateServer()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        ActionsOnControls(False)
        INDlcgRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Prepara los controles para un nuevo registror
    ''' </summary>
    Private Async Sub NewStandarCost()
        If _sequenceCost.IsManual Then
            Me.ActionsOnControls(True)
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If _sequenceCost.Scope.Equals("O") Then
                Me._idCurrentSequence = Me._sequenceCost.CostSecuenceDetail(0).Id
            ElseIf Me._sequenceCost.Scope.Equals("OU") Then
                If Me._sequenceCost.CostSecuenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequenceCost.CostSecuenceDetail.FirstOrDefault(Function(f) f.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Sub
                End If
            End If

            If Me._sequenceCost.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls(True)
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls(True)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MCommonCost(Me.tagForm)
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls(True)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls(True)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Instancia el formulario detallel
    ''' </summary>
    Private Sub EditDetail()
        Me._stanCostDetail = TryCast(INDgvCostStandarDetail.GetFocusedRow, StandarCostDetails)
        If Me._stanCostDetail IsNot Nothing Then
            Me.flagEdit = True
            OpenFormDetail()
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = "No se logro obtener el registro"
        End If
    End Sub

    ''' <summary>
    ''' Elimina un registro de StandarCostDetail
    ''' </summary>
    Private Sub DeleteDetail()
        Dim row = TryCast(INDgvCostStandarDetail.GetFocusedRow, StandarCostDetails)
        If row IsNot Nothing Then
            Dim index As Integer = Me.details.IndexOf(row)
            If Me.details.Item(index).Id > 0 Then
                Me.details.Item(index).ChangeTracker.State = ObjectState.Deleted
                Me.details.RemoveAt(index)
            Else
                Me.details.RemoveAt(index)
            End If
            INDgcCostStandarDetail.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Instancia el formulario detallel
    ''' </summary>
    Private Sub OpenFormDetail()
        Using form As New FrmAddStandarCost()
            form.flagEdit = Me.flagEdit
            form.stanCostDetail = Me._stanCostDetail
            form.CurrencyAbbreviation = CurrencyAbbreviation
            form.Height = Screen.PrimaryScreen.WorkingArea.Height * 0.7
            AddHandler form.AddInfoToGridFormPrincipal, AddressOf SaveSantandardCostDetail
            Dim frm As New FrmTransparent(form, False)
            Me.Cursor = Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Guarda o actualiza los detalles del costo
    ''' </summary>
    ''' <param name="flagEdit"></param>
    ''' <param name="costStandarDetails"></param>
    Private Sub SaveSantandardCostDetail(flagEdit As Boolean, costStandarDetails As List(Of StandarCostDetails))
        If Me.details Is Nothing Then Me.details = New List(Of StandarCostDetails)
        If flagEdit Then
            Dim singleCostStandarDetails = costStandarDetails.FirstOrDefault()
            For Each item In Me.details.Where(Function(w) w.Id = singleCostStandarDetails.Id)
                item.InitialDate = singleCostStandarDetails.InitialDate
                item.FinalDate = singleCostStandarDetails.FinalDate
                item.CostActivityId = singleCostStandarDetails.CostActivityId
                item.Observation = singleCostStandarDetails.Observation
                item.CodeNameActivity = singleCostStandarDetails.CodeNameActivity
            Next
        Else
            For Each item In costStandarDetails
                Me.details.Add(item)
            Next
        End If
        Me.flagEdit = False
        INDgcCostStandarDetail.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Agrega a la lista los items procesados
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(6)})
                                              End SyncLock
                                          End Sub)
    End Sub

    ''' <summary>
    ''' Importa de manera secuencial el archivo en excel
    ''' </summary>
    ''' <returns></returns>
    Private Function loadImportFile() As Task
        Return Task.Factory.StartNew(Sub()
                                         listErrorsImportFile = New List(Of String)
                                         Dim sddf = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
                                         sddf.AllowDrop = False
                                         sddf.LoadDocument(myStream)
                                         Dim workBook As IWorkbook = sddf.Document
                                         rows = workBook.Worksheets(0).Rows
                                         If rows.LastUsedIndex <= 0 Then
                                             Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                                             Exit Sub
                                         End If
                                         totalProcessedItems = 0
                                         totalItems = rows.LastUsedIndex
                                         Dim indexSend As Integer = 0
                                         While (totalItems + 1) > totalProcessedItems
                                             Dim quantityDetailsToProcess = If((totalItems + 1) < (totalProcessedItems + itemsSend), ((totalItems + 1) - totalProcessedItems), itemsSend)
                                             indexSend = totalProcessedItems
                                             totalProcessedItems += quantityDetailsToProcess
                                             SetRow(indexSend + If(indexSend = 0, 1, 0), totalProcessedItems)
                                             Using model As New MAverageStandardCost(Me.tagForm)
                                                 Dim result = model.ImportOrCopyAndPasteDetails(listRows.ToList(), Nothing)
                                                 If Not result?.MessageResult.Any() Then
                                                     If Me.details IsNot Nothing AndAlso Me.details.Count > 0 Then
                                                         For Each item In result.ObjectEmbbeded
                                                             Me.details.Add(item)
                                                         Next
                                                     Else
                                                         Me.details = result.ObjectEmbbeded
                                                     End If
                                                 Else
                                                     listErrorsImportFile.AddRange(result.MessageResult)
                                                 End If
                                             End Using
                                         End While
                                     End Sub)
    End Function

#End Region

#Region "Functions"

#Region "for indexing"
    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._standarCost.Code, Me._standarCost.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._standarCost.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._standarCost.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._standarCost.Code, Me._standarCost.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._standarCost.Code)
        End If
        Return Me._doc
    End Function
#End Region

#End Region

#Region "Handles Events"

#Region "Load"
    ''' <summary>
    ''' Evento load del frontal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmStandarCost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me.GenerateExcelStructure()
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        _presenter.getSequence()
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Me.AddListActions()
        Me.LoadStatus()
        Me.Deshacer()
        CurrencyAbbreviation = _presenter.GetOfficialCurrencyFromCompanySettings().OfficialCurrency.Abbreviation
        SetCurrencyFormat(CurrencyAbbreviation)
    End Sub

    ''' <summary>
    ''' Establece el formato de moneda en los controles del formulario.
    ''' </summary>
    ''' <param name="_currencyAbbreviation">Abreviación de la moneda.</param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        changeNumericFormatByCurrency(numberFormat)
        INDColStandarCost = Window.Utils.FormatGrid(INDColStandarCost, _currencyAbbreviation)
    End Sub

    ''' <summary>
    ''' Se dispara cuando el frontal esta siendo eliminado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Me.indigo = Nothing
        Me._presenter = Nothing
        Me._sequenceCost = Nothing
        Me.FormSearchObjects = Nothing
    End Sub
#End Region

#Region "ClickButtton"
    Private Sub IndigoGridView11_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView11.ContexMenuActions, IndigoGridView11.Click_ButtonAction
        Select Case (sender.Tag)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    Private Sub INDsbAddDetail_Click(sender As Object, e As EventArgs) Handles INDsbAddDetail.Click
        OpenFormDetail()
    End Sub
#End Region

#Region "key"
    Private Sub INDbteCode_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequenceCost Is Nothing OrElse _sequenceCost.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequenceCost.IsManual Then
                If Not String.IsNullOrEmpty(Code?.Trim()) Then
                    Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Me.NewStandarCost()
                Else
                    Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = Windows.Forms.Keys.F4 Then
            Me.OpenSearch()
        End If
    End Sub
#End Region

#Region "Import And Copy&Paste"

    ''' <summary>
    ''' Evento que se dispara al pegar en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub IndigoGridControl11_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl11.PasteToGrid
        Try
            AsyncLoader(True)
            Using model As New MAverageStandardCost(Me.tagForm)
                Dim result = Await model.ImportOrCopyAndPasteDetailsAsync(Nothing, e.Rows)
                If Not result.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                If Me.details IsNot Nothing AndAlso Me.details.Count > 0 Then
                    For Each item In result.ObjectEmbbeded
                        Me.details.Add(item)
                    Next
                Else
                    Me.details = result.ObjectEmbbeded
                End If
                If result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
                Me.Cursor = Cursors.Default
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' clic boton importar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                'obtengo la rura del archivo
                myStream = openFileDialog1.FileName
                If (myStream IsNot Nothing AndAlso Not myStream.Trim().Equals(String.Empty)) Then
                    Await Me.loadImportFile()
                    If listErrorsImportFile?.Any Then
                        Using formulario As New FrmListErrors(listErrorsImportFile)
                            formulario.StartPosition = FormStartPosition.CenterParent
                            Dim transparent As New FrmTransparent(formulario, False)
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            transparent.ShowDialog(Me)
                        End Using
                    End If
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                End If
                AsyncLoader(False)
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
                AsyncLoader(False)
            End Try
        Else
            AsyncLoader(False)
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Método que asigna el valor mínimo de la fecha final acorde a la inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDdeStartDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdeStartDate.EditValueChanged
        INDdeEndDate.Properties.MinValue = StartDate
    End Sub
#End Region

#End Region

End Class