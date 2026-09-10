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

Public Class AGCITASERIPSRepository
    'Inherits GenericRepository(Of AGCITASERIPS)
    'Implements IAGCITASERIPSRepository

    ''Contexto del repositorio de Indigo Vie Cloud Platform
    'Private _crystalContext As ICrystalModelUnitOfWork

    '''' <summary>
    '''' Inicializa una nueva instancia de la clase
    '''' </summary>
    '''' <param name="crystalContext">Contexto</param>
    'Public Sub New(ByVal crystalContext As ICrystalModelUnitOfWork)
    '    MyBase.New(crystalContext)
    '    Me._crystalContext = crystalContext
    'End Sub

    'Public Function GetAGCITASERIPSByID(id As Integer) As AGCITASERIPS Implements IAGCITASERIPSRepository.GetAGCITASERIPSByID
    '    Dim res = (From b In _crystalContext.AGCITASERIPS Where b.ID = id Select b).FirstOrDefault()
    '    If res IsNot Nothing AndAlso res.ID > 0 Then
    '        Return res
    '    Else
    '        Return New AGCITASERIPS()
    '    End If
    'End Function

    'Public Function GetAGCITASERIPSByIDCITA(idCita As Integer) As List(Of AGCITASERIPS) Implements IAGCITASERIPSRepository.GetAGCITASERIPSByIDCITA
    '    Return (From b In _crystalContext.AGCITASERIPS Where b.IDCITA = idCita Select b).ToList()
    'End Function
End Class