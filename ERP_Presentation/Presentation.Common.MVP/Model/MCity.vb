'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Jose Luis Rojas
' Created          : 11-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base

#End Region

''' <summary>
''' Model de conexion con los servicios distribuidos de ciudades
''' </summary>
Public Class MCity
    Inherits ModelBase
    Implements IDisposable

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

    Public Shared TAG As String = "513"

#Region "Methods"

    ''' <summary>
    ''' Obtiene la ciudad por codigo
    ''' </summary>
    ''' <param name="code">Codigo</param>
    ''' <returns>Ciudad</returns>
    Public Function GetCity(ByVal code As String) As City
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetCity(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene la ciudad por codigo asincrono
    ''' </summary>
    ''' <param name="code">Codigo</param>
    ''' <returns>Ciudad</returns>
    Public Async Function GetCityAsync(ByVal code As String) As Task(Of City)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetCityAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene la ciudad por codigo y departamento
    ''' </summary>
    ''' <param name="code">Codigo</param>
    ''' <returns>Ciudad</returns>
    Public Function GetCityByDepartment(ByVal code As String, ByVal departmentCode As String) As City
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetCityByDepartment(code, departmentCode, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene la ciudad por codigo y departamento asincrono
    ''' </summary>
    ''' <param name="code">Codigo</param>
    ''' <returns>Ciudad</returns>
    Public Async Function GetCityByDepartmentAsync(ByVal code As String, ByVal departmentCode As String) As Task(Of City)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetCityByDepartmentAsync(code, departmentCode, Indigo)
    End Function

    ''' <summary>
    ''' Guarda la ciudad
    ''' </summary>
    ''' <param name="city">Ciudad</param>
    ''' <returns>Si se hizo o no</returns>
    Public Function SaveCity(ByVal city As City) As ActionResult(Of City)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveCity(city, Indigo)
    End Function

    ''' <summary>
    ''' Guarda la ciudad asincrono
    ''' </summary>
    ''' <param name="city">Ciudad</param>
    ''' <returns>Si se hizo o no</returns>
    Public Async Function SaveCityAsync(ByVal city As City) As Task(Of ActionResult(Of City))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveCityAsync(city, Indigo)
    End Function

    ''' <summary>
    ''' borra la ciudad
    ''' </summary>
    ''' <param name="city">Ciudad</param>
    ''' <returns>Si se hizo o no</returns>
    Public Function DeleteCity(ByVal city As City) As ActionMessageResult(Of City)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteCity(city, Indigo)
    End Function

    Public Function ListAllCityDepartment(ByVal IdDepartment As Integer) As List(Of City)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllCitiesByIdDepartment(IdDepartment, Indigo)
    End Function

    ''' <summary>
    ''' Metodo para obtener una ciudad por id
    ''' </summary>
    ''' <param name="IdCity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCityById(ByVal IdCity As Integer) As City
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetCityById(IdCity, Indigo)
    End Function

    ''' <summary>
    ''' Metodo para obtener una ciudad por id de modo asincrono
    ''' </summary>
    ''' <param name="IdCity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetCityByIdAsync(ByVal IdCity As Integer) As Task(Of City)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetCityByIdAsync(IdCity, Indigo)
    End Function

    ''' <summary>
    ''' borra la ciudad asincrono
    ''' </summary>
    ''' <param name="city">Ciudad</param>
    ''' <returns>Si se hizo o no</returns>
    Public Async Function DeleteCityAsync(ByVal city As City) As Task(Of ActionMessageResult(Of City))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteCityAsync(city, Indigo)
    End Function

    ''' <summary>
    ''' Lista las ciudades
    ''' </summary>
    ''' <returns>Lista de ciudades</returns>
    Public Function ListAllCities() As List(Of City)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllCity(Indigo)
    End Function

    ''' <summary>
    ''' Lista las ciudades asincrono
    ''' </summary>
    ''' <returns>Lista de ciudades</returns>
    Public Async Function ListAllCitiesAsync() As Task(Of List(Of City))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllCityAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista todos los campos nulos
    ''' </summary>
    ''' <returns>lista de paises</returns>
    Public Async Function GetFieldsNULLAsync() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("City", Me.Indigo)
    End Function

    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of City))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ChangeStateCityAsync(code, state, Me.Indigo)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
