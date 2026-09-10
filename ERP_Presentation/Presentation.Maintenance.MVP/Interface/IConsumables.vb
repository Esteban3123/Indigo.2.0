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
Imports Domain.Entities
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IConsumables
    Inherits IcrudBase
#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo del Consumible
    ''' </summary>
    Property CodeConsumable As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del Consumible
    ''' </summary>
    Property NameConsumable As String
    ''' <summary>
    ''' Esta propiedad contiene el id del tipo de equipo
    ''' </summary>
    Property IdEquipmentType As Integer
    ''' <summary>
    ''' Propiedad que carga todas  tipos de equipo
    ''' </summary>
    WriteOnly Property EquipmentTypeDataSource As XPCollection
    ''' <summary>
    ''' Esta propiedad contiene el estado del Consumible
    ''' </summary>
    Property StateConsumable As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region
End Interface
