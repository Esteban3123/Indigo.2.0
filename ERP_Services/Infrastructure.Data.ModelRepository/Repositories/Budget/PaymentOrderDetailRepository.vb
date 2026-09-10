'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Juan Carlos Bermudez
' Created          : 28-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class PaymentOrderDetailRepository
    Inherits GenericRepository(Of PaymentOrderDetail)
    Implements IPaymentOrderDetailRepository

#Region "Properties"

    'Contexto de Budget
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Buider"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un detalle por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentOrderDetailById(id As Integer) As PaymentOrderDetail Implements IPaymentOrderDetailRepository.GetPaymentOrderDetailById
        Return (From pod In _context.PaymentOrderDetail Where pod.Id = id Select pod).FirstOrDefault()
    End Function

#End Region
    
End Class
