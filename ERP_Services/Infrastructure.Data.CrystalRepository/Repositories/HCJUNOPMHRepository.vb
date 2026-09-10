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

Public Class HCJUNOPMHRepository
    Inherits GenericRepository(Of HCJUNOPMH)
    Implements IHCJUNOPMHRepository

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

    Public Function GetHCJUNOPMHByCODPRONOPAndNUMINGRES(CODPRONOPList As List(Of String), AdmissionCode As String) As List(Of HCJUNOPMH) Implements IHCJUNOPMHRepository.GetHCJUNOPMHByCODPRONOPAndNUMINGRES
        Return (From b In _crystalContext.HCJUNOPMH Where CODPRONOPList.Contains(b.CODPRONOP) AndAlso b.NUMINGRES.Equals(AdmissionCode) Select b).ToList()
    End Function

    Public Function GetHCJUNOPMH(ListProductATC As List(Of ProductATC), AdmissionCode As String) As List(Of ProductATC) Implements IHCJUNOPMHRepository.GetHCJUNOPMH
        For Each i As ProductATC In ListProductATC
            Dim r = (From b In _crystalContext.HCJUNOPMH.AsNoTracking() Where i.ATCCodeNoPOS.Equals(b.CODPRONOP) AndAlso b.NUMINGRES.Equals(AdmissionCode) Select b).FirstOrDefault()
            If r IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.CODCONCEC) Then
                i.ATCCodePOS = r.CODPRODUC
            Else
                i.ATCCodeNoPOS = ""
            End If
        Next
        Return ListProductATC
    End Function

End Class