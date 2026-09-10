'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 28-10-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Maintenance.Entities
#End Region

Public Interface ISupplierMaintenanceRepository
    Inherits IRepository(Of SupplierMaintenance)

    ''' <summary>
    ''' funcion que lista todas los fabricantes
    ''' </summary>
    ''' <returns>Lista de fabricantes</returns>
    Function ListAllSupplier() As List(Of SupplierMaintenance)
    ''' <summary>
    ''' consulta para retornar un fabricante teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="Nit">el codigo del fabricante</param>
    ''' <returns>Objeto fabricanre</returns>
    Function GetSupplier(ByVal Nit As String, Optional tracking As Boolean = True) As SupplierMaintenance
    
    ''' <summary>
    ''' consulta para retornar un fabricante teniendo en cuenta el codigo
    ''' </summary>
    ''' <returns>Objeto fabricanre</returns>
    Function GetSupplierById(ByVal Id As Integer, Optional tracking As Boolean = True) As SupplierMaintenance
   

End Interface
