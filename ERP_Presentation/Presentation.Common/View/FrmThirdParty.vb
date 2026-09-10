'***********************************************************************
' Assembly         : Presentation.Common
' Author           : Kevin Garay
' Created          : 28-06-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Common.MVP
Imports Presentation.Accounting.MVP
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo
Imports System.Windows.Forms
Imports DevExpress.Data.Async.Helpers

#End Region

''' <summary>
''' Formulario Terceros
''' </summary>
''' <remarks></remarks>
Public Class FrmThirdParty
    Implements IThirdParty, ICustomizableForm

#Region "Variables"

    ''' <summary>
    ''' Listado de terceros con sucursal
    ''' </summary>
    Private ListThirdPartyBranchOffice As List(Of ThirdPartyBranchOffice)

    ''' <summary>
    ''' Listado de eliminados de terceros con sucursal
    ''' </summary>
    Private ListDeleteThirdPartyBranchOffice As List(Of ThirdPartyBranchOffice)

    ''' <summary>
    ''' Listado de terceros con responsabilidades fiscales
    ''' </summary>
    Private ListThirdPartyFiscalResponsability As List(Of ThirdPartyFiscalResponsibility)

    ''' <summary>
    ''' Listado de terceros con actividades economicas
    ''' </summary>
    Private ListThirdPartyEconomicActivities As List(Of ThirdPartyEconomicActivities)

    ''' <summary>
    ''' Listado de eliminados de terceros con responsabilidades fiscales
    ''' </summary>
    Private ListDeleteThirdPartyFiscalResponsability As List(Of ThirdPartyFiscalResponsibility)

    ''' <summary>
    ''' Listado de eliminados de terceros con actividades economicas
    ''' </summary>
    Private ListDeleteThirdPartyEconomicActivities As List(Of ThirdPartyEconomicActivities)

    ''' <summary>
    ''' Listado de eliminados de las exoneraciones tributarias
    ''' </summary>
    Private ListDeleteThirdPartyTaxExemptions As List(Of ThirdPartyTaxExemptions)

    ''' <summary>
    ''' Bandera para saber si se calcula el digito de verificación
    ''' </summary>
    Private FlagLoadControls As Boolean = True

    ''' <summary>
    ''' Variable para la customizacion del formulario
    ''' </summary>
    ''' <remarks></remarks>
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
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Instancia la clase singleton
    ''' </summary>
    ''' <remarks></remarks>
    Private indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Private Model As MThirdParty

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Private presenter As PThirdParty

    ''' <summary>
    ''' Variable que contiene la entidad tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private thirdparty As ThirdParty

    ''' <summary>
    ''' Variable que contiene la entidad persona
    ''' </summary>
    Private Person As Person

    ''' <summary>
    ''' Varaible para administrar el registro bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Private record As BlockRecord

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Private ListPersonType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Private ListHandlesBranchOffice As New List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Listado de tipos de empresa estatal
    ''' </summary>
    Private ListStateEnterpriseType As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Private ListClass As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la longitud minima del Nit
    ''' </summary>
    Private IdentificationTypeXpo As ADTIPOIDENTIFICAXpo

    ''' <summary>
    ''' indice del registro que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Private _indexEditRecord As Integer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Id del concepto de retencion iva
    ''' </summary>
    ''' <returns></returns>
    Public Property IVARetentionConceptId As Integer? Implements IThirdParty.IVARetentionConceptId
        Get
            Return INDsleIVARetentionConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleIVARetentionConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del concepto de retencion iva
    ''' </summary>
    ''' <returns></returns>
    Public Property IVARetentionConceptXpo As XPInstantFeedbackSource Implements IThirdParty.IVARetentionConceptXpo
        Get
            Return INDsleIVARetentionConcept.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIVARetentionConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Permite saber si maneja sucursales
    ''' </summary>
    ''' <returns></returns>
    Public Property HandlesBranchOffice As Boolean Implements IThirdParty.HandlesBranchOffice
        Get
            Return INDsleHandlesBranchOffice.EditValue
        End Get
        Set(value As Boolean)
            INDsleHandlesBranchOffice.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la sucursal
    ''' </summary>
    ''' <returns></returns>
    Public Property BranchOfficeId As Integer Implements IThirdParty.BranchOfficeId
        Get
            Return INDsleBranchOffice.EditValue
        End Get
        Set(value As Integer)
            INDsleBranchOffice.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de la sucursal
    ''' </summary>
    ''' <returns></returns>
    Public Property BranchOfficeXpo As XPInstantFeedbackSource Implements IThirdParty.BranchOfficeXpo
        Get
            Return INDsleBranchOffice.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBranchOffice.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la responsabilidad fiscal
    ''' </summary>
    ''' <returns></returns>
    Public Property FiscalResponsabilityId As Integer Implements IThirdParty.FiscalResponsabilityId
        Get
            Return INDsleFiscalResponsability.EditValue
        End Get
        Set(value As Integer)
            INDsleFiscalResponsability.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de la responsabilidad fiscal
    ''' </summary>
    ''' <returns></returns>
    Public Property FiscalResponsabilityXpo As XPInstantFeedbackSource Implements IThirdParty.FiscalResponsabilityXpo
        Get
            Return INDsleFiscalResponsability.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleFiscalResponsability.Properties.DataSource = value
        End Set
    End Property

    Public Property Status As Integer Implements IThirdParty.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Integer)
            If value Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la clase del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ClassThirdParty As Integer? Implements IThirdParty.ClassThirdParty
        Get
            Return INDsleClass.EditValue
        End Get
        Set(value As Integer?)
            INDsleClass.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la actividad economica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EconomicActivityId As Integer? Implements IThirdParty.EconomicActivityId
        Get
            Return INDsleEconomicActivity.EditValue
        End Get
        Set(value As Integer?)
            INDsleEconomicActivity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de actividad economica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EconomicActivityXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IThirdParty.EconomicActivityXpo
        Get
            Return INDsleEconomicActivity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleEconomicActivity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de ciudades
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CitiesXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IThirdParty.CitiesXpo
        Get
            Return INDsleCity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de ciudades
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CityId As Integer? Implements IThirdParty.CityId
        Get
            Return INDsleCity.EditValue
        End Get
        Set(value As Integer?)
            INDsleCity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de persona (1 = Natural, 2 = Juridica)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PersonType As Integer Implements IThirdParty.PersonType
        Get
            Return INDslePersonType.EditValue
        End Get
        Set(value As Integer)
            INDslePersonType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el nit del tercero
    ''' </summary>
    Public Property ThirdPartyNit As String Implements IThirdParty.ThirdPartyNit
        Get
            Return INDBteThirdPartyNit.EditValue
        End Get
        Set(value As String)
            INDBteThirdPartyNit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el codigo de verificacion (VC) o digito de verificacion (DV)
    ''' </summary>
    ''' <returns></returns>
    Public Property VerificationCode As String Implements IThirdParty.VerificationCode
        Get
            Return INDTxtVC.EditValue
        End Get
        Set(value As String)
            INDTxtVC.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el nombre o razon social de tercero
    ''' </summary>
    Public Property ThridPartyName As String Implements IThirdParty.ThridPartyName
        Get
            Return INDTxtThridPartyName.Text
        End Get
        Set(value As String)
            INDTxtThridPartyName.Text = value
        End Set
    End Property

    Private _identificationType As Integer
    ''' <summary>
    ''' Propiedad que contiene el tipo de identificacion de persona
    ''' </summary>
    <Obsolete>
    Public Property IdentificationType As Integer Implements IThirdParty.IdentificationType
        Get
            Return _identificationType
        End Get
        Set(value As Integer)
            _identificationType = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el primer nombre de persona
    ''' </summary>
    Public Property FirstName As String Implements IThirdParty.FirstName
        Get
            Return INDTxtFirstName.Text
        End Get
        Set(value As String)
            INDTxtFirstName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el segundo nombre de persona
    ''' </summary>
    Public Property SecondName As String Implements IThirdParty.SecondName
        Get
            Return INDTxtSecondName.Text
        End Get
        Set(value As String)
            INDTxtSecondName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el primer apellido de persona
    ''' </summary>
    Public Property FirstLastName As String Implements IThirdParty.FirstLastName
        Get
            Return INDTxtFirstLastName.Text
        End Get
        Set(value As String)
            INDTxtFirstLastName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el segundo apellido de persona
    ''' </summary>
    Public Property SecondLastName As String Implements IThirdParty.SecondLastName
        Get
            Return INDTxtSecondLastName.Text
        End Get
        Set(value As String)
            INDTxtSecondLastName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el tipo de retencion
    ''' </summary>
    Public Property RetentionType As Integer Implements IThirdParty.RetentionType
        Get
            Return INDCbeRetentionType.SelectedIndex
        End Get
        Set(value As Integer)
            INDCbeRetentionType.SelectedIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el tipo de contribuyente
    ''' </summary>
    Public Property ContributionType As Integer Implements IThirdParty.ContributionType
        Get
            Return INDCbeContributionType.SelectedIndex
        End Get
        Set(value As Integer)
            INDCbeContributionType.SelectedIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene si maneja ICA
    ''' </summary>
    Public Property Ica As Boolean Implements IThirdParty.Ica
        Get
            Return INDRgIca.EditValue
        End Get
        Set(value As Boolean)
            INDRgIca.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el porcentaje ICA
    ''' </summary>
    Public Property IcaPercentage As Decimal Implements IThirdParty.IcaPercentage
        Get
            Return INDTxtIcaPercentage.EditValue
        End Get
        Set(value As Decimal)
            INDTxtIcaPercentage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene si maneja tope de ICA
    ''' </summary>
    Public Property IcaTop As Boolean Implements IThirdParty.IcaTop
        Get
            Return INDRgIcaTop.EditValue
        End Get
        Set(value As Boolean)
            INDRgIcaTop.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el valor del tope ICA
    ''' </summary>
    Public Property IcaTopValue As Double Implements IThirdParty.IcaTopValue
        Get
            Return INDTxtIcaTopValue.EditValue
        End Get
        Set(value As Double)
            INDTxtIcaTopValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece/Obtiene el estado de la persona
    ''' </summary>
    Public Property StatusPerson As Boolean Implements IThirdParty.StatusPerson
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            Me.BarraBotones.StatusRecord = value
        End Set
    End Property

    ''' <summary>
    ''' Define el estado de activacion de los controles
    ''' </summary>
    Public WriteOnly Property ThirdParty_ActionsOnContros As Boolean Implements IThirdParty.ThirdParty_ActionsOnContros
        Set(value As Boolean)
            INDLyThirdParty.BeginUpdate()

            INDBteThirdPartyNit.Enabled = Not value
            INDTxtThridPartyName.Enabled = value
            INDslePersonType.Enabled = value
            INDTxtCIIU.Enabled = value
            INDPceContacts.Enabled = value
            INDsleIdentificationType.Enabled = value
            INDsleIdentificationTypeJuridic.Enabled = value
            INDTxtFirstName.Enabled = value
            INDTxtSecondName.Enabled = value
            INDTxtFirstLastName.Enabled = value
            INDTxtSecondLastName.Enabled = value
            INDsleHandlesBranchOffice.Enabled = value
            INDCbeRetentionType.Enabled = value
            INDCbeContributionType.Enabled = value
            INDsleIVARetentionConcept.Enabled = value
            INDRgIca.Enabled = value
            INDTxtIcaPercentage.Enabled = False
            INDRgIcaTop.Enabled = value
            INDTxtIcaTopValue.Enabled = False
            INDsleCity.Enabled = value
            INDRgElectronicBiller.Enabled = value
            INDsleEconomicActivity.Enabled = value
            INDbtnAddEconomicActivity.Enabled = value
            INDgcEconomicActivities.Enabled = value
            INDsleFiscalResponsability.Enabled = value
            INDbtnAddFiscalResponsability.Enabled = value
            INDgcFiscalResponsabilities.Enabled = value
            INDTxtVC.Enabled = value

            '********************Habilitar Txt Ica***************
            If Ica Then
                INDTxtIcaPercentage.Enabled = True
            End If

            If IcaTop Then
                INDTxtIcaTopValue.Enabled = True
            End If

            INDLyThirdParty.EndUpdate()
            '********************Establecer focos cuando se limpian controles, o cuando se busca un tercero***************
            If Not value Then
                INDBteThirdPartyNit.Focus()
            Else
                INDslePersonType.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Define el estado de activacion de los controles
    ''' </summary>
    Public WriteOnly Property Person_ActionsOnContros As Boolean Implements IThirdParty.Person_ActionsOnContros
        Set(value As Boolean)

            INDsleIdentificationType.Enabled = value
            INDsleCity.Enabled = value
            INDTxtFirstName.Enabled = value
            INDTxtSecondName.Enabled = value
            INDTxtFirstLastName.Enabled = value
            INDTxtSecondLastName.Enabled = value

            '*********************Establecer foco en nit de tercero si persona ya existe************************
            If Person.Id > 0 Then
                INDBteThirdPartyNit.Focus()
            Else
                INDslePersonType.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVARetentionAccountPayableConceptId As Integer? Implements IThirdParty.IVARetentionAccountPayableConceptId
        Get
            Return INDsleIVARetentionAccountPayableConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDsleIVARetentionAccountPayableConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de conceptos de cuentas por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVARetentionAccountPayableConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IThirdParty.IVARetentionAccountPayableConceptXpo
        Get
            Return INDsleIVARetentionAccountPayableConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIVARetentionAccountPayableConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' si el tercero es o no facturador electronico
    ''' </summary>
    ''' <returns></returns>
    Public Property ElectronicBiller As Boolean Implements IThirdParty.ElectronicBiller
        Get
            Return IIf(INDRgElectronicBiller.EditValue Is Nothing, False, INDRgElectronicBiller.EditValue)
        End Get
        Set(value As Boolean)
            INDRgElectronicBiller.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el tipo de identificacion
    ''' </summary>
    ''' <returns></returns>
    Public Property IdentificationTypeDatasource As XPInstantFeedbackSource Implements IThirdParty.IdentificationTypeDatasource
        Get
            Return INDsleIdentificationType.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIdentificationType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el tipo de identificacion
    ''' </summary>
    ''' <returns></returns>
    Public Property IdentificationTypeJuridicDatasource As XPInstantFeedbackSource Implements IThirdParty.IdentificationTypeJuridicDatasource
        Get
            Return INDsleIdentificationTypeJuridic.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIdentificationTypeJuridic.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o asigna el id del tipo de identificacion
    ''' </summary>
    ''' <returns></returns>
    Public Property IdentificationTypeId As Integer? Implements IThirdParty.IdentificationTypeId
        Get
            If PersonType = 1 Then
                Return INDsleIdentificationType.EditValue
            ElseIf PersonType = 2 Then
                Return INDsleIdentificationTypeJuridic.EditValue
            End If
        End Get
        Set(value As Integer?)
            If PersonType = 1 Then
                INDsleIdentificationType.EditValue = value
            ElseIf PersonType = 2 Then
                INDsleIdentificationTypeJuridic.EditValue = value
            End If
        End Set
    End Property

    Private _legalIdentificationType As Task(Of ADTIPOIDENTIFICAXpo)
    ''' <summary>
    ''' propiedad que consulta y devuelve, el tipo de identificacion juridica
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property LegalIdentificationType As Task(Of ADTIPOIDENTIFICAXpo)
        Get
            If _legalIdentificationType?.Result Is Nothing Then
                Using Model As New MThirdParty("")
                    _legalIdentificationType = Model.GetADTIPOIDENTIFICAXpoByAbbreviation(If(indigo.LanguageCulture = "es-CR", "CJ", "NI"))
                End Using
            End If
            Return _legalIdentificationType
        End Get
    End Property

    Private _validIdentificationType As ActionResult(Of Boolean)
    Private ReadOnly Property ValidIdentificationType As ActionResult(Of Boolean)
        Get
            Return _validIdentificationType
        End Get
    End Property


#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga/consulta una persona
    ''' </summary>
    Public Async Function Person_LoadControls() As Task
        Try
            AsyncLoader(True)
            Await Me.LegalIdentificationType
            Using Model As New MThirdParty(MThirdParty.TAG)
                Person = Await Model.GetPersonAsync(INDBteThirdPartyNit.Text)
            End Using

            If Person IsNot Nothing Then
                If Person.Id > 0 Then
                    With Person
                        IdentificationType = .IdentificationType
                        Me.IdentificationTypeId = .IdentificationTypeId
                        Me.INDsleIdentificationType.Properties.NullText = .IdentificationTypeName
                        Me.INDsleIdentificationTypeJuridic.Properties.NullText = .IdentificationTypeName
                        FirstName = .FirstName
                        SecondName = .SecondName
                        FirstLastName = .FirstLastName
                        SecondLastName = .SecondLastName
                        CtrContacts.EstablecerDataSourceDireccion = .Address.ToList
                        CtrContacts.EstablecerDataSourceTelefono = .Phone.ToList
                        CtrContacts.EstablecerDataSourceEmail = .Email.ToList
                        StatusPerson = .State

                        If .IdentificacionCityId IsNot Nothing Then
                            CityId = .IdentificacionCityId.Value
                            INDsleCity.Properties.NullText = .CityDescription
                        End If
                    End With
                Else
                    CleanControlsPerson()
                End If
            Else
                CleanControlsPerson()
            End If

            Person_ActionsOnContros = True
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Metodo que inicializa los datasources de los search que se cargan con tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function InitializeTuples() As Task
        ListPersonType = New List(Of Tuple(Of Integer, String))
        ListPersonType.Add(New Tuple(Of Integer, String)(1, "Natural"))
        ListPersonType.Add(New Tuple(Of Integer, String)(2, "Jurídico"))
        INDslePersonType.Properties.DataSource = ListPersonType.ToList

        ListClass = New List(Of Tuple(Of Integer, String))
        ListClass.Add(New Tuple(Of Integer, String)(1, "Nacional"))
        ListClass.Add(New Tuple(Of Integer, String)(2, "Extranjero"))
        INDsleClass.Properties.DataSource = ListClass.ToList

        ListStateEnterpriseType = New List(Of Tuple(Of Integer, String))
        ListStateEnterpriseType.Add(New Tuple(Of Integer, String)(0, "No Aplica"))
        ListStateEnterpriseType.Add(New Tuple(Of Integer, String)(1, "Municipal"))
        ListStateEnterpriseType.Add(New Tuple(Of Integer, String)(2, "Departamental"))
        ListStateEnterpriseType.Add(New Tuple(Of Integer, String)(3, "Distrital"))
        INDsleStateEnterpriseType.Properties.DataSource = ListStateEnterpriseType.ToList

        ListHandlesBranchOffice = New List(Of Tuple(Of Boolean, String))
        ListHandlesBranchOffice.Add(New Tuple(Of Boolean, String)(False, "No"))
        ListHandlesBranchOffice.Add(New Tuple(Of Boolean, String)(True, "Si"))
        INDsleHandlesBranchOffice.Properties.DataSource = ListHandlesBranchOffice.ToList
        Await GetThirdPartyCheck()
        presenter.InitializaIdentificationType()
    End Function

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmThirdPartyMetaData, Eform.InfoMetaData), Me.thirdparty.Nit, Me.thirdparty.Name, Me.thirdparty.Person.FirstName, Me.thirdparty.Person.SecondName, Me.thirdparty.Person.FirstLastName, Me.thirdparty.Person.SecondLastName, Me.thirdparty.Person.IdentificationNumber, INDsleIdentificationType.Text, INDCbeRetentionType.Text, INDCbeContributionType.Text),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.thirdparty.Nit & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmThirdPartyMetaDataTitle, Eform.InfoMetaData), Me.thirdparty.Nit),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmThirdPartyMetaData, Eform.InfoMetaData), Me.thirdparty.Nit, Me.thirdparty.Name, Me.thirdparty.Person.FirstName, Me.thirdparty.Person.SecondName, Me.thirdparty.Person.FirstLastName, Me.thirdparty.Person.SecondLastName, Me.thirdparty.Person.IdentificationNumber, INDsleIdentificationType.Text, INDCbeRetentionType.Text, INDCbeContributionType.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmThirdPartyMetaDataTitle, Eform.InfoMetaData), Me.thirdparty.Nit)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MThirdParty(MThirdParty.TAG)
                Await Model.DeleteBlockRecord(record)
            End Using

            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Limpia los controles del formulario
    ''' </summary>
    Public Sub CleanControls()
        INDLyThirdParty.BeginUpdate()

        INDLcgDocuments.HideControl()
        INDlyItemStateEnterpriseType.HideLayout()
        INDLCCodDivipola.HideLayout()
        INDlygBranchOffice.HideControl()
        INDlygThirdPartyTaxExemptions.HideControl()

        HandlesBranchOffice = False
        BranchOfficeId = Nothing
        INDgcInfo.DataSource = Nothing
        ListThirdPartyBranchOffice = Nothing
        ListDeleteThirdPartyBranchOffice = Nothing

        FiscalResponsabilityId = Nothing
        INDgcFiscalResponsabilities.DataSource = Nothing
        ListThirdPartyFiscalResponsability = Nothing
        ListDeleteThirdPartyFiscalResponsability = Nothing

        IVARetentionConceptId = Nothing
        INDsleIVARetentionConcept.Properties.NullText = String.Empty

        IdentificationTypeXpo = Nothing
        ThirdParty_ActionsOnContros = False
        PersonType = Nothing
        INDTxtCIIU.Text = String.Empty
        INDCodDivipola.Text = String.Empty
        INDBteThirdPartyNit.Text = String.Empty
        VerificationCode = String.Empty
        ThridPartyName = String.Empty
        CityId = Nothing
        INDsleCity.Properties.NullText = String.Empty
        IdentificationType = Nothing
        Me.IdentificationTypeId = Nothing
        INDsleIdentificationType.EditValue = Nothing
        INDsleIdentificationTypeJuridic.EditValue = Nothing
        Me.INDsleIdentificationType.Properties.NullText = Nothing
        Me.INDsleIdentificationTypeJuridic.Properties.NullText = Nothing
        Me.IdentificationTypeDatasource = Nothing
        Me.IdentificationTypeJuridicDatasource = Nothing
        FirstName = String.Empty
        SecondName = String.Empty
        FirstLastName = String.Empty
        SecondLastName = String.Empty
        RetentionType = -1
        ContributionType = -1
        Ica = False
        IcaPercentage = Nothing
        IcaTop = False
        IcaTopValue = Nothing
        CtrContacts.LimpiarControles()
        ClassThirdParty = Nothing

        EconomicActivityId = Nothing
        INDgcEconomicActivities.DataSource = Nothing
        ListThirdPartyEconomicActivities = Nothing
        ListDeleteThirdPartyEconomicActivities = Nothing

        ElectronicBiller = False
        Me.BarraBotones.StatusRecordVisible = False
        thirdparty = Nothing
        Person = Nothing
        Me.INDBteThirdPartyNit.Properties.MaxLength = 25
        Me._doc = Nothing
        Me._validIdentificationType = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        INDLyThirdParty.EndUpdate()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)

        BarraBotones.CleanAuditBasic()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        DeleteBlockedRecord()
    End Sub

    Public Sub CleanControlsPerson()
        CityId = Nothing
        Me.IdentificationTypeDatasource = Nothing
        Me.IdentificationTypeJuridicDatasource = Nothing
        Me.INDsleIdentificationType.Properties.NullText = Nothing
        FirstName = String.Empty
        SecondName = String.Empty
        FirstLastName = String.Empty
        SecondLastName = String.Empty
    End Sub

    ''' <summary>
    ''' Valida los controles del formulario
    ''' </summary>
    Public Function ValidateControlsForm() As Boolean

        If INDLCCodDivipola.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDCodDivipola.Text = String.Empty Then
                Return False
            End If
        End If

        If INDBteThirdPartyNit.Text = String.Empty Then
            Return False
        End If

        If PersonType = Nothing Then
            Return False
        End If

        If ({1, 2}.Contains(PersonType) AndAlso Me.IdentificationTypeId Is Nothing) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe selecionar un tipo de identificación"
            Return False
        End If

        If (PersonType = 2 AndAlso Me.LegalIdentificationType?.Result Is Nothing) Then
            Mensaje(EeventViewerImages.Advertencia) = "No existe un tipo de identificación jurídica"
            Return False
        End If

        If Not ListThirdPartyEconomicActivities.Any() And (PersonType = 2 OrElse (PersonType = 1 And ElectronicBiller)) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe agregar al menos una actividad economica"
            Return False
        End If

        If Not thirdparty?.ThirdPartyTaxExemptions.Any() And ContributionType = 5 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe agregar al menos una exoneracion tributaria"
            Return False
        End If

        If ((PersonType = 2 AndAlso Me.Person?.Id = 0) OrElse Me.PersonType <> 2) AndAlso Me.ValidIdentificationType IsNot Nothing AndAlso Not Me.ValidIdentificationType.StateResult Then
            Mensaje(EeventViewerImages.Advertencia) = ValidIdentificationType?.Message
            Return False
        End If

        If INDLyItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ThridPartyName = String.Empty Then
                Return False
            End If
        End If

        If INDLyGrPersonIdentification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If FirstName = String.Empty Then
                Return False
            End If

            If FirstLastName = String.Empty Then
                Return False
            End If
        End If

        If INDLyGrParameter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then


            If ClassThirdParty Is Nothing And PersonType = 2 Then
                Return False
            End If

            If RetentionType = -1 Then
                Return False
            End If

            If ContributionType = -1 Then
                Return False
            End If

            If INDRgIca.SelectedIndex = -1 Then
                Return False
            End If

            If INDRgIcaTop.SelectedIndex = -1 Then
                Return False
            End If
        End If

        'Se valida si maneja sucursales el tercero
        If HandlesBranchOffice Then
            If ListThirdPartyBranchOffice Is Nothing OrElse ListThirdPartyBranchOffice.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe agregar al menos una sucursal ya que el tercero tiene el parámetro de maneja sucursal en Si"
                Return False
            End If
        End If

        'El tercero debe tener asignada al menos una responsabilidad fiscal
        If ListThirdPartyFiscalResponsability Is Nothing OrElse Not ListThirdPartyFiscalResponsability.Any Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe agregar al menos una responsabilidad fiscal"
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Asigna los valores del formulario a la entidad
    ''' </summary>
    Public Async Function AssigningValues() As Task

        If Person Is Nothing Then
            Person = New Person With {.State = True}
        End If

        With Person
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            If PersonType = 1 Then 'Si es natural
                .IdentificationNumber = ThirdPartyNit
                .IdentificationType = IdentificationType
                .IdentificationTypeId = Me.IdentificationTypeId
                .DocumentTypeAbbreviation = IdentificationTypeXpo.SIGLA
                .FirstName = FirstName
                .SecondName = SecondName
                .FirstLastName = FirstLastName
                .SecondLastName = SecondLastName
                .IdentificacionCityId = CityId
                .BirthCityId = CityId

            Else 'Si es juridica
                .IdentificationNumber = ThirdPartyNit
                .IdentificationType = 7 'Nit
                .IdentificationTypeId = IdentificationTypeId
                .DocumentTypeAbbreviation = IdentificationTypeXpo.SIGLA
                .FirstName = Nothing
                .SecondName = Nothing
                .FirstLastName = Nothing
                .SecondLastName = Nothing
            End If
        End With

        If thirdparty Is Nothing Then
            thirdparty = New ThirdParty With {.State = True}
        End If

        With thirdparty
            .DigitVerification = VerificationCode
            .Person = Person
            .Nit = ThirdPartyNit
            .PersonType = PersonType
            .CodeCIIU = INDTxtCIIU.Text
            .ElectronicBiller = ElectronicBiller

            If INDLCCodDivipola.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .CodeDivipola = INDCodDivipola.Text
            Else
                .CodeDivipola = String.Empty
            End If

            .HandlesBranchOffice = HandlesBranchOffice

            If INDlyItemIVARetentionConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .IVARetentionConceptId = IVARetentionConceptId
            Else
                .IVARetentionConceptId = Nothing
            End If

            If PersonType = 1 Then 'Si es natural
                .Class = Nothing
                .EconomicActivityId = Nothing
                .EntityCode = Nothing
                .RetentionType = RetentionType
                .ContributionType = ContributionType
                .Ica = Ica

                If Ica Then
                    .IcaPercentage = IcaPercentage
                Else
                    .IcaPercentage = 0
                End If

                .IcaTop = IcaTop
                .IcaTopValue = IcaTopValue
                .DigitalSignature = INDCtrDigitalSignature.DigitalSignature
                .Name = FirstName + " " + SecondName + " " + FirstLastName + " " + SecondLastName

                If ContributionType = 1 Then
                    .IVARetentionAccountPayableConceptId = IVARetentionAccountPayableConceptId
                End If

            Else 'Si es juridica
                .Class = ClassThirdParty
                .EconomicActivityId = EconomicActivityId

                If INDtxtEntityCode.Text <> String.Empty Then
                    .EntityCode = INDtxtEntityCode.Text
                Else
                    .EntityCode = Nothing
                End If

                .RetentionType = RetentionType
                .ContributionType = ContributionType
                .IVARetentionAccountPayableConceptId = IVARetentionAccountPayableConceptId
                .Ica = Ica

                If Ica Then
                    .IcaPercentage = IcaPercentage
                Else
                    .IcaPercentage = 0
                End If

                .IcaTop = IcaTop
                .IcaTopValue = IcaTopValue
                .Name = ThridPartyName
            End If

            If thirdparty.ChangeTracker.State = ObjectState.Added Then
                .CreationDate = DateTime.Now()
            Else
                thirdparty.MarkAsModified()
            End If

            .UserId = indigo.UserIndigoId

            If INDlyItemStateEnterpriseType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .StateEnterpriseType = INDsleStateEnterpriseType.EditValue
            Else
                .StateEnterpriseType = 0
            End If

            '*********Datos Contacto
            If thirdparty IsNot Nothing Then
                If Person.Address IsNot Nothing Then
                    For Each direccion In ListadoEliminadosDireccion
                        direccion.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                        Person.Address.Add(direccion)
                    Next

                    If Person.Id > 0 Then
                        Person.MarkAsModified()
                    End If

                    ListadoEliminadosDireccion.Clear()
                End If

                If Person.Phone IsNot Nothing Then
                    For Each telefono In ListadoEliminadosTelefono
                        telefono.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                        Person.Phone.Add(telefono)
                    Next

                    If Person.Id > 0 Then
                        Person.MarkAsModified()
                    End If

                    ListadoEliminadosTelefono.Clear()
                End If
                If Person.Email IsNot Nothing Then
                    For Each email In Person.Email
                        email.Email1 = email.Email1.Replace(" ", "")
                    Next
                    For Each correo In ListadoEliminadosEmail
                        If correo.Id > 0 Then
                            correo.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                            Person.Email.Add(correo)
                        End If
                    Next

                    If Person.Id > 0 Then
                        Person.MarkAsModified()
                    End If
                    ListadoEliminadosEmail.Clear()
                End If
            End If

            'Se valida si maneja sucursales para agregarlas a la entidad
            If HandlesBranchOffice AndAlso ListThirdPartyBranchOffice IsNot Nothing AndAlso ListThirdPartyBranchOffice.Any() Then
                ListThirdPartyBranchOffice.ForEach(Sub(x) .ThirdPartyBranchOffice.Add(x))
            End If

            'Si el usuario dejo el parametro de si maneja sucursales se verifica que no hayan en el listado, si hay se eliminan
            If Not HandlesBranchOffice AndAlso ListThirdPartyBranchOffice IsNot Nothing AndAlso ListThirdPartyBranchOffice.Any() Then
                ListThirdPartyBranchOffice.ForEach(Sub(x) .ThirdPartyBranchOffice.Add(x.MarkAsDeleted()))
            End If

            'Si hay sucursales eliminadas
            If ListDeleteThirdPartyBranchOffice IsNot Nothing AndAlso ListDeleteThirdPartyBranchOffice.Any() Then
                ListDeleteThirdPartyBranchOffice.ForEach(Sub(x) .ThirdPartyBranchOffice.Add(x))
            End If

            If ListThirdPartyFiscalResponsability IsNot Nothing AndAlso ListThirdPartyFiscalResponsability.Any() Then
                ListThirdPartyFiscalResponsability.ForEach(Sub(d) .ThirdPartyFiscalResponsibility.Add(d))
            End If

            If ListDeleteThirdPartyFiscalResponsability IsNot Nothing AndAlso ListDeleteThirdPartyFiscalResponsability.Any() Then
                ListDeleteThirdPartyFiscalResponsability.ForEach(Sub(d) .ThirdPartyFiscalResponsibility.Add(d.MarkAsDeleted()))
            End If

            If ListThirdPartyEconomicActivities?.Any() Then
                ListThirdPartyEconomicActivities.ForEach(Sub(d) .ThirdPartyEconomicActivities.Add(d))
            End If

            If ListDeleteThirdPartyEconomicActivities?.Any() Then
                ListDeleteThirdPartyEconomicActivities.ForEach(Sub(d) .ThirdPartyEconomicActivities.Add(d.MarkAsDeleted()))
            End If

            If ListDeleteThirdPartyTaxExemptions?.Any() Then
                ListDeleteThirdPartyTaxExemptions.ForEach(Sub(x) .ThirdPartyTaxExemptions.Add(x.MarkAsDeleted()))
            End If
        End With
    End Function

    ''' <summary>
    ''' Carga/consulta un tercero
    ''' </summary>
    Public Async Function ThirdParty_LoadControls() As Task
        Try
            AsyncLoader(True)
            ThirdParty_ActionsOnContros = True
            INDLycgOtherParams.HideControl(False)
            Using Model As New MThirdParty(MThirdParty.TAG)
                thirdparty = Await Model.GetThirdPartyAsync(ThirdPartyNit)
                If thirdparty IsNot Nothing Then
                    INDLyThirdParty.BeginUpdate()

                    Me.BarraBotones.StatusRecordVisible = True
                    If thirdparty.Id > 0 Then
                        Dim result = Await Model.GetBlockRecord(Me.Tag, thirdparty.Id)
                        INDLcgDocuments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        IndigoGridControl1.AcceptXPO = True
                        INDGcDocuments.DataSource = Model.ListThirdPartyDocuments(thirdparty.Id)
                        Person = thirdparty.Person

                        With thirdparty
                            Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
                            ThridPartyName = .Name
                            VerificationCode = .DigitVerification
                            FlagLoadControls = False
                            Me.PersonType = .PersonType
                            Await INDslePersonTypeEditValueChangedManual()
                            INDTxtCIIU.Text = .CodeCIIU
                            INDCodDivipola.Text = .CodeDivipola
                            IdentificationType = .Person.IdentificationType
                            Me.IdentificationTypeId = .Person.IdentificationTypeId
                            Me.INDsleIdentificationType.Properties.NullText = .Person.IdentificationTypeName
                            FlagLoadControls = True
                            FirstName = .Person.FirstName
                            SecondName = .Person.SecondName
                            FirstLastName = .Person.FirstLastName
                            SecondLastName = .Person.SecondLastName
                            RetentionType = .RetentionType
                            ContributionType = .ContributionType
                            INDsleStateEnterpriseType.EditValue = .StateEnterpriseType
                            IVARetentionAccountPayableConceptId = .IVARetentionAccountPayableConceptId

                            'Campos de auditoria
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            IVARetentionConceptId = .IVARetentionConceptId
                            INDsleIVARetentionConcept.Properties.NullText = .IVARetentionConceptDescription

                            Ica = .Ica
                            IcaPercentage = .IcaPercentage
                            IcaTop = .IcaTop
                            IcaTopValue = .IcaTopValue
                            presenter.InitializeEconomicActivity()
                            presenter.InitializeAccountPayableConcepts()
                            EconomicActivityId = .EconomicActivityId
                            ClassThirdParty = .Class
                            INDtxtEntityCode.Text = .EntityCode
                            INDCtrDigitalSignature.DigitalSignature = .DigitalSignature
                            ElectronicBiller = .ElectronicBiller

                            HandlesBranchOffice = .HandlesBranchOffice
                            If HandlesBranchOffice Then
                                ListThirdPartyBranchOffice = .ThirdPartyBranchOffice.ToList()
                                INDgcInfo.DataSource = Nothing
                                INDgcInfo.DataSource = ListThirdPartyBranchOffice
                            End If

                            ListThirdPartyFiscalResponsability = .ThirdPartyFiscalResponsibility.ToList()
                            INDgcFiscalResponsabilities.DataSource = Nothing
                            INDgcFiscalResponsabilities.DataSource = ListThirdPartyFiscalResponsability

                            ListThirdPartyEconomicActivities = .ThirdPartyEconomicActivities.ToList()
                            INDgcEconomicActivities.DataSource = Nothing
                            INDgcEconomicActivities.DataSource = ListThirdPartyEconomicActivities

                            INDgcTaxExemptions.DataSource = Nothing
                            INDgcTaxExemptions.DataSource = .ThirdPartyTaxExemptions.ToList()

                            Status = .State
                            If Person.IdentificacionCityId IsNot Nothing Then
                                CityId = .Person.IdentificacionCityId
                                INDsleCity.Properties.NullText = .Person.CityDescription
                            End If

                        End With

                        Me.GetDocumentIndexed(Me.Tag & "_" & Me.thirdparty.Nit)

                        If result.Id = 0 Then
                            Me.BarraBotones.SetDocuments(thirdparty.Id)
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            record = New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = thirdparty.Id}
                            Dim operation = Await Model.SaveBlockRecord(record)
                            record = operation.ObjectEmbbeded
                        Else
                            record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If

                        'Deshabilito el control de tipo de persona, si el tercero existe y se quiere modificar
                        INDslePersonType.Enabled = False
                        INDCbeRetentionType.Enabled = If(ContributionType = 4, False, True)
                    Else
                        Me.thirdparty.State = True
                        Me.BarraBotones.StatusRecordVisible = True
                        Me.BarraBotones.StatusRecord = Me.BarraBotones.States(0).StatusValue
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        If IdentificationTypeId IsNot Nothing Then
                            Await Me.ValidateIdentificationType(ThirdPartyNit, IdentificationTypeId, Nothing, True)
                        End If
                    End If

                    INDLyThirdParty.EndUpdate()
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                End If
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.thirdparty IsNot Nothing AndAlso Me.thirdparty.Id > 0 Then
            If Not (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Exit Sub
            End If
        End If

        If record IsNot Nothing AndAlso record.Id > 0 Then
            If record.CodUser = indigo.UserIndigo Then
                DeleteBlockedRecord()
            End If
        End If

        INDBteThirdPartyNit.Text = Me.IdEntity.Trim()
        Await Me.ThirdParty_LoadControls()
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Actualiza el estado del tercero
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Me.thirdparty.Nit) Then
            Try
                Using model As New MThirdParty(MyBase.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not thirdparty.State
                    If Await model.UpdateStateThirdPartyAsync(Me.thirdparty.Id, state) Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        Me.Person.State = Not Person.State
                        Me.thirdparty.State = Me.Person.State
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        INDBteThirdPartyNit.Enabled = False
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteThirdPartyNit.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

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
            INDBteThirdPartyNit.Properties.MaxLength = 25

            If String.IsNullOrEmpty(thirdPartyNit) OrElse (String.IsNullOrEmpty(abbreviation) AndAlso identificationTypeId = 0) Then
                Return False
            End If

            Using Model As New MThirdParty("")

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
                    Me.ThirdPartyNit = String.Empty
                    INDBteThirdPartyNit.Properties.MaxLength = IdentificationTypeXpo.MaximumLength
                    INDBteThirdPartyNit.Enabled = True
                    INDBteThirdPartyNit.Focus()
                End If
                Return False
            End If

            INDBteThirdPartyNit.Enabled = False
            Return True
        Catch ex As Exception
            _validIdentificationType = New ActionResult(Of Boolean) With {.StateResult = False, .Message = $"Error comunicación al servidor: {Utils.GetInnerExceptionMessages(ex)}"}
            Return False
        Finally
            AsyncLoader(False)
        End Try
    End Function
#End Region

#Region "ICRUD"

    ''' <summary>
    ''' Abre el formulario de busqueda de terceros
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.ICrudBase.OpenSearch
        If Not BarraBotones.PermiteConsultar Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        Dim ListState As New List(Of Tuple(Of String, Boolean))
        ListState.Add(New Tuple(Of String, Boolean)("Inactivo", 0))
        ListState.Add(New Tuple(Of String, Boolean)("Activo", 1))
        With FormSearchObjects

            '****************************Si hay persona, se filtra la busqueda de terceros********************************
            If Person IsNot Nothing Then
                If Person.Id > 0 Then
                    .FiltroBusqueda = Person.Id
                    .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ThirdPartyByPersonId
                End If
                '****************************Si no hay persona, se hace busqueda de todos los terceros**************************
            Else
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllThirdParty
            End If

            .ListaColumnas = {New ColumnInfo() With {.Caption = "Nit", .FieldName = "Nit"},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name"},
                              New ColumnInfo() With {.Caption = "Identificación Persona", .FieldName = "PersonId.IdentificationNumber"},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "PersonId.FirstName"},
                              New ColumnInfo() With {.Caption = "Apellido", .FieldName = "PersonId.FirstLastName"},
                               New ColumnInfo() With {.Caption = "Estado", .FieldName = "State", .ColumnEdit = True, .ListItemsDatasourceColumEdit = ListState}}.ToList
            BarraBotones.PrepareToolbar(eAction.New)
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
        DeleteBlockedRecord()
        INDBteThirdPartyNit.Text = ReturnValue

        If INDBteThirdPartyNit.Text <> String.Empty Then
            Await ThirdParty_LoadControls()
            If Not INDBteThirdPartyNit.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If

            INDBteThirdPartyNit.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Abre el formulario de busqueda de ciudades
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Deshace los cambios hechos
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Elimina una ciudad
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If Me.thirdparty IsNot Nothing AndAlso Me.thirdparty.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MThirdParty(MThirdParty.TAG)
                        AsyncLoader(True)
                        If Await Model.DeleteThirdPartyAsync(thirdparty) Then
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.MensajeError) = "El tercero No se puede eliminar porque tiene movimientos"
                        End If
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBteThirdPartyNit.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Guarda un tercero
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If Not ValidateControlsForm() Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If

        Try
            Await AssigningValues()
            Using Model As New MThirdParty(MThirdParty.TAG)
                AsyncLoader(True)
                Dim result = Await Model.SaveThirdPartyAsync(thirdparty)
                If result Then
                    If thirdparty.ChangeTracker.State <> ObjectState.Unchanged Then
                        If thirdparty.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                        ElseIf thirdparty.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                        End If

                    ElseIf thirdparty.Person.ChangeTracker.State <> ObjectState.Unchanged Then
                        If thirdparty.Person.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                        ElseIf thirdparty.Person.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                        End If
                    End If

                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    CleanControls()
                Else
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteThirdPartyNit.Enabled = False
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Indica si el dato existe y se va a actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Metodo para mostrar los mensajes de retorno
    ''' </summary>
    ''' <param name="Icono"></param>
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
    ''' Limpia el formulario 
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
        CleanControls()
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed

        ListHandlesBranchOffice = Nothing
        ListThirdPartyBranchOffice = Nothing
        ListDeleteThirdPartyBranchOffice = Nothing
        ListThirdPartyFiscalResponsability = Nothing
        ListDeleteThirdPartyFiscalResponsability = Nothing
        FlagLoadControls = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Model = Nothing
        presenter = Nothing
        thirdparty = Nothing
        Person = Nothing
        record = Nothing
        ListPersonType = Nothing
        ListStateEnterpriseType = Nothing
        ListClass = Nothing
        _legalIdentificationType = Nothing
        Me._identificationType = Nothing
        Me._validIdentificationType = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del formulario terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmThirdParty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        '******************************'

        Me.Funct = AddressOf GenerateDoc
        presenter = New PThirdParty(Me)
        Me.INDPceContacts.Properties.PopupFormSize = New Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width * 0.3, Screen.PrimaryScreen.WorkingArea.Height * 0.4)
        LoadStatus()
        Deshacer()

        Await InitializeTuples()
        SetListActions()
    End Sub

    ''' <summary>
    ''' Metodo que nos setea las acciones de las rejillas dispuestas en el Form
    ''' </summary>
    Private Sub SetListActions()
        IndigoGridView1.SetListAcction(INDviewInfo, {eAcciones.Remove}.ToList())
        IndigoGridView2.SetListAcction(INDgvFiscalResponsabilities, {eAcciones.Remove}.ToList())
        IndigoGridView3.SetListAcction(INDgvEconomicActivities, {eAcciones.Remove}.ToList())
        IndigoGridView4.SetListAcction(INDgvTaxExemptions, {eAcciones.Remove, eAcciones.Edit}.ToList())
        IndigoGridView4.MoreInfoColunmns(INDgvTaxExemptions)
    End Sub

