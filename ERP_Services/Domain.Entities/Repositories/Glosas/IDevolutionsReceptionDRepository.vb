'************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz
' Created          : 13-06-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Interfaz del repositorio de Devolución Detalles.
''' </summary>
Public Interface IDevolutionsReceptionDRepository
    Inherits IRepository(Of GlosaDevolutionsReceptionD)

    ''' <summary>
    ''' Lista todos los detalles de devoluciones.
    ''' </summary>
    ''' <returns>Lista Devolución Detalle</returns>
    Function ListAllDevolutionD() As List(Of GlosaDevolutionsReceptionD)
    ''' <summary>
    ''' Obtiene detalles de devolución especificos.
    ''' </summary>
    ''' <param name="Id">El Id de la devolución cabecera</param>
    ''' <returns>Lista Devolución Detalle</returns>
    Function ListDevolutionDByIdDevolutionnC(ByVal Id As String) As List(Of GlosaDevolutionsReceptionD)
    ''' <summary>
    ''' Obtiene un detalle de devolución especifico.
    ''' </summary>
    ''' <param name="Id">El Id de la devolución detalle</param>
    ''' <returns>Objeto Devolución Detalle</returns>
    Function GetDevolutionDByIdDevolutionD(ByVal Id As String, Optional tracking As Boolean = True) As GlosaDevolutionsReceptionD
    ''' <summary>
    ''' Obtiene un detalle de devolución especifico.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDevolutionDById(Id As String) As GlosaDevolutionsReceptionD

End Interface
