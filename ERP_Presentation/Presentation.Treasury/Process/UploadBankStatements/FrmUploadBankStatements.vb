'***********************************************************************
' Assembly         : Presentation.Treasury
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 12-04-2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Treasury.MVP
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo
Imports DevExpress.Spreadsheet
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports DevExpress.Data.Linq
Imports Newtonsoft.Json
Imports DevExpress.XtraSpreadsheet
Imports System.Text
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP

#End Region

Public Class FrmUploadBankStatements
    Implements IUploadBankStatements, ICustomizableForm

#Region "Globals"
    Private presenter As PUploadBankStatements

    Private movimientosContainer As New BankMovementsContainer

    Private BankMovements As New List(Of BankMovements)

    Private uploadBankStatements As UploadBankStatements

    Private listUploadBankStatementsDelete As List(Of UploadBankStatementsDetail)

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Treasury"
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordTreasury
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues

    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Private rows As RowCollection

    ''' <summary>
    ''' ColAction del gridview
    ''' </summary>
    Private _colActions As GridColumn

    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Private listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()
    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Private myStream As String = Nothing

    ''' <summary>
    ''' Año del cargue de extracto
    ''' </summary>
    Property Year As Integer

    ''' <summary>
    ''' Mes del cargue de extracto
    ''' </summary>
    Property Month As Integer

    Private UploadBankStatementsDetailEdit As UploadBankStatementsDetail

    ''' <summary>
    ''' Lista de los conceptos de conciliación asociados al banco
    ''' </summary>
    Private ListConciliationConcepts As List(Of BankConciliationConcepts) = New List(Of BankConciliationConcepts)

    ''' <summary>
    ''' Lista de detalles del extracto Bancario
    ''' </summary>
    Private UploadBankStatementDetails As List(Of UploadBankStatementsDetail) = New List(Of UploadBankStatementsDetail)

    ''' <summary>
    ''' Variable que almacena la Cuenta Bancaria
    ''' </summary>
    Private _bank As EntityBankAccounts
#End Region

#Region "Properties"

    ''' <summary>
    ''' Lista con el DataSource de los detalles del extracto bancario
    ''' </summary>
    ''' <returns></returns>
    Private Property _listUploadBankStatements As List(Of UploadBankStatementsDetail)
        Get
            Return CType(INDGcBankStatements.DataSource, List(Of UploadBankStatementsDetail))
        End Get
        Set(value As List(Of UploadBankStatementsDetail))
            INDGcBankStatements.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Código de la transacción del detalle de extracto bancario
    ''' </summary>
    ''' <returns></returns>
    Public Property TransactionCode As String
        Get
            Return INDtxtTransactionCode.EditValue
        End Get
        Set(value As String)
            INDtxtTransactionCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequence As TreasurySequence Implements IUploadBankStatements.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As TreasurySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.TreasurySequenceDetail In Me._sequence.TreasurySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements IUploadBankStatements.ActionsOnControls
        Set(value As Boolean)
            INDBteCode.Enabled = Not value

            INDGcBankStatements.Enabled = value
            INDsleEntityAccount.Enabled = value
            INDEsbBankStatementsDetail.Enabled = value
            INDCdnPeriod.Enabled = value
            BarraBotones.StatusRecordVisible = value
            INDBtnImportFileBankStatementsDetail.Enabled = value
            INDBtnImportFileBankStatementsDetailAPI.Enabled = value
            INDLciInitialBalance.Enabled = value
            If Not value Then
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Código del cargue de extracto bancario
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements IUploadBankStatements.Code
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Indica los tipos de documentos de tesorería que crean movimiento en el banco
    ''' </summary>
    ''' <returns></returns>
    Public Property DocumentType As Integer?
        Get
            Return INDsleDocument.EditValue
        End Get
        Set(value As Integer?)
            INDsleDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Saldo inicial del extracto
    ''' </summary>
    ''' <returns></returns>
    Public Property InitialBalance As Decimal
        Get
            Return INDTxtInitialBalance.EditValue
        End Get
        Set(value As Decimal)
            INDTxtInitialBalance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Saldo final del extracto
    ''' </summary>
    ''' <returns></returns>
    Public Property EndingBalance As Decimal
        Get
            Return INDTxtEndingBalance.EditValue
        End Get
        Set(value As Decimal)
            INDTxtEndingBalance.EditValue = value
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IUploadBankStatements.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IUploadBankStatements.MyTag
        Get
            Return Me.Tag
        End Get
    End Property


#End Region

