'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 28-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface IPaymentOrderDetailRepository
    Inherits IRepository(Of PaymentOrderDetail)
    ''' <summary>
    ''' obtiene un detalle por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentOrderDetailById(id As Integer) As PaymentOrderDetail
End Interface
