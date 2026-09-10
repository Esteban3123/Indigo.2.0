'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 29-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports System.Dynamic

Public Class SpecialityRepository
    Inherits GenericRepository(Of INESPECIA)
    Implements ISpecialityRepository

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

    Public Function GetRIASById(id As Integer) As RIAS Implements ISpecialityRepository.GetRIASById
        Return (From x In _crystalContext.RIAS Where x.ID = id Select x).FirstOrDefault()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSpecialityByCode(code As String) As INESPECIA Implements ISpecialityRepository.GetSpecialityByCode
        Dim query = (From e In _crystalContext.INESPECIA Where e.CODESPECI = code
                    Select e)
        If query.Count > 0 Then
            Return query.SingleOrDefault()
        Else
            Return New INESPECIA()
        End If
    End Function
End Class
