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
''' Clase que contiene la vista del funcional ingreso
''' </summary>
''' <remarks></remarks>
Public Class FrmAdmissions
    Implements IAdmissions

#Region "Builder"

    Public ctrTmp As CtrInfoRequirements

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ctrTmp = New CtrInfoRequirements()
        ctrTmp.SetTotalValues(AddressOf getValues)
        ctrTmp.RefreshInfo()
        ctrTmp.PopupContainerControlEntity = INDpopupRequirement
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    Private Function getValues() As Tuple(Of String, Integer)
        Return New Tuple(Of String, Integer)(RequirementTemplateName, CountRequirement)
    End Function

#End Region

#Region "Global Variables"

    ''' <summary>
    ''' Variable que contiene el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PAdmissions

    ''' <summary>
    ''' Variable que contiene el modelo
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As MAdmissions

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

    ''' <summary>
    ''' Entidad Paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim Admissions As ADINGRESO

    ''' <summary>
    ''' Variable que contiene el paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim Patient As INPACIENT

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Crystal"

    ''' <summary>
    ''' Establece si permite asignar ingreos hospitalarios
    ''' </summary>
    ''' <remarks></remarks>
    Dim INDInterfazHospitalizacion As Boolean

    ''' <summary>
    ''' Establece los días maximos de vigencia de los ingresos
    ''' </summary>
    ''' <remarks></remarks>
    Dim INDVigenciaIngresos As Integer

    ''' <summary>
    ''' Entidad del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim GENCONCENTITYPatient As Integer

    ''' <summary>
    ''' Grupo atención del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim GENCAREGROUPPatient As Integer?

    ''' <summary>
    ''' Descripcion de grupo de atención del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim GENCAREGROUPPatientDesc As String

    ''' <summary>
    ''' Establece el nombre de la plantilla de requerimientos
    ''' </summary>
    ''' <remarks></remarks>
    Dim RequirementTemplateName As String

    ''' <summary>
    ''' Establece la cantidad de requerimientos
    ''' </summary>
    ''' <remarks></remarks>
    Dim CountRequirement As Integer

    ''' <summary>
    ''' Listado de requerimientos
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListRequirement As List(Of Requirement)

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

            'INDtePatientName.Enabled = Not value
            'INDsleAdmission.Enabled = value
            INDteControl.Enabled = value
            INDgleAdmissionType.Enabled = value
            INDdeAdmissionDate.Enabled = value

            INDgleRiskType.Enabled = value
            INDgleAccessed.Enabled = value
            INDgleCause.Enabled = value
            INDsleCenter.Enabled = value
            INDsleFunctionalUnit.Enabled = value
            INDmeAuthorizationNumber.Enabled = value
            INDmeObservations.Enabled = value
            INDsleCareGroup.Enabled = value
            INDteContract.Enabled = value
            INDsleEntityCups.Enabled = value
            INDteThirdParty.Enabled = value
            INDgleLiquidation.Enabled = value
            INDdeHospDate.Enabled = value
            INDsleBed.Enabled = value
            INDteRemissionNumber.Enabled = value
            INDsleTown.Enabled = value
            INDmeRemissionAuthorizationNumber.Enabled = value
            INDmeRemissionObservation.Enabled = value
            INDdeRemissionDate.Enabled = value
            INDsleIPS.Enabled = value
            INDsleTCAdmission.Enabled = value
            INDteSentValue.Enabled = value
            INDsleMinWage.Enabled = value
            INDpceTAVictims.Enabled = value

            INDtePopUpTAPersonName.Enabled = value
            INDtePopUpTAmeObservations.Enabled = value
            INDtePopUpTAteDate.Enabled = value
            INDtePopUpTAteIdentification.Enabled = value
            INDtePopUpTAteExpedition.Enabled = value
            INDtePopUpTAtedHour.Enabled = value
            INDtePopUpTAtePhone.Enabled = value
            INDtePopUpTAmeObservations.Enabled = value
            If value = False Then
                INDbeIdentification.Focus()
            Else
                'INDsleAdmission.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [state].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [state]; otherwise, <c>false</c>.
    ''' </value>
    Public Property State As String
        Get
            Select Case BarraBotones.StatusRecord
                Case Is = EAdmissionStatus.Unconfirmed
                    Return " "
                Case Is = EAdmissionStatus.Confirmed
                    Return "F"
                Case Is = EAdmissionStatus.Canceled
                    Return "A"
                Case Is = EAdmissionStatus.Closed
                    Return "C"
                Case Else
                    Return Nothing
            End Select

        End Get
        Set(value As String)
            Select Case value
                Case Is = " "
                    Me.BarraBotones.StatusRecord = EAdmissionStatus.Unconfirmed
                Case Is = "F"
                    Me.BarraBotones.StatusRecord = EAdmissionStatus.Confirmed
                Case Is = "A"
                    Me.BarraBotones.StatusRecord = EAdmissionStatus.Canceled
                Case Is = "C"
                    Me.BarraBotones.StatusRecord = EAdmissionStatus.Closed
            End Select
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece si es 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EAPBType As ECareGroupType = Nothing
#End Region

