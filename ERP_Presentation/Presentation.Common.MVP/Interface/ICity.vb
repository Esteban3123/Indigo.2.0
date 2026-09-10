'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Jose Luis Rojas 
' Created          : 11-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "imports"
Imports Presentation.Base
Imports Domain.Entities
Imports Presentation.Controls
#End Region

''' <summary>
''' Interfaz que contiene las propiedades del frontal de Ciudades
''' </summary>
Public Interface ICity
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el codigo de la ciudad
    ''' </summary>
    Property CodeCity As String

    ''' <summary>
    ''' Propiedad que contiene el nombre de la ciudad
    ''' </summary>
    Property NameCity As String

    ''' <summary>
    ''' Propiedad que contiene el id del departamento al cual pertenece la ciudad
    ''' </summary>
    Property IdDepartment As String

    ''' <summary>
    ''' Propiedad que contiene el estado de la ciudad
    ''' </summary>
    Property StatusCity As Boolean

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControsl As Boolean

    ''' <summary>
    ''' DataSource que contiene el listado completo de departamentos 
    ''' </summary>
    Property DataSourceOfAllDepartments As List(Of Department)

    ''' <summary>
    ''' DataSource que contiene el listado completo de Paises
    ''' </summary>
    Property DataSourceOfAllCountries As List(Of Country)

    ''' <summary>
    ''' Id del concepto de retención
    ''' </summary>
    ''' <returns></returns>
    Property ICARetentionConceptId As Integer?

    ''' <summary>
    ''' Datasource del concepto de retencion
    ''' </summary>
    ''' <returns></returns>
    Property ICARetentionConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que retorna el layout para customizaciones
    ''' </summary>

    ReadOnly Property MyLayoutControl As IndigoLayoutControl
End Interface
