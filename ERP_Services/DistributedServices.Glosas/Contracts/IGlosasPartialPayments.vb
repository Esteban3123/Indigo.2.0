'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Rafael Eduardo Patiño
' Created          : 07-05-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface IGlosasPartialPayments


#Region "PartialPaymentsC"

    ''' <summary>
    ''' Obtener un oficio de pagos parciales por medio del consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function GetPartialPaymentsC(ByVal consecutive As String, ByVal session As SessionValues) As ActionResult(Of PartialPaymentsC)

    ''' <summary>
    ''' Confirmar un oficio de pagos parciales
    ''' </summary>
    ''' <param name="PartialPaymentsC"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ConfirmPartialPaymentsC(ByVal PartialPaymentsC As PartialPaymentsC, ByVal session As SessionValues) As ActionResult(Of PartialPaymentsC)

    ''' <summary>
    ''' guardar un oficio de radicacion de cuentas
    ''' </summary>
    ''' <param name="PartialPaymentsC">objeto radicacion de cuentas</param>
    ''' <param name="session">mensaje de auditoria</param>
    ''' <returns>valor de guardado</returns>
    <OperationContract>
    Function SavePartialPaymentsC(ByVal PartialPaymentsC As PartialPaymentsC, ByVal session As SessionValues) As ActionResult(Of PartialPaymentsC)


    ''' <summary>
    ''' Funcion para Anular un oficio
    ''' </summary>
    ''' <param name="PartialPaymentsC"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function InvalidatePaymentsC(PartialPaymentsC As PartialPaymentsC, session As SessionValues) As ActionResult(Of PartialPaymentsC)

#End Region

#Region "PartialpaymentsD"

    ''' <summary>
    ''' Funcion para eliminacion de facturas en pagos parciales
    ''' </summary>
    ''' <param name="tmpList">lista de item a eliminar</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function DeletePartialPaymentsD(ByVal tmpList As List(Of PartialPaymentsD), ByVal session As SessionValues) As ActionResult

#End Region

#Region "PartialPaymentsMovements"

#End Region
End Interface
