'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 07-07-2013
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
''' Realiza la conexion con los servicios del grupo
''' </summary>
Public Class MPosition
    Inherits ModelBase
    Implements IDisposable

    Sub New(tag As String)
        MyBase.New(tag)
    End Sub


#Region "Methods"

    ''' <summary>
    ''' Obtiene el cargo de acuerdo al codigo 
    ''' </summary>
    ''' <param name="code">Codigo del cargo</param>
    ''' <returns>cargo</returns>
    Public Async Function GetPositionAsync(ByVal code As String) As Task(Of Position)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetPositionAsync(code, Indigo)
    End Function


    ''' <summary>
    ''' Guarda los cambios del cargo asincrono
    ''' </summary>
    ''' <param name="Position">cargo</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SavePositionAsync(ByVal position As Position) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SavePositionAsync(position, Indigo)
    End Function


    ''' <summary>
    ''' Borra cargo asincrono
    ''' </summary>
    ''' <param name="Position">cargo</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function DeletePositionAsync(ByVal position As Position) As Task(Of ActionMessageResult(Of Position))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeletePositionAsync(position, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el listado de tipos de pensionado asincrono
    ''' </summary>
    ''' <returns>Listado de tipos de pensionado</returns>
    Public Async Function ListAllPositionAsync() As Task(Of List(Of Position))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllPositionAsync(Indigo)
    End Function


    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Position", Indigo)
    End Function

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStatePosition(Code As String, State As Boolean) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ChangeStatePositionAsync(Code, State, Indigo)
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
