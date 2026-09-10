'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 24-08-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Text
Imports System.Windows.Forms
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Budget.MVP
Imports Presentation.Controls

#End Region

Public Class FrmPACModificationExpense
    Implements IAnnualizedCashFlowModification

#Region "BUILDER"

    Public ctrTmp As CtrInfoEntity

    Public Sub New()
        ' Add any initialization after the InitializeComponent() call.
        ctrTmp = New CtrInfoEntity()
        ' This call is required by the designer.
        InitializeComponent()
        ctrTmp.SetTotalValues(AddressOf getValues)
        ctrTmp.RefreshInfo()
        ctrTmp.PopupContainerControlEntity = INDpccChangeEntity
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    Private Function getValues() As Tuple(Of Integer, String, Integer, String, String)
        Return New Tuple(Of Integer, String, Integer, String, String)(BudgetEntitiesId, _EntityDescription, BudgetaryValidityId, _Year, statusValidity)
    End Function

#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' descripcion de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _EntityDescription As String
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.BudgetSequence
    ''' <summary>
    ''' Variable para conocer si el formulario abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean
    ''' <summary>
    ''' Presentador del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PAnnualizedCashFlowModification
    ''' <summary>
    ''' Variable que controla el registero bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim blockRecord As BlockRecordBudget
    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Variable de sesiones
    ''' </summary>
    ''' <remarks></remarks>
    Dim indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' representa la entidad de modificaciones del PAC
    ''' </summary>
    ''' <remarks></remarks>
    Dim annualizedCashFlowModification As AnnualizedCashFlowModification
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Budget"
    ''' <summary>
    ''' Año vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _Year As String
    ''' <summary>
    ''' Estado de a vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim statusValidity As String
    ''' <summary>
    ''' Mes Gastos
    ''' </summary>
    ''' <remarks></remarks>
    Dim _expenseMonth As Integer
    ''' <summary>
    ''' listado con los detalles para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listAnnualizedCashFlowModificationDetail As List(Of AnnualizedCashFlowModificationDetail)
    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer
