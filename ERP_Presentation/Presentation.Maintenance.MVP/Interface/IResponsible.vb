'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 02-09-2013
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
Public Interface IResponsible
    Inherits IcrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo del responsable
    ''' </summary>
    Property CodeResponsible As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del responsable
    ''' </summary>
    Property NameResponsible As String
    ''' <summary>
    ''' Esta propiedad contiene el tipo de responsable
    ''' </summary>
    Property ResponsibleType As String
    ''' <summary>
    ''' Esta propiedad contiene el tipo de vinculacion
    ''' </summary>
    Property TypeEntailment As String
    ''' <summary>
    ''' Esta propiedad contiene id de la sucursal
    ''' </summary>
    Property IdBranch As Integer
    ''' <summary>
    ''' Esta propiedad contiene id del centro de costo
    ''' </summary>
    Property IdCostCenter As Integer
    ''' <summary>
    ''' Esta propiedad contiene el estado del responsable
    ''' </summary>
    Property StateResponsible As Boolean
    ''' <summary>
    ''' Propiedad que carga todas las sucursales
    ''' </summary>
    WriteOnly Property BranchDataSource As List(Of Branch)
    ''' <summary>
    ''' Propiedad que carga todas los centros de costo
    ''' </summary>
    WriteOnly Property CostCenterDataSource As List(Of Domain.Maintenance.Entities.CostCenter)
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object
    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.MaintenanceSequence

#End Region
End Interface
