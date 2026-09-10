'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 2015-03-25
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class BedRepository
    Inherits GenericRepository(Of CHCAMASHO)
    Implements IBedRepository

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
    ''' obtiene una cama por codigo
    ''' </summary>
    ''' <param name="bedCode"></param>
    ''' <returns></returns>
    Public Function GetBedbyCode(bedCode As Integer) As CHCAMASHO Implements IBedRepository.GetBedbyCode
        Dim res = (From b In _crystalContext.CHCAMASHO.Include("INUNIFUNC") Where b.CODICAMAS = bedCode Select b).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New CHCAMASHO
        End If
    End Function
End Class
