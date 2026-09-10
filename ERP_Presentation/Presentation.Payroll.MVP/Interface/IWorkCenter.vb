'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 02-07-2013
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

#End Region

''' <summary>
''' Interfaz que maneja el frontal de centros de trabajo
''' </summary>
Public Interface IWorkCenter
    Inherits IcrudBase


    ''' <summary>
    ''' Propiedad que contiene el codigo de centros de trabajo
    ''' </summary>
    Property WorkCenterCode As String

    ''' <summary>
    ''' Propiedad que contiene el nombre de centros de trabajo
    ''' </summary>
    Property WorkCenterName As String

    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades xpo
    ''' </summary>
    Property WorkCenterCitiesXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene la ciudad seleccionada
    ''' </summary>
    Property WorkCenterCityId As Integer

    ''' <summary>
    ''' Propiedad que contiene el estado de centros de trabajo
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

End Interface
