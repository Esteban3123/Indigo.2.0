'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 02-09-2015
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
Imports Domain.Entities

#End Region

Public Interface IMaintenancePlan
    Inherits IcrudBase

    ''' <summary>
    ''' Esta propiedad contiene el codigo del responsable
    ''' </summary>
    Property CodePlanMaintenance As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del responsable
    ''' </summary>
    Property NamePlanMaintenance As String
    ''' <summary>
    ''' Esta propiedad contiene el tipo de responsable
    ''' </summary>
    Property InitialDate As Date
    ''' <summary>
    ''' Esta propiedad contiene el tipo de vinculacion
    ''' </summary>
    Property RegimeType As Byte
    ''' <summary>
    ''' Esta propiedad contiene id de la sucursal
    ''' </summary>
    Property IdMeasurementUnit As Integer
    ''' <summary>
    ''' Esta propiedad contiene id del centro de costo
    ''' </summary>
    Property IdEquipmentType As Integer
    ''' <summary>
    ''' Esta propiedad contiene el estado del responsable
    ''' </summary>
    Property StateMaintenancePlan As Boolean
    ''' <summary>
    ''' Propiedad que carga todas las sucursales
    ''' </summary>
    WriteOnly Property MeasurmentUnitDataSource As List(Of MeasurementUnit)

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

End Interface
