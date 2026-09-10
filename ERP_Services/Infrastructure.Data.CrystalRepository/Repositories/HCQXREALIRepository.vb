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

Public Class HCQXREALIRepository
    Inherits GenericRepository(Of HCQXREALI)
    Implements IHCQXREALIRepository

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

    Public Function GetHCQXREALIByAdmissionNumberAndNumeFolioAndCodserips(admissionNumber As String, numefolio As String, codserips As String, Optional CONSECUQX As Integer? = Nothing) As HCQXREALI Implements IHCQXREALIRepository.GetHCQXREALIByAdmissionNumberAndNumeFolioAndCodserips
        Return (From b In _crystalContext.HCQXREALI.AsNoTracking().Include("HCQXVIABO").AsNoTracking() Where b.NUMINGRES = admissionNumber _
                                                                                                           AndAlso b.NUMEFOLIO = numefolio AndAlso b.CODSERIPS = codserips _
                                                                                                           AndAlso (CONSECUQX Is Nothing OrElse CONSECUQX = b.CONSECUQX)
                Select b).FirstOrDefault()
    End Function
End Class