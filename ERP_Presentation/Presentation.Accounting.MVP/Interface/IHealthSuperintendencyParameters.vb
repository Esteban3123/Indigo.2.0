#Region "Imports"

Imports DevExpress.Xpo
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IHealthSuperParameters
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Propiedad que retorna el layout para customizaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.GeneralLedgerSequence

    ''' <summary>
    ''' Codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Formato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Format As Byte
#Region "FT003"

    Property DebtorFieldId As Byte

    Property DebtorsConcept As Byte

    Property Typedebt As Byte

#End Region

#Region "Ft004"

    ''' <summary>
    ''' Concepto de acreencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CreditConcept As Byte?

    ''' <summary>
    ''' Id del acreedor por nit o numero de cuenta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property creditorIdBy As Byte?

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MainAccountId As Integer?

    ''' <summary>
    ''' Medicion Posterior
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SubsequentMeasurement As Byte?

    ''' <summary>
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ExcludeAccountingVoucher As HealthSuperParametersFt004Detail

#End Region

#Region "Detalle FT006"
    ''' <summary>
    ''' Establece u Obtiene el establecimiento
    ''' </summary>
    ''' <returns></returns>
    Property Storehouse As String

    ''' <summary>
    ''' establece u optiene el Id de un tercero
    ''' </summary>
    ''' <returns></returns>
    Property ThirdPartyId As Integer

    ''' <summary>
    ''' establece u optiene el codigo  SIMEV
    ''' </summary>
    ''' <returns></returns>
    Property SIMEVCode As String

    ''' <summary>
    ''' Entidad de riesgo
    ''' </summary>
    ''' <returns></returns>
    Property RiskRating As String

    ''' <summary>
    ''' entidad calificadora
    ''' </summary>
    ''' <returns></returns>
    Property RatingEntity As Byte

    ''' <summary>
    ''' Otra entidad calificadora
    ''' </summary>
    ''' <returns></returns>
    Property AnotherQualifier As String

    ''' <summary>
    ''' Clase de cuenta
    ''' </summary>
    ''' <returns></returns>
    Property ClassAccount As Byte

    ''' <summary>
    ''' Tipo de Moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyType As String

    ''' <summary>
    ''' Si tienen Gravamen
    ''' </summary>
    ''' <returns></returns>
    Property Assessment As Boolean

    ''' <summary>
    ''' Stado
    ''' </summary>
    ''' <returns></returns>
    Property Status As Byte

    ''' <summary>
    ''' Fecha de medicion
    ''' </summary>
    ''' <returns></returns>
    Property MeasureDate As Date?

    ''' <summary>
    ''' valor de medicion
    ''' </summary>
    ''' <returns></returns>
    Property MeasuredValue As Decimal

    Property InvestmentReserves As Decimal
#End Region

#Region "FT007"

    Property Instrument As Byte

    Property InvestmentType As Byte

    Property Mnemonic As String

    Property CodeTitle As String

    Property BroadcastDate As Date?

    Property ExpirationDate As Date?

    Property PurchaseDate As Date?

    Property PurchaseValue As Decimal

    Property PurchaseRate As Decimal

    Property NominalValue As Decimal

    Property FacialRate As String

    Property Modality As Byte

    Property Periodicity As String

    Property MarketRate As String

    Property MarketValue As Decimal

    Property Duration As Decimal

    Property Linked As Boolean

    Property Dematerialized As Byte

    Property InvestmentTechnicalReserves As Boolean
#End Region

#Region "FT008"

    Property Country As String

    Property Participation As Decimal

    Property Yields As Decimal

    Property AnotherInvestmentType As String

#End Region

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Xpo de cuentas contables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MainAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Xpo de cuentas contables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property JournalVoucherXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Xpo de los terceros
    ''' </summary>
    ''' <returns></returns>
    Property ThirdPartyXpo As XPInstantFeedbackSource

#End Region

#Region "Methods"

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
