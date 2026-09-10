'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 03-09-2013
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

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IEquipment
    Inherits IcrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo del equipo
    ''' </summary>
    Property CodeEquipment As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del equipo
    ''' </summary>
    Property NameEquipment As String
    ''' <summary>
    ''' Esta propiedad contiene id del tipo de equipo
    ''' </summary>
    Property IdEquipmentType As Integer
    ''' <summary>
    ''' Esta propiedad contiene tiempo de vida 
    ''' </summary>
    Property LifeTime As Integer
    ''' <summary>
    ''' Esta propiedad contiene los comentarios
    ''' </summary>
    Property Comments As String
    ''' <summary>
    ''' Esta propiedad contiene el estado de la torre
    ''' </summary>
    Property StateEquipment As Boolean
    ''' <summary>
    ''' Propiedad que carga todas  tipos de equipo
    ''' </summary>
    WriteOnly Property EquipmentTypeDataSource As List(Of Object)
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region
End Interface
