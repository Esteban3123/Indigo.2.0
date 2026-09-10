'***********************************************************************
' Assembly         : Presentacion.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 10-04-2014
'
' Last Modified By :  
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Portfolio.MVP
Imports Presentation.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports System.Text
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources

#End Region

Public Class FrmProvisionRanges
    Implements IProvisionRanges



#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Portfolio"

#End Region

#Region "CRUD BASE"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        'If _searchMode = False Then
        '    'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        '    Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        'Else
        '    If indigo.UserViewMode = True Then
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        '    End If
        'End If
        'INDBtnCode.Focus()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        'If provisionRanges IsNot Nothing AndAlso provisionRanges.Id > -1 Then
        '    If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Try
        '            Using model As New MProvisionRanges(Me.Tag.ToString())
        '                provisionRanges.MarkAsDeleted()
        '                AsyncLoader(True)
        '                Dim result = Await model.DeleteProvisionRanges(provisionRanges)
        '                If result.StateResult = True Then
        '                    Await Me.DeleteDocumentIndexed()
        '                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
        '                    AsyncLoader(False)
        '                    _searchMode = False
        '                    Deshacer()
        '                Else
        '                    If result.MessageResult(0) = "-999" Then
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '                        AsyncLoader(False)
        '                    ElseIf result.MessageResult(0) = "-000" Then
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
        '                        AsyncLoader(False)
        '                    Else
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                        AsyncLoader(False)
        '                    End If
        '                End If
        '            End Using
        '        Catch ex As Exception
        '            Throw ex
        '            AsyncLoader(False)
        '        End Try
        '    End If
        'End If


        If Me.provisionRanges IsNot Nothing AndAlso Me.provisionRanges.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MProvisionRanges(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteProvisionRanges(Me.provisionRanges)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBtnCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        'If ValidateControls() = False Then

        '    Exit Sub
        'End If
        'If ValidateInitialFinal() = True Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ProvisionRangesInicialFinal", NAME_MODULE)
        '    Exit Sub
        'End If
        'AssigningValues()
        'Try
        '    Using model As New MProvisionRanges(Me.Tag.ToString())
        '        AsyncLoader(True)
        '        Dim Result = Await model.SaveProvisionRanges(provisionRanges, _idCurrentSequense)
        '        AsyncLoader(False)
        '        If Result.StateResult = True Then
        '            provisionRanges = Result.ObjectEmbbeded
        '            If provisionRanges.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '                'Se descarta la secuencia numerica usada
        '                If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
        '                    Me.DicSequense(Me._sequense.PortfolioSequenceDetail(0).Id).RemoveAt(0)
        '                End If
        '                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Me.provisionRanges.Code)
        '            Else
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
        '            End If
        '            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '            _searchMode = False
        '            Me.Deshacer()
        '        Else
        '            If Result.MessageResult(0) = ErrorConcurrencia Then
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '                AsyncLoader(False)
        '            Else
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                AsyncLoader(False)
        '            End If
        '        End If
        '    End Using
        'Catch ex As Exception
        '    Throw ex
        '    AsyncLoader(False)
        'End Try



        If Not ValidateControls() Then
            Exit Sub
        End If
        If ValidateInitialFinal() = True Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ProvisionRangesInicialFinal", NAME_MODULE)
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MProvisionRanges(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of ProvisionRanges) = Await Model.SaveProvisionRanges(Me.provisionRanges, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If provisionRanges.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.provisionRanges = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            NewProvisionRanges()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 50}, New ColumnInfo With {.Caption = "Inicial", .FieldName = "Initial", .ColumnWidth = 150}, New ColumnInfo With {.Caption = "Final", .FieldName = "Final", .ColumnWidth = 150}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListProvisionRanges
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
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' establece el estado de los controles
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IProvisionRanges.ActionsOnControls
        Set(value As Boolean)
            INDLycProvisionRanges.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDSpinInitial.Enabled = value
            INDSpinFinal.Enabled = value
            BarraBotones.StatusRecordVisible = value
            INDLycProvisionRanges.EndUpdate()
            If value = True Then
                INDSpinInitial.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property
#End Region

#Region "Variable Globales Propiedades Interfaz"
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PortfolioSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' entidad de indicadores economicos
    ''' </summary>
    Dim provisionRanges As ProvisionRanges
    ''' <summary>
    ''' modelo de indicaores economicos
    ''' </summary>
    Dim model As MProvisionRanges
    ''' <summary>
    ''' presentador de indicadores economicos
    ''' </summary>
    Dim presenter As PProvisionRanges
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordPortfolio
    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' obtiene o establece el codigo
    ''' </summary>
    ''' <value>
    ''' The code.
    ''' </value>
    Public Property Code As String Implements IProvisionRanges.Code
        Get
            If INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBtnCode.Text
            End If
        End Get
        Set(value As String)
            INDBtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el numero final
    ''' </summary>
    ''' <value>
    ''' The final.
    ''' </value>
    Public Property Final As Integer Implements IProvisionRanges.Final
        Get
            Return INDSpinFinal.EditValue
        End Get
        Set(value As Integer)
            INDSpinFinal.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el numero inicial
    ''' </summary>
    ''' <value>
    ''' The initial.
    ''' </value>
    Public Property Initial As Integer Implements IProvisionRanges.Initial
        Get
            Return INDSpinInitial.EditValue
        End Get
        Set(value As Integer)
            INDSpinInitial.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el estado
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [status]; otherwise, <c>false</c>.
    ''' </value>
    Public Property Status As Boolean Implements IProvisionRanges.Status
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            If value = False Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IProvisionRanges.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequence As PortfolioSequence Implements IProvisionRanges.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As PortfolioSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PortfolioSequenceDetail In Me._sequence.PortfolioSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IProvisionRanges.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
#End Region

#Region "Metodos Funciones Propiedades"
    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Function NewProvisionRanges() As Task
        'provisionRanges = New ProvisionRanges()
        'If Me._sequense.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequense = Me._sequense.PortfolioSequenceDetail(0).Id
        '    ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me._sequense.PortfolioSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequense = Me._sequense.PortfolioSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '        Else
        '            Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '            Exit Sub
        '        End If
        '    End If
        '    If Not Me._sequense.Sequential Then
        '        If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '            If Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
        '                PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
        '            Else
        '                Using model As New MBlockRecordAndSequense(Me.Tag)
        '                    Me.DicSequense(Me._idCurrentSequense) = Await model.GetNumericSequenseGroup(Me._idCurrentSequense)
        '                End Using
        '                If Me.DicSequense(Me._idCurrentSequense) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
        '                    PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
        '        End If
        '    Else
        '        PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
        '    End If
        'End If




        provisionRanges = New ProvisionRanges() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
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
    End Function

    ''' <summary>
    ''' Metodo para preparar la barra de usuario cuando el registro es nuevo
    ''' </summary>
    ''' <param name="code">The code.</param>
    Private Sub PrepareToolbar(code As String)
        Me.Code = code
        Me.ActionsOnControls = True
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub

    ''' <summary>
    ''' Metodo para generar la indexacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), provisionRanges.Code, provisionRanges.Initial, provisionRanges.Final),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & provisionRanges.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), provisionRanges.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), provisionRanges.Code, provisionRanges.Initial, provisionRanges.Final)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), provisionRanges.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        'INDLycProvisionRanges.BeginUpdate()
        'BarraBotones.CleanAuditBasic()
        'ActionsOnControls = False
        'INDBtnCode.Text = String.Empty
        'INDSpinFinal.EditValue = 0
        'INDSpinInitial.EditValue = 0
        'Me.BarraBotones.StatusRecordVisible = False
        'provisionRanges = Nothing
        'DeleteBlockedRecord()
        'Me._doc = Nothing
        ' Me.BarraBotones.EnableBarItems()
        'Me.BarraBotones.DisableBarDocument()
        'INDLycProvisionRanges.EndUpdate()


        INDLycProvisionRanges.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        ActionsOnControls = False
        INDBtnCode.Text = String.Empty
        INDSpinFinal.EditValue = 0
        INDSpinInitial.EditValue = 0
        'Limpiar controles
        provisionRanges = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDLycProvisionRanges.EndUpdate()
        DeleteBlockedRecord()
    End Sub


    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With provisionRanges
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            If Initial < 0 Then
                .Initial = Initial * -1
            Else
                .Initial = Initial
            End If
            If Final < 0 Then
                .Final = Final * -1
            Else
                .Final = Final
            End If
        End With
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        'If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
        '    Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
        '        Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '        Await model.DeleteBlockRecord(record)
        '        record = Nothing
        '    End Using
        'Else
        '    Me.BarraBotones.EnableBarItems()
        'End If



        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo para cargar los controles con el registro consultado
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        'If Me.BarraBotones.PermiteConsultar = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
        '    Exit Function
        'End If

        'Using model As New MProvisionRanges(Me.Tag.ToString())
        '    AsyncLoader(True)
        '    provisionRanges = Await model.GetProvisionRangesByCode(Code)
        '    INDLycProvisionRanges.BeginUpdate()
        'End Using

        'Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
        '    If Not provisionRanges Is Nothing AndAlso provisionRanges.Id > 0 Then
        '        Me.BarraBotones.StatusRecordVisible = True
        '        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), provisionRanges.CreationUser)
        '        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), provisionRanges.CreationDate)
        '        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), provisionRanges.ModificationUser)
        '        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), provisionRanges.ModificationDate)
        '        Dim result = Await model.GetBlockRecord(Me.Tag, Me.provisionRanges.Id)
        '        Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
        '        With provisionRanges
        '            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
        '            Initial = .Initial
        '            Final = .Final
        '            Status = .Status
        '        End With
        '        BarraBotones.SetDocuments(provisionRanges.Id)
        '        ActionsOnControls = True
        '        AsyncLoader(False)
        '        INDSpinInitial.Focus()
        '        Me.GetDocumentIndexed(Me.Tag & "_" & Me.provisionRanges.Code)
        '        If result IsNot Nothing AndAlso result.Id = 0 Then
        '            Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '            state.State = Domain.Base.Entities.ObjectState.Added
        '            record = New BlockRecordPortfolio With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = provisionRanges.Id}
        '            Dim operation = Await model.SaveBlockRecord(record)
        '            record = operation.ObjectEmbbeded
        '        Else
        '            Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
        '            record = result
        '            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '        End If
        '    Else
        '        'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '        'Code = String.Empty
        '        'INDBtnCode.Focus()
        '        AsyncLoader(False)
        '        If Me._sequense.IsManual Then
        '            Me.NewProvisionRanges()
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            Code = String.Empty
        '            Deshacer()
        '            INDBtnCode.Focus()
        '        End If
        '    End If
        'End Using
        'INDLycProvisionRanges.EndUpdate()




        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MProvisionRanges(CStr(Me.Tag))
                    AsyncLoader(True)
                    provisionRanges = Await Model.GetProvisionRangesByCode(INDBtnCode.Text.Trim)
                    INDLycProvisionRanges.BeginUpdate()
                    If provisionRanges IsNot Nothing AndAlso provisionRanges.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(provisionRanges.Id))
                            With provisionRanges
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                Initial = .Initial
                                Final = .Final
                                Status = .Status
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.provisionRanges.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordPortfolio With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = provisionRanges.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(provisionRanges.Id, Me.Tag.ToString(), Nothing, GetType(ProvisionRanges).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewProvisionRanges()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBtnCode.Focus()
                        End If
                    End If
                    INDLycProvisionRanges.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo para validar que el numero inicial no sea mayor que el final
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateInitialFinal() As Boolean
        If Initial < 0 Then
            Initial = Initial * -1
        End If
        If Final < 0 Then
            Final = Final * -1
        End If
        If Initial >= Final Then
            Return True
        End If
        Return False
    End Function
#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        provisionRanges = Nothing
        model = Nothing
        presenter = Nothing
        record = Nothing
        _searchMode = Nothing
    End Sub

    ''' <summary>
    ''' evento load del formulario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmProvisionRanges_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDLycProvisionRanges, True)
        '****Inicializar variables*****'
        ' Me._doc = Nothing
        '_idOperativeUnit = BarraBotones.OperatingUnitValue
        ' Me._indigoSession = SessionValues.Instance
        '******************************'
        'Me.Funct = AddressOf GenerateDoc

        'presenter = New PProvisionRanges(Me)
        'presenter.LoadDefinitionLayout()
        'presenter.GetSequense()
        ' Deshacer()
        'Me.BarraBotones.StatusRecordVisible = True
        ' Me.LoadStatus()
        '_searchMode = False



        'Me.LayoutControls.SetIsCustomizable(Me.INDLcBillingGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        presenter = New PProvisionRanges(Me)
        presenter.GetSequense()
        'Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' evnto que se dispara cuando el funcional esta activo y coloca el foco en el codigo si esta habilitado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmProvisionRanges_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBtnCode.Enabled Then
            INDBtnCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar enter en el control y realiza una busqueda por codigo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDBtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBtnCode.KeyDown
        ''If e.KeyCode = Keys.Enter Then
        ''    If String.IsNullOrEmpty(Code) Then
        ''        Me.NewProvisionRanges()
        ''    Else
        ''        Await Me.LoadControls()
        ''    End If
        ''End If

        'If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        '    If Me._sequense.IsManual Then
        '        If Not String.IsNullOrEmpty(Code) Then
        '            Await Me.LoadControls()
        '        End If
        '    Else
        '        If String.IsNullOrEmpty(Code) Then
        '            Me.NewProvisionRanges()
        '        Else
        '            Await Me.LoadControls()
        '        End If
        '    End If
        'End If





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
                    Await Me.NewProvisionRanges()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al cerrar el funcional y elimina el registro bloqueado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmProvisionRanges_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' evento que se dispoara al dar clic en el boton del control y lanza el form de busqueda
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDBtnCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBtnCode.ButtonClick
        OpenSearch()
    End Sub