#Region "Tuples and xpo"

    ''' <summary>
    ''' listado de los tipos de riesgo
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listRiskType As List(Of Tuple(Of String, String))
    Sub SetRiskRype()
        If _listRiskType Is Nothing Then
            _listRiskType = New List(Of Tuple(Of String, String))
            _listRiskType.Add(New Tuple(Of String, String)("2", "Accidente de Tránsito"))
            _listRiskType.Add(New Tuple(Of String, String)("3", "Catástrofe"))
            _listRiskType.Add(New Tuple(Of String, String)("1", "Enfermedad General y Maternidad"))
            _listRiskType.Add(New Tuple(Of String, String)("5", "Accidente de Trabajo"))
            _listRiskType.Add(New Tuple(Of String, String)("6", "Enfermedad Profesional"))
            _listRiskType.Add(New Tuple(Of String, String)("7", "Atención Inicial de Urgencias"))
            _listRiskType.Add(New Tuple(Of String, String)("8", "Otro Tipo de Accidente"))
            _listRiskType.Add(New Tuple(Of String, String)("9", "Lesión Por Agresión"))
            _listRiskType.Add(New Tuple(Of String, String)("10", "Lesión AutoInfligida"))
            _listRiskType.Add(New Tuple(Of String, String)("11", "Maltrato Fisico"))
            _listRiskType.Add(New Tuple(Of String, String)("12", "Promoción y Prevención"))
            _listRiskType.Add(New Tuple(Of String, String)("13", "Otro"))
            _listRiskType.Add(New Tuple(Of String, String)("14", "Accidente Rábico"))
            _listRiskType.Add(New Tuple(Of String, String)("15", "Accidente Ofídico"))
            _listRiskType.Add(New Tuple(Of String, String)("16", "Sopecha de Abuso Sexual"))
            _listRiskType.Add(New Tuple(Of String, String)("17", "Sopecha de Violencia Sexual"))
            _listRiskType.Add(New Tuple(Of String, String)("18", "Sopecha de Maltrato Emocional"))
            INDgleRiskType.Properties.DataSource = _listRiskType
            INDgleRiskType.Properties.PopupFormSize = New Size(500, 350)
        End If
    End Sub

    ''' <summary>
    ''' listado de los tipos de ingresa por
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listAccessed As List(Of Tuple(Of Integer, String))
    Sub SetAccessed()
        If _listAccessed Is Nothing Then
            _listAccessed = New List(Of Tuple(Of Integer, String))
            _listAccessed.Add(New Tuple(Of Integer, String)(1, "Urgencias"))
            _listAccessed.Add(New Tuple(Of Integer, String)(2, "Consulta Externa"))
            _listAccessed.Add(New Tuple(Of Integer, String)(3, "Nacido IPS"))
            _listAccessed.Add(New Tuple(Of Integer, String)(4, "Remitido"))
            _listAccessed.Add(New Tuple(Of Integer, String)(5, "Hosp. Urgencias"))
            INDgleAccessed.Properties.DataSource = _listAccessed
            INDgleAccessed.Properties.PopupFormSize = New Size(500, 350)
        End If
    End Sub

    ''' <summary>
    ''' listado de los tipos de ingresa por
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listLiquidation As List(Of Tuple(Of Integer, String))
    Sub SetLiquidation()
        If _listLiquidation Is Nothing Then
            _listLiquidation = New List(Of Tuple(Of Integer, String))
            _listLiquidation.Add(New Tuple(Of Integer, String)(1, "Copago"))
            _listLiquidation.Add(New Tuple(Of Integer, String)(2, "C.Moderadora"))
            _listLiquidation.Add(New Tuple(Of Integer, String)(4, "CP \ CM"))
            _listLiquidation.Add(New Tuple(Of Integer, String)(3, "No Aplica"))
            INDgleLiquidation.Properties.DataSource = _listLiquidation
            INDgleLiquidation.Properties.PopupFormSize = New Size(500, 350)
        End If
    End Sub

    ''' <summary>
    ''' listado de los tipos de ingresa por
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listAdmissionType As List(Of Tuple(Of Integer, String))
    Sub SetAdmissionType()
        If _listAdmissionType Is Nothing Then
            _listAdmissionType = New List(Of Tuple(Of Integer, String))
            _listAdmissionType.Add(New Tuple(Of Integer, String)(1, "Ambulatorio"))
            _listAdmissionType.Add(New Tuple(Of Integer, String)(2, "Hospitalario"))
            INDgleAdmissionType.Properties.DataSource = _listAdmissionType
            INDgleAdmissionType.Properties.PopupFormSize = New Size(500, 350)
        End If
    End Sub

    ''' <summary>
    ''' listado de los tipos de ingresa por
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listCause As List(Of Tuple(Of Integer, String))
    Sub SetCause()
        If _listCause Is Nothing Then
            _listCause = New List(Of Tuple(Of Integer, String))
            _listCause.Add(New Tuple(Of Integer, String)(1, "Heridos en Combate"))
            _listCause.Add(New Tuple(Of Integer, String)(2, "Enfermedad Profesional"))
            _listCause.Add(New Tuple(Of Integer, String)(3, "Enfermedad General Adulto"))
            _listCause.Add(New Tuple(Of Integer, String)(4, "Enfermedad General Pediatría"))
            _listCause.Add(New Tuple(Of Integer, String)(5, "Odontología"))
            _listCause.Add(New Tuple(Of Integer, String)(6, "Accidente de Tránsito"))
            _listCause.Add(New Tuple(Of Integer, String)(7, "Evento Catastrófico"))
            _listCause.Add(New Tuple(Of Integer, String)(8, "Quemados"))
            _listCause.Add(New Tuple(Of Integer, String)(9, "Maternidad"))
            _listCause.Add(New Tuple(Of Integer, String)(10, "Accidente Laboral"))
            _listCause.Add(New Tuple(Of Integer, String)(11, "Cirugía Programada"))
            INDgleCause.Properties.DataSource = _listCause
            INDgleCause.Properties.PopupFormSize = New Size(500, 350)
        End If
    End Sub

    ''' <summary>
    ''' Carga los centros de atencion XPO
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadCenters()
        Using _mPatient As New MPatient(Me.Tag)
            INDsleCenter.Properties.DataSource = _mPatient.ListCenter
        End Using
    End Sub

    ''' <summary>
    ''' Carga las unidades funcionales
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadFunctionalUnit()
        Using Model As New MAdmissions(Me.Tag)
            If ObjUSerCrystal IsNot Nothing Then
                INDsleFunctionalUnit.Properties.DataSource = Model.ListViewListFuncionalUnitAuthorization(ObjUSerCrystal.CODGRUPOU, indigo.AuditMessageWcf.CodeUser)  'Model.ListAllFunctionalUnit
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se encontro usuario con codigo en el HIS " & indigo.AuditMessageWcf.CodeUser
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Carga los grupos de atencion
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadCareGroup()
        Using Model As New MAdmissions(Me.Tag)
            INDsleCareGroup.Properties.DataSource = Model.ListCareGroup
        End Using
    End Sub

    ''' <summary>
    ''' Carga los municipios
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadTown()
        Using Model As New MAdmissions(Me.Tag)
            INDsleTown.Properties.DataSource = Model.ListAllTown
            INDsleTown.Properties.PopupFormSize = New Size(700, 300)
        End Using
    End Sub

    ''' <summary>
    ''' Carga las IPS
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadIPS()
        Using Model As New MAdmissions(Me.Tag)
            INDsleIPS.Properties.DataSource = Model.ListAllIPS
        End Using
    End Sub

    ''' <summary>
    ''' Carga las IPS
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadMinWage()
        Using Model As New MAdmissions(Me.Tag)
            INDsleMinWage.Properties.DataSource = Model.ListAllMinWage
        End Using
    End Sub

    ''' <summary>
    ''' Carga las camas con xpo
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadBeds()
        Using Model As New MAdmissions(Me.Tag)
            INDsleBed.Properties.DataSource = Nothing
            INDsleBed.Properties.DataSource = Model.ListBeds(INDsleCenter.EditValue, INDsleFunctionalUnit.EditValue)
        End Using
    End Sub

    ''' <summary>
    ''' Usuario de Crystal
    ''' </summary>
    ''' <remarks></remarks>
    Dim ObjUSerCrystal As SEGusuaru

#End Region