#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' Obtiene o establece el id de la vigencia al cual pertenece
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityId As Integer
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer)
            INDsleValidity.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el id de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesId As Integer
        Get
            Return INDsleBudgetEntity.EditValue
        End Get
        Set(value As Integer)
            INDsleBudgetEntity.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para mostrar los mensajes en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
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
    ''' Propiedad que permite habilitar o deshabiliatar controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAnnualizedCashFlowModification.ActionsOnControls
        Set(value As Boolean)
            INDLyAnnualizedCashFlowModification.BeginUpdate()
            INDbtnConsecutive.Enabled = Not value
            INDdteDate.Enabled = value
            INDtxtDocument.Enabled = value
            INDmemoObservations.Enabled = value
            If annualizedCashFlowModification IsNot Nothing AndAlso annualizedCashFlowModification.Status < 2 Then
                INDBtnAdd.Enabled = value
            Else
                INDBtnAdd.Enabled = False
            End If

            INDgcDetail.Enabled = value
            INDLyAnnualizedCashFlowModification.EndUpdate()
            If value Then
                INDdteDate.Focus()
            Else
                INDbtnConsecutive.Focus()
            End If
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements IAnnualizedCashFlowModification.Code
        Get
            If (INDbtnConsecutive.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnConsecutive.Text
            End If
        End Get
        Set(value As String)
            INDbtnConsecutive.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el documento 
    ''' </summary>
    ''' <returns></returns>
    Public Property Document As String Implements IAnnualizedCashFlowModification.Document
        Get
            Return INDtxtDocument.EditValue
        End Get
        Set(value As String)
            INDtxtDocument.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <returns></returns>
    Public Property DocumentDate As Date Implements IAnnualizedCashFlowModification.DocumentDate
        Get
            Return INDdteDate.EditValue
        End Get
        Set(value As Date)
            INDdteDate.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' obtiene el valor de layaoutControls
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IAnnualizedCashFlowModification.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
    ''' <summary>
    ''' Obtiene el tag del frm
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IAnnualizedCashFlowModification.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Obtiene o establece las observaciones
    ''' </summary>
    ''' <returns></returns>
    Public Property Observations As String Implements IAnnualizedCashFlowModification.Observations
        Get
            Return INDmemoObservations.EditValue
        End Get
        Set(value As String)
            INDmemoObservations.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece la sequencia
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequense As BudgetSequence Implements IAnnualizedCashFlowModification.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As BudgetSequence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.BudgetSequenceDetail In Me._sequense.BudgetSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el datasource para entidades presupuestales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IAnnualizedCashFlowModification.BudgetEntitiesXpo
        Get
            Return INDsleBudgetEntity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleBudgetEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de las vigencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityXpo As DevExpress.Xpo.XPCollection Implements IAnnualizedCashFlowModification.BudgetaryValidityXpo
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el datasource para entidades presupuestales del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesPopUpXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IAnnualizedCashFlowModification.BudgetEntitiesPopUpXpo
        Get
            Return INDsleBudgetEntityPopUp.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleBudgetEntityPopUp.Properties.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece el listado de las vigencias del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityPopUpXpo As DevExpress.Xpo.XPCollection Implements IAnnualizedCashFlowModification.BudgetaryValidityPopUpXpo
        Get
            Return INDsleValidityPopUp.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidityPopUp.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "CRUD"
    ''' <summary>
    ''' Metodo Buscar
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub
    ''' <summary>
    ''' Metodo para deshacer
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        BarraBotones.PrepareToolbar(eAction.NewAndFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
    End Sub
    ''' <summary>
    ''' Metodo para eliminar, sin usarse
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub
    ''' <summary>
    ''' Metodo guardar
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If annualizedCashFlowModification.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If
            Dim errors As String = ValidateDetail()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            AssigningValues()
        End If
        Try
            Using model As New MAnnualizedCashFlowModification(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.SaveAnnualizedCashFlowModification(annualizedCashFlowModification, _idCurrentSequense)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If Result.ObjectEmbbeded.Status = 1 Then 'Guardar o Actualizar
                        If annualizedCashFlowModification.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            'Se descarta la secuencia numerica usada
                            If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                                Me.DicSequense(Me._sequense.BudgetSequenceDetail(0).Id).RemoveAt(0)
                            End If
                            If Me._sequense.Sequential Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                            End If
                        ElseIf annualizedCashFlowModification.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    ElseIf Result.ObjectEmbbeded.Status = 2 Then 'Confirmar
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveConfirm"), Result.ObjectEmbbeded.Code)
                    ElseIf Result.ObjectEmbbeded.Status = 3 Then 'Anular
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                    End If
                    Me.annualizedCashFlowModification = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, annualizedCashFlowModification.Id, 0, annualizedCashFlowModification.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, annualizedCashFlowModification.Id, 0, annualizedCashFlowModification.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, annualizedCashFlowModification.Id, 0, annualizedCashFlowModification.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, annualizedCashFlowModification.Id, 0, annualizedCashFlowModification.Id)
                    End Select
                    AsyncLoader(False)
                    'SearchMode = False
                    Me.Deshacer()
                Else
                    If Result.StateResult = False And Result.StateResultAux = False Then
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                    If annualizedCashFlowModification.Id > 0 Then
                        annualizedCashFlowModification = Await model.GetAnnualizedCashFlowModificationByCode(Code, 2)
                    Else
                        annualizedCashFlowModification = New AnnualizedCashFlowModification
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Metodo para el boton actulizar, sin usarse
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub
    ''' <summary>
    ''' MEtodo para generar un neuvo indicador economico
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        NewAnnualizedCashFlowModification()

        'If Me._sequense.IsManual Then

        'Else
        '    NewAnnualizedCashFlowModification()
        'End If
    End Sub
    ''' <summary>
    ''' Metodo que abre la busqueda por el id de la vigencia de presupuesto
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo With {.Caption = "Documento", .FieldName = "Document", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList()
            .ValorSolicitado = "Code"
            .SearchParameters = {BudgetaryValidityId, 2}
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListPACModification
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
        DeleteBlockedRecord()
        INDbtnConsecutive.Text = ReturnValue
        If INDbtnConsecutive.Text <> String.Empty Then
            LoadControls()
            If INDbtnConsecutive.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnConsecutive.Enabled = False
        End If
    End Sub

#End Region

#Region "METHODS"
    ''' <summary>
    ''' funcion que retorna el documento a indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), annualizedCashFlowModification.Code), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & annualizedCashFlowModification.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), annualizedCashFlowModification.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), annualizedCashFlowModification.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), annualizedCashFlowModification.Code)
            Return Me._doc
        End If
    End Function

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

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If blockRecord IsNot Nothing AndAlso blockRecord.Id > 0 AndAlso blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBudgetModification(MyTag)
                Await Model.DeleteBlockRecord(blockRecord)
            End Using
            blockRecord = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Sub NewAnnualizedCashFlowModification()
        If INDsleBudgetEntity.EditValue IsNot Nothing AndAlso CType(INDsleBudgetEntity.EditValue, String) <> "" Then
            annualizedCashFlowModification = New AnnualizedCashFlowModification
            If Me._sequense.IsManual Then
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me._sequense.Scope Is Nothing Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.Code = String.Empty
                    INDbtnConsecutive.Focus()
                    Exit Sub
                End If
                If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    Me._idCurrentSequense = Me._sequense.BudgetSequenceDetail(0).Id
                ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    If Me._sequense.BudgetSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                        Me._idCurrentSequense = Me._sequense.BudgetSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        Exit Sub
                    End If
                End If
                If Not Me._sequense.Sequential Then
                    If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                        If Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
                            PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
                        Else
                            Using model As New ModelBaseBudget(Me.Tag)
                                Me.DicSequense(Me._idCurrentSequense) = Await model.GetNumericSequenseGroup(Me._idCurrentSequense)
                            End Using
                            If Me.DicSequense(Me._idCurrentSequense) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
                                PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
                            Else
                                Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
                            End If
                        End If
                    Else
                        PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
                    End If
                Else
                    PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para preparar la barra de usuario cuando el registro es nuevo
    ''' </summary>
    ''' <param name="code">The code.</param>
    Private Sub PrepareToolbar(code As String)
        Me.Code = code
        Me.ActionsOnControls = True
        Me.BarraBotones.StatusRecord = "1"
        Me.BarraBotones.ControlHideStatus = True
        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        INDsleBudgetEntityPopUp.Properties.ReadOnly = True
        INDsleValidityPopUp.Properties.ReadOnly = True
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                BudgetaryValidityId = item.Id
                _Year = item.Year
                statusValidity = item.StatusText
            End If
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControls()
        INDLyAnnualizedCashFlowModification.BeginUpdate()
        annualizedCashFlowModification = Nothing
        _listAnnualizedCashFlowModificationDetail = Nothing
        INDsleBudgetEntityPopUp.Properties.ReadOnly = False
        INDsleValidityPopUp.Properties.ReadOnly = False
        ReadOnlyControls(False)
        ActionsOnControls = False
        Code = String.Empty
        INDdteDate.EditValue = Nothing
        Document = String.Empty
        Observations = String.Empty
        INDgcDetail.DataSource = Nothing
        DeleteBlockedRecord()
        BarraBotones.ControlHideStatus = False
        BarraBotones.CleanAuditBasic()
        BarraBotones.ReassignOperatingUnit()
        INDLyAnnualizedCashFlowModification.EndUpdate()
    End Sub

    ''' <summary>
    ''' Metodo que muestra los layoutGroup que estan ocultos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadLayoutGroup()
        INDlygPrincipalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlygGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlygCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.ControlHideStatus = False
        BarraBotones.PrepareToolbar(eAction.NewAndFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DeshacerTodo()
        CleanControlsTodo()
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
    End Sub

    ''' <summary>
    ''' Metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControlsTodo()
        INDLyAnnualizedCashFlowModification.BeginUpdate()
        ActionsOnControls = False
        INDsleValidity.EditValue = Nothing
        CleanControls()
        BarraBotones.StatusRecordVisible = False
        INDlygPrincipalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlygGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlygCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleBudgetEntity.Focus()
        INDLyAnnualizedCashFlowModification.EndUpdate()
    End Sub
    ''' <summary>
    ''' Carga los controles
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        Using model As New MAnnualizedCashFlowModification(MyTag)
            AsyncLoader(True)
            annualizedCashFlowModification = Await model.GetAnnualizedCashFlowModificationByCode(Code, 2)
            INDLyAnnualizedCashFlowModification.BeginUpdate()
            If annualizedCashFlowModification IsNot Nothing AndAlso annualizedCashFlowModification.Id > 0 Then

                _listAnnualizedCashFlowModificationDetail = model.GetAnnualizedCashFlowModificationDetailByPACModificationId(annualizedCashFlowModification.Id)
                Dim result = Await model.GetBlockRecord(MyTag, annualizedCashFlowModification.Id)
                INDsleBudgetEntityPopUp.Properties.ReadOnly = True
                INDsleValidityPopUp.Properties.ReadOnly = True
                With annualizedCashFlowModification
                    LayoutControls.SetCustomFieldsValue(.CustomProperties)

                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                    Code = .Code
                    BudgetaryValidityId = .BudgetaryValidityId
                    BudgetEntitiesId = .BudgetEntityId
                    Document = .Document
                    DocumentDate = .DocumentDate
                    Observations = .Observations
                    Me.BarraBotones.StatusRecord = .Status.ToString()
                    Me.BarraBotones.ControlHideStatus = True
                End With
                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.annualizedCashFlowModification.Code)
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    blockRecord = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = annualizedCashFlowModification.Id}
                    Dim operation = Await model.SaveBlockRecord(blockRecord)
                    blockRecord = operation.ObjectEmbbeded
                Else
                    blockRecord = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
                If annualizedCashFlowModification.Status = 1 Then 'Registrado
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                Else 'Confirmado o Anulado
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    ReadOnlyControls(True)
                End If
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
                Me.BarraBotones.SetDocuments(annualizedCashFlowModification.Id)

                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.PrintReport(PrintReportAction.None, annualizedCashFlowModification.Id, 0, annualizedCashFlowModification.Id)
                AsyncLoader(False)
                ActionsOnControls = True
                INDgcDetail.DataSource = _listAnnualizedCashFlowModificationDetail
                INDgcDetail.RefreshDataSource()
            Else
                AsyncLoader(False)
                If Me._sequense.IsManual Then
                    Me.NewAnnualizedCashFlowModification()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.Code = String.Empty
                    Deshacer()
                    INDbtnConsecutive.Focus()
                End If
            End If
        End Using
        INDLyAnnualizedCashFlowModification.EndUpdate()
    End Function
    ''' <summary>
    ''' Funcion para validar el detalle
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateDetail() As String
        Dim errors As New StringBuilder
        If INDgvDetail.RowCount = 0 Then
            errors.AppendLine("Se debe agregar minimo un detalle para la modificación")
        End If
        If INDgvDetail.RowCount > 0 Then
            Dim listNotNature = _listAnnualizedCashFlowModificationDetail.FindAll(Function(x) x.Nature = 0)
            If listNotNature.Count > 0 Then
                errors.AppendLine("Se encontraron items sin naturaleza")
            End If
            Dim ListNotValue = _listAnnualizedCashFlowModificationDetail.FindAll(Function(x) x.Value = 0)
            If ListNotValue.Count > 0 Then
                errors.AppendLine("Se encontraron items sin valor")
            End If
        End If
        Return errors.ToString()
    End Function
    ''' <summary>
    ''' Metodo para asignar los valores al objeto desde la vista
    ''' </summary>
    Private Sub AssigningValues()
        With annualizedCashFlowModification
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .BudgetaryValidityId = BudgetaryValidityId
            .Document = Document
            .DocumentDate = DocumentDate
            .DocumentSource = 2
            .Observations = Observations
            .PACType = 1
            For Each item In _listAnnualizedCashFlowModificationDetail
                .AnnualizedCashFlowModificationDetail.Add(item)
            Next
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub
#End Region

