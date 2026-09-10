'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 09-04-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface IPackagingUnitRepository
    Inherits IRepository(Of PackagingUnit)

    ''' <summary>
    ''' obtiene una unidad de paquete por codigo
    ''' </summary>
    ''' <param name="code">codigo de la unidad de paquete</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPackagingUnitByCode(code As String) As PackagingUnit

    ''' <summary>
    ''' obtiene una unidad de paquete por id
    ''' </summary>
    ''' <param name="id">id de la unidad de paquete</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPackagingUnitById(id As Integer) As PackagingUnit

End Interface