#Region "DataSources"
    ''' <summary>
    ''' Obtiene o establece el datasource de la cuentas bancarias
    ''' </summary>  
    Public Property BankAccountDatasource As LinqInstantFeedbackSource Implements IUploadBankStatements.BankAccountDatasource
        Get
            Return CType(INDsleEntityAccount.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleEntityAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la Cuenta Bancaria
    ''' </summary>    
    Public Property IdEntityBankAccount As Integer? Implements IUploadBankStatements.IdEntityBankAccount
        Get
            If String.IsNullOrEmpty(INDsleEntityAccount.EditValue) Then
                Return Nothing
            Else
                Return INDsleEntityAccount.EditValue
            End If
        End Get
        Set(value As Integer?)
            INDsleEntityAccount.EditValue = value
        End Set
    End Property

#End Region

#Region "CRUD"
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar
        If uploadBankStatements IsNot Nothing AndAlso uploadBankStatements.Status < 3 Then
            If Not ValidateControls() Then
                Exit Sub
            End If
            If INDGvBankStatements.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar minimo un extracto bancario"
                Exit Sub
            End If
            AssigningValues()
        End If
        Execute()
    End Sub

    ''' <summary>
    ''' metodo para ejecutar el proceso de guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub Execute()
        Try
            Using model As New MUploadBankStatements(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveUploadBankStatements(uploadBankStatements)
                Select Case result.StatusCode
                    Case eStatusResult.SUCCESS
                        If uploadBankStatements.ChangeTracker.State = ObjectState.Added Then
                            If Not Me._sequence.Sequential Then
                                Me.DicSequense(_idCurrentSequence).RemoveAt(0)
                            End If
                            Mensaje(EeventViewerImages.Informacion) = result.Message
                        Else
                            If uploadBankStatements.Status = 3 Then
                                Mensaje(EeventViewerImages.Informacion) = result.Message
                            Else
                                Mensaje(EeventViewerImages.Informacion) = result.Message
                            End If
                        End If
                        AsyncLoader(False)
                        Me.Deshacer()
                    Case eStatusResult.WARNING
                        AsyncLoader(False)
                        INDBteCode.Enabled = False
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Case eStatusResult.EXCEPTION
                        AsyncLoader(False)
                        INDBteCode.Enabled = False
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                End Select
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewUploadBankStatements()
        End If
    End Sub

    Private Sub SaveAndConfirm()
        If Not ValidateControls() Then
            Exit Sub
        End If
        If INDGvBankStatements.RowCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar minimo un extracto bancario"
            Exit Sub
        End If
        'Se limpia la lista que valida los detalles del extracto
        UploadBankStatementDetails.Clear()
        UploadBankStatementDetails = INDGcBankStatements.DataSource 'Se asignan los items del Datasource
        If UploadBankStatementDetails.Exists(Function(x) x.DocumentType Is Nothing) Then
            Mensaje(EeventViewerImages.Advertencia) = "Todos los detalles deben tener asignado un Tipo Documento"
            Exit Sub
        End If
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            AssigningValues()
            uploadBankStatements.Status = 2
            Execute()
        End If
    End Sub
#End Region

#Region "Methods"
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        Dim listItemsColumnEditStatus As New List(Of Tuple(Of String, Byte))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Registrado", 1))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Confirmado", 2))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Anulado", 3))

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Banco", .FieldName = "EntityBankAccountId.CodeBankName", .ColumnWidth = 200},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = 200, .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEditStatus},
                              New ColumnInfo With {.Caption = "Periodo/Mes", .FieldName = "Period", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListUploadBankStatements
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
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub



    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function NewUploadBankStatements() As Task
        uploadBankStatements = New UploadBankStatements()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.TreasurySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.TreasurySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
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
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
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
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = "1"

    End Function

    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me.uploadBankStatements.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordTreasury With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Me.uploadBankStatements.Id}
                Dim operation = Await model.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    Private Sub CleanControls()
        INDLcMain.BeginUpdate()
        ReadOnlyControls(False)
        INDBteCode.EditValue = Nothing
        INDsleEntityAccount.EditValue = Nothing
        Me.movimientosContainer = New BankMovementsContainer
        Me.BankMovements = New List(Of BankMovements)
        Me.listUploadBankStatementsDelete = New List(Of UploadBankStatementsDetail)
        INDsleEntityAccount.Properties.NullText = String.Empty
        INDCdnPeriod.SetMonth = Date.Now.Month
        INDCdnPeriod.SetYear = Date.Now.Year
        IdEntityBankAccount = Nothing
        INDGcBankStatements.DataSource = Nothing
        uploadBankStatements = Nothing
        _listUploadBankStatements = Nothing
        UploadBankStatementsDetailEdit = Nothing
        movimientosContainer = Nothing
        INDpceAddDetail.Enabled = False

        InitialBalance = 0
        EndingBalance = 0

        ActionsOnControls = False
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
        ShowColActionsGrid(True)
        Me.BarraBotones.ReassignOperatingUnit()
        INDLcMain.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MUploadBankStatements(CStr(Me.Tag))
                    AsyncLoader(True)
                    uploadBankStatements = Await Model.GetUploadBankStatementsByCode(INDBteCode.Text.Trim)
                    INDLcMain.BeginUpdate()
                    If uploadBankStatements IsNot Nothing AndAlso uploadBankStatements.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(uploadBankStatements.Id))
                            _listUploadBankStatements = Model.GetUploadBankStatementsDetail(uploadBankStatements.Id)
                            With uploadBankStatements
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.StatusRecordVisible = True
                                BarraBotones.OperatingUnitValue = .OperativeUnit

                                Code = .Code

                                IdEntityBankAccount = .EntityBankAccountId

                                INDCdnPeriod.SetYear = .Year
                                INDCdnPeriod.SetMonth = .Month

                                InitialBalance = .InitialBalance
                                EndingBalance = .EndingBalance

                                INDpceAddDetail.Enabled = True
                                BarraBotones.StatusRecord = .Status.ToString()

                                INDGcBankStatements.RefreshDataSource()
                                CalculatedEndingBalance()
                            End With
                            ActivateButtons(False) 'No se pueden agregar detalles 
                            ShowColActionsGrid(False) 'Ocultamos la columna Acciones
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.uploadBankStatements.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = uploadBankStatements.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(uploadBankStatements.Id, Me.Tag.ToString(), Nothing, GetType(UploadBankStatements).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            Select Case uploadBankStatements.Status
                                Case 1
                                    BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmAnnular)
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                                Case 2
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                    ReadOnlyControls(True)
                                    ActivateUnconfirmButton()
                                Case Else
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                    ReadOnlyControls(True)
                            End Select
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewUploadBankStatements()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLcMain.EndUpdate()
                End Using
            Catch ex As Exception
                INDBteCode.Enabled = False
                Me.Mensaje(EeventViewerImages.MensajeError) = ex.Message
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Function
    ''' <summary>
    ''' Método que se encarga de habilitar o deshabilitar el botón de deconfirmar
    ''' </summary>
    Private Async Sub ActivateUnconfirmButton()
        If IdEntityBankAccount IsNot Nothing Then
            Using model As New MUploadBankStatements(CStr(Me.Tag))
                Dim res = Await model.GetConciliationBankByEntityBankAccount(IdEntityBankAccount)
                If res Is Nothing Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = False
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Establece el formato moneda en los controles del formulario
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat

        Me.INDTxtInitialBalance.Properties.Mask.Culture = _culture
        Me.INDTxtEndingBalance.Properties.Mask.Culture = _culture
        Me.INDtxtDebitValue.Properties.Mask.Culture = _culture
        Me.INDtxtCreditValue.Properties.Mask.Culture = _culture

        Me.GridColumn5 = Window.Utils.FormatGrid(GridColumn5, _currencyAbbreviation)
        Me.GridColumn6 = Window.Utils.FormatGrid(GridColumn6, _currencyAbbreviation)
    End Sub

    Private Sub AssigningValues()
        With uploadBankStatements
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Year = INDCdnPeriod.GetYear
            .EntityBankAccountId = IdEntityBankAccount
            .Month = INDCdnPeriod.GetMonth
            .OperativeUnit = BarraBotones.OperatingUnitValue
            .InitialBalance = InitialBalance
            .EndingBalance = EndingBalance

            For Each item In _listUploadBankStatements
                .UploadBankStatementsDetail.Add(item)
            Next

            If listUploadBankStatementsDelete IsNot Nothing AndAlso listUploadBankStatementsDelete.Count > 0 Then
                listUploadBankStatementsDelete.ForEach(Sub(x) .UploadBankStatementsDetail.Add(x.MarkAsDeleted()))
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(9)})
                                              End SyncLock
                                          End Sub)
    End Sub

    ''' <summary>
    ''' Clean Bank movements details
    ''' </summary>
    Private Sub CleanBankMovementDetail()
        UploadBankStatementsDetailEdit = Nothing
        INDtseTransactionDate.EditValue = Nothing
        DocumentType = Nothing
        TransactionCode = Nothing
        INDtxtBankConsecutive.EditValue = Nothing
        INDtxtTransactionDescription.EditValue = Nothing
        INDtxtDebitValue.EditValue = 0
        INDtxtCreditValue.EditValue = 0
        INDtxtReference1.EditValue = Nothing
        INDtxtReference2.EditValue = Nothing
        INDtxtBankCheck.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Add Bank movements details
    ''' </summary>
    Private Sub AddBankMovementsDetails(ByVal _BankMovementDetail As UploadBankStatementsDetail,
                                       ByVal TransactionDate As Date,
                                        ByVal TransactionCode As String,
                                        ByVal BankConsecutive As String,
                                        ByVal TransactionDescription As String,
                                        Optional ByVal Document As Integer? = Nothing,
                                        Optional ByVal DebitValue As Decimal? = 0,
                                        Optional ByVal CreditValue As Decimal? = 0,
                                        Optional ByVal Reference1 As String = Nothing,
                                        Optional ByVal Reference2 As String = Nothing,
                                        Optional ByVal BankCheck As Long? = Nothing
                                    )

        With _BankMovementDetail
            .TransactionDate = TransactionDate
            .DocumentType = Document
            .TransactionCode = TransactionCode
            .ConsecutiveBank = BankConsecutive
            .DescriptionTransaction = TransactionDescription
            .ValueDebit = DebitValue
            .ValueCredit = CreditValue
            .PaymentReferenceOne = Reference1
            .PaymentReferenceTwo = Reference2
            .BankCheck = BankCheck

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With

        If UploadBankStatementsDetailEdit Is Nothing Then 'Nuevo
            _listUploadBankStatements.Add(_BankMovementDetail)
        End If

        MatchConciliationConcepts(_listUploadBankStatements)
        CleanBankMovementDetail()
    End Sub

    ''' <summary>
    ''' Asigna el DataSource después de buscar el Tipo Documento acorde a los conceptos del banco
    ''' </summary>
    ''' <param name="listUploadBankStatements"></param>
    Private Async Sub MatchConciliationConcepts(ByVal listUploadBankStatements As List(Of UploadBankStatementsDetail))
        Using Model As New MUploadBankStatements(MyTag)
            Dim res = Await Model.MatchConciliationConceptsWithDescriptionTransaction(ListConciliationConcepts, listUploadBankStatements)
            If res.StateResult Then
                INDGcBankStatements.DataSource = res.ObjectEmbbeded
                INDGcBankStatements.RefreshDataSource()
                CalculatedEndingBalance()
            Else
                Mensaje(EeventViewerImages.MensajeError) = res.Message
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Validate controls to add Bank movements details
    ''' </summary>
    Private Function ValidateControlsAddBankMovements() As Boolean
        Dim errorList As New StringBuilder()

        If INDtseTransactionDate.EditValue Is Nothing Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Fecha"))
        End If

        If String.IsNullOrEmpty(TransactionCode) Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Código de la transacción"))
        End If

        If String.IsNullOrEmpty(INDtxtTransactionDescription.EditValue) Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Descripción de la transacción"))
        End If

        If CType(INDtxtDebitValue.EditValue, Decimal) = 0 AndAlso CType(INDtxtCreditValue.EditValue, Decimal) = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Valores débito o crédito"))
        End If

        If CType(INDtxtDebitValue.EditValue, Decimal) <> 0 AndAlso CType(INDtxtCreditValue.EditValue, Decimal) <> 0 Then
            errorList.AppendLine("El registro no debe ser débito y crédito a la vez")
        End If

        If _listUploadBankStatements Is Nothing Then
            _listUploadBankStatements = New List(Of UploadBankStatementsDetail)
        End If

        If UploadBankStatementsDetailEdit Is Nothing Then
            Dim detail = _listUploadBankStatements.Where(Function(x) x.TransactionCode = TransactionCode).FirstOrDefault()
            If detail IsNot Nothing Then
                errorList.AppendLine("El extracto " & detail.TransactionCode & " ya esta agregado")
            End If
        End If

        If errorList.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Edit bank movements details
    ''' </summary>
    Private Sub EditBankMovementDetail()
        UploadBankStatementsDetailEdit = TryCast(INDGvBankStatements.GetFocusedRow, UploadBankStatementsDetail)
        With UploadBankStatementsDetailEdit
            INDtseTransactionDate.EditValue = .TransactionDate
            DocumentType = .DocumentType
            TransactionCode = .TransactionCode
            INDtxtBankConsecutive.EditValue = .ConsecutiveBank
            INDtxtTransactionDescription.EditValue = .DescriptionTransaction
            INDtxtDebitValue.EditValue = .ValueDebit
            INDtxtCreditValue.EditValue = .ValueCredit
            INDtxtReference1.EditValue = .PaymentReferenceOne
            INDtxtReference2.EditValue = .PaymentReferenceTwo
            INDtxtBankCheck.EditValue = .BankCheck
        End With
        INDpceAddDetail.ShowPopup()
    End Sub


    ''' <summary>
    ''' Remove bank movements details
    ''' </summary>
    Private Sub RemoveBankMovementDetail()
        If Not _listUploadBankStatements.Where(Function(d) d.SelectOption).Any() Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un registro a eliminar"
            Exit Sub
        End If

        If MessageIndigo.Show(String.Format("¿Desea eliminar los registros seleccionados ({0})?", _listUploadBankStatements.Where(Function(d) d.SelectOption).Count()), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If listUploadBankStatementsDelete Is Nothing Then
                listUploadBankStatementsDelete = New List(Of UploadBankStatementsDetail)
            End If

            While _listUploadBankStatements.Where(Function(d) d.SelectOption).Any()
                Dim detail = _listUploadBankStatements.Where(Function(d) d.SelectOption).FirstOrDefault()
                If detail.Id > 0 Then
                    detail.MarkAsDeleted()
                    listUploadBankStatementsDelete.Add(detail)
                End If

                _listUploadBankStatements.Remove(detail)
            End While

            INDGcBankStatements.RefreshDataSource()
            CalculatedEndingBalance()
        End If
    End Sub
#End Region

#Region "HANDLES"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        presenter = Nothing
        uploadBankStatements = Nothing
        _listUploadBankStatements = Nothing
        listUploadBankStatementsDelete = Nothing
        UploadBankStatementsDetailEdit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
        rows = Nothing
        listRows = Nothing
        myStream = Nothing
        _colActions = Nothing
    End Sub

    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.uploadBankStatements IsNot Nothing AndAlso Me.uploadBankStatements.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    Private Sub Frm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#Region "Load"
    Private Sub FrmUploadBankStatements_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDEsbBankStatementsDetail.AddRangeColumns("Fecha Transacción", "Consecutivo Banco", "Código Transacción", " Descripción Transacción", " Valor Debito", " Valor Credito", " Cheque", "Referencia Pago 1", " Referencia Pago 2")
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        _indigoSession = SessionValues.Instance
        presenter = New PUploadBankStatements(Me)
        presenter.GetSequense()
        presenter.InitializeEntityBankAccount()
        BarraBotones.OperatingUnitVisible = True
        IndigoGridView1.SetListAcction(INDGvBankStatements, New List(Of eAcciones)() From {{eAcciones.Edit}, {eAcciones.Remove}})
        LoadDocumentTypeDataSource()
        Deshacer()
        LoadStatus()
        SetActionsGrid()
    End Sub

    ''' <summary>
    ''' Proporciona las opciones de Tipo de Documento
    ''' </summary>
    Private Sub LoadDocumentTypeDataSource()
        Dim fillingDocument As New List(Of ViewDocumentType)
        fillingDocument.Add(New ViewDocumentType With {.Id = 1, .Name = "Recibo de caja"})
        fillingDocument.Add(New ViewDocumentType With {.Id = 2, .Name = "Comprobante de egreso"})
        fillingDocument.Add(New ViewDocumentType With {.Id = 3, .Name = "Notas"})
        fillingDocument.Add(New ViewDocumentType With {.Id = 4, .Name = "Consignaciones"})

        INDsleDocument.Properties.DataSource = fillingDocument
        INDRepDocument.DataSource = fillingDocument
    End Sub

    ''' <summary>
    ''' Close PopUp add bank movements details
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAddDetail_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceAddDetail.CloseUp
        If UploadBankStatementsDetailEdit IsNot Nothing Then
            CleanBankMovementDetail()
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDsleEntityAccount_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntityAccount.QueryPopUp
        If INDsleEntityAccount.Properties.DataSource Is Nothing Then
            presenter.InitializeEntityBankAccount()
        End If
    End Sub
#End Region


#Region "KeyDown"
    Private Async Sub INDBteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewUploadBankStatements()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region

#Region "Click"
    Private Sub INDsbAddProductionCenter_Click(sender As Object, e As EventArgs) Handles INDsbAddProductionCenter.Click
        If ValidateControlsAddBankMovements() Then
            Dim BankMovementDetail = UploadBankStatementsDetailEdit
            Dim DebitValue As Decimal? = 0
            Dim CreditValue As Decimal? = 0
            Dim BankCheck As Long? = Nothing

            If BankMovementDetail Is Nothing Then
                BankMovementDetail = New UploadBankStatementsDetail
            End If
            If INDtxtDebitValue.EditValue IsNot Nothing Then
                Decimal.TryParse(INDtxtDebitValue.EditValue, DebitValue)
            End If
            If INDtxtCreditValue.EditValue IsNot Nothing Then
                Decimal.TryParse(INDtxtCreditValue.EditValue, CreditValue)
            End If
            If INDtxtBankCheck.EditValue IsNot Nothing Then
                BankCheck = 0
                Long.TryParse(INDtxtBankCheck.EditValue, BankCheck)
            End If

            AddBankMovementsDetails(
                BankMovementDetail,
                INDtseTransactionDate.EditValue,
                TransactionCode,
                INDtxtBankConsecutive.EditValue,
                INDtxtTransactionDescription.EditValue,
                DocumentType,
                DebitValue,
                CreditValue,
                INDtxtReference1.EditValue,
                INDtxtReference2.EditValue,
                BankCheck
                )
        End If
    End Sub
#End Region


#Region "ButtonClick"
    Private Sub INDBteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        OpenSearch()
    End Sub
#End Region

#Region "MenuActions"
    ''' <summary>
    ''' Agrega las acciones respectivas a la Columna Acciones
    ''' </summary>
    Private Sub SetActionsGrid()
        Dim _listActions As New List(Of eAcciones)()
        _listActions.Add(eAcciones.Edit)
        _listActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvBankStatements, _listActions)
        For Each col As GridColumn In INDGvBankStatements.Columns
            If col.Name = "colActions" Then
                _colActions = col
                col.Width = 80
                col.OptionsColumn.FixedWidth = True
            End If
        Next
    End Sub

    ''' <summary>
    ''' Muestra u oculta la columna Acciones
    ''' </summary>
    ''' <param name="showColumn"></param>
    Private Sub ShowColActionsGrid(ByVal showColumn As Boolean)
        For Each col As GridColumn In INDGvBankStatements.Columns
            If col.Name = "colActions" Then
                col.Visible = showColumn
            End If
        Next
    End Sub

    ''' <summary>
    ''' Ejecuta los métodos de la columna Acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Edit"
                EditBankMovementDetail()
            Case "Remove"
                RemoveBankMovementDetail()
        End Select
    End Sub
