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

Imports Presentation.Treasury.MVP
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
Imports Presentation.Accounting
Imports System.Text
Imports Presentation.Common
Imports Presentation.Common.MVP

#End Region

''' <summary>
''' Formulario de Conceptos de Notas
''' </summary>
Public Class FrmNoteConcepts
    Implements INoteConcepts, ICustomizableForm

#Region "Properties and Variables"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements INoteConcepts.MyTag
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
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements INoteConcepts.MyLayoutControl
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
    ''' <value>
    ''' The sequence.
    ''' </value>
    Public Property Sequence As TreasurySequence Implements INoteConcepts.Sequence
        Get
            Return _sequence
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
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value>
    ''' The code note concepts.
    ''' </value>
    Public Property Code As String Implements INoteConcepts.Code
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cuenta contable
    ''' </summary>
    ''' <value>
    ''' The account accounting.
    ''' </value>
    Public Property AccountAccounting As Integer Implements INoteConcepts.AccountAccounting
        Get
            Return INDsleAccountAccounting.EditValue
        End Get
        Set(value As Integer)
            INDsleAccountAccounting.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si afecta presupuesto
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [affect budget]; otherwise, <c>false</c>.
    ''' </value>
    Public Property AffectBudget As Byte? Implements INoteConcepts.AffectBudget
        Get
            Return INDrgAffectBudget.EditValue
        End Get
        Set(value As Byte?)
            INDrgAffectBudget.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si tiene recaudo automatico
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [automatic collection]; otherwise, <c>false</c>.
    ''' </value>
    Public Property AutomaticCollection As Byte? Implements INoteConcepts.AutomaticCollection
        Get
            Return INDrgAutomaticCollection.EditValue
        End Get
        Set(value As Byte?)
            INDrgAutomaticCollection.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la naturaleza de la cuenta
    ''' </summary>
    ''' <value>
    ''' The character.
    ''' </value>
    Public Property Nature As Integer Implements INoteConcepts.Character
        Get
            Return INDgleNature.EditValue
        End Get
        Set(value As Integer)
            INDgleNature.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion del concepto
    ''' </summary>
    ''' <value>
    ''' The description.
    ''' </value>
    Public Property Description As String Implements INoteConcepts.Description
        Get
            Return INDtxtDescription.EditValue
        End Get
        Set(value As String)
            INDtxtDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [state note concepts]; otherwise, <c>false</c>.
    ''' </value>
    Public Property StateNoteConcepts As Boolean Implements INoteConcepts.StateNoteConcepts
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
    ''' Obtiene o establece el datasource de cuentas contables
    ''' </summary>
    ''' <value>
    ''' The account accounting.
    ''' </value>
    Public Property AccountAccountingDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements INoteConcepts.AccountAccountingDatasource
        Get
            Return CType(INDsleAccountAccounting.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAccountAccounting.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' The presenter
    ''' </summary>
    Dim Presenter As PNoteConcepts

    ''' <summary>
    ''' Variable que contiene el registro bloqueado
    ''' </summary>
    Dim record As BlockRecordTreasury

    ''' <summary>
    ''' Variable de la entidad
    ''' </summary>
    Dim NoteConcept As NoteConcepts

    ''' <summary>
    ''' Lista de los datos de la naturaleza de la cuenta
    ''' </summary>
    Dim NatureFile As List(Of Tuple(Of Byte, String))

    Dim _ListAffect As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property ListAffect As List(Of Tuple(Of Byte, String))
        Get
            If _ListAffect Is Nothing Then
                _ListAffect = New List(Of Tuple(Of Byte, String))
                _ListAffect.Add(New Tuple(Of Byte, String)(0, "No"))
                _ListAffect.Add(New Tuple(Of Byte, String)(1, "Si"))
            End If
            Return _ListAffect
        End Get
    End Property

    ''' <summary>
    ''' obtiene o establece la afectacion del concepto de flujo de caja
    ''' </summary>
    ''' <returns></returns>
    Public Property AffectCashFlowConcept As Byte? Implements INoteConcepts.AffectCashFlowConcept
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
    Public Property IdAffectCashFlowConcept As Integer? Implements INoteConcepts.IdAffectCashFlowConcept
        Get
            Return INDSleCashFlowConcept.EditValue
        End Get
        Set(value As Integer?)
            INDSleCashFlowConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece si realiza reversion
    ''' </summary>
    ''' <returns></returns>
    Public Property Reversion As Byte? Implements INoteConcepts.Reversion
        Get
            Return INDchkReversion.EditValue
        End Get
        Set(value As Byte?)
            INDchkReversion.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Establece/Obtiene el datasource de conceptos de flujo de efectivo
    ''' </summary>
    Public Property CashFlowConceptDataSource As DevExpress.Xpo.XPInstantFeedbackSource Implements INoteConcepts.CashFlowConceptDataSource
        Get
            Return INDSleCashFlowConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCashFlowConcept.Properties.DataSource = value
        End Set
    End Property
#End Region

#Region "ICrud"

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
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        If Me.NoteConcept IsNot Nothing AndAlso Me.NoteConcept.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MNoteConcepts(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteNoteConcept(Me.NoteConcept)
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
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        If AffectCashFlowConcept.GetValueOrDefault = 1 AndAlso IdAffectCashFlowConcept.GetValueOrDefault = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("Los siguientes campos son requeridos y no se han diligenciado: {0}Concepto flujo efectivo", vbNewLine)
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MNoteConcepts(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of NoteConcepts) = Await Model.SaveNoteConcept(Me.NoteConcept, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If NoteConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.NoteConcept = result.ObjectEmbbeded
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
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Me.NewNoteConcept()
        End If
    End Sub

#End Region

#Region "Methods and Functions"

    Private Sub CreateNatureFile()
        NatureFile = New List(Of Tuple(Of Byte, String))
        NatureFile.Add(New Tuple(Of Byte, String)(1, "Débito"))
        NatureFile.Add(New Tuple(Of Byte, String)(2, "Crédito"))
        INDgleNature.Properties.DataSource = NatureFile
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
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
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue

        Dim ListNature As New List(Of Tuple(Of String, Byte))
        ListNature.Add(New Tuple(Of String, Byte)("Débito", 1))
        ListNature.Add(New Tuple(Of String, Byte)("Crédito", 2))

        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Description", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4},
                             New ColumnInfo() With {.Caption = "Naturaleza", .FieldName = "Nature", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2, .ColumnEdit = True, .ListItemsDatasourceColumEdit = ListNature},
                             New ColumnInfo() With {.Caption = "Cuenta", .FieldName = "IdMainAccount.NumberName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllNoteConcept
            .ValorSolicitado = "Code"
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
        Code = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements INoteConcepts.ActionsOnControls
        Set(value As Boolean)
            INDlycRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            INDrgAffectBudget.Enabled = value
            'INDrgAffectsCreditors.Enabled = value
            INDrgAutomaticCollection.Enabled = value
            INDgleNature.Enabled = value
            INDsleAccountAccounting.Enabled = value
            INDchkReversion.Enabled = value
            INDGleAffectCashFlowConcept.Enabled = value
            INDSleCashFlowConcept.Enabled = value
            INDlycRoot.EndUpdate()

            'CtrBudgetSettings1.ActionControlBudgetSetting()
            'CtrBudgetSettings2.ActionControlBudgetSetting()

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
        'TODO: cambiar los datos de indexacion
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.NoteConcept.Code, Me.NoteConcept.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.NoteConcept.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.NoteConcept.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.NoteConcept.Code, Me.NoteConcept.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.NoteConcept.Code)
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

        INDbteCode.EditValue = String.Empty
        INDtxtDescription.EditValue = String.Empty
        INDrgAffectBudget.EditValue = Nothing
        'INDrgAffectsCreditors.EditValue = Nothing
        INDrgAutomaticCollection.EditValue = Nothing
        INDgleNature.EditValue = Nothing
        INDsleAccountAccounting.EditValue = Nothing
        Me.NoteConcept = Nothing
        INDchkReversion.EditValue = 0
        CtrBudgetSettings1.CleanControlBudgetSetting()
        CtrBudgetSettings2.CleanControlBudgetSetting()
        INDGleAffectCashFlowConcept.EditValue = Nothing
        INDSleCashFlowConcept.EditValue = Nothing

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
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MNoteConcepts(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetNoteConcept(INDbteCode.Text.Trim)
                    NoteConcept = resultOperation.ObjectEmbbeded
                    INDlycRoot.BeginUpdate()
                    If NoteConcept IsNot Nothing AndAlso NoteConcept.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MCommonTreasury(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecordTreasury(CStr(Me.Tag), CStr(NoteConcept.Id))
                            With NoteConcept
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Code = .Code
                                Description = .Description
                                'AffectBudget = .AffectBudget
                                'AffectsCreditors = .AffectCreditor
                                'AutomaticCollection = .AutoCollect
                                Nature = .Nature
                                AccountAccounting = .IdMainAccount
                                StateNoteConcepts = .Status
                                Dim _affectCFC = .AffectBudget
                                AffectBudget = CByte(IIf(_affectCFC, 1, 0))
                                Select Case AffectBudget
                                    Case 1
                                        INDrgAffectBudget.Properties.NullText = "Si"
                                    Case 0
                                        INDrgAffectBudget.Properties.NullText = "No"
                                End Select
                                _affectCFC = .AutoCollect
                                AutomaticCollection = CByte(IIf(_affectCFC, 1, 0))
                                Select Case AutomaticCollection
                                    Case 1
                                        INDrgAutomaticCollection.Properties.NullText = "Si"
                                    Case 0
                                        INDrgAutomaticCollection.Properties.NullText = "No"
                                End Select

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

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.NoteConcept.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecordTreasury(
                                    New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = NoteConcept.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(NoteConcept.Id, Me.Tag.ToString(), Nothing, GetType(NoteConcepts).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewNoteConcept()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlycRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With NoteConcept
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Description = Description
            .AffectBudget = CBool(AffectBudget.GetValueOrDefault)
            '.AffectCreditor = AffectsCreditors
            .AutoCollect = CBool(AutomaticCollection.GetValueOrDefault)
            .Nature = Nature
            .IdMainAccount = AccountAccounting
            .AffectCashFlowConcept = CBool(AffectCashFlowConcept.GetValueOrDefault)
            .IdCashFlowConcept = IdAffectCashFlowConcept
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
                Await ModelCommonTreasury.DeleteBlockRecordTreasury(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Crea un nuevo concepto de nota
    ''' </summary>
    Private Async Function NewNoteConcept() As Task
        NoteConcept = New NoteConcepts() With {.Status = True}
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
                        Using model As New MCommonTreasury(CStr(Me.Tag))
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
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Me.NoteConcept.Code) Then
            Try
                Using model As New MNoteConcepts(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me.NoteConcept.Status
                    Dim result As ActionResult(Of NoteConcepts) = Await model.UpdateStateNoteConcept(Me.NoteConcept.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.NoteConcept = result.ObjectEmbbeded
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
    ''' 
    ''' </summary>
    Public Sub GetCashFlowConcept()
        Dim _presenter As New PCashFlowConcept
        CashFlowConceptDataSource = _presenter.GetCashFlowConcept(New String() {"1", "1,2"})
        _presenter = Nothing
    End Sub

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        Presenter = Nothing
        record = Nothing
        NoteConcept = Nothing
        NatureFile = Nothing
        _ListAffect = Nothing
        AffectBudget = Nothing
        AutomaticCollection = Nothing
        Reversion = Nothing
        AffectCashFlowConcept = Nothing
        IdAffectCashFlowConcept = Nothing
    End Sub
    ''' <summary>
    ''' Handles the Load event of the FrmNoteConcepts control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmNoteConcepts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PNoteConcepts(Me)
        Presenter.InitializeAccountAccounting()
        Presenter.GetSequence()
        Presenter.LoadDefinitionLayout()

        LoadStatus()
        Deshacer()
        CreateNatureFile()
        INDchkReversion_QueryPopUp(Nothing, Nothing)
        INDchkReversion.EditValue = 0

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
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewNoteConcept()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se activa al cargar el formulario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
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
    ''' Evento que se ejecuta al seleccionar un registro desde el Vituelf
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.NoteConcept IsNot Nothing AndAlso Me.NoteConcept.Id > 0 Then
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
    ''' Handles the FormClosing event of the FrmNoteConcepts control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmNoteConcepts_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrgAffectBudget_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDrgAffectBudget.QueryPopUp
        If INDrgAffectBudget.Properties.DataSource Is Nothing Then
            INDrgAffectBudget.Properties.DataSource = ListAffect
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrgAutomaticCollection_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDrgAutomaticCollection.QueryPopUp
        If INDrgAutomaticCollection.Properties.DataSource Is Nothing Then
            INDrgAutomaticCollection.Properties.DataSource = ListAffect
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDchkReversion_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDchkReversion.QueryPopUp
        If INDchkReversion.Properties.DataSource Is Nothing Then
            INDchkReversion.Properties.DataSource = ListAffect
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleAffectCashFlowConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGleAffectCashFlowConcept.QueryPopUp
        If INDGleAffectCashFlowConcept.Properties.DataSource Is Nothing Then
            INDGleAffectCashFlowConcept.Properties.DataSource = ListAffect
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCashFlowConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCashFlowConcept.QueryPopUp
        If INDSleCashFlowConcept.Properties.DataSource Is Nothing Then
            GetCashFlowConcept()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrgAffectBudget_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgAffectBudget.EditValueChanged
        If String.IsNullOrEmpty(INDrgAffectBudget.EditValue) Then
            INDrgAffectBudget.Properties.NullText = String.Empty
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrgAutomaticCollection_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgAutomaticCollection.EditValueChanged
        If String.IsNullOrEmpty(INDrgAutomaticCollection.EditValue) Then
            INDrgAutomaticCollection.Properties.NullText = String.Empty
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDchkReversion_EditValueChanged(sender As Object, e As EventArgs) Handles INDchkReversion.EditValueChanged
        If String.IsNullOrEmpty(INDchkReversion.EditValue) Then
            INDchkReversion.Properties.NullText = String.Empty
        End If
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCashFlowConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCashFlowConcept.EditValueChanged
        If String.IsNullOrEmpty(INDSleCashFlowConcept.EditValue) Then
            INDSleCashFlowConcept.Properties.NullText = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' 
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
            INDGleAffectCashFlowConcept.Properties.NullText = String.Empty
            INDSleCashFlowConcept.EditValue = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton de crear un pais
    ''' </summary>
    Private Sub INDSleCashFlowConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCashFlowConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCashFlowConcept
                Formulario.ViewModeEditHold = True
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                GetCashFlowConcept()
            End Using
        End If
    End Sub

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

End Class