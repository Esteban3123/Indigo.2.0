'***********************************************************************
' Assembly         : Presentation.Controls.MVP
' Author           : Jorge Vernaza
' Created          : 16-10-2013
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importaciones"
Imports Presentation.CloudAgent
Imports Domain.Entities
#End Region
Public Class MCtrContactos
    Implements IDisposable


    Public Async Function GetPhoneTypesAsync() As Threading.Tasks.Task(Of List(Of PhoneType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllPhoneTypeAsync(Infrastructure.CrossCutting.Base.SessionValues.Instance)

    End Function

    Public Async Function GetPhoneTypesSecurity() As Threading.Tasks.Task(Of List(Of Domain.Security.Entities.PhoneType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListAllPhoneTypeAsync(Infrastructure.CrossCutting.Base.SessionValues.Instance)
    End Function

    Public Async Function ListAllDepartmentAsync() As Threading.Tasks.Task(Of List(Of Department))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllDepartmentAsync(Infrastructure.CrossCutting.Base.SessionValues.Instance)
    End Function

    Public Function ListAllCityDepartment(ByVal IdDepartment As Integer) As List(Of City)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllCitiesByIdDepartment(IdDepartment, Infrastructure.CrossCutting.Base.SessionValues.Instance)
    End Function

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
