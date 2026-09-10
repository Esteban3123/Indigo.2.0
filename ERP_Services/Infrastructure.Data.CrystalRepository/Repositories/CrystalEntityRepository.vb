'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Jhossept K. Garay Rodriguez
' Created          : 19-03-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports System.Dynamic
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base

Public Class CrystalEntityRepository
    Inherits GenericRepository(Of INENTIDAD)
    Implements ICrystalEntityRepository


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

    Public Function GetEntityByNit(nit As String) As INENTIDAD Implements ICrystalEntityRepository.GetEntityByNit
        Dim nitCrystal As String = Utils.StringPad(nit.Trim(), 15, 0, Utils.PadType.STR_PAD_LEFT)
        Dim res = (From ec In _crystalContext.INENTIDAD.AsNoTracking() Where ec.CODIGONIT = nitCrystal Select ec).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        End If
        Return New INENTIDAD
    End Function

    

    ''' <summary>
    ''' metodo para obtener una entidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetEntityByCode(code As String) As INENTIDAD Implements ICrystalEntityRepository.GetEntityByCode
        Dim codeCrystal As String = Utils.StringPad(code.Trim(), 9, 0, Utils.PadType.STR_PAD_LEFT)
        Dim res = (From e In _crystalContext.INENTIDAD.AsNoTracking() Where e.CODENTIDA = codeCrystal Select e).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        End If
        Return New INENTIDAD
    End Function
End Class
