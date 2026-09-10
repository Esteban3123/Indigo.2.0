'***********************************************************************
' Assembly         : Domain.Security
' Author           : Jhon Tovar
' Created          : 10-03-2022
'
' Last Modified By :
' Last Modified On :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Domain.Base
Imports System.Dynamic
#End Region

Public Interface IProductCatalogRepository

    ''' <summary>
    ''' Guardar
    ''' </summary>
    ''' <param name="productCatalog"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function SaveProductCatalog(ByVal productCatalog As ProductCatalog, companyCode As String) As Boolean

    ''' <summary>
    ''' Actualizar
    ''' </summary>
    ''' <param name="productCatalog"></param>
    ''' <param name="companyCode"></param>
    ''' <returns></returns>
    Function UpdateProductCatalog(ByVal productCatalog As ProductCatalog, companyCode As String) As Boolean

    ''' <summary>
    ''' Consulta un ProductCatalog
    ''' </summary>
    ''' <param name="IdProductCatalog">el codigo del rol.</param>
    ''' <returns></returns>
    Function GetProductCatalog(ByVal IdProductCatalog As Integer, companyCode As String) As ProductCatalog

    ''' <summary>
    ''' elimina el ProductCatalog
    ''' </summary>
    ''' <returns></returns>
    Function DeleteProductCatalog(ByVal IdProductCatalog As Integer, companyCode As String) As Boolean

    ''' <summary>
    ''' Cambia estado el ProductCatalog
    ''' </summary>
    ''' <returns></returns>
    Function ChangeStateProductCatalog(ByVal IdProductCatalog As Integer, ByVal state As Byte, companyCode As String) As Boolean

    ''' <summary>
    ''' Consulta listado de ProductCatalog
    ''' </summary>
    ''' <returns></returns>
    Function ListProductCatalog() As List(Of ProductCatalog)

End Interface
