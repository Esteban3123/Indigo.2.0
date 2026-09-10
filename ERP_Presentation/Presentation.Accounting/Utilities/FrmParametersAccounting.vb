'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 18-03-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Accounting.MVP
Imports Domain.Entities
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports System.Windows.Forms
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports System.IO

#End Region
''' <summary>
''' 
''' </summary>
Public Class FrmParametersAccounting
    Implements IParameterAccountig

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Accounting"
#End Region

#Region "Variables"


    ''' <summary>
    ''' Variable para poder acceder al modelo
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As New MAccountClass(Me.Tag)

    ''' <summary>
    ''' Representa la entidad de parametros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _settingAccount As GeneralLedgerSettings

    ''' <summary>
    ''' Representa la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _IdOperationUnity As Integer

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordGeneralLedger

    ''' <summary>
    ''' Instancia de los valores de sesión
    ''' </summary>
    Private _indigoSession As SessionValues

    ''' <summary>
    ''' Presentador de los parametros de contabilidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Presenter As PParametersAccounting

#End Region

#Region "Properties"

    ''' <summary>
    ''' Datasource de tipo de documento
    ''' </summary>
    ''' <returns></returns>
    Private Property XpoDocumenttype As XPInstantFeedbackSource

    ''' <summary>
    ''' contiene la cuenta de deficit
    ''' </summary>
    ''' <returns></returns>
    Public Property IdDeficitAccount As Integer? Implements IParameterAccountig.IdDeficitAccount
        Get
            Return INDsleIdDeficitAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleIdDeficitAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene la cuenta de ganancias
    ''' </summary>
    ''' <returns></returns>
    Public Property IdUtilityAccount As Integer? Implements IParameterAccountig.IdUtilityAccount
        Get
            Return INDsleIdUtilityAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleIdUtilityAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene la cuenta de superavit
    ''' </summary>
    ''' <returns></returns>
    Public Property IdSuperavitAccount As Integer? Implements IParameterAccountig.IdSuperavitAccount
        Get
            Return INDsleIdSuperavitAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleIdSuperavitAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene le nombre del revisro fiscal
    ''' </summary>
    ''' <returns></returns>
    Public Property TaxReviewer As String Implements IParameterAccountig.TaxReviewer
        Get
            Return INDtxtTaxReviewer.Text
        End Get
        Set(value As String)
            INDtxtTaxReviewer.Text = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el comprobante de homologacion
    ''' </summary>
    ''' <returns></returns>
    Public Property IdApprovalDocument As Integer? Implements IParameterAccountig.IdApprovalDocument
        Get
            Return INDsleIdApprovalDocument.EditValue
        End Get
        Set(value As Integer?)
            INDsleIdApprovalDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene la tesoreria distrital
    ''' </summary>
    ''' <returns></returns>
    Public Property IdDistrictTreasury As Integer? Implements IParameterAccountig.IdDistrictTreasury
        Get
            Return INDsleIdDistrictTreasury.EditValue
        End Get
        Set(value As Integer?)
            INDsleIdDistrictTreasury.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece si aplica o no firmas
    ''' </summary>
    ''' <returns></returns>
    Public Property PrintSignature As Boolean? Implements IParameterAccountig.PrintSignature
        Get
            Return IIf(String.IsNullOrEmpty(INDslePrintSignature.EditValue), False, INDslePrintSignature.EditValue)
        End Get
        Set(value As Boolean?)
            INDslePrintSignature.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el nit de la Dian
    ''' </summary>
    ''' <returns></returns>
    Public Property IdDian As Integer? Implements IParameterAccountig.IdDian
        Get
            Return INDsleIdDian.EditValue
        End Get
        Set(value As Integer?)
            INDsleIdDian.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el cargo del que firma
    ''' </summary>
    ''' <returns></returns>
    Public Property Potition As String Implements IParameterAccountig.Potition
        Get
            Return INDtxtPosition.Text
        End Get
        Set(value As String)
            INDtxtPosition.Text = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el numero de la tarjeta profesional
    ''' </summary>
    ''' <returns></returns>
    Public Property ProfessionalCard As String Implements IParameterAccountig.ProfessionalCard
        Get
            Return INDtxtProfessionalCard.Text
        End Get
        Set(value As String)
            INDtxtProfessionalCard.Text = value
        End Set
    End Property

    ''' <summary>
    ''' contiene la cuenta de cierre
    ''' </summary>
    ''' <returns></returns>
    Public Property IdCloseDocument As Integer? Implements IParameterAccountig.IdCloseDocument
        Get
            Return INDsleIdCloseDocument.EditValue
        End Get
        Set(value As Integer?)
            INDsleIdCloseDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el id de la retefuente
    ''' </summary>
    ''' <returns></returns>
    Public Property IdSourceRetentionConcept As Integer? Implements IParameterAccountig.IdSourceRetentionConcept
        Get
            Return INDsleIdSourceRetentionConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleIdSourceRetentionConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el id de la retencion del ica
    ''' </summary>
    ''' <returns></returns>
    Public Property IdIcaRetentionConcept As Integer? Implements IParameterAccountig.IdIcaRetentionConcept
        Get
            Return INDsleIdIcaRetentionConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleIdIcaRetentionConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el id de la retencion del iva
    ''' </summary>
    ''' <returns></returns>
    Public Property IdIvaRetentionConcept As Integer? Implements IParameterAccountig.IdIvaRetentionConcept
        Get
            Return INDsleIdIvaRetentionConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleIdIvaRetentionConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' contiene el id del comprobante de translados
    ''' </summary>
    ''' <returns></returns>
    Public Property IdMovementDocument As Integer? Implements IParameterAccountig.IdMovementDocument
        Get
            Return INDsleIdMovementDocument.EditValue
        End Get
        Set(value As Integer?)
            INDsleIdMovementDocument.EditValue = value
        End Set

    End Property

    ''' <summary>
    ''' Gets or sets the _ firm1.
    ''' </summary>
    ''' <value>
    ''' The _ firm1.
    ''' </value>
    Public Property Signature As String Implements IParameterAccountig.Signature
        Get
            Return INDtxtSignature.Text
        End Get
        Set(value As String)
            INDtxtSignature.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el Id de documento de aprobación
    ''' </summary>
    ''' <returns></returns>
    Public Property IdApprovalDocumentXpo As XPInstantFeedbackSource Implements IParameterAccountig.IdApprovalDocumentXpo
        Get
            Return INDsleIdApprovalDocument.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIdApprovalDocument.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    '''  Contiene el Id de documento de cierre
    ''' </summary>
    ''' <returns></returns>
    Public Property IdCloseDocumentXpo As XPInstantFeedbackSource Implements IParameterAccountig.IdCloseDocumentXpo
        Get
            Return INDsleIdCloseDocument.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIdCloseDocument.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el Id de cuenta deficit
    ''' </summary>
    ''' <returns></returns>
    Public Property IdDeficitAccountXpo As XPInstantFeedbackSource Implements IParameterAccountig.IdDeficitAccountXpo
        Get
            Return INDsleIdDeficitAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIdDeficitAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el Id del Nit
    ''' </summary>
    ''' <returns></returns>
    Public Property IdDianXpo As XPInstantFeedbackSource Implements IParameterAccountig.IdDianXpo
        Get
            Return INDsleIdDian.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIdDian.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el Id de tesorería distrital
    ''' </summary>
    ''' <returns></returns>
    Public Property IdDistrictTreasuryXpo As XPInstantFeedbackSource Implements IParameterAccountig.IdDistrictTreasuryXpo
        Get
            Return INDsleIdDistrictTreasury.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIdDistrictTreasury.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el Id del concepto de retención ICA
    ''' </summary>
    ''' <returns></returns>
    Public Property IdIcaRetentionConceptXpo As XPInstantFeedbackSource Implements IParameterAccountig.IdIcaRetentionConceptXpo
        Get
            Return INDsleIdIcaRetentionConcept.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIdIcaRetentionConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el Id del concepto de retención IVA
    ''' </summary>
    ''' <returns></returns>
    Public Property IdIvaRetentionConceptXpo As XPInstantFeedbackSource Implements IParameterAccountig.IdIvaRetentionConceptXpo
        Get
            Return INDsleIdIvaRetentionConcept.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIdIvaRetentionConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el Id de comprobante de traslados
    ''' </summary>
    ''' <returns></returns>
    Public Property IdMovementDocumentXpo As XPInstantFeedbackSource Implements IParameterAccountig.IdMovementDocumentXpo
        Get
            Return INDsleIdMovementDocument.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIdMovementDocument.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el Id del concepto de retención en la fuente
    ''' </summary>
    ''' <returns></returns>
    Public Property IdSourceRetentionConceptXpo As XPInstantFeedbackSource Implements IParameterAccountig.IdSourceRetentionConceptXpo
        Get
            Return INDsleIdSourceRetentionConcept.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIdSourceRetentionConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el Id de cuenta superavit
    ''' </summary>
    ''' <returns></returns>
    Public Property IdSuperavitAccountXpo As XPInstantFeedbackSource Implements IParameterAccountig.IdSuperavitAccountXpo
        Get
            Return INDsleIdSuperavitAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIdSuperavitAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el Id de cuenta de ganancias
    ''' </summary>
    ''' <returns></returns>
    Public Property IdUtilityAccountXpo As XPInstantFeedbackSource Implements IParameterAccountig.IdUtilityAccountXpo
        Get
            Return INDsleIdUtilityAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIdUtilityAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Validar datos cliente
    ''' </summary>
    ''' <returns></returns>
    Public Property ValidateClientData As Boolean Implements IParameterAccountig.ValidateClientData
        Get
            Return CtrValidateClientData.EditValue
        End Get
        Set(value As Boolean)
            CtrValidateClientData.EditValue = value
        End Set
    End Property

#Region "ElectronicBilling"

    ''' <summary>
    ''' establece si maneja o no facturación electrónica
    ''' </summary>
    ''' <returns></returns>
    Public Property HandlesElectronicBilling As Boolean Implements IParameterAccountig.HandlesElectronicBilling
        Get
            Return IIf(String.IsNullOrEmpty(INDsleHandlesElectronicBilling.EditValue), False, INDsleHandlesElectronicBilling.EditValue)
        End Get
        Set(value As Boolean)
            INDsleHandlesElectronicBilling.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece el identificador del software
    ''' </summary>
    ''' <returns></returns>
    Public Property SoftwareIdentifier As String Implements IParameterAccountig.SoftwareIdentifier
        Get
            Return INDtxtSoftwareIdentifier.EditValue
        End Get
        Set(value As String)
            INDtxtSoftwareIdentifier.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece el pin del software
    ''' </summary>
    ''' <returns></returns>
    Public Property SoftwarePin As String Implements IParameterAccountig.SoftwarePin
        Get
            Return INDtxtSoftwarePin.EditValue
        End Get
        Set(value As String)
            INDtxtSoftwarePin.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Entorno de ejecución de la factura electrónica
    ''' </summary>
    ''' <returns></returns>
    Public Property Environment As Boolean Implements IParameterAccountig.Environment
        Get
            Return IIf(String.IsNullOrEmpty(INDGleEnvironment.EditValue), False, INDGleEnvironment.EditValue)
        End Get
        Set(value As Boolean)
            INDGleEnvironment.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece la clave del set de pruebas
    ''' </summary>
    ''' <returns></returns>
    Public Property TestSetId As String Implements IParameterAccountig.TestSetId
        Get
            Return INDtxtTestSetId.EditValue
        End Get
        Set(value As String)
            INDtxtTestSetId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece el certificado digital
    ''' </summary>
    ''' <returns></returns>
    Public Property DigitalCertificate As Byte() Implements IParameterAccountig.DigitalCertificate

    ''' <summary>
    ''' establece la contraseña del certificado digital
    ''' </summary>
    ''' <returns></returns>
    Public Property DigitalCertificateKey As String Implements IParameterAccountig.DigitalCertificateKey
        Get
            Return INDtxtDigitalCertificateKey.EditValue
        End Get
        Set(value As String)
            INDtxtDigitalCertificateKey.EditValue = value
        End Set
    End Property

#End Region

#Region "ElectronicPayroll"

    ''' <summary>
    ''' establece si maneja o no facturación electrónica
    ''' </summary>
    ''' <returns></returns>
    Public Property HandlesElectronicPayroll As Boolean Implements IParameterAccountig.HandlesElectronicPayroll
        Get
            Return IIf(String.IsNullOrEmpty(INDSleHandlesElectronicPayroll.EditValue), False, INDSleHandlesElectronicPayroll.EditValue)
        End Get
        Set(value As Boolean)
            INDSleHandlesElectronicPayroll.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece el identificador del software
    ''' </summary>
    ''' <returns></returns>
    Public Property ElectronicPayrollIdentifier As String Implements IParameterAccountig.ElectronicPayrollIdentifier
        Get
            Return INDtxtElectronicPayrollIdentifier.EditValue
        End Get
        Set(value As String)
            INDtxtElectronicPayrollIdentifier.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece el pin del software
    ''' </summary>
    ''' <returns></returns>
    Public Property ElectronicPayrollPin As String Implements IParameterAccountig.ElectronicPayrollPin
        Get
            Return INDtxtElectronicPayrollPin.EditValue
        End Get
        Set(value As String)
            INDtxtElectronicPayrollPin.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Entorno de ejecución de la nómina electrónica
    ''' </summary>
    ''' <returns></returns>
    Public Property ElectronicPayrollEnvironment As Boolean Implements IParameterAccountig.ElectronicPayrollEnvironment
        Get
            Return IIf(String.IsNullOrEmpty(INDGleElectronicPayrollEnvironment.EditValue), False, INDGleElectronicPayrollEnvironment.EditValue)
        End Get
        Set(value As Boolean)
            INDGleElectronicPayrollEnvironment.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece la clave del set de pruebas
    ''' </summary>
    ''' <returns></returns>
    Public Property ElectronicPayrollTestSetId As String Implements IParameterAccountig.ElectronicPayrollTestSetId
        Get
            Return INDtxtElectronicPayrollTestSetId.EditValue
        End Get
        Set(value As String)
            INDtxtElectronicPayrollTestSetId.EditValue = value
        End Set
    End Property

#End Region
#Region "SupportDocument"

    ''' <summary>
    ''' establece si maneja o no facturación electrónica
    ''' </summary>
    ''' <returns></returns>
    Public Property HandlesSupportDocument As Boolean Implements IParameterAccountig.HandlesSupportDocument
        Get
            Return IIf(String.IsNullOrEmpty(INDsleHandlesSupportDocument.EditValue), False, INDsleHandlesSupportDocument.EditValue)
        End Get
        Set(value As Boolean)
            INDsleHandlesSupportDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece el identificador del software
    ''' </summary>
    ''' <returns></returns>
    Public Property SupportDocumentIdentifier As String Implements IParameterAccountig.SupportDocumentIdentifier
        Get
            Return INDtxtSupportDocumentIdentifier.EditValue
        End Get
        Set(value As String)
            INDtxtSupportDocumentIdentifier.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece el pin del software
    ''' </summary>
    ''' <returns></returns>
    Public Property SupportDocumentPin As String Implements IParameterAccountig.SupportDocumentPin
        Get
            Return INDtxtSupportDocumentPin.EditValue
        End Get
        Set(value As String)
            INDtxtSupportDocumentPin.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Entorno de ejecución de la nómina electrónica
    ''' </summary>
    ''' <returns></returns>
    Public Property SupportDocumentEnvironment As Boolean Implements IParameterAccountig.SupportDocumentEnvironment
        Get
            Return IIf(String.IsNullOrEmpty(INDGleSupportDocumentEnvironment.EditValue), False, INDGleSupportDocumentEnvironment.EditValue)
        End Get
        Set(value As Boolean)
            INDGleSupportDocumentEnvironment.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece la clave del set de pruebas
    ''' </summary>
    ''' <returns></returns>
    Public Property SupportDocumentTestSetId As String Implements IParameterAccountig.SupportDocumentTestSetId
        Get
            Return INDtxtSupportDocumentTestSetId.EditValue
        End Get
        Set(value As String)
            INDtxtSupportDocumentTestSetId.EditValue = value
        End Set
    End Property

#End Region

#End Region

#Region "CRUD"

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Using Model As New MAccountClass(Me.Tag.ToString())
                AsyncLoader(True)
                'accountClass.MarkAsDeleted()
                'Dim result = Await Model.DeleteAccountClass(accountClass)
                'If result.StateResult = True Then
                '    Await Me.DeleteDocumentIndexed()
                '    AsyncLoader(False)
                '    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                '    Me.Deshacer()
                'Else
                '    If result.MessageResult(0) = "-999" Then
                '        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                '    ElseIf result.MessageResult(0) = "-000" Then
                '        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                '    Else
                '        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                '    End If
                '    AsyncLoader(False)
                'End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        If HandlesElectronicBilling = True
            if DigitalCertificate Is Nothing
                Mensaje(EeventViewerImages.Advertencia) = "Por favor agregue el Certificado Digital"
                Exit Sub
            End If

            Try
                Dim certificate As New Security.Cryptography.X509Certificates.X509Certificate2(DigitalCertificate, DigitalCertificateKey)
                if certificate Is Nothing
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontró un certificado válido"
                    Exit Sub
                ElseIf certificate.HasPrivateKey = False
                    Mensaje(EeventViewerImages.Advertencia) = "El certificado no contiene ninguna clave privada"
                    Exit Sub
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = "Ocurrio un error al intentar asociar la contraseña con el certificado digital"
                Exit Sub
            End Try
        End If

        AssigningValues()
        Try
            AsyncLoader(True)
            Using Model As New MSettingsAccount(Me.Tag.ToString())
                Dim Result = Await Model.SaveSettingAccount(_settingAccount)
                If Result.StateResult = True Then
                    If _settingAccount.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me._settingAccount = Result.ObjectEmbbeded
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                    BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                    Deshacer()
                    LoadControls()
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
                AsyncLoader(False)
            End Using
            INDsleIdDeficitAccount.Focus()
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = ResourceManager.GetString("StateActive"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = ResourceManager.GetString("StateInactive"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Inicializa los search que van quemados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        Dim ListYesOrNot = New List(Of Tuple(Of Boolean, String))
        ListYesOrNot.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListYesOrNot.Add(New Tuple(Of Boolean, String)(False, "No"))

        INDsleHandlesElectronicBilling.Properties.DataSource = ListYesOrNot.ToList
        INDSleHandlesElectronicPayroll.Properties.DataSource = ListYesOrNot.ToList
        INDslePrintSignature.Properties.DataSource = ListYesOrNot.ToList
        INDsleHandlesSupportDocument.Properties.DataSource = ListYesOrNot.ToList

        Dim ListEnvironment = New List(Of Tuple(Of Boolean, String))
        ListEnvironment.Add(New Tuple(Of Boolean, String)(True, "Producción"))
        ListEnvironment.Add(New Tuple(Of Boolean, String)(False, "Pruebas"))

        INDGleEnvironment.Properties.DataSource = ListEnvironment.ToList
        INDGleElectronicPayrollEnvironment.Properties.DataSource = ListEnvironment.ToList
        INDGleSupportDocumentEnvironment.Properties.DataSource = ListEnvironment.ToList
    End Sub

    Public Sub LoadDigitalCertificate()
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Certificado Digital(*.CER;*.DER;*.PEM;*.P7B;*.P7C;*.PFX;*.P12)|*.CER;*.DER;*.PEM;*.P7B;*.P7C;*.PFX;*.P12"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            DigitalCertificate = File.ReadAllBytes(openFileDialog1.FileName)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        DeleteBlockedRecord()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        BarraBotones.CleanAuditBasic()
        IdDeficitAccount = Nothing
        INDsleIdDeficitAccount.Properties.NullText = String.Empty
        IdSuperavitAccount = Nothing
        INDsleIdSuperavitAccount.Properties.NullText = String.Empty
        IdUtilityAccount = Nothing
        INDsleIdUtilityAccount.Properties.NullText = String.Empty
        IdCloseDocument = Nothing
        INDsleIdCloseDocument.Properties.NullText = String.Empty
        IdDian = Nothing
        INDsleIdDian.Properties.NullText = String.Empty
        IdDistrictTreasury = Nothing
        INDsleIdDistrictTreasury.Properties.NullText = String.Empty
        IdApprovalDocument = Nothing
        INDsleIdApprovalDocument.Properties.NullText = String.Empty
        IdMovementDocument = Nothing
        INDsleIdMovementDocument.Properties.NullText = String.Empty
        IdIvaRetentionConcept = Nothing
        INDsleIdIvaRetentionConcept.Properties.NullText = String.Empty
        IdIcaRetentionConcept = Nothing
        INDsleIdIcaRetentionConcept.Properties.NullText = String.Empty
        IdSourceRetentionConcept = Nothing
        INDsleIdSourceRetentionConcept.Properties.NullText = String.Empty
        PrintSignature = Nothing
        Signature = String.Empty
        Potition = String.Empty
        TaxReviewer = String.Empty
        ProfessionalCard = String.Empty

        HandlesElectronicBilling = Nothing
        SoftwareIdentifier = Nothing
        SoftwarePin = Nothing
        Environment = Nothing
        TestSetId = Nothing
        DigitalCertificate = Nothing
        DigitalCertificateKey = Nothing

        HandlesElectronicPayroll = Nothing
        ElectronicPayrollIdentifier = Nothing
        ElectronicPayrollPin = Nothing
        ElectronicPayrollEnvironment = Nothing
        ElectronicPayrollTestSetId = Nothing

        HandlesSupportDocument = Nothing
        SupportDocumentIdentifier = Nothing
        SupportDocumentPin = Nothing
        SupportDocumentEnvironment = Nothing
        SupportDocumentTestSetId = Nothing

        DeleteBlockedRecord()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Sub LoadControls()
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        Me.BarraBotones.StatusRecordVisible = True

        Using Model As New MSettingsAccount(Me.Tag)
            AsyncLoader(True)
            _settingAccount = Await Model.GetSettingAccount(BarraBotones.OperatingUnitValue)
            If _settingAccount IsNot Nothing AndAlso _settingAccount.Id > 0 Then

                Dim result = Await Model.GetBlockRecord(Me.Tag, _settingAccount.Id)
                With _settingAccount
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                    IdDeficitAccount = .IdDeficitAccount
                    INDsleIdDeficitAccount.Properties.NullText = .IdDeficitAccountDescription

                    IdSuperavitAccount = .IdSuperavitAccount
                    INDsleIdSuperavitAccount.Properties.NullText = .IdSuperavitAccountDescription

                    IdUtilityAccount = .IdUtilityAccount
                    INDsleIdUtilityAccount.Properties.NullText = .IdUtilityAccountDescription

                    IdCloseDocument = .IdCloseDocument
                    INDsleIdCloseDocument.Properties.NullText = .IdCloseDocumentDescription

                    IdDian = .IdDian
                    INDsleIdDian.Properties.NullText = .IdDianDescription

                    IdDistrictTreasury = .IdDistrictTreasury
                    INDsleIdDistrictTreasury.Properties.NullText = .IdDistrictTreasuryDescription

                    IdApprovalDocument = .IdApprovalDocument
                    INDsleIdApprovalDocument.Properties.NullText = .IdApprovalDocumentDescription

                    IdMovementDocument = .IdMovementDocument
                    INDsleIdMovementDocument.Properties.NullText = .IdMovementDocumentDescription

                    IdIvaRetentionConcept = .IdIvaRetentionConcept
                    INDsleIdIvaRetentionConcept.Properties.NullText = .IdIvaRetentionConceptDescription

                    IdIcaRetentionConcept = .IdIcaRetentionConcept
                    INDsleIdIcaRetentionConcept.Properties.NullText = .IdIcaRetentionConceptDescription

                    IdSourceRetentionConcept = .IdSourceRetentionConcept
                    INDsleIdSourceRetentionConcept.Properties.NullText = .IdSourceRetentionConceptDescription
                    ValidateClientData = .ValidateClientData
                    PrintSignature = .PrintSignature
                    If PrintSignature = True Then
                        Signature = .Signature
                        Potition = .Potition
                        TaxReviewer = .TaxReviewer
                        ProfessionalCard = .ProfessionalCard
                    End If

                    HandlesElectronicPayroll = .HandlesElectronicPayroll
                    If HandlesElectronicPayroll Then
                        ElectronicPayrollIdentifier = .ElectronicPayrollIdentifier
                        ElectronicPayrollPin = .ElectronicPayrollPin
                        ElectronicPayrollEnvironment = .ElectronicPayrollEnvironment
                        ElectronicPayrollTestSetId = .ElectronicPayrollTestSetId
                    End If

                    HandlesElectronicBilling = .HandlesElectronicBilling
                    If HandlesElectronicBilling Then
                        SoftwareIdentifier = .SoftwareIdentifier
                        SoftwarePin = .SoftwarePin
                        Environment = .Environment
                        TestSetId = .TestSetId
                        DigitalCertificate = .DigitalCertificate
                        DigitalCertificateKey = .DigitalCertificateKey
                    End If

                    HandlesSupportDocument = .HandlesSupportDocument
                    If HandlesSupportDocument Then
                        SupportDocumentIdentifier = .SupportDocumentIdentifier
                        SupportDocumentPin = .SupportDocumentPin
                        SupportDocumentEnvironment = .SupportDocumentEnvironment
                        SupportDocumentTestSetId = .SupportDocumentTestSetId
                    End If

                End With
                AsyncLoader(False)
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    _record = New BlockRecordGeneralLedger With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _settingAccount.Id}
                    Dim operation = Await Model.SaveBlockRecord(_record)
                    _record = operation.ObjectEmbbeded
                Else
                    _record = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
                Me.BarraBotones.SetDocuments(_settingAccount.Id)
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            Else
                AsyncLoader(False)
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmParametersAccounting_DontExist", NAME_MODULE)
                BarraBotones.PrepareToolbar(eAction.OnlySave)
                CleanControls()
                _settingAccount = New GeneralLedgerSettings()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Deletes the blocked record.
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record Is Nothing Then
            Exit Sub
        End If
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MSettingsAccount(Me.Tag)
                Await Model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento indexado
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        'Dim dateServer = Me.GetDateServer()
        'If Me._doc Is Nothing Then
        '    Me._doc = New IndexedDocument2 With {.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.accountClass.Code, Me.accountClass.Name), .CreationDate = dateServer, .CreationUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName, .JournalVoucher = IndexedDocumentType.File, .IdEntity = "$#" & Me.Tag & "_" & Me.accountClass.Code & "#$", .IdForm = Me.Tag, .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.accountClass.Code), .Update = dateServer, .UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName}
        '    Return Me._doc
        'Else
        '    Me._doc.Update = dateServer
        '    Me._doc.UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName
        '    Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.accountClass.Code, Me.accountClass.Name)
        '    Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.accountClass.Code)
        '    Return Me._doc
        'End If
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    ''' <exception cref="System.NotImplementedException"></exception>
    Private Sub AssigningValues()
        With _settingAccount
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .IdDeficitAccount = IdDeficitAccount
            .IdSuperavitAccount = IdSuperavitAccount
            .IdUtilityAccount = IdUtilityAccount
            .IdCloseDocument = IdCloseDocument
            .IdDian = IdDian
            .IdDistrictTreasury = IdDistrictTreasury
            .IdApprovalDocument = IdApprovalDocument
            .IdMovementDocument = IdMovementDocument
            .IdIvaRetentionConcept = IdIvaRetentionConcept
            .IdIcaRetentionConcept = IdIcaRetentionConcept
            .IdSourceRetentionConcept = IdSourceRetentionConcept
            .PrintSignature = PrintSignature
            If PrintSignature = True Then
                .Signature = Signature
                .Potition = Potition
                .TaxReviewer = TaxReviewer
                .ProfessionalCard = ProfessionalCard
            Else
                .Signature = String.Empty
                .Potition = String.Empty
                .TaxReviewer = String.Empty
                .ProfessionalCard = String.Empty
            End If

            .HandlesElectronicBilling = HandlesElectronicBilling
            .DianVersion = 2.1
            .SoftwareIdentifier = SoftwareIdentifier
            .SoftwarePin = SoftwarePin
            .Environment = Environment
            .TestSetId = TestSetId
            .DigitalCertificate = DigitalCertificate
            .DigitalCertificateKey = DigitalCertificateKey

            .HandlesElectronicPayroll = HandlesElectronicPayroll
            .ElectronicPayrollIdentifier = ElectronicPayrollIdentifier
            .ElectronicPayrollPin = ElectronicPayrollPin
            .ElectronicPayrollEnvironment = ElectronicPayrollEnvironment
            .ElectronicPayrollTestSetId = ElectronicPayrollTestSetId

            .HandlesSupportDocument = HandlesSupportDocument
            .SupportDocumentIdentifier = SupportDocumentIdentifier
            .SupportDocumentPin = SupportDocumentPin
            .SupportDocumentEnvironment = SupportDocumentEnvironment
            .SupportDocumentTestSetId = SupportDocumentTestSetId
            .ValidateClientData = ValidateClientData
            .IdOperatingUnit = BarraBotones.OperatingUnit.Id
        End With
    End Sub
