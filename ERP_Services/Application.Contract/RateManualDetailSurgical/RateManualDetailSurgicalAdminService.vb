'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 24/11/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources

Public Class RateManualDetailSurgicalAdminService
    Implements IRateManualDetailSurgicalAdminService


#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _rateManualDetailSurgicalRepository As IRateManualDetailSurgicalRepository
    
#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(rateManualDetailSurgicalRepository As IRateManualDetailSurgicalRepository)
        If rateManualDetailSurgicalRepository Is Nothing Then
            Throw New ArgumentNullException("rateManualDetailSurgicalRepository Vacio")
        End If
        _rateManualDetailSurgicalRepository = rateManualDetailSurgicalRepository
    End Sub

#End Region


    ''' <summary>
    ''' metodo para obtener un detalla del manual de tarifas quirurgico
    ''' </summary>
    ''' <param name="rateManualId"></param>
    ''' <param name="ipsService"></param>
    ''' <param name="surgicalGrouopId"></param>
    ''' <param name="UVRNumber"></param>
    ''' <param name="serviceManual"></param>
    ''' <returns></returns>
    Public Function GetSurgicalDetailServiceOrder(rateManualId As Integer, ipsService As Integer, surgicalGrouopId As Integer?, UVRNumber As Integer?, serviceManual As Integer) As RateManualDetailSurgical Implements IRateManualDetailSurgicalAdminService.GetSurgicalDetailServiceOrder
        Try
            Return _rateManualDetailSurgicalRepository.GetSurgicalDetailServiceOrder(rateManualId, ipsService, surgicalGrouopId, UVRNumber, serviceManual)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New RateManualDetailSurgical
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _rateManualDetailSurgicalRepository = Nothing
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
