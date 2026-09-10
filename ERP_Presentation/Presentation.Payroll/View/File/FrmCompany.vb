'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 25-04-2013
'
' Last Modified By : Kevin Garay Rodriguez
' Last Modified On : 05-06-2013
' Description      : Refactoring
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Payroll.Entities
Imports Presentation.Payroll.MVP
Imports Domain.Base.Entities
Imports Presentation.Common
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Clase que tiene el comportamiento de la vista en el formulario Company 
''' </summary>
''' <remarks></remarks>
Public Class FrmCompany
    Implements ICompany

#Region "Variables globales, load, y propiedades"
    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' Varaible que contiene la entidad de las empresas
    ''' </summary> 
    Dim Company As Domain.Payroll.Entities.Company
    ''' <summary>
    ''' Variable para la entidad de ciudades
    ''' </summary>
    ''' <remarks></remarks>
    Dim CityEntity As Domain.Entities.City
    ''' <summary>
    ''' Variable para poder acceder al Modelo de las empresas
    ''' </summary>
    Dim ModelCompany As New MCompany(MyBase.Tag)
    ''' <summary>
    ''' Variable que se utiliza para acceder al modelo de los departamentos
    ''' </summary>
    Dim ModelDepartment As Presentation.Common.MVP.MDepartaments

    Dim ModelFund As Presentation.Payroll.MVP.MFunds
    ''' <summary>
    ''' Variable para utilizar el modelo de las ciudades
    ''' </summary>
    ''' <remarks></remarks>
    Dim ModelCity As Presentation.Common.MVP.MCity
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PCompany
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable que maneja el listado de los telefonos agregados al control
    ''' </summary>
    Dim ListadoEliminadosTelefono As New List(Of Domain.Payroll.Entities.Phone)
    ''' <summary>
    '''  Variable que maneja el listado de los Direccions agregados al control
    ''' </summary>
    Dim ListadoEliminadosDireccion As New List(Of Domain.Payroll.Entities.Address)
    ''' <summary>
    '''  Variable que maneja el listado de los Email agregados al control
    ''' </summary>
    Dim ListadoEliminadosEmail As New List(Of Domain.Payroll.Entities.Email)
    ''' <summary>
    ''' variable que maneja el objeto tercero
    ''' </summary>
    ''' <remarks></remarks>
    Dim TmpThirdParty As Domain.Payroll.Entities.ThirdParty


    ''' <summary>
    ''' Propiedad que contiene el codigo de la empresa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CompanyNit As String Implements ICompany.CompanyNit
        Get
            Return INDBteNitCompany.Text
        End Get
        Set(value As String)
            INDBteNitCompany.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el nombre de la empresa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CompanyName1 As String Implements ICompany.CompanyName
        Get
            Return INDTxtNameCompany.Text
        End Get
        Set(value As String)
            INDTxtNameCompany.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el representante legal de la empresa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LegalRepresentative As String Implements ICompany.LegalRepresentative
        Get
            Return INDTxtLegalRepresentative.Text
        End Get
        Set(value As String)
            INDTxtLegalRepresentative.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el departamento perteneciente de esta empresa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Department As Integer Implements ICompany.Department
        Get
            Return INDGleDepartment.EditValue
        End Get
        Set(value As Integer)
            INDGleDepartment.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene la ciudad perteneciente de esta empresa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property City As Integer Implements ICompany.City
        Get
            Return INDGleCity.EditValue
        End Get
        Set(value As Integer)
            INDGleCity.EditValue = value
        End Set
    End Property

    Public Property ARLFundId As Integer? Implements ICompany.ARLFundId
        Get
            Return INDSlARLCompany.EditValue
        End Get
        Set(value As Integer?)
            INDSlARLCompany.EditValue = value
        End Set
    End Property

    Public Property CompanyAgreementType As Boolean
        Get
            Return INDchkAgreementsType.EditValue
        End Get
        Set(value As Boolean)
            INDchkAgreementsType.EditValue = value
        End Set
    End Property

    Public Property CompanyPayrollType As Boolean
        Get
            Return Me.INDchkPayrolType.EditValue
        End Get
        Set(value As Boolean)
            Me.INDchkPayrolType.EditValue = value
        End Set
    End Property


    Public Property CompanyAttachmentType As Boolean?
        Get
            Return Me.INDchkAttachmentsType.EditValue
        End Get
        Set(value As Boolean?)
            Me.INDchkAttachmentsType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CompanyState As Boolean Implements ICompany.CompanyState
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            Me.BarraBotones.StatusRecord = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que carga los departamentos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property DepartmentDataSource As List(Of Domain.Entities.Department) Implements ICompany.DepartmentDataSource
        Set(value As List(Of Domain.Entities.Department))
            INDGleDepartment.Properties.DataSource = value
        End Set
    End Property

    Public Property JudgmentCode As String
        Get
            Return INDTxtJudgementCode.EditValue
        End Get
        Set(value As String)
            INDTxtJudgementCode.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que carga las ciudades
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property CityDataSource As List(Of Domain.Entities.City) Implements ICompany.CityDataSource
        Set(value As List(Of Domain.Entities.City))
            INDGleCity.Properties.DataSource = value
        End Set
    End Property

    Public WriteOnly Property FundDataSource As List(Of Domain.Payroll.Entities.Fund) Implements ICompany.FundDataSource
        Set(value As List(Of Domain.Payroll.Entities.Fund))
            Dim TmpListFund As New List(Of Domain.Payroll.Entities.Fund)
            TmpListFund = value.Where(Function(x) x.Risk = True).ToList()
            INDSlARLCompany.Properties.DataSource = TmpListFund
        End Set
    End Property
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICompany.ActionsOnControls
        Set(value As Boolean)
            INDBteNitCompany.Enabled = Not value
            INDTxtNameCompany.Enabled = value
            INDTxtLegalRepresentative.Enabled = value
            INDGleCity.Enabled = value
            INDGleDepartment.Enabled = value
            INDSlThirdParty.Enabled = value
            INDchkAgreementsType.Enabled = value
            INDchkPayrolType.Enabled = value
            INDchkAttachmentsType.Enabled = value
            INDpceControlData.Enabled = value
            INDSlARLCompany.Enabled = value
            INDTxtJudgementCode.Enabled = value
            Me.BarraBotones.StatusRecordVisible = value
            If value = True Then
                INDSlThirdParty.Focus()
            Else
                INDBteNitCompany.Focus()
            End If
        End Set
    End Property

    Public Property ListThirdParty As XPInstantFeedbackSource Implements ICompany.ListThirdParty
        Get
            Return INDSlThirdParty.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlThirdParty.Properties.DataSource = value
        End Set
    End Property

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        record = Nothing
        ExistDefinitionFront = Nothing
        Company = Nothing
        CityEntity = Nothing
        ModelCompany = Nothing
        ModelDepartment = Nothing
        ModelFund = Nothing
        ModelCity = Nothing
        Presenter = Nothing
        PathFunctionalDefinitions = Nothing
        ListadoEliminadosTelefono = Nothing
        ListadoEliminadosDireccion = Nothing
        ListadoEliminadosEmail = Nothing
        TmpThirdParty = Nothing
    End Sub

    ''' <summary>
    ''' Metodo que carga el load del formulario 
    ''' </summary>
    Private Async Sub FrmCompany_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc

        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollCompany.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter = New PCompany(Me)
        ModelCompany = New MCompany(MyBase.Tag)
        ModelDepartment = New Presentation.Common.MVP.MDepartaments(Presentation.Common.MVP.MDepartaments.TAG)
        DepartmentDataSource = Await ModelDepartment.ListAllDepartmentAsync()
        ModelFund = New Presentation.Payroll.MVP.MFunds("518")
        FundDataSource = Await ModelFund.ListAllFundsAsync()
        LoadStatus()
        Me.BarraBotones.StatusRecordVisible = True
        PopupSize()
        Deshacer()
    End Sub

    Dim searchMode As Boolean = False
#End Region

#Region "Metodos Funciones Propiedades"
    ''' <summary>
    ''' Metodo que establece el tamaño de los popup de los grid lookup edit
    ''' </summary>
    Private Sub PopupSize()
        Dim NewSize As Drawing.Size = New Drawing.Size(500, 200)
        INDGleCity.Properties.PopupFormSize = NewSize
        INDGleDepartment.Properties.PopupFormSize = NewSize
    End Sub
    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Company
            .Nit = CompanyNit
            .Name = CompanyName1
            .LegalRepresentative = LegalRepresentative
            .CityId = City
            .AgreementType = INDchkAgreementsType.EditValue
            .PayrollType = INDchkPayrolType.EditValue
            .ForeclousureType = INDchkAttachmentsType.EditValue
            .State = CompanyState
            If INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ThirdPartyId = INDSlThirdParty.EditValue
                .ThirdParty = TmpThirdParty

                '*********Datos Contacto
                If Object.Equals(.ThirdParty.Person.Address, Nothing) = False Then
                    For i As Integer = 0 To ListadoEliminadosDireccion.Count - 1
                        ListadoEliminadosDireccion.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                        .ThirdParty.Person.Address.Add(ListadoEliminadosDireccion.Item(i))
                    Next
                End If
                If Object.Equals(.ThirdParty.Person.Phone, Nothing) = False Then
                    For i As Integer = 0 To ListadoEliminadosTelefono.Count - 1
                        ListadoEliminadosTelefono.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                        .ThirdParty.Person.Phone.Add(ListadoEliminadosTelefono.Item(i))
                    Next
                End If
                If Object.Equals(.ThirdParty.Person.Email, Nothing) = False Then
                    For i As Integer = 0 To ListadoEliminadosEmail.Count - 1
                        ListadoEliminadosEmail.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                        .ThirdParty.Person.Email.Add(ListadoEliminadosEmail.Item(i))
                    Next
                End If
            Else
                .ThirdParty = Nothing
            End If

            .ARLFundId = ARLFundId

            If INDLciJudgmentAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .CodeJudgmentAccount = JudgmentCode
                .ThirdPartyId = Nothing
            End If

        End With
    End Sub
    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Me.BarraBotones.StatusRecordVisible = True
        AsyncLoader(True)
        Using ModelCompany As New MCompany(MyBase.Tag)
            Company = New Domain.Payroll.Entities.Company
            Company = ModelCompany.GetCompany(INDBteNitCompany.Text)
        End Using
        If Not Company Is Nothing Then
            If Company.Id > 0 Then
                Dim result = Await ModelCompany.GetBlockRecord(Me.Tag, Company.Id)
                With Company
                    Using ModelCity As New Presentation.Common.MVP.MCity(Presentation.Common.MVP.MCity.TAG)
                        CityEntity = ModelCity.GetCityById(.CityId)
                    End Using
                    Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Company.CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Company.CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Company.ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), Company.ModificationDate)
                    CompanyName1 = .Name
                    LegalRepresentative = .LegalRepresentative
                    City = .CityId
                    Department = CityEntity.DepartamentId
                    CompanyAgreementType = .AgreementType
                    CompanyPayrollType = .PayrollType
                    CompanyAttachmentType = .ForeclousureType
                    CompanyState = .State
                    JudgmentCode = .CodeJudgmentAccount
                    If INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        INDSlThirdParty.EditValue = .ThirdPartyId
                        INDSlThirdParty.Properties.NullText = .ThirdParty.Nit + " - " + .ThirdParty.Name

                        'Control Datos de Contacto
                        CtrContacts.EstablecerDataSourceDireccion = .ThirdParty.Person.Address.ToList
                        CtrContacts.EstablecerDataSourceTelefono = .ThirdParty.Person.Phone.ToList
                        CtrContacts.EstablecerDataSourceEmail = .ThirdParty.Person.Email.ToList
                        TmpThirdParty = .ThirdParty
                    End If
                    ARLFundId = .ARLFundId

                End With
                Me.GetDocumentIndexed(Me.Tag & "_" & Me.Company.Nit)
                If result.Id = 0 Then
                    Me.BarraBotones.SetDocuments(Company.Id)
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Company.Id}
                    Dim operation = Await ModelCompany.SaveBlockRecord(record)
                    record = operation.ObjectEmbbeded
                Else
                    record = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If

            Else
                Company.ThirdParty = New Domain.Payroll.Entities.ThirdParty
                Me.BarraBotones.StatusRecordVisible = True
                Me.BarraBotones.StatusRecord = Me.BarraBotones.States(0).StatusValue
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            End If
        Else
            Company = New Domain.Payroll.Entities.Company
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        End If
        AsyncLoader(False)
        ActionsOnControls = True
    End Function
    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        ActionsOnControls = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        INDBteNitCompany.Text = String.Empty
        INDTxtLegalRepresentative.Text = String.Empty
        INDTxtNameCompany.Text = String.Empty
        INDGleCity.Properties.DataSource = Nothing
        INDGleDepartment.EditValue = Nothing
        INDSlThirdParty.EditValue = Nothing
        INDSlThirdParty.Properties.NullText = String.Empty
        INDchkAgreementsType.EditValue = False
        INDchkPayrolType.EditValue = False
        INDchkAttachmentsType.EditValue = False
        INDSlARLCompany.EditValue = Nothing
        INDTxtJudgementCode.EditValue = Nothing
        INDLciARLCompany.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.BarraBotones.StatusRecordVisible = False
        Company = Nothing
        TmpThirdParty = Nothing
        CtrContacts.LimpiarControles()
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
    End Sub
    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDBteNitCompany.Text = String.Empty Then
                INDBteNitCompany.Focus()
                ValidateControls = False
                Exit Function
            End If

            If INDSlThirdParty.EditValue = Nothing Then
                INDSlThirdParty.Focus()
                ValidateControls = False
                Exit Function
            End If
        End If

        If INDTxtLegalRepresentative.Text = String.Empty Then
            INDTxtLegalRepresentative.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDTxtNameCompany.Text = String.Empty Then
            INDTxtNameCompany.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDGleCity.EditValue = Nothing Then
            INDGleCity.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDGleDepartment.EditValue = Nothing Then
            INDGleCity.Focus()
            ValidateControls = False
            Exit Function
        End If

    End Function

    ''' <summary>
    ''' Metodo para cargar tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadThird(ByVal Nit As String) As Task
        If Company.ThirdParty.Id <> INDSlThirdParty.EditValue Then
            AsyncLoader(True)
            TmpThirdParty = Await ModelCompany.GetThirdPartyAsync(Nit)
            AsyncLoader(False)
            If TmpThirdParty IsNot Nothing AndAlso TmpThirdParty.Id > 0 Then
                CtrContacts.EstablecerDataSourceDireccion = TmpThirdParty.Person.Address.ToList
                CtrContacts.EstablecerDataSourceTelefono = TmpThirdParty.Person.Phone.ToList
                CtrContacts.EstablecerDataSourceEmail = TmpThirdParty.Person.Email.ToList
            End If
        End If
        INDTxtNameCompany.Focus()
    End Function
    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDBteCodeCompany_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteNitCompany.ButtonClick
        AbrirBusqueda()
    End Sub


    ''' <summary>
    ''' Evento para consultar la empresa en el evento keydown del codigo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDBteCodeCompany_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteNitCompany.KeyDown
        If Not String.IsNullOrEmpty(INDBteNitCompany.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                LoadControls()
                If INDBteNitCompany.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
                End If
                INDBteNitCompany.Enabled = False
            End If
        End If
    End Sub


    ''' <summary>
    ''' Metodo que carga ciudades dependiendo el departamento
    ''' </summary>
    Private Sub INDGleDepartment_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleDepartment.EditValueChanged
        If Department <> Nothing And Department > 0 Then
            ModelCity = New Presentation.Common.MVP.MCity(Presentation.Common.MVP.MCity.TAG)
            CityDataSource = ModelCity.ListAllCityDepartment(Department)
        End If
    End Sub
    ''' <summary>
    ''' Metodo para mostrar popup de los gridlookup edit cuando se escriba en ellos
    ''' </summary>
    Private Sub INGGle_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)
        Dim ControlName As String = CType(sender, DevExpress.XtraEditors.GridLookUpEdit).Name
        Select Case ControlName
            Case "INDGleCity"
                INDGleCity.ShowPopup()
            Case "INDGleDepartment"
                INDGleDepartment.ShowPopup()
        End Select
    End Sub
    ''' <summary>
    ''' Evento para abrir formulario de departamentos en popup
    ''' </summary>
    Private Sub INDGleDepartment_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDGleDepartment.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmDepartaments
                Formulario.ViewModeEditHold = True
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento para abrir formulario de ciudades en popup
    ''' </summary>
    Private Sub INDGleCity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDGleCity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCity
                Formulario.ViewModeEditHold = True
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Await ModelCompany.DeleteBlockRecord(record)
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.Company IsNot Nothing AndAlso Me.Company.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Me.INDBteNitCompany.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteNitCompany.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Evento para eliminar las direcciones agregadas al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarDireccion() Handles CtrContacts.EliminarDireccion
        If TmpThirdParty.Person.Address.Count > 0 Then
            ListadoEliminadosDireccion.Add(TmpThirdParty.Person.Address.Item(CtrContacts.ItemSelecionadoDireccion))
            TmpThirdParty.Person.Address.RemoveAt(CtrContacts.ItemSelecionadoDireccion)
            CtrContacts.EstablecerDataSourceDireccion = TmpThirdParty.Person.Address
        End If
    End Sub
    ''' <summary>
    ''' Evento para eliminar los telefono agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarTelefono() Handles CtrContacts.EliminarTelefono
        If TmpThirdParty.Person.Phone.Count > 0 Then
            ListadoEliminadosTelefono.Add(TmpThirdParty.Person.Phone.Item(CtrContacts.ItemSelecionadoTelefono))
            TmpThirdParty.Person.Phone.RemoveAt(CtrContacts.ItemSelecionadoTelefono)
            CtrContacts.EstablecerDataSourceTelefono = TmpThirdParty.Person.Phone
        End If
    End Sub
    ''' <summary>
    ''' Evento para eliminar los Email agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarEmail() Handles CtrContacts.EliminarEmail
        If TmpThirdParty.Person.Email.Count > 0 Then
            ListadoEliminadosEmail.Add(TmpThirdParty.Person.Email.Item(CtrContacts.ItemSelecionadoEmail))
            TmpThirdParty.Person.Email.RemoveAt(CtrContacts.ItemSelecionadoEmail)
            CtrContacts.EstablecerDataSourceEmail = TmpThirdParty.Person.Email
        End If
    End Sub
    ''' <summary>
    ''' Evento para cambia el tipo Email
    ''' </summary>
    Private Sub CtrContactos1_ChangeEmailType(Type As Byte) Handles CtrContacts.ChangeEmailType
        If TmpThirdParty.Person.Email.Count > 0 Then
            Dim email = TmpThirdParty.Person.Email.ElementAt(CtrContacts.ItemSelecionadoEmail)
            email.Type = Type
            If email.Id > 0 Then
                email.ChangeTracker.State = ObjectState.Modified
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento para agregar al listado de direcciones  del control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevaDireccion() Handles CtrContacts.InsertoNuevaDireccion
        If TmpThirdParty.Person.Address Is Nothing Then
            TmpThirdParty.Person.Address = New Domain.Base.Entities.TrackableCollection(Of Address)
        End If
        If TmpThirdParty.Person.Address.Where(Function(e) e.Addresss = CtrContacts.Direccion).Count = 0 Then
            TmpThirdParty.Person.Address.Add(New Address With 
                                                {
                                                    .DepartmentId = CtrContacts.DepartmentId, 
                                                    .DepartmentName = CtrContacts.DepartmentName, 
                                                    .CityId = CtrContacts.CityId, 
                                                    .CityName = CtrContacts.CityName,
                                                    .Addresss = CtrContacts.Direccion, 
                                                    .Synchronized = "1", 
                                                    .State = True
                                                }
                                             )
        End If
        CtrContacts.EstablecerDataSourceDireccion = Nothing
        CtrContacts.EstablecerDataSourceDireccion = TmpThirdParty.Person.Address
    End Sub
    ''' <summary>
    ''' Evento para agregar al listado de correos  del control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevoEmail() Handles CtrContacts.InsertoNuevoEmail
        If TmpThirdParty.Person.Email Is Nothing Then
            TmpThirdParty.Person.Email = New Domain.Base.Entities.TrackableCollection(Of Email)
        End If
        If TmpThirdParty.Person.Email.Where(Function(e) e.Email1 = CtrContacts.Email).Count = 0 Then
            TmpThirdParty.Person.Email.Add(New Email With {.Email1 = CtrContacts.Email, .Synchronized = "1", .Type = CtrContacts.EmailType, .State = True})
        End If
        CtrContacts.EstablecerDataSourceEmail = Nothing
        CtrContacts.EstablecerDataSourceEmail = TmpThirdParty.Person.Email
    End Sub
    ''' <summary>
    ''' Evento para agregar al listado de Telefonos  del control de datos de contacto.
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevoTelefono() Handles CtrContacts.InsertoNuevoTelefono
        If TmpThirdParty.Person.Phone Is Nothing Then
            TmpThirdParty.Person.Phone = New Domain.Base.Entities.TrackableCollection(Of Phone)
        End If
        If TmpThirdParty.Person.Phone.Where(Function(e) e.Phone1 = CtrContacts.Telefono).Count = 0 Then
            TmpThirdParty.Person.Phone.Add(New Phone With {.Phone1 = CtrContacts.Telefono, .IdPhoneType = CShort(CtrContacts.TipoTelefono), .Synchronized = "1", .State = True})
        End If
        CtrContacts.EstablecerDataSourceTelefono = Nothing
        CtrContacts.EstablecerDataSourceTelefono = TmpThirdParty.Person.Phone
    End Sub

#End Region

#Region "CRUD Operations"
    ''' <summary>
    ''' METODO: Item Nuevo del control de Empresas.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        CleanControls()
    End Sub
    ''' <summary>
    ''' METODO: Item buscar del control de Empresas.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        AbrirBusqueda()
    End Sub
    ''' <summary>
    ''' METODO: Item Eliminar del control de Empresas.
    ''' </summary>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        Dim result As ActionMessageResult(Of Company)
        If Company IsNot Nothing Then
            If Company.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    AsyncLoader(True)
                    Using Model As New MCompany(MCompany.TAG)
                        result = Await Model.DeleteCompanyAsync(Company)
                        If result.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            AsyncLoader(False)
                            searchMode = False
                            Deshacer()
                        Else
                            AsyncLoader(False)
                            If result.MessageResult.ElementAt(0).CodeMessage = "c-0000" Then
                                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                            End If
                        End If
                    End Using
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesSeleccione, Comunes)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesSeleccione, Comunes)
        End If
    End Sub
    ''' <summary>
    ''' METODO: Item Guardar del control de Empresas.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AsyncLoader(True)
        AssigningValues()
        Using Model As New MCompany(MCompany.TAG)
            If Await Model.SaveCompanyAsync(Company) = True Then
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                If Company.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                ElseIf Company.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or Company.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                End If
                AsyncLoader(False)
                searchMode = False
                Deshacer()
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                AsyncLoader(False)
            End If
        End Using

    End Sub
    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch
        searchMode = True
        Deshacer()
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.CompanyPayroll
            BarraBotones.PrepareToolbar(eAction.OnlyNew)
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
        INDBteNitCompany.Text = ReturnValue
        If INDBteNitCompany.Text <> String.Empty Then
            Await LoadControls()
            If INDBteNitCompany.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteNitCompany.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de Empresas.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        If searchMode = False Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Else
            If indigo.UserViewMode = True Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(ByVal value As String)

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
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub
#End Region

