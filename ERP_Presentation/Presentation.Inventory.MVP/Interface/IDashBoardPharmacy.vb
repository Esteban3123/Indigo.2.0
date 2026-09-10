'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego Andrés Roldán
' Created          : 10-02-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Domain.Entities
Imports DevExpress.Data.Linq

#End Region

Public Interface IDashBoardPharmacy

#Region "Properties"

    ''' <summary>
    ''' Unidad funcional
    ''' </summary>
    Property CareCenterCode As String

    ''' <summary>
    ''' Datasource de unidades funcionales
    ''' </summary>
    Property CareCenterXPO As XPInstantFeedbackSource

#End Region

End Interface