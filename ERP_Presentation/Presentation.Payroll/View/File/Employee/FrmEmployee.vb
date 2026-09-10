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
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Presentation.Controls.MVP
Imports System.Windows.Forms
Imports Presentation.CloudAgent
Imports Infrastructure.Data.Xpo.CommonRepository
Imports DevExpress.XtraGrid.Views.Grid
Imports Newtonsoft.Json
Imports DevExpress.XtraLayout
Imports PopupMenuShowingEventArgs = DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs

#End Region

''' <summary>
''' Manejo del frontal de talento humano
''' </summary>
Partial Public Class FrmEmployee
    Implements IEmployee

#Region "Globals & Properties"
    Private ExcludeWords As String() = {"Root", "Información de Contratos"}


    Private dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Private ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Private PathFunctionalDefinitions As String

    ''' <summary>
    ''' Variable que contiene el Empleado
    ''' </summary>
    Private Employee As Domain.Payroll.Entities.Employee

    ''' <summary>
    ''' Variable para modelo de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Private ModelBusqueda As New MBusqueda

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Private Presenter As PEmployee

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable para obtner la fila de la rejilla hija 
    ''' </summary>
    Private childView As GridView

    ''' <summary>
    ''' Lista global que alamacena las rentas exentas
    ''' </summary>
    Private listExemptIncomeGlobal As New List(Of CommonExemptIncomeDetailXpo)

    ''' <summary>
    ''' Lista para guardar las rentas exentas
    ''' </summary>
    Private listToSaveExemptIncome As New List(Of ExemptIncome)

    ''' <summary>
    ''' Lista para eliminar rentas exentas
    ''' </summary>
    Private listToDeleteExemptIncome As New List(Of ExemptIncome)

    ''' <summary>
    ''' Variable bandera para determinar si es una modificacion de renta
    ''' </summary>
    Private exemptIncomeModification As Boolean = False

    Private ListRelocation As List(Of Tuple(Of Boolean, String))
    ''' <summary>
    ''' Variable que contiene controles de tipo LayoutControlItem
    ''' </summary>
    Private humanTalentControls As List(Of DevExpress.XtraLayout.LayoutControlItem)

    ''' <summary>
    ''' Variable que contiene la parametrización del formulario "Parametrización Talento Humano"
    ''' </summary>
    Private controlParameterization As HumanTalentParametrization

    ''' <summary>
    ''' Propiedad que contiene el datasource de la rejilla de nacionalidades
    ''' </summary>
    Private _nationalityDatasource As List(Of Domain.Payroll.Entities.PersonNationality)
    Public Property NationalityDatasource() As List(Of Domain.Payroll.Entities.PersonNationality)
        Get
            If _nationalityDatasource Is Nothing Then
                _nationalityDatasource = New List(Of Domain.Payroll.Entities.PersonNationality)()
            End If
            Return _nationalityDatasource
        End Get
        Set(ByVal value As List(Of Domain.Payroll.Entities.PersonNationality))
            _nationalityDatasource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de la rejilla de sindicatos
    ''' </summary>
    Private _tradeUnionDatasource As List(Of TradeUnionEmployee)
    Public Property TradeUnionDatasource() As List(Of TradeUnionEmployee)
        Get
            If _tradeUnionDatasource Is Nothing Then
                _tradeUnionDatasource = New List(Of TradeUnionEmployee)()
            End If
            Return _tradeUnionDatasource
        End Get
        Set(ByVal value As List(Of TradeUnionEmployee))
            _tradeUnionDatasource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad de la rejilla de discapacidades de la persona
    ''' </summary>
    Private _disabilitiesDatasource As List(Of Domain.Payroll.Entities.PersonDisability)
    Public Property DisabilitiesDatasource() As List(Of Domain.Payroll.Entities.PersonDisability)
        Get
            If _disabilitiesDatasource Is Nothing Then
                _disabilitiesDatasource = New List(Of Domain.Payroll.Entities.PersonDisability)()
            End If
            Return _disabilitiesDatasource
        End Get
        Set(ByVal value As List(Of Domain.Payroll.Entities.PersonDisability))
            _disabilitiesDatasource = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena temporalmente la discapacidad a editar
    ''' </summary>
    Private DisabilityToEdit As Domain.Payroll.Entities.PersonDisability

    ''' <summary>
    ''' propiedad de la rejilla de idiomas de la persona
    ''' </summary>
    Private _languagesDatasource As List(Of Domain.Payroll.Entities.PersonLanguage)
    Public Property LanguagesDatasource() As List(Of Domain.Payroll.Entities.PersonLanguage)
        Get
            If _languagesDatasource Is Nothing Then
                _languagesDatasource = New List(Of Domain.Payroll.Entities.PersonLanguage)()
            End If
            Return _languagesDatasource
        End Get
        Set(ByVal value As List(Of Domain.Payroll.Entities.PersonLanguage))
            _languagesDatasource = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena temporalmente el idioma a editar
    ''' </summary>
    Private LanguageToEdit As Domain.Payroll.Entities.PersonLanguage

    ''' <summary>
    ''' propiedad de la rejilla de idiomas de la persona
    ''' </summary>
    Private _professionsDatasource As List(Of Domain.Payroll.Entities.PersonProfession)
    Public Property ProfessionsDatasource() As List(Of Domain.Payroll.Entities.PersonProfession)
        Get
            If _professionsDatasource Is Nothing Then
                _professionsDatasource = New List(Of Domain.Payroll.Entities.PersonProfession)()
            End If
            Return _professionsDatasource
        End Get
        Set(ByVal value As List(Of Domain.Payroll.Entities.PersonProfession))
            _professionsDatasource = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena temporalmente la profesion a editar
    ''' </summary>
    Private ProfessionToEdit As Domain.Payroll.Entities.PersonProfession

    ''' <summary>
    ''' propiedad de la rejilla de relaciones de la persona
    ''' </summary>
    Private _relationshipsDatasource As List(Of Relationship)
    Public Property RelationshipsDatasource() As List(Of Relationship)
        Get
            If _relationshipsDatasource Is Nothing Then
                _relationshipsDatasource = New List(Of Relationship)()
            End If
            Return _relationshipsDatasource
        End Get
        Set(ByVal value As List(Of Relationship))
            _relationshipsDatasource = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena temporalmente la relacion a editar
    ''' </summary>
    Private RelationshipToEdit As Relationship

    ''' <summary>
    ''' propiedad de la rejilla de estudios de la persona
    ''' </summary>
    Private _studyDatasource As List(Of Domain.Payroll.Entities.PersonStudy)
    Public Property StudyDatasource() As List(Of Domain.Payroll.Entities.PersonStudy)
        Get
            If _studyDatasource Is Nothing Then
                _studyDatasource = New List(Of Domain.Payroll.Entities.PersonStudy)()
            End If
            Return _studyDatasource
        End Get
        Set(ByVal value As List(Of Domain.Payroll.Entities.PersonStudy))
            _studyDatasource = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena temporalmente el estudio a editar
    ''' </summary>
    Private StudyToEdit As Domain.Payroll.Entities.PersonStudy

    ''' <summary>
    ''' propiedad de la rejilla de contratos
    ''' </summary>
    Private _contractDatasource As List(Of Domain.Payroll.Entities.Contract)
    Public Property ContractDatasource() As List(Of Domain.Payroll.Entities.Contract)
        Get
            If _contractDatasource Is Nothing Then
                _contractDatasource = New List(Of Domain.Payroll.Entities.Contract)()
            End If
            Return _contractDatasource
        End Get
        Set(ByVal value As List(Of Domain.Payroll.Entities.Contract))
            _contractDatasource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de la rejilla de actividades en tiempo libre
    ''' </summary>
    Private _freeTimeUseDatasource As List(Of Domain.Payroll.Entities.PersonFreeTimeUse)
    Public Property FreeTimeUseDatasource() As List(Of Domain.Payroll.Entities.PersonFreeTimeUse)
        Get
            If _freeTimeUseDatasource Is Nothing Then
                _freeTimeUseDatasource = New List(Of Domain.Payroll.Entities.PersonFreeTimeUse)()
            End If
            Return _freeTimeUseDatasource
        End Get
        Set(ByVal value As List(Of Domain.Payroll.Entities.PersonFreeTimeUse))
            _freeTimeUseDatasource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de la rejilla de enfermedades diagnosticadas
    ''' </summary>
    Private _diagnosedDiseaseUseDatasource As List(Of Domain.Payroll.Entities.PersonDiagnosedDisease)
    Public Property DiagnosedDiseaseDatasource() As List(Of Domain.Payroll.Entities.PersonDiagnosedDisease)
        Get
            If _diagnosedDiseaseUseDatasource Is Nothing Then
                _diagnosedDiseaseUseDatasource = New List(Of Domain.Payroll.Entities.PersonDiagnosedDisease)()
            End If
            Return _diagnosedDiseaseUseDatasource
        End Get
        Set(ByVal value As List(Of Domain.Payroll.Entities.PersonDiagnosedDisease))
            _diagnosedDiseaseUseDatasource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el tipo de identificacion
    ''' </summary>
    ''' <returns></returns>
    Public Property IdentificationType As Integer?
        Get
            Return INDgleIdType.EditValue
        End Get
        Set(value As Integer?)
            INDgleIdType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece tipo de identifiacion para el pop up de relaciones
    ''' </summary>
    ''' <returns></returns>
    Public Property IdentificationTypeRelationship As Integer?
        Get
            Return INDgleIdentificationType.EditValue
        End Get
        Set(value As Integer?)
            INDgleIdentificationType.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para la pension complementaria
    ''' </summary>
    ''' <returns></returns>
    Public Property SupplementaryPensionValue As Decimal
        Get
            Return If(String.IsNullOrEmpty(INDtxtSupplementaryPensionValue.EditValue), 0D, INDtxtSupplementaryPensionValue.EditValue)
        End Get
        Set(value As Decimal)
            INDtxtSupplementaryPensionValue.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para almacenar el Nit del empleado
    ''' </summary>
    ''' <returns></returns>
    Public Property IdPersonNumber As String
        Get
            Return INDbteIdNumber.EditValue
        End Get
        Set(value As String)
            INDbteIdNumber.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que obtiene o establece el numero de identificacion para el popup de relaciones
    ''' </summary>
    ''' <returns></returns>
    Public Property IdentificationNumberRelationship As String
        Get
            Return INDtxeIdentificationNumber.EditValue
        End Get
        Set(value As String)
            INDtxeIdentificationNumber.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el cotizante en el exterior
    ''' </summary>
    ''' <returns></returns>
    Public Property ContributorAbroad As Boolean
        Get
            Return INDSlContributorAbroad.EditValue
        End Get
        Set(value As Boolean)
            INDSlContributorAbroad.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece la fecha de radicacion en el exterior
    ''' </summary>
    ''' <returns></returns>
    Public Property DateFilingAbroad As Date?
        Get
            Return INDDeDateFilingAbroad.EditValue
        End Get
        Set(value As Date?)
            INDDeDateFilingAbroad.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene y establece el código asegurado CCSS
    ''' </summary>
    ''' <returns></returns>
    Public Property InsuredCCSSCode As String
        Get
            Return INDtxtCodigoCCSS.EditValue
        End Get
        Set(value As String)
            INDtxtCodigoCCSS.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo interno del empleado
    ''' </summary>
    ''' <returns></returns>
    Public Property InternalCode As String
        Get
            Return INDtxtInternalCode.EditValue
        End Get
        Set(value As String)
            INDtxtInternalCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena temporalmente el contrato a editar
    ''' </summary>
    Private ContractToEdit As Domain.Payroll.Entities.Contract

    ''' <summary>
    ''' Variable que contiene las acciones del contrato
    ''' </summary>
    Private ContractAction As ContractActions

    ''' <summary>
    ''' Marca el empleado como nuevo o no, se usa para dar valores iniciales a variables como la fecha de creacio y demas
    ''' </summary>
    Private NewEmployee As Boolean

    ''' <summary>
    ''' Variable que contiene le nivel de estudio seleccionado
    ''' </summary>
    Private StudyLevel As Tuple(Of Integer, Integer, String)

    ''' <summary>
    ''' Propiedad del datasource de ciudades de nacimiento
    ''' </summary>
    Public WriteOnly Property BirthCitiesDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.BirthCitiesDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleBirthCity.Properties.DataSource = value
        End Set
    End Property

    Public WriteOnly Property ExemptIncomeDataSourceXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.ExemptIncomeXpo
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDgcExemptIncome.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del datasource de creencias religiosas
    ''' </summary>
    Public WriteOnly Property ReligiousBeliefsDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.ReligiousBeliefsDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleReligiousBeliefs.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del datasource de creencias religiosas
    ''' </summary>
    Public WriteOnly Property EthnicGroupsDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.EthnicGroupsDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleEthnicGroups.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del datasource de ciudades de expedicion
    ''' </summary>
    Public WriteOnly Property ExpeditionCitiesDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.ExpeditionCitiesDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIdExpeditionCityId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del datasource de ciudades de estudio
    ''' </summary>
    Public WriteOnly Property StudyCitiesDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.StudyCitiesDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleStudyCityId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del datasource de discapacidades
    ''' </summary>
    Public WriteOnly Property DisabilitiesDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.DisabilitiesDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleNewDisability.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del datasource de parentezco
    ''' </summary>
    Public WriteOnly Property KinshipsDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.KinshipsDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleKinshipId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del datasource de idiomas
    ''' </summary>
    Public WriteOnly Property LanguagesDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.LanguagesDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleLanguageId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del datasource de tipo de pensionado
    ''' </summary>
    Public WriteOnly Property PensionaryTypesDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.PensionaryTypesDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDslePensionaryTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del datasource de profesiones
    ''' </summary>
    Public WriteOnly Property ProfessionsDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.ProfessionsDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleProfessionId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del datasource de centro de estudio
    ''' </summary>
    Public WriteOnly Property StudyCentersDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.StudyCentersDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleStudyCenterId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del datasource de tipo de estudio
    ''' </summary>
    Public WriteOnly Property StudyTypesDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.StudyTypesDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleStudyTypeID.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del datasource de unidades de tiempo
    ''' </summary>
    Public WriteOnly Property TimeUnitsDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.UnitsTimeDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleTimeUnitId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de paises para obtener la nacionalidad (XPO)
    ''' </summary>
    Public WriteOnly Property NationalityDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.NationalityDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleNationality.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de actividades en tiempo libre (XPO)
    ''' </summary>
    Public WriteOnly Property FreeTimeUseDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.FreeTimeUseDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleFreeTimeUse.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de enfermedades diagnosticadas (XPO)
    ''' </summary>
    Public WriteOnly Property DiagnosedDiseaseDataSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.DiagnosedDiseaseDataSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleDiagnosedDisease.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que contiene el listado de sindicatos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property tradeUnionDatasourceXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IEmployee.tradeUnionDatasourceXpo
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleTradeUnion.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para el controlde fechas dentro del popup de rentas
    ''' </summary>
    ''' <returns></returns>
    Public Property DateExemptIcome As Date Implements IEmployee.DateExemptIcome
        Get
            Return INDdteDateExempt.EditValue
        End Get
        Set(value As Date)
            INDdteDateExempt.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el control dentro de valor de renta del popup de rentas
    ''' </summary>
    ''' <returns></returns>
    Public Property ExemptIncomeValue As Decimal Implements IEmployee.ExemptIncomeValue
        Get
            Return INDtxtValueExemptIncome.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValueExemptIncome.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para el control de comentarios dentro del popup de rentas
    ''' </summary>
    ''' <returns></returns>
    Public Property ExemptIncomeComment As String Implements IEmployee.ExemptIncomeComment
        Get
            Return INDmemoComments.EditValue
        End Get
        Set(value As String)
            INDmemoComments.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Variable que maneja el listado de los telefonos agregados al control
    ''' </summary>
    Private ListadoEliminadosTelefono As New List(Of Domain.Payroll.Entities.Phone)

    ''' <summary>
    '''  Variable que maneja el listado de los Direccions agregados al control
    ''' </summary>
    Private ListadoEliminadosDireccion As New List(Of Domain.Payroll.Entities.Address)

    ''' <summary>
    '''  Variable que maneja el listado de los Email agregados al control
    ''' </summary>
    Private ListadoEliminadosEmail As New List(Of Domain.Payroll.Entities.Email)

    Private ListadoEliminadosNacionalidades As New List(Of Domain.Payroll.Entities.PersonNationality)

    Private ListadoEliminadosActividadesTiempoLibre As New List(Of Domain.Payroll.Entities.PersonFreeTimeUse)

    Private ListadoEliminadosEnfermedadesDiagnosticadas As New List(Of Domain.Payroll.Entities.PersonDiagnosedDisease)

    Private ListadoEliminadosLanguage As New List(Of Domain.Payroll.Entities.PersonLanguage)

    Private ListadoEliminadosDisability As New List(Of Domain.Payroll.Entities.PersonDisability)

    Private ListadoEliminadosStudy As New List(Of Domain.Payroll.Entities.PersonStudy)

    Private ListadoEliminadosProfession As New List(Of Domain.Payroll.Entities.PersonProfession)

    Private ListadoEliminadosRelationship As New List(Of Domain.Payroll.Entities.Relationship)

    ''' <summary>
    ''' variable que maneja el listado de sindicatos eliminados
    ''' </summary>
    ''' <remarks></remarks>
    Private listTradeUnionDetail As New List(Of TradeUnionEmployee)

    ''' <summary>
    ''' Variable Bandera para saber si se activa el control de Aporte a Salud del Año Pasado o no deacuerdo a los parámetros de Nómina
    ''' </summary>
    ''' <remarks></remarks>
    Private RTFFlag As Boolean = False

    ''' <summary>
    ''' Variable que contiene el tipo de identificacion
    ''' </summary>
    Private IdentificationTypeXpo As ADTIPOIDENTIFICAXpo

