Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class HCPLAOTRPROCUPSRepository
    Inherits GenericRepository(Of HCPLAOTRPROCUPS)
    Implements IHCPLAOTRPROCUPSRepository

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

    Public Function GetHCPLAOTRPROCUPSByID(id As Integer) As HCPLAOTRPROCUPS Implements IHCPLAOTRPROCUPSRepository.GetHCPLAOTRPROCUPSByID
        Dim res = (From b In _crystalContext.HCPLAOTRPROCUPS Where b.ID = id Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso res.ID > 0 Then
            Return res
        Else
            Return New HCPLAOTRPROCUPS()
        End If
    End Function

End Class