#End Region

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la columna 'Principal' de lal rejilla Actividades economicas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RepositoryItemRadioGroup1_EditValueChanging(sender As Object, e As EventArgs) Handles RepositoryItemRadioGroup1.EditValueChanged
        Dim EconomicActivity = DirectCast(INDgvEconomicActivities.GetFocusedRow(), ThirdPartyEconomicActivities)
        If EconomicActivity Is Nothing Then
            Exit Sub
        End If

        For Each item In ListThirdPartyEconomicActivities
            item.Defect = False
        Next

        EconomicActivity.Defect = True
        INDgcEconomicActivities.RefreshDataSource()
    End Sub

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de maneja sucursales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleHandlesBranchOffice_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHandlesBranchOffice.EditValueChanged
        If HandlesBranchOffice Then
            INDlygBranchOffice.HideControl(False)
        Else
            INDlygBranchOffice.HideControl()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del tipo de persona
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDslePersonType_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePersonType.EditValueChanged
        If Not Me.FlagLoadControls Then
            Exit Sub
        End If
        Await INDslePersonTypeEditValueChangedManual()
    End Sub

    Private Async Function INDslePersonTypeEditValueChangedManual() As Task
        If PersonType <> Nothing Then
            FlagLoadControls = False
            If PersonType = 1 Then 'Si es Natural
                INDLyItemIdentificationTypeJuridic.HideLayout()
                INDLyGrPersonIdentification.HideControl(False)
                INDLyGrParameter.HideControl(False)
                INDlygFiscalResponsability.HideControl(False)
                INDLyItemName.HideLayout()

                INDlciIVARetentionAccountPayableConceptId.HideLayout()
                INDlyItemClass.HideLayout()
                INDlyItemEntityCode.HideLayout()
                INDLyItemContributionType.ShowLayout()
                INDLyItemIca.ShowLayout()
                INDLyItemRetentionType.ShowLayout()
                INDLyItemIcaPercentage.ShowLayout()
                INDLyItemIcaTop.ShowLayout()
                INDLyItemIcaTopValue.ShowLayout()

            Else 'Es juridico
                INDLyItemIdentificationTypeJuridic.ShowLayout()
                INDLyGrPersonIdentification.HideControl()
                INDLyGrParameter.HideControl(False)
                INDlygFiscalResponsability.HideControl(False)
                INDLyItemName.ShowLayout()
                IdentificationType = -1
                Dim Obj = Await Me.LegalIdentificationType
                IdentificationTypeId = Obj.ID
                INDsleIdentificationTypeJuridic.Properties.NullText = Obj.CodeName
                INDlyItemClass.ShowLayout()
                INDLyItemRetentionType.ShowLayout()
                INDLyItemContributionType.ShowLayout()
                INDlciIVARetentionAccountPayableConceptId.ShowLayout()
                INDlyItemEntityCode.ShowLayout()
                INDLyItemIca.ShowLayout()
                INDLyItemIcaPercentage.ShowLayout()
                INDLyItemIcaTop.ShowLayout()
                INDLyItemIcaTopValue.ShowLayout()
            End If
            Await Person_LoadControls()
            Await Me.NitLengthValidator(Me.IdentificationTypeId, ThirdPartyNit, PersonType, True)
            FlagLoadControls = True
        Else
            INDLyGrPersonIdentification.HideControl()
            INDLyGrParameter.HideControl()
            INDlygFiscalResponsability.HideControl()
            INDLyItemName.HideLayout()
            INDLyItemIdentificationTypeJuridic.HideLayout()
        End If
    End Function

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del tipo de contribuyente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDCbeContributionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDCbeContributionType.EditValueChanged
        If ContributionType > -1 Then
            If ContributionType = 2 Then

                INDtxtEntityCode.Enabled = True
                INDCbeRetentionType.Enabled = True
                IVARetentionAccountPayableConceptId = Nothing
                INDlciIVARetentionAccountPayableConceptId.HideLayout()
                INDLCCodDivipola.ShowLayout()
                INDlyItemStateEnterpriseType.ShowLayout()
                INDlygThirdPartyTaxExemptions.HideControl()

            ElseIf ContributionType = 1 Then

                INDtxtEntityCode.Enabled = False
                INDCbeRetentionType.Enabled = True
                INDtxtEntityCode.Text = String.Empty
                INDlyItemStateEnterpriseType.HideLayout()
                INDLCCodDivipola.HideLayout()
                INDlciIVARetentionAccountPayableConceptId.ShowLayout()
                INDlygThirdPartyTaxExemptions.HideControl()

            Else
                INDtxtEntityCode.Enabled = False
                INDCbeRetentionType.Enabled = True
                IVARetentionAccountPayableConceptId = Nothing
                INDtxtEntityCode.Text = String.Empty
                INDLCCodDivipola.HideLayout()
                INDlyItemStateEnterpriseType.HideLayout()
                INDlciIVARetentionAccountPayableConceptId.HideLayout()
                INDlygThirdPartyTaxExemptions.HideControl()

                If ContributionType = 4 Then
                    RetentionType = 0
                    INDCbeRetentionType.Enabled = False
                End If

                If ContributionType = 5 Then
                    INDlygThirdPartyTaxExemptions.HideControl(False)
                End If
            End If
        Else
            INDtxtEntityCode.Enabled = False
            INDCbeRetentionType.Enabled = True
            INDtxtEntityCode.Text = String.Empty
            IVARetentionAccountPayableConceptId = Nothing
            INDLCCodDivipola.HideLayout()
            INDlyItemStateEnterpriseType.HideLayout()
            INDlciIVARetentionAccountPayableConceptId.HideLayout()
            INDlygThirdPartyTaxExemptions.HideControl()
        End If
    End Sub

    Private Async Sub INDsleIdentificationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleIdentificationType.EditValueChanged, INDsleIdentificationTypeJuridic.EditValueChanged
        If IdentificationTypeId Is Nothing OrElse Not FlagLoadControls Then
            Exit Sub
        End If

        Await Me.NitLengthValidator(Me.IdentificationTypeId, ThirdPartyNit, PersonType, FlagLoadControls)
    End Sub


    ''' <summary>
    ''' Valida la logitud del nit dependiendo del tipo de indentificacion
    ''' </summary>
    ''' <param name="identificationTypeId"></param>
    ''' <param name="thirdPartyNit"></param>
    ''' <param name="personType"></param>
    ''' <param name="flagLoadControls"></param>
    ''' <returns></returns>

    Public Async Function NitLengthValidator(identificationTypeId As Integer?, thirdPartyNit As String, personType As Integer, flagLoadControls As Boolean) As Task

        If identificationTypeId Is Nothing Then
            Return
        End If

        If IdentificationTypeDatasource Is Nothing Then
            Using Model As New MThirdParty("")
                IdentificationTypeXpo = Await Model.GetADTIPOIDENTIFICAXpoById(identificationTypeId)
            End Using
        Else
            Dim focusedRow As Object
            If personType = 1 Then
                focusedRow = INDGvIdentificationType.GetFocusedRow()
            Else
                focusedRow = INDGvIdentificationTypeJuridic.GetFocusedRow()
            End If
            Dim proxy As DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread = TryCast(focusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)

            If proxy IsNot Nothing Then
                Dim originalRow As Object = proxy.OriginalRow
                IdentificationTypeXpo = TryCast(originalRow, ADTIPOIDENTIFICAXpo)
            Else
                IdentificationTypeXpo = Nothing
            End If
        End If

        Await Me.ValidateIdentificationType(thirdPartyNit, identificationTypeId, IdentificationTypeXpo?.SIGLA, Me.Person?.Id = 0)

        IdentificationType = Utils.IdentificationTypeObsolete(IdentificationTypeXpo?.SIGLA).Item1

        If IdentificationType >= 0 Then
            presenter.Calculate_VerificationCode(thirdPartyNit, personType, IdentificationType, flagLoadControls)
        End If
    End Function

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Metodo para controlar el enter en el button edit del nit del tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBteThirdPartyNit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteThirdPartyNit.KeyDown
        If Not String.IsNullOrEmpty(INDBteThirdPartyNit.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                If Not BarraBotones.PermiteConsultar Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
                    Exit Sub
                End If

                If Not INDBteThirdPartyNit.Enabled Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                End If

                Await ThirdParty_LoadControls()
                INDBteThirdPartyNit.Enabled = False
            End If
        End If
        If e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteThirdPartyNit.Text Is String.Empty Then
            INDBteThirdPartyNit.Focus()
        End If
    End Sub

#End Region

#Region "SelectedIndexChanged"

    ''' <summary>
    ''' Metodo que controla el evento change cuando maneja ICA
    ''' </summary>
    Private Sub INDRgIca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles INDRgIca.SelectedIndexChanged
        INDTxtIcaPercentage.Enabled = Ica
        If Not Ica Then
            IcaPercentage = Nothing
        ElseIf Ica Then
            If thirdparty IsNot Nothing Then
                If thirdparty.Id > 0 Then
                    IcaPercentage = thirdparty.IcaPercentage
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' Metodo que controla el evento change cuando maneja Tope ICA
    ''' </summary>
    Private Sub INDRgIcaTop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles INDRgIcaTop.SelectedIndexChanged
        INDTxtIcaTopValue.Enabled = IcaTop
        If Not IcaTop Then
            IcaTopValue = Nothing
        ElseIf IcaTop Then
            If thirdparty IsNot Nothing Then
                If thirdparty.Id > 0 Then
                    IcaTopValue = thirdparty.IcaTopValue
                End If
            End If
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de concepto de retencion iva
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleIVARetentionConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIVARetentionConcept.QueryPopUp
        If IVARetentionConceptXpo Is Nothing Then
            presenter.InitializeIVARetention()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de ciudad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCity.QueryPopUp
        If CitiesXpo Is Nothing Then
            presenter.InitializeCity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de actividad economica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEconomicActivity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleEconomicActivity.QueryPopUp
        If EconomicActivityXpo Is Nothing Then
            presenter.InitializeEconomicActivity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de concepto de cuenta por pagar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIVARetentionAccountPayableConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIVARetentionAccountPayableConceptId.QueryPopUp
        If IVARetentionAccountPayableConceptXpo Is Nothing Then
            presenter.InitializeAccountPayableConcepts()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de sucursales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBranchOffice_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBranchOffice.QueryPopUp
        If BranchOfficeXpo Is Nothing Then
            presenter.InitializaBranchOffice()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de sucursales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleFiscalResponsability_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFiscalResponsability.QueryPopUp
        If FiscalResponsabilityXpo Is Nothing Then
            presenter.InitializaFiscalResponsability()
        End If
    End Sub

    ''' <summary>
    ''' Tipo de identificacion, evento de consulta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleIdentificationType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIdentificationType.QueryPopUp, INDsleIdentificationTypeJuridic.QueryPopUp
        If Me.IdentificationTypeDatasource Is Nothing OrElse IdentificationTypeJuridicDatasource Is Nothing Then
            presenter.InitializaIdentificationType()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de conceptos de retencion iva
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleIVARetentionConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIVARetentionConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(605, Nothing, True)
            presenter.InitializeIVARetention()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de sucursales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBranchOffice_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBranchOffice.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1970, Nothing, True)
            presenter.InitializaBranchOffice()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de ciudades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCity With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using

            presenter.InitializeCity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de actividad economica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEconomicActivity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEconomicActivity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmEconomicActivity With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using

            presenter.InitializeEconomicActivity()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDBteThirdPartyNit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteThirdPartyNit.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al abrir el for de conceptos de cuentas por pagar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIVARetentionAccountPayableConceptId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIVARetentionAccountPayableConceptId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("725", Nothing, True)
            presenter.InitializeAccountPayableConcepts()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento para capturar el cierre del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmDepartaments_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "CtrContacts"

    ''' <summary>
    '''  Variable que maneja el listado de los Direccions agregados al control
    ''' </summary>
    Private ListadoEliminadosDireccion As New List(Of Address)

    ''' <summary>
    ''' Evento para eliminar las direcciones agregadas al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarDireccion() Handles CtrContacts.EliminarDireccion
        If Person.Address.Count > 0 Then
            ListadoEliminadosDireccion.Add(Person.Address.Item(CtrContacts.ItemSelecionadoDireccion))
            Person.Address.RemoveAt(CtrContacts.ItemSelecionadoDireccion)
            CtrContacts.EstablecerDataSourceDireccion = Person.Address
        End If
    End Sub

    ''' <summary>
    ''' Variable que maneja el listado de los telefonos agregados al control
    ''' </summary>
    Private ListadoEliminadosTelefono As New List(Of Phone)

    ''' <summary>
    ''' Evento para eliminar los telefono agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarTelefono() Handles CtrContacts.EliminarTelefono
        If Person.Phone.Count > 0 Then
            ListadoEliminadosTelefono.Add(Person.Phone.Item(CtrContacts.ItemSelecionadoTelefono))
            Person.Phone.RemoveAt(CtrContacts.ItemSelecionadoTelefono)
            CtrContacts.EstablecerDataSourceTelefono = Person.Phone
        End If
    End Sub

    ''' <summary>
    '''  Variable que maneja el listado de los Email agregados al control
    ''' </summary>
    Private ListadoEliminadosEmail As New List(Of Email)

    ''' <summary>
    ''' Evento para eliminar los Email agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarEmail() Handles CtrContacts.EliminarEmail
        If Person.Email.Count > 0 Then
            ListadoEliminadosEmail.Add(Person.Email.Item(CtrContacts.ItemSelecionadoEmail))
            Person.Email.RemoveAt(CtrContacts.ItemSelecionadoEmail)
            CtrContacts.EstablecerDataSourceEmail = Person.Email
        End If
    End Sub

    ''' <summary>
    ''' Evento para cambia el tipo Email
    ''' </summary>
    Private Sub CtrContactos1_ChangeEmailType(Type As Byte) Handles CtrContacts.ChangeEmailType
        If Person.Email.Count > 0 Then
            Dim email = Person.Email.ElementAt(CtrContacts.ItemSelecionadoEmail)
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
        If Person IsNot Nothing Then
            If Person.Address Is Nothing Then
                Person.Address = New Domain.Entities.TrackableCollection(Of Address)
            End If
            If Person.Address.Where(Function(e) e.Addresss = CtrContacts.Direccion).Count = 0 Then
                Person.Address.Add(New Address With
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
            CtrContacts.EstablecerDataSourceDireccion = Person.Address
        End If
    End Sub

    ''' <summary>
    ''' Evento para agregar al listado de correos  del control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevoEmail() Handles CtrContacts.InsertoNuevoEmail
        If Person IsNot Nothing Then
            If Person.Email Is Nothing Then
                Person.Email = New Domain.Entities.TrackableCollection(Of Email)
            End If
            If Person.Email.Where(Function(e) e.Email1 = CtrContacts.Email).Count = 0 Then
                Person.Email.Add(New Email With {.Email1 = CtrContacts.Email, .Synchronized = "1", .Type = CtrContacts.EmailType, .State = True})
            End If
            CtrContacts.EstablecerDataSourceEmail = Nothing
            CtrContacts.EstablecerDataSourceEmail = Person.Email
        End If
    End Sub

    ''' <summary>
    ''' Evento para agregar al listado de Telefonos  del control de datos de contacto.
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevoTelefono() Handles CtrContacts.InsertoNuevoTelefono
        If Person IsNot Nothing Then
            If Person.Phone Is Nothing Then
                Person.Phone = New Domain.Entities.TrackableCollection(Of Phone)
            End If
            If Person.Phone.Where(Function(e) e.Phone1 = CtrContacts.Telefono).Count = 0 Then
                Person.Phone.Add(New Phone With {.Phone1 = CtrContacts.Telefono, .IdPhoneType = CShort(CtrContacts.TipoTelefono), .Synchronized = "1"})
            End If
            CtrContacts.EstablecerDataSourceTelefono = Nothing
            CtrContacts.EstablecerDataSourceTelefono = Person.Phone
        End If
    End Sub

#End Region

#Region "MouseDoubleClick"
    Private Sub INDGcDocuments_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGcDocuments.MouseDoubleClick
        Dim hitPoint = Me.INDGvDocuments.CalcHitInfo(e.Location)
        If hitPoint.InDataRow Then
            Dim detail = DirectCast(DirectCast(INDGvDocuments.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewThirdPartyDocumentsXpo)
            If detail.TagForm IsNot String.Empty Then
                OpenForm(detail.TagForm, detail.Code, False)
            End If
        End If
    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al dar click sobre el boton de agregar sucursales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddBranchOffice_Click(sender As Object, e As EventArgs) Handles INDbtnAddBranchOffice.Click
        AddBranchOffice()
    End Sub

    ''' <summary>
    ''' Agrega la sucursal en la rejilla
    ''' </summary>
    Private Sub AddBranchOffice()
        'Se valida que hayan seleccionado una sucursal
        If BranchOfficeId = Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una sucursal"
            Exit Sub
        End If

        'Se valida que la sucursal no exista en el listado
        If ListThirdPartyBranchOffice IsNot Nothing Then
            If (From x In ListThirdPartyBranchOffice Where x.BranchOfficeId = BranchOfficeId Select x).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La sucursal seleccionada ya existe en el listado"
                Exit Sub
            End If
        Else 'Si es null se instancia el listado
            ListThirdPartyBranchOffice = New List(Of ThirdPartyBranchOffice)
        End If

        'Se consulta el branchoffice por xpo para poder llenar los campos
        Dim xpo = presenter.GetBranchOfficeXpo(BranchOfficeId)

        'Se crea el nuevo objeto que va al listado
        Dim thirdPartyBranchOffice As New ThirdPartyBranchOffice
        With thirdPartyBranchOffice
            .BranchOfficeId = BranchOfficeId
            .BranchOfficeDescription = xpo.Codigo + " - " + xpo.Descripcion
        End With

        'Se agrega el objeto creado al listado
        ListThirdPartyBranchOffice.Add(thirdPartyBranchOffice)
        INDgcInfo.DataSource = Nothing
        INDgcInfo.DataSource = ListThirdPartyBranchOffice
        Mensaje(EeventViewerImages.Informacion) = "Sucursal agregada correctamente"
        BranchOfficeId = Nothing
        INDsleBranchOffice.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click sobre el boton de agregar exoneraciones tributarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddTaxExemptions_Click(sender As Object, e As EventArgs) Handles INDbtnAddTaxExemptions.Click
        Using formulario As New FrmPopupThirdPartyTaxExemptions
            Me.Cursor = BaseClass.ChangeCursorIndigo()
            formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.4
            formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.MaximizeBox = True
            AddHandler formulario.AddThirdPartyTaxExemptions, AddressOf ReturnAddThirdPartyTaxExemptions
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de agregar las exoneraciones tributarias a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnAddThirdPartyTaxExemptions(sender As Object, e As AddBThirdPartyTaxExemptionsEventArgs)

        If thirdparty Is Nothing Then
            thirdparty = New ThirdParty With {.State = True}
        End If

        If e.EditMode Then
            Me.thirdparty.ThirdPartyTaxExemptions.RemoveAt(Me._indexEditRecord)
            Me.thirdparty.ThirdPartyTaxExemptions.Insert(Me._indexEditRecord, e.ThirdPartyTaxExemptions)
        Else
            If Me.thirdparty.ThirdPartyTaxExemptions.Any(
                Function(x) (
                                (x.DocumentTypeId = e.ThirdPartyTaxExemptions.DocumentTypeId) OrElse
                                (x.DocumentIdentification = e.ThirdPartyTaxExemptions.DocumentIdentification) OrElse
                                (x.InstitutionId = e.ThirdPartyTaxExemptions.InstitutionId)
                            )) Then
                Mensaje(EeventViewerImages.Advertencia) = "El Item ya esta agregado"
                Exit Sub
            End If

            Me.thirdparty.ThirdPartyTaxExemptions.Add(e.ThirdPartyTaxExemptions)
        End If

        INDgcTaxExemptions.DataSource = Nothing
        INDgcTaxExemptions.DataSource = Me.thirdparty.ThirdPartyTaxExemptions.ToList()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click sobre el boton de agregar actividades economicas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddEconomicActivity_Click(sender As Object, e As EventArgs) Handles INDbtnAddEconomicActivity.Click
        AddEconomicActivity()
    End Sub

    ''' <summary>
    ''' Agrega la actividad economica en la rejilla
    ''' </summary>
    Private Sub AddEconomicActivity()
        'Se valida que hayan seleccionado una actividad economica
        If EconomicActivityId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una actividad economica"
            Exit Sub
        End If

        'Se valida que la actividad no exista en el listado
        If ListThirdPartyEconomicActivities IsNot Nothing Then
            If (From x In ListThirdPartyEconomicActivities Where x.EconomicActivityId = EconomicActivityId Select x).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La actividad economica seleccionada ya existe en el listado"
                Exit Sub
            End If
        Else 'Si es null se instancia el listado
            ListThirdPartyEconomicActivities = New List(Of ThirdPartyEconomicActivities)
        End If

        'Valido que solo exista una actividad economica marcada por defecto
        Dim Defect As Boolean = False
        If Not ListThirdPartyEconomicActivities.Any(Function(x) x.Defect = True) Then
            Defect = True
        End If

        Dim EconomicActivityXpo As CommonEconomicActivity
        'Se consulta la actividad por medio del propio control para poder llenar los campos
        EconomicActivityXpo = TryCast(TryCast(INDgvEconomicActivity.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, CommonEconomicActivity)

        'Se consulta directamente si no se logra obtener el objeto
        If EconomicActivityXpo Is Nothing Then
            EconomicActivityXpo = presenter.GetEconomicActivityXpoById(EconomicActivityId)
        End If

        'Se crea el nuevo objeto que va al listado
        Dim ThirdPartyEconomicActivities As New ThirdPartyEconomicActivities
        With ThirdPartyEconomicActivities
            .EconomicActivityId = EconomicActivityId
            .Code = EconomicActivityXpo.Code
            .Name = EconomicActivityXpo.Name
            .Defect = Defect
        End With

        'Se agrega el objeto creado al listado
        ListThirdPartyEconomicActivities.Add(ThirdPartyEconomicActivities)
        INDgcEconomicActivities.DataSource = Nothing
        INDgcEconomicActivities.DataSource = ListThirdPartyEconomicActivities
        Mensaje(EeventViewerImages.Informacion) = "Actividad economica agregada correctamente"
        EconomicActivityId = Nothing
        INDsleEconomicActivity.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click sobre el boton de agregar responsabilidades fiscales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddFiscalResponsability_Click(sender As Object, e As EventArgs) Handles INDbtnAddFiscalResponsability.Click
        AddFiscalResponsability()
    End Sub

    ''' <summary>
    ''' Agrega la sucursal en la rejilla
    ''' </summary>
    Private Sub AddFiscalResponsability()
        'Se valida que hayan seleccionado una responsabilidad
        If FiscalResponsabilityId = Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una responsabilidad"
            Exit Sub
        End If

        'Se valida que la responsabilidad no exista en el listado
        If ListThirdPartyFiscalResponsability IsNot Nothing Then
            If (From x In ListThirdPartyFiscalResponsability Where x.FiscalResponsibilityId = FiscalResponsabilityId Select x).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La responsabilidad seleccionada ya existe en el listado"
                Exit Sub
            End If
        Else 'Si es null se instancia el listado
            ListThirdPartyFiscalResponsability = New List(Of ThirdPartyFiscalResponsibility)
        End If

        'Se consulta la responsabilidad por xpo para poder llenar los campos
        Dim xpo = presenter.GetFiscalResponsabilityXpo(FiscalResponsabilityId)

        'Se crea el nuevo objeto que va al listado
        Dim thirdPartyFiscalResponsibility As New ThirdPartyFiscalResponsibility
        With thirdPartyFiscalResponsibility
            .FiscalResponsibilityId = FiscalResponsabilityId
            .Code = xpo.Code
            .Name = xpo.Name
        End With

        'Se agrega el objeto creado al listado
        ListThirdPartyFiscalResponsability.Add(thirdPartyFiscalResponsibility)
        INDgcFiscalResponsabilities.DataSource = Nothing
        INDgcFiscalResponsabilities.DataSource = ListThirdPartyFiscalResponsability
        Mensaje(EeventViewerImages.Informacion) = "Responsabilidad agregada correctamente"
        FiscalResponsabilityId = Nothing
        INDsleFiscalResponsability.Focus()
    End Sub

#End Region

#Region "ContextMenu"

    ''' <summary>
    ''' Click derecho
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        DeleteBranchoffice()
    End Sub

    ''' <summary>
    ''' Elimina la sucursal de la rejilla
    ''' </summary>
    Private Sub DeleteBranchoffice()
        Dim entity As ThirdPartyBranchOffice = INDviewInfo.GetFocusedRow()
        If entity.Id > 0 Then
            If ListDeleteThirdPartyBranchOffice Is Nothing Then
                ListDeleteThirdPartyBranchOffice = New List(Of ThirdPartyBranchOffice)
            End If
            ListDeleteThirdPartyBranchOffice.Add(entity.MarkAsDeleted())
        End If

        ListThirdPartyBranchOffice.Remove(entity)
        INDgcInfo.DataSource = Nothing
        INDgcInfo.DataSource = ListThirdPartyBranchOffice
    End Sub

    ''' <summary>
    ''' Click derecho
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions, IndigoGridView2.Click_ButtonAction
        DeleteFiscalResponsibility()
    End Sub

    ''' <summary>
    ''' Elimina la sucursal de la rejilla
    ''' </summary>
    Private Sub DeleteFiscalResponsibility()
        Dim entity As ThirdPartyFiscalResponsibility = INDgvFiscalResponsabilities.GetFocusedRow()
        If entity.Id > 0 Then
            If ListDeleteThirdPartyFiscalResponsability Is Nothing Then
                ListDeleteThirdPartyFiscalResponsability = New List(Of ThirdPartyFiscalResponsibility)
            End If
            ListDeleteThirdPartyFiscalResponsability.Add(entity.MarkAsDeleted())
        End If

        ListThirdPartyFiscalResponsability.Remove(entity)
        INDgcFiscalResponsabilities.DataSource = Nothing
        INDgcFiscalResponsabilities.DataSource = ListThirdPartyFiscalResponsability
    End Sub

    ''' <summary>
    ''' Click derecho
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView3_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView3.ContexMenuActions, IndigoGridView3.Click_ButtonAction
        Dim entity As ThirdPartyEconomicActivities = INDgvEconomicActivities.GetFocusedRow()
        If entity.Id > 0 Then
            If ListDeleteThirdPartyEconomicActivities Is Nothing Then
                ListDeleteThirdPartyEconomicActivities = New List(Of ThirdPartyEconomicActivities)
            End If
            ListDeleteThirdPartyEconomicActivities.Add(entity.MarkAsDeleted())
        End If

        ListThirdPartyEconomicActivities.Remove(entity)
        INDgcEconomicActivities.DataSource = Nothing
        INDgcEconomicActivities.DataSource = ListThirdPartyEconomicActivities
    End Sub

    ''' <summary>
    ''' Click derecho
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView4_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView4.ContexMenuActions, IndigoGridView4.Click_ButtonAction
        Dim entity As ThirdPartyTaxExemptions = INDgvTaxExemptions.GetFocusedRow()
        Select Case (sender.Tag)

            Case "Edit", ResourceManager.GetString("Edit")
                Me._indexEditRecord = Me.thirdparty.ThirdPartyTaxExemptions.IndexOf(entity)
                Using formulario As New FrmPopupThirdPartyTaxExemptions
                    Me.Cursor = BaseClass.ChangeCursorIndigo()
                    formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.4
                    formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    formulario.MaximizeBox = True
                    formulario.EditMode = True
                    formulario.ListThirdPartyTaxExemptions = thirdparty.ThirdPartyTaxExemptions.ToList()
                    formulario.ThirdPartyTaxExemptions = entity
                    AddHandler formulario.AddThirdPartyTaxExemptions, AddressOf ReturnAddThirdPartyTaxExemptions
                    Dim transparent = New Base.FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using

            Case "Remove", ResourceManager.GetString("Remove")
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

                    If Me.ListDeleteThirdPartyTaxExemptions Is Nothing Then
                        Me.ListDeleteThirdPartyTaxExemptions = New List(Of ThirdPartyTaxExemptions)
                    End If

                    entity.MarkAsDeleted
                    ListDeleteThirdPartyTaxExemptions.Add(entity)

                    INDgcTaxExemptions.DataSource = Nothing
                    INDgcTaxExemptions.DataSource = thirdparty.ThirdPartyTaxExemptions.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
                End If
        End Select
    End Sub

#End Region

#End Region

#Region "Bar Buttons Events"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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

    ''' <summary>
    ''' Barra Botones: Activar Inactivar
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

#End Region

#Region "Customize"
    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDLyThirdParty.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LoadhronousDefinitions.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(PathFunctionalDefinitions) Then
            ExistDefinitionFront = True
        End If
    End Sub

    ''' <summary>
    ''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadhronousDefinitions.RunWorkerCompleted
        If ExistDefinitionFront Then
            INDLyThirdParty.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDLyCtrContractTemplate_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLyThirdParty.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MThirdParty(MThirdParty.TAG)
                Dim dsFields As DataSet = model.GetFieldsNULL()
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDLyThirdParty.Items.Count - 1
                        INDLyThirdParty.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDLyThirdParty.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDLyThirdParty.Items.Item(j).Tag.ToString.Trim Then
                                    INDLyThirdParty.Items.Item(j).AllowHide = True
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
    ''' Funcion que nos establece la visibilidad del Campo 'Digito de verificación'
    ''' </summary>
    Private Async Function GetThirdPartyCheck() As Task
        Using model As New MCompanySettings(MThirdParty.TAG)
            Dim setting = Await model.GetCompanySettings
            If setting IsNot Nothing Then
                If setting.ThirdPartyCheckDigit Then
                    INDLyVC.ShowLayout()
                Else
                    INDLyVC.HideLayout()
                    INDLyItemNit.Size = New Drawing.Size(411, 36)
                    INDLyItemNit.MinSize = New Drawing.Size(410, 36)
                    INDLyItemNit.MaxSize = New Drawing.Size(410, 36)

                    INDBteThirdPartyNit.Size = New Drawing.Size(259, 30)
                End If
            End If
        End Using
    End Function

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCtrContractTemplate_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLyThirdParty.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDLyThirdParty.IsModified Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDLyThirdParty.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDLyThirdParty.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

End Class

Public Class AddBThirdPartyTaxExemptionsEventArgs
    Inherits EventArgs

    Property ThirdPartyTaxExemptions As ThirdPartyTaxExemptions

    Property EditMode As Boolean

End Class