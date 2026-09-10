'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 19-03-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Accounting.MVP
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.ComponentModel
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports System.Windows.Forms
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo.AccountingRepository

#End Region

Public Class FrmPopupPUC
    Implements IPUC

#Region "Constant"

    ''' <summary>
    ''' Constante con el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Accounting"

#End Region

#Region "Fields"

    ''' <summary>
    ''' presenter
    ''' </summary>
    Private Presenter As PPUC

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private indigoValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable para manejar el registro bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Private record As BlockRecordGeneralLedger

    ''' <summary>
    ''' miembro privado para el objeto de cuentas
    ''' </summary>
    Private _Account As MainAccounts

    ''' <summary>
    ''' The list account class
    ''' </summary>
    Private ListAccountClass As List(Of MainAccountClasses)

    ''' <summary>
    ''' The list account level
    ''' </summary>
    Private ListAccountLevel As List(Of MainAccountLevels)

    ''' <summary>
    ''' The mode search
    ''' </summary>
    Public ModeSearch As Boolean

    Private listAviable As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Cargar el datasource de tipo de retencion
    ''' </summary>
    ''' <remarks></remarks>
    Private listRetencionType As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Cargar el datasource de naturaleza
    ''' </summary>
    ''' <remarks></remarks>
    Private listNature As List(Of Tuple(Of Integer, String))

    Private ListTypeRestriction As List(Of Tuple(Of Integer, String))

    Private ListTypeRestrictionCostCenter As List(Of Tuple(Of Integer, String))

    Private ListTypeRestrictionThirdParty As List(Of Tuple(Of Integer, String))

    Private ListRestriction As List(Of Tuple(Of Integer, String))

    Private Property PucXpo As XPInstantFeedbackSource

    Private Property _AccountXpo As XPInstantFeedbackSource

    Private thirdXpo As XPInstantFeedbackSource

    Private _accountMayor As MainAccounts

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad que permite inicializar la tupla del search de disponibilidad-tipo
    ''' </summary>
    Private WriteOnly Property SetListAvailabilityOrType As Integer
        Set(value As Integer)
            listAviable = New List(Of Tuple(Of Integer, String))

            If value = 1 Then 'Si el tipo de la clase es Balance
                listAviable.Add(New Tuple(Of Integer, String)(0, "Ninguna"))
                listAviable.Add(New Tuple(Of Integer, String)(1, "Corriente"))
                listAviable.Add(New Tuple(Of Integer, String)(2, "No Corriente"))
                listAviable.Add(New Tuple(Of Integer, String)(3, "Ambas"))
                INDlyItemAvailability.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemAvailability.Text = "Disponibilidad"
            ElseIf value = 2 Then 'Si el tipo de la clase es Resultado
                listAviable.Add(New Tuple(Of Integer, String)(4, "Operacional"))
                listAviable.Add(New Tuple(Of Integer, String)(5, "No Operacional"))
                INDlyItemAvailability.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemAvailability.Text = "Tipo"
            Else 'Si es diferente a balance y resultado se oculta el label de disponibilidad-tipo
                INDlyItemAvailability.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            indGlAvailability.Properties.DataSource = listAviable
        End Set
    End Property

    Private _LegalBookId As Integer
    ''' <summary>
    ''' Obtiene el id del libro oficial que viene del form principal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LegalBookId As Integer
        Get
            Return _LegalBookId
        End Get
        Set(value As Integer)
            _LegalBookId = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el codigo de la clase de cuenta
    ''' </summary>
    ''' <returns></returns>
    Public Property CodeAccountingClass As String Implements IPUC.CodeAccountingClass
        Get
            Return indtxtCode.Text.Trim
        End Get
        Set(value As String)
            indtxtCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el estado de la cuenta
    ''' </summary>
    ''' <returns></returns>
    Public Property State As Boolean Implements IPUC.State
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
    ''' obtiene o establece la cuenta contable
    ''' </summary>
    ''' <value>
    ''' la cuenta contable
    ''' </value>
    Public Property Account As MainAccounts
        Get
            Return _Account
        End Get
        Set(value As MainAccounts)
            _Account = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the level xpo.
    ''' </summary>
    ''' <value>
    ''' The level xpo.
    ''' </value>
    Public Property LevelXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IPUC.LevelXpo
        Get
            Return CType(indGlLevel.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            indGlLevel.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la clases de cuentas contables
    ''' </summary>
    ''' <value>
    ''' The class acounting xpo.
    ''' </value>
    Public Property ClassAcountingXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IPUC.ClassAcountingXpo
        Get
            Return CType(indGlAccountClass.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            indGlAccountClass.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Asigna el valor de activo o inactivo a los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            indtxtCode.Enabled = Not value
            indTxtName.Enabled = value
            INDsleNatureAccountingClass.Enabled = value
            INDsleCloseThirdParty.Enabled = value
            indGlNit.Enabled = value
            indGlAvailability.Enabled = value
            INDsleShowCGN2.Enabled = value
            INDsleHandlesBase.Enabled = value
            If _Account IsNot Nothing AndAlso _Account.hasMovements Then
                INDsleHandlesThirdParty.Enabled = False
                INDsleHandlesCostCenter.Enabled = False
                INDsleHandlesCostCenterRestriction.Enabled = False
                INDsleHandlesThirdPartyRestriction.Enabled = False
                INDsleRetencionType.Enabled = False
                INDsleReconcileAccount.Enabled = False
            Else
                INDsleHandlesThirdParty.Enabled = value
                INDsleHandlesCostCenter.Enabled = value
                INDsleHandlesCostCenterRestriction.Enabled = value
                INDsleHandlesThirdPartyRestriction.Enabled = value
                INDsleRetencionType.Enabled = value
                INDsleReconcileAccount.Enabled = value
            End If
        End Set
    End Property

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
    ''' maneja base, por defecto No
    ''' </summary>
    ''' <returns></returns>
    Property HandleBase As Boolean Implements IPUC.HandleBase
        Get
            Return CType(INDsleHandlesBase.EditValue, Boolean)
        End Get
        Set(value As Boolean)
            INDsleHandlesBase.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el estado Maneja restriccion de centro de costo
    ''' </summary>
    ''' <returns></returns>
    Public Property HandlesCostCenterRestriction As Boolean Implements IPUC.HandlesCostCenterRestriction
        Get
            Return INDsleHandlesCostCenterRestriction.EditValue
        End Get
        Set(value As Boolean)
            INDsleHandlesCostCenterRestriction.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el estado maneja restriccion de tercero
    ''' </summary>
    ''' <returns></returns>
    Public Property HandlesThirdPartyRestriction As Boolean Implements IPUC.HandlesThirdPartyRestriction
        Get
            Return INDsleHandlesThirdPartyRestriction.EditValue
        End Get
        Set(value As Boolean)
            INDsleHandlesThirdPartyRestriction.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el dato si es de centro de costo o tercero
    ''' </summary>
    ''' <returns></returns>
    Public Property CostCenterRule As Integer
        Get
            Return INDsleCostCenterRule.EditValue
        End Get
        Set(value As Integer)
            INDsleCostCenterRule.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el dato si es de centro de costo o tercero
    ''' </summary>
    ''' <returns></returns>
    Public Property ThirdPartieRule As Integer
        Get
            Return INDsleThirdPartie.EditValue
        End Get
        Set(value As Integer)
            INDsleThirdPartie.EditValue = value
        End Set
    End Property

    Public Property HandlesCostCenter As Boolean
        Get
            Return INDsleHandlesCostCenter.EditValue
        End Get
        Set(value As Boolean)
            INDsleHandlesCostCenter.EditValue = value
        End Set
    End Property

    Public Property HandlesThirdParty As Boolean
        Get
            Return INDsleHandlesThirdParty.EditValue
        End Get
        Set(value As Boolean)
            INDsleHandlesThirdParty.EditValue = value
        End Set
    End Property

    Dim MainAccountRestrictions As MainAccountRestrictions

    ''' <summary>
    ''' variable que se utiliza para almacenar las nuevas restriccion de las cuentas contables
    ''' </summary>
    Private ListNewMainAccountRestrictions As List(Of MainAccountRestrictions)

    ''' <summary>
    ''' variable que se utiliza para almacenar las nuevas restriccion de las cuentas contables
    ''' </summary>
    Private ListNewMainAccountRestrictionsDelete As List(Of MainAccountRestrictions)

