'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 26-09-2013
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
#End Region
''' <summary>
''' Interfaz que maneja el frontal de razones de modificacion de contratos
''' </summary>
''' <remarks></remarks>
Public Interface IContractModificationReason
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Propiedad para almacenar el tipo de novedad elegido
    ''' </summary>
    ''' <returns></returns>
    Property NoveltyType As Byte

End Interface