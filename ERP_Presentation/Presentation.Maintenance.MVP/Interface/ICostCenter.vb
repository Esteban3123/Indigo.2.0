'***********************************************************************
' Assembly         : Presentation.Common.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 16-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Domain.Maintenance.Entities
Imports Domain.Entities

Public Interface ICostCenter
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el codigo del centro de costo
    ''' </summary>
    Property CodeCostCenter As String

    ''' <summary>
    ''' Nombre centro de costo
    ''' </summary>
    Property NameCostCenter As String

    ''' <summary>
    ''' Estado de un centro de costo
    ''' </summary>
    Property StatusCostCenter As Boolean

    WriteOnly Property ActionsOnControls As Boolean
    ReadOnly Property MyTag As Object
    Property Sequence As MaintenanceSequence
End Interface
