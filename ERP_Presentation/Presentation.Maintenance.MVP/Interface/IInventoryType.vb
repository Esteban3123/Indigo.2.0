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
Public Interface IInventoryType
    Inherits IcrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo del tipo de inventario
    ''' </summary>
    Property CodeInventoryType As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del tipo de inventario
    ''' </summary>
    Property NameInventoryType As String
    ''' <summary>
    ''' Esta propiedad contiene el tipo de inventario
    ''' </summary>
    Property TypeInventoryType As String
    ''' <summary>
    ''' Esta propiedad contiene el estado del tipo de inventario
    ''' </summary>
    Property StateInventoryType As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region
End Interface
