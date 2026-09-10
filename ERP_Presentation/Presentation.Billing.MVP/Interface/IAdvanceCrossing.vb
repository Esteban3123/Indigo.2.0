'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Giovanny Plazas Lozano
' Created          : 27/06/2024
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Controls
#End Region

Public Interface IAdvanceCrossing

#Region "Properties"
    ''' <summary>
    ''' Public property to get source currencyId 
    ''' </summary>
    ReadOnly Property DocumentCurrencyId As Integer

    ''' <summary>
    ''' Public property to get source balance document
    ''' </summary>
    ReadOnly Property BalanceDocument As Decimal

    ''' <summary>
    ''' Public property to get source thirdpartyId
    ''' </summary>
    ReadOnly Property ThirdPartyId As Integer

    ''' <summary>
    ''' public property to obtain Current Balance (Document Balance - SumOfAdvances)
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property GetCurrentBalance As Decimal

#End Region
#Region "DataSource"
    ''' <summary>
    ''' Private Property to Get List of Portfolio Advances added in gridview
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property ListPortfolioAdvance As List(Of Domain.Entities.PortfolioAdvance)

    ''' <summary>
    ''' Private Property to Get/set DataSource of PortfolioAdvance
    ''' </summary>
    ''' <returns></returns>
    Property DataSourceAdvanced As XPInstantFeedbackSource
#End Region


End Interface
