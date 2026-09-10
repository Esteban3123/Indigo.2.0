'***********************************************************************
' Assembly         : Application.Contract
' Author           : Hector Rodriguez Rubiano
' Created          : 14/03/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Base
Imports Domain.Base.Entities
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class ImagingGroupAdminService
    Implements IImagingGroupAdminService


    #Region "Fields"
    Private _RISGRIMAGE As IRISGRIMAGERepository
    #End Region
    
    #Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal _RISGRIMAGERepository As IRISGRIMAGERepository)
        If _RISGRIMAGERepository Is Nothing Then
            Throw New ArgumentNullException("_RISGRIMAGERepository Vacio")
        End If
        _RISGRIMAGE = _RISGRIMAGERepository
    End Sub

#End Region

    Public Function GetImagingGroupActive() As ActionResult(Of List(Of RISGRIMAGE)) Implements IImagingGroupAdminService.GetImagingGroupActive
        Try
            Dim imagingGroups As List(Of RISGRIMAGE) = _RISGRIMAGE.GetImagingGroups()
            If imagingGroups IsNot Nothing AndAlso imagingGroups.Count > 0 AndAlso imagingGroups.Any(Function(ig) ig.ESTADO = True) Then
                 Return New ActionResult(Of List(Of RISGRIMAGE)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = imagingGroups.Where(Function(ig) ig.ESTADO = True).ToList()}
            Else
                Return New ActionResult(Of List(Of RISGRIMAGE)) With {.StatusCode = eStatusResult.WARNING, .Message = "No se encontraron productos"}
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of RISGRIMAGE)) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetImagingGroupById(id As Integer, audit As AuditMessage) As ActionResult(Of RISGRIMAGE) Implements IImagingGroupAdminService.GetImagingGroupById
        Try
            Dim imagingGroup As RISGRIMAGE = _RISGRIMAGE.GetImagingGroupById(id)
            If imagingGroup IsNot Nothing AndAlso imagingGroup.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of RISGRIMAGE)(imagingGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of RISGRIMAGE) With {.StateResult = True, .ObjectEmbbeded = imagingGroup}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RISGRIMAGE) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                '_RISGRIMAGE.Dispose()
            End If
            _RISGRIMAGE = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region




End Class