#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    ''' 
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'If provisionRanges IsNot Nothing AndAlso provisionRanges.Id > 0 Then
        '    If MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelContent"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        DeleteBlockedRecord()
        '        Code = Me.IdEntity.Trim()
        '        Await LoadControls()
        '    End If
        'Else 'Realiza la consulta normal
        '    Code = Me.IdEntity.Trim()
        '    Await LoadControls()
        'End If
        'Me.IdEntity =  String.Empty
        'If INDBtnCode.Enabled = False Then
        '    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        'End If

        Me.ViewModeEditHold = True
        If Me.provisionRanges IsNot Nothing AndAlso Me.provisionRanges.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
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
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
        _searchMode = False
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
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        'Try
        '    Using model As New MProvisionRanges(Me.Tag.ToString())
        '        AsyncLoader(True)
        '        Dim Result As New ActionResult(Of ProvisionRanges)
        '        Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
        '            Case eActionsStatusRecords.Active
        '                Result = Await model.ChangeState(Code, True)
        '            Case eActionsStatusRecords.Inactive
        '                Result = Await model.ChangeState(Code, False)
        '        End Select
        '        AsyncLoader(False)
        '        If Result.StateResult = True Then
        '            provisionRanges = Result.ObjectEmbbeded
        '            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
        '        Else
        '            If Result.MessageResult(0) = ErrorConcurrencia Then
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '            Else
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '            End If
        '        End If
        '    End Using
        'Catch ex As Exception
        '    Throw ex
        '    AsyncLoader(False)
        'End Try



        If Not String.IsNullOrEmpty(Me.provisionRanges.Code) Then
            Try
                Using model As New MProvisionRanges(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not provisionRanges.Status
                    Dim result As ActionResult(Of ProvisionRanges) = Await model.ChangeState(Me.provisionRanges.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.provisionRanges = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDBtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        'If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.PortfolioSequenceDetail IsNot Nothing Then
        '    If Me._sequense.PortfolioSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
        '        Me._idOperativeUnit = Me._sequense.PortfolioSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Else
        '        Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '    End If
        'End If


        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PortfolioSequenceDetail IsNot Nothing Then
                If Not Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

End Class