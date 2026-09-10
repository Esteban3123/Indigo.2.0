Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Infrastructure.Data.Base
Imports Domain.Crystal

Public Class SEGpermirRepository
    Inherits GenericRepository(Of SEGpermir)
    Implements ISEGpermirRepository

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
    ''' <param name="codigoRol"></param>
    ''' <returns></returns>
    Public Function GetPermisoRol(idMenu As String, codigoRol As String) As ActionResult(Of SEGpermir) Implements ISEGpermirRepository.GetPermisoRol
        Dim segp = (From e In _crystalContext.SEGpermir Where e.indidmenu = idMenu AndAlso e.codigorol = codigoRol).FirstOrDefault()
        Return New ActionResult(Of SEGpermir) With {.StateResult = True, .ObjectEmbbeded = segp}
    End Function

#End Region

End Class