#End Region

#Region "ToolBar Events"

    ''' <summary>
    ''' Evento nuevo de la barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.Deshacer()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de actualizar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Me.Guardar()
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Model = Nothing
        _settingAccount = Nothing
        _IdOperationUnity = Nothing
        _record = Nothing
        Presenter = Nothing
    End Sub


    ''' <summary>
    ''' Handles the FormClosing event of the FrmParametersAccounting control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmParametersAccounting_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmParametersAccounting control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmParametersAccounting_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.LayoutControls.SetIsCustomizable(Me.lySettingAccount, True)
        Await Me.LayoutControls.LoadDefinitionAsync()
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        Presenter = New PParametersAccounting(Me)
        LoadStatus()
        InitializeTuple()
        Deshacer()
        LoadControls()
    End Sub

    ''' <summary>
    ''' Evento que abre una ventana emergente al dar click sobre el control "Nit"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indGlNitDian_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIdDian.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New Presentation.Common.FrmThirdParty With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            INDsleIdDian.Properties.DataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre una ventana emergente al dar click sobre el control "Tesorería Distrital"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indGLDistrictTreasury_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIdDistrictTreasury.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New Presentation.Common.FrmThirdParty With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            INDsleIdDistrictTreasury.Properties.DataSource = Nothing
        End If
    End Sub

