'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Diego Andrés Roldán
' Created          : 24-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class HCQXEQUIPRepository
    Inherits GenericRepository(Of HCQXEQUIP)
    Implements IHCQXEQUIPRepository

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

    Public Function GetHCQXEQUIPByAdmissionNumberAndNumefolio(admissionNumber As String, numefolio As String) As List(Of HCQXEQUIP) Implements IHCQXEQUIPRepository.GetHCQXEQUIPByAdmissionNumberAndNumefolio
        Return (From b In _crystalContext.HCQXEQUIP.AsNoTracking() Where b.NUMINGRES = admissionNumber AndAlso b.NUMEFOLIO = numefolio Select b).ToList()
    End Function

End Class