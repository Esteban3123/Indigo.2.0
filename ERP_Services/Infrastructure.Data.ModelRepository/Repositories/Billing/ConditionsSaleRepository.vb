'************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Andres Alarcon
' Created          : 14-06-2024
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class ConditionsSaleRepository
    Inherits GenericRepository(Of ConditionSales)
    Implements IConditionSalesRepository

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Lista todas las condiciones de venta por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Public Function ListConditionSalesByUserCode(userCode As String) As List(Of ConditionSales) Implements IConditionSalesRepository.ListConditionSalesByUserCode
        Return (From a As ConditionSales In Me._context.ConditionSales.Include("ConditionSalesUser")
                Where a.Status And a.ConditionSalesUser.Any(Function(u) u.UserCode.Equals(userCode))
                Select a).ToList()
    End Function

    ''' <summary>
    ''' Obtiene una condicion de venta por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetConditionSalesById(Id As Integer) As ConditionSales Implements IConditionSalesRepository.GetConditionSalesById
        Return (From a As ConditionSales
                   In Me._context.ConditionSales
                Where a.Id = Id
                Select a).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene una condicion de venta por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConditionSalesByCode(code As String) As ConditionSales Implements IConditionSalesRepository.GetConditionSalesByCode
        Return (From ba In _context.ConditionSales.Include("ConditionSalesUser")
                Where ba.Code.Equals(code.Trim())
                Select ba).FirstOrDefault
    End Function

End Class
