'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 28-10-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
#End Region

Public Interface IServiceOrder
    Inherits IcrudBase
    ''' <summary>
    ''' Codigo de la orden de servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String
    ''' <summary>
    ''' numero del ingreso del paciente, esto se saca de la tabla ADINGRESO de Crystal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property AdmissionNumber As String
    ''' <summary>
    ''' Fecha de la orden de servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OrderDate As Date?

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
    Property Sequense As Domain.Entities.BillingSequence

    ''' <summary>
    ''' Bnadera que establece si el sistema es impuesto incluido o no
    ''' </summary>
    WriteOnly Property FlagTaxInclude As Boolean
End Interface