#Region "Querypopup"
    ''' <summary>
    ''' Handles the QueryPopUp event of the indGlAccountDeficit control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub indGlAccountDeficit_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleIdDeficitAccount.QueryPopUp
        If IdDeficitAccountXpo Is Nothing Then
            Presenter.InitializeIdDeficitAccount()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the indGlAccountingSuperavit control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub indGlAccountingSuperavit_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleIdSuperavitAccount.QueryPopUp
        If IdSuperavitAccountXpo Is Nothing Then
            Presenter.InitializeIdSuperavitAccount()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the indGLAccountingEarnings control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub indGLAccountingEarnings_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleIdUtilityAccount.QueryPopUp
        If IdUtilityAccountXpo Is Nothing Then
            Presenter.InitializeIdUtilityAccount()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the indGlProofClosure control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub indGlProofClosure_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleIdCloseDocument.QueryPopUp
        If IdCloseDocumentXpo Is Nothing Then
            Presenter.InitializeIdCloseDocument()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the indGlNitDian control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub indGlNitDian_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleIdDian.QueryPopUp
        If IdDianXpo Is Nothing Then
            Presenter.InitializeIdDian()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the indGLDistrictTreasury control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub indGLDistrictTreasury_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleIdDistrictTreasury.QueryPopUp
        If IdDistrictTreasuryXpo Is Nothing Then
            Presenter.InitializeIdDistrictTreasury()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the indGlComprobanteHomologacion control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub indGlComprobanteHomologacion_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleIdApprovalDocument.QueryPopUp
        If IdApprovalDocumentXpo Is Nothing Then
            Presenter.InitializeIdApprovalDocument()
        End If
    End Sub

    Private Sub indGLVoucherTransfers_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleIdMovementDocument.QueryPopUp
        If IdMovementDocumentXpo Is Nothing Then
            Presenter.InitializeIdMovementDocument()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the indGlRetentionIva control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub indGlRetentionIva_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleIdIvaRetentionConcept.QueryPopUp
        If IdIvaRetentionConceptXpo Is Nothing Then
            Presenter.InitializeIdIvaRetentionConcept()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the ingGlRetentionICA control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub ingGlRetentionICA_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleIdIcaRetentionConcept.QueryPopUp
        If IdIcaRetentionConceptXpo Is Nothing Then
            Presenter.InitializeIdIcaRetentionConcept()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the indGlReteFuente control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub indGlReteFuente_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleIdSourceRetentionConcept.QueryPopUp
        If IdSourceRetentionConceptXpo Is Nothing Then
            Presenter.InitializeIdSourceRetentionConcept()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            BarraBotones.OperatingUnitValue = operatingUnit.Id
            LoadControls()
        End If
    End Sub

