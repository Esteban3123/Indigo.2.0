'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jose Luis Rojas
' Created          : 20-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Payroll.MVP
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports System.Text
Imports DevExpress.XtraLayout
Imports Infrastructure.Data.Xpo.CommonRepository

#End Region

Partial Public Class FrmEmployee

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Private ListDependsValue As New List(Of Tuple(Of Integer, String))

#Region "Methods"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmEmployeeMetaData, Eform.InfoMetaData), Me.Employee.ThirdParty.Nit, INDgleIdType.Text, Me.Employee.ThirdParty.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.Employee.ThirdParty.Nit & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmEmployeeMetaDataTitle, Eform.InfoMetaData), Me.Employee.ThirdParty.Nit),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmEmployeeMetaData, Eform.InfoMetaData), Me.Employee.ThirdParty.Nit, INDgleIdType.Text, Me.Employee.ThirdParty.Name)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmEmployeeMetaDataTitle, Eform.InfoMetaData), Me.Employee.ThirdParty.Nit)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MEmployee(MyBase.Tag)
                Await model.DeleteBlockRecord(record)
            End Using
            record = Nothing
        End If
    End Function

    ''' <summary>
    ''' Metodo para ocultar o mostrar las rejillas en cambio de que no hallan registros mostrar el label con informacion de que no hay registros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgrvNationalities_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgrvRelationships.DataSourceChanged, INDgrvRelationships.RowCountChanged, INDgrvProfessions.DataSourceChanged, INDgrvProfessions.RowCountChanged, INDgrvStudies.DataSourceChanged, INDgrvStudies.RowCountChanged, INDgrvLanguages.DataSourceChanged, INDgrvLanguages.RowCountChanged, INDgrvNationalities.DataSourceChanged, INDgrvNationalities.RowCountChanged, INDgrvFreeTimeUse.DataSourceChanged, INDgrvFreeTimeUse.RowCountChanged, INDgrvDiagnosedDisease.DataSourceChanged, INDgrvDiagnosedDisease.RowCountChanged, INDgrvDisabilities.DataSourceChanged, INDgrvDisabilities.RowCountChanged, GridView1.DataSourceChanged, GridView1.RowCountChanged
        Dim View As GridView = CType(sender, GridView)
        Dim VisibilityGrid As Boolean
        If View.DataSource IsNot Nothing AndAlso View.DataSource.count > 0 Then
            VisibilityGrid = True
        Else
            VisibilityGrid = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo asincrono que permite habilitar el sistema documental según el registro
    ''' </summary>
    ''' <returns></returns>
    Private Function SetDocuments() As Task
        Return Task.Factory.StartNew(Sub()
                                         BarraBotones.SafeInvoke(Sub(x) x.SetDocuments(Employee.Id, Me.Tag, Nothing, GetType(Domain.Payroll.Entities.Employee).Name))
                                     End Sub)
    End Function

    ''' <summary>
    ''' Metodo que me permitirá imprimir el reporte del formulario.
    ''' </summary>
    ''' <param name="printAction"></param>
    ''' <returns></returns>
    Private Function LoadReport(printAction As PrintReportAction) As Task
        Return Task.Factory.StartNew(Sub()
                                         Me.BarraBotones.SafeInvoke(Sub(x) x.PrintReport(printAction, Employee.Id, 0, Employee))
                                     End Sub)
    End Function


    ''' <summary>
    ''' Inicializa los componentes del formulario
    ''' </summary>
    Private Sub Initializes()

        INDgrdNationalities.DataSource = NationalityDatasource
        INDgrdFreeTimeUses.DataSource = FreeTimeUseDatasource
        INDgrdDiagnosedDiseases.DataSource = DiagnosedDiseaseDatasource
        INDgrdDisabilities.DataSource = DisabilitiesDatasource
        INDgrdLanguages.DataSource = LanguagesDatasource
        INDgrdProfessions.DataSource = ProfessionsDatasource
        INDgrdRelationships.DataSource = RelationshipsDatasource
        INDgrdStudies.DataSource = StudyDatasource
        GridControl1.DataSource = TradeUnionDatasource

        INDgleIdType.Properties.DataSource = Presenter.InitializaIdentificationType
        INDgleIdentificationType.Properties.DataSource = Presenter.InitializaIdentificationType

        INDgleMaritalStatus.Properties.DataSource = EmployeeHelper.MaritalStatus
        INDgleGender.Properties.DataSource = EmployeeHelper.Gender
        INDgleMilitaryCardType.Properties.DataSource = EmployeeHelper.MilitaryCardType
        INDgleBloodGroup.Properties.DataSource = EmployeeHelper.BloodGroup
        INDgleRH.Properties.DataSource = EmployeeHelper.RH
        INDgleTradeUnion.Properties.DataSource = EmployeeHelper.TradeUnion
        INDglePensionary.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList
        INDglePensionaryStatus.Properties.DataSource = EmployeeHelper.ActiveSuspendedBoolean.ToList
        INDGleForeignobligedQuotePension.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList
        INDgleRetiredForeign.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList
        INDgleRetentionType.Properties.DataSource = EmployeeHelper.RetentionType
        INDSlDeclarantType.Properties.DataSource = EmployeeHelper.DeclarantType
        INDgleIsFormal.Properties.DataSource = EmployeeHelper.IsFormal
        INDgleStudyStatus.Properties.DataSource = EmployeeHelper.StudyStatus
        INDgleProfessionalCardOnProcess.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList
        INDgleIsInternal.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList
        INDgleIsSupported.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList
        INDgleProvidesUpc.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList
        INDgleDependent.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList

        INDgleIsEmergencyContact.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList

        INDgleHousingType.Properties.DataSource = EmployeeHelper.HousingType.ToList
        INDgleSocioEconomicStatus.Properties.DataSource = EmployeeHelper.SocioEconomicStatus.ToList
        INDgleCigaretteConsumption.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList
        INDgleSportPractice.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList

        ListDependsValue = New List(Of Tuple(Of Integer, String))
        ListDependsValue.Add(New Tuple(Of Integer, String)(1, "Valor Fijo"))
        ListDependsValue.Add(New Tuple(Of Integer, String)(2, "Porcentaje"))

        INDSlDependsTypeValue.Properties.DataSource = ListDependsValue

        INDgleListOfStudies.Properties.DataSource = StudyDatasource

        INDSlGenderParents.Properties.DataSource = EmployeeHelper.Gender

        'Fechas limitadas al dia 
        INDdteBirthDate.Properties.MaxValue = Date.Today.AddYears(-15)
        INDdtePersonBirthdate.Properties.MaxValue = Date.Today
        INDdteDeathDate.Properties.MaxValue = Date.Today
        INDdteIdDate.Properties.MaxValue = Date.Today
        INDdteStudyStartDate.Properties.MaxValue = Date.Today
        INDdteStudyEndingDate.Properties.MaxValue = Date.Today
        INDdteStudyGraduationDate.Properties.MaxValue = Date.Today
        INDdteProfessionalCardExpeditionDate.Properties.MaxValue = Date.Today

        'focus el primer campo de texto
        INDbteIdNumber.Focus()

        'Estados
        Me.BarraBotones.StatusRecordVisible = True

        'Incapacidad Permanente.
        INDGlePermanentInability.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList

        Dim humanTalentStates As New List(Of StatusRecord)
        humanTalentStates.Add(New StatusRecord With {.StatusName = "Empleado Activo", .StatusValue = True, .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        humanTalentStates.Add(New StatusRecord With {.StatusName = "Contrato Activo", .StatusValue = CByte(1), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(173, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(126, Byte), Integer))})
        humanTalentStates.Add(New StatusRecord With {.StatusName = "Contrato Liquidado", .StatusValue = CByte(2), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(120, Byte), Integer))})
        humanTalentStates.Add(New StatusRecord With {.StatusName = "Contrato Anulado", .StatusValue = CByte(3), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(124, Byte), Integer))})
        humanTalentStates.Add(New StatusRecord With {.StatusName = "Contrato Reemplazado", .StatusValue = CByte(4), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(102, Byte), Integer))})
        humanTalentStates.Add(New StatusRecord With {.StatusName = "Parcialmente Retirado", .StatusValue = CByte(5), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(51, Byte), Integer))})
        humanTalentStates.Add(New StatusRecord With {.StatusName = "Empleado Inactivo", .StatusValue = False, .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        Me.BarraBotones.States = humanTalentStates

        INDctrContractViewer.BarraBotones = Me.BarraBotones
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Async Function CleanControls() As Task

        ActionsOnControls = False

        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        IdPersonNumber = String.Empty
        IdentificationType = Nothing
        ValidatingFlag = False
        IdentificationTypeXpo = Nothing
        INDsleIdExpeditionCityId.Properties.NullText = String.Empty
        INDsleIdExpeditionCityId.EditValue = Nothing
        INDdteIdDate.EditValue = Nothing
        INDtxtFirstName.EditValue = String.Empty
        INDtxtSecondName.EditValue = String.Empty
        INDtxtFirstLastname.EditValue = String.Empty
        INDtxtSecondLastname.EditValue = String.Empty
        InternalCode = String.Empty
        InsuredCCSSCode = String.Empty
        INDdteBirthDate.EditValue = Nothing
        INDlblAge.Text = Nothing
        INDlblCompanyTime.Text = Nothing
        INDlblPositionTime.Text = Nothing
        INDsleBirthCity.Properties.NullText = String.Empty
        INDsleBirthCity.EditValue = -1
        INDgleGender.EditValue = Nothing
        INDgleMaritalStatus.EditValue = Nothing
        INDgleMilitaryCardType.EditValue = Nothing
        INDtxtMilitaryCardNumber.EditValue = String.Empty
        INDdteDeathDate.EditValue = Nothing
        INDgleBloodGroup.EditValue = Nothing
        INDgleRH.EditValue = Nothing
        INDgleTradeUnion.EditValue = Nothing
        INDglePensionary.EditValue = Nothing
        INDglePensionaryStatus.EditValue = Nothing
        INDGleForeignobligedQuotePension.EditValue = Nothing
        INDGlePermanentInability.EditValue = Nothing
        INDDeFinalDateInability.EditValue = Nothing
        INDDeFinalDateInability.EditValue = Nothing
        INDslePensionaryTypeId.Properties.NullText = String.Empty
        INDslePensionaryTypeId.EditValue = -1
        INDgleRetiredForeign.EditValue = Nothing
        INDgleRetentionType.EditValue = Nothing

        INDSlRelocation.EditValue = False
        Me.INDbteIdNumber.Properties.MaxLength = 25
        INDgleHousingType.EditValue = Nothing
        INDgleSocioEconomicStatus.EditValue = Nothing
        INDgleCigaretteConsumption.EditValue = Nothing
        INDgleSportPractice.EditValue = Nothing

        INDSlDeclarantType.EditValue = Nothing
        INDtxtAverageHealthYearValue.EditValue = Nothing
        INDtxtHousingDeductionValue.EditValue = String.Empty
        INDtxtEducationDeductionValue.EditValue = String.Empty
        INDtxtSupplementaryPensionValue.EditValue = String.Empty
        INDTxtHealthContributorRTF.EditValue = String.Empty
        INDtxtDependents.EditValue = String.Empty
        INDChkAllowJobReference.EditValue = False
        Me.BarraBotones.StatusRecord = True
        Me.BarraBotones.StatusRecordVisible = False

        INDCtrDigitalSignature.DigitalSignature = Nothing

        'Nacionalidades
        NationalityDatasource.Clear()
        INDgrdNationalities.RefreshDataSource()
        INDsleNationality.EditValue = -1

        'Actividades en Tiempo Libre
        FreeTimeUseDatasource.Clear()
        INDgrdFreeTimeUses.RefreshDataSource()
        INDsleFreeTimeUse.EditValue = -1

        'Enfermedades Diagnosticadas
        DiagnosedDiseaseDatasource.Clear()
        INDgrdDiagnosedDiseases.RefreshDataSource()
        INDsleDiagnosedDisease.EditValue = -1

        'sindicatos
        TradeUnionDatasource.Clear()
        GridControl1.RefreshDataSource()
        INDsleTradeUnion.EditValue = -1
        'Popup de discapacidades
        INDsleNewDisability.EditValue = -1
        INDspeNewDisabilityPercentage.EditValue = 0
        DisabilitiesDatasource.Clear()
        INDgrdDisabilities.RefreshDataSource()
        'rentas exentas
        INDgcExemptIncome.DataSource = Nothing
        'Popup Rentas Exentas
        ExemptIncomeValue = Nothing
        DateExemptIcome = Nothing
        ExemptIncomeComment = String.Empty

        'popup de idiomas
        INDsleLanguageId.EditValue = -1
        INDspeLanguageLevel.EditValue = 0
        LanguagesDatasource.Clear()
        INDgrdLanguages.RefreshDataSource()

        'popup de profesiones
        CleanProfessionsPopUp()
        ProfessionsDatasource.Clear()
        INDgrdProfessions.RefreshDataSource()
        INDsleProfessionId.EditValue = -1

        'popup de estudios
        CleanStudyPopUp()
        StudyDatasource.Clear()
        INDgrdStudies.RefreshDataSource()

        'popup de relaciones
        CleanRelationshipsPopUp()
        RelationshipsDatasource.Clear()
        INDgrdRelationships.RefreshDataSource()

        ContractDatasource.Clear()

        'Control Datos de Contacto
        CtrContacts.EstablecerDataSourceDireccion = Nothing
        CtrContacts.EstablecerDataSourceTelefono = Nothing
        CtrContacts.EstablecerDataSourceEmail = Nothing

        Employee = New Domain.Payroll.Entities.Employee
        listTradeUnionDetail = New List(Of TradeUnionEmployee)

        ListadoEliminadosDisability = New List(Of Domain.Payroll.Entities.PersonDisability)
        ListadoEliminadosLanguage = New List(Of Domain.Payroll.Entities.PersonLanguage)
        ListadoEliminadosNacionalidades = New List(Of Domain.Payroll.Entities.PersonNationality)
        ListadoEliminadosNacionalidadesistadoEliminadosStudy = New List(Of Domain.Payroll.Entities.PersonStudy)
        ListadoEliminadosProfession = New List(Of Domain.Payroll.Entities.PersonProfession)
        ListadoEliminadosRelationship = New List(Of Relationship)

        INDctrContractViewer.CleanControls()
        Employee.Contract = Nothing
        ActionsOnControls = False
        Await DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.StatusRecordEnabled = False
        INDLcDateFilingAbroad.HideLayout()
        INDLciContributorAbroad.HideLayout()

        ContributorAbroad = False
        INDDeDateFilingAbroad.EditValue = Nothing
        INDlygExcemptIncome.Enabled = False
        'Validamos si las listas de rentas tienen datos para limpiar las 
        If listExemptIncomeGlobal IsNot Nothing Or (listToDeleteExemptIncome.Count > 0 And listToSaveExemptIncome.Count > 0) Then

            For Each exemptList In {listExemptIncomeGlobal, listToDeleteExemptIncome, listToSaveExemptIncome}
                If exemptList.Count > 0 Then
                    exemptList.Clear()
                End If
            Next
        End If

        'Colapsamos las rejillas
        viewExemptIncome.CollapseAllDetails()
    End Function

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IEmployee.ActionsOnControls
        Set(value As Boolean)

            INDlyCtrHumanTalent.BeginUpdate()
            INDlcgEmployee.BeginUpdate()

            INDbteIdNumber.Enabled = Not value
            INDgleIdType.Enabled = value
            INDsleIdExpeditionCityId.Enabled = value
            INDdteIdDate.Enabled = value
            INDtxtFirstName.Enabled = value
            INDtxtSecondName.Enabled = value
            INDtxtFirstLastname.Enabled = value
            INDtxtSecondLastname.Enabled = value
            INDdteBirthDate.Enabled = value
            INDlblAge.Enabled = value
            INDlblPositionTime.Enabled = value
            INDlblCompanyTime.Enabled = value
            INDsleBirthCity.Enabled = value
            INDsleReligiousBeliefs.Enabled = value
            INDsleEthnicGroups.Enabled = value
            INDgleGender.Enabled = value
            INDgleMaritalStatus.Enabled = value

            INDgleHousingType.Enabled = value
            INDgleSocioEconomicStatus.Enabled = value
            INDgleCigaretteConsumption.Enabled = value
            INDgleSportPractice.Enabled = value

            INDSlRelocation.Enabled = value

            INDgleMilitaryCardType.Enabled = value
            INDtxtMilitaryCardNumber.Enabled = value
            INDdteDeathDate.Enabled = value
            INDgleBloodGroup.Enabled = value
            INDgleRH.Enabled = value
            INDgleTradeUnion.Enabled = value
            INDglePensionary.Enabled = value
            INDglePensionaryStatus.Enabled = value
            INDGleForeignobligedQuotePension.Enabled = value
            INDGlePermanentInability.Enabled = value
            INDDeInitialDateInability.Enabled = value
            INDDeFinalDateInability.Enabled = value
            INDslePensionaryTypeId.Enabled = value
            INDgleRetiredForeign.Enabled = value
            INDgleRetentionType.Enabled = value
            INDSlDeclarantType.Enabled = value
            INDtxtAverageHealthYearValue.Enabled = value
            INDtxtHousingDeductionValue.Enabled = value
            INDtxtEducationDeductionValue.Enabled = value
            INDtxtSupplementaryPensionValue.Enabled = value
            INDTxtHealthContributorRTF.Enabled = value
            INDbtnNationality.Enabled = value
            INDbtnFreeTimeUse.Enabled = value
            INDbtnDiagnosedDisease.Enabled = value
            INDbtnNewTradeUnion.Enabled = value
            INDbtnContactInfo.Enabled = value
            INDbtnAddDisability.Enabled = value
            INDbtnAddLanguage.Enabled = value
            INDbtnAddProfession.Enabled = value
            INDbtnAddStudy.Enabled = value
            INDtxtDependents.Enabled = value
            INDbtnAddRelationship.Enabled = value
            INDctrContractViewer.Enabled = value
            INDpceContacts.Enabled = value
            INDChkAllowJobReference.Enabled = value
            INDPopDigitalSignature.Enabled = value
            INDgrdRelationships.Enabled = value
            INDgrdNationalities.Enabled = value
            INDgrdFreeTimeUses.Enabled = value
            INDgrdDiagnosedDiseases.Enabled = value
            GridControl1.Enabled = value
            INDgrdDisabilities.Enabled = value
            INDgrdLanguages.Enabled = value
            INDgrdStudies.Enabled = value
            INDgrdProfessions.Enabled = value
            INDtxtInternalCode.Enabled = value
            INDtxtCodigoCCSS.Enabled = value

            If value Then
                INDgleIdType.Focus()
            Else
                INDbteIdNumber.Focus()
            End If

            INDlcgEmployee.EndUpdate()
            INDlyCtrHumanTalent.EndUpdate()
        End Set
    End Property

    Public Function GetAge(ByVal birthDate As DateTime) As Integer

        Dim n As DateTime = DateTime.Now ' To avoid a race condition around midnight
        Dim age As Integer = n.Year - birthDate.Year

        If n.Month < birthDate.Month Or (n.Month = birthDate.Month And n.Day < birthDate.Day) Then
            age -= 1
        End If

        Return age
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Sub LoadControls()
        Try
            Me.BarraBotones.StatusRecordVisible = True
            Me.BarraBotones.StatusRecord = True
            ActionsOnControls = True
            AsyncLoader(True)
            Using Model As New MEmployee(MEmployee.TAG)
                Employee = Await Model.GetEmployeeAsync(IdPersonNumber)
                INDlyCtrHumanTalent.BeginUpdate()
                If Employee IsNot Nothing AndAlso (Employee.Id > 0 OrElse Employee.ThirdParty.Id > 0 OrElse Employee.ThirdParty.Person.Id > 0) Then
                    With Employee

                        If Employee.Id = 0 Then
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Else
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                        End If
                        NewEmployee = False

                        'Tercero
                        IdPersonNumber = .ThirdParty.Nit
                        INDgleRetentionType.EditValue = .ProcedureTypeRTF
                        INDSlDeclarantType.EditValue = .DeclarantType
                        INDtxtAverageHealthYearValue.EditValue = .AverageYearHealth

                        INDSlRelocation.EditValue = .Relocation
                        'Tercero para ejecutar la funcion
                        INDgcExemptIncome.DataSource = Presenter.ListExemptIncome(Employee.ThirdParty.Id)

                        'Persona
                        IdPersonNumber = .ThirdParty.Person.IdentificationNumber
                        IdentificationType = .ThirdParty.Person.IdentificationTypeId
                        Await ValidateIdentificationType(IdPersonNumber, IdentificationType, isNew:=NewEmployee)

                        INDsleIdExpeditionCityId.Properties.NullText = .ThirdParty.Person.IdentificationCityName
                        INDsleIdExpeditionCityId.EditValue = .ThirdParty.Person.IdentificacionCityId

                        INDdteIdDate.EditValue = .ThirdParty.Person.IdentificationExpeditionDate
                        INDtxtFirstName.EditValue = .ThirdParty.Person.FirstName
                        INDtxtSecondName.EditValue = .ThirdParty.Person.SecondName
                        INDtxtFirstLastname.EditValue = .ThirdParty.Person.FirstLastName
                        INDtxtSecondLastname.EditValue = .ThirdParty.Person.SecondLastName
                        INDdteBirthDate.EditValue = .ThirdParty.Person.BirthDate

                        If .ThirdParty.Person.BirthDate IsNot Nothing Then
                            INDlblAge.Text = Math.Floor((Math.Abs(DateDiff(DateInterval.Month, Date.Today.Date, INDdteBirthDate.EditValue)) / 12))
                        End If

                        INDsleBirthCity.Properties.NullText = .ThirdParty.Person.BirthCityName
                        INDsleBirthCity.EditValue = .ThirdParty.Person.BirthCityId

                        INDgleGender.EditValue = .ThirdParty.Person.Gender
                        INDtxtMilitaryCardNumber.EditValue = .ThirdParty.Person.MilitaryCardNumber
                        INDgleMilitaryCardType.EditValue = .ThirdParty.Person.MilitaryCardId
                        INDdteDeathDate.EditValue = .ThirdParty.Person.DeathDate
                        INDgleBloodGroup.EditValue = .ThirdParty.Person.BloodGroup
                        INDgleRH.EditValue = .ThirdParty.Person.RH
                        INDgleMaritalStatus.EditValue = .ThirdParty.Person.MaritalStatus
                        INDtxtDependents.EditValue = .ThirdParty.Person.Dependents

                        INDgleHousingType.EditValue = .ThirdParty.Person.HousingType
                        INDgleSocioEconomicStatus.EditValue = .ThirdParty.Person.SocioEconomicStatus
                        INDgleCigaretteConsumption.EditValue = .ThirdParty.Person.CigaretteConsumption
                        INDgleSportPractice.EditValue = .ThirdParty.Person.SportPractice

                        If .Contract.Any() Then
                            Dim positionTime As Integer = 0
                            Dim companyTime As Integer = 0

                            If .Contract.LastOrDefault.ContractEndingDate <= Date.Today Then
                                companyTime = Math.Abs(DateDiff(DateInterval.Month, .Contract.FirstOrDefault.JobBondingDate, .Contract.LastOrDefault.ContractEndingDate))
                            Else
                                companyTime = Math.Abs(DateDiff(DateInterval.Month, .Contract.FirstOrDefault.JobBondingDate, Date.Today))
                            End If

                            For Each aux In .Contract.Where(Function(x) x.PositionId = .Contract.LastOrDefault.PositionId)
                                If aux.ContractEndingDate <= Date.Today Then
                                    positionTime = positionTime + Math.Abs(DateDiff(DateInterval.Month, aux.ContractInitialDate, aux.ContractEndingDate))
                                Else
                                    positionTime = positionTime + Math.Abs(DateDiff(DateInterval.Month, aux.ContractInitialDate, Date.Today))
                                End If
                            Next

                            INDlblCompanyTime.Text = companyTime.ToString + " Meses"
                            INDlblPositionTime.Text = positionTime.ToString + " Meses"
                        Else

                            INDlblCompanyTime.Text = "No tiene contratos asociados"
                            INDlblPositionTime.Text = "No tiene contratos asociados"
                        End If

                        INDChkAllowJobReference.EditValue = .AllowJobReference
                        INDCtrDigitalSignature.DigitalSignature = .ThirdParty.DigitalSignature

                        Me.BarraBotones.StatusRecord = .State

                        If .TradeUnion = 3 Then
                            For Each item As TradeUnionEmployee In .TradeUnionEmployee
                                TradeUnionDatasource.Add(item)
                            Next
                        End If
                        GridControl1.DataSource = TradeUnionDatasource
                        GridControl1.RefreshDataSource()
                        For Each item As Domain.Payroll.Entities.PersonNationality In .ThirdParty.Person.PersonNationality
                            NationalityDatasource.Add(item)
                        Next
                        INDgrdNationalities.RefreshDataSource()

                        For Each item As Domain.Payroll.Entities.PersonFreeTimeUse In .ThirdParty.Person.PersonFreeTimeUse
                            FreeTimeUseDatasource.Add(item)
                        Next
                        INDgrdFreeTimeUses.RefreshDataSource()

                        For Each item As Domain.Payroll.Entities.PersonDiagnosedDisease In .ThirdParty.Person.PersonDiagnosedDisease
                            DiagnosedDiseaseDatasource.Add(item)
                        Next
                        INDgrdDiagnosedDiseases.RefreshDataSource()

                        For Each item As Domain.Payroll.Entities.PersonStudy In .ThirdParty.Person.PersonStudy
                            StudyDatasource.Add(item)
                        Next
                        INDgrdStudies.RefreshDataSource()

                        For Each item As Domain.Payroll.Entities.PersonDisability In .ThirdParty.Person.PersonDisability
                            DisabilitiesDatasource.Add(item)
                        Next
                        INDgrdDisabilities.RefreshDataSource()

                        For Each item As Domain.Payroll.Entities.PersonLanguage In .ThirdParty.Person.PersonLanguage
                            LanguagesDatasource.Add(item)
                        Next
                        INDgrdLanguages.RefreshDataSource()

                        For Each item As Domain.Payroll.Entities.PersonProfession In .ThirdParty.Person.PersonProfession
                            ProfessionsDatasource.Add(item)
                        Next
                        INDgrdProfessions.RefreshDataSource()

                        'Control Datos de Contacto
                        CtrContacts.EstablecerDataSourceDireccion = .ThirdParty.Person.Address.ToList
                        CtrContacts.EstablecerDataSourceTelefono = .ThirdParty.Person.Phone.ToList
                        CtrContacts.EstablecerDataSourceEmail = .ThirdParty.Person.Email.ToList

                        'Employee
                        INDgleTradeUnion.EditValue = .TradeUnion
                        INDglePensionary.EditValue = .Pensionary
                        INDglePensionaryStatus.EditValue = .PensionaryStatus
                        INDGleForeignobligedQuotePension.EditValue = .ForeignobligedQuotePension
                        INDGlePermanentInability.EditValue = If(.PermanentInability Is Nothing, False, .PermanentInability)
                        INDDeInitialDateInability.EditValue = .InitialDatePermanentInability
                        INDDeFinalDateInability.EditValue = .EndDatePermanentInability

                        If .PensionaryTypeId IsNot Nothing Then
                            INDslePensionaryTypeId.Properties.NullText = .PensionaryTypeName
                            INDslePensionaryTypeId.EditValue = .PensionaryTypeId
                        End If

                        INDgleRetiredForeign.EditValue = .RetiredForeign
                        INDtxtHousingDeductionValue.EditValue = .HousingDeductionValue
                        INDtxtEducationDeductionValue.EditValue = .EducationDeductionValue
                        INDtxtSupplementaryPensionValue.EditValue = .SupplementaryPension
                        INDTxtHealthContributorRTF.EditValue = .HealthContributorRTF
                        InternalCode = .InternalCode
                        InsuredCCSSCode = .InsuredCCSSCode
                        Me.BarraBotones.StatusRecord = .State

                        For Each item As Relationship In .Relationship
                            RelationshipsDatasource.Add(item)
                        Next
                        INDgrdRelationships.RefreshDataSource()

                        INDtxtDependents.EditValue = RelationshipsDatasource.Where(Function(i) i.Dependent = True).Count
                        For Each item As Domain.Payroll.Entities.Contract In .Contract
                            ContractDatasource.Add(item)
                        Next

                        'se valida si el campo viene en 'SI' asigno los datos 
                        If ValidateContributorAbroad() Then
                            ContributorAbroad = .ContributorAbroad
                            If ValidateDateFilingAbroad() Then
                                DateFilingAbroad = .DateFilingAbroad
                            End If
                        End If
                    End With

                    Me.GetDocumentIndexed(Me.Tag & "_" & Me.Employee.Id)
                    If Employee.Id > 0 Then
                        Dim result = Await Model.GetBlockRecord(Me.Tag, Employee.Id)
                        If result.Id = 0 Then
                            Me.BarraBotones.SetDocuments(Me.Employee.Id)
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            record = New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Me.Employee.Id}
                            Dim operation = Await Model.SaveBlockRecord(record)
                            record = operation.ObjectEmbbeded
                        Else
                            record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                    End If

                    INDctrContractViewer.Employee(Employee)
                    INDctrContractViewer.ShowLatestOrActualContract()
                    Await SetDocuments()
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    Await LoadReport(PrintReportAction.None)
                Else
                    INDlciDeathDate.HideLayout()
                    INDctrContractViewer.Employee(Employee)
                    INDctrContractViewer.EnableControlsFor(True, False, False, False)
                    NewEmployee = True

                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                End If
            End Using

            ' Los detalles se cargarán solo cuando se expanda una fila del maestro
            If INDgcExemptIncome.DataSource IsNot Nothing Then
                INDlygExcemptIncome.Enabled = True
            Else
                INDlygExcemptIncome.Enabled = False
            End If

            INDlyCtrHumanTalent.EndUpdate()

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de cargar los controles para el popup de rentas
    ''' </summary>
    Private Sub LoadpopupControl()
        Try
            Dim PopupList = childView.DataSource(childView.FocusedRowHandle)
            Dim PopupObject = CType(PopupList, ExemptIncome)

            ' Buscar en listExemptIncomeGlobal (ediciones locales)
            Dim foundInMemory = False
            For Each item In listExemptIncomeGlobal
                Dim idParts() As String = item.Id.Split("-"c)
                If CType(idParts(0), Integer) = PopupObject.Id Then
                    ExemptIncomeValue = item.ExemptIncomeValue
                    DateExemptIcome = item.DateLiquidation
                    ExemptIncomeComment = item.Comments
                    foundInMemory = True
                    Exit For
                End If
            Next

            ' Si no se encontró en memoria, usar los datos del grid y agregarlo a la lista para futuras ediciones
            If Not foundInMemory Then
                ExemptIncomeValue = PopupObject.ExemptIncomeValue
                DateExemptIcome = PopupObject.DateLiquidation
                ExemptIncomeComment = PopupObject.Comments

                ' Agregar a listExemptIncomeGlobal para que las modificaciones funcionen
                Dim itemToAdd = New CommonExemptIncomeDetailXpo With {
                    .Id = PopupObject.Id.ToString() & "-" & PopupObject.ThirdPartyId.ToString(),
                    .ExemptIncomeValue = PopupObject.ExemptIncomeValue,
                    .DateLiquidation = PopupObject.DateLiquidation,
                    .ThirdPartyId = PopupObject.ThirdPartyId,
                    .YearLiquidated = PopupObject.YearLiquidated,
                    .Comments = PopupObject.Comments,
                    .VoucherType = PopupObject.VoucherType,
                    .VoucherCode = PopupObject.VoucherCode,
                    .MonthlyIncome = PopupObject.MonthlyIncome,
                    .RegisterStatus = PopupObject.RegisterStatus,
                    .IsNew = 0
                }
                listExemptIncomeGlobal.Add(itemToAdd)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Valida la nacionalidad y en el item 6 oculta los campos no necesitados para esta nacionalidad
    ''' </summary>
    Private Sub ShowButtonSuprementaryPension()
        If indigo.LanguageCulture = "es-CR" Then
            INDlciHousingDeductionValue.HideControl()
            INDlciEducationDeductionValue.HideControl()
            INDLciHealthContribution.HideControl()
            INDlciAverageHealthLastYearValue.HideControl()

        End If
    End Sub

    ''' <summary>
    ''' Función que valida los controles de tipo GridControl y devuelve el listado de controles que son obligatorios y no se han diligenciado
    ''' </summary>
    Private Function ValidateGridControls()
        Dim controlGridPairs As New List(Of Tuple(Of DevExpress.XtraGrid.GridControl, LayoutControlItem)) From {
        Tuple.Create(Of DevExpress.XtraGrid.GridControl, LayoutControlItem)(INDlciFreeTimeUse.Control, INDlciFreeTime),
        Tuple.Create(Of DevExpress.XtraGrid.GridControl, LayoutControlItem)(INDlciLanguages.Control, INDlciLanguage),
        Tuple.Create(Of DevExpress.XtraGrid.GridControl, LayoutControlItem)(INDlciNationalities.Control, INDlciNationalityPopup),
        Tuple.Create(Of DevExpress.XtraGrid.GridControl, LayoutControlItem)(LayoutControlItem3.Control, INDlciContactInfo),
        Tuple.Create(Of DevExpress.XtraGrid.GridControl, LayoutControlItem)(INDlciDisabilities.Control, INDlciDisability),
        Tuple.Create(Of DevExpress.XtraGrid.GridControl, LayoutControlItem)(INDlciDiagnosedDiseaseMain.Control, LayoutControlItem5),
        Tuple.Create(Of DevExpress.XtraGrid.GridControl, LayoutControlItem)(INDlciStudies.Control, INDlciStudy),
        Tuple.Create(Of DevExpress.XtraGrid.GridControl, LayoutControlItem)(INDlciProfessions.Control, INDlciProfession),
        Tuple.Create(Of DevExpress.XtraGrid.GridControl, LayoutControlItem)(INDlciRelationships.Control, INDlciRelationship)
    }

        Dim errors As New StringBuilder
        For Each gridControl In controlGridPairs
            For Each gridControlJSON In controlParameterization.Items

                If gridControlJSON.ItemName = gridControl.Item2.Name Then


                    If gridControlJSON.Obligatory Then
                        If gridControl.Item1.DataSource.Count = 0 Then
                            errors.AppendLine(gridControl.Item2.Text)
                        End If
                    End If
                End If

            Next
        Next

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Employee
            'Tercero
            .ThirdParty.Nit = IdPersonNumber
            .ThirdParty.Name = INDtxtFirstName.Text.Trim + " " + INDtxtSecondName.Text.Trim + " " + INDtxtFirstLastname.Text.Trim + " " + INDtxtSecondLastname.Text.Trim
            'se asigna 1 que es el valor de las personas naturales, siempre deben ser personas naturales
            .ThirdParty.PersonType = 1
            'se asigna 0 valor de regimen simplificado
            .ThirdParty.ContributionType = 0
            'se asigna los siguientes valores ya que un empleado no maneja ICA
            .ThirdParty.Ica = False
            .ThirdParty.IcaPercentage = 0D
            .ThirdParty.IcaTop = False
            .ThirdParty.IcaTopValue = 0D
            .ThirdParty.DigitalSignature = INDCtrDigitalSignature.DigitalSignature
            .ThirdParty.State = True
            If .ThirdParty.ChangeTracker.State = ObjectState.Added Then
                .ThirdParty.CreationDate = Date.Today
            End If
            .ThirdParty.State = True

            'Persona
            .ThirdParty.Person.IdentificationNumber = IdPersonNumber
            .ThirdParty.Person.IdentificationType = 0
            .ThirdParty.Person.IdentificationTypeId = IdentificationType
            .ThirdParty.Person.DocumentTypeAbbreviation = IdentificationTypeXpo.SIGLA
            .ThirdParty.Person.IdentificacionCityId = INDsleIdExpeditionCityId.EditValue
            .ThirdParty.Person.IdentificationExpeditionDate = INDdteIdDate.EditValue
            .ThirdParty.Person.FirstName = INDtxtFirstName.Text.Trim
            .ThirdParty.Person.SecondName = INDtxtSecondName.Text.Trim
            .ThirdParty.Person.FirstLastName = INDtxtFirstLastname.Text.Trim
            .ThirdParty.Person.SecondLastName = INDtxtSecondLastname.Text.Trim
            .ThirdParty.Person.BirthDate = INDdteBirthDate.EditValue
            .ThirdParty.Person.BirthCityId = INDsleBirthCity.EditValue
            .ThirdParty.Person.Gender = CType(INDgleGender.EditValue, Byte)
            .ThirdParty.Person.MilitaryCardNumber = INDtxtMilitaryCardNumber.EditValue
            .ThirdParty.Person.MilitaryCardId = INDgleMilitaryCardType.EditValue
            .ThirdParty.Person.DeathDate = If(INDdteDeathDate.EditValue Is Nothing OrElse INDdteDeathDate.EditValue.ToString.Trim.Equals(String.Empty), Nothing, INDdteDeathDate.EditValue)
            .ThirdParty.Person.BloodGroup = INDgleBloodGroup.EditValue
            .ThirdParty.Person.RH = INDgleRH.EditValue
            .ThirdParty.Person.MaritalStatus = CType(INDgleMaritalStatus.EditValue, Byte)
            .ThirdParty.Person.Dependents = If(INDtxtDependents.EditValue Is Nothing OrElse INDtxtDependents.EditValue.ToString.Trim.Equals(String.Empty), CType(0, Byte), CType(INDtxtDependents.EditValue, Byte))
            .ThirdParty.Person.State = Me.BarraBotones.StatusRecord
            .ThirdParty.Person.HousingType = CType(INDgleHousingType.EditValue, Byte)
            .ThirdParty.Person.SocioEconomicStatus = CType(INDgleSocioEconomicStatus.EditValue, Byte)
            .ThirdParty.Person.CigaretteConsumption = CType(INDgleCigaretteConsumption.EditValue, Byte)
            .ThirdParty.Person.SportPractice = CType(INDgleSportPractice.EditValue, Byte)
            .ThirdParty.Person.ReligiousBeliefsId = INDsleReligiousBeliefs.EditValue
            .ThirdParty.Person.EthnicGroupId = INDsleEthnicGroups.EditValue

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

            If Object.Equals(.ThirdParty.Person.PersonNationality, Nothing) = False Then
                For i As Integer = 0 To ListadoEliminadosNacionalidades.Count - 1
                    ListadoEliminadosNacionalidades.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    .ThirdParty.Person.PersonNationality.Add(ListadoEliminadosNacionalidades.Item(i))
                Next
            End If

            If Object.Equals(.ThirdParty.Person.PersonFreeTimeUse, Nothing) = False Then
                For i As Integer = 0 To ListadoEliminadosActividadesTiempoLibre.Count - 1
                    ListadoEliminadosActividadesTiempoLibre.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    .ThirdParty.Person.PersonFreeTimeUse.Add(ListadoEliminadosActividadesTiempoLibre.Item(i))
                Next
            End If

            If Object.Equals(.ThirdParty.Person.PersonDiagnosedDisease, Nothing) = False Then
                For i As Integer = 0 To ListadoEliminadosEnfermedadesDiagnosticadas.Count - 1
                    ListadoEliminadosEnfermedadesDiagnosticadas.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    .ThirdParty.Person.PersonDiagnosedDisease.Add(ListadoEliminadosEnfermedadesDiagnosticadas.Item(i))
                Next
            End If

            If Object.Equals(.ThirdParty.Person.PersonDisability, Nothing) = False Then
                For i As Integer = 0 To ListadoEliminadosDisability.Count - 1
                    ListadoEliminadosDisability.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    .ThirdParty.Person.PersonDisability.Add(ListadoEliminadosDisability.Item(i))
                Next
            End If

            If Object.Equals(.ThirdParty.Person.PersonStudy, Nothing) = False Then
                For i As Integer = 0 To ListadoEliminadosStudy.Count - 1
                    ListadoEliminadosStudy.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    .ThirdParty.Person.PersonStudy.Add(ListadoEliminadosStudy.Item(i))
                Next
            End If

            If Object.Equals(.ThirdParty.Person.PersonProfession, Nothing) = False Then
                For i As Integer = 0 To ListadoEliminadosProfession.Count - 1
                    ListadoEliminadosProfession.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    .ThirdParty.Person.PersonProfession.Add(ListadoEliminadosProfession.Item(i))
                Next
            End If

            If Object.Equals(.ThirdParty.Person.PersonLanguage, Nothing) = False Then
                For i As Integer = 0 To ListadoEliminadosLanguage.Count - 1
                    ListadoEliminadosLanguage.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    .ThirdParty.Person.PersonLanguage.Add(ListadoEliminadosLanguage.Item(i))
                Next
            End If

            If Object.Equals(.Relationship, Nothing) = False Then
                For i As Integer = 0 To ListadoEliminadosRelationship.Count - 1
                    ListadoEliminadosRelationship.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    .Relationship.Add(ListadoEliminadosRelationship.Item(i))
                Next
            End If

            For Each item As Domain.Payroll.Entities.PersonNationality In NationalityDatasource
                item.Country = Nothing
                .ThirdParty.Person.PersonNationality.Add(item)
            Next

            For Each item As Domain.Payroll.Entities.PersonFreeTimeUse In FreeTimeUseDatasource
                item.FreeTimeUse = Nothing
                .ThirdParty.Person.PersonFreeTimeUse.Add(item)
            Next

            For Each item As Domain.Payroll.Entities.PersonDiagnosedDisease In DiagnosedDiseaseDatasource
                item.DiagnosedDisease = Nothing
                .ThirdParty.Person.PersonDiagnosedDisease.Add(item)
            Next

            For Each item As Domain.Payroll.Entities.PersonStudy In StudyDatasource
                item.StudyType = Nothing
                .ThirdParty.Person.PersonStudy.Add(item)
            Next

            For Each item As Domain.Payroll.Entities.PersonDisability In DisabilitiesDatasource
                item.Disability = Nothing
                .ThirdParty.Person.PersonDisability.Add(item)
            Next

            For Each item As Domain.Payroll.Entities.PersonLanguage In LanguagesDatasource
                item.Language = Nothing
                .ThirdParty.Person.PersonLanguage.Add(item)
            Next

            For Each item As Domain.Payroll.Entities.PersonProfession In ProfessionsDatasource
                item.Profession = Nothing
                .ThirdParty.Person.PersonProfession.Add(item)
            Next

            'Employee
            .ProcedureTypeRTF = CType(INDgleRetentionType.EditValue, Byte)
            .DeclarantType = CType(INDSlDeclarantType.EditValue, Byte)
            .AverageYearHealth = CType(INDtxtAverageHealthYearValue.EditValue, Decimal)
            .TradeUnion = CType(INDgleTradeUnion.EditValue, Byte)
            .Pensionary = INDglePensionary.EditValue
            .PensionaryStatus = INDglePensionaryStatus.EditValue
            .ForeignobligedQuotePension = INDGleForeignobligedQuotePension.EditValue
            .PermanentInability = INDGlePermanentInability.EditValue
            .InitialDatePermanentInability = If(INDGlePermanentInability.EditValue = False, Nothing, INDDeInitialDateInability.EditValue)
            .EndDatePermanentInability = If(INDGlePermanentInability.EditValue = False, Nothing, INDDeFinalDateInability.EditValue)
            .PensionaryTypeId = If(INDslePensionaryTypeId.EditValue Is Nothing OrElse INDslePensionaryTypeId.EditValue = -1, 0, INDslePensionaryTypeId.EditValue)
            .RetiredForeign = INDgleRetiredForeign.EditValue
            .HousingDeductionValue = If(INDtxtHousingDeductionValue.EditValue Is Nothing OrElse INDtxtHousingDeductionValue.EditValue.ToString.Trim.Equals(String.Empty), 0D, CType(INDtxtHousingDeductionValue.EditValue, Decimal))
            .EducationDeductionValue = If(INDtxtEducationDeductionValue.EditValue Is Nothing OrElse INDtxtEducationDeductionValue.EditValue.ToString.Trim.Equals(String.Empty), 0D, CType(INDtxtEducationDeductionValue.EditValue, Decimal))
            .HealthContributorRTF = If(INDTxtHealthContributorRTF.EditValue Is Nothing OrElse INDTxtHealthContributorRTF.EditValue.ToString.Trim.Equals(String.Empty), 0D, CType(INDTxtHealthContributorRTF.EditValue, Decimal))
            .State = Me.BarraBotones.StatusRecord
            .DateModified = Date.Today
            .UserModified = indigo.UserIndigo
            .SupplementaryPension = SupplementaryPensionValue
            .Relocation = INDSlRelocation.EditValue
            .InternalCode = InternalCode
            .InsuredCCSSCode = InsuredCCSSCode

            If CType(INDgleTradeUnion.EditValue, Byte) = 3 Then
                For Each item As TradeUnionEmployee In TradeUnionDatasource
                    .TradeUnionEmployee.Add(item)
                Next
            End If
            If listTradeUnionDetail IsNot Nothing AndAlso listTradeUnionDetail.Count > 0 Then
                For Each item In listTradeUnionDetail
                    .TradeUnionEmployee.Add(item)
                Next
            End If

            If .ChangeTracker.State = ObjectState.Added Then
                .AdmissionDate = Date.Today
                .VacationLastDateLiquidation = Date.Today
            End If

            If .CostCenter IsNot Nothing Then
                Dim costCenterId As Integer = Employee.CostCenterId
                Employee.CostCenter = Nothing
                Employee.CostCenterId = costCenterId
            End If

            If .WorkCenter IsNot Nothing Then
                Dim workCenterId As Integer = Employee.WorkCenterId
                Employee.WorkCenter = Nothing
                Employee.WorkCenterId = workCenterId
            End If

            For Each item As Relationship In RelationshipsDatasource
                item.Kinship = Nothing
                .Relationship.Add(item)
            Next

            For Each item As Domain.Payroll.Entities.Contract In Employee.Contract
                If item.ContractModificationReason IsNot Nothing AndAlso item.ContractModificationReason.ChangeTracker.State = ObjectState.Added Then
                    item.ContractModificationReason.ChangeTracker.State = ObjectState.Unchanged
                    item.ContractModificationReason.ChangeTracker.ChangeTrackingEnabled = False
                End If
                If item.RetirementReason IsNot Nothing AndAlso item.RetirementReason.ChangeTracker.State = ObjectState.Added Then
                    item.RetirementReason.ChangeTracker.State = ObjectState.Unchanged
                    item.RetirementReason.ChangeTracker.ChangeTrackingEnabled = False
                End If
                For Each fund As FundContract In item.FundContract
                    If fund.Fund.ChangeTracker.State = ObjectState.Added Then
                        fund.Fund = Nothing
                    End If
                Next
            Next
            .AllowJobReference = INDChkAllowJobReference.EditValue

            .ContributorAbroad = ContributorAbroad
            .DateFilingAbroad = DateFilingAbroad

        End With
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores basicos de los controles al objeto para enviar al contrato
    ''' </summary>
    Private Sub BasicAssigningValues()
        With Employee
            'Tercero
            .ThirdParty.Nit = IdPersonNumber

            'Persona
            .ThirdParty.Person.IdentificationNumber = IdPersonNumber
            .ThirdParty.Person.FirstName = INDtxtFirstName.Text.Trim
            .ThirdParty.Person.SecondName = INDtxtSecondName.Text.Trim
            .ThirdParty.Person.FirstLastName = INDtxtFirstLastname.Text.Trim
            .ThirdParty.Person.SecondLastName = INDtxtSecondLastname.Text.Trim
            .ThirdParty.Person.BirthDate = INDdteBirthDate.EditValue

            'Employee
            For Each item As Domain.Payroll.Entities.Contract In ContractDatasource
                .Contract.Add(item)
            Next

        End With
    End Sub

#Region "Validaciones"

    ''' <summary>
    ''' Valida el popup de discapacidades
    ''' </summary>
    Private Function ValidateDisabilityPopupControls() As Boolean
        ValidateDisabilityPopupControls = True
        Dim msj As String = String.Empty

        If INDsleNewDisability.EditValue Is Nothing OrElse INDsleNewDisability.EditValue.ToString.Equals(String.Empty) OrElse INDsleNewDisability.EditValue = -1 Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciNewDisability.Text) + vbCrLf
            ValidateDisabilityPopupControls = False
        End If

        If INDspeNewDisabilityPercentage.EditValue Is Nothing OrElse INDspeNewDisabilityPercentage.EditValue <= 0 Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciNewDisabilityPercentage.Text) + vbCrLf
            ValidateDisabilityPopupControls = False
        End If

        If Not ValidateDisabilityPopupControls Then
            Mensaje(EeventViewerImages.Advertencia) = msj
        End If

    End Function

    ''' <summary>
    ''' Valida el popup de idiomas
    ''' </summary>
    Private Function ValidateLanguagePopupControl() As Boolean
        ValidateLanguagePopupControl = True
        Dim msj As String = String.Empty

        If INDsleLanguageId.EditValue Is Nothing OrElse INDsleLanguageId.EditValue.ToString.Equals(String.Empty) OrElse INDsleLanguageId.EditValue = -1 Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciLanguage.Text) + vbCrLf
            ValidateLanguagePopupControl = False
        End If

        If INDspeLanguageLevel.EditValue Is Nothing OrElse INDspeLanguageLevel.EditValue <= 0 Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciLanguageLevel.Text) + vbCrLf
            ValidateLanguagePopupControl = False
        End If

        If Not ValidateLanguagePopupControl Then
            Mensaje(EeventViewerImages.Advertencia) = msj
        End If
    End Function

    ''' <summary>
    ''' Valida el popup de profesiones
    ''' </summary>
    Private Function ValidateProfessionsPopupControl() As Boolean
        ValidateProfessionsPopupControl = True
        Dim msj As String = String.Empty

        If INDsleProfessionId.EditValue Is Nothing OrElse INDsleProfessionId.EditValue.ToString.Equals(String.Empty) OrElse INDsleProfessionId.EditValue = -1 Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciProfession.Text) + vbCrLf
            ValidateProfessionsPopupControl = False
        End If

        If INDgleIsSupported.EditValue Is Nothing Then

            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciIsSupported.Text) + vbCrLf
            ValidateProfessionsPopupControl = False

        ElseIf INDgleIsSupported.EditValue = True Then

            If INDgleListOfStudies.EditValue Is Nothing Then

                msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciListOfStudies.Text) + vbCrLf
                ValidateProfessionsPopupControl = False

            End If
        End If

        If Not ValidateProfessionsPopupControl Then
            Mensaje(EeventViewerImages.Advertencia) = msj
        End If
    End Function

    ''' <summary>
    ''' Valida el popup de relaciones
    ''' </summary>
    Private Function ValidateRelationshipsPopupControl() As Boolean
        ValidateRelationshipsPopupControl = True
        Dim msj As String = String.Empty

        If INDsleKinshipId.EditValue Is Nothing OrElse INDsleProfessionId.EditValue.ToString.Equals(String.Empty) OrElse INDsleKinshipId.EditValue = -1 Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciKinship.Text) + vbCrLf
            ValidateRelationshipsPopupControl = False
        End If

        If INDtxtPersonName.Text Is Nothing OrElse INDtxtPersonName.Text.Trim.Equals(String.Empty) Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciPersonName.Text) + vbCrLf
            ValidateRelationshipsPopupControl = False
        End If

        If INDdtePersonBirthdate.EditValue Is Nothing OrElse INDdtePersonBirthdate.EditValue.Equals(String.Empty) Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciPersonBirthdate.Text) + vbCrLf
            ValidateRelationshipsPopupControl = False
        End If

        If INDgleDependent.EditValue Is Nothing OrElse String.IsNullOrEmpty(INDgleDependent.Text) Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciDependent.Text) + vbCrLf
            ValidateRelationshipsPopupControl = False
        End If

        If INDgleIsEmergencyContact.EditValue = True Then
            If INDtxtPhoneNumber.Text Is Nothing OrElse INDtxtPersonName.Text.Trim.Equals(String.Empty) Then
                msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciPhoneNumber.Text) + vbCrLf
                ValidateRelationshipsPopupControl = False
            End If
        End If

        If INDgleProvidesUpc.EditValue Is Nothing Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciProvidesUpc.Text) + vbCrLf
            ValidateRelationshipsPopupControl = False
        End If

        If INDgleProvidesUpc.EditValue IsNot Nothing AndAlso INDgleProvidesUpc.EditValue = True Then
            If INDtxtUpcValue.Text Is Nothing OrElse INDtxtUpcValue.Text.Trim.Equals(String.Empty) OrElse Integer.Parse(INDtxtUpcValue.Text) <= 0 Then
                msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciUpcValue.Text) + vbCrLf
                ValidateRelationshipsPopupControl = False
            End If
        End If

        If INDSlGenderParents.EditValue Is Nothing Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDLciGenderParents.Text) + vbCrLf
            ValidateRelationshipsPopupControl = False
        End If

        If Not ValidateRelationshipsPopupControl Then
            Mensaje(EeventViewerImages.Advertencia) = msj
        End If

    End Function

    ''' <summary>
    ''' Valida el popup de estudios
    ''' </summary>
    Private Function ValidateStudiesPopupControl() As Boolean
        ValidateStudiesPopupControl = True
        Dim msj As String = String.Empty

        'Tipo de educacion
        If INDgleIsFormal.EditValue Is Nothing Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciIsFormal.Text) + vbCrLf
            ValidateStudiesPopupControl = False
        End If

        'Tipo de estudio
        If INDsleStudyTypeID.EditValue Is Nothing OrElse INDsleStudyTypeID.EditValue = -1 Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciStudyType.Text) + vbCrLf
            ValidateStudiesPopupControl = False
        End If

        'Titulo
        If INDtxtStudyName.Text Is Nothing OrElse INDtxtStudyName.Text.Trim.Equals(String.Empty) Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciStudy.Text) + vbCrLf
            ValidateStudiesPopupControl = False
        End If

        'Centro de estudio
        If INDsleStudyCenterId.EditValue Is Nothing OrElse INDsleStudyCenterId.EditValue = -1 Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciStudyCenter.Text) + vbCrLf
            ValidateStudiesPopupControl = False
        End If

        'Ciudad
        If INDsleStudyCityId.EditValue Is Nothing OrElse INDsleStudyCityId.EditValue = -1 Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciStudyCity.Text) + vbCrLf
            ValidateStudiesPopupControl = False
        End If

        'Fecha de inicio
        If INDdteStudyStartDate.EditValue Is Nothing OrElse INDdteStudyStartDate.EditValue.Equals(String.Empty) Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciStartDate.Text) + vbCrLf
            ValidateStudiesPopupControl = False
        End If

        'Estado del estudio
        If INDgleStudyStatus.EditValue Is Nothing OrElse INDgleStudyStatus.EditValue.ToString.Equals(String.Empty) Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciStatus.Text) + vbCrLf
            ValidateStudiesPopupControl = False

        ElseIf INDgleStudyStatus.EditValue = 3 Then 'Si el estado del estudio es terminado debe ingresar fecha de terminacion
            If INDdteStudyEndingDate.EditValue Is Nothing OrElse INDdteStudyEndingDate.EditValue.ToString.Trim.Equals(String.Empty) Then
                msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciEndingDate.Text) + vbCrLf
                ValidateStudiesPopupControl = False
            End If

        ElseIf INDgleStudyStatus.EditValue = 4 Then 'Si el estado del estudio es graduado debe ingresar fecha de terminacion y graduacion
            If INDdteStudyEndingDate.EditValue Is Nothing OrElse INDdteStudyEndingDate.EditValue.ToString.Trim.Equals(String.Empty) Then
                msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciEndingDate.Text) + vbCrLf
                ValidateStudiesPopupControl = False
            End If

            If INDdteStudyGraduationDate.EditValue Is Nothing OrElse INDdteStudyGraduationDate.EditValue.ToString.Trim.Equals(String.Empty) Then
                msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciGraduationDate.Text) + vbCrLf
                ValidateStudiesPopupControl = False
            End If
        End If

        'Tarjeta profesional
        If StudyLevel.Item1 = Universitario AndAlso INDgleStudyStatus.EditValue = EmployeeHelper.StudyStatus.Where(Function(i) i.Item1 = Graduado).FirstOrDefault.Item2 Then

            If INDgleProfessionalCardOnProcess.EditValue Is Nothing Then
                msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciProfessionalCardOnProcess.Text) + vbCrLf
                ValidateStudiesPopupControl = False
            ElseIf INDgleProfessionalCardOnProcess.EditValue = False Then
                If INDtxtProfessionalCardNumber.Text Is Nothing OrElse INDtxtProfessionalCardNumber.Text.Equals(String.Empty) Then
                    msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciProfessionalCardNumber.Text) + vbCrLf
                    ValidateStudiesPopupControl = False
                End If

                If INDdteProfessionalCardExpeditionDate.EditValue Is Nothing OrElse INDdteProfessionalCardExpeditionDate.EditValue.Equals(String.Empty) Then
                    msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciProfessionalCardExpeditionDate.Text) + vbCrLf
                    ValidateStudiesPopupControl = False
                End If
            End If

        End If

        If INDgleIsInternal.EditValue Is Nothing Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciIsInternal.Text) + vbCrLf
            ValidateStudiesPopupControl = False

        ElseIf INDgleIsInternal.EditValue = True Then
            'valor del estudio
            If INDtxtStudyValue.EditValue Is Nothing OrElse INDtxtStudyValue.EditValue.ToString.Trim.Equals(String.Empty) Then
                msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciStudyValue.Text) + vbCrLf
                ValidateStudiesPopupControl = False
            End If

            'valor pagado por la empresa
            If INDtxtStudyValueCompanyPercentage.EditValue Is Nothing OrElse INDtxtStudyValueCompanyPercentage.EditValue.ToString.Trim.Equals(String.Empty) Then
                msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciStudyValueCompanyPercentage.Text) + vbCrLf
                ValidateStudiesPopupControl = False
            End If
        End If

        'Duracion
        If INDtxtStudyLength.Text Is Nothing OrElse INDtxtStudyLength.Text.Trim.Equals(String.Empty) Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciStudyLength.Text) + vbCrLf
            ValidateStudiesPopupControl = False
        End If

        'Unidad de tiempo
        If INDsleTimeUnitId.EditValue Is Nothing OrElse INDsleTimeUnitId.EditValue = -1 Then
            msj += String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciStudyTimeUnit.Text) + vbCrLf
            ValidateStudiesPopupControl = False
        End If

        If Not ValidateStudiesPopupControl Then
            Mensaje(EeventViewerImages.Advertencia) = msj
        End If
    End Function

#End Region
#End Region

End Class
