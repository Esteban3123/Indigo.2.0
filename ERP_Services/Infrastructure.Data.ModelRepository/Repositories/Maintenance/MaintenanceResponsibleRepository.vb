#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
#End Region


Public Class MaintenanceResponsibleRepository

    Inherits GenericRepository(Of MaintenanceResponsible)
    Implements IMaintenanceResponsibleRepository

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
    Public Function GetResponsibleTypeByCode(Code As String) As MaintenanceResponsible Implements IMaintenanceResponsibleRepository.GetResponsibleByCode

        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim res = (From d As MaintenanceResponsible In _context.MaintenanceResponsible.Include("ResponsibleCatalogOfArticles").Include("ResponsibleCatalogOfArticles.FixedAssetItemCatalog") Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim ObjThirdParty = (From a In _context.ThirdParty.AsNoTracking Where a.Id = res.ThirdPartyId Select a).FirstOrDefault()
            Dim ObjVinculationType = (From b In _context.FixedAssetVinculationType.AsNoTracking Where b.Id = res.VinculationTypeId Select b).FirstOrDefault()
            Dim ObjResponsibleType = (From c In _context.ResponsibleType.AsNoTracking Where c.Id = res.ReponsibleTypeId Select c).FirstOrDefault()

            res.ThirdPartyName = ObjThirdParty.Name
            res.VinculationName = ObjVinculationType.Description
            res.ResponsibleTypeName = ObjResponsibleType.Description

            If res.ResponsibleRole.ToString IsNot Nothing Then
                Dim DescRol = (From p In _context.MaintenanceResponsible.AsNoTracking Where p.ResponsibleRole = res.ResponsibleRole Select p).FirstOrDefault
                Select Case res.ResponsibleRole
                    Case 1
                        DescRol.ResponsibleRoleName = "Administrador"

                    Case 2
                        DescRol.ResponsibleRoleName = "Encargado"

                    Case 3
                        DescRol.ResponsibleRoleName = "Coordinador"

                    Case 4
                        DescRol.ResponsibleRoleName = "Operario-Tecnico"

                    Case 5
                        DescRol.ResponsibleRoleName = "Externo"
                   
                End Select

            End If

            res.OriginalValue = (From d As MaintenanceResponsible In Me._context.MaintenanceResponsible.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault()
            Return res
        Else
            Return New MaintenanceResponsible()
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas las tipos de Responsables
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllResponsibleType() As List(Of MaintenanceResponsible) Implements IMaintenanceResponsibleRepository.ListAllResponsible
        Dim ListResponsible = From e In _context.MaintenanceResponsible
                              Select e

        If ListResponsible.Count() > 0 Then
            Return ListResponsible.ToList()
        Else
            Return New List(Of MaintenanceResponsible)
        End If
    End Function

    Public Function GetItemCatalogByCode(Id As String) As FixedAssetItemCatalog Implements IMaintenanceResponsibleRepository.GetItemCatalogByCode
        Dim ItemCatalog = From e In _context.FixedAssetItemCatalog
                          Where e.Id = Id
                          Select e

        If ItemCatalog IsNot Nothing Then
            Return ItemCatalog.FirstOrDefault()
        Else
            Return Nothing
        End If
    End Function

    Public Function GetItemCatalogByIdUser(IdResponsible As Integer) As List(Of ResponsibleCatalogOfArticles) Implements IMaintenanceResponsibleRepository.GetItemCatalogByIdUser
        Dim ObjResponsibleCatalogOfArticles = From e In _context.ResponsibleCatalogOfArticles.Include("FixedAssetItemCatalog")
                                              Where e.ResponsibleId = IdResponsible
                                              Select e

        If ObjResponsibleCatalogOfArticles IsNot Nothing Then
            Return ObjResponsibleCatalogOfArticles.ToList()
        Else
            Return Nothing
        End If
    End Function
End Class
