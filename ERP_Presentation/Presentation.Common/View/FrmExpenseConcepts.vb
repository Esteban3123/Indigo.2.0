'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán
' Created          : 17-03-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Exceptions
Imports DevExpress.Xpo
Imports Presentation.Accounting
Imports System.Text
Imports Presentation.Accounting.MVP
Imports Presentation.Common.MVP
Imports System.Drawing
Imports DevExpress.XtraLayout.Utils

#End Region

''' <summary>
''' Formulario de Conceptos de Egresos
''' </summary>
Public Class FrmExpenseConcepts
    Implements IExpenseConcepts, ICustomizableForm

#Region "Properties and Variables"

    ''' <summary>
    ''' The nam e_ module
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IExpenseConcepts.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IExpenseConcepts.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' variable que contiene la cabecera de la secuencia
    ''' </summary>
    Dim _sequence As TreasurySequence

    ''' <summary>
    ''' variable que contiene el id del detalle de la secuencia
    ''' </summary>
    Dim _idCurrentSequence As Int64

    ''' <summary>
    ''' Obtiene o establece la cabecera de la secuencia
    ''' </summary>
    Public Property Sequence As TreasurySequence Implements IExpenseConcepts.Sequence
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

    ''' <summary>
    ''' Gets or sets the code expense concepts.
    ''' </summary>
    ''' <value>
    ''' The code expense concepts.
    ''' </value>
    Public Property CodeExpenseConcepts As String Implements IExpenseConcepts.CodeExpenseConcepts
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
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
    ''' Obtiene o establece la cuenta contable
    ''' </summary>
    ''' <value>
    ''' The account accounting.
    ''' </value>
    Public Property AccountAccounting As Integer Implements IExpenseConcepts.AccountAccounting
        Get
            Return CType(INDsleAccountAccounting.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleAccountAccounting.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el comportamiento de los conceptos de recibos de caja
    ''' </summary>
    ''' <value>
    ''' The behavior.
    ''' </value>
    Public Property Behavior As Short Implements IExpenseConcepts.Behavior
        Get
            Return INDrgBehavior.EditValue
        End Get
        Set(value As Short)
            INDrgBehavior.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si afecta el presupuesto
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [affect budget]; otherwise, <c>false</c>.
    ''' </value>
    Public Property AffectBudget As Byte? Implements IExpenseConcepts.AffectBudget
        Get
            Return INDrgAffectBudget.EditValue
        End Get
        Set(value As Byte?)
            INDrgAffectBudget.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la naturaleza de la cuenta
    ''' </summary>
    ''' <value>
    ''' The character.
    ''' </value>
    Public Property Nature As Integer Implements IExpenseConcepts.Nature
        Get
            Return INDgleNature.EditValue
        End Get
        Set(value As Integer)
            INDgleNature.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    ''' <value>
    ''' The description.
    ''' </value>
    Public Property Description As String Implements IExpenseConcepts.Description
        Get
            Return INDtxtDescription.EditValue
        End Get
        Set(value As String)
            INDtxtDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the cash register.
    ''' </summary>
    ''' <value>
    ''' The cash register.
    ''' </value>
    Public Property CashRegister As Integer Implements IExpenseConcepts.CashRegister
        Get
            Return INDsleCashRegister.EditValue
        End Get
        Set(value As Integer)
            INDsleCashRegister.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Maneja impuesto
    ''' </summary>
    ''' <returns></returns>
    Public Property TaxManagement As Boolean Implements IExpenseConcepts.TaxManagement
        Get
            Return INGRgTaxManagement.EditValue
        End Get
        Set(value As Boolean)
            INGRgTaxManagement.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Maneja Orden de Compra
    ''' </summary>
    ''' <returns></returns>
    Public Property BuyOrderManagement As Boolean Implements IExpenseConcepts.BuyOrderManagement
        Get
            Return INGRgBuyOrderManagement.EditValue
        End Get
        Set(value As Boolean)
            INGRgBuyOrderManagement.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [state expense concepts].
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [state expense concepts]; otherwise, <c>false</c>.
    ''' </value>
    Public Property StateExpenseConcepts As Boolean Implements IExpenseConcepts.StateExpenseConcepts
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
    ''' Obtiene o establece el datasource para cuentas contables
    ''' </summary>
    ''' <value>
    ''' The account accounting datasource.
    ''' </value>
    Public Property AccountAccountingDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IExpenseConcepts.AccountAccountingDatasource
        Get
            Return CType(INDsleAccountAccounting.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAccountAccounting.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para cajas
    ''' </summary>
    ''' <value>
    ''' The cash register datasource.
    ''' </value>
    Public Property CashRegisterDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IExpenseConcepts.CashRegisterDatasource
        Get
            Return CType(INDsleCashRegister.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCashRegister.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece la afectacion del concepto de flujo de caja
    ''' </summary>
    ''' <returns></returns>
    Public Property AffectCashFlowConcept As Byte? Implements IExpenseConcepts.AffectCashFlowConcept
        Get
            Return INDGleAffectCashFlowConcept.EditValue
        End Get
        Set(value As Byte?)
            INDGleAffectCashFlowConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el id de concepto de flujo de caja
    ''' </summary>
    ''' <returns></returns>
    Public Property IdAffectCashFlowConcept As Integer? Implements IExpenseConcepts.IdAffectCashFlowConcept
        Get
            Return INDSleCashFlowConcept.EditValue
        End Get
        Set(value As Integer?)
            INDSleCashFlowConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece/Obtiene el datasource de conceptos de flujo de efectivo
    ''' </summary>
    Public Property CashFlowConceptDataSource As DevExpress.Xpo.XPInstantFeedbackSource Implements IExpenseConcepts.CashFlowConceptDataSource
        Get
            Return INDSleCashFlowConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCashFlowConcept.Properties.DataSource = value
        End Set
    End Property

    Dim _ListAffectCashFlowConcept As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property ListAffectCashFlowConcept As List(Of Tuple(Of Byte, String))
        Get
            If _ListAffectCashFlowConcept Is Nothing Then
                _ListAffectCashFlowConcept = New List(Of Tuple(Of Byte, String))
                _ListAffectCashFlowConcept.Add(New Tuple(Of Byte, String)(0, "No"))
                _ListAffectCashFlowConcept.Add(New Tuple(Of Byte, String)(1, "Si"))
            End If
            Return _ListAffectCashFlowConcept
        End Get
    End Property

    ''' <summary>
    ''' Presentador de Conceptos de Ingresos
    ''' </summary>
    Dim Presenter As PExpenseConcepts

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' variable que contiene la entidad de conceptos de egresos
    ''' </summary>
    Dim ExpenseConcept As ExpenseConcepts

    ''' <summary>
    ''' variable que contiene el registro bloqueado
    ''' </summary>
    Dim record As BlockRecordTreasury

    ''' <summary>
    ''' lista que carga afecta pagos
    ''' </summary>
    Dim AffectPaymentTuple As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Crea la lista para llenar la naturaleza de la cuenta
    ''' </summary>
    Dim NatureFile As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Crea la lista para llenar los comportamientos
    ''' </summary>
    Dim BehaviorFile As List(Of Tuple(Of Integer, String))

    Dim flagLoadControls As Boolean = False
