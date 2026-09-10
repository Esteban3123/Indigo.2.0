'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 22-07-2014
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
Public Interface IAccountingStructure
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad del codigo de la Estructura Contable
    ''' </summary>
    Property AccountingStructureCode As String

    ''' <summary>
    ''' Propiedad de la Descripción de la Estructura Contable
    ''' </summary>
    Property AccountingStructureDescription As String

    ''' <summary>
    ''' Propiedad del estado de la Estructura Contable
    ''' </summary>
    Property AccountingStructureStatus As Boolean

    ''' <summary>
    ''' Propiedad que contiene el comportamiento de los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

End Interface
