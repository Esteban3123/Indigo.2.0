'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Daniel Eduardo Arévalo 
' Created          : 28-10-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ISupplierMaintenanceAdminService
    Inherits IDisposable
    ''' <summary>
    ''' funcion que sirve para lñistar todas los fabricantes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllSupplierMaintenance() As List(Of SupplierMaintenance)

    ''' <summary>
    ''' funcion que sirve para eliminar un fabricante
    ''' </summary>
    ''' <param name="Supplier"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteSupplierMaintenance(ByVal Supplier As SupplierMaintenance, ByVal session As SessionValues) As ActionResult

    ''' <summary>
    ''' funcion que sirve para guardar un fabricante
    ''' </summary>
    ''' <param name="Supplier"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveSupplierMaintenance(ByVal Supplier As SupplierMaintenance, ByVal session As SessionValues) As ActionResult(Of SupplierMaintenance)

    ''' <summary>
    ''' funciona que sirve para listar un fabricante
    ''' </summary>
    ''' <param name="Nit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSupplierMaintenance(ByVal Nit As String) As SupplierMaintenance

    ''' <summary>
    ''' funciona que sirve para listar un fabricante
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSupplierMaintenanceById(ByVal id As Integer) As SupplierMaintenance

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal session As SessionValues) As ActionResult(Of SupplierMaintenance)

End Interface