#Region "Methods"
    ''' <summary>
    ''' Loads the status.
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = EAdmissionStatus.Unconfirmed, .StatusName = ResourceManager.GetString("StateUnconfirmedAd", NAME_MODULE), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = EAdmissionStatus.Confirmed, .StatusName = ResourceManager.GetString("StateConfirmedAd", NAME_MODULE), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = EAdmissionStatus.Canceled, .StatusName = ResourceManager.GetString("StatusAnulateAd", NAME_MODULE), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = EAdmissionStatus.Closed, .StatusName = ResourceManager.GetString("StatusCloseAd", NAME_MODULE), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Async Function CleanControls() As Task
        INDlcAdmissions.BeginUpdate()
        ActionsOnControls = False
        INDsleAdmission.Enabled = True
        Me.Admissions = Nothing
        Me.Patient = Nothing
        INDbeIdentification.Text = String.Empty
        INDtePatientName.Text = String.Empty
        INDsleAdmission.Text = String.Empty
        INDsleAdmission.EditValue = Nothing
        INDsleAdmission.Properties.ReadOnly = True
        INDteControl.Text = String.Empty
        INDgleAdmissionType.EditValue = Nothing
        INDdeAdmissionDate.EditValue = Nothing

        INDgleRiskType.EditValue = Nothing
        INDgleAccessed.EditValue = Nothing
        INDgleCause.EditValue = Nothing
        'INDsleCenter.Text = String.Empty
        INDsleCenter.EditValue = Nothing
        INDsleCenter.Properties.NullText = String.Empty
        'INDsleFunctionalUnit.Text = String.Empty
        INDsleFunctionalUnit.EditValue = Nothing
        INDsleFunctionalUnit.Properties.NullText = String.Empty
        INDmeAuthorizationNumber.EditValue = Nothing
        INDmeObservations.Text = String.Empty
        INDsleCareGroup.Properties.NullText = String.Empty
        INDsleCareGroup.EditValue = Nothing
        INDsleEntityCups.Properties.NullText = String.Empty
        INDsleEntityCups.EditValue = Nothing
        INDgleLiquidation.Text = String.Empty
        INDgleLiquidation.EditValue = Nothing
        INDdeHospDate.EditValue = Nothing
        INDsleBed.Text = String.Empty
        INDsleBed.EditValue = Nothing
        INDsleBed.Properties.NullText = String.Empty
        INDteRemissionNumber.Text = String.Empty
        INDsleTown.Properties.NullText = String.Empty
        INDsleTown.EditValue = Nothing
        INDmeRemissionAuthorizationNumber.Text = String.Empty
        INDmeRemissionObservation.Text = String.Empty
        INDdeRemissionDate.EditValue = Nothing
        INDsleIPS.Properties.NullText = String.Empty
        INDsleIPS.EditValue = Nothing
        INDsleTCAdmission.EditValue = Nothing
        INDteSentValue.Text = Nothing
        INDteSentValue.EditValue = Nothing
        INDsleMinWage.EditValue = Nothing
        INDsleMinWage.Properties.NullText = String.Empty
        INDpceTAVictims.EditValue = Nothing

        INDtePopUpTAPersonName.Text = String.Empty
        INDtePopUpTAmeObservations.Text = String.Empty
        INDtePopUpTAteDate.EditValue = Nothing
        INDtePopUpTAteIdentification.Text = String.Empty
        INDtePopUpTAteExpedition.Text = String.Empty
        INDtePopUpTAtedHour.EditValue = Nothing
        INDtePopUpTAtePhone.Text = String.Empty
        INDtePopUpTAmeObservations.Text = String.Empty

        GENCONCENTITYPatient = 0
        GENCAREGROUPPatient = 0
        GENCAREGROUPPatientDesc = ""

        ListRequirement = Nothing
        INDgcRequeriments.DataSource = Nothing
        RequirementTemplateName = String.Empty
        CountRequirement = 0
        ctrTmp.RefreshInfo()

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.CleanAuditBasic()
        Await DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        INDlcAdmissions.EndUpdate()
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MAdmissions(Me.Tag)
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que inicializa un nuevo paciente sin numero de identificación
    ''' </summary>
    ''' <remarks></remarks>
    Function NewAdmission() As Task
        Admissions = New ADINGRESO
        Admissions.INPACIENT = Patient
        Admissions.TIPOINGRE = "1"
        INDdeAdmissionDate.EditValue = GetDateServer()
        INDgleAdmissionType.EditValue = Admissions.TIPOINGRE
        Select Case fncTipoLiquidacionTipoPaciente(Patient.IPTIPOPAC, Patient.IPTIPOAFI, Patient.CAPACIPAG)
            Case 1
                INDgleLiquidation.EditValue = 1
            Case 2
                INDgleLiquidation.EditValue = 2
            Case 4
                INDgleLiquidation.EditValue = 4
            Case Else
                INDgleLiquidation.EditValue = 3
        End Select
        INDsleCareGroup.EditValue = If(GENCAREGROUPPatient > 0, GENCAREGROUPPatient, Nothing)
        INDsleEntityCups.EditValue = If(GENCONCENTITYPatient > 0, GENCONCENTITYPatient, Nothing)
        If INDsleCareGroup.Properties.DataSource Is Nothing Then
            INDsleCareGroup.Properties.NullText = GENCAREGROUPPatientDesc
        End If
        ActionsOnControls = True
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        INDgleRiskType.Focus()
        INDsleAdmission.Enabled = False
        'Me.BarraBotones.StatusRecordVisible = True
    End Function

    Function getStringTrimField(field As String) As String
        Return If(field IsNot Nothing, field.Trim, "")
    End Function

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Sub LoadControls()
        loadAdmission = True
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        Using Model As New MAdmissions(Me.Tag)
            AsyncLoader(True)
            Dim resultOperation As ActionResult(Of ADINGRESO) = Await Model.GetAdmissionsByCode(INDsleAdmission.EditValue)
            AsyncLoader(False)
            Admissions = resultOperation.ObjectEmbbeded
            If resultOperation.StateResult Then
                If resultOperation.ObjectEmbbeded IsNot Nothing AndAlso resultOperation.ObjectEmbbeded.NUMINGRES IsNot Nothing AndAlso resultOperation.ObjectEmbbeded.NUMINGRES.ToString IsNot String.Empty Then
                    Me.BarraBotones.StatusRecordVisible = True
                    'Dim result = Await Model.GetBlockRecord(Me.Tag, resultOperation.ObjectEmbbeded.IPCODPACI)
                    With resultOperation.ObjectEmbbeded
                        If .IESTADOIN = " " Then
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                            If Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Anular)) Then
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                            End If
                            ActionsOnControls = True
                        End If

                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), getStringTrimField(.CODUSUCRE))
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .FECREGCRE)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), getStringTrimField(.CODUSUMOD))
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .FECREGMOD)
                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)

                        INDteControl.Text = String.Empty
                        INDgleAdmissionType.EditValue = .TIPOINGRE
                        INDdeAdmissionDate.EditValue = .IFECHAING

                        INDgleRiskType.EditValue = .ITIPORIES
                        INDgleAccessed.EditValue = .IINGREPOR
                        INDgleCause.EditValue = .ICAUSAING
                        INDsleCenter.Properties.NullText = .ExtCenter
                        INDsleCenter.EditValue = .CODCENATE
                        INDsleFunctionalUnit.Properties.NullText = .ExtFunctionalUnit
                        INDsleFunctionalUnit.EditValue = .UFUCODIGO.ToString.Trim()
                        INDmeAuthorizationNumber.EditValue = .IAUTORIZA
                        INDmeObservations.Text = getStringTrimField(.IOBSERVAC)
                        INDsleCareGroup.Properties.NullText = .ExtCareGroup
                        INDsleCareGroup.EditValue = .GENCAREGROUP
                        INDsleEntityCups.Properties.NullText = .ExtCupsEntity
                        INDsleEntityCups.EditValue = .GENCONENTITY

                        'Select Case fncTipoLiquidacionTipoPaciente(Patient.IPTIPOPAC, Patient.IPTIPOAFI, Patient.CAPACIPAG)
                        '    Case 1
                        '        INDgleLiquidation.EditValue = 1
                        '    Case 2
                        '        INDgleLiquidation.EditValue = 2
                        '    Case 4
                        '        INDgleLiquidation.EditValue = 4
                        '    Case Else
                        '        INDgleLiquidation.EditValue = 3
                        'End Select

                        INDgleLiquidation.EditValue = .ILIQUIDAC


                        If INDgleAdmissionType.EditValue = "2" Then
                            If .FECHOSPIT IsNot Nothing Then INDdeHospDate.EditValue = .FECHOSPIT
                            If .CODICAMHO IsNot Nothing Then INDsleBed.EditValue = .CODICAMHO
                            INDsleBed.Properties.NullText = .ExtHospitalizationBed
                        End If

                        If .IINGREPOR = 4 Then
                            INDteRemissionNumber.Text = getStringTrimField(.INUMERORE)
                            INDsleTown.Properties.NullText = .ExtTown
                            INDsleTown.EditValue = .DEPMUNCOD
                            INDmeRemissionAuthorizationNumber.Text = .IAUTORREM
                            INDmeRemissionObservation.Text = getStringTrimField(.OBSERAREM)
                            INDdeRemissionDate.EditValue = .IFECHAREM
                            INDsleIPS.Properties.NullText = .ExtIPS
                            INDsleIPS.EditValue = .AIPSREMIS
                        End If
                        State = .IESTADOIN

                        If .ITIPORIES = "2" Then 'Accidente de transito
                            INDtePopUpTAPersonName.Text = getStringTrimField(.IPRNOMBRE)
                            INDtePopUpTAteDate.EditValue = .FECACTRAN
                            INDtePopUpTAteIdentification.Text = getStringTrimField(.IPCODACTR)
                            INDtePopUpTAteExpedition.Text = getStringTrimField(.IPEXPEDIC)
                            If .HORACIDEN IsNot Nothing AndAlso .HORACIDEN.ToString.Trim IsNot String.Empty Then
                                Dim _hour As String() = Admissions.HORACIDEN.ToString.Trim.Split(New [Char]() {":"})
                                INDtePopUpTAtedHour.EditValue = New DateTime(2015, 1, 1, _hour(0), _hour(1), 0)
                            End If
                            INDtePopUpTAtePhone.Text = getStringTrimField(Admissions.IPTELEFON)
                            INDtePopUpTAmeObservations.Text = getStringTrimField(Admissions.OBSERACIT)
                            If .ISOATVALO IsNot Nothing Then
                                INDteSentValue.Text = getStringTrimField(.ISOATVALO)
                            End If
                            INDsleMinWage.Properties.NullText = .ExtMinWage
                            INDsleMinWage.EditValue = Admissions.ISALCODIG
                            INDsleTCAdmission.Properties.NullText = .IINGRESOA
                        End If
                    End With

                    'Me.GetDocumentIndexed(Me.Tag & "_" & resultOperation.ObjectEmbbeded.IPCODPACI)
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
                    INDbeIdentification.Enabled = False
                    INDsleAdmission.Enabled = False
                    INDteControl.Focus()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "El Ingreso no Existe!"
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = resultOperation.Message
            End If
        End Using
        loadAdmission = False
    End Sub

    ''' <summary>
    ''' Método para asignar valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Sub AssigningValues()
        With Admissions
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .ICONTROLI = INDteControl.Text
            .TIPOINGRE = INDgleAdmissionType.EditValue
            .IFECHAING = INDdeAdmissionDate.EditValue

            .ITIPORIES = INDgleRiskType.EditValue
            .IINGREPOR = INDgleAccessed.EditValue
            .ICAUSAING = INDgleCause.EditValue
            .CODCENATE = INDsleCenter.EditValue
            .UFUCODIGO = INDsleFunctionalUnit.EditValue
            .UFUACTPAC = INDsleFunctionalUnit.EditValue
            .IAUTORIZA = INDmeAuthorizationNumber.EditValue
            .IOBSERVAC = INDmeObservations.Text
            .GENCAREGROUP = INDsleCareGroup.EditValue
            .ILIQUIDAC = INDgleLiquidation.EditValue
            .IESTADOIN = Me.State

            'auditoria
            If .ChangeTracker.State = ObjectState.Added Then
                .CODUSUCRE = indigo.UserIndigoId
                .FECHOSPIT = INDdeHospDate.EditValue
            Else
                .CODUSUMOD = indigo.UserIndigoId
            End If

            If INDgleAdmissionType.EditValue = "2" Then
                .CODICAMHO = INDsleBed.EditValue
            End If

            If .ITIPORIES = "2" Then
                .IPRNOMBRE = INDtePopUpTAPersonName.Text
                .FECACTRAN = INDtePopUpTAteDate.EditValue
                .IPCODACTR = INDtePopUpTAteIdentification.Text
                .IPEXPEDIC = INDtePopUpTAteExpedition.Text
                If INDtePopUpTAtedHour.EditValue IsNot Nothing Then
                    'Dim fmt As String = "HH:mm"
                    '.HORACIDEN = (INDtePopUpTAtedHour.EditValue).ToString(fmt)
                    .HORACIDEN = CType(INDtePopUpTAtedHour.EditValue, Date).Hour & ":" & CType(INDtePopUpTAtedHour.EditValue, Date).Minute
                End If
                .IPTELEFON = INDtePopUpTAtePhone.Text
                .OBSERACIT = INDtePopUpTAmeObservations.Text
                .ISOATVALO = CDec(INDteSentValue.EditValue)
                .ISALCODIG = INDsleMinWage.EditValue
            End If

            If INDlgRemission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .INUMERORE = INDteRemissionNumber.Text
                .DEPMUNCOD = INDsleTown.EditValue
                .IAUTORREM = INDmeRemissionAuthorizationNumber.Text
                .OBSERAREM = INDmeRemissionObservation.Text
                .IFECHAREM = INDdeRemissionDate.EditValue
                .AIPSREMIS = INDsleIPS.EditValue
                .IINGRESOA = INDsleTCAdmission.EditValue

            End If
        End With
    End Sub

    ''' <summary>
    ''' Define si se cobra cuota o copago
    ''' </summary>
    ''' <param name="TipoPaciente">tipo paciente.</param>
    ''' <param name="TipoAfiliado">tipo afiliado.</param>
    ''' <returns></returns>
    Shared Function fncTipoLiquidacionTipoPaciente(ByVal TipoPaciente As String, ByVal TipoAfiliado As String, CapacidadPago As String) As Integer

        'marco la liquidacion segun aplique
        Select Case TipoPaciente
            Case "1" 'contributivo
                If TipoAfiliado = "2" Then 'beneficiario Then
                    Return 4
                End If
                If TipoAfiliado = "1" Then 'cotizante
                    Return 2
                End If
            Case "1", "6" 'contributivo y desplazado contributivo
                If TipoAfiliado = "1" Then 'cotizante
                    Return 2
                End If
                If TipoAfiliado = "2" Then 'beneficiario
                    Return 2
                End If
                If TipoAfiliado = "3" Then 'adicional
                    Return 2
                End If
                If TipoAfiliado = "4" Then 'jubilado
                    Return 3
                End If
                If TipoAfiliado = "5" Then 'pensionado
                    Return 3
                End If
            Case "2" 'subsidiado
                Return 1
            Case "3" 'vinculado
                If CapacidadPago = "1" OrElse CapacidadPago = "3" Then
                    Return 3
                Else
                    Return 1
                End If
            Case "8" 'desplazado no asegurado
                Return 1
        End Select
        'los demas tipos de pacientes/afiliados no aplica = N/A
        Return 3
    End Function

    Async Sub LoadPatient()
        Using m As New MPatient(Me.Tag)
            AsyncLoader(True)
            Dim result As ActionResult(Of INPACIENT) = Await m.GetPacientByIdentification(INDbeIdentification.Text)
            AsyncLoader(False)
            If result.StateResult = True Then
                Patient = result.ObjectEmbbeded
                If Patient IsNot Nothing AndAlso Patient.IPCODPACI IsNot Nothing AndAlso Patient.IPCODPACI IsNot String.Empty Then
                    INDtePatientName.Text = getStringTrimField(Patient.IPNOMCOMP)
                    GENCAREGROUPPatient = If(Patient.GENCAREGROUP IsNot Nothing, Patient.GENCAREGROUP, 0)
                    GENCONCENTITYPatient = If(Patient.GENCONENTITY IsNot Nothing, Patient.GENCONENTITY, 0)
                    GENCAREGROUPPatientDesc = If(Patient.CareGroupDesc IsNot Nothing, Patient.CareGroupDesc, "")
                    INDsleAdmission.Properties.ReadOnly = False
                    Using Model As New MAdmissions(Me.Tag)
                        Dim res = Await Model.GetAdmissionsByPatient(INDbeIdentification.Text)
                        INDsleAdmission.Properties.DataSource = res.ObjectEmbbeded
                        INDsleAdmission.Properties.PopupFormSize = New Size(700, 300)
                        INDsleAdmission.Focus()
                        INDbeIdentification.Enabled = False
                    End Using
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = "El paciente con código " & INDbeIdentification.Text & " no existe!"
                End If
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = result.Message
            End If

        End Using
    End Sub

