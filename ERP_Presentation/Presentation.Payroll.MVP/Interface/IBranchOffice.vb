'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 19-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports CommonEntities = Domain.Entities

#End Region
''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IBranchOffice
    Inherits IcrudBase

#Region "Properties"
    ''' <summary>
    ''' Propiedad que contiene el codigo de la unidad de negocio
    ''' </summary>
    Property Code As String
    ''' <summary>
    ''' Propiedad que contiene el nombre de la unidad de negocio
    ''' </summary>
    Property Name As String
    ''' <summary>
    ''' Propiedad que contiene el ID de el tercero
    ''' </summary>
    Property CompanyId As Integer
    ''' <summary>
    ''' Propiedad que contiene la direccion
    ''' </summary>
    Property Address As String
    ''' <summary>
    ''' Propiedad que contiene ID del departamento
    ''' </summary>
    Property DepartmentId As Integer
    ''' <summary>
    ''' Propiedad que contiene el ID de la ciudad
    ''' </summary>
    Property CityId As Integer
    ''' <summary>
    ''' Propiedad que contiene el telefono fijo
    ''' </summary>
    Property Phone As String
    ''' <summary>
    ''' Propiedad que carga el grid look up de departamentos
    ''' </summary>
    WriteOnly Property Departments As List(Of Domain.Entities.Department)
    ''' <summary>
    ''' Propiedad que carga el grid look up de ciudades
    ''' </summary>
    WriteOnly Property City As List(Of Domain.Entities.City)
    ''' <summary>
    ''' Propiedad que carga el grid look up de terceros
    ''' </summary>
    WriteOnly Property Company As List(Of Company)
    ''' <summary>
    ''' Propiedad que Contiene el estado de la unidad de negocio
    ''' </summary>
    Property State As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
#End Region
End Interface