#End Region

#Region "Methods"

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        indtxtCode.Text = ReturnValue
        If indtxtCode.Text <> String.Empty Then
            Await LoadControls()
            If indtxtCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            indtxtCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MPUC(MyBase.Tag)
                Await Model.DeleteBlockRecord(record)
            End Using
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Cargamos los estados en la barra de usuario
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' metodo para asignar los valores
    ''' </summary>
    Private Sub AssignValues()

        With _Account
            .IdAccountLevel = indGlLevel.EditValue
            .IdAccountClass = indGlAccountClass.EditValue
            .Number = indtxtCode.Text
            .Name = indTxtName.Text
            .Nature = CByte(INDsleNatureAccountingClass.EditValue)
            .HandlesThirdParty = INDsleHandlesThirdParty.EditValue
            .CloseThirdParty = INDsleCloseThirdParty.EditValue
            .IdThirdParty = indGlNit.EditValue
            .ReconcileAccount = INDsleReconcileAccount.EditValue

            If INDlyItemAvailability.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .Availability = CInt(indGlAvailability.EditValue)
            Else
                .Availability = 0
            End If

            .HandlesCostCenter = INDsleHandlesCostCenter.EditValue
            .HandlesCostCenterRestriction = HandlesCostCenterRestriction
            .HandlesThirdPartyRestriction = HandlesThirdPartyRestriction
            .RetencionType = CInt(INDsleRetencionType.EditValue)
            .FreelancerCategory = INDsleFreelancerCategory.EditValue
            .LegalBookId = LegalBookId
            .ShowCGN2 = INDsleShowCGN2.EditValue
            .HandleBase = Me.HandleBase

			If ListNewMainAccountRestrictions IsNot Nothing Then
				For Each itemRule As MainAccountRestrictions In ListNewMainAccountRestrictions
					.MainAccountRestrictions.Add(itemRule)
				Next
			End If
			If ListNewMainAccountRestrictionsDelete IsNot Nothing Then
				For Each itemRule As MainAccountRestrictions In ListNewMainAccountRestrictionsDelete
					ListNewMainAccountRestrictions.Add(itemRule)
					.MainAccountRestrictions.Add(itemRule)
				Next
			End If

			If Not _Account.AllowsMovement Then
                If ListNewMainAccountRestrictions IsNot Nothing Then
                    For Each deleteItem In ListNewMainAccountRestrictions
                        If Not deleteItem.Id > 0 Then
                            .MainAccountRestrictions.Remove(deleteItem)
                        End If
                    Next
                End If
            End If
        End With
    End Sub

    ''' <summary>
    ''' Metodo para cargar los controles
    ''' </summary>
    Private Async Function LoadControls() As Task

        If CodeAccountingClass = "" Then
            Exit Function
        End If

        If ListAccountLevel Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay creadas Niveles de Cuentas"
            Exit Function
        End If

        AsyncLoader(True)
        Using Model As New MPUC(Me.Tag)
            Dim levelMainAccountObject = (From d In ListAccountLevel Where d.digits = CodeAccountingClass.Length Select d).FirstOrDefault()
            If levelMainAccountObject Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No existe un nivel que coincida con la cantidad de digitos que tiene la cuenta"
                AsyncLoader(False)
                Deshacer()
                Exit Function
            End If

            Dim allowMovement = False

            indGlLevel.EditValue = levelMainAccountObject.Id

            If levelMainAccountObject.Level <> 5 Then
                groupauxiliary.HideControl()
                groupHandledCostCenterHideControl()
            Else
                groupauxiliary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                groupHandledCostCenterHideControl(False)
                allowMovement = True
            End If

            If levelMainAccountObject.Level = 1 Then
                indGlAccountClass.Enabled = True
            Else
                indGlAccountClass.Enabled = False
                Dim accountParent = CodeAccountingClass.Substring(0, levelMainAccountObject.digits - levelMainAccountObject.Length)
                _accountMayor = Await Model.GetAccountParentByCode(accountParent, LegalBookId)
                If _accountMayor Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "No existe un cuenta padre que coincida con el nivel"
                    AsyncLoader(False)
                    Deshacer()
                    Exit Function
                End If

                If _accountMayor.GeneralLedgerBalance.Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "La cuenta contable padre " + _accountMayor.Number + " ya tiene movimientos y no puede crear mas subcuentas"
                    AsyncLoader(False)
                    Deshacer()
                    Exit Function
                End If

                indGlAccountClass.EditValue = _accountMayor.IdAccountClass
                IndGlGreaterAccountCode.Text = _accountMayor.Number
                IndTxtGreaterAccountName.Text = _accountMayor.Name
                ShowOrHideHandleBase(_accountMayor?.Nature = 1 AndAlso _accountMayor?.MainAccountClasses?.Type = 2 AndAlso levelMainAccountObject?.Level = 5)
            End If

            _Account = Await Model.GetAccountByCodeAndLegalBookId(CodeAccountingClass, LegalBookId)
            If _Account IsNot Nothing AndAlso _Account.Id > 0 Then
                Dim result = Await Model.GetBlockRecord(Me.Tag, _Account.Id)
                With _Account
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                    indtxtCode.Text = _Account.Number
                    indTxtName.Text = _Account.Name

                    'Se creo el campo en la BD de Nature para la tabla MainAccount y se creo que permitiera nulo.
                    If .Nature IsNot Nothing Then 'Se valida que no sea nula la naturaleza
                        INDsleNatureAccountingClass.EditValue = .Nature
                    Else 'Si es nula se postula la naturaleza de la clase contable
                        INDsleNatureAccountingClass.EditValue = _Account.MainAccountClasses.Nature
                    End If
                    INDsleHandlesThirdParty.EditValue = _Account.HandlesThirdParty
                    INDsleCloseThirdParty.EditValue = _Account.CloseThirdParty
                    indGlNit.EditValue = _Account.IdThirdParty
                    INDsleRetencionType.EditValue = _Account.RetencionType
                    indGlAvailability.EditValue = _Account.Availability
                    INDsleHandlesCostCenter.EditValue = _Account.HandlesCostCenter
                    HandlesCostCenterRestriction = _Account.HandlesCostCenterRestriction
                    HandlesThirdPartyRestriction = _Account.HandlesThirdPartyRestriction
                    INDsleReconcileAccount.EditValue = _Account.ReconcileAccount
                    INDsleFreelancerCategory.EditValue = _Account.FreelancerCategory
                    INDsleShowCGN2.EditValue = _Account.ShowCGN2
                    State = .Status
                    Me.HandleBase = .HandleBase

                    If _Account.AllowsMovement = True Then
                        groupauxiliary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        groupHandledCostCenterHideControl(False)

                    Else
                        groupauxiliary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                        INDlygRules.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        CleandControlsPopup()
                        groupHandledCostCenterHideControl()
                    End If

                    Await loadRules()

                    Me.BarraBotones.StatusRecordVisible = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)

                    ActionsOnControls = True

                    indTxtName.Focus()
                End With
                Me.GetDocumentIndexed(Me.Tag & "_" & Me._Account.Number)
                If result.Id = 0 Then
                    Me.BarraBotones.SetDocuments(_Account.Id)
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    record = New BlockRecordGeneralLedger With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _Account.Id}
                    Dim operation = Await Model.SaveBlockRecord(record)
                    record = operation.ObjectEmbbeded
                Else
                    Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                    record = result
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
            Else
                _Account = New MainAccounts With {.Status = True, .AllowsMovement = allowMovement}

                'valores por defecto al crear una nueva cuenta
                INDsleNatureAccountingClass.EditValue = _accountMayor.Nature
                INDsleHandlesThirdParty.EditValue = False
                INDsleCloseThirdParty.EditValue = False
                INDsleRetencionType.EditValue = 0
                INDsleHandlesCostCenter.EditValue = False
                HandlesCostCenterRestriction = False
                HandlesThirdPartyRestriction = False
                INDsleReconcileAccount.EditValue = False
                INDsleFreelancerCategory.EditValue = False
                INDsleShowCGN2.EditValue = False
                Me.HandleBase = False

                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                Me.BarraBotones.StatusRecordVisible = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)

                ActionsOnControls = True

                indTxtName.Focus()
            End If
        End Using
        AsyncLoader(False)
    End Function

    ''' <summary>
    ''' Método que limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        indtxtCode.EditValue = Nothing
        indTxtName.Text = ""
        indGlLevel.EditValue = Nothing
        IndGlGreaterAccountCode.EditValue = Nothing
        IndTxtGreaterAccountName.Text = ""
        INDsleNatureAccountingClass.EditValue = Nothing
        INDsleHandlesThirdParty.EditValue = Nothing
        indGlNit.EditValue = Nothing
        INDsleCloseThirdParty.EditValue = Nothing
        INDsleRetencionType.EditValue = Nothing
        indGlAvailability.EditValue = Nothing
        INDsleHandlesCostCenter.EditValue = Nothing
        HandlesCostCenterRestriction = Nothing
        HandlesThirdPartyRestriction = Nothing
        INDsleReconcileAccount.EditValue = Nothing
        ActionsOnControls = False
        indGlAccountClass.Enabled = False
        IndGlGreaterAccountCode.Enabled = False
        IndTxtGreaterAccountName.Enabled = False
        ShowOrHideHandleBase(False)

        indGlAccountClass.EditValue = Nothing
        INDsleNatureAccountingClass.EditValue = Nothing
        BarraBotones.PrepareToolbar(eAction.OnlyNew)
        indtxtCode.Focus()
        UnblockedRecord()
        groupHandledCostCenterHideControl()
        groupauxiliary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlygRules.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ListNewMainAccountRestrictions = Nothing
        INDgcRules.DataSource = Nothing
        INDgcRules.RefreshDataSource()
        indTxtName.Enabled = False
		INDsleShowCGN2.EditValue = Nothing
		ListNewMainAccountRestrictionsDelete = Nothing
	End Sub

    ''' <summary>  
    ''' funcion para validar los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsForm() As Boolean
        'Si el codigo se modifica y este tiene dependencia
        If _Account.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Added And indtxtCode.Text <> _Account.Number And Presenter.GetAccountParent(_Account.Id) Then
            Mensaje(EeventViewerImages.Advertencia) = "No puedes modificar el código de la cuenta cuando esta tiene dependencia"
            Return False
        End If

        If indtxtCode.Text Is String.Empty Then
            Return False
        ElseIf indTxtName.Text Is String.Empty Then
            Return False
        ElseIf indGlLevel.EditValue Is Nothing Then
            Return False
        ElseIf indGlAccountClass.EditValue Is Nothing Then
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Desbloquea el registro bloqueado
    ''' </summary>
    Private Async Sub UnblockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 Then
            Using model As New MPUC(Me.Tag.ToString())
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource de las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        listRetencionType = New List(Of Tuple(Of Integer, String))
        listRetencionType.Add(New Tuple(Of Integer, String)(0, "Ninguna"))
        listRetencionType.Add(New Tuple(Of Integer, String)(1, "ReteFuente"))
        listRetencionType.Add(New Tuple(Of Integer, String)(2, "ReteIva"))
        listRetencionType.Add(New Tuple(Of Integer, String)(3, "ReteIca"))
        listRetencionType.Add(New Tuple(Of Integer, String)(4, "Otras"))
        INDsleRetencionType.Properties.DataSource = listRetencionType.ToList

        listNature = New List(Of Tuple(Of Integer, String))
        listNature.Add(New Tuple(Of Integer, String)(1, "Debito"))
        listNature.Add(New Tuple(Of Integer, String)(2, "Credito"))
        INDsleNatureAccountingClass.Properties.DataSource = listNature.ToList

        ListTypeRestriction = New List(Of Tuple(Of Integer, String))
        ListTypeRestriction.Add(New Tuple(Of Integer, String)(1, "Centro de Costos"))
        ListTypeRestriction.Add(New Tuple(Of Integer, String)(2, "Terceros"))

        ListTypeRestrictionCostCenter = New List(Of Tuple(Of Integer, String))
        ListTypeRestrictionCostCenter.Add(New Tuple(Of Integer, String)(1, "Centro de Costos"))

        ListTypeRestrictionThirdParty = New List(Of Tuple(Of Integer, String))
        ListTypeRestrictionThirdParty.Add(New Tuple(Of Integer, String)(2, "Terceros"))

        ListRestriction = New List(Of Tuple(Of Integer, String))
        ListRestriction.Add(New Tuple(Of Integer, String)(1, "Habilitado"))
        ListRestriction.Add(New Tuple(Of Integer, String)(2, "Deshabilitado"))
        INDsleRestription.Properties.DataSource = ListRestriction.ToList
    End Sub

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Me.Account.Number) Then
            Try
                Using Model As New MPUC(Me.Tag)
                    AsyncLoader(True)
                    Dim statePuc As Boolean = Not _Account.Status
                    Dim Result = Await Model.UpdateStatePUC(Me.Account.Number, LegalBookId, statePuc)
                    AsyncLoader(False)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        Me.Account = Result.ObjectEmbbeded
                    Else
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    Public Async Function loadRules() As Task
        If ListNewMainAccountRestrictions Is Nothing Then
            ListNewMainAccountRestrictions = New List(Of MainAccountRestrictions)
        End If
        ListNewMainAccountRestrictions = Await Task.Factory.StartNew(Function()
                                                                         Dim listXpo As XPCollection(Of MainAccountRestrictionsXpo) = Presenter.ListMainAccountRestriction(Account.Id)

                                                                         If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
                                                                             For Each itemXpo As MainAccountRestrictionsXpo In listXpo
                                                                                 MainAccountRestrictions = New MainAccountRestrictions
                                                                                 MainAccountRestrictions.StartTracking()
                                                                                 With MainAccountRestrictions
                                                                                     .Id = itemXpo.Id
                                                                                     .MainAccountId = itemXpo.MainAccountId.Id
                                                                                     .ItemType = itemXpo.ItemType
                                                                                     .CostCenterId = itemXpo.CostCenterId?.Id
                                                                                     .ThirdPartyId = itemXpo.ThirdPartyId?.Id
                                                                                     .RestrictionType = itemXpo.RestrictionType
                                                                                     .AllItems = itemXpo.AllItems
                                                                                     .MarkAsUnchanged()
                                                                                 End With

                                                                                 If itemXpo.CostCenterId IsNot Nothing Then
                                                                                     MainAccountRestrictions.EntityName = itemXpo.CostCenterId.CodeName
                                                                                 ElseIf itemXpo.ThirdPartyId IsNot Nothing Then
                                                                                     MainAccountRestrictions.EntityName = itemXpo.ThirdPartyId.NitName
                                                                                 End If

                                                                                 ListNewMainAccountRestrictions.Add(MainAccountRestrictions)
                                                                             Next
                                                                         End If
                                                                         Return ListNewMainAccountRestrictions
                                                                     End Function)
        INDgcRules.DataSource = ListNewMainAccountRestrictions
    End Function

    ''' <summary>
    ''' metodo para mostrar u ocultar el campo de maneja base
    ''' </summary>
    ''' <param name="Value"></param>
    Private Sub ShowOrHideHandleBase(Value As Boolean)
        If Value Then
            INDlciHandlesBase.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlciHandlesBase.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.HandleBase = Value
        End If
    End Sub

    ''' <summary>
    ''' metodo que se encarga de ocultar (true) o mostrar(false) los controles del grupo de centro de costo
    ''' </summary>
    ''' <param name="value">true- ocultar; false - mostrar</param>
    Private Sub groupHandledCostCenterHideControl(Optional value As Boolean = True)
        INDlciHandlesCostCenter.HideControl(value)
        INDlciReconcileAccount.HideControl(value)
        INDlciShowCGN2.HideControl(value)
    End Sub

    Private Sub AddActionsColumns()
        IndigoGridView1.SetListAcction(viewRules, {eAcciones.Remove}.ToList())
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewRules.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    Private Sub DeleteMainAccountRestrictions()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
			MainAccountRestrictions = DirectCast(viewRules.GetFocusedRow(), MainAccountRestrictions)
			If MainAccountRestrictions IsNot Nothing AndAlso MainAccountRestrictions.Id > 0 Then
				If ListNewMainAccountRestrictionsDelete Is Nothing Then
					ListNewMainAccountRestrictionsDelete = New List(Of MainAccountRestrictions)
				End If
				MainAccountRestrictions.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
				ListNewMainAccountRestrictionsDelete.Add(MainAccountRestrictions)
			End If
			Dim index = ListNewMainAccountRestrictions.IndexOf(MainAccountRestrictions)
			ListNewMainAccountRestrictions.Remove(MainAccountRestrictions)
			MainAccountRestrictions = Nothing

			INDgcRules.DataSource = Nothing
			INDgcRules.DataSource = ListNewMainAccountRestrictions

			'Dim row = DirectCast(viewRules.GetFocusedRow(), MainAccountRestrictions)
			'Dim _indexEditRecord = Me.ListNewMainAccountRestrictions.IndexOf(row)
			'If ListNewMainAccountRestrictions.Item(_indexEditRecord).Id > 0 Then
			'    ListNewMainAccountRestrictions.Item(_indexEditRecord).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
			'Else
			'    ListNewMainAccountRestrictions.RemoveAt(_indexEditRecord)
			'End If
			'INDgcRules.DataSource = ListNewMainAccountRestrictions.Where(Function(x) x.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted).ToList()
			INDgcRules.RefreshDataSource()
			End If
    End Sub

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If _Account IsNot Nothing AndAlso _Account.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MPUC(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim result = Await Model.DeleteAccounting(_Account)
                    If result.StateResult Then
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = result.MessageResult(0)
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar

        If Not ValidateControls() Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If

        AssignValues()

        Dim result As ActionResult(Of MainAccounts)
        Using Model As New MPUC(Me.Tag)
            AsyncLoader(True)
            With Account
                If Account.Id > 0 Then
                    Dim _result As ActionResult(Of MainAccounts)
                    _result = Await Model.UpdatePuc(_Account)
                    AsyncLoader(False)
                    If _result.StateResult = True Then
                        If _Account.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        ElseIf _Account.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or _Account.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                        ModeSearch = True
                        Deshacer()
                    Else
                        For Each res As String In _result.MessageResult
                            If res = "a-0001" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("No Existe una Cuenta Padre para la Cuenta que esta Creando")
                            End If
                            If res = "a-0002" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                            End If
                            If res = "-0003" Then
                                Mensaje(EeventViewerImages.Advertencia) = "No se puede actualizar la naturaleza porque la cuenta ya tiene movimientos"
                            End If
                        Next
                        AsyncLoader(False)
                    End If
                Else
                    result = Await Model.Sp_InsertPUC(_Account)

                    AsyncLoader(False)
                    If result.StateResult = True Then
                        If _Account.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        ElseIf _Account.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or _Account.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                        ModeSearch = False
                        Deshacer()
                    Else
                        For Each res As String In result.MessageResult
                            If res = "a-0001" Then
                                Mensaje(EeventViewerImages.MensajeError) = "No Existe una Cuenta Padre para la Cuenta que esta Creando"
                            End If
                            If res = "a-0002" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                            End If
                        Next
                        AsyncLoader(False)
                    End If
                End If
            End With
        End Using
        indtxtCode.Focus()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Number", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.34)}}.ToList
            .ValorSolicitado = "Number"
            .SearchParameters = {LegalBookId}
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAccountsForSearch
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If Not ModeSearch Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            If indigo.UserViewMode Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
            End If
        End If
        indtxtCode.Focus()
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param> 
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Loads the account class.
    ''' </summary>
    Private Async Function LoadAccountSearchGridLookup() As Task
        Using modelAccountClass As New MAccountClass("601")
            ListAccountClass = Await modelAccountClass.GetAllAccountClass()
            indGlAccountClass.Properties.DataSource = ListAccountClass
        End Using
        Using modelAccountLevel As New MPUC("600")
            ListAccountLevel = Await modelAccountLevel.GetAllAccoutnLevel()
            indGlLevel.Properties.DataSource = ListAccountLevel
        End Using
    End Function

