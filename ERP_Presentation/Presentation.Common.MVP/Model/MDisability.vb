'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 04-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports Domain.Base.Entities
#End Region

''' <summary>
''' Realiza la conexion con los servicios
''' </summary>
Public Class MDisability
    Inherits ModelBase
    Implements IDisposable

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="TAG">Tag del formulario</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal TAG As String)
        MyBase.New(TAG)
    End Sub

#Region "Properties"
    Dim Indigo As SessionValues = SessionValues.Instance
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la discapacidad de acuerdo al codigo 
    ''' </summary>
    ''' <param name="code">Codigo dla discapacidad</param>
    ''' <returns>discapacidad</returns>
    Public Async Function GetDisabilityAsync(ByVal code As String) As Task(Of Disability)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetDisabilityAsync(code, Indigo)
    End Function


    ''' <summary>
    ''' Guarda los cambios la discapacidad asincrono
    ''' </summary>
    ''' <param name="Disability">discapacidad</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SaveDisabilityAsync(ByVal disability As Disability) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveDisabilityAsync(disability, Indigo)
    End Function


    ''' <summary>
    ''' Borra discapacidad asincrono
    ''' </summary>
    ''' <param name="Disability">discapacidad</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function DeleteDisabilityAsync(ByVal disability As Disability) As Task(Of ActionMessageResult(Of Disability))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteDisabilityAsync(disability, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el listado de tipos de pensionado asincrono
    ''' </summary>
    ''' <returns>Listado de discapacidades</returns>
    Public Async Function ListAllDisabilityAsync() As Task(Of List(Of Disability))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllDisabilityAsync(Indigo)
    End Function


    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Async Function GetFieldsNULLAsync() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("Disability", Me.Indigo)
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
