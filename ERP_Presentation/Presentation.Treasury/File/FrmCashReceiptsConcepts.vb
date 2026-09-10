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

Imports DevExpress.Data.Async.Helpers
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Portfolio.MVP
Imports Presentation.Treasury.MVP

#End Region

''' <summary>
''' Formulario de Conceptos de Recibo de Caja
''' </summary>
Public Class FrmCashReceiptsConcepts
    Implements ICashReceiptsConcepts, ICustomizableForm

#Region "Properties and Variables"

    Dim _listAffectation As List(Of Tuple(Of Byte, String))

    ReadOnly Property listAffectation As List(Of Tuple(Of Byte, String))
        Get
            If _listAffectation Is Nothing Then
                _listAffectation = New List(Of Tuple(Of Byte, String))
                _listAffectation.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("AffectPortfoNone", NAME_MODULE)))
                _listAffectation.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("AffectsPortfolio", NAME_MODULE)))
                _listAffectation.Add(New Tuple(Of Byte, String)(3, ResourceManager.GetString("RepaymentAdvances", NAME_MODULE)))
                _listAffectation.Add(New Tuple(Of Byte, String)(4, "Reintegro De Cuentas Por Pagar"))
            End If
            Return _listAffectation
        End Get
    End Property

    ''' <summary>
    ''' Constante con el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements ICashReceiptsConcepts.MyTag
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
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICashReceiptsConcepts.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public Property RubroDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements ICashReceiptsConcepts.RubroDatasource

    ''' <summary>
    ''' variable que contiene la cabecera de la secuencia para este frontal
    ''' </summary>
    Dim _sequence As TreasurySequence

    ''' <summary>
    ''' Contiene el id del detalle de la secuencia
    ''' </summary>
    Dim _idCurrentSequence As Int64

    Public Property Sequence As TreasurySequence Implements ICashReceiptsConcepts.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As TreasurySequence)
            _sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As TreasurySequenceDetail In Me._sequence.TreasurySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the code cash receipts concepts.
    ''' </summary>
    ''' <value>
    ''' The code cash receipts concepts.
    ''' </value>
    Public Property Code As String Implements ICashReceiptsConcepts.Code
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
    Public Property AccountAccounting As Integer Implements ICashReceiptsConcepts.AccountAccounting
        Get
            Return INDsleAccountAccounting.EditValue
        End Get
        Set(value As Integer)
            INDsleAccountAccounting.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si afecta el presupuesto
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [affects budget]; otherwise, <c>false</c>.
    ''' </value>
    Public Property AffectsBudget As Boolean Implements ICashReceiptsConcepts.AffectsBudget
        Get
            Return INDGleAffectBudget.EditValue
        End Get
        Set(value As Boolean)
            INDGleAffectBudget.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece la afectacion
    ''' </summary>
    ''' <value>
    ''' The affectation.
    ''' </value>
    Public Property Affectation As Integer Implements ICashReceiptsConcepts.Affectation
        Get
            Return INDGleAffectation.EditValue
        End Get
        Set(value As Integer)
            INDGleAffectation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece la afectacion del concepto de flujo de caja
    ''' </summary>
    ''' <returns></returns>
    Public Property AffectCashFlowConcept As Boolean? Implements ICashReceiptsConcepts.AffectCashFlowConcept
        Get
            Return CBool(INDGleAffectCashFlowConcept.EditValue)
        End Get
        Set(value As Boolean?)
            INDGleAffectCashFlowConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el id de concepto de flujo de caja
    ''' </summary>
    ''' <returns></returns>
    Public Property IdAffectCashFlowConcept As Integer? Implements ICashReceiptsConcepts.IdAffectCashFlowConcept
        Get
            Return INDSleCashFlowConcept.EditValue
        End Get
        Set(value As Integer?)
            INDSleCashFlowConcept.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece la naturaleza de la cuenta
    ''' </summary>
    ''' <value>
    ''' The character.
    ''' </value>
    Public Property Nature As Integer Implements ICashReceiptsConcepts.Character
        Get
            Return INDgleNature.EditValue
        End Get
        Set(value As Integer)
            INDgleNature.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre del concepto
    ''' </summary>
    ''' <value>
    ''' The name cash receipts concepts.
    ''' </value>
    Public Property NameCashReceiptsConcepts As String Implements ICashReceiptsConcepts.NameCashReceiptsConcepts
        Get
            Return INDtxtName.EditValue
        End Get
        Set(value As String)
            INDtxtName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [state cash receipts concepts].
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [state cash receipts concepts]; otherwise, <c>false</c>.
    ''' </value>
    Public Property StateCashReceiptsConcepts As Boolean Implements ICashReceiptsConcepts.StateCashReceiptsConcepts
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
    Public Property AccountAccountingDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements ICashReceiptsConcepts.AccountAccountingDatasource
        Get
            Return CType(INDsleAccountAccounting.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAccountAccounting.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Presentador de Conceptos de recibo de caja
    ''' </summary>
    Dim Presenter As PCashReceiptsConcepts

    ''' <summary>
    ''' Variable de la entidad de conceptos de recibos de caja
    ''' </summary>
    Dim CashReceiptConcept As CashReceiptConcepts

    ''' <summary>
    ''' variable que contiene el registro bloqueado
    ''' </summary>
    Dim record As BlockRecordTreasury

    ''' <summary>
    ''' Variable que contiene la lista con la naturaleza de la cuenta
    ''' </summary>
    Dim NatureCashReceiptsConcepts As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Establece/Obtiene el datasource de conceptos de flujo de efectivo
    ''' </summary>
    Public Property CashFlowConceptDataSource As DevExpress.Xpo.XPInstantFeedbackSource Implements ICashReceiptsConcepts.CashFlowConceptDataSource
        Get
            Return INDSleCashFlowConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCashFlowConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece la afectacion del concepto de flujo de caja
    ''' </summary>
    ''' <returns></returns>
    Public Property MakeAssociateIncome As Boolean? Implements ICashReceiptsConcepts.MakeAssociateIncome
        Get
            Return CBool(INDGleMakeAssociateIncome.EditValue)
        End Get
        Set(value As Boolean?)
            INDGleMakeAssociateIncome.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece si vincula tercero beneficiario o no
    ''' </summary>
    ''' <returns></returns>
    Public Property LinkThirdPartyBeneficiary As Boolean Implements ICashReceiptsConcepts.LinkThirdPartyBeneficiary
        Get
            Return CBool(INDSleLinkThirdPartyBeneficiary.EditValue)
        End Get
        Set(value As Boolean)
            INDSleLinkThirdPartyBeneficiary.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece si el concepto es para anticipo factura monto fijo
    ''' </summary>
    ''' <returns></returns>
    Public Property IsFixedAmountInvoiceAdvance As Boolean Implements ICashReceiptsConcepts.IsFixedAmountInvoiceAdvance
        Get
            Return CBool(INDSleIsFixedAmountInvoiceAdvance.EditValue)
        End Get
        Set(value As Boolean)
            INDSleIsFixedAmountInvoiceAdvance.EditValue = value
        End Set
    End Property

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
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Me.CashReceiptConcept IsNot Nothing AndAlso Me.CashReceiptConcept.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MCashReceiptsConcepts(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteCashReceiptConcept(Me.CashReceiptConcept)
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
        If Not ValidateControls() Then
            Exit Sub
        End If

        If AffectCashFlowConcept.GetValueOrDefault = 1 AndAlso IdAffectCashFlowConcept.GetValueOrDefault = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("Los siguientes campos son requeridos y no se han diligenciado: {0}Concepto flujo efectivo", vbNewLine)
            Exit Sub
        ElseIf INDGleAffectation.EditValue = 2 AndAlso MakeAssociateIncome Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("Los siguientes campos son requeridos y no se han diligenciado: {0}Obligatorio Vincular Ingreso", vbNewLine)
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MCashReceiptsConcepts(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of CashReceiptConcepts) = Await Model.SaveCashReceiptsConcepts(Me.CashReceiptConcept, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If CashReceiptConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.CashReceiptConcept = result.ObjectEmbbeded
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
            Await Me.NewCashReceiptsConcept()
        End If
    End Sub

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' Funcion que llena PopUp de Comportamiento segun los tipos de cuentas 
    ''' </summary>
    ''' <param name="affectationValue"></param>
    Private Sub InitializeAccounts(ByVal affectationValue As Integer)
        Select Case affectationValue
            Case 1
                Presenter.InitializeAccountAccounting(True)
            Case 2
                Presenter.InitializeAccountResult()
            Case Else
                Presenter.InitializeAccountAccounting(False)
        End Select
    End Sub


    ''' <summary>
    ''' Llena el control con el listado de la naturaleza de la cuenta
    ''' </summary>
    Private Sub CreateNature()
        NatureCashReceiptsConcepts = New List(Of Tuple(Of Integer, String))
        NatureCashReceiptsConcepts.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("AccountNatureDebit")))
        NatureCashReceiptsConcepts.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("AccountNatureCredit")))

        INDgleNature.Properties.DataSource = NatureCashReceiptsConcepts.ToList()
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
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4},
                              New ColumnInfo() With {.Caption = "Comportamiento", .FieldName = "AffectationName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4},
                              New ColumnInfo() With {.Caption = "Naturaleza", .FieldName = "Nature", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Cuenta", .FieldName = "IdMainAccount.NumberName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllCashReceiptConcept
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
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICashReceiptsConcepts.ActionsOnControls
        Set(value As Boolean)
            INDlycRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDGleAffectation.Enabled = value
            INDGleAffectBudget.Enabled = value
            INDGleAffectBudget.Enabled = value
            INDSleBudget.Enabled = value
            INDgleNature.Enabled = value
            INDsleAccountAccounting.Enabled = value
            INDsleAccountAccounting.Enabled = value
            INDGleAffectCashFlowConcept.Enabled = value
            INDSleCashFlowConcept.Enabled = value
            INDGleMakeAssociateIncome.Enabled = value
            INDSleLinkThirdPartyBeneficiary.Enabled = value
            INDSleIsFixedAmountInvoiceAdvance.Enabled = value

            INDSleUser.Enabled = value
            INDSbAdd.Enabled = value
            INDGcUser.Enabled = value

            INDlycRoot.EndUpdate()
            If value Then
                INDtxtName.Focus()
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.CashReceiptConcept.Code, Me.CashReceiptConcept.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.CashReceiptConcept.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.CashReceiptConcept.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.CashReceiptConcept.Code, Me.CashReceiptConcept.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.CashReceiptConcept.Code)
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

        CashReceiptConcept = Nothing
        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDGleAffectBudget.EditValue = False
        INDGleAffectation.EditValue = Nothing
        INDGleAffectation.Properties.NullText = String.Empty
        INDLciAffectsBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciAffectsBudget.AllowHide = True
        INDLciBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciBudget.AllowHide = True
        INDSleBudget.EditValue = Nothing
        INDgleNature.EditValue = Nothing
        INDsleAccountAccounting.EditValue = Nothing
        INDsleAccountAccounting.Properties.NullText = String.Empty
        StateCashReceiptsConcepts = True
        INDGleAffectCashFlowConcept.EditValue = Nothing
        INDGleAffectCashFlowConcept.Properties.NullText = String.Empty
        INDSleCashFlowConcept.EditValue = Nothing
        INDSleCashFlowConcept.Properties.NullText = String.Empty
        MakeAssociateIncome = Nothing
        INDGleMakeAssociateIncome.Properties.NullText = String.Empty
        LinkThirdPartyBeneficiary = False
        IsFixedAmountInvoiceAdvance = False
        INDLciLinkThirdPartyBeneficiary.HideLayout()
        INDLciIsFixedAmountInvoiceAdvance.HideLayout()
        INDGcUser.DataSource = Nothing
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
                Using Model As New MCashReceiptsConcepts(CStr(Me.Tag))
                    AsyncLoader(True)
                    CashReceiptConcept = Await Model.GetCashReceiptConcept(INDbteCode.Text.Trim)
                    INDlycRoot.BeginUpdate()
                    If CashReceiptConcept IsNot Nothing AndAlso CashReceiptConcept.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MCommonTreasury(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecordTreasury(CStr(Me.Tag), CStr(CashReceiptConcept.Id))
                            With CashReceiptConcept
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Code = .Code
                                NameCashReceiptsConcepts = .Name
                                'AutomaticCollection = .AutoCollect
                                Affectation = .Affectation
                                AffectsBudget = .AffectBudget
                                INDSleBudget.EditValue = .BudgetId
                                Select Case .Affectation
                                    Case 1
                                        INDGleAffectation.Properties.NullText = ResourceManager.GetString("AffectPortfoNone", NAME_MODULE)
                                    Case 2
                                        INDGleAffectation.Properties.NullText = ResourceManager.GetString("AffectsPortfolio", NAME_MODULE)
                                    Case 3
                                        INDGleAffectation.Properties.NullText = ResourceManager.GetString("RepaymentAdvances", NAME_MODULE)
                                    Case 4
                                        INDGleAffectation.Properties.NullText = "Reintegro De Cuentas Por Pagar"
                                End Select
                                'Discount = .Discount
                                Nature = .Nature
                                AccountAccounting = .IdMainAccount
                                INDsleAccountAccounting.Properties.NullText = .CodeNameMainAccount
                                StateCashReceiptsConcepts = .Status
                                AffectCashFlowConcept = .AffectCashFlowConcept
                                IdAffectCashFlowConcept = .IdCashFlowConcept
                                If .CashFlowConcept IsNot Nothing Then
                                    INDSleCashFlowConcept.Properties.NullText = String.Format("{0} - {1}", .CashFlowConcept.Code, .CashFlowConcept.NameConcept)
                                End If

                                MakeAssociateIncome = .MakeAssociateIncome
                                LinkThirdPartyBeneficiary = .LinkThirdPartyBeneficiary
                                IsFixedAmountInvoiceAdvance = .IsFixedAmountInvoiceAdvance
                            End With
                            'Llenar NullText
                            INDGcUser.DataSource = CashReceiptConcept.CashReceiptConceptUser.ToList()
                            INDGcUser.RefreshDataSource()
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.CashReceiptConcept.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecordTreasury(
                                    New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = CashReceiptConcept.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(CashReceiptConcept.Id, Me.Tag.ToString(), Nothing, GetType(CashReceiptConcepts).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewCashReceiptsConcept()
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
        With CashReceiptConcept
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameCashReceiptsConcepts
            .Affectation = Affectation
            '.AutoCollect = AutomaticCollection
            .AffectBudget = AffectsBudget
            .BudgetId = INDSleBudget.EditValue
            '.Discount = Discount
            .Nature = Nature
            .IdMainAccount = AccountAccounting
            .AffectCashFlowConcept = CBool(AffectCashFlowConcept.GetValueOrDefault)
            .IdCashFlowConcept = IdAffectCashFlowConcept
            .MakeAssociateIncome = CBool(MakeAssociateIncome.GetValueOrDefault)
            .LinkThirdPartyBeneficiary = LinkThirdPartyBeneficiary
            .IsFixedAmountInvoiceAdvance = IsFixedAmountInvoiceAdvance
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModeloCommonTreasury As New MCommonTreasury(Me.Tag)
                Await ModeloCommonTreasury.DeleteBlockRecordTreasury(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Crea un nuevo concepto de recibo de caja
    ''' </summary>
    Private Async Function NewCashReceiptsConcept() As Task
        CashReceiptConcept = New CashReceiptConcepts() With {.Status = True}
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
                INDsleAccountAccounting.Enabled = False
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
                        Using model As New MEntityAccount(CStr(Me.Tag))
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
        If Not String.IsNullOrEmpty(Me.CashReceiptConcept.Code) Then
            Try
                Using model As New MCashReceiptsConcepts(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not CashReceiptConcept.Status
                    Dim result As ActionResult(Of CashReceiptConcepts) = Await model.UpdateStateCashReceiptConcept(Me.CashReceiptConcept.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.CashReceiptConcept = result.ObjectEmbbeded
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

    Public Sub GetCashFlowConcept()
        Dim _presenter As New PCashFlowConcept
        CashFlowConceptDataSource = _presenter.GetCashFlowConcept(New String() {"1", "1"})
        _presenter = Nothing
    End Sub
#End Region


#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _listAffectation = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        Presenter = Nothing
        CashReceiptConcept = Nothing
        record = Nothing
        NatureCashReceiptsConcepts = Nothing
        AffectCashFlowConcept = Nothing
        IdAffectCashFlowConcept = Nothing
        MakeAssociateIncome = Nothing
    End Sub
    ''' <summary>
    ''' Handles the Load event of the FrmCashReceiptsConcepts control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmCashReceiptsConcepts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, True)
        Me._idOperativeUnit = BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PCashReceiptsConcepts(Me)
        Presenter.GetSequence()
        Presenter.LoadDefinitionLayout()
        Using model As New MPortfolioInitialBalance(Me.Tag)
            INDSleBudget.Properties.DataSource = model.ListBudgetByCategoryItemType()
        End Using
        INDSleBudget.Properties.Buttons(1).Visible = False

        setListActions()
        LoadStatus()
        Deshacer()
        CreateNature()
        InitTuples()
    End Sub

    Private Sub InitTuples()
        Dim ListYesOrNo As New List(Of Tuple(Of Byte, String))()
        ListYesOrNo.Add(New Tuple(Of Byte, String)(0, "No"))
        ListYesOrNo.Add(New Tuple(Of Byte, String)(1, "Si"))

        INDSleLinkThirdPartyBeneficiary.Properties.DataSource = ListYesOrNo
        INDSleIsFixedAmountInvoiceAdvance.Properties.DataSource = ListYesOrNo
        INDGleMakeAssociateIncome.Properties.DataSource = ListYesOrNo
        INDGleAffectCashFlowConcept.Properties.DataSource = ListYesOrNo
    End Sub

    ''' <summary>
    ''' Listado de acciones
    ''' </summary>
    Private Sub setListActions()
        IndigoGridView1.SetListAcction(INDGvUser, {eAcciones.Remove}.ToList())
        Dim col = INDGvUser.Columns.FirstOrDefault(Function(m) m.Name = "colActions")

        If col IsNot Nothing Then
            col.Width = 120
        End If
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmCashReceiptsConcepts control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmCashReceiptsConcepts_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
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
                    Await Me.NewCashReceiptsConcept()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Activated event of the FrmCashReceiptsConcepts control.
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
                InitializeAccounts(Affectation)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al seleccionar un registro desde el Vituelf
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.CashReceiptConcept IsNot Nothing AndAlso Me.CashReceiptConcept.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmThirdPartyAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmThirdPartyAccount_FormClosing(sender As Object, e As FormClosingEventArgs)
        DeleteBlockedRecord()
    End Sub

    Private Sub INDGleType_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGleAffectation.QueryPopUp
        If INDGleAffectation.Properties.DataSource Is Nothing Then
            INDGleAffectation.Properties.DataSource = listAffectation
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
    Private Sub INDGleMakeAssociateIncome_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleMakeAssociateIncome.EditValueChanged
        If MakeAssociateIncome IsNot Nothing Then
            If INDGleAffectation.EditValue = 2 And MakeAssociateIncome = 0 Then
                INDLciLinkThirdPartyBeneficiary.ShowLayout()
            Else
                INDLciLinkThirdPartyBeneficiary.HideLayout()
                LinkThirdPartyBeneficiary = False
            End If
        End If
    End Sub

    Private Sub INDGleAffectation_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleAffectation.EditValueChanged
        INDsleAccountAccounting.Properties.DataSource = Nothing
        INDsleAccountAccounting.EditValue = Nothing
        INDsleAccountAccounting.Properties.NullText = String.Empty

        If INDGleAffectation.EditValue IsNot Nothing Then
            INDsleAccountAccounting.Enabled = True
            If INDGleAffectation.EditValue = 1 Then
                INDLciAffectsBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciAffectsBudget.AllowHide = False

            Else
                INDLciAffectsBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciAffectsBudget.AllowHide = True
                INDGleAffectBudget.EditValue = False
                INDSleBudget.EditValue = Nothing
            End If

            If INDGleAffectation.EditValue = 2 Then
                INDLciIsFixedAmountInvoiceAdvance.ShowLayout()
                INDLciMakeAssociateIncome.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciMakeAssociateIncome.AllowHide = False
            Else
                INDLciIsFixedAmountInvoiceAdvance.HideLayout()
                IsFixedAmountInvoiceAdvance = False
                INDLciMakeAssociateIncome.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciMakeAssociateIncome.AllowHide = True
                MakeAssociateIncome = Nothing
            End If
        Else
            INDGleAffectBudget.EditValue = False
            INDSleBudget.EditValue = Nothing
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
            INDSleCashFlowConcept.EditValue = Nothing
        End If

    End Sub


    Private Sub INDsleAccountAccounting_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleAccountAccounting.QueryPopUp
        If INDsleAccountAccounting.Properties.DataSource Is Nothing Then
            InitializeAccounts(Affectation)
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
        Me.Deshacer()
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


    Private Sub INDGleAffectBudget_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleAffectBudget.EditValueChanged
        If INDGleAffectBudget.EditValue = True Then
            INDLciBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciBudget.AllowHide = False
        Else
            INDLciBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciBudget.AllowHide = True
            INDSleBudget.EditValue = Nothing
        End If
    End Sub

    Private Sub INDSleUser_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleUser.QueryPopUp
        If INDSleUser.Properties.DataSource Is Nothing Then
            INDSleUser.Properties.DataSource = XpoServiceEx.Instance(indigo.SecurityContainer).SecurityService.ListUserByContainer(indigo.IndigoContainerId)
        End If
    End Sub

    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click

        Dim user = TryCast(TryCast(GridView3.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, SecurityRepository.UserXpo)
        If user Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Intente agregar de nuevo el usuario"
            Return
        End If

        If CashReceiptConcept.CashReceiptConceptUser.Any(Function(m) m.UserId = user.Id) Then
            Mensaje(EeventViewerImages.Advertencia) = "El usuario ya se encuentra en la lista"
            INDSleUser.Focus()
            Return
        End If

        Dim newUser As New CashReceiptConceptUser With {.UserId = user.Id, .UserCode = user.UserCode, .FullNameUser = user.PersonFullName}
        CashReceiptConcept.CashReceiptConceptUser.Add(newUser)
        INDGcUser.DataSource = CashReceiptConcept.CashReceiptConceptUser.ToList()
        INDGcUser.RefreshDataSource()
        INDSleUser.EditValue = Nothing
        INDSleUser.Focus()
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show("¿Desea eliminar el registro?", MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
            Return
        End If

        Dim bau = INDGvUser.GetFocusedObject(Of CashReceiptConceptUser)()
        bau.MarkAsDeleted()

        INDGcUser.DataSource = CashReceiptConcept.CashReceiptConceptUser.ToList()
        INDGcUser.RefreshDataSource()
    End Sub
End Class