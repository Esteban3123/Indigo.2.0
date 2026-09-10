'************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Interfaz del repositorio de Movimientos Devoluciones.
''' </summary>
Public Interface IMovementDevolutionsRepository
    Inherits IRepository(Of GlosaMovementDevolutions)

    ''' <summary>
    ''' Lista todos los Movimientos de Devoluciones.
    ''' </summary>
    ''' <returns>Lista de Movimiento de Devoluciones</returns>
    Function ListAllMovementGlosaDevolutions() As List(Of GlosaMovementDevolutions)
    ''' <summary>
    ''' Obtiene un Movimiento Devolucion especifico.
    ''' </summary>
    ''' <param name="Invoice">Numero de factura</param>
    ''' <param name="IdDevolutionD">Id Devolución Detalle</param>
    ''' <returns>Objeto Movimiento Devolucion</returns>
    Function GetMovementDevolutionByInvoiceNumber(ByVal Invoice As String, IdDevolutionD As String) As GlosaMovementDevolutions

End Interface