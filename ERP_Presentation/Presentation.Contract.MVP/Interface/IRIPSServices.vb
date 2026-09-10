Imports DevExpress.Xpo
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Controls

Public Interface IRIPSServices
    Inherits ICrudBase

    ''' <summary>
    ''' Retorna el Layout para customizaciones
    ''' </summary>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Establece el valor AcciónControles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Obtiene o asigna una secuencia numerica al formulario
    ''' </summary>
    ''' <returns></returns>
    Property Sequense As ContractSequence

    ''' <summary>
    ''' Obtiene o asigna código al grupo de servicio RIPS
    ''' </summary>
    ''' <returns></returns>
    Property Code As String

    ''' <summary>
    ''' Obtiene o asigna un nombre al grupo de servicio RIPS
    ''' </summary>
    ''' <returns></returns>
    Property Name As String

    ''' <summary>
    ''' Obtiene o asigna el estado
    '''     1. Activo
    '''     2. Inactivo
    '''     Al grupo de servicios RIPS
    ''' </summary>
    ''' <returns></returns>
    Property Status As Boolean

    ''' <summary>
    ''' Id de grupo de servicios RIPS que viene de RIPSServiceGroups
    ''' </summary>
    ''' <returns></returns>
    Property ServiceGroupId As Integer?

    ''' <summary>
    ''' Establece el datasource de los grupos de servicios RIPS
    ''' </summary>
    ''' <returns></returns>
    Property RIPSServiceGroupsXpo As XPInstantFeedbackSource
End Interface
