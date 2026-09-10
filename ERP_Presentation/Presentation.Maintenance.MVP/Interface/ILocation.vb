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
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface ILocation
    Inherits IcrudBase

#Region "properties"

    ''' <summary>
    ''' Esta propiedad contiene el datasource de las ubicaciones
    ''' </summary>
    WriteOnly Property LocationDatasource As List(Of Location)
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' establece el estado 
    ''' </summary>
    Property StateLocation As Boolean

#End Region

End Interface
