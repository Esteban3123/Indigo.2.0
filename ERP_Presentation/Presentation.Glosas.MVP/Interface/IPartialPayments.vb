'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Rafael Patiño
' Created          : 2014-10-10
'
' Last Modified By : Rafael Patiño
' Last Modified On : 2014-10-10
' Description      : Interface del frontal de pagos parciales
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base

#End Region

Public Interface IPartialPayments
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene o asigna el numero consecutivo de la conciliacion
    ''' </summary>
    ''' <value>Numero de la conciliacion</value>
    ''' <returns>Numero de la conciliacion</returns>
    Property Consecutive As Long
    ''' <summary>
    ''' Obtiene o asigna el nit de la entidad
    ''' </summary>
    ''' <value>Nit de la entidad</value>
    ''' <returns>Nit de la entidad</returns>
    Property Nit As String
    ''' <summary>
    ''' Obtiene o asigna la fecha en que se realiza la conciliacion
    ''' </summary>
    ''' <value>Fecha de la conciliacion</value>
    ''' <returns>Fecha de la conciliacion</returns>
    Property DateRadicate As DateTime
    ''' <summary>
    ''' Obtiene o asigna la fecha del oficio
    ''' </summary>
    ''' <value>Fecha del oficio</value>
    ''' <returns>Fecha del oficio</returns>
    Property DateDocument As DateTime
    ''' <summary>
    ''' Obtiene o asigna el comentario de observacion en la conciliacion
    ''' </summary>
    ''' <value>Comentario de observacion</value>
    ''' <returns>Comentario de observacion</returns>
    Property Comment As String

    ''' <summary>
    ''' estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property StatusDocument As String
End Interface
