#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Common
Imports Presentation.Controls

#End Region

Public Class FrmHealthSuperParameters
    Implements IHealthSuperParameters

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Accounting"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa al presentador
    ''' </summary>
    Private _presenter As PHealthSuperParameters

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.GeneralLedgerSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    Private JournalSourceById As List(Of ViewJournalVouchersByMainAccountXpo)
    ''' <summary>
    ''' Variable para controlar el registro bloqueado en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim _blockRecord As Domain.Entities.BlockRecordGeneralLedger

    ''' <summary>
    ''' Variable que contiene la entidad de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _HealthSuperParameters As HealthSuperParameters

    ''' <summary>
    ''' detalle ft003
    ''' </summary>
    Dim _HealthSuperParametersFt003 As HealthSuperParametersFt003

    ''' <summary>
    ''' Lista de detalle parametrs ft003
    ''' </summary>
    Dim _listHealthSuperParametersFt003 As List(Of HealthSuperParametersFt003)

    ''' <summary>
    ''' Lista de eliminados 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteHealthSuperParametersFt003 As List(Of HealthSuperParametersFt003)

    ''' <summary>
    ''' representa la entidad de detalle de lineas de distirbucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _HealthSuperParametersFt004 As HealthSuperParametersFt004

    ''' <summary>
    ''' Listado del detalle de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listHealthSuperParametersFt004 As List(Of HealthSuperParametersFt004)

    ''' <summary>
    ''' Lista de eliminados 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteHealthSuperParametersFt004 As List(Of HealthSuperParametersFt004)

    ''' <summary>
    ''' Detalle del FT004
    ''' </summary>
    Dim _healthSuperParametersFt004Detail As HealthSuperParametersFt004Detail

    ''' <summary>
    ''' Lista de Detalle FT004
    ''' </summary>
    Dim _ListhealthSuperParametersFt004Detail As New List(Of HealthSuperParametersFt004Detail)

    ''' <summary>
    ''' detalle ft006
    ''' </summary>
    Dim _HealthSuperParametersFt006 As HealthSuperParametersFt006

    ''' <summary>
    ''' Lista de detalle parametrs ft006
    ''' </summary>
    Dim _listHealthSuperParametersFt006 As List(Of HealthSuperParametersFt006)

    ''' <summary>
    ''' Lista de eliminados 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteHealthSuperParametersFt006 As List(Of HealthSuperParametersFt006)

    ''' <summary>
    ''' detalle ft007
    ''' </summary>
    Dim _HealthSuperParametersFt007 As HealthSuperParametersFt007

    ''' <summary>
    ''' Lista de detalle parametrs ft007
    ''' </summary>
    Dim _listHealthSuperParametersFt007 As List(Of HealthSuperParametersFt007)

    ''' <summary>
    ''' Lista de eliminados 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteHealthSuperParametersFt007 As List(Of HealthSuperParametersFt007)

    ''' <summary>
    ''' detalle ft008
    ''' </summary>
    Dim _HealthSuperParametersFt008 As HealthSuperParametersFt008

    ''' <summary>
    ''' Lista de detalle parametrs ft008
    ''' </summary>
    Dim _listHealthSuperParametersFt008 As List(Of HealthSuperParametersFt008)

    ''' <summary>
    ''' Lista de eliminados 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteHealthSuperParametersFt008 As List(Of HealthSuperParametersFt008)

    ''' <summary>
    ''' detalle ft009
    ''' </summary>
    Dim _HealthSuperParametersFt009 As HealthSuperParametersFt009

    ''' <summary>
    ''' Lista de detalle parametrs ft009
    ''' </summary>
    Dim _listHealthSuperParametersFt009 As List(Of HealthSuperParametersFt009)

    ''' <summary>
    ''' Lista de eliminados 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteHealthSuperParametersFt009 As List(Of HealthSuperParametersFt009)

    Private MainAccountsById As List(Of PUCServiceXpo)


#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IHealthSuperParameters.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que retorna el layout para customizaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IHealthSuperParameters.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As GeneralLedgerSequence Implements IHealthSuperParameters.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As GeneralLedgerSequence)
            Me._sequense = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.GeneralLedgerSequenceDetail In Me._sequense.GeneralLedgerSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IHealthSuperParameters.Code
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
    ''' Formato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Format As Byte Implements IHealthSuperParameters.Format
        Get
            Return INDsleFormat.EditValue
        End Get
        Set(value As Byte)
            INDsleFormat.EditValue = value
        End Set
    End Property

#Region "FT003"
    ''' <summary>
    ''' Concepto de Deudores
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DebtorsConcept As Byte Implements IHealthSuperParameters.DebtorsConcept
        Get
            Return INDsleDebtorsConcept.EditValue
        End Get
        Set(value As Byte)
            INDsleDebtorsConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Campo Id Deudores
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DebtorFieldId As Byte Implements IHealthSuperParameters.DebtorFieldId
        Get
            Return INDsleDebtorFieldId.EditValue
        End Get
        Set(value As Byte)
            INDsleDebtorFieldId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de deuda
    ''' </summary>
    ''' <returns></returns>
    Public Property Typedebt As Byte Implements IHealthSuperParameters.Typedebt
        Get
            Return INDsleTypedebt.EditValue
        End Get
        Set(value As Byte)
            INDsleTypedebt.EditValue = value
        End Set
    End Property


#End Region

#Region "Details Ft004"

    ''' <summary>
    ''' Concepto del detalle
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CreditConcept As Byte? Implements IHealthSuperParameters.CreditConcept
        Get
            Return INDsleCreditConcept.EditValue
        End Get
        Set(value As Byte?)
            INDsleCreditConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Columna del concepto del formato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SubsequentMeasurement As Byte? Implements IHealthSuperParameters.SubsequentMeasurement
        Get
            Return INDsleSubsequentMeasurement.EditValue
        End Get
        Set(value As Byte?)
            INDsleSubsequentMeasurement.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainAccountId As Integer? Implements IHealthSuperParameters.MainAccountId
        Get
            Return INDsleMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleMainAccount.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Id del acreedor según el método de busqueda
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CreditorIdBy As Byte? Implements IHealthSuperParameters.creditorIdBy
        Get
            Return INDsleCreditorIdBy.EditValue
        End Get
        Set(value As Byte?)
            INDsleCreditorIdBy.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a los detalles de exclusión de comprobante contable en los parámetros de HealthSuper
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExcludeAccountingVoucher As HealthSuperParametersFt004Detail Implements IHealthSuperParameters.ExcludeAccountingVoucher
        Get
            Return _healthSuperParametersFt004Detail
        End Get
        Set(value As HealthSuperParametersFt004Detail)
            _healthSuperParametersFt004Detail = value
        End Set
    End Property

#End Region

#Region "Detail FT006"

    ''' <summary>
    ''' Esta propiedad proporciona acceso al nombre del almacén en los parámetros de HealthSuper
    ''' </summary>
    ''' <returns></returns>
    Public Property StoreHouse As String Implements IHealthSuperParameters.Storehouse
        Get
            Return INDTbStoreHouse.EditValue
        End Get
        Set(value As String)
            INDTbStoreHouse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso al Id de la tercera parte en los parámetros de HealthSuper
    ''' </summary>
    ''' <returns></returns>
    Public Property ThirdPartyId As Integer Implements IHealthSuperParameters.ThirdPartyId
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso al código SIMEV
    ''' </summary>
    ''' <returns></returns>
    Public Property SIMEVCode As String Implements IHealthSuperParameters.SIMEVCode
        Get
            Return INDTbSIMEVCode.EditValue
        End Get
        Set(value As String)
            INDTbSIMEVCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la calificación de riesgo
    ''' </summary>
    ''' <returns></returns>
    Public Property RiskRating As String Implements IHealthSuperParameters.RiskRating
        Get
            Return INDTbRiskRating.EditValue
        End Get
        Set(value As String)
            INDTbRiskRating.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la entidad calificadora
    ''' </summary>
    ''' <returns></returns>
    Public Property RatingEntity As Byte Implements IHealthSuperParameters.RatingEntity
        Get
            Return INDsleRatingEntity.EditValue
        End Get
        Set(value As Byte)
            INDsleRatingEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a otra calificadora
    ''' </summary>
    ''' <returns></returns>
    Public Property AnotherQualifier As String Implements IHealthSuperParameters.AnotherQualifier
        Get
            Return INDTbAnotherQualifier.EditValue
        End Get
        Set(value As String)
            INDTbAnotherQualifier.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la clase de cuenta
    ''' </summary>
    ''' <returns></returns>
    Public Property ClassAccount As Byte Implements IHealthSuperParameters.ClassAccount
        Get
            Return INDsleClassAccount.EditValue
        End Get
        Set(value As Byte)
            INDsleClassAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso al tipo de divisa/moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyType As String Implements IHealthSuperParameters.CurrencyType
        Get
            Return INDsleCurrencyType.EditValue
        End Get
        Set(value As String)
            INDsleCurrencyType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a si tiene o no gravamen
    ''' </summary>
    ''' <returns></returns>
    Public Property Assessment As Boolean Implements IHealthSuperParameters.Assessment
        Get
            Return INDsleAssessment.EditValue
        End Get
        Set(value As Boolean)
            INDsleAssessment.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso al estado
    ''' </summary>
    ''' <returns></returns>
    Public Property Status As Byte Implements IHealthSuperParameters.Status
        Get
            Return INDsleStatus.EditValue
        End Get
        Set(value As Byte)
            INDsleStatus.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la fecha de medición
    ''' </summary>
    ''' <returns></returns>
    Public Property MeasureDate As Date? Implements IHealthSuperParameters.MeasureDate
        Get
            Return INDDMeasureDate.EditValue
        End Get
        Set(value As Date?)
            INDDMeasureDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso al valor de medición
    ''' </summary>
    ''' <returns></returns>
    Public Property MeasuredValue As Decimal Implements IHealthSuperParameters.MeasuredValue
        Get
            Return INDTbMeasuredValue.EditValue
        End Get
        Set(value As Decimal)
            INDTbMeasuredValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la inversión de reservas
    ''' </summary>
    ''' <returns></returns>
    Public Property InvestmentReserves As Decimal Implements IHealthSuperParameters.InvestmentReserves
        Get
            Return INDTbInvestmentReserves.EditValue
        End Get
        Set(value As Decimal)
            INDTbInvestmentReserves.EditValue = value
        End Set
    End Property
#End Region

