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

Public Class INCUPSIPSRepository
    Inherits GenericRepository(Of INCUPSIPS)
    Implements IINCUPSIPSRepository

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

    Public Function GetINCUPSIPSByCODSERIPS(CODSERIPS As String) As INCUPSIPS Implements IINCUPSIPSRepository.GetINCUPSIPSByCODSERIPS
        Dim res = (From b In _crystalContext.INCUPSIPS Where b.CODSERIPS = CODSERIPS Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso Not String.IsNullOrEmpty(res.CODSERIPS) Then
            Return res
        Else
            Return New INCUPSIPS()
        End If
    End Function

End Class