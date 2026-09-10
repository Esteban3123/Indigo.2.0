'***********************************************************************
' Assembly         : Presentation.Maintenance
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 04-02-2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IMaintenanceFailureRequest
    Inherits ICrudBase

    ''' <summary>
    ''' Obtiene o asigna el tag del funcional
    ''' </summary>
    ''' <value>Tag del funcional</value>
    ''' <returns>El tag del funcional</returns>
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o asigna la configuración de secuencia 
    ''' numerica asignada al funcional
    ''' </summary>
    ''' <value>Configuración de secuencia numerica</value>
    ''' <returns>La configuración de secuencia numerica</returns>
    Property Sequence As MaintenanceSequence

    ''' <summary>
    ''' codigo de la remision
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FailureRequestDate As Date

    ''' <summary>
    ''' Obtiene o Asigna el nombre
    ''' </summary>
    Property NameOther As String

    ''' <summary>
    ''' Obtiene o establece la observacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Observation As String

End Interface
