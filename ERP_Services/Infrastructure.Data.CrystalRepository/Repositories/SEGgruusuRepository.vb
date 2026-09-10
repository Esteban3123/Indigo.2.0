Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Infrastructure.Data.Base

Public Class SEGgruusuRepository
    Inherits GenericRepository(Of SEGgruusu)
    Implements ISEGgruusuRepository

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
    ''' Funcion que retorna un objeto tipo grupo de crystal
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGrupoCrystal(code As String) As SEGgruusu Implements ISEGgruusuRepository.GetGrupoCrystal
        Return (From e In _crystalContext.SEGgruusu Where e.codgrupou = code).FirstOrDefault()
    End Function

#End Region

End Class