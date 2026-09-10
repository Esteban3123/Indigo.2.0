'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Hector Rodriguez
' Created          : 21/11/2019
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
Imports System.Data
#End Region

Public Interface ICashFlowReclassification
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
    Property DocumentType As Byte?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property Code As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property DetailsDatasource As DataTable

    ''' <summary>
    ''' 
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

End Interface
