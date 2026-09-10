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

Public Class IHLISTPRORepository
    Inherits GenericRepository(Of IHLISTPRO)
    Implements IIHLISTPRORepository

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

    Public Function GetIHLISTPROByCode(code As String) As IHLISTPRO Implements IIHLISTPRORepository.GetIHLISTPROByCode
        Return (From e In _crystalContext.IHLISTPRO.AsNoTracking() Where e.CODPRODUC = code Select e).FirstOrDefault()
    End Function


    Public Function GetIHLISTPROwithTrackingByCode(code As String) As IHLISTPRO Implements IIHLISTPRORepository.GetIHLISTPROwithTrackingByCode
        Return (From e In _crystalContext.IHLISTPRO Where e.CODPRODUC = code Select e).FirstOrDefault()
    End Function

End Class
