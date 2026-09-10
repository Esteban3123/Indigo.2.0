Imports Domain.Base.Entities
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Infrastructure.Data.Base

Public Class SEGpermiuRepository
    Inherits GenericRepository(Of SEGpermiu)
    Implements ISEGpermiuRepository

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
    ''' 
    ''' </summary>
    ''' <param name="idMenu"></param>
    ''' <param name="codigoUsuario"></param>
    ''' <returns></returns>
    Public Function GetPermisoUsuario(idMenu As String, codigoUsuario As String) As ActionResult(Of SEGpermiu) Implements ISEGpermiuRepository.GetPermisoUsuario
        Dim segp = (From e In _crystalContext.SEGpermiu Where e.indidmenu = idMenu AndAlso e.codusuari = codigoUsuario).FirstOrDefault()
        Return New ActionResult(Of SEGpermiu) With {.StateResult = True, .ObjectEmbbeded = segp}
    End Function

#End Region

End Class
