'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 06-06-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IPackageRepository
    Inherits IRepository(Of Package)

    ''' <summary>
    ''' Obtiene todos los paquetes
    ''' </summary>
    ''' <returns>Lista de Monedas</returns>
    ''' <remarks></remarks>
    Function ListAllPackage() As List(Of Package)

    ''' <summary>
    ''' Lista todos los paquetes duplicados
    ''' </summary>
    ''' <param name="packageId"></param>
    ''' <param name="packageDetailTmp"></param>
    ''' <returns></returns>
    Function ListDuplicatePackage(packageId As Integer, packageDetailTmp As List(Of Tuple(Of Byte, Integer))) As List(Of PackageDto)

    ''' <summary>
    ''' Obtiene un paquete por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetPackage(code As String, Optional tracking As Boolean = True) As Package

    ''' <summary>
    ''' Obtiene un paquete por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetPackageById(id As String, Optional tracking As Boolean = True) As Package

    ''' <summary>
    ''' Sp que guarda los paquetes
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_SavePackage(xml As String, userCode As String) As SP_SavePackage_Result

    ''' <summary>
    ''' Obtiene un paquete asociado a un proceso de producción
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetProductionPackage(code As String, Optional tracking As Boolean = True) As Boolean
End Interface
