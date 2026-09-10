#Region "Imports"

Imports System.Text
Imports System.Text.RegularExpressions
Imports DevExpress.Data
Imports DevExpress.Xpo
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Treasury.MVP
Imports Presentation.Accounting.MVP
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors


#End Region

Public Class FrmBankConciliationAutomatic
    Implements IBankConciliationAutomatic

#Region "Consts"

    ''' <summary>
    ''' Constante que contiene el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"
    ''' <summary>
    ''' id delfrm de notas
    ''' </summary>
    Private Const _idFormTreasuryNote = 637
    ''' <summary>
    ''' Temporizador para manejar el evento de sumas Débitos y Créditos después del filtro
    ''' </summary>
    Private WithEvents filterTimer As New Timer With {.Interval = 1300}

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private _presenter As PBankConciliationAutomatic

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequense As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Variable para controlar el registro bloqueado en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private _blockRecord As Domain.Entities.BlockRecordTreasury

    ''' <summary>
    ''' Variable para guardar el estado
    ''' </summary>
    Private _varImp As Integer

    ''' <summary>
    ''' Variable que contiene la entidad de la conciliación bancaria
    ''' </summary>
    ''' <remarks></remarks>
    Private _BankReconciliationAutomatic As BankReconciliationAutomatic

    Private BankReconciliationAutomaticDetail As New BankReconciliationAutomaticDetail

    ''' <summary>
    ''' Representa la entidad del detale del estracto
    ''' </summary>
    Private AutomaticExtractDetail As BankReconciliationAutomaticExtractDetail

    ''' <summary>
    ''' Representa la entidad del cargue del extracto
    ''' </summary>
    Private StatementsDetail As New BankReconciliationAutomaticExtractDetail

    ''' <summary>
    ''' Lista sin filtros del extracto
    ''' </summary>
    Private BankReconciliationAutomaticExtractDetail As List(Of BankReconciliationAutomaticExtractDetail)

    ''' <summary>
    ''' List de chequeos
    ''' </summary>
    Private rowListCheck As New List(Of Object)

    ''' <summary>
    ''' Variable para el numero de mes
    ''' </summary>
    Private _month As Integer

    ''' <summary>
    ''' Variable para el año
    ''' </summary>
    Private _year As Integer

    ''' <summary>
    ''' Lista que se encarga de 
    ''' </summary>
    Private _listAutomaticAssociation As New List(Of BankReconciliationAutomaticAssociation)
    ''' <summary>
    ''' Bandera para encontrar concidencias 
    ''' </summary>
    Private FlagReconciled As Boolean

    ''' <summary>
    ''' Actions Context
    ''' </summary>
    Private ListActionsRequest As New List(Of eAcciones)

    ''' <summary>
    ''' Acciones para el segmento de Extracto Bancario y Libro de Bancos
    ''' </summary>
    Private _actionsForNotes As New List(Of eAcciones)

    ''' <summary>
    ''' Acciones para el segmento de Libro de bancos   
    ''' </summary>
    Private _actionsBankReconciliationDetail As New List(Of eAcciones)

    ''' <summary>
    ''' Lista de entitycode multi select
    ''' </summary>
    Private EntityCodeList As New List(Of String)

    ''' <summary>
    ''' Control de Conciliación Automática Bancaria
    ''' </summary>
    Private _ctrBankConciliation As New CtrBankConciliationAutomatic()
    ''' <summary>
    ''' Lista para Conciliación Automática Bancaria
    ''' </summary>
    Private bankReconciliationAutomatic As New ActionResult(Of BankReconciliationAutomatic)
    ''' <summary>
    ''' Datos del extracto con los que se crearon la nota de gastos
    ''' </summary>
    Private _extractFromNewNote As New List(Of BankReconciliationAutomaticExtractDetail)
    ''' <summary>
    ''' objeto de tesoreria
    ''' </summary>
    Private _treasuryNote As TreasuryNote
    ''' <summary>
    ''' Filtro para obtener el detalle Conciliado con el Documento de Libros de Bancos
    ''' </summary>
    Private _filterToGetItemRelated As String
    ''' <summary>
    ''' Almacena el Id de la cuenta bancaria anterior para validar y evitar consultas innecesarias
    ''' </summary>
    Private _lastEntityBankAccountId As Integer? = Nothing
    ''' <summary>
    ''' Almacena la fecha anterior para validar y evitar consultas innecesarias
    ''' </summary>
    Private _lastDocumentDate As Date? = Nothing

#End Region