#Region "bar buttons and events"
    ''' <summary>
    '''Evento load de la barra de Empresas.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
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
        searchMode = False
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
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

    '' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmCompanyPayrollMetaData, Eform.InfoMetaData), Me.Company.Nit, Me.Company.Name, Me.Company.LegalRepresentative),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.Company.Nit & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmCompanyPayrollMetaDataTitle, Eform.InfoMetaData), Me.Company.Nit),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmCompanyPayrollMetaData, Eform.InfoMetaData), Me.Company.Nit, Me.Company.Name, Me.Company.LegalRepresentative)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmCompanyPayrollMetaDataTitle, Eform.InfoMetaData), Me.Company.Nit)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = obtenerRecurso(ComunesActivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = obtenerRecurso(ComunesInactivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Elimina el reg bloqueado cuando se cierra el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCompany_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "Customize"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyCompany.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LoadhronousDefinitions.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(PathFunctionalDefinitions) = True Then
            ExistDefinitionFront = True
        End If
    End Sub
    ''' <summary>
    ''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadhronousDefinitions.RunWorkerCompleted
        If ExistDefinitionFront = True Then
            INDlyCompany.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCompany.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MCompany(MCompany.TAG)
                Dim dsFields As DataSet = model.GetNullFields()
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyCompany.Items.Count - 1
                        INDlyCompany.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyCompany.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyCompany.Items.Item(j).Tag.ToString.Trim Then
                                    INDlyCompany.Items.Item(j).AllowHide = True
                                End If
                            End If
                        Next
                    Next
                End If
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCompany.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyCompany.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyCompany.SaveLayoutToXml(PathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDlyCompany.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub


#Region "Query Popup"
    Private Sub INDSlThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlThirdParty.QueryPopUp
        If INDSlThirdParty.Properties.DataSource Is Nothing Then
            Presenter.LoadListThirdParty()
        End If
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDSlThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(532, Nothing, True)
            Presenter.LoadListThirdParty()
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    Private Async Sub INDSlThirdParty_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlThirdParty.EditValueChanged
        If INDSlThirdParty.EditValue IsNot Nothing Then

            If Company.ThirdParty IsNot Nothing Then
                If Company.ThirdParty.Id <> INDSlThirdParty.EditValue Then

                    Dim objetoSeleccion As Infrastructure.Data.Xpo.CommonRepository.CommonThirdPartyXpo = DirectCast(DirectCast(GridView1.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonThirdPartyXpo)

                    AsyncLoader(True)
                    TmpThirdParty = Await ModelCompany.GetThirdPartyAsync(objetoSeleccion.Nit)
                    AsyncLoader(False)
                    If TmpThirdParty IsNot Nothing AndAlso TmpThirdParty.Id > 0 Then
                        CtrContacts.EstablecerDataSourceDireccion = TmpThirdParty.Person.Address.ToList
                        CtrContacts.EstablecerDataSourceTelefono = TmpThirdParty.Person.Phone.ToList
                        CtrContacts.EstablecerDataSourceEmail = TmpThirdParty.Person.Email.ToList
                    End If
                End If
            Else
                Dim objetoSeleccion As Infrastructure.Data.Xpo.CommonRepository.CommonThirdPartyXpo = DirectCast(DirectCast(GridView1.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonThirdPartyXpo)

                AsyncLoader(True)
                TmpThirdParty = Await ModelCompany.GetThirdPartyAsync(objetoSeleccion.Nit)
                AsyncLoader(False)
                If TmpThirdParty IsNot Nothing AndAlso TmpThirdParty.Id > 0 Then
                    CtrContacts.EstablecerDataSourceDireccion = TmpThirdParty.Person.Address.ToList
                    CtrContacts.EstablecerDataSourceTelefono = TmpThirdParty.Person.Phone.ToList
                    CtrContacts.EstablecerDataSourceEmail = TmpThirdParty.Person.Email.ToList
                End If
            End If
        End If
    End Sub

    Private Sub INDchkPayrolType_EditValueChanged(sender As Object, e As EventArgs) Handles INDchkPayrolType.EditValueChanged
        If CompanyPayrollType = True Then
            INDLciARLCompany.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciARLCompany.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub INDchkAttachmentsType_EditValueChanged(sender As Object, e As EventArgs) Handles INDchkAttachmentsType.EditValueChanged
        If CompanyAttachmentType = True Then
            INDLciJudgmentAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDLciJudgmentAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
#End Region


#End Region

End Class