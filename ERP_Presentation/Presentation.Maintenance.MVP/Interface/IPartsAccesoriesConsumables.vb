'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 26-08-2015
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

#End Region

Public Interface IPartsAccesoriesConsumables
    Inherits IcrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo de PartsAccesoriesConsumables
    ''' </summary>
    Property CodePartsAccesoriesConsumables As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre de PartsAccesoriesConsumables
    ''' </summary>
    Property NamePartsAccesoriesConsumables As String
   
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