#Region "HANDLES"

#Region "Load"
    ''' <summary>
    ''' Libera memeoria al cerrar el frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _EntityDescription = Nothing
        _sequense = Nothing
        SearchMode = Nothing
        presenter = Nothing
        blockRecord = Nothing
        _idCurrentSequense = Nothing
        _idOperativeUnit = Nothing
        annualizedCashFlowModification = Nothing
        _Year = Nothing
        statusValidity = Nothing
        _expenseMonth = Nothing
        _listAnnualizedCashFlowModificationDetail = Nothing
        varImp = Nothing
    End Sub
    ''' <summary>
    ''' Carga el frm FrmInitialBudgetExpense
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmInitialBudgetExpense_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLyAnnualizedCashFlowModification, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        presenter = New PAnnualizedCashFlowModification(Me)
        presenter.LoadDefinitionLayout()
        presenter.GetSequense()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        IndigoGridControl1.RefreshGrid(INDgcDetail)

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvDetail, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvDetail.Columns
            If col.Name = "colActions" Then
                col.Visible = False
            End If
        Next

        LoadStatus()
        DeshacerTodo()
    End Sub
#End Region

#Region "Shown"
    ''' <summary>
    ''' Evento que enfoca INDsleBudgetEntity
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmInitialBudgetExpense_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleBudgetEntity.Focus()
    End Sub
