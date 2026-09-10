'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 05-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Domain.Payroll.Entities
#End Region

''' <summary>
''' Interfaz que maneja el frontal de grupo de contratos
''' </summary>
Public Interface IContractGroup
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el codigo del grupo de contratos
    ''' </summary>
    Property ContractGroupCode As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del grupo de contratos
    ''' </summary>
    Property ContractGroupName As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del grupo de contratos
    ''' </summary>
    Property ContractGroupDescription As String

    ''' <summary>
    ''' Propiedad que contiene el estado del grupo de contratos
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

End Interface
