'************************************************************
' Assembly         : Infraestructure.Data.GlosasRepository
' Author           : Juan Diego Diaz
' Created          : 08-07-2013
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio Movimiento Devoluciones
''' </summary>
Public Class MovementDevolutionsRepository
    Inherits GenericRepository(Of GlosaMovementDevolutions)
    Implements IMovementDevolutionsRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Obtiene un Movimiento Devolucion especifico.
    ''' </summary>
    ''' <param name="Invoice">Numero de factura</param>
    ''' <param name="IdDevolutionD">Id Devolución Detalle</param>
    ''' <returns>Objeto Movimiento Devolucion</returns>
    Public Function GetMovementDevolutionByInvoiceNumber(Invoice As String, IdDevolutionD As String) As GlosaMovementDevolutions Implements IMovementDevolutionsRepository.GetMovementDevolutionByInvoiceNumber
        Dim MovementDevolution = From e In _context.GlosaMovementDevolutions
                                 Where e.InvoiceNumber = Invoice And e.IdDevolutionsReceptionD = CInt(IdDevolutionD)
                                 Select e
        If MovementDevolution.Count > 0 Then
            Dim MovementDevolutionData = MovementDevolution.FirstOrDefault
            MovementDevolutionData.OriginalValue = (From e In _context.GlosaMovementDevolutions.AsNoTracking
                                 Where e.InvoiceNumber = Invoice And e.IdDevolutionsReceptionD = CInt(IdDevolutionD)
                                 Select e).FirstOrDefault
            Return MovementDevolutionData
        Else
            Return New GlosaMovementDevolutions
        End If
    End Function

    ''' <summary>
    ''' Lista todos los Movimientos de Devoluciones.
    ''' </summary>
    ''' <returns>Lista de Movimiento de Devoluciones</returns>
    Public Function ListAllMovementGlosaDevolutions() As List(Of GlosaMovementDevolutions) Implements IMovementDevolutionsRepository.ListAllMovementGlosaDevolutions
        Dim Busqueda = From e In _context.GlosaMovementDevolutions
                     Select e

        Return Busqueda.ToList
    End Function
End Class
