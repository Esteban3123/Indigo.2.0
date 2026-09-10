Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Infrastructure.Data.Base

Public Class SEGrolesuRepository
    Inherits GenericRepository(Of SEGrolesu)
    Implements ISEGrolesuRepository

#Region "Builder"

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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Funcion que retorna un objeto tipo rol de crystal
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRolCrystal(code As String) As SEGrolesu Implements ISEGrolesuRepository.GetRolCrystal
        Return (From e In _crystalContext.SEGrolesu Where e.codigorol = code).FirstOrDefault()
    End Function

#End Region

End Class