#End Region

#Region "Buttonclick"

    ''' <summary>
    ''' Evento que abre una ventana emergente al dar click sobre alguno de los controles (Cuenta Superavit, Cuenta Deficit, Cuenta de ganancias)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indGlAccountDeficit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIdDeficitAccount.ButtonClick, INDsleIdSuperavitAccount.ButtonClick,
                                                                                                                                      INDsleIdUtilityAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using

        End If
    End Sub

    ''' <summary>
    ''' Evento que abre una ventana emergente al dar click sobre el control alguno de los controles (Comprobante de Cierre, Comprobante de Homologación, Comprobante de Traslados)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indGlProofClosure_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIdCloseDocument.ButtonClick, INDsleIdApprovalDocument.ButtonClick, INDsleIdMovementDocument.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using

        End If
    End Sub

    ''' <summary>
    ''' Evento que abre una ventana emergente al dar click sobre el control alguno de los controles (Retención IVA, Retención ICA, Rete Fuente)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indGlRetentionIva_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIdIvaRetentionConcept.ButtonClick, INDsleIdIcaRetentionConcept.ButtonClick, INDsleIdSourceRetentionConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmRetentionConcept With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using

        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al dar click sobre el control "Certificado Digital"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsbDigitalCertificate_Click(sender As Object, e As EventArgs) Handles INDsbDigitalCertificate.Click
        LoadDigitalCertificate()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDslePrintSignature control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDslePrintSignature_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePrintSignature.EditValueChanged
        If PrintSignature = True Then
            INDLciSignature.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciSignature.AllowHide = False
            INDLciPosition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciPosition.AllowHide = False
            INDLciTaxReviewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciTaxReviewer.AllowHide = False
            INDLciProfessionalCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciProfessionalCard.AllowHide = False
        Else
            INDLciSignature.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciSignature.AllowHide = True
            INDLciPosition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciPosition.AllowHide = True
            INDLciTaxReviewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciTaxReviewer.AllowHide = True
            INDLciProfessionalCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciProfessionalCard.AllowHide = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando el valor del control INDsleHandlesElectronicBilling cambia, mostrando u ocultando controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleHandlesElectronicBilling_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHandlesElectronicBilling.EditValueChanged
        INDlygElectronicBillingInformation.HideControl(Not HandlesElectronicBilling)
        INDLciTestSetId.HideControl(Not HandlesElectronicBilling)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando el valor del control INDSleHandlesElectronicPayroll cambia, mostrando u ocultando controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleHandlesElectronicPayroll_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleHandlesElectronicPayroll.EditValueChanged
        INDlygElectronicPayrollInformation.HideControl(Not HandlesElectronicPayroll)
        INDLciElectronicPayrollTestSetId.HideControl(Not HandlesElectronicPayroll)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando el valor del control INDGleEnvironment cambia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleEnvironment_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleEnvironment.EditValueChanged
        If (HandlesElectronicBilling) Then
            INDLciTestSetId.HideControl(Environment)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando el valor del control INDGleElectronicPayrollEnvironment cambia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleElectronicPayrollEnvironment_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleElectronicPayrollEnvironment.EditValueChanged
        If (HandlesElectronicPayroll) Then
            INDLciElectronicPayrollTestSetId.HideControl(ElectronicPayrollEnvironment)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando el valor del control INDsleHandlesSupportDocument cambia, mostrando u ocultando controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleHandlesSupportDocument_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHandlesSupportDocument.EditValueChanged
        INDlygSupportDocument.HideControl(Not HandlesSupportDocument)
        INDLciSupportDocumentSetId.HideControl(Not HandlesSupportDocument)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando el valor del control INDGleSupportDocumentEnvironment cambia, mostrando u ocultando controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleSupportDocumentEnvironment_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleSupportDocumentEnvironment.EditValueChanged
        If (HandlesSupportDocument) Then
            INDLciSupportDocumentSetId.HideControl(SupportDocumentEnvironment)
        End If
    End Sub

#End Region

#End Region

End Class