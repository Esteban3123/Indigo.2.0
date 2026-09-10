'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 30-06-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports DevExpress.Xpo

#End Region

Public Interface ISalaryIncrease
    Inherits IcrudBase

#Region "Properties"
   
    ''' <summary>
    ''' Propiedad que contiene el id del Grupo
    ''' </summary>
    Property GroupId As Integer
    ''' <summary>
    ''' Propiedad que contiene el id de la Unidad Funcional
    ''' </summary>
    Property FunctionalUnitId As Integer?
    ''' <summary>
    ''' Propiedad que contiene el id de la Unidad Funcional
    ''' </summary>
    Property PositionId As Integer?
    ''' <summary>
    ''' Propiedad que contiene el id de la Unidad Funcional
    ''' </summary>
    Property ModificationReasonContractId As Integer
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
