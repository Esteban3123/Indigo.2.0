#Region "Imports"
Imports System.Drawing
Imports System.Windows.Forms
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common.MVP
Imports Presentation.Contract.MVP
Imports Presentation.Controls

#End Region

''' <summary>
''' Clase que contiene la vista del funcional pacientes
''' </summary>
''' <remarks></remarks>
Public Class FrmPatient
    Implements Ipatient

#Region "Global Variables"
    ''' <summary>
    ''' Variable que contiene el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PPatient

    ''' <summary>
    ''' Variable que contiene el modelo
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As MPatient

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Entidad de bloqueo de registros
    ''' </summary>
    ''' <remarks></remarks>
    Private record As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    Dim _isEditingTopYear As Boolean

    ''' <summary>
    ''' Entidad Paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim Patient As INPACIENT

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Crystal"

    ''' <summary>
    ''' Variable para almacenar una identificación temporal
    ''' </summary>
    ''' <remarks></remarks>
    Dim TempIdentification As String

    ''' <summary>
    ''' Bandera para saber si vamos a guardar un menor sin identificación
    ''' </summary>
    ''' <remarks></remarks>
    Dim AnonimusRegBaby As Boolean = False

    ''' <summary>
    ''' Bandera para saber si vamos a guardar un adulto sin identificación
    ''' </summary>
    ''' <remarks></remarks>
    Dim AnonimusAdult As Boolean = False

    ''' <summary>
    ''' Para almacenar el codigo en caso de que no sea un recien nacido
    ''' </summary>
    ''' <remarks></remarks>
    Dim CodAnonimusAdult As String = ""

    ''' <summary>
    ''' Variable para almacenar el nivel
    ''' </summary>
    ''' <remarks></remarks>
    Dim ADNIVELES As ADNIVELES
    ''' <summary>
    ''' Para saber si es o no una aseguradora 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Isinsurance As Boolean
    ''' <summary>
    ''' Id Administradora de Salud
    ''' </summary>
    ''' <remarks></remarks>
    Dim _HealthAdministratorId As Integer? = Nothing

    ''' <summary>
    ''' Gets or sets the list top anual.
    ''' </summary>
    ''' <value>
    ''' The list top anual.
    ''' </value>
    Public Property ListTopAnual As List(Of INPACIENTTOPANU)
        Get
            Return CType(INDGcTopAnual.DataSource, List(Of INPACIENTTOPANU))
        End Get
        Set(value As List(Of INPACIENTTOPANU))
            INDGcTopAnual.DataSource = value
        End Set
    End Property

    Public Property TopYearCuotaModeradora As Decimal
        Get
            Return CType(INDTxtTopCuotaModeradora.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDTxtTopCuotaModeradora.EditValue = value
        End Set
    End Property

    Public Property TopYearCopago As Decimal
        Get
            Return CType(INDTxtTopCopago.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDTxtTopCopago.EditValue = value
        End Set
    End Property

    Public Property TopYearCuotaRecuperacion As Decimal
        Get
            Return CType(INDTxtTopCuotaRecuperacion.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDTxtTopCuotaRecuperacion.EditValue = value
        End Set
    End Property

    Public Property Anio As Integer
        Get
            Return CType(INDTxtAnio.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDTxtAnio.EditValue = value
        End Set
    End Property

#End Region

#Region "Properties"
    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDbeIdentification.Enabled = Not value
            INDgleIdentificationType.Enabled = value
            INDsleCityExpedition.Enabled = value
            INDteFName.Enabled = value
            INDteSName.Enabled = value
            INDteFLName.Enabled = value
            INDteSLName.Enabled = value
            INDteBirthDate.Enabled = value
            INDgleMaritalStatus.Enabled = value
            INDgleGender.Enabled = value
            INDsleEthnicGroup.Enabled = value
            INDsleCompany.Enabled = value
            INDsleLocation.Enabled = value
            INDsleActivity.Enabled = value
            INDteAddress.Enabled = value
            INDteMobile.Enabled = value
            INDtePhone.Enabled = value
            INDteEmail.Enabled = value
            INDglePatientType.Enabled = value
            INDgleAffiliateType.Enabled = value
            INDsleCareGroup.Enabled = value
            INDgleAbilityPay.Enabled = value
            INDgle3047.Enabled = value
            INDsleLevel.Enabled = value
            INDmeObservations.Enabled = value
            INDsleEducationLevel.Enabled = value
            INDsleLanguage.Enabled = value
            INDgleStratum.Enabled = value
            INDsleBelief.Enabled = value
            INDsleDisability.Enabled = value
            INDsleSpecialGroups.Enabled = value
            INDPceAddTopYear.Enabled = value
            INDGcTopAnual.Enabled = value
            If value = False Then
                INDbeIdentification.Focus()
            Else
                INDgleIdentificationType.Focus()
            End If
        End Set
    End Property
    ''' <summary>
    ''' Datasource de ciudades
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DatasourceListCitys As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return Me.INDsleCityExpedition.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            Me.INDsleCityExpedition.Properties.DataSource = value
        End Set
    End Property

    Public Property Citys As Object
        Get
            Return Me.INDsleCityExpedition.EditValue
        End Get
        Set(value As Object)
            Me.INDsleCityExpedition.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Gets or sets a value indicating whether [state].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [state]; otherwise, <c>false</c>.
    ''' </value>
    Public Property State As Boolean
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property
#End Region

#Region "Load"
    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPatient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcPatient, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        'Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PPatient(Me)

        '******* Inicializo Controles**********
        'Load_Activities()
        'Load_EthnicGroup()
        'Load_Company()
        'Load_Location()
        'Load_CareGroup()
        'Load_Levels()
        'Load_EducationLevels()
        'Load_Language()
        'Load_Belief()
        'Load_Disability()
        'Load_SpecialGroups()
        SetListIdentificationType()
        SetListMaritalStatus()
        SetGender()
        SetPatientType()
        SetAffiliateType()
        SetAbilityPay()
        Set3047()
        SetStratum()
        SetAnonimusType()

        Dim actionList As New List(Of eAcciones)()
        actionList.Add(eAcciones.Remove)
        actionList.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvTopYear, actionList)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvTopYear.Columns
            If col.Name = "colActions" Then
                col.Width = 150
            End If
        Next

        If BarraBotones.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ModificarTopeAnual)) Then
            INDLcgPatientTopYear.HideControl(False)
            Anio = GetDateServer().Year
        Else
            INDLcgPatientTopYear.HideControl()
        End If

        If FormSearchObjects Is Nothing Then
            SearchMode = False
        End If
        LoadStatus()
        Deshacer()
    End Sub
#End Region

#Region "Load Controls Datasource"
    ''' <summary>
    ''' cargar Ciudades
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_Citys()
        Using Model As New MPatient(Me.Tag)
            DatasourceListCitys = Model.ListAllCities()
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar actividades
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_Activities()
        Using Model As New MPatient(Me.Tag)
            INDsleActivity.Properties.DataSource = Model.ListActivities
            INDsleActivity.Properties.PopupFormSize = New Size(500, 350)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar actividades
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_EthnicGroup()
        Using Model As New MPatient(Me.Tag)
            INDsleEthnicGroup.Properties.DataSource = Model.ListEthnicGroupHIS
            INDsleEthnicGroup.Properties.PopupFormSize = New Size(500, 350)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar empresas
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_Company()
        Using Model As New MPatient(Me.Tag)
            INDsleCompany.Properties.DataSource = Model.ListCompanyHIS
            INDsleCompany.Properties.PopupFormSize = New Size(500, 350)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar Ubicaciones
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_Location()
        Using Model As New MPatient(Me.Tag)
            INDsleLocation.Properties.DataSource = Model.ListLocationHIS
            INDsleLocation.Properties.PopupFormSize = New Size(600, 350)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar centros de atención
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_CareGroup()
        Using Model As New MPatient(Me.Tag)
            INDsleCareGroup.Properties.DataSource = Model.ListCareGroup
            INDsleCareGroup.Properties.PopupFormSize = New Size(500, 350)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar niveles
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_Levels()
        Using Model As New MPatient(Me.Tag)
            INDsleLevel.Properties.DataSource = Model.ListLevelsSHIS
            INDsleLevel.Properties.PopupFormSize = New Size(500, 350)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar niveles
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_EducationLevels()
        Using Model As New MPatient(Me.Tag)
            INDsleEducationLevel.Properties.DataSource = Model.ListEducationLevelsHIS
            INDsleEducationLevel.Properties.PopupFormSize = New Size(500, 350)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar niveles
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_Language()
        Using Model As New MPatient(Me.Tag)
            INDsleLanguage.Properties.DataSource = Model.ListLanguageHIS
            INDsleLanguage.Properties.PopupFormSize = New Size(500, 350)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar creencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_Belief()
        Using Model As New MPatient(Me.Tag)
            INDsleBelief.Properties.DataSource = Model.ListBeliefHIS
            INDsleBelief.Properties.PopupFormSize = New Size(500, 350)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar discapacidades
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_Disability()
        Using Model As New MPatient(Me.Tag)
            INDsleDisability.Properties.DataSource = Model.ListDisabilityHIS
            INDsleDisability.Properties.PopupFormSize = New Size(500, 350)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar grupos especiales
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_SpecialGroups()
        Using Model As New MPatient(Me.Tag)
            INDsleSpecialGroups.Properties.DataSource = Model.ListSpecialGroupsHIS
            INDsleSpecialGroups.Properties.PopupFormSize = New Size(500, 350)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para cargar grupos especiales
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_Centers()
        Using Model As New MPatient(Me.Tag)
            INDsleAtentionCenter.Properties.DataSource = Model.ListCenter
            INDsleAtentionCenter.Properties.PopupFormSize = New Size(500, 350)
            '  INDsleAtentionCenter.ShowPopup()
        End Using
    End Sub


#End Region

#Region "Tuples"
    ''' <summary>
    ''' listado de los tipos de identificación
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listIdentificationType As List(Of Tuple(Of Integer, String))
    Sub SetListIdentificationType(Optional MS_or_AS As Boolean = False, Optional ShowControlAnonymuos As Boolean = True)
        _listIdentificationType = New List(Of Tuple(Of Integer, String))
        If MS_or_AS = False Then
            INDlciAnonimus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            _listIdentificationType.Add(New Tuple(Of Integer, String)(1, "CC"))
            _listIdentificationType.Add(New Tuple(Of Integer, String)(2, "CE"))
            _listIdentificationType.Add(New Tuple(Of Integer, String)(3, "TI"))
            _listIdentificationType.Add(New Tuple(Of Integer, String)(4, "RC"))
            _listIdentificationType.Add(New Tuple(Of Integer, String)(5, "PA"))
            _listIdentificationType.Add(New Tuple(Of Integer, String)(8, "NU"))
        Else
            If ShowControlAnonymuos = True Then
                INDlciAnonimus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
            _listIdentificationType.Add(New Tuple(Of Integer, String)(6, "AS"))
            _listIdentificationType.Add(New Tuple(Of Integer, String)(7, "MS"))
        End If
        INDgleIdentificationType.Properties.DataSource = _listIdentificationType
        INDgleIdentificationType.Properties.PopupFormSize = New Size(500, 350)

    End Sub

    ''' <summary>
    ''' listado de los tipos de identificación
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listMaritalStatus As List(Of Tuple(Of Integer, String))
    Sub SetListMaritalStatus()
        If _listMaritalStatus Is Nothing Then
            _listMaritalStatus = New List(Of Tuple(Of Integer, String))
            _listMaritalStatus.Add(New Tuple(Of Integer, String)(1, "Soltero (a)"))
            _listMaritalStatus.Add(New Tuple(Of Integer, String)(2, "Casado (a)"))
            _listMaritalStatus.Add(New Tuple(Of Integer, String)(3, "Unión Libre"))
            _listMaritalStatus.Add(New Tuple(Of Integer, String)(4, "Viudo (a)"))
            _listMaritalStatus.Add(New Tuple(Of Integer, String)(5, "Separado"))
            INDgleMaritalStatus.Properties.DataSource = _listMaritalStatus
            INDgleMaritalStatus.Properties.PopupFormSize = New Size(500, 350)
        End If
    End Sub

    ''' <summary>
    ''' listado de los tipos de identificación
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listGender As List(Of Tuple(Of Integer, String))
    Sub SetGender()
        If _listGender Is Nothing Then
            _listGender = New List(Of Tuple(Of Integer, String))
            _listGender.Add(New Tuple(Of Integer, String)(1, "Masculino"))
            _listGender.Add(New Tuple(Of Integer, String)(2, "Femenino"))
            INDgleGender.Properties.DataSource = _listGender
            INDgleGender.Properties.PopupFormSize = New Size(500, 350)
        End If
    End Sub

    ''' <summary>
    ''' listado de los tipos de paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listPatientType As List(Of Tuple(Of Integer, String))
    Sub SetPatientType()
        If _listPatientType Is Nothing Then
            _listPatientType = New List(Of Tuple(Of Integer, String))
            _listPatientType.Add(New Tuple(Of Integer, String)(1, "Contributivo"))
            _listPatientType.Add(New Tuple(Of Integer, String)(2, "Subsidiado"))
            _listPatientType.Add(New Tuple(Of Integer, String)(3, "Vinculado"))
            _listPatientType.Add(New Tuple(Of Integer, String)(4, "Particular"))
            _listPatientType.Add(New Tuple(Of Integer, String)(5, "Otro"))
            _listPatientType.Add(New Tuple(Of Integer, String)(6, "Desplazado Reg. Contributivo"))
            _listPatientType.Add(New Tuple(Of Integer, String)(7, "Desplazado Reg. Subsidiado"))
            _listPatientType.Add(New Tuple(Of Integer, String)(8, "Desplazado No Asegurado"))
            INDglePatientType.Properties.DataSource = _listPatientType
            INDglePatientType.Properties.PopupFormSize = New Size(500, 350)
        End If
    End Sub

    ''' <summary>
    ''' listado de los tipos de afiliacion Paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listAffiliateType As List(Of Tuple(Of Integer, String))
    Sub SetAffiliateType()
        If _listAffiliateType Is Nothing Then
            _listAffiliateType = New List(Of Tuple(Of Integer, String))
            _listAffiliateType.Add(New Tuple(Of Integer, String)(1, "Cotizante"))
            _listAffiliateType.Add(New Tuple(Of Integer, String)(2, "Beneficiario"))
            _listAffiliateType.Add(New Tuple(Of Integer, String)(3, "Adicional"))
            _listAffiliateType.Add(New Tuple(Of Integer, String)(4, "Jubilado / Retirado"))
            _listAffiliateType.Add(New Tuple(Of Integer, String)(5, "Pensionado"))
            INDgleAffiliateType.Properties.DataSource = _listAffiliateType
            INDgleAffiliateType.Properties.PopupFormSize = New Size(500, 350)
        End If
    End Sub

    ''' <summary>
    ''' listado de las habilidades de pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listAbilityPay As List(Of Tuple(Of Integer, String))
    Sub SetAbilityPay()
        If _listAbilityPay Is Nothing Then
            _listAbilityPay = New List(Of Tuple(Of Integer, String))
            _listAbilityPay.Add(New Tuple(Of Integer, String)(1, "Total Paciente"))
            _listAbilityPay.Add(New Tuple(Of Integer, String)(2, "Cuota Recuperacion"))
            _listAbilityPay.Add(New Tuple(Of Integer, String)(3, "Total Entidad"))
            INDgleAbilityPay.Properties.DataSource = _listAbilityPay
            INDgleAbilityPay.Properties.PopupFormSize = New Size(500, 350)
        End If
    End Sub

    ''' <summary>
    ''' listado de cobertura 3047
    ''' </summary>
    ''' <remarks></remarks>
    Dim _list3047 As List(Of Tuple(Of Integer, String))
    Sub Set3047()
        If _list3047 Is Nothing Then
            _list3047 = New List(Of Tuple(Of Integer, String))
            _list3047.Add(New Tuple(Of Integer, String)(1, "Contributivo"))
            _list3047.Add(New Tuple(Of Integer, String)(2, "Subsidiado Total"))
            _list3047.Add(New Tuple(Of Integer, String)(3, "Subsidio Parcial"))
            _list3047.Add(New Tuple(Of Integer, String)(4, "Poblacion Pobre sin Asegurar con Sisben"))
            _list3047.Add(New Tuple(Of Integer, String)(5, "Poblacion Pobre sin Asegurar sin Sisben"))
            _list3047.Add(New Tuple(Of Integer, String)(6, "Desplazados"))
            _list3047.Add(New Tuple(Of Integer, String)(7, "Plan de Salud Adicional"))
            _list3047.Add(New Tuple(Of Integer, String)(8, "Otro"))
            INDgle3047.Properties.DataSource = _list3047
            INDgle3047.Properties.PopupFormSize = New Size(500, 350)
        End If
    End Sub

    ''' <summary>
    ''' listado de los estratos
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listStratum As List(Of Tuple(Of Integer, String))
    Sub SetStratum()
        If _listStratum Is Nothing Then
            _listStratum = New List(Of Tuple(Of Integer, String))
            _listStratum.Add(New Tuple(Of Integer, String)(1, "ESTRATO 1"))
            _listStratum.Add(New Tuple(Of Integer, String)(2, "ESTRATO 2"))
            _listStratum.Add(New Tuple(Of Integer, String)(3, "ESTRATO 3"))
            _listStratum.Add(New Tuple(Of Integer, String)(4, "ESTRATO 4"))
            _listStratum.Add(New Tuple(Of Integer, String)(5, "ESTRATO 5"))
            _listStratum.Add(New Tuple(Of Integer, String)(6, "ESTRATO 6"))
            _listStratum.Add(New Tuple(Of Integer, String)(7, "ESTRATO 7"))
            INDgleStratum.Properties.DataSource = _listStratum
            INDgleStratum.Properties.PopupFormSize = New Size(500, 350)
        End If
    End Sub

    ''' <summary>
    ''' listado de los tipos de anonimos
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listAnonimus As List(Of Tuple(Of String, String))
    Sub SetAnonimusType()
        _listAnonimus = New List(Of Tuple(Of String, String))
        If INDgleIdentificationType.EditValue IsNot Nothing Then
            If INDgleIdentificationType.EditValue = "6" Then 'adulto
                _listAnonimus.Add(New Tuple(Of String, String)("0", "Personas de la tercera edad en protección de ancianatos."))
                _listAnonimus.Add(New Tuple(Of String, String)("1", "Comunidad Indígena que no este identificada por la Registraduría Nacional del Estado Civil."))
                _listAnonimus.Add(New Tuple(Of String, String)("2", "Población indigente."))
            End If
            If INDgleIdentificationType.EditValue = "7" Then 'Menor
                _listAnonimus.Add(New Tuple(Of String, String)("3", "Población infantil a cargo del ICBF."))
                _listAnonimus.Add(New Tuple(Of String, String)("4", "Comunidad indígena menor de edad no identificada por la RNEC."))
                _listAnonimus.Add(New Tuple(Of String, String)("5", "Menor de edad recién nacido vivo sin identificar al infante con edad menor o igual a 30 días."))
                INDgleMaritalStatus.EditValue = 1
            End If
        End If
        INDgleAnonimusType.Properties.DataSource = _listAnonimus
        INDgleAnonimusType.Properties.PopupFormSize = New Size(500, 350)
    End Sub

#End Region

#Region "Methods"
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
        INDlcPatient.BeginUpdate()
        ActionsOnControls = False
        Me.Patient = Nothing
        ADNIVELES = Nothing
        INDlciIdentification.ShowInCustomizationForm = False
        INDbeIdentification.Text = String.Empty
        INDgleIdentificationType.EditValue = Nothing
        INDsleCityExpedition.Properties.NullText = String.Empty
        INDsleCityExpedition.Properties.DataSource = Nothing
        INDteFName.Text = String.Empty
        INDteSName.Text = String.Empty
        INDteFLName.Text = String.Empty
        INDteSLName.Text = String.Empty
        INDteMobile.Text = String.Empty
        INDteBirthDate.EditValue = Nothing
        INDgleMaritalStatus.EditValue = Nothing
        INDgleGender.EditValue = Nothing
        INDsleEthnicGroup.EditValue = Nothing
        INDsleEthnicGroup.Properties.NullText = String.Empty
        INDsleCompany.EditValue = Nothing
        INDsleCompany.Properties.NullText = String.Empty
        INDsleLocation.EditValue = Nothing
        INDsleLocation.Properties.NullText = String.Empty
        INDsleActivity.EditValue = Nothing
        INDsleActivity.Properties.NullText = String.Empty
        INDteAddress.Text = String.Empty
        INDtePhone.Text = String.Empty
        INDteEmail.Text = String.Empty
        INDglePatientType.EditValue = Nothing
        INDgleAffiliateType.EditValue = Nothing
        INDsleCareGroup.EditValue = Nothing
        INDsleCareGroup.Properties.NullText = String.Empty
        INDgleAbilityPay.EditValue = Nothing
        INDgle3047.EditValue = Nothing
        INDsleLevel.EditValue = Nothing
        INDsleLevel.Properties.NullText = String.Empty
        INDmeObservations.Text = String.Empty
        INDsleEducationLevel.EditValue = Nothing
        INDsleEducationLevel.Properties.NullText = String.Empty
        INDsleLanguage.EditValue = Nothing
        INDsleLanguage.Properties.NullText = String.Empty
        INDgleStratum.EditValue = Nothing
        INDgleStratum.Properties.NullText = String.Empty
        INDsleBelief.EditValue = Nothing
        INDsleBelief.Properties.NullText = String.Empty
        INDsleDisability.EditValue = Nothing
        INDsleDisability.Properties.NullText = String.Empty
        INDsleSpecialGroups.EditValue = Nothing
        INDsleSpecialGroups.Properties.NullText = String.Empty
        TempIdentification = String.Empty
        INDlciAnonimus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDlciExpedition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDgleAnonimusType.EditValue = Nothing
        INDteMotherIdentification.Text = String.Empty
        INDseSonNumber.EditValue = 1
        INDsleAtentionCenter.EditValue = Nothing
        INDliEntityCups.HideControl(True)

        ListTopAnual = Nothing
        TopYearCopago = 0
        TopYearCuotaModeradora = 0
        TopYearCuotaRecuperacion = 0
        _isEditingTopYear = False

        Me.AnonimusAdult = False
        Me.AnonimusRegBaby = False
        Me.CodAnonimusAdult = String.Empty
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.CleanAuditBasic()
        DeleteBlockedRecord()
        INDliContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        INDlcPatient.EndUpdate()
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MPatient(Me.Tag)
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo que inicializa un nuevo paciente sin numero de identificación
    ''' </summary>
    ''' <remarks></remarks>
    Sub NewPatient_MS_AS()
        Patient = New INPACIENT With {.ESTADOPAC = True}
        ActionsOnControls = True
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        SetListIdentificationType(True)
        INDlciExpedition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'Me.BarraBotones.StatusRecordVisible = True
    End Sub

    ''' <summary>
    '''  esta función establece el contexto y la interfaz gráfica para crear
    '''  un nuevo paciente, configurando la barra de herramientas y mostrando ciertos controles específicos.
    ''' </summary>
    Sub NewPatient()
        Patient = New INPACIENT With {.ESTADOPAC = True}
        ActionsOnControls = True
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        INDlciExpedition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Sub LoadControls()
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        Using Model As New MPatient(Me.Tag)
            AsyncLoader(True)
            Dim resultOperation As ActionResult(Of INPACIENT) = Await Model.GetPacientByIdentification(INDbeIdentification.Text.Trim)
            AsyncLoader(False)
            Patient = resultOperation.ObjectEmbbeded
            If resultOperation.StateResult Then
                If resultOperation.ObjectEmbbeded IsNot Nothing AndAlso resultOperation.ObjectEmbbeded.IPCODPACI IsNot Nothing AndAlso resultOperation.ObjectEmbbeded.IPCODPACI.ToString IsNot String.Empty Then
                    Me.BarraBotones.StatusRecordVisible = True
                    ActionsOnControls = True
                    'Dim result = Await Model.GetBlockRecord(Me.Tag, resultOperation.ObjectEmbbeded.IPCODPACI)
                    With resultOperation.ObjectEmbbeded
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        If .IPTIPODOC.ToString = "6" Or .IPTIPODOC.ToString = "7" Then
                            SetListIdentificationType(True, False)
                            INDgleIdentificationType.Enabled = False
                            INDlciExpedition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Else
                            SetListIdentificationType()
                            INDlciExpedition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        End If
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CODUSUCRE)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .FECREGCRE)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .CODUSUMOD)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .FECREGMOD)
                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        INDbeIdentification.Text = .IPCODPACI.ToString
                        INDgleIdentificationType.EditValue = .IPTIPODOC
                        INDsleCityExpedition.Properties.NullText = .ExpeditionCityDescription
                        Citys = .GENEXPEDITIONCITY
                        INDteFName.Text = .IPPRINOMB.ToString.Trim()
                        INDteSName.Text = .IPSEGNOMB.ToString.Trim()
                        INDteFLName.Text = .IPPRIAPEL.ToString.Trim()
                        INDteSLName.Text = .IPSEGAPEL.ToString.Trim()
                        INDteBirthDate.EditValue = .IPFECNACI
                        INDgleMaritalStatus.EditValue = .IPESTADOC.ToString
                        INDgleGender.EditValue = .IPSEXOPAC.ToString
                        INDsleEthnicGroup.EditValue = .CODGRUPOE
                        INDsleEthnicGroup.Properties.NullText = .EtGroupDesc
                        INDsleCompany.EditValue = .CODEMPRES
                        INDsleCompany.Properties.NullText = .CompanyDesc
                        INDsleLocation.EditValue = .AUUBICACI
                        INDsleLocation.Properties.NullText = .LocationDesc
                        INDsleActivity.EditValue = .CODACTIVI
                        INDsleActivity.Properties.NullText = .ActivityDesc
                        INDteAddress.Text = .IPDIRECCI
                        INDtePhone.Text = .IPTELEFON
                        INDteMobile.Text = .IPTELMOVI.ToString
                        INDteEmail.Text = .CORELEPAC
                        INDglePatientType.EditValue = .IPTIPOPAC
                        'If INDglePatientType.EditValue IsNot Nothing AndAlso INDglePatientType.EditValue = 1 Then
                        '    INDgleAbilityPay.Enabled = False
                        '    INDgleAffiliateType.Enabled = True
                        'ElseIf INDglePatientType.EditValue IsNot Nothing AndAlso (INDglePatientType.EditValue = 2 Or INDglePatientType.EditValue = 3) Then
                        '    INDgleAbilityPay.Enabled = True
                        '    INDgleAffiliateType.Enabled = False
                        'End If
                        INDgleAffiliateType.EditValue = .IPTIPOAFI
                        INDsleCareGroup.EditValue = .GENCAREGROUP
                        INDsleCareGroup.Properties.NullText = .CareGroupDesc
                        INDgleAbilityPay.EditValue = .CAPACIPAG
                        INDgle3047.EditValue = .TIPCOBSAL
                        INDsleLevel.EditValue = .NIVCODIGO
                        INDsleLevel.Properties.NullText = .LevelDescription
                        INDmeObservations.Text = .OBSERVACI
                        INDsleEducationLevel.EditValue = .NIVECODIGO
                        INDsleEducationLevel.Properties.NullText = .EducationLevelDesc
                        INDsleLanguage.EditValue = .IDICODIGO
                        INDsleLanguage.Properties.NullText = .LanguageDesc
                        INDgleStratum.EditValue = .IPESTRATO
                        INDsleBelief.EditValue = .CREDCODIGO
                        INDsleBelief.Properties.NullText = .BeliefDesc
                        INDsleDisability.EditValue = .DISCCODIGO
                        INDsleDisability.Properties.NullText = .DisabilityDesc
                        INDsleSpecialGroups.EditValue = .GRUPCODIGO
                        INDsleSpecialGroups.Properties.NullText = .SpecialGroupDesc
                        Me.State = .ESTADOPAC
                    End With
                    ListTopAnual = Patient.INPACIENTTOPANU.ToList()
                    Me.GetDocumentIndexed(Me.Tag & "_" & resultOperation.ObjectEmbbeded.IPCODPACI)
                    'If result.Id = 0 Then
                    '    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    '    state.State = Domain.Base.Entities.ObjectState.Added
                    '    record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = resultOperation.ObjectEmbbeded.IPCODPACI}
                    '    Dim operation = Await Model.SaveBlockRecord(record)
                    '    record = operation.ObjectEmbbeded
                    'Else
                    '    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    '    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning)
                    'End If
                    'Me.BarraBotones.SetDocuments(Patient.IPCODPACI, Me.Tag.ToString(), Nothing, GetType(INPACIENT).Name)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    INDbeIdentification.Enabled = False
                    Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                Else
                    TempIdentification = INDbeIdentification.Text
                    INDbeIdentification.Text = String.Empty
                    Me.Mensaje(EeventViewerImages.Informacion) = "Digite De Nuevo El Número De Identificación Del Paciente Por Favor!"
                    INDbeIdentification.Focus()
                End If
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Método para asignar valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Sub AssigningValues()
        With Patient
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            Patient.StopTracking()
            .IPCODPACI = INDbeIdentification.Text
            .IPTIPODOC = INDgleIdentificationType.EditValue
            .IPEXPEDIC = INDsleCityExpedition.Text
            .GENEXPEDITIONCITY = Citys
            .IPPRINOMB = INDteFName.Text
            .IPSEGNOMB = INDteSName.Text
            .IPPRIAPEL = INDteFLName.Text
            .IPSEGAPEL = INDteSLName.Text
            .IPFECNACI = INDteBirthDate.EditValue
            .IPESTADOC = INDgleMaritalStatus.EditValue
            .IPSEXOPAC = INDgleGender.EditValue
            .CODGRUPOE = INDsleEthnicGroup.EditValue
            .CODEMPRES = INDsleCompany.EditValue
            .AUUBICACI = INDsleLocation.EditValue
            .CODACTIVI = INDsleActivity.EditValue
            .IPDIRECCI = INDteAddress.Text
            .IPTELEFON = INDtePhone.Text
            .IPTELMOVI = INDteMobile.Text
            .CORELEPAC = INDteEmail.Text
            .IPTIPOPAC = INDglePatientType.EditValue
            .IPTIPOAFI = INDgleAffiliateType.EditValue
            .GENCAREGROUP = INDsleCareGroup.EditValue
            .CAPACIPAG = INDgleAbilityPay.EditValue
            .TIPCOBSAL = INDgle3047.EditValue
            'If Patient.ChangeTracker.State = ObjectState.Added Then
            '    .ADNIVELES = ADNIVELES
            'End If
            .OBSERVACI = INDmeObservations.Text
            .NIVECODIGO = INDsleEducationLevel.EditValue
            .IDICODIGO = INDsleLanguage.EditValue
            .IPESTRATO = INDgleStratum.EditValue
            .CREDCODIGO = INDsleBelief.EditValue
            .DISCCODIGO = INDsleDisability.EditValue
            .GRUPCODIGO = INDsleSpecialGroups.EditValue
            If INDliContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .GENCONENTITY = _HealthAdministratorId
            Else
                .GENCONENTITY = INDsleEntityHealth.EditValue
            End If
            If INDlciAnonimus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AnonymousType = INDgleAnonimusType.EditValue
                If .AnonymousType = "4" Then
                    .AnonymousType = "1"
                End If
                .AnonimusRegBaby = AnonimusRegBaby
                .AnonimusAdult = AnonimusAdult
                .CodAnonimusAdult = CodAnonimusAdult
            End If
            .NIVCODIGO = INDsleLevel.EditValue
            'If INDsleLevel.EditValue IsNot Nothing AndAlso INDsleLevel.Text IsNot String.Empty Then
            '    If Patient.ADNIVELES Is Nothing OrElse ADNIVELES.NIVCODIGO <> INDsleLevel.EditValue.ToString.Trim Then
            '        Using m As New MPatient(Me.Tag)
            '            ADNIVELES = m.GetLevelByCode(INDsleLevel.EditValue.ToString.Trim)
            '            If ADNIVELES IsNot Nothing AndAlso ADNIVELES.NIVCODIGO.ToString.Trim IsNot String.Empty Then
            '                Patient.ADNIVELES = ADNIVELES
            '            End If
            '        End Using
            '    End If
            'End If
            Patient.StartTracking()
        End With
    End Sub

    ''' <summary>
    ''' Funcion para validar si la identificación ingresada existe
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function ValidateMother(Identification As String) As Task(Of Boolean)
        Using Model As New MPatient(Me.Tag)

            Dim resultOperation As ActionResult(Of INPACIENT) = Await Model.GetPacientByIdentification(Identification)
            If resultOperation.StateResult Then
                If resultOperation.ObjectEmbbeded IsNot Nothing AndAlso resultOperation.ObjectEmbbeded.IPCODPACI IsNot Nothing AndAlso resultOperation.ObjectEmbbeded.IPCODPACI.ToString IsNot String.Empty Then
                    Patient = New INPACIENT
                    If MessageIndigo.Show(ResourceManager.GetString("LoadMotherInformation", NAME_MODULE), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        With resultOperation.ObjectEmbbeded
                            If .ESTADOPAC = False Then
                                Me.Mensaje(EeventViewerImages.Informacion) = "El Paciente Esta Inactivo!"
                                Return False
                            End If
                            INDbeIdentification.Text = .IPCODPACI.ToString.Trim
                            'INDgleIdentificationType.EditValue = .IPTIPODOC
                            'INDteExpedition.Text = .IPEXPEDIC
                            INDteFName.Text = "HIJO"
                            INDteSName.Text = "DE " & .IPPRINOMB.ToString.Trim & " " & .IPSEGNOMB.ToString.Trim
                            INDteFLName.Text = .IPPRIAPEL
                            INDteSLName.Text = .IPSEGAPEL
                            'INDteBirthDate.EditValue = .IPFECNACI
                            'INDgleMaritalStatus.EditValue = .IPESTADOC.ToString
                            'INDgleGender.EditValue = .IPSEXOPAC.ToString
                            'INDsleEthnicGroup.EditValue = .CODGRUPOE
                            'INDsleEthnicGroup.Properties.NullText = .EtGroupDesc
                            INDsleCompany.EditValue = .CODEMPRES
                            INDsleCompany.Properties.NullText = .CompanyDesc
                            INDsleLocation.EditValue = .AUUBICACI
                            INDsleLocation.Properties.NullText = .LocationDesc
                            INDsleActivity.EditValue = .CODACTIVI
                            INDsleActivity.Properties.NullText = .ActivityDesc
                            'INDteAddress.Text = .IPDIRECCI.Trim
                            INDtePhone.Text = .IPTELEFON.Trim
                            INDteMobile.Text = .IPTELMOVI
                            'INDteEmail.Text = .CORELEPAC
                            INDglePatientType.EditValue = .IPTIPOPAC
                            SetStatusControlsPatient()
                            'If INDglePatientType.EditValue IsNot Nothing AndAlso INDglePatientType.EditValue = 1 Then
                            '    INDgleAbilityPay.Enabled = False
                            '    INDgleAffiliateType.Enabled = True
                            'ElseIf INDglePatientType.EditValue IsNot Nothing AndAlso (INDglePatientType.EditValue = 2 Or INDglePatientType.EditValue = 3) Then
                            '    INDgleAbilityPay.Enabled = True
                            '    INDgleAffiliateType.Enabled = False
                            'End If
                            INDgleAffiliateType.EditValue = .IPTIPOAFI
                            INDsleCareGroup.EditValue = .GENCAREGROUP
                            INDsleCareGroup.Properties.NullText = .CareGroupDesc
                            INDgleAbilityPay.EditValue = .CAPACIPAG
                            INDgle3047.EditValue = .TIPCOBSAL
                            'ADNIVELES = .ADNIVELES
                            'If .ADNIVELES IsNot Nothing AndAlso .ADNIVELES.NIVCODIGO.ToString.Trim IsNot String.Empty Then
                            '    INDsleLevel.EditValue = .ADNIVELES.NIVCODIGO.ToString.Trim
                            '    INDsleLevel.Properties.NullText = .ADNIVELES.NIVDESCRI.ToString.Trim
                            'End If
                            INDsleLevel.EditValue = .NIVCODIGO
                            INDsleLevel.Properties.NullText = .LevelDescription
                            'INDmeObservations.Text = .OBSERVACI.Trim
                            'INDsleEducationLevel.EditValue = .NIVECODIGO
                            'INDsleLanguage.EditValue = .IDICODIGO
                            'INDsleLanguage.Properties.NullText = .LanguageDesc
                            'INDgleStratum.EditValue = .IPESTRATO
                            'INDsleBelief.EditValue = .CREDCODIGO
                            'INDsleBelief.Properties.NullText = .BeliefDesc
                            'INDsleDisability.EditValue = .DISCCODIGO
                            'INDsleDisability.Properties.NullText = .DisabilityDesc
                            'INDsleSpecialGroups.EditValue = .GRUPCODIGO
                            'INDsleSpecialGroups.Properties.NullText = .SpecialGroupDesc
                            Me.State = .ESTADOPAC
                        End With
                    End If

                    If resultOperation.ObjectEmbbeded.SonNumber >= INDseSonNumber.EditValue Then
                        Mensaje(EeventViewerImages.Advertencia) = "La Madre Tiene Registrados: " & INDseSonNumber.EditValue & " Hijos"
                        INDseSonNumber.Focus()
                        Return False
                    End If

                    Return True
                Else
                    Me.Mensaje(EeventViewerImages.Informacion) = "El Paciente No Existe!"
                    Return False
                End If
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = resultOperation.Message
                Return False
            End If
        End Using
    End Function

    ''' <summary>
    ''' Obtener el código del paciente anónimo 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CodigoAfiliadoAnonimo() As String
        Try
        Dim LocationCode As String = ""
        If INDsleAtentionCenter.EditValue IsNot Nothing AndAlso INDsleAtentionCenter.Text IsNot String.Empty Then
            LocationCode = INDsleAtentionCenterView.GetFocusedRowCellValue("DEPMUNCOD")
        End If

        Select Case INDgleAnonimusType.EditValue
            Case Is = "0"
                Return String.Concat(LocationCode.ToString.Trim, "S")
            Case Is = "1"
                Return String.Concat(LocationCode.ToString.Trim, "I")
            Case Is = "2"
                Return String.Concat(LocationCode.ToString.Trim, "D")
            Case Is = "3"
                Return String.Concat(LocationCode.ToString.Trim, "A")
            Case Is = "4"
                Return String.Concat(LocationCode.ToString.Trim, "I")
            Case Else
                Return ""
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Events Handles"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        Model = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
        SearchMode = Nothing
        _isEditingTopYear = Nothing
        Patient = Nothing
        TempIdentification = Nothing
        AnonimusRegBaby = Nothing
        AnonimusAdult = Nothing
        CodAnonimusAdult = Nothing
        ADNIVELES = Nothing
        Isinsurance = Nothing
        _HealthAdministratorId = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se ejecuta al cerrar el formulario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmPatient_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Evento para controlar si se abre el control de tipos de anonimo y no se ha seleccionado el tipo de identificación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleAnonimusType_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDgleAnonimusType.QueryPopUp
        If INDgleIdentificationType.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe Seleccionar El Tipo De Identificación Primero!"
            INDgleAnonimusType.ClosePopup()
            INDpceAnonimus.ClosePopup()
            INDgleIdentificationType.Focus()
            INDgleIdentificationType.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Carga los tipos de anonimo cuando se cambia el tipo de identificación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleIdentificationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleIdentificationType.EditValueChanged
        SetAnonimusType()
    End Sub



    ''' <summary>
    ''' Carga los grupos etnicos cuando se abre el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEthnicGroup_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEthnicGroup.QueryPopUp
        If INDsleEthnicGroup.Properties.DataSource Is Nothing Then
            Load_EthnicGroup()
        End If
    End Sub

    ''' <summary>
    ''' Carga las empresas cuando se abre el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCompany_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCompany.QueryPopUp
        If INDsleCompany.Properties.DataSource Is Nothing Then
            Load_Company()
        End If
    End Sub

    ''' <summary>
    ''' Carga las ubicaciones cuando se abre el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLocation_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleLocation.QueryPopUp
        If INDsleLocation.Properties.DataSource Is Nothing Then
            Load_Location()
        End If
    End Sub

    ''' <summary>
    ''' Carga las actividades cuando se abre el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleActivity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleActivity.QueryPopUp
        If INDsleActivity.Properties.DataSource Is Nothing Then
            Load_Activities()
        End If
    End Sub

    ''' <summary>
    ''' Carga los grupos de atencion cuando se abre el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCareGroup_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCareGroup.QueryPopUp
        If INDsleCareGroup.Properties.DataSource Is Nothing Then
            Load_CareGroup()
        End If
    End Sub

    ''' <summary>
    ''' Carga los niveles cuando se abre el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLevel_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleLevel.QueryPopUp
        If INDsleLevel.Properties.DataSource Is Nothing Then
            Load_Levels()
        End If
    End Sub

    ''' <summary>
    ''' Carga los niveles cuando se abre el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEducationLevel_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEducationLevel.QueryPopUp
        If INDsleEducationLevel.Properties.DataSource Is Nothing Then
            Load_EducationLevels()
        End If
    End Sub

    ''' <summary>
    ''' Carga los lenguajes cuando se abre el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLanguage_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleLanguage.QueryPopUp
        If INDsleLanguage.Properties.DataSource Is Nothing Then
            Load_Language()
        End If
    End Sub

    ''' <summary>
    ''' Carga las creencias cuando se abre el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBelief_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleBelief.QueryPopUp
        If INDsleBelief.Properties.DataSource Is Nothing Then
            Load_Belief()
        End If
    End Sub

    ''' <summary>
    ''' carga las discapacidades cuando se abre el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDisability_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleDisability.QueryPopUp
        If INDsleDisability.Properties.DataSource Is Nothing Then
            Load_Disability()
        End If
    End Sub

    ''' <summary>
    ''' Carga los grupos especiales cuando se abre el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSpecialGroups_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleSpecialGroups.QueryPopUp
        If INDsleSpecialGroups.Properties.DataSource Is Nothing Then
            Load_SpecialGroups()
        End If
    End Sub

    ''' <summary>
    ''' Click Aceptar de Paciente Sin Identificación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsbAcceptAnonimus_Click(sender As Object, e As EventArgs) Handles INDsbAcceptAnonimus.Click
        AnonimusAdult = False
        AnonimusRegBaby = False
        CodAnonimusAdult = ""
        If INDgleAnonimusType.EditValue Is Nothing OrElse INDgleAnonimusType.EditValue Is String.Empty Then
            Exit Sub
        End If
        If INDgleAnonimusType.EditValue = "5" Then
            If INDteMotherIdentification.Text Is String.Empty Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe Digitar La Identificación De La Mama!"
                INDteMotherIdentification.Focus()
                Exit Sub
            End If
            If INDseSonNumber.EditValue <= 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe Digitar El Número De Hijo!"
                INDseSonNumber.Focus()
                Exit Sub
            End If
            If Await ValidateMother(INDteMotherIdentification.Text) = False Then
                Exit Sub
            End If
            AnonimusRegBaby = True
        Else
            If INDsleAtentionCenter.EditValue Is Nothing OrElse INDsleAtentionCenter.Text Is String.Empty Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe Seleccionar Centro De Atención!"
                INDsleAtentionCenter.Focus()
                INDsleAtentionCenter.ShowPopup()
                Exit Sub
            End If
            CodAnonimusAdult = CodigoAfiliadoAnonimo()
            If CodigoAfiliadoAnonimo() = String.Empty Then
                Mensaje(EeventViewerImages.Advertencia) = "Hubo Un Error Al consultar El Código!"
            Else
                AnonimusAdult = True
            End If
        End If
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        INDpceAnonimus.ClosePopup()
    End Sub

    ''' <summary>
    ''' Evento cuando se presiona una tecla en el código del paciente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbeIdentification_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbeIdentification.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If TempIdentification IsNot Nothing AndAlso TempIdentification IsNot String.Empty Then
                If INDbeIdentification.Text IsNot String.Empty Then
                    If TempIdentification.Trim = INDbeIdentification.Text.Trim Then
                        NewPatient()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "El Número De Identificación No Coincide!"
                        TempIdentification = String.Empty
                        INDbeIdentification.Text = String.Empty
                    End If
                End If
                Exit Sub
            End If
            If String.IsNullOrEmpty(INDbeIdentification.Text) Then
                SetListIdentificationType(True)
                NewPatient_MS_AS()
                INDbeIdentification.Enabled = False
            ElseIf INDbeIdentification.Text.ToString.Length >= 4 Then
                SetListIdentificationType()
                LoadControls()
            Else
                Mensaje(EeventViewerImages.Advertencia) = "El Número De Identificación No Puede Ser Inferior A 4!"
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Cuando se cambia de tipo de paciente se ocultan y se muestran controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDglePatientType_EditValueChanged(sender As Object, e As EventArgs) Handles INDglePatientType.EditValueChanged
        SetStatusControlsPatient()
    End Sub

    ''' <summary>
    ''' Coloca los controles ocultos o muestra segun opciones de pacientes
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetStatusControlsPatient()
        If INDglePatientType.EditValue IsNot Nothing Then
            Select Case INDglePatientType.EditValue
                Case "1"
                    INDlciAbilityPay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlciAffiliateType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Case "2"
                    INDlciAbilityPay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlciAffiliateType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDgleAffiliateType.Text = Nothing
                Case "3"
                    INDlciAbilityPay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlciAffiliateType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDgleAffiliateType.Text = Nothing
                Case "4"
                    INDlciAbilityPay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlciAffiliateType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDgleAffiliateType.Text = Nothing
                Case "5"
                    INDlciAbilityPay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlciAffiliateType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDgleAffiliateType.Text = Nothing
                Case "6"
                    INDlciAbilityPay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlciAffiliateType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Case "7"
                    INDlciAbilityPay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlciAffiliateType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDgleAffiliateType.Text = Nothing
                Case "8"
                    INDlciAbilityPay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlciAffiliateType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDgleAffiliateType.Text = Nothing
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Cuando se cambia la opción del tipo de anonimo se ocultan y muestran controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleAnonimusType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleAnonimusType.EditValueChanged
        INDlciMotherIdentification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlciSonNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlciAtentionCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        If INDgleAnonimusType.EditValue = "5" Then
            INDlciMotherIdentification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciSonNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Load_Centers()
            INDlciAtentionCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en el boton del control de almacenes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCareGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCareGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Me.OpenForm(985, Nothing, True)
            Load_CareGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en el boton del control de almacenes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntityCups_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntityHealth.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Me.OpenForm(972, Nothing, True)
            HealthAdministrator()
        End If
    End Sub


    ''' <summary>
    ''' este controlador de eventos ajusta la visibilidad y el estado de los controles en función del tipo de grupo de atención
    ''' seleccionado en INDsleCareGroup. Además, establece la variable Isinsurance según el tipo de grupo de atención.
    ''' </summary>
    Private Async Sub INDsleCareGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCareGroup.EditValueChanged
        If INDsleCareGroup.EditValue IsNot Nothing Then
            Using Model As New MCareGroup(Me.Tag)
                Dim careGroup = Await Model.GetCareGroupById(INDsleCareGroup.EditValue)
                If careGroup.ObjectEmbbeded.CareGroupType = 1 Then 'EAPB con contrato
                    Isinsurance = False
                    INDliContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDliEntityCups.HideControl(True)
                    INDteContract.Text = careGroup.ObjectEmbbeded.ContractDescription
                    INDteContract.Properties.ReadOnly = True
                    _HealthAdministratorId = careGroup.ObjectEmbbeded.Contract.HealthAdministratorId
                ElseIf careGroup.ObjectEmbbeded.CareGroupType = 2 Then 'Sin contrato
                    Isinsurance = False
                    INDliContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliEntityCups.HideControl(False)
                    INDteContract.Text = String.Empty
                ElseIf careGroup.ObjectEmbbeded.CareGroupType = 3 Then 'Particulares
                    Isinsurance = False
                    INDliContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliEntityCups.HideControl(True)
                    INDteContract.Text = String.Empty
                ElseIf careGroup.ObjectEmbbeded.CareGroupType = 4 Then 'Aseguradoras
                    Isinsurance = True
                    INDliContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliEntityCups.HideControl(False)
                    INDteContract.Text = String.Empty
                End If
            End Using
        End If
    End Sub

    Private Sub INDsleEntityCups_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntityHealth.QueryPopUp
        HealthAdministrator()
    End Sub


    ''' <summary>
    '''  esta función ajusta el origen de datos del control INDsleEntityHealth según si el paciente
    '''  tiene un seguro médico o no, cargando diferentes listas de administradores de salud en función de esa condición.
    ''' </summary>
    Private Sub HealthAdministrator()
        Using _Model As New MPatient(Me.Tag)
            If Isinsurance = True Then
                INDsleEntityHealth.Properties.DataSource = _Model.ListHealthAdministratorByType(2) 'EAS
            Else
                INDsleEntityHealth.Properties.DataSource = _Model.ListHealthAdministratorByNotType(2)
            End If
        End Using
    End Sub


    ''' <summary>
    ''' esta función intenta deducir el género del paciente en función 
    ''' de la última letra de su nombre introducido en el control INDteFName.
    ''' Si la última letra es "o", se supone que es un nombre masculino, y si es "a", se supone que es un nombre femenino.
    ''' </summary>
    Private Sub INDteFName_EditValueChanged(sender As Object, e As EventArgs) Handles INDteFName.LostFocus
        If INDteFName.Text <> String.Empty Then
            Dim Palabras As Char() = INDteFName.Text.TrimEnd.ToCharArray(0, INDteFName.Text.TrimEnd.Length)
            If Palabras.Count() > 0 Then
                Dim p As Integer = UBound(Palabras)
                Dim letra As String = Palabras(p).ToString()
                If letra.ToLower() = "o" Then
                    INDgleGender.EditValue = 1
                ElseIf letra.ToLower() = "a" Then
                    INDgleGender.EditValue = 2
                End If
            End If
        End If
    End Sub

    ' ''' <summary>
    ' ''' Evento que se dispara al presionar enter al control de popup de Anónimos
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Private Sub INDpceAnonimus_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceAnonimus.KeyDown
    '    'If e.KeyCode = System.Windows.Forms.Keys.Enter Then
    '    '    INDpceAnonimus.ShowPopup()
    '    'End If
    'End Sub

    ''' <summary>
    ''' Obtiene foco en el primer control al hacel el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAnonimus_Popup(sender As Object, e As EventArgs) Handles INDpceAnonimus.Popup
        INDgleAnonimusType.Focus()
        INDgleAnonimusType.ShowPopup()
    End Sub

    ''' <summary>
    ''' en el evento closed del tipo de identificacion anonimo procedo a abrir el popup de centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleAnonimusType_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDgleAnonimusType.Closed
        If INDgleAnonimusType.EditValue IsNot Nothing Then
            If INDlciMotherIdentification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                INDsleAtentionCenter.ShowPopup()
            End If
        End If
    End Sub

    Private Sub FrmPatient_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDbeIdentification.Focus()
    End Sub
#End Region

#Region "ICRUD"
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' esta función se utiliza para deshacer los cambios realizados en los controles
    ''' de la interfaz de usuario y preparar la barra de herramientas para la creación
    ''' de un nuevo registro y, en algunos casos, también para la búsqueda.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
    End Sub

    ''' <summary>
    ''' esta función maneja el proceso de eliminación de un registro de paciente en un sistema médico.
    ''' Verifica si se ha seleccionado un paciente válido, muestra un cuadro de diálogo de confirmación,
    ''' realiza la eliminación y maneja los resultados de la operación.
    ''' </summary>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        Dim actionResult As ActionResult
        If Patient IsNot Nothing Then
            If Patient.IPCODPACI IsNot String.Empty Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Try
                        Using Model As New MPatient(Me.Tag)
                            AsyncLoader(True)
                            actionResult = Await Model.DeletePatient(Patient.IPCODPACI)
                            AsyncLoader(False)
                            If actionResult.StateResult = True Then
                                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                                SearchMode = False
                                Deshacer()
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = actionResult.Message
                            End If

                        End Using
                    Catch ex As Exception
                        Throw ex
                        AsyncLoader(False)
                    End Try
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectPatient", NAME_MODULE)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectPatient", NAME_MODULE)
        End If
    End Sub

    ''' <summary>
    ''' esta función se utiliza para guardar los datos del paciente en la base de datos,
    ''' realizando varias validaciones y mostrando mensajes de advertencia en caso de que
    ''' falten datos o no cumplan con ciertas condiciones
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If INDgleIdentificationType IsNot Nothing AndAlso (INDgleIdentificationType.EditValue = "6" Or INDgleIdentificationType.EditValue = "7") Then
            INDlciIdentification.ShowInCustomizationForm = True
            INDlciIdentification.AllowHide = True
        End If
        If ValidateControls() = False Then
            Exit Sub
        End If

        If INDlciAbilityPay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDgleAbilityPay.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar la capacidad de pago!"
                INDgleAbilityPay.Focus()
                Exit Sub
            End If
        End If

        If INDlciAffiliateType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDgleAffiliateType.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar el tipo de afiliado!"
                INDgleAbilityPay.Focus()
                Exit Sub
            End If
        End If

        If INDgleIdentificationType.EditValue IsNot Nothing Then
            If INDgleIdentificationType.EditValue = "1" Or INDgleIdentificationType.EditValue = "6" Then
                If DateDiff(DateInterval.Year, INDteBirthDate.DateTime, Me.GetDateServer) < 18 Then
                    Mensaje(EeventViewerImages.Advertencia) = "La edad debe ser mayor o igual a 18 años"
                    INDteBirthDate.Focus()
                    Exit Sub
                End If
            ElseIf INDgleIdentificationType.EditValue = "7" Then
                If DateDiff(DateInterval.Year, INDteBirthDate.DateTime, Me.GetDateServer) >= 18 Then
                    Mensaje(EeventViewerImages.Advertencia) = "La edad debe ser menor a 18 años"
                    INDteBirthDate.Focus()
                    Exit Sub
                End If
                If INDgleAnonimusType.EditValue = "5" Then
                    If DateDiff(DateInterval.Day, INDteBirthDate.DateTime, Me.GetDateServer) > 30 Then
                        Mensaje(EeventViewerImages.Advertencia) = "La edad debe ser menor o igual a 30 dias"
                        INDteBirthDate.Focus()
                        Exit Sub
                    End If
                End If
            End If
            If INDgleIdentificationType.EditValue = "6" AndAlso INDgleAnonimusType.EditValue = "0" Then
                If DateDiff(DateInterval.Year, INDteBirthDate.DateTime, Me.GetDateServer) < 65 Then
                    Mensaje(EeventViewerImages.Advertencia) = "La edad debe ser mayor o igual a 65 años"
                    INDteBirthDate.Focus()
                    Exit Sub
                End If
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Debe Digitar El Tipo De Identificación"
            INDgleIdentificationType.Focus()
            Exit Sub
        End If

        If INDlciAnonimus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDgleAnonimusType.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe Diligenciar La Información Del Paciente Sin Identificación"
                INDpceAnonimus.ShowPopup()
                Exit Sub
            Else
                If INDgleAnonimusType.EditValue = "5" Then
                    If AnonimusRegBaby = False Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe Digitar La Identificación De La Madre y Su Respectivo Número De Hijo"
                        INDpceAnonimus.ShowPopup()
                        INDteMotherIdentification.Focus()
                        Exit Sub
                    End If
                Else
                    If AnonimusAdult = False Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe Diligenciar La Información Del Paciente Sin Identificación"
                        INDpceAnonimus.ShowPopup()
                        INDgleAnonimusType.Focus()
                        Exit Sub
                    End If
                End If
            End If
        End If
        AssigningValues()
        Try
            Using Model As New MPatient(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SavePatient(Me.Patient)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If Patient.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavePatient", NAME_MODULE), Result.ObjectEmbbeded.IPCODPACI.Trim)
                    ElseIf Patient.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("UpdatePatient", NAME_MODULE), Result.ObjectEmbbeded.IPCODPACI.Trim)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavePatient", NAME_MODULE), Result.ObjectEmbbeded.IPCODPACI.Trim)
                    End If
                    Me.Patient = Result.ObjectEmbbeded
                    'Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    SearchMode = False
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.MensajeError) = Result.Message

                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub


    ''' <summary>
    ''' esta propiedad permite que la clase que implementa la interfaz IcrudBase muestre mensajes
    ''' al usuario con diferentes tipos de iconos según la situación, como advertencias, información o errores.
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
    ''' se utiliza para preparar la interfaz y las opciones cuando se desea crear un nuevo registro de paciente.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Me.NewPatient_MS_AS()
        INDbeIdentification.Enabled = False
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
    End Sub

    ''' <summary>
    ''' permite a los usuarios buscar registros de pacientes utilizando una ventana de búsqueda,
    ''' mostrando los resultados en una lista con columnas específicas.
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        Using Model As New MPatient(Me.Tag)
            FormSearchObjects = New FrmBusqueda
            AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
            With FormSearchObjects
                .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "IPCODPACI", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                                  New ColumnInfo() With {.Caption = "Nombre", .FieldName = "IPNOMCOMP", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7)}}.ToList
                .ValorSolicitado = "IPCODPACI"
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllPatients
                BarraBotones.PrepareToolbar(eAction.OnlyFind)
                .FormParent = Me
                .ShowSearch()
            End With
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbeIdentification.Text = ReturnValue
        If INDbeIdentification.Text <> String.Empty Then
            LoadControls()
            If INDbeIdentification.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbeIdentification.Enabled = False
        End If
    End Sub

    Private Sub INDsleCityExpedition_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCityExpedition.QueryPopUp
        If DatasourceListCitys Is Nothing Then
            Load_Citys()
        End If
    End Sub

    Private Sub INDsleCityExpedition_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCityExpedition.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(513, Nothing, True)
            Load_Citys()
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbeIdentification.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
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
        'UpdateState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

#End Region

#Region "Event-Click"

    ''' <summary>
    ''' esta función agrega registros relacionados con los topes anuales de valores médicos a una estructura de datos,
    ''' realiza algunas comprobaciones de validación y actualiza la visualización de la información en una cuadrícula.
    ''' </summary>
    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        'Dim validationErrors As New StringBuilder()
        'If TopYearCopago = 0 Then
        '    validationErrors.AppendLine("El valor del tope del copago debe ser mayor a cero")
        'End If
        'If TopYearCuotaModeradora = 0 Then
        '    validationErrors.AppendLine("El valor del tope de la cuota moderadora debe ser mayor a cero")
        'End If
        'If TopYearCuotaRecuperacion = 0 Then
        '    validationErrors.AppendLine("El valor del tope de la cuota de recuperación debe ser mayor a cero")
        'End If
        'If validationErrors.Length > 0 Then
        '    Mensaje(EeventViewerImages.Advertencia) = validationErrors.ToString()
        '    Exit Sub
        'End If
        If _isEditingTopYear Then
            If ListTopAnual.Where(Function(x) Not x.Equals(CType(INDGvTopYear.GetFocusedRow(), INPACIENTTOPANU))).ToList().Find(Function(o) o.ANIO = Anio) IsNot Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Ya hay un registro creado para este año"
                Exit Sub
            End If
        Else
            If ListTopAnual.Find(Function(o) o.ANIO = Anio) IsNot Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Ya hay un registro creado para este año"
                Exit Sub
            End If
        End If
        Dim iNPACIENTOPANU As INPACIENTTOPANU
        If _isEditingTopYear Then
            iNPACIENTOPANU = CType(INDGvTopYear.GetFocusedRow(), INPACIENTTOPANU)
        Else
            iNPACIENTOPANU = New INPACIENTTOPANU()
        End If
        iNPACIENTOPANU.ANIO = Anio
        iNPACIENTOPANU.PAGADOCMO = TopYearCuotaModeradora
        iNPACIENTOPANU.PAGADOCOP = TopYearCopago
        iNPACIENTOPANU.PAGADOCRE = TopYearCuotaRecuperacion
        iNPACIENTOPANU.IPCODPACI = Patient.IPCODPACI

        Patient.INPACIENTTOPANU.Add(iNPACIENTOPANU)
        ListTopAnual = Patient.INPACIENTTOPANU.ToList()
        INDGcTopAnual.RefreshDataSource()

        _isEditingTopYear = False
        TopYearCopago = 0
        TopYearCuotaModeradora = 0
        TopYearCuotaRecuperacion = 0
        INDTxtAnio.Focus()
    End Sub

    ''' <summary>
    ''' esta función controla las acciones realizadas cuando se hacen clic en botones
    ''' o menús contextuales en una cuadrícula de datos. Dependiendo del botón o menú
    ''' contextual que se activa, puede permitir la edición de registros o la eliminación
    ''' de registros en la cuadrícula.
    ''' </summary>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag)
            Case "Edit", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
                Dim oj As INPACIENTTOPANU = CType(INDGvTopYear.GetFocusedRow(), INPACIENTTOPANU)
                Anio = oj.ANIO
                TopYearCopago = oj.PAGADOCOP
                TopYearCuotaModeradora = oj.PAGADOCMO
                TopYearCuotaRecuperacion = oj.PAGADOCRE
                _isEditingTopYear = True
                INDPceAddTopYear.ShowPopup()
            Case "Remove", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    CType(INDGvTopYear.GetFocusedRow(), INPACIENTTOPANU).MarkAsDeleted()
                    ListTopAnual = Patient.INPACIENTTOPANU.ToList()
                    INDGcTopAnual.RefreshDataSource()
                End If
        End Select
    End Sub
#End Region

End Class