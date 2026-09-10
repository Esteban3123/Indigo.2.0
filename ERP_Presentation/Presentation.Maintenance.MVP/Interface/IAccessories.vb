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
Public Interface IAccessories
    Inherits IcrudBase
#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo del accesorio
    ''' </summary>
    Property CodeAccessory As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del accesorio
    ''' </summary>
    Property NameAccessory As String
    ''' <summary>
    ''' Esta propiedad contiene el id del tipo de equipo
    ''' </summary>
    Property IdEquipmentType As Integer?
    ''' <summary>
    ''' Esta propiedad contiene el estado del accessorio
    ''' </summary>
    Property StateAccessory As Boolean
    ''' <summary>
    ''' Propiedad que carga todas  tipos de equipo
    ''' </summary>
    WriteOnly Property EquipmentTypeDataSource As List(Of FixedAssetEquipmentType)
    ''' <summary>
    ''' Propiedad que carga todos los tipos de equipos agregados 
    ''' </summary>
    WriteOnly Property ListEquipmentTypeDataSource As List(Of FixedAssetEquipmentType)
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    Property EquipmentTypeXpo As XPCollection

#End Region
End Interface
