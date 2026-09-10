'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 25-04-2047
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

Public Interface ISettingsExogenousInformation
    Inherits IcrudBase

    Property AccountingAccountFormat1001 As XPInstantFeedbackSource
    Property AccountingAccountFormat1007 As XPInstantFeedbackSource
    Property AccountingAccountFormat1003 As XPInstantFeedbackSource
    Property AccountingAccountFormat1004 As XPInstantFeedbackSource
    Property AccountingAccountFormat1008 As XPInstantFeedbackSource
    Property AccountingAccountFormat1009 As XPInstantFeedbackSource
    Property AccountingAccountFormat1647 As XPInstantFeedbackSource
    ReadOnly Property MyTag As Object

End Interface