#End Region

#Region "FormClosing"
    ''' <summary>
    ''' Evento al cerrar FrmInitialBudgetExpense
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmInitialBudgetExpense_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' carga el datasource de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleBudgetEntity.QueryPopUp
        If BudgetEntitiesXpo Is Nothing Then
            presenter.InitializeBudgetEntity()
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Metodo al cambiar el valor de las entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetEntity.EditValueChanged
        If BudgetEntitiesId <> Nothing AndAlso BudgetEntitiesId <> 0 Then
            _EntityDescription = INDsleBudgetEntity.Text
            BudgetaryValidityId = Nothing
            BudgetaryValidityXpo = Nothing
            presenter.InitializeValidity(BudgetEntitiesId)
            SetFirstOrDefaultValidity()
            ctrTmp.RefreshInfo()
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        End If
    End Sub

    ''' <summary>
    ''' Evento al cambiar el valor de las vigencias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If BudgetaryValidityId <> Nothing AndAlso BudgetaryValidityId <> 0 Then
            If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
                Dim item = (From l In BudgetaryValidityXpo Where l.Id = BudgetaryValidityId Select l).FirstOrDefault
                If item IsNot Nothing Then
                    _Year = item.Year
                    statusValidity = item.StatusText
                    _expenseMonth = item.ExpenseMonth
                    ctrTmp.RefreshInfo()
                    LoadLayoutGroup()
                    If BudgetEntitiesPopUpXpo Is Nothing Then
                        presenter.InitializeBudgetEntityPopUp()
                    End If
                    INDsleBudgetEntityPopUp.EditValue = BudgetEntitiesId
                    INDsleValidityPopUp.EditValue = BudgetaryValidityId
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento al editar INDsleBudgetEntityPopUp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetEntityPopUp.EditValueChanged
        If INDsleBudgetEntityPopUp.EditValue IsNot Nothing Then
            BudgetEntitiesId = INDsleBudgetEntityPopUp.EditValue
            If INDsleBudgetEntityPopUp.Text <> String.Empty Then
                _EntityDescription = INDsleBudgetEntityPopUp.Text
            End If
            INDsleValidityPopUp.EditValue = Nothing
            BudgetaryValidityPopUpXpo = Nothing
            presenter.InitializeValidityPopUp(INDsleBudgetEntityPopUp.EditValue)
            ctrTmp.RefreshInfo()
        End If
    End Sub
    ''' <summary>
    ''' Evento al cambiar los valores INDsleValidityPopUp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleValidityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidityPopUp.EditValueChanged
        If INDsleValidityPopUp.EditValue IsNot Nothing Then
            If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
                BudgetaryValidityId = INDsleValidityPopUp.EditValue
                Dim item = (From l In BudgetaryValidityXpo Where l.Id = BudgetaryValidityId Select l).FirstOrDefault
                If item IsNot Nothing Then
                    _Year = item.Year
                    statusValidity = item.StatusText
                    _expenseMonth = item.ExpenseMonth
                    ctrTmp.RefreshInfo()
                End If
            End If
        End If
    End Sub