#Region "Detail FT007"

    ''' <summary>
    ''' Esta propiedad proporciona acceso al instrumento
    ''' </summary>
    ''' <returns></returns>
    Public Property Instrument As Byte Implements IHealthSuperParameters.Instrument
        Get
            Return INDsleInstrument.EditValue
        End Get
        Set(value As Byte)
            INDsleInstrument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso al tipo de inversión
    ''' </summary>
    ''' <returns></returns>
    Public Property InvestmentType As Byte Implements IHealthSuperParameters.InvestmentType
        Get
            Return INDsleInvestmentType.EditValue
        End Get
        Set(value As Byte)
            INDsleInvestmentType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso al parámetro Nemotécnico
    ''' </summary>
    ''' <returns></returns>
    Public Property Mnemonic As String Implements IHealthSuperParameters.Mnemonic
        Get
            Return INDTbMnemonic.EditValue
        End Get
        Set(value As String)
            INDTbMnemonic.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso al título de código ISIN o CFI
    ''' </summary>
    ''' <returns></returns>
    Public Property CodeTitle As String Implements IHealthSuperParameters.CodeTitle
        Get
            Return INDTbCodeTitle.EditValue
        End Get
        Set(value As String)
            INDTbCodeTitle.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la fecha de emisión
    ''' </summary>
    ''' <returns></returns>
    Public Property BroadcastDate As Date? Implements IHealthSuperParameters.BroadcastDate
        Get
            Return INDDBroadcastDate.EditValue
        End Get
        Set(value As Date?)
            INDDBroadcastDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la fecha de vencimiento
    ''' </summary>
    ''' <returns></returns>
    Public Property ExpirationDate As Date? Implements IHealthSuperParameters.ExpirationDate
        Get
            Return INDDExpirationDate.EditValue
        End Get
        Set(value As Date?)
            INDDExpirationDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la fecha de compra
    ''' </summary>
    ''' <returns></returns>
    Public Property PurchaseDate As Date? Implements IHealthSuperParameters.PurchaseDate
        Get
            Return INDDPurchaseDate.EditValue
        End Get
        Set(value As Date?)
            INDDPurchaseDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso al valor de compra
    ''' </summary>
    ''' <returns></returns>
    Public Property PurchaseValue As Decimal Implements IHealthSuperParameters.PurchaseValue
        Get
            Return INDTbPurchaseValue.EditValue
        End Get
        Set(value As Decimal)
            INDTbPurchaseValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la tasa de compra 
    ''' </summary>
    ''' <returns></returns>
    Public Property PurchaseRate As Decimal Implements IHealthSuperParameters.PurchaseRate
        Get
            Return INDTbPurchaseRate.EditValue
        End Get
        Set(value As Decimal)
            INDTbPurchaseRate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso al valor nominal
    ''' </summary>
    ''' <returns></returns>
    Public Property NominalValue As Decimal Implements IHealthSuperParameters.NominalValue
        Get
            Return INDTbNominalValue.EditValue
        End Get
        Set(value As Decimal)
            INDTbNominalValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la tasa facial
    ''' </summary>
    ''' <returns></returns>
    Public Property FacialRate As String Implements IHealthSuperParameters.FacialRate
        Get
            Return INDTbFacialRate.EditValue
        End Get
        Set(value As String)
            INDTbFacialRate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la modalidad
    ''' </summary>
    ''' <returns></returns>
    Public Property Modality As Byte Implements IHealthSuperParameters.Modality
        Get
            Return INDsleModality.EditValue
        End Get
        Set(value As Byte)
            INDsleModality.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la periodicidad
    ''' </summary>
    ''' <returns></returns>
    Public Property Periodicity As String Implements IHealthSuperParameters.Periodicity
        Get
            Return INDslePeriodicity.EditValue
        End Get
        Set(value As String)
            INDslePeriodicity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la tasa de mercado
    ''' </summary>
    ''' <returns></returns>
    Public Property MarketRate As String Implements IHealthSuperParameters.MarketRate
        Get
            Return INDTbMarketRate.EditValue
        End Get
        Set(value As String)
            INDTbMarketRate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso al valor de mercado
    ''' </summary>
    ''' <returns></returns>
    Public Property MarketValue As Decimal Implements IHealthSuperParameters.MarketValue
        Get
            Return INDTbMarketValue.EditValue
        End Get
        Set(value As Decimal)
            INDTbMarketValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la duración
    ''' </summary>
    ''' <returns></returns>
    Public Property Duration As Decimal Implements IHealthSuperParameters.Duration
        Get
            Return INDTbDuration.EditValue
        End Get
        Set(value As Decimal)
            INDTbDuration.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la vinculación
    ''' </summary>
    ''' <returns></returns>
    Public Property Linked As Boolean Implements IHealthSuperParameters.Linked
        Get
            Return INDsleLinked.EditValue
        End Get
        Set(value As Boolean)
            INDsleLinked.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la desmaterialización
    ''' </summary>
    ''' <returns></returns>
    Public Property Dematerialized As Byte Implements IHealthSuperParameters.Dematerialized
        Get
            Return INDsleDematerialized.EditValue
        End Get
        Set(value As Byte)
            INDsleDematerialized.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la inversión de las reservas técnicas
    ''' </summary>
    ''' <returns></returns>
    Public Property InvestmentTechnicalReserves As Boolean Implements IHealthSuperParameters.InvestmentTechnicalReserves
        Get
            Return INDsleInvestmentTechnicalReserves.EditValue
        End Get
        Set(value As Boolean)
            INDsleInvestmentTechnicalReserves.EditValue = value
        End Set
    End Property
#End Region

#Region "Detail FT008"

    ''' <summary>
    ''' Esta propiedad proporciona acceso al pais
    ''' </summary>
    ''' <returns></returns>
    Public Property Country As String Implements IHealthSuperParameters.Country
        Get
            Return INDTbCountry.EditValue
        End Get
        Set(value As String)
            INDTbCountry.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a la participación
    ''' </summary>
    ''' <returns></returns>
    Public Property Participation As Decimal Implements IHealthSuperParameters.Participation
        Get
            Return INDTbParticipation.EditValue
        End Get
        Set(value As Decimal)
            INDTbParticipation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a los rendimientos
    ''' </summary>
    ''' <returns></returns>
    Public Property Yields As Decimal Implements IHealthSuperParameters.Yields
        Get
            Return INDTbYields.EditValue
        End Get
        Set(value As Decimal)
            INDTbYields.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a otro tipo de inversión
    ''' </summary>
    ''' <returns></returns>
    Public Property AnotherInvestmentType As String Implements IHealthSuperParameters.AnotherInvestmentType
        Get
            Return INDTbAnotherInvestmentType.EditValue
        End Get
        Set(value As String)
            INDTbAnotherInvestmentType.EditValue = value
        End Set
    End Property
#End Region

#End Region

