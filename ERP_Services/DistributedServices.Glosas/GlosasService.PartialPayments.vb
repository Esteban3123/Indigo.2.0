'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 12-04-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 2013-04-21
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


#Region "PartialPaymentsC"
    ''' <summary>
    ''' confirmar pago parcial
    ''' </summary>
    ''' <param name="PartialPaymentsC"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmPartialPaymentsC(PartialPaymentsC As PartialPaymentsC, session As SessionValues) As Domain.Base.Entities.ActionResult(Of PartialPaymentsC) Implements IGlosasPartialPayments.ConfirmPartialPaymentsC
        Using PartialPaymentsCAdmin As IPartialPaymentsCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPartialPaymentsCAdminService)()
            Return PartialPaymentsCAdmin.ConfirmPartialPaymentsC(PartialPaymentsC, session)
        End Using
    End Function
    ''' <summary>
    ''' obtener pago parcial
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPartialPaymentsC(consecutive As String, session As SessionValues) As Domain.Base.Entities.ActionResult(Of PartialPaymentsC) Implements IGlosasPartialPayments.GetPartialPaymentsC
        Using PartialPaymentsCAdmin As IPartialPaymentsCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPartialPaymentsCAdminService)()
            Return PartialPaymentsCAdmin.GetPartialPaymentsC(consecutive, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' guardar pago parcial
    ''' </summary>
    ''' <param name="PartialPaymentsC"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePartialPaymentsC(PartialPaymentsC As PartialPaymentsC, session As SessionValues) As Domain.Base.Entities.ActionResult(Of PartialPaymentsC) Implements IGlosasPartialPayments.SavePartialPaymentsC
        Using PartialPaymentsCAdmin As IPartialPaymentsCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPartialPaymentsCAdminService)()
            Return PartialPaymentsCAdmin.SavePartialPaymentsC(PartialPaymentsC, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para anular un oficio de pago parcial
    ''' </summary>
    ''' <param name="PartialPaymentsC"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InvalidatePaymentsC(PartialPaymentsC As PartialPaymentsC, session As SessionValues) As Domain.Base.Entities.ActionResult(Of PartialPaymentsC) Implements IGlosasPartialPayments.InvalidatePaymentsC
        Using PartialPaymentsCAdmin As IPartialPaymentsCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPartialPaymentsCAdminService)()
            Return PartialPaymentsCAdmin.InvalidatePaymentsC(PartialPaymentsC, session.AuditMessageWcf)
        End Using
    End Function
#End Region

#Region "PartialPaymentsD"

    ''' <summary>
    ''' Eliminar factura de pago parcial
    ''' </summary>
    ''' <param name="tmpList"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeletePartialPaymentsD(tmpList As List(Of PartialPaymentsD), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasPartialPayments.DeletePartialPaymentsD
        Using PartialPaymentsCAdmin As IPartialPaymentsDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPartialPaymentsDAdminService)()
            Return PartialPaymentsCAdmin.DeletePartialPaymentsD(tmpList, session.AuditMessageWcf)
        End Using
    End Function

#End Region

#Region "PartialPaymentsMovements"

  

#End Region

   

End Class
