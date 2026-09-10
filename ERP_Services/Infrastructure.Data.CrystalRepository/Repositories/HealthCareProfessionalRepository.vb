'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Jhossept K. Garay Rodriguez
' Created          : 11-02-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports System.Dynamic
Imports Infrastructure.CrossCutting.Resources
Public Class HealthCareProfessionalRepository
    Inherits GenericRepository(Of INPROFSAL)
    Implements IHealthCareProfessionalRepository

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
    ''' Obtener Profesional Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetProfessionalByCode(Code As String) As INPROFSAL Implements IHealthCareProfessionalRepository.GetProfessionalByCode
        Dim query = From e In _crystalContext.INPROFSAL
                    Where e.CODPROSAL = Code
                    Select e

        If query.Count > 0 Then
            If query.FirstOrDefault.CODESPEC1 IsNot Nothing Then
                Dim specialty = (From esp In _crystalContext.INESPECIA.AsNoTracking Where esp.CODESPECI = query.FirstOrDefault.CODESPEC1).FirstOrDefault
                query.FirstOrDefault.Specialty1Description = specialty.CODESPECI + " - " + specialty.DESESPECI
            End If
            If query.FirstOrDefault.CODESPEC2 IsNot Nothing Then
                Dim specialty = (From esp In _crystalContext.INESPECIA.AsNoTracking Where esp.CODESPECI = query.FirstOrDefault.CODESPEC2).FirstOrDefault
                query.FirstOrDefault.Specialty2Description = specialty.CODESPECI + " - " + specialty.DESESPECI
            End If
            If query.FirstOrDefault.CODESPEC3 IsNot Nothing Then
                Dim specialty = (From esp In _crystalContext.INESPECIA.AsNoTracking Where esp.CODESPECI = query.FirstOrDefault.CODESPEC3).FirstOrDefault
                query.FirstOrDefault.Specialty3Description = specialty.CODESPECI + " - " + specialty.DESESPECI
            End If

            Return query.SingleOrDefault
        Else
            Return New INPROFSAL
        End If
    End Function

    ''' <summary>
    ''' Obtener Especialidad Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSpecialityByCode(Code As String) As INESPECIA Implements IHealthCareProfessionalRepository.GetSpecialityByCode
        Dim query = From e In _crystalContext.INESPECIA.AsNoTracking
                    Where e.CODESPECI = Code
                    Select e

        If query.Count > 0 Then
            Return query.SingleOrDefault
        Else
            Return New INESPECIA
        End If
    End Function

    ''' <summary>
    ''' Obtener Usuario del HIS
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserHIS(Code As String) As SEGusuaru Implements IHealthCareProfessionalRepository.GetUserHIS
        Dim query = From e In _crystalContext.SEGusuaru.AsNoTracking
                    Where e.CODUSUARI = Code
                    Select e

        If query.Count > 0 Then
            Return query.SingleOrDefault
        Else
            Return New SEGusuaru
        End If
    End Function

End Class