#Region "Datasources"
    ''' <summary>
    ''' Listado de cuentas contables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IHealthSuperParameters.MainAccountXpo
        Get
            Return CType(INDsleMainAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleMainAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a los datos de cuentas contables y lo establece en el control INDsleExcludeAccountingVoucher
    ''' </summary>
    ''' <returns></returns>
    Public Property JournalVoucherXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IHealthSuperParameters.JournalVoucherXpo
        Get
            Return CType(INDsleExcludeAccountingVoucher.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleExcludeAccountingVoucher.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad proporciona acceso a los datos de los terceros y lo establece en el control Nit de tercero(INDsleThirdParty)
    ''' </summary>
    ''' <returns></returns>
    Public Property ThirdPartyXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IHealthSuperParameters.ThirdPartyXpo
        Get
            Return CType(INDsleThirdParty.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece los formatos
    ''' </summary>
    ''' <remarks></remarks>
    Private _ListFormat As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListFormat As List(Of Tuple(Of Byte, String))
        Get
            If _ListFormat Is Nothing Then
                _ListFormat = New List(Of Tuple(Of Byte, String))
                _ListFormat.Add(New Tuple(Of Byte, String)(1, "FT001 - Catalogo Información Financiera"))
                _ListFormat.Add(New Tuple(Of Byte, String)(3, "FT003 - Cuentas Por Cobrar – Deudores"))
                _ListFormat.Add(New Tuple(Of Byte, String)(4, "FT004 - Cuentas por Pagar – Acreedores"))
                _ListFormat.Add(New Tuple(Of Byte, String)(6, "FT006 - Bancos y Carteras Colectivas"))
                _ListFormat.Add(New Tuple(Of Byte, String)(7, "FT007 - Control de Inversiones Inscritas en el Mercado de Valores de Colombia"))
                _ListFormat.Add(New Tuple(Of Byte, String)(8, "FT008 - Inversiones – Otros Títulos"))
                _ListFormat.Add(New Tuple(Of Byte, String)(9, "FT009 - Activos y Pasivos en Moneda Extranjera"))
                _ListFormat.Add(New Tuple(Of Byte, String)(10, "FT010 - Activos No Monetarios"))
                _ListFormat.Add(New Tuple(Of Byte, String)(25, "FT025 - Facturación Radicada"))
            End If
            Return _ListFormat
        End Get
    End Property

    ''' <summary>
    ''' Establece los tipos de concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private _listCreditConcept As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListCreditConcept As List(Of Tuple(Of Byte, String))
        Get
            If _listCreditConcept Is Nothing Then
                _listCreditConcept = New List(Of Tuple(Of Byte, String))
                _listCreditConcept.Add(New Tuple(Of Byte, String)(1, "Prestación de servicios de salud"))
                _listCreditConcept.Add(New Tuple(Of Byte, String)(2, "Insumos y medicamentos"))
                _listCreditConcept.Add(New Tuple(Of Byte, String)(3, "Dispositivo médico o equipo biomédico"))
                _listCreditConcept.Add(New Tuple(Of Byte, String)(4, "Administrativo"))
                _listCreditConcept.Add(New Tuple(Of Byte, String)(5, "Restitución de recursos"))
                _listCreditConcept.Add(New Tuple(Of Byte, String)(6, "Otro"))
            End If
            Return _listCreditConcept
        End Get
    End Property

    ''' <summary>
    ''' Establece los tipos de concepto de deudores
    ''' </summary>
    ''' <remarks></remarks>
    Private _debtorsConcept As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListDebtorsConcept As List(Of Tuple(Of Byte, String))
        Get
            If _debtorsConcept Is Nothing Then
                _debtorsConcept = New List(Of Tuple(Of Byte, String))
                _debtorsConcept.Add(New Tuple(Of Byte, String)(1, "Plan obligatorio de Salud"))
                _debtorsConcept.Add(New Tuple(Of Byte, String)(2, "Planes adicionales de Salud"))
                _debtorsConcept.Add(New Tuple(Of Byte, String)(3, "Recobros No POS"))
                _debtorsConcept.Add(New Tuple(Of Byte, String)(4, "Reembolsos por incapacidades diferentes a enfermedad general"))
                _debtorsConcept.Add(New Tuple(Of Byte, String)(5, "SOAT y ARL"))
                _debtorsConcept.Add(New Tuple(Of Byte, String)(6, "Reclamaciones (ECAT)"))
                _debtorsConcept.Add(New Tuple(Of Byte, String)(7, "Otros"))
            End If
            Return _debtorsConcept
        End Get
    End Property

    ''' <summary>
    ''' Establece las naturalezas a manejar
    ''' </summary>
    ''' <remarks></remarks>
    Private _subsequentMeasurement As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListSubsequentMeasurement As List(Of Tuple(Of Byte, String))
        Get
            If _subsequentMeasurement Is Nothing Then
                _subsequentMeasurement = New List(Of Tuple(Of Byte, String))
                _subsequentMeasurement.Add(New Tuple(Of Byte, String)(1, "Precio de la Transacción / Valor Nominal / Costo"))
                _subsequentMeasurement.Add(New Tuple(Of Byte, String)(2, "Costo Amortizado"))
                _subsequentMeasurement.Add(New Tuple(Of Byte, String)(3, " Valor Razonable"))
                _subsequentMeasurement.Add(New Tuple(Of Byte, String)(4, "Valor Razonable con cambios en el ORI"))
                _subsequentMeasurement.Add(New Tuple(Of Byte, String)(5, "Valor Presente Pagos Futuros"))
                _subsequentMeasurement.Add(New Tuple(Of Byte, String)(6, "No aplica"))
            End If
            Return _subsequentMeasurement
        End Get
    End Property

    ''' <summary>
    ''' Establece lasde donde se obtendrá el tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private _listcreditorIdBy As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListcreditorIdBy As List(Of Tuple(Of Byte, String))
        Get
            If _listcreditorIdBy Is Nothing Then
                _listcreditorIdBy = New List(Of Tuple(Of Byte, String))
                _listcreditorIdBy.Add(New Tuple(Of Byte, String)(1, "Nit Tercero"))
                _listcreditorIdBy.Add(New Tuple(Of Byte, String)(2, "Código de la cuenta"))
            End If
            Return _listcreditorIdBy
        End Get
    End Property

    ''' <summary>
    ''' Establece los tipos de Entidad Calificadora
    ''' </summary>
    ''' <remarks></remarks>
    Private _ratingEntity As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListRatingEntity As List(Of Tuple(Of Byte, String))
        Get
            If _ratingEntity Is Nothing Then
                _ratingEntity = New List(Of Tuple(Of Byte, String))
                _ratingEntity.Add(New Tuple(Of Byte, String)(0, "No Aplica"))
                _ratingEntity.Add(New Tuple(Of Byte, String)(1, "BRC Investor Services S.A. (Stand & Poors)"))
                _ratingEntity.Add(New Tuple(Of Byte, String)(2, "Fitch Ratings Colombia S.A. (Antes Duff & Phelps De Colombia S.A.) "))
                _ratingEntity.Add(New Tuple(Of Byte, String)(3, "Value And Risk Rating S.A"))
                _ratingEntity.Add(New Tuple(Of Byte, String)(4, "Otra Sociedad Calificadora"))
            End If
            Return _ratingEntity
        End Get
    End Property

    ''' <summary>
    ''' Establece la Clase de la Cuenta
    ''' </summary>
    ''' <remarks></remarks>
    Private _classAccount As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListClassAccount As List(Of Tuple(Of Byte, String))
        Get
            If _classAccount Is Nothing Then
                _classAccount = New List(Of Tuple(Of Byte, String))
                _classAccount.Add(New Tuple(Of Byte, String)(1, "Cuenta Corriente"))
                _classAccount.Add(New Tuple(Of Byte, String)(2, "Cuenta de Ahorros"))
                _classAccount.Add(New Tuple(Of Byte, String)(3, "Cuenta Maestra de Recaudo "))
                _classAccount.Add(New Tuple(Of Byte, String)(4, "Cartera Colectiva Abierta o Fondos de Inversión en Mercado Monetario"))
                _classAccount.Add(New Tuple(Of Byte, String)(5, "Cartera Colectiva Cerrada"))
                _classAccount.Add(New Tuple(Of Byte, String)(6, "Otro tipo de Encargo Fiduciario o Fondo de Inversión, Fideicomiso, Fondos de Inversión Colectiva Inmobiliarios y/o Fondos de Capital Privado"))
            End If
            Return _classAccount
        End Get
    End Property

    ''' <summary>
    ''' Establece el tipo de moneda
    ''' </summary>
    ''' <remarks></remarks>
    Private _currencyType As List(Of Tuple(Of String, String))
    Private ReadOnly Property ListCurrencyType As List(Of Tuple(Of String, String))
        Get
            If _currencyType Is Nothing Then
                _currencyType = New List(Of Tuple(Of String, String))
                _currencyType.Add(New Tuple(Of String, String)("COP", "Pesos Colombianos"))
                _currencyType.Add(New Tuple(Of String, String)("USD", "Dólar de los Estados Unidos de América"))
                _currencyType.Add(New Tuple(Of String, String)("GBP", "Libra Británica"))
                _currencyType.Add(New Tuple(Of String, String)("EUR", "Euro"))
                _currencyType.Add(New Tuple(Of String, String)("CAD", "Dólar Canadiense"))
                _currencyType.Add(New Tuple(Of String, String)("CHF", "Franco Suizo"))
                _currencyType.Add(New Tuple(Of String, String)("JPY", "Yen Japonés"))
                _currencyType.Add(New Tuple(Of String, String)("OTR", "Otra"))
            End If
            Return _currencyType
        End Get
    End Property

    ''' <summary>
    ''' Establece el tipo de moneda
    ''' </summary>
    ''' <remarks></remarks>
    Private _assessment As List(Of Tuple(Of Boolean, String))
    Private ReadOnly Property ListAssessment As List(Of Tuple(Of Boolean, String))
        Get
            If _assessment Is Nothing Then
                _assessment = New List(Of Tuple(Of Boolean, String))
                _assessment.Add(New Tuple(Of Boolean, String)(False, "No tiene gravamen"))
                _assessment.Add(New Tuple(Of Boolean, String)(True, "Si tiene gravamen"))
            End If
            Return _assessment
        End Get
    End Property

    ''' <summary>
    ''' Tipo de Deuda
    ''' </summary>
    Private _typedebt As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListTypedebt As List(Of Tuple(Of Byte, String))
        Get
            If _typedebt Is Nothing Then
                _typedebt = New List(Of Tuple(Of Byte, String))
                _typedebt.Add(New Tuple(Of Byte, String)(1, "Activo no financiero – Anticipo"))
                _typedebt.Add(New Tuple(Of Byte, String)(2, "Instrumento financiero"))
            End If
            Return _typedebt
        End Get
    End Property

    ''' <summary>
    ''' Estado 
    ''' </summary>
    ''' <remarks></remarks>
    Private _status As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListStatus As List(Of Tuple(Of Byte, String))
        Get
            If _status Is Nothing Then
                _status = New List(Of Tuple(Of Byte, String))
                _status.Add(New Tuple(Of Byte, String)(0, "No Aplica"))
                _status.Add(New Tuple(Of Byte, String)(1, "Libre de afectación"))
                _status.Add(New Tuple(Of Byte, String)(2, "Embargos"))
                _status.Add(New Tuple(Of Byte, String)(3, "Medida Preventiva"))
            End If
            Return _status
        End Get
    End Property

    ''' <summary>
    ''' Instrumento 
    ''' </summary>
    ''' <remarks></remarks>
    Private _instrument As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListInstrument As List(Of Tuple(Of Byte, String))
        Get
            If _instrument Is Nothing Then
                _instrument = New List(Of Tuple(Of Byte, String))
                _instrument.Add(New Tuple(Of Byte, String)(1, "Títulos de deuda pública emitidos o garantizados por la Nación o por el Banco de la República"))
                _instrument.Add(New Tuple(Of Byte, String)(2, " Títulos de renta fija emitidos, aceptados, garantizados o avalados por entidades vigiladas por la
                                                                Superintendencia Financiera de Colombia, FOGAFIN y FOGACOOP."))
                _instrument.Add(New Tuple(Of Byte, String)(3, "Renta Variable"))
            End If
            Return _instrument
        End Get
    End Property

    ''' <summary>
    ''' Estado 
    ''' </summary>
    ''' <remarks></remarks>
    Private _investmentType As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListInvestmentType As List(Of Tuple(Of Byte, String))
        Get
            If _investmentType Is Nothing Then
                _investmentType = New List(Of Tuple(Of Byte, String))
                _investmentType.Add(New Tuple(Of Byte, String)(1, "Títulos de Deuda Pública (TES)"))
                _investmentType.Add(New Tuple(Of Byte, String)(2, "Certificados de Depósito a Término (CDT)"))
                _investmentType.Add(New Tuple(Of Byte, String)(3, "Bonos Ordinarios"))
                _investmentType.Add(New Tuple(Of Byte, String)(4, "Bonos Subordinados"))
                _investmentType.Add(New Tuple(Of Byte, String)(5, "Bonos Opcionalmente Convertibles en acciones"))
                _investmentType.Add(New Tuple(Of Byte, String)(6, "Bonos Obligatoriamente Convertibles en acciones"))
                _investmentType.Add(New Tuple(Of Byte, String)(7, "Bonos de Capitalización"))
                _investmentType.Add(New Tuple(Of Byte, String)(8, "Acciones Ordinarias"))
                _investmentType.Add(New Tuple(Of Byte, String)(9, "Acciones preferenciales"))
                _investmentType.Add(New Tuple(Of Byte, String)(10, "Otro"))
            End If
            Return _investmentType
        End Get
    End Property

    ''' <summary>
    ''' Modalidad 
    ''' </summary>
    ''' <remarks></remarks>
    Private _modality As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListModality As List(Of Tuple(Of Byte, String))
        Get
            If _modality Is Nothing Then
                _modality = New List(Of Tuple(Of Byte, String))
                _modality.Add(New Tuple(Of Byte, String)(0, "No Aplica"))
                _modality.Add(New Tuple(Of Byte, String)(1, "Vencida"))
                _modality.Add(New Tuple(Of Byte, String)(2, "Anticipada"))
            End If
            Return _modality
        End Get
    End Property

    ''' <summary>
    ''' Periocidad 
    ''' </summary>
    ''' <remarks></remarks>
    Private _periodicity As List(Of Tuple(Of String, String))
    Private ReadOnly Property ListPeriodicity As List(Of Tuple(Of String, String))
        Get
            If _periodicity Is Nothing Then
                _periodicity = New List(Of Tuple(Of String, String))
                _periodicity.Add(New Tuple(Of String, String)("M", "Mensual"))
                _periodicity.Add(New Tuple(Of String, String)("B", "Bimestral"))
                _periodicity.Add(New Tuple(Of String, String)("T", "Trimestral"))
                _periodicity.Add(New Tuple(Of String, String)("S", "Semestral"))
                _periodicity.Add(New Tuple(Of String, String)("A", "Anual"))
                _periodicity.Add(New Tuple(Of String, String)("O", "Otro o No Aplica"))
            End If
            Return _periodicity
        End Get
    End Property

    ''' <summary>
    ''' Vinculado 
    ''' </summary>
    ''' <remarks></remarks>
    Private _linked As List(Of Tuple(Of Boolean, String))
    Private ReadOnly Property ListLinked As List(Of Tuple(Of Boolean, String))
        Get
            If _linked Is Nothing Then
                _linked = New List(Of Tuple(Of Boolean, String))
                _linked.Add(New Tuple(Of Boolean, String)(False, "No Aplica"))
                _linked.Add(New Tuple(Of Boolean, String)(True, "Inversión en vinculado"))
            End If
            Return _linked
        End Get
    End Property

    ''' <summary>
    ''' Desmaterializado 
    ''' </summary>
    ''' <remarks></remarks>
    Private _dematerialized As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListDematerialized As List(Of Tuple(Of Byte, String))
        Get
            If _dematerialized Is Nothing Then
                _dematerialized = New List(Of Tuple(Of Byte, String))
                _dematerialized.Add(New Tuple(Of Byte, String)(1, "DECEVAL S.A. - Depósito Centralizado de Valores."))
                _dematerialized.Add(New Tuple(Of Byte, String)(2, "DCV - Depósito Central de Valores."))
            End If
            Return _dematerialized
        End Get
    End Property

    ''' <summary>
    ''' Inversión de las Reservas Técnicas 
    ''' </summary>
    ''' <remarks></remarks>
    Private _investmentTechnicalReserves As List(Of Tuple(Of Boolean, String))
    Private ReadOnly Property ListInvestmentTechnicalReserves As List(Of Tuple(Of Boolean, String))
        Get
            If _investmentTechnicalReserves Is Nothing Then
                _investmentTechnicalReserves = New List(Of Tuple(Of Boolean, String))
                _investmentTechnicalReserves.Add(New Tuple(Of Boolean, String)(False, "No aplica para SAP, EMP e IPS o no es inversión que respalde las Reservas Técnicas"))
                _investmentTechnicalReserves.Add(New Tuple(Of Boolean, String)(True, " Si Respalda las Reservas Técnicas."))
            End If
            Return _investmentTechnicalReserves
        End Get
    End Property

#End Region

#Region "ICrud Base"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Async Sub Deshacer() Implements Base.ICrudBase.Deshacer
        Await CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If Not ValidateControls() OrElse Not ValidateDetail() Then
            Exit Sub
        End If
        Try
            AssigningValues()
            Using Model As New MHealthSuperParameters(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of HealthSuperParameters) = Await Model.SaveHealthSuperParameters(Me._HealthSuperParameters, Me._idCurrentSequense)
                AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message

                    If _HealthSuperParameters.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._idCurrentSequense).RemoveAt(0)
                        End If
                    End If

                    Me._HealthSuperParameters = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequense IsNot Nothing AndAlso Me._sequense.IsManual Then
            Deshacer()
        Else
            NewEntity()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Formato", .FieldName = "FormatName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListHealthSuperParameters
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        Code = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Activa los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IHealthSuperParameters.ActionsOnControls
        Set(value As Boolean)
            INDlyHealthSuperParameters.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDsleFormat.Enabled = value
            INDpceAddDetail.Enabled = value
            INDgcDetail.Enabled = value
            INDGcDetailFt00.Enabled = value
            INDlyHealthSuperParameters.EndUpdate()
            If value Then
                INDsleFormat.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

#Region "Details"

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()

        '****FT004********************
        _HealthSuperParametersFt004 = Nothing
        '_ListhealthSuperParametersFt004Detail = Nothing
        CreditConcept = Nothing
        SubsequentMeasurement = Nothing
        MainAccountId = Nothing
        ExcludeAccountingVoucher = Nothing
        CreditorIdBy = Nothing
        JournalVoucherXpo = Nothing
        _selectorExcludeJounalV = New SelectorCache("JournalVouchersId", "Consecutive")
        INDsleExcludeAccountingVoucher.Properties.NullText = String.Empty
        '*****FT003*************************
        _HealthSuperParametersFt003 = Nothing
        DebtorFieldId = Nothing
        DebtorsConcept = Nothing
        Typedebt = Nothing
        '****FT006********************
        '_listHealthSuperParametersFt006 = Nothing
        _HealthSuperParametersFt006 = Nothing
        MainAccountId = Nothing
        StoreHouse = Nothing
        ThirdPartyId = Nothing
        SIMEVCode = Nothing
        RiskRating = Nothing
        RatingEntity = Nothing
        AnotherQualifier = Nothing
        ClassAccount = Nothing
        CurrencyType = Nothing
        Assessment = Nothing
        Status = Nothing
        MeasureDate = Nothing
        MeasuredValue = Nothing
        ThirdPartyXpo = Nothing
        InvestmentReserves = Nothing
        '***********FT007*******************
        _HealthSuperParametersFt007 = Nothing
        Instrument = Nothing
        InvestmentType = Nothing
        Mnemonic = Nothing
        CodeTitle = Nothing
        BroadcastDate = Nothing
        ExpirationDate = Nothing
        PurchaseDate = Nothing
        PurchaseValue = Nothing
        PurchaseRate = Nothing
        NominalValue = Nothing
        FacialRate = Nothing
        Modality = Nothing
        Periodicity = Nothing
        MarketRate = Nothing
        MarketValue = Nothing
        Duration = Nothing
        Linked = Nothing
        Dematerialized = Nothing
        InvestmentTechnicalReserves = Nothing
        '**************FT008****************************
        _HealthSuperParametersFt008 = Nothing
        Country = Nothing
        Participation = Nothing
        Yields = Nothing
        AnotherInvestmentType = Nothing
        INDLycAnotherInvestmentType.Enabled = False
        '*********FT009****************************************
        _HealthSuperParametersFt009 = Nothing
        _selectorMainAccount = New SelectorCache("Id", "NumberName")
        INDsleMainAccountFt009.Properties.NullText = String.Empty

    End Sub

    ''' <summary>
    ''' Valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As Boolean
        Dim listErrors As New StringBuilder

        If Format = 4 Then
            If INDlyItemCreditConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If CreditConcept Is Nothing Then
                    listErrors.AppendLine("Debe seleccionar un concepto Acreencia")
                End If
            End If

            If INDlyItemSubsequentMeasurement.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If SubsequentMeasurement Is Nothing Then
                    listErrors.AppendLine("Debe seleccionar una Medicion Posterior")
                End If
            End If

            If MainAccountId Is Nothing Then
                listErrors.AppendLine("Debe seleccionar una cuenta contable")
            End If

            If CreditorIdBy Is Nothing Then
                listErrors.AppendLine("Debe seleccionar el tipo del Id de Acreedor")
            End If

            If listErrors.Length = 0 AndAlso _listHealthSuperParametersFt004 IsNot Nothing Then
                If _listHealthSuperParametersFt004.Any(Function(d) d.CreditConcept.Equals(CreditConcept) AndAlso d.SubsequentMeasurement.Equals(SubsequentMeasurement) AndAlso d.MainAccountId.Equals(MainAccountId)) Then
                    listErrors.AppendLine("El detalle ya se encuentra agregado")
                End If
            End If

        ElseIf Format = 6 Then
            If MainAccountId Is Nothing Then
                listErrors.AppendLine("Debe seleccionar una cuenta contable")
            End If

            If listErrors.Length = 0 AndAlso _listHealthSuperParametersFt006 IsNot Nothing Then
                If _listHealthSuperParametersFt006.Any(Function(d) d.MainAccountId.Equals(MainAccountId)) Then
                    listErrors.AppendLine("El detalle ya se encuentra agregado")
                End If
            End If

        ElseIf Format = 7 Then

            If DateValue(BroadcastDate) >= DateValue(PurchaseDate) AndAlso DateValue(PurchaseDate) <= DateValue(ExpirationDate) Then
                listErrors.AppendLine("La Fecha de Compra debe esta dentro de la Fecha de Emision y Fecha de Expiración")
            End If

            If MainAccountId Is Nothing Then
                listErrors.AppendLine("Debe seleccionar una cuenta contable")
            End If

            If listErrors.Length = 0 AndAlso _listHealthSuperParametersFt007 IsNot Nothing Then
                If _listHealthSuperParametersFt007.Any(Function(d) d.MainAccountId.Equals(MainAccountId)) Then
                    listErrors.AppendLine("El detalle ya se encuentra agregado")
                End If
            End If
        End If

        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Agrega el detalle a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddNewDetail()
        If Format = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un formato."
            Exit Sub
        End If

        If ValidateControlsPopup() = True Then
            If Format = 4 Then
                AssigningValuesDetail()
                If _listHealthSuperParametersFt004 Is Nothing Then
                    _listHealthSuperParametersFt004 = New List(Of HealthSuperParametersFt004)
                End If
                _listHealthSuperParametersFt004.Add(_HealthSuperParametersFt004)
                If _HealthSuperParameters.Id > 0 Then
                    _HealthSuperParameters.MarkAsModified()
                End If
                INDgcDetail.DataSource = Nothing
                INDgcDetail.DataSource = _listHealthSuperParametersFt004

                If INDlyItemCreditConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    INDsleCreditConcept.Focus()
                ElseIf INDlyItemSubsequentMeasurement.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    INDsleSubsequentMeasurement.Focus()
                Else
                    INDsleMainAccount.Focus()
                End If

            ElseIf Format = 3 Then
                AssigningValuesDetailFT003()
                If _listHealthSuperParametersFt003 Is Nothing Then
                    _listHealthSuperParametersFt003 = New List(Of HealthSuperParametersFt003)
                End If
                _listHealthSuperParametersFt003.Add(_HealthSuperParametersFt003)

                If _HealthSuperParameters.Id > 0 Then
                    _HealthSuperParameters.MarkAsModified()
                End If

                INDgcDetail.DataSource = Nothing
                INDgcDetail.DataSource = _listHealthSuperParametersFt003

            ElseIf Format = 6 Then

                AssigningValuesDetailFT006()
                If _listHealthSuperParametersFt006 Is Nothing Then
                    _listHealthSuperParametersFt006 = New List(Of HealthSuperParametersFt006)
                End If
                _listHealthSuperParametersFt006.Add(_HealthSuperParametersFt006)

                If _HealthSuperParameters.Id > 0 Then
                    _HealthSuperParameters.MarkAsModified()
                End If

                INDgcDetail.DataSource = Nothing
                INDgcDetail.DataSource = _listHealthSuperParametersFt006

            ElseIf Format = 7 Then

                AssigningValuesDetailFT007()
                If _listHealthSuperParametersFt007 Is Nothing Then
                    _listHealthSuperParametersFt007 = New List(Of HealthSuperParametersFt007)
                End If
                _listHealthSuperParametersFt007.Add(_HealthSuperParametersFt007)

                If _HealthSuperParameters.Id > 0 Then
                    _HealthSuperParameters.MarkAsModified()
                End If

                INDGcDetailFt00.DataSource = Nothing
                INDGcDetailFt00.DataSource = _listHealthSuperParametersFt007

            ElseIf Format = 8 Then
                AssigningValuesDetailFT008()
                If _listHealthSuperParametersFt008 Is Nothing Then
                    _listHealthSuperParametersFt008 = New List(Of HealthSuperParametersFt008)
                End If
                _listHealthSuperParametersFt008.Add(_HealthSuperParametersFt008)

                If _HealthSuperParameters.Id > 0 Then
                    _HealthSuperParameters.MarkAsModified()
                End If

                INDGcDetailFt00.DataSource = Nothing
                INDGcDetailFt00.DataSource = _listHealthSuperParametersFt008

            ElseIf Format = 9 Then
                Dim List As New List(Of Integer)
                If _listHealthSuperParametersFt009 Is Nothing Then
                    _listHealthSuperParametersFt009 = New List(Of HealthSuperParametersFt009)
                End If
                Using Model As New MHealthSuperParameters(Me.Tag.ToString())
                    Me.MainAccountsById = Model.ListAccountsById(_selectorMainAccount.GetKeys())
                    For Each Item In MainAccountsById
                        _HealthSuperParametersFt009 = New HealthSuperParametersFt009
                        With _HealthSuperParametersFt009
                            .MainAccountId = Item.Id
                            .MainAccountNumberName = Item.NumberName
                        End With
                        _listHealthSuperParametersFt009.Add(_HealthSuperParametersFt009)
                    Next
                End Using

                If _HealthSuperParameters.Id > 0 Then
                    _HealthSuperParameters.MarkAsModified()
                End If

                INDgcDetail.DataSource = Nothing
                INDgcDetail.DataSource = _listHealthSuperParametersFt009
            End If

            INDsleFormat.Properties.ReadOnly = True
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado con éxito"
            CleanControlsPopup()

        End If
    End Sub

    ''' <summary>
    ''' Este método se encarga de asignar valores a los parámetros y detalles de _HealthSuperParametersFt004
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValuesDetail()
        Try
            Using Model As New MHealthSuperParameters(Me.Tag.ToString())
                _HealthSuperParametersFt004 = New HealthSuperParametersFt004
                _ListhealthSuperParametersFt004Detail = New List(Of HealthSuperParametersFt004Detail)

                Me.JournalSourceById = Model.JournalVouchersById(_selectorExcludeJounalV.GetKeys(), MainAccountId)

                With _HealthSuperParametersFt004
                    .CreditConcept = CreditConcept
                    .SubsequentMeasurement = SubsequentMeasurement
                    .MainAccountId = MainAccountId
                    .MainAccountNumberName = INDsleMainAccount.Text
                    .creditorIdBy = CreditorIdBy
                    For Each Item In JournalSourceById
                        ExcludeAccountingVoucher = New HealthSuperParametersFt004Detail
                        ExcludeAccountingVoucher.JournalVouchersId = Item.JournalVouchersId
                        ExcludeAccountingVoucher.Consecutive = Item.Consecutive
                        ExcludeAccountingVoucher.JournalVoucherTypeName = Item.JournalVoucherTypeName
                        ExcludeAccountingVoucher.StatusName = Item.StatusName
                        ExcludeAccountingVoucher.Detail = Item.Detail
                        _ListhealthSuperParametersFt004Detail.Add(ExcludeAccountingVoucher)
                    Next
                    For Each detail In _ListhealthSuperParametersFt004Detail.ToList()
                        .HealthSuperParametersFt004Detail.Add(detail)
                    Next
                End With
            End Using
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad HealthSuperParametersFt003
    ''' </summary>
    Private Sub AssigningValuesDetailFT003()
        Try
            _HealthSuperParametersFt003 = New HealthSuperParametersFt003
            With _HealthSuperParametersFt003
                .MainAccountId = MainAccountId
                .MainAccountNumberName = INDsleMainAccount.Text
                .DebtorFieldId = DebtorFieldId
                .DebtorsConcept = DebtorsConcept
                .SubsequentMeasurement = SubsequentMeasurement
                .Typedebt = Typedebt
            End With
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad HealthSuperParametersFt006
    ''' </summary>
    Private Sub AssigningValuesDetailFT006()
        Try
            _HealthSuperParametersFt006 = New HealthSuperParametersFt006
            With _HealthSuperParametersFt006
                .MainAccountId = MainAccountId
                .MainAccountNumberName = INDsleMainAccount.Text
                .Storehouse = StoreHouse
                .ThirdPartyId = ThirdPartyId
                .ThirdPartyNitName = INDsleThirdParty.Text
                .SIMEVCode = SIMEVCode
                .RiskRating = RiskRating
                .RatingEntity = RatingEntity
                .RatingEntityName = INDsleRatingEntity.Text
                .AnotherQualifier = AnotherQualifier
                .ClassAccount = ClassAccount
                .ClassAccountName = INDsleClassAccount.Text
                .CurrencyType = CurrencyType
                .Assessment = Assessment
                .Status = Status
                .StatusName = INDsleStatus.Text
                .MeasureDate = MeasureDate
                If MeasureDate = #1/01/0001# Then
                    .MeasureDateName = "00000000"
                Else
                    .MeasureDateName = MeasureDate
                End If
                .MeasuredValue = MeasuredValue
            End With
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad HealthSuperParametersFt007
    ''' </summary>
    Private Sub AssigningValuesDetailFT007()
        Try
            _HealthSuperParametersFt007 = New HealthSuperParametersFt007
            With _HealthSuperParametersFt007
                .MainAccountId = MainAccountId
                .MainAccountNumberName = INDsleMainAccount.Text
                .ThirdPartyId = ThirdPartyId
                .ThirdPartyNitName = INDsleThirdParty.Text
                .SIMEVCode = SIMEVCode
                .RiskRating = RiskRating
                .RatingEntity = RatingEntity
                .RatingEntityName = INDsleRatingEntity.Text
                .AnotherQualifier = AnotherQualifier
                .CurrencyType = CurrencyType
                .Assessment = Assessment
                .Status = Status
                .StatusName = INDsleStatus.Text
                .Instrument = Instrument
                .InstrumentName = INDsleInstrument.Text
                .InvestmentType = InvestmentType
                .InvestmentTypeName = INDsleInvestmentType.Text
                .Mnemonic = Mnemonic
                .CodeTitle = CodeTitle
                If BroadcastDate Is Nothing Then
                    .BroadcastDateName = "00000000"
                    .BroadcastDate = BroadcastDate
                Else
                    .BroadcastDateName = BroadcastDate
                    .BroadcastDate = BroadcastDate
                End If

                If ExpirationDate Is Nothing Then
                    .ExpirationDateName = "00000000"
                    .ExpirationDate = ExpirationDate
                Else
                    .ExpirationDateName = ExpirationDate
                    .ExpirationDate = ExpirationDate
                End If
                .PurchaseDate = PurchaseDate
                .PurchaseValue = PurchaseValue
                .PurchaseRate = PurchaseRate
                .NominalValue = NominalValue
                .FacialRate = FacialRate
                .Modality = Modality
                .ModalityName = INDsleModality.Text
                .Periodicity = Periodicity
                .PeriodicityName = INDslePeriodicity.Text
                .MarketRate = MarketRate
                .MarketValue = MarketValue
                .Duration = Duration
                .Linked = Linked
                .Dematerialized = Dematerialized
                .DematerializedName = INDsleDematerialized.Text
                .InvestmentTechnicalReserves = InvestmentTechnicalReserves
                .InvestmentTechnicalReservesName = INDsleInvestmentTechnicalReserves.Text

                If MeasureDate = #1/01/0001# Then
                    .MeasureDateName = "00000000"
                    .MeasureDate = Nothing
                Else
                    .MeasureDateName = MeasureDate
                    .MeasureDate = MeasureDate
                End If
                .MeasuredValue = MeasuredValue
            End With
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad HealthSuperParametersFt008
    ''' </summary>
    Private Sub AssigningValuesDetailFT008()
        Try
            _HealthSuperParametersFt008 = New HealthSuperParametersFt008
            With _HealthSuperParametersFt008
                .MainAccountId = MainAccountId
                .MainAccountNumberName = INDsleMainAccount.Text
                .ThirdPartyId = ThirdPartyId
                .ThirdPartyNitName = INDsleThirdParty.Text
                .Country = Country
                .CurrencyType = CurrencyType
                .Assessment = Assessment
                .Status = Status
                .StatusName = INDsleStatus.Text
                .InvestmentType = InvestmentType
                .InvestmentTypeName = INDsleInvestmentType.Text
                .AnotherInvestmentType = AnotherInvestmentType
                If BroadcastDate Is Nothing Then
                    .BroadcastDateName = "00000000"
                    .BroadcastDate = BroadcastDate
                Else
                    .BroadcastDateName = BroadcastDate
                    .BroadcastDate = BroadcastDate
                End If

                If ExpirationDate Is Nothing Then
                    .ExpirationDateName = "00000000"
                    .ExpirationDate = ExpirationDate
                Else
                    .ExpirationDateName = ExpirationDate
                    .ExpirationDate = ExpirationDate
                End If
                .PurchaseDate = PurchaseDate
                .PurchaseValue = PurchaseValue
                .PurchaseRate = PurchaseRate
                .NominalValue = NominalValue
                .FacialRate = FacialRate
                .Modality = Modality
                .ModalityName = INDsleModality.Text
                .Periodicity = Periodicity
                .PeriodicityName = INDslePeriodicity.Text
                .MarketRate = MarketRate
                .MarketValue = MarketValue
                .Duration = Duration
                .Participation = Participation
                .Yields = Yields
                .Linked = Linked

                If MeasureDate = #1/01/0001# Then
                    .MeasureDateName = "00000000"
                    .MeasureDate = Nothing
                Else
                    .MeasureDateName = MeasureDate
                    .MeasureDate = MeasureDate
                End If
                .MeasuredValue = MeasuredValue
            End With
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Elimina el detalle de la rejilla para el formato ft004
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        Dim dld As HealthSuperParametersFt004 = CType(INDgvDetails.GetFocusedRow, HealthSuperParametersFt004)
        If dld.Id <> 0 Then
            If _listDeleteHealthSuperParametersFt004 Is Nothing Then
                _listDeleteHealthSuperParametersFt004 = New List(Of HealthSuperParametersFt004)
            End If

            While dld.HealthSuperParametersFt004Detail.Count > 0
                dld.HealthSuperParametersFt004Detail(dld.HealthSuperParametersFt004Detail.Count - 1).MarkAsDeleted()
            End While
            dld.MarkAsDeleted()
            _listDeleteHealthSuperParametersFt004.Add(dld)
        End If
        _listHealthSuperParametersFt004.Remove(dld)
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = _listHealthSuperParametersFt004

        If Not _listHealthSuperParametersFt004.Any() Then
            INDsleFormat.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Elimina el detalle de la rejilla para el formato ft003
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetailFT003()
        Dim dld As HealthSuperParametersFt003 = CType(INDgvDetails.GetFocusedRow, HealthSuperParametersFt003)
        If dld.Id <> 0 Then
            If _listDeleteHealthSuperParametersFt003 Is Nothing Then
                _listDeleteHealthSuperParametersFt003 = New List(Of HealthSuperParametersFt003)
            End If
            dld.MarkAsDeleted()
            _listDeleteHealthSuperParametersFt003.Add(dld)
        End If
        _listHealthSuperParametersFt003.Remove(dld)
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = _listHealthSuperParametersFt003

        If Not _listHealthSuperParametersFt003.Any() Then
            INDsleFormat.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' elimina el detalle para el formato FT006
    ''' </summary>
    Private Sub DeleteDetailFT006()
        Dim dld As HealthSuperParametersFt006 = CType(INDgvDetails.GetFocusedRow, HealthSuperParametersFt006)
        If dld.Id <> 0 Then
            If _listDeleteHealthSuperParametersFt006 Is Nothing Then
                _listDeleteHealthSuperParametersFt006 = New List(Of HealthSuperParametersFt006)
            End If
            dld.MarkAsDeleted()
            _listDeleteHealthSuperParametersFt006.Add(dld)
        End If
        _listHealthSuperParametersFt006.Remove(dld)
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = _listHealthSuperParametersFt006

        If Not _listHealthSuperParametersFt006.Any() Then
            INDsleFormat.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' elimina el detalle para el formato FT007
    ''' </summary>
    Private Sub DeleteDetailFT007()
        Dim dld As HealthSuperParametersFt007 = CType(INDGvDetailFt00.GetFocusedRow, HealthSuperParametersFt007)
        If dld.Id <> 0 Then
            If _listDeleteHealthSuperParametersFt007 Is Nothing Then
                _listDeleteHealthSuperParametersFt007 = New List(Of HealthSuperParametersFt007)
            End If
            dld.MarkAsDeleted()
            _listDeleteHealthSuperParametersFt007.Add(dld)
        End If
        _listHealthSuperParametersFt007.Remove(dld)
        INDGcDetailFt00.DataSource = Nothing
        INDGcDetailFt00.DataSource = _listHealthSuperParametersFt007

        If Not _listHealthSuperParametersFt007.Any() Then
            INDsleFormat.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' elimina el detalle para el formato FT008
    ''' </summary>
    Private Sub DeleteDetailFT008()
        Dim dld As HealthSuperParametersFt008 = CType(INDGvDetailFt00.GetFocusedRow, HealthSuperParametersFt008)
        If dld.Id <> 0 Then
            If _listDeleteHealthSuperParametersFt008 Is Nothing Then
                _listDeleteHealthSuperParametersFt008 = New List(Of HealthSuperParametersFt008)
            End If
            dld.MarkAsDeleted()
            _listDeleteHealthSuperParametersFt008.Add(dld)
        End If
        _listHealthSuperParametersFt008.Remove(dld)
        INDGcDetailFt00.DataSource = Nothing
        INDGcDetailFt00.DataSource = _listHealthSuperParametersFt008

        If Not _listHealthSuperParametersFt008.Any() Then
            INDsleFormat.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Elimina el detalle de la rejilla para el formato ft009
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetailFT009()
        Dim dld As HealthSuperParametersFt009 = CType(INDgvDetails.GetFocusedRow, HealthSuperParametersFt009)
        If dld.Id <> 0 Then
            If _listDeleteHealthSuperParametersFt009 Is Nothing Then
                _listDeleteHealthSuperParametersFt009 = New List(Of HealthSuperParametersFt009)
            End If
            dld.MarkAsDeleted()
            _listDeleteHealthSuperParametersFt009.Add(dld)
        End If
        _listHealthSuperParametersFt009.Remove(dld)
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = _listHealthSuperParametersFt009

        If Not _listHealthSuperParametersFt009.Any() Then
            INDsleFormat.Properties.ReadOnly = False
        End If
    End Sub

#End Region

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function CleanControls() As Task
        INDlyHealthSuperParameters.BeginUpdate()
        Await DeleteBlockedRecord()

        INDbtnCode.Text = String.Empty
        INDsleFormat.EditValue = Nothing
        INDsleFormat.Properties.ReadOnly = False
        INDgcDetail.DataSource = Nothing
        INDGcDetailFt00.DataSource = Nothing

        CleanControlsPopup()

        _doc = Nothing
        _HealthSuperParameters = Nothing
        _listHealthSuperParametersFt003 = Nothing
        _listDeleteHealthSuperParametersFt003 = Nothing
        _listHealthSuperParametersFt004 = Nothing
        _listDeleteHealthSuperParametersFt004 = Nothing

        _listHealthSuperParametersFt006 = Nothing
        _listDeleteHealthSuperParametersFt006 = Nothing

        _listHealthSuperParametersFt007 = Nothing
        _listDeleteHealthSuperParametersFt007 = Nothing

        _listHealthSuperParametersFt008 = Nothing
        _listDeleteHealthSuperParametersFt008 = Nothing

        _listHealthSuperParametersFt009 = Nothing
        _listDeleteHealthSuperParametersFt009 = Nothing

        ActionsOnControls = False
        Me.BarraBotones.StatusRecord = -1
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        JournalVoucherXpo = Nothing
        MainAccountXpo = Nothing
        INDsleInstrument.Enabled = False
        INDlyHealthSuperParameters.EndUpdate()
    End Function

    ''' <summary>
    ''' Función para validar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateDetail() As Boolean
        Dim listErrors As New StringBuilder

        If Format = 4 Then
            If Not (_listHealthSuperParametersFt004 IsNot Nothing AndAlso _listHealthSuperParametersFt004.Any()) Then
                listErrors.AppendLine("Se debe agregar minimo un detalle.")
            End If
        ElseIf Format = 6 Then
            If Not (_listHealthSuperParametersFt006 IsNot Nothing AndAlso _listHealthSuperParametersFt006.Any()) Then
                listErrors.AppendLine("Se debe agregar minimo un detalle.")
            End If
        ElseIf Format = 7 Then
            If Not (_listHealthSuperParametersFt007 IsNot Nothing AndAlso _listHealthSuperParametersFt007.Any()) Then
                listErrors.AppendLine("Se debe agregar minimo un detalle.")
            End If
        ElseIf Format = 8 Then
            If Not (_listHealthSuperParametersFt008 IsNot Nothing AndAlso _listHealthSuperParametersFt008.Any()) Then
                listErrors.AppendLine("Se debe agregar minimo un detalle.")
            End If
        End If

        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _HealthSuperParameters
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Format = Format
            If Format = 4 Then
                If _listHealthSuperParametersFt004 IsNot Nothing Then
                    For Each itemDetail As HealthSuperParametersFt004 In _listHealthSuperParametersFt004
                        .HealthSuperParametersFt004.Add(itemDetail)
                    Next
                End If
                If _listDeleteHealthSuperParametersFt004 IsNot Nothing Then
                    For Each itemDetail As HealthSuperParametersFt004 In _listDeleteHealthSuperParametersFt004
                        .HealthSuperParametersFt004.Add(itemDetail)
                    Next
                    .MarkAsModified()
                End If
            ElseIf Format = 3 Then

                If _listHealthSuperParametersFt003 IsNot Nothing Then
                    For Each itemDetail As HealthSuperParametersFt003 In _listHealthSuperParametersFt003
                        .HealthSuperParametersFt003.Add(itemDetail)
                    Next
                End If
                If _listDeleteHealthSuperParametersFt003 IsNot Nothing Then
                    For Each itemDetail As HealthSuperParametersFt003 In _listDeleteHealthSuperParametersFt003
                        .HealthSuperParametersFt003.Add(itemDetail)
                    Next
                    .MarkAsModified()
                End If

            ElseIf Format = 6 Then
                If _listHealthSuperParametersFt006 IsNot Nothing Then
                    For Each itemDetail As HealthSuperParametersFt006 In _listHealthSuperParametersFt006
                        .HealthSuperParametersFt006.Add(itemDetail)
                    Next
                End If
                If _listDeleteHealthSuperParametersFt006 IsNot Nothing Then
                    For Each itemDetail As HealthSuperParametersFt006 In _listDeleteHealthSuperParametersFt006
                        .HealthSuperParametersFt006.Add(itemDetail)
                    Next
                    .MarkAsModified()
                End If

            ElseIf Format = 7 Then

                If _listHealthSuperParametersFt007 IsNot Nothing Then
                    For Each itemDetail As HealthSuperParametersFt007 In _listHealthSuperParametersFt007
                        .HealthSuperParametersFt007.Add(itemDetail)
                    Next
                End If
                If _listDeleteHealthSuperParametersFt007 IsNot Nothing Then
                    For Each itemDetail As HealthSuperParametersFt007 In _listDeleteHealthSuperParametersFt007
                        .HealthSuperParametersFt007.Add(itemDetail)
                    Next
                    .MarkAsModified()
                End If

            ElseIf Format = 8 Then
                If _listHealthSuperParametersFt008 IsNot Nothing Then
                    For Each itemDetail As HealthSuperParametersFt008 In _listHealthSuperParametersFt008
                        .HealthSuperParametersFt008.Add(itemDetail)
                    Next
                End If
                If _listDeleteHealthSuperParametersFt008 IsNot Nothing Then
                    For Each itemDetail As HealthSuperParametersFt008 In _listDeleteHealthSuperParametersFt008
                        .HealthSuperParametersFt008.Add(itemDetail)
                    Next
                    .MarkAsModified()
                End If

            ElseIf Format = 9 Then

                If _listHealthSuperParametersFt009 IsNot Nothing Then
                    For Each Item As HealthSuperParametersFt009 In _listHealthSuperParametersFt009
                        .HealthSuperParametersFt009.Add(Item)
                    Next
                End If

                If _listDeleteHealthSuperParametersFt009 IsNot Nothing Then
                    For Each itemDetail As HealthSuperParametersFt009 In _listDeleteHealthSuperParametersFt009
                        .HealthSuperParametersFt009.Add(itemDetail)
                    Next
                    .MarkAsModified()
                End If
            End If
        End With
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Sub NewEntity()
        Try
            _HealthSuperParameters = New HealthSuperParameters() With {.Status = True}
            If Me._sequense IsNot Nothing AndAlso Me._sequense.IsManual Then
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    Me._idCurrentSequense = Me._sequense.GeneralLedgerSequenceDetail(0).Id
                ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    If Me._sequense.GeneralLedgerSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                        Me._idCurrentSequense = Me._sequense.GeneralLedgerSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        Exit Sub
                    End If
                End If
                If Me._sequense.Sequential Then
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                Else
                    If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                        If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            AsyncLoader(True)
                            Using model As New MRetentionConcept(CStr(Me.Tag))
                                Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                            End Using
                            AsyncLoader(False)
                            If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                                Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                                Me.ActionsOnControls = True
                                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                            Else
                                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                            End If
                        End If
                    Else
                        Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MHealthSuperParameters(CStr(Me.Tag))
                    AsyncLoader(True)
                    INDlyHealthSuperParameters.BeginUpdate()
                    Dim resultOperation = Await Model.GetHealthSuperParametersByCode(INDbtnCode.Text.Trim)
                    If resultOperation.StateResult Then
                        _HealthSuperParameters = resultOperation.ObjectEmbbeded
                        If _HealthSuperParameters IsNot Nothing AndAlso _HealthSuperParameters.Id > 0 Then
                            Me.BarraBotones.StatusRecordVisible = True
                            _blockRecord = Await Model.GetBlockRecordAccounting(CStr(Me.Tag), CStr(_HealthSuperParameters.Id))
                            With _HealthSuperParameters
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                Format = .Format
                                INDsleFormat.Properties.ReadOnly = True
                                If .Format = 4 Then

                                    _listHealthSuperParametersFt004 = .HealthSuperParametersFt004.ToList()
                                    INDgcDetail.DataSource = _listHealthSuperParametersFt004

                                    For Each Item In .HealthSuperParametersFt004
                                        INDGcHealthSPFt004D.DataSource = Item.HealthSuperParametersFt004Detail
                                    Next

                                ElseIf .Format = 3 Then

                                    _listHealthSuperParametersFt003 = .HealthSuperParametersFt003.ToList()
                                    INDgcDetail.DataSource = _listHealthSuperParametersFt003

                                ElseIf .Format = 6 Then

                                    _listHealthSuperParametersFt006 = .HealthSuperParametersFt006.ToList()
                                    INDgcDetail.DataSource = _listHealthSuperParametersFt006

                                ElseIf .Format = 7 Then

                                    _listHealthSuperParametersFt007 = .HealthSuperParametersFt007.ToList()
                                    INDGcDetailFt00.DataSource = _listHealthSuperParametersFt007

                                ElseIf .Format = 8 Then
                                    _listHealthSuperParametersFt008 = .HealthSuperParametersFt008.ToList()
                                    INDGcDetailFt00.DataSource = _listHealthSuperParametersFt008

                                ElseIf .Format = 9 Then
                                    _listHealthSuperParametersFt009 = .HealthSuperParametersFt009.ToList()
                                    INDgcDetail.DataSource = _listHealthSuperParametersFt009
                                End If

                                Me.BarraBotones.StatusRecord = If(.Status, eActionsStatusRecords.Active, eActionsStatusRecords.Inactive)
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._HealthSuperParameters.Code)
                            If _blockRecord.Id = 0 Then
                                _blockRecord = (Await Model.SaveBlockRecordAccounting(
                                        New BlockRecordGeneralLedger With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                            .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _HealthSuperParameters.Id})
                                        ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                            End If

                            Me.BarraBotones.SetDocuments(_HealthSuperParameters.Id, Me.Tag.ToString(), Nothing, GetType(HealthSuperParameters).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        Else
                            AsyncLoader(False)
                            If Me._sequense.IsManual Then
                                Me.NewEntity()
                            Else
                                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                                Code = String.Empty
                                INDbtnCode.Focus()
                            End If
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = resultOperation.Message
                    End If
                    INDlyHealthSuperParameters.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me._HealthSuperParameters.Code) Then
            Try
                Using model As New MHealthSuperParameters(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not _HealthSuperParameters.Status
                    Dim result As ActionResult(Of HealthSuperParameters) = Await model.ChangeState(Me._HealthSuperParameters.Code, state)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me._HealthSuperParameters = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        INDbtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function


    ''' <summary>
    ''' Eliminar la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function Delete() As Task
        If Not String.IsNullOrEmpty(Me._HealthSuperParameters.Code) Then
            Try
                Using model As New MHealthSuperParameters(Me.Tag)
                    AsyncLoader(True)
                    Dim result As ActionResult(Of HealthSuperParameters) = Await model.Delete(Me._HealthSuperParameters)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me._HealthSuperParameters = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        INDbtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    '''' <summary>
    '''' Genera el documento a Indexar
    '''' </summary>
    '''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        '    Dim dateServer = Me.GetDateServer()
        '    If Me._doc Is Nothing Then
        '        Me._doc = New IndexedDocument2 With {
        '            .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent"), Me._HealthSuperParameters.Code, String.Format("{0} ({1})", Me._HealthSuperParameters.Format, Me._HealthSuperParameters.Version)),
        '            .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
        '            .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._HealthSuperParameters.Code & "#$", .IdForm = CStr(Me.Tag),
        '            .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle"), Me._HealthSuperParameters.Code),
        '            .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        '        Return Me._doc
        '    Else
        '        Me._doc.Update = dateServer
        '        Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
        '        Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent"), Me._HealthSuperParameters.Code, String.Format("{0} ({1})", Me._HealthSuperParameters.Format, Me._HealthSuperParameters.Version))
        '        Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle"), Me._HealthSuperParameters.Code)
        '        Return Me._doc
        '    End If
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MHealthSuperParameters(CStr(Me.Tag))
                Await model.DeleteBlockRecordAccounting(_blockRecord)
                _blockRecord = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._HealthSuperParameters IsNot Nothing AndAlso Me._HealthSuperParameters.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmHealthSuperParameters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyHealthSuperParameters, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        _presenter = New PHealthSuperParameters(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequense()
        '******************************
        _idOperativeUnit = BarraBotones.OperatingUnitValue

        IndigoGridView1.SetListAcction(INDgvDetails, New List(Of eAcciones)() From {{eAcciones.Remove}})
        IndigoGridControl1.RefreshGrid(INDgcDetail)

        IndigoGridView1.SetListAcction(INDGvDetailFt00, New List(Of eAcciones)() From {{eAcciones.Remove}})
        IndigoGridControl1.RefreshGrid(INDGcDetailFt00)
        INDlyItemPopupDetails.Enabled = False
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _idOperativeUnit = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        _blockRecord = Nothing
        _HealthSuperParameters = Nothing
        _HealthSuperParametersFt004 = Nothing
        _listHealthSuperParametersFt004 = Nothing
        _listDeleteHealthSuperParametersFt004 = Nothing
        _ListhealthSuperParametersFt004Detail = Nothing
        _HealthSuperParametersFt006 = Nothing
        _listHealthSuperParametersFt006 = Nothing
        _HealthSuperParametersFt007 = Nothing
        _listHealthSuperParametersFt007 = Nothing
        INDgvDetails.RefreshData()
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmHealthSuperParameters_Activated(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleFormat.Properties.DataSource = ListFormat
        '******************************
        INDbtnCode.Focus()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmHealthSuperParameters_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un concepto de pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequense Is Nothing OrElse _sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    NewEntity()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape al control del valor mínimo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtMinimumValue_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDpceAddDetail.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAddDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceAddDetail.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpceAddDetail.ShowPopup()
            If INDlyItemCreditConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDsleCreditConcept.Focus()
            ElseIf INDlyItemSubsequentMeasurement.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDsleSubsequentMeasurement.Focus()
            Else
                INDsleMainAccount.Focus()
            End If
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMainAccount.QueryPopUp
        If INDsleMainAccount.Properties.DataSource Is Nothing Then
            If Format = 3 OrElse Format = 6 Then
                _presenter.InitializeMainAccount(1)
            ElseIf Format = 7 OrElse Format = 8 OrElse Format = 4 Then
                _presenter.InitializeMainAccount()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control excluir cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleExcludeAccountingVoucher_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleExcludeAccountingVoucher.QueryPopUp
        If INDsleMainAccount.Properties.DataSource IsNot Nothing AndAlso MainAccountId IsNot Nothing Then
            _presenter.InitializeJournalVoucher(MainAccountId)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de Nit del tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThirdParty.QueryPopUp
        If INDsleThirdParty.Properties.DataSource Is Nothing Then
            _presenter.InitializeThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de cuenta contable para el formato ft009
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMainAccountFt009_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMainAccountFt009.QueryPopUp
        If INDsleMainAccountFt009.Properties.DataSource Is Nothing Then
            Using Model As New MHealthSuperParameters(CStr(Me.Tag))
                INDsleMainAccountFt009.Properties.DataSource = Model.ListAccounts()
            End Using
        End If
    End Sub


#End Region

#Region "Selector"

    ' Se utiliza para almacenar información sobre los comprobantes de diario seleccionados en forma de pares
    Private _selectorExcludeJounalV As SelectorCache = New SelectorCache("JournalVouchersId", "Consecutive")

    'Se utiliza para almacenar información sobre cuentas principales en forma de pares
    Private _selectorMainAccount As SelectorCache = New SelectorCache("Id", "NumberName")

    ''' <summary>
    ''' Este evento se activa cuando se necesita obtener datos personalizados para columnas sin enlace en los controles de grilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDgvJournalVoucher.CustomUnboundColumnData, INDgvMainAccountFT009.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDgvJournalVoucher" Then
                e.Value = _selectorExcludeJounalV.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDgvMainAccountFT009" Then
                e.Value = _selectorMainAccount.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando se hace clic en una celda de una fila en los controles de grilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDgvJournalVoucher.RowCellClick, INDgvMainAccountFT009.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)

            'Comprueba el nombre de la vista y asigna la caché de selección adecuada
            If view.Name = "INDgvJournalVoucher" Then
                selector = _selectorExcludeJounalV
            ElseIf view.Name = "INDgvMainAccountFT009" Then
                selector = _selectorMainAccount
            End If

            'Verifica si la fila es válida y asigna el valor correspondiente en la caché de selección, si no es válido se limpia 
            If e.RowHandle >= 0 Then
                Dim row = view.GetRow(e.RowHandle)
                selector.SetValue(row)
            Else
                selector.Clear()
            End If
            view.RefreshData()
        End If
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando se cierra el cuadro de diálogo de selección en los controles SearchLookUpEdit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDsleExcludeAccountingVoucher.Closed, INDsleMainAccountFt009.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDsleExcludeAccountingVoucher" Then
            searchLookupEdit.Properties.NullText = _selectorExcludeJounalV.ToString()
        ElseIf searchLookupEdit.Name = "INDsleMainAccountFt009" Then
            searchLookupEdit.Properties.NullText = _selectorMainAccount.ToString()
        End If
    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetail.Click
        AddNewDetail()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton del control de la cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            _presenter.InitializeMainAccount()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento para cargar la versión de acuerdo al formato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFormat_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFormat.EditValueChanged
        Me._listCreditConcept = Nothing
        Me._subsequentMeasurement = Nothing
        Me._listcreditorIdBy = Nothing
        Me._ratingEntity = Nothing
        Me._classAccount = Nothing
        Me._currencyType = Nothing
        Me._assessment = Nothing
        Me._status = Nothing
        Me._instrument = Nothing
        Me._investmentType = Nothing
        Me._modality = Nothing
        Me._periodicity = Nothing
        Me._linked = Nothing
        Me._dematerialized = Nothing
        Me._investmentTechnicalReserves = Nothing
        Me._debtorsConcept = Nothing
        Me._typedebt = Nothing
        CleanControlsPopup()
        INDlyItemPopupDetails.Enabled = True

        If Format = 4 Then
            '******************************
            INDsleCreditConcept.Properties.DataSource = Me.ListCreditConcept
            INDsleSubsequentMeasurement.Properties.DataSource = Me.ListSubsequentMeasurement
            INDsleCreditorIdBy.Properties.DataSource = ListcreditorIdBy
            '*****************************************
            INDrepSleCreditConcept.DataSource = Me.ListCreditConcept
            INDrepSlecreditorIdBy.DataSource = Me.ListcreditorIdBy
            INDrepSleSubsequentMeasurement.DataSource = Me.ListSubsequentMeasurement
            HideControls()
            HideColumns()
            ShowLyControl(4, False)
            VisibleIndex()
        ElseIf Format = 3 Then
            INDsleDebtorFieldId.Properties.DataSource = ListcreditorIdBy
            INDsleSubsequentMeasurement.Properties.DataSource = Me.ListSubsequentMeasurement
            INDsleDebtorsConcept.Properties.DataSource = ListDebtorsConcept
            INDsleTypedebt.Properties.DataSource = ListTypedebt
            '**************************************************
            INDRepDebtorFieldId.DataSource = ListcreditorIdBy
            INDRepDebtorsConcept.DataSource = ListDebtorsConcept
            INDrepSleSubsequentMeasurement.DataSource = ListSubsequentMeasurement
            INDRepTypedebt.DataSource = ListTypedebt
            '**************************************************
            HideControls()
            HideColumns()
            ShowLyControl(3, False)
            VisibleIndex()
            INDlyItemPopupDetails.Enabled = True

        ElseIf Format = 6 Then
            INDsleRatingEntity.Properties.DataSource = ListRatingEntity
            INDsleClassAccount.Properties.DataSource = ListClassAccount
            INDsleAssessment.Properties.DataSource = ListAssessment
            INDsleStatus.Properties.DataSource = ListStatus
            INDsleCurrencyType.Properties.DataSource = ListCurrencyType
            HideControls()
            HideColumns()
            ShowLyControl(6, False)
            VisibleIndex()
        ElseIf Format = 7 Then
            INDsleRatingEntity.Properties.DataSource = ListRatingEntity
            INDsleAssessment.Properties.DataSource = ListAssessment
            INDsleStatus.Properties.DataSource = ListStatus
            INDsleCurrencyType.Properties.DataSource = ListCurrencyType
            INDsleInstrument.Properties.DataSource = ListInstrument
            INDsleInvestmentType.Properties.DataSource = ListInvestmentType
            INDsleModality.Properties.DataSource = ListModality
            INDslePeriodicity.Properties.DataSource = ListPeriodicity
            INDsleLinked.Properties.DataSource = ListLinked
            INDsleDematerialized.Properties.DataSource = ListDematerialized
            INDsleInvestmentTechnicalReserves.Properties.DataSource = ListInvestmentTechnicalReserves
            HideControls()
            HideColumns()
            ShowLyControl(7, False)
            VisibleIndex()
        ElseIf Format = 8 Then
            INDsleAssessment.Properties.DataSource = ListAssessment
            INDsleStatus.Properties.DataSource = ListStatus
            INDsleCurrencyType.Properties.DataSource = ListCurrencyType
            INDsleInvestmentType.Properties.DataSource = ListInvestmentType
            INDsleModality.Properties.DataSource = ListModality
            INDslePeriodicity.Properties.DataSource = ListPeriodicity
            INDsleLinked.Properties.DataSource = ListLinked
            HideControls()
            HideColumns()
            ShowLyControl(8, False)
            VisibleIndex()
        ElseIf Format = 9 Then
            HideControls()
            HideColumns()
            ShowLyControl(9, False)
            VisibleIndex()
        Else
            HideControls()
            HideColumns()
            VisibleIndex()
            INDlyItemPopupDetails.Enabled = False
        End If

    End Sub

    ''' <summary>
    ''' Oculta los controles para añadir detalles
    ''' </summary>
    Private Sub HideControls()
        INDlyHealthSuperParameters.BeginUpdate()
        INDlyItemCreditorIdBy.HideControl(True)
        INDlyItemCreditConcept.HideControl(True)
        INDlyItemSubsequentMeasurement.HideControl(True)
        INDlyExcludeAccountingVoucher.HideControl(True)
        INDlyItemDebtorFieldId.HideControl(True)
        INDlyItemTypedebt.HideControl(True)
        INDlyItemDebtorsConcept.HideControl(True)
        INDLycStoreHouse.HideControl(True)
        INDlyThirdParty.HideControl(True)
        INDLycSIMEVCode.HideControl(True)
        INDLycRiskRating.HideControl(True)
        INDlyRatingEntity.HideControl(True)
        INDLycAnotherQualifier.HideControl(True)
        INDlyClassAccount.HideControl(True)
        INDlyCurrencyType.HideControl(True)
        INDlyAssessment.HideControl(True)
        INDlyStatus.HideControl(True)
        INDLciMeasureDate.HideControl(True)
        INDLycMeasuredValue.HideControl(True)
        INDLycInvestmentReserves.HideControl(True)
        INDlyInstrument.HideControl(True)
        INDlyInvestmentType.HideControl(True)
        INDLycMnemonic.HideControl(True)
        INDLycCodeTitle.HideControl(True)
        INDLciBroadcastDate.HideControl(True)
        INDLciExpirationDate.HideControl(True)
        INDLciPurchaseDate.HideControl(True)
        INDLycPurchaseValue.HideControl(True)
        INDLycPurchaseRate.HideControl(True)
        INDLycNominalValue.HideControl(True)
        INDLycFacialRate.HideControl(True)
        INDlyModality.HideControl(True)
        INDlyPeriodicity.HideControl(True)
        INDLycMarketRate.HideControl(True)
        INDLycMarketValue.HideControl(True)
        INDLycDuration.HideControl(True)
        INDlyLinked.HideControl(True)
        INDlyDematerialized.HideControl(True)
        INDlyInvestmentTechnicalReserves.HideControl(True)
        INDLycCountry.HideControl(True)
        INDLycParticipation.HideControl(True)
        INDLycYields.HideControl(True)
        INDLycAnotherInvestmentType.HideControl(True)
        INDlyIMainAccountFt009.HideControl(True)
        INDlyHealthSuperParameters.EndUpdate()
    End Sub

    ''' <summary>
    ''' Oculta todas las columnas
    ''' </summary>
    Private Sub HideColumns()
        INDcolDetail_MainAccount.Visible = False
        INDcolDetail_creditorIdBy.Visible = False
        INDcolDetail_CreditConcept.Visible = False
        INDcolDetail_SubsequentMeasurement.Visible = False
        INDCol_ExcludeJV.Visible = False
        INDColDetail_StoreHouse.Visible = False
        INDColDetail_ThirdPartyNitName.Visible = False
        INDColDetail_SIMEVCode.Visible = False
        INDColDetail_RiskRating.Visible = False
        INDColDetail_RatingEntity.Visible = False
        INDColDetail_AnotherQualifier.Visible = False
        INDColDetail_ClassAccount.Visible = False
        INDColDetail_CurrencyType.Visible = False
        INDColDetail_Assessment.Visible = False
        INDColDetail_StatusName.Visible = False
        INDColDetail_MeasureDate.Visible = False
        INDColDetail_MeasuredValue.Visible = False
        INDColDebtorFieldId.Visible = False
        INDColDebtorsConcept.Visible = False
        INDColTypedebt.Visible = False
        INDColInstrument.Visible = False
        INDColRatingEntity.Visible = False
        INDColRiskRating.Visible = False
        INDColDematerialized.Visible = False
        INDColInvestmentTechnicalReserves.Visible = False
        INDColMnemonic.Visible = False
        INDColCodeTitle.Visible = False
        INDColAnotherQualifier.Visible = False
        INDColSIMEVCode.Visible = False
        IndigoGridControl1.RefreshGrid(INDGcDetailFt00)
        IndigoGridControl1.RefreshGrid(INDgcDetail)
    End Sub

    ''' <summary>
    ''' Muestra u oculta controles específicos dentro del formulario según el valor de Detail
    ''' </summary>
    ''' <param name="Detail"></param>
    ''' <param name="Value"></param>
    Private Sub ShowLyControl(Detail As Integer, Value As Boolean)
        INDlyHealthSuperParameters.BeginUpdate()
        INDlyItemMainAccount.HideControl(Value)
        If Detail = 4 Then
            INDlyItemCreditorIdBy.HideControl(Value)
            INDlyItemCreditConcept.HideControl(Value)
            INDlyItemSubsequentMeasurement.HideControl(Value)
            INDlyExcludeAccountingVoucher.HideControl(Value)
        ElseIf Detail = 3 Then
            INDlyItemDebtorFieldId.HideControl(Value)
            INDlyItemTypedebt.HideControl(Value)
            INDlyItemDebtorsConcept.HideControl(Value)
            INDlyItemSubsequentMeasurement.HideControl(Value)
        ElseIf Detail = 6 Then
            INDLycStoreHouse.HideControl(Value)
            INDlyThirdParty.HideControl(Value)
            INDLycSIMEVCode.HideControl(Value)
            INDLycRiskRating.HideControl(Value)
            INDlyRatingEntity.HideControl(Value)
            INDLycAnotherQualifier.HideControl(Value)
            INDlyClassAccount.HideControl(Value)
            INDlyCurrencyType.HideControl(Value)
            INDlyAssessment.HideControl(Value)
            INDlyStatus.HideControl(Value)
            INDLciMeasureDate.HideControl(Value)
            INDLycMeasuredValue.HideControl(Value)
            INDLycInvestmentReserves.HideControl(Value)
        ElseIf Detail = 7 Then
            INDLycSIMEVCode.HideControl(Value)
            INDLycRiskRating.HideControl(Value)
            INDlyRatingEntity.HideControl(Value)
            INDLycAnotherQualifier.HideControl(Value)
            INDlyInstrument.HideControl(Value)
            INDLycMnemonic.HideControl(Value)
            INDLycCodeTitle.HideControl(Value)
            INDlyDematerialized.HideControl(Value)
            INDlyInvestmentTechnicalReserves.HideControl(Value)
        ElseIf Detail = 8 Then
            INDLycCountry.HideControl(Value)
            INDLycParticipation.HideControl(Value)
            INDLycYields.HideControl(Value)
            INDLycAnotherInvestmentType.HideControl(Value)

        ElseIf Detail = 9 Then
            INDlyItemMainAccount.HideControl(True)
            INDlyIMainAccountFt009.HideControl(Value)
        End If
        If {7, 8}.Contains(Detail) Then
            INDlyThirdParty.HideControl(Value)
            INDlyInvestmentType.HideControl(Value)
            INDLciBroadcastDate.HideControl(Value)
            INDLciExpirationDate.HideControl(Value)
            INDLciPurchaseDate.HideControl(Value)
            INDLycPurchaseValue.HideControl(Value)
            INDLycPurchaseRate.HideControl(Value)
            INDLycNominalValue.HideControl(Value)
            INDlyCurrencyType.HideControl(Value)
            INDLycFacialRate.HideControl(Value)
            INDlyModality.HideControl(Value)
            INDlyPeriodicity.HideControl(Value)
            INDLycMarketRate.HideControl(Value)
            INDLycMarketValue.HideControl(Value)
            INDLycDuration.HideControl(Value)
            INDlyAssessment.HideControl(Value)
            INDlyStatus.HideControl(Value)
            INDLciMeasureDate.HideControl(Value)
            INDLycMeasuredValue.HideControl(Value)
            INDlyLinked.HideControl(Value)

        End If
        INDlyHealthSuperParameters.EndUpdate()
    End Sub

    ''' <summary>
    ''' Ajusta la visibilidad y el orden de las columnas en un diseño de vista específico según el valor de "format"
    ''' </summary>
    Private Sub VisibleIndex()
        INDcolDetail_MainAccount.VisibleIndex = 0
        INDlcIDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcItemDetailFt00.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        If Format = 4 Then
            INDcolDetail_CreditConcept.VisibleIndex = 1
            INDcolDetail_creditorIdBy.VisibleIndex = 2
            INDcolDetail_SubsequentMeasurement.VisibleIndex = 3
            INDCol_ExcludeJV.VisibleIndex = 4
            INDlcIDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        ElseIf Format = 3 Then
            INDColDebtorFieldId.VisibleIndex = 1
            INDColDebtorsConcept.VisibleIndex = 2
            INDColTypedebt.VisibleIndex = 3
            INDcolDetail_SubsequentMeasurement.VisibleIndex = 4
            INDlcIDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        ElseIf Format = 6 Then
            INDColDetail_StoreHouse.VisibleIndex = 1
            INDColDetail_ThirdPartyNitName.VisibleIndex = 2
            INDColDetail_SIMEVCode.VisibleIndex = 3
            INDColDetail_RiskRating.VisibleIndex = 4
            INDColDetail_RatingEntity.VisibleIndex = 5
            INDColDetail_AnotherQualifier.VisibleIndex = 6
            INDColDetail_ClassAccount.VisibleIndex = 7
            INDColDetail_Assessment.VisibleIndex = 8
            INDColDetail_StatusName.VisibleIndex = 9
            INDColDetail_MeasureDate.VisibleIndex = 10
            INDColDetail_MeasuredValue.VisibleIndex = 11
            INDlcIDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        ElseIf Format = 7 Then
            INDLcItemDetailFt00.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDColInstrument.Visible = True
            INDColRatingEntity.Visible = True
            INDColRiskRating.Visible = True
            INDColDematerialized.Visible = True
            INDColInvestmentTechnicalReserves.Visible = True
            INDColMnemonic.Visible = True
            INDColCodeTitle.Visible = True
            INDColAnotherQualifier.Visible = True
            INDColSIMEVCode.Visible = True
        ElseIf Format = 8 Then
            INDLcItemDetailFt00.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDColCountry.VisibleIndex = 2
            INDColParticipation.VisibleIndex = 23
            INDColYields.Visible = 24
            INDColAnotherInvestmentType.Visible = 4
        ElseIf Format = 9 Then
            INDlcIDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando cambia la visibilidad del control INDPopUpGridD
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPopUpGridD_VisibleChanged(sender As Object, e As EventArgs) Handles INDPopUpGridD.VisibleChanged
        Dim ObjAccount = CType(INDgvDetails.GetFocusedRow(), HealthSuperParametersFt004)
        Dim HealthSuperParametersFt004 As New HealthSuperParametersFt004
        HealthSuperParametersFt004 = _listHealthSuperParametersFt004.Where(Function(x) x.MainAccountId = ObjAccount.MainAccountId).FirstOrDefault
        With HealthSuperParametersFt004
            INDGcHealthSPFt004D.DataSource = Nothing
            INDGcHealthSPFt004D.DataSource = .HealthSuperParametersFt004Detail
        End With
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando cambia el valor en el control de entidad calificadora
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRatingEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRatingEntity.EditValueChanged
        If RatingEntity = 4 Then
            INDTbAnotherQualifier.ReadOnly = False
        Else
            AnotherQualifier = "NA"
            INDTbAnotherQualifier.ReadOnly = True
        End If
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando cambia el valor en el control de Estado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleStatus_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleStatus.EditValueChanged
        If Status = 0 OrElse Status = 1 Then
            INDDMeasureDate.Text = "00000000"
            MeasureDate = #1/01/0001#
            MeasuredValue = 0
            INDDMeasureDate.ReadOnly = True
            INDTbMeasuredValue.ReadOnly = True
        Else
            INDTbMeasuredValue.ReadOnly = False
            INDDMeasureDate.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando cambia el valor en el control de tipo de inversión
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleInvestmentType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleInvestmentType.EditValueChanged
        Select Case InvestmentType
            Case 1
                Instrument = 1
                INDsleInstrument.Enabled = False
                INDsleInvestmentTechnicalReserves.Enabled = True
            Case 2 To 7
                Instrument = 2
                INDsleInstrument.Enabled = False
                INDsleInvestmentTechnicalReserves.Enabled = True
            Case 8, 9
                Instrument = 3
                INDsleInstrument.Enabled = False
                InvestmentTechnicalReserves = False
                INDsleInvestmentTechnicalReserves.Enabled = False
            Case 10
                INDsleInstrument.Enabled = True
                INDsleInvestmentTechnicalReserves.Enabled = True
        End Select
        If Format = 8 Then
            If InvestmentType = 10 Then
                INDLycAnotherInvestmentType.Enabled = True
            Else
                AnotherInvestmentType = "NA"
                INDLycAnotherInvestmentType.Enabled = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando cambia el valor en el control de Instrumento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleInstrument_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleInstrument.EditValueChanged
        If Instrument = 3 Then
            BroadcastDate = Nothing
            INDDBroadcastDate.Properties.NullText = "00000000"
            INDDBroadcastDate.Enabled = False
            ExpirationDate = Nothing
            INDDExpirationDate.Properties.NullText = "00000000"
            INDDExpirationDate.Enabled = False
            PurchaseDate = Nothing
            INDDPurchaseDate.Properties.NullText = "00000000"
            INDDPurchaseDate.Enabled = False
            FacialRate = "0"
            INDTbFacialRate.Enabled = False
            Duration = 0
            INDTbDuration.Enabled = False
        Else
            INDDBroadcastDate.Enabled = True
            INDDExpirationDate.Enabled = True
            INDDPurchaseDate.Enabled = True
            INDTbFacialRate.Enabled = True
            INDTbDuration.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando cambia el valor en el control de Fecha de vencimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDDExpirationDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDExpirationDate.EditValueChanged
        Dim ExpiartionDays As TimeSpan
        If ExpirationDate IsNot Nothing AndAlso BroadcastDate IsNot Nothing Then
            ExpiartionDays = DateValue(ExpirationDate) - DateValue(BroadcastDate)
            If ExpiartionDays.Days <= 0 Then
                ExpirationDate = BroadcastDate
            End If
        End If
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando cambia el valor en el control de inversión de las reservas técnicas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleInvestmentTechnicalReserves_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleInvestmentTechnicalReserves.EditValueChanged
        If InvestmentTechnicalReserves Then
            Status = 0
            Assessment = False
            INDsleAssessment.Enabled = False
        Else
            INDsleAssessment.Enabled = True
        End If
    End Sub

#End Region

#Region "ContextMenuGridControl"

    ''' <summary>
    ''' Este evento se activa cuando se hace clic en el botón de acción del menú contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If Format = 4 Then
            DeleteDetail()
        ElseIf Format = 3 Then
            DeleteDetailFT003()
        ElseIf Format = 6 Then
            DeleteDetailFT006()
        ElseIf Format = 7 Then
            DeleteDetailFT007()
        ElseIf Format = 8 Then
            DeleteDetailFT008()
        ElseIf Format = 9 Then
            DeleteDetailFT009()
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento de la barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Async Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Await Delete()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.GeneralLedgerSequenceDetail IsNot Nothing Then
                If Not Me._sequense.GeneralLedgerSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class