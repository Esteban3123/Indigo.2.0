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

Public Class INPACIENTTOPANURepository
    Inherits GenericRepository(Of INPACIENTTOPANU)
    Implements IINPACIENTTOPANURepository

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

    Public Function GetINPACIENTTOPANUByIPCODPACI(IPCODPACI As String, year As Integer) As INPACIENTTOPANU Implements IINPACIENTTOPANURepository.GetINPACIENTTOPANUByIPCODPACI
        Dim res = (From b In _crystalContext.INPACIENTTOPANU Where b.IPCODPACI = IPCODPACI AndAlso b.ANIO = year Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso res.Id > 0 Then
            Return res
        Else
            Return New INPACIENTTOPANU()
        End If
    End Function

End Class