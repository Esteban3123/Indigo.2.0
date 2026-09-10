'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 23-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
'Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Presentation.Base

#End Region

Public Class MHoliday
    Inherits ModelBase
    Implements IDisposable
    Private Indigo As SessionValues = SessionValues.Instance

    Shared TAG As String = "553"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        MyBase.New(TAG)
    End Sub

#Region "methods"
    ''' <summary>
    ''' Obtener un registro por sucodigo modo asincrono
    ''' </summary>
    ''' <param name="HoliDayDate">La fecha del registro.</param>
    ''' <returns>El registro</returns>
    Public Async Function GetHolidayAsync(ByVal HoliDayDate As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetHolidayAsync(HoliDayDate, Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtener un registro por su codigo
    ''' </summary>
    ''' <param name="HoliDayDate">La fecha del registro.</param>
    ''' <returns>El registro</returns>
    Public Function GetHoliday(ByVal HoliDayDate As String) As Object
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetHoliday(HoliDayDate, Me.Indigo)
    End Function


    ''' <summary>
    ''' Graba el registro Modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el registro</returns>
    Public Async Function SaveHolidayAsync(ByVal reg As Object) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveHolidayAsync(reg, Me.Indigo)
    End Function

    ''' <summary>
    ''' Graba el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el registro</returns>
    Public Function SaveHoliday(ByVal reg As Object) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveHoliday(reg, Me.Indigo)
    End Function

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el registro</returns>
    Public Function DeleteHoliday(ByVal reg As Object) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteHoliday(reg, Me.Indigo)
    End Function

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el registro</returns>
    Public Async Function DeleteHolidayAsync(ByVal reg As Object) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteHolidayAsync(reg, Me.Indigo)
    End Function

    ''' <summary>
    ''' Lista todos los Festivos modo asincrono
    ''' </summary>
    Public Async Function ListAllHolidaysAsync() As Task(Of List(Of Holiday))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllHolidaysAsync(Me.Indigo)
    End Function

    ''' <summary>
    ''' Lista todos los Festivos de un determinado año modo asincrono
    ''' </summary>
    Public Async Function ListAllHolidaysbyYearsAsync(ByVal year As Integer) As Task(Of List(Of Holiday))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListHolidaybyYearAsync(year, Me.Indigo)
    End Function

    ''' <summary>
    ''' Lista todos los Festivos de un determinado año
    ''' </summary>
    Public Function ListAllHolidaysbyYears(ByVal year As Integer) As List(Of Holiday)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListHolidaybyYear(year, Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtiene los festivos que estan dentro de un rango de fecha
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListHolidayBetweenDateAsync(ByVal initialDate As Date, ByVal endDate As Date) As Task(Of List(Of Holiday))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListHolidayBetweenDateAsync(initialDate, endDate, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene los festivos que estan dentro de un rango de fecha
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHolidayBetweenDate(ByVal initialDate As Date, ByVal endDate As Date) As List(Of Holiday)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListHolidayBetweenDate(initialDate, endDate, Indigo)
    End Function

    ' ''' <summary>
    ' ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ' ''' </summary>
    ' ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    'Public Function GetNullFields() As DataSet
    '    Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULL("Holiday")
    'End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