#End Region

#Region "PasteToGrid"
    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If uploadBankStatements.Id > 0 AndAlso uploadBankStatements.Status > 1 Then
            Exit Sub
        End If
        AsyncLoader(True)
        Using model As New MUploadBankStatements(MyTag)
            Dim result = Await model.SetCopyPasteOrImportFileUploadBankStatementsDetail(Nothing, e.Rows)

            If result.StatusCode = eStatusResult.EXCEPTION Then

                If result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
                Me.Cursor = System.Windows.Forms.Cursors.Default
                AsyncLoader(False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If

            Dim listErrors = New List(Of String)
            If _listUploadBankStatements Is Nothing Then
                _listUploadBankStatements = New List(Of UploadBankStatementsDetail)
            End If
            For Each item In result.ObjectEmbbeded
                Dim detail = _listUploadBankStatements.Where(Function(x) x.TransactionCode = item.TransactionCode).FirstOrDefault()
                If detail Is Nothing Then
                    _listUploadBankStatements.Add(item)
                Else
                    result.MessageResult.Add("El extracto " & detail.TransactionCode & " ya esta agregado")
                End If
            Next

            INDGcBankStatements.DataSource = _listUploadBankStatements
            INDGcBankStatements.RefreshDataSource()
            CalculatedEndingBalance()
            If result.MessageResult.Count > 0 Then
                Using formulario As New FrmListErrors(result.MessageResult)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Using
        AsyncLoader(False)
    End Sub
#End Region

#Region "ImportFile"
    Private Async Sub INDBtnImportFileBankStatementsDetail_Click(sender As Object, e As EventArgs) Handles INDBtnImportFileBankStatementsDetail.Click
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
                    Dim sddf = New SpreadsheetControl()
                    sddf.AllowDrop = False
                    sddf.LoadDocument(myStream)
                    Dim workBook As IWorkbook = sddf.Document

                    rows = workBook.Worksheets(0).Rows
                    If rows.LastUsedIndex = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                        AsyncLoader(False)
                        Exit Sub
                    End If
                    Using model As New MUploadBankStatements(MyTag)
                        listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()

                        SetRow(1, rows.LastUsedIndex + 1)

                        Dim result = Await model.SetCopyPasteOrImportFileUploadBankStatementsDetail(listRows.ToList(), Nothing)

                        If result.StatusCode = eStatusResult.EXCEPTION Then
                            If result.MessageResult.Count > 0 Then
                                Using formulario As New FrmListErrors(result.MessageResult)
                                    formulario.StartPosition = FormStartPosition.CenterParent
                                    Dim transparent As New FrmTransparent(formulario, False)
                                    Me.Cursor = System.Windows.Forms.Cursors.Default
                                    transparent.ShowDialog(Me)
                                End Using
                            End If
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            AsyncLoader(False)
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            Exit Sub
                        End If

                        Dim listErrors = New List(Of String)
                        If _listUploadBankStatements Is Nothing Then
                            _listUploadBankStatements = New List(Of UploadBankStatementsDetail)
                        End If
                        For Each item In result.ObjectEmbbeded
                            Dim detail = _listUploadBankStatements.Where(Function(x) x.TransactionCode = item.TransactionCode).FirstOrDefault()
                            If detail Is Nothing Then
                                _listUploadBankStatements.Add(item)
                            Else
                                result.MessageResult.Add("El extracto " & detail.TransactionCode & " ya está agregado")
                            End If
                        Next

                        MatchConciliationConcepts(_listUploadBankStatements)

                        If result.MessageResult.Count > 0 Then
                            Using formulario As New FrmListErrors(result.MessageResult)
                                formulario.StartPosition = FormStartPosition.CenterParent
                                Dim transparent As New FrmTransparent(formulario, False)
                                Me.Cursor = System.Windows.Forms.Cursors.Default
                                transparent.ShowDialog(Me)
                            End Using
                        End If
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                    End Using
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

    Private Async Sub INDBtnImportFileBankStatementsDetailAPI_Click(sender As Object, e As EventArgs) Handles INDBtnImportFileBankStatementsDetailAPI.Click

        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx|PDF Files (*.pdf)|*.pdf|Text Files (*.txt)|*.txt"
        openFileDialog1.FilterIndex = 3
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                'obtengo la rura del archivo
                myStream = openFileDialog1.FileName
                If (myStream IsNot Nothing AndAlso Not myStream.Trim().Equals(String.Empty)) Then

                    Using model As New MUploadBankStatements(MyTag)
                        Dim DetailExtractBankAPI = Await model.ExtractBankStatementDetail(myStream, INDCdnPeriod.GetYear)

                        If DetailExtractBankAPI Is Nothing OrElse DetailExtractBankAPI.StateResult = False Then
                            Throw New Exception(DetailExtractBankAPI?.Message)
                        End If

                        If _listUploadBankStatements Is Nothing Then
                            _listUploadBankStatements = New List(Of UploadBankStatementsDetail)
                        Else
                            For Each item In _listUploadBankStatements
                                item.MarkAsDeleted
                            Next
                        End If
                        movimientosContainer = JsonConvert.DeserializeObject(Of BankMovementsContainer)(DetailExtractBankAPI.Message)
                        For Each movements As BankMovements In movimientosContainer.BankMovements
                            ' Crear una nueva instancia de UploadBankStatementsDetail
                            Dim detail As New UploadBankStatementsDetail()


                            ' Asignar valores desde el BankMovements al UploadBankStatementsDetail
                            detail.ValueCredit = CDec(movements.ValueCredit)
                            detail.ValueDebit = CDec(movements.ValueDebit)
                            detail.DescriptionTransaction = movements.DescriptionTransaction
                            detail.TransactionDate = movements.TransactionDate
                            If movements.TransactionCode Is Nothing Then
                                detail.TransactionCode = " "
                            Else
                                detail.TransactionCode = movements.TransactionCode
                            End If
                            ' Agregar el detalle a la lista
                            _listUploadBankStatements.Add(detail)

                        Next

                        Dim BankUploadStatementDetail = _listUploadBankStatements.Where(Function(item) item.ChangeTracker.State <> 8).ToList()
                        'Se asigna el DocumentType acorde a la descripción de la transacción
                        MatchConciliationConcepts(BankUploadStatementDetail)
                    End Using

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

#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Evento que se dispara al seleccionar una Entidad Bancaria diferente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleEntityAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityAccount.EditValueChanged
        INDpceAddDetail.Enabled = True
        'Se consultan los conceptos de conciliación asociados al Banco
        If IdEntityBankAccount IsNot Nothing Then
            'Usamos Modelo del EntityBankAccount para obtener la entidad
            Using MBankAccount As New MEntityAccount(CStr(Me.Tag))
                _bank = Await MBankAccount.GetEntityBankAccountById(IdEntityBankAccount)
                SetCurrencyUI(_bank.CurrencyAbbreviation)
            End Using
            'Usamos el Modelo del Frm para obtener los conceptos de conciliación del banco
            Using Model As New MUploadBankStatements(CStr(Me.Tag))
                Dim res = Await Model.GetBankConciliationConceptsByEntityBankAccountsId(IdEntityBankAccount)
                If res.StateResult Then
                    If Not res.ObjectEmbbeded.Any() Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoConciliationConcepts", MODULE_NAME)
                    End If
                    ListConciliationConcepts = res.ObjectEmbbeded
                Else
                    Mensaje(EeventViewerImages.MensajeError) = res.Message
                End If
            End Using
            ValidateEntityBankAccountByPeriod(IdEntityBankAccount, INDCdnPeriod.GetYear, INDCdnPeriod.GetMonth)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cambiar la fecha del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDCdnPeriod_EditValueChanged(sender As Object, e As EventArgs) Handles INDCdnPeriod.OnChangeDate
        If IdEntityBankAccount IsNot Nothing Then
            ValidateEntityBankAccountByPeriod(IdEntityBankAccount, INDCdnPeriod.GetYear, INDCdnPeriod.GetMonth)
        End If
    End Sub
    ''' <summary>
    ''' Valida 
    ''' </summary>
    ''' <param name="IdBank"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    Private Async Sub ValidateEntityBankAccountByPeriod(ByVal IdBank As Integer, ByVal Year As Integer, ByVal Month As Integer)
        If _bank IsNot Nothing AndAlso Code = "" Then
            Using model As New MUploadBankStatements(CStr(Me.Tag))
                Dim res = Await model.GetUploadBankStatementsByPeriod(IdBank, Year, Month)
                If res IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "Ya existe un extracto bancario para la cuenta " & _bank.Code & " - " & _bank.Bank.Name & " en este periodo"
                    ActivateButtons(False)
                Else
                    ActivateButtons(True)
                End If
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Método para activar o desactivar botones
    ''' </summary>
    ''' <param name="value"></param>
    Private Sub ActivateButtons(ByVal value As Boolean)
        INDpceAddDetail.Enabled = value
        INDBtnImportFileBankStatementsDetailAPI.Enabled = value
        INDEsbBankStatementsDetail.Enabled = value
        INDBtnImportFileBankStatementsDetail.Enabled = value
    End Sub
#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
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
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        BarraBotones.Focus()
        uploadBankStatements.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        BarraBotones.Focus()
        uploadBankStatements.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento para desconfirmar un registro
    ''' </summary>
    Private Sub BarraBotones_ClickDesconfirmar() Handles BarraBotones.Click_Desconfirmar
        If MessageIndigo.Show("Esta seguro que desea desconfirmar el cargue de extracto bancario?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            uploadBankStatements.Status = 1
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.TreasurySequenceDetail IsNot Nothing Then
                If Not Me._sequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        SaveAndConfirm()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        BarraBotones.Focus()
        uploadBankStatements.Status = 3
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        SaveAndConfirm()
    End Sub

#End Region

#Region "Selection"

    ''' <summary>
    ''' Método que se encarga de poner o no visible el check en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectOption.EditValueChanging
        If e IsNot Nothing Then
            Dim row As UploadBankStatementsDetail = INDGvBankStatements.GetFocusedRow()
            If row IsNot Nothing Then
                row.SelectOption = e.NewValue

                VisibleCheck()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que se encarga de seleccionar los item visibles (con o sin filtro al dar doble click en el chck de la columna Sel.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDGcBankStatements_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDGcBankStatements.MouseDoubleClick
        If _listUploadBankStatements IsNot Nothing AndAlso _listUploadBankStatements.Count > 0 Then
            Dim hitPoint = Me.INDGvBankStatements.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDColSelect") Then

                    Dim listFilterXpCollection = INDGvBankStatements.DataController.GetAllFilteredAndSortedRows()
                    Dim count As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count

                    If count = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDColSelect.Image = Global.Presentation.Treasury.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        count = (From l In listFilterXpCollection Where l.SelectOption = True).Count
                        If count = _listUploadBankStatements.Count Then
                            Me.INDColSelect.Image = Global.Presentation.Treasury.My.Resources.Resources.check
                        End If
                    End If
                    Me.INDGcBankStatements.RefreshDataSource()
                    Me.INDGcBankStatements.Invalidate()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que se encarga de poner o no visible el check en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub VisibleCheck()
        If _listUploadBankStatements IsNot Nothing AndAlso _listUploadBankStatements.Count > 0 Then
            If _listUploadBankStatements.Where(Function(item) item.SelectOption = True).Count = _listUploadBankStatements.Count Then
                Me.INDColSelect.Image = Global.Presentation.Treasury.My.Resources.Resources.check
            Else
                Me.INDColSelect.Image = Global.Presentation.Treasury.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    Private Sub INDTxtInitialBalance_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtInitialBalance.EditValueChanged
        CalculatedEndingBalance()
    End Sub

    Private Sub CalculatedEndingBalance()
        Dim totalizado As Decimal = 0

        If _listUploadBankStatements IsNot Nothing AndAlso _listUploadBankStatements.Count > 0 Then
            If Me.indigo.Culture.Name = "es-CR" Then
                totalizado = _listUploadBankStatements.Sum(Function(d) d.ValueCredit - d.ValueDebit)
            Else
                totalizado = _listUploadBankStatements.Sum(Function(d) d.ValueDebit - d.ValueCredit)
            End If
        End If

        EndingBalance = InitialBalance + totalizado
    End Sub
#End Region

End Class

''' <summary>
''' Simplificacion de clase de la vista
''' Se usa para gestionar los valores directamte
''' </summary>
Public Class ViewDocumentType
    Public Property Id As Integer
    Public Property Name As String
End Class