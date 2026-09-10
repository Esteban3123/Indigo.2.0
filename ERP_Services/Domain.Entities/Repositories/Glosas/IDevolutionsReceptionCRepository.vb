'************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz
' Created          : 12-06-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Interfaz del repositorio de Devolución Cabeceras.
''' </summary>
Public Interface IDevolutionsReceptionCRepository
    Inherits IRepository(Of GlosaDevolutionsReceptionC)

    ''' <summary>
    ''' Lista todas las cabeceras de devoluciones.
    ''' </summary>
    ''' <returns>Lista de objetos de devolución cabecera</returns>
    Function ListAllDevolutionC() As List(Of GlosaDevolutionsReceptionC)
    ''' <summary>
    ''' Obtiene una cabecera de devolución especifica.
    ''' </summary>
    ''' <param name="Id">El Id de la devolución cabecera</param>
    ''' <returns>Objeto Devolución Cabecera</returns>
    Function GetDevolutionC(ByVal Id As String) As GlosaDevolutionsReceptionC
    ''' <summary>
    ''' Obtiene una cabecera de devolución especifica.
    ''' </summary>
    ''' <param name="Consecutive">Consecutivo de la devolución cabecera</param>
    ''' <returns>Objeto Devolución Cabecera</returns>
    Function GetDevolutionCByConsecutive(ByVal Consecutive As String) As GlosaDevolutionsReceptionC


End Interface
