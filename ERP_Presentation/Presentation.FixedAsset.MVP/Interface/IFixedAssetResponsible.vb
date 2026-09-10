'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 16-09-2014
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

Public Interface IFixedAssetResponsible
    Inherits IcrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo de la Marca
    ''' </summary>
    Property CodeResponsible As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre de la Marca
    ''' </summary>
    Property IdThirdParty As Integer
    ''' <summary>
    ''' Esta propiedad contiene el nombre de la Marca
    ''' </summary>
    Property IdVinculationType As Integer
    ''' <summary>
    ''' Esta propiedad contiene el nombre de la Marca
    ''' </summary>
    Property IdResponsibleType As Integer
    ''' <summary>
    ''' Esta Propiedad contiene el Estado de la Marca
    ''' </summary>
    Property StateResponsible As Boolean
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
