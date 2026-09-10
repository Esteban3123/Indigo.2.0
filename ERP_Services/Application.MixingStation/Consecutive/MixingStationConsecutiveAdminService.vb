'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Duván Albeiro Mejia Cortes
' Created          : 01-12-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region
Public Class MixingStationConsecutiveAdminService
    Implements IMixingStationConsecutiveAdminService, Inject

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly _mixingStationsConsecutiveRepository As IMixingStationConsecutiveRepository

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="mixingStationsConsecutiveRepository"></param>
    Public Sub New(mixingStationsConsecutiveRepository As IMixingStationSettingRepository)
        _mixingStationsConsecutiveRepository = mixingStationsConsecutiveRepository
    End Sub


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

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
