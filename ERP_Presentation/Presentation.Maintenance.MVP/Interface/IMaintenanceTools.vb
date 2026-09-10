#Region "Imports"

Imports Presentation.Base
Imports Presentation.CloudAgent.IndigoReference.FixedAsset
Imports Domain.Entities

#End Region

Public Interface IMaintenanceTools
    Inherits ICrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo de las herramientas
    ''' </summary>
    Property CodeTools As String
	
    ''' <summary>
    ''' Esta propiedad contiene el nombre de la herramientas
    ''' </summary>
    Property NameTools As String

    ''' <summary>
    ''' Esta Propiedad contiene el Estado de la herramientas
    ''' </summary>
    Property AssociateAsset As Boolean

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
	
    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.MaintenanceSequence

#End Region

End Interface
