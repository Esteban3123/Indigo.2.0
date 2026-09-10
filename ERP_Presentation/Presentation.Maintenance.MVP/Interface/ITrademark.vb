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
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Maintenance.Entities
Imports Domain.Entities

#End Region
Public Interface ITrademark
    Inherits IcrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo de la Marca
    ''' </summary>
    Property CodeTrademark As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre de la Marca
    ''' </summary>
    Property NameTrademark As String
    ''' <summary>
    ''' Esta Propiedad contiene el Estado de la Marca
    ''' </summary>
    Property StateTrademark As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    Property Sequence As MaintenanceSequence
    ReadOnly Property MyTag As Object

#End Region

End Interface
