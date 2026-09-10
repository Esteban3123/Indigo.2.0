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

Public Class HCINTESERRepository
    Inherits GenericRepository(Of HCINTESER)
    Implements IHCINTESERRepository

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

    Public Function GetHCINTESERByCodserIpsAndCentAten(codserips As String, codCentAten As String) As HCINTESER Implements IHCINTESERRepository.GetHCINTESERByCodserIpsAndCentAten
        Return (From b In _crystalContext.HCINTESER.AsNoTracking() Where b.CODSERIPS = codserips AndAlso b.CODCENATE = codCentAten Select b).FirstOrDefault()
    End Function
End Class
