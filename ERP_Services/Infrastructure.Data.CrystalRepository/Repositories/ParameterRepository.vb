'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Juan F. Tamayo
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports System.Dynamic

Public Class ParameterRepository
    Inherits GenericRepository(Of CHPARAMET)
    Implements IParameterRepository

    'Contexto del repositorio de Indigo Vie Cloud Platform
    Private _crystalContext As ICrystalModelUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="crystalContext">Contexto</param>
    Public Sub New(ByVal crystalContext As ICrystalModelUnitOfWork)
        MyBase.New(crystalContext)
        Me._crystalContext = crystalContext
    End Sub

    ''' <summary>
    ''' Obtiene los parámetros para un centro de atención
    ''' </summary>
    ''' <param name="attentionCenterCode">Código del centro de atención</param>
    ''' <param name="asNoTracking">Valor que indica si se consulta el parámetro con seguimiento</param>
    ''' <returns>Parámetros para el centro de atención</returns>
    Public Function GetParameterByAttentionCenter(attentionCenterCode As String, Optional asNoTracking As Boolean = True) As CHPARAMET Implements IParameterRepository.GetParameterByAttentionCenter
        If asNoTracking Then
            Dim res = (From para In Me._crystalContext.CHPARAMET.AsNoTracking() Where para.CODCENATE.Equals(attentionCenterCode) Select para).ToList()
            If res.Count > 0 Then
                Return res(0)
            Else
                Return New CHPARAMET()
            End If
        Else
            Dim res = (From para In Me._crystalContext.CHPARAMET Where para.CODCENATE.Equals(attentionCenterCode) Select para).ToList()
            If res.Count > 0 Then
                Return res(0)
            Else
                Return New CHPARAMET()
            End If
        End If
    End Function

End Class
