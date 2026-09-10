'***********************************************************************
' Assembly         : Presentacion.FixedAssets
' Author           : Andres Alarcon
' Created          : 10-06-2024
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Base
Imports Presentation.CloudAgent.IndigoReference.FixedAsset
Imports Domain.Entities

#End Region
Public Interface IFixedAssetCatalogOfPropertyandServices
    Inherits ICrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo de la Marca
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Esta propiedad contiene el nombre de la Marca
    ''' </summary>
    Property Name As String

    ''' <summary>
    ''' Esta propiedad contiene el Id del IVA
    ''' </summary>
    Property IVAId As Integer?

    ''' <summary>
    ''' Esta Propiedad contiene el Estado de la Marca
    ''' </summary>
    Property State As Boolean

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.FixedAssetSequence

#End Region

End Interface
