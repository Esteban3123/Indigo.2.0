'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 21-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports DevExpress.Xpo
Imports CommonEntities = Domain.Entities

#End Region

''' <summary>
''' Interfaz que maneja el control del visor de contratos
''' </summary>
''' <remarks></remarks>
Public Interface ICtrContractViewer

    ''' <summary>
    ''' Propiedad que contiene el listado de razones de retiro(XPO)
    ''' </summary>
    WriteOnly Property RetirementReasonDatasourceXPO As XPInstantFeedbackSource

End Interface
