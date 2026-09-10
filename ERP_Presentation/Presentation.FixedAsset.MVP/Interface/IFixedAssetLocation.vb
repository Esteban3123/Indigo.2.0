'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Jeisson Herrera Peña
' Created          : 25/09/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Domain.Entities

#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IFixedAssetLocation
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el datasource de las ubicaciones
    ''' </summary>
    WriteOnly Property LocationDatasource As List(Of FixedAssetLocation)
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
