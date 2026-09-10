#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
#End Region


Public Class MaintenanceToolsRepository

    Inherits GenericRepository(Of MaintenanceTools)
    Implements IMaintenanceToolsRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="context"></param>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Función que obtiene un Tipo de Responsable
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetToolsTypeByCode(Code As String) As MaintenanceTools Implements IMaintenanceToolsRepository.GetToolsByCode

        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim res = (From d As MaintenanceTools In _context.MaintenanceTools.Include("MaintenanceToolsItemDetail").Include("MaintenanceToolsItemDetail.FixedAssetItem") Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            res.OriginalValue = (From d As MaintenanceTools In Me._context.MaintenanceTools.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault()
            Return res
        Else
            Return New MaintenanceTools()
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas las tipos de Responsables
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllTools() As List(Of MaintenanceTools) Implements IMaintenanceToolsRepository.ListAllTools
        Dim ListTools = From e In _context.MaintenanceTools
                        Select e

        If ListTools.Count() > 0 Then
            Return ListTools.ToList()
        Else
            Return New List(Of MaintenanceTools)
        End If
    End Function

    Public Function GetItemByCode(Id As String) As FixedAssetItem Implements IMaintenanceToolsRepository.GetItemByCode
        Dim ItemCatalog = From e In _context.FixedAssetItem
                          Where e.Id = Id
                          Select e

        If ItemCatalog IsNot Nothing Then
            Return ItemCatalog.FirstOrDefault()
        Else
            Return Nothing
        End If
    End Function

    Public Function GetItemByIdTool(IdTools As Integer) As List(Of MaintenanceToolsItemDetail) Implements IMaintenanceToolsRepository.GetItemByIdTool
        Dim ObjMaintenanceToolsItemDetail = From e In _context.MaintenanceToolsItemDetail.Include("FixedAssetItem")
                                            Where e.ItemAsset = IdTools
                                            Select e

        If ObjMaintenanceToolsItemDetail IsNot Nothing Then
            Return ObjMaintenanceToolsItemDetail.ToList()
        Else
            Return Nothing
        End If
    End Function
End Class
