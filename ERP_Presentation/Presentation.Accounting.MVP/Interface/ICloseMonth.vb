'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 05-05-2014
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
Public Interface ICloseMonth
    Inherits IcrudBase

    ''' <summary>
    ''' Gets or sets the month.
    ''' </summary>
    ''' <value>
    ''' The month.
    ''' </value>
    Property Month As Integer
    ''' <summary>
    ''' Gets or sets the year.
    ''' </summary>
    ''' <value>
    ''' The year.
    ''' </value>
    Property Year As Integer
    ''' <summary>
    ''' Gets or sets a value indicating whether [status].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [status]; otherwise, <c>false</c>.
    ''' </value>
    Property Status As Boolean

End Interface
