'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 01-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Base
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Maintenance.Entities

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IBranch
    Inherits IcrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo del tipo de poliza
    ''' </summary>
    Property CodeBranch As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del tipo de poliza
    ''' </summary>
    Property NameBranch As String
    ''' <summary>
    ''' Propiedad que contiene el codigo de la ciudad
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Property IdCity As Integer
    ''' <summary>
    ''' Propiedad que carga los departamentos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property DepartmentDataSource As List(Of Domain.Entities.Department)
    ''' <summary>
    ''' Propiedad que carga las ciudades
    ''' </summary>
    WriteOnly Property CityDataSource As List(Of Domain.Entities.City)
    ''' <summary>
    ''' Esta propiedad contiene el estado del tipo de poliza
    ''' </summary>
    Property StateBranch As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region
End Interface
