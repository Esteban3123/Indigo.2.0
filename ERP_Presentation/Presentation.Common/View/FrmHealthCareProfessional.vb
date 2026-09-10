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
Imports Presentation.Controls

#End Region
''' <summary>
''' Clase que contiene la vista del funcional profesionales
''' </summary>
''' <remarks></remarks>
Public Class FrmHealthCareProfessional
    Implements IHealthCareProfessional

#Region "Variables"

    ''' <summary>
    ''' Variable que contiene el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PHealthCareProfessional

    ''' <summary>
    ''' Variable que contiene el modelo
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As MHealthCareProfessional

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
    ''' constante con el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Crystal"

    ''' <summary>
    ''' Para almacenar el id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim SupplierId As Integer? = 0

    ''' <summary>
    ''' Para almacenar el id de la linea de distribución
    ''' </summary>
    ''' <remarks></remarks>
    Dim DistributionLinesId As Integer = 0

    ''' <summary>
    ''' listado de los tipos de vinculación
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listJobBondingType As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' listado de los tipos de si realiza o no consulta externa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listOutPatient As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' listado de los tipos de profesional
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listProfession As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' listado de los tipos de los perfiles de cirugia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listSurgeryProfiler As List(Of Tuple(Of String, String))

    ''' <summary>
    ''' Entidad de especialidad 1
    ''' </summary>
    ''' <remarks></remarks>
    Dim INESPECIA1 As INESPECIA

    ''' <summary>
    ''' Entidad de especialidad 2
    ''' </summary>
    ''' <remarks></remarks>
    Dim INESPECIA2 As INESPECIA

    ''' <summary>
    ''' Entidad de especialidad 3
    ''' </summary>
    ''' <remarks></remarks>
    Dim INESPECIA3 As INESPECIA

    ''' <summary>
    ''' Listado de contratos que tiene asociado el médico
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListHealthProfessionalContract As List(Of HealthProfessionalContract)

    ''' <summary>
    ''' Listado de contratos que tiene asociado el médico
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteHealthProfessionalContract As List(Of HealthProfessionalContract)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Entidad Paciente
    ''' </summary>
    ''' <remarks></remarks>
    Private _professionalEHR As INPROFSAL

    Public Property ProfessionalEHR As INPROFSAL Implements IHealthCareProfessional.ProfessionalEHR
        Get
            Return _professionalEHR
        End Get
        Set(value As INPROFSAL)
            _professionalEHR = value
        End Set
    End Property

    Private _professionalERP As HealthProfessional
    Public Property ProfessionalERP As HealthProfessional Implements IHealthCareProfessional.ProfessionalERP
        Get
            Return _professionalERP
        End Get
        Set(value As HealthProfessional)
            _professionalERP = value
        End Set
    End Property

    Private _healthProfessionalModel As HealthProfessionalModel
    Public Property HealthProfessionalModel As HealthProfessionalModel Implements IHealthCareProfessional.HealthProfessionalModel
        Get
            Return _healthProfessionalModel
        End Get
        Set(value As HealthProfessionalModel)
            _healthProfessionalModel = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    Public WriteOnly Property IdentificationControlName As String
        Set(value As String)
            Me.INDlyItemNit.Text = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property IdentificationTypeId As Integer? Implements IHealthCareProfessional.IdentificationTypeId
        Get
            Return INDSleIdentificationType.EditValue
        End Get
        Set(value As Integer?)
            INDSleIdentificationType.EditValue = value
        End Set
    End Property

    Public Property SpecialtyExCode As String Implements IHealthCareProfessional.SpecialtyExCode
        Get
            Return INDsleSpecialtyEx.EditValue
        End Get
        Set(value As String)
            INDsleSpecialtyEx.EditValue = value
        End Set
    End Property

    Public Property ProfessionalLicenseNumber As String Implements IHealthCareProfessional.ProfessionalLicenseNumber
        Get
            Return INDtxtProfessionalLicenseNumber.EditValue
        End Get
        Set(value As String)
            INDtxtProfessionalLicenseNumber.EditValue = value
        End Set
    End Property

    Public Property ExternalProfessional As Boolean Implements IHealthCareProfessional.ExternalProfessional
        Get
            Return INDRgExternalProfessional.EditValue
        End Get
        Set(value As Boolean)
            INDRgExternalProfessional.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el contrato se liquida por defecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LiquidateDefault1 As Boolean? Implements IHealthCareProfessional.LiquidateDefault
        Get
            Return INDsleLiquidateDefault.EditValue
        End Get
        Set(value As Boolean?)
            INDsleLiquidateDefault.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IHealthCareProfessional.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
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
    ''' Obtiene o establece el id de la especialidad 1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SpecialtyId1 As String Implements IHealthCareProfessional.SpecialtyId1
        Get
            Return INDsleSpeciality1.EditValue
        End Get
        Set(value As String)
            INDsleSpeciality1.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la especialidad 2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SpecialtyId2 As String Implements IHealthCareProfessional.SpecialtyId2
        Get
            Return INDsleSpeciality2.EditValue
        End Get
        Set(value As String)
            INDsleSpeciality2.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la especialidad 3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SpecialtyId3 As String Implements IHealthCareProfessional.SpecialtyId3
        Get
            Return INDsleSpeciality3.EditValue
        End Get
        Set(value As String)
            INDsleSpeciality3.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [state].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [state]; otherwise, <c>false</c>.
    ''' </value>
    Public Property State As Integer
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Integer)
            If value = 1 Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la direccion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Address As String Implements IHealthCareProfessional.Address
        Get
            Return INDtxtAddress.EditValue
        End Get
        Set(value As String)
            INDtxtAddress.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el primer apellido
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FirstLastName As String Implements IHealthCareProfessional.FirstLastName
        Get
            Return INDtxtFirstLastName.EditValue
        End Get
        Set(value As String)
            INDtxtFirstLastName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el primer nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FirstName As String Implements IHealthCareProfessional.FirstName
        Get
            Return INDtxtFirstName.EditValue
        End Get
        Set(value As String)
            INDtxtFirstName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de ultima liquidación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LastLiquidationDate As DateTime? Implements IHealthCareProfessional.LastLiquidationDate
        Get
            Return INDdteLastLiquidationDate.EditValue
        End Get
        Set(value As DateTime?)
            INDdteLastLiquidationDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MedicalFeesContractId As Integer? Implements IHealthCareProfessional.MedicalFeesContractId
        Get
            Return INDsleMedicalFeesContract.EditValue
        End Get
        Set(value As Integer?)
            INDsleMedicalFeesContract.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MedicalFeesContractXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IHealthCareProfessional.MedicalFeesContractXpo
        Get
            Return INDsleMedicalFeesContract.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleMedicalFeesContract.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el movil
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Mobile As String Implements IHealthCareProfessional.Mobile
        Get
            Return INDtxtMobile.EditValue
        End Get
        Set(value As String)
            INDtxtMobile.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nit
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Nit As String Implements IHealthCareProfessional.Nit
        Get
            Return INDtxtNit.EditValue
        End Get
        Set(value As String)
            INDtxtNit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el medico realiza consulta externa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OutPatient As Boolean Implements IHealthCareProfessional.OutPatient
        Get
            Return INDsleOutPatient.EditValue
        End Get
        Set(value As Boolean)
            INDsleOutPatient.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el telefono
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Phone As String Implements IHealthCareProfessional.Phone
        Get
            Return INDtxtPhone.EditValue
        End Get
        Set(value As String)
            INDtxtPhone.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la profesion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Profession As Integer? Implements IHealthCareProfessional.Profession
        Get
            Return INDsleProfession.EditValue
        End Get
        Set(value As Integer?)
            INDsleProfession.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la tarjeta profesional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProfessionalCard As String Implements IHealthCareProfessional.ProfessionalCard
        Get
            Return INDtxtProfessionalCard.EditValue
        End Get
        Set(value As String)
            INDtxtProfessionalCard.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el segundo apellido
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SecondLastName As String Implements IHealthCareProfessional.SecondLastName
        Get
            Return INDtxtSecondLastName.EditValue
        End Get
        Set(value As String)
            INDtxtSecondLastName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el segundo nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SecondName As String Implements IHealthCareProfessional.SecondName
        Get
            Return INDtxtSecondName.EditValue
        End Get
        Set(value As String)
            INDtxtSecondName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del proveedor linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierDistributionLineId As Integer? Implements IHealthCareProfessional.SupplierDistributionLineId
        Get
            Return INDsleSupplierDistributionLine.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplierDistributionLine.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del proveedor linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierDistributionLineXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IHealthCareProfessional.SupplierDistributionLineXpo
        Get
            Return INDsleSupplierDistributionLine.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSupplierDistributionLine.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el medico tiene perfil de cirugia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SurgeryProfiler As String Implements IHealthCareProfessional.SurgeryProfiler
        Get
            Return INDsleSurgeryProfiler.EditValue
        End Get
        Set(value As String)
            INDsleSurgeryProfiler.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de vinculación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TypeLinkage As Integer? Implements IHealthCareProfessional.TypeLinkage
        Get
            Return INDsleTypeLinkage.EditValue
        End Get
        Set(value As Integer?)
            INDsleTypeLinkage.EditValue = value
        End Set
    End Property

    Public Property SpecialtyExtXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IHealthCareProfessional.SpecialtyExtXpo
        Get
            Return INDsleSpecialtyEx.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSpecialtyEx.Properties.DataSource = value
        End Set
    End Property

    Public Property IdentificationTypeXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IHealthCareProfessional.IdentificationTypeXpo
        Get
            Return INDSleIdentificationType.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleIdentificationType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Estabece el datasource de la especialidad 1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SpecialtyXpo1 As DevExpress.Xpo.XPInstantFeedbackSource Implements IHealthCareProfessional.SpecialtyXpo1
        Get
            Return INDsleSpeciality1.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSpeciality1.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Estabece el datasource de la especialidad 2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SpecialtyXpo2 As DevExpress.Xpo.XPInstantFeedbackSource Implements IHealthCareProfessional.SpecialtyXpo2
        Get
            Return INDsleSpeciality2.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSpeciality2.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Estabece el datasource de la especialidad 3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SpecialtyXpo3 As DevExpress.Xpo.XPInstantFeedbackSource Implements IHealthCareProfessional.SpecialtyXpo3
        Get
            Return INDsleSpeciality3.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSpeciality3.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que agrega un contrato profesional de la salud
    ''' a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddMedicalFeesContract()
        'Se valida que haya escogido un contrato
        If MedicalFeesContractId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontSelectMedicalFeesContract", NAME_MODULE)
            INDsleMedicalFeesContract.Focus()
            Exit Sub
        End If
        'Se valida que haya escogido si hay un contrato por defecto
        If LiquidateDefault1 Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontSelectLiquidateDefault", NAME_MODULE)
            INDsleMedicalFeesContract.Focus()
            Exit Sub
        End If
        If ListHealthProfessionalContract Is Nothing Then 'Si el listado viene nothing
            ListHealthProfessionalContract = New List(Of HealthProfessionalContract)
        Else 'Se valida que el contrato seleccionado no exista en la lista
            Dim cont = ListHealthProfessionalContract.FindAll(Function(item) item.MedicalFeesContractId = MedicalFeesContractId).Count
            If cont > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ExistMedicalFeesContract", NAME_MODULE), INDsleMedicalFeesContract.Text)
                INDsleMedicalFeesContract.Focus()
                Exit Sub
            End If
        End If
        'Se crea el nuevo objeto y se agrega al listado
        Dim healthProfessionalContract As New HealthProfessionalContract
        With healthProfessionalContract
            .HealthProfessionalCode = Code
            .MedicalFeesContractId = MedicalFeesContractId
            .MedicalFeesContractDescription = INDsleMedicalFeesContract.Text
            .LiquidateDefault = LiquidateDefault1
        End With
        ListHealthProfessionalContract.Add(healthProfessionalContract)
        INDgcMedicalFeesContract.DataSource = Nothing
        INDgcMedicalFeesContract.DataSource = ListHealthProfessionalContract
        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ContractAgregatedSatisfactory", NAME_MODULE)
        MedicalFeesContractId = Nothing
        INDsleMedicalFeesContract.Properties.NullText = String.Empty
        LiquidateDefault1 = False
        'Valido que en el listado exista al menos un true en LiquidateDefault
        Dim contTrue = ListHealthProfessionalContract.FindAll(Function(item) item.LiquidateDefault = True).Count
        If contTrue > 0 Then
            INDsleLiquidateDefault.Properties.ReadOnly = True
        End If
        INDsleMedicalFeesContract.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que elimina un contrato profesional de la salud de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteMedicalFeesContract()
        Dim healthProfessionalContract As HealthProfessionalContract = viewMedicalFeesContract.GetFocusedRow
        ListHealthProfessionalContract.Remove(healthProfessionalContract)
        If healthProfessionalContract.Id > 0 Then
            If ListDeleteHealthProfessionalContract Is Nothing Then
                ListDeleteHealthProfessionalContract = New List(Of HealthProfessionalContract)
            End If
            healthProfessionalContract.MarkAsDeleted()
            ListDeleteHealthProfessionalContract.Add(healthProfessionalContract)
        End If
        INDgcMedicalFeesContract.DataSource = Nothing
        INDgcMedicalFeesContract.DataSource = ListHealthProfessionalContract
        If ListHealthProfessionalContract.Count > 0 Then
            Dim cont = ListHealthProfessionalContract.FindAll(Function(item) item.LiquidateDefault = True).Count
            If cont = 0 Then
                INDsleLiquidateDefault.Properties.ReadOnly = False
            End If
        Else
            INDsleLiquidateDefault.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que carga el objeto para guardar en las propiedades de la entidad
    ''' de INPROFSAL
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadSpecialtyToPropertyEntity() As Task
        'Se consulta las especialidades para asignar el objeto
        'Specialty1
        If (INDsleSpeciality1.EditValue IsNot Nothing AndAlso INDsleSpeciality1.Text IsNot String.Empty AndAlso INESPECIA1 Is Nothing) OrElse
            (INDsleSpeciality1.EditValue IsNot Nothing AndAlso INDsleSpeciality1.Text IsNot String.Empty AndAlso INESPECIA1 IsNot Nothing AndAlso INESPECIA1.CODESPECI <> INDsleSpeciality1.EditValue) Then
            Using Model As New MHealthCareProfessional(Me.Tag)
                Dim _specialty = Await Model.GetSpecialityByCode(INDsleSpeciality1.EditValue)
                If _specialty.ObjectEmbbeded IsNot Nothing AndAlso _specialty.ObjectEmbbeded.CODESPECI.ToString.Trim IsNot String.Empty Then
                    Me.ProfessionalEHR.INESPECIA = _specialty.ObjectEmbbeded
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La Especialidad Con Código " & INDsleSpeciality1.EditValue & " No Fue Encontrada!"
                End If
            End Using
        End If
        'Specialty2
        If (INDsleSpeciality2.EditValue IsNot Nothing AndAlso INDsleSpeciality2.Text IsNot String.Empty AndAlso INESPECIA2 Is Nothing) OrElse
            (INDsleSpeciality2.EditValue IsNot Nothing AndAlso INDsleSpeciality2.Text IsNot String.Empty AndAlso INESPECIA2 IsNot Nothing AndAlso INESPECIA2.CODESPECI <> INDsleSpeciality2.EditValue) Then
            Using Model As New MHealthCareProfessional(Me.Tag)
                Dim _specialty = Await Model.GetSpecialityByCode(INDsleSpeciality2.EditValue)
                If _specialty.ObjectEmbbeded IsNot Nothing AndAlso _specialty.ObjectEmbbeded.CODESPECI.ToString.Trim IsNot String.Empty Then
                    ProfessionalEHR.INESPECIA1 = _specialty.ObjectEmbbeded
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La Especialidad Con Código " & INDsleSpeciality2.EditValue & " No Fue Encontrada!"
                End If
            End Using
        End If
        'Specialty3
        If (INDsleSpeciality3.EditValue IsNot Nothing AndAlso INDsleSpeciality3.Text IsNot String.Empty AndAlso INESPECIA3 Is Nothing) OrElse
            (INDsleSpeciality3.EditValue IsNot Nothing AndAlso INDsleSpeciality3.Text IsNot String.Empty AndAlso INESPECIA3 IsNot Nothing AndAlso INESPECIA3.CODESPECI <> INDsleSpeciality3.EditValue) Then
            Using Model As New MHealthCareProfessional(Me.Tag)
                Dim _specialty = Await Model.GetSpecialityByCode(INDsleSpeciality3.EditValue)
                If _specialty.ObjectEmbbeded IsNot Nothing AndAlso _specialty.ObjectEmbbeded.CODESPECI.ToString.Trim IsNot String.Empty Then
                    ProfessionalEHR.INESPECIA2 = _specialty.ObjectEmbbeded
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La Especialidad Con Código " & INDsleSpeciality3.EditValue & " No Fue Encontrada!"
                End If
            End Using
        End If
    End Function

    ''' <summary>
    ''' Inicializa las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        SetListIdentificationType()
        SetListOutPatient()
        SetListProfession()
        SetListSurgeryProfiler()
    End Sub

    ''' <summary>
    ''' listado de los tipos de vinculación
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetListIdentificationType()
        _listJobBondingType = New List(Of Tuple(Of Integer, String))
        _listJobBondingType.Add(New Tuple(Of Integer, String)(1, "Planta"))
        _listJobBondingType.Add(New Tuple(Of Integer, String)(2, "Contrato"))
        _listJobBondingType.Add(New Tuple(Of Integer, String)(3, "Residente"))
        _listJobBondingType.Add(New Tuple(Of Integer, String)(4, "Intero"))
        _listJobBondingType.Add(New Tuple(Of Integer, String)(5, "Externo"))
        INDsleTypeLinkage.Properties.DataSource = _listJobBondingType
        INDsleTypeLinkage.Properties.PopupFormSize = New Size(500, 350)
    End Sub

    ''' <summary>
    ''' listado de los tipos de si realiza o no consulta externa
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetListOutPatient()
        _listOutPatient = New List(Of Tuple(Of Integer, String))
        _listOutPatient.Add(New Tuple(Of Integer, String)(1, "Sí"))
        _listOutPatient.Add(New Tuple(Of Integer, String)(2, "No"))
        INDsleOutPatient.Properties.DataSource = _listOutPatient
        INDsleOutPatient.Properties.PopupFormSize = New Size(500, 350)
    End Sub

    ''' <summary>
    ''' listado de los tipos de profesional
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetListProfession()
        _listProfession = New List(Of Tuple(Of Integer, String))
        _listProfession.Add(New Tuple(Of Integer, String)(1, "Medico General"))
        _listProfession.Add(New Tuple(Of Integer, String)(2, "Medico Especialista"))
        _listProfession.Add(New Tuple(Of Integer, String)(3, "Enfermera"))
        _listProfession.Add(New Tuple(Of Integer, String)(4, "Auxiliar Enfermeria"))
        _listProfession.Add(New Tuple(Of Integer, String)(5, "Odontologo General"))
        _listProfession.Add(New Tuple(Of Integer, String)(6, "Odontologo Especialista"))
        _listProfession.Add(New Tuple(Of Integer, String)(7, "Nutricionista"))
        _listProfession.Add(New Tuple(Of Integer, String)(8, "Higienista"))
        _listProfession.Add(New Tuple(Of Integer, String)(9, "Psicologo"))
        _listProfession.Add(New Tuple(Of Integer, String)(10, "Trabajadora Social"))
        _listProfession.Add(New Tuple(Of Integer, String)(11, "Promotor de Saneamiento"))
        _listProfession.Add(New Tuple(Of Integer, String)(12, "Ingeniero Sanitario"))
        _listProfession.Add(New Tuple(Of Integer, String)(13, "Medico Veterinario"))
        _listProfession.Add(New Tuple(Of Integer, String)(14, "Ingeniero Alimento"))
        _listProfession.Add(New Tuple(Of Integer, String)(15, "Auxiliar Bacteriologo"))
        _listProfession.Add(New Tuple(Of Integer, String)(16, "Terapeuta"))
        _listProfession.Add(New Tuple(Of Integer, String)(17, "Optometra"))
        _listProfession.Add(New Tuple(Of Integer, String)(18, "Quimico Farmaceutico"))
        _listProfession.Add(New Tuple(Of Integer, String)(19, "Radiologo"))
        _listProfession.Add(New Tuple(Of Integer, String)(20, "Tecnologo Radiologo"))
        _listProfession.Add(New Tuple(Of Integer, String)(21, "Instrumentador Qx"))
        _listProfession.Add(New Tuple(Of Integer, String)(22, "Auxiliar Patologia"))
        _listProfession.Add(New Tuple(Of Integer, String)(23, "Otros"))
        _listProfession.Add(New Tuple(Of Integer, String)(24, "Medico Interno"))
        INDsleProfession.Properties.DataSource = _listProfession
        INDsleProfession.Properties.PopupFormSize = New Size(500, 350)
    End Sub

    ''' <summary>
    ''' listado de los tipos de los perfiles de cirugia
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetListSurgeryProfiler()
        _listSurgeryProfiler = New List(Of Tuple(Of String, String))
        _listSurgeryProfiler.Add(New Tuple(Of String, String)("0", "Ninguno"))
        _listSurgeryProfiler.Add(New Tuple(Of String, String)("1", "Cirujano"))
        _listSurgeryProfiler.Add(New Tuple(Of String, String)("2", "Anestesiólogos"))
        _listSurgeryProfiler.Add(New Tuple(Of String, String)("3", "Ayudantes"))
        _listSurgeryProfiler.Add(New Tuple(Of String, String)("4", "Anestesiólogo / Cirujano"))
        INDsleSurgeryProfiler.Properties.DataSource = _listSurgeryProfiler
        INDsleSurgeryProfiler.Properties.PopupFormSize = New Size(500, 350)
    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDlyHealthProfessional.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDtxtNit.Enabled = value
            INDtxtFirstName.Enabled = value
            INDtxtSecondName.Enabled = value
            INDtxtFirstLastName.Enabled = value
            INDtxtSecondLastName.Enabled = value
            INDtxtAddress.Enabled = value
            INDtxtPhone.Enabled = value
            INDtxtMobile.Enabled = value
            INDdteLastLiquidationDate.Enabled = value
            INDsleSupplierDistributionLine.Enabled = value
            INDsleTypeLinkage.Enabled = value
            INDsleOutPatient.Enabled = value
            INDsleSpeciality1.Enabled = value
            INDsleSpeciality2.Enabled = value
            INDsleSpeciality3.Enabled = value
            INDtxtProfessionalCard.Enabled = value
            INDsleProfession.Enabled = value
            INDsleSurgeryProfiler.Enabled = value
            INDpceMedicalFeesContract.Enabled = value
            INDsleMedicalFeesContract.Enabled = value
            INDbtnAddMedicalFeesContract.Enabled = value
            INDgcMedicalFeesContract.Enabled = value
            Me.INDRgExternalProfessional.Enabled = value
            Me.INDSleIdentificationType.Enabled = value
            Me.INDsleSpecialtyEx.Enabled = value
            Me.INDtxtProfessionalLicenseNumber.Enabled = value

            If value = False Then
                INDbtnCode.Focus()
            Else
                INDtxtNit.Focus()
            End If
            INDlyHealthProfessional.EndUpdate()
        End Set
    End Property

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
        INDlyHealthProfessional.BeginUpdate()
        INDlyItemCode.Enabled = True
        ActionsOnControls = False
        SupplierId = 0
        DistributionLinesId = 0
        Me.ProfessionalERP = Nothing
        Me.ProfessionalEHR = Nothing
        Me.HealthProfessionalModel = Nothing
        Code = String.Empty
        Nit = String.Empty
        FirstName = String.Empty
        SecondName = String.Empty
        FirstLastName = String.Empty
        SecondLastName = String.Empty
        Address = String.Empty
        Phone = String.Empty
        LastLiquidationDate = Nothing
        Mobile = String.Empty
        SupplierDistributionLineId = Nothing
        INDsleSupplierDistributionLine.Properties.NullText = String.Empty
        TypeLinkage = Nothing
        OutPatient = Nothing
        SpecialtyId1 = Nothing
        INDsleSpeciality1.Properties.NullText = String.Empty
        SpecialtyId2 = Nothing
        INDsleSpeciality2.Properties.NullText = String.Empty
        SpecialtyId3 = Nothing
        INDsleSpeciality3.Properties.NullText = String.Empty
        ProfessionalCard = String.Empty
        Profession = Nothing
        SurgeryProfiler = Nothing

        Me.ExternalProfessional = False
        Me.INDRgExternalProfessional.ReadOnly = False

        Me.IdentificationTypeId = Nothing
        Me.IdentificationTypeXpo = Nothing
        Me.INDSleIdentificationType.Properties.NullText = Nothing

        Me.ProfessionalLicenseNumber = String.Empty

        Me.SpecialtyExCode = Nothing
        Me.INDsleSpecialtyEx.Properties.NullText = Nothing
        Me.SpecialtyExtXpo = Nothing

        INESPECIA1 = Nothing
        INESPECIA2 = Nothing
        INESPECIA3 = Nothing
        ListHealthProfessionalContract = Nothing
        ListDeleteHealthProfessionalContract = Nothing

        MedicalFeesContractId = Nothing
        INDsleMedicalFeesContract.Properties.NullText = String.Empty
        LiquidateDefault1 = Nothing

        INDgcMedicalFeesContract.DataSource = Nothing
        INDsleLiquidateDefault.Properties.ReadOnly = False

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.CleanAuditBasic()
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        INDlyHealthProfessional.EndUpdate()
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MHealthCareProfessional(Me.Tag)
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo que inicializa un nuevo paciente sin numero de identificación
    ''' </summary>
    ''' <remarks></remarks>
    Async Function NewProfessional() As Task
        'Valido que el código que ingresaron exista como usuario en el HIS
        Using modelHealth As New MHealthCareProfessional(Tag)
            Dim result = Await modelHealth.GetUserHIS(INDbtnCode.Text.Trim)
            If result.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                Me.Deshacer()
                Exit Function
            End If
            If String.IsNullOrEmpty(result?.ObjectEmbbeded?.CODUSUARI) Then
                Me.ExternalProfessional = True
                Me.Nit = Me.Code
                Me.INDRgExternalProfessional.ReadOnly = True
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("UserDontExist", NAME_MODULE), INDbtnCode.Text.Trim)
            End If
        End Using
        Me.ProfessionalERP = New HealthProfessional
        Me.ProfessionalEHR = New INPROFSAL
        ActionsOnControls = True
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
    End Function

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If

        Using Model As New MHealthCareProfessional(Me.Tag)

            Try
                AsyncLoader(True)
                Dim resultOperation As ActionResult(Of HealthProfessionalModel) = Await Model.GetHealthProfessionalByCodeAsync(Me.Code)

                If resultOperation.StateResult = True Then
                    Me.HealthProfessionalModel = resultOperation.ObjectEmbbeded
                    Me.ProfessionalERP = HealthProfessionalModel.HealthProfessional

                    If ProfessionalERP IsNot Nothing AndAlso Not String.IsNullOrEmpty(Me.ProfessionalERP.IdentificationNumber) Then
                        Me.ProfessionalEHR = HealthProfessionalModel.INPROFSAL

                        If Not ProfessionalERP.ExternalProfessional AndAlso String.IsNullOrEmpty(ProfessionalEHR?.CODPROSAL) Then
                            Me.Deshacer()
                            Mensaje(EeventViewerImages.Advertencia) = "El profesional está marcado como interno, pero no está registrado en el módulo de profesionales de la salud del EHR."
                            Exit Function
                        End If

                        Me.BarraBotones.StatusRecordVisible = True
                        ActionsOnControls = True
                        Me.ExternalProfessional = ProfessionalERP.ExternalProfessional
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        Dim resultBlock = Await Model.GetBlockRecord(Me.Tag, ProfessionalERP.Id)

                        If Not ProfessionalERP.ExternalProfessional Then
                            Me.INDRgExternalProfessional.ReadOnly = True
                            With ProfessionalEHR
                                Code = .CODPROSAL
                                Nit = .CODIGONIT.ToString.Trim
                                FirstName = .MEDPRINOM.ToString.Trim
                                SecondName = .MEDSEGNOM.ToString.Trim
                                FirstLastName = .MEDPRIAPEL.ToString.Trim
                                SecondLastName = .MEDSEGAPEL.ToString.Trim
                                Address = .IMDIRECCI.ToString.Trim
                                Phone = .IMTELEFON.ToString.Trim
                                LastLiquidationDate = .FECULTLIQ
                                Mobile = .IMTELMOVI.ToString.Trim
                                SupplierDistributionLineId = .GENLINDIST
                                INDsleSupplierDistributionLine.Properties.NullText = .SupplierDistributionLineDesc
                                SupplierId = .GENPROVEE
                                TypeLinkage = .MEDTIPVIN
                                OutPatient = .REACONEXT

                                SpecialtyId1 = .CODESPEC1
                                SpecialtyId2 = .CODESPEC2
                                SpecialtyId3 = .CODESPEC3

                                INDsleSpeciality1.Properties.NullText = .Specialty1Description
                                INDsleSpeciality2.Properties.NullText = .Specialty2Description
                                INDsleSpeciality3.Properties.NullText = .Specialty3Description

                                ProfessionalCard = .TARJETAPR.ToString.Trim
                                Profession = .TIPPROFES
                                SurgeryProfiler = .MEDPERCIR
                                Me.State = .ESTADOMED
                            End With
                        Else
                            With ProfessionalERP
                                Me.Code = .IdentificationNumber
                                Me.Nit = .IdentificationNumber
                                Me.IdentificationTypeId = .IdentificationTypeId
                                Me.INDSleIdentificationType.Properties.NullText = .IdentificationTypeName
                                Me.FirstName = .FirstName
                                Me.SecondName = .SecondName
                                Me.FirstLastName = .FirstLastName
                                Me.SecondLastName = .SecondLastName
                                Me.SpecialtyExCode = .ProfessionalSpecialty
                                Me.INDsleSpecialtyEx.Properties.NullText = .ProfessionalSpecialtyCodeName
                                Me.ProfessionalLicenseNumber = .ProfessionalLicenseNumber
                                Me.State = If(.Status, 1, 0)
                            End With
                        End If

                        'Campos de auditoria
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), ProfessionalERP?.CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), ProfessionalERP?.CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), ProfessionalERP?.ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), ProfessionalERP?.ModificationDate)

                        If resultBlock.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            record = New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = ProfessionalERP.Id}
                            Dim operation = Await Model.SaveBlockRecord(record)
                            record = operation.ObjectEmbbeded
                        Else
                            record = resultBlock
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), resultBlock.CodUser, resultBlock.NameUser, resultBlock.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, resultBlock.CodUser)
                        End If

                        'Se consulta los detalles de contratos del medico
                        Dim result As ActionResult(Of List(Of HealthProfessionalContract)) = Await Model.GetListHealthProfessionalContractByHealthProfessionalCode(Code)
                        If result.StateResult = False Then
                            Mensaje(EeventViewerImages.Advertencia) = result.MessageResult(0).ToString
                            Deshacer()
                            Exit Function
                        End If

                        ListHealthProfessionalContract = result.ObjectEmbbeded
                        INDgcMedicalFeesContract.DataSource = Nothing
                        INDgcMedicalFeesContract.DataSource = ListHealthProfessionalContract
                        If ListHealthProfessionalContract IsNot Nothing AndAlso ListHealthProfessionalContract.Count > 0 Then
                            LiquidateDefault1 = False
                            INDsleLiquidateDefault.Properties.ReadOnly = True
                        End If

                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        INDlyItemCode.Enabled = False
                    Else
                        Await NewProfessional()
                    End If
                Else
                    If resultOperation.Message = "W" Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(resultOperation.MessageResult.Item(0), Me.Code)
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = resultOperation.Message
                    End If

                End If
            Finally
                AsyncLoader(False)
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Método para asignar valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Sub AssigningValues()

        With Me.ProfessionalERP
            .IdentificationNumber = Nit
            .IdentificationTypeId = If(IdentificationTypeId, 0)
            .FirstName = FirstName
            .SecondName = SecondName
            .FirstLastName = FirstLastName
            .SecondLastName = SecondLastName
            .ExternalProfessional = Me.ExternalProfessional
            .Status = CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
        End With

        If Me.ProfessionalERP.ExternalProfessional Then
            Me.ProfessionalERP.ProfessionalSpecialty = Me.SpecialtyExCode
            Me.ProfessionalERP.ProfessionalLicenseNumber = Me.ProfessionalLicenseNumber
            Me.ProfessionalEHR = Nothing
        Else
            Me.ProfessionalERP.ProfessionalSpecialty = Me.SpecialtyId1
            Me.ProfessionalERP.ProfessionalLicenseNumber = Me.ProfessionalCard

            If Me.ProfessionalEHR Is Nothing Then
                ProfessionalEHR = New INPROFSAL
            End If

            With Me.ProfessionalEHR
                .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
                .CODPROSAL = Code
                .CODIGONIT = Nit
                .MEDPRINOM = FirstName
                .MEDSEGNOM = SecondName
                .MEDPRIAPEL = FirstLastName
                .MEDSEGAPEL = SecondLastName
                .NOMMEDICO = String.Concat(.MEDPRINOM, " ", .MEDSEGNOM, " ", .MEDPRIAPEL, " ", .MEDSEGAPEL)
                .IMDIRECCI = Address
                .IMTELEFON = Phone
                .IMTELMOVI = Mobile
                .FECULTLIQ = LastLiquidationDate
                .GENLINDIST = INDsleSupplierDistributionLine.EditValue
                .GENPROVEE = SupplierId
                .MEDTIPVIN = TypeLinkage
                .REACONEXT = OutPatient
                .TARJETAPR = ProfessionalCard
                .TIPPROFES = Profession
                .MEDPERCIR = SurgeryProfiler

                .CODESPEC1 = SpecialtyId1
                .CODESPEC2 = SpecialtyId2
                .CODESPEC3 = SpecialtyId3

                Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
                    Case eActionsStatusRecords.Active
                        .ESTADOMED = 1
                    Case eActionsStatusRecords.Inactive
                        .ESTADOMED = 2
                End Select
            End With
        End If

        If Me.HealthProfessionalModel Is Nothing Then
            Me.HealthProfessionalModel = New HealthProfessionalModel()
        End If

        Me.HealthProfessionalModel.HealthProfessional = Me.ProfessionalERP
        Me.HealthProfessionalModel.INPROFSAL = Me.ProfessionalEHR
    End Sub

    Private Sub AllowValidateControls(value As Boolean)
        Me.INDlyItemProfession.AllowHide = Not value
        Me.INDlyItemProfession.ShowInCustomizationForm = Not value
        Me.INDlyItemProfession.AllowHide = Not value
        Me.INDlyItemProfession.ShowInCustomizationForm = Not value
        Me.INDlyItemTypeLinkage.AllowHide = Not value
        Me.INDlyItemTypeLinkage.ShowInCustomizationForm = Not value
        Me.INDlyItemSpecialty1.AllowHide = Not value
        Me.INDlyItemSpecialty1.ShowInCustomizationForm = Not value
        Me.INDlciIdentificationType.AllowHide = value
        Me.INDlciIdentificationType.ShowInCustomizationForm = value
    End Sub

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()

        Dim code As String
        Dim status As Boolean

        If Me.ProfessionalERP.ExternalProfessional Then
            code = Me.ProfessionalERP.IdentificationNumber
            status = Not Me.ProfessionalERP.Status
        Else
            code = Me.ProfessionalEHR.CODPROSAL
            status = If(ProfessionalEHR.ESTADOMED = 1, False, True)
        End If

        If Not String.IsNullOrEmpty(code) Then
            Try
                Using Model As New MHealthCareProfessional(Me.Tag)
                    AsyncLoader(True)
                    Dim Result = Await Model.UpdateStatusProfessionalHealthAsync(code, status)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        Me.HealthProfessionalModel = Result.ObjectEmbbeded
                        Me.ProfessionalERP = Me.HealthProfessionalModel.HealthProfessional
                        Me.ProfessionalEHR = Me.HealthProfessionalModel.INPROFSAL
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                    End If
                    AsyncLoader(False)
                End Using
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' Se habilitan los campos de profesionales externos o internos (true - externo, false - interno)
    ''' </summary>
    Public Sub HealtProfessionalControlsAvailable(value As Boolean)
        INDlygContract.HideControl(value)
        INDlcgProfessionalInformation.HideControl(value)
        INDlcgContractInformation.HideControl(value)
        INDlcgContactInformation.HideControl(value)
        INDLcgOtherData.HideControl(Not value)
        INDlciIdentificationType.HideControl(Not value)
        IdentificationControlName = If(value, "Identificación", "Nit")
        Me.AllowValidateControls(Not value)
    End Sub