#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Click en entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBudgetEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", Nothing, True)
        End If
    End Sub
    ''' <summary>
    ''' Click en Popup entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetEntityPopUp_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBudgetEntityPopUp.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", Nothing, True)
        End If
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Evento al dar enter en INDbtnConsecutive
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnConsecutive_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnConsecutive.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If BudgetaryValidityId = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe elegir una vigencia."
                Exit Sub
            End If
            If Me._sequense Is Nothing OrElse Me._sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(INDbtnConsecutive.Text.Trim()) Then
                    Await LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDbtnConsecutive.Text.Trim()) Then
                    Me.NewAnnualizedCashFlowModification()
                Else
                    Await LoadControls()
                End If
            End If
        End If
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Click en INDBtnAdd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Dim frmDetail As New FrmPopupCategoryExpense()
        frmDetail.Size = New System.Drawing.Size(800, 730)
        frmDetail.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        frmDetail.ValidatyId = BudgetaryValidityId
        frmDetail.monthOpen = _expenseMonth
        Dim frmTransparent As New FrmTransparent(frmDetail, False)
        If frmTransparent.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
            If _listAnnualizedCashFlowModificationDetail Is Nothing Then
                _listAnnualizedCashFlowModificationDetail = frmDetail.ListAnnualizedCashFlowModificationDetail
            Else
                For Each item In frmDetail.ListAnnualizedCashFlowModificationDetail
                    Dim detailAdded = _listAnnualizedCashFlowModificationDetail.Find(Function(x) x.AnnualizedCashFlowId = item.AnnualizedCashFlowId)
                    If detailAdded IsNot Nothing Then
                        Continue For
                    End If
                    _listAnnualizedCashFlowModificationDetail.Add(item)
                Next

            End If
            _listAnnualizedCashFlowModificationDetail = (From item In _listAnnualizedCashFlowModificationDetail Order By item.Month Ascending).ToList()
            INDgcDetail.DataSource = _listAnnualizedCashFlowModificationDetail
            INDgcDetail.RefreshDataSource()
        End If
    End Sub
