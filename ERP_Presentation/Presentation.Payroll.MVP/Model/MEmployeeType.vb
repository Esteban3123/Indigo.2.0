'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 25-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports Domain.Base.Entities

#End Region
''' <summary>
''' Realiza la conexion con los servicios del Tipo de empleado
''' </summary>
Public Class MEmployeeType
    Inherits ModelBase
    Implements IDisposable

    Public Shared TAG As String = "579"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub


#Region "Properties"
    Dim Indigo As SessionValues = SessionValues.Instance
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el Tipo de empleado de acuerdo al codigo de manera asincrona
    ''' </summary>
    ''' <param name="code">Codigo del Tipo de empleado</param>
    ''' <returns>El Tipo de empleado</returns>
    Public Async Function GetEmployeeTypeAsync(ByVal code As String) As Task(Of EmployeeType)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEmployeeTypeAsync(code, Indigo)
    End Function


    ''' <summary>
    ''' Guarda los cambios del Tipo de empleado asincrono
    ''' </summary>
    ''' <param name="EmployeeType">lenguaje</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SaveEmployeeTypeAsync(ByVal EmployeeType As Object) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveEmployeeTypeAsync(EmployeeType, Indigo)
    End Function

    ''' <summary>
    ''' Borra el Tipo de empleado asincrono
    ''' </summary>
    ''' <param name="EmployeeType">lenguaje</param>
    ''' <returns>Si se realizo o no el borrado</returns>
    Public Async Function DeleteEmployeeTypeAsync(ByVal EmployeeType As Object) As Task(Of ActionMessageResult(Of EmployeeType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteEmployeeTypeAsync(EmployeeType, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene los campos nulos 
    ''' </summary>
    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("EmployeeType", Indigo)
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
