'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/01/2017
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
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Presentation.Common.MVP
Imports DevExpress.Data.Async.Helpers
Imports Infrastructure.Data.Xpo.TreasuryRepository

#End Region

Public Class FrmConstitutionCashSmaller
    Implements IConstitutionCashSmaller, ICustomizableForm

#Region "Properties"

    ''' <summary>
    ''' Id de la caja mayor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CashRegisterId As Integer? Implements IConstitutionCashSmaller.CashRegisterId
        Get
            Return INDsleCashRegister.EditValue
        End Get
        Set(value As Integer?)
            INDsleCashRegister.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la caja menor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CashRegisterSmallerId As Integer Implements IConstitutionCashSmaller.CashRegisterSmallerId
        Get
            Return INDsleCashRegisterSmaller.EditValue
        End Get
        Set(value As Integer)
            INDsleCashRegisterSmaller.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource caja menor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CashRegisterSmallerXpo As XPInstantFeedbackSource Implements IConstitutionCashSmaller.CashRegisterSmallerXpo
        Get
            Return INDsleCashRegisterSmaller.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCashRegisterSmaller.Properties.DataSource = value
        End Set
    End Property

    Private _selectedCashRegisterSmaller As CashRegisterXpo
    Public Property SelectedCashRegisterSmaller As CashRegisterXpo Implements IConstitutionCashSmaller.SelectedCashRegisterSmaller
        Get
            Return _selectedCashRegisterSmaller
        End Get
        Set(value As CashRegisterXpo)
            _selectedCashRegisterSmaller = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource caja mayor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CashRegisterXpo As XPInstantFeedbackSource Implements IConstitutionCashSmaller.CashRegisterXpo
        Get
            Return INDsleCashRegister.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCashRegister.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IConstitutionCashSmaller.Code
        Get
            If INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
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
    ''' Fecha documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date? Implements IConstitutionCashSmaller.DocumentDate
        Get
            Return INDdteDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentType As Integer Implements IConstitutionCashSmaller.DocumentType
        Get
            Return INDsleDocuementType.EditValue
        End Get
        Set(value As Integer)
            INDsleDocuementType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la cuenta bancaria
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EntityBankAccountId As Integer? Implements IConstitutionCashSmaller.EntityBankAccountId
        Get
            Return INDsleEntityBankAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleEntityBankAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource cuenta bancaria
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EntityBankAccountXpo As XPInstantFeedbackSource Implements IConstitutionCashSmaller.EntityBankAccountXpo
        Get
            Return INDsleEntityBankAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEntityBankAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IConstitutionCashSmaller.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Tag
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IConstitutionCashSmaller.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Secuencia numérica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequence As TreasurySequence Implements IConstitutionCashSmaller.Sequence
        Get
            Return Me._sequense
        End Get
        Set(value As TreasurySequence)
            Me._sequense = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.TreasurySequenceDetail In Me._sequense.TreasurySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Tipo fuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SourceType As Integer Implements IConstitutionCashSmaller.SourceType
        Get
            Return INDsleSourceType.EditValue
        End Get
        Set(value As Integer)
            INDsleSourceType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value As Decimal Implements IConstitutionCashSmaller.Value
        Get
            Return INDseValue.EditValue
        End Get
        Set(value As Decimal)
            INDseValue.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordTreasury

    ''' <summary>
    ''' Secuencia numerica del fomulario
    ''' </summary>
    Private Property _sequense As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Presentador de concepto de notas
    ''' </summary>
    Dim Presenter As PConstitutionCashSmaller

    Dim ConstitutionCashSmaller As ConstitutionCashSmaller

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' Tipo de docuemnto
    ''' </summary>
    Dim ListDocumentType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Tipo fuente
    ''' </summary>
    Dim ListSourceType As New List(Of Tuple(Of Integer, String))

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ConstitutionCashSmaller.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If
            AssigningValues()
        End If
        Try
            Using Model As New MConstitutionCashSmaller(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of ConstitutionCashSmaller) = Await Model.SaveConstitutionCashSmaller(Me.ConstitutionCashSmaller, Me._idCurrentSequense)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If ConstitutionCashSmaller.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._idCurrentSequense).RemoveAt(0)
                        End If
                    End If
                    Me.ConstitutionCashSmaller = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            Await Me.NewConstitutionCashSmaller()
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub SetCultureBaseOnCashRegisterCurrency()
        If SelectedCashRegisterSmaller Is Nothing Then
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = New Globalization.CultureInfo(SelectedCashRegisterSmaller.CommonCurrency.Abbreviation.GetCultureId()).NumberFormat
        INDseValue.Properties.Mask.Culture = _culture

        Presenter.InitializeCashRegister()
        Presenter.InitializeEntityBankAccount()
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para crear una nueva dependencia
    ''' </summary>
    Private Async Function NewConstitutionCashSmaller() As Task
        ConstitutionCashSmaller = New ConstitutionCashSmaller()
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
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
                    Exit Function
                End If
            End If
            If Me._sequense.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MCommonTreasury(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
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
    End Function

    ''' <summary>
    ''' Carga los estados de la devolucion de solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.ConstitutionCashSmaller IsNot Nothing AndAlso Me.ConstitutionCashSmaller.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Metodo que carga los controles de la devolucion de solicitudes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MConstitutionCashSmaller(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetConstitutionCashSmaller(INDbtnCode.Text.Trim)
                    ConstitutionCashSmaller = resultOperation.ObjectEmbbeded
                    INDlyRoot.BeginUpdate()
                    If ConstitutionCashSmaller IsNot Nothing AndAlso ConstitutionCashSmaller.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MCommonTreasury(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecordTreasury(CStr(Me.Tag), CStr(ConstitutionCashSmaller.Id))
                            With ConstitutionCashSmaller
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                DocumentDate = .DocumentDate
                                DocumentType = .DocumentType

                                CashRegisterSmallerId = .CashRegisterSmallerId
                                INDsleCashRegisterSmaller.Properties.NullText = .CashRegisterSmallerDescription

                                SourceType = .SourceType

                                CashRegisterId = .CashRegisterId
                                INDsleCashRegister.Properties.NullText = .CashRegisterDescription

                                EntityBankAccountId = .EntityBankAccountId
                                INDsleEntityBankAccount.Properties.NullText = .EntityBankAccountDescription

                                Value = .Value

                                Me.BarraBotones.StatusRecordVisible = True
                                Me.BarraBotones.StatusRecord = .Status.ToString
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.ConstitutionCashSmaller.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecordTreasury(
                                New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = ConstitutionCashSmaller.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            If ConstitutionCashSmaller.Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Me.ReadOnlyControls(False)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                Me.ReadOnlyControls(True)
                            End If
                            Me.BarraBotones.SetDocuments(ConstitutionCashSmaller.Id, Me.Tag.ToString(), Nothing, GetType(ConstitutionCashSmaller).Name)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, ConstitutionCashSmaller.Id, 0, ConstitutionCashSmaller.Id)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequense.IsManual Then
                            Await Me.NewConstitutionCashSmaller()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.ConstitutionCashSmaller.Code),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.ConstitutionCashSmaller.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.ConstitutionCashSmaller.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.ConstitutionCashSmaller.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.ConstitutionCashSmaller.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Método que elimina el registro guardado para concurrencia
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub DeleteBlockedRecord()

        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MCommonTreasury(CStr(Me.Tag))
                Await Model.DeleteBlockRecordTreasury(record)
                record = Nothing
            End Using
        End If

    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IConstitutionCashSmaller.ActionsOnControls
        Set(value As Boolean)
            INDlyRoot.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDdteDocumentDate.Enabled = value
            INDsleDocuementType.Enabled = value
            INDsleCashRegisterSmaller.Enabled = value
            INDsleSourceType.Enabled = value
            INDsleCashRegister.Enabled = value
            INDsleEntityBankAccount.Enabled = value
            INDseValue.Enabled = value
            Me.BarraBotones.StatusRecordVisible = value
            INDlyRoot.EndUpdate()
            If value Then
                INDdteDocumentDate.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Llena el control de comportamiento con el listado
    ''' </summary>
    Private Sub InitializeTuples()
        ListDocumentType = New List(Of Tuple(Of Integer, String))
        ListDocumentType.Add(New Tuple(Of Integer, String)(1, "Aumento de Caja Menor"))
        ListDocumentType.Add(New Tuple(Of Integer, String)(2, "Disminución de Caja Menor"))
        INDsleDocuementType.Properties.DataSource = ListDocumentType.ToList()

        ListSourceType = New List(Of Tuple(Of Integer, String))
        ListSourceType.Add(New Tuple(Of Integer, String)(1, "Caja Mayor"))
        ListSourceType.Add(New Tuple(Of Integer, String)(2, "Cuenta Bancaria"))
        INDsleSourceType.Properties.DataSource = ListSourceType.ToList()
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue

        Dim ListDocumentTypeOS As New List(Of Tuple(Of String, Integer))
        ListDocumentTypeOS.Add(New Tuple(Of String, Integer)("Aumento de Caja Menor", 1))
        ListDocumentTypeOS.Add(New Tuple(Of String, Integer)("Disminución de Caja Menor", 2))

        Dim ListSourceTypeOS As New List(Of Tuple(Of String, Integer))
        ListSourceTypeOS.Add(New Tuple(Of String, Integer)("Caja Mayor", 1))
        ListSourceTypeOS.Add(New Tuple(Of String, Integer)("Cuenta Bancaria", 2))

        Dim ListStatusOS As New List(Of Tuple(Of String, Integer))
        ListStatusOS.Add(New Tuple(Of String, Integer)("Sin Confirmar", 1))
        ListStatusOS.Add(New Tuple(Of String, Integer)("Confirmado", 2))
        ListStatusOS.Add(New Tuple(Of String, Integer)("Anulado", 3))

        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.16)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.16)},
                              New ColumnInfo() With {.Caption = "Tipo Documento", .FieldName = "DocumentType", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.16), .ColumnEdit = True, .ListItemsDatasourceColumEdit = ListDocumentTypeOS},
                              New ColumnInfo() With {.Caption = "Caja Menor", .FieldName = "CashRegisterSmallerId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.16)},
                              New ColumnInfo() With {.Caption = "Tipo Fuente", .FieldName = "SourceType", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.16), .ColumnEdit = True, .ListItemsDatasourceColumEdit = ListSourceTypeOS},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.16), .ColumnEdit = True, .ListItemsDatasourceColumEdit = ListStatusOS}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListConstitutionCashSmaller
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
        DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyRoot.BeginUpdate()
        Me.ReadOnlyControls(False)

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        Code = String.Empty
        DocumentDate = Nothing
        DocumentType = Nothing
        CashRegisterSmallerId = Nothing
        INDsleCashRegisterSmaller.Properties.NullText = String.Empty
        SourceType = Nothing

        CashRegisterId = Nothing
        INDsleCashRegister.Properties.NullText = String.Empty
        INDlyItemCasRegister.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemCasRegister.AllowHide = True

        EntityBankAccountId = Nothing
        INDsleEntityBankAccount.Properties.NullText = String.Empty
        INDlyItemEntityBankAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemEntityBankAccount.AllowHide = True

        Value = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        INDlyRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With ConstitutionCashSmaller
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .DocumentType = DocumentType
            .CashRegisterSmallerId = CashRegisterSmallerId
            .SourceType = SourceType
            If INDlyItemCasRegister.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .CashRegisterId = CashRegisterId
            Else
                .CashRegisterId = Nothing
            End If
            If INDlyItemEntityBankAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .EntityBankAccountId = EntityBankAccountId
            Else
                .EntityBankAccountId = Nothing
            End If
            .Value = Value
            .OperatingUnitId = BarraBotones.OperatingUnit.Id

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _idCurrentSequense = Nothing
        record = Nothing
        _sequense = Nothing
        Presenter = Nothing
        ConstitutionCashSmaller = Nothing
        ListDocumentType = Nothing
        ListSourceType = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmConstitutionCashSmaller_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PConstitutionCashSmaller(Me)
        Presenter.LoadDefinitionLayout()
        Presenter.GetSequence()
        Deshacer()
        InitializeTuples()
        LoadStatus()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "KeyDown"

    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequense Is Nothing OrElse _sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewConstitutionCashSmaller()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se ejecuta al desplegar el control de caja menor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCashRegisterSmaller_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCashRegisterSmaller.QueryPopUp
        If CashRegisterSmallerXpo Is Nothing Then
            Presenter.InitializeCashRegisterSmaller()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al desplegar el control de caja mayor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCashRegister_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCashRegister.QueryPopUp
        If CashRegisterXpo Is Nothing Then
            Presenter.InitializeCashRegister()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al desplegar el control de cuenta bancaria a entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntityBankAccount_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntityBankAccount.QueryPopUp
        If EntityBankAccountXpo Is Nothing Then
            Presenter.InitializeEntityBankAccount()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del tipo de fuente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSourceType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSourceType.EditValueChanged
        If SourceType <> Nothing Then
            If SourceType = 1 Then 'Caja Mayor
                INDlyItemCasRegister.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemCasRegister.AllowHide = False

                INDlyItemEntityBankAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemEntityBankAccount.AllowHide = True
            Else 'Cuenta Bancaria
                INDlyItemCasRegister.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemCasRegister.AllowHide = True

                INDlyItemEntityBankAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemEntityBankAccount.AllowHide = False
            End If
        End If
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmConstitutionCashSmaller_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento para abrir el form de cajas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCashRegisterSmaller_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCashRegisterSmaller.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(626, Nothing, True)
            Presenter.InitializeCashRegisterSmaller()
        End If
    End Sub

    ''' <summary>
    ''' Evento para abrir el form de cajas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCashRegister_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCashRegister.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(626, Nothing, True)
            Presenter.InitializeCashRegister()
        End If
    End Sub

    ''' <summary>
    ''' Evento para abrir el form de cuenta bancaria a entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntityBankAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntityBankAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(628, Nothing, True)
            Presenter.InitializeEntityBankAccount()
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' LOAD: Carga los permisos de la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' CLICK_GUARDAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        ConstitutionCashSmaller.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' CLICK_ACTUALIZAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        ConstitutionCashSmaller.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' CLICK_ANULAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        ConstitutionCashSmaller.Status = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' CLICK_GUARDAR_CONFIRMAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        ConstitutionCashSmaller.Status = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' CLICK_ACTUALIZAR_COFIRMAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        ConstitutionCashSmaller.Status = 2
        Guardar()
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
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, ConstitutionCashSmaller.Id, 0, ConstitutionCashSmaller.Code)
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

    Private Sub INDsleCashRegisterSmaller_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCashRegisterSmaller.EditValueChanged
        SelectedCashRegisterSmaller = TryCast(TryCast(GridView1.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, CashRegisterXpo)
        SetCultureBaseOnCashRegisterCurrency()
    End Sub

#End Region

End Class