'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Hector Rodriguez
' Created          : 31/10/2019
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
Imports Presentation.Controls
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.TreasuryRepository
#End Region

Public Interface ICheckCashingControl
    Inherits IcrudBase

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property DocumentDate As DateTime?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property Observation As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property IdBankAccount As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property EntityBankAccountDatasource As LinqInstantFeedbackSource

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property VoucherTransactionDatasource As XPCollection(Of TreasuryVoucherTransactionXpo)

    ''' <summary>
    ''' 
    ''' </summary>
    WriteOnly Property DetailsVoucherTransaction As XPCollection(Of TreasuryVoucherTransactionXpo)

    ''' <summary>
    ''' 
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property EndBalanceBook As Decimal
End Interface
