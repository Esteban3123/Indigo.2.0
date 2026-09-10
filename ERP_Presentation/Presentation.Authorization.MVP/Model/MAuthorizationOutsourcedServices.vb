'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/07/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent

#End Region

Public Class MAuthorizationOutsourcedServices
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un registro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAuthorizationOutsourcedServices(ByVal code As String) As Task(Of ActionResult(Of AuthorizationOutsourcedServices))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.GetAuthorizationOutsourcedServicesAsync(code)
    End Function

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAuthorizationOutsourcedServicesById(ByVal id As Integer) As Task(Of ActionResult(Of AuthorizationOutsourcedServices))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.GetAuthorizationOutsourcedServicesByIdAsync(id)
    End Function

    ''' <summary>
    ''' Guarda o actualiza 
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAuthorizationOutsourcedServices(ByVal record As AuthorizationOutsourcedServices, ByVal idSequense As Int64) As Task(Of ActionResult(Of AuthorizationOutsourcedServices))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.SaveAuthorizationOutsourcedServicesAsync(record, Indigo.AuditMessageWcf, idSequense)
    End Function

    ''' <summary>
    ''' Obtiene un ingreso plano por su numero
    ''' </summary>
    Public Function GetAdmissionByServiceOrder(ByVal admissionNumber As String) As Object
        Dim res = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetAdmissionByServiceOrder(admissionNumber)
        If res IsNot Nothing AndAlso Not res.Trim().Equals(String.Empty) Then
            Return Utils.DeserializeJsonToObject(res)
        Else
            Return Nothing
        End If
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
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

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
