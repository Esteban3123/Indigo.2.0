'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 05-08-2013
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
Public Interface IEquipmentType
    Inherits IcrudBase
#Region "properties"
    ' ''' <summary>
    ' ''' Esta propiedad contiene el codigo del tipo de equipo
    ' ''' </summary>
    'Property CodeEquipamentType As String
    ' ''' <summary>
    ' ''' Esta propiedad contiene el nombre del tipo de equipo
    ' ''' </summary>
    'Property NameEquipamentType As String
    ' ''' <summary>
    ' ''' Esta propiedad contiene el codigo del tipo de inventario
    ' ''' </summary>
    'Property EquipamentType As String
    ' ''' <summary>
    ' ''' Esta propiedad contiene el estado tipo de equipo
    ' ''' </summary>
    'Property StateEquipamentType As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Propiedad que carga los tipo de invetario
    ''' </summary>
    WriteOnly Property EquipmentTypeDataSource As List(Of EquipmentType)

#End Region
End Interface