#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        'If Not SearchMode Then
        '    Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        'Else
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        'End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        'If Me.ExpenseConcept IsNot Nothing AndAlso Me.ExpenseConcept.Id > 0 Then
        '    If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Try
        '            Using Model As New MExpenseConcepts(Me.Tag)
        '                Me.ExpenseConcept.MarkAsDeleted()
        '                AsyncLoader(True)
        '                Dim result = Await Model.DeleteExpenseConcept(Me.ExpenseConcept)
        '                AsyncLoader(False)
        '                If result.StateResult = True Then
        '                    Await Me.DeleteDocumentIndexed()
        '                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
        '                    SearchMode = False
        '                    Me.Deshacer()
        '                Else
        '                    If result.MessageResult(0) = "-999" Then
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '                    ElseIf result.MessageResult(0) = "-000" Then
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
        '                    Else
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                    End If
        '                End If
        '            End Using
        '        Catch ex As Exception
        '            Throw ex
        '            AsyncLoader(False)
        '        End Try
        '    End If
        'End If

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If


        If Me.ExpenseConcept IsNot Nothing AndAlso Me.ExpenseConcept.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MExpenseConcepts(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteExpenseConcept(Me.ExpenseConcept)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        'If ValidateControls() = False Then
        '    Exit Sub
        'End If
        'AssigningValues()
        'Try
        '    Using Model As New MExpenseConcepts(Me.Tag.ToString())
        '        AsyncLoader(True)
        '        Dim Result = Await Model.SaveExpenseConcept(Me.ExpenseConcept, Me._idCurrentSequence)
        '        AsyncLoader(False)
        '        If Result.StateResult = True Then
        '            If ExpenseConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '                If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
        '                    Me.DicSequense(Me._sequence.TreasurySequenceDetail(0).Id).RemoveAt(0)
        '                End If
        '                If Me._sequence.Sequential Then
        '                    Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
        '                Else
        '                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
        '                End If
        '            ElseIf ExpenseConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
        '            End If
        '            Me.ExpenseConcept = Result.ObjectEmbbeded
        '            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '            SearchMode = False
        '            Me.Deshacer()
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


        If Not ValidateControls() Then
            Exit Sub
        End If
        If AffectCashFlowConcept.GetValueOrDefault = 1 AndAlso IdAffectCashFlowConcept.GetValueOrDefault = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("Los siguientes campos son requeridos y no se han diligenciado: {0}Concepto flujo efectivo", vbNewLine)
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MExpenseConcepts(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of ExpenseConcepts) = Await Model.SaveExpenseConcept(Me.ExpenseConcept, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If ExpenseConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.ExpenseConcept = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewExpenseConcept()
            'ActionsOnControls = True
            'INDbteCode.Enabled = False
        End If
    End Sub

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' Creates the nature file.
    ''' </summary>
    Private Sub CreateNatureFile()
        NatureFile.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("AccountNatureDebit")))
        NatureFile.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("AccountNatureCredit")))
        INDgleNature.Properties.DataSource = NatureFile
    End Sub

    ''' <summary>
    ''' Creates the behavior file.
    ''' </summary>
    Private Sub CreateBehaviorFile()
        BehaviorFile = New List(Of Tuple(Of Integer, String))
        'BehaviorFile.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("BehaviorTransferBetweenBanks", NAME_MODULE)))
        BehaviorFile.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("BehaviorPettyCash", NAME_MODULE)))
        BehaviorFile.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("BehaviorPaymentAdvancePaymentInvoices", NAME_MODULE)))
        BehaviorFile.Add(New Tuple(Of Integer, String)(4, ResourceManager.GetString("BehaviorReturningImprestRC", NAME_MODULE)))
        'BehaviorFile.Add(New Tuple(Of Integer, String)(5, ResourceManager.GetString("BehaviorPettyCashReimbursement", NAME_MODULE)))
        BehaviorFile.Add(New Tuple(Of Integer, String)(6, ResourceManager.GetString("BehaviorNone", NAME_MODULE)))
        'BehaviorFile.Add(New Tuple(Of Integer, String)(7, ResourceManager.GetString("BehaviorEndorsementBill", NAME_MODULE)))
        INDrgBehavior.Properties.DataSource = BehaviorFile
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Description", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.5},
                              New ColumnInfo() With {.Caption = "Naturaleza", .FieldName = "NatureName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Número de Cuenta", .FieldName = "IdMainAccount.Number", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListExpenseConcept
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Retorna el valor obtenido por el formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        CodeExpenseConcepts = ReturnValue
        If CodeExpenseConcepts IsNot String.Empty Then
            Await LoadControls()
            If Not INDbteCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IExpenseConcepts.ActionsOnControls
        Set(value As Boolean)
            INDlycRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            INDsleCashRegister.Enabled = value
            INDgleNature.Enabled = value
            INDsleAccountAccounting.Enabled = value
            INDrgAffectBudget.Enabled = value
            INDrgBehavior.Enabled = value
            INDGleAffectCashFlowConcept.Enabled = value
            INDSleCashFlowConcept.Enabled = value
            INDlycRoot.EndUpdate()
            If value Then
                INDtxtDescription.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Generates the document indexed.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.ExpenseConcept.Code, Me.ExpenseConcept.Description, Me.ExpenseConcept.IdMainAccount),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.ExpenseConcept.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.ExpenseConcept.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.ExpenseConcept.Code, Me.ExpenseConcept.Description, Me.ExpenseConcept.IdMainAccount)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.ExpenseConcept.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Loads the status.
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDlycRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        StateExpenseConcepts = True
        'Limpiar controles

        INDbteCode.Text = String.Empty
        INDtxtDescription.Text = String.Empty
        'INDgleAffectsPayments.EditValue = Nothing
        INDsleCashRegister.EditValue = Nothing
        'INDrgRefundAdvance.EditValue = Nothing
        INDrgAffectBudget.EditValue = Nothing
        'INDrgAffectsBanks.EditValue = Nothing
        'INDrgConceptCashRegisterMinor.EditValue = Nothing
        'INDrgRefundCashRegister.EditValue = Nothing
        'INDrgSpecialDiscount.EditValue = Nothing
        'INDsleEntityBankAccount.EditValue = Nothing
        Behavior = Nothing
        'INDlgrAuthorizationBoxes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDgleNature.EditValue = Nothing
        INDsleAccountAccounting.EditValue = Nothing
        ExpenseConcept = Nothing
        INDGleAffectCashFlowConcept.EditValue = Nothing
        'INDGleAffectCashFlowConcept.Properties.NullText = String.Empty
        INDSleCashFlowConcept.EditValue = Nothing
        'INDSleCashFlowConcept.Properties.NullText = String.Empty
        ' INDlycRoot.EndUpdate()
        TaxManagement = False
        INDlgrAuthorizationBoxes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciTaxManagement.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciBuyOrderManagement.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never



        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlycRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(CodeExpenseConcepts) AndAlso Not String.IsNullOrWhiteSpace(CodeExpenseConcepts) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MExpenseConcepts(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetExpenseConcept(INDbteCode.Text.Trim)
                    INDlycRoot.BeginUpdate()
                    ExpenseConcept = resultOperation.ObjectEmbbeded
                    If ExpenseConcept IsNot Nothing AndAlso ExpenseConcept.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        flagLoadControls = True
                        ' Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        record = Await Model.GetBlockRecordTreasury(CStr(Me.Tag), CStr(ExpenseConcept.Id))
                        Dim _affectCFC As Boolean
                        With ExpenseConcept
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            'Llenar Entidad

                            CodeExpenseConcepts = .Code
                            Description = .Description
                            Nature = .Nature
                            AccountAccounting = .IdMainAccount
                            If INDsleAccountAccounting IsNot Nothing Then
                                INDLciTaxManagement.Visibility = LayoutVisibility.Always
                            End If


                            _affectCFC = .AffectBudget
                            AffectBudget = CByte(IIf(_affectCFC, 1, 0))
                            Select Case AffectBudget
                                Case 1
                                    INDrgAffectBudget.Properties.NullText = "Si"
                                Case 0
                                    INDrgAffectBudget.Properties.NullText = "No"
                            End Select
                            Behavior = .Behavior
                            DisableBehaviorOptionalControls() 'Desativa los controles opcionales relacionados al "Comportamiento"
                            ActivateOptionalControlsByBehavior() 'Activa los controles según el "Comportamiento"
                            StateExpenseConcepts = .Status
                            TaxManagement = .TaxManagement
                            BuyOrderManagement = If(.BuyOrderManagement, False)
                            _affectCFC = .AffectCashFlowConcept.GetValueOrDefault
                            AffectCashFlowConcept = CByte(IIf(_affectCFC, 1, 0))
                            Select Case AffectCashFlowConcept
                                Case 1
                                    INDGleAffectCashFlowConcept.Properties.NullText = "Si"
                                Case 0
                                    INDGleAffectCashFlowConcept.Properties.NullText = "No"
                            End Select
                            IdAffectCashFlowConcept = .IdCashFlowConcept
                            If .CashFlowConcept IsNot Nothing Then
                                INDSleCashFlowConcept.Properties.NullText = String.Format("{0} - {1}", .CashFlowConcept.Code, .CashFlowConcept.NameConcept)
                            End If

                        End With
                        'Llenar NullText

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.ExpenseConcept.Code)
                        If record.Id = 0 Then
                            record = (Await Model.SaveBlockRecordTreasury(
                                    New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = ExpenseConcept.Id})
                                    ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                        Me.BarraBotones.SetDocuments(ExpenseConcept.Id, Me.Tag.ToString(), Nothing, GetType(ExpenseConcepts).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)


                        AsyncLoader(False)

                        ActionsOnControls = True
                        INDGcCash.DataSource = ExpenseConcept.ExpenseConceptCashRegisters
                        'End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewExpenseConcept()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            CodeExpenseConcepts = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlycRoot.EndUpdate()
                    flagLoadControls = False
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                flagLoadControls = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Activa los controles de acuerdo al campo "Comportamiento"
    ''' </summary>
    Private Sub ActivateOptionalControlsByBehavior()
        Select Case Behavior
            Case eBehavior.PettyCash
                INDlgrAuthorizationBoxes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDsbAdd.Enabled = True
                INDGcCash.Enabled = True
            Case eBehavior.PaymentAdvancePaymentInvoices
                INDLciBuyOrderManagement.Visibility = LayoutVisibility.Always
        End Select
    End Sub

    ''' <summary>
    ''' Desactiva todos los controles que son utilizados cuando se cumple una condición
    ''' </summary>
    Private Sub DisableBehaviorOptionalControls()
        INDlgrAuthorizationBoxes.Visibility = LayoutVisibility.Never
        INDLciBuyOrderManagement.Visibility = LayoutVisibility.Never
        INDGcCash.DataSource = Nothing
        INDsbAdd.Enabled = False
        INDGcCash.Enabled = False
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With Me.ExpenseConcept
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = CodeExpenseConcepts
            .Description = Description
            .Nature = Nature
            .IdMainAccount = AccountAccounting
            .AffectBudget = CBool(AffectBudget.GetValueOrDefault)
            .Behavior = Behavior
            .AffectCashFlowConcept = CBool(AffectCashFlowConcept.GetValueOrDefault)
            .IdCashFlowConcept = IdAffectCashFlowConcept
            .TaxManagement = TaxManagement
            If Behavior = eBehavior.PaymentAdvancePaymentInvoices Then
                .BuyOrderManagement = BuyOrderManagement
            Else
                .BuyOrderManagement = False
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonTreasury As New MExpenseConcepts(Me.Tag)
                Await ModelCommonTreasury.DeleteBlockRecordTreasury(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Crea un nuevo concepto de egreso
    ''' </summary>
    Private Async Function NewExpenseConcept() As Task
        ExpenseConcept = New ExpenseConcepts() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
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
                Me.CodeExpenseConcepts = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodeExpenseConcepts = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MExpenseConcepts(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.CodeExpenseConcepts = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodeExpenseConcepts = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Me.ExpenseConcept.Code) Then
            Try
                Using model As New MExpenseConcepts(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not ExpenseConcept.Status
                    Dim result As ActionResult(Of ExpenseConcepts) = Await model.UpdateStateExpenseConcept(Me.ExpenseConcept.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.ExpenseConcept = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' Elimina una caja de la lista
    ''' </summary>
    Private Sub DeleteCashRegister()
        Dim cash As ExpenseConceptCashRegisters = DirectCast(INDGvCash.GetFocusedRow, ExpenseConceptCashRegisters)
        If cash.Id > 0 Then
            cash.ChangeTracker.State = ObjectState.Deleted
        Else
            ExpenseConcept.ExpenseConceptCashRegisters.Remove(cash)
        End If
        If ExpenseConcept.ChangeTracker.State = ObjectState.Unchanged Then
            ExpenseConcept.ChangeTracker.State = ObjectState.Modified
        End If
    End Sub

    ''' <summary>
    ''' valida que solo el comportamiento ninguno pueda manejar retencion
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ValidateRetentionForBehavior(ByVal optionValidate As Integer) As Task
        If Behavior <> 0 AndAlso AccountAccounting <> 0 Then
            If Behavior <> 6 Then
                Using Model As New MExpenseConcepts(Me.Tag)
                    Dim _mainAccount = Await Model.GetAccountById(AccountAccounting)
                    If _mainAccount IsNot Nothing AndAlso _mainAccount.Id > 0 AndAlso _mainAccount.RetencionType <> 0 Then
                        If optionValidate = 1 Then
                            Behavior = Nothing
                        Else
                            INDsleAccountAccounting.EditValue = Nothing
                        End If
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("BehaviorWithRetentionError", NAME_MODULE)
                    End If
                End Using
            End If
        End If
    End Function

    ''' <summary>
    '''  la función crea un presentador, utiliza ese presentador para obtener conceptos específicos
    '''  de flujo de efectivo y asigna el resultado a una propiedad, posiblemente para su uso en la interfaz de usuario.
    ''' </summary>
    Public Sub GetCashFlowConcept()
        Dim _presenter As New Treasury.MVP.PCashFlowConcept
        CashFlowConceptDataSource = _presenter.GetCashFlowConcept(New String() {"1", "2"})
        _presenter = Nothing
    End Sub

#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        Presenter = Nothing
        SearchMode = Nothing
        ExpenseConcept = Nothing
        record = Nothing
        AffectPaymentTuple = Nothing
        NatureFile = Nothing
        BehaviorFile = Nothing
        flagLoadControls = Nothing
    End Sub


    ''' <summary>
    ''' Handles the Load event of the FrmExpenseConcepts control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmExpenseConcepts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PExpenseConcepts(Me)
        Presenter.InitializeAccountAccounting()
        Presenter.InitializeCashRegister()
        Presenter.GetSequence()
        Presenter.LoadDefinitionLayout()

        Dim listAction As New List(Of eAcciones)
        listAction.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvCash, listAction)
        IndigoGridControl1.RefreshGrid(INDGcCash)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvCash.Columns
            If col.Name = "colActions" Then
                col.Width = "200"
            End If
        Next
        LoadStatus()
        Deshacer()
        CreateNatureFile()
        CreateBehaviorFile()
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown

        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(CodeExpenseConcepts.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodeExpenseConcepts) Then
                    Await Me.NewExpenseConcept()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleAccountAccounting control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAccountAccounting_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountAccounting.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPopupPUC
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeAccountAccounting()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleCashRegister control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCashRegister_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCashRegister.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCash
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeCashRegister()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleEntityBankAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityBankAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmEntityAccount
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                'Presenter.InitializeEntityBankAccount()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the Activated event of the FrmExpenseConcepts control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmExpenseConcepts_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al seleccionar un registro desde el Vituelf
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.ExpenseConcept IsNot Nothing AndAlso Me.ExpenseConcept.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDrgConceptCashRegisterMinor control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDrgConceptCashRegisterMinor_EditValueChanged(sender As Object, e As EventArgs)
        'If INDrgConceptCashRegisterMinor.EditValue Then
        '    If INDsleCashRegister.EditValue Is Nothing Then
        '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CashEmpty", NAME_MODULE)
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleAccountAccounting control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleAccountAccounting_EditValueChanged(sender As Object, e As EventArgs)
        If INDsleCashRegister.EditValue IsNot Nothing AndAlso INDsleCashRegister.EditValue > 0 Then
            Using Model As New MCashRegister(Me.Tag)
                Dim cash As CashRegisters = Await Model.GetcashRegisterById(INDsleCashRegister.EditValue)
                If cash IsNot Nothing Then
                    INDsleAccountAccounting.EditValue = cash.IdMainAccount
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleEntityBankAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityBankAccount_EditValueChanged(sender As Object, e As EventArgs)
        'If INDsleEntityBankAccount.EditValue IsNot Nothing AndAlso INDsleEntityBankAccount.EditValue > 0 Then
        '    Using Model As New MEntityAccount(Me.Tag)
        '        Dim EntityAcc As EntityBankAccount = Await Model.GetEntityBankAccountById(INDsleEntityBankAccount.EditValue)
        '        If EntityAcc IsNot Nothing AndAlso EntityAcc.Id > 0 Then
        '            AccountAccounting = EntityAcc.IdAccount
        '        End If
        '    End Using
        'End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDrgBehavior control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDrgBehavior_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgBehavior.EditValueChanged
        If flagLoadControls Then
            Exit Sub
        End If
        If Behavior <> 0 Then
            Await ValidateRetentionForBehavior(1)
            DisableBehaviorOptionalControls()
            ActivateOptionalControlsByBehavior()
        End If
    End Sub

    ''' <summary>
    ''' Handles the 1 event of the INDsleAccountAccounting_EditValueChanged control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleAccountAccounting_EditValueChanged_1(sender As Object, e As EventArgs) Handles INDsleAccountAccounting.EditValueChanged
        If flagLoadControls Then
            Exit Sub
        End If
        If INDsleAccountAccounting.EditValue IsNot Nothing Then
            Await ValidateRetentionForBehavior(2)
            If Behavior = eBehavior.PettyCash Then
                INDlgrAuthorizationBoxes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDlgrAuthorizationBoxes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                While ExpenseConcept.ExpenseConceptCashRegisters.Count > 0
                    ExpenseConcept.ExpenseConceptCashRegisters.ElementAt(0).MarkAsDeleted()
                End While
                INDGcCash.DataSource = Nothing
            End If
            INDLciTaxManagement.Visibility = LayoutVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAdd control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If INDsleCashRegister.EditValue IsNot Nothing Then
            For Each cash As ExpenseConceptCashRegisters In ExpenseConcept.ExpenseConceptCashRegisters
                If cash.IdCashRegister.Equals(CashRegister) Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CashRegisterExist", NAME_MODULE)
                    INDsleCashRegister.EditValue = Nothing
                    Exit Sub
                End If
            Next
            ExpenseConcept.ExpenseConceptCashRegisters.Add(New ExpenseConceptCashRegisters() With {.IdCashRegister = CashRegister})
            If ExpenseConcept.ChangeTracker.State = ObjectState.Unchanged Then
                ExpenseConcept.ChangeTracker.State = ObjectState.Modified
            End If
            INDsleCashRegister.EditValue = Nothing
            INDGcCash.DataSource = ExpenseConcept.ExpenseConceptCashRegisters
        End If
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmExpenseConcepts control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmExpenseConcepts_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Obtiene o establece el repositorio para las cajas en la rejilla
    ''' </summary>
    ''' <value>
    ''' The repository code cash.
    ''' </value>
    Public WriteOnly Property RepositoryCodeCashDatasource As XPInstantFeedbackSource Implements IExpenseConcepts.RepositoryCodeCashDatasource
        Set(value As XPInstantFeedbackSource)
            RepositoryCodeCash.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el repositorio para las cajas en la rejilla
    ''' </summary>
    ''' <value>
    ''' The repository name cash.
    ''' </value>
    Public WriteOnly Property RepositoryNameCashDatasource As XPInstantFeedbackSource Implements IExpenseConcepts.RepositoryNameCashDatasource
        Set(value As XPInstantFeedbackSource)
            RepositoryNameCash.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteCashRegister()
    End Sub

    ''' <summary>
    ''' Handles the ContexMenuActions event of the IndigoGridView1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteCashRegister()
    End Sub

    ''' <summary>
    '''  esta función asegura que el origen de datos ListAffectCashFlowConcept esté asignado al control INDrgAffectBudget
    '''  cuando se muestre el cuadro de diálogo emergente, si aún no se ha asignado ningún origen de datos previamente. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrgAffectBudget_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDrgAffectBudget.QueryPopUp
        If INDrgAffectBudget.Properties.DataSource Is Nothing Then
            INDrgAffectBudget.Properties.DataSource = ListAffectCashFlowConcept
        End If
    End Sub

    ''' <summary>
    ''' esta función actualiza el texto de visualización en el control INDrgAffectBudget si el usuario selecciona
    ''' una opción nula, proporcionando una guía visual para la selección adecuada.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrgAffectBudget_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgAffectBudget.EditValueChanged
        If INDrgAffectBudget.EditValue Is Nothing Then
            INDrgAffectBudget.Properties.NullText = "Seleccione Afecta Presupuesto"
        End If
    End Sub

    ''' <summary>
    ''' Esta función asegura que el control INDGleAffectCashFlowConcept tenga datos disponibles para mostrar en su ventana
    ''' emergente cuando el usuario intente abrirlo. Si no se proporciona un origen de datos previamente, se asigna la lista
    ''' ListAffectCashFlowConcept al control para que el usuario pueda seleccionar una opción válida desde la ventana emergente.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleAffectCashFlowConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGleAffectCashFlowConcept.QueryPopUp
        If INDGleAffectCashFlowConcept.Properties.DataSource Is Nothing Then
            INDGleAffectCashFlowConcept.Properties.DataSource = ListAffectCashFlowConcept
        End If
    End Sub

    ''' <summary>
    ''' Esta función garantiza que el control INDSleCashFlowConcept tenga datos disponibles para mostrar en su ventana emergente cuando el usuario intente abrirlo.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCashFlowConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCashFlowConcept.QueryPopUp
        'Si no se proporciona un origen de datos previamente, se obtienen los conceptos de flujo de efectivo
        'mediante la función GetCashFlowConcept() y se asignan al control para que el usuario pueda seleccionar
        'una opción válida desde la ventana emergente.
        If INDSleCashFlowConcept.Properties.DataSource Is Nothing Then
            GetCashFlowConcept()
        End If
    End Sub

    ''' <summary>
    ''' Esta función maneja el evento EditValueChanged del control INDSleCashFlowConcept.
    ''' El evento EditValueChanged se dispara cuando el valor seleccionado en el control cambia.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCashFlowConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCashFlowConcept.EditValueChanged
        If String.IsNullOrEmpty(INDSleCashFlowConcept.EditValue) Then
            INDSleCashFlowConcept.Properties.NullText = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' esta función ajusta la visibilidad y el comportamiento de los controles INDLciCashFlowConcept, INDGleAffectCashFlowConcept
    ''' e INDSleCashFlowConcept basándose en el valor seleccionado en el control INDGleAffectCashFlowConcept.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleAffectCashFlowConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleAffectCashFlowConcept.EditValueChanged
        INDLciCashFlowConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciCashFlowConcept.AllowHide = True
        If INDGleAffectCashFlowConcept.EditValue IsNot Nothing Then
            If INDGleAffectCashFlowConcept.EditValue = 1 Then
                INDLciCashFlowConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciCashFlowConcept.AllowHide = False
            Else
                INDSleCashFlowConcept.EditValue = Nothing
            End If
        Else
            INDGleAffectCashFlowConcept.Properties.NullText = "Seleccione Afecta concepto flujo efectivo"
            INDSleCashFlowConcept.EditValue = Nothing
        End If

    End Sub

    '''' <summary>
    '''' Evento que se dispara al presionar el boton de crear un pais
    '''' </summary>
    'Private Sub INDSleCashFlowConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCashFlowConcept.ButtonClick
    '    If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
    '        Using Formulario As New FrmCashFlowConcept
    '            Formulario.ViewModeEditHold = True
    '            Dim transparent As New FrmTransparent(Formulario, False)
    '            transparent.ShowDialog()
    '            GetCashFlowConcept()
    '        End Using
    '    End If
    'End Sub
#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        ' SearchMode = False
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
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
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

#End Region

#Region "Enum Behavior"

    ''' <summary>
    ''' Enumeracón para el comportamiento del concepto de egreso
    ''' </summary>
    Public Enum eBehavior
        ''' <summary>
        ''' Traslado entre bancos
        ''' </summary>
        TransferBetweenBanks = 1
        ''' <summary>
        ''' Caja Menor
        ''' </summary>
        PettyCash = 2
        ''' <summary>
        ''' Cancelación/Anticipo de Facturas CxP
        ''' </summary>
        PaymentAdvancePaymentInvoices = 3
        ''' <summary>
        ''' Devolutivos de Anticipos RC
        ''' </summary>
        ReturningImprestRC = 4
        ''' <summary>
        '''  Reembolso de Caja Menor
        ''' </summary>
        PettyCashReimbursement = 5
    End Enum

#End Region

End Class