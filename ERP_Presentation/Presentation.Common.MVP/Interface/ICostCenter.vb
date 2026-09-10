'***********************************************************************
' Assembly         : Presentation.Common.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 16-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Presentation.Controls

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

    ''' <summary>
    ''' Propiedad que retorna el layout para customizaciones
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Propiedad que devuelve el tag del formulario para hacer interfaz con el presentador
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyTag As Object


    Property Sequense As Domain.Entities.GeneralLedgerSequence


    WriteOnly Property ActionsOnControls As Boolean

End Interface