#Region "Properties"

    ''' <summary>
    ''' Tag
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IBankConciliationAutomatic.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Layout
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IBankConciliationAutomatic.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Secuencia numerica
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequence As TreasurySequence Implements IBankConciliationAutomatic.Sequence
        Get
            Return Me._sequense
        End Get
        Set(value As TreasurySequence)
            _sequense = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.TreasurySequenceDetail In Me._sequense.TreasurySequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
        End Set
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements IBankConciliationAutomatic.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la cuenta bancaria
    ''' </summary>
    ''' <returns></returns>
    Public Property EntityBankAccountId As Integer? Implements IBankConciliationAutomatic.EntityBankAccountId
        Get
            Return INDsleEntityBankAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleEntityBankAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha de documento
    ''' </summary>
    ''' <returns></returns>
    Public Property DocumentDate As Date? Implements IBankConciliationAutomatic.DocumentDate
        Get
            Return INDdteDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Saldo final libros
    ''' </summary>
    ''' <returns></returns>
    Public Property EntityBankAccountValue As Decimal Implements IBankConciliationAutomatic.EntityBankAccountValue
        Get
            Return INDseEndEntityBankAccountValue.EditValue
        End Get
        Set(value As Decimal)
            INDseEndEntityBankAccountValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Saldo final extracto
    ''' </summary>
    ''' <returns></returns>
    Public Property ExtractValue As Decimal Implements IBankConciliationAutomatic.ExtractValue
        Get
            Return INDseExtractValue.EditValue
        End Get
        Set(value As Decimal)
            INDseExtractValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Saldo final del movimiento de extracto
    ''' </summary>
    ''' <returns></returns>
    Public Property ExtractValueMovement As Decimal Implements IBankConciliationAutomatic.ExtractValueMovement
        Get
            Return INDseExtractValueMovement.EditValue
        End Get
        Set(value As Decimal)
            INDseExtractValueMovement.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Diferencia a conciliar
    ''' </summary>
    ''' <returns></returns>
    Public Property DifferenceReconcile As Decimal Implements IBankConciliationAutomatic.DifferenceReconcile
        Get
            Return INDseDifferenceReconcile.EditValue
        End Get
        Set(value As Decimal)
            INDseDifferenceReconcile.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Indica el estado del proceso de conciliación
    ''' 0.No se ha ejecutado  1.Se ejecutó la conciliación  2.La conciliación se cerró
    ''' </summary>
    ''' <returns></returns>
    Public Property IsProcessed As Byte Implements IBankConciliationAutomatic.IsProcessed

    ''' <summary>
    ''' Datasource con el listado del detalle de los documentos de tesorería para la conciliación
    ''' </summary>
    ''' <remarks></remarks>
    Private Property _listBankReconciliationAutomaticDetail As List(Of BankReconciliationAutomaticDetail)
        Get
            Return CType(INDgcTreasury.DataSource, List(Of BankReconciliationAutomaticDetail))
        End Get
        Set(value As List(Of BankReconciliationAutomaticDetail))
            INDgcTreasury.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource con la lista del cargue de extracto bancarios
    ''' </summary>
    Private Property _listBankReconciliationExtract As List(Of BankReconciliationAutomaticExtractDetail)
        Get
            Return CType(INDGcBankStatements.DataSource, List(Of BankReconciliationAutomaticExtractDetail))
        End Get
        Set(value As List(Of BankReconciliationAutomaticExtractDetail))
            INDGcBankStatements.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource con los detalles del extracto bancario que han sido Conciliados con Documentos de Tesorería
    ''' </summary>
    Private Property _listExtractDetailReconciled As List(Of BankReconciliationAutomaticExtractDetail)
        Get
            Return CType(INDGcBankReconciliation.DataSource, List(Of BankReconciliationAutomaticExtractDetail))
        End Get
        Set(value As List(Of BankReconciliationAutomaticExtractDetail))
            INDGcBankReconciliation.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de las Partidas pendientes por conciliar/Otros
    ''' </summary>
    Private Property _listPendingItemsToReconciled As List(Of PendingItemsToReconciled)
        Get
            Return CType(INDGcPendingItemsToReconciled.DataSource, List(Of PendingItemsToReconciled))
        End Get
        Set(value As List(Of PendingItemsToReconciled))
            INDGcPendingItemsToReconciled.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            If _BankReconciliationAutomatic IsNot Nothing Then
                _BankReconciliationAutomatic.Status = value
            End If
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Almacena el tipo de Nota de Tesorería
    ''' 1 - Nota de Gastos, 2 - Nota Terceros pendientes por Identificar
    ''' </summary>
    ''' <returns></returns>
    Public Property TreasuryNoteType As Integer
        Get
            Return INDSleNoteType.EditValue
        End Get
        Set(value As Integer)
            INDSleNoteType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor de la nota de Tesorería
    ''' </summary>
    ''' <returns></returns>
    Public Property TreasuryNoteValue As Decimal
        Get
            Return INDSeValue.EditValue
        End Get
        Set(value As Decimal)
            INDSeValue.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Almacena el comentario
    ''' </summary>
    ''' <returns></returns>
    Public Property Comments As String
        Get
            Return INDMeComments.Text
        End Get
        Set(value As String)
            INDMeComments.Text = value
        End Set
    End Property


#End Region

#Region "Datasources"

    ''' <summary>
    ''' Datasource cuenta bancaria
    ''' </summary>
    ''' <returns></returns>
    Public Property EntityBankAccountXpo As XPInstantFeedbackSource Implements IBankConciliationAutomatic.EntityBankAccountXpo
        Get
            Return INDsleEntityBankAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEntityBankAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece los tipos de documentos
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingDocumentType As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property FillingDocumentType As List(Of Tuple(Of Byte, String)) Implements IBankConciliationAutomatic.FillingDocumentType
        Get
            If _FillingDocumentType Is Nothing Then
                _FillingDocumentType = New List(Of Tuple(Of Byte, String))
                _FillingDocumentType.Add(New Tuple(Of Byte, String)(1, "Recibo de caja"))
                _FillingDocumentType.Add(New Tuple(Of Byte, String)(2, "Comprobante de egreso"))
                _FillingDocumentType.Add(New Tuple(Of Byte, String)(3, "Nota"))
                _FillingDocumentType.Add(New Tuple(Of Byte, String)(4, "Consignación"))
                _FillingDocumentType.Add(New Tuple(Of Byte, String)(5, "Fondo de Caja Menor"))
            End If
            Return _FillingDocumentType
        End Get
    End Property

    ''' <summary>
    ''' Establece las naturalezas a manejar
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingNature As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property FillingNature As List(Of Tuple(Of Byte, String)) Implements IBankConciliationAutomatic.FillingNature
        Get
            If _FillingNature Is Nothing Then
                _FillingNature = New List(Of Tuple(Of Byte, String))
                _FillingNature.Add(New Tuple(Of Byte, String)(1, "Débito"))
                _FillingNature.Add(New Tuple(Of Byte, String)(2, "Crédito"))
            End If
            Return _FillingNature
        End Get
    End Property

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Deshace los cambios que se hayan hecho al form
    ''' </summary>
    Public Async Sub Deshacer() Implements ICrudBase.Deshacer
        Await CleanControls()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() OrElse Not ValidateDetail() OrElse Not Await ValidateRules() Then
            Exit Sub
        End If
        Try
            AssigningValues()
            Using Model As New MBankConciliationAutomatic(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of BankReconciliationAutomatic) = Await Model.SaveBankReconciliationAutomatic(Me._BankReconciliationAutomatic, Me._idCurrentSequense)
                AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message

                    If _BankReconciliationAutomatic.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._idCurrentSequense).RemoveAt(0)
                        End If
                    End If

                    Me._BankReconciliationAutomatic = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Slide de mensajes
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

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            NewEntity()
        End If
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Banco", .FieldName = "EntityBankAccountId.CodeBankAccount", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListBankReconciliationAutomatic
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
        Code = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Función que evalua si existe un extracto relacionado a la Cuenta Bancaria y el Periodo
    ''' Evalúa que no existan más Conciliaciones Automáticas de la misma entidad bancaria
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ValidateRules() As Task(Of Boolean)
        If Not EntityBankAccountId.HasValue OrElse Not DocumentDate.HasValue Then
            Return False
        End If

        Using model As New MBankConciliationAutomatic(Me.Tag.ToString())
            Dim res = Await model.GetUploadBankStatementsByEntityBankAccountAndPeriod(EntityBankAccountId, DocumentDate.Value.Month, DocumentDate.Value.Year)
            If res Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoUploadBankStatements", NAME_MODULE)
                Return False
            End If
            'Se valida solo si la Conciliación es nueva que no exista otra para el mismo banco y mismo periodo
            If _BankReconciliationAutomatic.Id = 0 Then
                Dim results = Await model.GetBankConciliationAutomaticByBank(EntityBankAccountId)
                Dim alreadyExists = results.Any(Function(x) Month(x.DocumentDate) = DocumentDate.Value.Month AndAlso Year(x.DocumentDate) = DocumentDate.Value.Year)

                If alreadyExists Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("BankConciliationAutomaticAlreadyExists", NAME_MODULE)
                    Return False
                End If
            End If
        End Using

        Return True
    End Function

    ''' <summary>
    ''' Función para obtener los detalles de la conciliación bancaria
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetBankReconciliationDetails() As Task
        Try
            Using model As New MBankConciliationAutomatic(CStr(MyTag))
                AsyncLoader(True)
                'Obtenemos detalles del segmento Libro de Bancos
                Dim bankDetails = Await Me._presenter.ListBankReconciliationAutomaticDetail(_BankReconciliationAutomatic.Id, EntityBankAccountId, DocumentDate)
                If bankDetails.StateResult Then
                    ' Usar ToLookup para recorrer la lista una sola vez
                    Dim partitionedData = bankDetails.ObjectEmbbeded.ToLookup(Function(x) x.ReconciledStatus Is Nothing)

                    _listBankReconciliationAutomaticDetail = partitionedData(True).ToList()
                    _listPendingItemsToReconciled = Await model.CreatePendingItemsToReconciled(partitionedData(False).ToList(), Nothing)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = bankDetails.Message
                End If

                'Obtenemos detalles del segmento Extracto Bancario
                Dim bankExtractDetails = Await Me._presenter.ListGetUploadBankStatementsDetailByEntityBankAccountAutomatic(_BankReconciliationAutomatic.Id, EntityBankAccountId, _month, _year)
                If bankExtractDetails.StateResult Then
                    'Añadimos los detalles del extracto pendientes por conciliar
                    _listBankReconciliationExtract = bankExtractDetails.ObjectEmbbeded.Where(Function(x) Not x.Reconciled).ToList()
                    'Añadimos los detalles del extracto conciliados
                    _listExtractDetailReconciled = bankExtractDetails.ObjectEmbbeded.Where(Function(x) x.Reconciled).ToList()
                End If

                Await GetPendingItems(model, EntityBankAccountId, DocumentDate)

                If IsProcessed = 2 OrElse BarraBotones.StatusRecord = 2 Then
                    GenerateBankReconciliationClosing(_listBankReconciliationAutomaticDetail, _listBankReconciliationExtract)
                End If
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Función para refrescar los detalles de la conciliación bancaria sin perder cambios locales
    ''' Obtiene nuevos documentos del servidor y hace merge con el estado local
    ''' </summary>
    ''' <returns></returns>
    Private Async Function RefreshBankReconciliationDetailsWithMerge() As Task
        Try
            Using model As New MBankConciliationAutomatic(CStr(MyTag))
                AsyncLoader(True)

                ' 1. Almacenar estado local actual en un diccionario para acceso rápido
                Dim localDocumentsDict As New Dictionary(Of Integer, BankReconciliationAutomaticDetail)()
                If _listBankReconciliationAutomaticDetail IsNot Nothing Then
                    For Each doc In _listBankReconciliationAutomaticDetail
                        If doc.EntityId > 0 AndAlso Not localDocumentsDict.ContainsKey(doc.EntityId) Then
                            localDocumentsDict.Add(doc.EntityId, doc)
                        End If
                    Next
                End If

                ' 2. Obtener datos del servidor
                Dim serverBankDetails = Await Me._presenter.ListBankReconciliationAutomaticDetail(_BankReconciliationAutomatic.Id, EntityBankAccountId, DocumentDate)
                If Not serverBankDetails.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = serverBankDetails.Message
                    Exit Try
                End If

                Dim serverDocuments = serverBankDetails.ObjectEmbbeded.Where(Function(x) x.ReconciledStatus Is Nothing).ToList()
                Dim serverDocumentsDict As New Dictionary(Of Integer, BankReconciliationAutomaticDetail)()
                For Each doc In serverDocuments
                    If doc.EntityId > 0 AndAlso Not serverDocumentsDict.ContainsKey(doc.EntityId) Then
                        serverDocumentsDict.Add(doc.EntityId, doc)
                    End If
                Next

                ' 3. Detectar documentos eliminados del servidor que estaban conciliados localmente
                Dim deletedReconciledDocuments As New List(Of BankReconciliationAutomaticDetail)()
                For Each localDoc In localDocumentsDict.Values
                    If Not serverDocumentsDict.ContainsKey(localDoc.EntityId) Then
                        ' Documento eliminado del servidor
                        If localDoc.Reconciled Then
                            deletedReconciledDocuments.Add(localDoc)
                        End If
                    End If
                Next

                ' 4. Manejar documentos eliminados que estaban conciliados
                If deletedReconciledDocuments.Any() Then
                    HandleDeletedReconciledDocuments(deletedReconciledDocuments)
                End If

                ' 5. Hacer merge de documentos
                Dim mergedDocuments As New List(Of BankReconciliationAutomaticDetail)()

                For Each serverDoc In serverDocuments
                    If localDocumentsDict.ContainsKey(serverDoc.EntityId) Then
                        ' Documento existe localmente: mantener propiedades locales
                        Dim localDoc = localDocumentsDict(serverDoc.EntityId)
                        serverDoc.Reconciled = localDoc.Reconciled
                        serverDoc.Comments = localDoc.Comments
                        serverDoc.Checked = localDoc.Checked
                        serverDoc.ReconciledStatus = localDoc.ReconciledStatus
                        serverDoc.BankReconciliationAutomaticOriginId = localDoc.BankReconciliationAutomaticOriginId
                    End If
                    ' Si no existe localmente, es nuevo y se agrega tal cual
                    mergedDocuments.Add(serverDoc)
                Next

                ' 6. Actualizar la lista de detalles
                _listBankReconciliationAutomaticDetail = mergedDocuments

                ' 7. Obtener partidas pendientes de periodos anteriores (solo las nuevas)
                Await GetPendingItems(model, EntityBankAccountId, DocumentDate)

            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        Finally
            AsyncLoader(False)
            ' 8. Refrescar rejillas
            RefreshGrids()
            INDGcPendingItemsToReconciled.RefreshDataSource()
            Me.CalculateDifference(False)
            UpdateSummaryDebitCredit(False)
        End Try
    End Function

    ''' <summary>
    ''' Maneja los documentos eliminados del servidor que estaban conciliados localmente
    ''' Desmarca los extractos relacionados y elimina las asociaciones
    ''' </summary>
    ''' <param name="deletedDocuments">Lista de documentos eliminados que estaban conciliados</param>
    Private Sub HandleDeletedReconciledDocuments(deletedDocuments As List(Of BankReconciliationAutomaticDetail))
        If deletedDocuments Is Nothing OrElse Not deletedDocuments.Any() Then Exit Sub
        If _listAutomaticAssociation Is Nothing Then Exit Sub

        For Each deletedDoc In deletedDocuments
            ' Buscar asociaciones relacionadas con el documento eliminado
            Dim associationsToRemove = _listAutomaticAssociation.Where(Function(a) _
                a.BankReconciliationAutomaticDetail IsNot Nothing AndAlso
                a.BankReconciliationAutomaticDetail.EntityId = deletedDoc.EntityId
            ).ToList()

            For Each assoc In associationsToRemove
                ' Obtener el extracto asociado
                Dim relatedExtract = assoc.BankReconciliationAutomaticExtractDetail
                If relatedExtract IsNot Nothing Then
                    ' Desmarcar el extracto como conciliado
                    relatedExtract.Reconciled = False
                    relatedExtract.CodeNoteReconciled = Nothing

                    ' Mover el extracto de _listExtractDetailReconciled a _listBankReconciliationExtract
                    If _listExtractDetailReconciled IsNot Nothing Then
                        _listExtractDetailReconciled.Remove(relatedExtract)
                    End If

                    If _listBankReconciliationExtract Is Nothing Then
                        _listBankReconciliationExtract = New List(Of BankReconciliationAutomaticExtractDetail)()
                    End If

                    ' Evitar duplicados al agregar
                    If Not _listBankReconciliationExtract.Any(Function(e) e.UploadBankStatementsDetailId = relatedExtract.UploadBankStatementsDetailId) Then
                        _listBankReconciliationExtract.Add(relatedExtract)
                    End If
                End If
            Next

            ' Eliminar las asociaciones del documento eliminado
            _listAutomaticAssociation.RemoveAll(Function(a) _
                a.BankReconciliationAutomaticDetail IsNot Nothing AndAlso
                a.BankReconciliationAutomaticDetail.EntityId = deletedDoc.EntityId
            )

            ' Eliminar el documento de la lista local
            _listBankReconciliationAutomaticDetail?.RemoveAll(Function(d) d.EntityId = deletedDoc.EntityId)
        Next
    End Sub

    ''' <summary>
    ''' Obtiene la nueva nota
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetNewNote() As Task(Of BankReconciliationAutomaticDetail)
        Dim updatedDocuments As List(Of BankReconciliationAutomaticDetail)
        ' Almacenamos los documentos añadidos anteriores a la Nota Creada
        Dim previousDocumentCodes = _listBankReconciliationAutomaticDetail.Select(Function(d) d.EntityCode).ToList()
        ' Actualizamos para obtener la nueva nota
        Dim result = Await Me._presenter.ListBankReconciliationAutomaticDetail(_BankReconciliationAutomatic.Id, EntityBankAccountId, DocumentDate)
        If result.StateResult Then
            updatedDocuments = result.ObjectEmbbeded
            ' Obtenemos la nueva Nota
            Dim newDocument = updatedDocuments.FirstOrDefault(Function(d) Not previousDocumentCodes.Contains(d.EntityCode))
            If newDocument IsNot Nothing Then
                Return newDocument
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = result.Message
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    Public WriteOnly Property ActionsOnControls As Boolean Implements IBankConciliationAutomatic.ActionsOnControls
        Set(value As Boolean)
            INDlyRoot.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDsleEntityBankAccount.Enabled = value
            INDdteDocumentDate.Enabled = value
            INDseEndEntityBankAccountValue.Enabled = value
            INDseExtractValue.Enabled = value
            INDseExtractValueMovement.Enabled = value
            INDseDifferenceReconcile.Enabled = value
            INDMeComments.Enabled = value
            INDgcTreasury.Enabled = value
            INDGcBankReconciliation.Enabled = value
            INDGcBankStatements.Enabled = value
            INDGcPendingItemsToReconciled.Enabled = value
            INDlyRoot.EndUpdate()
            If value Then
                INDsleEntityBankAccount.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Calcula el valor del saldo anterior del banco
    ''' </summary>
    Private Sub CalculateLastBalance()
        If EntityBankAccountId IsNot Nothing AndAlso DocumentDate IsNot Nothing Then
            EntityBankAccountValue = _presenter.CalculateBalance(EntityBankAccountId, DocumentDate)
        End If
    End Sub

    ''' <summary>
    ''' Realiza la operacion para saber la diferencia a conciliar
    ''' </summary>
    ''' <remarks>
    ''' Incluye los detalles visibles en las rejillas y los detalles pendientes por conciliar (no descartados)
    ''' </remarks>
    Private Sub CalculateDifference(ByVal closeReconciliation As Boolean)
        Dim differenceReconciledCalculated As Decimal = 0
        Dim DetailUncheckedDebitValue As Decimal = 0
        Dim DetailUncheckedCreditValue As Decimal = 0
        Dim ExtractDetailDebitValue As Decimal = 0
        Dim ExtractDetailCreditValue As Decimal = 0

        ' Detalles de Libros de Bancos visibles en la rejilla
        If _listBankReconciliationAutomaticDetail IsNot Nothing Then
            DetailUncheckedDebitValue = _listBankReconciliationAutomaticDetail.Where(Function(d) d.Nature = 1 AndAlso Not d.Reconciled).Sum(Function(d) d.Value)
            DetailUncheckedCreditValue = _listBankReconciliationAutomaticDetail.Where(Function(d) d.Nature = 2 AndAlso Not d.Reconciled).Sum(Function(d) d.Value)
        End If

        ' Detalles de Extractos Bancarios visibles en la rejilla
        If _listBankReconciliationExtract IsNot Nothing Then
            ExtractDetailDebitValue = _listBankReconciliationExtract.Where(Function(d) d.Nature = 1 AndAlso Not d.Reconciled).Sum(Function(d) d.Value)
            ExtractDetailCreditValue = _listBankReconciliationExtract.Where(Function(d) d.Nature = 2 AndAlso Not d.Reconciled).Sum(Function(d) d.Value)
        End If

        ' Agregar los detalles pendientes por conciliar (no descartados)
        If _listPendingItemsToReconciled IsNot Nothing Then
            ' Detalles pendientes del Libro de Bancos (Origin = 1, ReconciledStatus != 2)
            Dim pendingDocuments = _listPendingItemsToReconciled.Where(Function(p) p.Origin = 1 AndAlso p.ReconciledStatus <> 2 AndAlso p.DocumentDetail IsNot Nothing).ToList()
            For Each pendingItem In pendingDocuments
                If pendingItem.DocumentDetail.Nature = 1 Then
                    DetailUncheckedDebitValue += pendingItem.DocumentDetail.Value
                ElseIf pendingItem.DocumentDetail.Nature = 2 Then
                    DetailUncheckedCreditValue += pendingItem.DocumentDetail.Value
                End If
            Next

            ' Detalles pendientes del Extracto Bancario (Origin = 2, ReconciledStatus != 2)
            Dim pendingExtracts = _listPendingItemsToReconciled.Where(Function(p) p.Origin = 2 AndAlso p.ReconciledStatus <> 2 AndAlso p.ExtractDetail IsNot Nothing).ToList()
            For Each pendingItem In pendingExtracts
                If pendingItem.ExtractDetail.Nature = 1 Then
                    ExtractDetailDebitValue += pendingItem.ExtractDetail.Value
                ElseIf pendingItem.ExtractDetail.Nature = 2 Then
                    ExtractDetailCreditValue += pendingItem.ExtractDetail.Value
                End If
            Next
        End If

        ' Calculamos la diferencia a Conciliar de acuerdo a si es cierre de conciliación o no
        If closeReconciliation Then
            Dim aux As Decimal = 0
            If EntityBankAccountValue > ExtractValue Then
                aux = EntityBankAccountValue + DetailUncheckedCreditValue - DetailUncheckedDebitValue - ExtractDetailCreditValue + ExtractDetailDebitValue
            Else
                aux = EntityBankAccountValue - DetailUncheckedCreditValue + DetailUncheckedDebitValue + ExtractDetailCreditValue - ExtractDetailDebitValue
            End If
            differenceReconciledCalculated = aux - ExtractValue
        Else
            differenceReconciledCalculated = (EntityBankAccountValue - DetailUncheckedDebitValue + DetailUncheckedCreditValue) - (ExtractValue + ExtractDetailDebitValue - ExtractDetailCreditValue)
        End If

        DifferenceReconcile = differenceReconciledCalculated
        _ctrBankConciliation.DifferenceReconcile = DifferenceReconcile
    End Sub

    Public Async Function CleanControls() As Task
        INDlyRoot.BeginUpdate()
        Await DeleteBlockedRecord()
        INDbtnCode.Text = String.Empty
        INDsleEntityBankAccount.EditValue = Nothing
        INDsleEntityBankAccount.Properties.NullText = String.Empty
        INDsleEntityBankAccount.Properties.ReadOnly = False
        INDdteDocumentDate.EditValue = Nothing
        IsProcessed = 0
        UpdateCloseReconciliationButtonText()
        Comments = Nothing
        INDseEndEntityBankAccountValue.EditValue = Nothing
        INDseExtractValue.EditValue = Nothing
        INDseDifferenceReconcile.EditValue = Nothing
        INDseExtractValue.Properties.NullText = String.Empty
        INDseExtractValueMovement.EditValue = Nothing
        INDgcTreasury.DataSource = Nothing
        INDGcBankStatements.DataSource = Nothing
        INDGcBankReconciliation.DataSource = Nothing
        _doc = Nothing
        _BankReconciliationAutomatic = Nothing
        _listBankReconciliationAutomaticDetail = Nothing
        BankReconciliationAutomaticExtractDetail = Nothing
        _listBankReconciliationExtract = Nothing
        _listExtractDetailReconciled = Nothing
        _listPendingItemsToReconciled = Nothing
        _lastEntityBankAccountId = Nothing
        _lastDocumentDate = Nothing
        _extractFromNewNote.Clear()
        ActionsOnControls = False
        Me.BarraBotones.StatusRecord = 1
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        _ctrBankConciliation.CleanControls()
        INDLcgBankStatements.HideControl(False)
        INDGcPendingItemsToReconciled.DataSource = Nothing
        EnablePrintButton(False)
        ReadOnlyControls(False)

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Conciliar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
        Me.BarraBotones.ReassignOperatingUnit()
        PrintSummaryDebitCredit(0, 0, 0, 0)
        INDlyRoot.EndUpdate()
    End Function

    ''' <summary>
    ''' Función para validar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateDetail() As Boolean
        Dim listErrors As New StringBuilder

        Me.CalculateDifference(False)

        If EntityBankAccountId Is Nothing Then
            listErrors.AppendLine("Se debe seleccionar una Cuenta Bancaria")
        End If

        If DocumentDate Is Nothing Then
            listErrors.AppendLine("Se debe seleccionar una Fecha")
        End If

        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Assignings the values
    ''' </summary>
    Private Sub AssigningValues()
        With _BankReconciliationAutomatic
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .EntityBankAccountId = EntityBankAccountId
            .DocumentDate = DocumentDate
            .EntityBankAccountValue = EntityBankAccountValue
            .ExtractValue = ExtractValue
            .IsProcessed = IsProcessed
            .Association = _listAutomaticAssociation

            ' Uso de métodos específicos para cada tipo de detalle
            MergeAutomaticDetails(.BankReconciliationAutomaticDetail)
            MergeExtractDetails(.BankReconciliationAutomaticExtractDetail)
            MergePendingItems(.BankReconciliationAutomaticDetail, .BankReconciliationAutomaticExtractDetail)

            ' Marcar el objeto principal
            If .Id > 0 Then
                .MarkAsModified()
            Else
                .MarkAsAdded()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Agrega o modifica los detalles del segmento libro de bancos
    ''' </summary>
    ''' <param name="targetCollection"></param>
    Private Sub MergeAutomaticDetails(targetCollection As ICollection(Of BankReconciliationAutomaticDetail))
        If _listBankReconciliationAutomaticDetail Is Nothing Then Return

        For Each detailItem In _listBankReconciliationAutomaticDetail
            Dim existingDetail = targetCollection.FirstOrDefault(Function(x) x.Id = detailItem.Id AndAlso detailItem.Id > 0)

            If existingDetail IsNot Nothing Then
                ' Actualizar el existente
                UpdateAutomaticDetail(existingDetail, detailItem)
                existingDetail.MarkAsModified()
            Else
                ' Agregar nuevo
                If detailItem.Id > 0 Then detailItem.MarkAsModified()
                targetCollection.Add(detailItem)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Función para guardar los detalles de extracto conciliados y no conciliados
    ''' </summary>
    ''' <param name="targetCollection"></param>
    Private Sub MergeExtractDetails(targetCollection As ICollection(Of BankReconciliationAutomaticExtractDetail))
        ' Procesar _listBankReconciliationExtract
        ProcessExtractDetailsList(_listBankReconciliationExtract, targetCollection)

        ' Procesar _listExtractDetailReconciled
        ProcessExtractDetailsList(_listExtractDetailReconciled, targetCollection)
    End Sub

    ''' <summary>
    ''' Agrega o modifica los detalles del extracto bancario
    ''' </summary>
    ''' <param name="sourceList"></param>
    ''' <param name="targetCollection"></param>
    Private Sub ProcessExtractDetailsList(sourceList As IEnumerable(Of BankReconciliationAutomaticExtractDetail), targetCollection As ICollection(Of BankReconciliationAutomaticExtractDetail))
        If sourceList Is Nothing Then Return

        For Each extractItem In sourceList
            Dim existingExtract = targetCollection.FirstOrDefault(Function(x) x.Id = extractItem.Id AndAlso extractItem.Id > 0)

            If existingExtract IsNot Nothing Then
                ' Actualizar el existente
                UpdateExtractDetail(existingExtract, extractItem)
                existingExtract.MarkAsModified()
            Else
                ' Agregar nuevo
                If extractItem.Id > 0 Then extractItem.MarkAsModified()
                targetCollection.Add(extractItem)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Separa y procesa los detalles pendientes por conciliar para guardar
    ''' </summary>
    ''' <param name="automaticDetailCollection"></param>
    ''' <param name="extractDetailCollection"></param>
    Private Sub MergePendingItems(automaticDetailCollection As ICollection(Of BankReconciliationAutomaticDetail), extractDetailCollection As ICollection(Of BankReconciliationAutomaticExtractDetail))
        If _listPendingItemsToReconciled Is Nothing Then Return

        For Each pendingItem In _listPendingItemsToReconciled
            ' Procesar DocumentDetail
            ProcessPendingDocumentDetail(pendingItem, automaticDetailCollection)

            ' Procesar ExtractDetail
            ProcessPendingExtractDetail(pendingItem, extractDetailCollection)
        Next
    End Sub

    ''' <summary>
    ''' Función para manejar y gestionar el guardado de los detalles pendientes por conciliar
    ''' </summary>
    ''' <param name="pendingItem"></param>
    ''' <param name="targetCollection"></param>
    Private Sub ProcessPendingDocumentDetail(pendingItem As PendingItemsToReconciled, targetCollection As ICollection(Of BankReconciliationAutomaticDetail))
        If pendingItem.DocumentDetail Is Nothing Then Return

        ' Buscar por EntityId en lugar de solo verificar existencia en la lista original
        Dim existingDocument = targetCollection.FirstOrDefault(Function(d) d.EntityId = pendingItem.DocumentDetail.EntityId)

        If existingDocument IsNot Nothing Then
            ' Actualizar el existente
            UpdateAutomaticDetail(existingDocument, pendingItem.DocumentDetail)
            existingDocument.MarkAsModified()
        Else
            ' Agregar nuevo
            targetCollection.Add(pendingItem.DocumentDetail)
        End If
    End Sub

    ''' <summary>
    ''' Función para manejar y gestionar el guardado de los detalles de extracto pendientes por conciliar
    ''' </summary>
    ''' <param name="pendingItem"></param>
    ''' <param name="targetCollection"></param>
    Private Sub ProcessPendingExtractDetail(pendingItem As PendingItemsToReconciled, targetCollection As ICollection(Of BankReconciliationAutomaticExtractDetail))
        If pendingItem.ExtractDetail Is Nothing Then Return

        Dim existingExtract = targetCollection.FirstOrDefault(Function(e) e.UploadBankStatementsDetailId = pendingItem.ExtractDetail.UploadBankStatementsDetailId)

        If existingExtract IsNot Nothing Then
            ' Actualizar el existente
            UpdateExtractDetail(existingExtract, pendingItem.ExtractDetail)
            existingExtract.MarkAsModified()
        Else
            ' Agregar nuevo
            targetCollection.Add(pendingItem.ExtractDetail)
        End If
    End Sub

    ''' <summary>
    ''' Método para actualizar los detalles modificados
    ''' </summary>
    ''' <param name="target"></param>
    ''' <param name="source"></param>
    Private Sub UpdateAutomaticDetail(target As BankReconciliationAutomaticDetail, source As BankReconciliationAutomaticDetail)
        target.Reconciled = source.Reconciled
        target.Comments = source.Comments
        target.ReconciledStatus = source.ReconciledStatus
        target.BankReconciliationAutomaticOriginId = source.BankReconciliationAutomaticOriginId
    End Sub

    ''' <summary>
    ''' Método para actualizar los extractos modificados
    ''' </summary>
    ''' <param name="target"></param>
    ''' <param name="source"></param>
    Private Sub UpdateExtractDetail(target As BankReconciliationAutomaticExtractDetail, source As BankReconciliationAutomaticExtractDetail)
        target.DocumentType = source.DocumentType
        target.Reconciled = source.Reconciled
        target.CodeNoteReconciled = source.CodeNoteReconciled
        target.BankReconciliationAutomaticOriginId = source.BankReconciliationAutomaticOriginId
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Sub NewEntity()
        _BankReconciliationAutomatic = New BankReconciliationAutomatic() With {.Status = True}
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.TreasurySequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequense = Me._sequense.TreasurySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Sub
                End If
            End If
            If Me._sequense.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Acción para comentarios
    ''' </summary>
    Private Sub ActionComments(itemsSelected As List(Of BankReconciliationAutomaticDetail), Optional triggeringControl As Control = Nothing)
        If itemsSelected.Count > 0 Then
            Comments = itemsSelected.First().Comments
        Else
            Comments = String.Empty
        End If

        ' Si se proporcionó un control que disparó el evento, usa su ubicación.
        If triggeringControl IsNot Nothing Then
            ' Calcula la posición para mostrar el pop-up justo debajo del botón
            Dim screenLocation As Point = triggeringControl.PointToScreen(New Point(0, triggeringControl.Height))
            INDPcComments.ShowPopup(screenLocation)
        Else
            ' Se muestra desde la posición del cursor o una posición predeterminada.
            INDPcComments.ShowPopup(New System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y))
        End If

        INDPcComments.Focus()
    End Sub

    ''' <summary>
    ''' Evento que muestra el botón de Notas de Tesorería
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TreasuryNotes_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridViewBankStatement.ContexMenuActions
        Dim barItem As BarButtonItem = TryCast(sender, BarButtonItem)

        If barItem IsNot Nothing AndAlso barItem.Links.Count > 0 Then
            ' Asignar el PopupControlContainer al botón
            INDPcNoteType.Manager = barItem.Manager
            barItem.DropDownControl = INDPcNoteType

            ' Asignar el campo valor de acuerdo a los Items Checkeados
            Dim debitValue As Decimal = _listBankReconciliationExtract.Where(Function(x) x.Checked And x.Nature = 1).Sum(Function(x) x.Value)
            Dim creditValue As Decimal = _listBankReconciliationExtract.Where(Function(x) x.Checked And x.Nature = 2).Sum(Function(x) x.Value)
            TreasuryNoteValue = Math.Abs(debitValue - creditValue)

            ' Mostrar el PopupControlContainer
            INDPcNoteType.Show()
            INDSleNoteType.Focus()
        End If
    End Sub

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
                Using Model As New MBankConciliationAutomatic(CStr(Me.Tag))
                    AsyncLoader(True)
                    INDlyRoot.BeginUpdate()
                    Dim resultOperation = Await Model.GetBankReconciliationAutomaticByCode(Code)
                    bankReconciliationAutomatic = resultOperation
                    If resultOperation.StateResult Then
                        _BankReconciliationAutomatic = resultOperation.ObjectEmbbeded
                        INDsleEntityBankAccount.Properties.ReadOnly = True
                        If _BankReconciliationAutomatic IsNot Nothing AndAlso _BankReconciliationAutomatic.Id > 0 Then
                            Me.BarraBotones.StatusRecordVisible = True
                            Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                                _blockRecord = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_BankReconciliationAutomatic.Id))

                                With _BankReconciliationAutomatic
                                    LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                    Me.BarraBotones.StatusRecordVisible = True
                                    Code = .Code
                                    EntityBankAccountId = .EntityBankAccountId
                                    INDsleEntityBankAccount.Properties.NullText = .EntityBankAccountCodeName
                                    DocumentDate = .DocumentDate
                                    EntityBankAccountValue = .EntityBankAccountValue
                                    ExtractValue = .ExtractValue
                                    IsProcessed = .IsProcessed
                                    UpdateCloseReconciliationButtonText()

                                    _month = Month(DocumentDate)
                                    _year = Year(DocumentDate)

                                    'Obtener datos de la pivot
                                    _listAutomaticAssociation = .Association

                                    Me.CalculateDifference(False)
                                    BarraBotones.StatusRecord = .Status.ToString
                                End With

                                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._BankReconciliationAutomatic.Code)
                                If _blockRecord.Id = 0 Then
                                    _blockRecord = (Await ModelRecord.SaveBlockRecord(
                                            New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                                .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _BankReconciliationAutomatic.Id})
                                            ).ObjectEmbbeded
                                Else
                                    Dim xtraMessage As String = String.Format(BaseClass.obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                                End If

                                Select Case _BankReconciliationAutomatic.Status
                                    Case 1
                                        ToolbarWithBankReconciliationOpen()

                                    Case 2
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = False
                                        EnablePrintButton(True)
                                        ReadOnlyControls(True)

                                    Case Else
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                        ReadOnlyControls(True)
                                End Select

                                ActionsOnControls = True
                                UpdateSummaryDebitCredit(False)
                            End Using
                        Else
                            AsyncLoader(False)
                            If Me._sequense.IsManual Then
                                Me.NewEntity()
                            Else
                                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                                Code = String.Empty
                                INDbtnCode.Focus()
                            End If
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = resultOperation.Message
                    End If
                    INDlyRoot.EndUpdate()
                End Using
            Catch ex As Exception
                INDbtnCode.Enabled = False
                Mensaje(EeventViewerImages.MensajeError) = ex.Message
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Function

    ''' <summary>
    ''' Obtiene las partidas pendientes por Conciliar de la cuenta bancaria en periodos anteriores
    ''' </summary>
    ''' <param name="entityBankAccountId"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    Private Async Function GetPendingItems(model As MBankConciliationAutomatic, entityBankAccountId As Integer, documentDate As Date) As Task
        Dim pendingItemsBankBook = Await model.GetPendingItemsBankBook(entityBankAccountId, documentDate)
        If pendingItemsBankBook.StateResult Then
            _listBankReconciliationAutomaticDetail.AddRange(pendingItemsBankBook.ObjectEmbbeded)
            INDgcTreasury.RefreshDataSource()
        Else
            Mensaje(EeventViewerImages.MensajeError) = pendingItemsBankBook.Message
        End If
        Dim pendingItemsExtract = Await model.GetPendingItemsExtract(entityBankAccountId, documentDate)
        If pendingItemsExtract.StateResult Then
            _listBankReconciliationExtract.AddRange(pendingItemsExtract.ObjectEmbbeded)
            INDGcBankStatements.RefreshDataSource()
        Else
            Mensaje(EeventViewerImages.MensajeError) = pendingItemsExtract.Message
        End If
    End Function

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent"), Me._BankReconciliationAutomatic.Code, String.Format("{0} ({1})", Me._BankReconciliationAutomatic.EntityBankAccountCodeName)),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._BankReconciliationAutomatic.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle"), Me._BankReconciliationAutomatic.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent"), Me._BankReconciliationAutomatic.Code, String.Format("{0} ({1})", Me._BankReconciliationAutomatic.EntityBankAccountCodeName))
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle"), Me._BankReconciliationAutomatic.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_blockRecord)
                _blockRecord = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._BankReconciliationAutomatic IsNot Nothing AndAlso Me._BankReconciliationAutomatic.Id > 0 Then
            If (MessageIndigo.Show(BaseClass.obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, BaseClass.obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Método que obtiene y asigna valores del Ctr
    ''' </summary>
    Private Async Sub AssigningCtrValues()
        If EntityBankAccountId.HasValue AndAlso DocumentDate.HasValue Then
            Using Model As New MBankConciliationAutomatic(CStr(Me.Tag))
                Dim res = Await Model.GetUploadBankStatementsByEntityBankAccountAndPeriod(EntityBankAccountId, DocumentDate.Value.Month, DocumentDate.Value.Year)
                _ctrBankConciliation.EntityBankAccountName = INDsleEntityBankAccount.Text
                _ctrBankConciliation.Month = MonthName(_month) + " " + CStr(_year)
                If res IsNot Nothing Then
                    _ctrBankConciliation.FinalStatementBalance = res.EndingBalance
                    ExtractValue = res.EndingBalance
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Establece el formato moneda en los controles del formulario
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)
        If _currencyAbbreviation Is Nothing Then Exit Sub

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrencyAbbreviationEmpty")
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat

        Me.INDseEndEntityBankAccountValue.Properties.Mask.Culture = _culture
        INDseExtractValueMovement.Properties.Mask.Culture = _culture
        INDseExtractValue.Properties.Mask.Culture = _culture
        INDseDifferenceReconcile.Properties.Mask.Culture = _culture

        Me.INDcolBankReconciliationDetail_Value = Window.Utils.FormatGrid(INDcolBankReconciliationDetail_Value, _currencyAbbreviation)
        Me.INDcolBankReconciliationAutomaticExtractDetail_Value = Window.Utils.FormatGrid(INDcolBankReconciliationAutomaticExtractDetail_Value, _currencyAbbreviation)
        _ctrBankConciliation._currencyAbbreviation = _currencyAbbreviation
        _ctrBankConciliation.PrintInfo()
    End Sub


    ''' <summary>
    ''' Actualiza los resúmenes de los totales de débito y crédito en las columnas correspondientes
    ''' de las tablas de reconciliación bancaria, tanto para la reconciliación automática como
    ''' para el extracto bancario.
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Private Sub UpdateSummaryDebitCredit(ByVal isFiltered As Boolean)
        If _listBankReconciliationAutomaticDetail Is Nothing OrElse _listBankReconciliationExtract Is Nothing Then Exit Sub

        Dim totalDebitReconciliation As Decimal = 0
        Dim totalCreditReconciliation As Decimal = 0
        Dim totalDebitExtract As Decimal = 0
        Dim totalCreditExtract As Decimal = 0

        ' Calcular totales de débito y crédito para la reconciliación automática usando el GridView
        If isFiltered Then
            '' Para la reconciliación automática
            For i As Integer = 0 To INDgvTreasury.RowCount - 1
                Dim nature As Integer = INDgvTreasury.GetRowCellValue(i, "Nature")
                Dim value As Decimal = INDgvTreasury.GetRowCellValue(i, "Value")
                If nature = 1 Then
                    totalDebitReconciliation += value
                ElseIf nature = 2 Then
                    totalCreditReconciliation += value
                End If
            Next

            ' Para el extracto bancario
            For i As Integer = 0 To INDGvBankStatements.RowCount - 1
                Dim nature As Integer = INDGvBankStatements.GetRowCellValue(i, "Nature")
                Dim value As Decimal = INDGvBankStatements.GetRowCellValue(i, "Value")
                If nature = 1 Then
                    totalDebitExtract += value
                ElseIf nature = 2 Then
                    totalCreditExtract += value
                End If
            Next
        Else
            ' Calcular totales de débito y crédito totales de los DataSource
            totalDebitReconciliation = _listBankReconciliationAutomaticDetail.Where(Function(x) x.Nature = 1).Sum(Function(x) x.Value)
            totalCreditReconciliation = _listBankReconciliationAutomaticDetail.Where(Function(x) x.Nature = 2).Sum(Function(x) x.Value)
            totalDebitExtract = _listBankReconciliationExtract.Where(Function(x) x.Nature = 1).Sum(Function(x) x.Value)
            totalCreditExtract = _listBankReconciliationExtract.Where(Function(x) x.Nature = 2).Sum(Function(x) x.Value)
        End If

        PrintSummaryDebitCredit(totalDebitReconciliation, totalCreditReconciliation, totalDebitExtract, totalCreditExtract)
    End Sub

    ''' <summary>
    ''' Pinta los valores Débitos Créditos de las Rejillas
    ''' </summary>
    ''' <param name="totalDebitReconciliation"></param>
    ''' <param name="totalCreditReconciliation"></param>
    ''' <param name="totalDebitExtract"></param>
    ''' <param name="totalCreditExtract"></param>
    Private Sub PrintSummaryDebitCredit(totalDebitReconciliation As Decimal, totalCreditReconciliation As Decimal, totalDebitExtract As Decimal, totalCreditExtract As Decimal)
        Dim textDebitReconciliation As String = String.Format("Débitos: {0:c2} ", totalDebitReconciliation)
        Dim textCreditReconciliation As String = String.Format("Créditos: {0:c2} ", totalCreditReconciliation)
        Dim textDebitExtract As String = String.Format("Débitos: {0:c2} ", totalDebitExtract)
        Dim textCreditExtract As String = String.Format("Créditos: {0:c2} ", totalCreditExtract)

        Dim columnValueReconciliation As GridColumn = INDgvTreasury.Columns("Value")
        Dim columnValueExtract As GridColumn = INDGvBankStatements.Columns("Value")

        If columnValueReconciliation IsNot Nothing AndAlso columnValueExtract IsNot Nothing Then
            columnValueReconciliation.Summary.Clear()
            columnValueExtract.Summary.Clear()

            columnValueReconciliation.Summary.Add(DevExpress.Data.SummaryItemType.Custom, "Value", textDebitReconciliation + "  " + textCreditReconciliation)
            columnValueExtract.Summary.Add(DevExpress.Data.SummaryItemType.Custom, "Value", textCreditExtract + "  " + textDebitExtract)
        End If
    End Sub

    ''' <summary>
    ''' Método que validar y conciliar las notas creadas
    ''' </summary>
    Private Sub ProcessNoteToReconciled(extractList As List(Of BankReconciliationAutomaticExtractDetail), newNote As BankReconciliationAutomaticDetail)
        Dim debitValue As Decimal = extractList.Where(Function(x) x.Nature = 1).Sum(Function(x) x.Value)
        Dim creditValue As Decimal = extractList.Where(Function(x) x.Nature = 2).Sum(Function(x) x.Value)
        Dim totalValue As Decimal = Math.Abs(debitValue - creditValue)
        Dim nature As Integer = If(debitValue > creditValue, 1, 2)
        If newNote.Value = totalValue AndAlso newNote.Nature = nature Then
            For Each extract In extractList
                extract.Reconciled = True
                extract.CodeNoteReconciled = newNote.EntityCode
                Dim automaticAssosiation As New BankReconciliationAutomaticAssociation
                With automaticAssosiation
                    .BankReconciliationAutomaticDetail = newNote
                    .BankReconciliationAutomaticExtractDetail = extract
                End With
                _listAutomaticAssociation.Add(automaticAssosiation)
            Next

            'Marcamos como conciliada la nueva nota
            Dim documentToUpdate = _listBankReconciliationAutomaticDetail?.FirstOrDefault(Function(d) d.EntityCode = newNote.EntityCode)
            If documentToUpdate IsNot Nothing Then
                documentToUpdate.Reconciled = True
            End If

            ' Eliminar los elementos conciliados de _listBankReconciliationExtract
            Dim extractKeys As New HashSet(Of (ConsecutiveBank As String, DocumentType As Integer?, Nature As Byte?, Value As Decimal))(
            extractList.Select(Function(e) (e.ConsecutiveBank, e.DocumentType, e.Nature, e.Value))
            )

            _listBankReconciliationExtract.RemoveAll(Function(extract) _
            extractKeys.Contains((extract.ConsecutiveBank, extract.DocumentType, extract.Nature, extract.Value))
            )

            ' Agregar los elementos conciliados a _listExtractDetailReconciled
            If _listExtractDetailReconciled Is Nothing Then
                _listExtractDetailReconciled = extractList
            Else
                _listExtractDetailReconciled.AddRange(extractList)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Función que actualiza las rejillas afectadas en el proceso de conciliación
    ''' </summary>
    Private Sub RefreshGrids()
        INDgcTreasury.RefreshDataSource()
        INDGcBankStatements.RefreshDataSource()
        INDGcBankReconciliation.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Función para buscar las Concidencias a conciliar
    ''' </summary>
    Private Async Function FindCoincidences(extractList As List(Of BankReconciliationAutomaticExtractDetail), documentList As List(Of BankReconciliationAutomaticDetail), associationList As List(Of BankReconciliationAutomaticAssociation)) As Task
        Try
            AsyncLoader(True)

            Using model As New MBankConciliationAutomatic(CStr(Me.Tag))

                Dim result = Await model.FindCoincidences(extractList, documentList, associationList)

                If result.StateResult Then
                    Dim reconciliationResult = result.ObjectEmbbeded

                    _listBankReconciliationAutomaticDetail = reconciliationResult.Documents
                    _listBankReconciliationExtract = reconciliationResult.Extracts.Where(Function(x) Not x.Reconciled).ToList()
                    _listExtractDetailReconciled = reconciliationResult.Extracts.Where(Function(x) x.Reconciled).ToList()
                    _listAutomaticAssociation = reconciliationResult.Associations

                    'Se asignan los DataSource modificados después del proceso de Conciliación
                    If _listExtractDetailReconciled IsNot Nothing AndAlso _listExtractDetailReconciled.Any() Then
                        RefreshGrids()
                        Me.CalculateDifference(False)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("NoDataToReconciled", NAME_MODULE)
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
            Throw
        Finally
            AsyncLoader(False)
        End Try
    End Function


    ''' <summary>
    ''' Crea una asociación automática entre un documento y un extracto
    ''' </summary>
    Private Sub CreateAutomaticAssociation(document As BankReconciliationAutomaticDetail, extractDetail As BankReconciliationAutomaticExtractDetail)
        Dim automaticAssociation As New BankReconciliationAutomaticAssociation
        With automaticAssociation
            .BankReconciliationAutomaticDetail = document
            .BankReconciliationAutomaticExtractDetail = extractDetail
        End With
        _listAutomaticAssociation.Add(automaticAssociation)
    End Sub

    ''' <summary>
    ''' Agrega coincidencias a la rejilla de Coinciliación y elimina esos registros de Extractos
    ''' </summary>
    Private Sub HandleConciliation(extractList As List(Of BankReconciliationAutomaticExtractDetail), documentList As List(Of BankReconciliationAutomaticDetail))
        Dim documentsToReconciled = _listBankReconciliationAutomaticDetail.Intersect(documentList).ToList()

        'Marcar como Conciliados los detalles del extracto que tienen coincidencias
        extractList.ForEach(Sub(extract)
                                ' Verificar que el extracto no haya sido conciliado previamente
                                If Not extract.Reconciled AndAlso Not String.IsNullOrEmpty(extract.CodeNoteReconciled) Then
                                    'Marcar los documentos coinciliados
                                    For Each document In documentsToReconciled
                                        If document.EntityCode = extract.CodeNoteReconciled Then
                                            If Not document.Reconciled Then 'Validamos que se realice conciliación con Documentos que no hayan sido conciliados
                                                extract.Reconciled = True
                                                document.Reconciled = True
                                                CreateAutomaticAssociation(document, extract)
                                                Exit For
                                            End If
                                        End If
                                    Next
                                End If
                            End Sub)

        'Se eliminan de la rejilla de Extracto los detalles que han sido coinciliados
        _listBankReconciliationExtract.RemoveAll(Function(extractDetail) extractList.Where(Function(extract) extract.Reconciled = True).Contains(extractDetail))

        'Se agregan las coincidencias al DataSource de Conciliación
        Dim reconciledExtracts = extractList.Where(Function(x) x.Reconciled = True).ToList()
        If reconciledExtracts.Any() Then
            If _listExtractDetailReconciled Is Nothing Then
                _listExtractDetailReconciled = reconciledExtracts.ToList()
            Else
                _listExtractDetailReconciled.AddRange(reconciledExtracts)
            End If
        End If
    End Sub


    ''' <summary>
    ''' Realiza conciliación manual entre extractos y documentos seleccionados
    ''' </summary>
    Private Sub HandleConciliationOneToOne(extractDetails As List(Of BankReconciliationAutomaticExtractDetail), documentDetails As List(Of BankReconciliationAutomaticDetail))
        ' Asignar códigos de conciliación antes de llamar a HandleConciliation
        For Each document In documentDetails
            For Each extractDetail In extractDetails
                ' Relacionar el código de la Nota Conciliada
                extractDetail.CodeNoteReconciled = document.EntityCode
            Next
        Next
        ' Procesar la conciliación 
        HandleConciliation(extractDetails, documentDetails)
    End Sub

    ''' <summary>
    ''' Realiza el proceso de conciliación de un documento a muchos extractos
    ''' </summary>
    ''' <param name="document"></param>
    ''' <param name="reconciledExtracts"></param>
    Private Sub HandleConciliationOneToMany(document As BankReconciliationAutomaticDetail, reconciledExtracts As List(Of BankReconciliationAutomaticExtractDetail))
        For Each extractReconciled In reconciledExtracts
            extractReconciled.Reconciled = True
            extractReconciled.CodeNoteReconciled = document.EntityCode
            Dim automaticAssosiation As New BankReconciliationAutomaticAssociation
            With automaticAssosiation
                .BankReconciliationAutomaticDetail = document
                .BankReconciliationAutomaticExtractDetail = extractReconciled
            End With
            _listAutomaticAssociation.Add(automaticAssosiation)
        Next

        ' Actualizar _listBankReconciliationAutomaticDetail
        Dim documentToUpdate = _listBankReconciliationAutomaticDetail.FirstOrDefault(Function(n) n.EntityCode = document.EntityCode)
        If documentToUpdate IsNot Nothing Then
            documentToUpdate.Reconciled = True
        End If

        ' Eliminar los elementos conciliados de _listBankReconciliationExtract usando un HashSet de tuplas
        Dim reconciledKeys As New HashSet(Of (ConsecutiveBank As String, DocumentType As Integer?, Nature As Byte?, Value As Decimal))(
            reconciledExtracts.Select(Function(e) (e.ConsecutiveBank, e.DocumentType, e.Nature, e.Value))
        )

        _listBankReconciliationExtract.RemoveAll(Function(extract) _
            reconciledKeys.Contains((extract.ConsecutiveBank, extract.DocumentType, extract.Nature, extract.Value))
        )


        ' Agregar los elementos coinciliados a _listExtractDetailReconciled
        If _listExtractDetailReconciled Is Nothing Then
            _listExtractDetailReconciled = reconciledExtracts
        Else
            _listExtractDetailReconciled.AddRange(reconciledExtracts)
        End If
    End Sub

    ''' <summary>
    ''' Realiza el proceso de conciliación de muchos documentos a un detalle del extracto
    ''' </summary>
    ''' <param name="documentsReconciled"></param>
    ''' <param name="extract"></param>
    Private Sub HandleConciliationManyToOne(documentsReconciled As List(Of BankReconciliationAutomaticDetail), extract As BankReconciliationAutomaticExtractDetail)
        ' Concatenar EntityCode de todas las notesReconciled
        Dim concatenatedEntityCodes As String = String.Join(", ", documentsReconciled.Select(Function(n) n.EntityCode))
        ' Se marca el detalle del extracto como conciliado
        extract.Reconciled = True
        extract.CodeNoteReconciled = concatenatedEntityCodes 'Se relaciona el código de las notas que fueron coinciliadas

        For Each documentReconciled In documentsReconciled
            ' Actualizar _listBankReconciliationAutomaticDetail con los códigos de los documentos concatenados
            Dim codes As String() = concatenatedEntityCodes.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
            For Each code As String In codes
                Dim separatedCode As String = code.Trim() 'Limpiamos los códigos separados
                _listBankReconciliationAutomaticDetail.Where(Function(x) x.EntityCode = separatedCode).ToList().ForEach(Sub(d) d.Reconciled = True)
            Next
            Dim automaticAssosiation As New BankReconciliationAutomaticAssociation
            With automaticAssosiation
                .BankReconciliationAutomaticDetail = documentReconciled
                .BankReconciliationAutomaticExtractDetail = extract
            End With
            _listAutomaticAssociation.Add(automaticAssosiation)
        Next

        ' Agregar el extracto conciliado a _listExtractDetailReconciled
        If _listExtractDetailReconciled Is Nothing Then
            _listExtractDetailReconciled = New List(Of BankReconciliationAutomaticExtractDetail)
            _listExtractDetailReconciled.Add(extract)
        Else
            _listExtractDetailReconciled.Add(extract)
        End If
        ' Buscar el extracto en _listBankReconciliationExtract que tenga el mismo Id que el extract actual
        Dim extractToRemove = _listBankReconciliationExtract.FirstOrDefault(Function(e) e.Id = extract.Id)
        If extractToRemove IsNot Nothing Then
            _listBankReconciliationExtract.Remove(extractToRemove)
        End If
    End Sub

    ''' <summary>
    ''' Maneja la conciliación de muchos documentos contra muchos extractos (N:M).
    ''' </summary>
    ''' <param name="documents">Lista de documentos a conciliar (BankReconciliationAutomaticDetail)</param>
    ''' <param name="extracts">Lista de extractos a conciliar (BankReconciliationAutomaticExtractDetail)</param>
    Private Sub HandleConciliationManyToMany(documents As List(Of BankReconciliationAutomaticDetail), extracts As List(Of BankReconciliationAutomaticExtractDetail))

        If documents Is Nothing OrElse documents.Count = 0 Then Exit Sub
        If extracts Is Nothing OrElse extracts.Count = 0 Then Exit Sub

        ' 1) Preparar concatenación de códigos de documentos (se usará en los extractos)
        Dim concatenatedEntityCodes As String = String.Join(", ", documents.
                                                        Where(Function(d) d IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(d.EntityCode)).
                                                        Select(Function(d) d.EntityCode).
                                                        Distinct())

        ' 2) Marcar extractos y asignar CodeNoteReconciled
        For Each ex In extracts
            If ex Is Nothing Then Continue For
            ex.Reconciled = True
            ex.CodeNoteReconciled = concatenatedEntityCodes
        Next

        ' 3) Marcar documentos como conciliados en memoria (_listBankReconciliationAutomaticDetail)
        '    y en la lista local que recibimos
        For Each doc In documents
            If doc Is Nothing Then Continue For
            doc.Reconciled = True

            ' Si existe en la lista principal, reflejar el cambio
            Dim docToUpdate = _listBankReconciliationAutomaticDetail _
                          ?.FirstOrDefault(Function(x) x IsNot Nothing AndAlso x.EntityCode = doc.EntityCode)
            If docToUpdate IsNot Nothing Then
                docToUpdate.Reconciled = True
            End If
        Next

        ' 4) Crear asociaciones N:M (evitar duplicados si ya existen)
        If _listAutomaticAssociation Is Nothing Then
            _listAutomaticAssociation = New List(Of BankReconciliationAutomaticAssociation)()
        End If

        For Each doc In documents
            If doc Is Nothing Then Continue For
            For Each ex In extracts
                If ex Is Nothing Then Continue For

                Dim exists As Boolean = _listAutomaticAssociation.Any(Function(a) _
                a IsNot Nothing AndAlso
                Object.ReferenceEquals(a.BankReconciliationAutomaticDetail, doc) AndAlso
                Object.ReferenceEquals(a.BankReconciliationAutomaticExtractDetail, ex))

                If Not exists Then
                    Dim assoc As New BankReconciliationAutomaticAssociation With {
                    .BankReconciliationAutomaticDetail = doc,
                    .BankReconciliationAutomaticExtractDetail = ex
                }
                    _listAutomaticAssociation.Add(assoc)
                End If
            Next
        Next

        ' 5) Mover extractos conciliados a _listExtractDetailReconciled
        If _listExtractDetailReconciled Is Nothing Then
            _listExtractDetailReconciled = New List(Of BankReconciliationAutomaticExtractDetail)()
        End If

        ' Evitar duplicados al agregar
        Dim existingIds As HashSet(Of Integer?) = New HashSet(Of Integer?)(
        _listExtractDetailReconciled.
            Where(Function(e) e IsNot Nothing).
            Select(Function(e) CType(e.Id, Integer?))
        )

        For Each ex In extracts
            If ex Is Nothing Then Continue For
            If Not existingIds.Contains(CType(ex.Id, Integer?)) Then
                _listExtractDetailReconciled.Add(ex)
            End If
        Next

        ' 6) Eliminar de _listBankReconciliationExtract los extractos conciliados.
        '    Usamos el mismo enfoque que en OneToMany, con HashSet de tuplas seguras.
        If _listBankReconciliationExtract IsNot Nothing Then
            Dim reconciledKeys As New HashSet(Of (ConsecutiveBank As String, DocumentType As Integer?, Nature As Byte?, Value As Decimal))(
            extracts.
                Where(Function(e) e IsNot Nothing).
                Select(Function(e) (e.ConsecutiveBank, e.DocumentType, e.Nature, e.Value))
        )

            _listBankReconciliationExtract.RemoveAll(Function(extract) _
            extract IsNot Nothing AndAlso
            reconciledKeys.Contains((extract.ConsecutiveBank, extract.DocumentType, extract.Nature, extract.Value)))
        End If
    End Sub


    ''' <summary>
    ''' Método para reconciliar las nuevas notas creadas
    ''' </summary>
    Private Async Sub ReconciledNewNote(listExtractDetail As List(Of BankReconciliationAutomaticExtractDetail))
        ' Obtenemos la nueva Nota
        Dim newDocument = Await GetNewNote()
        'Enviamos a conciliar los detalles que crearon la nota, junto con la misma
        If newDocument IsNot Nothing Then
            _listBankReconciliationAutomaticDetail.Add(newDocument)
            Dim itemsReconciled As Integer = _listExtractDetailReconciled.Count()
            Dim newNoteCode As String = newDocument.EntityCode
            ProcessNoteToReconciled(listExtractDetail, newDocument)
            'Validamos si se añadieron los nuevos items conciliados
            If _listExtractDetailReconciled.Count() > itemsReconciled Then
                Dim consecutives As String = String.Join(", ", listExtractDetail.Select(Function(e) e.ConsecutiveBank))
                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("NewNoteReconciled", NAME_MODULE), newNoteCode, consecutives)
                'Actualizamos los Datasource
                RefreshGrids()
                Me.CalculateDifference(False)
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("NewNoteNoReconciled", NAME_MODULE), newNoteCode)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoFoundNoteToReconciled", NAME_MODULE)
        End If
    End Sub

    ''' <summary>
    ''' Método que realiza el proceso para desmarcar los Conciliados
    ''' </summary>
    ''' <param name="codeDocumentReconciled"></param>
    Private Sub UnReconciledInfo(codeDocumentReconciled As String)
        ' Se buscan otros extractos coinciliados con la misma nota 
        Dim objsToNotReconciled = _listExtractDetailReconciled.Where(Function(x) x.CodeNoteReconciled = codeDocumentReconciled).ToList()

        ' Se descheckea las notas coinciliadas relacionadas
        Dim codes As String() = codeDocumentReconciled.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
        For Each code As String In codes
            Dim separatedCodeReconciled As String = code.Trim()
            _listBankReconciliationAutomaticDetail.Where(Function(x) x.Reconciled = True).ToList().ForEach(Sub(d)
                                                                                                               If d.EntityCode = separatedCodeReconciled Then d.Reconciled = False
                                                                                                           End Sub)
        Next

        If objsToNotReconciled IsNot Nothing Then
            'Eliminamos todas las asociaciones relacionadas con los extractos a desconciliar
            Dim extractsToRemove As New HashSet(Of BankReconciliationAutomaticExtractDetail)(objsToNotReconciled)
            _listAutomaticAssociation?.RemoveAll(Function(a) _
                a.BankReconciliationAutomaticExtractDetail IsNot Nothing AndAlso
                extractsToRemove.Contains(a.BankReconciliationAutomaticExtractDetail)
            )

            _listExtractDetailReconciled.RemoveAll(Function(x) objsToNotReconciled.Contains(x)) 'Se eliminan de la lista de conciliados
            objsToNotReconciled.ForEach(Sub(o)
                                            o.Reconciled = False
                                            o.CodeNoteReconciled = Nothing
                                        End Sub) ' Se desmarcan los detalles como conciliados
            _listBankReconciliationExtract.AddRange(objsToNotReconciled)  'Se agrega nuevamente a la lista de detalles pendientes

            'Actualización de las rejillas
            RefreshGrids()
        End If
    End Sub

    ''' <summary>
    ''' Genera el cierre de conciliación
    ''' </summary>
    Private Async Sub GenerateBankReconciliationClosing(bankAutomaticDetail As List(Of BankReconciliationAutomaticDetail), bankExtractDetail As List(Of BankReconciliationAutomaticExtractDetail))
        'Se calcula la diferencia a conciliar para realizar el cierre y dejarla en 0
        Me.CalculateDifference(True)

        'Obtenemos las partidas pendientes por conciliar
        Using model As New MBankConciliationAutomatic(CStr(MyTag))
            If _listPendingItemsToReconciled Is Nothing Then
                _listPendingItemsToReconciled = Await model.CreatePendingItemsToReconciled(bankAutomaticDetail, bankExtractDetail)
            Else
                _listPendingItemsToReconciled.AddRange(Await model.CreatePendingItemsToReconciled(bankAutomaticDetail, bankExtractDetail))
            End If
        End Using

        'Se agregan al DataSource correspondiente
        INDLcgBankStatements.HideControl(True) 'Se oculta el segmento de extracto bancario
        'Eliminamos los elementos pendientes 
        bankAutomaticDetail.RemoveAll(Function(x) Not x.Reconciled)
        bankExtractDetail.RemoveAll(Function(x) Not x.Reconciled)
        ' Actualizamos rejillas
        RefreshGrids()
        INDGcPendingItemsToReconciled.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Pasa registros al segmento de Pendientes por Conciliar
    ''' </summary>
    Private Async Sub MoveDocumentsToPendingItems(objsToMove As List(Of BankReconciliationAutomaticDetail), ByVal reconciledStatus As Byte)
        If objsToMove Is Nothing OrElse Not objsToMove.Any() Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoFoundDataToMove", NAME_MODULE)
            Return
        End If

        objsToMove.ForEach(Sub(x) x.ReconciledStatus = reconciledStatus)

        ' Obtener los nuevos elementos pendientes
        Using model As New MBankConciliationAutomatic(CStr(MyTag))
            Dim newPendingItems As List(Of PendingItemsToReconciled) = Await model.CreatePendingItemsToReconciled(objsToMove, Nothing)

            ' Validar si el resultado es Nothing
            If Not newPendingItems.Any() Then
                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("PendingItemsToReconciledNotCreated", NAME_MODULE)
                Return
            End If

            ' Agregar los elementos a la lista de pendientes
            If _listPendingItemsToReconciled Is Nothing Then
                _listPendingItemsToReconciled = newPendingItems
            Else
                _listPendingItemsToReconciled.AddRange(newPendingItems)
            End If
        End Using

        ' Remover elementos de la lista original y actualizar interfaces
        _listBankReconciliationAutomaticDetail.RemoveAll(Function(documents) objsToMove.Contains(documents))
        INDgcTreasury.RefreshDataSource()
        INDGcPendingItemsToReconciled.RefreshDataSource()
    End Sub


    ''' <summary>
    ''' Mueve el documento al segmento Libros de Bancos
    ''' </summary>
    Private Sub MoveToBankBook(objsToMove As List(Of PendingItemsToReconciled))
        If objsToMove Is Nothing OrElse Not objsToMove.Any() Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoFoundDataToMove", NAME_MODULE)
            Exit Sub
        End If
        For Each obj In objsToMove
            obj.DocumentDetail.ReconciledStatus = Nothing
            _listBankReconciliationAutomaticDetail.Add(obj.DocumentDetail)
        Next
        _listPendingItemsToReconciled.RemoveAll(Function(pendingItem) objsToMove.Contains(pendingItem))
        INDgcTreasury.RefreshDataSource()
        INDGcPendingItemsToReconciled.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Método para la conciliación manual
    ''' </summary>
    Private Async Function ManualReconciliation(documentDetails As List(Of BankReconciliationAutomaticDetail), extractDetails As List(Of BankReconciliationAutomaticExtractDetail)) As Task
        If Not documentDetails.Any() OrElse Not extractDetails.Any() Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoDataSelected", NAME_MODULE)
            Return
        End If
        ' Validar que las notas con recibos de caja deben ser conciliación 1 a 1
        Dim hasNoteWithCashReceipts = documentDetails.Any(Function(d) d.DocumentType = 3 AndAlso d.ListCashReceipts IsNot Nothing AndAlso d.ListCashReceipts.Any())
        If hasNoteWithCashReceipts AndAlso (documentDetails.Count > 1 OrElse extractDetails.Count > 1) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoteWithCashReceiptsRule", NAME_MODULE)
            Return
        End If
        Dim codes As String = String.Join(", ", documentDetails.Select(Function(d) d.EntityCode))
        Dim consecutives As String = String.Join(", ", extractDetails.Select(Function(e) e.ConsecutiveBank))
        If MessageIndigo.Show(String.Format(ResourceManager.GetString("ReconciliationMessage", NAME_MODULE), codes, consecutives), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Using model As New MBankConciliationAutomatic(CStr(MyTag))
                Dim result = Await model.ValidateManualReconciliation(documentDetails, extractDetails)
                If result.StatusCode = eStatusResult.WARNING Then
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Return
                Else
                    If Not result.StateResult Then
                        If MessageIndigo.Show(String.Format(ResourceManager.GetString("ValidationManualReconciliation", NAME_MODULE), result.Message), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                            Return
                        End If
                    End If
                End If
            End Using
            ' Función para realizar la Conciliación Manual
            PerformManualConciliation(documentDetails, extractDetails)
        End If
    End Function

    ''' <summary>
    ''' Lleva a cabo el proceso de conciliación manual
    ''' </summary>
    ''' <param name="documentDetails"></param>
    ''' <param name="extractDetails"></param>
    Private Sub PerformManualConciliation(documentDetails As List(Of BankReconciliationAutomaticDetail), extractDetails As List(Of BankReconciliationAutomaticExtractDetail))
        Try
            If documentDetails.Count = 1 AndAlso extractDetails.Count = 1 Then
                HandleConciliationOneToOne(extractDetails, documentDetails)
            ElseIf documentDetails.Count = 1 AndAlso extractDetails.Count > 1 Then
                HandleConciliationOneToMany(documentDetails(0), extractDetails)
            ElseIf documentDetails.Count > 1 AndAlso extractDetails.Count = 1 Then
                HandleConciliationManyToOne(documentDetails, extractDetails(0))
            Else
                HandleConciliationManyToMany(documentDetails, extractDetails)
            End If

            ' Actualizar rejillas
            RefreshGrids()
            ' Mostrar mensaje de éxito
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ManualReconciliationSuccess", NAME_MODULE)

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = $"Error en conciliación manual: {ex.Message}"
        End Try
    End Sub

    ''' <summary>
    ''' Habilitar o deshabilitar el botón de imprimir y visualizar
    ''' </summary>
    ''' <param name="show"></param>
    Private Sub EnablePrintButton(ByVal show As Boolean)
        If show Then
            Me.BarraBotones.SetDocuments(_BankReconciliationAutomatic.Id)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
            Me.BarraBotones.PrintReport(PrintReportAction.None, _BankReconciliationAutomatic.Id, 0, {_BankReconciliationAutomatic.Id})
        Else
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = True
        End If
    End Sub

#End Region

#Region "Handlers"


#Region "Load"

    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub FrmBankConciliationAutomatic_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        _presenter = New PBankConciliationAutomatic(Me)
        Await _presenter.LoadDefinitionLayout()
        _presenter.GetSequence()
        '******************************
        BarraBotones.StatusRecordVisible = True
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue

        LoadNoteTypeDataSource()
        LoadDocumentTypeDataSource()

        _idOperativeUnit = BarraBotones.OperatingUnitValue
        IndigoGridControl1.RefreshGrid(INDgcTreasury)
        IndigoGridControl1.RefreshGrid(INDGcBankStatements)

        _actionsForNotes.Add(eAcciones.CreateTreasuryNote)
        IndigoGridViewBankStatement.SetListAcction(INDGvBankStatements, _actionsForNotes)
        INDGvBankStatements.Columns.ColumnByName("colActions").Visible = False

        IndigoGridViewPendingItems.MoreInfoColunmns(INDGvPendingItemsToReconciled)

        AdditionalControlPanel.Controls.Add(_ctrBankConciliation)
        _ctrBankConciliation.Dock = DockStyle.Fill

        Deshacer()
        LoadStatus()
    End Sub

    ''' <summary>
    ''' Proporciona las opciones de Tipo de Nota
    ''' </summary>
    Private Sub LoadNoteTypeDataSource()
        Dim fillingNoteType As New List(Of ViewNoteType)
        fillingNoteType.Add(New ViewNoteType With {.Id = 1, .Name = "Nota de gastos bancarios"})
        fillingNoteType.Add(New ViewNoteType With {.Id = 2, .Name = "Terceros pendientes por identificar"})

        INDSleNoteType.Properties.DataSource = fillingNoteType
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

        INDrepSleDocumentType.DataSource = fillingDocument
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _idOperativeUnit = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        _blockRecord = Nothing
        _BankReconciliationAutomatic = Nothing
        _listBankReconciliationAutomaticDetail = Nothing
        BankReconciliationAutomaticExtractDetail = Nothing
        _listBankReconciliationExtract = Nothing
        _varImp = Nothing
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBankConciliationAutomatic_Activated(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.INDrepSleDetailDocumentType.DataSource = FillingDocumentType
        Me.INDrepSleExtractDetailDocumentType.DataSource = FillingDocumentType
        Me.INDrepSleDetailNature.DataSource = FillingNature
        Me.INDrepSleExtractDetailNature.DataSource = FillingNature
        INDbtnCode.Focus()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Async Sub FrmBankConciliationAutomatic_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Captura la tecla enter, para buscar un regitro por codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me._sequense Is Nothing OrElse Me._sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Await LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Me.NewEntity()
                Else
                    Await LoadControls()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' MultiSelect Mode 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvBankReconciliationDetail_SelectionChangedEventArgs(sender As Object, e As EventArgs) Handles INDgvTreasury.SelectionChanged
        Dim Rows As New ArrayList()
        Dim AutomaticDetailSelected As New BankReconciliationAutomaticDetail
        EntityCodeList = New List(Of String)
        ' Add the selected rows to the list.
        Dim selectedRowHandles As Int32() = INDgvTreasury.GetSelectedRows()
        Dim I As Integer
        For I = 0 To selectedRowHandles.Length - 1
            Dim selectedRowHandle As Int32 = selectedRowHandles(I)
            If (selectedRowHandle >= 0) Then
                Rows.Add(INDgvTreasury.GetDataRow(selectedRowHandle))
                Dim automaticDetailRow = INDgvTreasury.GetRowCellValue(selectedRowHandle, "EntityCode")
                EntityCodeList.Add(automaticDetailRow.ToString())
            End If
        Next
    End Sub


    ''' <summary>
    ''' Evento que se dispara al presionar escape al control del valor mínimo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDseExtractValue_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDseExtractValue.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDgcTreasury.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuenta bancaria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntityBankAccount_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntityBankAccount.QueryPopUp
        If EntityBankAccountXpo Is Nothing Then
            _presenter.InitializeEntityBankAccount()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el mas del control de cuenta bancaria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntityBankAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntityBankAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(628, Nothing, True)
            _presenter.InitializeEntityBankAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento para realizar filtro acorde al documento seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvTreasury_RowClick(sender As Object, e As RowClickEventArgs) Handles INDgvTreasury.RowClick
        If e.Button = MouseButtons.Left Then
            Dim view As GridView = TryCast(sender, GridView)
            If view IsNot Nothing Then
                Dim row As Object = view.GetRow(e.RowHandle)
                If row IsNot Nothing AndAlso row.Reconciled Then
                    ' Obtenemos el valor para filtrar
                    Dim filter As String = DirectCast(view.GetRowCellValue(e.RowHandle, "EntityCode"), String)
                    ' Filtra en la otra rejilla usando el valor obtenido
                    If filter IsNot Nothing Then
                        _filterToGetItemRelated = filter
                        INDGvConciliation.RefreshData()
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento para realizar selección masiva en Tesorería
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgcTreasuryAndINDGcBankStatements_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcTreasury.MouseDoubleClick, INDGcBankStatements.MouseDoubleClick
        Dim control = TryCast(sender, GridControl)
        If control Is Nothing Then Exit Sub
        Dim view = TryCast(control.DefaultView, GridView)
        If view Is Nothing Then Exit Sub

        Dim hitPoint = view.CalcHitInfo(e.Location)

        If hitPoint.Column IsNot Nothing AndAlso hitPoint.Column.FieldName = "Checked" Then
            Dim existeNoChecked As Boolean = False

            ' Verifica si hay al menos un registro visible no chequeado
            For i As Integer = 0 To view.DataRowCount
                Dim rowHandle As Integer = view.GetVisibleRowHandle(i)
                If view.IsDataRow(rowHandle) Then
                    Dim value = view.GetRowCellValue(rowHandle, "Checked")
                    If value Is Nothing OrElse Not CBool(value) Then
                        existeNoChecked = True
                        Exit For
                    End If
                End If
            Next

            ' Si hay alguno sin check, marcar todos; si no, desmarcar todos
            For i As Integer = 0 To view.DataRowCount
                Dim rowHandle As Integer = view.GetVisibleRowHandle(i)
                If view.IsDataRow(rowHandle) Then
                    view.SetRowCellValue(rowHandle, "Checked", existeNoChecked)
                End If
            Next
            view.RefreshData()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar Click Aceptar en el PopUp 
    ''' Para crear la Nota de Tesorería
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If TreasuryNoteType = 1 Then ' Nota de Gastos
            Await HandleExpenseNoteAsync()
        ElseIf TreasuryNoteType = 2 Then ' Nota Terceros Pendientes por Identificar
            ' Creamos la nota
            AsyncLoader(True)
            Dim response = Await CreateNote(_listBankReconciliationExtract, _BankReconciliationAutomatic, Nothing)
            If response.StateResult Then
                _extractFromNewNote.Clear()
                _extractFromNewNote = _listBankReconciliationExtract.Where(Function(x) x.Checked).ToList()
                SaveAndConfirmTreasuryNote(response.ObjectEmbbeded)
                'Se desmarcan los detalles seleccionados para la nota
                _listBankReconciliationExtract.Where(Function(x) x.Checked).ToList().ForEach(Sub(x) x.Checked = False)
                INDGcBankStatements.RefreshDataSource()
            Else
                ShowMessages(response.Message)
                AsyncLoader(False)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar Click Aceptar en el PopUp 
    ''' Para crear la Nota de Tesorería
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAccept_Click(sender As Object, e As EventArgs) Handles INDBtnAccept.Click

        If String.IsNullOrEmpty(Comments) Then
            Mensaje(EeventViewerImages.Advertencia) = "El campo Comentarios no está diligenciado"
            INDMeComments.Focus()
            Exit Sub
        End If

        Dim detallesSeleccionados = _listBankReconciliationAutomaticDetail.Where(Function(x) x.Checked).ToList()

        For Each detalle In detallesSeleccionados
            detalle.Comments = Comments
        Next

        INDgvTreasury.RefreshData()

        INDPcComments.Hide()

    End Sub

#End Region

#Region "RowStyle"
    ''' <summary>
    ''' Evento para cambiar el color de los detalles conciliados relacionados con el documento de libro de bancos clickeado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvConciliation_RowStyle(sender As Object, e As RowStyleEventArgs) Handles INDGvConciliation.RowStyle
        If _filterToGetItemRelated IsNot Nothing Then
            ' Verificamos que la fila es de datos
            If e.RowHandle >= 0 Then
                ' Obtener el valor de la columna "CodeNoteReconciled"
                Dim codeNoteReconciled As String = TryCast(INDGvConciliation.GetRowCellValue(e.RowHandle, "CodeNoteReconciled"), String)

                ' Verificamos si el valor coincide con el filtro global
                If codeNoteReconciled IsNot Nothing Then
                    'Se busca de esta forma el código del documento ya que pueden ser varios los relacionados
                    If codeNoteReconciled = _filterToGetItemRelated Or codeNoteReconciled.Contains(", " & _filterToGetItemRelated) Or codeNoteReconciled.Contains(_filterToGetItemRelated & ",") Then
                        ' Aplicar el estilo para resaltar la fila
                        e.Appearance.BackColor = Color.LightGreen
                        e.Appearance.BackColor2 = Color.White
                        e.HighPriority = True
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que pinta de color diferente los documentos no Conciliados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvTreasury_RowStyle(sender As Object, e As RowStyleEventArgs) Handles INDgvTreasury.RowStyle
        If _listExtractDetailReconciled IsNot Nothing AndAlso _listExtractDetailReconciled.Any() Then
            'Verificamos que la fila es de datos
            If e.RowHandle >= 0 Then
                Dim reconciled As Boolean = INDgvTreasury.GetRowCellValue(e.RowHandle, "Reconciled")
                'Verificamos las notas no conciliadas
                If Not reconciled Then
                    e.Appearance.BackColor = Color.White
                    e.Appearance.BackColor2 = Color.LightPink
                    e.HighPriority = True
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Maneja la obtención de la lista de recibos de caja para el nivel detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvTreasury_MasterRowGetChildList(sender As Object, e As MasterRowGetChildListEventArgs) Handles INDgvTreasury.MasterRowGetChildList
        Try
            ' Obtener el objeto BankReconciliationAutomaticDetail de la fila maestra
            Dim detail As BankReconciliationAutomaticDetail = TryCast(INDgvTreasury.GetRow(e.RowHandle), BankReconciliationAutomaticDetail)

            If detail IsNot Nothing Then
                INDgvTreasury.OptionsDetail.ShowDetailTabs = False
                ' Solo mostrar el detalle si el DocumentType = 3 (Nota)
                If detail.DocumentType = 3 Then
                    ' Si ListCashReceipts es Nothing, asignar una lista vacía para evitar errores
                    If detail.ListCashReceipts Is Nothing Then
                        e.ChildList = New List(Of CashReceipts)()
                    Else
                        e.ChildList = detail.ListCashReceipts
                    End If
                Else
                    ' Para otros tipos de documento, no mostrar el nivel detalle
                    e.ChildList = Nothing
                End If
            Else
                e.ChildList = Nothing
            End If
        Catch ex As Exception
            e.ChildList = Nothing
        End Try
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la cuenta bancaria o de la fecha del documento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleEntityBankAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityBankAccount.EditValueChanged, INDdteDocumentDate.EditValueChanged
        If EntityBankAccountId IsNot Nothing AndAlso DocumentDate IsNot Nothing Then
            ' Validar si los valores cambiaron
            If EntityBankAccountId = _lastEntityBankAccountId AndAlso DocumentDate = _lastDocumentDate Then
                Return
            End If

            ' Actualizar valores almacenados para comparar la próxima vez
            _lastEntityBankAccountId = EntityBankAccountId
            _lastDocumentDate = DocumentDate

            Using model As New MBankConciliationAutomatic(CStr(Me.Tag))
                Dim EntityBank = Await model.GetEntityBankAccountById(EntityBankAccountId) ' Consultamos la entidad Bancaria para obtener la moneda
                If EntityBank IsNot Nothing Then
                    SetCurrencyUI(EntityBank.CurrencyAbbreviation)
                End If
            End Using

            AssigningCtrValues()
            _month = Month(DocumentDate)
            _year = Year(DocumentDate)

            Await GetBankReconciliationDetails()

            If _listBankReconciliationExtract IsNot Nothing Then
                Dim DetailDebitValue As Decimal = 0
                Dim DetailCreditValue As Decimal = 0
                DetailDebitValue = _listBankReconciliationExtract.Where(Function(d) d.Nature = 1 AndAlso Not d.Reconciled).Sum(Function(d) d.Value)
                DetailCreditValue = _listBankReconciliationExtract.Where(Function(d) d.Nature = 2 AndAlso Not d.Reconciled).Sum(Function(d) d.Value)
                ExtractValueMovement = DetailDebitValue + DetailCreditValue
            End If

            CalculateLastBalance()
            UpdateSummaryDebitCredit(False)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del libro de bancos, o el valor de los extractos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDseEndEntityBankAccountValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDseEndEntityBankAccountValue.EditValueChanged, INDseExtractValue.EditValueChanged
        Me.CalculateDifference(False)
    End Sub

    ''' <summary>
    ''' Evento para eliminar de Coinciliados un elemento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvConciliation_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles INDGvConciliation.CellValueChanged
        Dim objToNotReconciled = _listExtractDetailReconciled.Where(Function(x) Not x.Reconciled).FirstOrDefault()
        If objToNotReconciled IsNot Nothing Then
            UnReconciledInfo(objToNotReconciled.CodeNoteReconciled)
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el estado de un detalle de libro de bancos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDrepSelectOption.EditValueChanging
        If e IsNot Nothing Then
            ' Actualizar el ExtractValueMovement si es necesario
            If _listBankReconciliationExtract IsNot Nothing Then
                ExtractValueMovement = _listBankReconciliationExtract.Sum(Function(d) d.Value)
            End If

            ' Recalcular la diferencia usando la función centralizada que considera:
            ' - Items visibles en rejillas
            ' - Items pendientes (no descartados)
            ' - Fórmula de cierre si IsProcessed = 2
            CalculateDifference(IsProcessed = 2)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de un detalle del extracto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepTxtExtractDetailValue_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDrepTxtExtractDetailValue.EditValueChanging
        If e IsNot Nothing Then
            Dim value As Decimal = 0
            If Not Decimal.TryParse(e.NewValue.ToString().Replace(indigo.Culture.NumberFormat.CurrencyGroupSeparator, indigo.Culture.NumberFormat.NumberDecimalSeparator), Globalization.NumberStyles.AllowDecimalPoint, indigo.Culture, value) Then
                e.Cancel = True
                Exit Sub
            End If

            If value <= 0 Then
                e.Cancel = True
                Exit Sub
            End If

            ' Recalcular la diferencia usando la función centralizada que considera:
            ' - Items visibles en rejillas
            ' - Items pendientes (no descartados)
            ' - Fórmula de cierre si IsProcessed = 2
            CalculateDifference(IsProcessed = 2)
        End If
    End Sub

#End Region

#Region "ValidatingEditor"

    Private Sub INDgvBankReconciliationAutomaticExtractDetail_ValidatingEditor(sender As Object, e As BaseContainerValidateEditorEventArgs) Handles INDGvBankStatements.ValidatingEditor
        Dim view As ColumnView = sender
        Dim column As GridColumn = If(TryCast(e, EditFormValidateEditorEventArgs)?.Column, view.FocusedColumn)
        If column.FieldName <> "DocumentDate" Then Exit Sub
        If CDate(e.Value).AsDate > DocumentDate.AsDate Then
            e.Valid = False
        End If
    End Sub

#End Region

#Region "ColumnFilterChanged"
    Private Sub INDgvBankReconciliationDetail_ColumnFilterChanged(sender As Object, e As EventArgs) Handles INDgvTreasury.ColumnFilterChanged
        filterTimer.Stop()
        filterTimer.Start()
    End Sub

    Private Sub INDGvBankStatements_ColumnFilterChanged(sender As Object, e As EventArgs) Handles INDGvBankStatements.ColumnFilterChanged
        filterTimer.Stop()
        filterTimer.Start()
    End Sub

    Private Sub filterTimer_Tick(sender As Object, e As EventArgs) Handles filterTimer.Tick
        filterTimer.Stop()
        UpdateSummaryDebitCredit(True)
    End Sub
#End Region

#Region "Leave or LostFocus"
    ''' <summary>
    ''' Evento para cerrar el PopUp notas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPcNoteType_Leave(sender As Object, e As EventArgs) Handles INDPcNoteType.Leave
        INDPcNoteType.Hide()
    End Sub

    ''' <summary>
    ''' Evento para cerrar el PopUp de comentarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPcComments_Leave(sender As Object, e As EventArgs) Handles INDPcComments.Leave
        INDPcComments.Hide()
    End Sub

    ''' <summary>
    ''' Evento para evitar resaltar detalles de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvTreasury_LostFocus(sender As Object, e As EventArgs) Handles INDgvTreasury.LostFocus
        INDGvConciliation.Appearance.Reset()
        _filterToGetItemRelated = Nothing
    End Sub

#End Region


#End Region

#Region "BarButtons"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _BankReconciliationAutomatic.Status = 1
        _varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opción de Coinciliar
    ''' </summary>
    Private Async Sub BarraBotones_ClickConciliar() Handles BarraBotones.ClickConciliar
        IsProcessed = 1
        UpdateCloseReconciliationButtonText()
        _listBankReconciliationExtract.AddRange(_listExtractDetailReconciled)
        Await FindCoincidences(_listBankReconciliationExtract, _listBankReconciliationAutomaticDetail, _listAutomaticAssociation)
    End Sub

    ''' <summary>
    ''' Ejecuta la opción de Cerrar Conciliación
    ''' </summary>
    Private Sub BarraBotones_ClickCerrarConciliación() Handles BarraBotones.ClickCerrarConciliacion
        If IsProcessed = 2 Then
            ' Si ya está cerrada, se abre para edición
            IsProcessed = 1
            OpenBankReconciliation()
        Else
            ' Si no está cerrada, se procede a cerrar
            IsProcessed = 2
            GenerateBankReconciliationClosing(_listBankReconciliationAutomaticDetail, _listBankReconciliationExtract)
        End If
        UpdateCloseReconciliationButtonText()
    End Sub

    ''' <summary>
    ''' Evento para desconfirmar la Conciliación Bancaria
    ''' </summary>
    Private Async Sub BarraBotones_ClickDeconfirmar() Handles BarraBotones.Click_Desconfirmar
        Using mCierre As New MCloseMonth(Me.Tag)
            Dim closedMonth = Await mCierre.GetMonth(Month(DocumentDate), Year(DocumentDate))
            If closedMonth?.Status Then
                If MessageIndigo.Show(ResourceManager.GetString("UnconfirmBankReconciliationAutomatic", NAME_MODULE), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    AsyncLoader(True)
                    Status = 1
                    ToolbarWithBankReconciliationOpen()
                    BarraBotones_ClickCerrarConciliación()
                    AsyncLoader(False)
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ClosedMonthForBankReconciliation", NAME_MODULE)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Prepara el Toolbar cuando la Conciliación Bancaria Automática se encuentra abierta
    ''' </summary>
    Private Sub ToolbarWithBankReconciliationOpen()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Conciliar) = False
        ReadOnlyControls(False)
    End Sub

    ''' <summary>
    ''' Actualiza el texto del botón Cerrar/Editar Conciliación según el estado de IsProcessed
    ''' </summary>
    Private Sub UpdateCloseReconciliationButtonText()
        If IsProcessed = 2 Then
            BarraBotones.SetCloseReconciliationButtonText("Editar Conciliación")
        Else
            BarraBotones.SetCloseReconciliationButtonText("Cerrar conciliación")
        End If
    End Sub

    ''' <summary>
    ''' Abre la conciliación para permitir edición
    ''' </summary>
    ''' <remarks>Restaura los elementos pendientes a sus rejillas originales, excepto los descartados</remarks>
    Private Async Sub OpenBankReconciliation()
        If _listPendingItemsToReconciled Is Nothing OrElse Not _listPendingItemsToReconciled.Any() Then
            ' Si no hay elementos pendientes, solo mostrar el segmento de extracto bancario
            INDLcgBankStatements.HideControl(False)
            Return
        End If

        ' Filtrar elementos a restaurar (todos excepto los descartados con ReconciledStatus = 2)
        Dim itemsToRestore = _listPendingItemsToReconciled.Where(Function(x) x.ReconciledStatus <> 2).ToList()

        If itemsToRestore.Any() Then
            Using model As New MBankConciliationAutomatic(CStr(MyTag))
                ' Separar los elementos pendientes en documentos y extractos
                Dim splitResult = Await model.SplitPendingItemsToReconciled(itemsToRestore)

                ' Restaurar documentos a la lista de detalle automático
                If splitResult.Item1 IsNot Nothing AndAlso splitResult.Item1.Any() Then
                    For Each document In splitResult.Item1
                        ' Limpiar el ReconciledStatus
                        document.ReconciledStatus = Nothing
                        ' Agregar el documento a la lista
                        If _listBankReconciliationAutomaticDetail Is Nothing Then
                            _listBankReconciliationAutomaticDetail = New List(Of BankReconciliationAutomaticDetail)
                        End If
                        _listBankReconciliationAutomaticDetail.Add(document)
                    Next
                End If

                ' Restaurar extractos a la lista de extractos bancarios
                If splitResult.Item2 IsNot Nothing AndAlso splitResult.Item2.Any() Then
                    If _listBankReconciliationExtract Is Nothing Then
                        _listBankReconciliationExtract = splitResult.Item2
                    Else
                        _listBankReconciliationExtract.AddRange(splitResult.Item2)
                    End If
                End If

                ' Remover los elementos restaurados de la lista de pendientes
                _listPendingItemsToReconciled.RemoveAll(Function(x) itemsToRestore.Contains(x))
            End Using
        End If

        ' Mostrar el segmento de extracto bancario
        INDLcgBankStatements.HideControl(False)

        ' Actualizar rejillas
        RefreshGrids()
        INDGcPendingItemsToReconciled.RefreshDataSource()

        ' Recalcular la diferencia
        Me.CalculateDifference(False)
    End Sub

    ''' <summary>
    ''' Barras the botones_GuardarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If DifferenceReconcile <> 0 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DifferenceReconcile", NAME_MODULE)
            Exit Sub
        End If
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _BankReconciliationAutomatic.Status = 2
            _varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _BankReconciliationAutomatic.Status = 1
        _varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ActualizarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If DifferenceReconcile <> 0 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DifferenceReconcile", NAME_MODULE)
            Exit Sub
        End If
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _BankReconciliationAutomatic.Status = 2
            _varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Click Boton de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _BankReconciliationAutomatic.Status = 3
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _BankReconciliationAutomatic.Id, 0, _BankReconciliationAutomatic.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.TreasurySequenceDetail IsNot Nothing Then
                If Not Me._sequense.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

#Region "ExpenceNote"
    ''' <summary>
    ''' Funcion para generar la nota de tesoreria
    ''' </summary>
    ''' <returns></returns>
    Private Async Function HandleExpenseNoteAsync() As Task
        AsyncLoader(True)

        Dim entityBankAccountId = bankReconciliationAutomatic.ObjectEmbbeded.EntityBankAccountId

        Try
            ' Obtener reglas de reconocimiento
            Dim recognitionRules = Await GetRules(entityBankAccountId)

            Dim result = (From rules In recognitionRules.ObjectEmbbeded
                          From extract In _listBankReconciliationExtract
                          Where String.Compare(rules.DescriptionTransaction, extract.DescriptionTransaction, StringComparison.OrdinalIgnoreCase) = 0 AndAlso
                            rules.TypeOfItemPendingInReconciliation = 1
                          Select rules, extract).Distinct.ToList()
            'Almacenamos los detalles del extracto con los que se creó la nota
            _extractFromNewNote.Clear()
            _extractFromNewNote = result.Select(Function(x) x.extract).Distinct.ToList()
            'Obtenemos las reglas de reconocimiento del banco
            Dim recognitionRulesList = result.Select(Function(x) x.rules).Distinct.ToList()

            ' Verificar si existen reglas de reconocimiento
            If Not recognitionRulesList.Any() Then
                ShowMessages("No se encuentran reglas de reconocimiento para generar la Nota")
                Return
            End If

            ' Crear y guardar la nota
            Dim response = Await CreateNote(_extractFromNewNote, _BankReconciliationAutomatic, recognitionRulesList)
            If response.StateResult Then
                SaveAndConfirmTreasuryNote(response.ObjectEmbbeded)
            Else
                ShowMessages(response.Message)
            End If
        Catch ex As Exception
            ShowMessages("Ocurrió un error al generar la nota de gastos: " & ex.Message)
            Throw
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Guarda y confirma la Nota de Tesorería
    ''' </summary>
    Private Async Sub SaveAndConfirmTreasuryNote(ByVal treasuryNote As TreasuryNote)
        Try
            Dim treasurySequence = Await GetSequence()
            Dim idSequence = treasurySequence.ObjectEmbbeded
            'Validar la secuencia
            If treasurySequence.StateResult = False Then
                ShowMessages(treasurySequence.Message)
                Return
            End If

            Dim resultSave = Await SaveTreasuryNoteAsync(treasuryNote, idSequence)

            If resultSave.StateResult Then

                Dim confirmNote = Await ConfirmTreasuryNote(resultSave.ObjectEmbbeded.Id, idSequence) 'Confirmacion de la nota
                Dim typeJournal As ActionResult(Of JournalVoucherTypes)
                If confirmNote.StateResult Then
                    Using modelType As New MDocumentType(Me.Tag)
                        typeJournal = modelType.GetJournalVoucherById(CType(confirmNote.MessageResult(0), Integer))
                    End Using
                    Dim consecutive = confirmNote.ObjectEmbbeded

                    Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmSatisfactory"), resultSave.ObjectEmbbeded.Code,
                                                                        String.Concat(typeJournal.ObjectEmbbeded.Code, " - ", typeJournal.ObjectEmbbeded.Name), String.Concat(consecutive, " y se confirmó."))

                    'Conciliamos la nueva nota con los respectivos detalles del extracto
                    ReconciledNewNote(_extractFromNewNote)
                    AsyncLoader(False)
                Else
                    AsyncLoader(False)
                    If confirmNote.Message IsNot Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SavedButNotConfirmed", NAME_MODULE), resultSave.ObjectEmbbeded.Code, confirmNote.Message)
                    End If
                End If
            Else
                ShowMessages(resultSave.Message)
            End If
        Catch ex As Exception
            ShowMessages("Ocurrió un error al generar la nota de gastos: " & ex.Message)
            Throw
        Finally
            AsyncLoader(False)
        End Try
    End Sub


    ''' <summary>
    ''' Mostramos mensajes de advertencia
    ''' </summary>
    ''' <param name="message"></param>
    Private Sub ShowMessages(message As String)
        Mensaje(EeventViewerImages.Advertencia) = message
    End Sub
    ''' <summary>
    ''' Se  guarda la nota
    ''' </summary>
    ''' <param name="note"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Private Async Function SaveTreasuryNoteAsync(note As Object, idSequence As Integer) As Task(Of ActionResult(Of TreasuryNote))
        Using model As New MTreasuryNote(Me.MyTag.ToString())
            Return Await model.SaveTreasuryNote(note, 0, idSequence)
        End Using
    End Function
    ''' <summary>
    ''' Confirmacion de la nota
    ''' </summary>
    ''' <param name="_treasuryNoteId"></param>
    ''' <param name="_idCurrentSequence"></param>
    ''' <returns></returns>
    Private Async Function ConfirmTreasuryNote(_treasuryNoteId As Integer, _idCurrentSequence As Integer?) As Task(Of ActionResult(Of String))
        Using model As New MTreasuryNote(Me.MyTag.ToString())
            Return Await model.ConfirmTreasuryNote(_treasuryNoteId, _idCurrentSequence)
        End Using
    End Function

    ''' <summary>
    ''' Funcion que obtiene  las reglas asociadas a los bancos 
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Private Async Function GetRules(entityId As Integer) As Task(Of ActionResult(Of List(Of BankAutomaticRecognitionRules)))
        Try
            Using model As New MBankConciliationAutomatic(Me.Tag.ToString())

                Return Await model.GetBankAutomaticRecognitionRules(entityId)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Se crea la nota
    ''' </summary>
    ''' <param name="BankStatement"></param>
    ''' <param name="_BankReconciliationAutomatic"></param>
    ''' <param name="RecognitionRulesList"></param>
    ''' <returns></returns>
    Private Async Function CreateNote(BankStatement As List(Of BankReconciliationAutomaticExtractDetail), _BankReconciliationAutomatic As BankReconciliationAutomatic,
                                      RecognitionRulesList As List(Of BankAutomaticRecognitionRules)) As Task(Of ActionResult(Of TreasuryNote))
        Try
            Using model As New MBankConciliationAutomatic(Me.Tag.ToString())
                Return Await model.MakeObjectTreasuryNote(RecognitionRulesList, _BankReconciliationAutomatic, BankStatement, Me._idOperativeUnit)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Se obtiene la secuencia
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetSequence() As Task(Of ActionResult(Of Integer?))
        Using model As New MBankConciliationAutomatic(Me.Tag.ToString())
            Return Await model.GetTreasurySecuence(_idFormTreasuryNote, Me._idOperativeUnit)
        End Using
    End Function

    ''' <summary>
    ''' Evento para manejar el PopUp de acciones 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvTreasury_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDgvTreasury.PopupMenuShowing
        If e.HitInfo.RowHandle < 0 Then Exit Sub

        INDBbiComment.Visibility = BarItemVisibility.Never
        INDBbiManualReconciliation.Visibility = BarItemVisibility.Never
        INDBbiPendingToReconciled.Visibility = BarItemVisibility.Never
        INDBbiDiscard.Visibility = BarItemVisibility.Never

        Dim checkedItems = _listBankReconciliationAutomaticDetail.Where(Function(x) x.Checked).ToList()

        If Not checkedItems.Any() Then Exit Sub

        INDBbiComment.Visibility = BarItemVisibility.Always

        ' Mostrar botón de conciliación manual si algún elemento Checkeado no está Conciliado 
        If IsProcessed = 1 AndAlso checkedItems.All(Function(x) Not x.Reconciled) Then
            INDBbiManualReconciliation.Visibility = BarItemVisibility.Always
            INDBbiPendingToReconciled.Visibility = BarItemVisibility.Always
            INDBbiDiscard.Visibility = BarItemVisibility.Always
        End If

        Dim view As GridView = CType(sender, GridView)
        INDPopupActions.Manager = BarManager1
        INDPopupActions.ShowPopup(view.GridControl.PointToScreen(e.Point))
    End Sub

    ''' <summary>
    ''' Evento para manejar el Popup de acciones de la rejilla de Pendientes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvPendingItemsToReconciled_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDGvPendingItemsToReconciled.PopupMenuShowing
        If e.HitInfo.RowHandle < 0 Then Exit Sub

        INDBbiMoveBankBook.Visibility = BarItemVisibility.Never

        Dim checkedItems = _listPendingItemsToReconciled.Where(Function(x) x.Checked AndAlso x.Origin = 1).ToList()

        If Not checkedItems.Any() Then Exit Sub

        INDBbiMoveBankBook.Visibility = BarItemVisibility.Always

        Dim view As GridView = CType(sender, GridView)
        INDPopupPendingActions.Manager = BarManager2
        INDPopupPendingActions.ShowPopup(view.GridControl.PointToScreen(e.Point))
    End Sub

    ''' <summary>
    ''' Evento para manejar las acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub BarButtonItem_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiComment.ItemClick, INDBbiPendingToReconciled.ItemClick, INDBbiDiscard.ItemClick, INDBbiMoveBankBook.ItemClick, INDBbiManualReconciliation.ItemClick
        Dim clickedButton As BarButtonItem = CType(e.Item, BarButtonItem)

        Dim itemsSelected As List(Of BankReconciliationAutomaticDetail) = _listBankReconciliationAutomaticDetail.Where(Function(x) x.Checked AndAlso Not x.Reconciled).ToList()
        Select Case clickedButton.Name
            Case "INDBbiComment"
                INDPcComments.Manager = INDBbiComment.Manager
                INDBbiComment.DropDownControl = INDPcComments
                ActionComments(itemsSelected)
            Case "INDBbiPendingToReconciled"
                MoveDocumentsToPendingItems(itemsSelected, 1)
            Case "INDBbiDiscard"
                MoveDocumentsToPendingItems(itemsSelected, 2)
            Case "INDBbiMoveBankBook"
                Dim objsToMove As List(Of PendingItemsToReconciled) = _listPendingItemsToReconciled.Where(Function(x) x.Checked AndAlso x.Origin = 1).ToList()
                MoveToBankBook(objsToMove)
            Case "INDBbiManualReconciliation"
                Dim extractDetails As List(Of BankReconciliationAutomaticExtractDetail) = _listBankReconciliationExtract.Where(Function(x) x.Checked AndAlso Not x.Reconciled).ToList()
                Await ManualReconciliation(itemsSelected, extractDetails)
        End Select
    End Sub

    Private Async Sub SimpleButton_Click(sender As Object, e As EventArgs) Handles INDSbComment.Click, INDSbPendingToReconciled.Click, INDSbDiscard.Click, INDSbMoveToBookBank.Click, INDSbManualReconciliation.Click
        Dim clickedButton As SimpleButton = CType(sender, SimpleButton)

        Dim itemsSelected As List(Of BankReconciliationAutomaticDetail) = _listBankReconciliationAutomaticDetail.Where(Function(x) x.Checked AndAlso Not x.Reconciled).ToList()
        Select Case clickedButton.Name
            Case "INDSbComment"
                ActionComments(itemsSelected, sender)
            Case "INDSbPendingToReconciled"
                MoveDocumentsToPendingItems(itemsSelected, 1)
            Case "INDSbDiscard"
                MoveDocumentsToPendingItems(itemsSelected, 2)
            Case "INDSbMoveToBookBank"
                Dim objsToMove As List(Of PendingItemsToReconciled) = _listPendingItemsToReconciled.Where(Function(x) x.Checked AndAlso x.Origin = 1).ToList()
                MoveToBankBook(objsToMove)
            Case "INDSbManualReconciliation"
                Dim extractDetails As List(Of BankReconciliationAutomaticExtractDetail) = _listBankReconciliationExtract.Where(Function(x) x.Checked AndAlso Not x.Reconciled).ToList()
                Await ManualReconciliation(itemsSelected, extractDetails)
        End Select
    End Sub

    ''' <summary>
    ''' Evento para ocultar o mostrar el campo valor del PopUp de Notas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleNoteType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleNoteType.EditValueChanged
        If TreasuryNoteType = 1 Then
            INDLciValue.HideControl(True)
        ElseIf TreasuryNoteType = 2 Then
            INDLciValue.HideControl(False)
        End If
    End Sub

    ''' <summary>
    ''' Evento que dispara el método de Refresh con merge inteligente
    ''' Obtiene nuevos documentos del servidor sin perder cambios locales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDlygEntityBankAccountInformation_CustomButtonClick(sender As Object, e As Docking2010.BaseButtonEventArgs) Handles INDlygEntityBankAccountInformation.CustomButtonClick
        Await RefreshBankReconciliationDetailsWithMerge()
    End Sub

#End Region

End Class

''' <summary>
''' Clase que permite identificar el tipo de Nota
''' </summary>
Public Class ViewNoteType
    Public Property Id As Integer
    Public Property Name As String
End Class
