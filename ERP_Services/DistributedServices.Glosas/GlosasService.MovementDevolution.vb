'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Juan Diego Diaz
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base

#End Region

Partial Class GlosasService

#Region "GlosaMovementDevolutions"

    ''' <summary>
    ''' Funcion para eliminar un Movimiento de Devolución
    ''' </summary>
    ''' <param name="MovementDevolution">Movimiento Devolución</param>
    ''' <returns></returns>
    Public Function DeleteMovementDevolution(MovementDevolution As GlosaMovementDevolutions, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasMovementDevolutions.DeleteMovementDevolution
        Using MovementDevolutionsAdminservice As IMovementDevolutionsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementDevolutionsAdminService)()
            Return MovementDevolutionsAdminservice.DeleteMovementDevolution(MovementDevolution, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Movimiento Devolucion especifico.
    ''' </summary>
    ''' <param name="Invoice">Numero de factura</param>
    ''' <param name="IdDevolutionD">Id Devolución Detalle</param>
    ''' <returns>Objeto Movimiento Devolucion</returns>
    Public Function GetMovementDevolutionByInvoiceNumber(Invoice As String, IdDevolutionD As String, session As SessionValues) As GlosaMovementDevolutions Implements IGlosasMovementDevolutions.GetMovementDevolutionByInvoiceNumber
        Using MovementDevolutionsAdminservice As IMovementDevolutionsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementDevolutionsAdminService)()
            Return MovementDevolutionsAdminservice.GetMovementDevolutionByInvoiceNumber(Invoice, IdDevolutionD)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los Movimientos de Devoluciones.
    ''' </summary>
    ''' <returns>Lista de Movimiento de Devoluciones</returns>
    Public Function ListAllMovementGlosaDevolutions(session As SessionValues) As List(Of GlosaMovementDevolutions) Implements IGlosasMovementDevolutions.ListAllMovementGlosaDevolutions
        Using MovementDevolutionsAdminservice As IMovementDevolutionsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementDevolutionsAdminService)()
            Return MovementDevolutionsAdminservice.ListAllMovementGlosaDevolutions
        End Using
    End Function

    ''' <summary>
    ''' Funcion para Guardar un Movimiento de Devolución
    ''' </summary>
    ''' <param name="MovementDevolution">Movimineto Devolución</param>
    ''' <param name="session">Objeto session</param>
    Public Function SaveMovementDevolution(MovementDevolution As GlosaMovementDevolutions, session As SessionValues) As Domain.Base.Entities.ActionResult(Of GlosaMovementDevolutions) Implements IGlosasMovementDevolutions.SaveMovementDevolution
        Using MovementDevolutionsAdminservice As IMovementDevolutionsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementDevolutionsAdminService)()
            Return MovementDevolutionsAdminservice.SaveMovementDevolution(MovementDevolution, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para Confirmar un Movimiento de Devolución
    ''' </summary>
    ''' <param name="listDevolutionD">lista de facturas Devolución</param>
    ''' <param name="IndigoSessionValues">Objeto session</param>
    Public Function ConfirmDevolution(ByVal listDevolutionD As List(Of GlosaDevolutionsReceptionD), ByVal idSequence As Integer, ByVal Injustificate As Boolean, ByVal UserFreeInvoice As Boolean, IndigoSessionValues As SessionValues) As ActionResult Implements IGlosasMovementDevolutions.ConfirmDevolution
        Using MovementDevolutionsAdminservice As IMovementDevolutionsAdminService = IocFactory.Instance(IndigoSessionValues.TransactionalContainer).CurrentContainer.Resolve(Of IMovementDevolutionsAdminService)()
            Return MovementDevolutionsAdminservice.ConfirmDevolution(listDevolutionD, idSequence, Injustificate, UserFreeInvoice, IndigoSessionValues)
        End Using
    End Function

#End Region
End Class
