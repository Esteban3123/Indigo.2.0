'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Johan Carranza
' Created          : 22-05-2019
'
' Last Modified By : Johan Carranza
' Last Modified On : 22-05-2019
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Security.Entities
Imports Presentation.Base
Imports Presentation.Security.MVP
Imports Infrastructure.Data.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.CrystalRepository

Public Class MReporteAuditoriaHC
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Public Indigo As SessionValues = SessionValues.Instance


#End Region


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If

            ' TODO: libere los recursos no administrados (objetos no administrados) y reemplace Finalize() a continuación.
            ' TODO: configure los campos grandes en nulos.
        End If
        disposedValue = True
    End Sub

    ' TODO: reemplace Finalize() solo si el anterior Dispose(disposing As Boolean) tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
        Dispose(True)
        ' TODO: quite la marca de comentario de la siguiente línea si Finalize() se ha reemplazado antes.
        ' GC.SuppressFinalize(Me)
    End Sub

    Public Function ListAllPatients()
        Return XpoServiceEx.Instance(Me.Indigo.HisContainer).CrystalService.ListAllPatients()
    End Function
#End Region

#Region "Metodos"

    Public Function GetPatientXpo(code As String) As PatientXpo
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetPatientXpo(code)
    End Function

    Public Function GetReporteAuditoria(FechaIn As String, FechaFin As String, CodUser As String, Paciente As String) As List(Of HCAUDITORIAXpo )

        Dim filtro As String = ""
        
        filtro = "FECHCONSU >= '" & FechaIn & "' AND FECHCONSU <= '" & FechaFin & "' AND INGRESO IS NOT NULL"
        'Si el codigo del usuario no viene vacio
        If CodUser <> ""
            filtro &= " AND  CODUSUCONS = '" & CodUser & "'"
        End If

        'S el codigo del paciente no viene vacio
        If Paciente <> ""
            filtro &= " AND  CODPACQCON = '" & Paciente & "'"
        End If

        Dim filterOrder As List(Of HCAUDITORIAXpo ) = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of HCAUDITORIAXpo)(Nothing, filtro)
        Return filterOrder
    End Function
    

#End Region

End Class