#End Region

#Region "Events Handler"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        Presenter = Nothing
        Model = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
        SearchMode = Nothing
        Admissions = Nothing
        Patient = Nothing
        INDVigenciaIngresos = Nothing
        GENCONCENTITYPatient = Nothing
        GENCAREGROUPPatient = Nothing
        GENCAREGROUPPatientDesc = Nothing
        RequirementTemplateName = Nothing
        CountRequirement = Nothing
        ListRequirement = Nothing
    End Sub


    ''' <summary>
    ''' Evento cuando se presiona una tecla en el código del paciente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbeIdentification_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbeIdentification.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If String.IsNullOrEmpty(INDbeIdentification.Text) = False Then
                LoadPatient()
            Else
                Mensaje(EeventViewerImages.Advertencia) = "Digite el Número de Identificación!"
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Cuando se da enter en el control para cargar el ingreso o crear uno nuevo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAdmission_KeyDown(sender As Object, e As KeyEventArgs) Handles INDsleAdmission.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If String.IsNullOrEmpty(INDsleAdmission.Text.ToString.Trim) Then  'nuevo ingreso
                NewAdmission()
            Else ' carga un ingreso
                LoadControls()
            End If
        End If
    End Sub
#End Region

#Region "ICRUD"
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub



    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
    End Sub

    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        'Dim actionResult As ActionResult
        'If Patient IsNot Nothing Then
        '    If Patient.IPCODPACI IsNot String.Empty Then
        '        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '            Using Model As New MPatient(Me.Tag)
        '                actionResult = Await Me.RunAsyncOperation(Model.DeletePatient(Patient.IPCODPACI))
        '                If actionResult.StateResult = True Then
        '                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        '                    SearchMode = False
        '                    Deshacer()
        '                Else
        '                    Mensaje(EeventViewerImages.Advertencia) = actionResult.Message
        '                End If

        '            End Using
        '        End If
        '    Else
        '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectPatient", NAME_MODULE)
        '    End If
        'Else
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectPatient", NAME_MODULE)
        'End If
    End Sub

    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        Using Model As New MAdmissions(Me.Tag.ToString())
            'If INDlciTrafficAccident.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            '    If INDsleMinWage.EditValue Is Nothing Then
            '        Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar salario mínimo!"
            '        INDsleMinWage.Focus()
            '        Exit Sub
            '    End If
            'End If

            'If INDlgRemission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            '    If INDsleIPS.EditValue Is Nothing Then
            '        Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar IPS!"
            '        INDsleMinWage.Focus()
            '        Exit Sub
            '    End If
            'End If

            'fecha ingreso no puede ser mayot a la actual
            Dim FechaActual As Date = GetServerDate()
            If Date.Compare(INDdeAdmissionDate.DateTime, FechaActual) > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La Fecha del Ingreso no puede ser Mayor a La Fecha Actual"
                INDdeAdmissionDate.Focus()
                Exit Sub
            End If

            If INDVigenciaIngresos > 0 Then
                If Date.Compare(INDdeAdmissionDate.DateTime, FechaActual.AddDays(-INDVigenciaIngresos)) < 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "La Fecha del Ingreso no puede ser menor a La de Vigencia de los Ingresos " & FechaActual.AddDays(-INDVigenciaIngresos)
                    INDdeAdmissionDate.Focus()
                    Exit Sub
                End If
            End If

            'valido fecha del ingreso con respecto al triage
            Dim dateTriage As DateTime = Await Model.GetDateTriage(INDbeIdentification.Text)
            If Object.Equals(dateTriage, Nothing) = False Then
                If DateTime.Compare(INDdeAdmissionDate.DateTime, dateTriage) < 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "La Fecha de Ingreso no Puede ser Menor a la Fecha del Triage: " & dateTriage.ToString
                    INDdeAdmissionDate.Focus()
                    Exit Sub
                End If
            End If

            'valido si es hospitalizacion
            If INDInterfazHospitalizacion = True AndAlso INDgleAdmissionType.EditValue = "2" Then
                'si hay fecha
                If INDdeHospDate.DateTime = Date.MinValue Then
                    Mensaje(EeventViewerImages.Advertencia) = "Debe Especificar la Fecha de Hospitalización"
                    INDdeHospDate.Focus()
                    Exit Sub
                End If
                'la fecha de hospitalizacion no puede ser mayor a la actual
                If Date.Compare(INDdeHospDate.DateTime, FechaActual) > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "La Fecha de Hospitalización no puede ser Mayor a la Actual"
                    INDdeHospDate.Focus()
                    Exit Sub
                End If
                'la fecha de hospitalizacion no puede ser menor a la de ingreso
                If Date.Compare(String.Format(INDdeAdmissionDate.DateTime, "dd/MM/yyyy HH:mm:ss"), String.Format(INDdeHospDate.DateTime, "dd/MM/yyyy HH:mm:ss")) > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "La Fecha de Ingreso no puede ser Mayor a la de Hospitalización"
                    INDdeHospDate.Focus()
                    Exit Sub
                End If
                If INDsleBed.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "Debe especificar la cama!"
                    INDsleBed.Focus()
                    Exit Sub
                End If
            End If

            AssigningValues()

            Try
                AsyncLoader(True)
                Dim Result = Await Model.SaveAdmission(Me.Admissions)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If Patient.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAdmission", NAME_MODULE), Result.ObjectEmbbeded.NUMINGRES.Trim)
                    ElseIf Patient.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("UpdateAdmission", NAME_MODULE), Result.ObjectEmbbeded.NUMINGRES.Trim)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAdmission", NAME_MODULE), Result.ObjectEmbbeded.NUMINGRES.Trim)
                    End If
                    Me.Admissions = Result.ObjectEmbbeded
                    'Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    SearchMode = False
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.MensajeError) = Result.Message

                End If
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
            End Try
        End Using
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

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

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        If Patient IsNot Nothing AndAlso Patient.IPCODPACI IsNot Nothing AndAlso Patient.IPCODPACI.ToString.Trim IsNot String.Empty Then
            Me.NewAdmission()
            INDbeIdentification.Enabled = False
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Debe cargar primero un paciente!"
            'Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        Using Model As New MAdmissions(Me.Tag)
            FormSearchObjects = New FrmBusqueda
            AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
            With FormSearchObjects
                .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "IPCODPACI", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                                  New ColumnInfo() With {.Caption = "Nombre", .FieldName = "IPNOMCOMP", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7)}}.ToList
                .ValorSolicitado = "IPCODPACI"
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllPatients
                BarraBotones.PrepareToolbar(eAction.OnlyFind)
                .FormParent = Me
                If SessionValues.Instance.UserViewMode = True And Me.ViewModeEditHold = False Then 'abre completo el form de busqueda
                    Me.Text = "Ingresos (Busqueda de pacientes)"
                End If
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
        Me.Text = "Ingresos"
        DeleteBlockedRecord()
        INDbeIdentification.Text = ReturnValue
        If INDbeIdentification.Text <> String.Empty Then
            LoadPatient()
            If INDbeIdentification.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbeIdentification.Enabled = False
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
        Me.Text = "Ingresos"
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
        'Deshacer()
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