#End Region

#Region "Events"

#Region "EditValueChanged"

    Private Sub INDRgExternalProfessional_EditValueChanged(sender As Object, e As EventArgs) Handles INDRgExternalProfessional.EditValueChanged
        Me.HealtProfessionalControlsAvailable(If(INDRgExternalProfessional.EditValue Is Nothing, False, Me.ExternalProfessional))
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDistributionLine_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSupplierDistributionLine.EditValueChanged
        If viewDistributionLine.GetFocusedRow IsNot Nothing Then
            SupplierId = 0
            If INDsleSupplierDistributionLine.EditValue IsNot Nothing AndAlso INDsleSupplierDistributionLine.Text IsNot String.Empty AndAlso INDsleSupplierDistributionLine.Properties.DataSource IsNot Nothing Then
                Dim suppplierMainAccount = DirectCast(DirectCast(viewDistributionLine.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo)
                SupplierId = suppplierMainAccount.IdSupplier.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la especialidad uno
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSpeciality1_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSpeciality1.EditValueChanged
        If SpecialtyId1 IsNot Nothing Then
            If SpecialtyId1 = SpecialtyId2 Then
                Mensaje(EeventViewerImages.Advertencia) = "No puede elegir esta especialidad, ya fue seleccionada en la especialidad 2."
                INDsleSpeciality1.Properties.NullText = String.Empty
                SpecialtyId1 = Nothing
            ElseIf SpecialtyId1 = SpecialtyId3 Then
                Mensaje(EeventViewerImages.Advertencia) = "No puede elegir esta especialidad, ya fue seleccionada en la especialidad 3."
                INDsleSpeciality1.Properties.NullText = String.Empty
                SpecialtyId1 = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la especialidad dos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSpeciality2_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSpeciality2.EditValueChanged
        If SpecialtyId2 IsNot Nothing Then
            If SpecialtyId2 = SpecialtyId1 Then
                Mensaje(EeventViewerImages.Advertencia) = "No puede elegir esta especialidad, ya fue seleccionada en la especialidad 1."
                INDsleSpeciality2.Properties.NullText = String.Empty
                SpecialtyId2 = Nothing
            ElseIf SpecialtyId2 = SpecialtyId3 Then
                Mensaje(EeventViewerImages.Advertencia) = "No puede elegir esta especialidad, ya fue seleccionada en la especialidad 3."
                INDsleSpeciality2.Properties.NullText = String.Empty
                SpecialtyId2 = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la especialidad tres
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSpeciality3_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSpeciality3.EditValueChanged
        If SpecialtyId3 IsNot Nothing Then
            If SpecialtyId3 = SpecialtyId1 Then
                Mensaje(EeventViewerImages.Advertencia) = "No puede elegir esta especialidad, ya fue seleccionada en la especialidad 1."
                INDsleSpeciality3.Properties.NullText = String.Empty
                SpecialtyId3 = Nothing
            ElseIf SpecialtyId3 = SpecialtyId2 Then
                Mensaje(EeventViewerImages.Advertencia) = "No puede elegir esta especialidad, ya fue seleccionada en la especialidad 2."
                INDsleSpeciality3.Properties.NullText = String.Empty
                SpecialtyId3 = Nothing
            End If
        End If
    End Sub

#End Region

#Region "FormClosing"
    ''' <summary>
    ''' Evento que se ejecuta al cerrar el formulario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmPatient_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento cuando se presiona una tecla en el código del paciente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbeIdentification_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If String.IsNullOrEmpty(INDbtnCode.Text) Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar un código."
                Exit Sub
            Else
                Await LoadControls()
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter o f4 al control del popup container
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceMedicalFeesContract_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceMedicalFeesContract.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceMedicalFeesContract.ShowPopup()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' carga los tipo de identificacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleIdentificationType_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleIdentificationType.QueryPopUp
        If IdentificationTypeXpo Is Nothing Then
            Using Model As New MHealthCareProfessional(Me.Tag)
                IdentificationTypeXpo = Model.ListIdentificationType
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Carga las especialidades para medicos externos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleSpecialtyEx_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleSpecialtyEx.QueryPopUp
        If SpecialtyExtXpo Is Nothing Then
            Using Model As New MHealthCareProfessional(Me.Tag)
                SpecialtyExtXpo = Model.ListSpecialties
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Carga los contratos cuando se abra el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleContract_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleMedicalFeesContract.QueryPopUp
        If MedicalFeesContractXpo Is Nothing Then
            Using Model As New MHealthCareProfessional(Me.Tag)
                MedicalFeesContractXpo = Model.ListMedicalFeesContract
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Carga las lineas de distribución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDistributionLine_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleSupplierDistributionLine.QueryPopUp
        If SupplierDistributionLineXpo Is Nothing Then
            Using Model As New MHealthCareProfessional(Me.Tag)
                SupplierDistributionLineXpo = Model.ListSupplierDistributionLine
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Lista las especialidades cuando se abre el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSpeciality1_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleSpeciality1.QueryPopUp
        If SpecialtyXpo1 Is Nothing Then
            Using Model As New MHealthCareProfessional(Me.Tag)
                SpecialtyXpo1 = Model.ListSpecialties
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Lista las especialidades cuando se abre el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSpeciality2_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleSpeciality2.QueryPopUp
        If SpecialtyXpo2 Is Nothing Then
            Using Model As New MHealthCareProfessional(Me.Tag)
                SpecialtyXpo2 = Model.ListSpecialties
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Lista las especialidades cuando se abre el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSpeciality3_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleSpeciality3.QueryPopUp
        If SpecialtyXpo3 Is Nothing Then
            Using Model As New MHealthCareProfessional(Me.Tag)
                SpecialtyXpo3 = Model.ListSpecialties
            End Using
        End If
    End Sub
#End Region

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        Model = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
        SearchMode = Nothing
        Me._professionalEHR = Nothing
        Me._professionalERP = Nothing
        Me._healthProfessionalModel = Nothing
        SupplierId = Nothing
        DistributionLinesId = Nothing
        _listJobBondingType = Nothing
        _listOutPatient = Nothing
        _listProfession = Nothing
        _listSurgeryProfiler = Nothing
        INESPECIA1 = Nothing
        INESPECIA2 = Nothing
        INESPECIA3 = Nothing
        ListHealthProfessionalContract = Nothing
        ListDeleteHealthProfessionalContract = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmProfessional_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyHealthProfessional, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        'Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PHealthCareProfessional(Me)

        '******* Inicializo Controles**********
        InitializeTuples()
        LoadStatus()
        Deshacer()
        INDsleSpeciality1.Properties.Buttons(1).Visible = False
        INDsleSpeciality2.Properties.Buttons(1).Visible = False
        INDsleSpeciality3.Properties.Buttons(1).Visible = False
        If INDsleMedicalFeesContract.Properties.Buttons.Count > 1 Then
            INDsleMedicalFeesContract.Properties.Buttons(1).Visible = False
        End If

        IndigoGridControl1.RefreshGrid(INDgcMedicalFeesContract)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewMedicalFeesContract, ListActions)

        SearchMode = False
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' La función simplemente establece el foco en el control INDbtnCode. Esto significa que cuando el formulario
    ''' FrmHealthCareProfessional se muestra, el control INDbtnCode será el primer control en recibir el foco,
    ''' lo que permite al usuario interactuar directamente con ese control sin tener que hacer clic en él.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmHealthCareProfessional_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbtnCode.Focus()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' esta función maneja el clic en el botón de adición en el control de selección de contrato médico. Abre un formulario relacionado con contratos médicos,
    ''' realiza algunas operaciones y actualiza la lista de contratos médicos en el modelo.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleContract_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMedicalFeesContract.ButtonClick
        'Genera referencia circular y por eso no puedo poner la referencia de medicalFees
        'If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
        '    Dim size As System.Drawing.Size
        '    size.Width = 780
        '    size.Height = 768
        '    Using pop As New FrmTransparent(New FrmMedicalFeesContract With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
        '        pop.Show()
        '    End Using
        '    Presenter.InitializeIPSService()
        'End If
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1300, Nothing, True)
            Using Model As New MHealthCareProfessional(Me.Tag)
                MedicalFeesContractXpo = Model.ListMedicalFeesContract
            End Using
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar.
    ''' Agrega un contrato profesional de la salud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddContract_Click(sender As Object, e As EventArgs) Handles INDbtnAddMedicalFeesContract.Click
        AddMedicalFeesContract()
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceMedicalFeesContract_Popup(sender As Object, e As EventArgs) Handles INDpceMedicalFeesContract.Popup
        INDsleMedicalFeesContract.Focus()
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Evento que se dispara para desplegar el menu de opciones de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteMedicalFeesContract()
    End Sub

    ''' <summary>
    ''' Evento que se dispara para desplegar el menu de opciones de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteMedicalFeesContract()
    End Sub

