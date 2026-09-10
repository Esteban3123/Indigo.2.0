'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Juan F. Tamayo
' Created          : 2014-10-23
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Domain.Entities
Imports Presentation.Controls

#End Region

Public Interface IInventoryRiskLevel
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el código del nivel de riesgo
    ''' </summary>
    ''' <value>Código del nivel de riesgo</value>
    ''' <returns>El código del nivel de riesgo</returns>
    Property Code As String

    ''' <summary>
    ''' Obtiene o asigna el nombre del nivel de riesgo
    ''' </summary>
    ''' <value>Nombre del nivel de riesgo</value>
    ''' <returns>El nombre del nivel de riesgo</returns>
    Property RiskLevelName As String

    ''' <summary>
    ''' Obtiene o asigna un valor que indica el estado del nivel de riesgo
    ''' </summary>
    ''' <value>Estado del nivel de riesgo</value>
    ''' <returns>El estado del nivel de riesgo</returns>
    Property Status As Boolean

    ''' <summary>
    ''' Obtiene o asigna la configuración de secuencia 
    ''' numerica asignada al funcional
    ''' </summary>
    ''' <value>Configuración de secuencia numerica</value>
    ''' <returns>La configuración de secuencia numerica</returns>
    Property Sequence As InventorySequence

    ''' <summary>
    ''' Obtiene o asigna el tag del funcional
    ''' </summary>
    ''' <value>Tag del funcional</value>
    ''' <returns>El tag del funcional</returns>
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Propiedad para habilitar o desabilitar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ActionsOnControls As Boolean

    ReadOnly Property MyLayoutControl As IndigoLayoutControl

#End Region

End Interface