#Region "Load"
    ''' <summary>
    ''' Load Formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmAdmissions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcAdmissions, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        'Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PAdmissions(Me)
        INDsleCenter.Properties.Buttons(1).Visible = False
        Me.SetAccessed()
        Me.SetAdmissionType()
        Me.SetCause()
        Me.SetLiquidation()
        Me.SetRiskRype()
        IndigoGridControl1.RefreshGrid(INDgcRequeriments)
        INDsleAdmission.Properties.View.Columns.Item(1).OptionsColumn.AllowEdit = True
        If FormSearchObjects Is Nothing Then
            SearchMode = False
        End If
        LoadStatus()
        Deshacer()
        Using Model As New MAdmissions(Me.Tag)
            ObjUSerCrystal = Await Model.GetUsuarioCrystal(indigo.AuditMessageWcf.CodeUser)
        End Using
    End Sub
#End Region

    ''' <summary>
    ''' controla si se esta cargado el paciente para no modificar la causa que trae
    ''' </summary>
    ''' <remarks></remarks>
    Dim loadAdmission As Boolean = False

    ''' <summary>
    ''' Controla cuando se selecciona tipo de riesgo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleRiskType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleRiskType.EditValueChanged
        If INDgleRiskType.EditValue IsNot Nothing AndAlso INDgleRiskType.EditValue = "2" Then
            INDlciTrafficAccident.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'LayoutControls.SetAllowHideInLayoutControlGroup(INDlciTrafficAccident, False)
            INDlciSentValue.AllowHide = False
            INDlciMinWage.AllowHide = False
            'cargar los ingresos que el paciente tiene con accidente de transito
            If INDsleAdmission.Properties.DataSource IsNot Nothing Then
                Dim listAdmissions As List(Of ADINGRESO) = INDsleAdmission.Properties.DataSource
                INDsleTCAdmission.Properties.DataSource = listAdmissions.FindAll(Function(admissions) admissions.IESTADOIN = "F")
                INDsleTCAdmission.Properties.PopupFormSize = New Size(700, 300)
            End If
        Else
            INDlciTrafficAccident.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LayoutControls.SetAllowHideInLayoutControlGroup(INDlciTrafficAccident, True)
        End If

        If loadAdmission = False Then
            'valido tipos de riesgo con causa
            If INDgleRiskType.EditValue IsNot Nothing Then
                Select Case INDgleRiskType.EditValue
                    Case Is = "2" 'Accidente de Transito
                        INDgleCause.EditValue = "6"
                    Case Is = "3" 'Catastrofe
                        INDgleCause.EditValue = "7"
                    Case Is = "1" 'Enfermedad General y Maternidad
                        INDgleCause.EditValue = "3"
                    Case Is = "5" 'Accidente de trabajo
                        INDgleCause.EditValue = "10"
                    Case Is = "6" 'Enfermedad Profesional
                        INDgleCause.EditValue = "2"
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Para validar si se muestra o no el grupo de remision
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleAccessed_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleAccessed.EditValueChanged
        If INDgleAccessed.EditValue IsNot Nothing AndAlso INDgleAccessed.EditValue = 4 Then
            INDlgRemission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'LayoutControls.SetAllowHideInLayoutControlGroup(INDlgRemission, False)
            INDlciRemissionNumber.AllowHide = False
            INDlciTown.AllowHide = False
            INDlciRemissionDate.AllowHide = False
            INDlciIPS.AllowHide = False
        Else
            INDlgRemission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LayoutControls.SetAllowHideInLayoutControlGroup(INDlgRemission, True)
        End If
    End Sub

    ''' <summary>
    ''' Cuando se selecciona un ingreso de accidente de trafico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTCAdmission_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTCAdmission.EditValueChanged
        If INDsleTCAdmission.EditValue IsNot Nothing Then
            Dim listADmissions As List(Of ADINGRESO) = INDsleTCAdmission.Properties.DataSource
            Dim _admission = listADmissions.Find(Function(ad) ad.NUMINGRES = INDsleTCAdmission.EditValue)
            INDtePopUpTAPersonName.Text = getStringTrimField(_admission.IPRNOMBRE)
            INDtePopUpTAteDate.EditValue = _admission.FECACTRAN
            INDtePopUpTAteIdentification.Text = getStringTrimField(_admission.IPCODACTR)
            INDtePopUpTAteExpedition.Text = getStringTrimField(_admission.IPEXPEDIC)
            INDtePopUpTAtedHour.EditValue = _admission.HORACIDEN
            INDtePopUpTAtePhone.Text = getStringTrimField(_admission.IPTELEFON)
            INDtePopUpTAmeObservations.Text = getStringTrimField(_admission.OBSERACIT)
        End If
    End Sub

    ''' <summary>
    ''' carga el control cuando se ingresa en el 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCenter.QueryPopUp
        If INDsleCenter.Properties.DataSource Is Nothing Then
            LoadCenters()
        End If
    End Sub

    ''' <summary>
    ''' carga el control cuando se ingresa en el 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFunctionalUnit_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleFunctionalUnit.QueryPopUp
        If INDsleFunctionalUnit.Properties.DataSource Is Nothing Then
            LoadFunctionalUnit()
        End If
    End Sub

    ''' <summary>
    ''' carga el control cuando se ingresa en el 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCareGroup_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCareGroup.QueryPopUp
        If INDsleCareGroup.Properties.DataSource Is Nothing Then
            LoadCareGroup()
        End If
    End Sub

    ''' <summary>
    ''' carga el control cuando se ingresa en el 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTown_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleTown.QueryPopUp
        If INDsleTown.Properties.DataSource Is Nothing Then
            LoadTown()
        End If
    End Sub

    ''' <summary>
    ''' carga el control cuando se ingresa en el 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIPS_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleIPS.QueryPopUp
        If INDsleIPS.Properties.DataSource Is Nothing Then
            LoadIPS()
        End If
    End Sub

    ''' <summary>
    ''' carga el control cuando se ingresa en el 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMinWage_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleMinWage.QueryPopUp
        If INDsleMinWage.Properties.DataSource Is Nothing Then
            LoadMinWage()
        End If
    End Sub

    Private Sub GridView1_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDsleAdmissionView.CustomColumnDisplayText
        If e.Column IsNot Nothing AndAlso Object.Equals(e.Column, INDColStatus) Then
            If e.Value IsNot Nothing Then
                Select Case e.Value
                    Case Is = " "
                        e.DisplayText = "Sin Confirmar Hoja de Trabajo"
                    Case Is = "F"
                        e.DisplayText = "Confirmada Hoja de Trabajo"
                    Case Is = "A"
                        e.DisplayText = "Anulado"
                    Case Is = "C"
                        e.DisplayText = "Cancelado"
                End Select
            End If
        End If

        If e.Column IsNot Nothing AndAlso Object.Equals(e.Column, INDColTipoRiesgo) Then
            If e.Value IsNot Nothing AndAlso CStr(e.Value) IsNot String.Empty Then
                If _listRiskType.Find(Function(x) x.Item1 = CStr(e.Value)) IsNot Nothing Then
                    e.DisplayText = _listRiskType.Find(Function(x) x.Item1 = CStr(e.Value)).Item2
                End If
                'Select Case e.Value
                '    Case Is = 1
                '        e.DisplayText = "Ninguno"
                '    Case Is = 2
                '        e.DisplayText = _listRiskType.Find(Function(x) x.Item1 = "2").ToString
                '    Case Is = 3
                '        e.DisplayText = _listRiskType.Find(Function(x) x.Item1 = "3").ToString
                '    Case Is = 4
                '        e.DisplayText = _listRiskType.Find(Function(x) x.Item1 = "4").ToString
                '    Case Is = 5
                '        e.DisplayText = _listRiskType.Find(Function(x) x.Item1 = "5").ToString
                '    Case Is = 6
                '        e.DisplayText = _listRiskType.Find(Function(x) x.Item1 = "6").ToString
                '    Case Is = 7
                '        e.DisplayText = "Ninguno"
                '    Case Is = 8
                '        e.DisplayText = "Ninguno"
                '    Case Is = 9
                '        e.DisplayText = "Ninguno"
                '    Case Is = 10
                '        e.DisplayText = "Ninguno"
                '    Case Is = 11
                '        e.DisplayText = "Ninguno"
                '    Case Is = 12
                '        e.DisplayText = "Ninguno"

                'End Select
            End If
        End If

    End Sub

    ''' <summary>
    ''' para validar cuando mostrar u ocultar el grupo de hospitalizacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleAdmissionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleAdmissionType.EditValueChanged
        If INDgleAdmissionType.EditValue IsNot Nothing AndAlso INDgleAdmissionType.EditValue = "2" Then
            INDlgHosp.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlgHosp.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Async Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show("Esta seguro que desea anular el ingreso?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim showJustification As New ShowDialogJustification
            Dim frmTransparent As New FrmTransparent(showJustification, False)
            Dim Justification As String = ""
            If frmTransparent.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Justification = showJustification.Justification
            Else
                Exit Sub
            End If
            Try
                AsyncLoader(True)
                Using m As New MAdmissions(Me.Tag)
                    Dim res = Await m.UpdateStatusAdmission(Admissions.NUMINGRES, "A", Justification)
                    If res.StateResult = True Then
                        If res.StateResultAux = False Then
                            Mensaje(EeventViewerImages.Advertencia) = res.Message
                        Else
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("AdmissionAnulated", NAME_MODULE), Admissions.NUMINGRES)
                            SearchMode = False
                            Me.Deshacer()
                        End If
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = res.Message
                    End If
                End Using
                AsyncLoader(False)
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se cambia de grupo de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleCareGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCareGroup.EditValueChanged
        'limpo controles
        INDsleEntityCups.Properties.ReadOnly = False
        EAPBType = Nothing
        INDlciContract.HideControl(True)
        INDteContract.Properties.ReadOnly = True
        INDlciEntity.HideControl(True)
        INDlciThirdPartyPatient.HideControl(True)
        INDteContract.Text = String.Empty

        If INDsleCareGroup.EditValue IsNot Nothing Then
            Using Model As New MCareGroup(Me.Tag)
                Dim careGroup = Await Model.GetCareGroupById(INDsleCareGroup.EditValue)
                INDsleEntityCups.Properties.DataSource = Nothing
                INDsleEntityCups.Properties.NullText = String.Empty
                Select Case careGroup.ObjectEmbbeded.CareGroupType
                    Case Is = 1 'Con contrato (mostrat EAPB y no permite modificar, )
                        EAPBType = ECareGroupType.EAPBConContrato
                        INDlciContract.HideControl(False)
                        INDlciEntity.HideControl(False)
                        INDsleEntityCups.Properties.ReadOnly = True
                        INDsleEntityCups.EditValue = careGroup.ObjectEmbbeded.Contract.HealthAdministratorId
                        Admissions.GENCONENTITY = careGroup.ObjectEmbbeded.Contract.HealthAdministratorId
                        INDsleEntityCups.Properties.NullText = careGroup.ObjectEmbbeded.Contract.HealthAdministratorDescription
                        INDteContract.Text = careGroup.ObjectEmbbeded.ContractDescription
                    Case Is = 2 'Sin contrato (preguntar por EAPB diferente a aseguradoras)
                        EAPBType = ECareGroupType.EAPBSinContrato
                        INDlciEntity.HideControl(False)
                        If Admissions.GENCONENTITY IsNot Nothing AndAlso Admissions.GENCONENTITY > 0 Then
                            Using modelHealAdm As New MHealthAdministrator(Me.Tag)
                                Dim _entityHealthadm As ActionResult(Of HealthAdministrator) = Await modelHealAdm.GetHealthAdministratorById(Admissions.GENCONENTITY)
                                INDsleEntityCups.EditValue = _entityHealthadm.ObjectEmbbeded.Id
                                INDsleEntityCups.Properties.NullText = _entityHealthadm.ObjectEmbbeded.Code & " - " & _entityHealthadm.ObjectEmbbeded.Name
                            End Using
                        End If
                    Case Is = 3 'Particulares (Mostrar tercero del paciente)
                        EAPBType = ECareGroupType.EAPBParticular
                        INDlciThirdPartyPatient.HideControl(False)
                        Using modelAdmissions As New MAdmissions(Me.Tag)
                            Dim thirdPartyPatient As Domain.Entities.ThirdParty = Await modelAdmissions.GetThirdpartyPatient(INDbeIdentification.Text)
                            If thirdPartyPatient IsNot Nothing AndAlso thirdPartyPatient.Id > 0 Then
                                INDteThirdParty.Text = thirdPartyPatient.Nit.Trim & " - " & thirdPartyPatient.Name.Trim
                            End If
                        End Using
                        Admissions.GENCONENTITY = Nothing
                    Case Is = 4 'Aseguradoras (preguntar por EAPB = aseguradoras)
                        EAPBType = ECareGroupType.EAPBAseguradora
                        INDlciEntity.HideControl(False)

                End Select

                If careGroup.ObjectEmbbeded IsNot Nothing Then
                    If careGroup.ObjectEmbbeded.RequirementTemplate IsNot Nothing AndAlso careGroup.ObjectEmbbeded.RequirementTemplate.Requirement IsNot Nothing AndAlso careGroup.ObjectEmbbeded.RequirementTemplate.Requirement.Count > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        RequirementTemplateName = careGroup.ObjectEmbbeded.RequirementTemplate.Code + " - " + careGroup.ObjectEmbbeded.RequirementTemplate.Name
                        CountRequirement = careGroup.ObjectEmbbeded.RequirementTemplate.Requirement.Count
                        ctrTmp.RefreshInfo()
                        ListRequirement = careGroup.ObjectEmbbeded.RequirementTemplate.Requirement.ToList
                        INDgcRequeriments.DataSource = Nothing
                        INDgcRequeriments.DataSource = ListRequirement
                    End If
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' cuando se abre el popup del control de entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntityCups_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntityCups.QueryPopUp
        If INDsleEntityCups.Properties.DataSource Is Nothing Then
            Using Model As New MAdmissions(Me.Tag)
                If EAPBType = ECareGroupType.EAPBAseguradora Then
                    INDsleEntityCups.Properties.DataSource = Model.ListHealthAdministratorByType(2) ' entidades iguales a aseguradoras
                ElseIf EAPBType = ECareGroupType.EAPBSinContrato Then
                    INDsleEntityCups.Properties.DataSource = Model.ListHealthAdministratorByNotType(2) ' entidades diferentes a aseguradoras
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Consulta los parametros cada q cambiamos de centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCenter.EditValueChanged
        If INDsleCenter.EditValue IsNot Nothing Then
            Using Model As New MAdmissions(Me.Tag)
                Dim result = Await Model.GetCenterParameters(INDsleCenter.EditValue)
                If result.StateResult = True Then
                    If result.ObjectEmbbeded.CODCENATE IsNot Nothing AndAlso result.ObjectEmbbeded.CODCENATE IsNot String.Empty Then
                        INDVigenciaIngresos = If(result.ObjectEmbbeded.VIGMESING Is Nothing, 0, result.ObjectEmbbeded.VIGMESING)
                        INDInterfazHospitalizacion = If(result.ObjectEmbbeded.INGHOSPIT Is Nothing, False, result.ObjectEmbbeded.INGHOSPIT)
                        If INDInterfazHospitalizacion Then
                            INDgleAdmissionType.Properties.ReadOnly = False
                        Else
                            INDgleAdmissionType.Properties.ReadOnly = True
                        End If
                    End If
                Else
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                End If
            End Using
            If INDsleFunctionalUnit.EditValue IsNot Nothing Then
                LoadBeds()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Carga las camas cuando se cambia de unidad funcional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFunctionalUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFunctionalUnit.EditValueChanged
        If INDsleFunctionalUnit.EditValue IsNot Nothing AndAlso INDsleCenter.EditValue IsNot Nothing Then
            LoadBeds()
        End If
    End Sub

    ''' <summary>
    ''' Valida si no se ha seleccionado unidad funcional para cargar las camas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBed_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleBed.QueryPopUp
        If INDsleFunctionalUnit.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione primero una unidad funcional!"
            INDsleFunctionalUnit.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' persiste en el campo GENCONENTITY del ingreso el id de la entidad seleccionada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntityCups_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityCups.EditValueChanged
        If INDsleEntityCups.Properties.DataSource IsNot Nothing AndAlso INDsleEntityCups.EditValue IsNot Nothing AndAlso CStr(INDsleEntityCups.EditValue).ToString.Trim IsNot String.Empty Then
            Admissions.GENCONENTITY = INDsleEntityCups.EditValue
        End If
    End Sub

    Private Sub INDsleCareGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCareGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("985", Nothing, True)
        End If
    End Sub

    Private Sub INDsleFunctionalUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFunctionalUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("523", Nothing, True)
        End If
    End Sub

    Private Sub INDsleEntityCups_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntityCups.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("972", Nothing, True)
        End If
    End Sub

    Private Sub FrmAdmissions_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbeIdentification.Focus()
    End Sub
End Class

''' <summary>
''' Estados del ingreso
''' </summary>
''' <remarks></remarks>
Public Enum EAdmissionStatus
    Unconfirmed
    Confirmed
    Canceled
    Closed
End Enum

''' <summary>
''' Enumeracion que contiene los tipos de grupos de atencion
''' </summary>
''' <remarks></remarks>
Public Enum ECareGroupType
    EAPBConContrato
    EAPBSinContrato
    EAPBAseguradora
    EAPBParticular
End Enum