#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface ICPCCatalogRepository
    Inherits IRepository(Of CPCCatalog)

    ''' <summary>
    ''' consulta un CPCCatalog por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCPCCatalogById(Id As Integer) As CPCCatalog

    ''' <summary>
    ''' Obtiene un CPCCatalog por codigo 
    ''' </summary>
    '''<param name="Code">Código del Conceptos CPCCatalog</param>
    ''' <returns></returns>
    Function GetCPCCatalogByCode(Code As String) As CPCCatalog

End Interface