#End Region

#Region "Bar Buttons Events"

    ''' <summary>
    '''Evento load de la barra de botones
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        INDlcgActualContractInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Dim ContractPermiso = IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsUserToolbar(indigo.UserIndigoId, indigo.UserRol, CStr(MyBase.Tag), indigo)
        For i As Integer = 0 To ContractPermiso.Count() - 1
            If ContractPermiso.Item(i).TagButton = 38 Then
                INDlcgActualContractInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        Next
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
        viewExemptIncome.CollapseAllDetails() 'Colapsa la grilla de detalle en rentas exentas
        Nuevo()
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
    ''' Barra de botones_ click imprimir
    ''' </summary>
    Private Async Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Await LoadReport(PrintReportAction.DirectPrinting)
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

#End Region

#Region "Customization"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyCtrHumanTalent.ShowCustomizationForm()
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
            INDlyCtrHumanTalent.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCtrHumanTalent.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MEmployee(MEmployee.TAG)
                Dim dsFields As DataSet = model.GetFieldsNULL
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyCtrHumanTalent.Items.Count - 1
                        INDlyCtrHumanTalent.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyCtrHumanTalent.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyCtrHumanTalent.Items.Item(j).Tag.ToString.Trim Then
                                    INDlyCtrHumanTalent.Items.Item(j).AllowHide = True
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
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCtrHumanTalent.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyCtrHumanTalent.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyCtrHumanTalent.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDlyCtrHumanTalent.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub INDsleBirthCity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBirthCity.QueryPopUp
        If INDsleBirthCity.Properties.DataSource Is Nothing Then
            BirthCitiesDataSourceXPO = ModelBusqueda.ConsultarEntidades(eDataSource.AllCity)
        End If
    End Sub

    Private Async Sub FrmEmployee_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

    Private Sub INDsleIdExpeditionCityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIdExpeditionCityId.QueryPopUp
        If INDsleIdExpeditionCityId.Properties.DataSource Is Nothing Then
            ExpeditionCitiesDataSourceXPO = ModelBusqueda.ConsultarEntidades(eDataSource.AllCity)
        End If
    End Sub

    Private Sub INDslePensionaryTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePensionaryTypeId.QueryPopUp
        If INDslePensionaryTypeId.Properties.DataSource Is Nothing Then
            PensionaryTypesDataSourceXPO = ModelBusqueda.ConsultarEntidades(eDataSource.PensionaryType)
        End If
    End Sub

    Private Sub INDsleNewDisability_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleNewDisability.QueryPopUp
        If INDsleNewDisability.Properties.DataSource Is Nothing Then
            DisabilitiesDataSourceXPO = ModelBusqueda.ConsultarEntidades(eDataSource.Disability)
        End If
    End Sub

    Private Sub INDsleLanguageId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleLanguageId.QueryPopUp
        If INDsleLanguageId.Properties.DataSource Is Nothing Then
            LanguagesDataSourceXPO = ModelBusqueda.ConsultarEntidades(eDataSource.Language)
        End If
    End Sub

    Private Sub INDsleProfessionId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProfessionId.QueryPopUp
        If INDsleProfessionId.Properties.DataSource Is Nothing Then
            ProfessionsDataSourceXPO = ModelBusqueda.ConsultarEntidades(eDataSource.Professions)
        End If
    End Sub

    Private Sub INDsleNationality_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleNationality.QueryPopUp
        If INDsleNationality.Properties.DataSource Is Nothing Then
            NationalityDataSourceXPO = ModelBusqueda.ConsultarEntidades(eDataSource.Country)
        End If
    End Sub

    Private Sub INDsleFreeTimeUse_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFreeTimeUse.QueryPopUp
        If INDsleFreeTimeUse.Properties.DataSource Is Nothing Then
            FreeTimeUseDataSourceXPO = ModelBusqueda.ConsultarEntidades(eDataSource.FreeTimeUse)
        End If
    End Sub


    Private Sub INDsleDiagnosedDisease_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDiagnosedDisease.QueryPopUp
        If INDsleDiagnosedDisease.Properties.DataSource Is Nothing Then
            DiagnosedDiseaseDataSourceXPO = ModelBusqueda.ConsultarEntidades(eDataSource.DiagnosedDisease)
        End If
    End Sub

    Private Sub INDsleKinshipId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleKinshipId.QueryPopUp
        If INDsleKinshipId.Properties.DataSource Is Nothing Then
            KinshipsDataSourceXPO = ModelBusqueda.ConsultarEntidades(eDataSource.Kinship)
        End If
    End Sub

    Private Sub INDsleTradeUnion_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleTradeUnion.QueryPopUp
        Dim filter() As Object = {True}
        If INDsleTradeUnion.Properties.DataSource Is Nothing Then
            tradeUnionDatasourceXpo = ModelBusqueda.ConsultarEntidades(eDataSource.ListTradeUnionByStatus, filter)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el formulario Creencias Religiosas
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDsleReligiousBeliefs_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleReligiousBeliefs.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormTransparentType(GetType(FrmReligiousBeliefs))
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el formulario Grupos Étnicos
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDsleEthnicGroups_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEthnicGroups.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormTransparentType(GetType(FrmEthnicGroups))
        End If
    End Sub
    Private Sub INDsleIdExpeditionCityId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIdExpeditionCityId.ButtonClick, INDsleBirthCity.ButtonClick, INDsleStudyCityId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormTransparentType(GetType(FrmCity))
        End If
    End Sub


    Private Sub INDsleTradeUnion_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleTradeUnion.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormTransparentType(GetType(FrmSyndicate))
        End If
    End Sub

    Private Sub INDsleNationality_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleNationality.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormTransparentType(GetType(FrmCountry))
        End If
    End Sub

    Private Sub INDsleFreeTimeUse_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFreeTimeUse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormTransparentType(GetType(FrmFreeTimeUse))
        End If
    End Sub

    Private Sub INDsleDiagnosedDisease_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleDiagnosedDisease.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormTransparentType(GetType(FrmDiagnosedDisease))
        End If
    End Sub

    Private Sub INDslePensionaryTypeId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePensionaryTypeId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormTransparentType(GetType(FrmPensionaryType))
        End If
    End Sub

    Private Sub INDsleNewDisability_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleNewDisability.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormTransparentType(GetType(FrmDisability))
        End If
    End Sub

    Private Sub INDsleLanguageId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleLanguageId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormTransparentType(GetType(FrmLanguage))
        End If
    End Sub

    Private Sub INDsleStudyTypeID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleStudyTypeID.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormTransparentType(GetType(FrmStudyType))
        End If
    End Sub

    Private Sub INDsleStudyCenterId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleStudyCenterId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormTransparentType(GetType(FrmStudyCenter))
        End If
    End Sub

    Private Sub INDsleTimeUnitId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleTimeUnitId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormTransparentType(GetType(FrmTimeUnit))
        End If
    End Sub

    Private Sub INDsleProfessionId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProfessionId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormTransparentType(GetType(FrmProfessions))
        End If
    End Sub

    Private Sub INDsleKinshipId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleKinshipId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormTransparentType(GetType(FrmKinship))
        End If
    End Sub

    Private Sub OpenFormTransparentType(ByVal formType As Type)
        Dim form = CType(Activator.CreateInstance(formType), FormBase)
        form.ViewModeEditHold = True
        form.StartPosition = FormStartPosition.CenterScreen
        Dim formTransparent As New FrmTransparent(form, False)
        formTransparent.ShowDialog()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Employee = Nothing
        ModelBusqueda = Nothing
        Presenter = Nothing
        record = Nothing
        _nationalityDatasource = Nothing
        _freeTimeUseDatasource = Nothing
        _diagnosedDiseaseUseDatasource = Nothing
        ContractToEdit = Nothing
        ContractAction = Nothing
        NewEmployee = Nothing
        StudyLevel = Nothing
        ListadoEliminadosTelefono = Nothing
        ListadoEliminadosDireccion = Nothing
        ListadoEliminadosEmail = Nothing
        ListadoEliminadosNacionalidades = Nothing
        ListadoEliminadosLanguage = Nothing
        ListadoEliminadosDisability = Nothing
        ListadoEliminadosStudy = Nothing
        ListadoEliminadosProfession = Nothing
        ListadoEliminadosRelationship = Nothing
        listTradeUnionDetail = Nothing
        RTFFlag = Nothing
        _validIdentificationType = Nothing
    End Sub

    Private Sub INDgleTradeUnion_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleTradeUnion.EditValueChanged
        If INDgleTradeUnion.EditValue = 3 Then
            If TradeUnionDatasource.Count > 0 Then
                GridControl1.DataSource = TradeUnionDatasource
            End If

            INDlciNewTradeUnion.ShowLayout()
        Else
            'eliminamos los registros de la lista
            If TradeUnionDatasource.Any() Then
                While TradeUnionDatasource.Count > 0
                    If TradeUnionDatasource.Item(0).Id > 0 Then
                        If listTradeUnionDetail Is Nothing Then
                            listTradeUnionDetail = New List(Of TradeUnionEmployee)
                        End If

                        TradeUnionDatasource.Item(0).MarkAsDeleted()
                        listTradeUnionDetail.Add(TradeUnionDatasource.Item(0))
                    End If

                    TradeUnionDatasource.Remove(TradeUnionDatasource.Item(0))
                End While
            End If
            INDlciNewTradeUnion.HideLayout()
            GridControl1.DataSource = TradeUnionDatasource
            GridControl1.RefreshDataSource()
            INDsleTradeUnion.EditValue = -1
        End If
    End Sub

    Private Sub INDgleMilitaryCardType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleMilitaryCardType.EditValueChanged
        If INDgleMilitaryCardType.EditValue = 0 Then
            INDtxtMilitaryCardNumber.Text = String.Empty
        End If
    End Sub

    Private Sub INDgleRetentionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleRetentionType.EditValueChanged
        If RTFFlag Then
            INDlciAverageHealthLastYearValue.ShowLayout()
        Else
            INDlciAverageHealthLastYearValue.HideLayout()
        End If
    End Sub

    Private Sub INDgleDependent_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleDependent.EditValueChanged
        If INDgleDependent.EditValue = "1" Then
            IndLciOptionDependsValue.ShowLayout()
        Else
            INDSlDependsTypeValue.EditValue = 1
            INDlciDependsValue.HideLayout()
            INDlciDependsValue.HideLayout()
            IndLciOptionDependsValue.HideLayout()
            INDLciPercentageDepends.HideLayout()
        End If
    End Sub

    Private Async Sub FrmEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Using Model As New MPayrollSettings(MEmployee.TAG)
                AsyncLoader(True)
                Dim PayrollSettings = Await Model.GetParameters()
                AsyncLoader(False)
                If PayrollSettings IsNot Nothing Then
                    If PayrollSettings.RtfHealthContribution IsNot Nothing AndAlso PayrollSettings.RtfHealthContribution = 2 Then
                        RTFFlag = indigo.LanguageCulture <> "es-CR"
                    End If
                End If
            End Using

            Using Model As New MHumanTalentParameterization(Me.Tag)
                AsyncLoader(True)
                result = Await Model.GetHumanTalentParameterizationAsync()
                controlParameterization = JsonConvert.DeserializeObject(Of HumanTalentParametrization)(result.JSONdata)

                For Each item In controlParameterization.Items
                    Dim controlName As String = item.ItemName
                    Dim control = FindControlByName(controlName)

                    If control IsNot Nothing Then
                        If Not item.Visible Then
                            control.HideLayout()
                        Else
                            control.ShowLayout()
                        End If

                        If item.Obligatory Then
                            control.Control.CausesValidation = True

                            control.AllowHide = False
                            control.ShowInCustomizationForm = False
                            control.ShowLayout()
                            If Not TypeOf control.Control Is DevExpress.XtraEditors.DropDownButton Then
                                Me.IndigoTextEdit1.SetCampoObligatorio(control.Control, True)
                            End If

                        Else
                            control.Control.CausesValidation = False
                            control.AllowHide = True
                            If TypeOf control.Control Is DevExpress.XtraEditors.TextEdit Then
                                Me.IndigoTextEdit1.SetCampoObligatorio(control.Control, False)
                            End If
                        End If

                    End If

                Next

                ValidateShowGrid()
                AsyncLoader(False)
            End Using

            Dim ContractPermiso = IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsUserToolbar(indigo.UserIndigoId, indigo.UserRol, CStr(MyBase.Tag), indigo)
            For i As Integer = 0 To ContractPermiso.Count() - 1
                If ContractPermiso.Item(i).TagButton = 38 Then
                    INDlcgActualContractInfo.HideControl(False)
                Else
                    INDlcgActualContractInfo.HideControl()
                End If
            Next

            INDlcgActualContractInfo.HideControl(False)
            ShowButtonSuprementaryPension()

            CreateListRelocation()
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Función que recorre los controles del formulario y devuelve el control el cual se encuentre en el JSON de parametrización
    ''' </summary>
    ''' <param name="controlName"></param>
    ''' <returns></returns>
    Private Function FindControlByName(controlName As String) As DevExpress.XtraLayout.LayoutControlItem
        Dim frmControls = GethumanTalentControls()
        For Each ctrl In frmControls
            ' Verificar si el nombre del control coincide con el nombre buscado
            If ctrl.Name = controlName Then
                Return ctrl
            End If
        Next
        Return Nothing
    End Function

    Private Sub INDSlDependsTypeValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlDependsTypeValue.EditValueChanged
        If INDSlDependsTypeValue.EditValue = 1 Then
            INDlciDependsValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciPercentageDepends.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDlciDependsValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciPercentageDepends.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    Private Sub CreateListRelocation()
        ListRelocation = New List(Of Tuple(Of Boolean, String))
        ListRelocation.Add(New Tuple(Of Boolean, String)(False, "No"))
        ListRelocation.Add(New Tuple(Of Boolean, String)(True, "Si"))

        INDSlRelocation.Properties.DataSource = ListRelocation.ToList()
    End Sub

    Private Sub INDsleReligiousBeliefs_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleReligiousBeliefs.QueryPopUp
        If INDsleReligiousBeliefs.Properties.DataSource Is Nothing Then
            ReligiousBeliefsDataSourceXPO = ModelBusqueda.ConsultarEntidades(eDataSource.ReligiousBeliefs)
        End If
    End Sub

    Private Sub INDsleEthnicGroups_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleEthnicGroups.QueryPopUp
        If INDsleEthnicGroups.Properties.DataSource Is Nothing Then
            EthnicGroupsDataSourceXPO = ModelBusqueda.ConsultarEntidades(eDataSource.EthnicGroups)
        End If
    End Sub

    ''' <summary>
    ''' funcion que valida si el tipo de docuemtno es c.c o t.i muestre el campo cotizante en el exterior 
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateContributorAbroad() As Boolean
        If IdentificationTypeXpo?.SIGLA = "CC" Or IdentificationTypeXpo?.SIGLA = "TI" Then
            INDLciContributorAbroad.ShowLayout()
            Return True
        Else
            INDLciContributorAbroad.HideLayout()
            INDLcDateFilingAbroad.HideLayout()
            ContributorAbroad = 0
            Return False
        End If
    End Function

    ''' <summary>
    ''' funcion que valida si el campo de cotizante esta en si muestre el campo de fecha de radicacion en el exterior
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateDateFilingAbroad() As Boolean
        If ContributorAbroad Then
            INDLcDateFilingAbroad.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Return True
        Else
            INDLcDateFilingAbroad.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Return False
        End If
    End Function

    Private Async Sub INDgleIdType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleIdType.EditValueChanged
        If IdentificationType Is Nothing OrElse Not NewEmployee Then Exit Sub

        Await Me.NitLengthValidator(Me.IdentificationType, IdPersonNumber, NewEmployee)
    End Sub

    ''' <summary>
    ''' Valida la logitud del nit dependiendo del tipo de indentificacion
    ''' </summary>
    ''' <param name="identificationTypeId"></param>
    ''' <param name="thirdPartyNit"></param>
    ''' <param name="flagLoadControls"></param>
    ''' <returns></returns>
    Public Async Function NitLengthValidator(identificationTypeId As Integer?, thirdPartyNit As String, flagLoadControls As Boolean) As Task

        If identificationTypeId Is Nothing Then
            Return
        End If

        If INDgleIdType.Properties.DataSource Is Nothing Then
            Using Model As New Common.MVP.MThirdParty("")
                IdentificationTypeXpo = Await Model.GetADTIPOIDENTIFICAXpoById(identificationTypeId)
            End Using
        Else
            IdentificationTypeXpo = TryCast(TryCast(INDGvIdentificationType.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ADTIPOIDENTIFICAXpo)
        End If

        Await Me.ValidateIdentificationType(thirdPartyNit, identificationTypeId, IdentificationTypeXpo?.SIGLA, If(Employee?.ThirdParty?.Person?.Id = 0, True, False))
    End Function

    ''' <summary>
    ''' funcion centralizada de consultar y validar la longitud de caracteres por tipo de identificacion
    ''' </summary>
    ''' <param name="thirdPartyNit"></param>
    ''' <param name="abbreviation"></param>
    ''' <returns></returns>
    Private Async Function ValidateIdentificationType(thirdPartyNit As String,
                                                      identificationTypeId As Integer,
                                                      Optional abbreviation As String = Nothing,
                                                      Optional isNew As Boolean = True) As Task(Of Boolean)
        Try
            AsyncLoader(True)
            _validIdentificationType = Nothing
            INDbteIdNumber.Properties.MaxLength = 25

            If String.IsNullOrEmpty(thirdPartyNit) OrElse (String.IsNullOrEmpty(abbreviation) AndAlso identificationTypeId = 0) Then
                Return False
            End If

            Using Model As New Common.MVP.MThirdParty("")

                If IdentificationTypeXpo Is Nothing _
                    OrElse IdentificationTypeXpo?.ID <> identificationTypeId Then
                    IdentificationTypeXpo = Await Model.GetADTIPOIDENTIFICAXpoById(identificationTypeId)
                    abbreviation = IdentificationTypeXpo?.SIGLA
                End If

                If IdentificationTypeXpo?.SIGLA <> abbreviation Then
                    abbreviation = IdentificationTypeXpo?.SIGLA
                End If

                _validIdentificationType = Await Model.ValidateLenghtNit(thirdPartyNit, abbreviation)
            End Using

            If _validIdentificationType Is Nothing OrElse Not _validIdentificationType.StateResult Then
                Mensaje(EeventViewerImages.Advertencia) = _validIdentificationType?.Message
                If isNew Then
                    Me.IdPersonNumber = String.Empty
                    INDbteIdNumber.Properties.MaxLength = IdentificationTypeXpo.MaximumLength
                    INDbteIdNumber.Enabled = True
                    INDbteIdNumber.Focus()
                End If
                Return False
            End If

            INDbteIdNumber.Enabled = False
            Return True
        Catch ex As Exception
            _validIdentificationType = New ActionResult(Of Boolean) With {.StateResult = False, .Message = $"Error comunicación al servidor: {Utils.GetInnerExceptionMessages(ex)}"}
            Return False
        Finally
            AsyncLoader(False)
        End Try
    End Function

    Private Sub INDSlContributorAbroad_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlContributorAbroad.EditValueChanged
        ValidateDateFilingAbroad()
    End Sub

    ''' <summary>
    ''' Función que recorre todos los controles y los incluye dentro del extendido para armar el archivo JSON de parametrización
    ''' </summary>
    ''' <returns></returns>
    Public Function GetControls() As HumanTalentParametrization
        Dim humanTalentParametrization As New HumanTalentParametrization()
        ' Obtener el control LayoutControl del formulario
        Dim layoutControl As DevExpress.XtraLayout.LayoutControl = CType(INDlyCtrHumanTalent, DevExpress.XtraLayout.LayoutControl)

        ' Recorrer los elementos dentro del LayoutControl
        For Each item As DevExpress.XtraLayout.BaseLayoutItem In layoutControl.Items
            If TypeOf item Is DevExpress.XtraLayout.LayoutControlGroup Then

            End If
            ' Es un grupo (LayoutControlGroup)
            Dim group As DevExpress.XtraLayout.LayoutControlGroup = TryCast(item, DevExpress.XtraLayout.LayoutControlGroup)

            If group IsNot Nothing AndAlso Not String.IsNullOrEmpty(group.Text) AndAlso Not ExcludeWords.Contains(group.Text) Then
                ' Verifica si el nombre del grupo es uno de los tabs específicos
                If group.Text = "Extranjero Cotiza Pensión" OrElse group.Text = "Incapacitado Permanente" Then
                    ' Agrega los elementos al tab "Información de pensionado"
                    Dim informacionPensionadoTab = humanTalentParametrization.Tabs _
                        .FirstOrDefault(Function(tab) tab.TabName = "Información de Pensionado")

                    If informacionPensionadoTab IsNot Nothing Then
                        ' Recorrer los elementos (LayoutControlItem) dentro del grupo
                        For Each subItem As DevExpress.XtraLayout.BaseLayoutItem In group.Items
                            If Not TypeOf subItem Is DevExpress.XtraLayout.EmptySpaceItem Then
                                If TypeOf subItem Is DevExpress.XtraLayout.LayoutControlItem Then
                                    ' Es un elemento (LayoutControlItem)
                                    Dim layoutControlItem As DevExpress.XtraLayout.LayoutControlItem = TryCast(subItem, DevExpress.XtraLayout.LayoutControlItem)
                                    If Not TypeOf layoutControlItem.Control Is DevExpress.XtraGrid.GridControl Then
                                        humanTalentParametrization.Items.Add(New HumanTalentParametrization.Item With
                                    {
                                     .ItemText = layoutControlItem.Text,
                                     .ItemName = layoutControlItem.Name,
                                     .Control = layoutControlItem.Control.ToString(),
                                     .LayaoutContolItem = layoutControlItem.ToString(),
                                     .Mandatory = False,
                                     .Visible = True,
                                     .Obligatory = False,
                                     .LcgName = informacionPensionadoTab.TabName
                                    })
                                    End If
                                End If
                            End If
                        Next
                    End If
                Else

                    If group IsNot Nothing AndAlso Not String.IsNullOrEmpty(group.Text) AndAlso Not ExcludeWords.Contains(group.Text) Then
                        humanTalentParametrization.Tabs.Add(New HumanTalentParametrization.Tab With
                                                            {
                                                            .TabName = group.Text,
                                                            .Code = group.Name
                                                            })

                        ' Recorrer los elementos (LayoutControlItem) dentro del grupo
                        For Each subItem As DevExpress.XtraLayout.BaseLayoutItem In group.Items
                            If Not TypeOf subItem Is DevExpress.XtraLayout.EmptySpaceItem Then
                                If TypeOf subItem Is DevExpress.XtraLayout.LayoutControlItem AndAlso Not ExcludeWords.Contains(group.Text) Then
                                    ' Es un elemento (LayoutControlItem)
                                    Dim layoutControlItem As DevExpress.XtraLayout.LayoutControlItem = TryCast(subItem, DevExpress.XtraLayout.LayoutControlItem)
                                    If Not TypeOf layoutControlItem.Control Is DevExpress.XtraGrid.GridControl Then
                                        humanTalentParametrization.Items.Add(New HumanTalentParametrization.Item With
                                                                         {
                                                                         .ItemText = layoutControlItem.Text,
                                                                         .Control = layoutControlItem.Control.ToString(),
                                                                         .LayaoutContolItem = layoutControlItem.ToString(),
                                                                         .ItemName = layoutControlItem.Name,
                                                                         .Mandatory = False,
                                                                         .Visible = True,
                                                                         .Obligatory = False,
                                                                         .LcgName = group.Text
                                                                         })
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        Next
        Return humanTalentParametrization
    End Function

    ''' <summary>
    ''' Función que recorre todos los controles del formulario y devuelve solos los controles de tipo LayoutControlItem
    ''' </summary>
    ''' <returns></returns>
    Private Function GethumanTalentControls()
        ' Obtener el control LayoutControl del formulario
        Dim layoutControl As DevExpress.XtraLayout.LayoutControl = CType(INDlyCtrHumanTalent, DevExpress.XtraLayout.LayoutControl)
        Dim humanTalentControls As New List(Of DevExpress.XtraLayout.LayoutControlItem)()

        ' Recorrer los elementos dentro del LayoutControl
        For Each item As DevExpress.XtraLayout.BaseLayoutItem In layoutControl.Items
            If TypeOf item Is DevExpress.XtraLayout.LayoutControlGroup Then

            End If
            ' Es un grupo (LayoutControlGroup)
            Dim group As DevExpress.XtraLayout.LayoutControlGroup = TryCast(item, DevExpress.XtraLayout.LayoutControlGroup)

            If group IsNot Nothing AndAlso Not String.IsNullOrEmpty(group.Text) Then

                ' Recorrer los elementos (LayoutControlItem) dentro del grupo
                For Each subItem As DevExpress.XtraLayout.BaseLayoutItem In group.Items
                    If Not TypeOf subItem Is DevExpress.XtraLayout.EmptySpaceItem Then
                        If TypeOf subItem Is DevExpress.XtraLayout.LayoutControlItem Then
                            ' Es un elemento (LayoutControlItem)
                            Dim layoutControlItem As DevExpress.XtraLayout.LayoutControlItem = TryCast(subItem, DevExpress.XtraLayout.LayoutControlItem)
                            If Not TypeOf layoutControlItem.Control Is DevExpress.XtraGrid.GridControl Then
                                humanTalentControls.Add(layoutControlItem)
                            End If
                        End If
                    End If
                Next
            End If
        Next
        Return humanTalentControls
    End Function

    ''' <summary>
    ''' Método que valida si los layoutcontrolgroup tienen items, si no tienen se oculta el layout
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDlcgPersonalIdInfo_CustomDraw(sender As Object, e As DevExpress.XtraLayout.ItemCustomDrawEventArgs) Handles INDlcgPersonalIdInfo.CustomDraw, INDlcgPersonalInfo.CustomDraw, INDlcgPersonalInfo2.CustomDraw, INDlcgGeneralInfo.CustomDraw, INDlcgPensionaryInfo.CustomDraw, INDlcgDeductionInfo.CustomDraw, INDLcGroupForeignobligedQuotePension.CustomDraw, INDLcGroupPermanentInability.CustomDraw, INDlcgDisabilityAndLanguageInfo.CustomDraw, INDlcgStudyAndProfessionInfo.CustomDraw, INDlcgFamilyInfo.CustomDraw
        Dim group As DevExpress.XtraLayout.LayoutControlGroup = TryCast(sender, DevExpress.XtraLayout.LayoutControlGroup)
        If group IsNot Nothing Then
            Dim allItemsHidden As Boolean = True
            For Each item As DevExpress.XtraLayout.BaseLayoutItem In group.Items
                If Not TypeOf item Is DevExpress.XtraLayout.EmptySpaceItem Then
                    If TypeOf item Is DevExpress.XtraLayout.LayoutControlItem AndAlso item.Visible Then
                        allItemsHidden = False
                        Exit For
                    End If
                End If
            Next
            group.Visibility = If(allItemsHidden, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        End If
    End Sub

    ''' <summary>
    ''' Método que valida la visibilidad del botón asociado a la rejilla y cambia el estado visible de esta
    ''' </summary>
    Private Sub ValidateShowGrid()

        ' Define una lista de tuplas que asocian los controles con sus respectivos LayoutControlItems
        Dim controlItemPairs As New List(Of Tuple(Of LayoutControlItem, LayoutControlItem)) From {
        Tuple.Create(Of LayoutControlItem, LayoutControlItem)(INDlciFreeTimeUse, INDlciFreeTime),
        Tuple.Create(Of LayoutControlItem, LayoutControlItem)(INDlciLanguages, INDlciLanguage),
        Tuple.Create(Of LayoutControlItem, LayoutControlItem)(INDlciNationalities, INDlciNationalityPopup),
        Tuple.Create(Of LayoutControlItem, LayoutControlItem)(INDlciDisabilities, INDlciDisability),
        Tuple.Create(Of LayoutControlItem, LayoutControlItem)(INDlciDiagnosedDiseaseMain, LayoutControlItem5),
        Tuple.Create(Of LayoutControlItem, LayoutControlItem)(INDlciStudies, INDlciStudy),
        Tuple.Create(Of LayoutControlItem, LayoutControlItem)(INDlciProfessions, INDlciProfession),
        Tuple.Create(Of LayoutControlItem, LayoutControlItem)(INDlciRelationships, INDlciRelationship)
    }

        For Each pair In controlItemPairs
            ' Verifica si el LayoutControlItem asociado no está visible
            If pair.Item2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                ' Oculta el control asociado
                pair.Item1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        Next

    End Sub


    ''' <summary>
    ''' Evento que despliega el popup Menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub viewExemptIncomeDetail_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles viewExemptIncomeDetail.PopupMenuShowing
        If e.HitInfo.RowHandle < 0 Then
            Exit Sub
        End If

        Dim view = CType(sender, GridView)
        Dim x = childView.DataSource(childView.FocusedRowHandle)

        If DirectCast(x, Domain.Entities.ExemptIncome).VoucherCode <> "" Then
            Mensaje(EeventViewerImages.Advertencia) = "Este registro ya esta Confirmado"
            INDbbiDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            INDbbiEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Else
            INDbbiDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDbbiEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        End If

        view.RefreshRow(e.HitInfo.RowHandle)
        PopupMenu1.Manager = BarManager1
        PopupMenu1.ShowPopup(view.GridControl.PointToScreen(e.Point))
    End Sub
    ''' <summary>
    ''' Evento al dar click y seleccionar eliminar en el popup Menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbbiDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbiDelete.ItemClick

        Dim SelectedRow = childView.DataSource(childView.FocusedRowHandle) 'obtiene los dato de la fila seleccionada

        Dim ActualObjectToDelete = CType(SelectedRow, ExemptIncome)

        Dim objectToDeleteExemptIncome As New ExemptIncome
        With objectToDeleteExemptIncome
            .Id = CType(ActualObjectToDelete.Id, Integer)
            .ThirdPartyId = ActualObjectToDelete.ThirdPartyId
            .DateLiquidation = ActualObjectToDelete.DateLiquidation
            .VoucherType = ActualObjectToDelete.VoucherType
            .VoucherCode = ActualObjectToDelete.VoucherCode
            .MonthlyIncome = ActualObjectToDelete.MonthlyIncome
            .ExemptIncomeValue = ActualObjectToDelete.ExemptIncomeValue
            .Comments = ActualObjectToDelete.Comments
        End With

        listToDeleteExemptIncome.Add(ActualObjectToDelete)

        Dim objectToDeleteExemptIncomeView As New CommonExemptIncomeDetailXpo
        With objectToDeleteExemptIncomeView
            .Id = CType(ActualObjectToDelete.Id, Integer)
            .ThirdPartyId = ActualObjectToDelete.ThirdPartyId
            .DateLiquidation = ActualObjectToDelete.DateLiquidation
            .VoucherType = ActualObjectToDelete.VoucherType
            .VoucherCode = ActualObjectToDelete.VoucherCode
            .MonthlyIncome = ActualObjectToDelete.MonthlyIncome
            .ExemptIncomeValue = ActualObjectToDelete.ExemptIncomeValue
            .Comments = ActualObjectToDelete.Comments
        End With

        For Each item In listExemptIncomeGlobal
            If item.Id = objectToDeleteExemptIncomeView.Id Then
                listExemptIncomeGlobal.Remove(item)

                Dim subGridView = INDgcExemptIncome.MainView
                viewExemptIncome.CollapseAllDetails()
                subGridView.RefreshData()
                Exit For
            End If
        Next
    End Sub
    ''' <summary>
    ''' Evento al dar click en editar del popup menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbbiEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbiEdit.ItemClick
        exemptIncomeModification = True
        LoadpopupControl()
        INDpceExemptIncome.ShowPopup()
    End Sub

    ''' <summary>
    ''' Evento para obtener la fila del grid hijo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub viewExemptIncome_MasterRowExpanding(sender As Object, e As CustomMasterRowEventArgs) Handles viewExemptIncome.MasterRowExpanded

        childView = viewExemptIncome.GetDetailView(e.RowHandle, e.RelationIndex)

        childView.FocusedRowHandle = childView.DataRowCount - 1
        childView.FocusedColumn = childView.VisibleColumns(childView.VisibleColumns.Count - 1)
    End Sub

End Class