#End Region

#End Region

#Region "ICrud"

    ''' <summary>
    ''' a función Buscar(), se iniciaría un proceso
    ''' de búsqueda en la aplicación, que podría implicar
    ''' la presentación de una ventana de búsqueda o la activación
    ''' de alguna funcionalidad de búsqueda implementada en la interfaz del programa.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    '''  la función Deshacer() deshace los cambios realizados en los controles de la interfaz,
    '''  restablece la barra de herramientas a un estado apropiado según si está en modo de
    '''  búsqueda o no, y prepara la interfaz para permitir la creación de un nuevo registro
    '''  o la búsqueda de registros existentes, según corresponda.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
    End Sub

    ''' <summary>
    ''' la función Eliminar() se encarga de eliminar un profesional de la base de datos,
    ''' asegurándose de que se cumplan las condiciones adecuadas y mostrando mensajes de confirmación y estado según corresponda.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        Dim actionResult As ActionResult
        If (Me.ProfessionalERP.Id > 0 OrElse Not String.IsNullOrEmpty(Me.ProfessionalEHR?.CODPROSAL)) Then
            If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MHealthCareProfessional(Me.Tag)
                        AsyncLoader(True)
                        actionResult = Await Model.DeleteProfessional(If(Me.ProfessionalERP.ExternalProfessional, Me.ProfessionalERP.IdentificationNumber, Me.ProfessionalEHR.CODPROSAL))
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
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectProfessional", NAME_MODULE)
        End If
    End Sub

    ''' <summary>
    ''' la función Guardar() se encarga de guardar los cambios realizados en el registro de un médico en la base de datos,
    ''' asegurándose de que los controles sean válidos, verificando los contratos asociados y mostrando mensajes de confirmación y estado según corresponda.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Dim StatusOperation As Domain.Base.Entities.ObjectState
        If ValidateControls() = False Then
            Exit Sub
        End If
        If ListHealthProfessionalContract IsNot Nothing AndAlso ListHealthProfessionalContract.Count > 0 Then
            Dim cont = ListHealthProfessionalContract.FindAll(Function(item) item.LiquidateDefault = True).Count
            If cont = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Los contratos asociados al médico, ninguno liquida por defecto."
                Exit Sub
            End If
        End If
        AssigningValues()
        StatusOperation = Me.ProfessionalERP.ChangeTracker.State
        Try
            Using Model As New MHealthCareProfessional(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SaveHealthProfessionalAsync(Me.HealthProfessionalModel, ListHealthProfessionalContract, ListDeleteHealthProfessionalContract)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If StatusOperation = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("UpdateProfessional", NAME_MODULE), Me.Code)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveProfessional", NAME_MODULE), Me.Code)
                    End If
                    SearchMode = False
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Esta función implementa la interfaz ICrudBase y parece proporcionar una lógica específica relacionada
    ''' con la actualización de botones en una interfaz de usuario que se utiliza para administrar datos.
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

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
    ''' Esta función implementa la interfaz ICrudBase y parece proporcionar una lógica específica para manejar
    ''' la acción de "Nuevo" en una interfaz de usuario que se utiliza para administrar datos.
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' esta función se utiliza para abrir una ventana de búsqueda con criterios predefinidos y opciones de filtrado para buscar profesionales médicos.
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        Using Model As New MHealthCareProfessional(Me.Tag)
            FormSearchObjects = New FrmBusqueda
            AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
            With FormSearchObjects
                .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                                  New ColumnInfo() With {.Caption = "Nombre", .FieldName = "FullName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.6)},
                                  New ColumnInfo() With {.Caption = "Especialidad", .FieldName = "DescriptionSpeciality", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                                  New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)}}.ToList
                .ValorSolicitado = "Code"
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllHealthProfessional
                .FormParent = Me
                .ShowSearch()
            End With
        End Using
        SearchMode = True
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
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
    Private Async Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        If String.IsNullOrEmpty(INDbtnCode.Text) Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
            Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar un código."
            Exit Sub
        Else
            Await LoadControls()
        End If
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
        End If
    End Sub

#End Region

End Class
