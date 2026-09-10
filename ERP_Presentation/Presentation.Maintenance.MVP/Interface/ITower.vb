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
Public Interface ITower

    Inherits IcrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo de la torre
    ''' </summary>
    Property CodeTower As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre de la torre
    ''' </summary>
    Property NameTower As String
    ''' <summary>
    ''' Esta propiedad contiene id de la sucursal
    ''' </summary>
    Property IdBranch As Integer
    ''' <summary>
    ''' Esta propiedad contiene el estado de la torre
    ''' </summary>
    Property StateTower As Boolean
    ''' <summary>
    ''' Propiedad que carga todas las sucursales
    ''' </summary>
    WriteOnly Property BranchDataSource As List(Of Branch)
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region
End Interface
