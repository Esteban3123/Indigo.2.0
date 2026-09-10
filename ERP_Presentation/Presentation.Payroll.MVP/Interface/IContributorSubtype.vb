'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 06-02-2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IContributorSubtype
    Inherits ICrudBase

#Region "Fields"

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

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
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequence As Domain.Entities.PayrollSequence

#End Region

#Region "Properties"

    ''' <summary>
    ''' Codigo del subtipo de cotizante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Nombre del subtipo de cotizante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Name As String

#End Region



End Interface
