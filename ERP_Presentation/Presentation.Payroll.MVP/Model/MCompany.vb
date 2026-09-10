'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 25-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
'Imports Domain.Entities
Imports Presentation.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Esta clase tiene el modelo del patron MVP implementado en las empresas
''' </summary>
Public Class MCompany
    Inherits ModelBase
    Implements IDisposable

    Public Shared TAG As String = "525"

    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub


#Region "Properties"

    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtener una empresa por su codigo
    ''' </summary>
    ''' <param name="code">El codigo de la empresa.</param>
    ''' <returns>La Profesion</returns>
    Public Function GetCompany(ByVal code As String) As Object
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetCompany(code, Indigo)
    End Function
    ''' <summary>
    ''' Obtener una empresa por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la empresa.</param>
    ''' <returns>La Profesion</returns>
    Public Async Function GetCompanyAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetCompanyAsync(code, Indigo)
    End Function
    ''' <summary>
    ''' Graba la empresa
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo la empresa</returns>
    Public Function SaveCompany(ByVal reg As Object) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveCompany(reg, Indigo)
    End Function
    ''' <summary>
    ''' Graba la empresa modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo la empresa</returns>
    Public Async Function SaveCompanyAsync(ByVal reg As Object) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveCompanyAsync(reg, Indigo)
    End Function
    ''' <summary>
    ''' Objeto tercero
    ''' </summary>
    ''' <param name="nit">nit de busqueda</param>
    ''' <returns>Un Objeto tipo tercero</returns>
    ''' <remarks></remarks>
    Public Async Function GetThirdPartyAsync(ByVal nit As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetThirdPartyByNitAsync(nit, Indigo)
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetThirdPartyByIdAsync(nit, Indigo)
    End Function

    ''' <summary>
    ''' Elimina la empresa
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito la empresa</returns>
    Public Function DeleteCompany(ByVal reg As Object) As ActionMessageResult(Of Company)
        Try
            Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteCompany(reg, Indigo)
        Catch ex As Exception

        End Try
    End Function
    ''' <summary>
    ''' Elimina la empresa modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito la empresa</returns>
    Public Async Function DeleteCompanyAsync(ByVal reg As Object) As Task(Of ActionMessageResult(Of Company))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteCompanyAsync(reg, Indigo)
    End Function
    ''' <summary>
    ''' Lista las empresas
    ''' </summary>
    Public Function ListAllCompany() As List(Of Domain.Payroll.Entities.Company)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllCompany(Indigo)
    End Function


    ''' <summary>
    ''' Lista las empresas
    ''' </summary>
    Public Async Function ListAllCompanyAsync() As Task(Of List(Of Domain.Payroll.Entities.Company))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllCompanyAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista los terceros
    ''' </summary>
    Public Function ListAllCustomer() As List(Of Domain.Entities.Customer)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListCustomerAll(Indigo)
    End Function
    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Company", Indigo)
    End Function
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
