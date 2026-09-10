Imports Domain.Base.Entities
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Infrastructure.Data.Base

Public Class INCUPSSUBRepository
    Inherits GenericRepository(Of INCUPSSUB)
    Implements IINCUPSSUBRepository

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
    ''' <returns></returns>
    Public Function GetSubCupsImaging() As ActionResult(Of List(Of INCUPSSUB)) Implements IINCUPSSUBRepository.GetSubCupsImaging
        Dim list = (From g In _crystalContext.INCUPSGRU
                    Join s In _crystalContext.INCUPSSUB
                        On g.CODGRUIPS Equals s.CODGRUIPS
                    Where g.GRUPOIMAG = 1 AndAlso s.MDASHIMAG = 1
                    Select s)
        Return New ActionResult(Of List(Of INCUPSSUB)) With {.StateResult = True, .ObjectEmbbeded = list.ToList}
    End Function

#End Region

End Class
