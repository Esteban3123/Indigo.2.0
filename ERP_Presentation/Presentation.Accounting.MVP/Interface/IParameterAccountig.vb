'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 18-03-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo

#End Region

Public Interface IParameterAccountig
    Inherits IcrudBase

#Region "Properties"
    ''' <summary>
    ''' contiene la cuenta de deficit
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdDeficitAccount As Integer?

    ''' <summary>
    ''' contiene la cuenta de superavit
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdSuperavitAccount As Integer?

    ''' <summary>
    ''' contiene la cuenta de ganancias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdUtilityAccount As Integer?

    ''' <summary>
    ''' contiene la cuenta de cierre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdCloseDocument As Integer?

    ''' <summary>
    ''' contiene el nit de la Dian
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdDian As Integer?

    ''' <summary>
    ''' contiene la tesoreria distrital
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdDistrictTreasury As Integer?

    ''' <summary>
    ''' contiene el comprobante de homologacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdApprovalDocument As Integer?

    ''' <summary>
    ''' contiene el id del comprobante de translados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdMovementDocument As Integer?

    ''' <summary>
    ''' contiene el id de la retencion del iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdIvaRetentionConcept As Integer?

    ''' <summary>
    '''  contiene el id de la retencion del ica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdIcaRetentionConcept As Integer?

    ''' <summary>
    '''  contiene el id de la retefuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdSourceRetentionConcept As Integer?
    
    ''' <summary>
    ''' establece si aplica o no firmas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PrintSignature As Boolean?

    ''' <summary>
    ''' contiene la firma
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Signature As String

    ''' <summary>
    '''  contiene el cargo del que firma
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Potition As String

    ''' <summary>
    ''' contiene le nombre del revisro fiscal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TaxReviewer As String

    ''' <summary>
    ''' contiene el numero de la tarjeta profesional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProfessionalCard As String

    ''' <summary>
    ''' Validar datos del cliente
    ''' </summary>
    ''' <returns></returns>
    Property ValidateClientData As Boolean


    Property IdDeficitAccountXpo As XPInstantFeedbackSource
    Property IdSuperavitAccountXpo As XPInstantFeedbackSource
    Property IdUtilityAccountXpo As XPInstantFeedbackSource
    Property IdCloseDocumentXpo As XPInstantFeedbackSource
    Property IdDianXpo As XPInstantFeedbackSource
    Property IdDistrictTreasuryXpo As XPInstantFeedbackSource
    Property IdApprovalDocumentXpo As XPInstantFeedbackSource
    Property IdMovementDocumentXpo As XPInstantFeedbackSource
    Property IdIvaRetentionConceptXpo As XPInstantFeedbackSource
    Property IdIcaRetentionConceptXpo As XPInstantFeedbackSource
    Property IdSourceRetentionConceptXpo As XPInstantFeedbackSource

#Region "ElectronicBilling"

    ''' <summary>
    ''' establece si maneja o no Facturación Electronica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HandlesElectronicBilling As Boolean

    ''' <summary>
    '''  contiene el identificador del software
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SoftwareIdentifier As String

    ''' <summary>
    '''  contiene el pin del software
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SoftwarePin As String

    ''' <summary>
    '''  Entorno de ejecución de la factura electrónica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Environment As Boolean

    ''' <summary>
    '''  contiene la clave del set de pruebas 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TestSetId As String

    ''' <summary>
    '''  contiene el certificado digital
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DigitalCertificate As Byte()

    ''' <summary>
    '''  contiene la contraseña del certificado digital
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DigitalCertificateKey As String

#End Region

#Region "ElectronicPayroll"

    ''' <summary>
    ''' establece si maneja o no Nómina Electronica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HandlesElectronicPayroll As Boolean

    ''' <summary>
    '''  contiene el identificador del software
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ElectronicPayrollIdentifier As String

    ''' <summary>
    '''  contiene el pin del software
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ElectronicPayrollPin As String

    ''' <summary>
    '''  Entorno de ejecución de la nómina electrónica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ElectronicPayrollEnvironment As Boolean

    ''' <summary>
    '''  contiene la clave del set de pruebas 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ElectronicPayrollTestSetId As String

#End Region

#Region "SupportDocument"

    ''' <summary>
    ''' establece si maneja o no documento soporte
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HandlesSupportDocument As Boolean

    ''' <summary>
    '''  contiene el identificador del software
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupportDocumentIdentifier As String

    ''' <summary>
    '''  contiene el pin del software
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupportDocumentPin As String

    ''' <summary>
    '''  Entorno de ejecución del documento soporte
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupportDocumentEnvironment As Boolean

    ''' <summary>
    '''  contiene la clave del set de pruebas 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupportDocumentTestSetId As String

#End Region

#End Region

End Interface
