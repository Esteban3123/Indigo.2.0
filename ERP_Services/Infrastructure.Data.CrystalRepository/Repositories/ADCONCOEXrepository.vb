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

Public Class ADCONCOEXrepository
    Inherits GenericRepository(Of ADCONCOEX)
    Implements IADCONCOEXrepository

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

    Public Function GetADCONCOEXByConsecutive(consecutive As Decimal) As ADCONCOEX Implements IADCONCOEXrepository.GetADCONCOEXByConsecutive
        Dim res = (From b In _crystalContext.ADCONCOEX Where b.CODCONCEC = consecutive Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso res.CODCONCEC > 0 Then
            Return res
        Else
            Return New ADCONCOEX()
        End If
    End Function

    Public Function GetADCONCOEXByAdmissionAndIPCODPACIAndESTADO(admissionNumber As String, ipcodpaci As String, estado As Integer) As ADCONCOEX Implements IADCONCOEXrepository.GetADCONCOEXByAdmissionAndIPCODPACIAndESTADO
        Return (From b In _crystalContext.ADCONCOEX.AsNoTracking() Where b.NUMINGRES = admissionNumber AndAlso b.IPCODPACI = ipcodpaci AndAlso b.CONESTADO = estado Select b).FirstOrDefault()
    End Function
End Class