#End Region

#Region "Click_ButtonAction"
    ''' <summary>
    ''' Evento cuado se quiere modificar el registro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If annualizedCashFlowModification.Status > 1 Then
            If annualizedCashFlowModification.Status = 2 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque esta confirmado"
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se puede modificar el registro porque esta anulado"
            End If
            Exit Sub
        End If
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim modificationDetail = DirectCast(INDgvDetail.GetFocusedRow(), AnnualizedCashFlowModificationDetail)
            If modificationDetail.Id > 0 Then
                annualizedCashFlowModification.AnnualizedCashFlowModificationDetail.Add(modificationDetail.MarkAsDeleted())
            End If
            _listAnnualizedCashFlowModificationDetail.Remove(modificationDetail)
            INDgcDetail.DataSource = _listAnnualizedCashFlowModificationDetail
            INDgcDetail.RefreshDataSource()
        End If

    End Sub
#End Region

#Region "EditValueChanging"
    ''' <summary>
    ''' Evento al modificar INDRptTxtValue
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRptTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptTxtValue.EditValueChanging
        If e.NewValue = String.Empty Then
            Exit Sub
        End If
        Dim modificationDetail = DirectCast(INDgvDetail.GetFocusedRow(), AnnualizedCashFlowModificationDetail)
        If modificationDetail.Nature = 1 Then
            If modificationDetail.Balance < e.NewValue Then
                Mensaje(EeventViewerImages.Advertencia) = "Con la naturaleza débito el saldo no puede ser menor al valor."
                e.Cancel = True
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento al modificar INDRptTxtValue
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRptIcbNature_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptIcbNature.EditValueChanging
        Dim nature = e.NewValue
        Dim modificationDetail = DirectCast(INDgvDetail.GetFocusedRow(), AnnualizedCashFlowModificationDetail)
        If modificationDetail.Value > 0 Then
            If nature = 1 Then 'si es debito
                If modificationDetail.Balance < modificationDetail.Value Then
                    Mensaje(EeventViewerImages.Advertencia) = "Con la naturaleza débito el saldo no puede ser menor al valor."
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If
        modificationDetail.Nature = nature
        INDgcDetail.DataSource = _listAnnualizedCashFlowModificationDetail
        INDgcDetail.RefreshDataSource()
    End Sub
#End Region

#Region "IdEntityLoaded"
    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.annualizedCashFlowModification IsNot Nothing AndAlso Me.annualizedCashFlowModification.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Me.INDbtnConsecutive.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnConsecutive.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        Me.IdEntity =  String.Empty
    End Sub
#End Region

#End Region

#Region "BAR BUTTONS"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnConsecutive.ButtonClick
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
        annualizedCashFlowModification.Status = 1
        varImp = 1
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
        annualizedCashFlowModification.Status = 1
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Click Boton de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            annualizedCashFlowModification.Status = 3
            varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.BudgetSequenceDetail IsNot Nothing Then
            If Me._sequense.BudgetSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.BudgetSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Else
                Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento para desahcer todo
    ''' </summary>
    Private Sub BarraBotones_Click_DeshacerTodo() Handles BarraBotones.Click_DeshacerTodo
        DeshacerTodo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ActualizarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            annualizedCashFlowModification.Status = 2
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_GuardarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            annualizedCashFlowModification.Status = 2
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, annualizedCashFlowModification.Id, 0, annualizedCashFlowModification.Id)
    End Sub

#End Region

End Class