#End Region

#Region "Events"
    Public Sub CleandControlsPopup()
        INDsleType.EditValue = Nothing
        CostCenterRule = Nothing
        ThirdPartieRule = Nothing
        INDcbAllItems.CheckState = 0
        INDsleRestription.EditValue = Nothing
        INDlciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlciThirdPartie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlciAllItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

#End Region



#Region "KeyDown"

    ''' <summary>
    ''' Handles the KeyDown event of the FrmPopupPUC control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub FrmPopupPUC_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the indtxtCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub indtxtCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles indtxtCode.KeyDown
        If Not String.IsNullOrEmpty(indtxtCode.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                Await LoadControls()
            End If
        End If
    End Sub

#End Region

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        indigoValues = Nothing
        record = Nothing
        _Account = Nothing
        ListAccountClass = Nothing
        ListAccountLevel = Nothing
        ModeSearch = Nothing
        listAviable = Nothing
        listRetencionType = Nothing
        listNature = Nothing
        ListTypeRestriction = Nothing
        ListRestriction = Nothing
        PucXpo = Nothing
        _AccountXpo = Nothing
        thirdXpo = Nothing
        _accountMayor = Nothing
        Presenter = Nothing
    End Sub


    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmPopupPUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PPUC(Me)
        Await LoadAccountSearchGridLookup()
        InitializeTuples()
        Await LoadControls()

        If _Account Is Nothing Then
            Deshacer()
            indtxtCode.Focus()
        Else
            indTxtName.Focus()
            BarraBotones.StatusRecordVisible = True
            State = _Account.Status
        End If
        Me.AddActionsColumns()
        LoadStatus()
    End Sub

#End Region

#Region "QueryPopup"
    Private Sub INDsleCostCenterRule_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenterRule.QueryPopUp
        If INDsleCostCenterRule.Properties.DataSource Is Nothing Then
            Using model As New MCostCenter(Tag)
                'Presenter.InitializeCostCenter()
                INDsleCostCenterRule.Properties.DataSource = model.ListCostcenterReport()
            End Using
        End If
    End Sub

    Private Sub INDsleThirdPartie_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThirdPartie.QueryPopUp
        If INDsleThirdPartie.Properties.DataSource Is Nothing Then
            'Presenter.InitializeThirdPartie()
            Using model As New MThirdParty(Tag)
                INDsleThirdPartie.Properties.DataSource = model.GetThirdParty()
            End Using
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleRetencionType control.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRetencionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRetencionType.EditValueChanged
        If INDsleRetencionType.EditValue = 1 Then
            INDlciFreelancerCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlciFreelancerCategory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDsleFreelancerCategory.EditValue = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the indGlLevel control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub indGlLevel_EditValueChanged(sender As Object, e As EventArgs) Handles indGlLevel.EditValueChanged
        If indGlLevel.EditValue = 5 Then
            groupauxiliary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            groupauxiliary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleCloseThirdParty control.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCloseThirdParty_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCloseThirdParty.EditValueChanged
        If INDsleCloseThirdParty.EditValue = True Then
            lyItemNit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            If indGlNit.Properties.DataSource Is Nothing Then
                Using model As New MBusqueda
                    thirdXpo = model.ConsultarEntidades(eDataSource.ThirdParty)
                    indGlNit.Properties.DataSource = thirdXpo
                End Using
            End If
        Else
            lyItemNit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Async Sub indGlAccountClass_EditValueChanged(sender As Object, e As EventArgs) Handles indGlAccountClass.EditValueChanged
        If indGlAccountClass.EditValue <> Nothing Then
            Dim claseContable As Domain.Entities.MainAccountClasses
            Using modelAccoutnClasses As New MAccountClass("601")
                claseContable = Await modelAccoutnClasses.GetAccountClassById(indGlAccountClass.EditValue, False)

                'Se inicializa la tupla de Disponibilidad-Tipo dependiendo de el tipo de la clase
                SetListAvailabilityOrType = CInt(claseContable.Type.Value)
            End Using
        End If
    End Sub


    Private Sub INDsleHandlesCostCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHandlesCostCenter.EditValueChanged
        If _Account.AllowsMovement AndAlso HandlesCostCenter Then
            INDlciHandlesCostCenterRestriction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlciHandlesCostCenterRestriction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            HandlesCostCenterRestriction = False
        End If
    End Sub


    Private Sub INDsleHandlesThirdParty_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHandlesThirdParty.EditValueChanged
        If _Account.AllowsMovement AndAlso HandlesThirdParty Then
            INDlciHandlesThirdPartyRestriction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlciHandlesThirdPartyRestriction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            HandlesThirdPartyRestriction = False
        End If
    End Sub

    Private Sub INDsleHandlesThirdPartyRestriction_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHandlesThirdPartyRestriction.EditValueChanged, INDsleHandlesCostCenterRestriction.EditValueChanged

        If HandlesCostCenterRestriction And HandlesThirdPartyRestriction Then
            INDlygRules.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciAllItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlciThirdPartie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDsleType.Properties.DataSource = ListTypeRestriction.ToList
            INDsleType.EditValue = Nothing
        ElseIf HandlesCostCenterRestriction Then
            INDlygRules.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciAllItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDsleType.Properties.DataSource = ListTypeRestrictionCostCenter.ToList
            INDsleType.EditValue = 1
            ThirdPartieRule = Nothing
        ElseIf HandlesThirdPartyRestriction Then
            INDlygRules.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciAllItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDsleType.Properties.DataSource = ListTypeRestrictionThirdParty.ToList
            INDsleType.EditValue = 2
            CostCenterRule = Nothing
        Else
            INDlygRules.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDsleType.Properties.DataSource = ListTypeRestriction.ToList
            CleandControlsPopup()
        End If

    End Sub

    Private Sub INDsleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleType.EditValueChanged
        If INDsleType.EditValue = 1 Then
            INDlciAllItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciThirdPartie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ThirdPartieRule = Nothing
            INDcbAllItems.Text = "Todos los Centros de Costo"
        ElseIf INDsleType.EditValue = 2 Then
            INDlciAllItems.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlciThirdPartie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            CostCenterRule = Nothing
            INDcbAllItems.Text = "Todos los Terceros"
        End If
    End Sub

    Private Sub INDcbAllItems_CheckedChanged(sender As Object, e As EventArgs) Handles INDcbAllItems.CheckedChanged
        If INDcbAllItems.Checked Then
            INDlciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlciThirdPartie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            If INDsleType.EditValue = 1 Then
                INDlciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlciThirdPartie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                ThirdPartieRule = Nothing
            Else
                INDlciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlciThirdPartie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                CostCenterRule = Nothing
            End If
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Handles the ButtonClick event of the indGlAccountClass control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub indGlAccountClass_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles indGlAccountClass.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmAccountClass
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                LoadAccountSearchGridLookup()
            End Using
        End If
    End Sub

    Private Sub indtxtCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles indtxtCode.ButtonClick
        OpenSearch()
    End Sub

#End Region

#Region "MenuContextual"
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
		DeleteMainAccountRestrictions()
	End Sub
#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupPUC_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        indtxtCode.Focus()
    End Sub

#End Region

#Region "Click"
    Private Sub INDbtnAddRule_Click(sender As Object, e As EventArgs) Handles INDbtnAddRule.Click
        'validamos los campos
        If INDsleType.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "El campo Condición es obligatorio"
            Exit Sub
        End If
        If INDlciCostCenter.Visibility.Always Or INDlciThirdPartie.Visibility.Always Then
            Select Case INDsleType.EditValue
                Case 1
                    If Not CostCenterRule Then
                        Mensaje(EeventViewerImages.Advertencia) = "El campo Tipo de Centro de Costo es obligatorio"
                        Exit Sub
                    End If
                Case 2
                    If Not ThirdPartieRule Then
                        Mensaje(EeventViewerImages.Advertencia) = "El campo Grupo de Terceros es obligatorio"
                        Exit Sub
                    End If
            End Select
        End If
        If INDsleRestription.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "El campo tipo de restricción es obligatorio"
            Exit Sub
        End If

		If ListNewMainAccountRestrictions Is Nothing Then
			ListNewMainAccountRestrictions = New List(Of MainAccountRestrictions)
		End If
		Dim newCondition As MainAccountRestrictions = New MainAccountRestrictions()
        Dim entityName = ""
        Select Case INDsleType.EditValue
            Case 1
                entityName = INDsleCostCenterRule.Text
            Case 2
                entityName = INDsleThirdPartie.Text
        End Select
        With newCondition
            .ItemType = INDsleType.EditValue
            .CostCenterId = IIf(CostCenterRule = 0, Nothing, CostCenterRule)
            .ThirdPartyId = IIf(ThirdPartieRule = 0, Nothing, ThirdPartieRule)
            .RestrictionType = INDsleRestription.EditValue
            .AllItems = INDcbAllItems.Checked
            .EntityName = entityName
        End With

        If ListNewMainAccountRestrictions.Any(Function(x) x.ItemType = newCondition.ItemType AndAlso
                                                          x.AllItems = newCondition.AllItems AndAlso
                                                          IIf(newCondition.CostCenterId Is Nothing And newCondition.ThirdPartyId Is Nothing, True,
                                                              (IIf(newCondition.CostCenterId Is Nothing, False, newCondition.CostCenterId = x?.CostCenterId) OrElse
                                                                IIf(newCondition.ThirdPartyId Is Nothing, False, newCondition.ThirdPartyId = x?.ThirdPartyId))
                                                            )
                                                         ) Then
            Mensaje(EeventViewerImages.Advertencia) = "Ya agregó una Regla con las mismas condiciones"
            Exit Sub
        Else
            ListNewMainAccountRestrictions.Add(newCondition)
        End If
        INDgcRules.DataSource = ListNewMainAccountRestrictions
        INDgcRules.RefreshDataSource()
        CleandControlsPopup()
    End Sub

#End Region

#Region "CustomDrawCell"
    Private Sub viewRules_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles viewRules.CustomDrawCell
        Dim item = CType(Me.viewRules.GetRow(e.RowHandle), MainAccountRestrictions)
        If item?.AllItems = True AndAlso e.Column.Name.Equals(ColName.Name) Then
            e.DisplayText = "Todos"
        End If
    End Sub
#End Region

#Region "BarraBotones"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
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
        ModeSearch = False
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
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._Account IsNot Nothing AndAlso Me._Account.Id > 0 Then

            If Not (MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelTitle"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Exit Sub
            End If
        End If
        If record IsNot Nothing AndAlso record.Id > 0 Then
            If record.CodUser = indigo.UserIndigo Then
                DeleteBlockedRecord()
            End If
        End If
        indtxtCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            Me.IdEntity =  String.Empty
            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmPopupPUC control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmPopupPUC_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

#End Region

End Class