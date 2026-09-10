'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 25-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo

#End Region
''' <summary>
''' Interfaz que maneja el frontal de tipo de empleados
''' </summary>
''' <remarks></remarks>
Public Interface IEmployeeType
    Inherits ICrudBase

    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene el codigo de la clase del tipo de empleado
    ''' </summary>
    ''' <returns></returns>
    Property EmployeeClass As String

    ''' <summary>
    ''' Obtiene el codigo del subtipo de cotizante
    ''' </summary>
    ''' <returns></returns>
    Property ContributorSubtypeId As Integer

    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades xpo
    ''' </summary>
    Property ContributorSubtypeXpo As XPInstantFeedbackSource

End Interface
